using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.Common.DAO.Impl.Adapters;
using TechnoPro.Common.DAO.LookupCourses.Management;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.LookupCourses;
using TechnoPro.Common.Public.Entities.LookupCourses.Management;

namespace TechnoPro.Common.DAO.Impl.LookupCourses.Management
{
	// Token: 0x020000A2 RID: 162
	public class LookupInstructorManagementDAO : ILookupInstructorManagementDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600046F RID: 1135 RVA: 0x0002876C File Offset: 0x0002696C
		// (set) Token: 0x06000470 RID: 1136 RVA: 0x00028774 File Offset: 0x00026974
		public OperationContext OpContext { get; set; }

		// Token: 0x06000471 RID: 1137 RVA: 0x00028780 File Offset: 0x00026980
		public IList<LookupInstructorForManagement> LoadAllLookupInstructorsForManagement()
		{
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, this.OpContext.TenantId);
			IList<LookupInstructorForManagement> result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT\tlucd.luCourseDataID AS instructorid,\r\n        lucd.altLookupString AS instructorname,lucd.email AS instructoremail,\r\n        lucd.ExternalId AS instructorexternalid,lucd.id AS instructoremployeeid,\r\n\t\tlucd.lookupString,lucd.passwordhash,lucd.PermissionLevel,lucd.phone AS instructorphone,lucd.username AS instructorusername,\r\n        lucd2.altlookupstring AS subject,luc.course,luc.[section],luc.timeofday,luc.campus,luc.duration,luc.term,\r\n        luc.startdate,luc.enddate,luc.lucourseid,\r\n        c.registrationstatus,c.personid,\r\n        p.firstname,p.lastname,p.middlename,p.student_no,\r\n        vil.InstructorExemptAssignmentFromDataSync,lucd.exemptfromdatasync\r\nFROM\tLuCourseData lucd LEFT JOIN vInstructorList vil ON vil.instructorid=lucd.luCourseDataID\r\n\t\tLEFT JOIN LUCourses luc ON luc.LUCourseID=vil.lucourseid\r\n        LEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luc.subjectid\r\n\t\tLEFT JOIN Courses c ON c.luCourseID=luc.LUCourseID \r\n\t\tLEFT JOIN People p ON p.PersonID=c.personID\r\nWHERE\tlucd.lookupListType=1\r\nORDER BY lucd.altLookupString,lucd.username,lucd.externalid,lucd.luCourseDataID"))
			{
				bool flag = dataReader == null || !dataReader.Read();
				if (flag)
				{
					result = null;
				}
				else
				{
					List<LookupInstructorForManagement> list = new List<LookupInstructorForManagement>();
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					bool flag4;
					do
					{
						bool flag2;
						LookupInstructorForManagement lookupInstructorForManagementFromReader = this.GetLookupInstructorForManagementFromReader(dataReader, batchDecryptor, out flag2);
						bool flag3 = lookupInstructorForManagementFromReader != null;
						if (flag3)
						{
							list.Add(lookupInstructorForManagementFromReader);
						}
						flag4 = flag2;
					}
					while (!flag4);
					result = list;
				}
			}
			return result;
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x00028830 File Offset: 0x00026A30
		public void SwapInstructors(int instructorSourceId, int instructorDestId)
		{
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, this.OpContext.TenantId);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@instructorsourceid", DbType.Int32, instructorSourceId),
				databaseLayer.GetParameter("@instructordestid", DbType.Int32, instructorDestId)
			};
			databaseLayer.ExecuteNonQuery("-- insert new rows into lucourseinstructor with existing lucourseids and @instructorsourceid, unless that already exists\r\n-- then delete the original rows with @instructorsourceid\r\nINSERT INTO LuCourseInstructor (lucourseid,instructorid,ExemptAssignmentFromDataSync)\r\n     SELECT lucourseid,@instructordestid,ExemptAssignmentFromDataSync FROM LuCourseInstructor \r\n\t WHERE  instructorid=@instructorsourceid AND NOT lucourseid IN (SELECT lucourseid FROM LuCourseInstructor WHERE instructorid=@instructordestid)\r\n\r\nDELETE FROM LuCourseInstructor WHERE instructorid=@instructorsourceid\r\n\r\nUPDATE LuCourses SET InstructorID=@instructordestid WHERE InstructorID=@instructorsourceid", parameters);
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x00028890 File Offset: 0x00026A90
		public void DeleteInstructor(int instructorId)
		{
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, this.OpContext.TenantId);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@instructorid", DbType.Int32, instructorId)
			};
			databaseLayer.ExecuteNonQuery("DELETE FROM LuCourseInstructor WHERE instructorid=@instructorid\r\nUPDATE LuCourses SET InstructorID=-1 WHERE InstructorID=@instructorid\r\nDELETE FROM LuCourseData WHERE LuCourseDataId=@instructorid", parameters);
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x000288DC File Offset: 0x00026ADC
		private LookupInstructorForManagement GetLookupInstructorForManagementFromReader(IDataReader reader, IBatchDecryptor batchDecryptor, out bool endOfRecords)
		{
			LookupInstructor instructorFromReader = LookupInstructorDAO.GetInstructorFromReader(reader, "");
			bool flag = instructorFromReader == null;
			LookupInstructorForManagement result;
			if (flag)
			{
				endOfRecords = !reader.Read();
				result = null;
			}
			else
			{
				bool flag2 = reader.ContainsColumn("exemptfromdatasync");
				if (flag2)
				{
					instructorFromReader.IsExemptFromDataSync = (!(reader["exemptfromdatasync"] is DBNull) && (bool)reader["exemptfromdatasync"]);
				}
				List<LookupInstructorManagementDAO.LookupInstructorInfoRow> list = new List<LookupInstructorManagementDAO.LookupInstructorInfoRow>
				{
					this.GetLookupInstructorInfoRowFromRecord(reader, batchDecryptor)
				};
				int instructorId = instructorFromReader.InstructorId;
				for (;;)
				{
					endOfRecords = !reader.Read();
					bool flag3 = endOfRecords;
					if (flag3)
					{
						break;
					}
					int num = (reader["InstructorId"] is DBNull) ? 0 : ((int)reader["InstructorId"]);
					bool flag4 = num != instructorId;
					if (flag4)
					{
						break;
					}
					list.Add(this.GetLookupInstructorInfoRowFromRecord(reader, batchDecryptor));
				}
				List<LookupInstructorCourseAttachmentForManagement> list2 = new List<LookupInstructorCourseAttachmentForManagement>();
				list.Sort(delegate(LookupInstructorManagementDAO.LookupInstructorInfoRow g1, LookupInstructorManagementDAO.LookupInstructorInfoRow g2)
				{
					LookupInstructorCourseAttachmentForManagement course3 = g1.Course;
					int num4 = (course3 != null) ? course3.LuCourseId : 0;
					LookupInstructorCourseAttachmentForManagement course4 = g2.Course;
					int num5 = (course4 != null) ? course4.LuCourseId : 0;
					bool flag8 = num4 != num5;
					int result2;
					if (flag8)
					{
						result2 = num4.CompareTo(num5);
					}
					else
					{
						LookupInstructorCourseStudentAttachmentForManagement student2 = g1.Student;
						int num6 = (student2 != null) ? student2.PersonId : 0;
						LookupInstructorCourseStudentAttachmentForManagement student3 = g2.Student;
						result2 = num6.CompareTo((student3 != null) ? new int?(student3.PersonId) : null);
					}
					return result2;
				});
				int i = 0;
				while (i < list.Count)
				{
					LookupInstructorManagementDAO.LookupInstructorInfoRow lookupInstructorInfoRow = list[i];
					LookupInstructorCourseAttachmentForManagement course = lookupInstructorInfoRow.Course;
					int num2 = (course != null) ? course.LuCourseId : 0;
					bool flag5 = num2 < 1;
					if (flag5)
					{
						i++;
					}
					else
					{
						int j = i;
						List<LookupInstructorCourseStudentAttachmentForManagement> list3 = new List<LookupInstructorCourseStudentAttachmentForManagement>();
						while (j < list.Count)
						{
							LookupInstructorManagementDAO.<>c__DisplayClass8_0 CS$<>8__locals1 = new LookupInstructorManagementDAO.<>c__DisplayClass8_0();
							LookupInstructorManagementDAO.LookupInstructorInfoRow lookupInstructorInfoRow2 = list[j];
							LookupInstructorCourseAttachmentForManagement course2 = lookupInstructorInfoRow2.Course;
							int num3 = (course2 != null) ? course2.LuCourseId : 0;
							bool flag6 = num3 != num2;
							if (flag6)
							{
								break;
							}
							LookupInstructorManagementDAO.<>c__DisplayClass8_0 CS$<>8__locals2 = CS$<>8__locals1;
							LookupInstructorCourseStudentAttachmentForManagement student = lookupInstructorInfoRow2.Student;
							CS$<>8__locals2.pid = ((student != null) ? student.PersonId : 0);
							bool flag7 = CS$<>8__locals1.pid > 0 && list3.All((LookupInstructorCourseStudentAttachmentForManagement g) => g.PersonId != CS$<>8__locals1.pid);
							if (flag7)
							{
								list3.Add(lookupInstructorInfoRow2.Student);
							}
							j++;
						}
						lookupInstructorInfoRow.Course.Students = list3;
						list2.Add(lookupInstructorInfoRow.Course);
						i = j;
					}
				}
				LookupInstructorForManagement lookupInstructorForManagement = new LookupInstructorForManagement
				{
					Instructor = instructorFromReader,
					AttachedCourses = list2
				};
				result = lookupInstructorForManagement;
			}
			return result;
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x00028B44 File Offset: 0x00026D44
		private LookupInstructorManagementDAO.LookupInstructorInfoRow GetLookupInstructorInfoRowFromRecord(IDataReader reader, IBatchDecryptor batchDecryptor)
		{
			int num = (reader["lucourseid"] is DBNull) ? 0 : ((int)reader["lucourseid"]);
			LookupInstructorCourseAttachmentForManagement course = (num < 1) ? null : new LookupInstructorCourseAttachmentForManagement
			{
				LuCourseId = num,
				CourseDescription = this.GetCourseDescriptionFromRecord(reader),
				StartDate = ((reader["startdate"] is DBNull) ? DateTime.Now : ((DateTime)reader["startdate"])),
				EndDate = ((reader["enddate"] is DBNull) ? DateTime.Now : ((DateTime)reader["enddate"])),
				IsInstructorExemptFromDataSyncAssignment = (!(reader["InstructorExemptAssignmentFromDataSync"] is DBNull) && (bool)reader["InstructorExemptAssignmentFromDataSync"])
			};
			int num2 = (reader["personid"] is DBNull) ? 0 : ((int)reader["personid"]);
			LookupInstructorCourseStudentAttachmentForManagement student = (num2 < 1) ? null : new LookupInstructorCourseStudentAttachmentForManagement
			{
				PersonId = num2,
				Name = this.GetStudentNameFromRecord(reader, batchDecryptor),
				StudentNumber = reader.GetEncryptedStringFromRecord(batchDecryptor, "student_no"),
				IsCourseDropped = (!(reader["registrationstatus"] is DBNull) && (int)reader["registrationstatus"] == 2)
			};
			return new LookupInstructorManagementDAO.LookupInstructorInfoRow
			{
				Course = course,
				Student = student
			};
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x00028CE4 File Offset: 0x00026EE4
		private string GetCourseDescriptionFromRecord(IDataRecord record)
		{
			string stringFromRecord = record.GetStringFromRecord("subject");
			string stringFromRecord2 = record.GetStringFromRecord("course");
			string stringFromRecord3 = record.GetStringFromRecord("section");
			string stringFromRecord4 = record.GetStringFromRecord("duration");
			string stringFromRecord5 = record.GetStringFromRecord("term");
			string stringFromRecord6 = record.GetStringFromRecord("campus");
			string stringFromRecord7 = record.GetStringFromRecord("timeofday");
			return string.Concat(new string[]
			{
				stringFromRecord,
				" ",
				stringFromRecord2,
				" ",
				stringFromRecord3,
				" ",
				stringFromRecord7,
				" (",
				stringFromRecord4,
				" - ",
				stringFromRecord5,
				") ",
				stringFromRecord6
			}).Trim();
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x00028DB4 File Offset: 0x00026FB4
		private string GetStudentNameFromRecord(IDataRecord record, IBatchDecryptor batchDecryptor)
		{
			string encryptedStringFromRecord = record.GetEncryptedStringFromRecord(batchDecryptor, "firstname");
			string encryptedStringFromRecord2 = record.GetEncryptedStringFromRecord(batchDecryptor, "middlename");
			string encryptedStringFromRecord3 = record.GetEncryptedStringFromRecord(batchDecryptor, "lastname");
			return string.Join(" ", (from g in new string[]
			{
				encryptedStringFromRecord,
				encryptedStringFromRecord2,
				encryptedStringFromRecord3
			}
			where g.Trim().Length > 0
			select g).ToArray<string>());
		}

		// Token: 0x020001F5 RID: 501
		internal class LookupInstructorInfoRow
		{
			// Token: 0x1700013E RID: 318
			// (get) Token: 0x06000CD9 RID: 3289 RVA: 0x00081BB9 File Offset: 0x0007FDB9
			// (set) Token: 0x06000CDA RID: 3290 RVA: 0x00081BC1 File Offset: 0x0007FDC1
			public LookupInstructorCourseAttachmentForManagement Course { get; set; }

			// Token: 0x1700013F RID: 319
			// (get) Token: 0x06000CDB RID: 3291 RVA: 0x00081BCA File Offset: 0x0007FDCA
			// (set) Token: 0x06000CDC RID: 3292 RVA: 0x00081BD2 File Offset: 0x0007FDD2
			public LookupInstructorCourseStudentAttachmentForManagement Student { get; set; }
		}
	}
}
