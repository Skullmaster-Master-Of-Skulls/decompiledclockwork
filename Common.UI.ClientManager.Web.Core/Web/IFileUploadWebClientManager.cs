using System;
using TechnoPro.Common.UI.Web.Entity.Common.FileUpload;

namespace TechnoPro.Common.UI.ClientManager.Web.Core.Web
{
	// Token: 0x02000005 RID: 5
	public interface IFileUploadWebClientManager
	{
		// Token: 0x06000005 RID: 5
		void RemoveFileForUploadSessionInfo(string guid);

		// Token: 0x06000006 RID: 6
		void ClearOldFileForUploadSessionInfos();

		// Token: 0x06000007 RID: 7
		FileForUploadSet GetFileForUploadInfoFromSession(string guid);

		// Token: 0x06000008 RID: 8
		void UpdateFileForUploadInfoInSession(FileForUploadSet fileForUploadInfo);

		// Token: 0x06000009 RID: 9
		FileForUploadSet CreateNewFileForUploadInfoInSession();

		// Token: 0x0600000A RID: 10
		FileForUploadSet CreateNewFileForUploadInfoInSession(string guid);

		// Token: 0x0600000B RID: 11
		FileForUpload AddFileForUpload(string guid, string filename, byte[] fileBytes);

		// Token: 0x0600000C RID: 12
		void DeleteFileForUpload(string guid, int fileForUploadId);
	}
}
