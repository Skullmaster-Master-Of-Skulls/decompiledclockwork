using System;

namespace System.Xml
{
	// Token: 0x020000E7 RID: 231
	public class XmlNodeChangedEventArgs : EventArgs
	{
		// Token: 0x06000E01 RID: 3585 RVA: 0x0003E3E5 File Offset: 0x0003D3E5
		public XmlNodeChangedEventArgs(XmlNode node, XmlNode oldParent, XmlNode newParent, string oldValue, string newValue, XmlNodeChangedAction action)
		{
			this.node = node;
			this.oldParent = oldParent;
			this.newParent = newParent;
			this.action = action;
			this.oldValue = oldValue;
			this.newValue = newValue;
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06000E02 RID: 3586 RVA: 0x0003E41A File Offset: 0x0003D41A
		public XmlNodeChangedAction Action
		{
			get
			{
				return this.action;
			}
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06000E03 RID: 3587 RVA: 0x0003E422 File Offset: 0x0003D422
		public XmlNode Node
		{
			get
			{
				return this.node;
			}
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06000E04 RID: 3588 RVA: 0x0003E42A File Offset: 0x0003D42A
		public XmlNode OldParent
		{
			get
			{
				return this.oldParent;
			}
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06000E05 RID: 3589 RVA: 0x0003E432 File Offset: 0x0003D432
		public XmlNode NewParent
		{
			get
			{
				return this.newParent;
			}
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06000E06 RID: 3590 RVA: 0x0003E43A File Offset: 0x0003D43A
		public string OldValue
		{
			get
			{
				return this.oldValue;
			}
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06000E07 RID: 3591 RVA: 0x0003E442 File Offset: 0x0003D442
		public string NewValue
		{
			get
			{
				return this.newValue;
			}
		}

		// Token: 0x0400097C RID: 2428
		private XmlNodeChangedAction action;

		// Token: 0x0400097D RID: 2429
		private XmlNode node;

		// Token: 0x0400097E RID: 2430
		private XmlNode oldParent;

		// Token: 0x0400097F RID: 2431
		private XmlNode newParent;

		// Token: 0x04000980 RID: 2432
		private string oldValue;

		// Token: 0x04000981 RID: 2433
		private string newValue;
	}
}
