﻿namespace Sitecore.Pathfinder.Projects.Files
{
  using Sitecore.Pathfinder.Diagnostics;

  public class ContentFile : File
  {
    public ContentFile([NotNull] IProject project, [NotNull] ISourceFile sourceFile) : base(project, sourceFile)
    {
    }
  }
}