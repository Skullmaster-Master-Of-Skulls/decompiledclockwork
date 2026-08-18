using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using Databases;
using TechnoPro.Common.DAO.Impl.CourseRegistrations;
using TechnoPro.Common.DAO.LookupCourses;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.CourseRegistrations;
using TechnoPro.Common.Public.Entities.DataSync;
using TechnoPro.Common.Public.Entities.LookupCourses;

namespace TechnoPro.Common.DAO.Impl.LookupCourses
{
	// Token: 0x0200009B RID: 155
	public class LookupCourseDAO : ILookupCourseDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060003FA RID: 1018 RVA: 0x00022D14 File Offset: 0x00020F14
		// (set) Token: 0x060003FB RID: 1019 RVA: 0x00022D1C File Offset: 0x00020F1C
		public DatabaseLayer DatabaseManager { get; private set; }

		// Token: 0x060003FC RID: 1020 RVA: 0x00022D25 File Offset: 0x00020F25
		public LookupCourseDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext2 = this.OpContext;
			this.DatabaseManager = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext2 != null) ? opContext2.TenantId : null);
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060003FD RID: 1021 RVA: 0x00022D56 File Offset: 0x00020F56
		// (set) Token: 0x060003FE RID: 1022 RVA: 0x00022D5E File Offset: 0x00020F5E
		public OperationContext OpContext { get; set; }

		// Token: 0x060003FF RID: 1023 RVA: 0x00022D68 File Offset: 0x00020F68
		private static bool ReaderContainsColumn(IDataReader reader, string colName)
		{
			for (int i = 0; i < reader.FieldCount; i++)
			{
				bool flag = reader.GetName(i).Equals(colName, StringComparison.OrdinalIgnoreCase);
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x00022DA8 File Offset: 0x00020FA8
		internal static void GetMainCourseFromReader(LookupCourse course, string colNamePrefix, IDataReader record)
		{
			bool flag = record[colNamePrefix + "lucourseid"] == DBNull.Value;
			if (!flag)
			{
				course.LuCourseId = (int)record[colNamePrefix + "lucourseid"];
				course.StartDate = (DateTime)record[colNamePrefix + "startdate"];
				course.EndDate = (DateTime)record[colNamePrefix + "enddate"];
				course.Term = record[colNamePrefix + "term"].ToString();
				course.Duration = record[colNamePrefix + "duration"].ToString();
				course.Course = record[colNamePrefix + "course"].ToString();
				course.Section = record[colNamePrefix + "section"].ToString();
				course.TimeOfDay = record[colNamePrefix + "timeofday"].ToString();
				course.Campus = record[colNamePrefix + "campus"].ToString();
				course.Department = record[colNamePrefix + "department"].ToString();
				course.Location = record[colNamePrefix + "location"].ToString();
				course.CourseNote = (LookupCourseDAO.ReaderContainsColumn(record, colNamePrefix + "coursenote") ? record[colNamePrefix + "coursenote"].ToString() : "");
				course.ExternalCourseId = (LookupCourseDAO.ReaderContainsColumn(record, colNamePrefix + "externalid") ? record[colNamePrefix + "externalid"].ToString() : "");
				string text = colNamePrefix + "credits";
				bool flag2 = LookupCourseDAO.ReaderContainsColumn(record, text) && !(record[text] is DBNull);
				if (flag2)
				{
					course.Credits = (decimal)record[text];
				}
				bool flag3 = course.Instructors == null;
				if (flag3)
				{
					course.Instructors = new List<LookupInstructor>();
				}
				bool flag4 = course.AlternateContacts == null;
				if (flag4)
				{
					course.AlternateContacts = new List<AlternateContact>();
				}
				bool flag5 = course.TimetableItems == null;
				if (flag5)
				{
					course.TimetableItems = new List<LookupTimetableItem>();
				}
				bool flag6 = course.Subject == null;
				if (flag6)
				{
					course.Subject = LookupSubjectDAO.GetSubjectFromCourseRecord(colNamePrefix, record);
				}
				bool flag7 = record["pinstructorid"] != DBNull.Value;
				if (flag7)
				{
					LookupInstructor primaryInstructorFromCourseRecord = LookupInstructorDAO.GetPrimaryInstructorFromCourseRecord("", record);
					bool flag8 = primaryInstructorFromCourseRecord != null;
					if (flag8)
					{
						course.Instructors.Add(primaryInstructorFromCourseRecord);
					}
				}
				bool flag9 = LookupCourseDAO.ReaderContainsColumn(record, "alternatecontactid") && record["alternatecontactid"] != DBNull.Value;
				if (flag9)
				{
					AlternateContact alternateContactFromRecord = AlternateContactDAO.GetAlternateContactFromRecord("", record);
					bool flag10 = alternateContactFromRecord != null;
					if (flag10)
					{
						course.AlternateContacts.Add(alternateContactFromRecord);
					}
				}
				bool flag11 = LookupCourseDAO.ReaderContainsColumn(record, "lucexemptfromdatasync");
				if (flag11)
				{
					course.IsExemptFromDataSync = (record["lucexemptfromdatasync"] != DBNull.Value && Convert.ToBoolean(record["lucexemptfromdatasync"]));
				}
				bool flag12 = LookupCourseDAO.ReaderContainsColumn(record, "batchdatasynclogid");
				if (flag12)
				{
					course.BatchDataSyncLogId = ((record["batchdatasynclogid"] is DBNull) ? 0 : ((int)record["batchdatasynclogid"]));
				}
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000401 RID: 1025 RVA: 0x00023144 File Offset: 0x00021344
		private AlternateContactDAO alternateContactDao
		{
			get
			{
				bool flag = this.acdao == null;
				if (flag)
				{
					this.acdao = new AlternateContactDAO(this.OpContext);
				}
				return this.acdao;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000402 RID: 1026 RVA: 0x0002317C File Offset: 0x0002137C
		private LookupSubjectDAO lookupSubjectDao
		{
			get
			{
				bool flag = this.sdao == null;
				if (flag)
				{
					this.sdao = new LookupSubjectDAO(this.OpContext);
				}
				return this.sdao;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000403 RID: 1027 RVA: 0x000231B4 File Offset: 0x000213B4
		private LookupInstructorDAO lookupInstructorDAO
		{
			get
			{
				bool flag = this.lid == null;
				if (flag)
				{
					this.lid = new LookupInstructorDAO(this.OpContext);
				}
				return this.lid;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000404 RID: 1028 RVA: 0x000231EC File Offset: 0x000213EC
		private LookupTimetableItemDAO lookupTimetableItemDAO
		{
			get
			{
				bool flag = this.ltd == null;
				if (flag)
				{
					this.ltd = new LookupTimetableItemDAO(this.OpContext);
				}
				return this.ltd;
			}
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x00023224 File Offset: 0x00021424
		internal static void AddCourseInfoFromReader(LookupCourse course, string colNamePrefix, IDataReader record)
		{
			LookupInstructor prof = LookupInstructorDAO.GetSecondaryInstructorFromCourseRecord(colNamePrefix, record);
			bool flag = prof != null && course.Instructors.Find((LookupInstructor pr) => pr.InstructorId == prof.InstructorId) == null;
			if (flag)
			{
				course.Instructors.Add(prof);
			}
			List<LookupTimetableItem> timetableItemsFromCourseRecord = LookupTimetableItemDAO.GetTimetableItemsFromCourseRecord(colNamePrefix, record);
			bool flag2 = timetableItemsFromCourseRecord != null && timetableItemsFromCourseRecord.Count > 0;
			if (flag2)
			{
				using (List<LookupTimetableItem>.Enumerator enumerator = timetableItemsFromCourseRecord.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						LookupTimetableItem item = enumerator.Current;
						LookupTimetableItem lookupTimetableItem = course.TimetableItems.Find((LookupTimetableItem g) => g.DayOfWeek == item.DayOfWeek && g.StartTime.Equals(item.StartTime) && g.EndTime.Equals(item.EndTime));
						bool flag3 = lookupTimetableItem == null;
						if (flag3)
						{
							course.TimetableItems.Add(item);
						}
					}
				}
			}
			bool flag4 = LookupCourseDAO.ReaderContainsColumn(record, "secondaryalternatecontactid");
			if (flag4)
			{
				AlternateContact altContact = AlternateContactDAO.GetAlternateContactFromRecord("secondary", record);
				bool flag5 = altContact != null && course.AlternateContacts.Find((AlternateContact pr) => pr.AlternateContactId == altContact.AlternateContactId) == null;
				if (flag5)
				{
					course.AlternateContacts.Add(altContact);
				}
			}
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x00023394 File Offset: 0x00021594
		internal static LookupCourseBase GetCourseBaseFromReader(string colNamePrefix, IDataReader reader)
		{
			return LookupCourseDAO.GetCourseBaseFromReader<LookupCourseBase>(colNamePrefix, reader);
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x000233B0 File Offset: 0x000215B0
		internal static T GetCourseBaseFromReader<T>(string colNamePrefix, IDataReader reader) where T : LookupCourseBase
		{
			string name = colNamePrefix + "lucourseid";
			bool flag = reader[name] == DBNull.Value || (int)reader[name] < 1;
			T result;
			if (flag)
			{
				result = default(T);
			}
			else
			{
				string text = colNamePrefix + "campus";
				string text2 = colNamePrefix + "department";
				string text3 = colNamePrefix + "location";
				string text4 = colNamePrefix + "coursenote";
				T t = (T)((object)Activator.CreateInstance(typeof(T)));
				t.LuCourseId = (int)reader[name];
				t.StartDate = (DateTime)reader[colNamePrefix + "startdate"];
				t.EndDate = (DateTime)reader[colNamePrefix + "enddate"];
				t.Duration = reader[colNamePrefix + "duration"].ToString();
				t.Term = reader[colNamePrefix + "term"].ToString();
				t.Subject = LookupSubjectDAO.GetSubjectFromCourseRecord(colNamePrefix, reader);
				t.Course = reader[colNamePrefix + "course"].ToString();
				t.Section = reader[colNamePrefix + "section"].ToString();
				t.TimeOfDay = reader[colNamePrefix + "timeofday"].ToString();
				t.Campus = (LookupCourseDAO.ReaderContainsColumn(reader, text) ? reader[text].ToString() : "");
				t.Department = (LookupCourseDAO.ReaderContainsColumn(reader, text2) ? reader[text2].ToString() : "");
				t.Location = (LookupCourseDAO.ReaderContainsColumn(reader, text3) ? reader[text3].ToString() : "");
				t.CourseNote = (LookupCourseDAO.ReaderContainsColumn(reader, text4) ? reader[text4].ToString() : "");
				result = t;
			}
			return result;
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x00023618 File Offset: 0x00021818
		internal static LookupCourseBaseWithPrimaryInstructor GetCourseBaseWithPrimaryInstructorFromReader(string colNamePrefix, IDataReader reader)
		{
			LookupCourseBaseWithPrimaryInstructor courseBaseFromReader = LookupCourseDAO.GetCourseBaseFromReader<LookupCourseBaseWithPrimaryInstructor>(colNamePrefix, reader);
			bool flag = courseBaseFromReader == null;
			LookupCourseBaseWithPrimaryInstructor result;
			if (flag)
			{
				result = null;
			}
			else
			{
				LookupInstructor instructorFromReader = LookupInstructorDAO.GetInstructorFromReader(reader, colNamePrefix);
				courseBaseFromReader.PrimaryInstructor = instructorFromReader;
				result = courseBaseFromReader;
			}
			return result;
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x00023650 File Offset: 0x00021850
		internal static List<LookupCourseBase> GetCoursesBaseFromReader(string colNamePrefix, IDataReader reader)
		{
			bool flag = reader != null;
			List<LookupCourseBase> result;
			if (flag)
			{
				List<LookupCourseBase> list = new List<LookupCourseBase>();
				int num = 0;
				while (reader.Read())
				{
					object obj = reader[colNamePrefix + "lucourseid"];
					int num2 = (int)obj;
					bool flag2 = num2 != num;
					if (flag2)
					{
						num = num2;
						LookupCourseBase courseBaseFromReader = LookupCourseDAO.GetCourseBaseFromReader(colNamePrefix, reader);
						list.Add(courseBaseFromReader);
					}
				}
				result = list;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x000236D8 File Offset: 0x000218D8
		internal static List<LookupCourse> GetCoursesFromReader(string colNamePrefix, IDataReader record)
		{
			bool flag = record != null;
			List<LookupCourse> result;
			if (flag)
			{
				List<LookupCourse> list = new List<LookupCourse>();
				LookupCourse lookupCourse = null;
				int num = 0;
				while (record.Read())
				{
					object obj = record[colNamePrefix + "lucourseid"];
					int num2 = (int)obj;
					bool flag2 = num2 != num;
					LookupCourse lookupCourse2;
					if (flag2)
					{
						num = num2;
						lookupCourse2 = new LookupCourse
						{
							LuCourseId = num2
						};
						LookupCourseDAO.GetMainCourseFromReader(lookupCourse2, colNamePrefix, record);
						lookupCourse = lookupCourse2;
						list.Add(lookupCourse2);
					}
					else
					{
						lookupCourse2 = lookupCourse;
					}
					LookupCourseDAO.AddCourseInfoFromReader(lookupCourse2, colNamePrefix, record);
				}
				result = list;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x00023780 File Offset: 0x00021980
		public List<CourseRegistration> LoadStudentsCourses(int PersonId, DateTime StartDate, DateTime EndDate)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@pid", DbType.Int32, PersonId),
				this.DatabaseManager.GetParameter("@sd", DbType.DateTime, StartDate),
				this.DatabaseManager.GetParameter("@ed", DbType.DateTime, EndDate.AddDays(1.0))
			};
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT luc.lucourseid,luc.startdate,luc.enddate,luc.duration,luc.term,luc.subjectid,luc.externalid,luc.exemptfromdatasync AS lucexemptfromdatasync\r\n        ,lucd.lookupstring AS subjectcode,lucd.altlookupstring AS subjectdescription\r\n        ,luc.course,luc.timeofday,luc.[section]\r\n        ,luc.campus,luc.department,luc.location,luc.credits\r\n        ,luc.instructorid AS pinstructorid,lucd2.altlookupstring AS pinstructorname,lucd2.email AS pinstructoremail,lucd2.phone AS pinstructorphone,lucd2.username AS pinstructorusername,lucd2.exemptfromdatasync AS pexemptfromdatasync,lucd2.id AS pinstructoremployeeid,lucd2.externalid AS pinstructorexternalid\r\n        ,luc.ExemptAssignmentFromDataSync AS pExemptAssignmentFromDataSync\r\n        ,luc.BatchDataSyncLogId\r\n        ,lci.instructorid AS p3instructorid,lucd3.altlookupstring AS p3instructorname,lucd3.email AS p3instructoremail,lucd3.phone AS p3instructorphone,lucd3.username AS p3instructorusername,lucd3.exemptfromdatasync AS p3exemptfromdatasync,lucd3.id AS p3instructoremployeeid,lucd3.externalid AS p3instructorexternalid\r\n        ,lci.ExemptAssignmentFromDataSync AS p3ExemptAssignmentFromDataSync\r\n        ,tt.timetableid\r\n        ,tt.sunstartminutes,tt.sunendminutes,tt.monstartminutes,tt.monendminutes,tt.tuestartminutes,tt.tueendminutes\r\n        ,tt.wedstartminutes,tt.wedendminutes,tt.thustartminutes,tt.thuendminutes,tt.fristartminutes,tt.friendminutes\r\n        ,tt.satstartminutes,tt.satendminutes,tt.sunroom,tt.monroom,tt.tueroom,tt.wedroom,tt.thuroom,tt.friroom,tt.satroom,\r\n        luc.alternatecontactid,ac.altname,ac.altemail,ac.altphone,ac.altusername,ac.externalid,ac.altpermissionlevel,\r\n        lucac.alternatecontactid AS secondaryalternatecontactid,\r\n        ac2.altname AS secondaryaltname,ac2.altemail AS secondaryaltemail,ac2.altphone AS secondaryaltphone,\r\n        ac2.altusername AS secondaryaltusername,ac2.externalid AS secondaryexternalid,\r\n        ac2.altpermissionlevel AS secondaryaltpermissionlevel,\r\n        luc.coursenote, c.personid, c.registrationstatus,c.CoursesID,c.dateAdded,c.dateinstructorlastviewed,c.datestudentlastviewed,\r\n\t\tc.DateLetterIssued,c.DateLetterReturned,\r\n\t\tp.PersonID, p.firstname, p.lastname, p.middlename, p.student_no, pg.mingroupid,\r\n\t\tpwhoadded.PersonID as whoaddedpersonid, pwhoadded.firstname as whoaddedfirstname, pwhoadded.lastname as whoaddedlastname, pwhoadded.middlename as whoaddedmiddlename, pwhoadded.student_no as whoaddedstudent_no, pgwhoadded.mingroupid as whoaddedmingroupid\r\nFROM    courses c LEFT JOIN lucourses luc ON luc.lucourseid=c.lucourseid\r\n        LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid\r\n        LEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luc.instructorid\r\n        LEFT JOIN lucourseinstructor lci ON lci.lucourseid=luc.lucourseid\r\n        LEFT JOIN lucoursedata lucd3 ON lucd3.lucoursedataid=lci.instructorid\r\n        LEFT JOIN timetable tt ON tt.timetabletype='C' AND tt.lucourseid=luc.lucourseid\r\n        LEFT JOIN lucoursealternatecontact ac ON ac.alternatecontactid=luc.alternatecontactid\r\n        LEFT JOIN LuCourseAltContact lucac ON lucac.lucourseid=luc.lucourseid\r\n        LEFT JOIN lucoursealternatecontact ac2 ON ac2.alternatecontactid=lucac.alternatecontactid\r\n\t\tLEFT JOIN People p ON p.PersonId = @pid\r\n\t\tLEFT JOIN peoplemingroup pg ON pg.PersonID = @pid\r\n\t\tLEFT JOIN People pwhoadded ON pwhoadded.PersonId = c.whoAdded\r\n\t\tLEFT JOIN peoplemingroup pgwhoadded ON pgwhoadded.PersonID = c.whoAdded\r\nWHERE   c.personid=@pid\r\n        AND (c.registrationstatus IS NULL OR NOT c.registrationstatus=2)\r\n        AND (NOT ( enddate <= @sd OR startdate > @ed))\r\n        AND NOT luc.lucourseid IS NULL\r\nORDER BY luc.startdate,luc.duration,luc.term,lucd.altlookupstring,luc.course,luc.[section],luc.timeofday,luc.lucourseid", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					return CourseRegistrationDAO.GetCourseRegistrationsFromReader0<CourseRegistration>("", dataReader, this.OpContext);
				}
			}
			return null;
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x0002384C File Offset: 0x00021A4C
		public LookupCourse LoadCourse(int LuCourseId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@lucourseid", DbType.Int32, LuCourseId)
			};
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT luc.lucourseid,luc.startdate,luc.enddate,luc.duration,luc.term,luc.subjectid,luc.externalid,luc.exemptfromdatasync AS lucexemptfromdatasync\r\n        ,lucd.lookupstring AS subjectcode,lucd.altlookupstring AS subjectdescription\r\n        ,luc.course,luc.timeofday,luc.[section]\r\n        ,luc.campus,luc.department,luc.location,luc.credits\r\n        ,luc.instructorid AS pinstructorid,lucd2.altlookupstring AS pinstructorname,lucd2.email AS pinstructoremail,lucd2.phone AS pinstructorphone,lucd2.username AS pinstructorusername,lucd2.exemptfromdatasync AS pexemptfromdatasync,lucd2.id AS pinstructoremployeeid,lucd2.externalid AS pinstructorexternalid\r\n        ,luc.ExemptAssignmentFromDataSync AS pExemptAssignmentFromDataSync\r\n        ,luc.BatchDataSyncLogId\r\n        ,lci.instructorid AS p3instructorid,lucd3.altlookupstring AS p3instructorname,lucd3.email AS p3instructoremail,lucd3.phone AS p3instructorphone,lucd3.username AS p3instructorusername,lucd3.exemptfromdatasync AS p3exemptfromdatasync,lucd3.id AS p3instructoremployeeid,lucd3.externalid AS p3instructorexternalid\r\n        ,lci.ExemptAssignmentFromDataSync AS p3ExemptAssignmentFromDataSync\r\n        ,tt.timetableid\r\n        ,tt.sunstartminutes,tt.sunendminutes,tt.monstartminutes,tt.monendminutes,tt.tuestartminutes,tt.tueendminutes\r\n        ,tt.wedstartminutes,tt.wedendminutes,tt.thustartminutes,tt.thuendminutes,tt.fristartminutes,tt.friendminutes\r\n        ,tt.satstartminutes,tt.satendminutes,tt.sunroom,tt.monroom,tt.tueroom,tt.wedroom,tt.thuroom,tt.friroom,tt.satroom,\r\n        luc.alternatecontactid,ac.altname,ac.altemail,ac.altphone,ac.altusername,ac.externalid,ac.altpermissionlevel,\r\n        lucac.alternatecontactid AS secondaryalternatecontactid,\r\n        ac2.altname AS secondaryaltname,ac2.altemail AS secondaryaltemail,ac2.altphone AS secondaryaltphone,\r\n        ac2.altusername AS secondaryaltusername,ac2.externalid AS secondaryexternalid,\r\n        ac2.altpermissionlevel AS secondaryaltpermissionlevel,\r\n        luc.coursenote\r\nFROM    lucourses luc LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid\r\n        LEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luc.instructorid\r\n        LEFT JOIN lucourseinstructor lci ON lci.lucourseid=luc.lucourseid\r\n        LEFT JOIN lucoursedata lucd3 ON lucd3.lucoursedataid=lci.instructorid\r\n        LEFT JOIN timetable tt ON tt.timetabletype='C' AND tt.lucourseid=luc.lucourseid\r\n        LEFT JOIN lucoursealternatecontact ac ON ac.alternatecontactid=luc.alternatecontactid\r\n        LEFT JOIN LuCourseAltContact lucac ON lucac.lucourseid=luc.lucourseid\r\n        LEFT JOIN lucoursealternatecontact ac2 ON ac2.alternatecontactid=lucac.alternatecontactid\r\nWHERE   luc.lucourseid=@lucourseid\r\nORDER BY luc.startdate,luc.duration,luc.term,lucd.altlookupstring,luc.course,luc.[section],luc.timeofday,luc.lucourseid", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					List<LookupCourse> coursesFromReader = LookupCourseDAO.GetCoursesFromReader("", dataReader);
					return (coursesFromReader.Count > 0) ? coursesFromReader[0] : null;
				}
			}
			return null;
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x000238E0 File Offset: 0x00021AE0
		public List<int> LoadLookupCourseIdsWithAtLeastOneClassTestDefinition(List<int> LuCourseIds, DateTime StartDate, DateTime EndDate)
		{
			DbParameter[] array = new DbParameter[3];
			array[0] = this.DatabaseManager.GetParameter("@lucids", DbType.String, string.Join(",", LuCourseIds.ConvertAll<string>((int f) => f.ToString()).ToArray()));
			array[1] = this.DatabaseManager.GetParameter("@startdate", DbType.DateTime, StartDate.Date);
			array[2] = this.DatabaseManager.GetParameter("@enddate", DbType.DateTime, EndDate.Date.AddDays(1.0).AddMinutes(-1.0));
			DbParameter[] parameters = array;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT lucourseid FROM exams WHERE lucourseid IN (SELECT orderid AS lucourseid FROM splitorderids(@lucids,',')) AND dateoftest>=@startdate AND dateoftest<@enddate", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					List<int> list = new List<int>();
					while (dataReader.Read())
					{
						int num = (int)dataReader["lucourseid"];
						bool flag2 = num > 0;
						if (flag2)
						{
							list.Add(num);
						}
					}
					return list;
				}
			}
			return null;
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x00023A28 File Offset: 0x00021C28
		public List<LookupCourseBase> LoadCourseBaseInfoByDate(DateTime StartDate, DateTime EndDate)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@startdate", DbType.DateTime, StartDate),
				this.DatabaseManager.GetParameter("@enddate", DbType.DateTime, EndDate)
			};
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT    luc.lucourseid,luc.startdate,luc.enddate,luc.duration,luc.term,luc.subjectid,luc.externalid,luc.exemptfromdatasync AS lucexemptfromdatasync,\r\n            lucd.lookupstring AS subjectcode,lucd.altlookupstring AS subjectdescription,\r\n            luc.course,luc.timeofday,luc.[section],\r\n            luc.campus,luc.department,luc.location,luc.credits,\r\n            luc.coursenote\r\nFROM        lucourses luc LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid\r\nWHERE       NOT ( luc.enddate <= @startdate OR luc.startdate > @enddate)\r\nORDER BY    luc.startdate,luc.duration,luc.term,lucd.altlookupstring,luc.course,luc.[section],luc.timeofday,luc.lucourseid", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					return LookupCourseDAO.GetCoursesBaseFromReader("", dataReader);
				}
			}
			return null;
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x00023AC4 File Offset: 0x00021CC4
		public List<LookupCourse> LoadCoursesByDate(DateTime StartDate, DateTime EndDate)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@startdate", DbType.DateTime, StartDate),
				this.DatabaseManager.GetParameter("@enddate", DbType.DateTime, EndDate)
			};
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT luc.lucourseid,luc.startdate,luc.enddate,luc.duration,luc.term,luc.subjectid,luc.externalid,luc.exemptfromdatasync AS lucexemptfromdatasync\r\n        ,lucd.lookupstring AS subjectcode,lucd.altlookupstring AS subjectdescription\r\n        ,luc.course,luc.timeofday,luc.[section]\r\n        ,luc.campus,luc.department,luc.location,luc.credits\r\n        ,luc.instructorid AS pinstructorid,lucd2.altlookupstring AS pinstructorname,lucd2.email AS pinstructoremail,lucd2.phone AS pinstructorphone,lucd2.username AS pinstructorusername,lucd2.exemptfromdatasync AS pexemptfromdatasync,lucd2.id AS pinstructoremployeeid,lucd2.externalid AS pinstructorexternalid\r\n        ,luc.ExemptAssignmentFromDataSync AS pExemptAssignmentFromDataSync\r\n        ,luc.BatchDataSyncLogId\r\n        ,lci.instructorid AS p3instructorid,lucd3.altlookupstring AS p3instructorname,lucd3.email AS p3instructoremail,lucd3.phone AS p3instructorphone,lucd3.username AS p3instructorusername,lucd3.exemptfromdatasync AS p3exemptfromdatasync,lucd3.id AS p3instructoremployeeid,lucd3.externalid AS p3instructorexternalid\r\n        ,lci.ExemptAssignmentFromDataSync AS p3ExemptAssignmentFromDataSync\r\n        ,tt.timetableid\r\n        ,tt.sunstartminutes,tt.sunendminutes,tt.monstartminutes,tt.monendminutes,tt.tuestartminutes,tt.tueendminutes\r\n        ,tt.wedstartminutes,tt.wedendminutes,tt.thustartminutes,tt.thuendminutes,tt.fristartminutes,tt.friendminutes\r\n        ,tt.satstartminutes,tt.satendminutes,tt.sunroom,tt.monroom,tt.tueroom,tt.wedroom,tt.thuroom,tt.friroom,tt.satroom,\r\n        luc.alternatecontactid,ac.altname,ac.altemail,ac.altphone,ac.altusername,ac.externalid,ac.altpermissionlevel,\r\n        lucac.alternatecontactid AS secondaryalternatecontactid,\r\n        ac2.altname AS secondaryaltname,ac2.altemail AS secondaryaltemail,ac2.altphone AS secondaryaltphone,\r\n        ac2.altusername AS secondaryaltusername,ac2.externalid AS secondaryexternalid,\r\n        ac2.altpermissionlevel AS secondaryaltpermissionlevel,\r\n        luc.coursenote\r\nFROM    lucourses luc LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid\r\n        LEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luc.instructorid\r\n        LEFT JOIN lucourseinstructor lci ON lci.lucourseid=luc.lucourseid\r\n        LEFT JOIN lucoursedata lucd3 ON lucd3.lucoursedataid=lci.instructorid\r\n        LEFT JOIN timetable tt ON tt.timetabletype='C' AND tt.lucourseid=luc.lucourseid\r\n        LEFT JOIN lucoursealternatecontact ac ON ac.alternatecontactid=luc.alternatecontactid\r\n        LEFT JOIN LuCourseAltContact lucac ON lucac.lucourseid=luc.lucourseid\r\n        LEFT JOIN lucoursealternatecontact ac2 ON ac2.alternatecontactid=lucac.alternatecontactid\r\nWHERE   NOT ( luc.enddate <= @startdate OR luc.startdate > @enddate)\r\nORDER BY luc.startdate,luc.duration,luc.term,lucd.altlookupstring,luc.course,luc.[section],luc.timeofday,luc.lucourseid", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					return LookupCourseDAO.GetCoursesFromReader("", dataReader);
				}
			}
			return null;
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x00023B60 File Offset: 0x00021D60
		public IList<LookupCourse> LoadCoursesByIds(IList<int> LuCourseIds)
		{
			DbParameter[] array = new DbParameter[1];
			array[0] = this.DatabaseManager.GetParameter("@lucids", DbType.String, string.Join(",", LuCourseIds.ToList<int>().ConvertAll<string>((int f) => f.ToString()).ToArray()));
			DbParameter[] parameters = array;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT luc.lucourseid,luc.startdate,luc.enddate,luc.duration,luc.term,luc.subjectid,luc.externalid,luc.exemptfromdatasync AS lucexemptfromdatasync\r\n        ,lucd.lookupstring AS subjectcode,lucd.altlookupstring AS subjectdescription\r\n        ,luc.course,luc.timeofday,luc.[section]\r\n        ,luc.campus,luc.department,luc.location,luc.credits\r\n        ,luc.instructorid AS pinstructorid,lucd2.altlookupstring AS pinstructorname,lucd2.email AS pinstructoremail,lucd2.phone AS pinstructorphone,lucd2.username AS pinstructorusername,lucd2.exemptfromdatasync AS pexemptfromdatasync,lucd2.id AS pinstructoremployeeid,lucd2.externalid AS pinstructorexternalid\r\n        ,luc.ExemptAssignmentFromDataSync AS pExemptAssignmentFromDataSync\r\n        ,luc.BatchDataSyncLogId\r\n        ,lci.instructorid AS p3instructorid,lucd3.altlookupstring AS p3instructorname,lucd3.email AS p3instructoremail,lucd3.phone AS p3instructorphone,lucd3.username AS p3instructorusername,lucd3.exemptfromdatasync AS p3exemptfromdatasync,lucd3.id AS p3instructoremployeeid,lucd3.externalid AS p3instructorexternalid\r\n        ,lci.ExemptAssignmentFromDataSync AS p3ExemptAssignmentFromDataSync\r\n        ,tt.timetableid\r\n        ,tt.sunstartminutes,tt.sunendminutes,tt.monstartminutes,tt.monendminutes,tt.tuestartminutes,tt.tueendminutes\r\n        ,tt.wedstartminutes,tt.wedendminutes,tt.thustartminutes,tt.thuendminutes,tt.fristartminutes,tt.friendminutes\r\n        ,tt.satstartminutes,tt.satendminutes,tt.sunroom,tt.monroom,tt.tueroom,tt.wedroom,tt.thuroom,tt.friroom,tt.satroom,\r\n        luc.alternatecontactid,ac.altname,ac.altemail,ac.altphone,ac.altusername,ac.externalid,ac.altpermissionlevel,\r\n        lucac.alternatecontactid AS secondaryalternatecontactid,\r\n        ac2.altname AS secondaryaltname,ac2.altemail AS secondaryaltemail,ac2.altphone AS secondaryaltphone,\r\n        ac2.altusername AS secondaryaltusername,ac2.externalid AS secondaryexternalid,\r\n        ac2.altpermissionlevel AS secondaryaltpermissionlevel,\r\n        luc.coursenote\r\nFROM    lucourses luc LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid\r\n        LEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luc.instructorid\r\n        LEFT JOIN lucourseinstructor lci ON lci.lucourseid=luc.lucourseid\r\n        LEFT JOIN lucoursedata lucd3 ON lucd3.lucoursedataid=lci.instructorid\r\n        LEFT JOIN timetable tt ON tt.timetabletype='C' AND tt.lucourseid=luc.lucourseid\r\n        LEFT JOIN lucoursealternatecontact ac ON ac.alternatecontactid=luc.alternatecontactid\r\n        LEFT JOIN LuCourseAltContact lucac ON lucac.lucourseid=luc.lucourseid\r\n        LEFT JOIN lucoursealternatecontact ac2 ON ac2.alternatecontactid=lucac.alternatecontactid\r\nWHERE   luc.lucourseid IN (SELECT orderid AS lucourseid FROM splitorderids(@lucids,','))\r\nORDER BY luc.startdate,luc.duration,luc.term,lucd.altlookupstring,luc.course,luc.[section],luc.timeofday,luc.lucourseid", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					return LookupCourseDAO.GetCoursesFromReader("", dataReader);
				}
			}
			return null;
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000411 RID: 1041 RVA: 0x00023C18 File Offset: 0x00021E18
		private LookupSubjectDAO lookupSubjectDAO
		{
			get
			{
				bool flag = this.lsd == null;
				if (flag)
				{
					this.lsd = new LookupSubjectDAO(this.OpContext);
				}
				return this.lsd;
			}
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x00023C50 File Offset: 0x00021E50
		public void SaveCourse(LookupCourse course)
		{
			LookupSubjectDAO lookupSubjectDAO = this.lookupSubjectDAO;
			lookupSubjectDAO.SaveSubject(course.Subject);
			LookupInstructorDAO lookupInstructorDAO = this.lookupInstructorDAO;
			foreach (LookupInstructor instructor in course.Instructors)
			{
				lookupInstructorDAO.SaveInstructor(instructor);
			}
			bool flag = course.LuCourseId > 0;
			if (flag)
			{
				DbParameter[] parameters = new DbParameter[]
				{
					this.DatabaseManager.GetParameter("@lucid", DbType.Int32, course.LuCourseId),
					this.DatabaseManager.GetParameter("@campus", DbType.String, course.Campus),
					this.DatabaseManager.GetParameter("@department", DbType.String, course.Department),
					this.DatabaseManager.GetParameter("@location", DbType.String, course.Location)
				};
				this.DatabaseManager.ExecuteNonQuery("UPDATE lucourses SET campus=@campus,department=@department,location=@location WHERE lucourseid=@lucid", parameters);
			}
			else
			{
				DbParameter[] parameters = new DbParameter[]
				{
					this.DatabaseManager.GetParameter("@term", DbType.String, course.Term),
					this.DatabaseManager.GetParameter("@campus", DbType.String, course.Campus ?? ""),
					this.DatabaseManager.GetParameter("@department", DbType.String, course.Department ?? ""),
					this.DatabaseManager.GetParameter("@location", DbType.String, course.Location ?? ""),
					this.DatabaseManager.GetParameter("@duration", DbType.String, course.Duration ?? ""),
					this.DatabaseManager.GetParameter("@course", DbType.String, course.Course),
					this.DatabaseManager.GetParameter("@section", DbType.String, course.Section),
					this.DatabaseManager.GetParameter("@timeofday", DbType.String, course.TimeOfDay ?? ""),
					this.DatabaseManager.GetParameter("@startdate", DbType.DateTime, course.StartDate),
					this.DatabaseManager.GetParameter("@enddate", DbType.DateTime, course.EndDate),
					this.DatabaseManager.GetParameter("@subjectid", DbType.Int32, course.Subject.Id),
					this.DatabaseManager.GetParameter("@primaryinstructorid", DbType.Int32, (course.Instructors.Count > 0) ? course.Instructors[0].InstructorId : -1),
					this.DatabaseManager.GetParameter("@externalid", DbType.String, course.ExternalCourseId ?? ""),
					this.DatabaseManager.GetParameter("@whoami", DbType.Int32, (this.OpContext == null) ? 0 : this.OpContext.WhoAmI),
					this.DatabaseManager.GetParameter("@credits", DbType.Decimal, course.Credits)
				};
				object obj = this.DatabaseManager.ExecuteQuery("IF EXISTS( SELECT lucourseid FROM lucourses luc WHERE NOT ( luc.enddate <= @startdate OR luc.startdate > @enddate)\r\n        AND luc.duration=@duration AND luc.term=@term AND luc.subjectid=@subjectid AND luc.course=@course\r\n        AND luc.section=@section AND luc.timeofday=@timeofday AND luc.campus=@campus\r\n)\r\nBEGIN\r\n    SELECT lucourseid FROM lucourses luc WHERE NOT ( luc.enddate <= @startdate OR luc.startdate > @enddate)\r\n        AND luc.duration=@duration AND luc.term=@term AND luc.subjectid=@subjectid AND luc.course=@course\r\n        AND luc.section=@section AND luc.timeofday=@timeofday AND luc.campus=@campus\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO lucourses (term,duration,subjectid,course,section,timeofday,startdate,enddate,campus,department,location,externalid,instructorid,whoadded,credits)\r\n        VALUES (@term,@duration,@subjectid,@course,@section,@timeofday,@startdate,@enddate,@campus,@department,@location,@externalid,@primaryinstructorid,@whoami,@credits);\r\n    SELECT CAST(SCOPE_IDENTITY() AS int) AS lucourseid;\r\nEND", parameters);
				int num = (obj == null) ? 0 : ((int)obj);
				course.LuCourseId = num;
				bool flag2 = num < 1;
				if (flag2)
				{
					throw new Exception("Unable to create course");
				}
			}
			LookupTimetableItemDAO lookupTimetableItemDAO = this.lookupTimetableItemDAO;
			lookupInstructorDAO.SaveInstructorsForCourse(course.LuCourseId, course.Instructors, false);
			lookupTimetableItemDAO.SaveLookupTimetableItems(course.LuCourseId, course.TimetableItems);
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x00024004 File Offset: 0x00022204
		public List<LookupCourse> LoadLookupCoursesByInstructor(int InstructorId, DateTime StartDate, DateTime EndDate)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@instructorid", DbType.Int32, InstructorId),
				this.DatabaseManager.GetParameter("@startdate", DbType.DateTime, StartDate),
				this.DatabaseManager.GetParameter("@enddate", DbType.DateTime, EndDate)
			};
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT luc.lucourseid,luc.startdate,luc.enddate,luc.duration,luc.term,luc.subjectid,luc.externalid,luc.exemptfromdatasync AS lucexemptfromdatasync\r\n        ,lucd.lookupstring AS subjectcode,lucd.altlookupstring AS subjectdescription\r\n        ,luc.course,luc.timeofday,luc.[section]\r\n        ,luc.campus,luc.department,luc.location,luc.credits\r\n        ,luc.instructorid AS pinstructorid,lucd2.altlookupstring AS pinstructorname,lucd2.email AS pinstructoremail,lucd2.phone AS pinstructorphone,lucd2.username AS pinstructorusername,lucd2.exemptfromdatasync AS pexemptfromdatasync,lucd2.id AS pinstructoremployeeid,lucd2.externalid AS pinstructorexternalid\r\n        ,luc.ExemptAssignmentFromDataSync AS pExemptAssignmentFromDataSync\r\n        ,luc.BatchDataSyncLogId\r\n        ,lci.instructorid AS p3instructorid,lucd3.altlookupstring AS p3instructorname,lucd3.email AS p3instructoremail,lucd3.phone AS p3instructorphone,lucd3.username AS p3instructorusername,lucd3.exemptfromdatasync AS p3exemptfromdatasync,lucd3.id AS p3instructoremployeeid,lucd3.externalid AS p3instructorexternalid\r\n        ,lci.ExemptAssignmentFromDataSync AS p3ExemptAssignmentFromDataSync\r\n        ,tt.timetableid\r\n        ,tt.sunstartminutes,tt.sunendminutes,tt.monstartminutes,tt.monendminutes,tt.tuestartminutes,tt.tueendminutes\r\n        ,tt.wedstartminutes,tt.wedendminutes,tt.thustartminutes,tt.thuendminutes,tt.fristartminutes,tt.friendminutes\r\n        ,tt.satstartminutes,tt.satendminutes,tt.sunroom,tt.monroom,tt.tueroom,tt.wedroom,tt.thuroom,tt.friroom,tt.satroom,\r\n        luc.alternatecontactid,ac.altname,ac.altemail,ac.altphone,ac.altusername,ac.externalid,ac.altpermissionlevel,\r\n        lucac.alternatecontactid AS secondaryalternatecontactid,\r\n        ac2.altname AS secondaryaltname,ac2.altemail AS secondaryaltemail,ac2.altphone AS secondaryaltphone,\r\n        ac2.altusername AS secondaryaltusername,ac2.externalid AS secondaryexternalid,\r\n        ac2.altpermissionlevel AS secondaryaltpermissionlevel,\r\n        luc.coursenote\r\nFROM    lucourses luc LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid\r\n        LEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luc.instructorid\r\n        LEFT JOIN lucourseinstructor lci ON lci.lucourseid=luc.lucourseid\r\n        LEFT JOIN lucoursedata lucd3 ON lucd3.lucoursedataid=lci.instructorid\r\n        LEFT JOIN timetable tt ON tt.timetabletype='C' AND tt.lucourseid=luc.lucourseid\r\n        LEFT JOIN lucoursealternatecontact ac ON ac.alternatecontactid=luc.alternatecontactid\r\n        LEFT JOIN LuCourseAltContact lucac ON lucac.lucourseid=luc.lucourseid\r\n        LEFT JOIN lucoursealternatecontact ac2 ON ac2.alternatecontactid=lucac.alternatecontactid\r\nWHERE   luc.instructorid=@instructorid OR lci.instructorid=@instructorid\r\nORDER BY luc.startdate,luc.duration,luc.term,lucd.altlookupstring,luc.course,luc.[section],luc.timeofday,luc.lucourseid", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					return LookupCourseDAO.GetCoursesFromReader("", dataReader);
				}
			}
			return null;
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x000240BC File Offset: 0x000222BC
		public List<LookupCourse> LoadCoursesBySubjectAndSession(Session Session, int SubjectId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@startdate", DbType.DateTime, Session.StartDate),
				this.DatabaseManager.GetParameter("@enddate", DbType.DateTime, Session.EndDate),
				this.DatabaseManager.GetParameter("@subjectid", DbType.Int32, SubjectId)
			};
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECTluc.lucourseid,luc.startdate,luc.enddate,luc.duration,luc.term,luc.subjectid,luc.externalid,luc.exemptfromdatasync AS lucexemptfromdatasync\r\n        ,lucd.lookupstring AS subjectcode,lucd.altlookupstring AS subjectdescription\r\n        ,luc.course,luc.timeofday,luc.[section]\r\n        ,luc.campus,luc.department,luc.location,luc.credits\r\n        ,luc.instructorid AS pinstructorid,lucd2.altlookupstring AS pinstructorname,lucd2.email AS pinstructoremail,lucd2.phone AS pinstructorphone,lucd2.username AS pinstructorusername,lucd2.exemptfromdatasync AS pexemptfromdatasync,lucd2.id AS pinstructoremployeeid,lucd2.externalid AS pinstructorexternalid\r\n        ,luc.ExemptAssignmentFromDataSync AS pExemptAssignmentFromDataSync\r\n        ,luc.BatchDataSyncLogId\r\n        ,lci.instructorid AS p3instructorid,lucd3.altlookupstring AS p3instructorname,lucd3.email AS p3instructoremail,lucd3.phone AS p3instructorphone,lucd3.username AS p3instructorusername,lucd3.exemptfromdatasync AS p3exemptfromdatasync,lucd3.id AS p3instructoremployeeid,lucd3.externalid AS p3instructorexternalid\r\n        ,lci.ExemptAssignmentFromDataSync AS p3ExemptAssignmentFromDataSync\r\n        ,tt.timetableid\r\n        ,tt.sunstartminutes,tt.sunendminutes,tt.monstartminutes,tt.monendminutes,tt.tuestartminutes,tt.tueendminutes\r\n        ,tt.wedstartminutes,tt.wedendminutes,tt.thustartminutes,tt.thuendminutes,tt.fristartminutes,tt.friendminutes\r\n        ,tt.satstartminutes,tt.satendminutes,tt.sunroom,tt.monroom,tt.tueroom,tt.wedroom,tt.thuroom,tt.friroom,tt.satroom,\r\n        luc.alternatecontactid,ac.altname,ac.altemail,ac.altphone,ac.altusername,ac.externalid,ac.altpermissionlevel,\r\n        lucac.alternatecontactid AS secondaryalternatecontactid,\r\n        ac2.altname AS secondaryaltname,ac2.altemail AS secondaryaltemail,ac2.altphone AS secondaryaltphone,\r\n        ac2.altusername AS secondaryaltusername,ac2.externalid AS secondaryexternalid,\r\n        ac2.altpermissionlevel AS secondaryaltpermissionlevel,\r\n        luc.coursenote\r\nFROM    lucourses luc LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid\r\n        LEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luc.instructorid\r\n        LEFT JOIN lucourseinstructor lci ON lci.lucourseid=luc.lucourseid\r\n        LEFT JOIN lucoursedata lucd3 ON lucd3.lucoursedataid=lci.instructorid\r\n        LEFT JOIN timetable tt ON tt.timetabletype='C' AND tt.lucourseid=luc.lucourseid\r\n        LEFT JOIN lucoursealternatecontact ac ON ac.alternatecontactid=luc.alternatecontactid\r\n        LEFT JOIN LuCourseAltContact lucac ON lucac.lucourseid=luc.lucourseid\r\n        LEFT JOIN lucoursealternatecontact ac2 ON ac2.alternatecontactid=lucac.alternatecontactid\r\nWHERE   NOT ( luc.enddate <= @startdate OR luc.startdate > @enddate)\r\n        AND luc.subjectid=@subjectid\r\nORDER BY luc.startdate,luc.duration,luc.term,lucd.altlookupstring,luc.course,luc.[section],luc.timeofday,luc.lucourseid", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					return LookupCourseDAO.GetCoursesFromReader("", dataReader);
				}
			}
			return null;
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0002417C File Offset: 0x0002237C
		public LookupCourse CreateLookupCourseBase(LookupCourseBase CourseBase)
		{
			bool flag = CourseBase != null && CourseBase.Subject != null && CourseBase.Subject.SubjectId < 1 && !string.IsNullOrEmpty(CourseBase.Subject.SubjectDescription);
			if (flag)
			{
				LookupSubject lookupSubject = this.lookupSubjectDao.LoadLookupSubject(CourseBase.Subject.SubjectCode ?? "", CourseBase.Subject.SubjectDescription);
				bool flag2 = lookupSubject != null && lookupSubject.SubjectId > 0;
				if (flag2)
				{
					CourseBase.Subject = lookupSubject;
				}
				else
				{
					this.lookupSubjectDao.SaveSubject(CourseBase.Subject);
				}
			}
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@startdate", DbType.DateTime, CourseBase.StartDate),
				this.DatabaseManager.GetParameter("@enddate", DbType.DateTime, CourseBase.EndDate),
				this.DatabaseManager.GetParameter("@duration", DbType.String, CourseBase.Duration ?? ""),
				this.DatabaseManager.GetParameter("@term", DbType.String, CourseBase.Term ?? ""),
				this.DatabaseManager.GetParameter("@subjectid", DbType.Int32, CourseBase.Subject.SubjectId),
				this.DatabaseManager.GetParameter("@course", DbType.String, CourseBase.Course ?? ""),
				this.DatabaseManager.GetParameter("@section", DbType.String, CourseBase.Section ?? ""),
				this.DatabaseManager.GetParameter("@timeofday", DbType.String, CourseBase.TimeOfDay ?? ""),
				this.DatabaseManager.GetParameter("@campus", DbType.String, CourseBase.Campus ?? ""),
				this.DatabaseManager.GetParameter("@primaryinstructorid", DbType.Int32, -1),
				this.DatabaseManager.GetParameter("@location", DbType.String, CourseBase.Location ?? ""),
				this.DatabaseManager.GetParameter("@externalid", DbType.String, ""),
				this.DatabaseManager.GetParameter("@department", DbType.String, CourseBase.Department ?? ""),
				this.DatabaseManager.GetParameter("@whoami", DbType.Int32, (this.OpContext == null) ? 0 : this.OpContext.WhoAmI),
				this.DatabaseManager.GetParameter("@credits", DbType.Decimal, CourseBase.Credits)
			};
			object obj = this.DatabaseManager.ExecuteScalar("IF EXISTS( SELECT lucourseid FROM lucourses luc WHERE NOT ( luc.enddate <= @startdate OR luc.startdate > @enddate)\r\n        AND luc.duration=@duration AND luc.term=@term AND luc.subjectid=@subjectid AND luc.course=@course\r\n        AND luc.section=@section AND luc.timeofday=@timeofday AND luc.campus=@campus\r\n)\r\nBEGIN\r\n    SELECT lucourseid FROM lucourses luc WHERE NOT ( luc.enddate <= @startdate OR luc.startdate > @enddate)\r\n        AND luc.duration=@duration AND luc.term=@term AND luc.subjectid=@subjectid AND luc.course=@course\r\n        AND luc.section=@section AND luc.timeofday=@timeofday AND luc.campus=@campus\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO lucourses (term,duration,subjectid,course,section,timeofday,startdate,enddate,campus,department,location,externalid,instructorid,whoadded,credits)\r\n        VALUES (@term,@duration,@subjectid,@course,@section,@timeofday,@startdate,@enddate,@campus,@department,@location,@externalid,@primaryinstructorid,@whoami,@credits);\r\n    SELECT CAST(SCOPE_IDENTITY() AS int) AS lucourseid;\r\nEND", parameters);
			int num = (obj != DBNull.Value) ? ((int)obj) : 0;
			bool flag3 = num < 1;
			if (flag3)
			{
				throw new Exception("Couldn't create lookup course.");
			}
			IList<LookupCourse> list = this.LoadCoursesByIds(new List<int>
			{
				num
			});
			return (list != null && list.Count > 0) ? list[0] : null;
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x000244A0 File Offset: 0x000226A0
		public LookupCourse CreateLookupCourseFromExternalCourse(DataSyncExternalCourse ExternalCourse, int SubjectId)
		{
			LookupInstructorDAO lookupInstructorDAO = this.lookupInstructorDAO;
			int num = 0;
			bool flag = ExternalCourse.Instructors == null;
			if (flag)
			{
				ExternalCourse.Instructors = new List<DataSyncExternalCourseInstructor>();
			}
			DbParameter[] parameters;
			foreach (DataSyncExternalCourseInstructor dataSyncExternalCourseInstructor in ExternalCourse.Instructors)
			{
				bool flag2 = dataSyncExternalCourseInstructor.ClockWorkInstructor == null || dataSyncExternalCourseInstructor.ClockWorkInstructor.InstructorId < 1;
				if (flag2)
				{
					parameters = new DbParameter[]
					{
						this.DatabaseManager.GetParameter("@instructorname", DbType.String, dataSyncExternalCourseInstructor.Name ?? "Unknown"),
						this.DatabaseManager.GetParameter("@instructoremail", DbType.String, dataSyncExternalCourseInstructor.Email ?? ""),
						this.DatabaseManager.GetParameter("@instructorphone", DbType.String, dataSyncExternalCourseInstructor.Phone ?? ""),
						this.DatabaseManager.GetParameter("@instructorusername", DbType.String, dataSyncExternalCourseInstructor.Username ?? ""),
						this.DatabaseManager.GetParameter("@instructoremployeeid", DbType.String, dataSyncExternalCourseInstructor.EmployeeId ?? ""),
						this.DatabaseManager.GetParameter("@instructorexternalid", DbType.String, dataSyncExternalCourseInstructor.ExternalInstructorId ?? ""),
						this.DatabaseManager.GetParameter("@exemptfromdatasync", DbType.Boolean, false)
					};
					using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("IF LEN(COALESCE(@instructorexternalid,',')) < 1 AND LEN(COALESCE(@instructorusername,',')) < 1 AND LEN(COALESCE(@instructoremployeeid,',')) < 1 AND LEN(COALESCE(@instructoremail,',')) < 1\r\n\tSELECT 0 AS lucoursedataid\r\nELSE IF EXISTS(\r\nSELECT lucoursedataid FROM lucoursedata lucd \r\nWHERE lucd.lookuplisttype=1 AND \r\n    (\r\n        (LEN(COALESCE(@instructorexternalid,',')) > 0 AND lucd.ExternalId=@instructorexternalid)\r\n\t\t    OR\r\n\t\t(LEN(COALESCE(@instructorusername,',')) > 0 AND lucd.username=@instructorusername)\r\n\t\t    OR\r\n\t\t(LEN(COALESCE(@instructoremployeeid,',')) > 0 AND lucd.id=@instructoremployeeid)\r\n            OR\r\n        (LEN(COALESCE(@instructoremail,',')) > 0 AND lucd.username=@instructoremail)\r\n    )\r\n)\r\n    SELECT TOP 1 lucoursedataid FROM lucoursedata lucd \r\n    WHERE lucd.lookuplisttype=1 AND \r\n    (\r\n        (LEN(COALESCE(@instructorexternalid,',')) > 0 AND lucd.ExternalId=@instructorexternalid)\r\n\t    \tOR\r\n\t\t(LEN(COALESCE(@instructorusername,',')) > 0 AND lucd.username=@instructorusername)\r\n\t\t    OR\r\n\t\t(LEN(COALESCE(@instructoremployeeid,',')) > 0 AND lucd.id=@instructoremployeeid)\r\n            OR\r\n        (LEN(COALESCE(@instructoremail,',')) > 0 AND lucd.username=@instructoremail)\r\n    )\r\n\tORDER BY lucd.luCourseDataID DESC\r\nELSE\r\nBEGIN\r\n    INSERT INTO lucoursedata (lookuplisttype,lookupstring,altlookupstring,email,phone,username,id,externalid,exemptfromdatasync) \r\n    VALUES (1,@instructorname,@instructorname,@instructoremail,@instructorphone,@instructorusername,@instructoremployeeid,@instructorexternalid,@exemptfromdatasync);\r\n\r\n    SELECT CAST(SCOPE_IDENTITY() AS INT) AS lucoursedataid\r\nEND", parameters))
					{
						bool flag3 = dataReader != null && dataReader.Read();
						if (flag3)
						{
							int instructorId = (int)dataReader[0];
							dataSyncExternalCourseInstructor.ClockWorkInstructor = lookupInstructorDAO.LoadInstructor(instructorId);
						}
					}
				}
			}
			List<DataSyncExternalCourseInstructor> instructors = ExternalCourse.Instructors;
			bool flag4 = instructors.Count > 0;
			LookupInstructor lookupInstructor;
			if (flag4)
			{
				DataSyncExternalCourseInstructor dataSyncExternalCourseInstructor2 = instructors.Find((DataSyncExternalCourseInstructor pp) => pp.IsPrimary && pp.ClockWorkInstructor != null && pp.ClockWorkInstructor.InstructorId > 0);
				lookupInstructor = ((dataSyncExternalCourseInstructor2 != null) ? dataSyncExternalCourseInstructor2.ClockWorkInstructor : null);
				bool flag5 = lookupInstructor == null;
				if (flag5)
				{
					instructors.Sort((DataSyncExternalCourseInstructor p1, DataSyncExternalCourseInstructor p2) => p1.Percentage.CompareTo(p2.Percentage));
					for (int i = 0; i < instructors.Count; i++)
					{
						DataSyncExternalCourseInstructor dataSyncExternalCourseInstructor3 = instructors[i];
						bool flag6 = dataSyncExternalCourseInstructor3.ClockWorkInstructor != null && dataSyncExternalCourseInstructor3.ClockWorkInstructor.InstructorId > 0;
						if (flag6)
						{
							lookupInstructor = dataSyncExternalCourseInstructor3.ClockWorkInstructor;
							lookupInstructor.IsPrimary = true;
							break;
						}
					}
				}
			}
			else
			{
				lookupInstructor = null;
			}
			bool flag7 = lookupInstructor != null;
			DbParameter parameter;
			if (flag7)
			{
				parameter = this.DatabaseManager.GetParameter("@primaryinstructorid", DbType.Int32, lookupInstructor.InstructorId);
			}
			else
			{
				parameter = this.DatabaseManager.GetParameter("@primaryinstructorid", DbType.Int32, -1);
			}
			parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@startdate", DbType.DateTime, ExternalCourse.StartDate),
				this.DatabaseManager.GetParameter("@enddate", DbType.DateTime, ExternalCourse.EndDate),
				this.DatabaseManager.GetParameter("@duration", DbType.String, ExternalCourse.Duration ?? ""),
				this.DatabaseManager.GetParameter("@term", DbType.String, ExternalCourse.Term ?? ""),
				this.DatabaseManager.GetParameter("@subjectid", DbType.Int32, SubjectId),
				this.DatabaseManager.GetParameter("@course", DbType.String, ExternalCourse.Course ?? ""),
				this.DatabaseManager.GetParameter("@section", DbType.String, ExternalCourse.Section ?? ""),
				this.DatabaseManager.GetParameter("@timeofday", DbType.String, ExternalCourse.TimeOfDay ?? ""),
				this.DatabaseManager.GetParameter("@campus", DbType.String, ExternalCourse.Campus ?? ""),
				this.DatabaseManager.GetParameter("@department", DbType.String, ExternalCourse.Department ?? ""),
				parameter,
				this.DatabaseManager.GetParameter("@location", DbType.String, ExternalCourse.Location ?? ""),
				this.DatabaseManager.GetParameter("@externalid", DbType.String, ExternalCourse.ExternalCourseId ?? ""),
				this.DatabaseManager.GetParameter("@whoami", DbType.Int32, (this.OpContext == null) ? 0 : this.OpContext.WhoAmI),
				this.DatabaseManager.GetParameter("@credits", DbType.Decimal, ExternalCourse.Credits)
			};
			object obj = this.DatabaseManager.ExecuteScalar("IF EXISTS( SELECT lucourseid FROM lucourses luc WHERE NOT ( luc.enddate <= @startdate OR luc.startdate > @enddate)\r\n        AND luc.duration=@duration AND luc.term=@term AND luc.subjectid=@subjectid AND luc.course=@course\r\n        AND luc.section=@section AND luc.timeofday=@timeofday AND luc.campus=@campus\r\n)\r\nBEGIN\r\n    SELECT lucourseid FROM lucourses luc WHERE NOT ( luc.enddate <= @startdate OR luc.startdate > @enddate)\r\n        AND luc.duration=@duration AND luc.term=@term AND luc.subjectid=@subjectid AND luc.course=@course\r\n        AND luc.section=@section AND luc.timeofday=@timeofday AND luc.campus=@campus\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO lucourses (term,duration,subjectid,course,section,timeofday,startdate,enddate,campus,department,location,externalid,instructorid,whoadded,credits)\r\n        VALUES (@term,@duration,@subjectid,@course,@section,@timeofday,@startdate,@enddate,@campus,@department,@location,@externalid,@primaryinstructorid,@whoami,@credits);\r\n    SELECT CAST(SCOPE_IDENTITY() AS int) AS lucourseid;\r\nEND", parameters);
			bool flag8 = obj != DBNull.Value;
			if (flag8)
			{
				num = (int)obj;
			}
			bool flag9 = num < 1;
			LookupCourse result;
			if (flag9)
			{
				result = null;
			}
			else
			{
				foreach (DataSyncExternalCourseInstructor dataSyncExternalCourseInstructor4 in instructors.FindAll((DataSyncExternalCourseInstructor pp) => pp.ClockWorkInstructor != null && pp.ClockWorkInstructor.InstructorId > 0))
				{
					bool flag10 = lookupInstructor == null || dataSyncExternalCourseInstructor4.ClockWorkInstructor.InstructorId != lookupInstructor.InstructorId;
					if (flag10)
					{
						parameters = new DbParameter[]
						{
							this.DatabaseManager.GetParameter("@lucid", DbType.Int32, num),
							this.DatabaseManager.GetParameter("@instructorid", DbType.Int32, dataSyncExternalCourseInstructor4.ClockWorkInstructor.InstructorId)
						};
						this.DatabaseManager.ExecuteNonQuery("IF NOT EXISTS(SELECT lucourseid FROM lucourseinstructor WHERE lucourseid=@lucid AND instructorid=@instructorid)\r\n    INSERT INTO lucourseinstructor(lucourseid,instructorid) VALUES (@lucid,@instructorid)", parameters);
					}
				}
				LookupTimetableItemDAO lookupTimetableItemDAO = this.lookupTimetableItemDAO;
				lookupTimetableItemDAO.SaveLookupTimetableItems(num, ExternalCourse.TimetableItems.ConvertAll<LookupTimetableItem>((DataSyncExternalCourseTimetableItem tti) => new LookupTimetableItem
				{
					DayOfWeek = tti.DayOfWeek,
					EndTime = tti.EndTime,
					Room = tti.Room,
					StartTime = tti.StartTime,
					TimetableType = 'C'
				}));
				result = this.LoadCourse(num);
			}
			return result;
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x00024B6C File Offset: 0x00022D6C
		public void RemoveSecondaryInstructorFromCourse(int lucid, int iid)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@lucid", DbType.Int32, lucid),
				this.DatabaseManager.GetParameter("@iid", DbType.Int32, iid)
			};
			this.DatabaseManager.ExecuteNonQuery("DELETE FROM lucourseinstructor WHERE lucourseid=@lucid AND instructorid=@iid", parameters);
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x00024BCC File Offset: 0x00022DCC
		public void AddSecondaryInstructorToCourse(int lucid, int iid)
		{
			bool flag = iid > 0;
			if (flag)
			{
				DbParameter[] parameters = new DbParameter[]
				{
					this.DatabaseManager.GetParameter("@lucid", DbType.Int32, lucid),
					this.DatabaseManager.GetParameter("@iid", DbType.Int32, iid)
				};
				this.DatabaseManager.ExecuteNonQuery("IF NOT EXISTS(SELECT lucourseid FROM lucourseinstructor WHERE lucourseid=@lucid AND instructorid=@iid)\r\n    INSERT INTO lucourseinstructor (lucourseid,instructorid) VALUES (@lucid,@iid)", parameters);
			}
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x00024C34 File Offset: 0x00022E34
		public void SetPrimaryInstructor(int lucid, int iid)
		{
			IList<LookupInstructor> list = this.LoadCourseInstructors(lucid);
			LookupInstructor lookupInstructor = null;
			bool flag = list != null && list.Count > 0;
			if (flag)
			{
				lookupInstructor = list.FirstOrDefault((LookupInstructor g) => g.IsPrimary);
			}
			this.ReplacePrimaryInstructor(lucid, iid);
			bool flag2 = lookupInstructor != null && lookupInstructor.InstructorId > 0;
			if (flag2)
			{
				this.AddSecondaryInstructorToCourse(lucid, lookupInstructor.InstructorId);
			}
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x00024CAF File Offset: 0x00022EAF
		public void ClearPrimaryInstructor(int lucid)
		{
			this.ReplacePrimaryInstructor(lucid, 0);
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x00024CBC File Offset: 0x00022EBC
		public void ReplacePrimaryInstructor(int lucid, int iid)
		{
			bool flag = iid > 0;
			DbParameter parameter;
			if (flag)
			{
				parameter = this.DatabaseManager.GetParameter("@iid", DbType.Int32, iid);
			}
			else
			{
				parameter = this.DatabaseManager.GetParameter("@iid", DbType.Int32, -1);
			}
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@lucid", DbType.Int32, lucid),
				parameter
			};
			this.DatabaseManager.ExecuteNonQuery("UPDATE lucourses SET instructorid=@iid WHERE lucourseid=@lucid", parameters);
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x00024D40 File Offset: 0x00022F40
		public void UpdateCourseInstructorExemption(int LuCourseId, int InstructorId, bool NewIsInstructorExemptFromCourseList)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@lucid", DbType.Int32, LuCourseId),
				this.DatabaseManager.GetParameter("@iid", DbType.Int32, InstructorId),
				this.DatabaseManager.GetParameter("@isexempt", DbType.Boolean, NewIsInstructorExemptFromCourseList)
			};
			this.DatabaseManager.ExecuteNonQuery("UPDATE lucourses SET ExemptAssignmentFromDataSync=@isexempt WHERE lucourseid=@lucid AND instructorid=@iid;\r\nUPDATE lucourseinstructor SET ExemptAssignmentFromDataSync=@isexempt WHERE lucourseid=@lucid AND instructorid=@iid", parameters);
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x00024DB8 File Offset: 0x00022FB8
		public IList<LookupCourseBase> LoadCourseBasesBySearchString(DateTime StartDate, DateTime EndDate, string SearchString)
		{
			bool flag = string.IsNullOrEmpty(SearchString);
			IList<LookupCourseBase> result;
			if (flag)
			{
				result = new List<LookupCourseBase>();
			}
			else
			{
				string arg = SearchString.Replace(" ", "");
				DbParameter[] parameters = new DbParameter[]
				{
					this.DatabaseManager.GetParameter("@startdate", DbType.DateTime, StartDate),
					this.DatabaseManager.GetParameter("@enddate", DbType.DateTime, EndDate),
					this.DatabaseManager.GetParameter("@str", DbType.String, string.Format("%{0}%", arg))
				};
				using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT    DISTINCT luc.lucourseid,luc.startdate,luc.enddate,luc.duration,luc.term,luc.subjectid,luc.externalid,luc.exemptfromdatasync AS lucexemptfromdatasync,\r\n            lucd.lookupstring AS subjectcode,lucd.altlookupstring AS subjectdescription,\r\n            luc.course,luc.timeofday,luc.[section],\r\n            luc.campus,luc.department,luc.location,luc.credits,\r\n            lucd.altLookupString + luc.course + luc.Section,luc.coursenote\r\nFROM        lucourses luc LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid\r\nWHERE       NOT @str ='%%' AND\r\n\t\t\tNOT ( luc.enddate <= @startdate OR luc.startdate > @enddate)\r\n            AND\r\n            (\r\n                (lucd.altLookupString + luc.course + luc.Section) LIKE @str\r\n            )\r\nORDER BY    luc.startdate,luc.duration,luc.term,lucd.altlookupstring,luc.course,luc.[section],luc.timeofday,luc.lucourseid", parameters))
				{
					bool flag2 = dataReader != null;
					if (flag2)
					{
						return LookupCourseDAO.GetCoursesBaseFromReader("", dataReader);
					}
				}
				result = null;
			}
			return result;
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x00024EA0 File Offset: 0x000230A0
		public IDictionary<int, bool> LoadIsLookupCourseExemptFromDataSync(IList<int> LuCourseIds)
		{
			DbParameter[] array = new DbParameter[1];
			array[0] = this.DatabaseManager.GetParameter("@lucids", DbType.String, string.Join(",", LuCourseIds.ToList<int>().ConvertAll<string>((int g) => g.ToString()).ToArray()));
			DbParameter[] parameters = array;
			IDictionary<int, bool> result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT lucourseid,exemptfromdatasync FROM lucourses WHERE lucourseid IN (SELECT orderid AS lucourseid FROM splitorderids(@lucids,','))", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					Dictionary<int, bool> dictionary = new Dictionary<int, bool>();
					while (dataReader.Read())
					{
						int num = (dataReader[0] is DBNull) ? 0 : ((int)dataReader[0]);
						bool flag2 = num > 0;
						if (flag2)
						{
							bool value = !(dataReader[1] is DBNull) && Convert.ToBoolean(dataReader[1]);
							bool flag3 = !dictionary.ContainsKey(num);
							if (flag3)
							{
								dictionary.Add(num, value);
							}
						}
					}
					result = dictionary;
				}
			}
			return result;
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x00024FC8 File Offset: 0x000231C8
		public void UpdateLookupCourseExemptionFromDataSync(int LuCourseId, bool NewIsExempt)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@lucid", DbType.Int32, LuCourseId),
				this.DatabaseManager.GetParameter("@isexempt", DbType.Boolean, NewIsExempt)
			};
			this.DatabaseManager.ExecuteNonQuery("UPDATE lucourses SET exemptfromdatasync=@isexempt WHERE lucourseid=@lucid", parameters);
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x00025024 File Offset: 0x00023224
		public IList<LookupInstructor> LoadCourseInstructors(int LuCourseId)
		{
			ILookupInstructorDAO lookupInstructorDAO = new LookupInstructorDAO(this.OpContext);
			return lookupInstructorDAO.LoadInstructorsByCourse(LuCourseId);
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x0002504C File Offset: 0x0002324C
		public IList<LookupDurationTermSubject> LoadDurationTermSubjectsBySession(Session Session)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@startdate", DbType.DateTime, Session.StartDate),
				this.DatabaseManager.GetParameter("@enddate", DbType.DateTime, Session.EndDate)
			};
			IList<LookupDurationTermSubject> result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT DISTINCT luc.duration,luc.term,luc.subjectid,lucd.altlookupstring AS subjecttitle\r\nFROM  lucourses luc LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid\r\nWHERE NOT ( ( luc.enddate<@startdate ) OR (luc.startdate > @enddate ) )\r\nORDER BY luc.duration,luc.term,lucd.altlookupstring", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<LookupDurationTermSubject> list = new List<LookupDurationTermSubject>();
					while (dataReader.Read())
					{
						list.Add(new LookupDurationTermSubject
						{
							SubjectId = ((dataReader["subjectid"] is DBNull) ? 0 : ((int)dataReader["subjectid"])),
							SubjectTitle = ((dataReader["subjecttitle"] is DBNull) ? "" : ((string)dataReader["subjecttitle"])),
							Duration = ((dataReader["duration"] is DBNull) ? "" : ((string)dataReader["duration"])),
							Term = ((dataReader["term"] is DBNull) ? "" : ((string)dataReader["term"]))
						});
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x000251DC File Offset: 0x000233DC
		public void UpdateCourseNote(int lucid, string newCourseNote)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@lucid", DbType.Int32, lucid),
				this.DatabaseManager.GetParameter("@coursenote", DbType.String, newCourseNote ?? "")
			};
			this.DatabaseManager.ExecuteNonQuery("UPDATE lucourses SET coursenote=@coursenote WHERE lucourseid=@lucid", parameters);
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x00025240 File Offset: 0x00023440
		public IList<LookupCourseBase> LoadCourseBasesByIds(int[] LuCourseIds)
		{
			DbParameter[] array = new DbParameter[1];
			array[0] = this.DatabaseManager.GetParameter("@lucids", DbType.String, string.Join(",", (from g in LuCourseIds
			select g.ToString()).ToArray<string>()));
			DbParameter[] parameters = array;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT orderid AS lucourseid INTO #t1 FROM splitorderids(@lucids,',');\r\n\r\nSELECT    luc.lucourseid,luc.startdate,luc.enddate,luc.duration,luc.term,luc.subjectid,luc.externalid,luc.exemptfromdatasync AS lucexemptfromdatasync,\r\n            lucd.lookupstring AS subjectcode,lucd.altlookupstring AS subjectdescription,\r\n            luc.course,luc.timeofday,luc.[section],\r\n            luc.campus,luc.department,luc.location,luc.credits,\r\n            luc.coursenote\r\nFROM        lucourses luc LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid\r\nWHERE       luc.lucourseid IN (SELECT lucourseid FROM #t1)\r\nORDER BY    luc.startdate,luc.duration,luc.term,lucd.altlookupstring,luc.course,luc.[section],luc.timeofday,luc.lucourseid;\r\n\r\nDROP TABLE #t1", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					return LookupCourseDAO.GetCoursesBaseFromReader("", dataReader);
				}
			}
			return null;
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x000252F0 File Offset: 0x000234F0
		public void UpdateClockWorkCourseCredits(int lucid, decimal newCredits)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@lucid", DbType.Int32, lucid),
				databaseLayer.GetParameter("@credits", DbType.Decimal, newCredits)
			};
			databaseLayer.ExecuteNonQuery("UPDATE lucourses SET credits=@credits WHERE lucourseid=@lucid", parameters);
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x00025358 File Offset: 0x00023558
		public LookupCourse LoadLookupCourseByExamId(int ExamId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@examid", DbType.Int32, ExamId)
			};
			LookupCourse result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT e.examid,luc.lucourseid,luc.startdate,luc.enddate,luc.duration,luc.term,luc.subjectid,luc.externalid,luc.exemptfromdatasync AS lucexemptfromdatasync\r\n        ,lucd.lookupstring AS subjectcode,lucd.altlookupstring AS subjectdescription\r\n        ,luc.course,luc.timeofday,luc.[section]\r\n        ,luc.campus,luc.department,luc.location\r\n        ,luc.instructorid AS pinstructorid,lucd2.altlookupstring AS pinstructorname,lucd2.email AS pinstructoremail,lucd2.phone AS pinstructorphone,lucd2.username AS pinstructorusername,lucd2.exemptfromdatasync AS pexemptfromdatasync,lucd2.id AS pinstructoremployeeid,lucd2.externalid AS pinstructorexternalid\r\n        ,luc.ExemptAssignmentFromDataSync AS pExemptAssignmentFromDataSync\r\n        ,luc.BatchDataSyncLogId\r\n        ,lci.instructorid AS p3instructorid,lucd3.altlookupstring AS p3instructorname,lucd3.email AS p3instructoremail,lucd3.phone AS p3instructorphone,lucd3.username AS p3instructorusername,lucd3.exemptfromdatasync AS p3exemptfromdatasync,lucd3.id AS p3instructoremployeeid,lucd3.externalid AS p3instructorexternalid\r\n        ,lci.ExemptAssignmentFromDataSync AS p3ExemptAssignmentFromDataSync\r\n        ,tt.timetableid\r\n        ,tt.sunstartminutes,tt.sunendminutes,tt.monstartminutes,tt.monendminutes,tt.tuestartminutes,tt.tueendminutes\r\n        ,tt.wedstartminutes,tt.wedendminutes,tt.thustartminutes,tt.thuendminutes,tt.fristartminutes,tt.friendminutes\r\n        ,tt.satstartminutes,tt.satendminutes,tt.sunroom,tt.monroom,tt.tueroom,tt.wedroom,tt.thuroom,tt.friroom,tt.satroom,\r\n        luc.alternatecontactid,ac.altname,ac.altemail,ac.altphone,ac.altusername,ac.externalid,ac.altpermissionlevel,\r\n        lucac.alternatecontactid AS secondaryalternatecontactid,\r\n        ac2.altname AS secondaryaltname,ac2.altemail AS secondaryaltemail,ac2.altphone AS secondaryaltphone,\r\n        ac2.altusername AS secondaryaltusername,ac2.externalid AS secondaryexternalid,\r\n        ac2.altpermissionlevel AS secondaryaltpermissionlevel,\r\n        luc.coursenote\r\nFROM    exams e LEFT JOIN lucourses luc ON luc.lucourseid=e.lucourseid \r\n        LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid\r\n        LEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luc.instructorid\r\n        LEFT JOIN lucourseinstructor lci ON lci.lucourseid=luc.lucourseid\r\n        LEFT JOIN lucoursedata lucd3 ON lucd3.lucoursedataid=lci.instructorid\r\n        LEFT JOIN timetable tt ON tt.timetabletype='C' AND tt.lucourseid=luc.lucourseid\r\n        LEFT JOIN lucoursealternatecontact ac ON ac.alternatecontactid=luc.alternatecontactid\r\n        LEFT JOIN LuCourseAltContact lucac ON lucac.lucourseid=luc.lucourseid\r\n        LEFT JOIN lucoursealternatecontact ac2 ON ac2.alternatecontactid=lucac.alternatecontactid\r\nWHERE   e.examid=@examid\r\nORDER BY luc.startdate,luc.duration,luc.term,lucd.altlookupstring,luc.course,luc.[section],luc.timeofday,luc.lucourseid", parameters))
			{
				result = ((dataReader == null) ? null : (LookupCourseDAO.GetCoursesFromReader("", dataReader) ?? new List<LookupCourse>()).FirstOrDefault<LookupCourse>());
			}
			return result;
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x000253DC File Offset: 0x000235DC
		public IList<LookupCourseDateRange> LoadUniqueCourseDateRanges(DateTime startDate, DateTime endDate)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@startdate", DbType.DateTime, startDate.Date),
				this.DatabaseManager.GetParameter("@enddate", DbType.DateTime, endDate.Date)
			};
			IList<LookupCourseDateRange> result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("DECLARE @sd datetime = DATEADD(D, 0, DATEDIFF(D, 0, @startdate))\r\nDECLARE @ed datetime = DATEADD(D, 0, DATEDIFF(D, 0, @enddate))\r\n\r\nSELECT\tDISTINCT DATEADD(D, 0, DATEDIFF(D, 0, luc.startdate)) AS startdate,DATEADD(D, 0, DATEDIFF(D, 0, luc.enddate)) AS enddate,COUNT(lucourseid) AS CourseCount \r\nFROM\tlucourses luc \r\nWHERE\tNOT ( ( luc.enddate<@sd ) OR (luc.startdate > @ed ) )\r\nGROUP BY DATEADD(D, 0, DATEDIFF(D, 0, luc.startdate)),DATEADD(D, 0, DATEDIFF(D, 0, luc.enddate))\r\nORDER BY DATEADD(D, 0, DATEDIFF(D, 0, luc.startdate)),DATEADD(D, 0, DATEDIFF(D, 0, luc.enddate))", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<LookupCourseDateRange> list = new List<LookupCourseDateRange>();
					while (dataReader.Read())
					{
						LookupCourseDateRange courseDateRangeFromRecord = this.GetCourseDateRangeFromRecord(dataReader);
						bool flag2 = courseDateRangeFromRecord == null;
						if (!flag2)
						{
							list.Add(courseDateRangeFromRecord);
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x000254A8 File Offset: 0x000236A8
		private LookupCourseDateRange GetCourseDateRangeFromRecord(IDataRecord record)
		{
			return new LookupCourseDateRange
			{
				StartDate = (DateTime)record["startdate"],
				EndDate = (DateTime)record["enddate"],
				CourseCount = ((record["coursecount"] is DBNull) ? 0 : ((int)record["coursecount"]))
			};
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x0002551C File Offset: 0x0002371C
		public void UpdateCourseDateRange(DateTime oldStartDate, DateTime oldEndDate, DateTime newStartDate, DateTime newEndDate)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@oldstartdate", DbType.DateTime, oldStartDate.Date),
				this.DatabaseManager.GetParameter("@oldenddate", DbType.DateTime, oldEndDate.Date),
				this.DatabaseManager.GetParameter("@newstartdate", DbType.DateTime, newStartDate.Date),
				this.DatabaseManager.GetParameter("@newenddate", DbType.DateTime, newEndDate.Date)
			};
			this.DatabaseManager.ExecuteNonQuery("DECLARE @oldSd1 datetime = DATEADD(D, 0, DATEDIFF(D, 0, @oldstartdate))\r\nDECLARE @oldEd1 datetime = DATEADD(D, 0, DATEDIFF(D, 0, @oldenddate))\r\n\r\nDECLARE @oldSd2 datetime = DATEADD(D,1,@oldSd1)\r\nDECLARE @oldEd2 datetime = DATEADD(D,1,@oldEd1)\r\n\r\nUPDATE    lucourses SET startdate=@newstartdate,enddate=@newenddate\r\nWHERE     startdate>=@oldSd1 AND startdate < @oldSd2\r\n          AND enddate>=@oldEd1 AND enddate < @oldEd2", parameters);
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x000255C4 File Offset: 0x000237C4
		public IList<LookupCourseBase> LoadCoursesInDateRange(DateTime startDate, DateTime endDate)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@startdate", DbType.DateTime, startDate.Date),
				this.DatabaseManager.GetParameter("@enddate", DbType.DateTime, endDate.Date)
			};
			IList<LookupCourseBase> result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("DECLARE @sd datetime = DATEADD(D, 0, DATEDIFF(D, 0, @startdate))\r\nDECLARE @ed datetime = DATEADD(D, 0, DATEDIFF(D, 0, @enddate))\r\n\r\nSELECT    luc.lucourseid,luc.startdate,luc.enddate,luc.duration,luc.term,luc.subjectid,luc.externalid,luc.exemptfromdatasync AS lucexemptfromdatasync,\r\n            lucd.lookupstring AS subjectcode,lucd.altlookupstring AS subjectdescription,\r\n            luc.course,luc.timeofday,luc.[section],\r\n            luc.campus,luc.department,luc.location,luc.credits,\r\n            luc.coursenote\r\nFROM        lucourses luc LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid\r\nWHERE\tNOT ( ( luc.enddate<@sd ) OR (luc.startdate > @ed ) )\r\nORDER BY    luc.startdate,luc.duration,luc.term,lucd.altlookupstring,luc.course,luc.[section],luc.timeofday,luc.lucourseid", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<LookupCourseBase> coursesBaseFromReader = LookupCourseDAO.GetCoursesBaseFromReader("", dataReader);
					result = coursesBaseFromReader;
				}
			}
			return result;
		}

		// Token: 0x040001DF RID: 479
		private AlternateContactDAO acdao;

		// Token: 0x040001E0 RID: 480
		private LookupSubjectDAO sdao;

		// Token: 0x040001E1 RID: 481
		private LookupInstructorDAO lid;

		// Token: 0x040001E2 RID: 482
		private LookupTimetableItemDAO ltd;

		// Token: 0x040001E3 RID: 483
		private LookupSubjectDAO lsd;
	}
}
