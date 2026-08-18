using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files.FileUpload;
using TechnoPro.Common.ClientManager.Core.TempFiles;
using TechnoPro.Common.ClientManager.ICore.TempFiles;
using TechnoPro.Common.Public.Entities.Files.FileUpload;
using TechnoPro.Common.UI.ClientManager.Web.Core.Web;
using TechnoPro.Common.UI.Web.Entity.Common.FileUpload;

namespace TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web
{
	// Token: 0x0200000E RID: 14
	public class FileUploadWebClientManager : IFileUploadWebClientManager
	{
		// Token: 0x0600003C RID: 60 RVA: 0x00002D44 File Offset: 0x00000F44
		public void RemoveFileForUploadSessionInfo(string guid)
		{
			HttpSessionState session = HttpContext.Current.Session;
			string sessionKey = FileUploadWebClientManager.GetSessionKey(guid);
			object obj = session[sessionKey];
			bool flag = obj == null || !(obj is FileForUploadSet);
			if (!flag)
			{
				FileForUploadSet fileForUploadSet = (FileForUploadSet)obj;
				session.Remove(sessionKey);
				foreach (FileForUpload fileForUpload in fileForUploadSet.FilesForUpload)
				{
					this.DeleteFileForUpload(guid, fileForUpload.FileForUploadId);
				}
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002DE8 File Offset: 0x00000FE8
		public void ClearOldFileForUploadSessionInfos()
		{
			HttpSessionState session = HttpContext.Current.Session;
			List<string> list = (from string key in session.Keys
			where key.StartsWith("FileForUploadInfo_")
			select key).Where(new Func<string, bool>(FileUploadWebClientManager.IsSessionInfoOld)).ToList<string>();
			foreach (string name in list)
			{
				session.Remove(name);
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002E8C File Offset: 0x0000108C
		private static bool IsSessionInfoOld(string sessionKey)
		{
			HttpSessionState session = HttpContext.Current.Session;
			FileForUploadSet fileForUploadInfoFromSession = FileUploadWebClientManager.GetFileForUploadInfoFromSession(session, sessionKey);
			return fileForUploadInfoFromSession == null || (DateTime.Now - fileForUploadInfoFromSession.DateCreated).TotalDays > 0.0;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002EDC File Offset: 0x000010DC
		private static FileForUploadSet GetFileForUploadInfoFromSession(HttpSessionState session, string sessionKey)
		{
			object obj = session[sessionKey];
			return (obj == null || !(obj is FileForUploadSet)) ? null : ((FileForUploadSet)obj);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002F0C File Offset: 0x0000110C
		public FileForUploadSet GetFileForUploadInfoFromSession(string guid)
		{
			HttpSessionState session = HttpContext.Current.Session;
			string sessionKey = FileUploadWebClientManager.GetSessionKey(guid);
			return FileUploadWebClientManager.GetFileForUploadInfoFromSession(session, sessionKey) ?? this.CreateNewFileForUploadInfoInSession(guid);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002F44 File Offset: 0x00001144
		public void UpdateFileForUploadInfoInSession(FileForUploadSet fileForUploadInfo)
		{
			HttpSessionState session = HttpContext.Current.Session;
			string sessionKey = FileUploadWebClientManager.GetSessionKey(fileForUploadInfo.Guid);
			session.Add(sessionKey, fileForUploadInfo);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002F74 File Offset: 0x00001174
		private static string GetSessionKey(string guid)
		{
			return "FileForUploadInfo_" + guid;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002F94 File Offset: 0x00001194
		public FileForUploadSet CreateNewFileForUploadInfoInSession()
		{
			FileForUploadSet fileForUploadSet = new FileForUploadSet();
			this.UpdateFileForUploadInfoInSession(fileForUploadSet);
			return fileForUploadSet;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002FB8 File Offset: 0x000011B8
		public FileForUploadSet CreateNewFileForUploadInfoInSession(string guid)
		{
			this.RemoveFileForUploadSessionInfo(guid);
			FileForUploadSet fileForUploadSet = new FileForUploadSet(guid);
			this.UpdateFileForUploadInfoInSession(fileForUploadSet);
			return fileForUploadSet;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002FE4 File Offset: 0x000011E4
		public FileForUpload AddFileForUpload(string guid, string filename, byte[] fileBytes)
		{
			FileForUploadSet fileForUploadInfoFromSession = this.GetFileForUploadInfoFromSession(guid);
			ITempFileClientManager tempFileClientManager = new TempFileClientManager();
			int fileForUploadId = tempFileClientManager.AddNewTempFile(new TempFileContextDTO
			{
				Usage = eTempFileUsage.InstructorUpload,
				GroupId = guid
			}, new BinaryFileDTO
			{
				FileName = filename,
				ByteArray = fileBytes,
				FileSize = fileBytes.Length
			});
			FileForUpload fileForUpload = new FileForUpload
			{
				FileForUploadId = fileForUploadId,
				Filename = filename,
				FileSize = (long)fileBytes.Length
			};
			fileForUploadInfoFromSession.FilesForUpload.Add(fileForUpload);
			return fileForUpload;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00003070 File Offset: 0x00001270
		public void DeleteFileForUpload(string guid, int fileForUploadId)
		{
			FileForUploadSet fileForUploadInfoFromSession = this.GetFileForUploadInfoFromSession(guid);
			FileForUpload fileForUpload = fileForUploadInfoFromSession.FilesForUpload.FirstOrDefault((FileForUpload g) => g.FileForUploadId == fileForUploadId);
			bool flag = fileForUpload == null;
			if (!flag)
			{
				fileForUploadInfoFromSession.FilesForUpload.Remove(fileForUpload);
				ITempFileClientManager tempFileClientManager = new TempFileClientManager();
				tempFileClientManager.DeleteTempFile(new TempFileContextDTO
				{
					Usage = eTempFileUsage.InstructorUpload,
					GroupId = guid
				}, fileForUploadId);
			}
		}

		// Token: 0x04000010 RID: 16
		private const string SessionKeyPrefix = "FileForUploadInfo_";
	}
}
