using System;

namespace TechnoPro.Common.DAO.Impl.Messaging
{
	// Token: 0x0200008B RID: 139
	internal static class QueryStorageDropBox
	{
		// Token: 0x0400019D RID: 413
		internal const string SQ_GET_DROPBOX_IM_BY_ID = "Select * from [Messaging_IMDropBox] where ID=@id";

		// Token: 0x0400019E RID: 414
		internal const string SQ_GET_ALL_DROPBOX_ATT_INFO = "select * from Messaging_AttachmentDropBox \r\n                                                              where [ToID]=@username";

		// Token: 0x0400019F RID: 415
		internal const string SQ_COUNT_IM = "select COUNT(*) as [Count] from Messaging_IMDropBox where [ToID]=@username";

		// Token: 0x040001A0 RID: 416
		internal const string SQ_COUNT_ATT = "select COUNT(*) as [Count] from Messaging_AttachmentDropBox where [ToID]=@username";

		// Token: 0x040001A1 RID: 417
		internal const string SQ_GET_ALL_DROPBOX_IM = "select * from Messaging_IMDropBox where [ToID]=@username";

		// Token: 0x040001A2 RID: 418
		internal const string SQ_GET_DROPBOX_ATT_BY_FILENAME = "Select * from [Messaging_AttachmentDropBox] where Filename=@filename and Extension=@extension";

		// Token: 0x040001A3 RID: 419
		internal const string SQ_GET_DROPBOX_ATT_BY_ID = "Select * from [Messaging_AttachmentDropBox] where ID=@id";

		// Token: 0x040001A4 RID: 420
		internal const string IQ_SAVE_DROPBOX_IM = "insert into Messaging_IMDropBox ([ToID], [FromID], [Message], IssuedOn, RequiredResponse, ReqReceivingConfirmation)\r\n              values (@to, @from, @message, @issuedon, @requiredresponse, @reqreceivingconfirmation)\r\n              set @id = SCOPE_IDENTITY()";

		// Token: 0x040001A5 RID: 421
		internal const string IQ_SAVE_DROPBOX_ATT = "insert into Messaging_AttachmentDropBox ([ToID], [FromID], [BinaryData], [IssuedOn], [Filename], [Extension], [Description], [ReqReceivingConfirmation], [SizeInBytes])\r\n              values (@to, @from, @binarydata, @issuedon, @filename, @extension, @description, @reqreceivingconfirmation, @sizeinbytes)\r\n              set @id = SCOPE_IDENTITY()";

		// Token: 0x040001A6 RID: 422
		internal const string DQ_DELETE_IM = "Delete from [Messaging_IMDropBox] where ID=@id";

		// Token: 0x040001A7 RID: 423
		internal const string DQ_DELETE_ATTACHMENT = "Delete from [Messaging_AttachmentDropBox] where ID=@id";
	}
}
