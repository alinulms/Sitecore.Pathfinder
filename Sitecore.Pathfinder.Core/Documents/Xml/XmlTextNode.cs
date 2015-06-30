﻿// © 2015 Sitecore Corporation A/S. All rights reserved.

using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Sitecore.Pathfinder.Diagnostics;

namespace Sitecore.Pathfinder.Documents.Xml
{
    public class XmlTextNode : TextNode
    {
        private readonly XObject _node;

        private ITextNode _innerText;

        public XmlTextNode([NotNull] ITextSnapshot snapshot, [NotNull] XElement element, [CanBeNull] ITextNode parent = null) : base(snapshot, GetPosition(element, element.Name.LocalName.Length), element.Name.LocalName, string.Empty, parent)
        {
            _node = element;
        }

        public XmlTextNode([NotNull] ITextSnapshot snapshot, [NotNull] XAttribute attribute, [CanBeNull] ITextNode parent) : base(snapshot, GetPosition(attribute, attribute.Name.LocalName.Length), attribute.Name.LocalName, attribute.Value, parent)
        {
            _node = attribute;
        }

        public XmlTextNode([NotNull] ITextSnapshot snapshot, [NotNull] XNode node, [NotNull] string name, [NotNull] string value, [CanBeNull] ITextNode parent = null) : base(snapshot, GetPosition(node, value.Length), name, value, parent)
        {
            _node = node;
        }

        private static TextPosition GetPosition([NotNull] IXmlLineInfo lineInfo, int lineLength)
        {
            return new TextPosition(lineInfo.LineNumber, lineInfo.LinePosition, lineLength);
        }

        public override ITextNode GetInnerTextNode()
        {
            var element = _node as XElement;
            if (element != null)
            {
                return _innerText ?? (_innerText = new InnerTextNode(this, element));
            }

            return null;
        }

        public override bool SetName(string newName)
        {
            var element = _node as XElement;
            if (element != null)
            {
                element.Name = newName;
                Snapshot.IsModified = true;
                return true;
            }

            var attribute = _node as XAttribute;
            if (attribute != null)
            {
                var parent = attribute.Parent;
                if (parent == null)
                {
                    return false;
                }

                var newAttribute = new XAttribute(newName, attribute.Value);

                var attributes = parent.Attributes().ToList();
                var n = attributes.IndexOf(attribute);

                attributes.RemoveAt(n);
                attributes.Insert(n, newAttribute);

                parent.ReplaceAttributes(attributes);

                Snapshot.IsModified = true;
                return true;
            }

            return false;
        }

        public override bool SetValue(string value)
        {
            var element = _node as XElement;
            if (element != null)
            {
                element.Value = value;
                Snapshot.IsModified = true;
                return true;
            }

            var attribute = _node as XAttribute;
            if (attribute != null)
            {
                attribute.Value = value;
                Snapshot.IsModified = true;
                return true;
            }

            var node = _node as XNode;
            if (node?.Parent != null)
            {
                node.Parent.Value = value;
                Snapshot.IsModified = true;
                return true;
            }

            return false;
        }
    }
}
