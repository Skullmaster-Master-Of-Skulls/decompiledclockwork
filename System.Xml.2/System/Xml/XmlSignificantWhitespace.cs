using System;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x0200011D RID: 285
	public class XmlSignificantWhitespace : XmlCharacterData
	{
		// Token: 0x0600142B RID: 5163 RVA: 0x00053B3B File Offset: 0x00051D3B
		protected internal XmlSignificantWhitespace(string strData, XmlDocument doc) : base(strData, doc)
		{
			if (!doc.IsLoading && !base.CheckOnData(strData))
			{
				throw new ArgumentException(Res.GetString("Xdom_WS_Char"));
			}
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x0600142C RID: 5164 RVA: 0x00053B66 File Offset: 0x00051D66
		public override string Name
		{
			get
			{
				return this.OwnerDocument.strSignificantWhitespaceName;
			}
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x0600142D RID: 5165 RVA: 0x00053B73 File Offset: 0x00051D73
		public override string LocalName
		{
			get
			{
				return this.OwnerDocument.strSignificantWhitespaceName;
			}
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x0600142E RID: 5166 RVA: 0x00053B80 File Offset: 0x00051D80
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.SignificantWhitespace;
			}
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x0600142F RID: 5167 RVA: 0x00053B84 File Offset: 0x00051D84
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

		// Token: 0x06001430 RID: 5168 RVA: 0x00053BDD File Offset: 0x00051DDD
		public override XmlNode CloneNode(bool deep)
		{
			return this.OwnerDocument.CreateSignificantWhitespace(this.Data);
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06001431 RID: 5169 RVA: 0x00053BF0 File Offset: 0x00051DF0
		// (set) Token: 0x06001432 RID: 5170 RVA: 0x00053BF8 File Offset: 0x00051DF8
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

		// Token: 0x06001433 RID: 5171 RVA: 0x00053C1A File Offset: 0x00051E1A
		public override void WriteTo(XmlWriter w)
		{
			w.WriteString(this.Data);
		}

		// Token: 0x06001434 RID: 5172 RVA: 0x00053C28 File Offset: 0x00051E28
		public override void WriteContentTo(XmlWriter w)
		{
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06001435 RID: 5173 RVA: 0x00053C2C File Offset: 0x00051E2C
		internal override XPathNodeType XPNodeType
		{
			get
			{
				XPathNodeType result = XPathNodeType.SignificantWhitespace;
				base.DecideXPNodeTypeForTextNodes(this, ref result);
				return result;
			}
		}

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x06001436 RID: 5174 RVA: 0x00053C46 File Offset: 0x00051E46
		internal override bool IsText
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06001437 RID: 5175 RVA: 0x00053C49 File Offset: 0x00051E49
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
