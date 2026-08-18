using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml;

namespace Telerik.Web.UI.Editor.TrackChanges
{
	// Token: 0x02000B58 RID: 2904
	internal class TrackChangesXmlAdapter : ITrackChangesAdapter
	{
		// Token: 0x06006D75 RID: 28021 RVA: 0x0019669B File Offset: 0x0019489B
		public TrackChangesXmlAdapter(RadEditor editor) : this(editor, new TrackChangesNamesProvider())
		{
		}

		// Token: 0x06006D76 RID: 28022 RVA: 0x001966A9 File Offset: 0x001948A9
		public TrackChangesXmlAdapter(RadEditor editor, ITrackChangesNamesProvider namesProvider)
		{
			this._editor = editor;
			this._namesProvider = namesProvider;
		}

		// Token: 0x06006D77 RID: 28023 RVA: 0x001966D6 File Offset: 0x001948D6
		public string AcceptChanges()
		{
			return this.HandleChanges(delegate(XmlDocumentFragment fragment)
			{
				this.AcceptDeleteChanges(fragment);
				this.AcceptInsertChanges(fragment);
				this.AcceptFormatChanges(fragment);
			});
		}

		// Token: 0x06006D78 RID: 28024 RVA: 0x00196701 File Offset: 0x00194901
		public string RejectChanges()
		{
			return this.HandleChanges(delegate(XmlDocumentFragment fragment)
			{
				this.RejectDeleteChanges(fragment);
				this.RejectInsertChanges(fragment);
				this.RejectFormatChanges(fragment);
			});
		}

		// Token: 0x170023E7 RID: 9191
		// (get) Token: 0x06006D79 RID: 28025 RVA: 0x00196715 File Offset: 0x00194915
		public ITrackChangesNamesProvider NamesProvider
		{
			get
			{
				return this._namesProvider;
			}
		}

		// Token: 0x06006D7A RID: 28026 RVA: 0x00196720 File Offset: 0x00194920
		private string HandleChanges(Action<XmlDocumentFragment> handler)
		{
			this._document = new XmlDocument();
			XmlElement xmlElement = this._document.CreateElement("root");
			XmlDocumentFragment xmlDocumentFragment = this._document.CreateDocumentFragment();
			xmlDocumentFragment.InnerXml = this._editor.Content;
			handler(xmlDocumentFragment);
			xmlElement.AppendChild(xmlDocumentFragment);
			return xmlElement.InnerXml;
		}

		// Token: 0x06006D7B RID: 28027 RVA: 0x0019677B File Offset: 0x0019497B
		private void AcceptDeleteChanges(XmlDocumentFragment fragment)
		{
			this.RemoveAllNodesByName(fragment, this._namesProvider.DeleteTagName);
		}

		// Token: 0x06006D7C RID: 28028 RVA: 0x0019678F File Offset: 0x0019498F
		private void AcceptInsertChanges(XmlDocumentFragment fragment)
		{
			this.StripAllNodesByName(fragment, this._namesProvider.InsertTagName);
		}

		// Token: 0x06006D7D RID: 28029 RVA: 0x001967A4 File Offset: 0x001949A4
		private void AcceptFormatChanges(XmlDocumentFragment fragment)
		{
			string text = string.Format("*[@{0}]", this._namesProvider.BrowserCommandAttribute);
			string xpath = "*//" + text;
			this.IterateNodes(fragment, text, new Action<XmlNode>(this.AcceptNodeFormatChange));
			this.IterateNodes(fragment, xpath, new Action<XmlNode>(this.AcceptNodeFormatChange));
		}

		// Token: 0x06006D7E RID: 28030 RVA: 0x001967FC File Offset: 0x001949FC
		private void AcceptNodeFormatChange(XmlNode node)
		{
			node.Attributes.RemoveNamedItem(this._namesProvider.AuthorAttribute);
			node.Attributes.RemoveNamedItem(this._namesProvider.BrowserCommandAttribute);
			node.Attributes.RemoveNamedItem(this._namesProvider.TimestampAttribute);
			node.Attributes.RemoveNamedItem(this._namesProvider.TitleAttribute);
			this.RemoveTrackChangesCssClasses(node);
		}

		// Token: 0x06006D7F RID: 28031 RVA: 0x0019686C File Offset: 0x00194A6C
		private void RejectDeleteChanges(XmlDocumentFragment fragment)
		{
			this.StripAllNodesByName(fragment, this._namesProvider.DeleteTagName);
		}

		// Token: 0x06006D80 RID: 28032 RVA: 0x00196880 File Offset: 0x00194A80
		private void RejectInsertChanges(XmlDocumentFragment fragment)
		{
			this.RemoveAllNodesByName(fragment, this._namesProvider.InsertTagName);
		}

