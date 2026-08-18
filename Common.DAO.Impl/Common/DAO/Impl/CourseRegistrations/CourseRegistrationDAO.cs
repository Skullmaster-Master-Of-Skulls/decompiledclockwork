using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.Common.DAO.CourseRegistrations;
using TechnoPro.Common.DAO.Impl.LookupCourses;
using TechnoPro.Common.DAO.Impl.People;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.CourseRegistrations;
using TechnoPro.Common.Public.Entities.DataSync;
using TechnoPro.Common.Public.Entities.LookupCourses;
using TechnoPro.Common.Public.Entities.People;
using TechnoPro.Common.Public.Entities.StudentAccommodationRequests;

namespace TechnoPro.Common.DAO.Impl.CourseRegistrations
{
	// Token: 0x02000106 RID: 262
	public class CourseRegistrationDAO : ICourseRegistrationDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000766 RID: 1894 RVA: 0x0004B954 File Offset: 0x00049B54
		public CourseRegistrationDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext2 = this.OpContext;
			this.DatabaseManager = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext2 != null) ? opContext2.TenantId : null);
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000767 RID: 1895 RVA: 0x0004B98B File Offset: 0x00049B8B
		// (set) Token: 0x06000768 RID: 1896 RVA: 0x0004B993 File Offset: 0x00049B93
		public OperationContext OpContext { get; set; }

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000769 RID: 1897 RVA: 0x0004B99C File Offset: 0x00049B9C
		private PeopleDAO peopleDao
		{
			get
			{
				bool flag = this.pd == null;
				if (flag)
				{
					this.pd = new PeopleDAO(this.OpContext);
				}
				return this.pd;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600076A RID: 1898 RVA: 0x0004B9D4 File Offset: 0x00049BD4
		private LookupCourseDAO lookupCourseDao
		{
			get
			{
				bool flag = this.ld == null;
				if (flag)
				{
					this.ld = new LookupCourseDAO(this.OpContext);
				}
				return this.ld;
			}
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x0004BA0C File Offset: 0x00049C0C
		internal static List<T> GetCourseRegistrationsFromReader0<T>(string colNamePrefix, IDataReader reader, OperationContext opContext) where T : CourseRegistration
		{
			List<T> list = new List<T>();
			T t = default(T);
			while (reader.Read())
			{
				int num = (int)reader["lucourseid"];
				int num2 = (int)reader["personid"];
				bool flag = t == null || t.Course.LuCourseId != num || t.Student.PersonId != num2;
				if (flag)
				{
					LookupCourse course = new LookupCourse();
					LookupCourseDAO.GetMainCourseFromReader(course, "", reader);
					t = CourseRegistrationDAO.GetCourseRegistrationFromRecord0<T>(course, reader, opContext);
					list.Add(t);
				}
				LookupCourseDAO.AddCourseInfoFromReader(t.Course, colNamePrefix, reader);
				bool flag2 = PeopleDAO.ReaderContainsColumn(reader, "pExemptAssignmentFromDataSync") && reader["pinstructorid"] != DBNull.Value && reader["pExemptAssignmentFromDataSync"] != DBNull.Value && Convert.ToBoolean(reader["pExemptAssignmentFromDataSync"]);
				if (flag2)
				{
					t.ExemptedInstructorAssignments.Add((int)reader["pinstructorid"]);
				}
				bool flag3 = PeopleDAO.ReaderContainsColumn(reader, "p3ExemptAssignmentFromDataSync") && reader["p3instructorid"] != DBNull.Value && reader["p3ExemptAssignmentFromDataSync"] != DBNull.Value && Convert.ToBoolean(reader["p3ExemptAssignmentFromDataSync"]);
				if (flag3)
				{
					t.ExemptedInstructorAssignments.Add((int)reader["p3instructorid"]);
				}
			}
			return list;
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x0004BBB8 File Offset: 0x00049DB8
		internal static List<CourseRegistration> GetCourseRegistrationsFromReader(string colNamePrefix, IDataReader reader, OperationContext opContext)
		{
			return CourseRegistrationDAO.GetCourseRegistrationsFromReader0<CourseRegistration>(colNamePrefix, reader, opContext);
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x0004BBD4 File Offset: 0x00049DD4
		internal static T GetCourseRegistrationFromRecord0<T>(LookupCourse course, IDataReader record, OperationContext opContext) where T : CourseRegistration
		{
			bool flag = record["registrationstatus"] == DBNull.Value;
			eRegistrationStatus registrationStatus;
			if (flag)
			{
				registrationStatus = eRegistrationStatus.Normal;
			}
			else
			{
				int num = (int)record["registrationstatus"];
				registrationStatus = (eRegistrationStatus)(Enum.IsDefined(typeof(eRegistrationStatus), num) ? num : 0);
			}
			CourseRequestBase courseRequestBaseFromRecord = CourseRegistrationDAO.GetCourseRequestBaseFromRecord(record, opContext);
			T t = Activator.CreateInstance<T>();
			t.ExemptedInstructorAssignments = new List<int>();
			t.Course = course;
			t.Student = PeopleDAO.GetPersonFromReader("", record, opContext, null);
			t.CourseNote = record["coursenote"].ToString();
			t.CoursesId = (int)record["coursesid"];
			t.DateAdded = (DateTime)record["dateadded"];
			t.DateInstructorLastViewed = ((record["dateinstructorlastviewed"] != DBNull.Value) ? new DateTime?((DateTime)record["dateinstructorlastviewed"]) : null);
			t.DateStudentLastViewed = ((record["datestudentlastviewed"] != DBNull.Value) ? new DateTime?((DateTime)record["datestudentlastviewed"]) : null);
			t.DateLetterIssued = ((record["dateletterissued"] != DBNull.Value) ? new DateTime?((DateTime)record["dateletterissued"]) : null);
			t.DateLetterReturned = ((record["dateletterreturned"] != DBNull.Value) ? new DateTime?((DateTime)record["dateletterreturned"]) : null);
			t.RegistrationStatus = registrationStatus;
			t.WhoAdded = PeopleDAO.GetPersonFromReader("whoadded", record, opContext, null);
			t.CourseAccommodationRequestBase = courseRequestBaseFromRecord;
			CourseRegistrationWithStudentSpecificInfo courseRegistrationWithStudentSpecificInfo = t as CourseRegistrationWithStudentSpecificInfo;
			bool flag2 = courseRegistrationWithStudentSpecificInfo != null;
			if (flag2)
			{
				courseRegistrationWithStudentSpecificInfo.StudentSpecificInfo = CourseRegistrationDAO.GetCourseRegistrationStudentSpecificInfoFromRecord(record);
			}
			return t;
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x0004BE24 File Offset: 0x0004A024
		internal static CourseStudentSpecific GetCourseRegistrationStudentSpecificInfoFromRecord(IDataReader record)
		{
			string s = (record["tuitioncost"] is DBNull) ? "" : record["tuitioncost"].ToString().Trim();
			double tuitionCost;
			bool flag = !double.TryParse(s, out tuitionCost);
			if (flag)
			{
				tuitionCost = 0.0;
			}
			return new CourseStudentSpecific
			{
				Grade = ((record["GradeNumber"] is DBNull) ? 0m : ((decimal)record["GradeNumber"])),
				InProgressGrade = ((record["InProgressGradeNumber"] is DBNull) ? 0m : ((decimal)record["InProgressGradeNumber"])),
				GradeLetter = ((record["GradeLetter"] is DBNull) ? "" : record["GradeLetter"].ToString().Trim()),
				InProgressGradeLetter = ((record["InProgressGradeLetter"] is DBNull) ? "" : record["InProgressGradeLetter"].ToString().Trim()),
				TuitionCost = tuitionCost,
				RegistrationDate = ((record["RegistrationDate"] is DBNull) ? null : new DateTime?((DateTime)record["RegistrationDate"])),
				RegistrationNote = ((record["RegistrationNote"] is DBNull) ? string.Empty : ((string)record["RegistrationNote"]))
			};
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x0004BFC4 File Offset: 0x0004A1C4
		internal static CourseRequestBase GetCourseRequestBaseFromRecord(IDataReader record, OperationContext opContext)
		{
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, (opContext != null) ? opContext.TenantId : null);
			bool flag = !PeopleDAO.ReaderContainsColumn(record, "StudentCourseAccommodationRequestId") || record["StudentCourseAccommodationRequestId"] == DBNull.Value;
			CourseRequestBase result;
			if (flag)
			{
				result = null;
			}
			else
			{
				int studentCourseAccommodationRequestId = (int)record["StudentCourseAccommodationRequestId"];
				int num = (record["rstatus"] == DBNull.Value) ? 0 : ((int)record["rstatus"]);
				bool flag2 = Enum.IsDefined(typeof(eStudentCourseAccommodationRequestStatus), num);
				eStudentCourseAccommodationRequestStatus status;
				if (flag2)
				{
					status = (eStudentCourseAccommodationRequestStatus)num;
				}
				else
				{
					status = eStudentCourseAccommodationRequestStatus.Unknown;
				}
				result = new CourseRequestBase
				{
					StudentCourseAccommodationRequestId = studentCourseAccommodationRequestId,
					CoursesId = (int)record["coursesid"],
					DateEntered = ((record["rdateentered"] == DBNull.Value) ? DateTime.MinValue : ((DateTime)record["rdateentered"])),
					DateRequested = ((record["rdateentered"] == DBNull.Value) ? null : new DateTime?((DateTime)record["rdateentered"])),
					Note1 = ((record["rnote1"] == DBNull.Value) ? "" : databaseLayer.Encryption.Decrypt((byte[])record["rnote1"])),
					Note2 = ((record["rnote2"] == DBNull.Value) ? "" : databaseLayer.Encryption.Decrypt((byte[])record["rnote2"])),
					Status = status
				};
			}
			return result;
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x0004C180 File Offset: 0x0004A380
		public void DeleteCourseRegistration(int CoursesId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@coursesid", DbType.Int32, CoursesId)
			};
			this.DatabaseManager.ExecuteNonQuery("DELETE FROM courses WHERE coursesid=@coursesid", parameters);
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x0004C1C4 File Offset: 0x0004A3C4
		public int[] LoadStudentCourseRegistrationLuCourseIds(int studentPersonId, bool includeDroppedCourses)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			DbParameter[] parameters = new DbParameter[]
			{
				clockWork.GetParameter("@pid", DbType.Int32, studentPersonId),
				clockWork.GetParameter("@includedropped", DbType.Boolean, includeDroppedCourses)
			};
			List<int> list = new List<int>();
			int[] result;
			using (IDataReader dataReader = clockWork.ExecuteQueryReader("SELECT DISTINCT lucourseid FROM courses WHERE personid=@pid AND (@includedropped=1 OR (registrationstatus IS NULL OR NOT registrationstatus=2))", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					while (dataReader.Read())
					{
						int num = (dataReader["lucourseid"] is DBNull) ? 0 : ((int)dataReader["lucourseid"]);
						bool flag2 = num > 0;
						if (flag2)
						{
							list.Add(num);
						}
					}
					result = list.ToArray();
				}
			}
			return result;
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x0004C2A0 File Offset: 0x0004A4A0
		public void ChangeCourseRegistrationStatus(int CoursesId, eRegistrationStatus NewStatus)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@coursesid", DbType.Int32, CoursesId),
				this.DatabaseManager.GetParameter("@registrationstatus", DbType.Int32, (int)NewStatus)
			};
			this.DatabaseManager.ExecuteNonQuery("UPDATE courses SET registrationstatus=@registrationstatus WHERE coursesid=@coursesid", parameters);
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x0004C300 File Offset: 0x0004A500
		public CourseRegistration RegisterStudentInCourse(int StudentPid, int Lucid)
		{
			return this.RegisterStudentInCourse(StudentPid, Lucid, null);
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x0004C324 File Offset: 0x0004A524
		public T RegisterStudentInCourse0<T>(int StudentPid, int Lucid, bool? ExemptCourseFromDataSyncForStudent) where T : CourseRegistration
		{
			DbParameter dbParameter = (ExemptCourseFromDataSyncForStudent != null) ? this.DatabaseManager.GetParameter("@isexempt", DbType.Boolean, ExemptCourseFromDataSyncForStudent.Value) : this.DatabaseManager.GetParameter("@isexempt", DbType.Boolean, DBNull.Value);
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@pid", DbType.Int32, StudentPid),
				this.DatabaseManager.GetParameter("@lucid", DbType.Int32, Lucid),
				this.DatabaseManager.GetParameter("@whoami", DbType.Int32, this.OpContext.WhoAmI),
				dbParameter
			};
			T result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("IF EXISTS(SELECT coursesid FROM courses WHERE personid=@pid AND lucourseid=@lucid)\r\nBEGIN\r\n    UPDATE courses SET registrationstatus=NULL,exemptfromdatasync=COALESCE(@isexempt,exemptfromdatasync) WHERE personid=@pid AND lucourseid=@lucid\r\n    SELECT coursesid FROM courses WHERE personid=@pid AND lucourseid=@lucid\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO courses (personid,lucourseid,whoadded,dateadded,exemptfromdatasync) VALUES (@pid,@lucid,@whoami,getdate(),COALESCE(@isexempt,0));\r\n    SELECT CAST(SCOPE_IDENTITY() AS int) AS coursesid\r\nEND", parameters))
			{
				bool flag = dataReader == null || !dataReader.Read();
				if (flag)
				{
					result = default(T);
				}
				else
				{
					int num = (int)dataReader[0];
					T t = this.LoadCourseRegistration0<T>(StudentPid, Lucid);
					bool flag2 = t == null || ExemptCourseFromDataSyncForStudent == null;
					if (flag2)
					{
						result = t;
					}
					else
					{
						bool flag3 = t.IsExemptFromDataSync == ExemptCourseFromDataSyncForStudent.Value;
						if (flag3)
						{
							result = t;
						}
						else
						{
							eRegistrationStatus newStatus = ExemptCourseFromDataSyncForStudent.Value ? eRegistrationStatus.NormalAndExemptFromDataSync : eRegistrationStatus.Normal;
							this.ChangeCourseRegistrationStatus(t.CoursesId, newStatus);
							t = this.LoadCourseRegistration0<T>(StudentPid, Lucid);
							result = t;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x0004C4BC File Offset: 0x0004A6BC
		public CourseRegistration RegisterStudentInCourse(int StudentPid, int Lucid, bool? ExemptCourseFromDataSyncForStudent)
		{
			return this.RegisterStudentInCourse0<CourseRegistration>(StudentPid, Lucid, ExemptCourseFromDataSyncForStudent);
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x0004C4D8 File Offset: 0x0004A6D8
		public CourseRegistration LoadCourseRegistration(int StudentPid, int Lucid)
		{
			return this.LoadCourseRegistration0<CourseRegistration>(StudentPid, Lucid);
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x0004C4F4 File Offset: 0x0004A6F4
		public T LoadCourseRegistration0<T>(int StudentPid, int Lucid) where T : CourseRegistration
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@pid", DbType.Int32, StudentPid),
				this.DatabaseManager.GetParameter("@lucid", DbType.Int32, Lucid)
			};
			T result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT    c.coursesid,c.personid,c.lucourseid,c.registrationstatus,c.dateadded,\r\nc.whoadded AS whoaddedpersonid,pc.firstname AS whoaddedfirstname,pc.lastname AS whoaddedlastname,pc.student_no AS whoaddedstudent_no,\r\nc.dateletterissued,c.dateletterreturned,c.needsnotes,c.extracode,c.coursenote,c.notetakerrequired,\r\nc.datestudentlastviewed,c.dateinstructorlastviewed,c.wholastviewed,c.instructorconfirmed,c.exemptfromdatasync,\r\nluc.lucourseid,luc.startdate,luc.enddate,luc.duration,luc.term,luc.subjectid,luc.externalid,luc.exemptfromdatasync AS lucexemptfromdatasync\r\n        ,lucd.lookupstring AS subjectcode,lucd.altlookupstring AS subjectdescription\r\n        ,luc.course,luc.timeofday,luc.[section]\r\n        ,luc.campus,luc.department,luc.location\r\n        ,luc.instructorid AS pinstructorid,lucd2.altlookupstring AS pinstructorname,lucd2.email AS pinstructoremail,lucd2.phone AS pinstructorphone,lucd2.username AS pinstructorusername,lucd2.exemptfromdatasync AS pexemptfromdatasync,lucd2.id AS pinstructoremployeeid,lucd2.externalid AS pinstructorexternalid\r\n        ,luc.ExemptAssignmentFromDataSync AS pExemptAssignmentFromDataSync\r\n        ,luc.BatchDataSyncLogId,luc.credits\r\n        ,lci.instructorid AS p3instructorid,lucd3.altlookupstring AS p3instructorname,lucd3.email AS p3instructoremail,lucd3.phone AS p3instructorphone,lucd3.username AS p3instructorusername,lucd3.exemptfromdatasync AS p3exemptfromdatasync,lucd3.id AS p3instructoremployeeid,lucd3.externalid AS p3instructorexternalid\r\n        ,lci.ExemptAssignmentFromDataSync AS p3ExemptAssignmentFromDataSync\r\n        ,tt.timetableid\r\n        ,tt.sunstartminutes,tt.sunendminutes,tt.monstartminutes,tt.monendminutes,tt.tuestartminutes,tt.tueendminutes\r\n        ,tt.wedstartminutes,tt.wedendminutes,tt.thustartminutes,tt.thuendminutes,tt.fristartminutes,tt.friendminutes\r\n        ,tt.satstartminutes,tt.satendminutes,tt.sunroom,tt.monroom,tt.tueroom,tt.wedroom,tt.thuroom,tt.friroom,tt.satroom,\r\n        luc.alternatecontactid,ac.altname,ac.altemail,ac.altphone,ac.altusername,ac.externalid,ac.altpermissionlevel,\r\n        lucac.alternatecontactid AS secondaryalternatecontactid,\r\n        ac2.altname AS secondaryaltname,ac2.altemail AS secondaryaltemail,ac2.altphone AS secondaryaltphone,\r\n        ac2.altusername AS secondaryaltusername,ac2.externalid AS secondaryexternalid,\r\n        ac2.altpermissionlevel AS secondaryaltpermissionlevel,\r\n        p.firstname,p.lastname,p.student_no,\r\n        scar.StudentCourseAccommodationRequestId,scar.[status] AS rstatus,scar.daterequested AS rdateapproved,\r\n\t\tscar.dateentered AS rdateentered,scar.note1 AS rnote1,scar.note2 AS rnote2,\r\n        c.tuitioncost,c.GradeNumber,c.GradeLetter,c.InProgressGradeNumber,c.InProgressGradeLetter,\r\n        c.RegistrationDate,c.RegistrationNote\r\nFROM    courses c LEFT JOIN lucourses luc ON luc.lucourseid=c.lucourseid\r\n        LEFT JOIN people p ON p.personid=c.personid\r\n        LEFT JOIN people pc ON pc.personid=c.whoadded\r\n        LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid\r\n        LEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luc.instructorid\r\n        LEFT JOIN lucourseinstructor lci ON lci.lucourseid=luc.lucourseid\r\n        LEFT JOIN lucoursedata lucd3 ON lucd3.lucoursedataid=lci.instructorid\r\n        LEFT JOIN timetable tt ON tt.timetabletype='C' AND tt.lucourseid=luc.lucourseid\r\n        LEFT JOIN lucoursealternatecontact ac ON ac.alternatecontactid=luc.alternatecontactid\r\n        LEFT JOIN LuCourseAltContact lucac ON lucac.lucourseid=luc.lucourseid\r\n        LEFT JOIN lucoursealternatecontact ac2 ON ac2.alternatecontactid=lucac.alternatecontactid\r\n        LEFT JOIN StudentCourseAccommodationRequest scar ON scar.PersonId=c.PersonId AND scar.lucourseid=c.lucourseid\r\nWHERE   c.personid=@pid AND c.lucourseid=@lucid\r\nORDER BY c.lucourseid", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = default(T);
				}
				else
				{
					List<T> courseRegistrationsFromReader = CourseRegistrationDAO.GetCourseRegistrationsFromReader0<T>("", dataReader, this.OpContext);
					result = ((courseRegistrationsFromReader.Count > 0) ? courseRegistrationsFromReader[0] : default(T));
				}
			}
			return result;
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x0004C5B8 File Offset: 0x0004A7B8
		public List<CourseRegistrationWithStudentSpecificInfo> LoadStudentsCoursesWithStudentSpecificInfo(DateTime StartDate, DateTime EndDate, int PersonId, bool IncludeDroppedCourses)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@startdate", DbType.DateTime, StartDate),
				this.DatabaseManager.GetParameter("@enddate", DbType.DateTime, EndDate),
				this.DatabaseManager.GetParameter("@includedropped", DbType.Boolean, IncludeDroppedCourses),
				this.DatabaseManager.GetParameter("@pid", DbType.Int32, PersonId)
			};
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT    c.coursesid,c.personid,c.lucourseid,c.registrationstatus,c.dateadded,\r\nc.whoadded AS whoaddedpersonid,pc.firstname AS whoaddedfirstname,pc.lastname AS whoaddedlastname,pc.student_no AS whoaddedstudent_no,\r\nc.dateletterissued,c.dateletterreturned,c.needsnotes,c.extracode,c.coursenote,c.notetakerrequired,\r\nc.datestudentlastviewed,c.dateinstructorlastviewed,c.wholastviewed,c.instructorconfirmed,c.exemptfromdatasync,\r\nluc.lucourseid,luc.startdate,luc.enddate,luc.duration,luc.term,luc.subjectid,luc.externalid,luc.exemptfromdatasync AS lucexemptfromdatasync\r\n        ,lucd.lookupstring AS subjectcode,lucd.altlookupstring AS subjectdescription\r\n        ,luc.course,luc.timeofday,luc.[section]\r\n        ,luc.campus,luc.department,luc.location\r\n        ,luc.instructorid AS pinstructorid,lucd2.altlookupstring AS pinstructorname,lucd2.email AS pinstructoremail,lucd2.phone AS pinstructorphone,lucd2.username AS pinstructorusername,lucd2.exemptfromdatasync AS pexemptfromdatasync,lucd2.id AS pinstructoremployeeid,lucd2.externalid AS pinstructorexternalid\r\n        ,luc.ExemptAssignmentFromDataSync AS pExemptAssignmentFromDataSync\r\n        ,luc.BatchDataSyncLogId,luc.credits\r\n        ,lci.instructorid AS p3instructorid,lucd3.altlookupstring AS p3instructorname,lucd3.email AS p3instructoremail,lucd3.phone AS p3instructorphone,lucd3.username AS p3instructorusername,lucd3.exemptfromdatasync AS p3exemptfromdatasync,lucd3.id AS p3instructoremployeeid,lucd3.externalid AS p3instructorexternalid\r\n        ,lci.ExemptAssignmentFromDataSync AS p3ExemptAssignmentFromDataSync\r\n        ,tt.timetableid\r\n        ,tt.sunstartminutes,tt.sunendminutes,tt.monstartminutes,tt.monendminutes,tt.tuestartminutes,tt.tueendminutes\r\n        ,tt.wedstartminutes,tt.wedendminutes,tt.thustartminutes,tt.thuendminutes,tt.fristartminutes,tt.friendminutes\r\n        ,tt.satstartminutes,tt.satendminutes,tt.sunroom,tt.monroom,tt.tueroom,tt.wedroom,tt.thuroom,tt.friroom,tt.satroom,\r\n        luc.alternatecontactid,ac.altname,ac.altemail,ac.altphone,ac.altusername,ac.externalid,ac.altpermissionlevel,\r\n        lucac.alternatecontactid AS secondaryalternatecontactid,\r\n        ac2.altname AS secondaryaltname,ac2.altemail AS secondaryaltemail,ac2.altphone AS secondaryaltphone,\r\n        ac2.altusername AS secondaryaltusername,ac2.externalid AS secondaryexternalid,\r\n        ac2.altpermissionlevel AS secondaryaltpermissionlevel,\r\n        p.firstname,p.lastname,p.student_no,\r\n        scar.StudentCourseAccommodationRequestId,scar.[status] AS rstatus,scar.daterequested AS rdateapproved,\r\n\t\tscar.dateentered AS rdateentered,scar.note1 AS rnote1,scar.note2 AS rnote2,\r\n        c.tuitioncost,c.GradeNumber,c.GradeLetter,c.InProgressGradeNumber,c.InProgressGradeLetter,\r\n        c.RegistrationDate,c.RegistrationNote\r\nFROM    courses c LEFT JOIN lucourses luc ON luc.lucourseid=c.lucourseid\r\n        LEFT JOIN people p ON p.personid=c.personid\r\n        LEFT JOIN people pc ON pc.personid=c.whoadded\r\n        LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid\r\n        LEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luc.instructorid\r\n        LEFT JOIN lucourseinstructor lci ON lci.lucourseid=luc.lucourseid\r\n        LEFT JOIN lucoursedata lucd3 ON lucd3.lucoursedataid=lci.instructorid\r\n        LEFT JOIN timetable tt ON tt.timetabletype='C' AND tt.lucourseid=luc.lucourseid\r\n        LEFT JOIN lucoursealternatecontact ac ON ac.alternatecontactid=luc.alternatecontactid\r\n        LEFT JOIN LuCourseAltContact lucac ON lucac.lucourseid=luc.lucourseid\r\n        LEFT JOIN lucoursealternatecontact ac2 ON ac2.alternatecontactid=lucac.alternatecontactid\r\n        LEFT JOIN StudentCourseAccommodationRequest scar ON scar.PersonId=c.PersonId AND scar.lucourseid=c.lucourseid\r\nWHERE   c.personid=@pid AND NOT ( luc.enddate <= @startdate OR luc.startdate > @enddate)\r\n        AND (@includedropped=1 OR (c.registrationstatus IS NULL OR NOT c.registrationstatus=2))\r\nORDER BY c.lucourseid", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					return CourseRegistrationDAO.GetCourseRegistrationsFromReader0<CourseRegistrationWithStudentSpecificInfo>("", dataReader, this.OpContext);
				}
			}
			return null;
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x0004C68C File Offset: 0x0004A88C
		public List<CourseRegistration> LoadStudentsCourses(DateTime StartDate, DateTime EndDate, int PersonId, bool IncludeDroppedCourses)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@startdate", DbType.DateTime, StartDate),
				this.DatabaseManager.GetParameter("@enddate", DbType.DateTime, EndDate),
				this.DatabaseManager.GetParameter("@includedropped", DbType.Boolean, IncludeDroppedCourses),
				this.DatabaseManager.GetParameter("@pid", DbType.Int32, PersonId)
			};
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT    c.coursesid,c.personid,c.lucourseid,c.registrationstatus,c.dateadded,\r\nc.whoadded AS whoaddedpersonid,pc.firstname AS whoaddedfirstname,pc.lastname AS whoaddedlastname,pc.student_no AS whoaddedstudent_no,\r\nc.dateletterissued,c.dateletterreturned,c.needsnotes,c.extracode,c.coursenote,c.notetakerrequired,\r\nc.datestudentlastviewed,c.dateinstructorlastviewed,c.wholastviewed,c.instructorconfirmed,c.exemptfromdatasync,\r\nluc.lucourseid,luc.startdate,luc.enddate,luc.duration,luc.term,luc.subjectid,luc.externalid,luc.exemptfromdatasync AS lucexemptfromdatasync\r\n        ,lucd.lookupstring AS subjectcode,lucd.altlookupstring AS subjectdescription\r\n        ,luc.course,luc.timeofday,luc.[section]\r\n        ,luc.campus,luc.department,luc.location\r\n        ,luc.instructorid AS pinstructorid,lucd2.altlookupstring AS pinstructorname,lucd2.email AS pinstructoremail,lucd2.phone AS pinstructorphone,lucd2.username AS pinstructorusername,lucd2.exemptfromdatasync AS pexemptfromdatasync,lucd2.id AS pinstructoremployeeid,lucd2.externalid AS pinstructorexternalid\r\n        ,luc.ExemptAssignmentFromDataSync AS pExemptAssignmentFromDataSync\r\n        ,luc.BatchDataSyncLogId,luc.credits\r\n        ,lci.instructorid AS p3instructorid,lucd3.altlookupstring AS p3instructorname,lucd3.email AS p3instructoremail,lucd3.phone AS p3instructorphone,lucd3.username AS p3instructorusername,lucd3.exemptfromdatasync AS p3exemptfromdatasync,lucd3.id AS p3instructoremployeeid,lucd3.externalid AS p3instructorexternalid\r\n        ,lci.ExemptAssignmentFromDataSync AS p3ExemptAssignmentFromDataSync\r\n        ,tt.timetableid\r\n        ,tt.sunstartminutes,tt.sunendminutes,tt.monstartminutes,tt.monendminutes,tt.tuestartminutes,tt.tueendminutes\r\n        ,tt.wedstartminutes,tt.wedendminutes,tt.thustartminutes,tt.thuendminutes,tt.fristartminutes,tt.friendminutes\r\n        ,tt.satstartminutes,tt.satendminutes,tt.sunroom,tt.monroom,tt.tueroom,tt.wedroom,tt.thuroom,tt.friroom,tt.satroom,\r\n        luc.alternatecontactid,ac.altname,ac.altemail,ac.altphone,ac.altusername,ac.externalid,ac.altpermissionlevel,\r\n        lucac.alternatecontactid AS secondaryalternatecontactid,\r\n        ac2.altname AS secondaryaltname,ac2.altemail AS secondaryaltemail,ac2.altphone AS secondaryaltphone,\r\n        ac2.altusername AS secondaryaltusername,ac2.externalid AS secondaryexternalid,\r\n        ac2.altpermissionlevel AS secondaryaltpermissionlevel,\r\n        p.firstname,p.lastname,p.student_no,\r\n        scar.StudentCourseAccommodationRequestId,scar.[status] AS rstatus,scar.daterequested AS rdateapproved,\r\n\t\tscar.dateentered AS rdateentered,scar.note1 AS rnote1,scar.note2 AS rnote2,\r\n        c.tuitioncost,c.GradeNumber,c.GradeLetter,c.InProgressGradeNumber,c.InProgressGradeLetter,\r\n        c.RegistrationDate,c.RegistrationNote\r\nFROM    courses c LEFT JOIN lucourses luc ON luc.lucourseid=c.lucourseid\r\n        LEFT JOIN people p ON p.personid=c.personid\r\n        LEFT JOIN people pc ON pc.personid=c.whoadded\r\n        LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid\r\n        LEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luc.instructorid\r\n        LEFT JOIN lucourseinstructor lci ON lci.lucourseid=luc.lucourseid\r\n        LEFT JOIN lucoursedata lucd3 ON lucd3.lucoursedataid=lci.instructorid\r\n        LEFT JOIN timetable tt ON tt.timetabletype='C' AND tt.lucourseid=luc.lucourseid\r\n        LEFT JOIN lucoursealternatecontact ac ON ac.alternatecontactid=luc.alternatecontactid\r\n        LEFT JOIN LuCourseAltContact lucac ON lucac.lucourseid=luc.lucourseid\r\n        LEFT JOIN lucoursealternatecontact ac2 ON ac2.alternatecontactid=lucac.alternatecontactid\r\n        LEFT JOIN StudentCourseAccommodationRequest scar ON scar.PersonId=c.PersonId AND scar.lucourseid=c.lucourseid\r\nWHERE   c.personid=@pid AND NOT ( luc.enddate <= @startdate OR luc.startdate > @enddate)\r\n        AND (@includedropped=1 OR (c.registrationstatus IS NULL OR NOT c.registrationstatus=2))\r\nORDER BY c.lucourseid", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					return CourseRegistrationDAO.GetCourseRegistrationsFromReader0<CourseRegistration>("", dataReader, this.OpContext);
				}
			}
			return null;
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x0004C760 File Offset: 0x0004A960
		public IList<DateTime> GetUniqueCourseRegistrationStartDatesByStudent(int PersonId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@pid", DbType.Int32, PersonId)
			};
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT DISTINCT x.startdate FROM\r\n(\r\n    SELECT    luc.startdate \r\n    FROM        courses c LEFT JOIN lucourses luc ON luc.lucourseid=c.lucourseid\r\n    WHERE       c.personid=@pid AND (c.registrationstatus IS NULL OR NOT c.registrationstatus=2)\r\n UNION\r\n    SELECT    luc.enddate AS startdate\r\n    FROM        courses c LEFT JOIN lucourses luc ON luc.lucourseid=c.lucourseid\r\n    WHERE       c.personid=@pid AND (c.registrationstatus IS NULL OR NOT c.registrationstatus=2)\r\n) x\r\nORDER BY x.startdate", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					List<DateTime> list = new List<DateTime>();
					while (dataReader.Read())
					{
						bool flag2 = dataReader["startdate"] != DBNull.Value;
						if (flag2)
						{
							DateTime date = ((DateTime)dataReader["startdate"]).Date;
							bool flag3 = !list.Contains(date);
							if (flag3)
							{
								list.Add(date);
							}
						}
					}
					return list;
				}
			}
			return null;
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x0004C840 File Offset: 0x0004AA40
		public void MergeCourseRegistrations(int PersonIdNew, int PersonIdOld)
		{
			DateTime startDate = DateTime.Now.AddYears(-100);
			DateTime endDate = DateTime.Now.AddYears(100);
			List<CourseRegistration> keepCourses = this.LoadStudentsCourses(startDate, endDate, PersonIdNew, true);
			List<CourseRegistration> discardCourses = this.LoadStudentsCourses(startDate, endDate, PersonIdOld, true);
			IEnumerable<CourseRegistration> enumerable = from g in keepCourses
			where g.RegistrationStatus == eRegistrationStatus.Dropped && discardCourses.FirstOrDefault((CourseRegistration h) => h.RegistrationStatus != eRegistrationStatus.Dropped && h.Course.LuCourseId == g.Course.LuCourseId) != null
			select g;
			foreach (CourseRegistration courseRegistration in enumerable)
			{
				this.DeleteCourseRegistration(courseRegistration.CoursesId);
				keepCourses.Remove(courseRegistration);
			}
			Dictionary<CourseRegistration, CourseRegistration> dictionary = new Dictionary<CourseRegistration, CourseRegistration>();
			using (List<CourseRegistration>.Enumerator enumerator2 = keepCourses.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					CourseRegistration c = enumerator2.Current;
					CourseRegistration courseRegistration2 = discardCourses.FirstOrDefault((CourseRegistration g) => g.Course.LuCourseId == c.Course.LuCourseId);
					bool flag = courseRegistration2 != null;
					if (flag)
					{
						dictionary.Add(c, courseRegistration2);
					}
				}
			}
			bool flag2 = dictionary.Count > 0;
			if (flag2)
			{
				foreach (KeyValuePair<CourseRegistration, CourseRegistration> keyValuePair in dictionary)
				{
					DateTime? maxDate = this.GetMaxDate(keyValuePair.Key.DateLetterIssued, keyValuePair.Value.DateLetterIssued);
					DateTime? maxDate2 = this.GetMaxDate(keyValuePair.Key.DateLetterReturned, keyValuePair.Value.DateLetterReturned);
					DateTime? maxDate3 = this.GetMaxDate(keyValuePair.Key.DateStudentLastViewed, keyValuePair.Value.DateStudentLastViewed);
					DateTime? maxDate4 = this.GetMaxDate(keyValuePair.Key.DateInstructorLastViewed, keyValuePair.Value.DateInstructorLastViewed);
					DbParameter[] parameters = new DbParameter[]
					{
						this.DatabaseManager.GetParameter("@coursesid", DbType.Int32, keyValuePair.Key.CoursesId),
						this.DatabaseManager.GetParameter("@dateletterissued", DbType.DateTime, (maxDate != null) ? maxDate.Value : DBNull.Value),
						this.DatabaseManager.GetParameter("@dateletterreturned", DbType.DateTime, (maxDate2 != null) ? maxDate2.Value : DBNull.Value),
						this.DatabaseManager.GetParameter("@datestudentlastviewed", DbType.DateTime, (maxDate3 != null) ? maxDate3.Value : DBNull.Value),
						this.DatabaseManager.GetParameter("@dateinstructorlastviewed", DbType.DateTime, (maxDate4 != null) ? maxDate4.Value : DBNull.Value)
					};
					this.DatabaseManager.ExecuteNonQuery("UPDATE courses SET dateletterissued=@dateletterissued,dateletterreturned=@dateletterreturned,datestudentlastviewed=@datestudentlastviewed,dateinstructorlastviewed=@dateinstructorlastviewed\r\nWHERE coursesid=@coursesid", parameters);
				}
				foreach (KeyValuePair<CourseRegistration, CourseRegistration> keyValuePair2 in dictionary)
				{
				}
				foreach (KeyValuePair<CourseRegistration, CourseRegistration> keyValuePair3 in dictionary)
				{
					bool flag3 = keyValuePair3.Key.IsExemptFromDataSync != keyValuePair3.Value.IsExemptFromDataSync;
					if (flag3)
					{
						DbParameter[] parameters = new DbParameter[]
						{
							this.DatabaseManager.GetParameter("@coursesid", DbType.Int32, keyValuePair3.Key.CoursesId),
							this.DatabaseManager.GetParameter("@exemptfromdatasync", DbType.Boolean, true)
						};
						this.DatabaseManager.ExecuteNonQuery("UPDATE courses SET exemptfromdatasync=@exemptfromdatasync WHERE coursesid=@coursesid", parameters);
					}
				}
				foreach (KeyValuePair<CourseRegistration, CourseRegistration> keyValuePair4 in dictionary)
				{
					string text = (keyValuePair4.Key.CourseNote ?? "").Trim();
					string text2 = (keyValuePair4.Value.CourseNote ?? "").Trim();
					bool flag4 = text != text2;
					if (flag4)
					{
						DbParameter[] parameters = new DbParameter[]
						{
							this.DatabaseManager.GetParameter("@coursesid", DbType.Int32, keyValuePair4.Key.CoursesId),
							this.DatabaseManager.GetParameter("@coursenote", DbType.String, (text + " " + text2).Trim())
						};
						this.DatabaseManager.ExecuteNonQuery("UPDATE courses SET coursenote=@coursenote WHERE coursesid=@coursesid", parameters);
					}
				}
			}
			List<CourseRegistration> list = (from g in discardCourses
			where g.RegistrationStatus != eRegistrationStatus.Dropped && keepCourses.FirstOrDefault((CourseRegistration h) => h.Course.LuCourseId == g.Course.LuCourseId) == null
			select g).ToList<CourseRegistration>();
			foreach (CourseRegistration courseRegistration3 in list)
			{
				DbParameter[] parameters = new DbParameter[]
				{
					this.DatabaseManager.GetParameter("@coursesid", DbType.Int32, courseRegistration3.CoursesId),
					this.DatabaseManager.GetParameter("@newpid", DbType.Int32, PersonIdNew)
				};
				this.DatabaseManager.ExecuteNonQuery("UPDATE courses SET personid=@newpid WHERE coursesid=@coursesid", parameters);
			}
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x0004CE68 File Offset: 0x0004B068
		private DateTime? GetMaxDate(DateTime? d1, DateTime? d2)
		{
			bool flag = d1 == null && d2 == null;
			DateTime? result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = d1 == null;
				if (flag2)
				{
					result = d2;
				}
				else
				{
					bool flag3 = d2 == null;
					if (flag3)
					{
						result = d1;
					}
					else
					{
						DateTime value = d1.Value;
						DateTime value2 = d2.Value;
						result = new DateTime?((value >= value2) ? value : value2);
					}
				}
			}
			return result;
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x0004CEF0 File Offset: 0x0004B0F0
		public void SetDateLetterIssued(int PersonId, int LuCourseId, DateTime? Date)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@pid", DbType.Int32, PersonId),
				this.DatabaseManager.GetParameter("@lucid", DbType.Int32, LuCourseId),
				(Date != null) ? this.DatabaseManager.GetParameter("@dt", DbType.DateTime, Date.Value) : this.DatabaseManager.GetParameter("@dt", DbType.DateTime, DBNull.Value)
			};
			this.DatabaseManager.ExecuteNonQuery("UPDATE courses SET dateletterissued=@dt WHERE personid=@pid AND lucourseid=@lucid", parameters);
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x0004CF90 File Offset: 0x0004B190
		public void SetDateLetterReturned(int PersonId, int LuCourseId, DateTime? Date)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@pid", DbType.Int32, PersonId),
				this.DatabaseManager.GetParameter("@lucid", DbType.Int32, LuCourseId),
				(Date != null) ? this.DatabaseManager.GetParameter("@dt", DbType.DateTime, Date.Value) : this.DatabaseManager.GetParameter("@dt", DbType.DateTime, DBNull.Value)
			};
			this.DatabaseManager.ExecuteNonQuery("UPDATE courses SET dateletterreturned=@dt WHERE personid=@pid AND lucourseid=@lucid", parameters);
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x0004D030 File Offset: 0x0004B230
		public void SetProfLastViewedLetter(int PersonId, int LuCourseId, DateTime? Date)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@pid", DbType.Int32, PersonId),
				this.DatabaseManager.GetParameter("@lucid", DbType.Int32, LuCourseId),
				(Date != null) ? this.DatabaseManager.GetParameter("@dt", DbType.DateTime, Date.Value) : this.DatabaseManager.GetParameter("@dt", DbType.DateTime, DBNull.Value)
			};
			this.DatabaseManager.ExecuteNonQuery("UPDATE courses SET dateinstructorlastviewed=@dt WHERE personid=@pid AND lucourseid=@lucid", parameters);
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x0004D0D0 File Offset: 0x0004B2D0
		public void SetStudentLastViewedLetter(int PersonId, int LuCourseId, DateTime? Date)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@pid", DbType.Int32, PersonId),
				this.DatabaseManager.GetParameter("@lucid", DbType.Int32, LuCourseId),
				(Date != null) ? this.DatabaseManager.GetParameter("@dt", DbType.DateTime, Date.Value) : this.DatabaseManager.GetParameter("@dt", DbType.DateTime, DBNull.Value)
			};
			this.DatabaseManager.ExecuteNonQuery("UPDATE courses SET datestudentlastviewed=@dt WHERE personid=@pid AND lucourseid=@lucid", parameters);
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x0004D170 File Offset: 0x0004B370
		public void SetDateLetterIssued(int CoursesId, DateTime? Date)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@coursesid", DbType.Int32, CoursesId),
				(Date != null) ? this.DatabaseManager.GetParameter("@dt", DbType.DateTime, Date.Value) : this.DatabaseManager.GetParameter("@dt", DbType.DateTime, DBNull.Value)
			};
			this.DatabaseManager.ExecuteNonQuery("UPDATE courses SET dateletterissued=@dt WHERE coursesid=@coursesid", parameters);
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x0004D1F4 File Offset: 0x0004B3F4
		public void SetDateLetterReturned(int CoursesId, DateTime? Date)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@coursesid", DbType.Int32, CoursesId),
				(Date != null) ? this.DatabaseManager.GetParameter("@dt", DbType.DateTime, Date.Value) : this.DatabaseManager.GetParameter("@dt", DbType.DateTime, DBNull.Value)
			};
			this.DatabaseManager.ExecuteNonQuery("UPDATE courses SET dateletterreturned=@dt WHERE coursesid=@coursesid", parameters);
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x0004D278 File Offset: 0x0004B478
		public void SetProfLastViewedLetter(int CoursesId, DateTime? Date)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@coursesid", DbType.Int32, CoursesId),
				(Date != null) ? this.DatabaseManager.GetParameter("@dt", DbType.DateTime, Date.Value) : this.DatabaseManager.GetParameter("@dt", DbType.DateTime, DBNull.Value)
			};
			this.DatabaseManager.ExecuteNonQuery("UPDATE courses SET dateinstructorlastviewed=@dt WHERE coursesid=@coursesid", parameters);
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x0004D2FC File Offset: 0x0004B4FC
		public void SetStudentLastViewedLetter(int CoursesId, DateTime? Date)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@coursesid", DbType.Int32, CoursesId),
				(Date != null) ? this.DatabaseManager.GetParameter("@dt", DbType.DateTime, Date.Value) : this.DatabaseManager.GetParameter("@dt", DbType.DateTime, DBNull.Value)
			};
			this.DatabaseManager.ExecuteNonQuery("UPDATE courses SET datestudentlastviewed=@dt WHERE coursesid=@coursesid", parameters);
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x0004D380 File Offset: 0x0004B580
		public IList<CourseRegistration> LoadAllStudentsWithCoursesByDate(DateTime StartDate, DateTime EndDate, bool IncludeDroppedCourses)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@startdate", DbType.DateTime, StartDate),
				this.DatabaseManager.GetParameter("@enddate", DbType.DateTime, EndDate),
				this.DatabaseManager.GetParameter("@includedropped", DbType.Boolean, IncludeDroppedCourses)
			};
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT    c.coursesid,c.personid,c.lucourseid,c.registrationstatus,c.dateadded,\r\nc.whoadded AS whoaddedpersonid,pc.firstname AS whoaddedfirstname,pc.lastname AS whoaddedlastname,pc.student_no AS whoaddedstudent_no,\r\nc.dateletterissued,c.dateletterreturned,c.needsnotes,c.extracode,c.coursenote,c.notetakerrequired,\r\nc.datestudentlastviewed,c.dateinstructorlastviewed,c.wholastviewed,c.instructorconfirmed,c.exemptfromdatasync,\r\nluc.lucourseid,luc.startdate,luc.enddate,luc.duration,luc.term,luc.subjectid,luc.externalid,luc.exemptfromdatasync AS lucexemptfromdatasync\r\n        ,lucd.lookupstring AS subjectcode,lucd.altlookupstring AS subjectdescription\r\n        ,luc.course,luc.timeofday,luc.[section]\r\n        ,luc.campus,luc.department,luc.location\r\n        ,luc.instructorid AS pinstructorid,lucd2.altlookupstring AS pinstructorname,lucd2.email AS pinstructoremail,lucd2.phone AS pinstructorphone,lucd2.username AS pinstructorusername,lucd2.exemptfromdatasync AS pexemptfromdatasync,lucd2.id AS pinstructoremployeeid,lucd2.externalid AS pinstructorexternalid\r\n        ,luc.ExemptAssignmentFromDataSync AS pExemptAssignmentFromDataSync\r\n        ,luc.BatchDataSyncLogId,luc.credits\r\n        ,lci.instructorid AS p3instructorid,lucd3.altlookupstring AS p3instructorname,lucd3.email AS p3instructoremail,lucd3.phone AS p3instructorphone,lucd3.username AS p3instructorusername,lucd3.exemptfromdatasync AS p3exemptfromdatasync,lucd3.id AS p3instructoremployeeid,lucd3.externalid AS p3instructorexternalid\r\n        ,lci.ExemptAssignmentFromDataSync AS p3ExemptAssignmentFromDataSync\r\n        ,tt.timetableid\r\n        ,tt.sunstartminutes,tt.sunendminutes,tt.monstartminutes,tt.monendminutes,tt.tuestartminutes,tt.tueendminutes\r\n        ,tt.wedstartminutes,tt.wedendminutes,tt.thustartminutes,tt.thuendminutes,tt.fristartminutes,tt.friendminutes\r\n        ,tt.satstartminutes,tt.satendminutes,tt.sunroom,tt.monroom,tt.tueroom,tt.wedroom,tt.thuroom,tt.friroom,tt.satroom,\r\n        luc.alternatecontactid,ac.altname,ac.altemail,ac.altphone,ac.altusername,ac.externalid,ac.altpermissionlevel,\r\n        lucac.alternatecontactid AS secondaryalternatecontactid,\r\n        ac2.altname AS secondaryaltname,ac2.altemail AS secondaryaltemail,ac2.altphone AS secondaryaltphone,\r\n        ac2.altusername AS secondaryaltusername,ac2.externalid AS secondaryexternalid,\r\n        ac2.altpermissionlevel AS secondaryaltpermissionlevel,\r\n        p.firstname,p.lastname,p.student_no,\r\n        scar.StudentCourseAccommodationRequestId,scar.[status] AS rstatus,scar.daterequested AS rdateapproved,\r\n\t\tscar.dateentered AS rdateentered,scar.note1 AS rnote1,scar.note2 AS rnote2,\r\n        c.tuitioncost,c.GradeNumber,c.GradeLetter,c.InProgressGradeNumber,c.InProgressGradeLetter,\r\n        c.RegistrationDate,c.RegistrationNote\r\nFROM    courses c LEFT JOIN lucourses luc ON luc.lucourseid=c.lucourseid\r\n        LEFT JOIN people p ON p.personid=c.personid\r\n        LEFT JOIN people pc ON pc.personid=c.whoadded\r\n        LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid\r\n        LEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luc.instructorid\r\n        LEFT JOIN lucourseinstructor lci ON lci.lucourseid=luc.lucourseid\r\n        LEFT JOIN lucoursedata lucd3 ON lucd3.lucoursedataid=lci.instructorid\r\n        LEFT JOIN timetable tt ON tt.timetabletype='C' AND tt.lucourseid=luc.lucourseid\r\n        LEFT JOIN lucoursealternatecontact ac ON ac.alternatecontactid=luc.alternatecontactid\r\n        LEFT JOIN LuCourseAltContact lucac ON lucac.lucourseid=luc.lucourseid\r\n        LEFT JOIN lucoursealternatecontact ac2 ON ac2.alternatecontactid=lucac.alternatecontactid\r\n        LEFT JOIN StudentCourseAccommodationRequest scar ON scar.PersonId=c.PersonId AND scar.lucourseid=c.lucourseid\r\nWHERE  NOT ( luc.enddate <= @startdate OR luc.startdate > @enddate)\r\n         AND (@includedropped=1 OR (c.registrationstatus IS NULL OR NOT c.registrationstatus=2))\r\nORDER BY c.personid,c.lucourseid", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					List<CourseRegistration> courseRegistrationsFromReader = CourseRegistrationDAO.GetCourseRegistrationsFromReader0<CourseRegistration>("", dataReader, this.OpContext);
					courseRegistrationsFromReader.Sort((CourseRegistration c1, CourseRegistration c2) => c1.Student.LastName.CompareTo(c2.Student.LastName));
					return courseRegistrationsFromReader;
				}
			}
			return null;
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x0004D464 File Offset: 0x0004B664
		public IList<CourseRegistration> LoadCourseRegistrationsByCourse(int LuCourseId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@lucid", DbType.Int32, LuCourseId)
			};
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT    c.coursesid,c.personid,c.lucourseid,c.registrationstatus,c.dateadded,\r\nc.whoadded AS whoaddedpersonid,pc.firstname AS whoaddedfirstname,pc.lastname AS whoaddedlastname,pc.student_no AS whoaddedstudent_no,\r\nc.dateletterissued,c.dateletterreturned,c.needsnotes,c.extracode,c.coursenote,c.notetakerrequired,\r\nc.datestudentlastviewed,c.dateinstructorlastviewed,c.wholastviewed,c.instructorconfirmed,c.exemptfromdatasync,\r\nluc.lucourseid,luc.startdate,luc.enddate,luc.duration,luc.term,luc.subjectid,luc.externalid,luc.exemptfromdatasync AS lucexemptfromdatasync\r\n        ,lucd.lookupstring AS subjectcode,lucd.altlookupstring AS subjectdescription\r\n        ,luc.course,luc.timeofday,luc.[section]\r\n        ,luc.campus,luc.department,luc.location\r\n        ,luc.instructorid AS pinstructorid,lucd2.altlookupstring AS pinstructorname,lucd2.email AS pinstructoremail,lucd2.phone AS pinstructorphone,lucd2.username AS pinstructorusername,lucd2.exemptfromdatasync AS pexemptfromdatasync,lucd2.id AS pinstructoremployeeid,lucd2.externalid AS pinstructorexternalid\r\n        ,luc.ExemptAssignmentFromDataSync AS pExemptAssignmentFromDataSync\r\n        ,luc.BatchDataSyncLogId,luc.credits\r\n        ,lci.instructorid AS p3instructorid,lucd3.altlookupstring AS p3instructorname,lucd3.email AS p3instructoremail,lucd3.phone AS p3instructorphone,lucd3.username AS p3instructorusername,lucd3.exemptfromdatasync AS p3exemptfromdatasync,lucd3.id AS p3instructoremployeeid,lucd3.externalid AS p3instructorexternalid\r\n        ,lci.ExemptAssignmentFromDataSync AS p3ExemptAssignmentFromDataSync\r\n        ,tt.timetableid\r\n        ,tt.sunstartminutes,tt.sunendminutes,tt.monstartminutes,tt.monendminutes,tt.tuestartminutes,tt.tueendminutes\r\n        ,tt.wedstartminutes,tt.wedendminutes,tt.thustartminutes,tt.thuendminutes,tt.fristartminutes,tt.friendminutes\r\n        ,tt.satstartminutes,tt.satendminutes,tt.sunroom,tt.monroom,tt.tueroom,tt.wedroom,tt.thuroom,tt.friroom,tt.satroom,\r\n        luc.alternatecontactid,ac.altname,ac.altemail,ac.altphone,ac.altusername,ac.externalid,ac.altpermissionlevel,\r\n        lucac.alternatecontactid AS secondaryalternatecontactid,\r\n        ac2.altname AS secondaryaltname,ac2.altemail AS secondaryaltemail,ac2.altphone AS secondaryaltphone,\r\n        ac2.altusername AS secondaryaltusername,ac2.externalid AS secondaryexternalid,\r\n        ac2.altpermissionlevel AS secondaryaltpermissionlevel,\r\n        p.firstname,p.lastname,p.student_no,\r\n        scar.StudentCourseAccommodationRequestId,scar.[status] AS rstatus,scar.daterequested AS rdateapproved,\r\n\t\tscar.dateentered AS rdateentered,scar.note1 AS rnote1,scar.note2 AS rnote2,\r\n        c.tuitioncost,c.GradeNumber,c.GradeLetter,c.InProgressGradeNumber,c.InProgressGradeLetter,\r\n        c.RegistrationDate,c.RegistrationNote\r\nFROM    courses c LEFT JOIN lucourses luc ON luc.lucourseid=c.lucourseid\r\n        LEFT JOIN people p ON p.personid=c.personid\r\n        LEFT JOIN people pc ON pc.personid=c.whoadded\r\n        LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid\r\n        LEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luc.instructorid\r\n        LEFT JOIN lucourseinstructor lci ON lci.lucourseid=luc.lucourseid\r\n        LEFT JOIN lucoursedata lucd3 ON lucd3.lucoursedataid=lci.instructorid\r\n        LEFT JOIN timetable tt ON tt.timetabletype='C' AND tt.lucourseid=luc.lucourseid\r\n        LEFT JOIN lucoursealternatecontact ac ON ac.alternatecontactid=luc.alternatecontactid\r\n        LEFT JOIN LuCourseAltContact lucac ON lucac.lucourseid=luc.lucourseid\r\n        LEFT JOIN lucoursealternatecontact ac2 ON ac2.alternatecontactid=lucac.alternatecontactid\r\n        LEFT JOIN StudentCourseAccommodationRequest scar ON scar.PersonId=c.PersonId AND scar.lucourseid=c.lucourseid\r\nWHERE   c.lucourseid=@lucid", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					List<CourseRegistration> courseRegistrationsFromReader = CourseRegistrationDAO.GetCourseRegistrationsFromReader("", dataReader, this.OpContext);
					courseRegistrationsFromReader.Sort((CourseRegistration c1, CourseRegistration c2) => c1.Student.LastName.CompareTo(c2.Student.LastName));
					return courseRegistrationsFromReader;
				}
			}
			return null;
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x0004D514 File Offset: 0x0004B714
		public CourseRegistration LoadCourseRegistrationsByStudentAndCourse(int StudentPid, int Lucid)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@pid", DbType.Int32, StudentPid),
				this.DatabaseManager.GetParameter("@lucid", DbType.Int32, Lucid)
			};
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT    c.coursesid,c.personid,c.lucourseid,c.registrationstatus,c.dateadded,\r\nc.whoadded AS whoaddedpersonid,pc.firstname AS whoaddedfirstname,pc.lastname AS whoaddedlastname,pc.student_no AS whoaddedstudent_no,\r\nc.dateletterissued,c.dateletterreturned,c.needsnotes,c.extracode,c.coursenote,c.notetakerrequired,\r\nc.datestudentlastviewed,c.dateinstructorlastviewed,c.wholastviewed,c.instructorconfirmed,c.exemptfromdatasync,\r\nluc.lucourseid,luc.startdate,luc.enddate,luc.duration,luc.term,luc.subjectid,luc.externalid,luc.exemptfromdatasync AS lucexemptfromdatasync\r\n        ,lucd.lookupstring AS subjectcode,lucd.altlookupstring AS subjectdescription\r\n        ,luc.course,luc.timeofday,luc.[section]\r\n        ,luc.campus,luc.department,luc.location\r\n        ,luc.instructorid AS pinstructorid,lucd2.altlookupstring AS pinstructorname,lucd2.email AS pinstructoremail,lucd2.phone AS pinstructorphone,lucd2.username AS pinstructorusername,lucd2.exemptfromdatasync AS pexemptfromdatasync,lucd2.id AS pinstructoremployeeid,lucd2.externalid AS pinstructorexternalid\r\n        ,luc.ExemptAssignmentFromDataSync AS pExemptAssignmentFromDataSync\r\n        ,luc.BatchDataSyncLogId,luc.credits\r\n        ,lci.instructorid AS p3instructorid,lucd3.altlookupstring AS p3instructorname,lucd3.email AS p3instructoremail,lucd3.phone AS p3instructorphone,lucd3.username AS p3instructorusername,lucd3.exemptfromdatasync AS p3exemptfromdatasync,lucd3.id AS p3instructoremployeeid,lucd3.externalid AS p3instructorexternalid\r\n        ,lci.ExemptAssignmentFromDataSync AS p3ExemptAssignmentFromDataSync\r\n        ,tt.timetableid\r\n        ,tt.sunstartminutes,tt.sunendminutes,tt.monstartminutes,tt.monendminutes,tt.tuestartminutes,tt.tueendminutes\r\n        ,tt.wedstartminutes,tt.wedendminutes,tt.thustartminutes,tt.thuendminutes,tt.fristartminutes,tt.friendminutes\r\n        ,tt.satstartminutes,tt.satendminutes,tt.sunroom,tt.monroom,tt.tueroom,tt.wedroom,tt.thuroom,tt.friroom,tt.satroom,\r\n        luc.alternatecontactid,ac.altname,ac.altemail,ac.altphone,ac.altusername,ac.externalid,ac.altpermissionlevel,\r\n        lucac.alternatecontactid AS secondaryalternatecontactid,\r\n        ac2.altname AS secondaryaltname,ac2.altemail AS secondaryaltemail,ac2.altphone AS secondaryaltphone,\r\n        ac2.altusername AS secondaryaltusername,ac2.externalid AS secondaryexternalid,\r\n        ac2.altpermissionlevel AS secondaryaltpermissionlevel,\r\n        p.firstname,p.lastname,p.student_no,\r\n        scar.StudentCourseAccommodationRequestId,scar.[status] AS rstatus,scar.daterequested AS rdateapproved,\r\n\t\tscar.dateentered AS rdateentered,scar.note1 AS rnote1,scar.note2 AS rnote2,\r\n        c.tuitioncost,c.GradeNumber,c.GradeLetter,c.InProgressGradeNumber,c.InProgressGradeLetter,\r\n        c.RegistrationDate,c.RegistrationNote\r\nFROM    courses c LEFT JOIN lucourses luc ON luc.lucourseid=c.lucourseid\r\n        LEFT JOIN people p ON p.personid=c.personid\r\n        LEFT JOIN people pc ON pc.personid=c.whoadded\r\n        LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid\r\n        LEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luc.instructorid\r\n        LEFT JOIN lucourseinstructor lci ON lci.lucourseid=luc.lucourseid\r\n        LEFT JOIN lucoursedata lucd3 ON lucd3.lucoursedataid=lci.instructorid\r\n        LEFT JOIN timetable tt ON tt.timetabletype='C' AND tt.lucourseid=luc.lucourseid\r\n        LEFT JOIN lucoursealternatecontact ac ON ac.alternatecontactid=luc.alternatecontactid\r\n        LEFT JOIN LuCourseAltContact lucac ON lucac.lucourseid=luc.lucourseid\r\n        LEFT JOIN lucoursealternatecontact ac2 ON ac2.alternatecontactid=lucac.alternatecontactid\r\n        LEFT JOIN StudentCourseAccommodationRequest scar ON scar.PersonId=c.PersonId AND scar.lucourseid=c.lucourseid\r\nWHERE   c.personid=@pid AND c.lucourseid=@lucid", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					List<CourseRegistration> courseRegistrationsFromReader = CourseRegistrationDAO.GetCourseRegistrationsFromReader("", dataReader, this.OpContext);
					bool flag2 = courseRegistrationsFromReader == null || courseRegistrationsFromReader.Count < 1;
					if (flag2)
					{
						return null;
					}
					return courseRegistrationsFromReader[0];
				}
			}
			return null;
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x0004D5D8 File Offset: 0x0004B7D8
		public IList<PersonBase> LoadStudentsWithActiveRegisteredCoursesAndActiveAccommodations(DateTime StartDate, DateTime EndDate, int AccommodationsExpiryDateControlId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@startdate", DbType.DateTime, StartDate),
				this.DatabaseManager.GetParameter("@enddate", DbType.DateTime, EndDate),
				this.DatabaseManager.GetParameter("@cid", DbType.Int32, AccommodationsExpiryDateControlId)
			};
			IList<PersonBase> result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT    p.personid,p.firstname,p.middlename,p.lastname,p.student_no\r\nFROM        people p \r\nWHERE       p.isactive=1 --AND p.personid IN (SELECT personid FROM peoplegroups WHERE groupid=1)\r\n            AND p.personid IN (SELECT c.personid FROM courses c LEFT JOIN lucourses luc ON luc.lucourseid=c.lucourseid WHERE (c.registrationstatus IS NULL OR NOT c.registrationstatus=2) AND NOT ( ( luc.enddate<@startdate ) OR (luc.startdate > @enddate ) ) )\r\n            AND (@cid<1 OR p.personid IN (SELECT d.personid FROM datetimeinfoaccommodationps d WHERE d.controlid=@cid AND d.controlvalue >= @startdate ))", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					IBatchDecryptor batchDecryptor = this.DatabaseManager.Encryption.GetBatchDecryptor();
					List<PersonBase> list = new List<PersonBase>();
					while (dataReader.Read())
					{
						PersonBase personFromReader = PeopleDAO.GetPersonFromReader("", dataReader, this.OpContext, batchDecryptor);
						bool flag2 = personFromReader != null;
						if (flag2)
						{
							list.Add(personFromReader);
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x00003998 File Offset: 0x00001B98
		public IList<CourseRegistration> LoadStudentsCoursesBatch(DateTime StartDate, DateTime EndDate, IList<int> PersonIds, bool IncludeDroppedCourses)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x0004D6D4 File Offset: 0x0004B8D4
		public IList<CourseRegistration> LoadActiveStudentsWithCourses(DateTime StartDate, DateTime EndDate, bool IncludeDroppedCourses = false)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@startdate", DbType.DateTime, StartDate.Date),
				databaseLayer.GetParameter("@enddate", DbType.DateTime, EndDate.Date.AddDays(1.0)),
				databaseLayer.GetParameter("@includedropped", DbType.Boolean, IncludeDroppedCourses)
			};
			IList<CourseRegistration> result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("CREATE TABLE #tpids (personid INT);\r\n\r\nINSERT INTO #tpids\r\n\tEXEC ActiveStudentPids @startdate,@enddate;\r\n\r\nSELECT  t.personid,c.coursesid,c.personid,c.lucourseid,c.registrationstatus,c.dateadded,\r\n        c.whoadded AS whoaddedpersonid,pc.firstname AS whoaddedfirstname,pc.lastname AS whoaddedlastname,pc.student_no AS whoaddedstudent_no,\r\n        c.dateletterissued,c.dateletterreturned,c.needsnotes,c.extracode,c.coursenote,c.notetakerrequired,\r\n        c.datestudentlastviewed,c.dateinstructorlastviewed,c.wholastviewed,c.instructorconfirmed,c.exemptfromdatasync,\r\n        luc.lucourseid,luc.startdate,luc.enddate,luc.duration,luc.term,luc.subjectid,luc.externalid,luc.exemptfromdatasync AS lucexemptfromdatasync\r\n        ,lucd.lookupstring AS subjectcode,lucd.altlookupstring AS subjectdescription\r\n        ,luc.course,luc.timeofday,luc.[section]\r\n        ,luc.campus,luc.department,luc.location\r\n        ,luc.instructorid AS pinstructorid,lucd2.altlookupstring AS pinstructorname,lucd2.email AS pinstructoremail,lucd2.phone AS pinstructorphone,lucd2.username AS pinstructorusername,lucd2.exemptfromdatasync AS pexemptfromdatasync,lucd2.id AS pinstructoremployeeid,lucd2.externalid AS pinstructorexternalid\r\n        ,luc.ExemptAssignmentFromDataSync AS pExemptAssignmentFromDataSync\r\n        ,luc.BatchDataSyncLogId\r\n        ,lci.instructorid AS p3instructorid,lucd3.altlookupstring AS p3instructorname,lucd3.email AS p3instructoremail,lucd3.phone AS p3instructorphone,lucd3.username AS p3instructorusername,lucd3.exemptfromdatasync AS p3exemptfromdatasync,lucd3.id AS p3instructoremployeeid,lucd3.externalid AS p3instructorexternalid\r\n        ,lci.ExemptAssignmentFromDataSync AS p3ExemptAssignmentFromDataSync\r\n        ,tt.timetableid\r\n        ,tt.sunstartminutes,tt.sunendminutes,tt.monstartminutes,tt.monendminutes,tt.tuestartminutes,tt.tueendminutes\r\n        ,tt.wedstartminutes,tt.wedendminutes,tt.thustartminutes,tt.thuendminutes,tt.fristartminutes,tt.friendminutes\r\n        ,tt.satstartminutes,tt.satendminutes,tt.sunroom,tt.monroom,tt.tueroom,tt.wedroom,tt.thuroom,tt.friroom,tt.satroom,\r\n        luc.alternatecontactid,ac.altname,ac.altemail,ac.altphone,ac.altusername,ac.externalid,ac.altpermissionlevel,\r\n        lucac.alternatecontactid AS secondaryalternatecontactid,\r\n        ac2.altname AS secondaryaltname,ac2.altemail AS secondaryaltemail,ac2.altphone AS secondaryaltphone,\r\n        ac2.altusername AS secondaryaltusername,ac2.externalid AS secondaryexternalid,\r\n        ac2.altpermissionlevel AS secondaryaltpermissionlevel,\r\n        p.firstname,p.lastname,p.student_no,\r\n        scar.StudentCourseAccommodationRequestId,scar.[status] AS rstatus,scar.daterequested AS rdateapproved,\r\n\t\tscar.dateentered AS rdateentered,scar.note1 AS rnote1,scar.note2 AS rnote2\r\nFROM    #tpids t LEFT JOIN courses c ON c.personid=t.personid\r\n        LEFT JOIN lucourses luc ON luc.lucourseid=c.lucourseid\r\n        LEFT JOIN people p ON p.personid=t.personid\r\n        LEFT JOIN people pc ON pc.personid=c.whoadded\r\n        LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid\r\n        LEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luc.instructorid\r\n        LEFT JOIN lucourseinstructor lci ON lci.lucourseid=luc.lucourseid\r\n        LEFT JOIN lucoursedata lucd3 ON lucd3.lucoursedataid=lci.instructorid\r\n        LEFT JOIN timetable tt ON tt.timetabletype='C' AND tt.lucourseid=luc.lucourseid\r\n        LEFT JOIN lucoursealternatecontact ac ON ac.alternatecontactid=luc.alternatecontactid\r\n        LEFT JOIN LuCourseAltContact lucac ON lucac.lucourseid=luc.lucourseid\r\n        LEFT JOIN lucoursealternatecontact ac2 ON ac2.alternatecontactid=lucac.alternatecontactid\r\n        LEFT JOIN StudentCourseAccommodationRequest scar ON scar.PersonId=c.PersonId AND scar.lucourseid=c.lucourseid\r\nWHERE   (@includedropped=1 OR (c.registrationstatus IS NULL OR NOT c.registrationstatus=2))\r\n        AND NOT ( luc.enddate <= @startdate OR luc.startdate > @enddate)\r\nORDER BY t.personid,luc.startdate,c.lucourseid;\r\n\r\nDROP TABLE #tpids", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<CourseRegistration> list = new List<CourseRegistration>();
					CourseRegistration courseRegistration = null;
					while (dataReader.Read())
					{
						int num = (int)dataReader["lucourseid"];
						int num2 = (int)dataReader["personid"];
						bool flag2 = courseRegistration == null || courseRegistration.Course.LuCourseId != num || courseRegistration.Student.PersonId != num2;
						if (flag2)
						{
							LookupCourse course = new LookupCourse();
							LookupCourseDAO.GetMainCourseFromReader(course, "", dataReader);
							courseRegistration = CourseRegistrationDAO.GetCourseRegistrationFromRecord0<CourseRegistration>(course, dataReader, this.OpContext);
							list.Add(courseRegistration);
						}
						LookupCourseDAO.AddCourseInfoFromReader(courseRegistration.Course, "", dataReader);
						bool flag3 = PeopleDAO.ReaderContainsColumn(dataReader, "pExemptAssignmentFromDataSync") && dataReader["pinstructorid"] != DBNull.Value && dataReader["pExemptAssignmentFromDataSync"] != DBNull.Value && Convert.ToBoolean(dataReader["pExemptAssignmentFromDataSync"]);
						if (flag3)
						{
							courseRegistration.ExemptedInstructorAssignments.Add((int)dataReader["pinstructorid"]);
						}
						bool flag4 = PeopleDAO.ReaderContainsColumn(dataReader, "p3ExemptAssignmentFromDataSync") && dataReader["p3instructorid"] != DBNull.Value && dataReader["p3ExemptAssignmentFromDataSync"] != DBNull.Value && Convert.ToBoolean(dataReader["p3ExemptAssignmentFromDataSync"]);
						if (flag4)
						{
							courseRegistration.ExemptedInstructorAssignments.Add((int)dataReader["p3instructorid"]);
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x0004D938 File Offset: 0x0004BB38
		public void UpdateCourseRegistrationSpecificInfoNonEmptyFieldsOnly(int CoursesId, DataSyncExternalCourseStudentSpecific courseStudentSpecificInfo)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] array = new DbParameter[8];
			array[0] = databaseLayer.GetParameter("@coursesid", DbType.Int32, CoursesId);
			array[1] = databaseLayer.GetParameter("@tuitioncost", DbType.Decimal, courseStudentSpecificInfo.TuitionCost.ToString());
			array[2] = databaseLayer.GetParameter("@gradenumber", DbType.Decimal, courseStudentSpecificInfo.Grade);
			array[3] = databaseLayer.GetParameter("@inprogressgradenumber", DbType.Decimal, courseStudentSpecificInfo.InProgressGrade);
			array[4] = databaseLayer.GetParameter("@gradeletter", DbType.String, this.ConvertStringForDatabaseUseNullForEmpty(courseStudentSpecificInfo.GradeLetter, 255));
			array[5] = databaseLayer.GetParameter("@inprogressgradeletter", DbType.String, this.ConvertStringForDatabaseUseNullForEmpty(courseStudentSpecificInfo.InProgressGradeLetter, 255));
			int num = 6;
			DatabaseLayer databaseLayer2 = databaseLayer;
			string pName = "@registrationdate";
			DbType pType = DbType.DateTime;
			DateTime? registrationDate = courseStudentSpecificInfo.RegistrationDate;
			array[num] = databaseLayer2.GetParameter(pName, pType, (registrationDate != null) ? registrationDate.GetValueOrDefault() : DBNull.Value);
			array[7] = databaseLayer.GetParameter("@registrationnote", DbType.String, this.ConvertStringForDatabaseUseNullForEmpty(courseStudentSpecificInfo.RegistrationNote, 0));
			DbParameter[] parameters = array;
			databaseLayer.ExecuteNonQuery("UPDATE courses SET \r\ntuitioncost=COALESCE(@tuitioncost,tuitioncost),\r\nGradeNumber=COALESCE(@gradenumber,GradeNumber),\r\nGradeLetter=COALESCE(@gradeletter,GradeLetter),\r\nInProgressGradeNumber=COALESCE(@inprogressgradenumber,InProgressGradeNumber),\r\nInProgressGradeLetter=COALESCE(@inprogressgradeletter,InProgressGradeLetter),\r\nRegistrationDate=COALESCE(@registrationdate,RegistrationDate),\r\nRegistrationNote=COALESCE(@registrationnote,RegistrationNote)\r\nWHERE coursesid=@coursesid", parameters);
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x0004DA6C File Offset: 0x0004BC6C
		private object ConvertStringForDatabaseUseNullForEmpty(string s, int maxCharCount)
		{
			bool flag = s == null;
			object result;
			if (flag)
			{
				result = DBNull.Value;
			}
			else
			{
				string text = s.Trim();
				bool flag2 = text.Length < 1;
				if (flag2)
				{
					result = DBNull.Value;
				}
				else
				{
					bool flag3 = maxCharCount < 1;
					if (flag3)
					{
						result = text;
					}
					else
					{
						result = ((text.Length <= maxCharCount) ? text : text.Substring(0, maxCharCount));
					}
				}
			}
			return result;
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x0004DACC File Offset: 0x0004BCCC
		public IList<StudentWithCourseAndAccommodationInfo> LoadStudentsWithCourseAndAccommodationInfosByCourseIds(int accommExpiryCid, int noInstructorViewCid, params int[] lucids)
		{
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, this.OpContext.TenantId);
			DbParameter[] array = new DbParameter[3];
			array[0] = databaseLayer.GetParameter("@expirycid", DbType.Int32, accommExpiryCid);
			array[1] = databaseLayer.GetParameter("@nocid", DbType.Int32, noInstructorViewCid);
			array[2] = databaseLayer.GetParameter("@lucids", DbType.String, string.Join(",", (from g in lucids ?? new int[0]
			select g.ToString()).ToArray<string>()));
			DbParameter[] parameters = array;
			IList<StudentWithCourseAndAccommodationInfo> result;
			using (IDataReader dataReader = databaseLayer.ExecuteStoredProcedureReader("sp_CourseReg_GetStudentsWithAccommInfosForCourses", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<StudentWithCourseAndAccommodationInfo> list = new List<StudentWithCourseAndAccommodationInfo>();
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						StudentWithCourseAndAccommodationInfo studentWithCourseAndAccommodationInfoFromRecord = CourseRegistrationDAO.GetStudentWithCourseAndAccommodationInfoFromRecord(batchDecryptor, dataReader);
						bool flag2 = studentWithCourseAndAccommodationInfoFromRecord == null;
						if (!flag2)
						{
							list.Add(studentWithCourseAndAccommodationInfoFromRecord);
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x0004DBF0 File Offset: 0x0004BDF0
		private static StudentWithCourseAndAccommodationInfo GetStudentWithCourseAndAccommodationInfoFromRecord(IBatchDecryptor batchDecryptor, IDataReader record)
		{
			bool flag = record == null;
			StudentWithCourseAndAccommodationInfo result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = new StudentWithCourseAndAccommodationInfo
				{
					Student = PeopleDAO.GetBasicPersonFromRecord("", record, batchDecryptor),
					CourseBase = LookupCourseDAO.GetCourseBaseFromReader<LookupCourseBase>("", record),
					AccommodationExpiryDate = ((record["AccommodationExpiry"] is DBNull) ? null : new DateTime?((DateTime)record["AccommodationExpiry"])),
					DateLetterIssued = ((record["DateLetterIssued"] is DBNull) ? null : new DateTime?((DateTime)record["DateLetterIssued"])),
					DateLetterReturned = ((record["DateLetterReturned"] is DBNull) ? null : new DateTime?((DateTime)record["DateLetterReturned"])),
					NoInstructorViewEnabled = (!(record["NotAllowed"] is DBNull) && (int)record["NotAllowed"] != 0),
					SelfRegIsApproved = (!(record["SelfRegApprovedStatus"] is DBNull) && (int)record["SelfRegApprovedStatus"] == 8)
				};
			}
			return result;
		}

		// Token: 0x0400044E RID: 1102
		private DatabaseLayer DatabaseManager;

		// Token: 0x04000450 RID: 1104
		private PeopleDAO pd;

		// Token: 0x04000451 RID: 1105
		private LookupCourseDAO ld = null;
	}
}
