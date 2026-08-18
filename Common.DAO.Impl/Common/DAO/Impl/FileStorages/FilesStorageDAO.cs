using System;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Databases;
using TechnoPro.Common.DAO.FileStorage;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.FileStorage;

namespace TechnoPro.Common.DAO.Impl.FileStorages
{
	// Token: 0x020000CB RID: 203
	public class FilesStorageDAO : IFilesStorageDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x0600056F RID: 1391 RVA: 0x000344E5 File Offset: 0x000326E5
		// (set) Token: 0x06000570 RID: 1392 RVA: 0x000344ED File Offset: 0x000326ED
		public OperationContext OpContext { get; set; }

		// Token: 0x06000571 RID: 1393 RVA: 0x000344F6 File Offset: 0x000326F6
		public FilesStorageDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x00034508 File Offset: 0x00032708
		public BasicFileInfo GetFileInfo(FileIdentifier fileId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] array2;
			if (fileId.FileUniqueId == null)
			{
				DbParameter[] array = new DbParameter[2];
				array[0] = databaseLayer.GetParameter("@legacyid", DbType.Int32, fileId.LegacyId);
				array2 = array;
				array[1] = databaseLayer.GetParameter("@source", DbType.String, fileId.Source);
			}
			else
			{
				(array2 = new DbParameter[1])[0] = databaseLayer.GetParameter("@fileid", DbType.Guid, fileId.FileUniqueId.Value);
			}
			DbParameter[] parameters = array2;
			string query = (fileId.FileUniqueId != null) ? "select FileID, LegacyID, [Source], Filename, FileLength, FileUri, WhoUploaded, DateCreated from [FileStorage_Files] where FileID=@fileid" : "select FileID, LegacyID, [Source], Filename, FileLength, FileUri, WhoUploaded, DateCreated from [FileStorage_Files] where LegacyID=@legacyid AND [Source]=@source";
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader(query, parameters))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					return this.GetBasicFileInfoFromReader(dataReader);
				}
			}
			return null;
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x00034618 File Offset: 0x00032818
		[DebuggerStepThrough]
		public Task<BasicFileInfo> GetFileInfoAsync(FileIdentifier fileId)
		{
			FilesStorageDAO.<GetFileInfoAsync>d__6 <GetFileInfoAsync>d__ = new FilesStorageDAO.<GetFileInfoAsync>d__6();
			<GetFileInfoAsync>d__.<>t__builder = AsyncTaskMethodBuilder<BasicFileInfo>.Create();
			<GetFileInfoAsync>d__.<>4__this = this;
			<GetFileInfoAsync>d__.fileId = fileId;
			<GetFileInfoAsync>d__.<>1__state = -1;
			<GetFileInfoAsync>d__.<>t__builder.Start<FilesStorageDAO.<GetFileInfoAsync>d__6>(ref <GetFileInfoAsync>d__);
			return <GetFileInfoAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x00034664 File Offset: 0x00032864
		public FileIdentifier AddFileInfo(BasicFileInfo fileInfo)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			bool flag = fileInfo.FileIdentifier == null;
			if (flag)
			{
				fileInfo.FileIdentifier = new FileIdentifier();
			}
			bool flag2 = fileInfo.FileIdentifier != null && fileInfo.FileIdentifier.FileUniqueId == null;
			if (flag2)
			{
				fileInfo.FileIdentifier.FileUniqueId = new Guid?(Guid.NewGuid());
			}
			DbParameter[] array = new DbParameter[7];
			array[0] = databaseLayer.GetParameter("@fileid", DbType.Guid, fileInfo.FileIdentifier.FileUniqueId);
			array[1] = databaseLayer.GetParameter("@filename", DbType.String, fileInfo.FileName);
			array[2] = databaseLayer.GetParameter("@filelen", DbType.Int64, fileInfo.Length);
			array[3] = databaseLayer.GetParameter("@whouploaded", DbType.Int32, this.OpContext.WhoAmI);
			int num = 4;
			DatabaseLayer databaseLayer2 = databaseLayer;
			string pName = "@legacyid";
			DbType pType = DbType.Int32;
			FileIdentifier fileIdentifier = fileInfo.FileIdentifier;
			array[num] = databaseLayer2.GetParameter(pName, pType, (fileIdentifier != null && fileIdentifier.FileUniqueId != null) ? fileInfo.FileIdentifier.LegacyId : 0);
			int num2 = 5;
			DatabaseLayer databaseLayer3 = databaseLayer;
			string pName2 = "@source";
			DbType pType2 = DbType.String;
			FileIdentifier fileIdentifier2 = fileInfo.FileIdentifier;
			array[num2] = databaseLayer3.GetParameter(pName2, pType2, (fileIdentifier2 != null) ? fileIdentifier2.Source.ToString() : null);
			int num3 = 6;
			DatabaseLayer databaseLayer4 = databaseLayer;
			string pName3 = "@fileuri";
			DbType pType3 = DbType.String;
			Uri fileUri = fileInfo.FileUri;
			array[num3] = databaseLayer4.GetParameter(pName3, pType3, ((fileUri != null) ? fileUri.ToString() : null) ?? string.Empty);
			DbParameter[] parameters = array;
			databaseLayer.ExecuteNonQuery("insert into FileStorage_Files (FileID, Filename, FileLength, WhoUploaded, LegacyID, [Source], FileUri) values ( @fileid, @filename, @filelen, @whouploaded, @legacyid, @source, @fileuri)", parameters);
			return fileInfo.FileIdentifier;
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x00034810 File Offset: 0x00032A10
		[DebuggerStepThrough]
		public Task<FileIdentifier> AddFileInfoAsync(BasicFileInfo fileInfo)
		{
			FilesStorageDAO.<AddFileInfoAsync>d__8 <AddFileInfoAsync>d__ = new FilesStorageDAO.<AddFileInfoAsync>d__8();
			<AddFileInfoAsync>d__.<>t__builder = AsyncTaskMethodBuilder<FileIdentifier>.Create();
			<AddFileInfoAsync>d__.<>4__this = this;
			<AddFileInfoAsync>d__.fileInfo = fileInfo;
			<AddFileInfoAsync>d__.<>1__state = -1;
			<AddFileInfoAsync>d__.<>t__builder.Start<FilesStorageDAO.<AddFileInfoAsync>d__8>(ref <AddFileInfoAsync>d__);
			return <AddFileInfoAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x0003485C File Offset: 0x00032A5C
		public BasicFileInfo GetTempFileInfo(FileIdentifier fileId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] array2;
			if (fileId.FileUniqueId == null)
			{
				DbParameter[] array = new DbParameter[2];
				array[0] = databaseLayer.GetParameter("@legacyid", DbType.Int32, fileId.LegacyId);
				array2 = array;
				array[1] = databaseLayer.GetParameter("@source", DbType.String, fileId.Source);
			}
			else
			{
				(array2 = new DbParameter[1])[0] = databaseLayer.GetParameter("@fileid", DbType.Guid, fileId.FileUniqueId.Value);
			}
			DbParameter[] parameters = array2;
			string query = (fileId.FileUniqueId != null) ? "select FileID, LegacyID, [Source], Filename, FileLength, FileUri, WhoUploaded, DateCreated from [FileStorage_TempFiles] where FileID=@fileid" : "select FileID, LegacyID, [Source], Filename, FileLength, FileUri, WhoUploaded, DateCreated from [FileStorage_TempFiles] where LegacyID=@legacyid AND [Source]=@source";
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader(query, parameters))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					return this.GetBasicFileInfoFromReader(dataReader);
				}
			}
			return null;
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x0003496C File Offset: 0x00032B6C
		[DebuggerStepThrough]
		public Task<BasicFileInfo> GetTempFileInfoAsync(FileIdentifier fileId)
		{
			FilesStorageDAO.<GetTempFileInfoAsync>d__10 <GetTempFileInfoAsync>d__ = new FilesStorageDAO.<GetTempFileInfoAsync>d__10();
			<GetTempFileInfoAsync>d__.<>t__builder = AsyncTaskMethodBuilder<BasicFileInfo>.Create();
			<GetTempFileInfoAsync>d__.<>4__this = this;
			<GetTempFileInfoAsync>d__.fileId = fileId;
			<GetTempFileInfoAsync>d__.<>1__state = -1;
			<GetTempFileInfoAsync>d__.<>t__builder.Start<FilesStorageDAO.<GetTempFileInfoAsync>d__10>(ref <GetTempFileInfoAsync>d__);
			return <GetTempFileInfoAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x000349B8 File Offset: 0x00032BB8
		public FileIdentifier AddTempFileInfo(BasicFileInfo fileInfo)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			bool flag = fileInfo.FileIdentifier == null;
			if (flag)
			{
				fileInfo.FileIdentifier = new FileIdentifier();
			}
			bool flag2 = fileInfo.FileIdentifier != null && fileInfo.FileIdentifier.FileUniqueId == null;
			if (flag2)
			{
				fileInfo.FileIdentifier.FileUniqueId = new Guid?(Guid.NewGuid());
			}
			DbParameter[] array = new DbParameter[7];
			array[0] = databaseLayer.GetParameter("@fileid", DbType.Guid, fileInfo.FileIdentifier.FileUniqueId);
			array[1] = databaseLayer.GetParameter("@filename", DbType.String, fileInfo.FileName);
			array[2] = databaseLayer.GetParameter("@filelen", DbType.Int64, fileInfo.Length);
			array[3] = databaseLayer.GetParameter("@whouploaded", DbType.Int32, this.OpContext.WhoAmI);
			int num = 4;
			DatabaseLayer databaseLayer2 = databaseLayer;
			string pName = "@legacyid";
			DbType pType = DbType.Int32;
			FileIdentifier fileIdentifier = fileInfo.FileIdentifier;
			array[num] = databaseLayer2.GetParameter(pName, pType, (fileIdentifier != null && fileIdentifier.FileUniqueId != null) ? fileInfo.FileIdentifier.LegacyId : 0);
			int num2 = 5;
			DatabaseLayer databaseLayer3 = databaseLayer;
			string pName2 = "@source";
			DbType pType2 = DbType.String;
			FileIdentifier fileIdentifier2 = fileInfo.FileIdentifier;
			array[num2] = databaseLayer3.GetParameter(pName2, pType2, (fileIdentifier2 != null) ? fileIdentifier2.Source.ToString() : null);
			int num3 = 6;
			DatabaseLayer databaseLayer4 = databaseLayer;
			string pName3 = "@fileuri";
			DbType pType3 = DbType.String;
			Uri fileUri = fileInfo.FileUri;
			array[num3] = databaseLayer4.GetParameter(pName3, pType3, ((fileUri != null) ? fileUri.ToString() : null) ?? string.Empty);
			DbParameter[] parameters = array;
			databaseLayer.ExecuteNonQuery("insert into FileStorage_TempFiles (FileID, Filename, FileLength, WhoUploaded, LegacyID, [Source], FileUri) values ( @fileid, @filename, @filelen, @whouploaded, @legacyid, @source, @fileuri)", parameters);
			return fileInfo.FileIdentifier;
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x00034B64 File Offset: 0x00032D64
		[DebuggerStepThrough]
		public Task<FileIdentifier> AddTempFileInfoAsync(BasicFileInfo fileInfo)
		{
			FilesStorageDAO.<AddTempFileInfoAsync>d__12 <AddTempFileInfoAsync>d__ = new FilesStorageDAO.<AddTempFileInfoAsync>d__12();
			<AddTempFileInfoAsync>d__.<>t__builder = AsyncTaskMethodBuilder<FileIdentifier>.Create();
			<AddTempFileInfoAsync>d__.<>4__this = this;
			<AddTempFileInfoAsync>d__.fileInfo = fileInfo;
			<AddTempFileInfoAsync>d__.<>1__state = -1;
			<AddTempFileInfoAsync>d__.<>t__builder.Start<FilesStorageDAO.<AddTempFileInfoAsync>d__12>(ref <AddTempFileInfoAsync>d__);
			return <AddTempFileInfoAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x00034BB0 File Offset: 0x00032DB0
		public void DeleteFileInfo(FileIdentifier fileId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@fileid", DbType.Guid, fileId.FileUniqueId.Value)
			};
			databaseLayer.ExecuteNonQuery("delete from FileStorage_Files where FileID = @fileid", parameters);
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x00034C10 File Offset: 0x00032E10
		public void DeleteTempFileInfo(FileIdentifier fileId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@fileid", DbType.Guid, fileId.FileUniqueId.Value)
			};
			databaseLayer.ExecuteNonQuery("delete from FileStorage_TempFiles where FileID = @fileid", parameters);
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x00034C70 File Offset: 0x00032E70
		[DebuggerStepThrough]
		public Task DeleteFileInfoAsync(FileIdentifier fileId)
		{
			FilesStorageDAO.<DeleteFileInfoAsync>d__15 <DeleteFileInfoAsync>d__ = new FilesStorageDAO.<DeleteFileInfoAsync>d__15();
			<DeleteFileInfoAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<DeleteFileInfoAsync>d__.<>4__this = this;
			<DeleteFileInfoAsync>d__.fileId = fileId;
			<DeleteFileInfoAsync>d__.<>1__state = -1;
			<DeleteFileInfoAsync>d__.<>t__builder.Start<FilesStorageDAO.<DeleteFileInfoAsync>d__15>(ref <DeleteFileInfoAsync>d__);
			return <DeleteFileInfoAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x00034CBC File Offset: 0x00032EBC
		[DebuggerStepThrough]
		public Task DeleteTempFileInfoAsync(FileIdentifier fileId)
		{
			FilesStorageDAO.<DeleteTempFileInfoAsync>d__16 <DeleteTempFileInfoAsync>d__ = new FilesStorageDAO.<DeleteTempFileInfoAsync>d__16();
			<DeleteTempFileInfoAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<DeleteTempFileInfoAsync>d__.<>4__this = this;
			<DeleteTempFileInfoAsync>d__.fileId = fileId;
			<DeleteTempFileInfoAsync>d__.<>1__state = -1;
			<DeleteTempFileInfoAsync>d__.<>t__builder.Start<FilesStorageDAO.<DeleteTempFileInfoAsync>d__16>(ref <DeleteTempFileInfoAsync>d__);
			return <DeleteTempFileInfoAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x00034D08 File Offset: 0x00032F08
		private BasicFileInfo GetBasicFileInfoFromReader(IDataReader reader)
		{
			FileIdentifier fileIdentifier = new FileIdentifier
			{
				FileUniqueId = new Guid?((Guid)reader["FileID"]),
				LegacyId = (int)reader["LegacyID"],
				Source = (Enum.IsDefined(typeof(eFileSource), reader["Source"]) ? ((eFileSource)Enum.Parse(typeof(eFileSource), (string)reader["Source"])) : eFileSource.Unknown)
			};
			string text = (string)reader["FileUri"];
			return new BasicFileInfo
			{
				FileIdentifier = fileIdentifier,
				FileName = (string)reader["Filename"],
				Length = (long)reader["FileLength"],
				FileUri = (string.IsNullOrEmpty(text) ? null : new Uri(text))
			};
		}
	}
}
