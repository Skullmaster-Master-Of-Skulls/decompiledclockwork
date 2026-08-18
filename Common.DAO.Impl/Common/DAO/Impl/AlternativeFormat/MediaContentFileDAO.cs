using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.Common.DAO.AlternativeFormat;
using TechnoPro.Common.DAO.Impl.People;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AlternativeFormat;
using TechnoPro.Common.Public.Entities.FileStorage;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.DAO.Impl.AlternativeFormat
{
	// Token: 0x02000167 RID: 359
	public class MediaContentFileDAO : IMediaContentFileDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000A82 RID: 2690 RVA: 0x0006E5D1 File Offset: 0x0006C7D1
		// (set) Token: 0x06000A83 RID: 2691 RVA: 0x0006E5D9 File Offset: 0x0006C7D9
		public OperationContext OpContext { get; set; }

		// Token: 0x06000A84 RID: 2692 RVA: 0x0006E5E2 File Offset: 0x0006C7E2
		public MediaContentFileDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x0006E5F4 File Offset: 0x0006C7F4
		[DebuggerStepThrough]
		public Task<IList<StudentMediaContentFileWithProofOfPurchaseInfo>> LoadAvailableMediaContentFileByStudentIdAsync(int studentId, DateTime startDate, DateTime endDate)
		{
			MediaContentFileDAO.<LoadAvailableMediaContentFileByStudentIdAsync>d__5 <LoadAvailableMediaContentFileByStudentIdAsync>d__ = new MediaContentFileDAO.<LoadAvailableMediaContentFileByStudentIdAsync>d__5();
			<LoadAvailableMediaContentFileByStudentIdAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<StudentMediaContentFileWithProofOfPurchaseInfo>>.Create();
			<LoadAvailableMediaContentFileByStudentIdAsync>d__.<>4__this = this;
			<LoadAvailableMediaContentFileByStudentIdAsync>d__.studentId = studentId;
			<LoadAvailableMediaContentFileByStudentIdAsync>d__.startDate = startDate;
			<LoadAvailableMediaContentFileByStudentIdAsync>d__.endDate = endDate;
			<LoadAvailableMediaContentFileByStudentIdAsync>d__.<>1__state = -1;
			<LoadAvailableMediaContentFileByStudentIdAsync>d__.<>t__builder.Start<MediaContentFileDAO.<LoadAvailableMediaContentFileByStudentIdAsync>d__5>(ref <LoadAvailableMediaContentFileByStudentIdAsync>d__);
			return <LoadAvailableMediaContentFileByStudentIdAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x0006E650 File Offset: 0x0006C850
		[DebuggerStepThrough]
		public Task<IList<StudentMediaContentFileWithProofOfPurchaseInfo>> LoadAvailableMediaContentFileByStudentAndMediaContentAsync(int studentId, Guid mediaContentId, DateTime startDate, DateTime endDate)
		{
			MediaContentFileDAO.<LoadAvailableMediaContentFileByStudentAndMediaContentAsync>d__6 <LoadAvailableMediaContentFileByStudentAndMediaContentAsync>d__ = new MediaContentFileDAO.<LoadAvailableMediaContentFileByStudentAndMediaContentAsync>d__6();
			<LoadAvailableMediaContentFileByStudentAndMediaContentAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<StudentMediaContentFileWithProofOfPurchaseInfo>>.Create();
			<LoadAvailableMediaContentFileByStudentAndMediaContentAsync>d__.<>4__this = this;
			<LoadAvailableMediaContentFileByStudentAndMediaContentAsync>d__.studentId = studentId;
			<LoadAvailableMediaContentFileByStudentAndMediaContentAsync>d__.mediaContentId = mediaContentId;
			<LoadAvailableMediaContentFileByStudentAndMediaContentAsync>d__.startDate = startDate;
			<LoadAvailableMediaContentFileByStudentAndMediaContentAsync>d__.endDate = endDate;
			<LoadAvailableMediaContentFileByStudentAndMediaContentAsync>d__.<>1__state = -1;
			<LoadAvailableMediaContentFileByStudentAndMediaContentAsync>d__.<>t__builder.Start<MediaContentFileDAO.<LoadAvailableMediaContentFileByStudentAndMediaContentAsync>d__6>(ref <LoadAvailableMediaContentFileByStudentAndMediaContentAsync>d__);
			return <LoadAvailableMediaContentFileByStudentAndMediaContentAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000A87 RID: 2695 RVA: 0x0006E6B4 File Offset: 0x0006C8B4
		public IList<MediaContentFileWithoutData> LoadMediaContentFileByMediaContentPerFormatId(int mediaContentPerFormatId, int studentId = 0)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			List<MediaContentFileWithoutData> list = new List<MediaContentFileWithoutData>();
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@mediacontentperformatid", DbType.Int32, mediaContentPerFormatId),
				databaseLayer.GetParameter("@studentid", DbType.Int32, studentId)
			};
			using (IDataReader dataReader = databaseLayer.ExecuteStoredProcedureReader("sp_AlternateFormat_MediaContentFileByFormatId", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						MediaContentFileWithoutData mediaContentFileWithoutDataFromReader = this.GetMediaContentFileWithoutDataFromReader(dataReader, batchDecryptor);
						bool flag2 = mediaContentFileWithoutDataFromReader != null;
						if (flag2)
						{
							list.Add(mediaContentFileWithoutDataFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x0006E790 File Offset: 0x0006C990
		[DebuggerStepThrough]
		public Task<IList<MediaContentFileWithoutData>> LoadMediaContentFileByMediaContentPerFormatIdAsync(int mediaContentPerFormatId, int studentId = 0)
		{
			MediaContentFileDAO.<LoadMediaContentFileByMediaContentPerFormatIdAsync>d__8 <LoadMediaContentFileByMediaContentPerFormatIdAsync>d__ = new MediaContentFileDAO.<LoadMediaContentFileByMediaContentPerFormatIdAsync>d__8();
			<LoadMediaContentFileByMediaContentPerFormatIdAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<MediaContentFileWithoutData>>.Create();
			<LoadMediaContentFileByMediaContentPerFormatIdAsync>d__.<>4__this = this;
			<LoadMediaContentFileByMediaContentPerFormatIdAsync>d__.mediaContentPerFormatId = mediaContentPerFormatId;
			<LoadMediaContentFileByMediaContentPerFormatIdAsync>d__.studentId = studentId;
			<LoadMediaContentFileByMediaContentPerFormatIdAsync>d__.<>1__state = -1;
			<LoadMediaContentFileByMediaContentPerFormatIdAsync>d__.<>t__builder.Start<MediaContentFileDAO.<LoadMediaContentFileByMediaContentPerFormatIdAsync>d__8>(ref <LoadMediaContentFileByMediaContentPerFormatIdAsync>d__);
			return <LoadMediaContentFileByMediaContentPerFormatIdAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x0006E7E4 File Offset: 0x0006C9E4
		public IList<MediaContentFileWithoutData> LoadMediaContentFileByMediaContentPerFormatId(Guid mediaContentId, MediaContentFormat mediaContentFormat, int studentId = 0)
		{
			IMediaContentDAO mediaContentDAO = new MediaContentDAO(this.OpContext);
			int mediaContentPerFormatId = mediaContentDAO.GetMediaContentPerFormatId(mediaContentId, mediaContentFormat);
			return this.LoadMediaContentFileByMediaContentPerFormatId(mediaContentPerFormatId, studentId);
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x0006E814 File Offset: 0x0006CA14
		public int GetCountAvailableAlternateFormatFiles(int mediaContentPerFormatId, int studentId = 0)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@mediacontentperformatid", DbType.Int32, mediaContentPerFormatId),
				databaseLayer.GetParameter("@studentid", DbType.Int32, studentId)
			};
			return (int)databaseLayer.ExecuteScalar("SELECT count(*) FROM AlternativeFormat_MediaContentFile \r\n            where FKMediaContentFormatID = @mediacontentperformatid AND (UniqueOwner is NULL OR UniqueOwner = 0 OR UniqueOwner = @studentid)", parameters);
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x0006E884 File Offset: 0x0006CA84
		[DebuggerStepThrough]
		public Task<int> CreateStudentMediaContentFileTrackingAsync(StudentMediaContentFileTrackingInfo studentFileTrackingInfo)
		{
			MediaContentFileDAO.<CreateStudentMediaContentFileTrackingAsync>d__11 <CreateStudentMediaContentFileTrackingAsync>d__ = new MediaContentFileDAO.<CreateStudentMediaContentFileTrackingAsync>d__11();
			<CreateStudentMediaContentFileTrackingAsync>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<CreateStudentMediaContentFileTrackingAsync>d__.<>4__this = this;
			<CreateStudentMediaContentFileTrackingAsync>d__.studentFileTrackingInfo = studentFileTrackingInfo;
			<CreateStudentMediaContentFileTrackingAsync>d__.<>1__state = -1;
			<CreateStudentMediaContentFileTrackingAsync>d__.<>t__builder.Start<MediaContentFileDAO.<CreateStudentMediaContentFileTrackingAsync>d__11>(ref <CreateStudentMediaContentFileTrackingAsync>d__);
			return <CreateStudentMediaContentFileTrackingAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x0006E8D0 File Offset: 0x0006CAD0
		[DebuggerStepThrough]
		public Task DeleteMediaContentFileAsync(FileIdentifier fileId)
		{
			MediaContentFileDAO.<DeleteMediaContentFileAsync>d__12 <DeleteMediaContentFileAsync>d__ = new MediaContentFileDAO.<DeleteMediaContentFileAsync>d__12();
			<DeleteMediaContentFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<DeleteMediaContentFileAsync>d__.<>4__this = this;
			<DeleteMediaContentFileAsync>d__.fileId = fileId;
			<DeleteMediaContentFileAsync>d__.<>1__state = -1;
			<DeleteMediaContentFileAsync>d__.<>t__builder.Start<MediaContentFileDAO.<DeleteMediaContentFileAsync>d__12>(ref <DeleteMediaContentFileAsync>d__);
			return <DeleteMediaContentFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x0006E91C File Offset: 0x0006CB1C
		public IList<MediaContentFileWithoutData> GetMediaContentFileMatchingUsingEquivalentCoursesAlt(string searchText, int lucourseid = 0)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			List<MediaContentFileWithoutData> list = new List<MediaContentFileWithoutData>();
			DbParameter[] array;
			if (lucourseid <= 0)
			{
				(array = new DbParameter[1])[0] = databaseLayer.GetParameter("@searchtext", DbType.String, searchText);
			}
			else
			{
				DbParameter[] array2 = new DbParameter[2];
				array2[0] = databaseLayer.GetParameter("@searchtext", DbType.String, searchText);
				array = array2;
				array2[1] = databaseLayer.GetParameter("@lucourseid", DbType.Int32, lucourseid);
			}
			DbParameter[] parameters = array;
			string storeProcedureName = (lucourseid > 0) ? "sp_AlternateFormat_SearchMediaContentFileUsingEquivalentCoursesAlt" : "sp_AlternateFormat_SearchMediaContentFile";
			using (IDataReader dataReader = databaseLayer.ExecuteStoredProcedureReader(storeProcedureName, parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						MediaContentFileWithoutData mediaContentFileWithoutDataFromReader = this.GetMediaContentFileWithoutDataFromReader(dataReader, batchDecryptor);
						bool flag2 = mediaContentFileWithoutDataFromReader != null;
						if (flag2)
						{
							list.Add(mediaContentFileWithoutDataFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x0006EA24 File Offset: 0x0006CC24
		public IList<MediaContentFileWithoutData> GetMediaContentFileMatchingUsingUserDefinedEquivalentCoursesAlt(string searchText, int lucourseid = 0)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			List<MediaContentFileWithoutData> list = new List<MediaContentFileWithoutData>();
			DbParameter[] array;
			if (lucourseid <= 0)
			{
				(array = new DbParameter[1])[0] = databaseLayer.GetParameter("@searchtext", DbType.String, searchText);
			}
			else
			{
				DbParameter[] array2 = new DbParameter[2];
				array2[0] = databaseLayer.GetParameter("@searchtext", DbType.String, searchText);
				array = array2;
				array2[1] = databaseLayer.GetParameter("@lucourseid", DbType.Int32, lucourseid);
			}
			DbParameter[] parameters = array;
			string storeProcedureName = (lucourseid > 0) ? "sp_AlternateFormat_SearchMediaContentFileUsingEquivalentCoursesAlt_UserDefined" : "sp_AlternateFormat_SearchMediaContentFile";
			using (IDataReader dataReader = databaseLayer.ExecuteStoredProcedureReader(storeProcedureName, parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						MediaContentFileWithoutData mediaContentFileWithoutDataFromReader = this.GetMediaContentFileWithoutDataFromReader(dataReader, batchDecryptor);
						bool flag2 = mediaContentFileWithoutDataFromReader != null;
						if (flag2)
						{
							list.Add(mediaContentFileWithoutDataFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x0006EB2C File Offset: 0x0006CD2C
		public MediaContentFileWithoutData CreateMediaContentFileInfo(MediaContentFileWithoutData fileInfo)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			bool flag = fileInfo.MediaContentPerFormatId == 0;
			if (flag)
			{
				IMediaContentDAO mediaContentDAO = new MediaContentDAO(this.OpContext);
				fileInfo.MediaContentPerFormatId = mediaContentDAO.GetMediaContentPerFormatId(fileInfo.MediaContent.MediaContentUniqueId, fileInfo.ContentFormat);
				bool flag2 = fileInfo.MediaContentPerFormatId == 0;
				if (flag2)
				{
					return null;
				}
			}
			DbParameter[] array = new DbParameter[12];
			array[0] = databaseLayer.GetOutputParameter("@mediacontentfileid", DbType.Int32, 0);
			array[1] = databaseLayer.GetParameter("@size", DbType.Int32, fileInfo.Size);
			array[2] = databaseLayer.GetParameter("@language", DbType.String, fileInfo.ContentLanguage.ToString());
			array[3] = databaseLayer.GetParameter("@sourceprovider", DbType.String, fileInfo.SourceProvider ?? string.Empty);
			array[4] = databaseLayer.GetParameter("@notes", DbType.String, fileInfo.Notes ?? string.Empty);
			int num = 5;
			DatabaseLayer databaseLayer2 = databaseLayer;
			string pName = "@uniqueowner";
			DbType pType = DbType.Int32;
			PersonBase uniqueStudentOwner = fileInfo.UniqueStudentOwner;
			int? num2 = (uniqueStudentOwner != null) ? new int?(uniqueStudentOwner.PersonId) : null;
			array[num] = databaseLayer2.GetParameter(pName, pType, (num2 != null) ? num2.GetValueOrDefault() : DBNull.Value);
			array[6] = databaseLayer.GetParameter("@mediacontentformatid", DbType.Int32, fileInfo.MediaContentPerFormatId);
			array[7] = databaseLayer.GetParameter("@mediacontentfilename", DbType.String, fileInfo.Filename ?? string.Empty);
			int num3 = 8;
			DatabaseLayer databaseLayer3 = databaseLayer;
			string pName2 = "@whouploadpersonid";
			DbType pType2 = DbType.Int32;
			PersonBase whoUploadFile = fileInfo.WhoUploadFile;
			array[num3] = databaseLayer3.GetParameter(pName2, pType2, (whoUploadFile != null) ? whoUploadFile.PersonId : this.OpContext.WhoAmI);
			array[9] = databaseLayer.GetParameter("@hardcopy", DbType.Boolean, fileInfo.HardCopy);
			array[10] = databaseLayer.GetParameter("@isfilereusable", DbType.Boolean, fileInfo.UniqueStudentOwner == null);
			int num4 = 11;
			DatabaseLayer databaseLayer4 = databaseLayer;
			string pName3 = "@fileid";
			DbType pType3 = DbType.Guid;
			Guid? mediaContentFileUniqueId = fileInfo.MediaContentFileUniqueId;
			array[num4] = databaseLayer4.GetParameter(pName3, pType3, (mediaContentFileUniqueId != null) ? mediaContentFileUniqueId.GetValueOrDefault() : DBNull.Value);
			DbParameter[] array2 = array;
			databaseLayer.ExecuteNonQuery("INSERT INTO [AlternativeFormat_MediaContentFile]\r\n           ([Size]\r\n           ,[Language]\r\n           ,[SourceProvider]\r\n           ,[MediaContentFileNotes]\r\n           ,[IsReusable]\r\n           ,[UniqueOwner]\r\n           ,[FKMediaContentFormatID]\r\n           ,[WhoUploadFilePersonId]\r\n           ,[MediaContentFilename]\r\n           ,[HardCopy]\r\n           ,[MediaContentFileUniqueID])\r\n     VALUES\r\n           (@size\r\n           ,@language\r\n           ,@sourceprovider\r\n           ,@notes\r\n           ,@isfilereusable\r\n           ,@uniqueowner\r\n           ,@mediacontentformatid\r\n           ,@whouploadpersonid\r\n           ,@mediacontentfilename\r\n           ,@hardcopy\r\n           ,@fileid) \r\n             set @mediacontentfileid = SCOPE_IDENTITY()", array2);
			fileInfo.MediaContentFileId = ((array2[0].Value is DBNull) ? 0 : ((int)array2[0].Value));
			return fileInfo;
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x0006ED9C File Offset: 0x0006CF9C
		[DebuggerStepThrough]
		public Task<MediaContentFileWithoutData> CreateMediaContentFileInfoAsync(MediaContentFileWithoutData fileInfo)
		{
			MediaContentFileDAO.<CreateMediaContentFileInfoAsync>d__16 <CreateMediaContentFileInfoAsync>d__ = new MediaContentFileDAO.<CreateMediaContentFileInfoAsync>d__16();
			<CreateMediaContentFileInfoAsync>d__.<>t__builder = AsyncTaskMethodBuilder<MediaContentFileWithoutData>.Create();
			<CreateMediaContentFileInfoAsync>d__.<>4__this = this;
			<CreateMediaContentFileInfoAsync>d__.fileInfo = fileInfo;
			<CreateMediaContentFileInfoAsync>d__.<>1__state = -1;
			<CreateMediaContentFileInfoAsync>d__.<>t__builder.Start<MediaContentFileDAO.<CreateMediaContentFileInfoAsync>d__16>(ref <CreateMediaContentFileInfoAsync>d__);
			return <CreateMediaContentFileInfoAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x0006EDE8 File Offset: 0x0006CFE8
		public IList<MediaContentFileWithoutData> LoadMediaContentFileByContent(Guid mediaContentId, int studentId = 0)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			List<MediaContentFileWithoutData> list = new List<MediaContentFileWithoutData>();
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@mediacontentid", DbType.Guid, mediaContentId),
				databaseLayer.GetParameter("@studentid", DbType.Int32, studentId)
			};
			using (IDataReader dataReader = databaseLayer.ExecuteStoredProcedureReader("sp_AlternateFormat_MediaContentFileByContentId", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						MediaContentFileWithoutData mediaContentFileWithoutDataFromReader = this.GetMediaContentFileWithoutDataFromReader(dataReader, batchDecryptor);
						bool flag2 = mediaContentFileWithoutDataFromReader != null;
						if (flag2)
						{
							list.Add(mediaContentFileWithoutDataFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x0006EEC4 File Offset: 0x0006D0C4
		public IList<StudentMediaContentFileWithProofOfPurchaseInfo> LoadMediaContentFileByStudentId(int studentId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			List<StudentMediaContentFileWithProofOfPurchaseInfo> list = new List<StudentMediaContentFileWithProofOfPurchaseInfo>();
			DbParameter parameter = databaseLayer.GetParameter("@studentid", DbType.Int32, studentId);
			using (IDataReader dataReader = databaseLayer.ExecuteStoredProcedureReader("sp_AlternateFormat_LoadMediaContentFilesByStudentId", new DbParameter[]
			{
				parameter
			}))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						StudentMediaContentFileWithProofOfPurchaseInfo mediaContentFileWithProofOfPurchaseFromReader = this.GetMediaContentFileWithProofOfPurchaseFromReader(dataReader, batchDecryptor);
						bool flag2 = mediaContentFileWithProofOfPurchaseFromReader != null;
						if (flag2)
						{
							mediaContentFileWithProofOfPurchaseFromReader.StudentPersonId = studentId;
							list.Add(mediaContentFileWithProofOfPurchaseFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000A93 RID: 2707 RVA: 0x0006EF94 File Offset: 0x0006D194
		public void UpdateMediaContentFileWithoutData(MediaContentFileWithoutData mediaContentFile)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			bool flag = mediaContentFile.MediaContentPerFormatId == 0;
			if (flag)
			{
				IMediaContentDAO mediaContentDAO = new MediaContentDAO(this.OpContext);
				mediaContentFile.MediaContentPerFormatId = mediaContentDAO.GetMediaContentPerFormatId(mediaContentFile.MediaContent.MediaContentUniqueId, mediaContentFile.ContentFormat);
			}
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@size", DbType.Int32, mediaContentFile.Size),
				databaseLayer.GetParameter("@language", DbType.String, mediaContentFile.ContentLanguage.ToString()),
				databaseLayer.GetParameter("@sourceprovider", DbType.String, mediaContentFile.SourceProvider ?? string.Empty),
				databaseLayer.GetParameter("@notes", DbType.String, mediaContentFile.Notes ?? string.Empty),
				databaseLayer.GetParameter("@uniqueowner", DbType.Int32, (mediaContentFile.UniqueStudentOwner == null) ? DBNull.Value : mediaContentFile.UniqueStudentOwner.PersonId),
				databaseLayer.GetParameter("@mediacontentformatid", DbType.Int32, mediaContentFile.MediaContentPerFormatId),
				databaseLayer.GetParameter("@mediacontentfileid", DbType.Int32, mediaContentFile.MediaContentFileId),
				databaseLayer.GetParameter("@mediacontentfilename", DbType.String, mediaContentFile.Filename),
				databaseLayer.GetParameter("@datecreated", DbType.DateTime, mediaContentFile.DateCreated),
				databaseLayer.GetParameter("@whouploadpersonid", DbType.Int32, mediaContentFile.WhoUploadFile.PersonId),
				databaseLayer.GetParameter("@hardcopy", DbType.Boolean, mediaContentFile.HardCopy),
				databaseLayer.GetParameter("@isfilereusable", DbType.Boolean, mediaContentFile.UniqueStudentOwner == null)
			};
			databaseLayer.ExecuteNonQuery("UPDATE [AlternativeFormat_MediaContentFile]\r\n         SET   [Size] = @size\r\n              ,[Language] = @language\r\n              ,[SourceProvider] = @sourceprovider\r\n              ,[MediaContentFileNotes] = @notes\r\n              ,[IsReusable] = @isfilereusable\r\n              ,[UniqueOwner] = @uniqueowner\r\n              ,[FKMediaContentFormatID] = @mediacontentformatid\r\n              ,[WhoUploadFilePersonId] = @whouploadpersonid\r\n              ,[DateCreated] = @datecreated\r\n              ,[MediaContentFilename] = @mediacontentfilename\r\n              ,[HardCopy] = @hardcopy\r\n        WHERE [MediaContentFileID] = @mediacontentfileid", parameters);
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x0006F178 File Offset: 0x0006D378
		public void DeleteMediaContentFile(FileIdentifier fileId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			bool flag = fileId.FileUniqueId != null;
			if (flag)
			{
				DbParameter[] array = new DbParameter[1];
				int num = 0;
				DatabaseLayer databaseLayer2 = databaseLayer;
				string pName = "@fileid";
				DbType pType = DbType.Guid;
				Guid? fileUniqueId = fileId.FileUniqueId;
				array[num] = databaseLayer2.GetParameter(pName, pType, (fileUniqueId != null) ? fileUniqueId.GetValueOrDefault() : DBNull.Value);
				DbParameter[] parameters = array;
				databaseLayer.ExecuteNonQuery("delete from [AlternativeFormat_MediaContentFile] where [MediaContentFileUniqueID]=@fileid", parameters);
			}
			else
			{
				DbParameter[] parameters2 = new DbParameter[]
				{
					databaseLayer.GetParameter("@fileid", DbType.Int32, fileId.LegacyId)
				};
				databaseLayer.ExecuteNonQuery("delete from [AlternativeFormat_MediaContentFile] where [MediaContentFileID]=@fileid", parameters2);
			}
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x0006F230 File Offset: 0x0006D430
		private MediaContentFileWithoutData GetMediaContentFileWithoutDataFromReader(IDataReader reader, IBatchDecryptor decryptor = null)
		{
			MediaContentDAO mediaContentDAO = new MediaContentDAO(this.OpContext);
			return new MediaContentFileWithoutData
			{
				ContentFormat = ((reader["MediaContentFormat"] is DBNull) ? MediaContentFormat.UNSPECIFIED : ((MediaContentFormat)Enum.Parse(typeof(MediaContentFormat), Convert.ToString(reader["MediaContentFormat"])))),
				ContentLanguage = (eMediaContentLanguage)Enum.Parse(typeof(eMediaContentLanguage), Convert.ToString(reader["Language"])),
				MediaContentFileId = Convert.ToInt32(reader["MediaContentFileID"]),
				MediaContentFileUniqueId = ((reader["MediaContentFileUniqueID"] is DBNull) ? null : new Guid?((Guid)reader["MediaContentFileUniqueID"])),
				MediaContent = mediaContentDAO.GetMediaContentFromReader<MediaContent>(reader, decryptor, true),
				Notes = Convert.ToString(reader["MediaContentFileNotes"]),
				Size = (long)Convert.ToInt32(reader["Size"]),
				SourceProvider = Convert.ToString(reader["SourceProvider"]),
				UniqueStudentOwner = PeopleDAO.GetPersonFromReader("pow", reader, this.OpContext, decryptor),
				MediaContentPerFormatId = (int)reader["FKMediaContentFormatID"],
				DateCreated = Convert.ToDateTime(reader["FileDateCreated"]),
				Filename = Convert.ToString(reader["MediaContentFilename"]),
				WhoUploadFile = PeopleDAO.GetPersonFromReader("pupf", reader, this.OpContext, decryptor),
				HardCopy = (bool)reader["HardCopy"]
			};
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x0006F3F4 File Offset: 0x0006D5F4
		private StudentMediaContentFileWithProofOfPurchaseInfo GetMediaContentFileWithProofOfPurchaseFromReader(IDataReader reader, IBatchDecryptor decryptor = null)
		{
			MediaContentDAO mediaContentDAO = new MediaContentDAO(this.OpContext);
			return new StudentMediaContentFileWithProofOfPurchaseInfo
			{
				ContentFormat = ((reader["MediaContentFormat"] is DBNull) ? MediaContentFormat.UNSPECIFIED : ((MediaContentFormat)Enum.Parse(typeof(MediaContentFormat), Convert.ToString(reader["MediaContentFormat"])))),
				ContentLanguage = (eMediaContentLanguage)Enum.Parse(typeof(eMediaContentLanguage), Convert.ToString(reader["Language"])),
				MediaContentFileId = Convert.ToInt32(reader["MediaContentFileID"]),
				MediaContentFileUniqueId = ((reader["MediaContentFileUniqueID"] is DBNull) ? null : new Guid?((Guid)reader["MediaContentFileUniqueID"])),
				MediaContent = mediaContentDAO.GetMediaContentFromReader<MediaContent>(reader, decryptor, true),
				Notes = Convert.ToString(reader["MediaContentFileNotes"]),
				Size = (long)Convert.ToInt32(reader["Size"]),
				SourceProvider = Convert.ToString(reader["SourceProvider"]),
				UniqueStudentOwner = PeopleDAO.GetPersonFromReader("pow", reader, this.OpContext, decryptor),
				MediaContentPerFormatId = (int)reader["FKMediaContentFormatID"],
				DateCreated = Convert.ToDateTime(reader["FileDateCreated"]),
				Filename = Convert.ToString(reader["MediaContentFilename"]),
				WhoUploadFile = PeopleDAO.GetPersonFromReader("pupf", reader, this.OpContext, decryptor),
				HardCopy = (bool)reader["HardCopy"],
				FileStatus = (eStudentMediaContentFileStatus)Enum.Parse(typeof(eStudentMediaContentFileStatus), Convert.ToString(reader["FileStatus"])),
				ProofOfPurchaseId = ((reader["ProofOfPurchaseId"] is DBNull) ? 0 : ((int)reader["ProofOfPurchaseId"])),
				StudentPersonId = (int)reader["StudentPersonId"]
			};
		}
	}
}
