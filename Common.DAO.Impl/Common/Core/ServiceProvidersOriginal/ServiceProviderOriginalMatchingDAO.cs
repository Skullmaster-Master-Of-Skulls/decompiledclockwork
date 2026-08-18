using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.Common.DAO.Impl.People;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.OperationContexts;
using TechnoPro.Common.Public.Entities.ServiceProvidersOriginal;

namespace TechnoPro.Common.Core.ServiceProvidersOriginal
{
	// Token: 0x02000012 RID: 18
	public class ServiceProviderOriginalMatchingDAO : IServiceProviderOriginalMatchingDAO, IBaseOperationContext<ServiceProvidersOperationContext>
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00002F27 File Offset: 0x00001127
		// (set) Token: 0x0600005A RID: 90 RVA: 0x00002F2F File Offset: 0x0000112F
		public DatabaseLayer DatabaseManager { get; private set; }

		// Token: 0x0600005B RID: 91 RVA: 0x00002F38 File Offset: 0x00001138
		public ServiceProviderOriginalMatchingDAO(ServiceProvidersOperationContext opContext)
		{
			this.OpContext = opContext;
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			ServiceProvidersOperationContext opContext2 = this.OpContext;
			this.DatabaseManager = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext2 != null) ? opContext2.TenantId : null);
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00002F69 File Offset: 0x00001169
		// (set) Token: 0x0600005D RID: 93 RVA: 0x00002F71 File Offset: 0x00001171
		public ServiceProvidersOperationContext OpContext { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00002F7C File Offset: 0x0000117C
		private StudentCommonInfoDAO studentCommonInfoDao
		{
			get
			{
				bool flag = this._studentCommonInfoDao == null;
				if (flag)
				{
					this._studentCommonInfoDao = new StudentCommonInfoDAO(this.OpContext);
				}
				return this._studentCommonInfoDao;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00002FB4 File Offset: 0x000011B4
		private ServiceProviderOriginalProviderDAO providerDao
		{
			get
			{
				bool flag = this._providerDao == null;
				if (flag)
				{
					this._providerDao = new ServiceProviderOriginalProviderDAO(this.OpContext);
				}
				return this._providerDao;
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002FEC File Offset: 0x000011EC
		private ServiceProviderAssignment GetAssignmentFromReader(IDataReader reader, IBatchDecryptor decryptor = null)
		{
			int num = (reader["serviceproviderrequestid"] is DBNull) ? 0 : ((int)reader["serviceproviderrequestid"]);
			bool flag = num < 1;
			ServiceProviderAssignment result;
			if (flag)
			{
				result = null;
			}
			else
			{
				ServiceProviderOriginalRequestDAO serviceProviderOriginalRequestDAO = new ServiceProviderOriginalRequestDAO(this.OpContext);
				ServiceProviderAssignment serviceRequestFromRecord = serviceProviderOriginalRequestDAO.GetServiceRequestFromRecord<ServiceProviderAssignment>(reader, decryptor);
				bool flag2 = serviceRequestFromRecord != null;
				if (flag2)
				{
					serviceRequestFromRecord.StudentCommonInfo = this.studentCommonInfoDao.GetCommonInfoFromRecord(reader, "student_", decryptor);
					serviceRequestFromRecord.AssignedServiceProvider = this.providerDao.GetServiceProviderBaseFromRecord<ServiceProviderBase>(reader, decryptor);
					result = serviceRequestFromRecord;
				}
				else
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003088 File Offset: 0x00001288
		public IList<ServiceProviderAssignment> LoadAssignmentsByProviderAndAssignedDate(int ServiceProviderId, DateTime StartDate, DateTime EndDate)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			ServiceProvidersOperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@spid2", DbType.Int32, ServiceProviderId),
				databaseLayer.GetParameter("@sd", DbType.DateTime, StartDate.Date),
				databaseLayer.GetParameter("@ed", DbType.DateTime, EndDate.Date.AddDays(1.0))
			};
			IList<ServiceProviderAssignment> result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT\tspr.ServiceProviderRequestID,spr.IsActive,spr.serviceprovidertype,spr.ServiceProviderId,\r\n\t\tspr.dateentered,spr.startdate,spr.enddate,\r\n\t\tspr.whoentered AS whoentered_personid,p2.firstName AS whoentered_firstname,p2.middleName AS whoentered_middlename,\r\n\t\tp2.lastName AS whoentered_lastname,p2.student_no AS whoentered_student_no,\r\n\t\tspr.ServiceProviderRequestDetailId,\r\n\t\tspr.datetimerequesttitle,spr.startdatetimerequest,spr.enddatetimerequest,\r\n\t\tspr.notes,spr.studentrequested,spr.studentrequestedcancelnote,spr.dateassigned,spr.SpecialInstructions,spr.partsgroupid,\r\n\t\tspr.partsdescription,spr.dateinserted,\r\n\t\tspr.personid AS student_personid,p.lastName AS student_lastname,p.firstName AS student_firstname,p.middleName AS student_middlename,p.student_no AS student_student_no,\r\n\t\tspr.lucourseid AS student_lucourseid,\r\n\t\tlcs.StartDate AS student_StartDate,lcs.EndDate AS student_EndDate,lcs.Duration AS student_Duration,lcs.Term AS student_Term,\r\n\t\tlcs.SubjectID AS student_SubjectId,lucd.altlookupstring AS student_subject,lcs.course AS student_course,lcs.TimeOfDay AS student_TimeOfDay,\r\n\t\tlcs.[Section] AS student_Section,lcs.Campus AS student_Campus,lcs.Department AS student_Department,lcs.Location AS student_Location,lcs.CourseNote AS student_CourseNote,\r\n\t\tspr.serviceproviderlucourseid AS provider_lucourseid,\r\n\t\tlcp.StartDate AS provider_StartDate,lcp.EndDate AS provider_EndDate,lcp.Duration AS provider_Duration,lcp.Term AS provider_Term,\r\n\t\tlcp.SubjectID AS provider_SubjectId,lucdp.altlookupstring AS provider_subject,lcp.course AS provider_course,lcp.TimeOfDay AS provider_TimeOfDay,\r\n\t\tlcp.[Section] AS provider_Section,lcp.Campus AS provider_Campus,lcp.Department AS provider_Department,lcp.Location AS provider_Location,lcp.CourseNote AS provider_CourseNote,\r\n\t\tc.email AS student_email,c.oktoemail AS student_oktoemail,c.emailisnotencrypted AS student_emailisnotencrypted,\r\n        c.assignedcounsellorpid AS student_advisorpersonid,c.assignedcounsellorfirst AS student_advisorfirstname,\r\n        c.assignedcounsellorlast AS student_advisorlastname,'' AS student_advisorstudent_no,\r\n        c.advisortitle AS student_advisortitle,c.advisoremail AS student_advisoremail,c.advisorphone AS student_advisorphone,\r\n\t\tc.phone AS student_phone,c.dateofbirth AS student_dateofbirth,c.gender AS student_gender,\r\n\t\tsp.firstname,sp.middlename,sp.lastname,sp.student_no,sp.email,sp.email2,sp.altid,sp.registrationcomplete\r\nFROM\tServiceProviderRequests spr LEFT JOIN people p ON p.PersonID=spr.personid\r\n\t\tLEFT JOIN lucourses lcs ON lcs.LUCourseID=spr.lucourseid \r\n\t\tLEFT JOIN lucoursedata lucd ON lucd.luCourseDataID=lcs.SubjectID\r\n\t\tLEFT JOIN lucourses lcp ON lcp.LUCourseID=spr.serviceproviderlucourseid\r\n\t\tLEFT JOIN lucoursedata lucdp ON lucdp.luCourseDataID=lcp.SubjectID\r\n\t\tLEFT JOIN people p2 ON p2.PersonID=spr.whoentered\r\n\t\tLEFT JOIN common c ON c.personid=spr.personid\r\n\t\tLEFT JOIN serviceproviders sp ON sp.ServiceProviderId=spr.ServiceProviderId\r\nWHERE\tspr.IsActive=1 AND NOT spr.ServiceProviderId IS NULL AND spr.ServiceProviderId=@spid2\r\n\t\tAND\r\n        (\r\n            (spr.lucourseid IS NULL AND NOT ( ( spr.enddate<@sd ) OR (spr.startdate > @ed ) ) )\r\n            OR\r\n            (NOT spr.lucourseid IS NULL AND spr.lucourseid IN (SELECT lucourseid FROM lucourses WHERE NOT ( ( enddate<@sd ) OR (startdate > @ed ) )) )\r\n        )\r\nORDER BY spr.partsgroupid,spr.lucourseid", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					List<ServiceProviderAssignment> list = new List<ServiceProviderAssignment>();
					while (dataReader.Read())
					{
						ServiceProviderAssignment assignmentFromReader = this.GetAssignmentFromReader(dataReader, batchDecryptor);
						bool flag2 = assignmentFromReader != null;
						if (flag2)
						{
							list.Add(assignmentFromReader);
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x04000026 RID: 38
		private StudentCommonInfoDAO _studentCommonInfoDao;

		// Token: 0x04000027 RID: 39
		private ServiceProviderOriginalProviderDAO _providerDao;
	}
}
