namespace Sitecore.Pathfinder.Projects.Items
{
  using System.Xml.Linq;
  using Sitecore.Pathfinder.Diagnostics;

  // todo: consider basing this on ProjectElement
  public class FieldModel
  {
    public FieldModel([NotNull] ISourceFile sourceFile)
    {
      this.SourceFile = sourceFile;
      this.Name = string.Empty;
      this.Language = string.Empty;
      this.Value = string.Empty;
    }

    [NotNull]
    public string Language { get; set; }

    [NotNull]
    public string Name { get; set; }

    [CanBeNull]
    public XElement SourceElement { get; set; }

    [NotNull]
    public ISourceFile SourceFile { get; }

    [NotNull]
    public string Value { get; set; }

    public int Version { get; set; }
  }
}