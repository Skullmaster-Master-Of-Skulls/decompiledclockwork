using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Databases;
using TechnoPro.Common.DAO.FileStorage;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.FileStorage;

namespace TechnoPro.Common.DAO.Impl.FileStorages
{
	// Token: 0x020000CE RID: 206
	public class DbFilesStorageDAO : IDbFilesStorageDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000584 RID: 1412 RVA: 0x00034F72 File Offset: 0x00033172
		public DbFilesStorageDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x00034F84 File Offset: 0x00033184
		public StreamingFile DownloadLargeFile(FileIdentifier fileId)
		{
			IFilesStorageDAO filesStorageDAO = new FilesStorageDAO(this.OpContext);
			BasicFileInfo fileInfo = filesStorageDAO.GetFileInfo(fileId);
			bool flag = fileInfo == null;
			StreamingFile result;
			if (flag)
			{
				result = null;
			}
			else
			{
				DatabaseLayer clockWorkFiles = DatabaseLayerFactory.ClockWorkFiles;
				DbParameter[] parameters = new DbParameter[]
				{
					clockWorkFiles.GetParameter("@fileid", DbType.Guid, fileInfo.FileIdentifier.FileUniqueId.Value)
				};
				IDataReader dataReader = clockWorkFiles.ExecuteQueryReader("select FileData from FileStorage_FilesData where FileID=@fileid", new CommandOverrideSettings((int)TimeSpan.FromMinutes(30.0).TotalSeconds, CommandBehavior.SequentialAccess), parameters);
				bool flag2 = dataReader != null && dataReader.Read();
				if (flag2)
				{
					result = this.GetLargeFileInfoFromReader(fileInfo, (DbDataReader)dataReader);
				}
				else
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x00035048 File Offset: 0x00033248
		[DebuggerStepThrough]
		public Task<StreamingFile> DownloadLargeFileAsync(FileIdentifier fileId)
		{
			DbFilesStorageDAO.<DownloadLargeFileAsync>d__2 <DownloadLargeFileAsync>d__ = new DbFilesStorageDAO.<DownloadLargeFileAsync>d__2();
			<DownloadLargeFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder<StreamingFile>.Create();
			<DownloadLargeFileAsync>d__.<>4__this = this;
			<DownloadLargeFileAsync>d__.fileId = fileId;
			<DownloadLargeFileAsync>d__.<>1__state = -1;
			<DownloadLargeFileAsync>d__.<>t__builder.Start<DbFilesStorageDAO.<DownloadLargeFileAsync>d__2>(ref <DownloadLargeFileAsync>d__);
			return <DownloadLargeFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x00035094 File Offset: 0x00033294
		public BasicFileInfo UploadLargeFile(StreamingFile file)
		{
			Stream fileByteStream = file.FileByteStream;
			IFilesStorageDAO filesStorageDAO = new FilesStorageDAO(this.OpContext);
			file.FileIdentifier = filesStorageDAO.AddFileInfo(file);
			DatabaseLayer clockWorkFiles = DatabaseLayerFactory.ClockWorkFiles;
			DbParameter[] parameters = new DbParameter[]
			{
				clockWorkFiles.GetParameter("@fileid", DbType.Guid, file.FileIdentifier.FileUniqueId),
				clockWorkFiles.GetParameter("@filedata", DbType.Binary, fileByteStream, -1),
				clockWorkFiles.GetParameter("@filelegacyid", DbType.Int32, file.FileIdentifier.LegacyId),
				clockWorkFiles.GetParameter("@filesource", DbType.String, file.FileIdentifier.Source.ToString())
			};
			clockWorkFiles.ExecuteNonQuery("if not exists (select 1 from FileStorage_FilesData where FileID=@fileid)\r\n\tbegin\r\n\t\tinsert into FileStorage_FilesData (FileID, FileData, LegacyID, Source) values ( @fileid, @filedata, @filelegacyid, @filesource)\r\n\tend\r\nelse\r\n\tbegin\r\n\t\tupdate FileStorage_FilesData set FileData=@filedata, LegacyID=@filelegacyid, Source=@filesource where FileID=@fileid\r\n\tend", new CommandOverrideSettings((int)TimeSpan.FromMinutes(30.0).TotalSeconds), parameters);
			return file;
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x0003517C File Offset: 0x0003337C
		[DebuggerStepThrough]
		public Task<BasicFileInfo> UploadLargeFileAsync(StreamingFile file)
		{
			DbFilesStorageDAO.<UploadLargeFileAsync>d__4 <UploadLargeFileAsync>d__ = new DbFilesStorageDAO.<UploadLargeFileAsync>d__4();
			<UploadLargeFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder<BasicFileInfo>.Create();
			<UploadLargeFileAsync>d__.<>4__this = this;
			<UploadLargeFileAsync>d__.file = file;
			<UploadLargeFileAsync>d__.<>1__state = -1;
			<UploadLargeFileAsync>d__.<>t__builder.Start<DbFilesStorageDAO.<UploadLargeFileAsync>d__4>(ref <UploadLargeFileAsync>d__);
			return <UploadLargeFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x000351C8 File Offset: 0x000333C8
		public StreamingFile DownloadLargeTempFile(FileIdentifier fileId)
		{
			IFilesStorageDAO filesStorageDAO = new FilesStorageDAO(this.OpContext);
			BasicFileInfo tempFileInfo = filesStorageDAO.GetTempFileInfo(fileId);
			bool flag = tempFileInfo == null;
			StreamingFile result;
			if (flag)
			{
				result = null;
			}
			else
			{
				DatabaseLayer clockWorkFiles = DatabaseLayerFactory.ClockWorkFiles;
				DbParameter[] parameters = new DbParameter[]
				{
					clockWorkFiles.GetParameter("@fileid", DbType.Guid, tempFileInfo.FileIdentifier.FileUniqueId.Value)
				};
				IDataReader dataReader = clockWorkFiles.ExecuteQueryReader("select FileData from FileStorage_TempFilesData where FileID=@fileid", new CommandOverrideSettings((int)TimeSpan.FromMinutes(30.0).TotalSeconds, CommandBehavior.SequentialAccess), parameters);
				bool flag2 = dataReader != null && dataReader.Read();
				if (flag2)
				{
					result = this.GetLargeFileInfoFromReader(tempFileInfo, (DbDataReader)dataReader);
				}
				else
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x0003528C File Offset: 0x0003348C
		[DebuggerStepThrough]
		public Task<StreamingFile> DownloadLargeTempFileAsync(FileIdentifier fileId)
		{
			DbFilesStorageDAO.<DownloadLargeTempFileAsync>d__6 <DownloadLargeTempFileAsync>d__ = new DbFilesStorageDAO.<DownloadLargeTempFileAsync>d__6();
			<DownloadLargeTempFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder<StreamingFile>.Create();
			<DownloadLargeTempFileAsync>d__.<>4__this = this;
			<DownloadLargeTempFileAsync>d__.fileId = fileId;
			<DownloadLargeTempFileAsync>d__.<>1__state = -1;
			<DownloadLargeTempFileAsync>d__.<>t__builder.Start<DbFilesStorageDAO.<DownloadLargeTempFileAsync>d__6>(ref <DownloadLargeTempFileAsync>d__);
			return <DownloadLargeTempFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x000352D8 File Offset: 0x000334D8
		public BasicFileInfo UploadLargeTempFile(StreamingFile file)
		{
			Stream fileByteStream = file.FileByteStream;
			IFilesStorageDAO filesStorageDAO = new FilesStorageDAO(this.OpContext);
			file.FileIdentifier = filesStorageDAO.AddTempFileInfo(file);
			DatabaseLayer clockWorkFiles = DatabaseLayerFactory.ClockWorkFiles;
			DbParameter[] parameters = new DbParameter[]
			{
				clockWorkFiles.GetParameter("@fileid", DbType.Guid, file.FileIdentifier.FileUniqueId),
				clockWorkFiles.GetParameter("@filedata", DbType.Binary, fileByteStream, -1)
			};
			clockWorkFiles.ExecuteNonQuery("insert into FileStorage_TempFilesData (FileID, FileData) values ( @fileid, @filedata)", new CommandOverrideSettings((int)TimeSpan.FromMinutes(30.0).TotalSeconds), parameters);
			return file;
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x00035378 File Offset: 0x00033578
		[DebuggerStepThrough]
		public Task<BasicFileInfo> UploadLargeTempFileAsync(StreamingFile file)
		{
			DbFilesStorageDAO.<UploadLargeTempFileAsync>d__8 <UploadLargeTempFileAsync>d__ = new DbFilesStorageDAO.<UploadLargeTempFileAsync>d__8();
			<UploadLargeTempFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder<BasicFileInfo>.Create();
			<UploadLargeTempFileAsync>d__.<>4__this = this;
			<UploadLargeTempFileAsync>d__.file = file;
			<UploadLargeTempFileAsync>d__.<>1__state = -1;
			<UploadLargeTempFileAsync>d__.<>t__builder.Start<DbFilesStorageDAO.<UploadLargeTempFileAsync>d__8>(ref <UploadLargeTempFileAsync>d__);
			return <UploadLargeTempFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x000353C4 File Offset: 0x000335C4
		public InMemoryFile DownloadFile(FileIdentifier fileId)
		{
			IFilesStorageDAO filesStorageDAO = new FilesStorageDAO(this.OpContext);
			BasicFileInfo fileInfo = filesStorageDAO.GetFileInfo(fileId);
			bool flag = fileInfo == null;
			InMemoryFile result;
			if (flag)
			{
				result = null;
			}
			else
			{
				DatabaseLayer clockWorkFiles = DatabaseLayerFactory.ClockWorkFiles;
				DbParameter[] parameters = new DbParameter[]
				{
					clockWorkFiles.GetParameter("@fileid", DbType.Guid, fileInfo.FileIdentifier.FileUniqueId.Value)
				};
				IDataReader dataReader = clockWorkFiles.ExecuteQueryReader("select FileData from FileStorage_FilesData where FileID=@fileid", new CommandOverrideSettings((int)TimeSpan.FromMinutes(10.0).TotalSeconds), parameters);
				bool flag2 = dataReader != null && dataReader.Read();
				if (flag2)
				{
					result = this.GetInMemoryFileInfoFromReader(fileInfo, (DbDataReader)dataReader);
				}
				else
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x00035488 File Offset: 0x00033688
		[DebuggerStepThrough]
		public Task<InMemoryFile> DownloadFileAsync(FileIdentifier fileId)
		{
			DbFilesStorageDAO.<DownloadFileAsync>d__10 <DownloadFileAsync>d__ = new DbFilesStorageDAO.<DownloadFileAsync>d__10();
			<DownloadFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder<InMemoryFile>.Create();
			<DownloadFileAsync>d__.<>4__this = this;
			<DownloadFileAsync>d__.fileId = fileId;
			<DownloadFileAsync>d__.<>1__state = -1;
			<DownloadFileAsync>d__.<>t__builder.Start<DbFilesStorageDAO.<DownloadFileAsync>d__10>(ref <DownloadFileAsync>d__);
			return <DownloadFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x000354D4 File Offset: 0x000336D4
		public BasicFileInfo UploadFile(InMemoryFile file)
		{
			IFilesStorageDAO filesStorageDAO = new FilesStorageDAO(this.OpContext);
			file.FileIdentifier = filesStorageDAO.AddFileInfo(file);
			DatabaseLayer clockWorkFiles = DatabaseLayerFactory.ClockWorkFiles;
			DbParameter[] parameters = new DbParameter[]
			{
				clockWorkFiles.GetParameter("@fileid", DbType.Guid, file.FileIdentifier.FileUniqueId),
				clockWorkFiles.GetParameter("@filedata", DbType.Binary, file.FileData),
				clockWorkFiles.GetParameter("@filelegacyid", DbType.Int32, file.FileIdentifier.LegacyId),
				clockWorkFiles.GetParameter("@filesource", DbType.String, file.FileIdentifier.Source.ToString())
			};
			clockWorkFiles.ExecuteNonQuery("if not exists (select 1 from FileStorage_FilesData where FileID=@fileid)\r\n\tbegin\r\n\t\tinsert into FileStorage_FilesData (FileID, FileData, LegacyID, Source) values ( @fileid, @filedata, @filelegacyid, @filesource)\r\n\tend\r\nelse\r\n\tbegin\r\n\t\tupdate FileStorage_FilesData set FileData=@filedata, LegacyID=@filelegacyid, Source=@filesource where FileID=@fileid\r\n\tend", new CommandOverrideSettings((int)TimeSpan.FromMinutes(10.0).TotalSeconds), parameters);
			return file;
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x000355B8 File Offset: 0x000337B8
		[DebuggerStepThrough]
		public Task<BasicFileInfo> UploadFileAsync(InMemoryFile file)
		{
			DbFilesStorageDAO.<UploadFileAsync>d__12 <UploadFileAsync>d__ = new DbFilesStorageDAO.<UploadFileAsync>d__12();
			<UploadFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder<BasicFileInfo>.Create();
			<UploadFileAsync>d__.<>4__this = this;
			<UploadFileAsync>d__.file = file;
			<UploadFileAsync>d__.<>1__state = -1;
			<UploadFileAsync>d__.<>t__builder.Start<DbFilesStorageDAO.<UploadFileAsync>d__12>(ref <UploadFileAsync>d__);
			return <UploadFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x00035604 File Offset: 0x00033804
		public InMemoryFile DownloadTempFile(FileIdentifier fileId)
		{
			IFilesStorageDAO filesStorageDAO = new FilesStorageDAO(this.OpContext);
			BasicFileInfo tempFileInfo = filesStorageDAO.GetTempFileInfo(fileId);
			bool flag = tempFileInfo == null;
			InMemoryFile result;
			if (flag)
			{
				result = null;
			}
			else
			{
				DatabaseLayer clockWorkFiles = DatabaseLayerFactory.ClockWorkFiles;
				DbParameter[] parameters = new DbParameter[]
				{
					clockWorkFiles.GetParameter("@fileid", DbType.Guid, tempFileInfo.FileIdentifier.FileUniqueId.Value)
				};
				IDataReader dataReader = clockWorkFiles.ExecuteQueryReader("select FileData from FileStorage_TempFilesData where FileID=@fileid", new CommandOverrideSettings((int)TimeSpan.FromMinutes(10.0).TotalSeconds), parameters);
				bool flag2 = dataReader != null && dataReader.Read();
				if (flag2)
				{
					result = this.GetInMemoryFileInfoFromReader(tempFileInfo, (DbDataReader)dataReader);
				}
				else
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x000356C8 File Offset: 0x000338C8
		[DebuggerStepThrough]
		public Task<InMemoryFile> DownloadTempFileAsync(FileIdentifier fileId)
		{
			DbFilesStorageDAO.<DownloadTempFileAsync>d__14 <DownloadTempFileAsync>d__ = new DbFilesStorageDAO.<DownloadTempFileAsync>d__14();
			<DownloadTempFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder<InMemoryFile>.Create();
			<DownloadTempFileAsync>d__.<>4__this = this;
			<DownloadTempFileAsync>d__.fileId = fileId;
			<DownloadTempFileAsync>d__.<>1__state = -1;
			<DownloadTempFileAsync>d__.<>t__builder.Start<DbFilesStorageDAO.<DownloadTempFileAsync>d__14>(ref <DownloadTempFileAsync>d__);
			return <DownloadTempFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x00035714 File Offset: 0x00033914
		public BasicFileInfo UploadTempFile(InMemoryFile file)
		{
			IFilesStorageDAO filesStorageDAO = new FilesStorageDAO(this.OpContext);
			file.FileIdentifier = filesStorageDAO.AddTempFileInfo(file);
			DatabaseLayer clockWorkFiles = DatabaseLayerFactory.ClockWorkFiles;
			DbParameter[] parameters = new DbParameter[]
			{
				clockWorkFiles.GetParameter("@fileid", DbType.Guid, file.FileIdentifier.FileUniqueId),
				clockWorkFiles.GetParameter("@filedata", DbType.Binary, file.FileData)
			};
			clockWorkFiles.ExecuteNonQuery("insert into FileStorage_TempFilesData (FileID, FileData) values ( @fileid, @filedata)", new CommandOverrideSettings((int)TimeSpan.FromMinutes(10.0).TotalSeconds), parameters);
			return file;
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x000357B0 File Offset: 0x000339B0
		[DebuggerStepThrough]
		public Task<BasicFileInfo> UploadTempFileAsync(InMemoryFile file)
		{
			DbFilesStorageDAO.<UploadTempFileAsync>d__16 <UploadTempFileAsync>d__ = new DbFilesStorageDAO.<UploadTempFileAsync>d__16();
			<UploadTempFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder<BasicFileInfo>.Create();
			<UploadTempFileAsync>d__.<>4__this = this;
			<UploadTempFileAsync>d__.file = file;
			<UploadTempFileAsync>d__.<>1__state = -1;
			<UploadTempFileAsync>d__.<>t__builder.Start<DbFilesStorageDAO.<UploadTempFileAsync>d__16>(ref <UploadTempFileAsync>d__);
			return <UploadTempFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x000357FC File Offset: 0x000339FC
		public void DeleteFile(FileIdentifier fileId)
		{
			DatabaseLayer clockWorkFiles = DatabaseLayerFactory.ClockWorkFiles;
			DbParameter[] parameters = new DbParameter[]
			{
				clockWorkFiles.GetParameter("@fileid", DbType.Guid, fileId.FileUniqueId.Value)
			};
			int num = clockWorkFiles.ExecuteNonQuery("delete from FileStorage_FilesData where FileID=@fileid", parameters);
			bool flag = num > 0;
			if (flag)
			{
				IFilesStorageDAO filesStorageDAO = new FilesStorageDAO(this.OpContext);
				filesStorageDAO.DeleteFileInfo(fileId);
			}
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x0003586C File Offset: 0x00033A6C
		[DebuggerStepThrough]
		public Task DeleteFileAsync(FileIdentifier fileId)
		{
			DbFilesStorageDAO.<DeleteFileAsync>d__18 <DeleteFileAsync>d__ = new DbFilesStorageDAO.<DeleteFileAsync>d__18();
			<DeleteFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<DeleteFileAsync>d__.<>4__this = this;
			<DeleteFileAsync>d__.fileId = fileId;
			<DeleteFileAsync>d__.<>1__state = -1;
			<DeleteFileAsync>d__.<>t__builder.Start<DbFilesStorageDAO.<DeleteFileAsync>d__18>(ref <DeleteFileAsync>d__);
			return <DeleteFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x000358B8 File Offset: 0x00033AB8
		public void DeleteTempFile(FileIdentifier fileId)
		{
			DatabaseLayer clockWorkFiles = DatabaseLayerFactory.ClockWorkFiles;
			DbParameter[] parameters = new DbParameter[]
			{
				clockWorkFiles.GetParameter("@fileid", DbType.Guid, fileId.FileUniqueId.Value)
			};
			int num = clockWorkFiles.ExecuteNonQuery("delete from FileStorage_TempFilesData where FileID=@fileid", parameters);
			bool flag = num > 0;
			if (flag)
			{
				IFilesStorageDAO filesStorageDAO = new FilesStorageDAO(this.OpContext);
				filesStorageDAO.DeleteTempFileInfo(fileId);
			}
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x00035928 File Offset: 0x00033B28
		[DebuggerStepThrough]
		public Task DeleteTempFileAsync(FileIdentifier fileId)
		{
			DbFilesStorageDAO.<DeleteTempFileAsync>d__20 <DeleteTempFileAsync>d__ = new DbFilesStorageDAO.<DeleteTempFileAsync>d__20();
			<DeleteTempFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<DeleteTempFileAsync>d__.<>4__this = this;
			<DeleteTempFileAsync>d__.fileId = fileId;
			<DeleteTempFileAsync>d__.<>1__state = -1;
			<DeleteTempFileAsync>d__.<>t__builder.Start<DbFilesStorageDAO.<DeleteTempFileAsync>d__20>(ref <DeleteTempFileAsync>d__);
			return <DeleteTempFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x00035974 File Offset: 0x00033B74
		public void DeleteTempFilesOlderThan(DateTimeOffset date)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@olderthan", DbType.DateTime, date.DateTime)
			};
			List<string> list = new List<string>();
			using (IDataReader dataReader = databaseLayer.ExecuteStoredProcedureReader("sp_FileStorage_DeleteTempFilesOlderThan", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					while (dataReader.Read())
					{
						string text = (string)dataReader["FileID"];
						bool flag2 = !string.IsNullOrEmpty(text);
						if (flag2)
						{
							list.Add(text);
						}
					}
				}
			}
			DatabaseLayer clockWorkFiles = DatabaseLayerFactory.ClockWorkFiles;
			DbParameter[] parameters2 = new DbParameter[]
			{
				clockWorkFiles.GetParameter("@fileidlist", DbType.String, list.CommaSeparatedValuesWithoutSpace<string>())
			};
			clockWorkFiles.ExecuteStoredProcedure("sp_FileStorage_DeleteTempFilesData", parameters2);
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x00035A6C File Offset: 0x00033C6C
		[DebuggerStepThrough]
		public Task DeleteTempFilesOlderThanAsync(DateTimeOffset date)
		{
			DbFilesStorageDAO.<DeleteTempFilesOlderThanAsync>d__22 <DeleteTempFilesOlderThanAsync>d__ = new DbFilesStorageDAO.<DeleteTempFilesOlderThanAsync>d__22();
			<DeleteTempFilesOlderThanAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<DeleteTempFilesOlderThanAsync>d__.<>4__this = this;
			<DeleteTempFilesOlderThanAsync>d__.date = date;
			<DeleteTempFilesOlderThanAsync>d__.<>1__state = -1;
			<DeleteTempFilesOlderThanAsync>d__.<>t__builder.Start<DbFilesStorageDAO.<DeleteTempFilesOlderThanAsync>d__22>(ref <DeleteTempFilesOlderThanAsync>d__);
			return <DeleteTempFilesOlderThanAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x00035AB8 File Offset: 0x00033CB8
		public void MoveTempFileToPersistentStorage(FileIdentifier fileId)
		{
			DatabaseLayer clockWorkFiles = DatabaseLayerFactory.ClockWorkFiles;
			DbParameter[] parameters = new DbParameter[]
			{
				clockWorkFiles.GetParameter("@fileid", DbType.Guid, fileId.FileUniqueId.Value)
			};
			int num = clockWorkFiles.ExecuteStoredProcedure("sp_FileStorage_MoveTempFileDataToPersistentStorage", parameters);
			bool flag = num > 0;
			if (flag)
			{
				eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
				OperationContext opContext = this.OpContext;
				DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
				DbParameter[] parameters2 = new DbParameter[]
				{
					databaseLayer.GetParameter("@fileid", DbType.Guid, fileId.FileUniqueId.Value)
				};
				databaseLayer.ExecuteStoredProcedure("sp_FileStorage_MoveTempFileToPersistentStorage", parameters2);
			}
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x00035B64 File Offset: 0x00033D64
		[DebuggerStepThrough]
		public Task MoveTempFileToPersistentStorageAsync(FileIdentifier fileId)
		{
			DbFilesStorageDAO.<MoveTempFileToPersistentStorageAsync>d__24 <MoveTempFileToPersistentStorageAsync>d__ = new DbFilesStorageDAO.<MoveTempFileToPersistentStorageAsync>d__24();
			<MoveTempFileToPersistentStorageAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<MoveTempFileToPersistentStorageAsync>d__.<>4__this = this;
			<MoveTempFileToPersistentStorageAsync>d__.fileId = fileId;
			<MoveTempFileToPersistentStorageAsync>d__.<>1__state = -1;
			<MoveTempFileToPersistentStorageAsync>d__.<>t__builder.Start<DbFilesStorageDAO.<MoveTempFileToPersistentStorageAsync>d__24>(ref <MoveTempFileToPersistentStorageAsync>d__);
			return <MoveTempFileToPersistentStorageAsync>d__.<>t__builder.Task;
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600059D RID: 1437 RVA: 0x00035BAF File Offset: 0x00033DAF
		// (set) Token: 0x0600059E RID: 1438 RVA: 0x00035BB7 File Offset: 0x00033DB7
		public OperationContext OpContext { get; set; }

		// Token: 0x0600059F RID: 1439 RVA: 0x00035BC0 File Offset: 0x00033DC0
		private InMemoryFile GetInMemoryFileInfoFromReader(BasicFileInfo fileInfo, IDataReader reader)
		{
			return new InMemoryFile(fileInfo)
			{
				FileData = (byte[])reader["FileData"]
			};
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x00035BF0 File Offset: 0x00033DF0
		private StreamingFile GetLargeFileInfoFromReader(BasicFileInfo fileInfo, DbDataReader reader)
		{
			Stream stream = reader.GetStream(0);
			return new StreamingFile(fileInfo)
			{
				FileByteStream = stream
			};
		}
	}
}
