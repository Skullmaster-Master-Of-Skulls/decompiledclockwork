using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;

namespace WebGrease.ImageAssemble
{
	// Token: 0x020001AF RID: 431
	internal sealed class ImageMap
	{
		// Token: 0x06001637 RID: 5687 RVA: 0x00080AA4 File Offset: 0x0007ECA4
		internal ImageMap(string mapFileName)
		{
			this.xdoc = new XDocument();
			this.Document.Declaration = new XDeclaration("1.0", "UTF-8", "UTF-8");
			this.root = new XElement("images");
			this.Document.AddFirst(this.root);
			this.mapFileName = mapFileName;
		}

		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x06001638 RID: 5688 RVA: 0x00080B19 File Offset: 0x0007ED19
		internal XDocument Document
		{
			get
			{
				return this.xdoc;
			}
		}

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x06001639 RID: 5689 RVA: 0x00080B21 File Offset: 0x0007ED21
		internal IList<string> SpritedFiles
		{
			get
			{
				return this.spritedFiles;
			}
		}

		// Token: 0x0600163A RID: 5690 RVA: 0x00080B2C File Offset: 0x0007ED2C
		internal void AppendToXml(string notAssembledFile, string comment)
		{
			if (this.notAssembledNode == null)
			{
				this.notAssembledNode = new XElement("output");
				this.notAssembledNode.SetAttributeValue("file", string.Empty);
				this.root.Add(this.notAssembledNode);
			}
			XElement xelement = new XElement("input");
			xelement.Add(new XElement("originalfile", notAssembledFile.ToLowerInvariant()));
			xelement.Add(new XElement("comment", comment));
			this.notAssembledNode.Add(xelement);
		}

		// Token: 0x0600163B RID: 5691 RVA: 0x00080BD0 File Offset: 0x0007EDD0
		internal void AppendToXml(string originalFile, string genFile, int width, int height, int posX, int posY, string comment, bool addOutputNode, ImagePosition? posSprite)
		{
			if (addOutputNode)
			{
				this.SpritedFiles.Add(genFile);
				this.currentOutputNode = new XElement("output");
				this.currentOutputNode.SetAttributeValue("file", genFile);
				this.root.Add(this.currentOutputNode);
			}
			XElement xelement = new XElement("input");
			xelement.Add(new XElement("originalfile", originalFile.ToLowerInvariant()));
			xelement.Add(new XElement("width", width.ToString(CultureInfo.InvariantCulture)));
			xelement.Add(new XElement("height", height.ToString(CultureInfo.InvariantCulture)));
			xelement.Add(new XElement("xposition", posX.ToString(CultureInfo.InvariantCulture)));
			xelement.Add(new XElement("yposition", posY.ToString(CultureInfo.InvariantCulture)));
			if (!string.IsNullOrEmpty(comment))
			{
				xelement.Add(new XElement("comment", comment));
			}
			if (posSprite != null)
			{
				xelement.Add(new XElement("positioninsprite", posSprite.ToString()));
			}
			this.currentOutputNode.Add(xelement);
		}

		// Token: 0x0600163C RID: 5692 RVA: 0x00080D2F File Offset: 0x0007EF2F
		internal void AppendPadding(string padding)
		{
			this.root.SetAttributeValue("padding", padding);
		}

		// Token: 0x0600163D RID: 5693 RVA: 0x00080D47 File Offset: 0x0007EF47
		private void SaveXmlMap()
		{
			if (!string.IsNullOrWhiteSpace(this.mapFileName))
			{
				this.Document.Save(this.mapFileName);
			}
		}

		// Token: 0x0600163E RID: 5694 RVA: 0x00080D94 File Offset: 0x0007EF94
		internal bool UpdateAssembledImageName(string oldName, string newName)
		{
			bool result = false;
			IEnumerable<XElement> source = from outNode in this.root.Elements("output")
			where (string)outNode.Attribute("file") == oldName
			select outNode;
			if (source.Count<XElement>() > 0)
			{
				XElement xelement = source.First<XElement>();
				xelement.Attribute("file").Value = newName;
				this.SaveXmlMap();
				result = true;
			}
			return result;
		}

		// Token: 0x0600163F RID: 5695 RVA: 0x00080E06 File Offset: 0x0007F006
		public void UpdateSize(string file, int width, int height)
		{
			this.UpdateOrSetOutputAttribute(file, "width", width.ToString(CultureInfo.InvariantCulture));
			this.UpdateOrSetOutputAttribute(file, "height", height.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x06001640 RID: 5696 RVA: 0x00080E64 File Offset: 0x0007F064
		private void UpdateOrSetOutputAttribute(string file, string attributeName, string value)
		{
			XElement xelement = this.root.Elements("output").FirstOrDefault((XElement e) => (string)e.Attribute("file") == file);
			if (xelement == null)
			{
				return;
			}
			XAttribute xattribute = xelement.Attribute(attributeName);
			if (xattribute == null)
			{
				xattribute = new XAttribute(attributeName, value);
				xelement.Add(xattribute);
				return;
			}
			xattribute.Value = value;
		}

		// Token: 0x04000BAF RID: 2991
		private const string XmlVersion = "1.0";

		// Token: 0x04000BB0 RID: 2992
		private const string XmlEncoding = "UTF-8";

		// Token: 0x04000BB1 RID: 2993
		private const string RootNode = "images";

		// Token: 0x04000BB2 RID: 2994
		private const string ImageNode = "input";

		// Token: 0x04000BB3 RID: 2995
		private const string OriginalFile = "originalfile";

		// Token: 0x04000BB4 RID: 2996
		private const string GeneratedFile = "file";

		// Token: 0x04000BB5 RID: 2997
		private const string Width = "width";

		// Token: 0x04000BB6 RID: 2998
		private const string Height = "height";

		// Token: 0x04000BB7 RID: 2999
		private const string XPosition = "xposition";

		// Token: 0x04000BB8 RID: 3000
		private const string YPosition = "yposition";

		// Token: 0x04000BB9 RID: 3001
		private const string PositionInSprite = "positioninsprite";

		// Token: 0x04000BBA RID: 3002
		private const string InputNode = "input";

		// Token: 0x04000BBB RID: 3003
		private const string OutputNode = "output";

		// Token: 0x04000BBC RID: 3004
		private const string CommentNode = "comment";

		// Token: 0x04000BBD RID: 3005
		private const string Padding = "padding";

		// Token: 0x04000BBE RID: 3006
		private readonly string mapFileName;

		// Token: 0x04000BBF RID: 3007
		private readonly XElement root;

		// Token: 0x04000BC0 RID: 3008
		private readonly XDocument xdoc;

		// Token: 0x04000BC1 RID: 3009
		private readonly IList<string> spritedFiles = new List<string>();

		// Token: 0x04000BC2 RID: 3010
		private XElement currentOutputNode;

		// Token: 0x04000BC3 RID: 3011
		private XElement notAssembledNode;
	}
}
