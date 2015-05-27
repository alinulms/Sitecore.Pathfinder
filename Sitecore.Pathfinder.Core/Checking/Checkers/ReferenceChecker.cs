﻿namespace Sitecore.Pathfinder.Checking.Checkers
{
  using System.ComponentModel.Composition;
  using Sitecore.Pathfinder.Documents;

  [Export(typeof(IChecker))]
  public class ReferenceChecker : CheckerBase
  {
    public override void Check(ICheckerContext context)
    {
      foreach (var projectItem in context.Project.Items)
      {
        foreach (var reference in projectItem.References)
        {
          if (reference.IsValid)
          {
            continue;
          }

          var textNode = reference.SourceTextNode;

          if (textNode != null)
          {
            context.Trace.TraceWarning(Texts.Reference_not_found, projectItem.DocumentSnapshot.SourceFile.FileName, textNode.Position, reference.TargetQualifiedName);
          }
          else
          {
            context.Trace.TraceWarning(Texts.Reference_not_found, projectItem.DocumentSnapshot.SourceFile.FileName, TextPosition.Empty, reference.TargetQualifiedName);
          }
        }
      }
    }
  }
}