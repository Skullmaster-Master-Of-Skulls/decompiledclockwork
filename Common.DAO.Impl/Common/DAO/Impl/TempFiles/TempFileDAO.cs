using System;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Databases;
using TechnoPro.Common.DAO.TempFiles;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Files;
using TechnoPro.Common.Public.Entities.Files.FileUpload;

namespace TechnoPro.Common.DAO.Impl.TempFiles
{
	// Token: 0x02000039 RID: 57
	public class TempFileDAO : ITempFileDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000168 RID: 360 RVA: 0x0000B98D File Offset: 0x00009B8D
		public TempFileDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000169 RID: 361 RVA: 0x0000B99F File Offset: 0x00009B9F
		// (set) Token: 0x0600016A RID: 362 RVA: 0x0000B9A7 File Offset: 0x00009BA7
		public OperationContext OpContext { get; set; }

		// Token: 0x0600016B RID: 363 RVA: 0x0000B9B0 File Offset: 0x00009BB0
		private static DatabaseLayer GetFilesDatabaseLayerOrClockWorkIfNoFiles(OperationContext opContext)
		{
			bool flag;
			return TempFileDAO.GetFilesDatabaseLayerOrClockWorkIfNoFiles(out flag, opContext);
		}

		// Token: 0x0600016C RID: 364 RVA: 0x0000B9CC File Offset: 0x00009BCC
		private static DatabaseLayer GetFilesDatabaseLayerOrClockWorkIfNoFiles(out bool hasFilesDb, OperationContext opContext)
		{
			DatabaseLayer databaseLayer;
			try
			{
				databaseLayer = DatabaseLayerFactory.ClockWorkFiles;
			}
			catch
			{
				databaseLayer = null;
			}
			hasFilesDb = (databaseLayer != null);
			return hasFilesDb ? databaseLayer : DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, (opContext != null) ? opContext.TenantId : null);
		}

		// Token: 0x0600016D RID: 365 RVA: 0x0000BA20 File Offset: 0x00009C20
		public void DeleteOldTempFiles(DateTime minDateToKeep)
		{
			DatabaseLayer filesDatabaseLayerOrClockWorkIfNoFiles = TempFileDAO.GetFilesDatabaseLayerOrClockWorkIfNoFiles(this.OpContext);
			DbParameter[] parameters = new DbParameter[]
			{
				filesDatabaseLayerOrClockWorkIfNoFiles.GetParameter("@mindate", DbType.DateTime, minDateToKeep)
			};
			filesDatabaseLayerOrClockWorkIfNoFiles.ExecuteNonQuery("DELETE FROM TempFiles WHERE dateentered<@mindate", parameters);
		}

		// Token: 0x0600016E RID: 366 RVA: 0x0000BA64 File Offset: 0x00009C64
		public int AddNewTempFile(TempFileContext context, BinaryFile fileToUpload)
		{
			DatabaseLayer filesDatabaseLayerOrClockWorkIfNoFiles = TempFileDAO.GetFilesDatabaseLayerOrClockWorkIfNoFiles(this.OpContext);
			DbParameter[] array = new DbParameter[]
			{
				filesDatabaseLayerOrClockWorkIfNoFiles.GetOutputParameter("@tempfileid", DbType.Int32, 0),
				filesDatabaseLayerOrClockWorkIfNoFiles.GetParameter("@usagecode", DbType.String, context.Usage.GetAttribute<TempFileUsageAttribute>().UsageCode),
				filesDatabaseLayerOrClockWorkIfNoFiles.GetParameter("@groupname", DbType.String, context.GroupId ?? ""),
				filesDatabaseLayerOrClockWorkIfNoFiles.GetParameter("@filename", DbType.String, fileToUpload.FileName),
				filesDatabaseLayerOrClockWorkIfNoFiles.GetParameter("@filebytes", DbType.Binary, fileToUpload.ByteArray)
			};
			filesDatabaseLayerOrClockWorkIfNoFiles.ExecuteNonQuery("INSERT INTO TempFiles (UsageCode,GroupName,Filename,FileBytes) VALUES (@usagecode,@groupname,@filename,@filebytes); SET @tempfileid = CAST(SCOPE_IDENTITY() as int)", array);
			return (array[0].Value == null || array[0].Value is DBNull) ? 0 : ((int)array[0].Value);
		}

		// Token: 0x0600016F RID: 367 RVA: 0x0000BB40 File Offset: 0x00009D40
		[DebuggerStepThrough]
		public Task<int> AddNewTempFileAsync(TempFileContext context, BinaryFile fileToUpload)
		{
			TempFileDAO.<AddNewTempFileAsync>d__9 <AddNewTempFileAsync>d__ = new TempFileDAO.<AddNewTempFileAsync>d__9();
			<AddNewTempFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<AddNewTempFileAsync>d__.<>4__this = this;
			<AddNewTempFileAsync>d__.context = context;
			<AddNewTempFileAsync>d__.fileToUpload = fileToUpload;
			<AddNewTempFileAsync>d__.<>1__state = -1;
			<AddNewTempFileAsync>d__.<>t__builder.Start<TempFileDAO.<AddNewTempFileAsync>d__9>(ref <AddNewTempFileAsync>d__);
			return <AddNewTempFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000170 RID: 368 RVA: 0x0000BB94 File Offset: 0x00009D94
		public BinaryFile GetTempFile(TempFileContext context, int tempFileId)
		{
			DatabaseLayer filesDatabaseLayerOrClockWorkIfNoFiles = TempFileDAO.GetFilesDatabaseLayerOrClockWorkIfNoFiles(this.OpContext);
			DbParameter[] parameters = new DbParameter[]
			{
				filesDatabaseLayerOrClockWorkIfNoFiles.GetParameter("@usagecode", DbType.String, context.Usage.GetAttribute<TempFileUsageAttribute>().UsageCode),
				filesDatabaseLayerOrClockWorkIfNoFiles.GetParameter("@groupname", DbType.String, context.GroupId ?? ""),
				filesDatabaseLayerOrClockWorkIfNoFiles.GetParameter("@tempfileid", DbType.Int32, tempFileId)
			};
			BinaryFile result;
			using (IDataReader dataReader = filesDatabaseLayerOrClockWorkIfNoFiles.ExecuteQueryReader("SELECT filename,filebytes FROM tempfiles WHERE usagecode=@usagecode AND groupname=@groupname AND tempfileid=@tempfileid", parameters))
			{
				bool flag = dataReader == null || !dataReader.Read();
				if (flag)
				{
					result = null;
				}
				else
				{
					result = new BinaryFile
					{
						FileName = dataReader["filename"].ToString(),
						ByteArray = ((dataReader["filebytes"] is DBNull) ? null : ((byte[])dataReader["filebytes"]))
					};
				}
			}
			return result;
		}

		// Token: 0x06000171 RID: 369 RVA: 0x0000BCA4 File Offset: 0x00009EA4
		[DebuggerStepThrough]
		public Task<BinaryFile> GetTempFileAsync(TempFileContext context, int tempFileId)
		{
			TempFileDAO.<GetTempFileAsync>d__11 <GetTempFileAsync>d__ = new TempFileDAO.<GetTempFileAsync>d__11();
			<GetTempFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder<BinaryFile>.Create();
			<GetTempFileAsync>d__.<>4__this = this;
			<GetTempFileAsync>d__.context = context;
			<GetTempFileAsync>d__.tempFileId = tempFileId;
			<GetTempFileAsync>d__.<>1__state = -1;
			<GetTempFileAsync>d__.<>t__builder.Start<TempFileDAO.<GetTempFileAsync>d__11>(ref <GetTempFileAsync>d__);
			return <GetTempFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0000BCF8 File Offset: 0x00009EF8
		public void DeleteTempFiles(TempFileContext context)
		{
			DatabaseLayer filesDatabaseLayerOrClockWorkIfNoFiles = TempFileDAO.GetFilesDatabaseLayerOrClockWorkIfNoFiles(this.OpContext);
			DbParameter[] parameters = new DbParameter[]
			{
				filesDatabaseLayerOrClockWorkIfNoFiles.GetParameter("@usagecode", DbType.String, context.Usage.GetAttribute<TempFileUsageAttribute>().UsageCode),
				filesDatabaseLayerOrClockWorkIfNoFiles.GetParameter("@groupname", DbType.String, context.GroupId ?? "")
			};
			filesDatabaseLayerOrClockWorkIfNoFiles.ExecuteNonQuery("DELETE FROM TempFiles WHERE usagecode=@usagecode AND groupname=@groupname", parameters);
		}

		// Token: 0x06000173 RID: 371 RVA: 0x0000BD6C File Offset: 0x00009F6C
		public void DeleteTempFile(TempFileContext context, int tempFileId)
		{
			DatabaseLayer filesDatabaseLayerOrClockWorkIfNoFiles = TempFileDAO.GetFilesDatabaseLayerOrClockWorkIfNoFiles(this.OpContext);
			DbParameter[] parameters = new DbParameter[]
			{
				filesDatabaseLayerOrClockWorkIfNoFiles.GetParameter("@usagecode", DbType.String, context.Usage.GetAttribute<TempFileUsageAttribute>().UsageCode),
				filesDatabaseLayerOrClockWorkIfNoFiles.GetParameter("@groupname", DbType.String, context.GroupId ?? ""),
				filesDatabaseLayerOrClockWorkIfNoFiles.GetParameter("@tempfileid", DbType.Int32, tempFileId)
			};
			filesDatabaseLayerOrClockWorkIfNoFiles.ExecuteNonQuery("DELETE FROM TempFiles WHERE usagecode=@usagecode AND groupname=@groupname AND tempfileid=@tempfileid", parameters);
		}

		// Token: 0x06000174 RID: 372 RVA: 0x0000BDF4 File Offset: 0x00009FF4
		[DebuggerStepThrough]
		public Task DeleteTempFileAsync(TempFileContext context, int tempFileId)
		{
			TempFileDAO.<DeleteTempFileAsync>d__14 <DeleteTempFileAsync>d__ = new TempFileDAO.<DeleteTempFileAsync>d__14();
			<DeleteTempFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<DeleteTempFileAsync>d__.<>4__this = this;
			<DeleteTempFileAsync>d__.context = context;
			<DeleteTempFileAsync>d__.tempFileId = tempFileId;
			<DeleteTempFileAsync>d__.<>1__state = -1;
			<DeleteTempFileAsync>d__.<>t__builder.Start<TempFileDAO.<DeleteTempFileAsync>d__14>(ref <DeleteTempFileAsync>d__);
			return <DeleteTempFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000175 RID: 373 RVA: 0x0000BE48 File Offset: 0x0000A048
		private static string GetDatabaseNameForFilesDb(DatabaseLayer dm)
		{
			object obj = dm.ExecuteScalar("SELECT DB_NAME()");
			string text = ((obj != null) ? obj.ToString() : null) ?? "";
			return (text.Length > 0) ? ("[" + text + "]") : string.Empty;
		}

		// Token: 0x06000176 RID: 374 RVA: 0x0000BE9C File Offset: 0x0000A09C
		public int[] CopyTempFilesToInstructorExamUploadAndDeleteTempFile(TempFileContext context, int examId, int whoEntered, string description)
		{
			bool flag;
			DatabaseLayer filesDatabaseLayerOrClockWorkIfNoFiles = TempFileDAO.GetFilesDatabaseLayerOrClockWorkIfNoFiles(out flag, this.OpContext);
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] array = new DbParameter[]
			{
				databaseLayer.GetOutputParameter("@examfileids", DbType.String, -1),
				databaseLayer.GetParameter("@examid", DbType.Int32, examId),
				databaseLayer.GetParameter("@whoenteredpid", DbType.Int32, whoEntered),
				databaseLayer.GetParameter("@description", DbType.String, description ?? ""),
				databaseLayer.GetParameter("@usagecode", DbType.String, context.Usage.GetAttribute<TempFileUsageAttribute>().UsageCode),
				databaseLayer.GetParameter("@groupname", DbType.String, context.GroupId ?? "")
			};
			string query = string.Format("DECLARE @t1 table( examfileid int );\r\n\r\nINSERT INTO ExamFiles (examid,[filename],filedata,dateentered,whoentered,[description],visible)\r\n\tOUTPUT\tINSERTED.examfileid\r\n\tINTO @t1\r\n\t\tSELECT\t@examid,[filename],filebytes,getdate() AS dateentered,@whoenteredpid,@description,1 FROM {0}TempFiles WHERE usagecode=@usagecode AND groupname=@groupname\r\n\r\nDECLARE @results varchar(max) = ''\r\nSELECT @results = COALESCE(@results + ',', '' ) + convert(varchar(12),cast(examfileid AS varchar(8000))) FROM @t1\r\n\r\nSET @examfileids=@results", flag ? (TempFileDAO.GetDatabaseNameForFilesDb(filesDatabaseLayerOrClockWorkIfNoFiles) + "..") : "");
			databaseLayer.ExecuteNonQuery(query, array);
			string text = (array[0].Value == null) ? "" : array[0].Value.ToString();
			this.DeleteTempFiles(context);
			return (from h in text.Split(new char[]
			{
				','
			}).Select(delegate(string g)
			{
				int num;
				return int.TryParse(g, out num) ? num : 0;
			})
			where h > 0
			select h).ToArray<int>();
		}
	}
}
