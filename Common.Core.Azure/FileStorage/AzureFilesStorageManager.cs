using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Blob;
using TechnoPro.Common.ClientManager.ICore.Azure.Storage;
using TechnoPro.Common.Core.Azure.Adapters;
using TechnoPro.Common.Core.Azure.Storage;
using TechnoPro.Common.ICore.Azure.Storage;
using TechnoPro.Common.ICore.FileStorages;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Azure.Storage;
using TechnoPro.Common.Public.Entities.FileStorage;
using TechnoPro.Common.Public.Entities.OperationContexts;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.Core.Azure.FileStorage
{
	// Token: 0x02000004 RID: 4
	public class AzureFilesStorageManager : IFilesStorageManager, IOperationContext, IBaseOperationContext<OperationContext>, IBaseOperationContext<AzureStorageOperationContext>
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002C80 File Offset: 0x00000E80
		// (set) Token: 0x0600002D RID: 45 RVA: 0x00002C88 File Offset: 0x00000E88
		public AzureStorageOperationContext OpContext { get; set; }

		// Token: 0x0600002E RID: 46 RVA: 0x00002C91 File Offset: 0x00000E91
		public AzureFilesStorageManager()
		{
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002CA1 File Offset: 0x00000EA1
		public AzureFilesStorageManager(AzureStorageOperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002CB8 File Offset: 0x00000EB8
		public StreamingFile DownloadLargeFile(FileIdentifier fileId)
		{
			IAzureStorageManager azureStorageManager = new AzureStorageManager(this.OpContext);
			Uri filesContainerUri = this.GetFilesContainerUri(AzureSharedAccessBlobPermissions.Read | AzureSharedAccessBlobPermissions.List);
			StreamingCloudBlob streamingCloudBlob = azureStorageManager.DownloadLargeBlob(filesContainerUri, fileId.GetBlobName());
			return new StreamingFile
			{
				FileIdentifier = fileId,
				FileUri = streamingCloudBlob.BlobUri,
				Length = streamingCloudBlob.SizeinBytes,
				FileName = streamingCloudBlob.BlobName,
				FileByteStream = streamingCloudBlob.FileByteStream
			};
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002D24 File Offset: 0x00000F24
		public Task<StreamingFile> DownloadLargeFileAsync(FileIdentifier fileId)
		{
			AzureFilesStorageManager.<DownloadLargeFileAsync>d__8 <DownloadLargeFileAsync>d__;
			<DownloadLargeFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder<StreamingFile>.Create();
			<DownloadLargeFileAsync>d__.<>4__this = this;
			<DownloadLargeFileAsync>d__.fileId = fileId;
			<DownloadLargeFileAsync>d__.<>1__state = -1;
			<DownloadLargeFileAsync>d__.<>t__builder.Start<AzureFilesStorageManager.<DownloadLargeFileAsync>d__8>(ref <DownloadLargeFileAsync>d__);
			return <DownloadLargeFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002D70 File Offset: 0x00000F70
		public BasicFileInfo UploadLargeFile(StreamingFile file)
		{
			IAzureStorageManager azureStorageManager = new AzureStorageManager(this.OpContext);
			Uri filesContainerUri = this.GetFilesContainerUri(this.WritingPermissions);
			if (file.FileIdentifier == null)
			{
				file.FileIdentifier = new FileIdentifier();
			}
			if (file.FileIdentifier.FileUniqueId == null)
			{
				file.FileIdentifier.FileUniqueId = new Guid?(Guid.NewGuid());
			}
			StreamingCloudBlob blob = new StreamingCloudBlob
			{
				FileByteStream = file.FileByteStream,
				BlobName = file.FileIdentifier.GetBlobName(),
				SizeinBytes = file.Length,
				ContainerUri = filesContainerUri
			};
			CloudBlobInfo cloudBlobInfo = azureStorageManager.UploadLargeBlob(blob);
			file.FileUri = cloudBlobInfo.BlobUri;
			return file;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002E1C File Offset: 0x0000101C
		public Task<BasicFileInfo> UploadLargeFileAsync(StreamingFile file)
		{
			AzureFilesStorageManager.<UploadLargeFileAsync>d__10 <UploadLargeFileAsync>d__;
			<UploadLargeFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder<BasicFileInfo>.Create();
			<UploadLargeFileAsync>d__.<>4__this = this;
			<UploadLargeFileAsync>d__.file = file;
			<UploadLargeFileAsync>d__.<>1__state = -1;
			<UploadLargeFileAsync>d__.<>t__builder.Start<AzureFilesStorageManager.<UploadLargeFileAsync>d__10>(ref <UploadLargeFileAsync>d__);
			return <UploadLargeFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002E68 File Offset: 0x00001068
		public StreamingFile DownloadLargeTempFile(FileIdentifier fileId)
		{
			IAzureStorageManager azureStorageManager = new AzureStorageManager(this.OpContext);
			Uri tempFilesContainerUri = this.GetTempFilesContainerUri(AzureSharedAccessBlobPermissions.Read | AzureSharedAccessBlobPermissions.List);
			StreamingCloudBlob streamingCloudBlob = azureStorageManager.DownloadLargeBlob(tempFilesContainerUri, fileId.GetBlobName());
			return new StreamingFile
			{
				FileIdentifier = fileId,
				FileUri = streamingCloudBlob.BlobUri,
				Length = streamingCloudBlob.SizeinBytes,
				FileName = streamingCloudBlob.BlobName,
				FileByteStream = streamingCloudBlob.FileByteStream
			};
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002ED4 File Offset: 0x000010D4
		public Task<StreamingFile> DownloadLargeTempFileAsync(FileIdentifier fileId)
		{
			AzureFilesStorageManager.<DownloadLargeTempFileAsync>d__12 <DownloadLargeTempFileAsync>d__;
			<DownloadLargeTempFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder<StreamingFile>.Create();
			<DownloadLargeTempFileAsync>d__.<>4__this = this;
			<DownloadLargeTempFileAsync>d__.fileId = fileId;
			<DownloadLargeTempFileAsync>d__.<>1__state = -1;
			<DownloadLargeTempFileAsync>d__.<>t__builder.Start<AzureFilesStorageManager.<DownloadLargeTempFileAsync>d__12>(ref <DownloadLargeTempFileAsync>d__);
			return <DownloadLargeTempFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002F20 File Offset: 0x00001120
		public BasicFileInfo UploadLargeTempFile(StreamingFile file)
		{
			IAzureStorageManager azureStorageManager = new AzureStorageManager(this.OpContext);
			Uri tempFilesContainerUri = this.GetTempFilesContainerUri(this.WritingPermissions);
			if (file.FileIdentifier == null)
			{
				file.FileIdentifier = new FileIdentifier();
			}
			if (file.FileIdentifier.FileUniqueId == null)
			{
				file.FileIdentifier.FileUniqueId = new Guid?(Guid.NewGuid());
			}
			StreamingCloudBlob blob = new StreamingCloudBlob
			{
				FileByteStream = file.FileByteStream,
				BlobName = file.FileIdentifier.GetBlobName(),
				SizeinBytes = file.Length,
				ContainerUri = tempFilesContainerUri
			};
			CloudBlobInfo cloudBlobInfo = azureStorageManager.UploadLargeBlob(blob);
			file.FileUri = cloudBlobInfo.BlobUri;
			return file;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002FCC File Offset: 0x000011CC
		public Task<BasicFileInfo> UploadLargeTempFileAsync(StreamingFile file)
		{
			AzureFilesStorageManager.<UploadLargeTempFileAsync>d__14 <UploadLargeTempFileAsync>d__;
			<UploadLargeTempFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder<BasicFileInfo>.Create();
			<UploadLargeTempFileAsync>d__.<>4__this = this;
			<UploadLargeTempFileAsync>d__.file = file;
			<UploadLargeTempFileAsync>d__.<>1__state = -1;
			<UploadLargeTempFileAsync>d__.<>t__builder.Start<AzureFilesStorageManager.<UploadLargeTempFileAsync>d__14>(ref <UploadLargeTempFileAsync>d__);
			return <UploadLargeTempFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00003018 File Offset: 0x00001218
		public InMemoryFile DownloadFile(FileIdentifier fileId)
		{
			IAzureStorageManager azureStorageManager = new AzureStorageManager(this.OpContext);
			Uri filesContainerUri = this.GetFilesContainerUri(AzureSharedAccessBlobPermissions.Read | AzureSharedAccessBlobPermissions.List);
			InMemoryCloudBlob inMemoryCloudBlob = azureStorageManager.DownloadBlob(filesContainerUri, fileId.GetBlobName());
			return new InMemoryFile
			{
				FileIdentifier = fileId,
				FileUri = inMemoryCloudBlob.BlobUri,
				Length = inMemoryCloudBlob.SizeinBytes,
				FileName = inMemoryCloudBlob.BlobName,
				FileData = inMemoryCloudBlob.FileBytes
			};
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00003084 File Offset: 0x00001284
		public Task<InMemoryFile> DownloadFileAsync(FileIdentifier fileId)
		{
			AzureFilesStorageManager.<DownloadFileAsync>d__16 <DownloadFileAsync>d__;
			<DownloadFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder<InMemoryFile>.Create();
			<DownloadFileAsync>d__.<>4__this = this;
			<DownloadFileAsync>d__.fileId = fileId;
			<DownloadFileAsync>d__.<>1__state = -1;
			<DownloadFileAsync>d__.<>t__builder.Start<AzureFilesStorageManager.<DownloadFileAsync>d__16>(ref <DownloadFileAsync>d__);
			return <DownloadFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000030D0 File Offset: 0x000012D0
		public BasicFileInfo UploadFile(InMemoryFile file)
		{
			IAzureStorageManager azureStorageManager = new AzureStorageManager(this.OpContext);
			Uri filesContainerUri = this.GetFilesContainerUri(this.WritingPermissions);
			if (file.FileIdentifier == null)
			{
				file.FileIdentifier = new FileIdentifier();
			}
			if (file.FileIdentifier.FileUniqueId == null)
			{
				file.FileIdentifier.FileUniqueId = new Guid?(Guid.NewGuid());
			}
			InMemoryCloudBlob blob = new InMemoryCloudBlob
			{
				FileBytes = file.FileData,
				BlobName = file.FileIdentifier.GetBlobName(),
				SizeinBytes = file.Length,
				ContainerUri = filesContainerUri
			};
			CloudBlobInfo cloudBlobInfo = azureStorageManager.UploadBlob(blob);
			file.FileUri = cloudBlobInfo.BlobUri;
			return file;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x0000317C File Offset: 0x0000137C
		public Task<BasicFileInfo> UploadFileAsync(InMemoryFile file)
		{
			AzureFilesStorageManager.<UploadFileAsync>d__18 <UploadFileAsync>d__;
			<UploadFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder<BasicFileInfo>.Create();
			<UploadFileAsync>d__.<>4__this = this;
			<UploadFileAsync>d__.file = file;
			<UploadFileAsync>d__.<>1__state = -1;
			<UploadFileAsync>d__.<>t__builder.Start<AzureFilesStorageManager.<UploadFileAsync>d__18>(ref <UploadFileAsync>d__);
			return <UploadFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000031C8 File Offset: 0x000013C8
		public InMemoryFile DownloadTempFile(FileIdentifier fileId)
		{
			IAzureStorageManager azureStorageManager = new AzureStorageManager(this.OpContext);
			Uri tempFilesContainerUri = this.GetTempFilesContainerUri(AzureSharedAccessBlobPermissions.Read | AzureSharedAccessBlobPermissions.List);
			InMemoryCloudBlob inMemoryCloudBlob = azureStorageManager.DownloadBlob(tempFilesContainerUri, fileId.GetBlobName());
			return new InMemoryFile
			{
				FileIdentifier = fileId,
				FileUri = inMemoryCloudBlob.BlobUri,
				Length = inMemoryCloudBlob.SizeinBytes,
				FileName = inMemoryCloudBlob.BlobName,
				FileData = inMemoryCloudBlob.FileBytes
			};
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00003234 File Offset: 0x00001434
		public Task<InMemoryFile> DownloadTempFileAsync(FileIdentifier fileId)
		{
			AzureFilesStorageManager.<DownloadTempFileAsync>d__20 <DownloadTempFileAsync>d__;
			<DownloadTempFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder<InMemoryFile>.Create();
			<DownloadTempFileAsync>d__.<>4__this = this;
			<DownloadTempFileAsync>d__.fileId = fileId;
			<DownloadTempFileAsync>d__.<>1__state = -1;
			<DownloadTempFileAsync>d__.<>t__builder.Start<AzureFilesStorageManager.<DownloadTempFileAsync>d__20>(ref <DownloadTempFileAsync>d__);
			return <DownloadTempFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00003280 File Offset: 0x00001480
		public BasicFileInfo UploadTempFile(InMemoryFile file)
		{
			IAzureStorageManager azureStorageManager = new AzureStorageManager(this.OpContext);
			Uri tempFilesContainerUri = this.GetTempFilesContainerUri(this.WritingPermissions);
			if (file.FileIdentifier == null)
			{
				file.FileIdentifier = new FileIdentifier();
			}
			if (file.FileIdentifier.FileUniqueId == null)
			{
				file.FileIdentifier.FileUniqueId = new Guid?(Guid.NewGuid());
			}
			InMemoryCloudBlob blob = new InMemoryCloudBlob
			{
				FileBytes = file.FileData,
				BlobName = file.FileIdentifier.GetBlobName(),
				SizeinBytes = file.Length,
				ContainerUri = tempFilesContainerUri
			};
			CloudBlobInfo cloudBlobInfo = azureStorageManager.UploadBlob(blob);
			file.FileUri = cloudBlobInfo.BlobUri;
			return file;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x0000332C File Offset: 0x0000152C
		public Task<BasicFileInfo> UploadTempFileAsync(InMemoryFile file)
		{
			AzureFilesStorageManager.<UploadTempFileAsync>d__22 <UploadTempFileAsync>d__;
			<UploadTempFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder<BasicFileInfo>.Create();
			<UploadTempFileAsync>d__.<>4__this = this;
			<UploadTempFileAsync>d__.file = file;
			<UploadTempFileAsync>d__.<>1__state = -1;
			<UploadTempFileAsync>d__.<>t__builder.Start<AzureFilesStorageManager.<UploadTempFileAsync>d__22>(ref <UploadTempFileAsync>d__);
			return <UploadTempFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00003378 File Offset: 0x00001578
		public void DeleteFile(FileIdentifier fileId)
		{
			IAzureStorageManager azureStorageManager = new AzureStorageManager(this.OpContext);
			Uri filesContainerUri = this.GetFilesContainerUri(AzureSharedAccessBlobPermissions.Read | AzureSharedAccessBlobPermissions.List);
			azureStorageManager.DeleteBlob(filesContainerUri, fileId.GetBlobName());
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000033A8 File Offset: 0x000015A8
		public Task DeleteFileAsync(FileIdentifier fileId)
		{
			AzureFilesStorageManager.<DeleteFileAsync>d__24 <DeleteFileAsync>d__;
			<DeleteFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<DeleteFileAsync>d__.<>4__this = this;
			<DeleteFileAsync>d__.fileId = fileId;
			<DeleteFileAsync>d__.<>1__state = -1;
			<DeleteFileAsync>d__.<>t__builder.Start<AzureFilesStorageManager.<DeleteFileAsync>d__24>(ref <DeleteFileAsync>d__);
			return <DeleteFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000033F4 File Offset: 0x000015F4
		public void DeleteTempFile(FileIdentifier fileId)
		{
			IAzureStorageManager azureStorageManager = new AzureStorageManager(this.OpContext);
			Uri tempFilesContainerUri = this.GetTempFilesContainerUri(AzureSharedAccessBlobPermissions.Read | AzureSharedAccessBlobPermissions.List);
			azureStorageManager.DeleteBlob(tempFilesContainerUri, fileId.GetBlobName());
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00003424 File Offset: 0x00001624
		public Task DeleteTempFileAsync(FileIdentifier fileId)
		{
			AzureFilesStorageManager.<DeleteTempFileAsync>d__26 <DeleteTempFileAsync>d__;
			<DeleteTempFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<DeleteTempFileAsync>d__.<>4__this = this;
			<DeleteTempFileAsync>d__.fileId = fileId;
			<DeleteTempFileAsync>d__.<>1__state = -1;
			<DeleteTempFileAsync>d__.<>t__builder.Start<AzureFilesStorageManager.<DeleteTempFileAsync>d__26>(ref <DeleteTempFileAsync>d__);
			return <DeleteTempFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00003470 File Offset: 0x00001670
		public void DeleteTempFilesOlderThan(DateTimeOffset date)
		{
			CloudBlobContainer cloudBlobContainer = new CloudBlobContainer(this.GetTempFilesContainerUri(this.WritingPermissions));
			foreach (CloudBlockBlob cloudBlockBlob in from b in cloudBlobContainer.ListBlobs(null, true, 0, null, null).OfType<CloudBlockBlob>()
			where date > b.Properties.LastModified.Value
			select b)
			{
				cloudBlobContainer.GetBlobReference(cloudBlockBlob.Uri.ToString()).DeleteIfExists(0, null, null, null);
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x0000350C File Offset: 0x0000170C
		public Task DeleteTempFilesOlderThanAsync(DateTimeOffset date)
		{
			AzureFilesStorageManager.<DeleteTempFilesOlderThanAsync>d__28 <DeleteTempFilesOlderThanAsync>d__;
			<DeleteTempFilesOlderThanAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<DeleteTempFilesOlderThanAsync>d__.<>4__this = this;
			<DeleteTempFilesOlderThanAsync>d__.date = date;
			<DeleteTempFilesOlderThanAsync>d__.<>1__state = -1;
			<DeleteTempFilesOlderThanAsync>d__.<>t__builder.Start<AzureFilesStorageManager.<DeleteTempFilesOlderThanAsync>d__28>(ref <DeleteTempFilesOlderThanAsync>d__);
			return <DeleteTempFilesOlderThanAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00003558 File Offset: 0x00001758
		public void MoveTempFileToPersistentStorage(FileIdentifier fileId)
		{
			Uri filesContainerUri = this.GetFilesContainerUri(this.WritingPermissions);
			Uri tempFilesContainerUri = this.GetTempFilesContainerUri(AzureSharedAccessBlobPermissions.Read | AzureSharedAccessBlobPermissions.List);
			CloudBlobContainer cloudBlobContainer = new CloudBlobContainer(filesContainerUri);
			CloudBlockBlob blockBlobReference = new CloudBlobContainer(tempFilesContainerUri).GetBlockBlobReference(fileId.GetBlobName());
			cloudBlobContainer.GetBlockBlobReference(fileId.GetBlobName()).StartCopy(blockBlobReference, null, null, null, null);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000035AC File Offset: 0x000017AC
		public Task MoveTempFileToPersistentStorageAsync(FileIdentifier fileId)
		{
			AzureFilesStorageManager.<MoveTempFileToPersistentStorageAsync>d__30 <MoveTempFileToPersistentStorageAsync>d__;
			<MoveTempFileToPersistentStorageAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<MoveTempFileToPersistentStorageAsync>d__.<>4__this = this;
			<MoveTempFileToPersistentStorageAsync>d__.fileId = fileId;
			<MoveTempFileToPersistentStorageAsync>d__.<>1__state = -1;
			<MoveTempFileToPersistentStorageAsync>d__.<>t__builder.Start<AzureFilesStorageManager.<MoveTempFileToPersistentStorageAsync>d__30>(ref <MoveTempFileToPersistentStorageAsync>d__);
			return <MoveTempFileToPersistentStorageAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000035F7 File Offset: 0x000017F7
		private Uri GetFilesContainerUri(AzureSharedAccessBlobPermissions permissions = AzureSharedAccessBlobPermissions.Read | AzureSharedAccessBlobPermissions.List)
		{
			return ObjectFactory.Resolve<IClockWorkSasTokenProviderClientManager>().GetContainerSasUri(TokenBasedClientCredentialsFactory.GenerateToken(this.OpContext.TenantId.ToLower(), this.OpContext.StoragePrivateKey), "files", true, permissions);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x0000362A File Offset: 0x0000182A
		private Uri GetTempFilesContainerUri(AzureSharedAccessBlobPermissions permissions = AzureSharedAccessBlobPermissions.Read | AzureSharedAccessBlobPermissions.List)
		{
			return ObjectFactory.Resolve<IClockWorkSasTokenProviderClientManager>().GetContainerSasUri(TokenBasedClientCredentialsFactory.GenerateToken(this.OpContext.TenantId.ToLower(), this.OpContext.StoragePrivateKey), "tempfiles", true, permissions);
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600004A RID: 74 RVA: 0x0000365D File Offset: 0x0000185D
		// (set) Token: 0x0600004B RID: 75 RVA: 0x00003665 File Offset: 0x00001865
		OperationContext IBaseOperationContext<OperationContext>.OpContext
		{
			get
			{
				return this.OpContext;
			}
			set
			{
				this.OpContext = (AzureStorageOperationContext)value;
			}
		}

		// Token: 0x04000005 RID: 5
		private AzureSharedAccessBlobPermissions WritingPermissions = AzureSharedAccessBlobPermissions.Read | AzureSharedAccessBlobPermissions.Write | AzureSharedAccessBlobPermissions.List | AzureSharedAccessBlobPermissions.Add | AzureSharedAccessBlobPermissions.Create;
	}
}