		// Token: 0x06006D81 RID: 28033 RVA: 0x001969BC File Offset: 0x00194BBC
		private void RejectFormatChanges(XmlDocumentFragment fragment)
		{
			this.IterateNodes(fragment, string.Format("*[@{0}]", this._namesProvider.BrowserCommandAttribute), delegate(XmlNode node)
			{
				string value = node.Attributes.GetNamedItem(this._namesProvider.BrowserCommandAttribute).Value;
				if (value == "Outdent")
				{
					this.ReplaceTagName(fragment, node, "blockquote");
					return;
				}
				if (value.StartsWith("Justify"))
				{
					this.RemoveTrackChangesCssClasses(node);
					XmlAttribute xmlAttribute = (XmlAttribute)node.Attributes.GetNamedItem("style");
					this.ChangeStylesByPreviousAlign(xmlAttribute, node.Attributes.GetNamedItem(this._namesProvider.AlignOrigAttribute).Value);
					XmlNode xmlNode = this.ReplaceTagName(fragment, node, "div");
					XmlAttribute xmlAttribute2 = (XmlAttribute)node.Attributes.GetNamedItem("class");
					if (!string.IsNullOrEmpty(xmlAttribute.Value))
					{
						xmlNode.Attributes.Append(xmlAttribute);
					}
					if (xmlAttribute2 != null)
					{
						xmlNode.Attributes.Append(xmlAttribute2);
						return;
					}
				}
				else
				{
					this.StripNode(fragment, node);
				}
			});
		}

		// Token: 0x06006D82 RID: 28034 RVA: 0x00196A0C File Offset: 0x00194C0C
		private void ChangeStylesByPreviousAlign(XmlNode styleAttr, string originalAlign)
		{
			string replacement;
			if (originalAlign == "none")
			{
				replacement = "";
			}
			else
			{
				replacement = string.Format("text-align: {0};", originalAlign);
			}
			styleAttr.Value = TrackChangesXmlAdapter.textAlignRegEx.Replace(styleAttr.Value, replacement).Trim();
		}

		// Token: 0x06006D83 RID: 28035 RVA: 0x00196A5C File Offset: 0x00194C5C
		private void RemoveTrackChangesCssClasses(XmlNode node)
		{
			XmlAttribute xmlAttribute = (XmlAttribute)node.Attributes.GetNamedItem("class");
			xmlAttribute.Value = this.ParseOutTrackChangesCssClasses(xmlAttribute.Value);
			if (xmlAttribute.Value == string.Empty)
			{
				node.Attributes.Remove(xmlAttribute);
			}
		}

		// Token: 0x06006D84 RID: 28036 RVA: 0x00196AB0 File Offset: 0x00194CB0
		private string ParseOutTrackChangesCssClasses(string classValue)
		{
			List<string> list = new List<string>(classValue.Split(new char[]
			{
				' '
			}));
			if (list.Count > 2)
			{
				list.RemoveRange(0, 2);
				return string.Join(" ", list.ToArray());
			}
			return string.Empty;
		}

		// Token: 0x06006D85 RID: 28037 RVA: 0x00196B0C File Offset: 0x00194D0C
		private void RemoveAllNodesByName(XmlDocumentFragment fragment, string nodeName)
		{
			this.IterateNodes(fragment, string.Format("//{0}", nodeName), delegate(XmlNode n)
			{
				n.ParentNode.RemoveChild(n);
			});
		}

		// Token: 0x06006D86 RID: 28038 RVA: 0x00196B5C File Offset: 0x00194D5C
		private void StripAllNodesByName(XmlDocumentFragment fragment, string nodeName)
		{
			this.IterateNodes(fragment, string.Format("//{0}", nodeName), delegate(XmlNode n)
			{
				this.StripNode(fragment, n);
			});
		}

		// Token: 0x06006D87 RID: 28039 RVA: 0x00196BA0 File Offset: 0x00194DA0
		private void StripNode(XmlDocumentFragment fragment, XmlNode node)
		{
			XmlDocumentFragment xmlDocumentFragment = this._document.CreateDocumentFragment();
			xmlDocumentFragment.InnerXml = node.InnerXml;
			XmlNode parentNode = node.ParentNode;
			parentNode.InsertBefore(xmlDocumentFragment, node);
			parentNode.RemoveChild(node);
		}

		// Token: 0x06006D88 RID: 28040 RVA: 0x00196BE0 File Offset: 0x00194DE0
		private XmlNode ReplaceTagName(XmlDocumentFragment fragment, XmlNode node, string newName)
		{
			XmlNode xmlNode = this._document.CreateNode(XmlNodeType.Element, newName, this._document.NamespaceURI);
			xmlNode.InnerXml = node.InnerXml;
			node.ParentNode.ReplaceChild(xmlNode, node);
			return xmlNode;
		}

		// Token: 0x06006D89 RID: 28041 RVA: 0x00196C24 File Offset: 0x00194E24
		private void IterateNodes(XmlDocumentFragment fragment, string xpath, Action<XmlNode> callback)
		{
			XmlNodeList xmlNodeList = fragment.SelectNodes(xpath);
			foreach (object obj in xmlNodeList)
			{
				XmlNode obj2 = (XmlNode)obj;
				callback(obj2);
			}
		}

		// Token: 0x04001D9C RID: 7580
		private readonly RadEditor _editor;

		// Token: 0x04001D9D RID: 7581
		private XmlDocument _document;

		// Token: 0x04001D9E RID: 7582
		private readonly ITrackChangesNamesProvider _namesProvider;

		// Token: 0x04001D9F RID: 7583
		private static readonly Regex textAlignRegEx = new Regex("text-align: ?\\w+;?", RegexOptions.IgnoreCase | RegexOptions.Compiled);
	}
}
