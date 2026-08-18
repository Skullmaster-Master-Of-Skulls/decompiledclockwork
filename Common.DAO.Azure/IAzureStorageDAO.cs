using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnoPro.Common.Public.Entities.Azure.Storage;

namespace TechnoPro.Common.DAO.Azure.Storage
{
	// Token: 0x02000002 RID: 2
	public interface IAzureStorageDAO
	{
		// Token: 0x06000001 RID: 1
		byte[] DownloadBlobckBlob(string containerSAS, string blobName);

		// Token: 0x06000002 RID: 2
		Task<byte[]> DownloadBlobAsync(string containerSAS, string blobName);

		// Token: 0x06000003 RID: 3
		void DownloadBlobToFile(string containerSAS, string blobName, string filename);

		// Token: 0x06000004 RID: 4
		Task DownloadBlobToFileAsync(string containerSAS, string blobName, string filename);

		// Token: 0x06000005 RID: 5
		bool BlobExists(string containerSAS, string blobName);

		// Token: 0x06000006 RID: 6
		IList<CloudBlobInfo> ListBlockBlobInfoInContainer(string containerSAS);

		// Token: 0x06000007 RID: 7
		void WriteToAppendBlob(string containerSAS, string blobName, string text);
	}
}
