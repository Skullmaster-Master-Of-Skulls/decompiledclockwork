using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Core.Settings;
using TechnoPro.Common.ICore.Settings;
using TechnoPro.Common.Public.Entities.Settings;

namespace TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.LookupCourses
{
	// Token: 0x0200001B RID: 27
	public static class InstructorClientHelper
	{
		// Token: 0x06000096 RID: 150 RVA: 0x0000626C File Offset: 0x0000446C
		public static IList<StudentCourseLetterInfo> GetStudentsCoursesLettersAreAllowedForByInstructorAndDateRange(int iid, int altContactId, DateTime startDate, DateTime endDate)
		{
			ISettingManager settingManager = InstructorClientHelper.GetSettingManager();
			bool settingValue = settingManager.GetSettingValue<bool>(Setting.INSTRUCTOR_LettersEnabled);
			bool flag = !settingValue;
			IList<StudentCourseLetterInfo> result;
			if (flag)
			{
				result = new List<StudentCourseLetterInfo>();
			}
			else
			{
				int settingValue2 = settingManager.GetSettingValue<int>(Setting.TESTBOOKING_AccommodationsExpiryDateCid);
				bool settingValue3 = settingManager.GetSettingValue<bool>(Setting.TESTBOOKING_AccommodationsTreatEmptyExpiryDateAsExpired);
				bool settingValue4 = settingManager.GetSettingValue<bool>(Setting.INSTRUCTOR_ShowStudentAccommodationLettersForStudentsWhereTheLetterWasGenerated);
				bool settingValue5 = settingManager.GetSettingValue<bool>(Setting.INSTRUCTOR_ShowStudentAccommodationLettersForAnyStudentWithActiveAccommodationExpiryDateAndSelfRegApproved);
				int settingValue6 = settingManager.GetSettingValue<int>(Setting.INSTRUCTOR_DontShowStudentAccommodationCid);
				bool settingValue7 = settingManager.GetSettingValue<bool>(Setting.INSTRUCTOR_ReverseDontShowStudentAccommodationCid);
				bool settingValue8 = settingManager.GetSettingValue<bool>(Setting.INSTRUCTOR_ShowStudentAccommodationLettersForAnyStudentWithActiveAccommodationExpiryDate);
				DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork);
				DbParameter[] parameters = new DbParameter[]
				{
					databaseLayer.GetParameter("@iid", DbType.Int32, iid),
					databaseLayer.GetParameter("@altcontactid", DbType.Int32, altContactId),
					databaseLayer.GetParameter("@startdate", DbType.DateTime, startDate),
					databaseLayer.GetParameter("@enddate", DbType.DateTime, endDate),
					databaseLayer.GetParameter("@accexpirycid", DbType.Int32, settingValue2),
					databaseLayer.GetParameter("@emptyexpirymeansexpired", DbType.Boolean, settingValue3),
					databaseLayer.GetParameter("@showiflettergenerated", DbType.Boolean, settingValue4),
					databaseLayer.GetParameter("@showifrequestapprovedandaccommsnotexpired", DbType.Boolean, settingValue5),
					databaseLayer.GetParameter("@noInstructorViewCid", DbType.Int32, settingValue6),
					databaseLayer.GetParameter("@reverseNoInstructorViewCidLogic", DbType.Boolean, settingValue7),
					databaseLayer.GetParameter("@showifaccommsnotexpired", DbType.Boolean, settingValue8)
				};
				result = InstructorClientHelper.LoadInfo(databaseLayer, parameters);
			}
			return result;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00006418 File Offset: 0x00004618
		private static IList<StudentCourseLetterInfo> LoadInfo(DatabaseLayer db, DbParameter[] parameters)
		{
			IList<StudentCourseLetterInfo> result;
			using (IDataReader dataReader = db.ExecuteStoredProcedureReader("sp_Instructor_GetStudentsCoursesLettersAllowed", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<StudentCourseLetterInfo> list = new List<StudentCourseLetterInfo>();
					IBatchDecryptor batchDecryptor = db.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						StudentCourseLetterInfo studentCourseLetterInfoFromRecord = InstructorClientHelper.GetStudentCourseLetterInfoFromRecord(dataReader, batchDecryptor);
						bool flag2 = studentCourseLetterInfoFromRecord == null;
						if (!flag2)
						{
							list.Add(studentCourseLetterInfoFromRecord);
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x000064A4 File Offset: 0x000046A4
		private static StudentCourseLetterInfo GetStudentCourseLetterInfoFromRecord(IDataReader record, IBatchDecryptor batchDecryptor)
		{
			int num = (record["personid"] is DBNull) ? 0 : ((int)record["personid"]);
			bool flag = num < 1;
			StudentCourseLetterInfo result;
			if (flag)
			{
				result = null;
			}
			else
			{
				BasicPersonDTO student = (num < 1) ? null : new BasicPersonDTO
				{
					PersonId = num,
					FirstName = ((record["firstname"] is DBNull) ? "" : batchDecryptor.Decrypt((byte[])record["firstname"])),
					MiddleName = ((record["middlename"] is DBNull) ? "" : batchDecryptor.Decrypt((byte[])record["middlename"])),
					LastName = ((record["lastname"] is DBNull) ? "" : batchDecryptor.Decrypt((byte[])record["lastname"])),
					StudentNumber = ((record["student_no"] is DBNull) ? "" : batchDecryptor.Decrypt((byte[])record["student_no"]))
				};
				int num2 = (record["lucourseid"] is DBNull) ? 0 : ((int)record["lucourseid"]);
				bool flag2 = num2 < 1;
				if (flag2)
				{
					result = null;
				}
				else
				{
					LookupCourseBaseDTO courseBase = (num2 < 1) ? null : new LookupCourseBaseDTO
					{
						LuCourseId = num2,
						Subject = new LookupSubjectDTO
						{
							SubjectId = ((record["subjectid"] is DBNull) ? 0 : ((int)record["subjectid"])),
							SubjectDescription = record["subject"].ToString().Trim()
						},
						Course = record["course"].ToString().Trim(),
						Section = record["section"].ToString().Trim(),
						TimeOfDay = record["timeofday"].ToString().Trim(),
						Term = record["term"].ToString().Trim(),
						Duration = record["duration"].ToString().Trim(),
						Campus = record["campus"].ToString().Trim(),
						StartDate = ((record["startdate"] is DBNull) ? DateTime.MinValue : ((DateTime)record["startdate"])),
						EndDate = ((record["enddate"] is DBNull) ? DateTime.MinValue : ((DateTime)record["enddate"]))
					};
					result = new StudentCourseLetterInfo
					{
						Student = student,
						CourseBase = courseBase,
						DateLetterIssued = ((record["dateletterissued"] is DBNull) ? null : new DateTime?((DateTime)record["dateletterissued"])),
						DateLetterReturned = ((record["dateletterreturned"] is DBNull) ? null : new DateTime?((DateTime)record["dateletterreturned"])),
						DateApproved = ((record["DateApproved"] is DBNull) ? null : new DateTime?((DateTime)record["DateApproved"]))
					};
				}
			}
			return result;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00006878 File Offset: 0x00004A78
		public static IList<StudentCourseLetterInfo> GetStudentsCoursesLettersAreAllowedForInstructorByStudentAndDateRange(int iid, int altContactId, int pid, DateTime startDate, DateTime endDate)
		{
			ISettingManager settingManager = InstructorClientHelper.GetSettingManager();
			bool settingValue = settingManager.GetSettingValue<bool>(Setting.INSTRUCTOR_LettersEnabled);
			bool flag = !settingValue;
			IList<StudentCourseLetterInfo> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				int settingValue2 = settingManager.GetSettingValue<int>(Setting.TESTBOOKING_AccommodationsExpiryDateCid);
				bool settingValue3 = settingManager.GetSettingValue<bool>(Setting.TESTBOOKING_AccommodationsTreatEmptyExpiryDateAsExpired);
				bool settingValue4 = settingManager.GetSettingValue<bool>(Setting.INSTRUCTOR_ShowStudentAccommodationLettersForStudentsWhereTheLetterWasGenerated);
				bool settingValue5 = settingManager.GetSettingValue<bool>(Setting.INSTRUCTOR_ShowStudentAccommodationLettersForAnyStudentWithActiveAccommodationExpiryDateAndSelfRegApproved);
				int settingValue6 = settingManager.GetSettingValue<int>(Setting.INSTRUCTOR_DontShowStudentAccommodationCid);
				bool settingValue7 = settingManager.GetSettingValue<bool>(Setting.INSTRUCTOR_ReverseDontShowStudentAccommodationCid);
				bool settingValue8 = settingManager.GetSettingValue<bool>(Setting.INSTRUCTOR_ShowStudentAccommodationLettersForAnyStudentWithActiveAccommodationExpiryDate);
				DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork);
				DbParameter[] parameters = new DbParameter[]
				{
					databaseLayer.GetParameter("@iid", DbType.Int32, iid),
					databaseLayer.GetParameter("@altcontactid", DbType.Int32, altContactId),
					databaseLayer.GetParameter("@pid", DbType.Int32, pid),
					databaseLayer.GetParameter("@usePid", DbType.Boolean, true),
					databaseLayer.GetParameter("@startdate", DbType.DateTime, startDate),
					databaseLayer.GetParameter("@enddate", DbType.DateTime, endDate),
					databaseLayer.GetParameter("@accexpirycid", DbType.Int32, settingValue2),
					databaseLayer.GetParameter("@emptyexpirymeansexpired", DbType.Boolean, settingValue3),
					databaseLayer.GetParameter("@showiflettergenerated", DbType.Boolean, settingValue4),
					databaseLayer.GetParameter("@showifrequestapprovedandaccommsnotexpired", DbType.Boolean, settingValue5),
					databaseLayer.GetParameter("@noInstructorViewCid", DbType.Int32, settingValue6),
					databaseLayer.GetParameter("@reverseNoInstructorViewCidLogic", DbType.Boolean, settingValue7),
					databaseLayer.GetParameter("@showifaccommsnotexpired", DbType.Boolean, settingValue8)
				};
				result = InstructorClientHelper.LoadInfo(databaseLayer, parameters);
			}
			return result;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00006A50 File Offset: 0x00004C50
		public static StudentCourseLetterInfo GetStudentsCoursesLettersAreAllowedForInstructorByStudentAndCourse(int iid, int altContactId, int pid, int lucid)
		{
			ISettingManager settingManager = InstructorClientHelper.GetSettingManager();
			bool settingValue = settingManager.GetSettingValue<bool>(Setting.INSTRUCTOR_LettersEnabled);
			bool flag = !settingValue;
			StudentCourseLetterInfo result;
			if (flag)
			{
				result = null;
			}
			else
			{
				int settingValue2 = settingManager.GetSettingValue<int>(Setting.TESTBOOKING_AccommodationsExpiryDateCid);
				bool settingValue3 = settingManager.GetSettingValue<bool>(Setting.TESTBOOKING_AccommodationsTreatEmptyExpiryDateAsExpired);
				bool settingValue4 = settingManager.GetSettingValue<bool>(Setting.INSTRUCTOR_ShowStudentAccommodationLettersForStudentsWhereTheLetterWasGenerated);
				bool settingValue5 = settingManager.GetSettingValue<bool>(Setting.INSTRUCTOR_ShowStudentAccommodationLettersForAnyStudentWithActiveAccommodationExpiryDateAndSelfRegApproved);
				int settingValue6 = settingManager.GetSettingValue<int>(Setting.INSTRUCTOR_DontShowStudentAccommodationCid);
				bool settingValue7 = settingManager.GetSettingValue<bool>(Setting.INSTRUCTOR_ReverseDontShowStudentAccommodationCid);
				bool settingValue8 = settingManager.GetSettingValue<bool>(Setting.INSTRUCTOR_ShowStudentAccommodationLettersForAnyStudentWithActiveAccommodationExpiryDate);
				DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork);
				DbParameter[] parameters = new DbParameter[]
				{
					databaseLayer.GetParameter("@iid", DbType.Int32, iid),
					databaseLayer.GetParameter("@altcontactid", DbType.Int32, altContactId),
					databaseLayer.GetParameter("@pid", DbType.Int32, pid),
					databaseLayer.GetParameter("@lucid", DbType.Int32, lucid),
					databaseLayer.GetParameter("@usePid", DbType.Boolean, true),
					databaseLayer.GetParameter("@useLucid", DbType.Boolean, true),
					databaseLayer.GetParameter("@accexpirycid", DbType.Int32, settingValue2),
					databaseLayer.GetParameter("@emptyexpirymeansexpired", DbType.Boolean, settingValue3),
					databaseLayer.GetParameter("@showiflettergenerated", DbType.Boolean, settingValue4),
					databaseLayer.GetParameter("@showifrequestapprovedandaccommsnotexpired", DbType.Boolean, settingValue5),
					databaseLayer.GetParameter("@noInstructorViewCid", DbType.Int32, settingValue6),
					databaseLayer.GetParameter("@reverseNoInstructorViewCidLogic", DbType.Boolean, settingValue7),
					databaseLayer.GetParameter("@showifaccommsnotexpired", DbType.Boolean, settingValue8)
				};
				IList<StudentCourseLetterInfo> list = InstructorClientHelper.LoadInfo(databaseLayer, parameters);
				result = ((list != null) ? list.FirstOrDefault<StudentCourseLetterInfo>() : null);
			}
			return result;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00006C34 File Offset: 0x00004E34
		public static IList<StudentCourseLetterInfo> GetStudentsCoursesLettersAreAllowedForInstructorByInstructorAndCourse(int iid, int altContactId, int lucid)
		{
			ISettingManager settingManager = InstructorClientHelper.GetSettingManager();
			bool settingValue = settingManager.GetSettingValue<bool>(Setting.INSTRUCTOR_LettersEnabled);
			bool flag = !settingValue;
			IList<StudentCourseLetterInfo> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				int settingValue2 = settingManager.GetSettingValue<int>(Setting.TESTBOOKING_AccommodationsExpiryDateCid);
				bool settingValue3 = settingManager.GetSettingValue<bool>(Setting.TESTBOOKING_AccommodationsTreatEmptyExpiryDateAsExpired);
				bool settingValue4 = settingManager.GetSettingValue<bool>(Setting.INSTRUCTOR_ShowStudentAccommodationLettersForStudentsWhereTheLetterWasGenerated);
				bool settingValue5 = settingManager.GetSettingValue<bool>(Setting.INSTRUCTOR_ShowStudentAccommodationLettersForAnyStudentWithActiveAccommodationExpiryDateAndSelfRegApproved);
				int settingValue6 = settingManager.GetSettingValue<int>(Setting.INSTRUCTOR_DontShowStudentAccommodationCid);
				bool settingValue7 = settingManager.GetSettingValue<bool>(Setting.INSTRUCTOR_ReverseDontShowStudentAccommodationCid);
				bool settingValue8 = settingManager.GetSettingValue<bool>(Setting.INSTRUCTOR_ShowStudentAccommodationLettersForAnyStudentWithActiveAccommodationExpiryDate);
				DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork);
				DbParameter[] parameters = new DbParameter[]
				{
					databaseLayer.GetParameter("@iid", DbType.Int32, iid),
					databaseLayer.GetParameter("@altcontactid", DbType.Int32, altContactId),
					databaseLayer.GetParameter("@lucid", DbType.Int32, lucid),
					databaseLayer.GetParameter("@useLucid", DbType.Boolean, true),
					databaseLayer.GetParameter("@accexpirycid", DbType.Int32, settingValue2),
					databaseLayer.GetParameter("@emptyexpirymeansexpired", DbType.Boolean, settingValue3),
					databaseLayer.GetParameter("@showiflettergenerated", DbType.Boolean, settingValue4),
					databaseLayer.GetParameter("@showifrequestapprovedandaccommsnotexpired", DbType.Boolean, settingValue5),
					databaseLayer.GetParameter("@noInstructorViewCid", DbType.Int32, settingValue6),
					databaseLayer.GetParameter("@reverseNoInstructorViewCidLogic", DbType.Boolean, settingValue7),
					databaseLayer.GetParameter("@showifaccommsnotexpired", DbType.Boolean, settingValue8)
				};
				result = InstructorClientHelper.LoadInfo(databaseLayer, parameters);
			}
			return result;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00006DE0 File Offset: 0x00004FE0
		private static ISettingManager GetSettingManager()
		{
			return SettingManager.CurrentInstance;
		}
	}
}
