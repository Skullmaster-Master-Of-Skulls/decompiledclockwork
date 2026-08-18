using System;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x0200011E RID: 286
	public class XmlText : XmlCharacterData
	{
		// Token: 0x06001438 RID: 5176 RVA: 0x00053C60 File Offset: 0x00051E60
		internal XmlText(string strData) : this(strData, null)
		{
		}

		// Token: 0x06001439 RID: 5177 RVA: 0x00053C6A File Offset: 0x00051E6A
		protected internal XmlText(string strData, XmlDocument doc) : base(strData, doc)
		{
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x0600143A RID: 5178 RVA: 0x00053C74 File Offset: 0x00051E74
		public override string Name
		{
			get
			{
				return this.OwnerDocument.strTextName;
			}
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x0600143B RID: 5179 RVA: 0x00053C81 File Offset: 0x00051E81
		public override string LocalName
		{
			get
			{
				return this.OwnerDocument.strTextName;
			}
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x0600143C RID: 5180 RVA: 0x00053C8E File Offset: 0x00051E8E
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.Text;
			}
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x0600143D RID: 5181 RVA: 0x00053C94 File Offset: 0x00051E94
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

		// Token: 0x0600143E RID: 5182 RVA: 0x00053CE8 File Offset: 0x00051EE8
		public override XmlNode CloneNode(bool deep)
		{
			return this.OwnerDocument.CreateTextNode(this.Data);
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x0600143F RID: 5183 RVA: 0x00053CFB File Offset: 0x00051EFB
		// (set) Token: 0x06001440 RID: 5184 RVA: 0x00053D04 File Offset: 0x00051F04
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

		// Token: 0x06001441 RID: 5185 RVA: 0x00053D44 File Offset: 0x00051F44
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

		// Token: 0x06001442 RID: 5186 RVA: 0x00053DB0 File Offset: 0x00051FB0
		public override void WriteTo(XmlWriter w)
		{
			w.WriteString(this.Data);
		}

		// Token: 0x06001443 RID: 5187 RVA: 0x00053DBE File Offset: 0x00051FBE
		public override void WriteContentTo(XmlWriter w)
		{
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06001444 RID: 5188 RVA: 0x00053DC0 File Offset: 0x00051FC0
		internal override XPathNodeType XPNodeType
		{
			get
			{
				return XPathNodeType.Text;
			}
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06001445 RID: 5189 RVA: 0x00053DC3 File Offset: 0x00051FC3
		internal override bool IsText
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06001446 RID: 5190 RVA: 0x00053DC6 File Offset: 0x00051FC6
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
