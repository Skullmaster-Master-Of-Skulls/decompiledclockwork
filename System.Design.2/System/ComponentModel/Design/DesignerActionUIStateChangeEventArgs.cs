using System;

namespace System.ComponentModel.Design
{
	// Token: 0x020001A9 RID: 425
	public class DesignerActionUIStateChangeEventArgs : EventArgs
	{
		// Token: 0x06000FB4 RID: 4020 RVA: 0x00059C82 File Offset: 0x00057E82
		public DesignerActionUIStateChangeEventArgs(object relatedObject, DesignerActionUIStateChangeType changeType)
		{
			this.relatedObject = relatedObject;
			this.changeType = changeType;
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06000FB5 RID: 4021 RVA: 0x00059C98 File Offset: 0x00057E98
		public DesignerActionUIStateChangeType ChangeType
		{
			get
			{
				return this.changeType;
			}
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06000FB6 RID: 4022 RVA: 0x00059CA0 File Offset: 0x00057EA0
		public object RelatedObject
		{
			get
			{
				return this.relatedObject;
			}
		}

		// Token: 0x0400092D RID: 2349
		private object relatedObject;

		// Token: 0x0400092E RID: 2350
		private DesignerActionUIStateChangeType changeType;
	}
}
