using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000F24 RID: 3876
	public class TreeListUpdatedEventArgs : TreeListDataChangeEventArgs
	{
		// Token: 0x060093EC RID: 37868 RVA: 0x00212DF3 File Offset: 0x00210FF3
		public TreeListUpdatedEventArgs(int affectedRows, Exception e, TreeListEditableItem item) : base(affectedRows, e, item)
		{
			this.KeepInEditMode = false;
		}

		// Token: 0x17002ECB RID: 11979
		// (get) Token: 0x060093ED RID: 37869 RVA: 0x00212E05 File Offset: 0x00211005
		// (set) Token: 0x060093EE RID: 37870 RVA: 0x00212E0D File Offset: 0x0021100D
		public bool KeepInEditMode { get; set; }
	}
}
