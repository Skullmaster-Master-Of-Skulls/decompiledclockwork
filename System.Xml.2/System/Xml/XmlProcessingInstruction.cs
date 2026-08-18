using System;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x0200011C RID: 284
	public class XmlProcessingInstruction : XmlLinkedNode
	{
		// Token: 0x0600141B RID: 5147 RVA: 0x00053A55 File Offset: 0x00051C55
		protected internal XmlProcessingInstruction(string target, string data, XmlDocument doc) : base(doc)
		{
			this.target = target;
			this.data = data;
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x0600141C RID: 5148 RVA: 0x00053A6C File Offset: 0x00051C6C
		public override string Name
		{
			get
			{
				if (this.target != null)
				{
					return this.target;
				}
				return string.Empty;
			}
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x0600141D RID: 5149 RVA: 0x00053A82 File Offset: 0x00051C82
		public override string LocalName
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x0600141E RID: 5150 RVA: 0x00053A8A File Offset: 0x00051C8A
		// (set) Token: 0x0600141F RID: 5151 RVA: 0x00053A92 File Offset: 0x00051C92
		public override string Value
		{
			get
			{
				return this.data;
			}
			set
			{
				this.Data = value;
			}
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06001420 RID: 5152 RVA: 0x00053A9B File Offset: 0x00051C9B
		public string Target
		{
			get
			{
				return this.target;
			}
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06001421 RID: 5153 RVA: 0x00053AA3 File Offset: 0x00051CA3
		// (set) Token: 0x06001422 RID: 5154 RVA: 0x00053AAC File Offset: 0x00051CAC
		public string Data
		{
			get
			{
				return this.data;
			}
			set
			{
				XmlNode parentNode = this.ParentNode;
				XmlNodeChangedEventArgs eventArgs = this.GetEventArgs(this, parentNode, parentNode, this.data, value, XmlNodeChangedAction.Change);
				if (eventArgs != null)
				{
					this.BeforeEvent(eventArgs);
				}
				this.data = value;
				if (eventArgs != null)
				{
					this.AfterEvent(eventArgs);
				}
			}
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06001423 RID: 5155 RVA: 0x00053AED File Offset: 0x00051CED
		// (set) Token: 0x06001424 RID: 5156 RVA: 0x00053AF5 File Offset: 0x00051CF5
		public override string InnerText
		{
			get
			{
				return this.data;
			}
			set
			{
				this.Data = value;
			}
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06001425 RID: 5157 RVA: 0x00053AFE File Offset: 0x00051CFE
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.ProcessingInstruction;
			}
		}

		// Token: 0x06001426 RID: 5158 RVA: 0x00053B01 File Offset: 0x00051D01
		public override XmlNode CloneNode(bool deep)
		{
			return this.OwnerDocument.CreateProcessingInstruction(this.target, this.data);
		}

		// Token: 0x06001427 RID: 5159 RVA: 0x00053B1A File Offset: 0x00051D1A
		public override void WriteTo(XmlWriter w)
		{
			w.WriteProcessingInstruction(this.target, this.data);
		}

		// Token: 0x06001428 RID: 5160 RVA: 0x00053B2E File Offset: 0x00051D2E
		public override void WriteContentTo(XmlWriter w)
		{
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06001429 RID: 5161 RVA: 0x00053B30 File Offset: 0x00051D30
		internal override string XPLocalName
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x0600142A RID: 5162 RVA: 0x00053B38 File Offset: 0x00051D38
		internal override XPathNodeType XPNodeType
		{
			get
			{
				return XPathNodeType.ProcessingInstruction;
			}
		}

		// Token: 0x04000586 RID: 1414
		private string target;

		// Token: 0x04000587 RID: 1415
		private string data;
	}
}
