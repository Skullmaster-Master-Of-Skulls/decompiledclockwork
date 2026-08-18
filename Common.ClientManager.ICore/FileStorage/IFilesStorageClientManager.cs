using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Contracts.DTO.FileStorage;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.FileStorage;

namespace TechnoPro.Common.ClientManager.ICore.FileStorage
{
	// Token: 0x02000054 RID: 84
	public interface IFilesStorageClientManager : IWebService
	{
		// Token: 0x0600026E RID: 622
		StreamingFileDTO DownloadLargeFile(FileIdentifier fileId);

		// Token: 0x0600026F RID: 623
		Task<StreamingFileDTO> DownloadLargeFileAsync(FileIdentifier fileId);

		// Token: 0x06000270 RID: 624
		BasicFileInfoDTO UploadLargeFile(StreamingFileDTO file);

		// Token: 0x06000271 RID: 625
		Task<BasicFileInfoDTO> UploadLargeFileAsync(StreamingFileDTO file);

		// Token: 0x06000272 RID: 626
		StreamingFileDTO DownloadLargeTempFile(FileIdentifier fileId);

		// Token: 0x06000273 RID: 627
		Task<StreamingFileDTO> DownloadLargeTempFileAsync(FileIdentifier fileId);

		// Token: 0x06000274 RID: 628
		BasicFileInfoDTO UploadLargeTempFile(StreamingFileDTO file);

		// Token: 0x06000275 RID: 629
		Task<BasicFileInfoDTO> UploadLargeTempFileAsync(StreamingFileDTO file);

		// Token: 0x06000276 RID: 630
		InMemoryFileDTO DownloadFile(FileIdentifier fileId);

		// Token: 0x06000277 RID: 631
		Task<InMemoryFileDTO> DownloadFileAsync(FileIdentifier fileId);

		// Token: 0x06000278 RID: 632
		BasicFileInfoDTO UploadFile(InMemoryFileDTO file);

		// Token: 0x06000279 RID: 633
		Task<BasicFileInfoDTO> UploadFileAsync(InMemoryFileDTO file);

		// Token: 0x0600027A RID: 634
		InMemoryFileDTO DownloadTempFile(FileIdentifier fileId);

		// Token: 0x0600027B RID: 635
		Task<InMemoryFileDTO> DownloadTempFileAsync(FileIdentifier fileId);

		// Token: 0x0600027C RID: 636
		BasicFileInfoDTO UploadTempFile(InMemoryFileDTO file);

		// Token: 0x0600027D RID: 637
		Task<BasicFileInfoDTO> UploadTempFileAsync(InMemoryFileDTO file);

		// Token: 0x0600027E RID: 638
		void ZipFiles(Stream zipStream, params FileIdentifier[] fileIds);

		// Token: 0x0600027F RID: 639
		Task ZipFilesAsync(Stream zipStream, params FileIdentifier[] fileIds);

		// Token: 0x06000280 RID: 640
		Task<Stream> ZipFilesAsync(params FileIdentifier[] fileIds);

		// Token: 0x06000281 RID: 641
		Task ZipFilesAsync(Stream zipStream, CancellationToken cancellationToken, params FileIdentifier[] fileIds);

		// Token: 0x06000282 RID: 642
		void DownloadFileTo(FileIdentifier fileId, string filename, long size);

		// Token: 0x06000283 RID: 643
		Task DownloadFileToAsync(FileIdentifier fileId, string filename, long size);

		// Token: 0x06000284 RID: 644
		Task DownloadLargeFileToAsync(FileIdentifier fileId, string filename);

		// Token: 0x06000285 RID: 645
		Task DownloadLargeTempFileToAsync(FileIdentifier fileId, string filename);

		// Token: 0x06000286 RID: 646
		Task DownloadFileToAsync(FileIdentifier fileId, string filename, long size, CancellationToken cancellationToken);

		// Token: 0x06000287 RID: 647
		BasicFileInfoDTO UploadFileFrom(string filename, eFileSource source);

		// Token: 0x06000288 RID: 648
		Task<BasicFileInfoDTO> UploadFileFromAsync(string filename, eFileSource source);

		// Token: 0x06000289 RID: 649
		Task<BasicFileInfoDTO> UploadFileFromAsync(Stream stream, string filename, long fileSize, eFileSource source);

		// Token: 0x0600028A RID: 650
		void DeleteFile(FileIdentifier fileIdentifier);

		// Token: 0x0600028B RID: 651
		Task DeleteFileAsync(FileIdentifier fileIdentifier);

		// Token: 0x0600028C RID: 652
		void DeleteTempFile(FileIdentifier fileId);

		// Token: 0x0600028D RID: 653
		Task DeleteTempFileAsync(FileIdentifier fileId);
	}
}
