using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.Common.DAO.OnlineForms;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.OnlineForms;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.DAO.Impl.OnlineForms
{
	// Token: 0x02000080 RID: 128
	public class OnlineFormQueueDAO : IOnlineFormQueueDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000332 RID: 818 RVA: 0x0000ED1A File Offset: 0x0000CF1A
		public OnlineFormQueueDAO()
		{
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0001BCE2 File Offset: 0x00019EE2
		public OnlineFormQueueDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000334 RID: 820 RVA: 0x0001BCF4 File Offset: 0x00019EF4
		// (set) Token: 0x06000335 RID: 821 RVA: 0x0001BCFC File Offset: 0x00019EFC
		public OperationContext OpContext { get; set; }

		// Token: 0x06000336 RID: 822 RVA: 0x0001BD08 File Offset: 0x00019F08
		[DebuggerStepThrough]
		public Task<bool> DeleteOnlineFormQueueItemAsync(int peopleOnlineFormId)
		{
			OnlineFormQueueDAO.<DeleteOnlineFormQueueItemAsync>d__6 <DeleteOnlineFormQueueItemAsync>d__ = new OnlineFormQueueDAO.<DeleteOnlineFormQueueItemAsync>d__6();
			<DeleteOnlineFormQueueItemAsync>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<DeleteOnlineFormQueueItemAsync>d__.<>4__this = this;
			<DeleteOnlineFormQueueItemAsync>d__.peopleOnlineFormId = peopleOnlineFormId;
			<DeleteOnlineFormQueueItemAsync>d__.<>1__state = -1;
			<DeleteOnlineFormQueueItemAsync>d__.<>t__builder.Start<OnlineFormQueueDAO.<DeleteOnlineFormQueueItemAsync>d__6>(ref <DeleteOnlineFormQueueItemAsync>d__);
			return <DeleteOnlineFormQueueItemAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0001BD54 File Offset: 0x00019F54
		public bool DeleteOnlineFormQueueItem(int peopleOnlineFormId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@id", DbType.Int32, peopleOnlineFormId)
			};
			databaseLayer.ExecuteNonQuery("UPDATE people_onlineform Set IsDeleted=1 WHERE people_onlineFormId=@id", parameters);
			return true;
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0001BDAC File Offset: 0x00019FAC
		[DebuggerStepThrough]
		public Task<IList<OnlineFormStatus>> LoadLookupOnlineFormStatusesAsync()
		{
			OnlineFormQueueDAO.<LoadLookupOnlineFormStatusesAsync>d__8 <LoadLookupOnlineFormStatusesAsync>d__ = new OnlineFormQueueDAO.<LoadLookupOnlineFormStatusesAsync>d__8();
			<LoadLookupOnlineFormStatusesAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<OnlineFormStatus>>.Create();
			<LoadLookupOnlineFormStatusesAsync>d__.<>4__this = this;
			<LoadLookupOnlineFormStatusesAsync>d__.<>1__state = -1;
			<LoadLookupOnlineFormStatusesAsync>d__.<>t__builder.Start<OnlineFormQueueDAO.<LoadLookupOnlineFormStatusesAsync>d__8>(ref <LoadLookupOnlineFormStatusesAsync>d__);
			return <LoadLookupOnlineFormStatusesAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0001BDF0 File Offset: 0x00019FF0
		public IList<OnlineFormStatus> LoadLookupOnlineFormStatuses()
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			IList<OnlineFormStatus> result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT PeopleOnlineFormStatusId,Title,StatusTypeId FROM people_onlineform_status WHERE IsDisabled=0 ORDER BY StatusTypeId,Title"))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<OnlineFormStatus> list = new List<OnlineFormStatus>();
					while (dataReader.Read())
					{
						int num = (dataReader["PeopleOnlineFormStatusId"] is DBNull) ? 0 : Convert.ToInt32(dataReader["PeopleOnlineFormStatusId"]);
						bool flag2 = num < 1;
						if (!flag2)
						{
							int num2 = (dataReader["StatusTypeId"] is DBNull) ? 0 : ((int)dataReader["StatusTypeId"]);
							list.Add(new OnlineFormStatus
							{
								PeopleOnlineFormStatusId = num,
								Title = dataReader["Title"].ToString(),
								StatusType = (eOnlineFormStatusType)(Enum.IsDefined(typeof(eOnlineFormStatusType), num2) ? num2 : 0)
							});
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0001BF24 File Offset: 0x0001A124
		[DebuggerStepThrough]
		public Task<int?> LoadOnlineFormIdByPeopleOnlineFormIdAsync(int peopleOnlineFormId)
		{
			OnlineFormQueueDAO.<LoadOnlineFormIdByPeopleOnlineFormIdAsync>d__10 <LoadOnlineFormIdByPeopleOnlineFormIdAsync>d__ = new OnlineFormQueueDAO.<LoadOnlineFormIdByPeopleOnlineFormIdAsync>d__10();
			<LoadOnlineFormIdByPeopleOnlineFormIdAsync>d__.<>t__builder = AsyncTaskMethodBuilder<int?>.Create();
			<LoadOnlineFormIdByPeopleOnlineFormIdAsync>d__.<>4__this = this;
			<LoadOnlineFormIdByPeopleOnlineFormIdAsync>d__.peopleOnlineFormId = peopleOnlineFormId;
			<LoadOnlineFormIdByPeopleOnlineFormIdAsync>d__.<>1__state = -1;
			<LoadOnlineFormIdByPeopleOnlineFormIdAsync>d__.<>t__builder.Start<OnlineFormQueueDAO.<LoadOnlineFormIdByPeopleOnlineFormIdAsync>d__10>(ref <LoadOnlineFormIdByPeopleOnlineFormIdAsync>d__);
			return <LoadOnlineFormIdByPeopleOnlineFormIdAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0001BF70 File Offset: 0x0001A170
		public int? LoadOnlineFormIdByPeopleOnlineFormId(int peopleOnlineFormId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@peopleOnlineFormId", DbType.Int32, peopleOnlineFormId)
			};
			object obj = databaseLayer.ExecuteScalar("SELECT onlineformid FROM people_onlineform WHERE people_onlineFormId=@peopleOnlineFormId", parameters);
			return (obj != null && obj is int) ? new int?(Convert.ToInt32(obj)) : null;
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0001BFE8 File Offset: 0x0001A1E8
		[DebuggerStepThrough]
		public Task<OnlineFormQueueItem> LoadOnlineFormQueueItemAsync(int peopleOnlineFormId)
		{
			OnlineFormQueueDAO.<LoadOnlineFormQueueItemAsync>d__12 <LoadOnlineFormQueueItemAsync>d__ = new OnlineFormQueueDAO.<LoadOnlineFormQueueItemAsync>d__12();
			<LoadOnlineFormQueueItemAsync>d__.<>t__builder = AsyncTaskMethodBuilder<OnlineFormQueueItem>.Create();
			<LoadOnlineFormQueueItemAsync>d__.<>4__this = this;
			<LoadOnlineFormQueueItemAsync>d__.peopleOnlineFormId = peopleOnlineFormId;
			<LoadOnlineFormQueueItemAsync>d__.<>1__state = -1;
			<LoadOnlineFormQueueItemAsync>d__.<>t__builder.Start<OnlineFormQueueDAO.<LoadOnlineFormQueueItemAsync>d__12>(ref <LoadOnlineFormQueueItemAsync>d__);
			return <LoadOnlineFormQueueItemAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0001C034 File Offset: 0x0001A234
		public OnlineFormQueueItem LoadOnlineFormQueueItem(int peopleOnlineFormId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@peopleOnlineFormId", DbType.Int32, peopleOnlineFormId)
			};
			OnlineFormQueueItem result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT\tps.people_onlineFormId AS peopleOnlineFormId,ps.personid,ps.onlineformid,ps.DateEntered,ps.FK_PeopleOnlineFormStatusId AS statusid,\r\n\t\ts.title AS OnlineFormTitle,s.[Description] AS OnlineFormDescription,s.FormNum AS screennum,ShortCode AS OnlineFormShortCode,\r\n\t\tp.student_no,p.firstName,p.middleName,p.lastName,\r\n\t\tc.email,c.emailisnotencrypted,c.assignedcounsellorpid,c.assignedcounsellorfirst,c.assignedcounsellorlast,\r\n        pss.title,pss.StatusTypeId,ps.StaffNote\r\nFROM\tpeople_onlineform ps LEFT JOIN people_onlineform_status pss ON pss.PeopleOnlineFormStatusId=ps.FK_PeopleOnlineFormStatusId\r\n        LEFT JOIN onlineform s ON s.OnlineFormId=ps.onlineformid\r\n\t\tLEFT JOIN people p ON p.PersonID=ps.personid\r\n\t\tLEFT JOIN [common] c ON c.personid=ps.personid\r\nWHERE ps.people_onlineformId=@peopleOnlineFormId", parameters))
			{
				bool flag = dataReader == null || !dataReader.Read();
				if (flag)
				{
					result = null;
				}
				else
				{
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					result = this.GetOnlineFormQueueItemFromRecord(dataReader, batchDecryptor);
				}
			}
			return result;
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0001C0D8 File Offset: 0x0001A2D8
		private OnlineFormQueueItem GetOnlineFormQueueItemFromRecord(IDataRecord record, IBatchDecryptor batchDecryptor)
		{
			int num = (record["peopleOnlineFormId"] is DBNull) ? 0 : Convert.ToInt32(record["peopleOnlineFormId"]);
			bool flag = num < 1;
			OnlineFormQueueItem result;
			if (flag)
			{
				result = null;
			}
			else
			{
				int num2 = (record["StatusTypeId"] is DBNull) ? 0 : ((int)record["StatusTypeId"]);
				int num3 = (record["assignedcounsellorpid"] is DBNull) ? 0 : ((int)record["assignedcounsellorpid"]);
				bool flag2 = record["emailisnotencrypted"] is DBNull || !Convert.ToBoolean(record["emailisnotencrypted"]);
				result = new OnlineFormQueueItem
				{
					PeopleOnlineFormId = num,
					StaffNote = ((record["StaffNote"] is DBNull) ? null : batchDecryptor.Decrypt((byte[])record["StaffNote"])),
					OnlineForm = new OnlineFormForDisplay
					{
						OnlineFormId = ((record["OnlineFormId"] is DBNull) ? 0 : Convert.ToInt32(record["OnlineFormId"])),
						Title = record["OnlineFormTitle"].ToString(),
						Description = record["OnlineFormDescription"].ToString(),
						ScreenNum = ((record["screennum"] is DBNull) ? 0 : ((int)record["screennum"])),
						ShortCode = record["OnlineFormShortCode"].ToString()
					},
					Student = new BasicPerson
					{
						PersonId = ((record["personid"] is DBNull) ? 0 : Convert.ToInt32(record["personid"])),
						FirstName = ((record["firstname"] is DBNull) ? string.Empty : batchDecryptor.Decrypt((byte[])record["firstname"]).Trim()),
						MiddleName = ((record["middlename"] is DBNull) ? string.Empty : batchDecryptor.Decrypt((byte[])record["middlename"]).Trim()),
						LastName = ((record["lastname"] is DBNull) ? string.Empty : batchDecryptor.Decrypt((byte[])record["lastname"]).Trim()),
						StudentNumber = ((record["student_no"] is DBNull) ? string.Empty : batchDecryptor.Decrypt((byte[])record["student_no"]).Trim())
					},
					Status = new OnlineFormStatus
					{
						PeopleOnlineFormStatusId = ((record["statusid"] is DBNull) ? 0 : ((int)record["statusid"])),
						Title = record["title"].ToString(),
						StatusType = (eOnlineFormStatusType)(Enum.IsDefined(typeof(eOnlineFormStatusType), num2) ? num2 : 0)
					},
					DateEntered = ((record["dateentered"] is DBNull) ? DateTime.MinValue : ((DateTime)record["dateentered"])),
					AssignedCounsellor = ((num3 < 1) ? null : new BasicPerson
					{
						PersonId = num3,
						FirstName = ((record["assignedcounsellorfirst"] is DBNull) ? string.Empty : batchDecryptor.Decrypt((byte[])record["assignedcounsellorfirst"]).Trim()),
						LastName = ((record["assignedcounsellorlast"] is DBNull) ? string.Empty : batchDecryptor.Decrypt((byte[])record["assignedcounsellorlast"]).Trim())
					}),
					StudentEmail = ((record["email"] is DBNull) ? null : (flag2 ? batchDecryptor.Decrypt((byte[])record["email"]) : Encoding.ASCII.GetString((byte[])record["email"])))
				};
			}
			return result;
		}

		// Token: 0x0600033F RID: 831 RVA: 0x0001C558 File Offset: 0x0001A758
		[DebuggerStepThrough]
		public Task<IList<OnlineFormIdWithOpenItemsCount>> LoadOnlineFormQueueFormsWithOpenItemsCountAsync(IList<int> onlineFormIds, DateTime startDate, DateTime? endDate, int filterByAssignedCounsellorPid, params int[] statusIdsToExclude)
		{
			OnlineFormQueueDAO.<LoadOnlineFormQueueFormsWithOpenItemsCountAsync>d__15 <LoadOnlineFormQueueFormsWithOpenItemsCountAsync>d__ = new OnlineFormQueueDAO.<LoadOnlineFormQueueFormsWithOpenItemsCountAsync>d__15();
			<LoadOnlineFormQueueFormsWithOpenItemsCountAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<OnlineFormIdWithOpenItemsCount>>.Create();
			<LoadOnlineFormQueueFormsWithOpenItemsCountAsync>d__.<>4__this = this;
			<LoadOnlineFormQueueFormsWithOpenItemsCountAsync>d__.onlineFormIds = onlineFormIds;
			<LoadOnlineFormQueueFormsWithOpenItemsCountAsync>d__.startDate = startDate;
			<LoadOnlineFormQueueFormsWithOpenItemsCountAsync>d__.endDate = endDate;
			<LoadOnlineFormQueueFormsWithOpenItemsCountAsync>d__.filterByAssignedCounsellorPid = filterByAssignedCounsellorPid;
			<LoadOnlineFormQueueFormsWithOpenItemsCountAsync>d__.statusIdsToExclude = statusIdsToExclude;
			<LoadOnlineFormQueueFormsWithOpenItemsCountAsync>d__.<>1__state = -1;
			<LoadOnlineFormQueueFormsWithOpenItemsCountAsync>d__.<>t__builder.Start<OnlineFormQueueDAO.<LoadOnlineFormQueueFormsWithOpenItemsCountAsync>d__15>(ref <LoadOnlineFormQueueFormsWithOpenItemsCountAsync>d__);
			return <LoadOnlineFormQueueFormsWithOpenItemsCountAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0001C5C4 File Offset: 0x0001A7C4
		public IList<OnlineFormIdWithOpenItemsCount> LoadOnlineFormQueueFormsWithOpenItemsCount(IList<int> onlineFormIds, DateTime startDate, DateTime? endDate, int filterByAssignedCounsellorPid, params int[] statusIdsToExclude)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] array = new DbParameter[5];
			int num = 0;
			DatabaseLayer databaseLayer2 = databaseLayer;
			string pName = "@onlineformids";
			DbType pType = DbType.String;
			string separator = ",";
			IEnumerable<string> enumerable;
			if (onlineFormIds == null)
			{
				enumerable = null;
			}
			else
			{
				enumerable = from g in onlineFormIds
				select g.ToString();
			}
			array[num] = databaseLayer2.GetParameter(pName, pType, string.Join(separator, enumerable ?? new List<string>()));
			array[1] = databaseLayer.GetParameter("@startdate", DbType.DateTime, startDate);
			array[2] = databaseLayer.GetParameter("@enddate", DbType.DateTime, (endDate != null) ? endDate.Value : DBNull.Value);
			int num2 = 3;
			DatabaseLayer databaseLayer3 = databaseLayer;
			string pName2 = "@excludeStatusIds";
			DbType pType2 = DbType.String;
			object value;
			if (statusIdsToExclude != null)
			{
				value = string.Join(",", from g in statusIdsToExclude
				select g.ToString());
			}
			else
			{
				value = DBNull.Value;
			}
			array[num2] = databaseLayer3.GetParameter(pName2, pType2, value);
			array[4] = databaseLayer.GetParameter("@filterCounsellorPid", DbType.Int32, filterByAssignedCounsellorPid);
			DbParameter[] parameters = array;
			IList<OnlineFormIdWithOpenItemsCount> result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT orderid AS FK_PeopleOnlineFormStatusId INTO #t1 FROM SplitOrderIDs(COALESCE(@excludeStatusIds,''),',')\r\nSELECT orderid AS onlineformid INTO #t2 FROM SplitOrderIDs(COALESCE(@onlineFormIds,''),',')\r\n\r\nDECLARE @sd datetime = DATEADD(D, 0, DATEDIFF(D, 0, @startdate))\r\nDECLARE @ed datetime = CASE WHEN @enddate IS NULL THEN NULL ELSE DATEADD(D, 1, DATEDIFF(D, 0, @enddate)) END\r\n\r\nSELECT DISTINCT ps.onlineformid,COUNT(ps.people_onlineformid) AS NumOpen\r\nFROM\tpeople_onlineform ps\r\nWHERE   ps.onlineformid IN (SELECT onlineformid FROM #t2)\r\n\t\tAND ps.isdeleted=0 \r\n\t\tAND ps.DateEntered>=@sd \r\n\t\tAND (@ed IS NULL OR ps.DateEntered<@ed)\r\n\t\tAND (ps.FK_PeopleOnlineFormStatusId IS NULL OR NOT ps.FK_PeopleOnlineFormStatusId IN (SELECT FK_PeopleOnlineFormStatusId FROM #t1))\r\n\t\tAND (@filterCounsellorPid IS NULL OR @filterCounsellorPid<1 OR ps.personid IN (SELECT c.personid FROM common c WHERE c.assignedcounsellorpid=@filterCounsellorPid))\r\nGROUP BY ps.onlineformid\r\n\r\nDROP TABLE #t1\r\nDROP TABLE #t2", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<OnlineFormIdWithOpenItemsCount> list = new List<OnlineFormIdWithOpenItemsCount>();
					while (dataReader.Read())
					{
						int num3 = (dataReader["onlineformid"] is DBNull) ? 0 : Convert.ToInt32(dataReader["onlineformid"]);
						bool flag2 = num3 < 1;
						if (!flag2)
						{
							int openItemsCount = (dataReader["NumOpen"] is DBNull) ? 0 : Convert.ToInt32(dataReader["NumOpen"]);
							list.Add(new OnlineFormIdWithOpenItemsCount
							{
								OnlineFormId = num3,
								OpenItemsCount = openItemsCount
							});
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0001C7B8 File Offset: 0x0001A9B8
		[DebuggerStepThrough]
		public Task<IList<OnlineFormQueueItem>> LoadOnlineFormQueueItemsAsync(int onlineFormId, DateTime startDate, DateTime? endDate, int filterByAssignedCounsellorPid, params int[] statusIdsToExclude)
		{
			OnlineFormQueueDAO.<LoadOnlineFormQueueItemsAsync>d__17 <LoadOnlineFormQueueItemsAsync>d__ = new OnlineFormQueueDAO.<LoadOnlineFormQueueItemsAsync>d__17();
			<LoadOnlineFormQueueItemsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<OnlineFormQueueItem>>.Create();
			<LoadOnlineFormQueueItemsAsync>d__.<>4__this = this;
			<LoadOnlineFormQueueItemsAsync>d__.onlineFormId = onlineFormId;
			<LoadOnlineFormQueueItemsAsync>d__.startDate = startDate;
			<LoadOnlineFormQueueItemsAsync>d__.endDate = endDate;
			<LoadOnlineFormQueueItemsAsync>d__.filterByAssignedCounsellorPid = filterByAssignedCounsellorPid;
			<LoadOnlineFormQueueItemsAsync>d__.statusIdsToExclude = statusIdsToExclude;
			<LoadOnlineFormQueueItemsAsync>d__.<>1__state = -1;
			<LoadOnlineFormQueueItemsAsync>d__.<>t__builder.Start<OnlineFormQueueDAO.<LoadOnlineFormQueueItemsAsync>d__17>(ref <LoadOnlineFormQueueItemsAsync>d__);
			return <LoadOnlineFormQueueItemsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0001C824 File Offset: 0x0001AA24
		public IList<OnlineFormQueueItem> LoadOnlineFormQueueItems(int onlineFormId, DateTime startDate, DateTime? endDate, int filterByAssignedCounsellorPid, params int[] statusIdsToExclude)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] array = new DbParameter[5];
			array[0] = databaseLayer.GetParameter("@onlineformid", DbType.Int32, onlineFormId);
			array[1] = databaseLayer.GetParameter("@startdate", DbType.DateTime, startDate);
			array[2] = databaseLayer.GetParameter("@enddate", DbType.DateTime, (endDate != null) ? endDate.Value : DBNull.Value);
			int num = 3;
			DatabaseLayer databaseLayer2 = databaseLayer;
			string pName = "@excludeStatusIds";
			DbType pType = DbType.String;
			object value;
			if (statusIdsToExclude != null)
			{
				value = string.Join(",", from g in statusIdsToExclude
				select g.ToString());
			}
			else
			{
				value = DBNull.Value;
			}
			array[num] = databaseLayer2.GetParameter(pName, pType, value);
			array[4] = databaseLayer.GetParameter("@filterCounsellorPid", DbType.Int32, filterByAssignedCounsellorPid);
			DbParameter[] parameters = array;
			IList<OnlineFormQueueItem> result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT orderid AS FK_PeopleOnlineFormStatusId INTO #t1 FROM SplitOrderIDs(COALESCE(@excludeStatusIds,''),',')\r\n\r\nDECLARE @sd datetime = DATEADD(D, 0, DATEDIFF(D, 0, @startdate))\r\nDECLARE @ed datetime = CASE WHEN @enddate IS NULL THEN NULL ELSE DATEADD(D, 1, DATEDIFF(D, 0, @enddate)) END\r\n\r\nSELECT\tps.people_onlineformId AS peopleOnlineFormId,ps.personid,ps.onlineformid,ps.DateEntered,ps.FK_PeopleOnlineFormStatusId AS statusid,\r\n\t\ts.title AS OnlineFormTitle,s.[Description] AS OnlineFormDescription,s.FormNum AS screennum,ShortCode AS OnlineFormShortCode,\r\n\t\tp.student_no,p.firstName,p.middleName,p.lastName,\r\n\t\tc.email,c.emailisnotencrypted,c.assignedcounsellorpid,c.assignedcounsellorfirst,c.assignedcounsellorlast,\r\n        pss.title,pss.StatusTypeId,ps.StaffNote\r\nFROM\tpeople_onlineform ps LEFT JOIN people_onlineform_status pss ON pss.PeopleOnlineFormStatusId=ps.FK_PeopleOnlineFormStatusId\r\n        LEFT JOIN onlineform s ON s.OnlineFormId=ps.onlineformid\r\n\t\tLEFT JOIN people p ON p.PersonID=ps.personid\r\n\t\tLEFT JOIN [common] c ON c.personid=ps.personid\r\nWHERE\tps.onlineformid=@onlineformid AND ps.isdeleted=0 \r\n\t\tAND ps.DateEntered>=@sd \r\n\t\tAND (@ed IS NULL OR ps.DateEntered<@ed)\r\n\t\tAND (ps.FK_PeopleOnlineFormStatusId IS NULL OR NOT ps.FK_PeopleOnlineFormStatusId IN (SELECT FK_PeopleOnlineFormStatusId FROM #t1))\r\n\t\tAND (@filterCounsellorPid IS NULL OR @filterCounsellorPid<1 OR c.assignedcounsellorpid=@filterCounsellorPid)\r\n\r\nDROP TABLE #t1", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					List<OnlineFormQueueItem> list = new List<OnlineFormQueueItem>();
					while (dataReader.Read())
					{
						OnlineFormQueueItem onlineFormQueueItemFromRecord = this.GetOnlineFormQueueItemFromRecord(dataReader, batchDecryptor);
						bool flag2 = onlineFormQueueItemFromRecord == null;
						if (!flag2)
						{
							list.Add(onlineFormQueueItemFromRecord);
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0001C990 File Offset: 0x0001AB90
		[DebuggerStepThrough]
		public Task<OnlineFormQueueItem> UpdateOnlineFormQueueItemStatusAsync(int peopleOnlineFormId, int? newPeopleOnlineFormStatusId)
		{
			OnlineFormQueueDAO.<UpdateOnlineFormQueueItemStatusAsync>d__19 <UpdateOnlineFormQueueItemStatusAsync>d__ = new OnlineFormQueueDAO.<UpdateOnlineFormQueueItemStatusAsync>d__19();
			<UpdateOnlineFormQueueItemStatusAsync>d__.<>t__builder = AsyncTaskMethodBuilder<OnlineFormQueueItem>.Create();
			<UpdateOnlineFormQueueItemStatusAsync>d__.<>4__this = this;
			<UpdateOnlineFormQueueItemStatusAsync>d__.peopleOnlineFormId = peopleOnlineFormId;
			<UpdateOnlineFormQueueItemStatusAsync>d__.newPeopleOnlineFormStatusId = newPeopleOnlineFormStatusId;
			<UpdateOnlineFormQueueItemStatusAsync>d__.<>1__state = -1;
			<UpdateOnlineFormQueueItemStatusAsync>d__.<>t__builder.Start<OnlineFormQueueDAO.<UpdateOnlineFormQueueItemStatusAsync>d__19>(ref <UpdateOnlineFormQueueItemStatusAsync>d__);
			return <UpdateOnlineFormQueueItemStatusAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0001C9E4 File Offset: 0x0001ABE4
		public OnlineFormQueueItem UpdateOnlineFormQueueItemStatus(int peopleOnlineFormId, int? newPeopleOnlineFormStatusId)
		{
			bool flag = peopleOnlineFormId < 1;
			OnlineFormQueueItem result;
			if (flag)
			{
				result = null;
			}
			else
			{
				eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
				OperationContext opContext = this.OpContext;
				DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
				DbParameter[] parameters = new DbParameter[]
				{
					databaseLayer.GetParameter("@peopleOnlineFormId", DbType.Int32, peopleOnlineFormId),
					databaseLayer.GetParameter("@peopleOnlineFormStatusId", DbType.Int32, (newPeopleOnlineFormStatusId != null) ? newPeopleOnlineFormStatusId.Value : DBNull.Value)
				};
				databaseLayer.ExecuteNonQuery("UPDATE people_onlineform SET FK_PeopleOnlineFormStatusId=@peopleOnlineFormStatusId WHERE people_onlineformid=@peopleOnlineFormId", parameters);
				result = this.LoadOnlineFormQueueItem(peopleOnlineFormId);
			}
			return result;
		}

		// Token: 0x06000345 RID: 837 RVA: 0x0001CA78 File Offset: 0x0001AC78
		[DebuggerStepThrough]
		public Task<OnlineFormQueueItem> UpdateOnlineFormQueueItemStaffNoteAsync(int peopleOnlineFormId, string newStaffNote)
		{
			OnlineFormQueueDAO.<UpdateOnlineFormQueueItemStaffNoteAsync>d__21 <UpdateOnlineFormQueueItemStaffNoteAsync>d__ = new OnlineFormQueueDAO.<UpdateOnlineFormQueueItemStaffNoteAsync>d__21();
			<UpdateOnlineFormQueueItemStaffNoteAsync>d__.<>t__builder = AsyncTaskMethodBuilder<OnlineFormQueueItem>.Create();
			<UpdateOnlineFormQueueItemStaffNoteAsync>d__.<>4__this = this;
			<UpdateOnlineFormQueueItemStaffNoteAsync>d__.peopleOnlineFormId = peopleOnlineFormId;
			<UpdateOnlineFormQueueItemStaffNoteAsync>d__.newStaffNote = newStaffNote;
			<UpdateOnlineFormQueueItemStaffNoteAsync>d__.<>1__state = -1;
			<UpdateOnlineFormQueueItemStaffNoteAsync>d__.<>t__builder.Start<OnlineFormQueueDAO.<UpdateOnlineFormQueueItemStaffNoteAsync>d__21>(ref <UpdateOnlineFormQueueItemStaffNoteAsync>d__);
			return <UpdateOnlineFormQueueItemStaffNoteAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0001CACC File Offset: 0x0001ACCC
		public OnlineFormQueueItem UpdateOnlineFormQueueItemStaffNote(int peopleOnlineFormId, string newStaffNote)
		{
			bool flag = peopleOnlineFormId < 1;
			OnlineFormQueueItem result;
			if (flag)
			{
				result = null;
			}
			else
			{
				eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
				OperationContext opContext = this.OpContext;
				DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
				DbParameter[] parameters = new DbParameter[]
				{
					databaseLayer.GetParameter("@peopleOnlineFormId", DbType.Int32, peopleOnlineFormId),
					databaseLayer.GetParameter("@staffnote", DbType.Binary, string.IsNullOrWhiteSpace(newStaffNote) ? DBNull.Value : databaseLayer.Encryption.Encrypt(newStaffNote))
				};
				databaseLayer.ExecuteNonQuery("UPDATE people_onlineform SET StaffNote=@staffnote WHERE people_onlineformid=@peopleOnlineFormId", parameters);
				result = this.LoadOnlineFormQueueItem(peopleOnlineFormId);
			}
			return result;
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0001CB60 File Offset: 0x0001AD60
		[DebuggerStepThrough]
		public Task<OnlineFormQueueItem> UpdateOnlineFormQueueItemStaffNoteAndStatusAsync(int peopleOnlineFormId, int? newPeopleOnlineFormStatusId, string newStaffNote)
		{
			OnlineFormQueueDAO.<UpdateOnlineFormQueueItemStaffNoteAndStatusAsync>d__23 <UpdateOnlineFormQueueItemStaffNoteAndStatusAsync>d__ = new OnlineFormQueueDAO.<UpdateOnlineFormQueueItemStaffNoteAndStatusAsync>d__23();
			<UpdateOnlineFormQueueItemStaffNoteAndStatusAsync>d__.<>t__builder = AsyncTaskMethodBuilder<OnlineFormQueueItem>.Create();
			<UpdateOnlineFormQueueItemStaffNoteAndStatusAsync>d__.<>4__this = this;
			<UpdateOnlineFormQueueItemStaffNoteAndStatusAsync>d__.peopleOnlineFormId = peopleOnlineFormId;
			<UpdateOnlineFormQueueItemStaffNoteAndStatusAsync>d__.newPeopleOnlineFormStatusId = newPeopleOnlineFormStatusId;
			<UpdateOnlineFormQueueItemStaffNoteAndStatusAsync>d__.newStaffNote = newStaffNote;
			<UpdateOnlineFormQueueItemStaffNoteAndStatusAsync>d__.<>1__state = -1;
			<UpdateOnlineFormQueueItemStaffNoteAndStatusAsync>d__.<>t__builder.Start<OnlineFormQueueDAO.<UpdateOnlineFormQueueItemStaffNoteAndStatusAsync>d__23>(ref <UpdateOnlineFormQueueItemStaffNoteAndStatusAsync>d__);
			return <UpdateOnlineFormQueueItemStaffNoteAndStatusAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0001CBBC File Offset: 0x0001ADBC
		public OnlineFormQueueItem UpdateOnlineFormQueueItemStaffNoteAndStatus(int peopleOnlineFormId, int? newPeopleOnlineFormStatusId, string newStaffNote)
		{
			bool flag = peopleOnlineFormId < 1;
			OnlineFormQueueItem result;
			if (flag)
			{
				result = null;
			}
			else
			{
				eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
				OperationContext opContext = this.OpContext;
				DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
				DbParameter[] parameters = new DbParameter[]
				{
					databaseLayer.GetParameter("@peopleOnlineFormId", DbType.Int32, peopleOnlineFormId),
					databaseLayer.GetParameter("@peopleOnlineFormStatusId", DbType.Int32, (newPeopleOnlineFormStatusId != null) ? newPeopleOnlineFormStatusId.Value : DBNull.Value),
					databaseLayer.GetParameter("@staffnote", DbType.Binary, string.IsNullOrWhiteSpace(newStaffNote) ? DBNull.Value : databaseLayer.Encryption.Encrypt(newStaffNote))
				};
				databaseLayer.ExecuteNonQuery("UPDATE people_onlineform SET FK_PeopleOnlineFormStatusId=@peopleOnlineFormStatusId,StaffNote=@staffnote WHERE people_onlineformid=@peopleOnlineFormId", parameters);
				result = this.LoadOnlineFormQueueItem(peopleOnlineFormId);
			}
			return result;
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0001CC80 File Offset: 0x0001AE80
		[DebuggerStepThrough]
		public Task<IList<OnlineFormQueueItem>> LoadAllStudentOnlineFormsAsync(int studentPersonId)
		{
			OnlineFormQueueDAO.<LoadAllStudentOnlineFormsAsync>d__25 <LoadAllStudentOnlineFormsAsync>d__ = new OnlineFormQueueDAO.<LoadAllStudentOnlineFormsAsync>d__25();
			<LoadAllStudentOnlineFormsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<OnlineFormQueueItem>>.Create();
			<LoadAllStudentOnlineFormsAsync>d__.<>4__this = this;
			<LoadAllStudentOnlineFormsAsync>d__.studentPersonId = studentPersonId;
			<LoadAllStudentOnlineFormsAsync>d__.<>1__state = -1;
			<LoadAllStudentOnlineFormsAsync>d__.<>t__builder.Start<OnlineFormQueueDAO.<LoadAllStudentOnlineFormsAsync>d__25>(ref <LoadAllStudentOnlineFormsAsync>d__);
			return <LoadAllStudentOnlineFormsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0001CCCC File Offset: 0x0001AECC
		public IList<OnlineFormQueueItem> LoadAllStudentOnlineForms(int studentPersonId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@pid", DbType.Int32, studentPersonId)
			};
			IList<OnlineFormQueueItem> result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT\tps.people_onlineformId AS peopleOnlineFormId,ps.personid,ps.onlineformid,ps.DateEntered,ps.FK_PeopleOnlineFormStatusId AS statusid,\r\n\t\ts.title AS OnlineFormTitle,s.[Description] AS OnlineFormDescription,s.FormNum AS screennum,ShortCode AS OnlineFormShortCode,\r\n\t\tp.student_no,p.firstName,p.middleName,p.lastName,\r\n\t\tc.email,c.emailisnotencrypted,c.assignedcounsellorpid,c.assignedcounsellorfirst,c.assignedcounsellorlast,\r\n        pss.title,pss.StatusTypeId,ps.StaffNote\r\nFROM\tpeople_onlineform ps LEFT JOIN people_onlineform_status pss ON pss.PeopleOnlineFormStatusId=ps.FK_PeopleOnlineFormStatusId\r\n        LEFT JOIN onlineform s ON s.OnlineFormId=ps.onlineformid\r\n\t\tLEFT JOIN people p ON p.PersonID=ps.personid\r\n\t\tLEFT JOIN [common] c ON c.personid=ps.personid\r\nWHERE\tps.isdeleted=0 \r\n\t\tAND ps.personid=@pid\r\nORDER BY ps.DateEntered DESC", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					List<OnlineFormQueueItem> list = new List<OnlineFormQueueItem>();
					while (dataReader.Read())
					{
						OnlineFormQueueItem onlineFormQueueItemFromRecord = this.GetOnlineFormQueueItemFromRecord(dataReader, batchDecryptor);
						bool flag2 = onlineFormQueueItemFromRecord == null;
						if (!flag2)
						{
							list.Add(onlineFormQueueItemFromRecord);
						}
					}
					result = list;
				}
			}
			return result;
		}
	}
}
