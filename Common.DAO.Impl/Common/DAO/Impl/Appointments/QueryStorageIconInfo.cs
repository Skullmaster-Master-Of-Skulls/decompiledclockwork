using System;

namespace TechnoPro.Common.DAO.Impl.Appointments
{
	// Token: 0x02000135 RID: 309
	public static class QueryStorageIconInfo
	{
		// Token: 0x04000559 RID: 1369
		internal const string QS_ICONINFO = "SELECT appointmenticoninfoid,iconindex,icontext,iconletteridentifier FROM appointmenticoninfo WHERE appointmenticoninfoid=@iconinfoid";

		// Token: 0x0400055A RID: 1370
		internal const string QS_ICONINFOS_ALL = "SELECT appointmenticoninfoid,iconindex,icontext,iconletteridentifier FROM appointmenticoninfo ORDER BY iconindex";

		// Token: 0x0400055B RID: 1371
		internal const string QD_ICONINFO = "DELETE FROM appointmenticoninfo WHERE appointmenticoninfoid=@iconinfoid";

		// Token: 0x0400055C RID: 1372
		internal const string QU_ICONINFO = "IF EXISTS(SELECT appointmenticoninfoid FROM appointmenticoninfo WHERE iconindex=@iconnum)\r\nBEGIN\r\n    UPDATE appointmenticoninfo SET icontext=@icontext,iconletteridentifier=@iconletteridentifier WHERE iconindex=@iconnum\r\n    SET @iconinfoidnew=(SELECT TOP 1 appointmenticoninfoid FROM appointmenticoninfo WHERE iconindex=@iconnum)\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO appointmenticoninfo (iconindex,icontext,iconletteridentifier) VALUES (@iconnum,@icontext,@iconletteridentifier)\r\n    SET @iconinfoidnew=SCOPE_IDENTITY()\r\nEND";
	}
}
