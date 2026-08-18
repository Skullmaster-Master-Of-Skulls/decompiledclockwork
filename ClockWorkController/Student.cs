using System;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Web.Caching;
using ClockWorkWebAPI;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.Public.Entities.Settings;

namespace ClockWorkController
{
	// Token: 0x0200000E RID: 14
	public class Student
	{
		// Token: 0x06000069 RID: 105 RVA: 0x00004E00 File Offset: 0x00003000
		public static Exception ActivateStudent(int pid)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			Exception result;
			try
			{
				DbParameter[] array = new DbParameter[2];
				array[0] = clockWork.Parameter;
				array[0].ParameterName = "@pid";
				array[0].DbType = DbType.Int32;
				array[0].Value = pid;
				array[1] = clockWork.Parameter;
				array[1].ParameterName = "@now";
				array[1].DbType = DbType.DateTime;
				array[1].Value = DateTime.Now;
				clockWork.ExecuteNonQuery(QueryStorage.QS_INSERT_ActivateStudent, array);
				result = null;
			}
			catch (Exception ex)
			{
				result = ex;
			}
			return result;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00004EAC File Offset: 0x000030AC
		public static int CreateUser(string snum, string fn, string mn, string ln, string groupIds, db conn)
		{
			string[] array = groupIds.Split(new char[]
			{
				','
			});
			int[] array2 = new int[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = int.Parse(array[i]);
			}
			return Student.CreateUser(snum, fn, mn, ln, array2);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00004F04 File Offset: 0x00003104
		public static int CreateUser(string snum, string fn, string mn, string ln, int[] groupIds)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			IEncryption encryption = clockWork.Encryption;
			byte[] value = encryption.Encrypt(snum);
			byte[] value2 = encryption.Encrypt(fn);
			byte[] value3 = encryption.Encrypt(ln);
			byte[] value4 = encryption.Encrypt(mn);
			int result;
			try
			{
				DbParameter[] parameters = new DbParameter[]
				{
					clockWork.GetParameter("@sne", DbType.Binary, value),
					clockWork.GetParameter("@mne", DbType.Binary, value4),
					clockWork.GetParameter("@lne", DbType.Binary, value3),
					clockWork.GetParameter("@fne", DbType.Binary, value2),
					clockWork.GetParameter("@now", DbType.DateTime, DateTime.Now),
					clockWork.GetParameter("@true", DbType.Boolean, true)
				};
				DataTable dataTable = clockWork.ExecuteQuery(QueryStorage.QS_INSERT_PersonRow, parameters);
				bool flag = dataTable.Rows.Count > 0 && dataTable.Rows[0][0] != DBNull.Value;
				int num;
				if (flag)
				{
					num = (int)dataTable.Rows[0][0];
				}
				else
				{
					num = 0;
				}
				bool flag2 = num > 0;
				if (!flag2)
				{
					throw new Exception("Error creating user");
				}
				bool flag3 = string.IsNullOrEmpty(snum);
				if (flag3)
				{
					parameters = new DbParameter[]
					{
						clockWork.GetParameter("@sne", DbType.Binary, encryption.Encrypt("user" + num.ToString())),
						clockWork.GetParameter("@pid", DbType.Int32, num)
					};
					clockWork.ExecuteNonQuery(QueryStorage.QS_INSERT_PersonRow, parameters);
				}
				for (int i = 0; i < groupIds.Length; i++)
				{
					bool flag4 = groupIds[i] == 1 || groupIds[i] == 2;
					bool flag5 = flag4;
					if (flag5)
					{
					}
					parameters = new DbParameter[]
					{
						clockWork.GetParameter("@pid", DbType.Int32, num),
						clockWork.GetParameter("@gid", DbType.Int32, groupIds[i]),
						clockWork.GetParameter("@primarygroup", DbType.Boolean, flag4)
					};
					clockWork.ExecuteNonQuery(QueryStorage.QS_INSERT_AddUserToGroup, parameters);
				}
				result = num;
			}
			catch (Exception ex)
			{
				result = 0;
			}
			finally
			{
				try
				{
				}
				catch
				{
				}
			}
			return result;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000051B4 File Offset: 0x000033B4
		public static string LookupEmail(int pid, int emailCid, bool emailEncrypted)
		{
			return Student.LookupEmail(pid, emailCid, emailEncrypted);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000051D0 File Offset: 0x000033D0
		public static string LookupEmail(int pid, int emailCid, bool emailEncrypted, out PersonBaseDTO studentNameAndNumber)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			IEncryption encryption = clockWork.Encryption;
			DbParameter[] parameters = new DbParameter[]
			{
				clockWork.GetParameter("@pid", DbType.Int32, pid),
				clockWork.GetParameter("@cid", DbType.Int32, emailCid)
			};
			DataTable dataTable = clockWork.ExecuteQuery(QueryStorage.QS_Select_DynamicStringData, parameters);
			bool flag = dataTable.Rows.Count > 0 && dataTable.Rows[0][0] != DBNull.Value;
			string result;
			if (flag)
			{
				DataRow dataRow = dataTable.Rows[0];
				if (emailEncrypted)
				{
					dataTable = encryption.EncryptOrDecryptNameDataTableBatch(false, dataTable, new string[]
					{
						"firstname",
						"lastname",
						"student_no",
						"controlvalue"
					});
					studentNameAndNumber = new PersonBaseDTO
					{
						PersonId = pid,
						FirstName = dataRow["firstname"].ToString(),
						MiddleName = "",
						LastName = dataRow["lastname"].ToString(),
						Student_no = dataRow["student_no"].ToString(),
						CoreGroup = eCoreGroupDTO.Students
					};
					result = dataRow["controlvalue"].ToString();
				}
				else
				{
					dataTable = encryption.EncryptOrDecryptNameDataTableBatch(false, dataTable, new string[]
					{
						"firstname",
						"lastname",
						"student_no"
					});
					studentNameAndNumber = new PersonBaseDTO
					{
						PersonId = pid,
						FirstName = dataRow["firstname"].ToString(),
						MiddleName = "",
						LastName = dataRow["lastname"].ToString(),
						Student_no = dataRow["student_no"].ToString(),
						CoreGroup = eCoreGroupDTO.Students
					};
					result = Core.BytesToString((byte[])dataTable.Rows[0][0], false, encryption);
				}
			}
			else
			{
				studentNameAndNumber = null;
				result = "";
			}
			return result;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000053F4 File Offset: 0x000035F4
		public static Exception AddRowToListView(int pid, int cid, params string[] cellData)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			IEncryption encryption = clockWork.Encryption;
			DbParameter[] array = new DbParameter[2];
			array[0] = clockWork.Parameter;
			array[0].ParameterName = "@pid";
			array[0].DbType = DbType.Int32;
			array[0].Value = pid;
			array[1] = clockWork.Parameter;
			array[1].ParameterName = "@cid";
			array[1].DbType = DbType.Int32;
			array[1].Value = cid;
			Exception result;
			try
			{
				DataTable dataTable = clockWork.ExecuteQuery(QueryStorage.QS_Select_DynamicStringData, array);
				bool flag = dataTable.Rows.Count > 0;
				string text;
				if (flag)
				{
					byte[] bytes = (byte[])dataTable.Rows[0][0];
					UTF8Encoding utf8Encoding = new UTF8Encoding();
					text = utf8Encoding.GetString(bytes);
				}
				else
				{
					text = "";
				}
				text = text.Trim();
				bool flag2 = text.Length > 0;
				if (flag2)
				{
					text += "\t";
				}
				for (int i = 0; i < cellData.Length; i++)
				{
					text += cellData[i];
					text += "\0";
				}
				text += DateTime.Now.ToString("yyyy-MM-dd hh:mm tt");
				UTF8Encoding utf8Encoding2 = new UTF8Encoding();
				byte[] bytes2 = utf8Encoding2.GetBytes(text);
				array = new DbParameter[3];
				array[0] = clockWork.Parameter;
				array[0].ParameterName = "@pid";
				array[0].DbType = DbType.Int32;
				array[0].Value = pid;
				array[1] = clockWork.Parameter;
				array[1].ParameterName = "@cid";
				array[1].DbType = DbType.Int32;
				array[1].Value = cid;
				array[1] = clockWork.Parameter;
				array[1].ParameterName = "@cv";
				array[1].DbType = DbType.Binary;
				array[1].Value = bytes2;
				clockWork.ExecuteNonQuery(QueryStorage.QS_UPDATE_UpdateDynamicDataPSOtherInfo1, array);
				clockWork.ExecuteNonQuery(QueryStorage.QS_INSERT_UpdateDynamicDataPSOtherInfo2, array);
				result = null;
			}
			catch (Exception ex)
			{
				result = ex;
			}
			return result;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00005640 File Offset: 0x00003840
		[Obsolete("")]
		public static DateTime BanStudent(int banCid, int pid, Cache Cache)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			IEncryption encryption = clockWork.Encryption;
			bool flag = banCid > 0;
			DateTime result;
			if (flag)
			{
				IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
				int settingValue = webSettingsClientManager.GetSettingValue<int>(Setting.APPOINTMENTBOOKING_bannedNumDays);
				DateTime dateTime = DateTime.Now.AddDays((double)settingValue);
				DbParameter[] array = new DbParameter[3];
				array[0] = clockWork.Parameter;
				array[0].ParameterName = "@pid";
				array[0].DbType = DbType.Int32;
				array[0].Value = pid;
				array[1] = clockWork.Parameter;
				array[1].ParameterName = "@cid";
				array[1].DbType = DbType.Int32;
				array[1].Value = banCid;
				array[2] = clockWork.Parameter;
				array[2].ParameterName = "@cv";
				array[2].DbType = DbType.DateTime;
				array[2].Value = dateTime;
				clockWork.ExecuteQuery(QueryStorage.QS_UPDATE_UpdateDynamicDataPSDateTime1, array);
				clockWork.ExecuteQuery(QueryStorage.QS_INSERT_UpdateDynamicDataPSDateTime2, array);
				result = dateTime;
			}
			else
			{
				result = DateTime.MinValue;
			}
			return result;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00005764 File Offset: 0x00003964
		public static Student LoadStudent(string student_no)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			IEncryption encryption = clockWork.Encryption;
			string query = "DECLARE @emailcid int\r\nSET @emailcid = (SELECT settingvalue FROM settingsgroups WHERE groupid=-1 AND settingcode=260)\r\n\r\nDECLARE @counsellorcid int\r\nSET @counsellorcid = (SELECT settingvalue FROM settingsgroups WHERE groupid=-1 AND settingcode=99671)\r\n\r\nDECLARE @counselloremailcid varchar(max)\r\nSET @counselloremailcid = (SELECT settingvalue FROM settingsgroups WHERE groupid=-1 AND settingcode=428)\r\n\r\nSELECT    p.firstname,p.middlename,p.lastname,p.student_no,p.personid\r\n          ,oi.valtext AS emailvaltext,oi.valbytes AS emailvalbytes\r\n          ,ps2.valbytes AS counsellorname\r\n          ,ois.valtext AS counselloremailvaltext,ois.valbytes AS counselloremailvalbytes,ois.valbytesisencrypted\r\nFROM        people p LEFT JOIN perstudentdata2 oi ON oi.personid=p.personid AND oi.controlid=@emailcid\r\n            LEFT JOIN perstudentdata2 ps2 ON ps2.personid=p.personid AND ps2.controlid=@counsellorcid\r\n            LEFT JOIN perstudentdata2 ois ON ois.controlid=@counselloremailcid AND ois.personid=ps2.valint\r\nWHERE       p.student_no=@sne AND p.isactive=1";
			byte[] value = encryption.Encrypt(student_no.ToUpper().Trim());
			DataTable dataTable = clockWork.ExecuteQuery(query, new DbParameter[]
			{
				clockWork.GetParameter("@sne", DbType.Binary, value)
			});
			bool flag = dataTable.Rows.Count > 0;
			Student result;
			if (flag)
			{
				dataTable = encryption.EncryptOrDecryptNameDataTableBatch(false, dataTable, new string[]
				{
					"firstname",
					"middlename",
					"lastname",
					"student_no",
					"counsellorname"
				});
				Student student = new Student();
				DataRow dataRow = dataTable.Rows[0];
				student.PersonId = (int)dataRow["personid"];
				student.FirstName = dataRow["firstname"].ToString();
				student.MiddleName = dataRow["middlename"].ToString();
				student.LastName = dataRow["lastname"].ToString();
				student.Student_no = dataRow["student_no"].ToString();
				student.Email = dataRow["emailvaltext"].ToString();
				student.CounsellorName = dataRow["counsellorname"].ToString();
				bool flag2 = dataRow["valbytesisencrypted"] != DBNull.Value && Convert.ToBoolean(dataRow["valbytesisencrypted"]);
				bool flag3 = flag2;
				if (flag3)
				{
					byte[] array = (dataRow["counselloremailvalbytes"] == DBNull.Value) ? new byte[0] : ((byte[])dataRow["counselloremailvalbytes"]);
					student.CounsellorEmail = ((array.Length != 0) ? encryption.Decrypt(array) : "");
				}
				else
				{
					student.CounsellorEmail = dataRow["counselloremailvaltext"].ToString();
				}
				bool flag4 = string.IsNullOrEmpty(student.Email) && dataRow["emailvalbytes"] != DBNull.Value;
				if (flag4)
				{
					student.Email = encryption.Decrypt((byte[])dataRow["emailvalbytes"]);
				}
				result = student;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000059C4 File Offset: 0x00003BC4
		public static Student LoadStudent(int pid)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			IEncryption encryption = clockWork.Encryption;
			string query = "DECLARE @emailcid int\r\nSET @emailcid = (SELECT settingvalue FROM settingsgroups WHERE groupid=-1 AND settingcode=260)\r\n\r\nDECLARE @counsellorcid int\r\nSET @counsellorcid = (SELECT settingvalue FROM settingsgroups WHERE groupid=-1 AND settingcode=99671)\r\n\r\nDECLARE @counselloremailcid varchar(max)\r\nSET @counselloremailcid = (SELECT settingvalue FROM settingsgroups WHERE groupid=-1 AND settingcode=428)\r\n\r\nSELECT    p.firstname,p.middlename,p.lastname,p.student_no,p.personid\r\n          ,oi.valtext AS emailvaltext,oi.valbytes AS emailvalbytes\r\n          ,ps2.valbytes AS counsellorname\r\n          ,ois.valtext AS counselloremailvaltext,ois.valbytes AS counselloremailvalbytes,ois.valbytesisencrypted\r\nFROM        people p LEFT JOIN perstudentdata2 oi ON oi.personid=p.personid AND oi.controlid=@emailcid\r\n            LEFT JOIN perstudentdata2 ps2 ON ps2.personid=p.personid AND ps2.controlid=@counsellorcid\r\n            LEFT JOIN perstudentdata2 ois ON ois.controlid=@counselloremailcid AND ois.personid=ps2.valint\r\nWHERE       p.personid=@pid AND p.isactive=1";
			DataTable dataTable = clockWork.ExecuteQuery(query, new DbParameter[]
			{
				clockWork.GetParameter("@pid", DbType.Int32, pid)
			});
			bool flag = dataTable.Rows.Count > 0;
			Student result;
			if (flag)
			{
				dataTable = encryption.EncryptOrDecryptNameDataTableBatch(false, dataTable, new string[]
				{
					"firstname",
					"middlename",
					"lastname",
					"student_no",
					"counsellorname"
				});
				Student student = new Student();
				DataRow dataRow = dataTable.Rows[0];
				student.PersonId = (int)dataRow["personid"];
				student.FirstName = dataRow["firstname"].ToString();
				student.MiddleName = dataRow["middlename"].ToString();
				student.LastName = dataRow["lastname"].ToString();
				student.Student_no = dataRow["student_no"].ToString();
				student.Email = dataRow["emailvaltext"].ToString();
				student.CounsellorName = dataRow["counsellorname"].ToString();
				bool flag2 = dataRow["valbytesisencrypted"] != DBNull.Value && Convert.ToBoolean(dataRow["valbytesisencrypted"]);
				bool flag3 = flag2;
				if (flag3)
				{
					byte[] array = (dataRow["counselloremailvalbytes"] == DBNull.Value) ? new byte[0] : ((byte[])dataRow["counselloremailvalbytes"]);
					student.CounsellorEmail = ((array.Length != 0) ? encryption.Decrypt(array) : "");
				}
				else
				{
					student.CounsellorEmail = dataRow["counselloremailvaltext"].ToString();
				}
				bool flag4 = string.IsNullOrEmpty(student.Email) && dataRow["emailvalbytes"] != DBNull.Value;
				if (flag4)
				{
					student.Email = encryption.Decrypt((byte[])dataRow["emailvalbytes"]);
				}
				result = student;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00005C18 File Offset: 0x00003E18
		public static Student LoadStudent(int perStudentFieldTextControlId, string controlValue)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			IEncryption encryption = clockWork.Encryption;
			string query = "DECLARE @emailcid int\r\nSET @emailcid = (SELECT settingvalue FROM settingsgroups WHERE groupid=-1 AND settingcode=260)\r\n\r\nDECLARE @counsellorcid int\r\nSET @counsellorcid = (SELECT settingvalue FROM settingsgroups WHERE groupid=-1 AND settingcode=99671)\r\n\r\nDECLARE @counselloremailcid varchar(max)\r\nSET @counselloremailcid = (SELECT settingvalue FROM settingsgroups WHERE groupid=-1 AND settingcode=428)\r\n\r\nSELECT    p.firstname,p.middlename,p.lastname,p.student_no,p.personid\r\n          ,oi.valtext AS emailvaltext,oi.valbytes AS emailvalbytes\r\n          ,ps2.valbytes AS counsellorname\r\n          ,ois.valtext AS counselloremailvaltext,ois.valbytes AS counselloremailvalbytes,ois.valbytesisencrypted\r\nFROM        perstudentdata2 ps1 LEFT JOIN people p ON p.personid=ps1.personid AND ps1.controlid=@cid\r\n            LEFT JOIN perstudentdata2 oi ON oi.personid=p.personid AND oi.controlid=@emailcid\r\n            LEFT JOIN perstudentdata2 ps2 ON ps2.personid=p.personid AND ps2.controlid=@counsellorcid\r\n            LEFT JOIN perstudentdata2 ois ON ois.controlid=@counselloremailcid AND ois.personid=ps2.valint\r\nWHERE       ps1.controlid=@cid AND (ps1.valbytes=@vale1 OR ps1.valbytes=@vale2 OR ps1.valbytes=@vale3 OR ps1.valtext=@val) AND p.isactive=1";
			byte[] array = encryption.Encrypt(controlValue.ToUpper().Trim());
			DbParameter[] parameters = new DbParameter[]
			{
				clockWork.GetParameter("@vale1", DbType.Binary, encryption.Encrypt(controlValue)),
				clockWork.GetParameter("@vale2", DbType.Binary, encryption.Encrypt(controlValue.ToUpper().Trim())),
				clockWork.GetParameter("@vale3", DbType.Binary, encryption.Encrypt(controlValue.ToLower().Trim())),
				clockWork.GetParameter("@val", DbType.String, controlValue),
				clockWork.GetParameter("@cid", DbType.Int32, perStudentFieldTextControlId)
			};
			DataTable dataTable = clockWork.ExecuteQuery(query, parameters);
			bool flag = dataTable.Rows.Count > 0;
			Student result;
			if (flag)
			{
				dataTable = encryption.EncryptOrDecryptNameDataTableBatch(false, dataTable, new string[]
				{
					"firstname",
					"middlename",
					"lastname",
					"student_no",
					"counsellorname"
				});
				Student student = new Student();
				DataRow dataRow = dataTable.Rows[0];
				student.PersonId = (int)dataRow["personid"];
				student.FirstName = dataRow["firstname"].ToString();
				student.MiddleName = dataRow["middlename"].ToString();
				student.LastName = dataRow["lastname"].ToString();
				student.Student_no = dataRow["student_no"].ToString();
				student.Email = dataRow["emailvaltext"].ToString();
				student.CounsellorName = dataRow["counsellorname"].ToString();
				bool flag2 = dataRow["valbytesisencrypted"] != DBNull.Value && Convert.ToBoolean(dataRow["valbytesisencrypted"]);
				bool flag3 = flag2;
				if (flag3)
				{
					byte[] array2 = (dataRow["counselloremailvalbytes"] == DBNull.Value) ? new byte[0] : ((byte[])dataRow["counselloremailvalbytes"]);
					student.CounsellorEmail = ((array2.Length != 0) ? encryption.Decrypt(array2) : "");
				}
				else
				{
					student.CounsellorEmail = dataRow["counselloremailvaltext"].ToString();
				}
				bool flag4 = string.IsNullOrEmpty(student.Email) && dataRow["emailvalbytes"] != DBNull.Value;
				if (flag4)
				{
					student.Email = encryption.Decrypt((byte[])dataRow["emailvalbytes"]);
				}
				result = student;
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}
