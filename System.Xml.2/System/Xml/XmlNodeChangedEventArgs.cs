using System;

namespace System.Xml
{
	// Token: 0x02000116 RID: 278
	public class XmlNodeChangedEventArgs : EventArgs
	{
		// Token: 0x0600138F RID: 5007 RVA: 0x000517C6 File Offset: 0x0004F9C6
		public XmlNodeChangedEventArgs(XmlNode node, XmlNode oldParent, XmlNode newParent, string oldValue, string newValue, XmlNodeChangedAction action)
		{
			this.node = node;
			this.oldParent = oldParent;
			this.newParent = newParent;
			this.action = action;
			this.oldValue = oldValue;
			this.newValue = newValue;
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06001390 RID: 5008 RVA: 0x000517FB File Offset: 0x0004F9FB
		public XmlNodeChangedAction Action
		{
			get
			{
				return this.action;
			}
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06001391 RID: 5009 RVA: 0x00051803 File Offset: 0x0004FA03
		public XmlNode Node
		{
			get
			{
				return this.node;
			}
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06001392 RID: 5010 RVA: 0x0005180B File Offset: 0x0004FA0B
		public XmlNode OldParent
		{
			get
			{
				return this.oldParent;
			}
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06001393 RID: 5011 RVA: 0x00051813 File Offset: 0x0004FA13
		public XmlNode NewParent
		{
			get
			{
				return this.newParent;
			}
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06001394 RID: 5012 RVA: 0x0005181B File Offset: 0x0004FA1B
		public string OldValue
		{
			get
			{
				return this.oldValue;
			}
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06001395 RID: 5013 RVA: 0x00051823 File Offset: 0x0004FA23
		public string NewValue
		{
			get
			{
				return this.newValue;
			}
		}

		// Token: 0x0400055E RID: 1374
		private XmlNodeChangedAction action;

		// Token: 0x0400055F RID: 1375
		private XmlNode node;

		// Token: 0x04000560 RID: 1376
		private XmlNode oldParent;

		// Token: 0x04000561 RID: 1377
		private XmlNode newParent;

		// Token: 0x04000562 RID: 1378
		private string oldValue;

		// Token: 0x04000563 RID: 1379
		private string newValue;
	}
}
