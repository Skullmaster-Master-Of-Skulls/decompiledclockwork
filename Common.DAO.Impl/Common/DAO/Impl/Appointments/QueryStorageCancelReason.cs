using System;

namespace TechnoPro.Common.DAO.Impl.Appointments
{
	// Token: 0x02000134 RID: 308
	public static class QueryStorageCancelReason
	{
		// Token: 0x04000554 RID: 1364
		internal const string QS_CANCELREASONS_ALL = "SELECT    c.cancelreasonid,c.cancelreasongroupname,c.cancelreasontitle,c.cancelreasondescription,\r\n            c.colour AS cancelreasoncolour,c.ordernum AS cancelreasonordernum,c.isactive AS cancelreasonisactive\r\nFROM        cancelreason c\r\nORDER BY    c.cancelreasontitle";

		// Token: 0x04000555 RID: 1365
		internal const string QS_CANCELREASON_BY_ID = "SELECT    c.cancelreasonid,c.cancelreasongroupname,c.cancelreasontitle,c.cancelreasondescription,\r\n            c.colour AS cancelreasoncolour,c.ordernum AS cancelreasonordernum,c.isactive AS cancelreasonisactive\r\nFROM        cancelreason c\r\nWHERE       c.cancelreasonid=@cancelreasonid";

		// Token: 0x04000556 RID: 1366
		internal const string QD_CANCELREASON = "DELETE FROM cancelreason WHERE cancelreasonid=@cancelreasonid";

		// Token: 0x04000557 RID: 1367
		internal const string QU_CANCELREASON = "UPDATE cancelreason SET cancelreasongroupname=@cancelreasongroup,cancelreasontitle=@cancelreasontitle,\r\n        colour=@colour,ordernum=@ordernum,isactive=@isactive\r\nWHERE cancelreasonid=@cancelreasonid";

		// Token: 0x04000558 RID: 1368
		internal const string QI_CANCELREASON = "INSERT INTO cancelreason (cancelreasongroupname,cancelreasontitle,cancelreasondescription,colour,ordernum,isactive)\r\nVALUES (@cancelreasongroup,@cancelreasontitle,'',@colour,@ordernum,@isactive);\r\nSET @cancelreasonid=SCOPE_IDENTITY()";
	}
}
