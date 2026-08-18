using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x020000AB RID: 171
	public class ListViewCommandEventArgs : CommandEventArgs
	{
		// Token: 0x060008A7 RID: 2215 RVA: 0x0002219C File Offset: 0x0002039C
		public ListViewCommandEventArgs(ListViewItem item, object commandSource, CommandEventArgs originalArgs) : base(originalArgs)
		{
			this._item = item;
			this._commandSource = commandSource;
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x060008A8 RID: 2216 RVA: 0x000221B3 File Offset: 0x000203B3
		public object CommandSource
		{
			get
			{
				return this._commandSource;
			}
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x060008A9 RID: 2217 RVA: 0x000221BB File Offset: 0x000203BB
		public ListViewItem Item
		{
			get
			{
				return this._item;
			}
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x060008AA RID: 2218 RVA: 0x000221C3 File Offset: 0x000203C3
		// (set) Token: 0x060008AB RID: 2219 RVA: 0x000221CB File Offset: 0x000203CB
		public bool Handled { get; set; }

		// Token: 0x040002D5 RID: 725
		private ListViewItem _item;

		// Token: 0x040002D6 RID: 726
		private object _commandSource;
	}
}
