using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using ClockWorkLogger;
using TechnoPro.ClockWorkServer.Contracts.DTO.FileStorage;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.ClockWorkWeb.Areas.AlternateFormat.Infrastructure;
using TechnoPro.ClockWorkWeb.Binders;
using TechnoPro.Common.ClientManager.Core.FileStorage;
using TechnoPro.Common.ClientManager.ICore.FileStorage;
using TechnoPro.Common.Public.Entities.FileStorage;

namespace TechnoPro.ClockWorkWeb.Controllers
{
	// Token: 0x02000155 RID: 341
	[NoCache]
	public class FilesStorageController : Controller
	{
		// Token: 0x06000A7F RID: 2687 RVA: 0x00048640 File Offset: 0x00046840
		public async Task<ActionResult> DownloadFile([ModelBinder(typeof(LogonStudentModelBinder))] PersonBaseDTO student, Guid? fileId, int legacyId, eFileSource source, long fileLength = 0L)
		{
			IFilesStorageClientManager filesStorageClientManager = new FilesStorageClientManager();
			FileIdentifier fileIdentifier = new FileIdentifier
			{
				FileUniqueId = fileId,
				LegacyId = legacyId,
				Source = source
			};
			bool flag = fileLength == 0L || fileLength > 1048576L;
			if (flag)
			{
				StreamingFileDTO streamingFileDTO = await filesStorageClientManager.DownloadLargeFileAsync(fileIdentifier);
				StreamingFileDTO resp = streamingFileDTO;
				streamingFileDTO = null;
				StreamingFileDTO streamingFileDTO2 = resp;
				if (((streamingFileDTO2 != null) ? streamingFileDTO2.FileByteStream : null) != null)
				{
					return this.File(resp.FileByteStream, "application/octet-stream", resp.FileName);
				}
				resp = null;
			}
			else
			{
				InMemoryFileDTO inMemoryFileDTO = await filesStorageClientManager.DownloadFileAsync(fileIdentifier);
				InMemoryFileDTO resp2 = inMemoryFileDTO;
				inMemoryFileDTO = null;
				InMemoryFileDTO inMemoryFileDTO2 = resp2;
				if (((inMemoryFileDTO2 != null) ? inMemoryFileDTO2.FileData : null) != null)
				{
					return this.File(resp2.FileData, "application/octet-stream", resp2.FileName);
				}
				resp2 = null;
			}
			CWLogger.Logger.Error(string.Format("File fileUniqueId={0}, fileLegacyId={1}, Source={2} does not exist in Files database", fileIdentifier.FileUniqueId, fileIdentifier.LegacyId, fileIdentifier.Source));
			return new HttpStatusCodeResult(HttpStatusCode.NoContent);
		}
	}
}
