using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.Common.DAO.Appointments;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.Appointments.AppointmentHistory;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.DAO.Impl.Appointments
{
	// Token: 0x02000126 RID: 294
	public class AppointmentHistoryDAO : IAppointmentHistoryDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000868 RID: 2152 RVA: 0x000563E8 File Offset: 0x000545E8
		public AppointmentHistoryDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000869 RID: 2153 RVA: 0x000563FA File Offset: 0x000545FA
		// (set) Token: 0x0600086A RID: 2154 RVA: 0x00056402 File Offset: 0x00054602
		public OperationContext OpContext { get; set; }

		// Token: 0x0600086B RID: 2155 RVA: 0x0005640C File Offset: 0x0005460C
		public IList<AppointmentRawHistoryItem> LoadAppointmentRawHistoryItems(int appId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			IList<AppointmentHistoryDAO.ArchiveAppointmentItem> source = this.LoadAppointmentArchiveEntries(databaseLayer, appId);
			IList<AppointmentHistoryDAO.AppointmentModifiedDateItem> list = this.LoadAppointmentModifiedDatesEntries(databaseLayer, appId);
			List<AppointmentRawHistoryItem> list2 = (from g in source
			select new AppointmentRawHistoryItem
			{
				AuditDateTime = g.AuditDateTime,
				AppointmentBeforeChange = g.AppointmentBeforeChange,
				IsDeleted = g.IsDeleted
			}).ToList<AppointmentRawHistoryItem>();
			foreach (AppointmentRawHistoryItem appointmentRawHistoryItem in list2)
			{
				AppointmentHistoryDAO.AppointmentModifiedDateItem appointmentModifiedDateItem = null;
				double num = 0.0;
				foreach (AppointmentHistoryDAO.AppointmentModifiedDateItem appointmentModifiedDateItem2 in list)
				{
					double num2 = Math.Abs((appointmentModifiedDateItem2.DateModified - appointmentRawHistoryItem.AuditDateTime).TotalSeconds);
					bool flag = num2 > 2.0;
					if (!flag)
					{
						bool flag2 = appointmentModifiedDateItem == null || num > num2;
						if (flag2)
						{
							appointmentModifiedDateItem = appointmentModifiedDateItem2;
							num = num2;
						}
					}
				}
				bool flag3 = appointmentModifiedDateItem == null;
				if (!flag3)
				{
					appointmentRawHistoryItem.AuditOwner = appointmentModifiedDateItem.WhoModified;
				}
			}
			return list2;
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x00056578 File Offset: 0x00054778
		private IList<AppointmentHistoryDAO.AppointmentModifiedDateItem> LoadAppointmentModifiedDatesEntries(DatabaseLayer db, int appId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				db.GetParameter("@appid", DbType.Int32, appId)
			};
			IList<AppointmentHistoryDAO.AppointmentModifiedDateItem> result;
			using (IDataReader dataReader = db.ExecuteQueryReader("SELECT amd.AppointmentsModifiedDatesID,amd.appointmentID,amd.dateModified,amd.personID,\r\n\t\tp.student_no,p.firstName,p.middleName,p.lastName\r\nFROM\tAppointmentsModifiedDates amd LEFT JOIN people p ON p.PersonID=amd.personID\r\nWHERE\tamd.appointmentID=@appid\r\nORDER BY amd.dateModified", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<AppointmentHistoryDAO.AppointmentModifiedDateItem> list = new List<AppointmentHistoryDAO.AppointmentModifiedDateItem>();
					IBatchDecryptor batchDecryptor = db.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						AppointmentHistoryDAO.AppointmentModifiedDateItem appointmentModifiedDateItemFromRecord = this.GetAppointmentModifiedDateItemFromRecord(batchDecryptor, dataReader);
						bool flag2 = appointmentModifiedDateItemFromRecord == null;
						if (!flag2)
						{
							list.Add(appointmentModifiedDateItemFromRecord);
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x00056624 File Offset: 0x00054824
		private IList<AppointmentHistoryDAO.ArchiveAppointmentItem> LoadAppointmentArchiveEntries(DatabaseLayer db, int appId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				db.GetParameter("@appid", DbType.Int32, appId)
			};
			IList<AppointmentHistoryDAO.ArchiveAppointmentItem> result;
			using (IDataReader dataReader = db.ExecuteQueryReader("SELECT\taa.RowNumber,aa.auditaction,aa.auditdatetime,\r\n        aa.AppointmentID,aa.AppTypeID,apt.[description] AS apptypedescription,\r\n\t\tapt.appointmentTypeGroupID,atg.title AS apptypegrouptitle,\r\n\t\taa.appCode,aa.startDate,aa.endDate,aa.[subject],aa.[location],\r\n\t\taa.cancelled,aa.isLocked,aa.isHidden,aa.groupCode,aa.extraAttendeesCount,\r\n\t\t0 AS AttendeeId,att.PersonID,p.firstName,p.lastName,p.student_no,att.miscCode,att.noshow,\r\n\t\tapt.isCourse,apt.isWorkshop,apt.defaultColour,\r\n\t\tam.memoText,am.isEncrypted,\r\n\t\taa.personID AS wbpersonid,p2.firstName AS wbfirstname,p2.lastName AS wblastname,p2.student_no AS wbstudent_no,\r\n\t\taa.dateAdded AS datebooked,CAST(NULL AS int) AS overridecolour,CAST(NULL AS datetime) AS actualstarttime,CAST(NULL AS datetime) AS actualendtime,\r\n\t\tCAST(NULL AS int) AS cancelreasonid,CAST(NULL AS varchar(256)) AS cancelreasongroupname,CAST(NULL AS varchar(256)) AS cancelreasontitle,\r\n\t\tCAST(NULL AS int) AS cbpersonid,CAST(NULL AS varbinary(8000)) AS cbfirstname,CAST(NULL AS varbinary(8000)) AS cblastname,CAST(NULL AS varbinary(8000)) AS cbstudent_no,\r\n\t\tCAST(NULL AS datetime) AS cancelleddate,CAST(NULL AS varchar(256)) AS cancelreasontext,\r\n\t\tpg.groupid,ast.appointmentshowtimeasid,ast.showtimeastitle,ast.extraiconid,ast.showtimeascolour\r\nFROM\t(SELECT ROW_NUMBER() OVER(ORDER BY AppointmentId) AS RowNumber,* FROM archive_appointments WHERE appointmentid=@appid) aa LEFT JOIN AppointmentTypes apt ON apt.AppTypeID=aa.AppTypeID\r\n\t\tLEFT JOIN AppointmentTypeGroups atg ON atg.AppointmentTypeGroupID=apt.appointmentTypeGroupID\r\n\t\tLEFT JOIN archive_attendees att ON att.AppointmentID=aa.AppointmentID\r\n\t\tLEFT JOIN people p ON p.PersonID=att.PersonID\r\n\t\tLEFT JOIN archive_appointmentMemos am ON am.AppointmentID=aa.AppointmentID\r\n\t\tLEFT JOIN people p2 ON p2.PersonID=aa.personID\r\n\t\tLEFT JOIN PeopleGroups pg ON pg.PersonID=att.PersonID AND pg.groupid<10\r\n\t\tLEFT JOIN AppointmentShowTimeAs ast ON ast.extraiconid=aa.appcode\r\nORDER BY aa.RowNumber,aa.auditdatetime", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<AppointmentHistoryDAO.ArchiveAppointmentItem> list = new List<AppointmentHistoryDAO.ArchiveAppointmentItem>();
					IBatchDecryptor batchDecryptor = db.Encryption.GetBatchDecryptor();
					BaseBasicAppointment baseBasicAppointment = null;
					int num = 0;
					while (dataReader.Read())
					{
						int num2 = (int)dataReader["RowNumber"];
						bool flag2 = baseBasicAppointment == null || num != num2;
						if (flag2)
						{
							num = num2;
							baseBasicAppointment = BaseAppointmentDAO.GetMainBaseBasicAppointment<BaseBasicAppointment>(dataReader, this.OpContext);
							List<AppointmentHistoryDAO.ArchiveAppointmentItem> list2 = list;
							AppointmentHistoryDAO.ArchiveAppointmentItem archiveAppointmentItem = new AppointmentHistoryDAO.ArchiveAppointmentItem();
							archiveAppointmentItem.AppointmentBeforeChange = baseBasicAppointment;
							archiveAppointmentItem.AuditDateTime = (DateTime)dataReader["auditdatetime"];
							object obj = dataReader["auditaction"];
							archiveAppointmentItem.IsDeleted = (((obj != null) ? obj.ToString().Trim().ToLower() : null) == "del");
							list2.Add(archiveAppointmentItem);
						}
						BaseAppointmentDAO.AddExtendedInfoToBaseBasicAppointment(dataReader, baseBasicAppointment, this.OpContext, null);
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x0005676C File Offset: 0x0005496C
		private AppointmentHistoryDAO.AppointmentModifiedDateItem GetAppointmentModifiedDateItemFromRecord(IBatchDecryptor batchDecryptor, IDataRecord record)
		{
			bool flag = record == null;
			AppointmentHistoryDAO.AppointmentModifiedDateItem result;
			if (flag)
			{
				result = null;
			}
			else
			{
				int num = (record["AppointmentsModifiedDatesID"] is DBNull) ? 0 : ((int)record["AppointmentsModifiedDatesID"]);
				bool flag2 = num < 1;
				if (flag2)
				{
					result = null;
				}
				else
				{
					int num2 = (record["personID"] is DBNull) ? 0 : ((int)record["personID"]);
					result = new AppointmentHistoryDAO.AppointmentModifiedDateItem
					{
						AppointmentsModifiedDatesId = num,
						AppointmentId = ((record["appointmentid"] is DBNull) ? 0 : ((int)record["appointmentid"])),
						DateModified = (DateTime)record["dateModified"],
						WhoModified = ((num2 > 0) ? new BasicPerson
						{
							PersonId = num2,
							FirstName = ((record["firstName"] is DBNull) ? "" : batchDecryptor.Decrypt((byte[])record["firstName"])),
							MiddleName = ((record["middlename"] is DBNull) ? "" : batchDecryptor.Decrypt((byte[])record["middlename"])),
							LastName = ((record["lastname"] is DBNull) ? "" : batchDecryptor.Decrypt((byte[])record["lastname"])),
							StudentNumber = ((record["student_no"] is DBNull) ? "" : batchDecryptor.Decrypt((byte[])record["student_no"]))
						} : null)
					};
				}
			}
			return result;
		}

		// Token: 0x02000299 RID: 665
		internal class AppointmentModifiedDateItem
		{
			// Token: 0x17000153 RID: 339
			// (get) Token: 0x06000F20 RID: 3872 RVA: 0x0008DCF4 File Offset: 0x0008BEF4
			// (set) Token: 0x06000F21 RID: 3873 RVA: 0x0008DCFC File Offset: 0x0008BEFC
			public int AppointmentsModifiedDatesId { get; set; }

			// Token: 0x17000154 RID: 340
			// (get) Token: 0x06000F22 RID: 3874 RVA: 0x0008DD05 File Offset: 0x0008BF05
			// (set) Token: 0x06000F23 RID: 3875 RVA: 0x0008DD0D File Offset: 0x0008BF0D
			public int AppointmentId { get; set; }

			// Token: 0x17000155 RID: 341
			// (get) Token: 0x06000F24 RID: 3876 RVA: 0x0008DD16 File Offset: 0x0008BF16
			// (set) Token: 0x06000F25 RID: 3877 RVA: 0x0008DD1E File Offset: 0x0008BF1E
			public DateTime DateModified { get; set; }

			// Token: 0x17000156 RID: 342
			// (get) Token: 0x06000F26 RID: 3878 RVA: 0x0008DD27 File Offset: 0x0008BF27
			// (set) Token: 0x06000F27 RID: 3879 RVA: 0x0008DD2F File Offset: 0x0008BF2F
			public BasicPerson WhoModified { get; set; }
		}

		// Token: 0x0200029A RID: 666
		internal class ArchiveAppointmentItem
		{
			// Token: 0x17000157 RID: 343
			// (get) Token: 0x06000F29 RID: 3881 RVA: 0x0008DD38 File Offset: 0x0008BF38
			// (set) Token: 0x06000F2A RID: 3882 RVA: 0x0008DD40 File Offset: 0x0008BF40
			public BaseBasicAppointment AppointmentBeforeChange { get; set; }

			// Token: 0x17000158 RID: 344
			// (get) Token: 0x06000F2B RID: 3883 RVA: 0x0008DD49 File Offset: 0x0008BF49
			// (set) Token: 0x06000F2C RID: 3884 RVA: 0x0008DD51 File Offset: 0x0008BF51
			public bool IsDeleted { get; set; }

			// Token: 0x17000159 RID: 345
			// (get) Token: 0x06000F2D RID: 3885 RVA: 0x0008DD5A File Offset: 0x0008BF5A
			// (set) Token: 0x06000F2E RID: 3886 RVA: 0x0008DD62 File Offset: 0x0008BF62
			public DateTime AuditDateTime { get; set; }
		}
	}
}
