using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.Common.DAO.AppointmentsTestBooking;
using TechnoPro.Common.DAO.Impl.LookupCourses;
using TechnoPro.Common.DAO.Impl.People;
using TechnoPro.Common.DataStructure;
using TechnoPro.Common.DataStructure.Adapters;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;

namespace TechnoPro.Common.DAO.Impl.AppointmentsTestBooking
{
	// Token: 0x0200014A RID: 330
	public class StudentClassTestInfoDAO : IStudentClassTestInfoDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000994 RID: 2452 RVA: 0x00063A24 File Offset: 0x00061C24
		// (set) Token: 0x06000995 RID: 2453 RVA: 0x00063A2C File Offset: 0x00061C2C
		public OperationContext OpContext { get; set; }

		// Token: 0x06000996 RID: 2454 RVA: 0x00063A35 File Offset: 0x00061C35
		public StudentClassTestInfoDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext2 = this.OpContext;
			this.DatabaseManager = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext2 != null) ? opContext2.TenantId : null);
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x00063A68 File Offset: 0x00061C68
		internal static T GetStudentClassTestBaseFromRecord<T>(IDataReader record, string coursePrefix = "", IBatchDecryptor batchDecryptor = null) where T : StudentClassTestBase
		{
			bool flag = record == null;
			T result;
			if (flag)
			{
				result = default(T);
			}
			else
			{
				int num = (record["appointmentcourseid"] is DBNull) ? 0 : ((int)record["appointmentcourseid"]);
				bool flag2 = num < 1;
				if (flag2)
				{
					result = default(T);
				}
				else
				{
					T t = (T)((object)Activator.CreateInstance(typeof(T)));
					t.AppointmentCourseId = num;
					t.Course = LookupCourseDAO.GetCourseBaseFromReader(string.IsNullOrEmpty(coursePrefix) ? "s" : coursePrefix, record);
					result = t;
				}
			}
			return result;
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x00063B18 File Offset: 0x00061D18
		internal static StudentClassTest GetStudentClassTestFromRecord(IDataReader record, OperationContext opContext, string coursePrefix = "", IBatchDecryptor batchDecryptor = null)
		{
			bool flag = record == null;
			StudentClassTest result;
			if (flag)
			{
				result = null;
			}
			else
			{
				StudentClassTest studentClassTestBaseFromRecord = StudentClassTestInfoDAO.GetStudentClassTestBaseFromRecord<StudentClassTest>(record, coursePrefix, batchDecryptor);
				bool flag2 = studentClassTestBaseFromRecord == null;
				if (flag2)
				{
					result = null;
				}
				else
				{
					StudentClassTestInfoDAO.AddExtendedClassTestInfo(record, ref studentClassTestBaseFromRecord, opContext, batchDecryptor);
					result = studentClassTestBaseFromRecord;
				}
			}
			return result;
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x00063B58 File Offset: 0x00061D58
		internal static void AddExtendedClassTestInfo(IDataReader record, ref StudentClassTest classTest, OperationContext opContext, IBatchDecryptor batchDecryptor = null)
		{
			bool flag = classTest == null;
			if (!flag)
			{
				DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, (opContext != null) ? opContext.TenantId : null);
				IEncryption encryption = databaseLayer.Encryption;
				classTest.ExtendedProperties = (PeopleDAO.ReaderContainsColumn(record, "sextendedproperties") ? ((record["sextendedproperties"] is DBNull) ? "" : ((string)record["sextendedproperties"])) : "");
				classTest.PrivateNote = (PeopleDAO.ReaderContainsColumn(record, "privatenote2") ? ((record["privatenote2"] is DBNull) ? "" : ((string)record["privatenote2"])) : "");
				classTest.StudentReportedClassStartDateTime = (PeopleDAO.ReaderContainsColumn(record, "originalstartdatetime") ? ((record["originalstartdatetime"] is DBNull) ? null : new DateTime?((DateTime)record["originalstartdatetime"])) : null);
				classTest.StudentReportedClassEndDateTime = (PeopleDAO.ReaderContainsColumn(record, "originalenddatetime") ? ((record["originalenddatetime"] is DBNull) ? null : new DateTime?((DateTime)record["originalenddatetime"])) : null);
				bool flag2 = PeopleDAO.ReaderContainsColumn(record, "testnote");
				if (flag2)
				{
					byte[] array = (record["testnote"] is DBNull) ? null : ((byte[])record["testnote"]);
					classTest.TestNote = ((array == null) ? "" : ((batchDecryptor == null) ? encryption.Decrypt(array) : batchDecryptor.Decrypt(array)));
				}
				else
				{
					classTest.TestNote = "";
				}
				bool flag3 = PeopleDAO.ReaderContainsColumn(record, "studentnote");
				if (flag3)
				{
					byte[] array2 = (record["studentnote"] is DBNull) ? null : ((byte[])record["studentnote"]);
					classTest.BookingNote = ((array2 == null) ? "" : ((batchDecryptor == null) ? encryption.Decrypt(array2) : batchDecryptor.Decrypt(array2)));
				}
				else
				{
					classTest.BookingNote = "";
				}
			}
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x00063DA0 File Offset: 0x00061FA0
		public void DeleteStudentClassTestInfo(int AppointmentCourseId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@appointmentcourseid", DbType.Int32, AppointmentCourseId)
			};
			this.DatabaseManager.ExecuteNonQuery("DELETE FROM appointmentcourses WHERE appointmentcourseid=@appointmentcourseid", parameters);
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x00063DE4 File Offset: 0x00061FE4
		public void UpdateExamStatus(int AppointmentId, int NewExamStatusLookupId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@appid", DbType.Int32, AppointmentId),
				this.DatabaseManager.GetParameter("@examstatuslookupid", DbType.Int32, (NewExamStatusLookupId < 1) ? DBNull.Value : NewExamStatusLookupId)
			};
			this.DatabaseManager.ExecuteNonQuery("UPDATE appointmentcourses SET examstatuslookupid=@examstatuslookupid WHERE appointmentid=@appid", parameters);
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x00063E4C File Offset: 0x0006204C
		public StudentClassTest LoadClassTestByAppointmentId(int AppointmentId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@appid", DbType.Int32, AppointmentId)
			};
			StudentClassTest result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT ac.appointmentid,\r\n        ac.appointmentcourseid,ac.lucourseid AS slucourseid,ac.originalstartdatetime,ac.originalenddatetime,ac.testnote,ac.studentnote,ac.instructoracknowledgevalue,ac.instructoracknowledgedate,\r\n        ac.privatenote2,ac.testpickedupdate AS stestpickedupdate,ac.testpickedupnote AS stestpickedupnote,ac.extendedproperties AS sextendedproperties,\r\n        luc.startdate AS sstartdate,luc.enddate AS senddate,luc.duration AS sduration,luc.term AS sterm,luc.subjectid AS ssubjectid,lucd.altlookupstring AS ssubject,\r\n        luc.course AS scourse,luc.[section] AS ssection,luc.timeofday AS stimeofday\r\nFROM\tappointmentcourses ac LEFT JOIN lucourses luc ON luc.lucourseid=ac.lucourseid\r\n        LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid\r\nWHERE ac.appointmentid=@appid", parameters))
			{
				bool flag = dataReader == null || !dataReader.Read();
				if (flag)
				{
					result = null;
				}
				else
				{
					result = StudentClassTestInfoDAO.GetStudentClassTestFromRecord(dataReader, this.OpContext, "s", null);
				}
			}
			return result;
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x00063ED8 File Offset: 0x000620D8
		public IDictionary<int, StudentClassTest> LoadClassTestsByAppointmentIds(params int[] appointmentIds)
		{
			List<int> list = (appointmentIds ?? new int[0]).ToList<int>();
			IList<Chunk> list2 = list.BreakdownItemsIntoChunks(500);
			Dictionary<int, StudentClassTest> result = new Dictionary<int, StudentClassTest>();
			foreach (Chunk appIdChunk in list2)
			{
				this.LoadClassTestsByAppointmentIdChunks(ref result, appIdChunk, list);
			}
			return result;
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x00063F54 File Offset: 0x00062154
		private void LoadClassTestsByAppointmentIdChunks(ref Dictionary<int, StudentClassTest> fullClassTestList, Chunk appIdChunk, List<int> allAppIds)
		{
			DbParameter[] array = new DbParameter[1];
			array[0] = this.DatabaseManager.GetParameter("@appids", DbType.String, string.Join(",", (from g in appIdChunk.GetChunkRange(allAppIds)
			select g.ToString()).ToArray<string>()));
			DbParameter[] parameters = array;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT ac.appointmentid,\r\n        ac.appointmentcourseid,ac.lucourseid AS slucourseid,ac.originalstartdatetime,ac.originalenddatetime,ac.testnote,ac.studentnote,ac.instructoracknowledgevalue,ac.instructoracknowledgedate,\r\n        ac.privatenote2,ac.testpickedupdate AS stestpickedupdate,ac.testpickedupnote AS stestpickedupnote,ac.extendedproperties AS sextendedproperties,\r\n        luc.startdate AS sstartdate,luc.enddate AS senddate,luc.duration AS sduration,luc.term AS sterm,luc.subjectid AS ssubjectid,lucd.altlookupstring AS ssubject,\r\n        luc.course AS scourse,luc.[section] AS ssection,luc.timeofday AS stimeofday\r\nFROM\tappointmentcourses ac LEFT JOIN lucourses luc ON luc.lucourseid=ac.lucourseid\r\n        LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid\r\nWHERE ac.appointmentid IN (SELECT orderid AS appointmentid FROM splitorderids(@appids,','))", parameters))
			{
				bool flag = dataReader == null;
				if (!flag)
				{
					while (dataReader.Read())
					{
						int num = (dataReader["appointmentid"] is DBNull) ? 0 : ((int)dataReader["appointmentid"]);
						bool flag2 = num < 1;
						if (!flag2)
						{
							StudentClassTest studentClassTestFromRecord = StudentClassTestInfoDAO.GetStudentClassTestFromRecord(dataReader, this.OpContext, "s", null);
							bool flag3 = studentClassTestFromRecord == null || fullClassTestList.ContainsKey(num);
							if (!flag3)
							{
								fullClassTestList.Add(num, studentClassTestFromRecord);
							}
						}
					}
				}
			}
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x00064070 File Offset: 0x00062270
		public ExamStatus LoadExamStatusByAppointmentId(int AppointmentId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@appid", DbType.Int32, AppointmentId)
			};
			ExamStatus result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT ac.appointmentid,ac.examstatuslookupid,el.title,el.colourargb,el.hidefromstudent\r\nFROM appointmentcourses ac LEFT JOIN examstatuslookup el ON el.examstatuslookupid=ac.examstatuslookupid\r\nWHERE NOT ac.examstatuslookupid IS NULL AND ac.appointmentid=@appid", parameters))
			{
				bool flag = dataReader == null || !dataReader.Read();
				if (flag)
				{
					result = null;
				}
				else
				{
					result = this.GetExamStatusFromRecord(dataReader);
				}
			}
			return result;
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x000640F0 File Offset: 0x000622F0
		private ExamStatus GetExamStatusFromRecord(IDataReader record)
		{
			bool flag = record == null || record["examstatuslookupid"] is DBNull;
			ExamStatus result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = new ExamStatus
				{
					ExamStatusLookupId = (int)record["examstatuslookupid"],
					ColourArgB = ((record["colourargb"] is DBNull) ? 0 : ((int)record["colourargb"])),
					Title = record["title"].ToString()
				};
			}
			return result;
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x00064184 File Offset: 0x00062384
		public void UpdateBookingNote(int AppointmentId, string BookingNote)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@appid", DbType.Int32, AppointmentId),
				databaseLayer.GetParameter("@note", DbType.String, BookingNote ?? "")
			};
			databaseLayer.ExecuteNonQuery("UPDATE appointmentcourses SET studentnote=@note WHERE appointmentid=@appid", parameters);
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x000641F0 File Offset: 0x000623F0
		public void UpdatePrivateNote(int AppointmentId, string PrivateNote)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@appid", DbType.Int32, AppointmentId),
				databaseLayer.GetParameter("@note", DbType.String, PrivateNote ?? "")
			};
			databaseLayer.ExecuteNonQuery("UPDATE appointmentcourses SET privatenote2=@note WHERE appointmentid=@appid", parameters);
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x0006425C File Offset: 0x0006245C
		public void UpdateStudentReportedClassDateAndTimeToMatchExamClassDateAndTime(int AppointmentId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@appid", DbType.Int32, AppointmentId)
			};
			databaseLayer.ExecuteNonQuery("SELECT e.dateoftest AS startdatetime,dateadd(minute,e.testduration,e.dateoftest) AS enddatetime INTO #t1 FROM appointments a LEFT JOIN exams e ON e.examid=a.examid WHERE a.appointmentid=@appid;\r\nDECLARE @sdt datetime, @edt datetime;\r\nSET @sdt=(SELECT TOP 1 startdatetime FROM #t1);\r\nSET @edt=(SELECT TOP 1 enddatetime FROM #t1);\r\n\r\nIF NOT @sdt IS NULL AND NOT @edt IS NULL\r\n    UPDATE appointmentcourses SET originalstartdatetime=@sdt,originalenddatetime=@edt WHERE appointmentid=@appid;\r\n\r\nDROP TABLE #t1", parameters);
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x00003998 File Offset: 0x00001B98
		public int CreateStudentClassTest(int AppointmentId, StudentClassTest StudentClassTest)
		{
			throw new NotImplementedException();
		}

		// Token: 0x040005AE RID: 1454
		private DatabaseLayer DatabaseManager;
	}
}
