using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Azure.Storage;

namespace TechnoPro.Common.ICore.Azure.Storage
{
	// Token: 0x020000D8 RID: 216
	public interface IAzureStorageManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060006A9 RID: 1705
		byte[] DownloadBlockBlob(Uri blobUri);

		// Token: 0x060006AA RID: 1706
		string DownloadTextBlockBlob(Uri blobUri);

		// Token: 0x060006AB RID: 1707
		string DownloadTextBlockBlob(Uri containerUri, string blobName);

		// Token: 0x060006AC RID: 1708
		Task<string> DownloadTextBlockBlobAsync(Uri containerUri, string blobName);

		// Token: 0x060006AD RID: 1709
		Task<string> DownloadTextBlockBlobAsync(Uri blobUri);

		// Token: 0x060006AE RID: 1710
		Task<byte[]> DownloadBlockBlobAsync(Uri blobUri);

		// Token: 0x060006AF RID: 1711
		byte[] DownloadBlockBlob(Uri containerUri, string blobName);

		// Token: 0x060006B0 RID: 1712
		Task<byte[]> DownloadBlockBlobAsync(Uri containerUri, string blobName);

		// Token: 0x060006B1 RID: 1713
		void DownloadBlobToFile(Uri blobUri, string filename);

		// Token: 0x060006B2 RID: 1714
		Task DownloadBlobToFileAsync(Uri blobUri, string filename);

		// Token: 0x060006B3 RID: 1715
		void DownloadBlobToFile(Uri containerUri, string blobName, string filename);

		// Token: 0x060006B4 RID: 1716
		Task DownloadBlobToFileAsync(Uri containerUri, string blobName, string filename);

		// Token: 0x060006B5 RID: 1717
		bool BlobExists(Uri containerUri, string blobName);

		// Token: 0x060006B6 RID: 1718
		IList<CloudBlobInfo> ListBlockBlobInfoInContainer(Uri containerUri);

		// Token: 0x060006B7 RID: 1719
		void WriteToAppendBlob(Uri containerUri, string blobName, string text);

		// Token: 0x060006B8 RID: 1720
		Task WriteToAppendBlobAsync(Uri containerUri, string blobName, string text);

		// Token: 0x060006B9 RID: 1721
		void WriteToAppendBlob(Uri appendBlobUri, string text);

		// Token: 0x060006BA RID: 1722
		Task WriteToAppendBlobAsync(Uri appendBlobUri, string text);

		// Token: 0x060006BB RID: 1723
		CloudBlobInfo GetBlobInfo(Uri blobUri);

		// Token: 0x060006BC RID: 1724
		Task<CloudBlobInfo> GetBlobInfoAsync(Uri blobUri);

		// Token: 0x060006BD RID: 1725
		CloudBlobInfo GetBlobInfo(Uri containerUri, string blobName);

		// Token: 0x060006BE RID: 1726
		Task<CloudBlobInfo> GetBlobInfoAsync(Uri containerUri, string blobName);

		// Token: 0x060006BF RID: 1727
		StreamingCloudBlob DownloadLargeBlob(Uri containerUri, string blobName);

		// Token: 0x060006C0 RID: 1728
		Task<StreamingCloudBlob> DownloadLargeBlobAsync(Uri containerUri, string blobName);

		// Token: 0x060006C1 RID: 1729
		StreamingCloudBlob DownloadLargeBlob(Uri blobUri);

		// Token: 0x060006C2 RID: 1730
		Task<StreamingCloudBlob> DownloadLargeBlobAsync(Uri blobUri);

		// Token: 0x060006C3 RID: 1731
		InMemoryCloudBlob DownloadBlob(Uri containerUri, string blobName);

		// Token: 0x060006C4 RID: 1732
		Task<InMemoryCloudBlob> DownloadBlobAsync(Uri containerUri, string blobName);

		// Token: 0x060006C5 RID: 1733
		InMemoryCloudBlob DownloadBlob(Uri blobUri);

		// Token: 0x060006C6 RID: 1734
		Task<InMemoryCloudBlob> DownloadBlobAsync(Uri blobUri);

		// Token: 0x060006C7 RID: 1735
		CloudBlobInfo UploadLargeBlob(StreamingCloudBlob blob);

		// Token: 0x060006C8 RID: 1736
		Task<CloudBlobInfo> UploadLargeBlobAsync(StreamingCloudBlob blob);

		// Token: 0x060006C9 RID: 1737
		CloudBlobInfo UploadBlob(InMemoryCloudBlob blob);

		// Token: 0x060006CA RID: 1738
		Task<CloudBlobInfo> UploadBlobAsync(InMemoryCloudBlob blob);

		// Token: 0x060006CB RID: 1739
		void DeleteBlob(Uri blobUri);

		// Token: 0x060006CC RID: 1740
		void DeleteBlob(Uri ContainerUri, string blobName);

		// Token: 0x060006CD RID: 1741
		Task DeleteBlobAsync(Uri blobUri);

		// Token: 0x060006CE RID: 1742
		Task DeleteBlobAsync(Uri ContainerUri, string blobName);
	}
}
