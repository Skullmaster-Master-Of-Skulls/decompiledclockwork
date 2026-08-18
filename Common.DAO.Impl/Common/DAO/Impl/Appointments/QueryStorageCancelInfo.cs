using System;

namespace TechnoPro.Common.DAO.Impl.Appointments
{
	// Token: 0x02000133 RID: 307
	public static class QueryStorageCancelInfo
	{
		// Token: 0x04000551 RID: 1361
		internal const string QS_CANCELINFO_BY_APP_ID = "SELECT    acr.appointmentid,acr.cancelreasonid,acr.cancelreasontext,\r\n            acr.cancelledbypersonid AS personid,p.firstname,p.lastname,p.student_no,\r\n            acr.cancelleddate,cr.cancelreasongroupname,cr.cancelreasontitle,cr.cancelreasondescription,\r\n            cr.colour AS cancelreasoncolour,cr.ordernum AS cancelreasonordernum,cr.isactive AS cancelreasonisactive\r\nFROM        appointmentcancelledreason acr LEFT JOIN cancelreason cr ON cr.cancelreasonid=acr.cancelreasonid\r\n            LEFT JOIN people p ON p.personid=acr.cancelledbypersonid";

		// Token: 0x04000552 RID: 1362
		internal const string QD_CANCELINFO = "DELETE FROM appointmentcancelledreason WHERE appointmentid=@appid";

		// Token: 0x04000553 RID: 1363
		internal const string QI_INSERT_OR_UPDATE_APP_CANCEL_INFO = "IF EXISTS(SELECT appointmentid FROM appointmentcancelledreason WHERE appointmentid=@appid)\r\nBEGIN\r\n    UPDATE appointmentcancelledreason SET cancelreasonid=@cancelreasonid,cancelreasontext=@canceltext \r\n        WHERE appointmentid=@appid\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO appointmentcancelledreason (appointmentid,cancelreasonid,cancelreasontext,cancelledbypersonid,cancelleddate)\r\n        VALUES (@appid,@cancelreasonid,@canceltext,@whoami,getdate())\r\nEND";
	}
}
