using System;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x02000120 RID: 288
	public class XmlWhitespace : XmlCharacterData
	{
		// Token: 0x06001452 RID: 5202 RVA: 0x00053EFF File Offset: 0x000520FF
		protected internal XmlWhitespace(string strData, XmlDocument doc) : base(strData, doc)
		{
			if (!doc.IsLoading && !base.CheckOnData(strData))
			{
				throw new ArgumentException(Res.GetString("Xdom_WS_Char"));
			}
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x06001453 RID: 5203 RVA: 0x00053F2A File Offset: 0x0005212A
		public override string Name
		{
			get
			{
				return this.OwnerDocument.strNonSignificantWhitespaceName;
			}
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x06001454 RID: 5204 RVA: 0x00053F37 File Offset: 0x00052137
		public override string LocalName
		{
			get
			{
				return this.OwnerDocument.strNonSignificantWhitespaceName;
			}
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x06001455 RID: 5205 RVA: 0x00053F44 File Offset: 0x00052144
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.Whitespace;
			}
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06001456 RID: 5206 RVA: 0x00053F48 File Offset: 0x00052148
		public override XmlNode ParentNode
		{
			get
			{
				XmlNodeType nodeType = this.parentNode.NodeType;
				if (nodeType - XmlNodeType.Text > 1)
				{
					if (nodeType == XmlNodeType.Document)
					{
						return base.ParentNode;
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

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x06001457 RID: 5207 RVA: 0x00053FA1 File Offset: 0x000521A1
		// (set) Token: 0x06001458 RID: 5208 RVA: 0x00053FA9 File Offset: 0x000521A9
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

		// Token: 0x06001459 RID: 5209 RVA: 0x00053FCB File Offset: 0x000521CB
		public override XmlNode CloneNode(bool deep)
		{
			return this.OwnerDocument.CreateWhitespace(this.Data);
		}

		// Token: 0x0600145A RID: 5210 RVA: 0x00053FDE File Offset: 0x000521DE
		public override void WriteTo(XmlWriter w)
		{
			w.WriteWhitespace(this.Data);
		}

		// Token: 0x0600145B RID: 5211 RVA: 0x00053FEC File Offset: 0x000521EC
		public override void WriteContentTo(XmlWriter w)
		{
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x0600145C RID: 5212 RVA: 0x00053FF0 File Offset: 0x000521F0
		internal override XPathNodeType XPNodeType
		{
			get
			{
				XPathNodeType result = XPathNodeType.Whitespace;
				base.DecideXPNodeTypeForTextNodes(this, ref result);
				return result;
			}
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x0600145D RID: 5213 RVA: 0x0005400A File Offset: 0x0005220A
		internal override bool IsText
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x0600145E RID: 5214 RVA: 0x0005400D File Offset: 0x0005220D
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
