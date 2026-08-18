using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200044A RID: 1098
	public interface IRepeatInfoUser
	{
		// Token: 0x17000F6B RID: 3947
		// (get) Token: 0x06003514 RID: 13588
		bool HasHeader { get; }

		// Token: 0x17000F6C RID: 3948
		// (get) Token: 0x06003515 RID: 13589
		bool HasFooter { get; }

		// Token: 0x17000F6D RID: 3949
		// (get) Token: 0x06003516 RID: 13590
		bool HasSeparators { get; }

		// Token: 0x17000F6E RID: 3950
		// (get) Token: 0x06003517 RID: 13591
		int RepeatedItemCount { get; }

		// Token: 0x06003518 RID: 13592
		Style GetItemStyle(ListItemType itemType, int repeatIndex);

		// Token: 0x06003519 RID: 13593
		void RenderItem(ListItemType itemType, int repeatIndex, RepeatInfo repeatInfo, HtmlTextWriter writer);
	}
}
