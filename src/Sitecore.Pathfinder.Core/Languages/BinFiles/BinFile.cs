﻿// © 2015 Sitecore Corporation A/S. All rights reserved.

using Sitecore.Pathfinder.Diagnostics;
using Sitecore.Pathfinder.Projects;
using Sitecore.Pathfinder.Projects.Files;
using Sitecore.Pathfinder.Snapshots;

namespace Sitecore.Pathfinder.Languages.BinFiles
{
    public class BinFile : File
    {
        public BinFile([NotNull] IProject project, [NotNull] ISnapshot snapshot, [NotNull] string filePath) : base(project, snapshot, filePath)
        {
        }
    }
}
