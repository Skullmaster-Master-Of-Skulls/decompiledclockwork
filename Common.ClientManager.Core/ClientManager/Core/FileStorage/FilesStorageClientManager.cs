using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.FileStorage;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.FileStorage;
using TechnoPro.Common.Core.Mappers.Files;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.FileStorage;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.FileStorage
{
	// Token: 0x0200005B RID: 91
	public class FilesStorageClientManager : IFilesStorageClientManager, IWebService
	{
		// Token: 0x06000336 RID: 822 RVA: 0x0000DF8C File Offset: 0x0000C18C
		public StreamingFileDTO DownloadLargeFile(FileIdentifier fileId)
		{
			DownloadLargeFileMessageReq downloadLargeFileMessageReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateMessageRequest<DownloadLargeFileMessageReq>();
			downloadLargeFileMessageReq.FileIdentifier = fileId.ToMessageDTO();
			return ClientServiceFactory.GetClientInstance<ILargeFileStreaming>().DownloadLargeFile(downloadLargeFileMessageReq);
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0000DFC4 File Offset: 0x0000C1C4
		[DebuggerStepThrough]
		public Task<StreamingFileDTO> DownloadLargeFileAsync(FileIdentifier fileId)
		{
			FilesStorageClientManager.<DownloadLargeFileAsync>d__2 <DownloadLargeFileAsync>d__ = new FilesStorageClientManager.<DownloadLargeFileAsync>d__2();
			<DownloadLargeFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder<StreamingFileDTO>.Create();
			<DownloadLargeFileAsync>d__.<>4__this = this;
			<DownloadLargeFileAsync>d__.fileId = fileId;
			<DownloadLargeFileAsync>d__.<>1__state = -1;
			<DownloadLargeFileAsync>d__.<>t__builder.Start<FilesStorageClientManager.<DownloadLargeFileAsync>d__2>(ref <DownloadLargeFileAsync>d__);
			return <DownloadLargeFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0000E010 File Offset: 0x0000C210
		public BasicFileInfoDTO UploadLargeFile(StreamingFileDTO file)
		{
			StreamingFileDTO file2 = ObjectFactory.Resolve<IRequestBuilderClientManager>().UpdateMessageRequest<StreamingFileDTO>(file);
			return ClientServiceFactory.GetClientInstance<ILargeFileStreaming>().UploadLargeFile(file2).FileInfo;
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0000E040 File Offset: 0x0000C240
		[DebuggerStepThrough]
		public Task<BasicFileInfoDTO> UploadLargeFileAsync(StreamingFileDTO file)
		{
			FilesStorageClientManager.<UploadLargeFileAsync>d__4 <UploadLargeFileAsync>d__ = new FilesStorageClientManager.<UploadLargeFileAsync>d__4();
			<UploadLargeFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder<BasicFileInfoDTO>.Create();
			<UploadLargeFileAsync>d__.<>4__this = this;
			<UploadLargeFileAsync>d__.file = file;
			<UploadLargeFileAsync>d__.<>1__state = -1;
			<UploadLargeFileAsync>d__.<>t__builder.Start<FilesStorageClientManager.<UploadLargeFileAsync>d__4>(ref <UploadLargeFileAsync>d__);
			return <UploadLargeFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0000E08C File Offset: 0x0000C28C
		public StreamingFileDTO DownloadLargeTempFile(FileIdentifier fileId)
		{
			DownloadLargeFileMessageReq downloadLargeFileMessageReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateMessageRequest<DownloadLargeFileMessageReq>();
			downloadLargeFileMessageReq.FileIdentifier = fileId.ToMessageDTO();
			return ClientServiceFactory.GetClientInstance<ILargeFileStreaming>().DownloadLargeTempFile(downloadLargeFileMessageReq);
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0000E0C4 File Offset: 0x0000C2C4
		[DebuggerStepThrough]
		public Task<StreamingFileDTO> DownloadLargeTempFileAsync(FileIdentifier fileId)
		{
			FilesStorageClientManager.<DownloadLargeTempFileAsync>d__6 <DownloadLargeTempFileAsync>d__ = new FilesStorageClientManager.<DownloadLargeTempFileAsync>d__6();
			<DownloadLargeTempFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder<StreamingFileDTO>.Create();
			<DownloadLargeTempFileAsync>d__.<>4__this = this;
			<DownloadLargeTempFileAsync>d__.fileId = fileId;
			<DownloadLargeTempFileAsync>d__.<>1__state = -1;
			<DownloadLargeTempFileAsync>d__.<>t__builder.Start<FilesStorageClientManager.<DownloadLargeTempFileAsync>d__6>(ref <DownloadLargeTempFileAsync>d__);
			return <DownloadLargeTempFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0000E110 File Offset: 0x0000C310
		public BasicFileInfoDTO UploadLargeTempFile(StreamingFileDTO file)
		{
			StreamingFileDTO request = ObjectFactory.Resolve<IRequestBuilderClientManager>().UpdateMessageRequest<StreamingFileDTO>(file);
			return ClientServiceFactory.GetClientInstance<ILargeFileStreaming>().UploadLargeTempFile(request).FileInfo;
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0000E140 File Offset: 0x0000C340
		[DebuggerStepThrough]
		public Task<BasicFileInfoDTO> UploadLargeTempFileAsync(StreamingFileDTO file)
		{
			FilesStorageClientManager.<UploadLargeTempFileAsync>d__8 <UploadLargeTempFileAsync>d__ = new FilesStorageClientManager.<UploadLargeTempFileAsync>d__8();
			<UploadLargeTempFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder<BasicFileInfoDTO>.Create();
			<UploadLargeTempFileAsync>d__.<>4__this = this;
			<UploadLargeTempFileAsync>d__.file = file;
			<UploadLargeTempFileAsync>d__.<>1__state = -1;
			<UploadLargeTempFileAsync>d__.<>t__builder.Start<FilesStorageClientManager.<UploadLargeTempFileAsync>d__8>(ref <UploadLargeTempFileAsync>d__);
			return <UploadLargeTempFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0000E18C File Offset: 0x0000C38C
		public InMemoryFileDTO DownloadFile(FileIdentifier fileId)
		{
			DownloadFileReq downloadFileReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<DownloadFileReq>();
			downloadFileReq.FileIdentifier = fileId.ToDTO();
			return ClientServiceFactory.GetClientInstance<IInMemoryFilesStorage>().DownloadFile(downloadFileReq).File;
		}

		// Token: 0x0600033F RID: 831 RVA: 0x0000E1C8 File Offset: 0x0000C3C8
		[DebuggerStepThrough]
		public Task<InMemoryFileDTO> DownloadFileAsync(FileIdentifier fileId)
		{
			FilesStorageClientManager.<DownloadFileAsync>d__10 <DownloadFileAsync>d__ = new FilesStorageClientManager.<DownloadFileAsync>d__10();
			<DownloadFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder<InMemoryFileDTO>.Create();
			<DownloadFileAsync>d__.<>4__this = this;
			<DownloadFileAsync>d__.fileId = fileId;
			<DownloadFileAsync>d__.<>1__state = -1;
			<DownloadFileAsync>d__.<>t__builder.Start<FilesStorageClientManager.<DownloadFileAsync>d__10>(ref <DownloadFileAsync>d__);
			return <DownloadFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0000E214 File Offset: 0x0000C414
		public BasicFileInfoDTO UploadFile(InMemoryFileDTO file)
		{
			UploadFileReq uploadFileReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UploadFileReq>();
			uploadFileReq.File = file;
			return ClientServiceFactory.GetClientInstance<IInMemoryFilesStorage>().UploadFile(uploadFileReq).FileInfo;
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0000E24C File Offset: 0x0000C44C
		[DebuggerStepThrough]
		public Task<BasicFileInfoDTO> UploadFileAsync(InMemoryFileDTO file)
		{
			FilesStorageClientManager.<UploadFileAsync>d__12 <UploadFileAsync>d__ = new FilesStorageClientManager.<UploadFileAsync>d__12();
			<UploadFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder<BasicFileInfoDTO>.Create();
			<UploadFileAsync>d__.<>4__this = this;
			<UploadFileAsync>d__.file = file;
			<UploadFileAsync>d__.<>1__state = -1;
			<UploadFileAsync>d__.<>t__builder.Start<FilesStorageClientManager.<UploadFileAsync>d__12>(ref <UploadFileAsync>d__);
			return <UploadFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0000E298 File Offset: 0x0000C498
		public InMemoryFileDTO DownloadTempFile(FileIdentifier fileId)
		{
			DownloadFileReq downloadFileReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<DownloadFileReq>();
			downloadFileReq.FileIdentifier = fileId.ToDTO();
			return ClientServiceFactory.GetClientInstance<IInMemoryFilesStorage>().DownloadTempFile(downloadFileReq).File;
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0000E2D4 File Offset: 0x0000C4D4
		[DebuggerStepThrough]
		public Task<InMemoryFileDTO> DownloadTempFileAsync(FileIdentifier fileId)
		{
			FilesStorageClientManager.<DownloadTempFileAsync>d__14 <DownloadTempFileAsync>d__ = new FilesStorageClientManager.<DownloadTempFileAsync>d__14();
			<DownloadTempFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder<InMemoryFileDTO>.Create();
			<DownloadTempFileAsync>d__.<>4__this = this;
			<DownloadTempFileAsync>d__.fileId = fileId;
			<DownloadTempFileAsync>d__.<>1__state = -1;
			<DownloadTempFileAsync>d__.<>t__builder.Start<FilesStorageClientManager.<DownloadTempFileAsync>d__14>(ref <DownloadTempFileAsync>d__);
			return <DownloadTempFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0000E320 File Offset: 0x0000C520
		public BasicFileInfoDTO UploadTempFile(InMemoryFileDTO file)
		{
			UploadFileReq uploadFileReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UploadFileReq>();
			uploadFileReq.File = file;
			return ClientServiceFactory.GetClientInstance<IInMemoryFilesStorage>().UploadTempFile(uploadFileReq).FileInfo;
		}

		// Token: 0x06000345 RID: 837 RVA: 0x0000E358 File Offset: 0x0000C558
		[DebuggerStepThrough]
		public Task<BasicFileInfoDTO> UploadTempFileAsync(InMemoryFileDTO file)
		{
			FilesStorageClientManager.<UploadTempFileAsync>d__16 <UploadTempFileAsync>d__ = new FilesStorageClientManager.<UploadTempFileAsync>d__16();
			<UploadTempFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder<BasicFileInfoDTO>.Create();
			<UploadTempFileAsync>d__.<>4__this = this;
			<UploadTempFileAsync>d__.file = file;
			<UploadTempFileAsync>d__.<>1__state = -1;
			<UploadTempFileAsync>d__.<>t__builder.Start<FilesStorageClientManager.<UploadTempFileAsync>d__16>(ref <UploadTempFileAsync>d__);
			return <UploadTempFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0000E3A4 File Offset: 0x0000C5A4
		public void ZipFiles(Stream zipStream, params FileIdentifier[] fileIds)
		{
			using (ZipArchive zipArchive = new ZipArchive(zipStream, ZipArchiveMode.Create, true))
			{
				foreach (FileIdentifier fileId in fileIds)
				{
					StreamingFileDTO streamingFileDTO = this.DownloadLargeFile(fileId);
					ZipArchiveEntry zipArchiveEntry = zipArchive.CreateEntry(streamingFileDTO.FileName);
					using (Stream stream = zipArchiveEntry.Open())
					{
						streamingFileDTO.FileByteStream.CopyTo(stream);
					}
				}
			}
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0000E440 File Offset: 0x0000C640
		[DebuggerStepThrough]
		public Task ZipFilesAsync(Stream zipStream, params FileIdentifier[] fileIds)
		{
			FilesStorageClientManager.<ZipFilesAsync>d__18 <ZipFilesAsync>d__ = new FilesStorageClientManager.<ZipFilesAsync>d__18();
			<ZipFilesAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ZipFilesAsync>d__.<>4__this = this;
			<ZipFilesAsync>d__.zipStream = zipStream;
			<ZipFilesAsync>d__.fileIds = fileIds;
			<ZipFilesAsync>d__.<>1__state = -1;
			<ZipFilesAsync>d__.<>t__builder.Start<FilesStorageClientManager.<ZipFilesAsync>d__18>(ref <ZipFilesAsync>d__);
			return <ZipFilesAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0000E494 File Offset: 0x0000C694
		[DebuggerStepThrough]
		public Task<Stream> ZipFilesAsync(params FileIdentifier[] fileIds)
		{
			FilesStorageClientManager.<ZipFilesAsync>d__19 <ZipFilesAsync>d__ = new FilesStorageClientManager.<ZipFilesAsync>d__19();
			<ZipFilesAsync>d__.<>t__builder = AsyncTaskMethodBuilder<Stream>.Create();
			<ZipFilesAsync>d__.<>4__this = this;
			<ZipFilesAsync>d__.fileIds = fileIds;
			<ZipFilesAsync>d__.<>1__state = -1;
			<ZipFilesAsync>d__.<>t__builder.Start<FilesStorageClientManager.<ZipFilesAsync>d__19>(ref <ZipFilesAsync>d__);
			return <ZipFilesAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0000E4E0 File Offset: 0x0000C6E0
		[DebuggerStepThrough]
		public Task ZipFilesAsync(Stream zipStream, CancellationToken cancellationToken, params FileIdentifier[] fileIds)
		{
			FilesStorageClientManager.<ZipFilesAsync>d__20 <ZipFilesAsync>d__ = new FilesStorageClientManager.<ZipFilesAsync>d__20();
			<ZipFilesAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ZipFilesAsync>d__.<>4__this = this;
			<ZipFilesAsync>d__.zipStream = zipStream;
			<ZipFilesAsync>d__.cancellationToken = cancellationToken;
			<ZipFilesAsync>d__.fileIds = fileIds;
			<ZipFilesAsync>d__.<>1__state = -1;
			<ZipFilesAsync>d__.<>t__builder.Start<FilesStorageClientManager.<ZipFilesAsync>d__20>(ref <ZipFilesAsync>d__);
			return <ZipFilesAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0000E53C File Offset: 0x0000C73C
		public void DownloadFileTo(FileIdentifier fileId, string filename, long size)
		{
			Stream stream = null;
			bool flag = size <= 1048576L;
			if (flag)
			{
				InMemoryFileDTO inMemoryFileDTO = this.DownloadFile(fileId);
				stream = new MemoryStream(inMemoryFileDTO.FileData);
			}
			else
			{
				StreamingFileDTO streamingFileDTO = this.DownloadLargeFile(fileId);
				stream = streamingFileDTO.FileByteStream;
			}
			try
			{
				using (FileStream fileStream = new FileStream(filename, FileMode.Create, FileAccess.Write))
				{
					stream.CopyTo(fileStream);
				}
			}
			finally
			{
				bool flag2 = stream != null;
				if (flag2)
				{
					stream.Close();
					stream.Dispose();
					stream = null;
				}
			}
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0000E5EC File Offset: 0x0000C7EC
		[DebuggerStepThrough]
		public Task DownloadFileToAsync(FileIdentifier fileId, string filename, long size)
		{
			FilesStorageClientManager.<DownloadFileToAsync>d__22 <DownloadFileToAsync>d__ = new FilesStorageClientManager.<DownloadFileToAsync>d__22();
			<DownloadFileToAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<DownloadFileToAsync>d__.<>4__this = this;
			<DownloadFileToAsync>d__.fileId = fileId;
			<DownloadFileToAsync>d__.filename = filename;
			<DownloadFileToAsync>d__.size = size;
			<DownloadFileToAsync>d__.<>1__state = -1;
			<DownloadFileToAsync>d__.<>t__builder.Start<FilesStorageClientManager.<DownloadFileToAsync>d__22>(ref <DownloadFileToAsync>d__);
			return <DownloadFileToAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0000E648 File Offset: 0x0000C848
		[DebuggerStepThrough]
		public Task DownloadLargeFileToAsync(FileIdentifier fileId, string filename)
		{
			FilesStorageClientManager.<DownloadLargeFileToAsync>d__23 <DownloadLargeFileToAsync>d__ = new FilesStorageClientManager.<DownloadLargeFileToAsync>d__23();
			<DownloadLargeFileToAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<DownloadLargeFileToAsync>d__.<>4__this = this;
			<DownloadLargeFileToAsync>d__.fileId = fileId;
			<DownloadLargeFileToAsync>d__.filename = filename;
			<DownloadLargeFileToAsync>d__.<>1__state = -1;
			<DownloadLargeFileToAsync>d__.<>t__builder.Start<FilesStorageClientManager.<DownloadLargeFileToAsync>d__23>(ref <DownloadLargeFileToAsync>d__);
			return <DownloadLargeFileToAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0000E69C File Offset: 0x0000C89C
		[DebuggerStepThrough]
		public Task DownloadLargeTempFileToAsync(FileIdentifier fileId, string filename)
		{
			FilesStorageClientManager.<DownloadLargeTempFileToAsync>d__24 <DownloadLargeTempFileToAsync>d__ = new FilesStorageClientManager.<DownloadLargeTempFileToAsync>d__24();
			<DownloadLargeTempFileToAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<DownloadLargeTempFileToAsync>d__.<>4__this = this;
			<DownloadLargeTempFileToAsync>d__.fileId = fileId;
			<DownloadLargeTempFileToAsync>d__.filename = filename;
			<DownloadLargeTempFileToAsync>d__.<>1__state = -1;
			<DownloadLargeTempFileToAsync>d__.<>t__builder.Start<FilesStorageClientManager.<DownloadLargeTempFileToAsync>d__24>(ref <DownloadLargeTempFileToAsync>d__);
			return <DownloadLargeTempFileToAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0000E6F0 File Offset: 0x0000C8F0
		[DebuggerStepThrough]
		public Task DownloadFileToAsync(FileIdentifier fileId, string filename, long size, CancellationToken cancellationToken)
		{
			FilesStorageClientManager.<DownloadFileToAsync>d__25 <DownloadFileToAsync>d__ = new FilesStorageClientManager.<DownloadFileToAsync>d__25();
			<DownloadFileToAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<DownloadFileToAsync>d__.<>4__this = this;
			<DownloadFileToAsync>d__.fileId = fileId;
			<DownloadFileToAsync>d__.filename = filename;
			<DownloadFileToAsync>d__.size = size;
			<DownloadFileToAsync>d__.cancellationToken = cancellationToken;
			<DownloadFileToAsync>d__.<>1__state = -1;
			<DownloadFileToAsync>d__.<>t__builder.Start<FilesStorageClientManager.<DownloadFileToAsync>d__25>(ref <DownloadFileToAsync>d__);
			return <DownloadFileToAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0000E754 File Offset: 0x0000C954
		public BasicFileInfoDTO UploadFileFrom(string filename, eFileSource source)
		{
			FileInfo fileInfo = new FileInfo(filename);
			bool flag = !fileInfo.Exists;
			BasicFileInfoDTO result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = fileInfo.Length >= 1048576L;
				if (flag2)
				{
					StreamingFileDTO file = new StreamingFileDTO
					{
						FileName = Path.GetFileName(filename),
						Length = fileInfo.Length,
						FileByteStream = fileInfo.Open(FileMode.Open, FileAccess.Read),
						FileIdentifier = new FileIdentifierMessageDTO
						{
							Source = source
						}
					};
					result = this.UploadLargeFile(file);
				}
				else
				{
					InMemoryFileDTO file2 = new InMemoryFileDTO
					{
						FileName = Path.GetFileName(filename),
						Length = fileInfo.Length,
						FileData = File.ReadAllBytes(filename),
						FileIdentifier = new FileIdentifierDTO
						{
							Source = source
						}
					};
					result = this.UploadFile(file2);
				}
			}
			return result;
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0000E834 File Offset: 0x0000CA34
		[DebuggerStepThrough]
		public Task<BasicFileInfoDTO> UploadFileFromAsync(string filename, eFileSource source)
		{
			FilesStorageClientManager.<UploadFileFromAsync>d__27 <UploadFileFromAsync>d__ = new FilesStorageClientManager.<UploadFileFromAsync>d__27();
			<UploadFileFromAsync>d__.<>t__builder = AsyncTaskMethodBuilder<BasicFileInfoDTO>.Create();
			<UploadFileFromAsync>d__.<>4__this = this;
			<UploadFileFromAsync>d__.filename = filename;
			<UploadFileFromAsync>d__.source = source;
			<UploadFileFromAsync>d__.<>1__state = -1;
			<UploadFileFromAsync>d__.<>t__builder.Start<FilesStorageClientManager.<UploadFileFromAsync>d__27>(ref <UploadFileFromAsync>d__);
			return <UploadFileFromAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000351 RID: 849 RVA: 0x0000E888 File Offset: 0x0000CA88
		[DebuggerStepThrough]
		public Task<BasicFileInfoDTO> UploadFileFromAsync(Stream stream, string filename, long fileSize, eFileSource source)
		{
			FilesStorageClientManager.<UploadFileFromAsync>d__28 <UploadFileFromAsync>d__ = new FilesStorageClientManager.<UploadFileFromAsync>d__28();
			<UploadFileFromAsync>d__.<>t__builder = AsyncTaskMethodBuilder<BasicFileInfoDTO>.Create();
			<UploadFileFromAsync>d__.<>4__this = this;
			<UploadFileFromAsync>d__.stream = stream;
			<UploadFileFromAsync>d__.filename = filename;
			<UploadFileFromAsync>d__.fileSize = fileSize;
			<UploadFileFromAsync>d__.source = source;
			<UploadFileFromAsync>d__.<>1__state = -1;
			<UploadFileFromAsync>d__.<>t__builder.Start<FilesStorageClientManager.<UploadFileFromAsync>d__28>(ref <UploadFileFromAsync>d__);
			return <UploadFileFromAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000352 RID: 850 RVA: 0x0000E8EC File Offset: 0x0000CAEC
		public void DeleteFile(FileIdentifier fileIdentifier)
		{
			DeleteFileReq deleteFileReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<DeleteFileReq>();
			deleteFileReq.FileIdentifier = fileIdentifier.ToDTO();
			ClientServiceFactory.GetClientInstance<IInMemoryFilesStorage>().DeleteFile(deleteFileReq);
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0000E920 File Offset: 0x0000CB20
		[DebuggerStepThrough]
		public Task DeleteFileAsync(FileIdentifier fileIdentifier)
		{
			FilesStorageClientManager.<DeleteFileAsync>d__30 <DeleteFileAsync>d__ = new FilesStorageClientManager.<DeleteFileAsync>d__30();
			<DeleteFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<DeleteFileAsync>d__.<>4__this = this;
			<DeleteFileAsync>d__.fileIdentifier = fileIdentifier;
			<DeleteFileAsync>d__.<>1__state = -1;
			<DeleteFileAsync>d__.<>t__builder.Start<FilesStorageClientManager.<DeleteFileAsync>d__30>(ref <DeleteFileAsync>d__);
			return <DeleteFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0000E96C File Offset: 0x0000CB6C
		public void DeleteTempFile(FileIdentifier fileId)
		{
			DeleteFileReq deleteFileReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<DeleteFileReq>();
			deleteFileReq.FileIdentifier = fileId.ToDTO();
			ClientServiceFactory.GetClientInstance<IInMemoryFilesStorage>().DeleteTempFile(deleteFileReq);
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0000E9A0 File Offset: 0x0000CBA0
		[DebuggerStepThrough]
		public Task DeleteTempFileAsync(FileIdentifier fileId)
		{
			FilesStorageClientManager.<DeleteTempFileAsync>d__32 <DeleteTempFileAsync>d__ = new FilesStorageClientManager.<DeleteTempFileAsync>d__32();
			<DeleteTempFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<DeleteTempFileAsync>d__.<>4__this = this;
			<DeleteTempFileAsync>d__.fileId = fileId;
			<DeleteTempFileAsync>d__.<>1__state = -1;
			<DeleteTempFileAsync>d__.<>t__builder.Start<FilesStorageClientManager.<DeleteTempFileAsync>d__32>(ref <DeleteTempFileAsync>d__);
			return <DeleteTempFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x04000010 RID: 16
		public const long MAXIMUM_FILE_SIZE_IN_MEMORY = 1048576L;
	}
}
