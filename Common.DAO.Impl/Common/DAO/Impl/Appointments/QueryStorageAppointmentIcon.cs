using System;

namespace TechnoPro.Common.DAO.Impl.Appointments
{
	// Token: 0x0200012E RID: 302
	public static class QueryStorageAppointmentIcon
	{
		// Token: 0x040004F7 RID: 1271
		internal const string QS_APPOINTMENTICONS_BY_APPOINTMENTID = "SELECT    ai.appiconid,ai.appointmentid,ai.iconnum,ai.screennum,\r\n            s.typecode,s.[description],s.shorttext,s.isactive,s.showasbutton,\r\n            ii.icontext,ii.iconletteridentifier,ii.appointmenticoninfoid\r\nFROM        appointmenticons ai LEFT JOIN appointmenticoninfo ii ON ii.iconindex=ai.iconnum\r\n            LEFT JOIN screens s ON s.screennum=ai.screennum\r\nWHERE       ai.appointmentid=@appid\r\nORDER BY ai.iconnum";

		// Token: 0x040004F8 RID: 1272
		internal const string QS_APPOINTMENTICONS_BY_APPOINTMENTIDS = "SELECT    ai.appiconid,ai.appointmentid,ai.iconnum,ai.screennum,\r\n            s.typecode,s.[description],s.shorttext,s.isactive,s.showasbutton,\r\n            ii.icontext,ii.iconletteridentifier,ii.appointmenticoninfoid\r\nFROM        appointmenticons ai LEFT JOIN appointmenticoninfo ii ON ii.iconindex=ai.iconnum\r\n            LEFT JOIN screens s ON s.screennum=ai.screennum\r\nWHERE       ai.appointmentid IN (SELECT orderid AS appointmentid FROM splitorderids(@appids,','))\r\nORDER BY ai.appointmentid,ai.iconnum";

		// Token: 0x040004F9 RID: 1273
		internal const string QS_APPOINTMENTICON_BY_APPID_ICONNUM = "SELECT    ai.appiconid,ai.appointmentid,ai.iconnum,ai.screennum,\r\n            s.typecode,s.[description],s.shorttext,s.isactive,s.showasbutton,\r\n            ii.icontext,ii.iconletteridentifier,ii.appointmenticoninfoid\r\nFROM        appointmenticons ai LEFT JOIN appointmenticoninfo ii ON ii.iconindex=ai.iconnum\r\n            LEFT JOIN screens s ON s.screennum=ai.screennum\r\nWHERE       ai.appointmentid=@appid AND ai.iconnum=@iconnum";

		// Token: 0x040004FA RID: 1274
		internal const string QS_APPOINTMENTICON_BY_AppointmentIconId = "SELECT    ai.appiconid,ai.appointmentid,ai.iconnum,ai.screennum,\r\n            s.typecode,s.[description],s.shorttext,s.isactive,s.showasbutton,\r\n            ii.icontext,ii.iconletteridentifier,ii.appointmenticoninfoid\r\nFROM        appointmenticons ai LEFT JOIN appointmenticoninfo ii ON ii.iconindex=ai.iconnum\r\n            LEFT JOIN screens s ON s.screennum=ai.screennum\r\nWHERE       ai.appiconid=@AppointmentIconId";

		// Token: 0x040004FB RID: 1275
		internal const string QS_APPOINTMENTICON_BY_ICONNUM = "SELECT    ai.appiconid,ai.appointmentid,ai.iconnum,ai.screennum,\r\n            s.typecode,s.[description],s.shorttext,s.isactive,s.showasbutton,\r\n            ii.icontext,ii.iconletteridentifier,ii.appointmenticoninfoid\r\nFROM        appointmenticons ai LEFT JOIN appointmenticoninfo ii ON ii.iconindex=ai.iconnum\r\n            LEFT JOIN screens s ON s.screennum=ai.screennum\r\nWHERE       ai.iconnum=@iconnum";

		// Token: 0x040004FC RID: 1276
		internal const string QD_APPOINTMENTICON = "DELETE FROM appointmenticons WHERE appointmentid=@appid AND iconnum=@iconnum";

		// Token: 0x040004FD RID: 1277
		internal const string QD_APPOINTMENTICONS_NOT_IN_LIST = "DELETE FROM appointmenticons WHERE appointmentid=@appid \r\n        AND NOT iconnum IN (SELECT orderid AS iconnum FROM splitorderids(@iconnums,','))";

		// Token: 0x040004FE RID: 1278
		internal const string QU_APPOINTMENTICON = "IF EXISTS(SELECT appiconid FROM appointmenticons WHERE appointmentid=@appid AND iconnum=@iconnum)\r\nBEGIN\r\n    UPDATE appointmenticons SET screennum=@screennum WHERE appointmentid=@appid AND iconnum=@iconnum\r\n    SET @appiconid=(SELECT TOP 1 appiconid FROM appointmenticons WHERE appointmentid=@appid AND iconnum=@iconnum)\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO appointmenticons (appointmentid,screennum,iconnum) VALUES (@appid,@screennum,@iconnum)\r\n    SET @appiconid=SCOPE_IDENTITY()\r\nEND";
	}
}
