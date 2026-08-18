using System;

namespace Telerik.Web.UI
{
	// Token: 0x0200116F RID: 4463
	public class GridSortCommandHeaderContextMenuEventArgs : GridSortCommandEventArgs
	{
		// Token: 0x0600B5E8 RID: 46568 RVA: 0x00280A78 File Offset: 0x0027EC78
		public GridSortCommandHeaderContextMenuEventArgs(GridItem item, object commandSource, object argument, GridSortOrder newSortOrder) : base(item, commandSource, argument)
		{
			this.newSortOrder = newSortOrder;
		}

		// Token: 0x17003AD8 RID: 15064
		// (get) Token: 0x0600B5E9 RID: 46569 RVA: 0x00280A8B File Offset: 0x0027EC8B
		public override GridSortOrder NewSortOrder
		{
			get
			{
				return this.newSortOrder;
			}
		}

		// Token: 0x04002FF4 RID: 12276
		private GridSortOrder newSortOrder;
	}
}
