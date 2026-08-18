using System;

namespace TechnoPro.Common.DAO.Impl.QueryStorage
{
	// Token: 0x0200011B RID: 283
	public static class QueryStorageUserCache
	{
		// Token: 0x040004BA RID: 1210
		internal const string SQ_ITEM = "select ItemValue from [CacheByUser] where UserID=@userid and ItemKey=@itemkey and (Expiry IS NULL OR Expiry>getdate())";

		// Token: 0x040004BB RID: 1211
		internal const string SQ_ITEMS_BY_KEYS = "select ItemKey, ItemValue from CacheByUser \r\n                where UserID = @userid \r\n                and ItemKey in (select OrderID as itemkey from SplitStrings(@itemkeys))\r\n                and (Expiry IS NULL OR Expiry>getdate())";

		// Token: 0x040004BC RID: 1212
		internal const string UQ_ITEM = "if exists(select 1 from CacheByUser where UserID=@userid and ItemKey=@itemkey)\r\n                begin\r\n\t                update cacheByUser set ItemValue = @itemvalue,Expiry=@expiry where UserID=@userid and ItemKey=@itemkey\r\n                end\r\n                else\r\n                begin\r\n\t                insert into CacheByUser (UserID, ItemKey, ItemValue, Expiry) values(@userid, @itemkey, @itemvalue, @expiry)\r\n                end";

		// Token: 0x040004BD RID: 1213
		internal const string DQ_DELETE_ALL_BY_USER_ID = "Delete from [CacheByUser] where UserID = @userid";

		// Token: 0x040004BE RID: 1214
		internal const string DQ_DELETE_ITEM_BY_USER_ID_AND_KEY = "DELETE from [CacheByUser] where UserID = @userid and ItemKey=@itemkey";

		// Token: 0x040004BF RID: 1215
		internal const string DQ_DELETE_ALL_ITEMS_BY_KEY = "DELETE from [CacheByUser] where ItemKey=@itemkey";
	}
}
