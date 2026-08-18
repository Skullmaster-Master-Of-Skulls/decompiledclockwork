using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Blob;
using TechnoPro.Common.ICore.Azure.Storage;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Azure.Storage;

namespace TechnoPro.Common.Core.Azure.Storage
{
	// Token: 0x02000002 RID: 2
	public class AzureStorageManager : IAzureStorageManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public AzureStorageManager()
		{
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
		public AzureStorageManager(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002068 File Offset: 0x00000268
		public byte[] DownloadBlockBlob(Uri blobUri)
		{
			CloudBlockBlob cloudBlockBlob = new CloudBlockBlob(blobUri);
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				cloudBlockBlob.DownloadToStream(memoryStream, null, null, null);
				result = memoryStream.ToArray();
			}
			return result;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020B0 File Offset: 0x000002B0
		public string DownloadTextBlockBlob(Uri blobUri)
		{
			return Encoding.UTF8.GetString(this.DownloadBlockBlob(blobUri));
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020C3 File Offset: 0x000002C3
		public string DownloadTextBlockBlob(Uri containerUri, string blobName)
		{
			return Encoding.UTF8.GetString(this.DownloadBlockBlob(containerUri, blobName));
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020D8 File Offset: 0x000002D8
		public Task<string> DownloadTextBlockBlobAsync(Uri containerUri, string blobName)
		{
			AzureStorageManager.<DownloadTextBlockBlobAsync>d__6 <DownloadTextBlockBlobAsync>d__;
			<DownloadTextBlockBlobAsync>d__.<>t__builder = AsyncTaskMethodBuilder<string>.Create();
			<DownloadTextBlockBlobAsync>d__.<>4__this = this;
			<DownloadTextBlockBlobAsync>d__.containerUri = containerUri;
			<DownloadTextBlockBlobAsync>d__.blobName = blobName;
			<DownloadTextBlockBlobAsync>d__.<>1__state = -1;
			<DownloadTextBlockBlobAsync>d__.<>t__builder.Start<AzureStorageManager.<DownloadTextBlockBlobAsync>d__6>(ref <DownloadTextBlockBlobAsync>d__);
			return <DownloadTextBlockBlobAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000212C File Offset: 0x0000032C
		public Task<string> DownloadTextBlockBlobAsync(Uri blobUri)
		{
			AzureStorageManager.<DownloadTextBlockBlobAsync>d__7 <DownloadTextBlockBlobAsync>d__;
			<DownloadTextBlockBlobAsync>d__.<>t__builder = AsyncTaskMethodBuilder<string>.Create();
			<DownloadTextBlockBlobAsync>d__.<>4__this = this;
			<DownloadTextBlockBlobAsync>d__.blobUri = blobUri;
			<DownloadTextBlockBlobAsync>d__.<>1__state = -1;
			<DownloadTextBlockBlobAsync>d__.<>t__builder.Start<AzureStorageManager.<DownloadTextBlockBlobAsync>d__7>(ref <DownloadTextBlockBlobAsync>d__);
			return <DownloadTextBlockBlobAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002178 File Offset: 0x00000378
		public Task<byte[]> DownloadBlockBlobAsync(Uri blobUri)
		{
			AzureStorageManager.<DownloadBlockBlobAsync>d__8 <DownloadBlockBlobAsync>d__;
			<DownloadBlockBlobAsync>d__.<>t__builder = AsyncTaskMethodBuilder<byte[]>.Create();
			<DownloadBlockBlobAsync>d__.blobUri = blobUri;
			<DownloadBlockBlobAsync>d__.<>1__state = -1;
			<DownloadBlockBlobAsync>d__.<>t__builder.Start<AzureStorageManager.<DownloadBlockBlobAsync>d__8>(ref <DownloadBlockBlobAsync>d__);
			return <DownloadBlockBlobAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021BC File Offset: 0x000003BC
		public byte[] DownloadBlockBlob(Uri containerUri, string blobName)
		{
			CloudBlockBlob blockBlobReference = new CloudBlobContainer(containerUri).GetBlockBlobReference(blobName);
			blockBlobReference.FetchAttributes(null, null, null);
			byte[] array = new byte[blockBlobReference.Properties.Length];
			blockBlobReference.DownloadToByteArray(array, 0, null, null, null);
			return array;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021FC File Offset: 0x000003FC
		public Task<byte[]> DownloadBlockBlobAsync(Uri containerUri, string blobName)
		{
			AzureStorageManager.<DownloadBlockBlobAsync>d__10 <DownloadBlockBlobAsync>d__;
			<DownloadBlockBlobAsync>d__.<>t__builder = AsyncTaskMethodBuilder<byte[]>.Create();
			<DownloadBlockBlobAsync>d__.containerUri = containerUri;
			<DownloadBlockBlobAsync>d__.blobName = blobName;
			<DownloadBlockBlobAsync>d__.<>1__state = -1;
			<DownloadBlockBlobAsync>d__.<>t__builder.Start<AzureStorageManager.<DownloadBlockBlobAsync>d__10>(ref <DownloadBlockBlobAsync>d__);
			return <DownloadBlockBlobAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002247 File Offset: 0x00000447
		public void DownloadBlobToFile(Uri blobUri, string filename)
		{
			new CloudBlockBlob(blobUri).DownloadToFile(filename, FileMode.Create, null, null, null);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000225C File Offset: 0x0000045C
		public Task DownloadBlobToFileAsync(Uri blobUri, string filename)
		{
			AzureStorageManager.<DownloadBlobToFileAsync>d__12 <DownloadBlobToFileAsync>d__;
			<DownloadBlobToFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<DownloadBlobToFileAsync>d__.blobUri = blobUri;
			<DownloadBlobToFileAsync>d__.filename = filename;
			<DownloadBlobToFileAsync>d__.<>1__state = -1;
			<DownloadBlobToFileAsync>d__.<>t__builder.Start<AzureStorageManager.<DownloadBlobToFileAsync>d__12>(ref <DownloadBlobToFileAsync>d__);
			return <DownloadBlobToFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000022A7 File Offset: 0x000004A7
		public void DownloadBlobToFile(Uri containerUri, string blobName, string filename)
		{
			new CloudBlobContainer(containerUri).GetBlockBlobReference(blobName).DownloadToFile(filename, FileMode.Create, null, null, null);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000022C0 File Offset: 0x000004C0
		public Task DownloadBlobToFileAsync(Uri containerUri, string blobName, string filename)
		{
			AzureStorageManager.<DownloadBlobToFileAsync>d__14 <DownloadBlobToFileAsync>d__;
			<DownloadBlobToFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<DownloadBlobToFileAsync>d__.containerUri = containerUri;
			<DownloadBlobToFileAsync>d__.blobName = blobName;
			<DownloadBlobToFileAsync>d__.filename = filename;
			<DownloadBlobToFileAsync>d__.<>1__state = -1;
			<DownloadBlobToFileAsync>d__.<>t__builder.Start<AzureStorageManager.<DownloadBlobToFileAsync>d__14>(ref <DownloadBlobToFileAsync>d__);
			return <DownloadBlobToFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002313 File Offset: 0x00000513
		public bool BlobExists(Uri containerUri, string blobName)
		{
			return new CloudBlobContainer(containerUri).GetBlockBlobReference(blobName).Exists(null, null);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002328 File Offset: 0x00000528
		public IList<CloudBlobInfo> ListBlockBlobInfoInContainer(Uri containerUri)
		{
			CloudBlobContainer container = new CloudBlobContainer(containerUri);
			return (from item in container.ListBlobs(null, false, 0, null, null)
			where item.GetType() == typeof(CloudBlockBlob)
			select (CloudBlockBlob)item into blob
			select new CloudBlobInfo
			{
				BlobName = blob.Name,
				BlobUri = blob.Uri,
				ContainerName = container.Name,
				ContainerUri = containerUri,
				SizeinBytes = (long)((int)blob.Properties.Length),
				LastModifiedTime = blob.Properties.LastModified
			}).ToList<CloudBlobInfo>();
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000023C4 File Offset: 0x000005C4
		public void WriteToAppendBlob(Uri containerUri, string blobName, string text)
		{
			CloudAppendBlob appendBlobReference = new CloudBlobContainer(containerUri).GetAppendBlobReference(blobName);
			if (!appendBlobReference.Exists(null, null))
			{
				appendBlobReference.CreateOrReplace(null, null, null);
			}
			appendBlobReference.AppendText(text, null, null, null, null);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000023FC File Offset: 0x000005FC
		public Task WriteToAppendBlobAsync(Uri containerUri, string blobName, string text)
		{
			AzureStorageManager.<WriteToAppendBlobAsync>d__18 <WriteToAppendBlobAsync>d__;
			<WriteToAppendBlobAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteToAppendBlobAsync>d__.containerUri = containerUri;
			<WriteToAppendBlobAsync>d__.blobName = blobName;
			<WriteToAppendBlobAsync>d__.text = text;
			<WriteToAppendBlobAsync>d__.<>1__state = -1;
			<WriteToAppendBlobAsync>d__.<>t__builder.Start<AzureStorageManager.<WriteToAppendBlobAsync>d__18>(ref <WriteToAppendBlobAsync>d__);
			return <WriteToAppendBlobAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002450 File Offset: 0x00000650
		public void WriteToAppendBlob(Uri appendBlobUri, string text)
		{
			CloudAppendBlob cloudAppendBlob = new CloudAppendBlob(appendBlobUri);
			if (!cloudAppendBlob.Exists(null, null))
			{
				cloudAppendBlob.CreateOrReplaceAsync();
			}
			cloudAppendBlob.AppendTextAsync(text);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002480 File Offset: 0x00000680
		public Task WriteToAppendBlobAsync(Uri appendBlobUri, string text)
		{
			AzureStorageManager.<WriteToAppendBlobAsync>d__20 <WriteToAppendBlobAsync>d__;
			<WriteToAppendBlobAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteToAppendBlobAsync>d__.appendBlobUri = appendBlobUri;
			<WriteToAppendBlobAsync>d__.text = text;
			<WriteToAppendBlobAsync>d__.<>1__state = -1;
			<WriteToAppendBlobAsync>d__.<>t__builder.Start<AzureStorageManager.<WriteToAppendBlobAsync>d__20>(ref <WriteToAppendBlobAsync>d__);
			return <WriteToAppendBlobAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000024CC File Offset: 0x000006CC
		public CloudBlobInfo GetBlobInfo(Uri blobUri)
		{
			CloudBlockBlob cloudBlockBlob = new CloudBlockBlob(blobUri);
			return new CloudBlobInfo
			{
				BlobName = cloudBlockBlob.Name,
				BlobUri = cloudBlockBlob.Uri,
				ContainerName = cloudBlockBlob.Container.Name,
				ContainerUri = cloudBlockBlob.Container.Uri,
				SizeinBytes = (long)((int)cloudBlockBlob.Properties.Length),
				LastModifiedTime = cloudBlockBlob.Properties.LastModified
			};
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002544 File Offset: 0x00000744
		public Task<CloudBlobInfo> GetBlobInfoAsync(Uri blobUri)
		{
			AzureStorageManager.<GetBlobInfoAsync>d__22 <GetBlobInfoAsync>d__;
			<GetBlobInfoAsync>d__.<>t__builder = AsyncTaskMethodBuilder<CloudBlobInfo>.Create();
			<GetBlobInfoAsync>d__.<>4__this = this;
			<GetBlobInfoAsync>d__.blobUri = blobUri;
			<GetBlobInfoAsync>d__.<>1__state = -1;
			<GetBlobInfoAsync>d__.<>t__builder.Start<AzureStorageManager.<GetBlobInfoAsync>d__22>(ref <GetBlobInfoAsync>d__);
			return <GetBlobInfoAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002590 File Offset: 0x00000790
		public CloudBlobInfo GetBlobInfo(Uri containerUri, string blobName)
		{
			CloudBlobContainer cloudBlobContainer = new CloudBlobContainer(containerUri);
			CloudBlockBlob blockBlobReference = cloudBlobContainer.GetBlockBlobReference(blobName);
			blockBlobReference.FetchAttributes(null, null, null);
			return new CloudBlobInfo
			{
				BlobName = blockBlobReference.Name,
				BlobUri = blockBlobReference.Uri,
				ContainerName = cloudBlobContainer.Name,
				ContainerUri = containerUri,
				SizeinBytes = (long)((int)blockBlobReference.Properties.Length),
				LastModifiedTime = blockBlobReference.Properties.LastModified
			};
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000260C File Offset: 0x0000080C
		public Task<CloudBlobInfo> GetBlobInfoAsync(Uri containerUri, string blobName)
		{
			AzureStorageManager.<GetBlobInfoAsync>d__24 <GetBlobInfoAsync>d__;
			<GetBlobInfoAsync>d__.<>t__builder = AsyncTaskMethodBuilder<CloudBlobInfo>.Create();
			<GetBlobInfoAsync>d__.containerUri = containerUri;
			<GetBlobInfoAsync>d__.blobName = blobName;
			<GetBlobInfoAsync>d__.<>1__state = -1;
			<GetBlobInfoAsync>d__.<>t__builder.Start<AzureStorageManager.<GetBlobInfoAsync>d__24>(ref <GetBlobInfoAsync>d__);
			return <GetBlobInfoAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002658 File Offset: 0x00000858
		public StreamingCloudBlob DownloadLargeBlob(Uri containerUri, string blobName)
		{
			CloudBlobContainer cloudBlobContainer = new CloudBlobContainer(containerUri);
			CloudBlockBlob blockBlobReference = cloudBlobContainer.GetBlockBlobReference(blobName);
			blockBlobReference.FetchAttributes(null, null, null);
			return new StreamingCloudBlob
			{
				BlobName = blockBlobReference.Name,
				BlobUri = blockBlobReference.Uri,
				ContainerName = cloudBlobContainer.Name,
				ContainerUri = containerUri,
				SizeinBytes = (long)((int)blockBlobReference.Properties.Length),
				LastModifiedTime = blockBlobReference.Properties.LastModified,
				FileByteStream = blockBlobReference.OpenRead(null, null, null)
			};
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000026E0 File Offset: 0x000008E0
		public Task<StreamingCloudBlob> DownloadLargeBlobAsync(Uri containerUri, string blobName)
		{
			AzureStorageManager.<DownloadLargeBlobAsync>d__26 <DownloadLargeBlobAsync>d__;
			<DownloadLargeBlobAsync>d__.<>t__builder = AsyncTaskMethodBuilder<StreamingCloudBlob>.Create();
			<DownloadLargeBlobAsync>d__.containerUri = containerUri;
			<DownloadLargeBlobAsync>d__.blobName = blobName;
			<DownloadLargeBlobAsync>d__.<>1__state = -1;
			<DownloadLargeBlobAsync>d__.<>t__builder.Start<AzureStorageManager.<DownloadLargeBlobAsync>d__26>(ref <DownloadLargeBlobAsync>d__);
			return <DownloadLargeBlobAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000272C File Offset: 0x0000092C
		public StreamingCloudBlob DownloadLargeBlob(Uri blobUri)
		{
			CloudBlockBlob cloudBlockBlob = new CloudBlockBlob(blobUri);
			return new StreamingCloudBlob
			{
				BlobName = cloudBlockBlob.Name,
				BlobUri = cloudBlockBlob.Uri,
				ContainerName = cloudBlockBlob.Container.Name,
				ContainerUri = cloudBlockBlob.Container.Uri,
				SizeinBytes = (long)((int)cloudBlockBlob.Properties.Length),
				LastModifiedTime = cloudBlockBlob.Properties.LastModified,
				FileByteStream = cloudBlockBlob.OpenRead(null, null, null)
			};
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000027B4 File Offset: 0x000009B4
		public Task<StreamingCloudBlob> DownloadLargeBlobAsync(Uri blobUri)
		{
			AzureStorageManager.<DownloadLargeBlobAsync>d__28 <DownloadLargeBlobAsync>d__;
			<DownloadLargeBlobAsync>d__.<>t__builder = AsyncTaskMethodBuilder<StreamingCloudBlob>.Create();
			<DownloadLargeBlobAsync>d__.blobUri = blobUri;
			<DownloadLargeBlobAsync>d__.<>1__state = -1;
			<DownloadLargeBlobAsync>d__.<>t__builder.Start<AzureStorageManager.<DownloadLargeBlobAsync>d__28>(ref <DownloadLargeBlobAsync>d__);
			return <DownloadLargeBlobAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000027F8 File Offset: 0x000009F8
		public InMemoryCloudBlob DownloadBlob(Uri containerUri, string blobName)
		{
			CloudBlobContainer cloudBlobContainer = new CloudBlobContainer(containerUri);
			CloudBlockBlob blockBlobReference = cloudBlobContainer.GetBlockBlobReference(blobName);
			blockBlobReference.FetchAttributes(null, null, null);
			byte[] array = new byte[blockBlobReference.Properties.Length];
			blockBlobReference.DownloadToByteArray(array, 0, null, null, null);
			return new InMemoryCloudBlob
			{
				BlobName = blockBlobReference.Name,
				BlobUri = blockBlobReference.Uri,
				ContainerName = cloudBlobContainer.Name,
				ContainerUri = containerUri,
				SizeinBytes = (long)((int)blockBlobReference.Properties.Length),
				LastModifiedTime = blockBlobReference.Properties.LastModified,
				FileBytes = array
			};
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002898 File Offset: 0x00000A98
		public Task<InMemoryCloudBlob> DownloadBlobAsync(Uri containerUri, string blobName)
		{
			AzureStorageManager.<DownloadBlobAsync>d__30 <DownloadBlobAsync>d__;
			<DownloadBlobAsync>d__.<>t__builder = AsyncTaskMethodBuilder<InMemoryCloudBlob>.Create();
			<DownloadBlobAsync>d__.containerUri = containerUri;
			<DownloadBlobAsync>d__.blobName = blobName;
			<DownloadBlobAsync>d__.<>1__state = -1;
			<DownloadBlobAsync>d__.<>t__builder.Start<AzureStorageManager.<DownloadBlobAsync>d__30>(ref <DownloadBlobAsync>d__);
			return <DownloadBlobAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000028E4 File Offset: 0x00000AE4
		public InMemoryCloudBlob DownloadBlob(Uri blobUri)
		{
			CloudBlockBlob cloudBlockBlob = new CloudBlockBlob(blobUri);
			byte[] array = new byte[cloudBlockBlob.Properties.Length];
			cloudBlockBlob.DownloadToByteArray(array, 0, null, null, null);
			return new InMemoryCloudBlob
			{
				BlobName = cloudBlockBlob.Name,
				BlobUri = cloudBlockBlob.Uri,
				ContainerName = cloudBlockBlob.Container.Name,
				ContainerUri = cloudBlockBlob.Container.Uri,
				SizeinBytes = (long)((int)cloudBlockBlob.Properties.Length),
				LastModifiedTime = cloudBlockBlob.Properties.LastModified,
				FileBytes = array
			};
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002980 File Offset: 0x00000B80
		public Task<InMemoryCloudBlob> DownloadBlobAsync(Uri blobUri)
		{
			AzureStorageManager.<DownloadBlobAsync>d__32 <DownloadBlobAsync>d__;
			<DownloadBlobAsync>d__.<>t__builder = AsyncTaskMethodBuilder<InMemoryCloudBlob>.Create();
			<DownloadBlobAsync>d__.blobUri = blobUri;
			<DownloadBlobAsync>d__.<>1__state = -1;
			<DownloadBlobAsync>d__.<>t__builder.Start<AzureStorageManager.<DownloadBlobAsync>d__32>(ref <DownloadBlobAsync>d__);
			return <DownloadBlobAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000029C4 File Offset: 0x00000BC4
		public CloudBlobInfo UploadLargeBlob(StreamingCloudBlob blobFile)
		{
			CloudBlockBlob blockBlobReference = new CloudBlobContainer(blobFile.ContainerUri).GetBlockBlobReference(blobFile.BlobName);
			int num = 1048576;
			long sizeinBytes = blobFile.SizeinBytes;
			if (sizeinBytes > (long)num)
			{
				int num2 = 0;
				List<string> list = new List<string>();
				int num3 = 256000;
				int num4 = 0;
				do
				{
					byte[] buffer = new byte[num3];
					int num5 = blobFile.FileByteStream.Read(buffer, 0, num3);
					string text = Convert.ToBase64String(BitConverter.GetBytes(num2++));
					blockBlobReference.PutBlock(text, new MemoryStream(buffer, true), null, null, null, null);
					list.Add(text);
					num4 += num5;
				}
				while ((long)num4 < sizeinBytes);
				blockBlobReference.PutBlockList(list, null, null, null);
			}
			else
			{
				blockBlobReference.UploadFromStream(blobFile.FileByteStream, null, null, null);
			}
			blobFile.BlobUri = blockBlobReference.Uri;
			return blobFile;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002A8C File Offset: 0x00000C8C
		public Task<CloudBlobInfo> UploadLargeBlobAsync(StreamingCloudBlob blobFile)
		{
			AzureStorageManager.<UploadLargeBlobAsync>d__34 <UploadLargeBlobAsync>d__;
			<UploadLargeBlobAsync>d__.<>t__builder = AsyncTaskMethodBuilder<CloudBlobInfo>.Create();
			<UploadLargeBlobAsync>d__.blobFile = blobFile;
			<UploadLargeBlobAsync>d__.<>1__state = -1;
			<UploadLargeBlobAsync>d__.<>t__builder.Start<AzureStorageManager.<UploadLargeBlobAsync>d__34>(ref <UploadLargeBlobAsync>d__);
			return <UploadLargeBlobAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002AD0 File Offset: 0x00000CD0
		public CloudBlobInfo UploadBlob(InMemoryCloudBlob blobFile)
		{
			CloudBlockBlob blockBlobReference = new CloudBlobContainer(blobFile.ContainerUri).GetBlockBlobReference(blobFile.BlobName);
			blockBlobReference.UploadFromByteArray(blobFile.FileBytes, 0, blobFile.FileBytes.Length, null, null, null);
			blobFile.BlobUri = blockBlobReference.Uri;
			return blobFile;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002B1C File Offset: 0x00000D1C
		public Task<CloudBlobInfo> UploadBlobAsync(InMemoryCloudBlob blobFile)
		{
			AzureStorageManager.<UploadBlobAsync>d__36 <UploadBlobAsync>d__;
			<UploadBlobAsync>d__.<>t__builder = AsyncTaskMethodBuilder<CloudBlobInfo>.Create();
			<UploadBlobAsync>d__.blobFile = blobFile;
			<UploadBlobAsync>d__.<>1__state = -1;
			<UploadBlobAsync>d__.<>t__builder.Start<AzureStorageManager.<UploadBlobAsync>d__36>(ref <UploadBlobAsync>d__);
			return <UploadBlobAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002B5F File Offset: 0x00000D5F
		public void DeleteBlob(Uri blobUri)
		{
			new CloudBlockBlob(blobUri).DeleteIfExists(0, null, null, null);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002B71 File Offset: 0x00000D71
		public void DeleteBlob(Uri containerUri, string blobName)
		{
			new CloudBlobContainer(containerUri).GetBlockBlobReference(blobName).DeleteIfExists(0, null, null, null);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002B8C File Offset: 0x00000D8C
		public Task DeleteBlobAsync(Uri blobUri)
		{
			AzureStorageManager.<DeleteBlobAsync>d__39 <DeleteBlobAsync>d__;
			<DeleteBlobAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<DeleteBlobAsync>d__.blobUri = blobUri;
			<DeleteBlobAsync>d__.<>1__state = -1;
			<DeleteBlobAsync>d__.<>t__builder.Start<AzureStorageManager.<DeleteBlobAsync>d__39>(ref <DeleteBlobAsync>d__);
			return <DeleteBlobAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002BD0 File Offset: 0x00000DD0
		public Task DeleteBlobAsync(Uri containerUri, string blobName)
		{
			AzureStorageManager.<DeleteBlobAsync>d__40 <DeleteBlobAsync>d__;
			<DeleteBlobAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<DeleteBlobAsync>d__.containerUri = containerUri;
			<DeleteBlobAsync>d__.blobName = blobName;
			<DeleteBlobAsync>d__.<>1__state = -1;
			<DeleteBlobAsync>d__.<>t__builder.Start<AzureStorageManager.<DeleteBlobAsync>d__40>(ref <DeleteBlobAsync>d__);
			return <DeleteBlobAsync>d__.<>t__builder.Task;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002C1B File Offset: 0x00000E1B
		// (set) Token: 0x0600002A RID: 42 RVA: 0x00002C23 File Offset: 0x00000E23
		public OperationContext OpContext { get; set; }

		// Token: 0x04000001 RID: 1
		public const int WRITE_BLOCK_SIZE_IN_BYTES = 1048576;
	}
}
