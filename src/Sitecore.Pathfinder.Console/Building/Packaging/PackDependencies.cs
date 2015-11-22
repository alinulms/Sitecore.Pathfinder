﻿// © 2015 Sitecore Corporation A/S. All rights reserved.

using System;
using System.IO;
using System.Text;
using NuGet;
using Sitecore.Pathfinder.Diagnostics;
using Sitecore.Pathfinder.Extensions;
using Sitecore.Pathfinder.IO;

namespace Sitecore.Pathfinder.Building.Packaging
{
    public class PackDependencies : BuildTaskBase
    {
        public PackDependencies() : base("pack-dependencies")
        {
        }

        public override void Run(IBuildContext context)
        {
            context.Trace.TraceInformation(Texts.Packing_dependency_Sitecore_packages_in_Nuget_packages___);

            var packagesDirectory = context.Configuration.Get(Constants.Configuration.CopyDependenciesSourceDirectory);
            var sourceDirectory = Path.Combine(context.ProjectDirectory, packagesDirectory);
            if (!context.FileSystem.DirectoryExists(sourceDirectory))
            {
                context.Trace.TraceInformation(Texts.Dependencies_directory_not_found__Skipping, packagesDirectory);
                return;
            }

            foreach (var fileName in context.FileSystem.GetFiles(sourceDirectory, "*.zip"))
            {
                Pack(context, fileName);
            }
        }

        public override void WriteHelp(HelpWriter helpWriter)
        {
            helpWriter.Summary.Write("Creates a Nuget package for Sitecore package in the sitecore.tools\\packages directory.");
            helpWriter.Remarks.Write("Creates a Nuget package for Sitecore package in the sitecore.tools\\packages directory.");
        }

        private void Pack([NotNull] IBuildContext context, [NotNull] string zipFileName)
        {
            var packageName = Path.GetFileNameWithoutExtension(zipFileName);
            var packageId = packageName.GetSafeCodeIdentifier();

            var srcFileName = PathHelper.UnmapPath(context.ProjectDirectory, zipFileName);
            var targetFileName = "content\\packages\\" + Path.GetFileName(zipFileName);

            context.Trace.TraceInformation(Texts.Packing, packageName);

            var nuspec = new StringWriter();
            nuspec.WriteLine("<package xmlns=\"http://schemas.microsoft.com/packaging/2010/07/nuspec.xsd\">");
            nuspec.WriteLine("    <metadata>");
            nuspec.WriteLine("        <id>" + packageId + "</id>");
            nuspec.WriteLine("        <title>" + packageName + "</title>");
            nuspec.WriteLine("        <version>1.0.0</version>");
            nuspec.WriteLine("        <authors>Sitecore Pathfinder</authors>");
            nuspec.WriteLine("        <requireLicenseAcceptance>false</requireLicenseAcceptance>");
            nuspec.WriteLine("        <description>Generated by Sitecore Pathfinder</description>");
            nuspec.WriteLine("    </metadata>");
            nuspec.WriteLine("    <files>");
            nuspec.WriteLine("        <file src=\"" + srcFileName + "\" target=\"" + targetFileName + "\"/>");
            nuspec.WriteLine("    </files>");
            nuspec.WriteLine("</package>");

            var nupkgFileName = Path.Combine(Path.GetDirectoryName(zipFileName) ?? string.Empty, packageId + ".nupkg");

            try
            {
                var byteArray = Encoding.UTF8.GetBytes(nuspec.ToString());
                using (var nuspecStream = new MemoryStream(byteArray))
                {
                    var packageBuilder = new PackageBuilder(nuspecStream, context.ProjectDirectory);

                    using (var nupkg = new FileStream(nupkgFileName, FileMode.Create))
                    {
                        packageBuilder.Save(nupkg);
                    }
                }
            }
            catch (Exception ex)
            {
                context.Trace.TraceError(Msg.M1003, Texts.Failed_to_create_the_Nupkg_file, ex.Message);
            }
        }
    }
}
