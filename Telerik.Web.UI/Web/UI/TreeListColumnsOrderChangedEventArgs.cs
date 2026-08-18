using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000F21 RID: 3873
	public class TreeListColumnsOrderChangedEventArgs : EventArgs
	{
		// Token: 0x060093DC RID: 37852 RVA: 0x00212D3C File Offset: 0x00210F3C
		public TreeListColumnsOrderChangedEventArgs(TreeListReorderedColumn[] reorderedColumns)
		{
			this._reorderedColumns = reorderedColumns;
		}

		// Token: 0x17002EC4 RID: 11972
		// (get) Token: 0x060093DD RID: 37853 RVA: 0x00212D4B File Offset: 0x00210F4B
		public TreeListReorderedColumn[] ReorderedColumns
		{
			get
			{
				return this._reorderedColumns;
			}
		}

		// Token: 0x04002A63 RID: 10851
		private TreeListReorderedColumn[] _reorderedColumns;
	}
}
