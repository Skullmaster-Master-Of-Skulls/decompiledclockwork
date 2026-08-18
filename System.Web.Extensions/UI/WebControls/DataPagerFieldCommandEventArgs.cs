using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000093 RID: 147
	public class DataPagerFieldCommandEventArgs : CommandEventArgs
	{
		// Token: 0x0600068B RID: 1675 RVA: 0x0001C3D6 File Offset: 0x0001A5D6
		public DataPagerFieldCommandEventArgs(DataPagerFieldItem item, object commandSource, CommandEventArgs originalArgs) : base(originalArgs)
		{
			this._item = item;
			this._commandSource = commandSource;
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x0600068C RID: 1676 RVA: 0x0001C3ED File Offset: 0x0001A5ED
		public object CommandSource
		{
			get
			{
				return this._commandSource;
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x0600068D RID: 1677 RVA: 0x0001C3F5 File Offset: 0x0001A5F5
		public DataPagerFieldItem Item
		{
			get
			{
				return this._item;
			}
		}

		// Token: 0x04000250 RID: 592
		private DataPagerFieldItem _item;

		// Token: 0x04000251 RID: 593
		private object _commandSource;
	}
}
