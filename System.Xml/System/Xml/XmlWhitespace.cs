using System;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x020000F1 RID: 241
	public class XmlWhitespace : XmlCharacterData
	{
		// Token: 0x06000EBD RID: 3773 RVA: 0x00040B03 File Offset: 0x0003FB03
		protected internal XmlWhitespace(string strData, XmlDocument doc) : base(strData, doc)
		{
			if (!doc.IsLoading && !base.CheckOnData(strData))
			{
				throw new ArgumentException(Res.GetString("Xdom_WS_Char"));
			}
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06000EBE RID: 3774 RVA: 0x00040B2E File Offset: 0x0003FB2E
		public override string Name
		{
			get
			{
				return this.OwnerDocument.strNonSignificantWhitespaceName;
			}
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06000EBF RID: 3775 RVA: 0x00040B3B File Offset: 0x0003FB3B
		public override string LocalName
		{
			get
			{
				return this.OwnerDocument.strNonSignificantWhitespaceName;
			}
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06000EC0 RID: 3776 RVA: 0x00040B48 File Offset: 0x0003FB48
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.Whitespace;
			}
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06000EC1 RID: 3777 RVA: 0x00040B4C File Offset: 0x0003FB4C
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

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06000EC2 RID: 3778 RVA: 0x00040BB9 File Offset: 0x0003FBB9
		// (set) Token: 0x06000EC3 RID: 3779 RVA: 0x00040BC1 File Offset: 0x0003FBC1
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

		// Token: 0x06000EC4 RID: 3780 RVA: 0x00040BE3 File Offset: 0x0003FBE3
		public override XmlNode CloneNode(bool deep)
		{
			return this.OwnerDocument.CreateWhitespace(this.Data);
		}

		// Token: 0x06000EC5 RID: 3781 RVA: 0x00040BF6 File Offset: 0x0003FBF6
		public override void WriteTo(XmlWriter w)
		{
			w.WriteWhitespace(this.Data);
		}

		// Token: 0x06000EC6 RID: 3782 RVA: 0x00040C04 File Offset: 0x0003FC04
		public override void WriteContentTo(XmlWriter w)
		{
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06000EC7 RID: 3783 RVA: 0x00040C08 File Offset: 0x0003FC08
		internal override XPathNodeType XPNodeType
		{
			get
			{
				XPathNodeType result = XPathNodeType.Whitespace;
				base.DecideXPNodeTypeForTextNodes(this, ref result);
				return result;
			}
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06000EC8 RID: 3784 RVA: 0x00040C22 File Offset: 0x0003FC22
		internal override bool IsText
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06000EC9 RID: 3785 RVA: 0x00040C25 File Offset: 0x0003FC25
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
