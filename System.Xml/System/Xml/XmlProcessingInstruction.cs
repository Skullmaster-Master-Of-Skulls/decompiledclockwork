using System;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x020000ED RID: 237
	public class XmlProcessingInstruction : XmlLinkedNode
	{
		// Token: 0x06000E86 RID: 3718 RVA: 0x0004062F File Offset: 0x0003F62F
		protected internal XmlProcessingInstruction(string target, string data, XmlDocument doc) : base(doc)
		{
			this.target = target;
			this.data = data;
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06000E87 RID: 3719 RVA: 0x00040646 File Offset: 0x0003F646
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

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06000E88 RID: 3720 RVA: 0x0004065C File Offset: 0x0003F65C
		public override string LocalName
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06000E89 RID: 3721 RVA: 0x00040664 File Offset: 0x0003F664
		// (set) Token: 0x06000E8A RID: 3722 RVA: 0x0004066C File Offset: 0x0003F66C
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

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06000E8B RID: 3723 RVA: 0x00040675 File Offset: 0x0003F675
		public string Target
		{
			get
			{
				return this.target;
			}
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06000E8C RID: 3724 RVA: 0x0004067D File Offset: 0x0003F67D
		// (set) Token: 0x06000E8D RID: 3725 RVA: 0x00040688 File Offset: 0x0003F688
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

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06000E8E RID: 3726 RVA: 0x000406C9 File Offset: 0x0003F6C9
		// (set) Token: 0x06000E8F RID: 3727 RVA: 0x000406D1 File Offset: 0x0003F6D1
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

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06000E90 RID: 3728 RVA: 0x000406DA File Offset: 0x0003F6DA
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.ProcessingInstruction;
			}
		}

		// Token: 0x06000E91 RID: 3729 RVA: 0x000406DD File Offset: 0x0003F6DD
		public override XmlNode CloneNode(bool deep)
		{
			return this.OwnerDocument.CreateProcessingInstruction(this.target, this.data);
		}

		// Token: 0x06000E92 RID: 3730 RVA: 0x000406F6 File Offset: 0x0003F6F6
		public override void WriteTo(XmlWriter w)
		{
			w.WriteProcessingInstruction(this.target, this.data);
		}

		// Token: 0x06000E93 RID: 3731 RVA: 0x0004070A File Offset: 0x0003F70A
		public override void WriteContentTo(XmlWriter w)
		{
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06000E94 RID: 3732 RVA: 0x0004070C File Offset: 0x0003F70C
		internal override string XPLocalName
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06000E95 RID: 3733 RVA: 0x00040714 File Offset: 0x0003F714
		internal override XPathNodeType XPNodeType
		{
			get
			{
				return XPathNodeType.ProcessingInstruction;
			}
		}

		// Token: 0x040009A6 RID: 2470
		private string target;

		// Token: 0x040009A7 RID: 2471
		private string data;
	}
}
