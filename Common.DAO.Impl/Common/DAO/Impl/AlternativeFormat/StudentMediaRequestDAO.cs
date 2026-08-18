using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ClockWorkLogger;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.Common.DAO.AlternativeFormat;
using TechnoPro.Common.DAO.Impl.Adapters;
using TechnoPro.Common.DAO.Impl.People;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AlternativeFormat;
using TechnoPro.Common.Public.Entities.People;
using TechnoPro.Common.Public.Exceptions;

namespace TechnoPro.Common.DAO.Impl.AlternativeFormat
{
	// Token: 0x0200016E RID: 366
	public class StudentMediaRequestDAO : IStudentMediaRequestDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000B1E RID: 2846 RVA: 0x00075E50 File Offset: 0x00074050
		// (set) Token: 0x06000B1F RID: 2847 RVA: 0x00075E58 File Offset: 0x00074058
		public OperationContext OpContext { get; set; }

		// Token: 0x06000B20 RID: 2848 RVA: 0x00075E61 File Offset: 0x00074061
		public StudentMediaRequestDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x00075E74 File Offset: 0x00074074
		public IList<MediaContentRequestedInfo> LoadAllMediaRequestInfoByJobId(int jobId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@mediajobid", DbType.Int32, jobId)
			};
			List<MediaContentRequestedInfo> list = new List<MediaContentRequestedInfo>();
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT  smr.[StudentMediaRequestID], smr.[RequestMadeFromStudentNo], smr.[CreatedDateTime], smr.[CompletedDateTime], \r\n                    smr.[CampusId],clk.CampusName,clk.CampusDescription,clk.[IsActive] as CampusIsActive,\r\n                    smrd.[IsApproved], smrd.[FKStudentMediaRequestID], smrd.[IsCompleted], smrd.[IsCancelled], smrd.[FKMediaJobId], mj.JobTitle,\r\n\t\t            smrd.[StudentMediaRequestDetailId], smrd.[FKMediaContentID], smrd.[Status], \r\n                    case pop.IsActive\r\n\t\t\t\t\t\twhen 1 then pop.ProofOfPurchaseID\r\n\t\t\t\t\t\telse NULL\r\n\t\t\t\t\tend as ProofOfPurchaseID,\r\n\t\t\t\t\tpop.IsActive, pop.ProofOfPurchaseNote, pop.WhoAcceptedProofOfPurchase, pop.WhenWasAccepted, pop.StudentPersonID, pop.[Filename], pop.Extension,\r\n                    smrd.[FKMediaContentPerFormatId], smrd.[AvailableStartTime], smrd.[AvailableEndTime], smrd.[StudentPreferredFormat],\r\n                    smrd.CreatedDateTime as [MediaRequestDetailCreatedDateTime], smrd.CompletedDateTime as [MediaRequestDetailCompletedDateTime], smrd.CompletionNotes,\r\n\t\t            mcf.[MediaContentFormat], mc.[ShortTitle], mc.[ISBN], mc.Authors, mc.[Edition], mc.[Summary], mc.[ProofOfPurchaseRequired],\r\n                    p.PersonID, p.firstname, p.lastname, p.middlename, p.student_no, pg.mingroupid,\r\n                    pop.WhoAcceptedProofOfPurchase as wpoppersonid, wpop.firstname as wpopfirstname, wpop.lastname as wpoplastname, wpop.middlename as wpopmiddlename, wpop.student_no as wpopstudent_no, wpopg.mingroupid as wpopmingroupid\r\n            FROM [AlternativeFormat_StudentMediaRequestDetail] as smrd\r\n            INNER JOIN  [AlternativeFormat_StudentMediaRequest] as smr ON smrd.[FKStudentMediaRequestID] = smr.[StudentMediaRequestID]\r\n            LEFT JOIN [AlternativeFormat_MediaContent_x_MediaContentFormat] as mcf ON mcf.[MediaContentPerFormatID] = smrd.[FKMediaContentPerFormatId] \r\n            LEFT JOIN [AlternativeFormat_MediaContent] as mc ON mc.[MediaContentID] = smrd.[FKMediaContentID] \r\n            LEFT JOIN people p ON p.PersonID=smr.[RequestMadeFromStudentNo]\r\n            LEFT JOIN peoplemingroup pg ON pg.PersonID=smr.[RequestMadeFromStudentNo]\r\n            LEFT JOIN CampusLookup clk ON clk.CampusId=smr.CampusId\r\n\t\t\tLEFT JOIN AlternativeFormat_MediaJob mj ON mj.MediaJobID = smrd.FKMediaJobId\r\n            LEFT JOIN AlternativeFormat_ProofOfPurchaseInfo pop ON pop.FK_MediaContentUniqueID = mc.MediaContentID AND pop.StudentPersonID = smr.RequestMadeFromStudentNo\r\n            LEFT JOIN people wpop ON wpop.PersonID=pop.[WhoAcceptedProofOfPurchase]\r\n            LEFT JOIN peoplemingroup wpopg ON wpopg.PersonID=pop.[WhoAcceptedProofOfPurchase] WHERE smrd.[FKMediaJobId] = @mediajobid", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					while (dataReader.Read())
					{
						MediaContentRequestedInfo studentMediaRequestInfoFromReader = this.GetStudentMediaRequestInfoFromReader(dataReader, this.OpContext, null);
						list.Add(studentMediaRequestInfoFromReader);
					}
				}
			}
			return list;
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x00075F28 File Offset: 0x00074128
		public ProofOfPurchaseInfo DownloadProofOfPurchase(Guid mediaContentUniqueId, int studentPersonId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@mediacontentuniqueid", DbType.Guid, mediaContentUniqueId),
				databaseLayer.GetParameter("@studentpersonid", DbType.Int32, studentPersonId)
			};
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT pop.[ProofOfPurchaseID],pop.[ProofOfPurchaseNote],pop.[WhenWasAccepted],pop.[FK_MediaContentUniqueID],pop.[StudentPersonID],pop.[Filename],pop.[Extension],\r\n                     p.PersonID, p.firstname, p.middlename,p.lastname, p.student_no, pg.mingroupid\r\n\t        FROM [AlternativeFormat_ProofOfPurchaseInfo] pop\r\n            LEFT JOIN People p ON p.PersonID=pop.WhoAcceptedProofOfPurchase\r\n            LEFT JOIN peoplemingroup pg ON pg.PersonID=pop.WhoAcceptedProofOfPurchase where pop.IsActive = 1 and pop.FK_MediaContentUniqueID=@mediacontentuniqueid AND pop.StudentPersonID=@studentpersonid", parameters))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					return this.GetProofOfPurchaseFromReader(dataReader, null);
				}
			}
			return null;
		}

		// Token: 0x06000B23 RID: 2851 RVA: 0x00075FD4 File Offset: 0x000741D4
		public ProofOfPurchaseInfo DownloadProofOfPurchase(int proofOfPurchaseId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter parameter = databaseLayer.GetParameter("@proofofpurchaseid", DbType.Int32, proofOfPurchaseId);
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT pop.[ProofOfPurchaseID],pop.[ProofOfPurchaseNote],pop.[WhenWasAccepted],pop.[FK_MediaContentUniqueID],pop.[StudentPersonID],pop.[Filename],pop.[Extension],\r\n                     p.PersonID, p.firstname, p.middlename,p.lastname, p.student_no, pg.mingroupid\r\n\t        FROM [AlternativeFormat_ProofOfPurchaseInfo] pop\r\n            LEFT JOIN People p ON p.PersonID=pop.WhoAcceptedProofOfPurchase\r\n            LEFT JOIN peoplemingroup pg ON pg.PersonID=pop.WhoAcceptedProofOfPurchase where pop.IsActive = 1 and pop.ProofOfPurchaseID=@proofofpurchaseid", new DbParameter[]
			{
				parameter
			}))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					return this.GetProofOfPurchaseFromReader(dataReader, null);
				}
			}
			return null;
		}

		// Token: 0x06000B24 RID: 2852 RVA: 0x00076068 File Offset: 0x00074268
		[DebuggerStepThrough]
		public Task<ProofOfPurchaseInfo> DownloadProofOfPurchaseAsync(Guid mediaContentUniqueId, int studentPersonId)
		{
			StudentMediaRequestDAO.<DownloadProofOfPurchaseAsync>d__8 <DownloadProofOfPurchaseAsync>d__ = new StudentMediaRequestDAO.<DownloadProofOfPurchaseAsync>d__8();
			<DownloadProofOfPurchaseAsync>d__.<>t__builder = AsyncTaskMethodBuilder<ProofOfPurchaseInfo>.Create();
			<DownloadProofOfPurchaseAsync>d__.<>4__this = this;
			<DownloadProofOfPurchaseAsync>d__.mediaContentUniqueId = mediaContentUniqueId;
			<DownloadProofOfPurchaseAsync>d__.studentPersonId = studentPersonId;
			<DownloadProofOfPurchaseAsync>d__.<>1__state = -1;
			<DownloadProofOfPurchaseAsync>d__.<>t__builder.Start<StudentMediaRequestDAO.<DownloadProofOfPurchaseAsync>d__8>(ref <DownloadProofOfPurchaseAsync>d__);
			return <DownloadProofOfPurchaseAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x000760BC File Offset: 0x000742BC
		[DebuggerStepThrough]
		public Task<ProofOfPurchaseInfo> DownloadProofOfPurchaseAsync(int proofOfPurchaseId)
		{
			StudentMediaRequestDAO.<DownloadProofOfPurchaseAsync>d__9 <DownloadProofOfPurchaseAsync>d__ = new StudentMediaRequestDAO.<DownloadProofOfPurchaseAsync>d__9();
			<DownloadProofOfPurchaseAsync>d__.<>t__builder = AsyncTaskMethodBuilder<ProofOfPurchaseInfo>.Create();
			<DownloadProofOfPurchaseAsync>d__.<>4__this = this;
			<DownloadProofOfPurchaseAsync>d__.proofOfPurchaseId = proofOfPurchaseId;
			<DownloadProofOfPurchaseAsync>d__.<>1__state = -1;
			<DownloadProofOfPurchaseAsync>d__.<>t__builder.Start<StudentMediaRequestDAO.<DownloadProofOfPurchaseAsync>d__9>(ref <DownloadProofOfPurchaseAsync>d__);
			return <DownloadProofOfPurchaseAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x00076108 File Offset: 0x00074308
		public int UploadProofOfPurchase(ProofOfPurchaseInfo proofOfPurchase)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] array = new DbParameter[8];
			array[0] = databaseLayer.GetOutputParameter("@proofofpurchaseid", DbType.Int32, 0);
			array[1] = databaseLayer.GetParameter("@proofofpurchasenote", DbType.String, string.IsNullOrEmpty(proofOfPurchase.Notes) ? string.Empty : proofOfPurchase.Notes);
			int num = 2;
			DatabaseLayer databaseLayer2 = databaseLayer;
			string pName = "@whoacceptedproofofpurchase";
			DbType pType = DbType.Int32;
			PersonBase whoAcceptedProofOfPurchase = proofOfPurchase.WhoAcceptedProofOfPurchase;
			array[num] = databaseLayer2.GetParameter(pName, pType, (whoAcceptedProofOfPurchase != null) ? whoAcceptedProofOfPurchase.PersonId : 0);
			array[3] = databaseLayer.GetParameter("@whenwasaccepted", DbType.DateTime, (proofOfPurchase.WhenWasAccepted != null) ? proofOfPurchase.WhenWasAccepted : DBNull.Value);
			array[4] = databaseLayer.GetParameter("@fkmediacontentuniqueid", DbType.Guid, proofOfPurchase.MediaContentUniqueId);
			array[5] = databaseLayer.GetParameter("@studentpersonid", DbType.Int32, proofOfPurchase.StudentPersonId);
			array[6] = databaseLayer.GetParameter("@filename", DbType.String, string.IsNullOrEmpty(proofOfPurchase.Filename) ? string.Empty : proofOfPurchase.Filename);
			array[7] = databaseLayer.GetParameter("@extension", DbType.String, string.IsNullOrEmpty(proofOfPurchase.Extension) ? string.Empty : proofOfPurchase.Extension);
			DbParameter[] array2 = array;
			databaseLayer.ExecuteStoredProcedure("sp_AlternateFormat_UploadProofOfPurchase", array2);
			proofOfPurchase.ProofOfPurchaseId = ((array2[0].Value is DBNull) ? 0 : ((int)array2[0].Value));
			bool flag = proofOfPurchase.ProofOfPurchaseId > 0 && proofOfPurchase.ProofOfPurchaseReceipt != null;
			if (flag)
			{
				try
				{
					this.AddProofOfPurchaseReceipt(proofOfPurchase.ProofOfPurchaseId, proofOfPurchase.ProofOfPurchaseReceipt);
				}
				catch (Exception ex)
				{
					this.RemoveProofOfPurchaseReceipt(proofOfPurchase.ProofOfPurchaseId);
					CWLogger.Logger.ErrorException("StudentMediaRequestDAO::UploadProofOfPurchase: " + ex.ToString(), ex);
					throw;
				}
			}
			return proofOfPurchase.ProofOfPurchaseId;
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x00076304 File Offset: 0x00074504
		[DebuggerStepThrough]
		public Task<int> UploadProofOfPurchaseAsync(ProofOfPurchaseInfo proofOfPurchase)
		{
			StudentMediaRequestDAO.<UploadProofOfPurchaseAsync>d__11 <UploadProofOfPurchaseAsync>d__ = new StudentMediaRequestDAO.<UploadProofOfPurchaseAsync>d__11();
			<UploadProofOfPurchaseAsync>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<UploadProofOfPurchaseAsync>d__.<>4__this = this;
			<UploadProofOfPurchaseAsync>d__.proofOfPurchase = proofOfPurchase;
			<UploadProofOfPurchaseAsync>d__.<>1__state = -1;
			<UploadProofOfPurchaseAsync>d__.<>t__builder.Start<StudentMediaRequestDAO.<UploadProofOfPurchaseAsync>d__11>(ref <UploadProofOfPurchaseAsync>d__);
			return <UploadProofOfPurchaseAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x00076350 File Offset: 0x00074550
		public void UpdateProofOfPurchase(ProofOfPurchaseInfo proofOfPurchase)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] array = new DbParameter[6];
			array[0] = databaseLayer.GetParameter("@proofofpurchaseid", DbType.Int32, proofOfPurchase.ProofOfPurchaseId);
			array[1] = databaseLayer.GetParameter("@proofofpurchasenote", DbType.String, string.IsNullOrEmpty(proofOfPurchase.Notes) ? string.Empty : proofOfPurchase.Notes);
			int num = 2;
			DatabaseLayer databaseLayer2 = databaseLayer;
			string pName = "@whoacceptedproofofpurchase";
			DbType pType = DbType.Int32;
			PersonBase whoAcceptedProofOfPurchase = proofOfPurchase.WhoAcceptedProofOfPurchase;
			array[num] = databaseLayer2.GetParameter(pName, pType, (whoAcceptedProofOfPurchase != null) ? whoAcceptedProofOfPurchase.PersonId : 0);
			array[3] = databaseLayer.GetParameter("@whenwasaccepted", DbType.DateTime, (proofOfPurchase.WhenWasAccepted != null) ? proofOfPurchase.WhenWasAccepted : DBNull.Value);
			array[4] = databaseLayer.GetParameter("@filename", DbType.String, string.IsNullOrEmpty(proofOfPurchase.Filename) ? string.Empty : proofOfPurchase.Filename);
			array[5] = databaseLayer.GetParameter("@extension", DbType.String, string.IsNullOrEmpty(proofOfPurchase.Extension) ? string.Empty : proofOfPurchase.Extension);
			DbParameter[] parameters = array;
			databaseLayer.ExecuteNonQuery("update [AlternativeFormat_ProofOfPurchaseInfo]\r\n\t\t            set \r\n\t\t\t               [ProofOfPurchaseNote]=@proofofpurchasenote\r\n\t\t\t              ,[WhoAcceptedProofOfPurchase]=@whoacceptedproofofpurchase\r\n                          ,[WhenWasAccepted]=@whenwasaccepted\r\n                          ,IsActive = 1\r\n\t\t            where [ProofOfPurchaseID]=@proofofpurchaseid", parameters);
			bool flag = proofOfPurchase.ProofOfPurchaseReceipt != null;
			if (flag)
			{
				this.AddProofOfPurchaseReceipt(proofOfPurchase.ProofOfPurchaseId, proofOfPurchase.ProofOfPurchaseReceipt);
			}
			else
			{
				this.RemoveProofOfPurchaseReceipt(proofOfPurchase.ProofOfPurchaseId);
			}
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x000764AC File Offset: 0x000746AC
		public void DeleteProofOfPurchase(int proofOfPurchaseId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@proofofpurchaseid", DbType.Int32, proofOfPurchaseId),
				databaseLayer.GetParameter("@whoacceptedproofofpurchase", DbType.Int32, this.OpContext.WhoAmI)
			};
			databaseLayer.ExecuteNonQuery("update [AlternativeFormat_ProofOfPurchaseInfo]\r\n\t\t      set IsActive=0\r\n                 ,WhoAcceptedProofOfPurchase=@whoacceptedproofofpurchase\r\n                 ,WhenWasAccepted=GetDate()\r\n\t\t      where [ProofOfPurchaseID]=@proofofpurchaseid", parameters);
			this.RemoveProofOfPurchaseReceipt(proofOfPurchaseId);
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x00076528 File Offset: 0x00074728
		[DebuggerStepThrough]
		public Task DeleteProofOfPurchaseAsync(int proofOfPurchaseId)
		{
			StudentMediaRequestDAO.<DeleteProofOfPurchaseAsync>d__14 <DeleteProofOfPurchaseAsync>d__ = new StudentMediaRequestDAO.<DeleteProofOfPurchaseAsync>d__14();
			<DeleteProofOfPurchaseAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<DeleteProofOfPurchaseAsync>d__.<>4__this = this;
			<DeleteProofOfPurchaseAsync>d__.proofOfPurchaseId = proofOfPurchaseId;
			<DeleteProofOfPurchaseAsync>d__.<>1__state = -1;
			<DeleteProofOfPurchaseAsync>d__.<>t__builder.Start<StudentMediaRequestDAO.<DeleteProofOfPurchaseAsync>d__14>(ref <DeleteProofOfPurchaseAsync>d__);
			return <DeleteProofOfPurchaseAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x00076574 File Offset: 0x00074774
		public void UpdateAvailableDownloadingTime(MediaContentRequestedInfo requestedInfo)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@mediarequestinfoid", DbType.Int32, requestedInfo.MediaContentRequestedInfoID),
				databaseLayer.GetParameter("@availablestarttime", DbType.DateTime, requestedInfo.AvailableStartTime),
				databaseLayer.GetParameter("@availableendtime", DbType.DateTime, requestedInfo.AvailableEndTime)
			};
			databaseLayer.ExecuteNonQuery("UPDATE [AlternativeFormat_StudentMediaRequestDetail]\r\n            SET  [AvailableStartTime] = @availablestarttime\r\n                ,[AvailableEndTime] = @availableendtime\r\n            WHERE StudentMediaRequestDetailId = @mediarequestinfoid", parameters);
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x00076600 File Offset: 0x00074800
		public bool IsProofOfPurchaseAvailable(Guid mediaContentUniqueId, int studentPersonId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@mediacontentuniqueid", DbType.Guid, mediaContentUniqueId),
				databaseLayer.GetParameter("@studentpersonid", DbType.Int32, studentPersonId)
			};
			object obj = databaseLayer.ExecuteScalar("SELECT ProoFOfPurchaseID FROM [AlternativeFormat_ProofOfPurchaseInfo] WHERE pop.IsActive = 1 and pop.FK_MediaContentUniqueID=@mediacontentuniqueid AND pop.StudentPersonID=@studentpersonid", parameters);
			return obj != null && !Convert.IsDBNull(obj) && (int)obj > 0;
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x00076684 File Offset: 0x00074884
		public StudentMediaRequest CreateStudentMediaRequest(StudentMediaRequest studentMediaRequest)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			try
			{
				DbParameter[] array = new DbParameter[]
				{
					databaseLayer.GetOutputParameter("@studentmediarequestid", DbType.Int32, 0),
					databaseLayer.GetParameter("@studentid", DbType.Int32, studentMediaRequest.RequestMadeFromStudent.PersonId),
					databaseLayer.GetParameter("@createddatetime", DbType.DateTime, studentMediaRequest.CreatedDatetime),
					databaseLayer.GetParameter("@campusid", DbType.Int32, (studentMediaRequest.Campus == null) ? DBNull.Value : studentMediaRequest.Campus.CampusId)
				};
				databaseLayer.ExecuteNonQuery("INSERT INTO [AlternativeFormat_StudentMediaRequest]\r\n           ([RequestMadeFromStudentNo]\r\n           ,[CreatedDateTime]\r\n           ,[CampusId])\r\n            VALUES\r\n           (@studentid\r\n           ,@createddatetime\r\n           ,@campusid)\r\n\r\n          set @studentmediarequestid = SCOPE_IDENTITY()", array);
				studentMediaRequest.StudentMediaRequestId = ((array[0].Value is DBNull) ? 0 : ((int)array[0].Value));
			}
			catch (Exception ex)
			{
				studentMediaRequest.StudentMediaRequestId = 0;
				CWLogger.Logger.ErrorException("StudentMediaRequestDAO::CreateStudentMediaRequest: " + ex.ToString(), ex);
				throw new DataAccessLayerException("Exception when a student media request is been created", ex);
			}
			return studentMediaRequest;
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x000767A8 File Offset: 0x000749A8
		public void UpdateStudentMediaRequest(StudentMediaRequest studentMediaRequest)
		{
			try
			{
				eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
				OperationContext opContext = this.OpContext;
				DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
				DbParameter[] parameters = new DbParameter[]
				{
					databaseLayer.GetParameter("@studentmediarequestid", DbType.Int32, studentMediaRequest.StudentMediaRequestId),
					databaseLayer.GetParameter("@studentid", DbType.Int32, studentMediaRequest.RequestMadeFromStudent.PersonId),
					databaseLayer.GetParameter("@createddatetime", DbType.DateTime, studentMediaRequest.CreatedDatetime),
					databaseLayer.GetParameter("@completeddatetime", DbType.DateTime, (studentMediaRequest.CompletedDateTime != null) ? studentMediaRequest.CompletedDateTime.Value : DBNull.Value),
					databaseLayer.GetParameter("@campusid", DbType.Int32, (studentMediaRequest.Campus == null) ? DBNull.Value : studentMediaRequest.Campus.CampusId)
				};
				databaseLayer.ExecuteNonQuery("UPDATE [AlternativeFormat_StudentMediaRequest]\r\n                SET [RequestMadeFromStudentNo] = @studentid\r\n                    ,[CreatedDateTime] = @createddatetime\r\n                    ,[CompletedDateTime] = @completeddatetime\r\n                    ,[CampusId] = @campusid\r\n            WHERE StudentMediaRequestID = @studentmediarequestid", parameters);
			}
			catch (Exception innerEx)
			{
				throw new DataAccessLayerException("Exception when a student media request is been updated", innerEx);
			}
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x000768C4 File Offset: 0x00074AC4
		public StudentMediaRequest LoadStudentMediaRequestById(int studentMediaRequestId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			StudentMediaRequest studentMediaRequest = null;
			DbParameter parameter = databaseLayer.GetParameter("@studentmediarequestid", DbType.Int32, studentMediaRequestId);
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT  smr.[StudentMediaRequestID], smr.[RequestMadeFromStudentNo], smr.[CreatedDateTime], smr.[CompletedDateTime], \r\n                    smr.[CampusId],clk.CampusName,clk.CampusDescription,clk.[IsActive] as CampusIsActive,\r\n                    smrd.[IsApproved], smrd.[FKStudentMediaRequestID], smrd.[IsCompleted], smrd.[IsCancelled], smrd.[FKMediaJobId], mj.JobTitle,\r\n\t\t            smrd.[StudentMediaRequestDetailId], smrd.[FKMediaContentID], smrd.[Status], \r\n                    case pop.IsActive\r\n\t\t\t\t\t\twhen 1 then pop.ProofOfPurchaseID\r\n\t\t\t\t\t\telse NULL\r\n\t\t\t\t\tend as ProofOfPurchaseID,\r\n\t\t\t\t\tpop.IsActive, pop.ProofOfPurchaseNote, pop.WhoAcceptedProofOfPurchase, pop.WhenWasAccepted, pop.StudentPersonID, pop.[Filename], pop.Extension,\r\n                    smrd.[FKMediaContentPerFormatId], smrd.[AvailableStartTime], smrd.[AvailableEndTime], smrd.[StudentPreferredFormat],\r\n                    smrd.CreatedDateTime as [MediaRequestDetailCreatedDateTime], smrd.CompletedDateTime as [MediaRequestDetailCompletedDateTime], smrd.CompletionNotes,\r\n\t\t            mcf.[MediaContentFormat], mc.[ShortTitle], mc.[ISBN], mc.Authors, mc.[Edition], mc.[Summary], mc.[ProofOfPurchaseRequired],\r\n                    p.PersonID, p.firstname, p.lastname, p.middlename, p.student_no, pg.mingroupid,\r\n                    pop.WhoAcceptedProofOfPurchase as wpoppersonid, wpop.firstname as wpopfirstname, wpop.lastname as wpoplastname, wpop.middlename as wpopmiddlename, wpop.student_no as wpopstudent_no, wpopg.mingroupid as wpopmingroupid\r\n            FROM [AlternativeFormat_StudentMediaRequestDetail] as smrd\r\n            INNER JOIN  [AlternativeFormat_StudentMediaRequest] as smr ON smrd.[FKStudentMediaRequestID] = smr.[StudentMediaRequestID]\r\n            LEFT JOIN [AlternativeFormat_MediaContent_x_MediaContentFormat] as mcf ON mcf.[MediaContentPerFormatID] = smrd.[FKMediaContentPerFormatId] \r\n            LEFT JOIN [AlternativeFormat_MediaContent] as mc ON mc.[MediaContentID] = smrd.[FKMediaContentID] \r\n            LEFT JOIN people p ON p.PersonID=smr.[RequestMadeFromStudentNo]\r\n            LEFT JOIN peoplemingroup pg ON pg.PersonID=smr.[RequestMadeFromStudentNo]\r\n            LEFT JOIN CampusLookup clk ON clk.CampusId=smr.CampusId\r\n\t\t\tLEFT JOIN AlternativeFormat_MediaJob mj ON mj.MediaJobID = smrd.FKMediaJobId\r\n            LEFT JOIN AlternativeFormat_ProofOfPurchaseInfo pop ON pop.FK_MediaContentUniqueID = mc.MediaContentID AND pop.StudentPersonID = smr.RequestMadeFromStudentNo\r\n            LEFT JOIN people wpop ON wpop.PersonID=pop.[WhoAcceptedProofOfPurchase]\r\n            LEFT JOIN peoplemingroup wpopg ON wpopg.PersonID=pop.[WhoAcceptedProofOfPurchase] WHERE smr.[StudentMediaRequestID] = @studentmediarequestid", new DbParameter[]
			{
				parameter
			}))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					while (dataReader.Read())
					{
						bool flag2 = studentMediaRequest == null;
						if (flag2)
						{
							IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
							studentMediaRequest = this.GetStudentMediaRequestFromReader(dataReader, batchDecryptor);
						}
						MediaContentRequestedInfo studentMediaRequestInfoFromReader = this.GetStudentMediaRequestInfoFromReader(dataReader, this.OpContext, studentMediaRequest.RequestMadeFromStudent);
						bool flag3 = studentMediaRequestInfoFromReader != null;
						if (flag3)
						{
							studentMediaRequest.ContentRequestedList.Add(studentMediaRequestInfoFromReader);
						}
					}
				}
			}
			return studentMediaRequest;
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x000769AC File Offset: 0x00074BAC
		public IList<MediaContentRequestedInfo> LoadStudentMediaRequestByStatus(MediaRequestStatus status)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter parameter = databaseLayer.GetParameter("@status", DbType.String, status.ToString());
			List<MediaContentRequestedInfo> list = new List<MediaContentRequestedInfo>();
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT  smr.[StudentMediaRequestID], smr.[RequestMadeFromStudentNo], smr.[CreatedDateTime], smr.[CompletedDateTime], \r\n                    smr.[CampusId],clk.CampusName,clk.CampusDescription,clk.[IsActive] as CampusIsActive,\r\n                    smrd.[IsApproved], smrd.[FKStudentMediaRequestID], smrd.[IsCompleted], smrd.[IsCancelled], smrd.[FKMediaJobId], mj.JobTitle,\r\n\t\t            smrd.[StudentMediaRequestDetailId], smrd.[FKMediaContentID], smrd.[Status], \r\n                    case pop.IsActive\r\n\t\t\t\t\t\twhen 1 then pop.ProofOfPurchaseID\r\n\t\t\t\t\t\telse NULL\r\n\t\t\t\t\tend as ProofOfPurchaseID,\r\n\t\t\t\t\tpop.IsActive, pop.ProofOfPurchaseNote, pop.WhoAcceptedProofOfPurchase, pop.WhenWasAccepted, pop.StudentPersonID, pop.[Filename], pop.Extension,\r\n                    smrd.[FKMediaContentPerFormatId], smrd.[AvailableStartTime], smrd.[AvailableEndTime], smrd.[StudentPreferredFormat],\r\n                    smrd.CreatedDateTime as [MediaRequestDetailCreatedDateTime], smrd.CompletedDateTime as [MediaRequestDetailCompletedDateTime], smrd.CompletionNotes,\r\n\t\t            mcf.[MediaContentFormat], mc.[ShortTitle], mc.[ISBN], mc.Authors, mc.[Edition], mc.[Summary], mc.[ProofOfPurchaseRequired],\r\n                    p.PersonID, p.firstname, p.lastname, p.middlename, p.student_no, pg.mingroupid,\r\n                    pop.WhoAcceptedProofOfPurchase as wpoppersonid, wpop.firstname as wpopfirstname, wpop.lastname as wpoplastname, wpop.middlename as wpopmiddlename, wpop.student_no as wpopstudent_no, wpopg.mingroupid as wpopmingroupid\r\n            FROM [AlternativeFormat_StudentMediaRequestDetail] as smrd\r\n            INNER JOIN  [AlternativeFormat_StudentMediaRequest] as smr ON smrd.[FKStudentMediaRequestID] = smr.[StudentMediaRequestID]\r\n            LEFT JOIN [AlternativeFormat_MediaContent_x_MediaContentFormat] as mcf ON mcf.[MediaContentPerFormatID] = smrd.[FKMediaContentPerFormatId] \r\n            LEFT JOIN [AlternativeFormat_MediaContent] as mc ON mc.[MediaContentID] = smrd.[FKMediaContentID] \r\n            LEFT JOIN people p ON p.PersonID=smr.[RequestMadeFromStudentNo]\r\n            LEFT JOIN peoplemingroup pg ON pg.PersonID=smr.[RequestMadeFromStudentNo]\r\n            LEFT JOIN CampusLookup clk ON clk.CampusId=smr.CampusId\r\n\t\t\tLEFT JOIN AlternativeFormat_MediaJob mj ON mj.MediaJobID = smrd.FKMediaJobId\r\n            LEFT JOIN AlternativeFormat_ProofOfPurchaseInfo pop ON pop.FK_MediaContentUniqueID = mc.MediaContentID AND pop.StudentPersonID = smr.RequestMadeFromStudentNo\r\n            LEFT JOIN people wpop ON wpop.PersonID=pop.[WhoAcceptedProofOfPurchase]\r\n            LEFT JOIN peoplemingroup wpopg ON wpopg.PersonID=pop.[WhoAcceptedProofOfPurchase] WHERE smrd.[Status] = @status", new DbParameter[]
			{
				parameter
			}))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					while (dataReader.Read())
					{
						MediaContentRequestedInfo studentMediaRequestInfoFromReader = this.GetStudentMediaRequestInfoFromReader(dataReader, this.OpContext, null);
						list.Add(studentMediaRequestInfoFromReader);
					}
				}
			}
			return list;
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x00076A68 File Offset: 0x00074C68
		public IList<MediaContentRequestedInfo> LoadAllToBeApprovedMediaRequest(int campusId = 0)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@approved", DbType.Boolean, false),
				databaseLayer.GetParameter("@campusid", DbType.Int32, campusId)
			};
			List<MediaContentRequestedInfo> list = new List<MediaContentRequestedInfo>();
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT  smr.[StudentMediaRequestID], smr.[RequestMadeFromStudentNo], smr.[CreatedDateTime], smr.[CompletedDateTime], \r\n                    smr.[CampusId],clk.CampusName,clk.CampusDescription,clk.[IsActive] as CampusIsActive,\r\n                    smrd.[IsApproved], smrd.[FKStudentMediaRequestID], smrd.[IsCompleted], smrd.[IsCancelled], smrd.[FKMediaJobId], mj.JobTitle,\r\n\t\t            smrd.[StudentMediaRequestDetailId], smrd.[FKMediaContentID], smrd.[Status], \r\n                    case pop.IsActive\r\n\t\t\t\t\t\twhen 1 then pop.ProofOfPurchaseID\r\n\t\t\t\t\t\telse NULL\r\n\t\t\t\t\tend as ProofOfPurchaseID,\r\n\t\t\t\t\tpop.IsActive, pop.ProofOfPurchaseNote, pop.WhoAcceptedProofOfPurchase, pop.WhenWasAccepted, pop.StudentPersonID, pop.[Filename], pop.Extension,\r\n                    smrd.[FKMediaContentPerFormatId], smrd.[AvailableStartTime], smrd.[AvailableEndTime], smrd.[StudentPreferredFormat],\r\n                    smrd.CreatedDateTime as [MediaRequestDetailCreatedDateTime], smrd.CompletedDateTime as [MediaRequestDetailCompletedDateTime], smrd.CompletionNotes,\r\n\t\t            mcf.[MediaContentFormat], mc.[ShortTitle], mc.[ISBN], mc.Authors, mc.[Edition], mc.[Summary], mc.[ProofOfPurchaseRequired],\r\n                    p.PersonID, p.firstname, p.lastname, p.middlename, p.student_no, pg.mingroupid,\r\n                    pop.WhoAcceptedProofOfPurchase as wpoppersonid, wpop.firstname as wpopfirstname, wpop.lastname as wpoplastname, wpop.middlename as wpopmiddlename, wpop.student_no as wpopstudent_no, wpopg.mingroupid as wpopmingroupid\r\n            FROM [AlternativeFormat_StudentMediaRequestDetail] as smrd\r\n            INNER JOIN  [AlternativeFormat_StudentMediaRequest] as smr ON smrd.[FKStudentMediaRequestID] = smr.[StudentMediaRequestID]\r\n            LEFT JOIN [AlternativeFormat_MediaContent_x_MediaContentFormat] as mcf ON mcf.[MediaContentPerFormatID] = smrd.[FKMediaContentPerFormatId] \r\n            LEFT JOIN [AlternativeFormat_MediaContent] as mc ON mc.[MediaContentID] = smrd.[FKMediaContentID] \r\n            LEFT JOIN people p ON p.PersonID=smr.[RequestMadeFromStudentNo]\r\n            LEFT JOIN peoplemingroup pg ON pg.PersonID=smr.[RequestMadeFromStudentNo]\r\n            LEFT JOIN CampusLookup clk ON clk.CampusId=smr.CampusId\r\n\t\t\tLEFT JOIN AlternativeFormat_MediaJob mj ON mj.MediaJobID = smrd.FKMediaJobId\r\n            LEFT JOIN AlternativeFormat_ProofOfPurchaseInfo pop ON pop.FK_MediaContentUniqueID = mc.MediaContentID AND pop.StudentPersonID = smr.RequestMadeFromStudentNo\r\n            LEFT JOIN people wpop ON wpop.PersonID=pop.[WhoAcceptedProofOfPurchase]\r\n            LEFT JOIN peoplemingroup wpopg ON wpopg.PersonID=pop.[WhoAcceptedProofOfPurchase] WHERE smrd.[IsApproved] = @approved AND (@campusid = 0 OR smr.[CampusId] = @campusid)", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					while (dataReader.Read())
					{
						MediaContentRequestedInfo studentMediaRequestInfoFromReader = this.GetStudentMediaRequestInfoFromReader(dataReader, this.OpContext, null);
						list.Add(studentMediaRequestInfoFromReader);
					}
				}
			}
			return list;
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x00076B30 File Offset: 0x00074D30
		public IList<MediaContentRequestedInfo> LoadAllToBeApprovedMediaRequestByStudent(int studentId, int campusId = 0)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@studentid", DbType.Int32, studentId),
				databaseLayer.GetParameter("@approved", DbType.Boolean, false),
				databaseLayer.GetParameter("@campusid", DbType.Int32, campusId)
			};
			List<MediaContentRequestedInfo> list = new List<MediaContentRequestedInfo>();
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT  smr.[StudentMediaRequestID], smr.[RequestMadeFromStudentNo], smr.[CreatedDateTime], smr.[CompletedDateTime], \r\n                    smr.[CampusId],clk.CampusName,clk.CampusDescription,clk.[IsActive] as CampusIsActive,\r\n                    smrd.[IsApproved], smrd.[FKStudentMediaRequestID], smrd.[IsCompleted], smrd.[IsCancelled], smrd.[FKMediaJobId], mj.JobTitle,\r\n\t\t            smrd.[StudentMediaRequestDetailId], smrd.[FKMediaContentID], smrd.[Status], \r\n                    case pop.IsActive\r\n\t\t\t\t\t\twhen 1 then pop.ProofOfPurchaseID\r\n\t\t\t\t\t\telse NULL\r\n\t\t\t\t\tend as ProofOfPurchaseID,\r\n\t\t\t\t\tpop.IsActive, pop.ProofOfPurchaseNote, pop.WhoAcceptedProofOfPurchase, pop.WhenWasAccepted, pop.StudentPersonID, pop.[Filename], pop.Extension,\r\n                    smrd.[FKMediaContentPerFormatId], smrd.[AvailableStartTime], smrd.[AvailableEndTime], smrd.[StudentPreferredFormat],\r\n                    smrd.CreatedDateTime as [MediaRequestDetailCreatedDateTime], smrd.CompletedDateTime as [MediaRequestDetailCompletedDateTime], smrd.CompletionNotes,\r\n\t\t            mcf.[MediaContentFormat], mc.[ShortTitle], mc.[ISBN], mc.Authors, mc.[Edition], mc.[Summary], mc.[ProofOfPurchaseRequired],\r\n                    p.PersonID, p.firstname, p.lastname, p.middlename, p.student_no, pg.mingroupid,\r\n                    pop.WhoAcceptedProofOfPurchase as wpoppersonid, wpop.firstname as wpopfirstname, wpop.lastname as wpoplastname, wpop.middlename as wpopmiddlename, wpop.student_no as wpopstudent_no, wpopg.mingroupid as wpopmingroupid\r\n            FROM [AlternativeFormat_StudentMediaRequestDetail] as smrd\r\n            INNER JOIN  [AlternativeFormat_StudentMediaRequest] as smr ON smrd.[FKStudentMediaRequestID] = smr.[StudentMediaRequestID]\r\n            LEFT JOIN [AlternativeFormat_MediaContent_x_MediaContentFormat] as mcf ON mcf.[MediaContentPerFormatID] = smrd.[FKMediaContentPerFormatId] \r\n            LEFT JOIN [AlternativeFormat_MediaContent] as mc ON mc.[MediaContentID] = smrd.[FKMediaContentID] \r\n            LEFT JOIN people p ON p.PersonID=smr.[RequestMadeFromStudentNo]\r\n            LEFT JOIN peoplemingroup pg ON pg.PersonID=smr.[RequestMadeFromStudentNo]\r\n            LEFT JOIN CampusLookup clk ON clk.CampusId=smr.CampusId\r\n\t\t\tLEFT JOIN AlternativeFormat_MediaJob mj ON mj.MediaJobID = smrd.FKMediaJobId\r\n            LEFT JOIN AlternativeFormat_ProofOfPurchaseInfo pop ON pop.FK_MediaContentUniqueID = mc.MediaContentID AND pop.StudentPersonID = smr.RequestMadeFromStudentNo\r\n            LEFT JOIN people wpop ON wpop.PersonID=pop.[WhoAcceptedProofOfPurchase]\r\n            LEFT JOIN peoplemingroup wpopg ON wpopg.PersonID=pop.[WhoAcceptedProofOfPurchase] WHERE smrd.[IsApproved] = @approved AND smr.[RequestMadeFromStudentNo] = @studentid AND (@campusid = 0 OR smr.[CampusId] = @campusid)", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					while (dataReader.Read())
					{
						MediaContentRequestedInfo studentMediaRequestInfoFromReader = this.GetStudentMediaRequestInfoFromReader(dataReader, this.OpContext, null);
						list.Add(studentMediaRequestInfoFromReader);
					}
				}
			}
			return list;
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x00076C10 File Offset: 0x00074E10
		public IList<MediaContentRequestedInfo> LoadAllCompletedStudentMediaRequest(int campusId = 0)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@campusid", DbType.Int32, campusId)
			};
			List<MediaContentRequestedInfo> list = new List<MediaContentRequestedInfo>();
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT  smr.[StudentMediaRequestID], smr.[RequestMadeFromStudentNo], smr.[CreatedDateTime], smr.[CompletedDateTime], \r\n                    smr.[CampusId],clk.CampusName,clk.CampusDescription,clk.[IsActive] as CampusIsActive,\r\n                    smrd.[IsApproved], smrd.[FKStudentMediaRequestID], smrd.[IsCompleted], smrd.[IsCancelled], smrd.[FKMediaJobId], mj.JobTitle,\r\n\t\t            smrd.[StudentMediaRequestDetailId], smrd.[FKMediaContentID], smrd.[Status], \r\n                    case pop.IsActive\r\n\t\t\t\t\t\twhen 1 then pop.ProofOfPurchaseID\r\n\t\t\t\t\t\telse NULL\r\n\t\t\t\t\tend as ProofOfPurchaseID,\r\n\t\t\t\t\tpop.IsActive, pop.ProofOfPurchaseNote, pop.WhoAcceptedProofOfPurchase, pop.WhenWasAccepted, pop.StudentPersonID, pop.[Filename], pop.Extension,\r\n                    smrd.[FKMediaContentPerFormatId], smrd.[AvailableStartTime], smrd.[AvailableEndTime], smrd.[StudentPreferredFormat],\r\n                    smrd.CreatedDateTime as [MediaRequestDetailCreatedDateTime], smrd.CompletedDateTime as [MediaRequestDetailCompletedDateTime], smrd.CompletionNotes,\r\n\t\t            mcf.[MediaContentFormat], mc.[ShortTitle], mc.[ISBN], mc.Authors, mc.[Edition], mc.[Summary], mc.[ProofOfPurchaseRequired],\r\n                    p.PersonID, p.firstname, p.lastname, p.middlename, p.student_no, pg.mingroupid,\r\n                    pop.WhoAcceptedProofOfPurchase as wpoppersonid, wpop.firstname as wpopfirstname, wpop.lastname as wpoplastname, wpop.middlename as wpopmiddlename, wpop.student_no as wpopstudent_no, wpopg.mingroupid as wpopmingroupid\r\n            FROM [AlternativeFormat_StudentMediaRequestDetail] as smrd\r\n            INNER JOIN  [AlternativeFormat_StudentMediaRequest] as smr ON smrd.[FKStudentMediaRequestID] = smr.[StudentMediaRequestID]\r\n            LEFT JOIN [AlternativeFormat_MediaContent_x_MediaContentFormat] as mcf ON mcf.[MediaContentPerFormatID] = smrd.[FKMediaContentPerFormatId] \r\n            LEFT JOIN [AlternativeFormat_MediaContent] as mc ON mc.[MediaContentID] = smrd.[FKMediaContentID] \r\n            LEFT JOIN people p ON p.PersonID=smr.[RequestMadeFromStudentNo]\r\n            LEFT JOIN peoplemingroup pg ON pg.PersonID=smr.[RequestMadeFromStudentNo]\r\n            LEFT JOIN CampusLookup clk ON clk.CampusId=smr.CampusId\r\n\t\t\tLEFT JOIN AlternativeFormat_MediaJob mj ON mj.MediaJobID = smrd.FKMediaJobId\r\n            LEFT JOIN AlternativeFormat_ProofOfPurchaseInfo pop ON pop.FK_MediaContentUniqueID = mc.MediaContentID AND pop.StudentPersonID = smr.RequestMadeFromStudentNo\r\n            LEFT JOIN people wpop ON wpop.PersonID=pop.[WhoAcceptedProofOfPurchase]\r\n            LEFT JOIN peoplemingroup wpopg ON wpopg.PersonID=pop.[WhoAcceptedProofOfPurchase] WHERE smrd.[IsCompleted] = 1 AND (@campusid = 0 OR smr.[CampusId] = @campusid)", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					while (dataReader.Read())
					{
						MediaContentRequestedInfo studentMediaRequestInfoFromReader = this.GetStudentMediaRequestInfoFromReader(dataReader, this.OpContext, null);
						bool flag2 = studentMediaRequestInfoFromReader != null;
						if (flag2)
						{
							list.Add(studentMediaRequestInfoFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x00076CD0 File Offset: 0x00074ED0
		public IList<MediaContentRequestedInfo> LoadAllCompletedStudentMediaRequestByStudent(int studentId, int campusId = 0)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@studentid", DbType.Int32, studentId),
				databaseLayer.GetParameter("@campusid", DbType.Int32, campusId)
			};
			List<MediaContentRequestedInfo> list = new List<MediaContentRequestedInfo>();
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT  smr.[StudentMediaRequestID], smr.[RequestMadeFromStudentNo], smr.[CreatedDateTime], smr.[CompletedDateTime], \r\n                    smr.[CampusId],clk.CampusName,clk.CampusDescription,clk.[IsActive] as CampusIsActive,\r\n                    smrd.[IsApproved], smrd.[FKStudentMediaRequestID], smrd.[IsCompleted], smrd.[IsCancelled], smrd.[FKMediaJobId], mj.JobTitle,\r\n\t\t            smrd.[StudentMediaRequestDetailId], smrd.[FKMediaContentID], smrd.[Status], \r\n                    case pop.IsActive\r\n\t\t\t\t\t\twhen 1 then pop.ProofOfPurchaseID\r\n\t\t\t\t\t\telse NULL\r\n\t\t\t\t\tend as ProofOfPurchaseID,\r\n\t\t\t\t\tpop.IsActive, pop.ProofOfPurchaseNote, pop.WhoAcceptedProofOfPurchase, pop.WhenWasAccepted, pop.StudentPersonID, pop.[Filename], pop.Extension,\r\n                    smrd.[FKMediaContentPerFormatId], smrd.[AvailableStartTime], smrd.[AvailableEndTime], smrd.[StudentPreferredFormat],\r\n                    smrd.CreatedDateTime as [MediaRequestDetailCreatedDateTime], smrd.CompletedDateTime as [MediaRequestDetailCompletedDateTime], smrd.CompletionNotes,\r\n\t\t            mcf.[MediaContentFormat], mc.[ShortTitle], mc.[ISBN], mc.Authors, mc.[Edition], mc.[Summary], mc.[ProofOfPurchaseRequired],\r\n                    p.PersonID, p.firstname, p.lastname, p.middlename, p.student_no, pg.mingroupid,\r\n                    pop.WhoAcceptedProofOfPurchase as wpoppersonid, wpop.firstname as wpopfirstname, wpop.lastname as wpoplastname, wpop.middlename as wpopmiddlename, wpop.student_no as wpopstudent_no, wpopg.mingroupid as wpopmingroupid\r\n            FROM [AlternativeFormat_StudentMediaRequestDetail] as smrd\r\n            INNER JOIN  [AlternativeFormat_StudentMediaRequest] as smr ON smrd.[FKStudentMediaRequestID] = smr.[StudentMediaRequestID]\r\n            LEFT JOIN [AlternativeFormat_MediaContent_x_MediaContentFormat] as mcf ON mcf.[MediaContentPerFormatID] = smrd.[FKMediaContentPerFormatId] \r\n            LEFT JOIN [AlternativeFormat_MediaContent] as mc ON mc.[MediaContentID] = smrd.[FKMediaContentID] \r\n            LEFT JOIN people p ON p.PersonID=smr.[RequestMadeFromStudentNo]\r\n            LEFT JOIN peoplemingroup pg ON pg.PersonID=smr.[RequestMadeFromStudentNo]\r\n            LEFT JOIN CampusLookup clk ON clk.CampusId=smr.CampusId\r\n\t\t\tLEFT JOIN AlternativeFormat_MediaJob mj ON mj.MediaJobID = smrd.FKMediaJobId\r\n            LEFT JOIN AlternativeFormat_ProofOfPurchaseInfo pop ON pop.FK_MediaContentUniqueID = mc.MediaContentID AND pop.StudentPersonID = smr.RequestMadeFromStudentNo\r\n            LEFT JOIN people wpop ON wpop.PersonID=pop.[WhoAcceptedProofOfPurchase]\r\n            LEFT JOIN peoplemingroup wpopg ON wpopg.PersonID=pop.[WhoAcceptedProofOfPurchase] WHERE smrd.[IsCompleted] = 1 AND smr.[RequestMadeFromStudentNo] = @studentid AND (@campusid = 0 OR smr.[CampusId] = @campusid)", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					while (dataReader.Read())
					{
						MediaContentRequestedInfo studentMediaRequestInfoFromReader = this.GetStudentMediaRequestInfoFromReader(dataReader, this.OpContext, null);
						bool flag2 = studentMediaRequestInfoFromReader != null;
						if (flag2)
						{
							list.Add(studentMediaRequestInfoFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x00076DA4 File Offset: 0x00074FA4
		public IList<MediaContentRequestedInfo> LoadAllCompletedStudentMediaRequest(DateTime startdate, DateTime endDate, int campusId = 0)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@startdate", DbType.DateTime, startdate),
				databaseLayer.GetParameter("@enddate", DbType.DateTime, endDate),
				databaseLayer.GetParameter("@campusid", DbType.Int32, campusId)
			};
			List<MediaContentRequestedInfo> list = new List<MediaContentRequestedInfo>();
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT  smr.[StudentMediaRequestID], smr.[RequestMadeFromStudentNo], smr.[CreatedDateTime], smr.[CompletedDateTime], \r\n                    smr.[CampusId],clk.CampusName,clk.CampusDescription,clk.[IsActive] as CampusIsActive,\r\n                    smrd.[IsApproved], smrd.[FKStudentMediaRequestID], smrd.[IsCompleted], smrd.[IsCancelled], smrd.[FKMediaJobId], mj.JobTitle,\r\n\t\t            smrd.[StudentMediaRequestDetailId], smrd.[FKMediaContentID], smrd.[Status], \r\n                    case pop.IsActive\r\n\t\t\t\t\t\twhen 1 then pop.ProofOfPurchaseID\r\n\t\t\t\t\t\telse NULL\r\n\t\t\t\t\tend as ProofOfPurchaseID,\r\n\t\t\t\t\tpop.IsActive, pop.ProofOfPurchaseNote, pop.WhoAcceptedProofOfPurchase, pop.WhenWasAccepted, pop.StudentPersonID, pop.[Filename], pop.Extension,\r\n                    smrd.[FKMediaContentPerFormatId], smrd.[AvailableStartTime], smrd.[AvailableEndTime], smrd.[StudentPreferredFormat],\r\n                    smrd.CreatedDateTime as [MediaRequestDetailCreatedDateTime], smrd.CompletedDateTime as [MediaRequestDetailCompletedDateTime], smrd.CompletionNotes,\r\n\t\t            mcf.[MediaContentFormat], mc.[ShortTitle], mc.[ISBN], mc.Authors, mc.[Edition], mc.[Summary], mc.[ProofOfPurchaseRequired],\r\n                    p.PersonID, p.firstname, p.lastname, p.middlename, p.student_no, pg.mingroupid,\r\n                    pop.WhoAcceptedProofOfPurchase as wpoppersonid, wpop.firstname as wpopfirstname, wpop.lastname as wpoplastname, wpop.middlename as wpopmiddlename, wpop.student_no as wpopstudent_no, wpopg.mingroupid as wpopmingroupid\r\n            FROM [AlternativeFormat_StudentMediaRequestDetail] as smrd\r\n            INNER JOIN  [AlternativeFormat_StudentMediaRequest] as smr ON smrd.[FKStudentMediaRequestID] = smr.[StudentMediaRequestID]\r\n            LEFT JOIN [AlternativeFormat_MediaContent_x_MediaContentFormat] as mcf ON mcf.[MediaContentPerFormatID] = smrd.[FKMediaContentPerFormatId] \r\n            LEFT JOIN [AlternativeFormat_MediaContent] as mc ON mc.[MediaContentID] = smrd.[FKMediaContentID] \r\n            LEFT JOIN people p ON p.PersonID=smr.[RequestMadeFromStudentNo]\r\n            LEFT JOIN peoplemingroup pg ON pg.PersonID=smr.[RequestMadeFromStudentNo]\r\n            LEFT JOIN CampusLookup clk ON clk.CampusId=smr.CampusId\r\n\t\t\tLEFT JOIN AlternativeFormat_MediaJob mj ON mj.MediaJobID = smrd.FKMediaJobId\r\n            LEFT JOIN AlternativeFormat_ProofOfPurchaseInfo pop ON pop.FK_MediaContentUniqueID = mc.MediaContentID AND pop.StudentPersonID = smr.RequestMadeFromStudentNo\r\n            LEFT JOIN people wpop ON wpop.PersonID=pop.[WhoAcceptedProofOfPurchase]\r\n            LEFT JOIN peoplemingroup wpopg ON wpopg.PersonID=pop.[WhoAcceptedProofOfPurchase] WHERE smrd.[IsCompleted] = 1 AND (@campusid = 0 OR smr.[CampusId]=@campusid) AND (smrd.[CreatedDateTime] <= @enddate AND smrd.[CompletedDateTime] >= @startdate)", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					while (dataReader.Read())
					{
						MediaContentRequestedInfo studentMediaRequestInfoFromReader = this.GetStudentMediaRequestInfoFromReader(dataReader, this.OpContext, null);
						bool flag2 = studentMediaRequestInfoFromReader != null;
						if (flag2)
						{
							list.Add(studentMediaRequestInfoFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x00076E8C File Offset: 0x0007508C
		public IList<MediaContentRequestedInfo> LoadAllCompletedStudentMediaRequestByStudent(int studentId, DateTime startdate, DateTime endDate, int campusId = 0)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@studentid", DbType.Int32, studentId),
				databaseLayer.GetParameter("@startdate", DbType.DateTime, startdate),
				databaseLayer.GetParameter("@enddate", DbType.DateTime, endDate),
				databaseLayer.GetParameter("@campusid", DbType.Int32, campusId)
			};
			List<MediaContentRequestedInfo> list = new List<MediaContentRequestedInfo>();
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT  smr.[StudentMediaRequestID], smr.[RequestMadeFromStudentNo], smr.[CreatedDateTime], smr.[CompletedDateTime], \r\n                    smr.[CampusId],clk.CampusName,clk.CampusDescription,clk.[IsActive] as CampusIsActive,\r\n                    smrd.[IsApproved], smrd.[FKStudentMediaRequestID], smrd.[IsCompleted], smrd.[IsCancelled], smrd.[FKMediaJobId], mj.JobTitle,\r\n\t\t            smrd.[StudentMediaRequestDetailId], smrd.[FKMediaContentID], smrd.[Status], \r\n                    case pop.IsActive\r\n\t\t\t\t\t\twhen 1 then pop.ProofOfPurchaseID\r\n\t\t\t\t\t\telse NULL\r\n\t\t\t\t\tend as ProofOfPurchaseID,\r\n\t\t\t\t\tpop.IsActive, pop.ProofOfPurchaseNote, pop.WhoAcceptedProofOfPurchase, pop.WhenWasAccepted, pop.StudentPersonID, pop.[Filename], pop.Extension,\r\n                    smrd.[FKMediaContentPerFormatId], smrd.[AvailableStartTime], smrd.[AvailableEndTime], smrd.[StudentPreferredFormat],\r\n                    smrd.CreatedDateTime as [MediaRequestDetailCreatedDateTime], smrd.CompletedDateTime as [MediaRequestDetailCompletedDateTime], smrd.CompletionNotes,\r\n\t\t            mcf.[MediaContentFormat], mc.[ShortTitle], mc.[ISBN], mc.Authors, mc.[Edition], mc.[Summary], mc.[ProofOfPurchaseRequired],\r\n                    p.PersonID, p.firstname, p.lastname, p.middlename, p.student_no, pg.mingroupid,\r\n                    pop.WhoAcceptedProofOfPurchase as wpoppersonid, wpop.firstname as wpopfirstname, wpop.lastname as wpoplastname, wpop.middlename as wpopmiddlename, wpop.student_no as wpopstudent_no, wpopg.mingroupid as wpopmingroupid\r\n            FROM [AlternativeFormat_StudentMediaRequestDetail] as smrd\r\n            INNER JOIN  [AlternativeFormat_StudentMediaRequest] as smr ON smrd.[FKStudentMediaRequestID] = smr.[StudentMediaRequestID]\r\n            LEFT JOIN [AlternativeFormat_MediaContent_x_MediaContentFormat] as mcf ON mcf.[MediaContentPerFormatID] = smrd.[FKMediaContentPerFormatId] \r\n            LEFT JOIN [AlternativeFormat_MediaContent] as mc ON mc.[MediaContentID] = smrd.[FKMediaContentID] \r\n            LEFT JOIN people p ON p.PersonID=smr.[RequestMadeFromStudentNo]\r\n            LEFT JOIN peoplemingroup pg ON pg.PersonID=smr.[RequestMadeFromStudentNo]\r\n            LEFT JOIN CampusLookup clk ON clk.CampusId=smr.CampusId\r\n\t\t\tLEFT JOIN AlternativeFormat_MediaJob mj ON mj.MediaJobID = smrd.FKMediaJobId\r\n            LEFT JOIN AlternativeFormat_ProofOfPurchaseInfo pop ON pop.FK_MediaContentUniqueID = mc.MediaContentID AND pop.StudentPersonID = smr.RequestMadeFromStudentNo\r\n            LEFT JOIN people wpop ON wpop.PersonID=pop.[WhoAcceptedProofOfPurchase]\r\n            LEFT JOIN peoplemingroup wpopg ON wpopg.PersonID=pop.[WhoAcceptedProofOfPurchase] WHERE smrd.[IsCompleted] = 1 AND smr.[RequestMadeFromStudentNo] = @studentid AND (@campusid = 0 OR smr.[CampusId]=@campusid) AND (smrd.[CreatedDateTime] <= @enddate AND smrd.[CompletedDateTime] >= @startdate)", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					while (dataReader.Read())
					{
						MediaContentRequestedInfo studentMediaRequestInfoFromReader = this.GetStudentMediaRequestInfoFromReader(dataReader, this.OpContext, null);
						bool flag2 = studentMediaRequestInfoFromReader != null;
						if (flag2)
						{
							list.Add(studentMediaRequestInfoFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000B37 RID: 2871 RVA: 0x00076F8C File Offset: 0x0007518C
		public IList<MediaContentRequestedInfo> LoadAllInProgressStudentMediaRequestByStudent(int studentId, int campusId = 0)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			List<MediaContentRequestedInfo> list = new List<MediaContentRequestedInfo>();
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@studentid", DbType.Int32, studentId),
				databaseLayer.GetParameter("@campusid", DbType.Int32, campusId)
			};
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT  smr.[StudentMediaRequestID], smr.[RequestMadeFromStudentNo], smr.[CreatedDateTime], smr.[CompletedDateTime], \r\n                    smr.[CampusId],clk.CampusName,clk.CampusDescription,clk.[IsActive] as CampusIsActive,\r\n                    smrd.[IsApproved], smrd.[FKStudentMediaRequestID], smrd.[IsCompleted], smrd.[IsCancelled], smrd.[FKMediaJobId], mj.JobTitle,\r\n\t\t            smrd.[StudentMediaRequestDetailId], smrd.[FKMediaContentID], smrd.[Status], \r\n                    case pop.IsActive\r\n\t\t\t\t\t\twhen 1 then pop.ProofOfPurchaseID\r\n\t\t\t\t\t\telse NULL\r\n\t\t\t\t\tend as ProofOfPurchaseID,\r\n\t\t\t\t\tpop.IsActive, pop.ProofOfPurchaseNote, pop.WhoAcceptedProofOfPurchase, pop.WhenWasAccepted, pop.StudentPersonID, pop.[Filename], pop.Extension,\r\n                    smrd.[FKMediaContentPerFormatId], smrd.[AvailableStartTime], smrd.[AvailableEndTime], smrd.[StudentPreferredFormat],\r\n                    smrd.CreatedDateTime as [MediaRequestDetailCreatedDateTime], smrd.CompletedDateTime as [MediaRequestDetailCompletedDateTime], smrd.CompletionNotes,\r\n\t\t            mcf.[MediaContentFormat], mc.[ShortTitle], mc.[ISBN], mc.Authors, mc.[Edition], mc.[Summary], mc.[ProofOfPurchaseRequired],\r\n                    p.PersonID, p.firstname, p.lastname, p.middlename, p.student_no, pg.mingroupid,\r\n                    pop.WhoAcceptedProofOfPurchase as wpoppersonid, wpop.firstname as wpopfirstname, wpop.lastname as wpoplastname, wpop.middlename as wpopmiddlename, wpop.student_no as wpopstudent_no, wpopg.mingroupid as wpopmingroupid\r\n            FROM [AlternativeFormat_StudentMediaRequestDetail] as smrd\r\n            INNER JOIN  [AlternativeFormat_StudentMediaRequest] as smr ON smrd.[FKStudentMediaRequestID] = smr.[StudentMediaRequestID]\r\n            LEFT JOIN [AlternativeFormat_MediaContent_x_MediaContentFormat] as mcf ON mcf.[MediaContentPerFormatID] = smrd.[FKMediaContentPerFormatId] \r\n            LEFT JOIN [AlternativeFormat_MediaContent] as mc ON mc.[MediaContentID] = smrd.[FKMediaContentID] \r\n            LEFT JOIN people p ON p.PersonID=smr.[RequestMadeFromStudentNo]\r\n            LEFT JOIN peoplemingroup pg ON pg.PersonID=smr.[RequestMadeFromStudentNo]\r\n            LEFT JOIN CampusLookup clk ON clk.CampusId=smr.CampusId\r\n\t\t\tLEFT JOIN AlternativeFormat_MediaJob mj ON mj.MediaJobID = smrd.FKMediaJobId\r\n            LEFT JOIN AlternativeFormat_ProofOfPurchaseInfo pop ON pop.FK_MediaContentUniqueID = mc.MediaContentID AND pop.StudentPersonID = smr.RequestMadeFromStudentNo\r\n            LEFT JOIN people wpop ON wpop.PersonID=pop.[WhoAcceptedProofOfPurchase]\r\n            LEFT JOIN peoplemingroup wpopg ON wpopg.PersonID=pop.[WhoAcceptedProofOfPurchase] WHERE smrd.[IsCompleted] = 0 AND smrd.[IsCancelled] = 0 AND smrd.[IsApproved] = 1 AND smr.[RequestMadeFromStudentNo] = @studentid AND (@campusid=0 OR smr.[CampusId]=@campusid)", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					while (dataReader.Read())
					{
						MediaContentRequestedInfo studentMediaRequestInfoFromReader = this.GetStudentMediaRequestInfoFromReader(dataReader, this.OpContext, null);
						list.Add(studentMediaRequestInfoFromReader);
					}
				}
			}
			return list;
		}

		// Token: 0x06000B38 RID: 2872 RVA: 0x00077054 File Offset: 0x00075254
		public IList<MediaContentRequestedInfo> LoadAllInProgressStudentMediaRequest(int campusId = 0)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			List<MediaContentRequestedInfo> list = new List<MediaContentRequestedInfo>();
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@campusid", DbType.Int32, campusId)
			};
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT  smr.[StudentMediaRequestID], smr.[RequestMadeFromStudentNo], smr.[CreatedDateTime], smr.[CompletedDateTime], \r\n                    smr.[CampusId],clk.CampusName,clk.CampusDescription,clk.[IsActive] as CampusIsActive,\r\n                    smrd.[IsApproved], smrd.[FKStudentMediaRequestID], smrd.[IsCompleted], smrd.[IsCancelled], smrd.[FKMediaJobId], mj.JobTitle,\r\n\t\t            smrd.[StudentMediaRequestDetailId], smrd.[FKMediaContentID], smrd.[Status], \r\n                    case pop.IsActive\r\n\t\t\t\t\t\twhen 1 then pop.ProofOfPurchaseID\r\n\t\t\t\t\t\telse NULL\r\n\t\t\t\t\tend as ProofOfPurchaseID,\r\n\t\t\t\t\tpop.IsActive, pop.ProofOfPurchaseNote, pop.WhoAcceptedProofOfPurchase, pop.WhenWasAccepted, pop.StudentPersonID, pop.[Filename], pop.Extension,\r\n                    smrd.[FKMediaContentPerFormatId], smrd.[AvailableStartTime], smrd.[AvailableEndTime], smrd.[StudentPreferredFormat],\r\n                    smrd.CreatedDateTime as [MediaRequestDetailCreatedDateTime], smrd.CompletedDateTime as [MediaRequestDetailCompletedDateTime], smrd.CompletionNotes,\r\n\t\t            mcf.[MediaContentFormat], mc.[ShortTitle], mc.[ISBN], mc.Authors, mc.[Edition], mc.[Summary], mc.[ProofOfPurchaseRequired],\r\n                    p.PersonID, p.firstname, p.lastname, p.middlename, p.student_no, pg.mingroupid,\r\n                    pop.WhoAcceptedProofOfPurchase as wpoppersonid, wpop.firstname as wpopfirstname, wpop.lastname as wpoplastname, wpop.middlename as wpopmiddlename, wpop.student_no as wpopstudent_no, wpopg.mingroupid as wpopmingroupid\r\n            FROM [AlternativeFormat_StudentMediaRequestDetail] as smrd\r\n            INNER JOIN  [AlternativeFormat_StudentMediaRequest] as smr ON smrd.[FKStudentMediaRequestID] = smr.[StudentMediaRequestID]\r\n            LEFT JOIN [AlternativeFormat_MediaContent_x_MediaContentFormat] as mcf ON mcf.[MediaContentPerFormatID] = smrd.[FKMediaContentPerFormatId] \r\n            LEFT JOIN [AlternativeFormat_MediaContent] as mc ON mc.[MediaContentID] = smrd.[FKMediaContentID] \r\n            LEFT JOIN people p ON p.PersonID=smr.[RequestMadeFromStudentNo]\r\n            LEFT JOIN peoplemingroup pg ON pg.PersonID=smr.[RequestMadeFromStudentNo]\r\n            LEFT JOIN CampusLookup clk ON clk.CampusId=smr.CampusId\r\n\t\t\tLEFT JOIN AlternativeFormat_MediaJob mj ON mj.MediaJobID = smrd.FKMediaJobId\r\n            LEFT JOIN AlternativeFormat_ProofOfPurchaseInfo pop ON pop.FK_MediaContentUniqueID = mc.MediaContentID AND pop.StudentPersonID = smr.RequestMadeFromStudentNo\r\n            LEFT JOIN people wpop ON wpop.PersonID=pop.[WhoAcceptedProofOfPurchase]\r\n            LEFT JOIN peoplemingroup wpopg ON wpopg.PersonID=pop.[WhoAcceptedProofOfPurchase] WHERE smrd.[IsCompleted] = 0 AND smrd.[IsCancelled] = 0 AND smrd.[IsApproved] = 1 AND (@campusid=0 OR smr.[CampusId]=@campusid)", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					while (dataReader.Read())
					{
						MediaContentRequestedInfo studentMediaRequestInfoFromReader = this.GetStudentMediaRequestInfoFromReader(dataReader, this.OpContext, null);
						list.Add(studentMediaRequestInfoFromReader);
					}
				}
			}
			return list;
		}

		// Token: 0x06000B39 RID: 2873 RVA: 0x00077108 File Offset: 0x00075308
		[DebuggerStepThrough]
		public Task<IList<MediaContentRequestedInfoExtended>> LoadAllStudentMediaRequestByStudentAsync(int studentId, DateTime startdate, DateTime enddate)
		{
			StudentMediaRequestDAO.<LoadAllStudentMediaRequestByStudentAsync>d__29 <LoadAllStudentMediaRequestByStudentAsync>d__ = new StudentMediaRequestDAO.<LoadAllStudentMediaRequestByStudentAsync>d__29();
			<LoadAllStudentMediaRequestByStudentAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<MediaContentRequestedInfoExtended>>.Create();
			<LoadAllStudentMediaRequestByStudentAsync>d__.<>4__this = this;
			<LoadAllStudentMediaRequestByStudentAsync>d__.studentId = studentId;
			<LoadAllStudentMediaRequestByStudentAsync>d__.startdate = startdate;
			<LoadAllStudentMediaRequestByStudentAsync>d__.enddate = enddate;
			<LoadAllStudentMediaRequestByStudentAsync>d__.<>1__state = -1;
			<LoadAllStudentMediaRequestByStudentAsync>d__.<>t__builder.Start<StudentMediaRequestDAO.<LoadAllStudentMediaRequestByStudentAsync>d__29>(ref <LoadAllStudentMediaRequestByStudentAsync>d__);
			return <LoadAllStudentMediaRequestByStudentAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x00077164 File Offset: 0x00075364
		public void MarkMediaContentRequestedAsCompleted(int mediaContentRequestInfoId, MediaRequestStatus status, DateTime availableStartTime, DateTime availableEndTime, int mediaContentPerFormatId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@studentmediarequestdetailid", DbType.Int32, mediaContentRequestInfoId),
				databaseLayer.GetParameter("@status", DbType.String, status.ToString()),
				databaseLayer.GetParameter("@mediacontentperformatid", DbType.Int32, mediaContentPerFormatId),
				databaseLayer.GetParameter("@availablestarttime", DbType.DateTime, availableStartTime),
				databaseLayer.GetParameter("@availableendtime", DbType.DateTime, availableEndTime)
			};
			databaseLayer.ExecuteStoredProcedure("sp_AlternateFormat_MarkMediaContentRequestedAsCompleted", parameters);
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x00077214 File Offset: 0x00075414
		public IList<MediaContentRequestedInfo> LoadAllApprovedMediaRequest(int campusId = 0)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			List<MediaContentRequestedInfo> list = new List<MediaContentRequestedInfo>();
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@approved", DbType.Boolean, true),
				databaseLayer.GetParameter("@campusid", DbType.Int32, campusId)
			};
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT  smr.[StudentMediaRequestID], smr.[RequestMadeFromStudentNo], smr.[CreatedDateTime], smr.[CompletedDateTime], \r\n                    smr.[CampusId],clk.CampusName,clk.CampusDescription,clk.[IsActive] as CampusIsActive,\r\n                    smrd.[IsApproved], smrd.[FKStudentMediaRequestID], smrd.[IsCompleted], smrd.[IsCancelled], smrd.[FKMediaJobId], mj.JobTitle,\r\n\t\t            smrd.[StudentMediaRequestDetailId], smrd.[FKMediaContentID], smrd.[Status], \r\n                    case pop.IsActive\r\n\t\t\t\t\t\twhen 1 then pop.ProofOfPurchaseID\r\n\t\t\t\t\t\telse NULL\r\n\t\t\t\t\tend as ProofOfPurchaseID,\r\n\t\t\t\t\tpop.IsActive, pop.ProofOfPurchaseNote, pop.WhoAcceptedProofOfPurchase, pop.WhenWasAccepted, pop.StudentPersonID, pop.[Filename], pop.Extension,\r\n                    smrd.[FKMediaContentPerFormatId], smrd.[AvailableStartTime], smrd.[AvailableEndTime], smrd.[StudentPreferredFormat],\r\n                    smrd.CreatedDateTime as [MediaRequestDetailCreatedDateTime], smrd.CompletedDateTime as [MediaRequestDetailCompletedDateTime], smrd.CompletionNotes,\r\n\t\t            mcf.[MediaContentFormat], mc.[ShortTitle], mc.[ISBN], mc.Authors, mc.[Edition], mc.[Summary], mc.[ProofOfPurchaseRequired],\r\n                    p.PersonID, p.firstname, p.lastname, p.middlename, p.student_no, pg.mingroupid,\r\n                    pop.WhoAcceptedProofOfPurchase as wpoppersonid, wpop.firstname as wpopfirstname, wpop.lastname as wpoplastname, wpop.middlename as wpopmiddlename, wpop.student_no as wpopstudent_no, wpopg.mingroupid as wpopmingroupid\r\n            FROM [AlternativeFormat_StudentMediaRequestDetail] as smrd\r\n            INNER JOIN  [AlternativeFormat_StudentMediaRequest] as smr ON smrd.[FKStudentMediaRequestID] = smr.[StudentMediaRequestID]\r\n            LEFT JOIN [AlternativeFormat_MediaContent_x_MediaContentFormat] as mcf ON mcf.[MediaContentPerFormatID] = smrd.[FKMediaContentPerFormatId] \r\n            LEFT JOIN [AlternativeFormat_MediaContent] as mc ON mc.[MediaContentID] = smrd.[FKMediaContentID] \r\n            LEFT JOIN people p ON p.PersonID=smr.[RequestMadeFromStudentNo]\r\n            LEFT JOIN peoplemingroup pg ON pg.PersonID=smr.[RequestMadeFromStudentNo]\r\n            LEFT JOIN CampusLookup clk ON clk.CampusId=smr.CampusId\r\n\t\t\tLEFT JOIN AlternativeFormat_MediaJob mj ON mj.MediaJobID = smrd.FKMediaJobId\r\n            LEFT JOIN AlternativeFormat_ProofOfPurchaseInfo pop ON pop.FK_MediaContentUniqueID = mc.MediaContentID AND pop.StudentPersonID = smr.RequestMadeFromStudentNo\r\n            LEFT JOIN people wpop ON wpop.PersonID=pop.[WhoAcceptedProofOfPurchase]\r\n            LEFT JOIN peoplemingroup wpopg ON wpopg.PersonID=pop.[WhoAcceptedProofOfPurchase] WHERE smrd.[IsApproved] = @approved AND (@campusid = 0 OR smr.[CampusId] = @campusid)", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					while (dataReader.Read())
					{
						MediaContentRequestedInfo studentMediaRequestInfoFromReader = this.GetStudentMediaRequestInfoFromReader(dataReader, this.OpContext, null);
						list.Add(studentMediaRequestInfoFromReader);
					}
				}
			}
			return list;
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x000772DC File Offset: 0x000754DC
		public int AddStudentContentMediaRequestInfo(MediaContentRequestedInfo mediaContentRequestedInfo)
		{
			IMediaContentDAO mediaContentDAO = new MediaContentDAO(this.OpContext);
			int num = mediaContentRequestedInfo.ContentDetailRequested.MediaContentPerFormatId;
			bool flag = num == 0;
			if (flag)
			{
				num = (mediaContentRequestedInfo.ContentDetailRequested.MediaContentPerFormatId = mediaContentDAO.GetMediaContentPerFormatId(mediaContentRequestedInfo.ContentDetailRequested.MediaContent.MediaContentUniqueId, mediaContentRequestedInfo.ContentDetailRequested.MediaContentFormat));
			}
			bool flag2 = num > 0;
			if (flag2)
			{
				bool flag3 = mediaContentRequestedInfo.ProofOfPurchase != null && mediaContentRequestedInfo.ProofOfPurchase.ProofOfPurchaseId > 0;
				if (flag3)
				{
					mediaContentRequestedInfo.ProofOfPurchaseId = mediaContentRequestedInfo.ProofOfPurchase.ProofOfPurchaseId;
				}
				eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
				OperationContext opContext = this.OpContext;
				DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
				DbParameter[] array = new DbParameter[14];
				array[0] = databaseLayer.GetOutputParameter("@mediarequestinfoid", DbType.Int32, 0);
				array[1] = databaseLayer.GetParameter("@studentmediarequestid", DbType.Int32, mediaContentRequestedInfo.StudentRequestId);
				array[2] = databaseLayer.GetParameter("@mediacontentid", DbType.Guid, mediaContentRequestedInfo.ContentDetailRequested.MediaContent.MediaContentUniqueId);
				array[3] = databaseLayer.GetParameter("@mediacontentperformatid", DbType.Int32, num);
				array[4] = databaseLayer.GetParameter("@status", DbType.String, mediaContentRequestedInfo.RequestStatus.ToString());
				array[5] = databaseLayer.GetParameter("@isapproved", DbType.Boolean, mediaContentRequestedInfo.IsApproved);
				array[6] = databaseLayer.GetParameter("@mediajobid", DbType.Int32, mediaContentRequestedInfo.MediaJobId);
				array[7] = databaseLayer.GetParameter("@iscompleted", DbType.Boolean, mediaContentRequestedInfo.IsCompleted);
				array[8] = databaseLayer.GetParameter("@proofofpurchaseid", DbType.Int32, (mediaContentRequestedInfo.ProofOfPurchaseId > 0) ? mediaContentRequestedInfo.ProofOfPurchaseId : DBNull.Value);
				array[9] = databaseLayer.GetParameter("@createddatetime", DbType.DateTime, mediaContentRequestedInfo.CreatedDatetime);
				int num2 = 10;
				DatabaseLayer databaseLayer2 = databaseLayer;
				string pName = "@completeddatetime";
				DbType pType = DbType.DateTime;
				DateTime? dateTime = mediaContentRequestedInfo.CompletedDateTime;
				array[num2] = databaseLayer2.GetParameter(pName, pType, (dateTime != null) ? dateTime.GetValueOrDefault() : DBNull.Value);
				int num3 = 11;
				DatabaseLayer databaseLayer3 = databaseLayer;
				string pName2 = "@availablestarttime";
				DbType pType2 = DbType.DateTime;
				dateTime = mediaContentRequestedInfo.AvailableStartTime;
				array[num3] = databaseLayer3.GetParameter(pName2, pType2, (dateTime != null) ? dateTime.GetValueOrDefault() : DBNull.Value);
				int num4 = 12;
				DatabaseLayer databaseLayer4 = databaseLayer;
				string pName3 = "@availableendtime";
				DbType pType3 = DbType.DateTime;
				dateTime = mediaContentRequestedInfo.AvailableEndTime;
				array[num4] = databaseLayer4.GetParameter(pName3, pType3, (dateTime != null) ? dateTime.GetValueOrDefault() : DBNull.Value);
				int num5 = 13;
				DatabaseLayer databaseLayer5 = databaseLayer;
				string pName4 = "@studentpreferredformat";
				DbType pType4 = DbType.String;
				MediaContentFormat? studentPreferredFormat = mediaContentRequestedInfo.ContentDetailRequested.StudentPreferredFormat;
				array[num5] = databaseLayer5.GetParameter(pName4, pType4, (studentPreferredFormat != null) ? studentPreferredFormat.GetValueOrDefault() : DBNull.Value);
				DbParameter[] array2 = array;
				databaseLayer.ExecuteNonQuery("INSERT INTO [AlternativeFormat_StudentMediaRequestDetail]\r\n           ([FKStudentMediaRequestID]\r\n           ,[FKMediaContentID]\r\n           ,[FKMediaContentPerFormatId]\r\n           ,[Status]\r\n           ,[IsApproved]\r\n           ,[FKMediaJobId]\r\n           ,[IsCompleted]\r\n           ,[CreatedDateTime]\r\n           ,[CompletedDateTime]\r\n           ,[AvailableStartTime]\r\n           ,[AvailableEndTime]\r\n           ,[StudentPreferredFormat])\r\n            VALUES\r\n           (@studentmediarequestid\r\n           ,@mediacontentid\r\n           ,@mediacontentperformatid\r\n           ,@status\r\n           ,@isapproved\r\n           ,@mediajobid\r\n           ,@iscompleted\r\n           ,@createddatetime\r\n           ,@completeddatetime\r\n           ,@availablestarttime\r\n           ,@availableendtime\r\n           ,@studentpreferredformat) \r\n\r\n            set @mediarequestinfoid = SCOPE_IDENTITY()", array2);
				mediaContentRequestedInfo.MediaContentRequestedInfoID = ((array2[0].Value is DBNull) ? 0 : ((int)array2[0].Value));
			}
			return mediaContentRequestedInfo.MediaContentRequestedInfoID;
		}

		// Token: 0x06000B3D RID: 2877 RVA: 0x000775E4 File Offset: 0x000757E4
		public void DeleteStudentContentMediaRequestInfo(MediaContentRequestedInfo requestedInfo, MediaRequestStatus status = MediaRequestStatus.Rejected_by_Staff)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@studentmediarequestdetailid", DbType.Int32, requestedInfo.MediaContentRequestedInfoID),
				databaseLayer.GetParameter("@whomodifiedpersonid", DbType.Int32, this.OpContext.WhoAmI),
				databaseLayer.GetParameter("@status", DbType.String, status.ToString()),
				databaseLayer.GetParameter("@mediacontentperformatid", DbType.Int32, requestedInfo.ContentDetailRequested.MediaContentPerFormatId)
			};
			databaseLayer.ExecuteStoredProcedure("sp_AlternateFormat_RemoveStudentMediaRequestInfo", parameters);
		}

		// Token: 0x06000B3E RID: 2878 RVA: 0x00077698 File Offset: 0x00075898
		public MediaContentRequestedInfo LoadMediaContentRequestInfoById(int mediaContentRequestInfoId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter parameter = databaseLayer.GetParameter("@studentmediarequestdetailid", DbType.Int32, mediaContentRequestInfoId);
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT  smr.[StudentMediaRequestID], smr.[RequestMadeFromStudentNo], smr.[CreatedDateTime], smr.[CompletedDateTime], \r\n                    smr.[CampusId],clk.CampusName,clk.CampusDescription,clk.[IsActive] as CampusIsActive,\r\n                    smrd.[IsApproved], smrd.[FKStudentMediaRequestID], smrd.[IsCompleted], smrd.[IsCancelled], smrd.[FKMediaJobId], mj.JobTitle,\r\n\t\t            smrd.[StudentMediaRequestDetailId], smrd.[FKMediaContentID], smrd.[Status], \r\n                    case pop.IsActive\r\n\t\t\t\t\t\twhen 1 then pop.ProofOfPurchaseID\r\n\t\t\t\t\t\telse NULL\r\n\t\t\t\t\tend as ProofOfPurchaseID,\r\n\t\t\t\t\tpop.IsActive, pop.ProofOfPurchaseNote, pop.WhoAcceptedProofOfPurchase, pop.WhenWasAccepted, pop.StudentPersonID, pop.[Filename], pop.Extension,\r\n                    smrd.[FKMediaContentPerFormatId], smrd.[AvailableStartTime], smrd.[AvailableEndTime], smrd.[StudentPreferredFormat],\r\n                    smrd.CreatedDateTime as [MediaRequestDetailCreatedDateTime], smrd.CompletedDateTime as [MediaRequestDetailCompletedDateTime], smrd.CompletionNotes,\r\n\t\t            mcf.[MediaContentFormat], mc.[ShortTitle], mc.[ISBN], mc.Authors, mc.[Edition], mc.[Summary], mc.[ProofOfPurchaseRequired],\r\n                    p.PersonID, p.firstname, p.lastname, p.middlename, p.student_no, pg.mingroupid,\r\n                    pop.WhoAcceptedProofOfPurchase as wpoppersonid, wpop.firstname as wpopfirstname, wpop.lastname as wpoplastname, wpop.middlename as wpopmiddlename, wpop.student_no as wpopstudent_no, wpopg.mingroupid as wpopmingroupid\r\n            FROM [AlternativeFormat_StudentMediaRequestDetail] as smrd\r\n            INNER JOIN  [AlternativeFormat_StudentMediaRequest] as smr ON smrd.[FKStudentMediaRequestID] = smr.[StudentMediaRequestID]\r\n            LEFT JOIN [AlternativeFormat_MediaContent_x_MediaContentFormat] as mcf ON mcf.[MediaContentPerFormatID] = smrd.[FKMediaContentPerFormatId] \r\n            LEFT JOIN [AlternativeFormat_MediaContent] as mc ON mc.[MediaContentID] = smrd.[FKMediaContentID] \r\n            LEFT JOIN people p ON p.PersonID=smr.[RequestMadeFromStudentNo]\r\n            LEFT JOIN peoplemingroup pg ON pg.PersonID=smr.[RequestMadeFromStudentNo]\r\n            LEFT JOIN CampusLookup clk ON clk.CampusId=smr.CampusId\r\n\t\t\tLEFT JOIN AlternativeFormat_MediaJob mj ON mj.MediaJobID = smrd.FKMediaJobId\r\n            LEFT JOIN AlternativeFormat_ProofOfPurchaseInfo pop ON pop.FK_MediaContentUniqueID = mc.MediaContentID AND pop.StudentPersonID = smr.RequestMadeFromStudentNo\r\n            LEFT JOIN people wpop ON wpop.PersonID=pop.[WhoAcceptedProofOfPurchase]\r\n            LEFT JOIN peoplemingroup wpopg ON wpopg.PersonID=pop.[WhoAcceptedProofOfPurchase] WHERE smrd.[StudentMediaRequestDetailId] = @studentmediarequestdetailid", new DbParameter[]
			{
				parameter
			}))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					return this.GetStudentMediaRequestInfoFromReader(dataReader, this.OpContext, null);
				}
			}
			return null;
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x00077734 File Offset: 0x00075934
		public MediaContentRequestedInfo LoadArchiveMediaContentRequestInfoById(int mediaContentRequestInfoId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter parameter = databaseLayer.GetParameter("@studentmediarequestdetailid", DbType.Int32, mediaContentRequestInfoId);
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT  smr.[StudentMediaRequestID], smr.[RequestMadeFromStudentNo], smr.[CreatedDateTime], smr.[CompletedDateTime], \r\n                    smr.[CampusId],clk.CampusName,clk.CampusDescription,clk.[IsActive] as CampusIsActive,\r\n                    smrd.[IsApproved], smrd.[FKStudentMediaRequestID], smrd.[IsCompleted], smrd.[IsCancelled], smrd.[FKMediaJobId], mj.JobTitle,\r\n\t\t            smrd.[StudentMediaRequestDetailId], smrd.[FKMediaContentID], smrd.[Status], \r\n                    case pop.IsActive\r\n\t\t\t\t\t\twhen 1 then pop.ProofOfPurchaseID\r\n\t\t\t\t\t\telse NULL\r\n\t\t\t\t\tend as ProofOfPurchaseID,\r\n\t\t\t\t\tpop.IsActive, pop.ProofOfPurchaseNote, pop.WhoAcceptedProofOfPurchase, pop.WhenWasAccepted, pop.StudentPersonID, pop.[Filename], pop.Extension,\r\n                    smrd.[FKMediaContentPerFormatId], smrd.[AvailableStartTime], smrd.[AvailableEndTime], smrd.[StudentPreferredFormat],\r\n                    smrd.CreatedDateTime as [MediaRequestDetailCreatedDateTime], smrd.CompletedDateTime as [MediaRequestDetailCompletedDateTime], smrd.CompletionNotes,\r\n\t\t            mcf.[MediaContentFormat], mc.[ShortTitle], mc.[ISBN], mc.Authors, mc.[Edition], mc.[Summary], mc.[ProofOfPurchaseRequired],\r\n                    p.PersonID, p.firstname, p.lastname, p.middlename, p.student_no, pg.mingroupid,\r\n                    pop.WhoAcceptedProofOfPurchase as wpoppersonid, wpop.firstname as wpopfirstname, wpop.lastname as wpoplastname, wpop.middlename as wpopmiddlename, wpop.student_no as wpopstudent_no, wpopg.mingroupid as wpopmingroupid\r\n            FROM [AlternativeFormat_Archive_StudentMediaRequestDetail] as smrd\r\n            INNER JOIN  [AlternativeFormat_StudentMediaRequest] as smr ON smrd.[FKStudentMediaRequestID] = smr.[StudentMediaRequestID]\r\n            LEFT JOIN [AlternativeFormat_MediaContent_x_MediaContentFormat] as mcf ON mcf.[MediaContentPerFormatID] = smrd.[FKMediaContentPerFormatId] \r\n            LEFT JOIN [AlternativeFormat_MediaContent] as mc ON mc.[MediaContentID] = smrd.[FKMediaContentID] \r\n            LEFT JOIN people p ON p.PersonID=smr.[RequestMadeFromStudentNo]\r\n            LEFT JOIN peoplemingroup pg ON pg.PersonID=smr.[RequestMadeFromStudentNo]\r\n            LEFT JOIN CampusLookup clk ON clk.CampusId=smr.CampusId\r\n\t\t\tLEFT JOIN AlternativeFormat_Archive_MediaJob mj ON mj.MediaJobID = smrd.FKMediaJobId\r\n            LEFT JOIN AlternativeFormat_ProofOfPurchaseInfo pop ON pop.FK_MediaContentUniqueID = mc.MediaContentID AND pop.StudentPersonID = smr.RequestMadeFromStudentNo\r\n            LEFT JOIN people wpop ON wpop.PersonID=pop.[WhoAcceptedProofOfPurchase]\r\n            LEFT JOIN peoplemingroup wpopg ON wpopg.PersonID=pop.[WhoAcceptedProofOfPurchase] WHERE smrd.[StudentMediaRequestDetailId] = @studentmediarequestdetailid", new DbParameter[]
			{
				parameter
			}))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					return this.GetStudentMediaRequestInfoFromReader(dataReader, this.OpContext, null);
				}
			}
			return null;
		}

		// Token: 0x06000B40 RID: 2880 RVA: 0x000777D0 File Offset: 0x000759D0
		public MediaContentRequestedInfo LoadMediaContentRequestInfoByMediaContentPerFormatAndStudent(int studentPersonId, int mediaContentPerFormatId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@studentpersonid", DbType.Int32, studentPersonId),
				databaseLayer.GetParameter("@mediacontentperformatid", DbType.Int32, mediaContentPerFormatId)
			};
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT  smr.[StudentMediaRequestID], smr.[RequestMadeFromStudentNo], smr.[CreatedDateTime], smr.[CompletedDateTime], \r\n                    smr.[CampusId],clk.CampusName,clk.CampusDescription,clk.[IsActive] as CampusIsActive,\r\n                    smrd.[IsApproved], smrd.[FKStudentMediaRequestID], smrd.[IsCompleted], smrd.[IsCancelled], smrd.[FKMediaJobId], mj.JobTitle,\r\n\t\t            smrd.[StudentMediaRequestDetailId], smrd.[FKMediaContentID], smrd.[Status], \r\n                    case pop.IsActive\r\n\t\t\t\t\t\twhen 1 then pop.ProofOfPurchaseID\r\n\t\t\t\t\t\telse NULL\r\n\t\t\t\t\tend as ProofOfPurchaseID,\r\n\t\t\t\t\tpop.IsActive, pop.ProofOfPurchaseNote, pop.WhoAcceptedProofOfPurchase, pop.WhenWasAccepted, pop.StudentPersonID, pop.[Filename], pop.Extension,\r\n                    smrd.[FKMediaContentPerFormatId], smrd.[AvailableStartTime], smrd.[AvailableEndTime], smrd.[StudentPreferredFormat],\r\n                    smrd.CreatedDateTime as [MediaRequestDetailCreatedDateTime], smrd.CompletedDateTime as [MediaRequestDetailCompletedDateTime], smrd.CompletionNotes,\r\n\t\t            mcf.[MediaContentFormat], mc.[ShortTitle], mc.[ISBN], mc.Authors, mc.[Edition], mc.[Summary], mc.[ProofOfPurchaseRequired],\r\n                    p.PersonID, p.firstname, p.lastname, p.middlename, p.student_no, pg.mingroupid,\r\n                    pop.WhoAcceptedProofOfPurchase as wpoppersonid, wpop.firstname as wpopfirstname, wpop.lastname as wpoplastname, wpop.middlename as wpopmiddlename, wpop.student_no as wpopstudent_no, wpopg.mingroupid as wpopmingroupid\r\n            FROM [AlternativeFormat_StudentMediaRequestDetail] as smrd\r\n            INNER JOIN  [AlternativeFormat_StudentMediaRequest] as smr ON smrd.[FKStudentMediaRequestID] = smr.[StudentMediaRequestID]\r\n            LEFT JOIN [AlternativeFormat_MediaContent_x_MediaContentFormat] as mcf ON mcf.[MediaContentPerFormatID] = smrd.[FKMediaContentPerFormatId] \r\n            LEFT JOIN [AlternativeFormat_MediaContent] as mc ON mc.[MediaContentID] = smrd.[FKMediaContentID] \r\n            LEFT JOIN people p ON p.PersonID=smr.[RequestMadeFromStudentNo]\r\n            LEFT JOIN peoplemingroup pg ON pg.PersonID=smr.[RequestMadeFromStudentNo]\r\n            LEFT JOIN CampusLookup clk ON clk.CampusId=smr.CampusId\r\n\t\t\tLEFT JOIN AlternativeFormat_MediaJob mj ON mj.MediaJobID = smrd.FKMediaJobId\r\n            LEFT JOIN AlternativeFormat_ProofOfPurchaseInfo pop ON pop.FK_MediaContentUniqueID = mc.MediaContentID AND pop.StudentPersonID = smr.RequestMadeFromStudentNo\r\n            LEFT JOIN people wpop ON wpop.PersonID=pop.[WhoAcceptedProofOfPurchase]\r\n            LEFT JOIN peoplemingroup wpopg ON wpopg.PersonID=pop.[WhoAcceptedProofOfPurchase] WHERE smrd.IsCancelled = 0 and smrd.FKMediaContentPerFormatId = @mediacontentperformatid and smr.RequestMadeFromStudentNo=@studentpersonid", parameters))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					return this.GetStudentMediaRequestInfoFromReader(dataReader, this.OpContext, null);
				}
			}
			return null;
		}

		// Token: 0x06000B41 RID: 2881 RVA: 0x00077880 File Offset: 0x00075A80
		[DebuggerStepThrough]
		public Task<IList<MediaContentRequestedInfo>> LoadAllMediaContentRequestInfoByMediaContentAndStudentAsync(int studentPersonId, Guid mediaContentId)
		{
			StudentMediaRequestDAO.<LoadAllMediaContentRequestInfoByMediaContentAndStudentAsync>d__37 <LoadAllMediaContentRequestInfoByMediaContentAndStudentAsync>d__ = new StudentMediaRequestDAO.<LoadAllMediaContentRequestInfoByMediaContentAndStudentAsync>d__37();
			<LoadAllMediaContentRequestInfoByMediaContentAndStudentAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<MediaContentRequestedInfo>>.Create();
			<LoadAllMediaContentRequestInfoByMediaContentAndStudentAsync>d__.<>4__this = this;
			<LoadAllMediaContentRequestInfoByMediaContentAndStudentAsync>d__.studentPersonId = studentPersonId;
			<LoadAllMediaContentRequestInfoByMediaContentAndStudentAsync>d__.mediaContentId = mediaContentId;
			<LoadAllMediaContentRequestInfoByMediaContentAndStudentAsync>d__.<>1__state = -1;
			<LoadAllMediaContentRequestInfoByMediaContentAndStudentAsync>d__.<>t__builder.Start<StudentMediaRequestDAO.<LoadAllMediaContentRequestInfoByMediaContentAndStudentAsync>d__37>(ref <LoadAllMediaContentRequestInfoByMediaContentAndStudentAsync>d__);
			return <LoadAllMediaContentRequestInfoByMediaContentAndStudentAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000B42 RID: 2882 RVA: 0x000778D4 File Offset: 0x00075AD4
		public bool IsMediaContentAlreadyRequested(int studentPersonId, MediaContentIdentifier identifier)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@studentpersonid", DbType.Int32, studentPersonId),
				databaseLayer.GetParameter("@identifiermediacontentuniqueid", DbType.Guid, identifier.MediaContentUniqueId),
				databaseLayer.GetParameter("@identifierisbn", DbType.String, identifier.ISBN ?? string.Empty),
				databaseLayer.GetParameter("@identifiermediacontentid", DbType.Int32, identifier.MediaContentId),
				databaseLayer.GetParameter("@identifierexternalid", DbType.String, identifier.ExternalId ?? string.Empty)
			};
			object obj = databaseLayer.ExecuteScalar("SELECT distinct 1 FROM [AlternativeFormat_StudentMediaRequestDetail] as smrd\r\n            INNER JOIN  [AlternativeFormat_StudentMediaRequest] as smr ON smrd.[FKStudentMediaRequestID] = smr.[StudentMediaRequestID]\r\n            LEFT JOIN [AlternativeFormat_MediaContent] as mc ON mc.[MediaContentID] = smrd.[FKMediaContentID] \r\n            WHERE smrd.IsCancelled = 0 and smr.RequestMadeFromStudentNo=@studentpersonid \r\n\t\t\tand (  mc.MediaContentID=@identifiermediacontentuniqueid \r\n\t\t\t\tor (@identifierisbn is not null and @identifierisbn <> '' and mc.ISBN=@identifierisbn)\r\n\t\t\t\tor mc.MediaContentDataID=@identifiermediacontentid\r\n\t\t\t\tor (@identifierexternalid is not null and @identifierexternalid <> '' and mc.ExternalId=@identifierexternalid) )", parameters);
			return obj != null && !Convert.IsDBNull(obj) && (int)obj > 0;
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x000779B4 File Offset: 0x00075BB4
		public void UpdateStudentContentMediaRequestInfo(MediaContentRequestedInfo requestedInfo)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			IMediaContentDAO mediaContentDAO = new MediaContentDAO(this.OpContext);
			int num = requestedInfo.ContentDetailRequested.MediaContentPerFormatId;
			bool flag = num == 0;
			if (flag)
			{
				num = (requestedInfo.ContentDetailRequested.MediaContentPerFormatId = mediaContentDAO.GetMediaContentPerFormatId(requestedInfo.ContentDetailRequested.MediaContent.MediaContentUniqueId, requestedInfo.ContentDetailRequested.MediaContentFormat));
			}
			bool flag2 = num > 0;
			if (flag2)
			{
				DbParameter[] parameters = new DbParameter[]
				{
					databaseLayer.GetParameter("@mediacontentid", DbType.Guid, requestedInfo.ContentDetailRequested.MediaContent.MediaContentUniqueId),
					databaseLayer.GetParameter("@status", DbType.String, requestedInfo.RequestStatus.ToString()),
					databaseLayer.GetParameter("@mediarequestedinfoid", DbType.Int32, requestedInfo.MediaContentRequestedInfoID),
					databaseLayer.GetParameter("@isapproved", DbType.Boolean, requestedInfo.IsApproved),
					databaseLayer.GetParameter("@mediacontentperformatid", DbType.Int32, (num == 0) ? DBNull.Value : num),
					databaseLayer.GetParameter("@availablestarttime", DbType.DateTime, (requestedInfo.AvailableStartTime != null) ? requestedInfo.AvailableStartTime : DBNull.Value),
					databaseLayer.GetParameter("@availableendtime", DbType.DateTime, (requestedInfo.AvailableEndTime != null) ? requestedInfo.AvailableEndTime : DBNull.Value),
					databaseLayer.GetParameter("@mediajobid", DbType.Int32, requestedInfo.MediaJobId),
					databaseLayer.GetParameter("@proofofpurchaseid", DbType.Int32, (requestedInfo.ProofOfPurchaseId > 0) ? requestedInfo.ProofOfPurchaseId : DBNull.Value),
					databaseLayer.GetParameter("@iscancelled", DbType.Boolean, requestedInfo.IsCancelled),
					databaseLayer.GetParameter("@iscompleted", DbType.Boolean, requestedInfo.IsCompleted),
					databaseLayer.GetParameter("@createddatetime", DbType.DateTime, requestedInfo.CreatedDatetime),
					databaseLayer.GetParameter("@completeddatetime", DbType.DateTime, (requestedInfo.CompletedDateTime != null) ? requestedInfo.CompletedDateTime.Value : DBNull.Value),
					databaseLayer.GetParameter("@studentcompletionnotes", DbType.String, requestedInfo.CompletionNotes ?? string.Empty)
				};
				databaseLayer.ExecuteNonQuery("UPDATE [AlternativeFormat_StudentMediaRequestDetail]\r\n            SET  [FKMediaContentID] = @mediacontentid\r\n                ,[Status] = @status\r\n                ,[FKMediaContentPerFormatId] = @mediacontentperformatid\r\n                ,[IsApproved] = @isapproved\r\n                ,[AvailableStartTime] = @availablestarttime\r\n                ,[AvailableEndTime] = @availableendtime\r\n                ,[FKMediaJobId] = @mediajobid\r\n                ,[FKProofOfPurchaseID] = @proofofpurchaseid\r\n                ,[IsCompleted] = @iscompleted\r\n                ,[IsCancelled] = @iscancelled\r\n                ,[CreatedDateTime]=@createddatetime\r\n                ,[CompletedDateTime]=@completeddatetime\r\n                ,[CompletionNotes] = @studentcompletionnotes\r\n            WHERE StudentMediaRequestDetailId = @mediarequestedinfoid", parameters);
			}
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x00077C48 File Offset: 0x00075E48
		[DebuggerStepThrough]
		public Task UpdateStudentContentMediaRequestInfoAsync(MediaContentRequestedInfo requestedInfo)
		{
			StudentMediaRequestDAO.<UpdateStudentContentMediaRequestInfoAsync>d__40 <UpdateStudentContentMediaRequestInfoAsync>d__ = new StudentMediaRequestDAO.<UpdateStudentContentMediaRequestInfoAsync>d__40();
			<UpdateStudentContentMediaRequestInfoAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<UpdateStudentContentMediaRequestInfoAsync>d__.<>4__this = this;
			<UpdateStudentContentMediaRequestInfoAsync>d__.requestedInfo = requestedInfo;
			<UpdateStudentContentMediaRequestInfoAsync>d__.<>1__state = -1;
			<UpdateStudentContentMediaRequestInfoAsync>d__.<>t__builder.Start<StudentMediaRequestDAO.<UpdateStudentContentMediaRequestInfoAsync>d__40>(ref <UpdateStudentContentMediaRequestInfoAsync>d__);
			return <UpdateStudentContentMediaRequestInfoAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000B45 RID: 2885 RVA: 0x00077C94 File Offset: 0x00075E94
		private ProofOfPurchaseInfo GetProofOfPurchaseFromReader(IDataReader reader, IBatchDecryptor decryptor = null)
		{
			return this.GetProofOfPurchaseReceipt(this.GetBaseProofOfPurchaseFromReader(reader, decryptor));
		}

		// Token: 0x06000B46 RID: 2886 RVA: 0x00077CB4 File Offset: 0x00075EB4
		private ProofOfPurchaseInfo GetBaseProofOfPurchaseFromReader(IDataReader reader, IBatchDecryptor decryptor = null)
		{
			return new ProofOfPurchaseInfo
			{
				ProofOfPurchaseId = (int)reader["ProofOfPurchaseID"],
				Notes = (string)reader["ProofOfPurchaseNote"],
				WhoAcceptedProofOfPurchase = PeopleDAO.GetPersonFromReader("", reader, this.OpContext, decryptor),
				WhenWasAccepted = ((reader["WhenWasAccepted"] is DBNull) ? null : new DateTime?((DateTime)reader["WhenWasAccepted"])),
				MediaContentUniqueId = (Guid)reader["FK_MediaContentUniqueID"],
				StudentPersonId = (int)reader["StudentPersonID"],
				Filename = (string)reader["Filename"],
				Extension = (string)reader["Extension"]
			};
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x00077DAC File Offset: 0x00075FAC
		[DebuggerStepThrough]
		private Task<ProofOfPurchaseInfo> GetProofOfPurchaseFromReaderAsync(IDataReader reader, IBatchDecryptor decryptor = null)
		{
			StudentMediaRequestDAO.<GetProofOfPurchaseFromReaderAsync>d__43 <GetProofOfPurchaseFromReaderAsync>d__ = new StudentMediaRequestDAO.<GetProofOfPurchaseFromReaderAsync>d__43();
			<GetProofOfPurchaseFromReaderAsync>d__.<>t__builder = AsyncTaskMethodBuilder<ProofOfPurchaseInfo>.Create();
			<GetProofOfPurchaseFromReaderAsync>d__.<>4__this = this;
			<GetProofOfPurchaseFromReaderAsync>d__.reader = reader;
			<GetProofOfPurchaseFromReaderAsync>d__.decryptor = decryptor;
			<GetProofOfPurchaseFromReaderAsync>d__.<>1__state = -1;
			<GetProofOfPurchaseFromReaderAsync>d__.<>t__builder.Start<StudentMediaRequestDAO.<GetProofOfPurchaseFromReaderAsync>d__43>(ref <GetProofOfPurchaseFromReaderAsync>d__);
			return <GetProofOfPurchaseFromReaderAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000B48 RID: 2888 RVA: 0x00077E00 File Offset: 0x00076000
		private ProofOfPurchaseInfo GetProofOfPurchaseReceipt(ProofOfPurchaseInfo proofOfPurchase)
		{
			DatabaseLayer clockWorkFiles = DatabaseLayerFactory.ClockWorkFiles;
			DbParameter[] parameters = new DbParameter[]
			{
				clockWorkFiles.GetParameter("@proofofpurchaseid", DbType.Int32, proofOfPurchase.ProofOfPurchaseId)
			};
			using (IDataReader dataReader = clockWorkFiles.ExecuteQueryReader("select ProofOfPurchaseReceipt from AlternativeFormat_ProofOfPurchaseImage where IsActive = 1 AND FK_ProofOfPurchaseID = @proofofpurchaseid", parameters))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					proofOfPurchase.ProofOfPurchaseReceipt = (byte[])dataReader["ProofOfPurchaseReceipt"];
				}
			}
			return proofOfPurchase;
		}

		// Token: 0x06000B49 RID: 2889 RVA: 0x00077E94 File Offset: 0x00076094
		[DebuggerStepThrough]
		private Task<ProofOfPurchaseInfo> GetProofOfPurchaseReceiptAsync(ProofOfPurchaseInfo proofOfPurchase)
		{
			StudentMediaRequestDAO.<GetProofOfPurchaseReceiptAsync>d__45 <GetProofOfPurchaseReceiptAsync>d__ = new StudentMediaRequestDAO.<GetProofOfPurchaseReceiptAsync>d__45();
			<GetProofOfPurchaseReceiptAsync>d__.<>t__builder = AsyncTaskMethodBuilder<ProofOfPurchaseInfo>.Create();
			<GetProofOfPurchaseReceiptAsync>d__.<>4__this = this;
			<GetProofOfPurchaseReceiptAsync>d__.proofOfPurchase = proofOfPurchase;
			<GetProofOfPurchaseReceiptAsync>d__.<>1__state = -1;
			<GetProofOfPurchaseReceiptAsync>d__.<>t__builder.Start<StudentMediaRequestDAO.<GetProofOfPurchaseReceiptAsync>d__45>(ref <GetProofOfPurchaseReceiptAsync>d__);
			return <GetProofOfPurchaseReceiptAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000B4A RID: 2890 RVA: 0x00077EE0 File Offset: 0x000760E0
		private StudentMediaRequest GetStudentMediaRequestFromReader(IDataReader reader, IBatchDecryptor decryptor = null)
		{
			return new StudentMediaRequest
			{
				StudentMediaRequestId = (int)reader["StudentMediaRequestID"],
				CompletedDateTime = ((reader["CompletedDateTime"] is DBNull) ? null : new DateTime?((DateTime)reader["CompletedDateTime"])),
				Id = (int)reader["StudentMediaRequestID"],
				CreatedDatetime = (DateTime)reader["CreatedDateTime"],
				RequestMadeFromStudent = PeopleDAO.GetPersonFromReader("", reader, this.OpContext, decryptor),
				ContentRequestedList = new List<MediaContentRequestedInfo>(),
				Campus = CampusDAO.GetCampusFromReader(reader)
			};
		}

		// Token: 0x06000B4B RID: 2891 RVA: 0x00077FAC File Offset: 0x000761AC
		[DebuggerStepThrough]
		private Task<MediaContentRequestedInfoExtended> GetExtendedStudentMediaRequestInfoFromReaderAsync(IDataReader reader, OperationContext opContext, PersonBase student = null)
		{
			StudentMediaRequestDAO.<GetExtendedStudentMediaRequestInfoFromReaderAsync>d__47 <GetExtendedStudentMediaRequestInfoFromReaderAsync>d__ = new StudentMediaRequestDAO.<GetExtendedStudentMediaRequestInfoFromReaderAsync>d__47();
			<GetExtendedStudentMediaRequestInfoFromReaderAsync>d__.<>t__builder = AsyncTaskMethodBuilder<MediaContentRequestedInfoExtended>.Create();
			<GetExtendedStudentMediaRequestInfoFromReaderAsync>d__.<>4__this = this;
			<GetExtendedStudentMediaRequestInfoFromReaderAsync>d__.reader = reader;
			<GetExtendedStudentMediaRequestInfoFromReaderAsync>d__.opContext = opContext;
			<GetExtendedStudentMediaRequestInfoFromReaderAsync>d__.student = student;
			<GetExtendedStudentMediaRequestInfoFromReaderAsync>d__.<>1__state = -1;
			<GetExtendedStudentMediaRequestInfoFromReaderAsync>d__.<>t__builder.Start<StudentMediaRequestDAO.<GetExtendedStudentMediaRequestInfoFromReaderAsync>d__47>(ref <GetExtendedStudentMediaRequestInfoFromReaderAsync>d__);
			return <GetExtendedStudentMediaRequestInfoFromReaderAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000B4C RID: 2892 RVA: 0x00078008 File Offset: 0x00076208
		[DebuggerStepThrough]
		private Task<int> GetTotalFileSizeAsync(int mediaContentPerFormatId)
		{
			StudentMediaRequestDAO.<GetTotalFileSizeAsync>d__48 <GetTotalFileSizeAsync>d__ = new StudentMediaRequestDAO.<GetTotalFileSizeAsync>d__48();
			<GetTotalFileSizeAsync>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<GetTotalFileSizeAsync>d__.<>4__this = this;
			<GetTotalFileSizeAsync>d__.mediaContentPerFormatId = mediaContentPerFormatId;
			<GetTotalFileSizeAsync>d__.<>1__state = -1;
			<GetTotalFileSizeAsync>d__.<>t__builder.Start<StudentMediaRequestDAO.<GetTotalFileSizeAsync>d__48>(ref <GetTotalFileSizeAsync>d__);
			return <GetTotalFileSizeAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000B4D RID: 2893 RVA: 0x00078054 File Offset: 0x00076254
		private MediaContentRequestedInfo GetStudentMediaRequestInfoFromReader(IDataReader reader, OperationContext opContext, PersonBase student = null)
		{
			bool flag = reader["StudentMediaRequestDetailId"] is DBNull;
			MediaContentRequestedInfo result;
			if (flag)
			{
				result = null;
			}
			else
			{
				MediaContentDetail mediaContentDetail = new MediaContentDetail();
				MediaContentDetail mediaContentDetail2 = mediaContentDetail;
				BasicMediaContent basicMediaContent = new BasicMediaContent();
				basicMediaContent.MediaContentUniqueId = (Guid)reader["FKMediaContentID"];
				basicMediaContent.ShortTitle = ((reader["ShortTitle"] is DBNull) ? string.Empty : ((string)reader["ShortTitle"]));
				basicMediaContent.Edition = ((reader["Edition"] is DBNull) ? string.Empty : ((string)reader["Edition"]));
				basicMediaContent.Summary = ((reader["Summary"] is DBNull) ? string.Empty : ((string)reader["Summary"]));
				basicMediaContent.ISBN = ((reader["ISBN"] is DBNull) ? string.Empty : ((string)reader["ISBN"]));
				BasicMediaContent basicMediaContent2 = basicMediaContent;
				IList<string> authors;
				if (!(reader["Authors"] is DBNull))
				{
					authors = (from s in ((string)reader["Authors"]).Split(new char[]
					{
						'|'
					})
					where !string.IsNullOrWhiteSpace(s)
					select s).ToList<string>();
				}
				else
				{
					authors = new List<string>();
				}
				basicMediaContent2.Authors = authors;
				basicMediaContent.ProofOfPurchaseRequired = (!(reader["ProofOfPurchaseRequired"] is DBNull) && (bool)reader["ProofOfPurchaseRequired"]);
				mediaContentDetail2.MediaContent = basicMediaContent;
				mediaContentDetail.MediaContentPerFormatId = ((reader["FKMediaContentPerFormatId"] == DBNull.Value) ? 0 : Convert.ToInt32(reader["FKMediaContentPerFormatId"]));
				mediaContentDetail.MediaContentFormat = ((reader["MediaContentFormat"] is DBNull || !Enum.IsDefined(typeof(MediaContentFormat), (string)reader["MediaContentFormat"])) ? MediaContentFormat.UNSPECIFIED : ((MediaContentFormat)Enum.Parse(typeof(MediaContentFormat), (string)reader["MediaContentFormat"])));
				mediaContentDetail.StudentPreferredFormat = ((reader["StudentPreferredFormat"] is DBNull || !Enum.IsDefined(typeof(MediaContentFormat), (string)reader["StudentPreferredFormat"])) ? null : ((MediaContentFormat?)Enum.Parse(typeof(MediaContentFormat), (string)reader["StudentPreferredFormat"])));
				MediaContentDetail mediaContentDetail3 = mediaContentDetail;
				int num = (reader["ProofOfPurchaseID"] is DBNull) ? 0 : ((int)reader["ProofOfPurchaseID"]);
				ProofOfPurchaseInfo proofOfPurchase = null;
				bool flag2 = num > 0;
				if (flag2)
				{
					proofOfPurchase = new ProofOfPurchaseInfo
					{
						ProofOfPurchaseId = num,
						WhoAcceptedProofOfPurchase = PeopleDAO.GetPersonFromReader("wpop", reader, null, null),
						Extension = ((reader["Extension"] is DBNull) ? string.Empty : ((string)reader["Extension"])),
						Filename = ((reader["Filename"] is DBNull) ? string.Empty : ((string)reader["Filename"])),
						Notes = ((reader["ProofOfPurchaseNote"] is DBNull) ? string.Empty : ((string)reader["ProofOfPurchaseNote"])),
						WhenWasAccepted = ((reader["WhenWasAccepted"] is DBNull) ? null : new DateTime?((DateTime)reader["WhenWasAccepted"])),
						StudentPersonId = ((reader["RequestMadeFromStudentNo"] is DBNull) ? 0 : ((int)reader["RequestMadeFromStudentNo"])),
						MediaContentUniqueId = mediaContentDetail3.MediaContent.MediaContentUniqueId
					};
				}
				result = new MediaContentRequestedInfo
				{
					MediaContentRequestedInfoID = (int)reader["StudentMediaRequestDetailId"],
					RequestStatus = (MediaRequestStatus)Enum.Parse(typeof(MediaRequestStatus), (string)reader["Status"]),
					IsApproved = (bool)reader["IsApproved"],
					AvailableStartTime = ((reader["AvailableStartTime"] is DBNull) ? null : new DateTime?((DateTime)reader["AvailableStartTime"])),
					AvailableEndTime = ((reader["AvailableEndTime"] is DBNull) ? null : new DateTime?((DateTime)reader["AvailableEndTime"])),
					ContentDetailRequested = mediaContentDetail3,
					MediaJobId = (int)reader["FKMediaJobId"],
					MediaJobTitle = ((reader["JobTitle"] is DBNull) ? string.Empty : ((string)reader["JobTitle"])),
					StudentRequestId = (int)reader["FKStudentMediaRequestID"],
					RequestMadeFromStudent = (student ?? PeopleDAO.GetPersonFromReader("", reader, opContext, null)),
					IsCompleted = (bool)reader["IsCompleted"],
					IsCancelled = (bool)reader["IsCancelled"],
					ProofOfPurchaseId = num,
					ProofOfPurchase = proofOfPurchase,
					Campus = CampusDAO.GetCampusFromReader(reader),
					CreatedDatetime = (DateTime)reader["MediaRequestDetailCreatedDateTime"],
					CompletedDateTime = ((reader["MediaRequestDetailCompletedDateTime"] is DBNull) ? null : new DateTime?((DateTime)reader["MediaRequestDetailCompletedDateTime"])),
					CompletionNotes = ((!reader.ContainsColumn("CompletionNotes") || reader["CompletionNotes"] is DBNull) ? string.Empty : Convert.ToString(reader["CompletionNotes"]))
				};
			}
			return result;
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x000786BC File Offset: 0x000768BC
		private void AddProofOfPurchaseReceipt(int proofOfPurchaseId, byte[] receiptBytes)
		{
			DatabaseLayer clockWorkFiles = DatabaseLayerFactory.ClockWorkFiles;
			DbParameter[] parameters = new DbParameter[]
			{
				clockWorkFiles.GetParameter("@proofofpurchaseid", DbType.Int32, proofOfPurchaseId),
				clockWorkFiles.GetParameter("@imagereceipt", DbType.Binary, receiptBytes)
			};
			clockWorkFiles.ExecuteNonQuery("if exists (select 1 from AlternativeFormat_ProofOfPurchaseImage where FK_ProofOfPurchaseID = @proofofpurchaseid)\r\n\tbegin\r\n\t\tupdate AlternativeFormat_ProofOfPurchaseImage\r\n\t\tset ProofOfPurchaseReceipt = @imagereceipt,\r\n            IsActive = 1\r\n\t\twhere FK_ProofOfPurchaseID = @proofofpurchaseid\r\n\tend\r\nelse\r\n\tbegin\r\n\t\tinsert into AlternativeFormat_ProofOfPurchaseImage\r\n\t\t\t(FK_ProofOfPurchaseID, ProofOfPurchaseReceipt)\r\n\t\tvalues\r\n\t\t\t(@proofofpurchaseid, @imagereceipt)\r\nend", parameters);
		}

		// Token: 0x06000B4F RID: 2895 RVA: 0x0007870C File Offset: 0x0007690C
		[DebuggerStepThrough]
		private Task AddProofOfPurchaseReceiptAsync(int proofOfPurchaseId, byte[] receiptBytes)
		{
			StudentMediaRequestDAO.<AddProofOfPurchaseReceiptAsync>d__51 <AddProofOfPurchaseReceiptAsync>d__ = new StudentMediaRequestDAO.<AddProofOfPurchaseReceiptAsync>d__51();
			<AddProofOfPurchaseReceiptAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<AddProofOfPurchaseReceiptAsync>d__.<>4__this = this;
			<AddProofOfPurchaseReceiptAsync>d__.proofOfPurchaseId = proofOfPurchaseId;
			<AddProofOfPurchaseReceiptAsync>d__.receiptBytes = receiptBytes;
			<AddProofOfPurchaseReceiptAsync>d__.<>1__state = -1;
			<AddProofOfPurchaseReceiptAsync>d__.<>t__builder.Start<StudentMediaRequestDAO.<AddProofOfPurchaseReceiptAsync>d__51>(ref <AddProofOfPurchaseReceiptAsync>d__);
			return <AddProofOfPurchaseReceiptAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000B50 RID: 2896 RVA: 0x00078760 File Offset: 0x00076960
		private void RemoveProofOfPurchaseReceipt(int proofOfPurchaseId)
		{
			DatabaseLayer clockWorkFiles = DatabaseLayerFactory.ClockWorkFiles;
			DbParameter[] parameters = new DbParameter[]
			{
				clockWorkFiles.GetParameter("@proofofpurchaseid", DbType.Int32, proofOfPurchaseId)
			};
			clockWorkFiles.ExecuteNonQuery("update AlternativeFormat_ProofOfPurchaseImage\r\n              set IsActive = 0\r\n              where FK_ProofOfPurchaseID = @proofofpurchaseid", parameters);
		}

		// Token: 0x06000B51 RID: 2897 RVA: 0x000787A0 File Offset: 0x000769A0
		[DebuggerStepThrough]
		private Task RemoveProofOfPurchaseImageAsync(int proofOfPurchaseId)
		{
			StudentMediaRequestDAO.<RemoveProofOfPurchaseImageAsync>d__53 <RemoveProofOfPurchaseImageAsync>d__ = new StudentMediaRequestDAO.<RemoveProofOfPurchaseImageAsync>d__53();
			<RemoveProofOfPurchaseImageAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<RemoveProofOfPurchaseImageAsync>d__.<>4__this = this;
			<RemoveProofOfPurchaseImageAsync>d__.proofOfPurchaseId = proofOfPurchaseId;
			<RemoveProofOfPurchaseImageAsync>d__.<>1__state = -1;
			<RemoveProofOfPurchaseImageAsync>d__.<>t__builder.Start<StudentMediaRequestDAO.<RemoveProofOfPurchaseImageAsync>d__53>(ref <RemoveProofOfPurchaseImageAsync>d__);
			return <RemoveProofOfPurchaseImageAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000B52 RID: 2898 RVA: 0x000787EC File Offset: 0x000769EC
		private void RemoveProofOfPurchaseImage(IList<int> proofOfPurchaseIdList)
		{
			DatabaseLayer clockWorkFiles = DatabaseLayerFactory.ClockWorkFiles;
			DbParameter[] parameters = new DbParameter[]
			{
				clockWorkFiles.GetParameter("@proofofpurchaseidlist", DbType.Int32, proofOfPurchaseIdList.CommaSeparatedValuesWithoutSpace<int>())
			};
			clockWorkFiles.ExecuteNonQuery("update AlternativeFormat_ProofOfPurchaseImage\r\n              set IsActive = 0\r\n              where FK_ProofOfPurchaseID in (SELECT OrderID as FK_ProofOfPurchaseID from SplitOrderIDs(@proofofpurchaseidlist, ','))", parameters);
		}
	}
}
