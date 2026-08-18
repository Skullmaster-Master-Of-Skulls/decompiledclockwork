using System;

namespace TechnoPro.Common.DAO.Impl.QueryStorage
{
	// Token: 0x0200011A RID: 282
	internal class QueryStorageMisc
	{
		// Token: 0x040004B5 RID: 1205
		internal const string SQ_MISC_SAFE_KEYS_BY_VALUE = "select safekey from miscsafe where safevalue = @safevalue";

		// Token: 0x040004B6 RID: 1206
		internal const string SQ_MISC_SAFE_BY_KEY = "select safevalue from miscsafe where safekey = @safekey";

		// Token: 0x040004B7 RID: 1207
		internal const string IQ_MISC_SAFE = "if not exists(select 1 from miscsafe where safekey=@safekey)\r\n            begin\r\n                insert into miscsafe (safekey, safevalue) values(@safekey, @safevalue)\r\n            end\r\n            else\r\n            begin\r\n                update miscsafe set safevalue=@safevalue where safekey=@safekey\r\n            end";

		// Token: 0x040004B8 RID: 1208
		internal const string SQ_MISC_BY_KEY = "select miscstring from misc where misccode = @misckey";

		// Token: 0x040004B9 RID: 1209
		internal const string IQ_MISC = "if not exists(select 1 from misc where misccode=@misckey)\r\n            begin\r\n                insert into misc (misccode, miscstring) values(@misckey, @miscvalue)\r\n            end\r\n            else\r\n            begin\r\n                update misc set miscstring=@miscvalue where misccode=@misckey\r\n            end";
	}
}
