using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001156 RID: 4438
	public class GridPageSizeChangedEventArgs : GridCommandEventArgs
	{
		// Token: 0x0600B4AB RID: 46251 RVA: 0x0027CB04 File Offset: 0x0027AD04
		public GridPageSizeChangedEventArgs(GridItem item, object commandSource, object argument, int newPageSize) : base(item, commandSource, "ChangePageSize", argument)
		{
			base.SetCommandSource(commandSource);
			this.newPageSize = newPageSize;
		}

		// Token: 0x17003A53 RID: 14931
		// (get) Token: 0x0600B4AC RID: 46252 RVA: 0x0027CB23 File Offset: 0x0027AD23
		public int NewPageSize
		{
			get
			{
				return this.newPageSize;
			}
		}

		// Token: 0x0600B4AD RID: 46253 RVA: 0x0027CB2B File Offset: 0x0027AD2B
		public override void ExecuteCommand(object source)
		{
			(source as GridTableView).OwnerGrid.CallOnPageSizeChanged(this);
		}

		// Token: 0x04002F9C RID: 12188
		private int newPageSize;
	}
}
