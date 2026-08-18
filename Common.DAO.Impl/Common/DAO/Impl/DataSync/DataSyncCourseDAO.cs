using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using ClockWorkLogger;
using Databases;
using TechnoPro.Common.DAO.DataSync;
using TechnoPro.Common.DAO.Impl.LookupCourses;
using TechnoPro.Common.DAO.LookupCourses;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.DataSync;
using TechnoPro.Common.Public.Entities.LookupCourses;

namespace TechnoPro.Common.DAO.Impl.DataSync
{
	// Token: 0x020000F6 RID: 246
	public class DataSyncCourseDAO : IDataSyncCourseDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060006FA RID: 1786 RVA: 0x00048950 File Offset: 0x00046B50
		private ILookupSubjectDAO lookupSubjectDAO
		{
			get
			{
				ILookupSubjectDAO result;
				if ((result = this._lsd) == null)
				{
					result = (this._lsd = new LookupSubjectDAO(this.OpContext));
				}
				return result;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060006FB RID: 1787 RVA: 0x0004897C File Offset: 0x00046B7C
		private LookupCourseDAO lookupCourseDAO
		{
			get
			{
				LookupCourseDAO result;
				if ((result = this._lcd) == null)
				{
					result = (this._lcd = new LookupCourseDAO(this.OpContext));
				}
				return result;
			}
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x000489A7 File Offset: 0x00046BA7
		public DataSyncCourseDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext2 = this.OpContext;
			this.DatabaseManager = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext2 != null) ? opContext2.TenantId : null);
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060006FD RID: 1789 RVA: 0x000489D7 File Offset: 0x00046BD7
		// (set) Token: 0x060006FE RID: 1790 RVA: 0x000489DF File Offset: 0x00046BDF
		public OperationContext OpContext { get; set; }

		// Token: 0x060006FF RID: 1791 RVA: 0x000489E8 File Offset: 0x00046BE8
		public LookupCourse CreateLookupCourse(DataSyncExternalCourse extCourse, int subjectId, IList<DataSyncExternalCourseSyncResult> results = null)
		{
			LookupCourse lookupCourse = this.lookupCourseDAO.CreateLookupCourseFromExternalCourse(extCourse, subjectId);
			bool flag = results != null;
			if (flag)
			{
				results.Add(new DataSyncExternalCourseSyncResult
				{
					ExternalCourse = extCourse,
					Lucid = ((lookupCourse != null) ? lookupCourse.LuCourseId : 0),
					LookupCourseAction = eDataSyncCourseLookupCourseAction.eCreatedCourse
				});
			}
			bool flag2 = lookupCourse == null;
			if (flag2)
			{
				CWLogger.Logger.Error("Failed to create lookup course:{0}", (extCourse == null) ? "NULL extcourse" : (extCourse.Subject ?? "empty subject"));
			}
			else
			{
				CWLogger.Logger.Trace("lookupcoursecreated:{0} {1} {2}:matching={3}", new object[]
				{
					extCourse.Subject,
					extCourse.Course,
					extCourse.Section,
					lookupCourse.LuCourseId.ToString()
				});
			}
			return lookupCourse;
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x00048ABC File Offset: 0x00046CBC
		public void FixNoPrimaryWhenSecondariesExistProblemWithProfs(List<int> lucids)
		{
			bool flag = lucids == null || lucids.Count < 1;
			if (!flag)
			{
				DbParameter[] array = new DbParameter[1];
				array[0] = this.DatabaseManager.GetParameter("@lucids", DbType.String, string.Join(",", lucids.ConvertAll<string>((int g) => g.ToString()).ToArray()));
				DbParameter[] parameters = array;
				Dictionary<int, int> dictionary = new Dictionary<int, int>();
				using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT DISTINCT luc.lucourseid,MIN(i.instructorid) AS instructorid\r\nFROM LUCourses luc LEFT JOIN lucourseinstructor i ON i.lucourseid=luc.lucourseid\r\nWHERE   luc.lucourseid IN (SELECT orderid AS lucourseid FROM splitorderids(@lucids,','))\r\n        AND luc.InstructorID<0 AND NOT i.lucourseid IS NULL\r\nGROUP BY luc.lucourseid\r\nORDER BY luc.lucourseid", parameters))
				{
					bool flag2 = dataReader == null;
					if (flag2)
					{
						return;
					}
					while (dataReader.Read())
					{
						try
						{
							int num = (dataReader["lucourseid"] is DBNull) ? 0 : ((int)dataReader["lucourseid"]);
							int num2 = (dataReader["instructorid"] is DBNull) ? 0 : ((int)dataReader["instructorid"]);
							bool flag3 = num > 0 && num2 > 0;
							if (flag3)
							{
								dictionary.Add(num, num2);
							}
						}
						catch (Exception ex)
						{
							CWLogger.Logger.Error("DataSyncCourses:FixNoPrimaryWhenSecondariesExistProblemWithProfs:Error={0}", ex.ToString());
						}
					}
				}
				foreach (KeyValuePair<int, int> keyValuePair in dictionary)
				{
					try
					{
						parameters = new DbParameter[]
						{
							this.DatabaseManager.GetParameter("@lucid", DbType.Int32, keyValuePair.Key),
							this.DatabaseManager.GetParameter("@iid", DbType.Int32, keyValuePair.Key)
						};
						this.DatabaseManager.ExecuteNonQuery("UPDATE lucourses SET instructorid=@iid WHERE lucourseid=@lucid", parameters);
					}
					catch (Exception ex2)
					{
						CWLogger.Logger.Error("DataSyncCourses:FixNoPrimaryWhenSecondariesExistProblemWithProfs:Error2={0}", ex2.ToString());
					}
				}
			}
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x00048CF4 File Offset: 0x00046EF4
		public void UpdateClockWorkCourse(DataSyncExternalCourse extCourse, bool campusChanged, bool deptChanged)
		{
			DbParameter dbParameter = campusChanged ? this.DatabaseManager.GetParameter("@campus", DbType.String, extCourse.Campus) : this.DatabaseManager.GetParameter("@campus", DbType.String, DBNull.Value);
			DbParameter dbParameter2 = deptChanged ? this.DatabaseManager.GetParameter("@department", DbType.String, extCourse.Department) : this.DatabaseManager.GetParameter("@department", DbType.String, DBNull.Value);
			DbParameter[] parameters = new DbParameter[]
			{
				dbParameter,
				dbParameter2,
				this.DatabaseManager.GetParameter("@lucid", DbType.Int32, extCourse.MatchingClockWorkLookupCourse.LuCourseId)
			};
			this.DatabaseManager.ExecuteQuery("UPDATE lucourses SET department=COALESCE(@department,department),campus=COALESCE(@campus,campus) WHERE lucourseid=@lucid", parameters);
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x00048DB4 File Offset: 0x00046FB4
		public LookupSubject FindLookupSubject(string SubjectName)
		{
			ILookupSubjectDAO lookupSubjectDAO = this.lookupSubjectDAO;
			return lookupSubjectDAO.LoadLookupSubjectBySubjectDescription(SubjectName);
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x00048DD8 File Offset: 0x00046FD8
		public LookupInstructor FindLookupInstructor(DataSyncExternalCourseInstructor ExternalInstructor)
		{
			bool flag = string.IsNullOrEmpty(ExternalInstructor.ExternalInstructorId) && string.IsNullOrEmpty(ExternalInstructor.Username) && string.IsNullOrEmpty(ExternalInstructor.EmployeeId);
			if (flag)
			{
				DbParameter[] parameters = new DbParameter[]
				{
					this.DatabaseManager.GetParameter("@email", DbType.String, ExternalInstructor.Email ?? "")
				};
				using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT    lucd.lucoursedataid AS instructorid,lucd.lookupstring,lucd.altlookupstring AS instructorname,\r\nlucd.email AS instructoremail,lucd.phone AS instructorphone,lucd.username AS instructorusername,\r\nlucd.externalid AS instructorexternalid,lucd.id AS instructoremployeeid,lucd.permissionlevel AS instructorpermissionlevel,\r\nlucd.exemptfromdatasync\r\nFROM        lucoursedata lucd \r\nWHERE       lucd.lookuplisttype=1 AND NOT lucd.email='' AND lucd.email=@email", parameters))
				{
					bool flag2 = dataReader != null && dataReader.Read();
					if (flag2)
					{
						return LookupInstructorDAO.GetInstructorFromReader(dataReader, "");
					}
				}
			}
			DbParameter[] parameters2 = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@instructorexternalid", DbType.String, ExternalInstructor.ExternalInstructorId ?? ""),
				this.DatabaseManager.GetParameter("@instructorusername", DbType.String, ExternalInstructor.Username ?? ""),
				this.DatabaseManager.GetParameter("@instructoremployeeid", DbType.String, ExternalInstructor.EmployeeId ?? "")
			};
			using (IDataReader dataReader2 = this.DatabaseManager.ExecuteQueryReader("SELECT\tlucd.luCourseDataID AS instructorid,lucd.altLookupString AS instructorname,\r\n\t\tlucd.email AS instructoremail,lucd.username AS instructorusername,lucd.id AS instructoremployeeid,\r\n\t\tlucd.phone AS instructorphone,lucd.ExternalId AS instructorexternalid,lucd.PermissionLevel AS instructorpermissionlevel,\r\n        lucd.ExemptFromDataSync\r\nFROM\tLUCourseData lucd \r\nWHERE\t(LEN(COALESCE(@instructorexternalid,'')) > 0 AND lucd.ExternalId=@instructorexternalid)\r\n\t\tOR\r\n\t\t(LEN(COALESCE(@instructorusername,'')) > 0 AND lucd.username=@instructorusername)\r\n\t\tOR\r\n\t\t(LEN(COALESCE(@instructoremployeeid,'')) > 0 AND lucd.id=@instructoremployeeid)", parameters2))
			{
				bool flag3 = dataReader2 != null && dataReader2.Read();
				if (flag3)
				{
					return LookupInstructorDAO.GetInstructorFromReader(dataReader2, "");
				}
			}
			return null;
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x00048F64 File Offset: 0x00047164
		public LookupCourse FindLookupCourse(DataSyncExternalCourse ExternalCourse, int SubjectId)
		{
			bool flag = SubjectId < 1;
			if (flag)
			{
				LookupSubject lookupSubject = this.FindLookupSubject(ExternalCourse.Subject);
				bool flag2 = lookupSubject == null || lookupSubject.SubjectId < 1;
				if (flag2)
				{
					return null;
				}
				SubjectId = lookupSubject.SubjectId;
			}
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@startdate", DbType.DateTime, ExternalCourse.StartDate),
				this.DatabaseManager.GetParameter("@enddate", DbType.DateTime, ExternalCourse.EndDate),
				this.DatabaseManager.GetParameter("@duration", DbType.String, ExternalCourse.Duration ?? ""),
				this.DatabaseManager.GetParameter("@term", DbType.String, ExternalCourse.Term ?? ""),
				this.DatabaseManager.GetParameter("@subjectid", DbType.Int32, SubjectId),
				this.DatabaseManager.GetParameter("@course", DbType.String, ExternalCourse.Course ?? ""),
				this.DatabaseManager.GetParameter("@section", DbType.String, ExternalCourse.Section ?? ""),
				this.DatabaseManager.GetParameter("@timeofday", DbType.String, ExternalCourse.TimeOfDay ?? ""),
				this.DatabaseManager.GetParameter("@campus", DbType.String, ExternalCourse.Campus ?? "")
			};
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT    luc.lucourseid,luc.startdate,luc.enddate,luc.term,luc.duration,luc.subjectid,\r\nlucd.altlookupstring AS subjectdescription,lucd.lookupstring AS subjectcode,lucd.email AS subjectemail,\r\nluc.course,luc.timeofday,luc.section,luc.instructorid AS pinstructorid,lucd2.phone AS pinstructorphone,\r\nlucd2.altlookupstring AS pinstructorname,lucd2.email AS pinstructoremail,\r\nlucd2.username AS pinstructorusername,lucd2.id AS pinstructoremployeeid,\r\nlucd2.externalid AS pinstructorexternalid,lucd2.permissionlevel AS pinstructorpermissionlevel,\r\nlucd2.exemptfromdatasync AS pexemptfromdatasync,\r\nluc.ExemptAssignmentFromDataSync AS pExemptAssignmentFromDataSync,\r\nluc.crosslistcode,luc.equivalentcode,\r\nluc.whoadded,luc.dateadded,luc.location,luc.alternatecontactid AS primaryalternatecontactid,\r\nac.altname AS primaryaltname,ac.altemail AS primaryaltemail,ac.altphone AS primaryaltphone,\r\nac.altusername AS primaryaltusername,ac.altpermissionlevel AS primaryaltpermissionlevel,ac.externalid AS primaryaltexternalid,\r\nluc.campus,luc.department,luc.externalid,\r\nli.instructorid AS p3instructorid,lucd3.phone AS p3instructorphone,\r\nlucd3.altlookupstring AS p3instructorname,lucd3.email AS p3instructoremail,\r\nlucd3.username AS p3instructorusername,lucd3.id AS p3instructoremployeeid,\r\nlucd3.externalid AS p3instructorexternalid,lucd3.permissionlevel AS p3instructorpermissionlevel,\r\nlucd3.exemptfromdatasync AS p3exemptfromdatasync,tt.timetableid,\r\nli.ExemptAssignmentFromDataSync AS p3ExemptAssignmentFromDataSync,\r\ntt.timetabletype,tt.sunstartminutes,tt.sunendminutes,tt.sunroom,\r\ntt.monstartminutes,tt.monendminutes,tt.monroom,tt.tuestartminutes,tt.tueendminutes,tt.tueroom,\r\ntt.wedstartminutes,tt.wedendminutes,tt.wedroom,tt.thustartminutes,tt.thuendminutes,tt.thuroom,\r\ntt.fristartminutes,tt.friendminutes,tt.friroom,tt.satstartminutes,tt.satendminutes,tt.satroom,\r\nluc.exemptfromdatasync AS lucexemptfromdatasync,luc.coursenote\r\nFROM    lucourses luc LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid\r\n        LEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luc.instructorid\r\n        LEFT JOIN lucourseinstructor li ON li.lucourseid=luc.lucourseid\r\n        LEFT JOIN lucoursedata lucd3 ON lucd3.lucoursedataid=li.instructorid\r\n        LEFT JOIN timetable tt ON tt.lucourseid=luc.lucourseid\r\n        LEFT JOIN lucoursealternatecontact ac ON ac.alternatecontactid=luc.alternatecontactid\r\n        LEFT JOIN LuCourseAltContact lucac ON lucac.lucourseid=luc.lucourseid\r\n        LEFT JOIN lucoursealternatecontact ac2 ON ac2.alternatecontactid=lucac.alternatecontactid\r\nWHERE   NOT ( luc.enddate <= @startdate OR luc.startdate > @enddate)\r\n        AND luc.duration=@duration AND luc.term=@term AND luc.subjectid=@subjectid AND luc.course=@course\r\n        AND luc.section=@section AND luc.timeofday=@timeofday AND luc.campus=@campus", parameters))
			{
				bool flag3 = dataReader != null;
				if (flag3)
				{
					List<LookupCourse> coursesFromReader = LookupCourseDAO.GetCoursesFromReader("", dataReader);
					bool flag4 = coursesFromReader.Count > 0;
					if (flag4)
					{
						return coursesFromReader[0];
					}
				}
			}
			return null;
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x00049158 File Offset: 0x00047358
		public DataTable LoadCustomCoursesTable(int RowsPerPage, int PageNumber)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@RowsPerPage", DbType.Int32, RowsPerPage),
				databaseLayer.GetParameter("@PageNumber", DbType.Int32, PageNumber)
			};
			return databaseLayer.ExecuteQuery("EXEC sp_DataSync_LoadCustomCourses @RowsPerPage,@PageNumber", parameters);
		}

		// Token: 0x04000415 RID: 1045
		private DatabaseLayer DatabaseManager;

		// Token: 0x04000416 RID: 1046
		private ILookupSubjectDAO _lsd;

		// Token: 0x04000417 RID: 1047
		private LookupCourseDAO _lcd;
	}
}
