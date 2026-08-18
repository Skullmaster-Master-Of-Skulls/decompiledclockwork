using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Databases;
using TechnoPro.Common.DAO.Impl.People;
using TechnoPro.Common.DAO.LookupCourses;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.LookupCourses;

namespace TechnoPro.Common.DAO.Impl.LookupCourses
{
	// Token: 0x02000098 RID: 152
	public class AlternateContactDAO : IAlternateContactDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060003E7 RID: 999 RVA: 0x000221E0 File Offset: 0x000203E0
		public AlternateContactDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext2 = this.OpContext;
			this.DatabaseManager = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext2 != null) ? opContext2.TenantId : null);
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060003E8 RID: 1000 RVA: 0x00022210 File Offset: 0x00020410
		// (set) Token: 0x060003E9 RID: 1001 RVA: 0x00022218 File Offset: 0x00020418
		public OperationContext OpContext { get; set; }

		// Token: 0x060003EA RID: 1002 RVA: 0x00022224 File Offset: 0x00020424
		private bool ReaderContainsColumn(IDataReader reader, string colName)
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

		// Token: 0x060003EB RID: 1003 RVA: 0x00022264 File Offset: 0x00020464
		internal static AlternateContact GetAlternateContactFromRecord(string prefix, IDataReader record)
		{
			bool flag = record == null;
			AlternateContact result;
			if (flag)
			{
				result = null;
			}
			else
			{
				string text = string.Format("{0}alternatecontactid", prefix);
				bool flag2 = !PeopleDAO.ReaderContainsColumn(record, text) || record[text] == DBNull.Value;
				if (flag2)
				{
					result = null;
				}
				else
				{
					int alternateContactId = (int)record[text];
					string text2 = prefix + "altpermissionlevel";
					bool flag3 = PeopleDAO.ReaderContainsColumn(record, text2);
					int permissionLevel;
					if (flag3)
					{
						permissionLevel = ((record[text2] == DBNull.Value) ? 0 : ((int)record[text2]));
					}
					else
					{
						permissionLevel = 0;
					}
					string text3 = prefix + "externalid";
					bool flag4 = PeopleDAO.ReaderContainsColumn(record, text3);
					string employeeId;
					if (flag4)
					{
						employeeId = record[text3].ToString();
					}
					else
					{
						text3 = prefix + "employeeid";
						bool flag5 = PeopleDAO.ReaderContainsColumn(record, text3);
						if (flag5)
						{
							employeeId = record[text3].ToString();
						}
						else
						{
							employeeId = "";
						}
					}
					result = new AlternateContact
					{
						AlternateContactId = alternateContactId,
						Name = record[prefix + "altname"].ToString(),
						Email = record[prefix + "altemail"].ToString(),
						Phone = record[prefix + "altphone"].ToString(),
						Username = record[prefix + "altusername"].ToString(),
						EmployeeId = employeeId,
						PermissionLevel = permissionLevel
					};
				}
			}
			return result;
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x000223FC File Offset: 0x000205FC
		public int CreateAlternateContact(AlternateContact AltContact)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@name", DbType.String, AltContact.Name),
				this.DatabaseManager.GetParameter("@email", DbType.String, AltContact.Email ?? ""),
				this.DatabaseManager.GetParameter("@phone", DbType.String, AltContact.Phone ?? ""),
				this.DatabaseManager.GetParameter("@username", DbType.String, AltContact.Username ?? ""),
				this.DatabaseManager.GetParameter("@externalid", DbType.String, AltContact.EmployeeId ?? ""),
				this.DatabaseManager.GetParameter("@permissionlevel", DbType.Int32, AltContact.PermissionLevel),
				this.DatabaseManager.GetParameter("@whocreated", DbType.Int32, this.OpContext.WhoAmI)
			};
			object obj = this.DatabaseManager.ExecuteScalar("INSERT INTO lucoursealternatecontact(altname,altemail,altphone,altusername,altpermissionlevel,whocreated,externalid) \r\nVALUES \r\n(@name,@email,@phone,@username,@permissionlevel,@whocreated,@externalid);\r\nSELECT CAST(SCOPE_IDENTITY() AS int) AS alternatecontactid", parameters);
			AltContact.AlternateContactId = (int)obj;
			return AltContact.AlternateContactId;
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x0002252C File Offset: 0x0002072C
		public AlternateContact LoadAlternateContactById(int AlternateContactId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@id", DbType.Int32, AlternateContactId)
			};
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT    ac.alternatecontactid,ac.altname,ac.altemail,ac.altphone,ac.altusername,ac.altpermissionlevel,\r\n            ac.whocreated AS whocreatedpersonid,pwho.firstname AS whocreatedfirstname,\r\n            pwho.lastname AS whocreatedlastname,pwho.student_no AS whocreatedstudent_no,\r\n            ac.isactive,ac.externalid\r\nFROM        lucoursealternatecontact ac LEFT JOIN people pwho ON pwho.personid=ac.whocreated\r\nWHERE       ac.alternatecontactid=@id", parameters))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					return AlternateContactDAO.GetAlternateContactFromRecord("", dataReader);
				}
			}
			return null;
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x000225B4 File Offset: 0x000207B4
		public IList<AlternateContact> LoadAlternateContactsByCourse(int LuCourseId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@lucid", DbType.Int32, LuCourseId)
			};
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT    ac.alternatecontactid,ac.altname,ac.altemail,ac.altphone,ac.altusername,ac.altpermissionlevel,\r\n            ac.whocreated AS whocreatedpersonid,pwho.firstname AS whocreatedfirstname,\r\n            pwho.lastname AS whocreatedlastname,pwho.student_no AS whocreatedstudent_no,\r\n            ac.isactive,ac.externalid\r\nFROM        lucoursealternatecontact ac LEFT JOIN people pwho ON pwho.personid=ac.whocreated\r\nWHERE       ac.alternatecontactid IN (SELECT alternatecontactid FROM lucourses WHERE lucourseid=@lucid)\r\n            OR ac.alternatecontactid IN (SELECT alternatecontactid FROM lucoursealtcontact WHERE lucourseid=@lucid)\r\nORDER BY    ac.altname", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					List<AlternateContact> list = new List<AlternateContact>();
					while (dataReader.Read())
					{
						AlternateContact alternateContactFromRecord = AlternateContactDAO.GetAlternateContactFromRecord("", dataReader);
						bool flag2 = alternateContactFromRecord != null;
						if (flag2)
						{
							list.Add(alternateContactFromRecord);
						}
					}
					return list;
				}
			}
			return null;
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x00022664 File Offset: 0x00020864
		public IList<AlternateContact> LoadAlternateContactsBySearchString(string SearchString)
		{
			bool flag = SearchString == null || SearchString.Trim().Length < 1;
			IList<AlternateContact> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				DbParameter[] parameters = new DbParameter[]
				{
					this.DatabaseManager.GetParameter("@searchstring", DbType.String, string.Format("%{0}%", SearchString))
				};
				using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT    ac.alternatecontactid,ac.altname,ac.altemail,ac.altphone,ac.altusername,ac.altpermissionlevel,\r\n            ac.whocreated AS whocreatedpersonid,pwho.firstname AS whocreatedfirstname,\r\n            pwho.lastname AS whocreatedlastname,pwho.student_no AS whocreatedstudent_no,\r\n            ac.isactive,ac.externalid\r\nFROM        lucoursealternatecontact ac LEFT JOIN people pwho ON pwho.personid=ac.whocreated\r\nWHERE       altname LIKE @searchstring OR altemail LIKE @searchstring OR altphone LIKE @searchstring\r\n            OR altusername LIKE @searchstring OR externalid LIKE @searchstring\r\nORDER BY    ac.altname", parameters))
				{
					bool flag2 = dataReader != null;
					if (flag2)
					{
						List<AlternateContact> list = new List<AlternateContact>();
						while (dataReader.Read())
						{
							AlternateContact alternateContactFromRecord = AlternateContactDAO.GetAlternateContactFromRecord("", dataReader);
							bool flag3 = alternateContactFromRecord != null;
							if (flag3)
							{
								list.Add(alternateContactFromRecord);
							}
						}
						return list;
					}
				}
				result = null;
			}
			return result;
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x00022738 File Offset: 0x00020938
		public void UpdateAlternateContact(AlternateContact AltContact)
		{
			bool flag = string.IsNullOrEmpty(AltContact.Name);
			DbParameter parameter;
			if (flag)
			{
				parameter = this.DatabaseManager.GetParameter("@name", DbType.String, DBNull.Value);
			}
			else
			{
				parameter = this.DatabaseManager.GetParameter("@name", DbType.String, AltContact.Name);
			}
			bool flag2 = string.IsNullOrEmpty(AltContact.Username);
			DbParameter parameter2;
			if (flag2)
			{
				parameter2 = this.DatabaseManager.GetParameter("@username", DbType.String, DBNull.Value);
			}
			else
			{
				parameter2 = this.DatabaseManager.GetParameter("@username", DbType.String, AltContact.Username);
			}
			bool flag3 = AltContact.Email == null;
			DbParameter parameter3;
			if (flag3)
			{
				parameter3 = this.DatabaseManager.GetParameter("@email", DbType.String, DBNull.Value);
			}
			else
			{
				parameter3 = this.DatabaseManager.GetParameter("@email", DbType.String, AltContact.Email);
			}
			bool flag4 = AltContact.Phone == null;
			DbParameter parameter4;
			if (flag4)
			{
				parameter4 = this.DatabaseManager.GetParameter("@phone", DbType.String, DBNull.Value);
			}
			else
			{
				parameter4 = this.DatabaseManager.GetParameter("@phone", DbType.String, AltContact.Phone);
			}
			bool flag5 = AltContact.EmployeeId == null;
			DbParameter parameter5;
			if (flag5)
			{
				parameter5 = this.DatabaseManager.GetParameter("@externalid", DbType.String, DBNull.Value);
			}
			else
			{
				parameter5 = this.DatabaseManager.GetParameter("@externalid", DbType.String, AltContact.EmployeeId);
			}
			bool flag6 = AltContact.PermissionLevel < 0;
			DbParameter parameter6;
			if (flag6)
			{
				parameter6 = this.DatabaseManager.GetParameter("@permissionlevel", DbType.Int32, DBNull.Value);
			}
			else
			{
				parameter6 = this.DatabaseManager.GetParameter("@permissionlevel", DbType.Int32, AltContact.PermissionLevel);
			}
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@id", DbType.Int32, AltContact.AlternateContactId),
				parameter,
				parameter3,
				parameter4,
				parameter2,
				parameter6,
				this.DatabaseManager.GetParameter("@isactive", DbType.Boolean, true),
				parameter5
			};
			this.DatabaseManager.ExecuteNonQuery("UPDATE lucoursealternatecontact SET \r\n        altname=COALESCE(@name,altname),altemail=COALESCE(@email,altemail),altphone=COALESCE(@phone,altphone),\r\n        altusername=COALESCE(@username,altusername),altpermissionlevel=COALESCE(@permissionlevel,altpermissionlevel),\r\n        isactive=COALESCE(@isactive,isactive),externalid=COALESCE(@externalid,externalid)\r\nWHERE   alternatecontactid=@id", parameters);
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x00022950 File Offset: 0x00020B50
		public void DeleteAlternateContact(int AlternateContactId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@id", DbType.Int32, AlternateContactId)
			};
			this.DatabaseManager.ExecuteNonQuery("DELETE FROM lucoursealternatecontact WHERE alternatecontactid=@id", parameters);
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x00022994 File Offset: 0x00020B94
		public AlternateContact LoadAlternateContactByUsername(string Username)
		{
			bool flag = Username == null || Username.Trim().Length < 1;
			AlternateContact result;
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
				using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT    ac.alternatecontactid,ac.altname,ac.altemail,ac.altphone,ac.altusername,ac.altpermissionlevel,\r\n            ac.whocreated AS whocreatedpersonid,pwho.firstname AS whocreatedfirstname,\r\n            pwho.lastname AS whocreatedlastname,pwho.student_no AS whocreatedstudent_no,\r\n            ac.isactive,ac.externalid\r\nFROM        lucoursealternatecontact ac LEFT JOIN people pwho ON pwho.personid=ac.whocreated\r\nWHERE       ac.altusername=@username", parameters))
				{
					bool flag2 = dataReader != null && dataReader.Read();
					if (flag2)
					{
						return AlternateContactDAO.GetAlternateContactFromRecord("", dataReader);
					}
				}
				result = null;
			}
			return result;
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x00022A34 File Offset: 0x00020C34
		public void AssignAlternateContactToCourse(int AlternateContactId, int LuCourseId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@alternatecontactid", DbType.Int32, AlternateContactId),
				this.DatabaseManager.GetParameter("@lucid", DbType.Int32, LuCourseId)
			};
			this.DatabaseManager.ExecuteNonQuery("IF NOT EXISTS(SELECT lucourseid FROM lucourses WHERE lucourseid=@lucid AND alternatecontactid=@alternatecontactid)\r\n    AND NOT EXISTS(SELECT lucourseid FROM lucoursealtcontact WHERE lucourseid=@lucid AND alternatecontactid=@alternatecontactid)\r\nBEGIN\r\n    IF EXISTS(SELECT lucourseid FROM lucourses WHERE lucourseid=@lucid AND alternatecontactid>0)\r\n        INSERT INTO lucoursealtcontact (lucourseid,alternatecontactid) VALUES (@lucid,@alternatecontactid)\r\n    ELSE\r\n        UPDATE lucourses SET alternatecontactid=@alternatecontactid WHERE lucourseid=@lucid\r\nEND", parameters);
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x00022A94 File Offset: 0x00020C94
		public void RemoveAlternateContactFromCourse(int AlternateContactId, int LuCourseId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@alternatecontactid", DbType.Int32, AlternateContactId),
				this.DatabaseManager.GetParameter("@lucid", DbType.Int32, LuCourseId)
			};
			this.DatabaseManager.ExecuteNonQuery("DELETE FROM lucoursealtcontact WHERE lucourseid=@lucid AND alternatecontactid=@alternatecontactid\r\nUPDATE LUCourses SET alternatecontactid=-1 WHERE LUCourseID=@lucid AND alternatecontactid=@alternatecontactid\r\n\r\nIF EXISTS(SELECT lucourseid FROM LUCourses WHERE LUCourseID=@lucid AND alternatecontactid=-1)\r\n\tAND EXISTS(SELECT lucourseid FROM lucoursealtcontact WHERE lucourseid=@lucid)\r\nBEGIN\r\n    DECLARE @acid int\r\n    SET @acid=(SELECT TOP 1 alternatecontactid FROM lucoursealtcontact WHERE lucourseid=@lucid)\r\n    UPDATE lucourses SET alternatecontactid=@acid WHERE lucourseid=@lucid\r\n    DELETE FROM lucoursealtcontact WHERE lucourseid=@lucid AND alternatecontactid=@acid\r\nEND", parameters);
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x00022AF4 File Offset: 0x00020CF4
		public AlternateContact LoadAlternateContactByEmployeeId(string EmployeeId)
		{
			bool flag = EmployeeId == null || EmployeeId.Trim().Length < 1;
			AlternateContact result;
			if (flag)
			{
				result = null;
			}
			else
			{
				DbParameter[] parameters = new DbParameter[]
				{
					this.DatabaseManager.GetParameter("@employeeid", DbType.String, EmployeeId)
				};
				using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT    ac.alternatecontactid,ac.altname,ac.altemail,ac.altphone,ac.altusername,ac.altpermissionlevel,\r\n            ac.whocreated AS whocreatedpersonid,pwho.firstname AS whocreatedfirstname,\r\n            pwho.lastname AS whocreatedlastname,pwho.student_no AS whocreatedstudent_no,\r\n            ac.isactive,ac.externalid\r\nFROM        lucoursealternatecontact ac LEFT JOIN people pwho ON pwho.personid=ac.whocreated\r\nWHERE       ac.externalid=@employeeid", parameters))
				{
					bool flag2 = dataReader != null && dataReader.Read();
					if (flag2)
					{
						return AlternateContactDAO.GetAlternateContactFromRecord("", dataReader);
					}
				}
				result = null;
			}
			return result;
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x00022B94 File Offset: 0x00020D94
		public IList<DateTime> GetUniqueCourseRegistrationStartDatesByAlternateContact(int AlternateContactId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@alternatecontactid", DbType.Int32, AlternateContactId)
			};
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT    DISTINCT luc.startdate \r\nFROM        vAlternateContactList c LEFT JOIN lucourses luc ON luc.lucourseid=c.lucourseid\r\nWHERE       c.alternatecontactid=@alternatecontactid\r\nORDER BY luc.startdate", parameters))
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

		// Token: 0x060003F7 RID: 1015 RVA: 0x00022C74 File Offset: 0x00020E74
		public AlternateContact LoadAlternateContactByEmail(string Email)
		{
			bool flag = Email == null || Email.Trim().Length < 1;
			AlternateContact result;
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
				using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT    ac.alternatecontactid,ac.altname,ac.altemail,ac.altphone,ac.altusername,ac.altpermissionlevel,\r\n            ac.whocreated AS whocreatedpersonid,pwho.firstname AS whocreatedfirstname,\r\n            pwho.lastname AS whocreatedlastname,pwho.student_no AS whocreatedstudent_no,\r\n            ac.isactive,ac.externalid\r\nFROM        lucoursealternatecontact ac LEFT JOIN people pwho ON pwho.personid=ac.whocreated\r\nWHERE       ac.altemail=@email", parameters))
				{
					bool flag2 = dataReader != null && dataReader.Read();
					if (flag2)
					{
						return AlternateContactDAO.GetAlternateContactFromRecord("", dataReader);
					}
				}
				result = null;
			}
			return result;
		}

		// Token: 0x040001C8 RID: 456
		private DatabaseLayer DatabaseManager;
	}
}
