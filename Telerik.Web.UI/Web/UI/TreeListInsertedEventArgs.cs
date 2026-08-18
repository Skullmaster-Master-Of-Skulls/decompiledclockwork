using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000F25 RID: 3877
	public class TreeListInsertedEventArgs : TreeListDataChangeEventArgs
	{
		// Token: 0x060093EF RID: 37871 RVA: 0x00212E16 File Offset: 0x00211016
		public TreeListInsertedEventArgs(int affectedRows, Exception e, TreeListEditableItem item) : base(affectedRows, e, item)
		{
			this.KeepInInsertMode = false;
		}

		// Token: 0x17002ECC RID: 11980
		// (get) Token: 0x060093F0 RID: 37872 RVA: 0x00212E28 File Offset: 0x00211028
		// (set) Token: 0x060093F1 RID: 37873 RVA: 0x00212E30 File Offset: 0x00211030
		public bool KeepInInsertMode { get; set; }
	}
}
