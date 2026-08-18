using System;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x020000CF RID: 207
	public class XmlCDataSection : XmlCharacterData
	{
		// Token: 0x06000C50 RID: 3152 RVA: 0x00037E88 File Offset: 0x00036E88
		protected internal XmlCDataSection(string data, XmlDocument doc) : base(data, doc)
		{
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000C51 RID: 3153 RVA: 0x00037E92 File Offset: 0x00036E92
		public override string Name
		{
			get
			{
				return this.OwnerDocument.strCDataSectionName;
			}
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000C52 RID: 3154 RVA: 0x00037E9F File Offset: 0x00036E9F
		public override string LocalName
		{
			get
			{
				return this.OwnerDocument.strCDataSectionName;
			}
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000C53 RID: 3155 RVA: 0x00037EAC File Offset: 0x00036EAC
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.CDATA;
			}
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06000C54 RID: 3156 RVA: 0x00037EB0 File Offset: 0x00036EB0
		public override XmlNode ParentNode
		{
			get
			{
				XmlNodeType nodeType = this.parentNode.NodeType;
				switch (nodeType)
				{
				case XmlNodeType.Text:
				case XmlNodeType.CDATA:
					break;
				default:
					if (nodeType == XmlNodeType.Document)
					{
						return null;
					}
					switch (nodeType)
					{
					case XmlNodeType.Whitespace:
					case XmlNodeType.SignificantWhitespace:
						break;
					default:
						return this.parentNode;
					}
					break;
				}
				XmlNode parentNode = this.parentNode.parentNode;
				while (parentNode.IsText)
				{
					parentNode = parentNode.parentNode;
				}
				return parentNode;
			}
		}

		// Token: 0x06000C55 RID: 3157 RVA: 0x00037F18 File Offset: 0x00036F18
		public override XmlNode CloneNode(bool deep)
		{
			return this.OwnerDocument.CreateCDataSection(this.Data);
		}

		// Token: 0x06000C56 RID: 3158 RVA: 0x00037F2B File Offset: 0x00036F2B
		public override void WriteTo(XmlWriter w)
		{
			w.WriteCData(this.Data);
		}

		// Token: 0x06000C57 RID: 3159 RVA: 0x00037F39 File Offset: 0x00036F39
		public override void WriteContentTo(XmlWriter w)
		{
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000C58 RID: 3160 RVA: 0x00037F3B File Offset: 0x00036F3B
		internal override XPathNodeType XPNodeType
		{
			get
			{
				return XPathNodeType.Text;
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000C59 RID: 3161 RVA: 0x00037F3E File Offset: 0x00036F3E
		internal override bool IsText
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000C5A RID: 3162 RVA: 0x00037F41 File Offset: 0x00036F41
		internal override XmlNode PreviousText
		{
			get
			{
				if (this.parentNode.IsText)
				{
					return this.parentNode;
				}
				return null;
			}
		}
	}
}
