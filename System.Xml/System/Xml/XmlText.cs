using System;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x020000EF RID: 239
	public class XmlText : XmlCharacterData
	{
		// Token: 0x06000EA3 RID: 3747 RVA: 0x00040850 File Offset: 0x0003F850
		internal XmlText(string strData) : this(strData, null)
		{
		}

		// Token: 0x06000EA4 RID: 3748 RVA: 0x0004085A File Offset: 0x0003F85A
		protected internal XmlText(string strData, XmlDocument doc) : base(strData, doc)
		{
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06000EA5 RID: 3749 RVA: 0x00040864 File Offset: 0x0003F864
		public override string Name
		{
			get
			{
				return this.OwnerDocument.strTextName;
			}
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06000EA6 RID: 3750 RVA: 0x00040871 File Offset: 0x0003F871
		public override string LocalName
		{
			get
			{
				return this.OwnerDocument.strTextName;
			}
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06000EA7 RID: 3751 RVA: 0x0004087E File Offset: 0x0003F87E
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.Text;
			}
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06000EA8 RID: 3752 RVA: 0x00040884 File Offset: 0x0003F884
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

		// Token: 0x06000EA9 RID: 3753 RVA: 0x000408EC File Offset: 0x0003F8EC
		public override XmlNode CloneNode(bool deep)
		{
			return this.OwnerDocument.CreateTextNode(this.Data);
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06000EAA RID: 3754 RVA: 0x000408FF File Offset: 0x0003F8FF
		// (set) Token: 0x06000EAB RID: 3755 RVA: 0x00040908 File Offset: 0x0003F908
		public override string Value
		{
			get
			{
				return this.Data;
			}
			set
			{
				this.Data = value;
				XmlNode parentNode = this.parentNode;
				if (parentNode != null && parentNode.NodeType == XmlNodeType.Attribute)
				{
					XmlUnspecifiedAttribute xmlUnspecifiedAttribute = parentNode as XmlUnspecifiedAttribute;
					if (xmlUnspecifiedAttribute != null && !xmlUnspecifiedAttribute.Specified)
					{
						xmlUnspecifiedAttribute.SetSpecified(true);
					}
				}
			}
		}

		// Token: 0x06000EAC RID: 3756 RVA: 0x00040948 File Offset: 0x0003F948
		public virtual XmlText SplitText(int offset)
		{
			XmlNode parentNode = this.ParentNode;
			int length = this.Length;
			if (offset > length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (parentNode == null)
			{
				throw new InvalidOperationException(Res.GetString("Xdom_TextNode_SplitText"));
			}
			int count = length - offset;
			string text = this.Substring(offset, count);
			this.DeleteData(offset, count);
			XmlText xmlText = this.OwnerDocument.CreateTextNode(text);
			parentNode.InsertAfter(xmlText, this);
			return xmlText;
		}

		// Token: 0x06000EAD RID: 3757 RVA: 0x000409B4 File Offset: 0x0003F9B4
		public override void WriteTo(XmlWriter w)
		{
			w.WriteString(this.Data);
		}

		// Token: 0x06000EAE RID: 3758 RVA: 0x000409C2 File Offset: 0x0003F9C2
		public override void WriteContentTo(XmlWriter w)
		{
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06000EAF RID: 3759 RVA: 0x000409C4 File Offset: 0x0003F9C4
		internal override XPathNodeType XPNodeType
		{
			get
			{
				return XPathNodeType.Text;
			}
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06000EB0 RID: 3760 RVA: 0x000409C7 File Offset: 0x0003F9C7
		internal override bool IsText
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06000EB1 RID: 3761 RVA: 0x000409CA File Offset: 0x0003F9CA
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
