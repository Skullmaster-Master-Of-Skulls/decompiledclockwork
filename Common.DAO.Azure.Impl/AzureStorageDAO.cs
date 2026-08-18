using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Blob;
using TechnoPro.Common.DAO.Azure.Storage;
using TechnoPro.Common.Public.Entities.Azure.Storage;

namespace TechnoPro.Common.DAO.Azure.Impl
{
	// Token: 0x02000002 RID: 2
	public class AzureStorageDAO : IAzureStorageDAO
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public byte[] DownloadBlobckBlob(string containerSAS, string blobName)
		{
			CloudBlobContainer container = this.GetContainer(containerSAS);
			CloudBlockBlob blockBlobReference = container.GetBlockBlobReference(blobName);
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				blockBlobReference.DownloadToStream(memoryStream, null, null, null);
				result = memoryStream.ToArray();
			}
			return result;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000021F0 File Offset: 0x000003F0
		public async Task<byte[]> DownloadBlobAsync(string containerSAS, string blobName)
		{
			CloudBlobContainer container = this.GetContainer(containerSAS);
			CloudBlockBlob blob = container.GetBlockBlobReference(blobName);
			byte[] result;
			using (MemoryStream ms = new MemoryStream())
			{
				await blob.DownloadToStreamAsync(ms);
				result = ms.ToArray();
			}
			return result;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002248 File Offset: 0x00000448
		public void DownloadBlobToFile(string containerSAS, string blobName, string filename)
		{
			CloudBlobContainer container = this.GetContainer(containerSAS);
			CloudBlockBlob blockBlobReference = container.GetBlockBlobReference(blobName);
			blockBlobReference.DownloadToFile(filename, FileMode.Create, null, null, null);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002374 File Offset: 0x00000574
		public async Task DownloadBlobToFileAsync(string containerSAS, string blobName, string filename)
		{
			CloudBlobContainer container = this.GetContainer(containerSAS);
			CloudBlockBlob blob = container.GetBlockBlobReference(blobName);
			await blob.DownloadToFileAsync(filename, FileMode.Create);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000023D4 File Offset: 0x000005D4
		public bool BlobExists(string containerSAS, string blobName)
		{
			CloudBlobContainer container = this.GetContainer(containerSAS);
			CloudBlockBlob blockBlobReference = container.GetBlockBlobReference(blobName);
			return blockBlobReference.Exists(null, null);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002484 File Offset: 0x00000684
		public IList<CloudBlobInfo> ListBlockBlobInfoInContainer(string containerSAS)
		{
			CloudBlobContainer container = this.GetContainer(containerSAS);
			return (from item in container.ListBlobs(null, false, 0, null, null)
			where item.GetType() == typeof(CloudBlockBlob)
			select (CloudBlockBlob)item into blob
			select new CloudBlobInfo
			{
				BlobName = blob.Name,
				BlobUri = blob.Uri,
				ContainerName = ((blob.Container != null) ? blob.Container.Name : null),
				SizeinBytes = (int)blob.Properties.Length,
				LastModifiedTime = blob.Properties.LastModified
			}).ToList<CloudBlobInfo>();
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002510 File Offset: 0x00000710
		public void WriteToAppendBlob(string containerSAS, string blobName, string text)
		{
			CloudBlobContainer container = this.GetContainer(containerSAS);
			CloudAppendBlob appendBlobReference = container.GetAppendBlobReference(blobName);
			if (!appendBlobReference.Exists(null, null))
			{
				appendBlobReference.CreateOrReplace(null, null, null);
			}
			appendBlobReference.AppendText(text, null, null, null, null);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000254B File Offset: 0x0000074B
		private CloudBlobContainer GetContainer(string containerSAS)
		{
			return new CloudBlobContainer(new Uri(containerSAS));
		}
	}
}
