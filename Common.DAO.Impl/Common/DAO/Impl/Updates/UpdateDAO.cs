using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using Databases;
using TechnoPro.Common.DAO.Impl.Institution;
using TechnoPro.Common.DAO.Updates;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Updates;
using TechnoPro.Common.Public.Entities.Updates.Adapters;

namespace TechnoPro.Common.DAO.Impl.Updates
{
	// Token: 0x02000031 RID: 49
	public class UpdateDAO : IUpdateDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000121 RID: 289 RVA: 0x00008E60 File Offset: 0x00007060
		// (set) Token: 0x06000122 RID: 290 RVA: 0x00008E68 File Offset: 0x00007068
		public string UpdatesPrivatePath { get; set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000123 RID: 291 RVA: 0x00008E71 File Offset: 0x00007071
		// (set) Token: 0x06000124 RID: 292 RVA: 0x00008E79 File Offset: 0x00007079
		private DatabaseLayer DatabaseManager { get; set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000125 RID: 293 RVA: 0x00008E82 File Offset: 0x00007082
		// (set) Token: 0x06000126 RID: 294 RVA: 0x00008E8A File Offset: 0x0000708A
		public OperationContext OpContext { get; set; }

		// Token: 0x06000127 RID: 295 RVA: 0x00008E94 File Offset: 0x00007094
		static UpdateDAO()
		{
			bool flag = !Directory.Exists(ClockWorkUpdateSystemPathVariables.UPDATES_PATH);
			if (flag)
			{
				Directory.CreateDirectory(ClockWorkUpdateSystemPathVariables.UPDATES_PATH);
			}
			bool flag2 = !Directory.Exists(ClockWorkUpdateSystemPathVariables.UPDATES_PUBLIC_PATH);
			if (flag2)
			{
				Directory.CreateDirectory(ClockWorkUpdateSystemPathVariables.UPDATES_PUBLIC_PATH);
			}
			bool flag3 = !Directory.Exists(ClockWorkUpdateSystemPathVariables.UPDATES_COMPUTER_PATH);
			if (flag3)
			{
				Directory.CreateDirectory(ClockWorkUpdateSystemPathVariables.UPDATES_COMPUTER_PATH);
			}
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00008EF6 File Offset: 0x000070F6
		public UpdateDAO() : this(null)
		{
			this.DatabaseManager = DatabaseLayerFactory.ClockWork;
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00008F10 File Offset: 0x00007110
		public UpdateDAO(OperationContext operationContext)
		{
			this.OpContext = operationContext;
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			this.DatabaseManager = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			InstitutionDAO institutionDAO = new InstitutionDAO();
			string path = institutionDAO.GetInstitutionUniqueName().ApplyAzureStorageContainerNamingConventionRules();
			this.UpdatesPrivatePath = Path.Combine(ClockWorkUpdateSystemPathVariables.UPDATES_PATH, path);
			bool flag = !Directory.Exists(this.UpdatesPrivatePath);
			if (flag)
			{
				Directory.CreateDirectory(this.UpdatesPrivatePath);
			}
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00008F90 File Offset: 0x00007190
		public string GetLegacyPrivateFolderPath()
		{
			InstitutionDAO institutionDAO = new InstitutionDAO();
			string institutionUniqueName = institutionDAO.GetInstitutionUniqueName();
			string text = Path.Combine(ClockWorkUpdateSystemPathVariables.UPDATES_PATH, institutionUniqueName);
			bool flag = !Directory.Exists(text);
			if (flag)
			{
				Directory.CreateDirectory(text);
			}
			return text;
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00008FD4 File Offset: 0x000071D4
		public IList<UpdateFileInfo> GetAvailableUpdates(eUpdateFolderAccess updFolderAccess)
		{
			List<UpdateFileInfo> list = new List<UpdateFileInfo>();
			bool flag = (updFolderAccess & eUpdateFolderAccess.Public) == eUpdateFolderAccess.Public;
			if (flag)
			{
				string[] files = Directory.GetFiles(ClockWorkUpdateSystemPathVariables.UPDATES_PUBLIC_PATH);
				list.AddRange(from f in files.Select(new Func<string, string>(Path.GetFileName))
				select this.GetFileInfo(f, true) into fileInfo
				where fileInfo != null
				select fileInfo);
			}
			bool flag2 = (updFolderAccess & eUpdateFolderAccess.Private) == eUpdateFolderAccess.Private;
			if (flag2)
			{
				string[] files2 = Directory.GetFiles(this.UpdatesPrivatePath);
				list.AddRange(from f in files2.Select(new Func<string, string>(Path.GetFileName))
				select this.GetFileInfo(f, false) into fileInfo
				where fileInfo != null
				select fileInfo);
			}
			return list;
		}

		// Token: 0x0600012C RID: 300 RVA: 0x000090BC File Offset: 0x000072BC
		public void ApplyUpdate(IList<UpdateFileInfo> updates)
		{
			foreach (UpdateFileInfo updateFileInfo in updates)
			{
				this.SaveExecutionStatus(new TechnoPro.Common.Public.Entities.Updates.UpdateStatus
				{
					FileType = updateFileInfo.Filename.GetFileTypeTitle(),
					AddressSize = updateFileInfo.AddressSize,
					IsPublic = updateFileInfo.IsPublic,
					Status = eUpdateStatus.OnSchedule.ToString(),
					Filename = updateFileInfo.Filename
				});
			}
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00009160 File Offset: 0x00007360
		public TechnoPro.Common.Public.Entities.Updates.UpdateStatus GetExecutionStatus(string fileType, int addSize, bool isPublic)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@filetype", DbType.String, fileType),
				this.DatabaseManager.GetParameter("@addsize", DbType.Int32, addSize),
				this.DatabaseManager.GetParameter("@ispublic", DbType.Boolean, isPublic)
			};
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("select * from UpdatingSystem_UpdateStatus where FileType=@filetype and AddSize=@addsize and IsPublic=@ispublic", parameters))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					return this.GetUpdateStatus(dataReader);
				}
			}
			return null;
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00009214 File Offset: 0x00007414
		public void SaveExecutionStatus(TechnoPro.Common.Public.Entities.Updates.UpdateStatus updateStatus)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@FileType", DbType.String, updateStatus.FileType),
				this.DatabaseManager.GetParameter("@addsize", DbType.Int32, updateStatus.AddressSize),
				this.DatabaseManager.GetParameter("@ispublic", DbType.Boolean, updateStatus.IsPublic),
				this.DatabaseManager.GetParameter("@status", DbType.String, updateStatus.Status),
				this.DatabaseManager.GetParameter("@filename", DbType.String, string.IsNullOrEmpty(updateStatus.Filename) ? string.Empty : updateStatus.Filename)
			};
			this.DatabaseManager.ExecuteNonQuery("IF not exists (select 1 FROM UpdatingSystem_UpdateStatus WHERE FileType=@filetype and AddSize=@addsize and IsPublic=@ispublic)\r\nbegin\r\n\tinsert into UpdatingSystem_UpdateStatus (FileType, AddSize, IsPublic, [Status], [Filename]) VALUES (@filetype, @addsize, @ispublic, @status, @filename)\r\nend\r\nelse\r\nbegin\r\n\tUPDATE UpdatingSystem_UpdateStatus SET [Status]=@status, [Filename]=@filename WHERE FileType=@filetype and AddSize=@addsize and IsPublic=@ispublic\r\nend", parameters);
		}

		// Token: 0x0600012F RID: 303 RVA: 0x000092E0 File Offset: 0x000074E0
		public IList<TechnoPro.Common.Public.Entities.Updates.UpdateStatus> GetExecutionStatus()
		{
			List<TechnoPro.Common.Public.Entities.Updates.UpdateStatus> list = new List<TechnoPro.Common.Public.Entities.Updates.UpdateStatus>();
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("select * from UpdatingSystem_UpdateStatus"))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					while (dataReader.Read())
					{
						TechnoPro.Common.Public.Entities.Updates.UpdateStatus updateStatus = this.GetUpdateStatus(dataReader);
						bool flag2 = updateStatus != null;
						if (flag2)
						{
							list.Add(updateStatus);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00009360 File Offset: 0x00007560
		public IList<UpdateFileInfo> GetOnScheduleUpdates()
		{
			return (from u in this.GetAvailableUpdates(eUpdateFolderAccess.All)
			where u.Status == eUpdateStatus.OnSchedule
			select u).ToList<UpdateFileInfo>();
		}

		// Token: 0x06000131 RID: 305 RVA: 0x000093A4 File Offset: 0x000075A4
		public void CancelOnScheduleUpdates(IList<UpdateFileInfo> updates)
		{
			foreach (UpdateFileInfo updateFileInfo in updates)
			{
				this.SaveExecutionStatus(new TechnoPro.Common.Public.Entities.Updates.UpdateStatus
				{
					FileType = updateFileInfo.Filename.GetFileTypeTitle(),
					AddressSize = updateFileInfo.AddressSize,
					IsPublic = updateFileInfo.IsPublic,
					Status = eUpdateStatus.Pending.ToString(),
					Filename = updateFileInfo.Filename
				});
			}
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00009448 File Offset: 0x00007648
		public static string GetUpdateHoldingFolder(string folderAccess)
		{
			string text = folderAccess.ToLower();
			string a = text;
			string result;
			if (!(a == "public"))
			{
				if (!(a == "computer"))
				{
					if (!(a == "recovery"))
					{
						string text2 = Path.Combine(ClockWorkUpdateSystemPathVariables.UPDATES_PATH, folderAccess);
						bool flag = !Directory.Exists(text2);
						if (flag)
						{
							Directory.CreateDirectory(text2);
						}
						result = text2;
					}
					else
					{
						result = ClockWorkUpdateSystemPathVariables.UPDATES_RECOVERY_PATH;
					}
				}
				else
				{
					result = ClockWorkUpdateSystemPathVariables.UPDATES_COMPUTER_PATH;
				}
			}
			else
			{
				result = ClockWorkUpdateSystemPathVariables.UPDATES_PUBLIC_PATH;
			}
			return result;
		}

		// Token: 0x06000133 RID: 307 RVA: 0x000094C8 File Offset: 0x000076C8
		private TechnoPro.Common.Public.Entities.Updates.UpdateStatus GetUpdateStatus(IDataRecord record)
		{
			return new TechnoPro.Common.Public.Entities.Updates.UpdateStatus
			{
				ID = (int)record["ID"],
				FileType = (string)record["FileType"],
				AddressSize = (int)record["AddSize"],
				IsPublic = (bool)record["IsPublic"],
				Status = (string)record["Status"],
				Filename = (string)record["Filename"]
			};
		}

		// Token: 0x06000134 RID: 308 RVA: 0x0000956C File Offset: 0x0000776C
		private UpdateFileInfo GetFileInfo(string filename, bool isPublic)
		{
			string version = filename.GetVersion();
			bool flag = string.IsNullOrEmpty(version);
			UpdateFileInfo result;
			if (flag)
			{
				result = null;
			}
			else
			{
				int addressSize = filename.GetAddressSize();
				string fileTypeTitle = filename.GetFileTypeTitle();
				IUpdateFileType updateFileType = UpdateFileTypeFactory.GetUpdateFileType(fileTypeTitle);
				FileType fileType = new FileType
				{
					AddrSizeVersion = (addressSize > 0),
					Title = fileTypeTitle,
					Extension = ((updateFileType != null) ? updateFileType.Extension : string.Empty),
					Description = ((updateFileType != null) ? updateFileType.UpdateFileType.GetDescription() : string.Empty)
				};
				FileInfo fileInfo = new FileInfo(Path.Combine(isPublic ? ClockWorkUpdateSystemPathVariables.UPDATES_PUBLIC_PATH : this.UpdatesPrivatePath, filename));
				DateTime lastWriteTime = fileInfo.LastWriteTime;
				TechnoPro.Common.Public.Entities.Updates.UpdateStatus executionStatus = this.GetExecutionStatus(fileTypeTitle, addressSize, isPublic);
				eUpdateStatus status = (executionStatus == null || string.IsNullOrEmpty(executionStatus.Status)) ? eUpdateStatus.Pending : ((eUpdateStatus)Enum.Parse(typeof(eUpdateStatus), executionStatus.Status));
				result = new UpdateFileInfo
				{
					Filename = filename,
					AddressSize = addressSize,
					FileType = fileType,
					Version = version,
					Status = status,
					LastModifiedTime = lastWriteTime,
					IsPublic = isPublic
				};
			}
			return result;
		}
	}
}
