using System;

namespace System.ComponentModel.Design
{
	// Token: 0x0200019F RID: 415
	public class DesignerActionListsChangedEventArgs : EventArgs
	{
		// Token: 0x06000F4C RID: 3916 RVA: 0x00057A64 File Offset: 0x00055C64
		public DesignerActionListsChangedEventArgs(object relatedObject, DesignerActionListsChangedType changeType, DesignerActionListCollection actionLists)
		{
			this.relatedObject = relatedObject;
			this.changeType = changeType;
			this.actionLists = actionLists;
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06000F4D RID: 3917 RVA: 0x00057A81 File Offset: 0x00055C81
		public DesignerActionListsChangedType ChangeType
		{
			get
			{
				return this.changeType;
			}
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06000F4E RID: 3918 RVA: 0x00057A89 File Offset: 0x00055C89
		public object RelatedObject
		{
			get
			{
				return this.relatedObject;
			}
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06000F4F RID: 3919 RVA: 0x00057A91 File Offset: 0x00055C91
		public DesignerActionListCollection ActionLists
		{
			get
			{
				return this.actionLists;
			}
		}

		// Token: 0x040008EB RID: 2283
		private object relatedObject;

		// Token: 0x040008EC RID: 2284
		private DesignerActionListCollection actionLists;

		// Token: 0x040008ED RID: 2285
		private DesignerActionListsChangedType changeType;
	}
}
