namespace Sitecore.Pathfinder.Projects.Layouts
{
  using Sitecore.Pathfinder.Diagnostics;
  using Sitecore.Pathfinder.Projects.Files;
  using Sitecore.Pathfinder.Projects.Items;

  public class Rendering : ContentFile
  {
    public Rendering([NotNull] IProject project, [NotNull] ISourceFile sourceFile, [NotNull] Item item) : base(project, sourceFile)
    {
      this.Item = item;
    }

    [NotNull]
    public Item Item { get; }
  }
}