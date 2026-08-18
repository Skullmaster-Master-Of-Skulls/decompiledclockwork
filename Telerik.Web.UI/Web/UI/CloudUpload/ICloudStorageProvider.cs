using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;

namespace Telerik.Web.UI.CloudUpload
{
	// Token: 0x020001A3 RID: 419
	internal interface ICloudStorageProvider
	{
		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x06000F57 RID: 3927
		// (set) Token: 0x06000F58 RID: 3928
		TimeSpan UncommitedFilesExpirationPeriod { get; set; }

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x06000F59 RID: 3929
		string SubFolderStructure { get; }

		// Token: 0x06000F5A RID: 3930
		void UploadFile(string keyName, NameValueCollection metaData, Stream fileStream);

		// Token: 0x06000F5B RID: 3931
		void UploadChunk(NameValueCollection config, Stream fileStream);

		// Token: 0x06000F5C RID: 3932
		void DeleteFile(string keyName);

		// Token: 0x06000F5D RID: 3933
		void CommitChunkUpload(IDictionary<string, object> config, NameValueCollection metaData);

		// Token: 0x06000F5E RID: 3934
		void EnsureStorageContainer();

		// Token: 0x06000F5F RID: 3935
		void EnsureWebClient();
	}
}
