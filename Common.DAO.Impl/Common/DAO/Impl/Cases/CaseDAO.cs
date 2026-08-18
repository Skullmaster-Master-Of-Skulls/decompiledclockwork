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
using TechnoPro.Common.DAO.Cases;
using TechnoPro.Common.DAO.DynamicForms;
using TechnoPro.Common.DAO.Impl.Appointments;
using TechnoPro.Common.DAO.Impl.DynamicForms;
using TechnoPro.Common.DAO.Impl.People;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.Cases;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.People;
using TechnoPro.Common.Public.Exceptions.DatabaseOperations;

namespace TechnoPro.Common.DAO.Impl.Cases
{
	// Token: 0x02000117 RID: 279
	public class CaseDAO : ICaseDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060007F9 RID: 2041 RVA: 0x00052316 File Offset: 0x00050516
		// (set) Token: 0x060007FA RID: 2042 RVA: 0x0005231E File Offset: 0x0005051E
		public DatabaseLayer DatabaseManager { get; private set; }

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060007FB RID: 2043 RVA: 0x00052327 File Offset: 0x00050527
		// (set) Token: 0x060007FC RID: 2044 RVA: 0x0005232F File Offset: 0x0005052F
		public OperationContext OpContext { get; set; }

		// Token: 0x060007FD RID: 2045 RVA: 0x00052338 File Offset: 0x00050538
		public CaseDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext2 = this.OpContext;
			this.DatabaseManager = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext2 != null) ? opContext2.TenantId : null);
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x0005236C File Offset: 0x0005056C
		private Case GetCaseFromReader(IDataReader reader, IBatchDecryptor batchDecryptor)
		{
			Case baseCaseFromRecord = this.GetBaseCaseFromRecord<Case>(reader, batchDecryptor);
			bool flag = baseCaseFromRecord == null;
			Case result;
			if (flag)
			{
				result = null;
			}
			else
			{
				baseCaseFromRecord.Status = reader["status"].ToString();
				baseCaseFromRecord.WhoEntered = PeopleDAO.GetPersonFromReader("whoentered", reader, this.OpContext, batchDecryptor);
				baseCaseFromRecord.DateEntered = ((reader["dateentered"] is DBNull) ? DateTime.MinValue : ((DateTime)reader["dateentered"]));
				baseCaseFromRecord.Clients = this.GetCaseClientsFromReader(reader, batchDecryptor);
				result = baseCaseFromRecord;
			}
			return result;
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x00052404 File Offset: 0x00050604
		private IList<CaseClient> GetCaseClientsFromReader(IDataReader reader, IBatchDecryptor batchDecryptor)
		{
			List<CaseClient> list = new List<CaseClient>();
			bool flag = reader == null;
			IList<CaseClient> result;
			if (flag)
			{
				result = list;
			}
			else
			{
				CaseClient caseClientFromRecord = this.GetCaseClientFromRecord(reader, batchDecryptor);
				bool flag2 = caseClientFromRecord != null;
				if (flag2)
				{
					list.Add(caseClientFromRecord);
				}
				while (reader.Read())
				{
					caseClientFromRecord = this.GetCaseClientFromRecord(reader, batchDecryptor);
					bool flag3 = caseClientFromRecord != null;
					if (flag3)
					{
						list.Add(caseClientFromRecord);
					}
				}
				result = list;
			}
			return result;
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x00052470 File Offset: 0x00050670
		private CaseClient GetCaseClientFromRecord(IDataReader record, IBatchDecryptor batchDecryptor)
		{
			PersonBase personFromReader = PeopleDAO.GetPersonFromReader("", record, this.OpContext, batchDecryptor);
			bool flag = personFromReader == null;
			CaseClient result;
			if (flag)
			{
				result = null;
			}
			else
			{
				int num = (record["usertype"] is DBNull) ? 0 : ((int)record["usertype"]);
				result = new CaseClient
				{
					Client = personFromReader,
					ClientType = (eCaseClientType)(Enum.IsDefined(typeof(eCaseClientType), num) ? num : 1)
				};
			}
			return result;
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x000524FC File Offset: 0x000506FC
		private T GetBaseCaseFromRecord<T>(IDataReader record, IBatchDecryptor batchDecryptor) where T : CaseBase
		{
			bool flag = record == null || record["infopcid"] is DBNull;
			T result;
			if (flag)
			{
				result = default(T);
			}
			else
			{
				byte[] array = (record["CaseNumber"] is DBNull) ? null : ((byte[])record["CaseNumber"]);
				T t = Activator.CreateInstance<T>();
				t.InfoPcId = (int)record["infopcid"];
				t.Title = record["title"].ToString();
				t.CaseNumber = ((array == null) ? "" : batchDecryptor.Decrypt(array));
				result = t;
			}
			return result;
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x000525C0 File Offset: 0x000507C0
		public void MergeCasesForTwoStudents(int PersonIdNew, int PersonIdOld)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@newpid", DbType.Int32, PersonIdNew),
				this.DatabaseManager.GetParameter("@oldpid", DbType.Int32, PersonIdOld)
			};
			this.DatabaseManager.ExecuteNonQuery("UPDATE infopcpeople SET personid=@newpid WHERE personid=@oldpid", parameters);
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x00052620 File Offset: 0x00050820
		public int CreateCase(Case Case)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			string text = DateTime.Now.Year.ToString().Substring(2);
			DbParameter[] array = new DbParameter[]
			{
				databaseLayer.GetOutputParameter("@infopcid", DbType.Int32, 0),
				databaseLayer.GetParameter("@student_no", DbType.Binary, databaseLayer.Encryption.Encrypt(text)),
				databaseLayer.GetParameter("@whoenteredpid", DbType.Int32, this.OpContext.WhoAmI),
				databaseLayer.GetParameter("@title", DbType.String, Case.Title ?? "")
			};
			databaseLayer.ExecuteNonQuery("INSERT INTO infopc (student_no,dateentered,whoentered,description,title,isactive) VALUES (@student_no,getdate(),@whoenteredpid,'',@title,1)\r\nSET @infopcid=(SELECT TOP 1 CAST(SCOPE_IDENTITY() AS int) AS infopcid)", array);
			object value = array[0].Value;
			int num = (value == null || !(value is int)) ? 0 : ((int)value);
			bool flag = num < 1;
			if (flag)
			{
				throw new DatabaseInsertFailedException(string.Format("CaseDAO:CreateCase:Failed to create case; returned id from sql insert was 0", Array.Empty<object>()));
			}
			text = text + "_" + num.ToString();
			array = new DbParameter[]
			{
				databaseLayer.GetParameter("@infopcid", DbType.Int32, num),
				databaseLayer.GetParameter("@student_no", DbType.Binary, databaseLayer.Encryption.Encrypt(text))
			};
			databaseLayer.ExecuteNonQuery("UPDATE infopc SET student_no=@student_no WHERE personid=@infopcid", array);
			this.UpdateCaseClientsAndRespondents(num, Case.Clients ?? new List<CaseClient>());
			return num;
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x0005279C File Offset: 0x0005099C
		public void DeleteCase(int InfoPcId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@infopcid", DbType.Int32, InfoPcId)
			};
			databaseLayer.ExecuteNonQuery("UPDATE infopc SET isactive=0 WHERE personid=@infopcid", parameters);
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x000527F0 File Offset: 0x000509F0
		public Case LoadCaseById(int InfoPcId, int ScreenNum)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@infopcid", DbType.Int32, InfoPcId),
				databaseLayer.GetParameter("@screennum", DbType.Int32, InfoPcId)
			};
			Case result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("DECLARE @statuscid int\r\nSET @statuscid=(SELECT TOP 1 dsc.controlid FROM DynamicScreenControls dsc LEFT JOIN DynamicControls dc ON dc.ControlID=dsc.controlID WHERE dsc.screenNum=@screennum AND dc.controlcaption LIKE '%status%')\r\n\r\nSELECT DISTINCT ipc.personid AS infopcid,ipc.student_no AS CaseNumber,ipc.dateentered,\r\n        ipc.whoentered AS whoenteredpersonid,p.firstname AS whoenteredfirstname,p.lastname AS whoenteredlastname,p.student_no AS whoenteredstudent_no,\r\n\t\tpcd.valtext AS [status],\r\n        att.personid,att.usertype,\r\n\t\tp2.lastname,p2.firstname,p2.middlename,p2.student_no,ipc.title\r\nFROM    infopc ipc LEFT JOIN people p ON p.personid=ipc.whoentered\r\n        LEFT JOIN pcdata2 pcd ON pcd.infopcid=ipc.personid AND pcd.controlid=@statuscid\r\n        LEFT JOIN infopcpeople att ON att.infopcid=ipc.personid\r\n\t\tLEFT JOIN people p2 ON p2.personid=att.personid\r\nWHERE ipc.personid=@infopcid", parameters))
			{
				bool flag = dataReader == null || !dataReader.Read();
				if (flag)
				{
					result = null;
				}
				else
				{
					result = this.GetCaseFromReader(dataReader, databaseLayer.Encryption.GetBatchDecryptor());
				}
			}
			return result;
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x000528A4 File Offset: 0x00050AA4
		public IList<CaseClient> LoadCaseClientsByCaseId(int InfoPcId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@infopcid", DbType.Int32, InfoPcId)
			};
			IList<CaseClient> result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT    DISTINCT ipc.personid AS infopcid,att.personid,p.firstname,p.middlename,p.lastname,p.student_no,att.usertype FROM infopc ipc LEFT JOIN infopcpeople att ON att.infopcid=ipc.personid LEFT JOIN people p ON p.personid=att.personid WHERE ipc.personid=@infopcid", parameters))
			{
				List<CaseClient> list = new List<CaseClient>();
				bool flag = dataReader == null;
				if (flag)
				{
					result = list;
				}
				else
				{
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						CaseClient caseClientFromRecord = this.GetCaseClientFromRecord(dataReader, batchDecryptor);
						bool flag2 = caseClientFromRecord != null;
						if (flag2)
						{
							list.Add(caseClientFromRecord);
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x0005296C File Offset: 0x00050B6C
		public void UpdateCaseClientsAndRespondents(int InfoPcId, IList<CaseClient> FullClientListForCase)
		{
			IList<CaseClient> existingCaseClients = this.LoadCaseClientsByCaseId(InfoPcId);
			List<CaseClient> list = (from g in existingCaseClients
			where FullClientListForCase.FirstOrDefault((CaseClient h) => h.Client.PersonId == g.Client.PersonId) == null
			select g).ToList<CaseClient>();
			List<CaseClient> first = (from g in FullClientListForCase
			where existingCaseClients.FirstOrDefault((CaseClient h) => h.Client.PersonId == g.Client.PersonId) == null
			select g).ToList<CaseClient>();
			List<CaseClient> second = FullClientListForCase.Where(delegate(CaseClient g)
			{
				CaseClient caseClient3 = existingCaseClients.FirstOrDefault((CaseClient h) => h.Client.PersonId == g.Client.PersonId);
				bool flag = caseClient3 == null;
				return !flag && caseClient3.ClientType != g.ClientType;
			}).ToList<CaseClient>();
			IEnumerable<CaseClient> enumerable = first.Concat(second);
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbTransaction dbTransaction = databaseLayer.BeginDbTransaction();
			try
			{
				foreach (CaseClient caseClient in list)
				{
					DbParameter[] parameters = new DbParameter[]
					{
						databaseLayer.GetParameter("@infopcid", DbType.Int32, InfoPcId),
						databaseLayer.GetParameter("@pid", DbType.Int32, caseClient.Client.PersonId)
					};
					databaseLayer.ExecuteNonQueryTransaction("DELETE FROM infopcpeople WHERE infopcid=@infopcid AND personid=@pid", dbTransaction, parameters);
				}
				foreach (CaseClient caseClient2 in enumerable)
				{
					DbParameter[] parameters2 = new DbParameter[]
					{
						databaseLayer.GetParameter("@infopcid", DbType.Int32, InfoPcId),
						databaseLayer.GetParameter("@pid", DbType.Int32, caseClient2.Client.PersonId),
						databaseLayer.GetParameter("@usertype", DbType.Int32, (int)caseClient2.ClientType)
					};
					databaseLayer.ExecuteNonQueryTransaction("IF EXISTS(SELECT infopcid FROM infopcpeople WHERE infopcid=@infopcid AND personid=@pid)\r\n    UPDATE infopcpeople SET usertype=@usertype WHERE infopcid=@infopcid AND personid=@pid\r\nELSE\r\n    INSERT INTO infopcpeople (infopcid,personid,usertype) VALUES (@infopcid,@pid,@usertype)", dbTransaction, parameters2);
				}
				dbTransaction.Commit();
			}
			catch (Exception ex)
			{
				string message = "CaseDAO:UpdateCaseClientsAndRespondents:Error - rolling back:err=" + ex.ToString();
				CWLogger.Logger.Error(message);
				dbTransaction.Rollback();
				throw new DatabaseInsertFailedException(message);
			}
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x00052BC8 File Offset: 0x00050DC8
		public void UpdateBasicCaseInfo(int InfoPcId, string NewTitle)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@infopcid", DbType.Int32, InfoPcId),
				databaseLayer.GetParameter("@title", DbType.String, NewTitle ?? "")
			};
			databaseLayer.ExecuteNonQuery("UPDATE infopc SET title=@title WHERE personid=@infopcid", parameters);
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x00052C34 File Offset: 0x00050E34
		public IList<CaseForDisplay> LoadCasesForDisplayForStudent(int PersonId, int ScreenNum, params int[] controlIdsToAddToColumn)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@pid", DbType.Int32, PersonId),
				databaseLayer.GetParameter("@screennum", DbType.Int32, ScreenNum)
			};
			List<CaseForDisplay> list = new List<CaseForDisplay>();
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("DECLARE @statuscid int\r\nSET @statuscid=(SELECT TOP 1 dsc.controlid FROM DynamicScreenControls dsc LEFT JOIN DynamicControls dc ON dc.ControlID=dsc.controlID WHERE dsc.screenNum=@screennum AND dc.controlcaption LIKE '%status%')\r\n\r\nSELECT DISTINCT ipc.personid AS infopcid,ipc.student_no AS CaseNumber,ipc.dateentered,\r\n            ipc.whoentered AS whoenteredpersonid,p.firstname AS whoenteredfirstname,p.lastname AS whoenteredlastname,p.student_no AS whoenteredstudent_no,\r\n            pcd.valtext AS [status],ipc.title\r\nFROM    infopc ipc LEFT JOIN people p ON p.personid=ipc.whoentered\r\n        LEFT JOIN pcdata2 pcd ON pcd.infopcid=ipc.personid AND pcd.controlid=@statuscid\r\nWHERE ipc.isactive=1 \r\n      AND ipc.personid IN (SELECT infopcid AS personid FROM infopcpeople WHERE personid=@pid)\r\n      --AND p.isactive=1 \r\n    AND EXISTS(SELECT screendataid FROM screendata WHERE personid=ipc.personid AND screennum=@screennum)\r\nORDER BY ipc.dateentered DESC,ipc.personid", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					return null;
				}
				IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
				while (dataReader.Read())
				{
					CaseForDisplay baseCaseFromRecord = this.GetBaseCaseFromRecord<CaseForDisplay>(dataReader, batchDecryptor);
					bool flag2 = baseCaseFromRecord != null;
					if (flag2)
					{
						baseCaseFromRecord.Status = dataReader["status"].ToString();
						baseCaseFromRecord.WhoEntered = PeopleDAO.GetPersonFromReader("whoentered", dataReader, this.OpContext, batchDecryptor);
						baseCaseFromRecord.DateEntered = ((dataReader["dateentered"] is DBNull) ? DateTime.MinValue : ((DateTime)dataReader["dateentered"]));
						baseCaseFromRecord.DynamicFormDataSummary = new List<DynamicData>();
						list.Add(baseCaseFromRecord);
					}
				}
			}
			List<int> list2 = (from g in list
			select g.InfoPcId).Distinct<int>().ToList<int>();
			List<int> list3 = (controlIdsToAddToColumn ?? new int[0]).ToList<int>();
			bool flag3 = list2.Count < 1 || list3.Count < 1;
			IList<CaseForDisplay> result;
			if (flag3)
			{
				result = list;
			}
			else
			{
				IDynamicDataDAO dynamicDataDAO = new DynamicDataDAO(this.OpContext);
				List<DynamicDataSet> source = dynamicDataDAO.LoadPerCaseDataForMultipleStudents(list2, list3);
				using (List<CaseForDisplay>.Enumerator enumerator = list.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						CaseForDisplay caseForDisplay = enumerator.Current;
						DynamicDataSet dynamicDataSet = source.FirstOrDefault((DynamicDataSet g) => g.Context.PrimaryId == caseForDisplay.InfoPcId);
						bool flag4 = dynamicDataSet == null;
						if (!flag4)
						{
							caseForDisplay.DynamicFormDataSummary = dynamicDataSet.Data;
						}
					}
				}
				result = list;
			}
			return result;
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x00052E9C File Offset: 0x0005109C
		public IList<BaseBasicAppointment> LoadBasicAppointmentsByCase(int infoPcId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@caseid", DbType.Int32, infoPcId)
			};
			IList<BaseBasicAppointment> result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT\ta.appointmentid,a.AppTypeID,a.[description] AS apptypedescription,\r\n        at.appointmenttypegroupid,atg.title AS apptypegrouptitle,\r\n\t\ta.appCode,a.startDate,a.endDate,a.[subject],a.location,\r\n\t\ta.cancelled,a.isLocked,a.isHidden,a.groupCode,a.extraattendeescount,\r\n\t\ta.AttendeeID,a.PersonID,a.firstName,a.lastName,a.student_no,a.miscCode,a.noShow,\r\n\t\tat.isCourse,at.isWorkshop,at.defaultColour,\r\n\t\tpg.groupid,ast.appointmentshowtimeasid,ast.showtimeastitle,ast.extraiconid,ast.showtimeascolour\r\nFROM\tapps a LEFT JOIN peoplegroups pg ON pg.personid=a.personid AND pg.groupid<10 \r\n\t\tLEFT JOIN AppointmentTypes at ON at.apptypeid=a.AppTypeID \r\n\t\tLEFT JOIN AppointmentTypeGroups atg ON atg.AppointmentTypeGroupID=at.appointmentTypeGroupID \r\n        LEFT JOIN AppointmentShowTimeAs ast ON ast.extraiconid=a.appcode\r\n WHERE a.caseid=@caseid ORDER BY a.startdate DESC", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<BaseBasicAppointment> list = new List<BaseBasicAppointment>();
					while (dataReader.Read())
					{
						BaseBasicAppointment mainBaseBasicAppointment = BaseAppointmentDAO.GetMainBaseBasicAppointment<BaseBasicAppointment>(dataReader, this.OpContext);
						bool flag2 = mainBaseBasicAppointment == null;
						if (!flag2)
						{
							list.Add(mainBaseBasicAppointment);
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x00052F5C File Offset: 0x0005115C
		[DebuggerStepThrough]
		public Task<IList<BaseBasicAppointment>> LoadBasicAppointmentsByCaseAsync(int infoPcId)
		{
			CaseDAO.<LoadBasicAppointmentsByCaseAsync>d__22 <LoadBasicAppointmentsByCaseAsync>d__ = new CaseDAO.<LoadBasicAppointmentsByCaseAsync>d__22();
			<LoadBasicAppointmentsByCaseAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<BaseBasicAppointment>>.Create();
			<LoadBasicAppointmentsByCaseAsync>d__.<>4__this = this;
			<LoadBasicAppointmentsByCaseAsync>d__.infoPcId = infoPcId;
			<LoadBasicAppointmentsByCaseAsync>d__.<>1__state = -1;
			<LoadBasicAppointmentsByCaseAsync>d__.<>t__builder.Start<CaseDAO.<LoadBasicAppointmentsByCaseAsync>d__22>(ref <LoadBasicAppointmentsByCaseAsync>d__);
			return <LoadBasicAppointmentsByCaseAsync>d__.<>t__builder.Task;
		}
	}
}
