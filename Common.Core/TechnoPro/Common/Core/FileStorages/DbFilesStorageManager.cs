using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TechnoPro.Common.DAO.Impl.FileStorages;
using TechnoPro.Common.ICore.FileStorages;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.FileStorage;

namespace TechnoPro.Common.Core.FileStorages
{
	// Token: 0x020000F1 RID: 241
	public class DbFilesStorageManager : IFilesStorageManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000952 RID: 2386 RVA: 0x0000672B File Offset: 0x0000492B
		public DbFilesStorageManager()
		{
		}

		// Token: 0x06000953 RID: 2387 RVA: 0x0003BAE4 File Offset: 0x00039CE4
		public DbFilesStorageManager(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x0003BAF8 File Offset: 0x00039CF8
		public StreamingFile DownloadLargeFile(FileIdentifier fileId)
		{
			return new DbFilesStorageDAO(this.OpContext).DownloadLargeFile(fileId);
		}

		// Token: 0x06000955 RID: 2389 RVA: 0x0003BB1C File Offset: 0x00039D1C
		[DebuggerStepThrough]
		public Task<StreamingFile> DownloadLargeFileAsync(FileIdentifier fileId)
		{
			DbFilesStorageManager.<DownloadLargeFileAsync>d__3 <DownloadLargeFileAsync>d__ = new DbFilesStorageManager.<DownloadLargeFileAsync>d__3();
			<DownloadLargeFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder<StreamingFile>.Create();
			<DownloadLargeFileAsync>d__.<>4__this = this;
			<DownloadLargeFileAsync>d__.fileId = fileId;
			<DownloadLargeFileAsync>d__.<>1__state = -1;
			<DownloadLargeFileAsync>d__.<>t__builder.Start<DbFilesStorageManager.<DownloadLargeFileAsync>d__3>(ref <DownloadLargeFileAsync>d__);
			return <DownloadLargeFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x0003BB68 File Offset: 0x00039D68
		public BasicFileInfo UploadLargeFile(StreamingFile file)
		{
			return new DbFilesStorageDAO(this.OpContext).UploadLargeFile(file);
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x0003BB8C File Offset: 0x00039D8C
		[DebuggerStepThrough]
		public Task<BasicFileInfo> UploadLargeFileAsync(StreamingFile file)
		{
			DbFilesStorageManager.<UploadLargeFileAsync>d__5 <UploadLargeFileAsync>d__ = new DbFilesStorageManager.<UploadLargeFileAsync>d__5();
			<UploadLargeFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder<BasicFileInfo>.Create();
			<UploadLargeFileAsync>d__.<>4__this = this;
			<UploadLargeFileAsync>d__.file = file;
			<UploadLargeFileAsync>d__.<>1__state = -1;
			<UploadLargeFileAsync>d__.<>t__builder.Start<DbFilesStorageManager.<UploadLargeFileAsync>d__5>(ref <UploadLargeFileAsync>d__);
			return <UploadLargeFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000958 RID: 2392 RVA: 0x0003BBD8 File Offset: 0x00039DD8
		public StreamingFile DownloadLargeTempFile(FileIdentifier fileId)
		{
			return new DbFilesStorageDAO(this.OpContext).DownloadLargeTempFile(fileId);
		}

		// Token: 0x06000959 RID: 2393 RVA: 0x0003BBFC File Offset: 0x00039DFC
		[DebuggerStepThrough]
		public Task<StreamingFile> DownloadLargeTempFileAsync(FileIdentifier fileId)
		{
			DbFilesStorageManager.<DownloadLargeTempFileAsync>d__7 <DownloadLargeTempFileAsync>d__ = new DbFilesStorageManager.<DownloadLargeTempFileAsync>d__7();
			<DownloadLargeTempFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder<StreamingFile>.Create();
			<DownloadLargeTempFileAsync>d__.<>4__this = this;
			<DownloadLargeTempFileAsync>d__.fileId = fileId;
			<DownloadLargeTempFileAsync>d__.<>1__state = -1;
			<DownloadLargeTempFileAsync>d__.<>t__builder.Start<DbFilesStorageManager.<DownloadLargeTempFileAsync>d__7>(ref <DownloadLargeTempFileAsync>d__);
			return <DownloadLargeTempFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600095A RID: 2394 RVA: 0x0003BC48 File Offset: 0x00039E48
		public BasicFileInfo UploadLargeTempFile(StreamingFile file)
		{
			return new DbFilesStorageDAO(this.OpContext).UploadLargeTempFile(file);
		}

		// Token: 0x0600095B RID: 2395 RVA: 0x0003BC6C File Offset: 0x00039E6C
		[DebuggerStepThrough]
		public Task<BasicFileInfo> UploadLargeTempFileAsync(StreamingFile file)
		{
			DbFilesStorageManager.<UploadLargeTempFileAsync>d__9 <UploadLargeTempFileAsync>d__ = new DbFilesStorageManager.<UploadLargeTempFileAsync>d__9();
			<UploadLargeTempFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder<BasicFileInfo>.Create();
			<UploadLargeTempFileAsync>d__.<>4__this = this;
			<UploadLargeTempFileAsync>d__.file = file;
			<UploadLargeTempFileAsync>d__.<>1__state = -1;
			<UploadLargeTempFileAsync>d__.<>t__builder.Start<DbFilesStorageManager.<UploadLargeTempFileAsync>d__9>(ref <UploadLargeTempFileAsync>d__);
			return <UploadLargeTempFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600095C RID: 2396 RVA: 0x0003BCB8 File Offset: 0x00039EB8
		public InMemoryFile DownloadFile(FileIdentifier fileId)
		{
			return new DbFilesStorageDAO(this.OpContext).DownloadFile(fileId);
		}

		// Token: 0x0600095D RID: 2397 RVA: 0x0003BCDC File Offset: 0x00039EDC
		[DebuggerStepThrough]
		public Task<InMemoryFile> DownloadFileAsync(FileIdentifier fileId)
		{
			DbFilesStorageManager.<DownloadFileAsync>d__11 <DownloadFileAsync>d__ = new DbFilesStorageManager.<DownloadFileAsync>d__11();
			<DownloadFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder<InMemoryFile>.Create();
			<DownloadFileAsync>d__.<>4__this = this;
			<DownloadFileAsync>d__.fileId = fileId;
			<DownloadFileAsync>d__.<>1__state = -1;
			<DownloadFileAsync>d__.<>t__builder.Start<DbFilesStorageManager.<DownloadFileAsync>d__11>(ref <DownloadFileAsync>d__);
			return <DownloadFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x0003BD28 File Offset: 0x00039F28
		public BasicFileInfo UploadFile(InMemoryFile file)
		{
			return new DbFilesStorageDAO(this.OpContext).UploadFile(file);
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x0003BD4C File Offset: 0x00039F4C
		[DebuggerStepThrough]
		public Task<BasicFileInfo> UploadFileAsync(InMemoryFile file)
		{
			DbFilesStorageManager.<UploadFileAsync>d__13 <UploadFileAsync>d__ = new DbFilesStorageManager.<UploadFileAsync>d__13();
			<UploadFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder<BasicFileInfo>.Create();
			<UploadFileAsync>d__.<>4__this = this;
			<UploadFileAsync>d__.file = file;
			<UploadFileAsync>d__.<>1__state = -1;
			<UploadFileAsync>d__.<>t__builder.Start<DbFilesStorageManager.<UploadFileAsync>d__13>(ref <UploadFileAsync>d__);
			return <UploadFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000960 RID: 2400 RVA: 0x0003BD98 File Offset: 0x00039F98
		public InMemoryFile DownloadTempFile(FileIdentifier fileId)
		{
			return new DbFilesStorageDAO(this.OpContext).DownloadTempFile(fileId);
		}

		// Token: 0x06000961 RID: 2401 RVA: 0x0003BDBC File Offset: 0x00039FBC
		[DebuggerStepThrough]
		public Task<InMemoryFile> DownloadTempFileAsync(FileIdentifier fileId)
		{
			DbFilesStorageManager.<DownloadTempFileAsync>d__15 <DownloadTempFileAsync>d__ = new DbFilesStorageManager.<DownloadTempFileAsync>d__15();
			<DownloadTempFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder<InMemoryFile>.Create();
			<DownloadTempFileAsync>d__.<>4__this = this;
			<DownloadTempFileAsync>d__.fileId = fileId;
			<DownloadTempFileAsync>d__.<>1__state = -1;
			<DownloadTempFileAsync>d__.<>t__builder.Start<DbFilesStorageManager.<DownloadTempFileAsync>d__15>(ref <DownloadTempFileAsync>d__);
			return <DownloadTempFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000962 RID: 2402 RVA: 0x0003BE08 File Offset: 0x0003A008
		public BasicFileInfo UploadTempFile(InMemoryFile file)
		{
			return new DbFilesStorageDAO(this.OpContext).UploadTempFile(file);
		}

		// Token: 0x06000963 RID: 2403 RVA: 0x0003BE2C File Offset: 0x0003A02C
		[DebuggerStepThrough]
		public Task<BasicFileInfo> UploadTempFileAsync(InMemoryFile file)
		{
			DbFilesStorageManager.<UploadTempFileAsync>d__17 <UploadTempFileAsync>d__ = new DbFilesStorageManager.<UploadTempFileAsync>d__17();
			<UploadTempFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder<BasicFileInfo>.Create();
			<UploadTempFileAsync>d__.<>4__this = this;
			<UploadTempFileAsync>d__.file = file;
			<UploadTempFileAsync>d__.<>1__state = -1;
			<UploadTempFileAsync>d__.<>t__builder.Start<DbFilesStorageManager.<UploadTempFileAsync>d__17>(ref <UploadTempFileAsync>d__);
			return <UploadTempFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000964 RID: 2404 RVA: 0x0003BE77 File Offset: 0x0003A077
		public void DeleteFile(FileIdentifier fileId)
		{
			new DbFilesStorageDAO(this.OpContext).DeleteFile(fileId);
		}

		// Token: 0x06000965 RID: 2405 RVA: 0x0003BE8C File Offset: 0x0003A08C
		[DebuggerStepThrough]
		public Task DeleteFileAsync(FileIdentifier fileId)
		{
			DbFilesStorageManager.<DeleteFileAsync>d__19 <DeleteFileAsync>d__ = new DbFilesStorageManager.<DeleteFileAsync>d__19();
			<DeleteFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<DeleteFileAsync>d__.<>4__this = this;
			<DeleteFileAsync>d__.fileId = fileId;
			<DeleteFileAsync>d__.<>1__state = -1;
			<DeleteFileAsync>d__.<>t__builder.Start<DbFilesStorageManager.<DeleteFileAsync>d__19>(ref <DeleteFileAsync>d__);
			return <DeleteFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000966 RID: 2406 RVA: 0x0003BED7 File Offset: 0x0003A0D7
		public void DeleteTempFile(FileIdentifier fileId)
		{
			new DbFilesStorageDAO(this.OpContext).DeleteTempFile(fileId);
		}

		// Token: 0x06000967 RID: 2407 RVA: 0x0003BEEC File Offset: 0x0003A0EC
		[DebuggerStepThrough]
		public Task DeleteTempFileAsync(FileIdentifier fileId)
		{
			DbFilesStorageManager.<DeleteTempFileAsync>d__21 <DeleteTempFileAsync>d__ = new DbFilesStorageManager.<DeleteTempFileAsync>d__21();
			<DeleteTempFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<DeleteTempFileAsync>d__.<>4__this = this;
			<DeleteTempFileAsync>d__.fileId = fileId;
			<DeleteTempFileAsync>d__.<>1__state = -1;
			<DeleteTempFileAsync>d__.<>t__builder.Start<DbFilesStorageManager.<DeleteTempFileAsync>d__21>(ref <DeleteTempFileAsync>d__);
			return <DeleteTempFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000968 RID: 2408 RVA: 0x0003BF37 File Offset: 0x0003A137
		public void DeleteTempFilesOlderThan(DateTimeOffset date)
		{
			new DbFilesStorageDAO(this.OpContext).DeleteTempFilesOlderThan(date);
		}

		// Token: 0x06000969 RID: 2409 RVA: 0x0003BF4C File Offset: 0x0003A14C
		[DebuggerStepThrough]
		public Task DeleteTempFilesOlderThanAsync(DateTimeOffset date)
		{
			DbFilesStorageManager.<DeleteTempFilesOlderThanAsync>d__23 <DeleteTempFilesOlderThanAsync>d__ = new DbFilesStorageManager.<DeleteTempFilesOlderThanAsync>d__23();
			<DeleteTempFilesOlderThanAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<DeleteTempFilesOlderThanAsync>d__.<>4__this = this;
			<DeleteTempFilesOlderThanAsync>d__.date = date;
			<DeleteTempFilesOlderThanAsync>d__.<>1__state = -1;
			<DeleteTempFilesOlderThanAsync>d__.<>t__builder.Start<DbFilesStorageManager.<DeleteTempFilesOlderThanAsync>d__23>(ref <DeleteTempFilesOlderThanAsync>d__);
			return <DeleteTempFilesOlderThanAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x0003BF97 File Offset: 0x0003A197
		public void MoveTempFileToPersistentStorage(FileIdentifier fileId)
		{
			new DbFilesStorageDAO(this.OpContext).MoveTempFileToPersistentStorage(fileId);
		}

		// Token: 0x0600096B RID: 2411 RVA: 0x0003BFAC File Offset: 0x0003A1AC
		[DebuggerStepThrough]
		public Task MoveTempFileToPersistentStorageAsync(FileIdentifier fileId)
		{
			DbFilesStorageManager.<MoveTempFileToPersistentStorageAsync>d__25 <MoveTempFileToPersistentStorageAsync>d__ = new DbFilesStorageManager.<MoveTempFileToPersistentStorageAsync>d__25();
			<MoveTempFileToPersistentStorageAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<MoveTempFileToPersistentStorageAsync>d__.<>4__this = this;
			<MoveTempFileToPersistentStorageAsync>d__.fileId = fileId;
			<MoveTempFileToPersistentStorageAsync>d__.<>1__state = -1;
			<MoveTempFileToPersistentStorageAsync>d__.<>t__builder.Start<DbFilesStorageManager.<MoveTempFileToPersistentStorageAsync>d__25>(ref <MoveTempFileToPersistentStorageAsync>d__);
			return <MoveTempFileToPersistentStorageAsync>d__.<>t__builder.Task;
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x0600096C RID: 2412 RVA: 0x0003BFF7 File Offset: 0x0003A1F7
		// (set) Token: 0x0600096D RID: 2413 RVA: 0x0003BFFF File Offset: 0x0003A1FF
		public OperationContext OpContext { get; set; }
	}
}
