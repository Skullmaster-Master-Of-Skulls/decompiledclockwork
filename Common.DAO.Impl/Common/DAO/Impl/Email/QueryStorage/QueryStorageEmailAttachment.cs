using System;

namespace TechnoPro.Common.DAO.Impl.Email.QueryStorage
{
	// Token: 0x020000D6 RID: 214
	public static class QueryStorageEmailAttachment
	{
		// Token: 0x040002F7 RID: 759
		internal const string QS_LOAD_ATTACHMENT = "SELECT fileid,filename,filebytes FROM emailtemplatefiles WHERE fileid=@fileid";

		// Token: 0x040002F8 RID: 760
		internal const string QD_ATTACHMENT = "DELETE FROM emailtemplatefiles WHERE fileid=@fileid";

		// Token: 0x040002F9 RID: 761
		internal const string QI_ATTACHMENT = "INSERT INTO emailtemplatefiles (filename,filebytes) VALUES (@fn,@bytes)\r\n SET @fileid= SCOPE_IDENTITY()";
	}
}
