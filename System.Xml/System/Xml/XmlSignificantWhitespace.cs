using System;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x020000EE RID: 238
	public class XmlSignificantWhitespace : XmlCharacterData
	{
		// Token: 0x06000E96 RID: 3734 RVA: 0x00040717 File Offset: 0x0003F717
		protected internal XmlSignificantWhitespace(string strData, XmlDocument doc) : base(strData, doc)
		{
			if (!doc.IsLoading && !base.CheckOnData(strData))
			{
				throw new ArgumentException(Res.GetString("Xdom_WS_Char"));
			}
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06000E97 RID: 3735 RVA: 0x00040742 File Offset: 0x0003F742
		public override string Name
		{
			get
			{
				return this.OwnerDocument.strSignificantWhitespaceName;
			}
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06000E98 RID: 3736 RVA: 0x0004074F File Offset: 0x0003F74F
		public override string LocalName
		{
			get
			{
				return this.OwnerDocument.strSignificantWhitespaceName;
			}
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06000E99 RID: 3737 RVA: 0x0004075C File Offset: 0x0003F75C
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.SignificantWhitespace;
			}
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06000E9A RID: 3738 RVA: 0x00040760 File Offset: 0x0003F760
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
						return base.ParentNode;
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

		// Token: 0x06000E9B RID: 3739 RVA: 0x000407CD File Offset: 0x0003F7CD
		public override XmlNode CloneNode(bool deep)
		{
			return this.OwnerDocument.CreateSignificantWhitespace(this.Data);
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06000E9C RID: 3740 RVA: 0x000407E0 File Offset: 0x0003F7E0
		// (set) Token: 0x06000E9D RID: 3741 RVA: 0x000407E8 File Offset: 0x0003F7E8
		public override string Value
		{
			get
			{
				return this.Data;
			}
			set
			{
				if (base.CheckOnData(value))
				{
					this.Data = value;
					return;
				}
				throw new ArgumentException(Res.GetString("Xdom_WS_Char"));
			}
		}

		// Token: 0x06000E9E RID: 3742 RVA: 0x0004080A File Offset: 0x0003F80A
		public override void WriteTo(XmlWriter w)
		{
			w.WriteString(this.Data);
		}

		// Token: 0x06000E9F RID: 3743 RVA: 0x00040818 File Offset: 0x0003F818
		public override void WriteContentTo(XmlWriter w)
		{
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06000EA0 RID: 3744 RVA: 0x0004081C File Offset: 0x0003F81C
		internal override XPathNodeType XPNodeType
		{
			get
			{
				XPathNodeType result = XPathNodeType.SignificantWhitespace;
				base.DecideXPNodeTypeForTextNodes(this, ref result);
				return result;
			}
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06000EA1 RID: 3745 RVA: 0x00040836 File Offset: 0x0003F836
		internal override bool IsText
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06000EA2 RID: 3746 RVA: 0x00040839 File Offset: 0x0003F839
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
