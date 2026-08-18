using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.Common.DAO.Impl.Adapters;
using TechnoPro.Common.DAO.Impl.People;
using TechnoPro.Common.DAO.LookupCourses;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.LookupCourses;
using TechnoPro.Common.Public.Entities.StudentAccommodationRequests;

namespace TechnoPro.Common.DAO.Impl.LookupCourses
{
	// Token: 0x0200009C RID: 156
	public class LookupInstructorDAO : ILookupInstructorDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600042A RID: 1066 RVA: 0x00025668 File Offset: 0x00023868
		// (set) Token: 0x0600042B RID: 1067 RVA: 0x00025670 File Offset: 0x00023870
		public DatabaseLayer DatabaseManager { get; private set; }

		// Token: 0x0600042C RID: 1068 RVA: 0x00025679 File Offset: 0x00023879
		public LookupInstructorDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext2 = this.OpContext;
			this.DatabaseManager = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext2 != null) ? opContext2.TenantId : null);
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600042D RID: 1069 RVA: 0x000256AA File Offset: 0x000238AA
		// (set) Token: 0x0600042E RID: 1070 RVA: 0x000256B2 File Offset: 0x000238B2
		public OperationContext OpContext { get; set; }

		// Token: 0x0600042F RID: 1071 RVA: 0x000256BC File Offset: 0x000238BC
		internal static LookupInstructor GetPrimaryInstructorFromCourseRecord(string colPrefix, IDataReader record)
		{
			bool flag = PeopleDAO.ReaderContainsColumn(record, colPrefix + "pinstructorid");
			if (flag)
			{
				bool flag2 = record[colPrefix + "pinstructorid"] != DBNull.Value;
				if (flag2)
				{
					int num = (int)record[colPrefix + "pinstructorid"];
					bool flag3 = num > 0;
					if (flag3)
					{
						string text = colPrefix + "pexemptfromdatasync";
						string text2 = colPrefix + "pExemptAssignmentFromDataSync";
						return new LookupInstructor
						{
							IsPrimary = true,
							InstructorId = num,
							Name = record[colPrefix + "pinstructorname"].ToString(),
							Email = record[colPrefix + "pinstructoremail"].ToString(),
							Phone = record[colPrefix + "pinstructorphone"].ToString(),
							Username = record[colPrefix + "pinstructorusername"].ToString(),
							ExternalId = record[colPrefix + "pinstructorexternalid"].ToString(),
							EmployeeId = record[colPrefix + "pinstructoremployeeid"].ToString(),
							IsExemptFromDataSync = (PeopleDAO.ReaderContainsColumn(record, text) && record[text] != DBNull.Value && Convert.ToBoolean(record[text])),
							IsExemptAssignmentFromDataSync = (PeopleDAO.ReaderContainsColumn(record, text2) && record[text2] != DBNull.Value && Convert.ToBoolean(record[text2]))
						};
					}
				}
			}
			return null;
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x00025880 File Offset: 0x00023A80
		internal static LookupInstructor GetSecondaryInstructorFromCourseRecord(string colPrefix, IDataReader record)
		{
			string text = colPrefix + "p3instructorid";
			bool flag = PeopleDAO.ReaderContainsColumn(record, text);
			if (flag)
			{
				bool flag2 = record[text] != DBNull.Value;
				if (flag2)
				{
					string text2 = colPrefix + "p3exemptfromdatasync";
					LookupInstructor lookupInstructor = new LookupInstructor
					{
						IsPrimary = false,
						InstructorId = (int)record[text],
						Name = record[colPrefix + "p3instructorname"].ToString(),
						Email = record[colPrefix + "p3instructoremail"].ToString(),
						Phone = record[colPrefix + "p3instructorphone"].ToString(),
						Username = record[colPrefix + "p3instructorusername"].ToString(),
						ExternalId = record[colPrefix + "p3instructorexternalid"].ToString(),
						EmployeeId = record[colPrefix + "p3instructoremployeeid"].ToString(),
						IsExemptFromDataSync = (PeopleDAO.ReaderContainsColumn(record, text2) && record[text2] != DBNull.Value && Convert.ToBoolean(record[text2]))
					};
					string text3 = colPrefix + "p3ExemptAssignmentFromDataSync";
					bool flag3 = PeopleDAO.ReaderContainsColumn(record, text3) && record[text3] != DBNull.Value;
					if (flag3)
					{
						lookupInstructor.IsExemptAssignmentFromDataSync = Convert.ToBoolean(record[text3]);
					}
					return lookupInstructor;
				}
			}
			return null;
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x00025A24 File Offset: 0x00023C24
		internal static LookupInstructor GetInstructorFromReader(IDataReader record, string colPrefix = "")
		{
			string name = colPrefix + "instructorid";
			bool flag = record[name] is DBNull;
			LookupInstructor result;
			if (flag)
			{
				result = null;
			}
			else
			{
				LookupInstructor lookupInstructor = new LookupInstructor
				{
					InstructorId = (int)record[name],
					Name = record[colPrefix + "instructorname"].ToString(),
					Email = record[colPrefix + "instructoremail"].ToString(),
					Phone = record[colPrefix + "instructorphone"].ToString(),
					Username = record[colPrefix + "instructorusername"].ToString(),
					ExternalId = record[colPrefix + "instructorexternalid"].ToString(),
					EmployeeId = record[colPrefix + "instructoremployeeid"].ToString()
				};
				string text = colPrefix + "InstructorExemptAssignmentFromDataSync";
				bool flag2 = record.ContainsColumn(text);
				if (flag2)
				{
					lookupInstructor.IsExemptAssignmentFromDataSync = (!(record[text] is DBNull) && Convert.ToBoolean(record[text]));
				}
				else
				{
					text = colPrefix + "ExemptAssignmentFromDataSync";
					bool flag3 = record.ContainsColumn(text);
					if (flag3)
					{
						lookupInstructor.IsExemptAssignmentFromDataSync = (!(record[text] is DBNull) && Convert.ToBoolean(record[text]));
					}
				}
				string text2 = colPrefix + "exemptfromdatasync";
				bool flag4 = PeopleDAO.ReaderContainsColumn(record, text2);
				if (flag4)
				{
					lookupInstructor.IsExemptFromDataSync = (!(record[text2] is DBNull) && Convert.ToBoolean(record[text2]));
				}
				string text3 = colPrefix + "PermissionLevel";
				bool flag5 = !PeopleDAO.ReaderContainsColumn(record, text3);
				if (flag5)
				{
					text3 = colPrefix + "InstructorPermissionLevel";
				}
				bool flag6 = PeopleDAO.ReaderContainsColumn(record, text3);
				if (flag6)
				{
					int num = (record[text3] == DBNull.Value) ? 0 : ((int)record[text3]);
					bool flag7 = Enum.IsDefined(typeof(ePermissionForCourse), num);
					if (flag7)
					{
						lookupInstructor.PermissionLevel = (ePermissionForCourse)num;
					}
				}
				bool flag8 = PeopleDAO.ReaderContainsColumn(record, colPrefix + "PrimaryInstructorId");
				if (flag8)
				{
					int num2 = (record["PrimaryInstructorId"] is DBNull) ? 0 : ((int)record["PrimaryInstructorId"]);
					bool flag9 = num2 > 0;
					if (flag9)
					{
						lookupInstructor.IsPrimary = (num2 == lookupInstructor.InstructorId);
					}
				}
				result = lookupInstructor;
			}
			return result;
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x00025CC4 File Offset: 0x00023EC4
		public LookupInstructor LoadInstructor(int InstructorId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@instructorid", DbType.Int32, InstructorId)
			};
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT    lucd.lucoursedataid AS instructorid,lucd.lookupstring,lucd.altlookupstring AS instructorname,\r\nlucd.email AS instructoremail,lucd.phone AS instructorphone,lucd.username AS instructorusername,\r\nlucd.externalid AS instructorexternalid,lucd.id AS instructoremployeeid,lucd.permissionlevel AS instructorpermissionlevel,\r\nlucd.exemptfromdatasync\r\nFROM        lucoursedata lucd \r\nWHERE       lucd.lookuplisttype=1 AND lucd.lucoursedataid=@instructorid", parameters))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					return LookupInstructorDAO.GetInstructorFromReader(dataReader, "");
				}
			}
			return null;
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x00025D48 File Offset: 0x00023F48
		public void SaveInstructorsForCourse(int LuCourseId, List<LookupInstructor> Instructors, bool updateInstructorInfo)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@lucid", DbType.Int32, LuCourseId)
			};
			this.DatabaseManager.ExecuteQuery("DELETE FROM lucourseinstructor WHERE lucourseid=@lucid", parameters);
			foreach (LookupInstructor lookupInstructor in Instructors)
			{
				if (updateInstructorInfo)
				{
					this.SaveInstructor(lookupInstructor);
				}
				bool flag = lookupInstructor.InstructorId > 0;
				if (flag)
				{
					parameters = new DbParameter[]
					{
						this.DatabaseManager.GetParameter("@lucid", DbType.Int32, LuCourseId),
						this.DatabaseManager.GetParameter("@instructorid", DbType.Int32, lookupInstructor.InstructorId)
					};
					this.DatabaseManager.ExecuteNonQuery("IF NOT EXISTS(SELECT lucourseid FROM lucourseinstructor WHERE lucourseid=@lucid AND instructorid=@instructorid)\r\n    INSERT INTO lucourseinstructor(lucourseid,instructorid) VALUES (@lucid,@instructorid)", parameters);
				}
			}
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x00025E40 File Offset: 0x00024040
		public void SaveInstructor(LookupInstructor instructor)
		{
			string text = (instructor.Name ?? "").Trim();
			bool flag = text.Length > 1000;
			if (flag)
			{
				text = text.Substring(0, 1000);
			}
			string text2 = (instructor.Username ?? "").Trim();
			bool flag2 = text2.Length > 8000;
			if (flag2)
			{
				text2 = text2.Substring(0, 8000);
			}
			string text3 = (instructor.EmployeeId ?? "").Trim();
			bool flag3 = text3.Length > 8000;
			if (flag3)
			{
				text3 = text3.Substring(0, 8000);
			}
			string text4 = (instructor.Email ?? "").Trim();
			bool flag4 = text4.Length > 500;
			if (flag4)
			{
				text4 = text4.Substring(0, 500);
			}
			string text5 = (instructor.Phone ?? "").Trim();
			bool flag5 = text5.Length > 1000;
			if (flag5)
			{
				text5 = text5.Substring(0, 100);
			}
			string text6 = (instructor.ExternalId ?? "").Trim();
			bool flag6 = text6.Length > 255;
			if (flag6)
			{
				text6 = text6.Substring(0, 255);
			}
			bool flag7 = instructor.InstructorId > 0;
			if (flag7)
			{
				DbParameter dbParameter = (text4.Length < 1) ? this.DatabaseManager.GetParameter("@email", DbType.String, DBNull.Value) : this.DatabaseManager.GetParameter("@email", DbType.String, text4);
				DbParameter dbParameter2 = (text5.Length < 1) ? this.DatabaseManager.GetParameter("@phone", DbType.String, DBNull.Value) : this.DatabaseManager.GetParameter("@phone", DbType.String, text5);
				DbParameter dbParameter3 = (text3.Length < 1) ? this.DatabaseManager.GetParameter("@employeeid", DbType.String, DBNull.Value) : this.DatabaseManager.GetParameter("@employeeid", DbType.String, text3);
				DbParameter dbParameter4 = (text2.Length < 1) ? this.DatabaseManager.GetParameter("@username", DbType.String, DBNull.Value) : this.DatabaseManager.GetParameter("@username", DbType.String, text2);
				DbParameter dbParameter5 = (text6.Length < 1) ? this.DatabaseManager.GetParameter("@externalid", DbType.String, DBNull.Value) : this.DatabaseManager.GetParameter("@externalid", DbType.String, text6);
				DbParameter[] parameters = new DbParameter[]
				{
					this.DatabaseManager.GetParameter("@instructorid", DbType.Int32, instructor.InstructorId),
					dbParameter,
					this.DatabaseManager.GetParameter("@name", DbType.String, text),
					dbParameter2,
					this.DatabaseManager.GetParameter("@exemptfromdatasync", DbType.Boolean, instructor.IsExemptFromDataSync),
					dbParameter4,
					dbParameter3,
					dbParameter5
				};
				this.DatabaseManager.ExecuteNonQuery("UPDATE lucoursedata SET \r\naltlookupstring=@name,lookupstring=@name,\r\nemail=COALESCE(@email,email),phone=COALESCE(@phone,phone),\r\nexemptfromdatasync=@exemptfromdatasync,\r\nusername=COALESCE(@username,username),\r\nid=COALESCE(@employeeid,id),externalid=COALESCE(@externalid,externalid)\r\nWHERE lucoursedataid=@instructorid", parameters);
			}
			else
			{
				DbParameter[] parameters = new DbParameter[]
				{
					this.DatabaseManager.GetParameter("@instructorname", DbType.String, text),
					this.DatabaseManager.GetParameter("@instructoremail", DbType.String, text4),
					this.DatabaseManager.GetParameter("@instructorusername", DbType.String, text2),
					this.DatabaseManager.GetParameter("@instructorphone", DbType.String, text5),
					this.DatabaseManager.GetParameter("@instructoremployeeid", DbType.String, text3),
					this.DatabaseManager.GetParameter("@instructorexternalid", DbType.String, text6),
					this.DatabaseManager.GetParameter("@exemptfromdatasync", DbType.Boolean, instructor.IsExemptFromDataSync)
				};
				object obj = this.DatabaseManager.ExecuteScalar("IF LEN(COALESCE(@instructorexternalid,',')) < 1 AND LEN(COALESCE(@instructorusername,',')) < 1 AND LEN(COALESCE(@instructoremployeeid,',')) < 1 AND LEN(COALESCE(@instructoremail,',')) < 1\r\n\tSELECT 0 AS lucoursedataid\r\nELSE IF EXISTS(\r\nSELECT lucoursedataid FROM lucoursedata lucd \r\nWHERE lucd.lookuplisttype=1 AND \r\n    (\r\n        (LEN(COALESCE(@instructorexternalid,',')) > 0 AND lucd.ExternalId=@instructorexternalid)\r\n\t\t    OR\r\n\t\t(LEN(COALESCE(@instructorusername,',')) > 0 AND lucd.username=@instructorusername)\r\n\t\t    OR\r\n\t\t(LEN(COALESCE(@instructoremployeeid,',')) > 0 AND lucd.id=@instructoremployeeid)\r\n            OR\r\n        (LEN(COALESCE(@instructoremail,',')) > 0 AND lucd.username=@instructoremail)\r\n    )\r\n)\r\n    SELECT TOP 1 lucoursedataid FROM lucoursedata lucd \r\n    WHERE lucd.lookuplisttype=1 AND \r\n    (\r\n        (LEN(COALESCE(@instructorexternalid,',')) > 0 AND lucd.ExternalId=@instructorexternalid)\r\n\t    \tOR\r\n\t\t(LEN(COALESCE(@instructorusername,',')) > 0 AND lucd.username=@instructorusername)\r\n\t\t    OR\r\n\t\t(LEN(COALESCE(@instructoremployeeid,',')) > 0 AND lucd.id=@instructoremployeeid)\r\n            OR\r\n        (LEN(COALESCE(@instructoremail,',')) > 0 AND lucd.username=@instructoremail)\r\n    )\r\n\tORDER BY lucd.luCourseDataID DESC\r\nELSE\r\nBEGIN\r\n    INSERT INTO lucoursedata (lookuplisttype,lookupstring,altlookupstring,email,phone,username,id,externalid,exemptfromdatasync) \r\n    VALUES (1,@instructorname,@instructorname,@instructoremail,@instructorphone,@instructorusername,@instructoremployeeid,@instructorexternalid,@exemptfromdatasync);\r\n\r\n    SELECT CAST(SCOPE_IDENTITY() AS INT) AS lucoursedataid\r\nEND", parameters);
				bool flag8 = obj == null;
				if (flag8)
				{
					throw new Exception(string.Format("Failed to insert new instructor:name={0}:email={1}:username={2}:employeeid={3}:externalid={4}", new object[]
					{
						text ?? "NULL",
						text4 ?? "NULL",
						text2 ?? "NULL",
						text3 ?? "NULL",
						text6 ?? "NULL"
					}));
				}
				instructor.InstructorId = (int)obj;
				bool flag9 = instructor.InstructorId < 1;
				if (flag9)
				{
					throw new Exception(string.Format("Failed to insert new instructor (invalid instructor id returned):name={0}:email={1}:username={2}:employeeid={3}:externalid={4}", new object[]
					{
						text ?? "NULL",
						text4 ?? "NULL",
						text2 ?? "NULL",
						text3 ?? "NULL",
						text6 ?? "NULL"
					}));
				}
			}
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x000262EC File Offset: 0x000244EC
		public LookupInstructor LoadInstructorByUsername(string Username)
		{
			bool flag = Username == null || Username.Trim().Length < 1;
			LookupInstructor result;
			if (flag)
			{
				result = null;
			}
			else
			{
				DbParameter[] parameters = new DbParameter[]
				{
					this.DatabaseManager.GetParameter("@username", DbType.String, Username)
				};
				List<LookupInstructor> list = new List<LookupInstructor>();
				using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT    lucd.lucoursedataid AS instructorid,lucd.lookupstring,lucd.altlookupstring AS instructorname,\r\nlucd.email AS instructoremail,lucd.phone AS instructorphone,lucd.username AS instructorusername,\r\nlucd.externalid AS instructorexternalid,lucd.id AS instructoremployeeid,lucd.permissionlevel AS instructorpermissionlevel,\r\nlucd.exemptfromdatasync\r\nFROM        lucoursedata lucd \r\nWHERE       lucd.lookuplisttype=1 AND NOT lucd.username='' AND lucd.username=@username", parameters))
				{
					bool flag2 = dataReader != null && dataReader.Read();
					if (flag2)
					{
						LookupInstructor prof = LookupInstructorDAO.GetInstructorFromReader(dataReader, "");
						bool flag3 = prof != null && list.Find((LookupInstructor p) => p.InstructorId == prof.InstructorId) == null;
						if (flag3)
						{
							list.Add(prof);
						}
					}
				}
				bool flag4 = list.Count > 0;
				if (flag4)
				{
					bool flag5 = list.Count > 1;
					if (flag5)
					{
						int instructorId = list[0].InstructorId;
						for (int i = 1; i < list.Count; i++)
						{
							int instructorId2 = list[i].InstructorId;
							parameters = new DbParameter[]
							{
								this.DatabaseManager.GetParameter("@primaryid", DbType.Int32, instructorId),
								this.DatabaseManager.GetParameter("@id", DbType.Int32, instructorId2)
							};
							this.DatabaseManager.ExecuteNonQuery("UPDATE lucourses SET instructorid=@primaryid WHERE instructorid=@id; DELETE FROM lucoursedata WHERE lucoursedataid=@id", parameters);
						}
					}
					result = list[0];
				}
				else
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x00026498 File Offset: 0x00024698
		public LookupInstructor LoadInstructorByEmail(string Email)
		{
			bool flag = Email == null || Email.Trim().Length < 1;
			LookupInstructor result;
			if (flag)
			{
				result = null;
			}
			else
			{
				DbParameter[] parameters = new DbParameter[]
				{
					this.DatabaseManager.GetParameter("@email", DbType.String, Email)
				};
				using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT    lucd.lucoursedataid AS instructorid,lucd.lookupstring,lucd.altlookupstring AS instructorname,\r\nlucd.email AS instructoremail,lucd.phone AS instructorphone,lucd.username AS instructorusername,\r\nlucd.externalid AS instructorexternalid,lucd.id AS instructoremployeeid,lucd.permissionlevel AS instructorpermissionlevel,\r\nlucd.exemptfromdatasync\r\nFROM        lucoursedata lucd \r\nWHERE       lucd.lookuplisttype=1 AND NOT lucd.email='' AND lucd.email=@email", parameters))
				{
					bool flag2 = dataReader != null && dataReader.Read();
					if (flag2)
					{
						return LookupInstructorDAO.GetInstructorFromReader(dataReader, "");
					}
				}
				result = null;
			}
			return result;
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x00026538 File Offset: 0x00024738
		public List<LookupCourse> LoadInstructorCourses(int InstructorId, int AltContactId, int PermissionLevel, bool MustHaveClassTestDefinition, DateTime StartDate, DateTime EndDate, bool EachCourseMustHaveAtLeastOneRegisteredStudent)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@iid", DbType.Int32, InstructorId),
				this.DatabaseManager.GetParameter("@altid", DbType.Int32, AltContactId),
				this.DatabaseManager.GetParameter("@startdate", DbType.DateTime, StartDate.Date),
				this.DatabaseManager.GetParameter("@enddate", DbType.DateTime, EndDate.Date.AddDays(1.0).AddMinutes(-1.0)),
				this.DatabaseManager.GetParameter("@musthavetestdef", DbType.Boolean, MustHaveClassTestDefinition),
				this.DatabaseManager.GetParameter("@permissionlevel", DbType.Int32, PermissionLevel),
				this.DatabaseManager.GetParameter("@eachCourseMustHaveAtLeastOneRegisteredStudent", DbType.Boolean, EachCourseMustHaveAtLeastOneRegisteredStudent)
			};
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT luc.lucourseid,luc.startdate,luc.enddate,luc.duration,luc.term,luc.subjectid,luc.externalid,luc.exemptfromdatasync AS lucexemptfromdatasync\r\n        ,lucd.lookupstring AS subjectcode,lucd.altlookupstring AS subjectdescription\r\n        ,luc.course,luc.timeofday,luc.[section]\r\n        ,luc.campus,luc.department,luc.location,luc.credits\r\n        ,luc.instructorid AS pinstructorid,lucd2.altlookupstring AS pinstructorname,lucd2.email AS pinstructoremail,lucd2.phone AS pinstructorphone,lucd2.username AS pinstructorusername,lucd2.exemptfromdatasync AS pexemptfromdatasync,lucd2.id AS pinstructoremployeeid,lucd2.externalid AS pinstructorexternalid\r\n        ,luc.ExemptAssignmentFromDataSync AS pExemptAssignmentFromDataSync\r\n        ,luc.BatchDataSyncLogId\r\n        ,lci.instructorid AS p3instructorid,lucd3.altlookupstring AS p3instructorname,lucd3.email AS p3instructoremail,lucd3.phone AS p3instructorphone,lucd3.username AS p3instructorusername,lucd3.exemptfromdatasync AS p3exemptfromdatasync,lucd3.id AS p3instructoremployeeid,lucd3.externalid AS p3instructorexternalid\r\n        ,lci.ExemptAssignmentFromDataSync AS p3ExemptAssignmentFromDataSync\r\n        ,tt.timetableid\r\n        ,tt.sunstartminutes,tt.sunendminutes,tt.monstartminutes,tt.monendminutes,tt.tuestartminutes,tt.tueendminutes\r\n        ,tt.wedstartminutes,tt.wedendminutes,tt.thustartminutes,tt.thuendminutes,tt.fristartminutes,tt.friendminutes\r\n        ,tt.satstartminutes,tt.satendminutes,tt.sunroom,tt.monroom,tt.tueroom,tt.wedroom,tt.thuroom,tt.friroom,tt.satroom,\r\n        luc.alternatecontactid,ac.altname,ac.altemail,ac.altphone,ac.altusername,ac.externalid,ac.altpermissionlevel,\r\n        lucac.alternatecontactid AS secondaryalternatecontactid,\r\n        ac2.altname AS secondaryaltname,ac2.altemail AS secondaryaltemail,ac2.altphone AS secondaryaltphone,\r\n        ac2.altusername AS secondaryaltusername,ac2.externalid AS secondaryexternalid,\r\n        ac2.altpermissionlevel AS secondaryaltpermissionlevel,\r\n        luc.coursenote\r\nFROM    lucourses luc \r\n        LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid\r\n        LEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luc.instructorid\r\n        LEFT JOIN lucourseinstructor lci ON lci.lucourseid=luc.lucourseid\r\n        LEFT JOIN lucoursedata lucd3 ON lucd3.lucoursedataid=lci.instructorid\r\n        LEFT JOIN timetable tt ON tt.timetabletype='C' AND tt.lucourseid=luc.lucourseid\r\n        LEFT JOIN lucoursealternatecontact ac ON ac.alternatecontactid=luc.alternatecontactid\r\n        LEFT JOIN LuCourseAltContact lucac ON lucac.lucourseid=luc.lucourseid\r\n        LEFT JOIN lucoursealternatecontact ac2 ON ac2.alternatecontactid=lucac.alternatecontactid\r\nWHERE   (NOT ( luc.enddate <= @startdate OR luc.startdate > @enddate))\r\n        AND (@musthavetestdef=0 OR luc.lucourseid IN (SELECT lucourseid FROM exams))\r\n        AND \r\n\t\t\t(dbo.InstructorAllowed(luc.lucourseid,@iid,@permissionlevel)=1\r\n\t\t\tOR dbo.AltContactAllowed(luc.lucourseid,@altid,@permissionlevel)=1\r\n\t\t\t)\r\n        AND \r\n        (\r\n            (@eachCourseMustHaveAtLeastOneRegisteredStudent IS NULL OR @eachCourseMustHaveAtLeastOneRegisteredStudent=0)\r\n            OR\r\n            luc.lucourseid IN (SELECT lucourseid FROM courses WHERE registrationstatus IS NULL OR NOT registrationstatus=2)\r\n        )\r\nORDER BY luc.startdate,luc.duration,luc.term,lucd.altlookupstring,luc.course,luc.[section],luc.timeofday,luc.lucourseid", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					return LookupCourseDAO.GetCoursesFromReader("", dataReader);
				}
			}
			return null;
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x00026688 File Offset: 0x00024888
		public IList<int> LoadInstructorOrAltContactAssignedLuCourseIds(int InstructorId, int AlternateContactId, bool MustHaveClassTestDefinition, bool MustHaveOneRegisteredStudent)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@iid", DbType.Int32, InstructorId),
				this.DatabaseManager.GetParameter("@altid", DbType.Int32, AlternateContactId),
				this.DatabaseManager.GetParameter("@musthavetestdef", DbType.Boolean, MustHaveClassTestDefinition),
				this.DatabaseManager.GetParameter("@eachCourseMustHaveAtLeastOneRegisteredStudent", DbType.Boolean, MustHaveOneRegisteredStudent)
			};
			IList<int> result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT luc.lucourseid,luc.startdate,luc.enddate,luc.duration,luc.term,luc.subjectid,luc.externalid,luc.exemptfromdatasync AS lucexemptfromdatasync\r\n        ,lucd.lookupstring AS subjectcode,lucd.altlookupstring AS subjectdescription\r\n        ,luc.course,luc.timeofday,luc.[section]\r\n        ,luc.campus,luc.department,luc.location,luc.credits\r\n        ,luc.instructorid AS pinstructorid,lucd2.altlookupstring AS pinstructorname,lucd2.email AS pinstructoremail,lucd2.phone AS pinstructorphone,lucd2.username AS pinstructorusername,lucd2.exemptfromdatasync AS pexemptfromdatasync,lucd2.id AS pinstructoremployeeid,lucd2.externalid AS pinstructorexternalid\r\n        ,luc.ExemptAssignmentFromDataSync AS pExemptAssignmentFromDataSync\r\n        ,luc.BatchDataSyncLogId\r\n        ,lci.instructorid AS p3instructorid,lucd3.altlookupstring AS p3instructorname,lucd3.email AS p3instructoremail,lucd3.phone AS p3instructorphone,lucd3.username AS p3instructorusername,lucd3.exemptfromdatasync AS p3exemptfromdatasync,lucd3.id AS p3instructoremployeeid,lucd3.externalid AS p3instructorexternalid\r\n        ,lci.ExemptAssignmentFromDataSync AS p3ExemptAssignmentFromDataSync\r\n        ,tt.timetableid\r\n        ,tt.sunstartminutes,tt.sunendminutes,tt.monstartminutes,tt.monendminutes,tt.tuestartminutes,tt.tueendminutes\r\n        ,tt.wedstartminutes,tt.wedendminutes,tt.thustartminutes,tt.thuendminutes,tt.fristartminutes,tt.friendminutes\r\n        ,tt.satstartminutes,tt.satendminutes,tt.sunroom,tt.monroom,tt.tueroom,tt.wedroom,tt.thuroom,tt.friroom,tt.satroom,\r\n        luc.alternatecontactid,ac.altname,ac.altemail,ac.altphone,ac.altusername,ac.externalid,ac.altpermissionlevel,\r\n        lucac.alternatecontactid AS secondaryalternatecontactid,\r\n        ac2.altname AS secondaryaltname,ac2.altemail AS secondaryaltemail,ac2.altphone AS secondaryaltphone,\r\n        ac2.altusername AS secondaryaltusername,ac2.externalid AS secondaryexternalid,\r\n        ac2.altpermissionlevel AS secondaryaltpermissionlevel,\r\n        luc.coursenote\r\nFROM    lucourses luc \r\n        LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid\r\n        LEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luc.instructorid\r\n        LEFT JOIN lucourseinstructor lci ON lci.lucourseid=luc.lucourseid\r\n        LEFT JOIN lucoursedata lucd3 ON lucd3.lucoursedataid=lci.instructorid\r\n        LEFT JOIN timetable tt ON tt.timetabletype='C' AND tt.lucourseid=luc.lucourseid\r\n        LEFT JOIN lucoursealternatecontact ac ON ac.alternatecontactid=luc.alternatecontactid\r\n        LEFT JOIN LuCourseAltContact lucac ON lucac.lucourseid=luc.lucourseid\r\n        LEFT JOIN lucoursealternatecontact ac2 ON ac2.alternatecontactid=lucac.alternatecontactid\r\nWHERE   (NOT ( luc.enddate <= @startdate OR luc.startdate > @enddate))\r\n        AND (@musthavetestdef=0 OR luc.lucourseid IN (SELECT lucourseid FROM exams))\r\n        AND \r\n\t\t\t(dbo.InstructorAllowed(luc.lucourseid,@iid,@permissionlevel)=1\r\n\t\t\tOR dbo.AltContactAllowed(luc.lucourseid,@altid,@permissionlevel)=1\r\n\t\t\t)\r\n        AND \r\n        (\r\n            (@eachCourseMustHaveAtLeastOneRegisteredStudent IS NULL OR @eachCourseMustHaveAtLeastOneRegisteredStudent=0)\r\n            OR\r\n            luc.lucourseid IN (SELECT lucourseid FROM courses WHERE registrationstatus IS NULL OR NOT registrationstatus=2)\r\n        )\r\nORDER BY luc.startdate,luc.duration,luc.term,lucd.altlookupstring,luc.course,luc.[section],luc.timeofday,luc.lucourseid", parameters))
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
						int num = (dataReader["lucourseid"] is DBNull) ? 0 : ((int)dataReader["lucourseid"]);
						bool flag2 = num > 0;
						if (flag2)
						{
							list.Add(num);
						}
					}
					result = list.Distinct<int>().ToList<int>();
				}
			}
			return result;
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x000267A8 File Offset: 0x000249A8
		public List<LookupInstructor> LoadAllAssignedInstructors()
		{
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT    DISTINCT lucd.lucoursedataid AS instructorid,lucd.lookupstring,lucd.altlookupstring AS instructorname,\r\nlucd.email AS instructoremail,lucd.phone AS instructorphone,lucd.username AS instructorusername,\r\nlucd.externalid AS instructorexternalid,lucd.id AS instructoremployeeid,lucd.permissionlevel AS instructorpermissionlevel,\r\nlucd.exemptfromdatasync\r\nFROM        lucoursedata lucd \r\nWHERE       lucd.lookuplisttype=1 \r\n            AND \r\n            (lucd.lucoursedataid IN (SELECT instructorid AS lucoursedataid FROM lucourses) \r\n                OR lucd.lucoursedataid IN (SELECT instructorid AS lucoursedataid FROM lucourseinstructor)\r\n            )\r\nORDER BY lucd.altlookupstring"))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					List<LookupInstructor> list = new List<LookupInstructor>();
					while (dataReader.Read())
					{
						LookupInstructor prof = LookupInstructorDAO.GetInstructorFromReader(dataReader, "");
						bool flag2 = prof != null && list.Find((LookupInstructor p) => p.InstructorId == prof.InstructorId) == null;
						if (flag2)
						{
							list.Add(prof);
						}
					}
					return list;
				}
			}
			return null;
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x0002685C File Offset: 0x00024A5C
		public LookupInstructor LoadInstructorByEmployeeId(string employeeId)
		{
			bool flag = employeeId == null || employeeId.Trim().Length < 1;
			LookupInstructor result;
			if (flag)
			{
				result = null;
			}
			else
			{
				DbParameter[] parameters = new DbParameter[]
				{
					this.DatabaseManager.GetParameter("@employeeid", DbType.String, employeeId)
				};
				using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT    lucd.lucoursedataid AS instructorid,lucd.lookupstring,lucd.altlookupstring AS instructorname,\r\nlucd.email AS instructoremail,lucd.phone AS instructorphone,lucd.username AS instructorusername,\r\nlucd.externalid AS instructorexternalid,lucd.id AS instructoremployeeid,lucd.permissionlevel AS instructorpermissionlevel,\r\nlucd.exemptfromdatasync\r\nFROM        lucoursedata lucd \r\nWHERE       lucd.lookuplisttype=1 AND NOT lucd.id='' AND lucd.id=@employeeid", parameters))
				{
					bool flag2 = dataReader != null && dataReader.Read();
					if (flag2)
					{
						return LookupInstructorDAO.GetInstructorFromReader(dataReader, "");
					}
				}
				result = null;
			}
			return result;
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x000268FC File Offset: 0x00024AFC
		public IList<LookupInstructor> LoadInstructorsBySearchString(string SearchString)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@searchstring", DbType.String, string.Format("%{0}%", SearchString))
			};
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT    lucd2.lucoursedataid AS instructorid,lucd2.altlookupstring AS instructorname,\r\n            lucd2.email AS instructoremail,lucd2.phone AS instructorphone,\r\n            lucd2.username AS instructorusername,lucd2.externalid AS instructorexternalid,\r\n            lucd2.id AS instructoremployeeid,lucd2.exemptfromdatasync\r\nFROM        lucoursedata lucd2\r\nWHERE       lucd2.lookuplisttype=1\r\n            AND\r\n            (lucd2.altlookupstring LIKE @searchstring OR lucd2.email LIKE @searchstring \r\n            OR lucd2.phone LIKE @searchstring OR lucd2.username LIKE @searchstring \r\n            OR lucd2.id LIKE @searchstring)\r\nORDER BY    lucd2.altlookupstring", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					List<LookupInstructor> list = new List<LookupInstructor>();
					while (dataReader.Read())
					{
						LookupInstructor instructorFromReader = LookupInstructorDAO.GetInstructorFromReader(dataReader, "");
						bool flag2 = instructorFromReader != null;
						if (flag2)
						{
							list.Add(instructorFromReader);
						}
					}
					return list;
				}
			}
			return null;
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x000269B0 File Offset: 0x00024BB0
		public void AssignInstructorToCourse(int InstructorId, int LuCourseId, bool? IsAssignmentExemptFromDataSync)
		{
			bool flag = IsAssignmentExemptFromDataSync != null;
			DbParameter parameter;
			if (flag)
			{
				parameter = this.DatabaseManager.GetParameter("@isexempt", DbType.Boolean, IsAssignmentExemptFromDataSync.Value);
			}
			else
			{
				parameter = this.DatabaseManager.GetParameter("@isexempt", DbType.Boolean, DBNull.Value);
			}
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@instructorid", DbType.Int32, InstructorId),
				this.DatabaseManager.GetParameter("@lucid", DbType.Int32, LuCourseId),
				parameter
			};
			this.DatabaseManager.ExecuteNonQuery("IF EXISTS(SELECT lucourseid FROM lucourses WHERE lucourseid=@lucid AND instructorid=@instructorid)\r\n    UPDATE lucourses SET ExemptAssignmentFromDataSync=COALESCE(@isexempt,ExemptAssignmentFromDataSync) WHERE lucourseid=@lucid AND instructorid=@instructorid\r\nELSE IF EXISTS(SELECT lucourseid FROM lucourseinstructor WHERE lucourseid=@lucid AND instructorid=@instructorid)\r\n    UPDATE lucourseinstructor SET ExemptAssignmentFromDataSync=COALESCE(@isexempt,ExemptAssignmentFromDataSync) WHERE lucourseid=@lucid AND instructorid=@instructorid\r\nELSE\r\nBEGIN\r\n    IF EXISTS(SELECT lucourseid FROM lucourses WHERE lucourseid=@lucid AND instructorid>0)\r\n        INSERT INTO lucourseinstructor (lucourseid,instructorid,ExemptAssignmentFromDataSync) VALUES (@lucid,@instructorid,COALESCE(@isexempt,0))\r\n    ELSE\r\n        UPDATE lucourses SET instructorid=@instructorid,ExemptAssignmentFromDataSync=COALESCE(@isexempt,ExemptAssignmentFromDataSync) WHERE lucourseid=@lucid\r\nEND", parameters);
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x00026A54 File Offset: 0x00024C54
		public void RemoveInstructorFromCourse(int InstructorId, int LuCourseId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@instructorid", DbType.Int32, InstructorId),
				this.DatabaseManager.GetParameter("@lucid", DbType.Int32, LuCourseId)
			};
			this.DatabaseManager.ExecuteNonQuery("DELETE FROM lucourseinstructor WHERE lucourseid=@lucid AND instructorid=@instructorid\r\nUPDATE LUCourses SET instructorid=-1 WHERE LUCourseID=@lucid AND instructorid=@instructorid\r\n\r\nIF EXISTS(SELECT lucourseid FROM LUCourses WHERE LUCourseID=@lucid AND instructorid=-1)\r\n\tAND EXISTS(SELECT lucourseid FROM lucourseinstructor WHERE lucourseid=@lucid)\r\nBEGIN\r\n    DECLARE @acid int\r\n    SET @acid=(SELECT TOP 1 instructorid FROM lucourseinstructor WHERE lucourseid=@lucid)\r\n    UPDATE lucourses SET instructorid=@acid WHERE lucourseid=@lucid\r\n    DELETE FROM lucourseinstructor WHERE lucourseid=@lucid AND instructorid=@acid\r\nEND", parameters);
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x00026AB4 File Offset: 0x00024CB4
		public IList<LookupInstructor> LoadInstructorsByCourse(int LuCourseId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@lucid", DbType.Int32, LuCourseId)
			};
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT    vil.instructorid,lucd.altlookupstring AS instructorname,lucd.email AS instructoremail,\r\n            lucd.phone AS instructorphone,lucd.username AS instructorusername,\r\n            lucd.externalid AS instructorexternalid,lucd.id AS instructoremployeeid,\r\n            lucd.exemptfromdatasync,vil.instructorexemptassignmentfromdatasync,vil.PrimaryInstructorId\r\nFROM        vInstructorList vil LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=vil.instructorid\r\nWHERE       vil.lucourseid=@lucid\r\nORDER BY    lucd.altlookupstring,lucd.email", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					List<LookupInstructor> list = new List<LookupInstructor>();
					while (dataReader.Read())
					{
						LookupInstructor instructorFromReader = LookupInstructorDAO.GetInstructorFromReader(dataReader, "");
						bool flag2 = instructorFromReader != null;
						if (flag2)
						{
							list.Add(instructorFromReader);
						}
					}
					return list;
				}
			}
			return null;
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x00026B64 File Offset: 0x00024D64
		public void UpdateInstructorDataSyncExemption(int InstructorId, bool NewInstructorExemptStatus)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@instructorid", DbType.Int32, InstructorId),
				this.DatabaseManager.GetParameter("@exempt", DbType.Boolean, NewInstructorExemptStatus)
			};
			this.DatabaseManager.ExecuteNonQuery("UPDATE lucoursedata SET exemptfromdatasync=@exempt WHERE lucoursedataid=@instructorid", parameters);
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x00026BC0 File Offset: 0x00024DC0
		public IList<DateTime> GetUniqueCourseRegistrationStartDatesByInstructor(int InstructorId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@instructorid", DbType.Int32, InstructorId)
			};
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT    DISTINCT luc.startdate \r\nFROM        vInstructorList c LEFT JOIN lucourses luc ON luc.lucourseid=c.lucourseid\r\nWHERE       c.instructorid=@instructorid\r\nORDER BY luc.startdate", parameters))
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

		// Token: 0x06000441 RID: 1089 RVA: 0x00026CA0 File Offset: 0x00024EA0
		private StudentWithRequestAndCourseInfo GetStudentWithRequestAndCourseInfoFromRecord(IDataReader record, IBatchDecryptor batchDecryptor = null)
		{
			bool flag = record == null;
			StudentWithRequestAndCourseInfo result;
			if (flag)
			{
				result = null;
			}
			else
			{
				int num = (record["status"] is DBNull) ? 0 : ((int)record["status"]);
				result = new StudentWithRequestAndCourseInfo
				{
					CourseBase = LookupCourseDAO.GetCourseBaseFromReader<LookupCourseBase>("", record),
					Student = PeopleDAO.GetPersonFromReader("", record, this.OpContext, batchDecryptor),
					StudentCourseAccommodationRequestId = ((record["StudentCourseAccommodationRequestId"] is DBNull) ? 0 : ((int)record["StudentCourseAccommodationRequestId"])),
					DateLetterReturned = ((record["DateLetterReturned"] is DBNull) ? null : new DateTime?((DateTime)record["DateLetterReturned"])),
					RequestDate = ((record["daterequested"] is DBNull) ? DateTime.MinValue : ((DateTime)record["daterequested"])),
					Status = (eStudentCourseAccommodationRequestStatus)(Enum.IsDefined(typeof(eStudentCourseAccommodationRequestStatus), num) ? num : 0),
					DateApproved = ((record["dateapproved"] is DBNull) ? null : new DateTime?((DateTime)record["dateapproved"]))
				};
			}
			return result;
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x00026E0C File Offset: 0x0002500C
		public IList<StudentWithRequestAndCourseInfo> GetStudentsWithApprovedRequestsByCourseDate(int InstructorId, int AlternateContactId, DateTime StartDate, DateTime EndDate, int ShowIfActiveAccommodationsExpiry_AccExpiryCid, bool ShowIfLetterGenerated, bool TreatEmptyExpiredDatesAsExpired, bool showifrequestapprovedandaccommsnotexpired)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@iid", DbType.Int32, InstructorId),
				databaseLayer.GetParameter("@altcontactid", DbType.Int32, AlternateContactId),
				databaseLayer.GetParameter("@startdate", DbType.DateTime, StartDate.Date),
				databaseLayer.GetParameter("@enddate", DbType.DateTime, EndDate.Date),
				databaseLayer.GetParameter("@accexpirycid", DbType.Int32, ShowIfActiveAccommodationsExpiry_AccExpiryCid),
				databaseLayer.GetParameter("@emptyexpirymeansexpired", DbType.Boolean, TreatEmptyExpiredDatesAsExpired),
				databaseLayer.GetParameter("@showiflettergenerated", DbType.Boolean, ShowIfLetterGenerated),
				databaseLayer.GetParameter("@showifrequestapprovedandaccommsnotexpired", DbType.Boolean, showifrequestapprovedandaccommsnotexpired)
			};
			List<StudentWithRequestAndCourseInfo> list = new List<StudentWithRequestAndCourseInfo>();
			using (IDataReader dataReader = databaseLayer.ExecuteStoredProcedureReader("sp_Instructor_LoadStudentsAndCoursesWithSelfRegApproved", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					return null;
				}
				IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
				while (dataReader.Read())
				{
					StudentWithRequestAndCourseInfo studentWithRequestAndCourseInfoFromRecord = this.GetStudentWithRequestAndCourseInfoFromRecord(dataReader, batchDecryptor);
					bool flag2 = studentWithRequestAndCourseInfoFromRecord != null;
					if (flag2)
					{
						list.Add(studentWithRequestAndCourseInfoFromRecord);
					}
				}
			}
			return list;
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x00026F7C File Offset: 0x0002517C
		public int[] FindAllCoursesAnInstructorOrAltContactIsAllowed(int instructorId, int altContactId, int permissionLevel)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@iid", DbType.Int32, instructorId),
				databaseLayer.GetParameter("@altid", DbType.Int32, altContactId),
				databaseLayer.GetParameter("@permissionlevel", DbType.Int32, permissionLevel)
			};
			int[] result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT DISTINCT lucourseid FROM \r\n(\r\nSELECT\tlucourseid FROM lucourseinstructor WHERE instructorid=@iid\r\nUNION ALL\r\nSELECT\tv.lucourseid\r\nFROM\tvAlternateContactList v LEFT JOIN lucoursealternatecontact alt ON alt.alternatecontactid=v.alternatecontactid\r\nWHERE\tv.alternatecontactid=@altid \r\n\t\tAND (@permissionlevel=-1 OR (alt.altpermissionlevel & @permissionlevel)>0)\r\n) x", parameters))
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
						int num = (dataReader["lucourseid"] is DBNull) ? 0 : ((int)dataReader["lucourseid"]);
						bool flag2 = num < 1;
						if (!flag2)
						{
							list.Add(num);
						}
					}
					result = list.ToArray();
				}
			}
			return result;
		}
	}
}
