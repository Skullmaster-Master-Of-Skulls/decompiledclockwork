using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000F23 RID: 3875
	public class TreeListDataChangeEventArgs : EventArgs
	{
		// Token: 0x060093E3 RID: 37859 RVA: 0x00212D8B File Offset: 0x00210F8B
		public TreeListDataChangeEventArgs(int affectedRows, Exception e, TreeListEditableItem item)
		{
			this.AffectedRows = affectedRows;
			this.ExceptionHandled = false;
			this.Exception = e;
			this.Item = item;
		}

		// Token: 0x17002EC7 RID: 11975
		// (get) Token: 0x060093E4 RID: 37860 RVA: 0x00212DAF File Offset: 0x00210FAF
		// (set) Token: 0x060093E5 RID: 37861 RVA: 0x00212DB7 File Offset: 0x00210FB7
		public int AffectedRows { get; private set; }

		// Token: 0x17002EC8 RID: 11976
		// (get) Token: 0x060093E6 RID: 37862 RVA: 0x00212DC0 File Offset: 0x00210FC0
		// (set) Token: 0x060093E7 RID: 37863 RVA: 0x00212DC8 File Offset: 0x00210FC8
		public Exception Exception { get; private set; }

		// Token: 0x17002EC9 RID: 11977
		// (get) Token: 0x060093E8 RID: 37864 RVA: 0x00212DD1 File Offset: 0x00210FD1
		// (set) Token: 0x060093E9 RID: 37865 RVA: 0x00212DD9 File Offset: 0x00210FD9
		public TreeListEditableItem Item { get; private set; }

		// Token: 0x17002ECA RID: 11978
		// (get) Token: 0x060093EA RID: 37866 RVA: 0x00212DE2 File Offset: 0x00210FE2
		// (set) Token: 0x060093EB RID: 37867 RVA: 0x00212DEA File Offset: 0x00210FEA
		public bool ExceptionHandled { get; set; }
	}
}
