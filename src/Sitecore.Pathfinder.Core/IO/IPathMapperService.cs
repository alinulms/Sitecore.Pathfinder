﻿// © 2015 Sitecore Corporation A/S. All rights reserved.

using System.Collections.Generic;
using Sitecore.Pathfinder.Diagnostics;
using Sitecore.Pathfinder.IO.PathMappers;

namespace Sitecore.Pathfinder.IO
{
    public interface IPathMapperService
    {
        [NotNull, ItemNotNull]
        ICollection<ProjectDirectoryToWebsiteDirectoryMapper> ProjectDirectoryToWebsiteDirectories { get; }

        [NotNull, ItemNotNull]
        ICollection<ProjectDirectoryToWebsiteItemPathMapper> ProjectDirectoryToWebsiteItemPaths { get; }

        [NotNull, ItemNotNull]
        ICollection<ProjectFileNameToWebsiteFileNameMapper> ProjectFileNameToWebsiteFileNames { get; }

        [NotNull, ItemNotNull]
        ICollection<WebsiteDirectoryToProjectDirectoryMapper> WebsiteDirectoryToProjectDirectories { get; }

        [NotNull, ItemNotNull]
        ICollection<WebsiteItemPathToProjectDirectoryMapper> WebsiteItemPathToProjectDirectories { get; }

        void Clear();

        bool TryGetProjectFileName([NotNull] string itemPath, [NotNull] string templateName, [NotNull] out string projectFileName, [NotNull] out string format);

        bool TryGetProjectFileName([NotNull] string websiteFileName, [NotNull] out string projectFileName);

        bool TryGetWebsiteFileName([NotNull] string projectFileName, [NotNull] out string websiteFileName);

        bool TryGetWebsiteItemPath([NotNull] string projectFileName, [NotNull] out string databaseName, [NotNull] out string itemPath, out bool isImport, out bool uploadMedia);
    }
}
