using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using Databases;
using TechnoPro.Common.DAO.Impl.LookupCourses;
using TechnoPro.Common.DAO.Notetaking;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.LookupCourses;
using TechnoPro.Common.Public.Entities.Notetaking.Notetakee;
using TechnoPro.Common.Public.Entities.Notetaking.Notetakee.Info;
using TechnoPro.Common.Public.Entities.StudentAccommodationRequests;

namespace TechnoPro.Common.DAO.Impl.Notetaking
{
	// Token: 0x02000082 RID: 130
	public class NotetakeeDAO : INotetakeeDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600034C RID: 844 RVA: 0x0001CD94 File Offset: 0x0001AF94
		public NotetakeeDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600034D RID: 845 RVA: 0x0001CDA6 File Offset: 0x0001AFA6
		// (set) Token: 0x0600034E RID: 846 RVA: 0x0001CDAE File Offset: 0x0001AFAE
		public OperationContext OpContext { get; set; }

		// Token: 0x0600034F RID: 847 RVA: 0x0001CDB8 File Offset: 0x0001AFB8
		private NotetakeeCourseRegistrationStudentCourseInfo GetCourseInfoFromRecord(IDataReader record)
		{
			int num = (record["status"] is DBNull) ? 0 : ((int)record["status"]);
			return new NotetakeeCourseRegistrationStudentCourseInfo
			{
				AssignedProviderServiceProviderRequestId = ((record["ServiceProviderRequestID"] is DBNull) ? 0 : ((int)record["ServiceProviderRequestID"])),
				AssignedProviderDateAssigned = ((record["DateAssigned"] is DBNull) ? null : new DateTime?((DateTime)record["DateAssigned"])),
				AssignedProviderId = ((record["ServiceProviderId"] is DBNull) ? 0 : ((int)record["ServiceProviderId"])),
				SelfRegistrationRequestId = ((record["StudentCourseAccommodationRequestId"] is DBNull) ? 0 : ((int)record["StudentCourseAccommodationRequestId"])),
				SelfRegistrationRequestStatus = (eStudentCourseAccommodationRequestStatus)(Enum.IsDefined(typeof(eStudentCourseAccommodationRequestStatus), num) ? num : 0),
				DateLetterIssued = ((record["dateletterissued"] is DBNull) ? null : new DateTime?((DateTime)record["dateletterissued"]))
			};
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0001CF10 File Offset: 0x0001B110
		private NotetakeeCourseRegistration GetNotetakeeCourseRegistrationInfoFromRecord(IDataReader record, LookupCourseBase courseBase)
		{
			bool flag = record == null || courseBase == null;
			NotetakeeCourseRegistration result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = new NotetakeeCourseRegistration
				{
					CourseBase = courseBase,
					CourseInfo = this.GetCourseInfoFromRecord(record)
				};
			}
			return result;
		}

