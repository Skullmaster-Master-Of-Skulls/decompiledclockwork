using System;

namespace TechnoPro.Common.DAO.Impl.Updates
{
	// Token: 0x02000030 RID: 48
	internal class QueryStorageUpdating
	{
		// Token: 0x04000073 RID: 115
		internal const string UQ_UPDATING_EXECUTION_STATUS = "IF not exists (select 1 FROM UpdatingSystem_UpdateStatus WHERE FileType=@filetype and AddSize=@addsize and IsPublic=@ispublic)\r\nbegin\r\n\tinsert into UpdatingSystem_UpdateStatus (FileType, AddSize, IsPublic, [Status], [Filename]) VALUES (@filetype, @addsize, @ispublic, @status, @filename)\r\nend\r\nelse\r\nbegin\r\n\tUPDATE UpdatingSystem_UpdateStatus SET [Status]=@status, [Filename]=@filename WHERE FileType=@filetype and AddSize=@addsize and IsPublic=@ispublic\r\nend";

		// Token: 0x04000074 RID: 116
		internal const string SQ_UPDATING_ALL_EXECUTION_STATUS = "select * from UpdatingSystem_UpdateStatus";

		// Token: 0x04000075 RID: 117
		internal const string SQ_UPDATING_EXECUTION_STATUS_BY_FILETYPE_AND_ADDSIZE = "select * from UpdatingSystem_UpdateStatus where FileType=@filetype and AddSize=@addsize and IsPublic=@ispublic";
	}
}
