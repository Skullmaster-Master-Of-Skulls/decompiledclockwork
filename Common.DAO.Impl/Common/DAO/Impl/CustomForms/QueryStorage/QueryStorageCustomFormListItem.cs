using System;

namespace TechnoPro.Common.DAO.Impl.CustomForms.QueryStorage
{
	// Token: 0x02000105 RID: 261
	public static class QueryStorageCustomFormListItem
	{
		// Token: 0x04000446 RID: 1094
		internal const string QS_CUSTOM_LIST_ITEMS_BY_GROUPID = "SELECT cli.CustomListItemId,cli.ItemCaption,cli.OrderNum FROM CustomListItem cli WHERE cli.CustomListGroupId=@groupid ORDER BY cli.OrderNum,cli.ItemCaption";

		// Token: 0x04000447 RID: 1095
		internal const string QS_CUSTOM_LIST_ITEM_BY_LISTID = "SELECT cli.CustomListItemId,cli.ItemCaption,cli.OrderNum FROM CustomListItem cli WHERE cli.CustomListItemId=@id";

		// Token: 0x04000448 RID: 1096
		internal const string QI_CUSTOM_LIST_GROUP = "INSERT INTO CustomListGroup (GroupCaption,IsHidden) OUTPUT inserted.CustomListGroupId AS groupid VALUES (@caption,0)";

		// Token: 0x04000449 RID: 1097
		internal const string QI_CUSTOM_LIST_ITEM = "INSERT INTO CustomListItem (CustomListGroupId,ItemCaption,OrderNum,IsHidden) OUTPUT inserted.CustomListItemId AS itemid VALUES (@groupid,@caption,@ordernum,0)";

		// Token: 0x0400044A RID: 1098
		internal const string QU_CUSTOM_LIST_ITEM = "UPDATE CustomListItem SET ItemCaption=@caption,OrderNum=@ordernum WHERE CustomListItemId=@id";

		// Token: 0x0400044B RID: 1099
		internal const string QU_CUSTOM_LIST_GROUP = "UPDATE CustomListGroup SET GroupCaption=@caption WHERE CustomListGroupId=@id";

		// Token: 0x0400044C RID: 1100
		internal const string QU_DISABLE_LIST_ITEM = "UPDATE CustomListItem SET IsHidden=@ishidden WHERE CustomListItemId=@id";

		// Token: 0x0400044D RID: 1101
		internal const string QU_DISABLE_LIST_GROUP = "UPDATE CustomListGroup SET IsHidden=@ishidden WHERE CustomListGroupId=@id";
	}
}