		// Token: 0x06000351 RID: 849 RVA: 0x0001CF50 File Offset: 0x0001B150
		public IList<NotetakeeCourseRegistration> LoadNotetakeeCourseRegistrations(int studentPid, DateTime startDate, DateTime endDate, bool loadSelfRegData, bool includeDroppedCourses = false)
		{
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, this.OpContext.TenantId);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@pid", DbType.Int32, studentPid),
				databaseLayer.GetParameter("@startdate", DbType.DateTime, startDate),
				databaseLayer.GetParameter("@enddate", DbType.DateTime, endDate),
				databaseLayer.GetParameter("@includedroppedcourses", DbType.Boolean, includeDroppedCourses),
				databaseLayer.GetParameter("@loadselfreg", DbType.Boolean, loadSelfRegData)
			};
			IList<NotetakeeCourseRegistration> result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("DECLARE @pid int = 5090\r\nDECLARE @startdate datetime = '2015-01-01'\r\nDECLARE @enddate datetime = '2016-01-01'\r\nDECLARE @includedroppedcourses bit = 0\r\nDECLARE @loadselfreg bit = 1\r\n\r\nDECLARE @sdate datetime = DATEADD(D, 0, DATEDIFF(D, 0, @startdate))\r\nDECLARE @edate datetime = DATEADD(D, 0, DATEDIFF(D, 0, @enddate))\r\n\r\nSELECT\t\tluc.lucourseid,luc.startdate,luc.enddate,luc.duration,luc.term,luc.subjectid,luc.externalid,luc.exemptfromdatasync AS lucexemptfromdatasync,\r\n            lucd.lookupstring AS subjectcode,lucd.altlookupstring AS subjectdescription,\r\n            luc.course,luc.timeofday,luc.[section],\r\n            luc.campus,luc.department,luc.location,luc.credits,\r\n            luc.coursenote,\r\n\t\t\tCAST(NULL AS int) AS ServiceProviderRequestID,CAST(NULL AS Datetime) AS DateAssigned,CAST(NULL AS int) AS ServiceProviderId,\r\n\t\t\tCAST(NULL AS int) AS StudentCourseAccommodationRequestId, CAST(NULL AS int) AS [status]\r\nINTO #t1\r\nFROM        courses c LEFT JOIN lucourses luc ON luc.LUCourseID=c.luCourseID\r\n\t\t\tLEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid\r\nWHERE       c.personID=@pid \r\n\t\t\tAND (@includedroppedcourses=1 OR (c.registrationstatus iS NULL OR NOT c.registrationstatus=2))\r\n\t\t\tAND NOT ( ( luc.enddate<@sdate ) OR (luc.startdate > @edate ) )\r\n--ORDER BY    luc.startdate,luc.duration,luc.term,lucd.altlookupstring,luc.course,luc.[section],luc.timeofday,luc.lucourseid;\r\n\r\nSELECT DISTINCT lucourseid INTO #tlucids FROM #t1 \r\n\r\n-- merge in service provider requests\r\n\r\nSELECT\t#tlucids.lucourseid,spr.ServiceProviderRequestID,spr.DateAssigned,spr.ServiceProviderId\r\nINTO #trequests\r\nFROM\t#tlucids LEFT JOIN ServiceProviderRequests spr ON spr.personid=@pid AND spr.lucourseid=#tlucids.LUCourseID\r\n\r\nUPDATE #t1 SET #t1.ServiceProviderRequestID=RAN.ServiceProviderRequestID,#t1.DateAssigned=RAN.DateAssigned,#t1.ServiceProviderId=RAN.ServiceProviderId\r\nFROM #t1 SI INNER JOIN #trequests RAN ON SI.lucourseid=RAN.lucourseid\r\n\r\n-- end merge in provider requests\r\n\r\n-- merge in self reg requests\r\n\r\nIF @loadselfreg=1 \r\nBEGIN\r\n\r\nSELECT  #tlucids.LUCourseID,sar.StudentCourseAccommodationRequestId,sar.[status]\r\nINTO #tselfreg\r\nFROM\t#tlucids LEFT JOIN StudentCourseAccommodationRequest sar ON sar.isactive=1 AND sar.personid=@pid AND sar.lucourseid=#tlucids.LUCourseID\r\nORDER BY CASE WHEN sar.[status]=8 THEN NULL ELSE sar.[status] END --make sure approved statuses are at the top - they will get picked\r\n\r\nUPDATE #t1 SET #t1.StudentCourseAccommodationRequestId=RAN.StudentCourseAccommodationRequestId,#t1.[status]=RAN.[status]\r\nFROM #t1 SI INNER JOIN #tselfreg RAN ON SI.lucourseid=RAN.lucourseid\r\n\r\nDROP TABLE #tselfreg\r\n\r\nEND\r\n\r\n-- end merge in self reg requests\r\n\r\nSELECT * FROM #t1 \r\nORDER BY    startdate,duration,term,subjectdescription,course,[section],timeofday,lucourseid\r\n\r\nDROP TABLE #t1\r\nDROP TABLE #tlucids\r\nDROP TABLE #trequests", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<NotetakeeCourseRegistration> list = new List<NotetakeeCourseRegistration>();
					while (dataReader.Read())
					{
						LookupCourseBase courseBaseFromReader = LookupCourseDAO.GetCourseBaseFromReader("", dataReader);
						bool flag2 = courseBaseFromReader == null;
						if (!flag2)
						{
							NotetakeeCourseRegistration notetakeeCourseRegistrationInfoFromRecord = this.GetNotetakeeCourseRegistrationInfoFromRecord(dataReader, courseBaseFromReader);
							bool flag3 = notetakeeCourseRegistrationInfoFromRecord != null;
							if (flag3)
							{
								list.Add(notetakeeCourseRegistrationInfoFromRecord);
							}
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x06000352 RID: 850 RVA: 0x0001D074 File Offset: 0x0001B274
		public IList<int> FindLuCourseidsWhereAtLeastOneNotetakerIsAvailable(int equivalentCourseNum, IList<int> lucids)
		{
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, this.OpContext.TenantId);
			DbParameter[] array = new DbParameter[1];
			int num = 0;
			DatabaseLayer databaseLayer2 = databaseLayer;
			string pName = "@lucids";
			DbType pType = DbType.String;
			string separator = ",";
			string[] array2;
			if (lucids == null)
			{
				array2 = null;
			}
			else
			{
				array2 = (from g in lucids
				select g.ToString()).ToArray<string>();
			}
			array[num] = databaseLayer2.GetParameter(pName, pType, string.Join(separator, array2 ?? new string[0]));
			DbParameter[] parameters = array;
			string newValue = (equivalentCourseNum <= 1) ? "EquivalentCourses" : string.Format("EquivalentCourses{0}", equivalentCourseNum);
			string query = "SELECT orderid AS lucourseid INTO #tlucids FROM splitorderids(COALESCE(@lucids,''),',')\r\n\r\nDECLARE @Fields TABLE(lucid int )\r\nINSERT INTO @Fields (lucid) SELECT lucourseid FROM #tlucids\r\n\r\nSELECT lucid,lucourseid\r\nINTO #tlucids2\r\nFROM @Fields \r\nCROSS APPLY EquivalentCourses1([@Fields].lucid)\r\n\r\nSELECT DISTINCT #tlucids2.lucid,spac.serviceproviderapplicationid,spac.lucourseid\r\nFROM #tlucids2 LEFT JOIN serviceproviderapplicationcourses spac ON spac.lucourseid=#tlucids2.lucourseid\r\nWHERE spac.serviceprovidertype=128 AND (spac.registrationstatus IS NULL OR NOT spac.registrationstatus=2)\r\n\r\nDROP TABLE #tlucids\r\nDROP TABLE #tlucids2".Replace("EquivalentCourses1", newValue);
			IList<int> result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader(query, parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<int> list = new List<int>();
					while (dataReader.Read())
					{
						int num2 = (dataReader["lucid"] is DBNull) ? 0 : ((int)dataReader["lucid"]);
						bool flag2 = num2 > 0 && !list.Contains(num2);
						if (flag2)
						{
							list.Add(num2);
						}
					}
					result = list;
				}
			}
			return result;
		}
	}
}
