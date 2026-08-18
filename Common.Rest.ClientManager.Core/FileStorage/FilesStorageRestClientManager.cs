using System;
using System.IO;
using System.IO.Compression;
using System.Threading;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Contracts.DTO.FileStorage;
using TechnoPro.Common.ClientManager.ICore.FileStorage;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.FileStorage;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.FileStorage
{
	// Token: 0x0200004B RID: 75
	public class FilesStorageRestClientManager : BearerTokenRestProxy<IFilesStorageClientManager>, IFilesStorageClientManager, IWebService
	{
		// Token: 0x060002C0 RID: 704 RVA: 0x0000818F File Offset: 0x0000638F
		public FilesStorageRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x00008199 File Offset: 0x00006399
		public FilesStorageRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x000081A4 File Offset: 0x000063A4
		public StreamingFileDTO DownloadLargeFile(FileIdentifier fileId)
		{
			return base.Get<StreamingFileDTO>(string.Format("largefilestreaming/fileid/{0}", fileId), true);
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x000081B8 File Offset: 0x000063B8
		public async Task<StreamingFileDTO> DownloadLargeFileAsync(FileIdentifier fileId)
		{
			return await this.GetAsync<StreamingFileDTO>(string.Format("largefilestreaming/fileid/{0}", fileId), true);
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x00008205 File Offset: 0x00006405
		public BasicFileInfoDTO UploadLargeFile(StreamingFileDTO file)
		{
			return base.Post<StreamingFileDTO, BasicFileInfoDTO>(file, "largefilestreaming/uploadfile");
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x00008214 File Offset: 0x00006414
		public async Task<BasicFileInfoDTO> UploadLargeFileAsync(StreamingFileDTO file)
		{
			return await this.PostAsync<StreamingFileDTO, BasicFileInfoDTO>(file, "largefilestreaming/uploadfile");
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x00008264 File Offset: 0x00006464
		public StreamingFileDTO DownloadLargeTempFile(FileIdentifier fileId)
		{
			if (fileId.FileUniqueId == null)
			{
				return base.Get<StreamingFileDTO>(string.Format("largefilestreaming/tempfile/legacyid/{0}/source/{1}", fileId.LegacyId, fileId.Source), true);
			}
			return base.Get<StreamingFileDTO>(string.Format("largefilestreaming/tempfile/fileid/{0}", fileId.FileUniqueId), true);
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x000082C8 File Offset: 0x000064C8
		public async Task<StreamingFileDTO> DownloadLargeTempFileAsync(FileIdentifier fileId)
		{
			StreamingFileDTO result;
			if (fileId.FileUniqueId != null)
			{
				result = await this.GetAsync<StreamingFileDTO>(string.Format("largefilestreaming/tempfile/fileid/{0}", fileId.FileUniqueId), true);
			}
			else
			{
				result = await this.GetAsync<StreamingFileDTO>(string.Format("largefilestreaming/tempfile/legacyid/{0}/source/{1}", fileId.LegacyId, fileId.Source), true);
			}
			return result;
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x00008315 File Offset: 0x00006515
		public BasicFileInfoDTO UploadLargeTempFile(StreamingFileDTO file)
		{
			return base.Post<StreamingFileDTO, BasicFileInfoDTO>(file, "largefilestreaming/uploadtempfile");
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x00008324 File Offset: 0x00006524
		public async Task<BasicFileInfoDTO> UploadLargeTempFileAsync(StreamingFileDTO file)
		{
			return await this.PostAsync<StreamingFileDTO, BasicFileInfoDTO>(file, "largefilestreaming/uploadtempfile");
		}

		// Token: 0x060002CA RID: 714 RVA: 0x00008374 File Offset: 0x00006574
		public InMemoryFileDTO DownloadFile(FileIdentifier fileId)
		{
			if (fileId.FileUniqueId == null)
			{
				return base.Get<InMemoryFileDTO>(string.Format("inmemoryfilesstorage/downloadfile/legacyid/{0}/source/{1}", fileId.LegacyId, fileId.Source), true);
			}
			return base.Get<InMemoryFileDTO>(string.Format("inmemoryfilesstorage/downloadfile/fileid/{0}", fileId.FileUniqueId), true);
		}

		// Token: 0x060002CB RID: 715 RVA: 0x000083D8 File Offset: 0x000065D8
		public async Task<InMemoryFileDTO> DownloadFileAsync(FileIdentifier fileId)
		{
			InMemoryFileDTO result;
			if (fileId.FileUniqueId != null)
			{
				result = await this.GetAsync<InMemoryFileDTO>(string.Format("inmemoryfilesstorage/downloadfile/fileid/{0}", fileId.FileUniqueId), true);
			}
			else
			{
				result = await this.GetAsync<InMemoryFileDTO>(string.Format("inmemoryfilesstorage/downloadfile/legacyid/{0}/source/{1}", fileId.LegacyId, fileId.Source), true);
			}
			return result;
		}

		// Token: 0x060002CC RID: 716 RVA: 0x00008425 File Offset: 0x00006625
		public BasicFileInfoDTO UploadFile(InMemoryFileDTO file)
		{
			return base.Post<InMemoryFileDTO, BasicFileInfoDTO>(file, "inmemoryfilesstorage/uploadfile");
		}

		// Token: 0x060002CD RID: 717 RVA: 0x00008434 File Offset: 0x00006634
		public async Task<BasicFileInfoDTO> UploadFileAsync(InMemoryFileDTO file)
		{
			return await this.PostAsync<InMemoryFileDTO, BasicFileInfoDTO>(file, "inmemoryfilesstorage/uploadfile");
		}

		// Token: 0x060002CE RID: 718 RVA: 0x00008484 File Offset: 0x00006684
		public InMemoryFileDTO DownloadTempFile(FileIdentifier fileId)
		{
			if (fileId.FileUniqueId == null)
			{
				return base.Get<InMemoryFileDTO>(string.Format("inmemoryfilesstorage/downloadtempfile/legacyid/{0}/source/{1}", fileId.LegacyId, fileId.Source), true);
			}
			return base.Get<InMemoryFileDTO>(string.Format("inmemoryfilesstorage/downloadtempfile/fileid/{0}", fileId.FileUniqueId), true);
		}

		// Token: 0x060002CF RID: 719 RVA: 0x000084E8 File Offset: 0x000066E8
		public async Task<InMemoryFileDTO> DownloadTempFileAsync(FileIdentifier fileId)
		{
			InMemoryFileDTO result;
			if (fileId.FileUniqueId != null)
			{
				result = await this.GetAsync<InMemoryFileDTO>(string.Format("inmemoryfilesstorage/downloadtempfile/fileid/{0}", fileId.FileUniqueId), true);
			}
			else
			{
				result = await this.GetAsync<InMemoryFileDTO>(string.Format("inmemoryfilesstorage/downloadtempfile/legacyid/{0}/source/{1}", fileId.LegacyId, fileId.Source), true);
			}
			return result;
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x00008535 File Offset: 0x00006735
		public BasicFileInfoDTO UploadTempFile(InMemoryFileDTO file)
		{
			return base.Post<InMemoryFileDTO, BasicFileInfoDTO>(file, "inmemoryfilesstorage/uploadtempfile");
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x00008544 File Offset: 0x00006744
		public async Task<BasicFileInfoDTO> UploadTempFileAsync(InMemoryFileDTO file)
		{
			return await this.PostAsync<InMemoryFileDTO, BasicFileInfoDTO>(file, "inmemoryfilesstorage/uploadtempfile");
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x00008594 File Offset: 0x00006794
		public async Task<Stream> ZipFilesAsync(params FileIdentifier[] fileIds)
		{
			MemoryStream ms = new MemoryStream();
			Stream result;
			using (ZipArchive zip = new ZipArchive(ms, ZipArchiveMode.Create, true))
			{
				foreach (FileIdentifier fileId in fileIds)
				{
					StreamingFileDTO streamingFileDTO = await this.DownloadLargeFileAsync(fileId);
					ZipArchiveEntry zipArchiveEntry = zip.CreateEntry(streamingFileDTO.FileName);
					using (Stream zipEntryStream = zipArchiveEntry.Open())
					{
						await streamingFileDTO.FileByteStream.CopyToAsync(zipEntryStream);
					}
					Stream zipEntryStream = null;
				}
				FileIdentifier[] array = null;
				ms.Position = 0L;
				result = ms;
			}
			return result;
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x000085E4 File Offset: 0x000067E4
		public void DownloadFileTo(FileIdentifier fileId, string filename, long size)
		{
			Stream stream = null;
			if (size <= 1048576L)
			{
				stream = new MemoryStream(this.DownloadFile(fileId).FileData);
			}
			else
			{
				stream = this.DownloadLargeFile(fileId).FileByteStream;
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
				if (stream != null)
				{
					stream.Close();
					stream.Dispose();
					stream = null;
				}
			}
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x00008668 File Offset: 0x00006868
		public async Task DownloadFileToAsync(FileIdentifier fileId, string filename, long size)
		{
			Stream oStream = null;
			if (size <= 1048576L)
			{
				InMemoryFileDTO inMemoryFileDTO = await this.DownloadFileAsync(fileId);
				oStream = new MemoryStream(inMemoryFileDTO.FileData);
			}
			else
			{
				oStream = (await this.DownloadLargeFileAsync(fileId)).FileByteStream;
			}
			try
			{
				using (FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.Write))
				{
					await oStream.CopyToAsync(fs);
				}
				FileStream fs = null;
			}
			finally
			{
				if (oStream != null)
				{
					oStream.Close();
					oStream.Dispose();
					oStream = null;
				}
			}
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x000086C8 File Offset: 0x000068C8
		public async Task DownloadLargeFileToAsync(FileIdentifier fileId, string filename)
		{
			StreamingFileDTO streamingFileDTO = await this.DownloadLargeFileAsync(fileId);
			Stream oStream = streamingFileDTO.FileByteStream;
			try
			{
				using (FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.Write))
				{
					await oStream.CopyToAsync(fs);
				}
				FileStream fs = null;
			}
			finally
			{
				if (oStream != null)
				{
					oStream.Close();
					oStream.Dispose();
				}
			}
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x00008720 File Offset: 0x00006920
		public async Task DownloadLargeTempFileToAsync(FileIdentifier fileId, string filename)
		{
			StreamingFileDTO streamingFileDTO = await this.DownloadLargeTempFileAsync(fileId);
			Stream oStream = streamingFileDTO.FileByteStream;
			try
			{
				using (FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.Write))
				{
					await oStream.CopyToAsync(fs);
				}
				FileStream fs = null;
			}
			finally
			{
				if (oStream != null)
				{
					oStream.Close();
					oStream.Dispose();
				}
			}
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x00008778 File Offset: 0x00006978
		public BasicFileInfoDTO UploadFileFrom(string filename, eFileSource source)
		{
			FileInfo fileInfo = new FileInfo(filename);
			if (!fileInfo.Exists)
			{
				return null;
			}
			if (fileInfo.Length >= 1048576L)
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
				return this.UploadLargeFile(file);
			}
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
			return this.UploadFile(file2);
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x00008830 File Offset: 0x00006A30
		public async Task<BasicFileInfoDTO> UploadFileFromAsync(string filename, eFileSource source)
		{
			FileInfo fileInfo = new FileInfo(filename);
			BasicFileInfoDTO result;
			if (!fileInfo.Exists)
			{
				result = null;
			}
			else if (fileInfo.Length >= 1048576L)
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
				result = await this.UploadLargeFileAsync(file);
			}
			else
			{
				result = await this.UploadFileAsync(new InMemoryFileDTO
				{
					FileName = Path.GetFileName(filename),
					Length = fileInfo.Length,
					FileData = File.ReadAllBytes(filename),
					FileIdentifier = new FileIdentifierDTO
					{
						Source = source
					}
				});
			}
			return result;
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x00008888 File Offset: 0x00006A88
		public void DeleteFile(FileIdentifier fileIdentifier)
		{
			base.Delete((fileIdentifier.FileUniqueId != null) ? string.Format("largefilestreaming/fileid/{0}", fileIdentifier.FileUniqueId) : string.Format("largefilestreaming/legacyid/{0}/source/{1}", fileIdentifier.LegacyId, fileIdentifier.Source));
		}

		// Token: 0x060002DA RID: 730 RVA: 0x000088E4 File Offset: 0x00006AE4
		public async Task DeleteFileAsync(FileIdentifier fileIdentifier)
		{
			await this.DeleteAsync((fileIdentifier.FileUniqueId != null) ? string.Format("largefilestreaming/fileid/{0}", fileIdentifier.FileUniqueId) : string.Format("largefilestreaming/legacyid/{0}/source/{1}", fileIdentifier.LegacyId, fileIdentifier.Source));
		}

		// Token: 0x060002DB RID: 731 RVA: 0x00008934 File Offset: 0x00006B34
		public void DeleteTempFile(FileIdentifier fileId)
		{
			base.Delete((fileId.FileUniqueId != null) ? string.Format("largefilestreaming/tempfileid/{0}", fileId.FileUniqueId) : string.Format("largefilestreaming/templegacyid/{0}/source/{1}", fileId.LegacyId, fileId.Source));
		}

		// Token: 0x060002DC RID: 732 RVA: 0x00008990 File Offset: 0x00006B90
		public async Task DeleteTempFileAsync(FileIdentifier fileId)
		{
			await this.DeleteAsync((fileId.FileUniqueId != null) ? string.Format("largefilestreaming/tempfileid/{0}", fileId.FileUniqueId) : string.Format("largefilestreaming/templegacyid/{0}/source/{1}", fileId.LegacyId, fileId.Source));
		}

		// Token: 0x060002DD RID: 733 RVA: 0x000089E0 File Offset: 0x00006BE0
		public async Task<BasicFileInfoDTO> UploadFileFromAsync(Stream stream, string filename, long fileSize, eFileSource source)
		{
			BasicFileInfoDTO result;
			if (fileSize > 1048576L)
			{
				result = await this.UploadLargeTempFileAsync(new StreamingFileDTO
				{
					Length = fileSize,
					FileName = filename,
					FileByteStream = stream,
					FileIdentifier = new FileIdentifierMessageDTO
					{
						Source = eFileSource.CustomForms_Files
					}
				});
			}
			else
			{
				using (MemoryStream ms = new MemoryStream())
				{
					await stream.CopyToAsync(ms);
					byte[] fileData = ms.ToArray();
					result = await this.UploadTempFileAsync(new InMemoryFileDTO
					{
						Length = fileSize,
						FileName = filename,
						FileData = fileData,
						FileIdentifier = new FileIdentifierDTO
						{
							Source = eFileSource.CustomForms_Files
						}
					});
				}
			}
			return result;
		}

		// Token: 0x060002DE RID: 734 RVA: 0x00008A40 File Offset: 0x00006C40
		public async Task DownloadFileToAsync(FileIdentifier fileId, string filename, long size, CancellationToken cancellationToken)
		{
			Stream oStream = null;
			if (size <= 1048576L)
			{
				InMemoryFileDTO inMemoryFileDTO = await this.DownloadFileAsync(fileId);
				cancellationToken.ThrowIfCancellationRequested();
				oStream = new MemoryStream(inMemoryFileDTO.FileData);
			}
			else
			{
				StreamingFileDTO streamingFileDTO = await this.DownloadLargeFileAsync(fileId);
				cancellationToken.ThrowIfCancellationRequested();
				oStream = streamingFileDTO.FileByteStream;
			}
			try
			{
				using (FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.Write))
				{
					await oStream.CopyToAsync(fs, 81920, cancellationToken);
				}
				FileStream fs = null;
			}
			finally
			{
				if (oStream != null)
				{
					oStream.Close();
					oStream.Dispose();
					oStream = null;
				}
			}
		}

		// Token: 0x060002DF RID: 735 RVA: 0x00008AA8 File Offset: 0x00006CA8
		public async Task ZipFilesAsync(Stream zipStream, CancellationToken cancellationToken, params FileIdentifier[] fileIds)
		{
			using (ZipArchive zip = new ZipArchive(zipStream, ZipArchiveMode.Create, true))
			{
				foreach (FileIdentifier fileId in fileIds)
				{
					cancellationToken.ThrowIfCancellationRequested();
					StreamingFileDTO streamingFileDTO = await this.DownloadLargeFileAsync(fileId);
					cancellationToken.ThrowIfCancellationRequested();
					ZipArchiveEntry zipArchiveEntry = zip.CreateEntry(streamingFileDTO.FileName);
					using (Stream zipEntryStream = zipArchiveEntry.Open())
					{
						await streamingFileDTO.FileByteStream.CopyToAsync(zipEntryStream, 81920, cancellationToken);
					}
					Stream zipEntryStream = null;
				}
				FileIdentifier[] array = null;
			}
			ZipArchive zip = null;
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x00008B08 File Offset: 0x00006D08
		public async Task ZipFilesAsync(Stream zipStream, params FileIdentifier[] fileIds)
		{
			using (ZipArchive zip = new ZipArchive(zipStream, ZipArchiveMode.Create, true))
			{
				foreach (FileIdentifier fileId in fileIds)
				{
					StreamingFileDTO streamingFileDTO = await this.DownloadLargeFileAsync(fileId);
					ZipArchiveEntry zipArchiveEntry = zip.CreateEntry(streamingFileDTO.FileName);
					using (Stream zipEntryStream = zipArchiveEntry.Open())
					{
						await streamingFileDTO.FileByteStream.CopyToAsync(zipEntryStream);
					}
					Stream zipEntryStream = null;
				}
				FileIdentifier[] array = null;
			}
			ZipArchive zip = null;
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x00008B60 File Offset: 0x00006D60
		public void ZipFiles(Stream zipStream, params FileIdentifier[] fileIds)
		{
			using (ZipArchive zipArchive = new ZipArchive(zipStream, ZipArchiveMode.Create, true))
			{
				foreach (FileIdentifier fileId in fileIds)
				{
					StreamingFileDTO streamingFileDTO = this.DownloadLargeFile(fileId);
					using (Stream stream = zipArchive.CreateEntry(streamingFileDTO.FileName).Open())
					{
						streamingFileDTO.FileByteStream.CopyTo(stream);
					}
				}
			}
		}

		// Token: 0x04000002 RID: 2
		public const long MAXIMUM_FILE_SIZE_IN_MEMORY = 1048576L;
	}
}
