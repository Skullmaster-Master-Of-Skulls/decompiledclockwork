using System;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x020000FC RID: 252
	public class XmlCDataSection : XmlCharacterData
	{
		// Token: 0x06001175 RID: 4469 RVA: 0x0004995E File Offset: 0x00047B5E
		protected internal XmlCDataSection(string data, XmlDocument doc) : base(data, doc)
		{
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06001176 RID: 4470 RVA: 0x00049968 File Offset: 0x00047B68
		public override string Name
		{
			get
			{
				return this.OwnerDocument.strCDataSectionName;
			}
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06001177 RID: 4471 RVA: 0x00049975 File Offset: 0x00047B75
		public override string LocalName
		{
			get
			{
				return this.OwnerDocument.strCDataSectionName;
			}
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06001178 RID: 4472 RVA: 0x00049982 File Offset: 0x00047B82
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.CDATA;
			}
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06001179 RID: 4473 RVA: 0x00049988 File Offset: 0x00047B88
		public override XmlNode ParentNode
		{
			get
			{
				XmlNodeType nodeType = this.parentNode.NodeType;
				if (nodeType - XmlNodeType.Text > 1)
				{
					if (nodeType == XmlNodeType.Document)
					{
						return null;
					}
					if (nodeType - XmlNodeType.Whitespace > 1)
					{
						return this.parentNode;
					}
				}
				XmlNode parentNode = this.parentNode.parentNode;
				while (parentNode.IsText)
				{
					parentNode = parentNode.parentNode;
				}
				return parentNode;
			}
		}

		// Token: 0x0600117A RID: 4474 RVA: 0x000499DC File Offset: 0x00047BDC
		public override XmlNode CloneNode(bool deep)
		{
			return this.OwnerDocument.CreateCDataSection(this.Data);
		}

		// Token: 0x0600117B RID: 4475 RVA: 0x000499EF File Offset: 0x00047BEF
		public override void WriteTo(XmlWriter w)
		{
			w.WriteCData(this.Data);
		}

		// Token: 0x0600117C RID: 4476 RVA: 0x000499FD File Offset: 0x00047BFD
		public override void WriteContentTo(XmlWriter w)
		{
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x0600117D RID: 4477 RVA: 0x000499FF File Offset: 0x00047BFF
		internal override XPathNodeType XPNodeType
		{
			get
			{
				return XPathNodeType.Text;
			}
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x0600117E RID: 4478 RVA: 0x00049A02 File Offset: 0x00047C02
		internal override bool IsText
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x0600117F RID: 4479 RVA: 0x00049A05 File Offset: 0x00047C05
		public override XmlNode PreviousText
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
