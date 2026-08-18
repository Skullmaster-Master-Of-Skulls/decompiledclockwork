using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Databases;
using TechnoPro.Common.DAO.Impl.Adapters;
using TechnoPro.Common.DAO.Impl.People;
using TechnoPro.Common.DAO.Impl.UserAccount;
using TechnoPro.Common.DAO.Membership;
using TechnoPro.Common.DAO.UserAccount;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Membership;
using TechnoPro.Common.Public.Entities.People;
using TechnoPro.Common.Public.Entities.UserAccount;
using TechnoPro.Common.Security.Hashing;

namespace TechnoPro.Common.DAO.Impl.Membership
{
	// Token: 0x02000090 RID: 144
	public class UserDAO : IUserDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060003B4 RID: 948 RVA: 0x00020E23 File Offset: 0x0001F023
		// (set) Token: 0x060003B5 RID: 949 RVA: 0x00020E2B File Offset: 0x0001F02B
		public OperationContext OpContext { get; set; }

		// Token: 0x060003B6 RID: 950 RVA: 0x00020E34 File Offset: 0x0001F034
		private PersonBase Authenticate(string userName, string password)
		{
			bool flag = string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password) || !this.ValidateUserPassword(userName, password);
			PersonBase result;
			if (flag)
			{
				result = null;
			}
			else
			{
				eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
				OperationContext opContext = this.OpContext;
				DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
				DbParameter[] parameters = new DbParameter[]
				{
					databaseLayer.GetParameter("@username", DbType.Binary, databaseLayer.Encryption.Encrypt(userName.ToUpper()))
				};
				using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT p.personid,p.student_no,p.middlename,m.*\r\nFROM Messaging_Users m LEFT JOIN people p ON p.personid=m.ID\r\nWHERE m.username=@username AND NOT p.personid IS NULL AND p.isactive=1", parameters))
				{
					bool flag2 = dataReader != null && dataReader.Read();
					if (flag2)
					{
						int num = (int)dataReader["personid"];
						bool flag3 = num > 0;
						if (flag3)
						{
							return PeopleDAO.GetPersonFromReader("", dataReader, this.OpContext, null);
						}
					}
				}
				result = null;
			}
			return result;
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x00020F2C File Offset: 0x0001F12C
		private User GetUser(IDataRecord record)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			int num = (int)record["ID"];
			int? num2 = (record["isEmailEncrypted"] is DBNull) ? null : new int?((int)record["isEmailEncrypted"]);
			string email = string.Empty;
			bool flag = num2 != null;
			if (flag)
			{
				email = ((num2.Value > 0) ? ((record["emailEncrypted"] is DBNull) ? string.Empty : databaseLayer.Encryption.Decrypt((byte[])record["emailEncrypted"])) : ((record["emailPlainText"] is DBNull) ? string.Empty : ((string)record["emailPlainText"])));
			}
			int? num3 = (record["isPhoneEncrypted"] is DBNull) ? null : new int?((int)record["isPhoneEncrypted"]);
			string phone = string.Empty;
			bool flag2 = num3 != null;
			if (flag2)
			{
				phone = ((num3.Value > 0) ? ((record["phoneEncrypted"] is DBNull) ? string.Empty : databaseLayer.Encryption.Decrypt((byte[])record["phoneEncrypted"])) : ((record["phonePlainText"] is DBNull) ? string.Empty : ((string)record["phonePlainText"])));
			}
			bool flag3 = !(record["requirepasswordchange"] is DBNull) && (bool)record["requirepasswordchange"];
			bool flag4 = !flag3 && !(record["passwordexpirydate"] is DBNull);
			if (flag4)
			{
				flag3 = (((DateTime)record["passwordexpirydate"]).Date < DateTime.Now.Date);
			}
			return new User
			{
				UserId = num,
				Name = databaseLayer.Encryption.Decrypt((byte[])record["username"]),
				FirstName = databaseLayer.Encryption.Decrypt((byte[])record["firstName"]),
				LastName = databaseLayer.Encryption.Decrypt((byte[])record["lastName"]),
				Roles = this.GetRoles(num),
				Email = email,
				Phone = phone,
				RequirePasswordChange = flag3
			};
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x000211EC File Offset: 0x0001F3EC
		private IList<Role> GetRoles(int userID)
		{
			List<Role> list = new List<Role>();
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter parameter = databaseLayer.GetParameter("@id", DbType.Int32, userID);
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select u.PersonID as UserID, r.GroupID as RoleID, r.description as 'RoleName' from Groups as r\r\n            Inner Join PeopleGroups as ur ON r.GroupID = ur.GroupID\r\n            Inner Join People as u ON ur.PersonID = u.PersonID\r\n            Where u.PersonID = @id", new DbParameter[]
			{
				parameter
			}))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					while (dataReader.Read())
					{
						list.Add(this.GetRole(dataReader));
					}
				}
			}
			return list;
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x00021294 File Offset: 0x0001F494
		private Role GetRole(IDataRecord record)
		{
			return new Role
			{
				Id = (int)record["RoleID"],
				Name = (string)record["RoleName"]
			};
		}

		// Token: 0x060003BA RID: 954 RVA: 0x000212DC File Offset: 0x0001F4DC
		public User GetUser(string username)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter parameter = databaseLayer.GetParameter("@username", DbType.Binary, databaseLayer.Encryption.Encrypt(username));
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select * from Messaging_Users Where username = @username", new DbParameter[]
			{
				parameter
			}))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					return this.GetUser(dataReader);
				}
			}
			return null;
		}

		// Token: 0x060003BB RID: 955 RVA: 0x00021374 File Offset: 0x0001F574
		public bool ValidateUserPassword(string userName, string password)
		{
			IUserAccountDAO userAccountDAO = new UserAccountDAO(this.OpContext);
			UserInfoPassword userInfoPassword = userAccountDAO.LoadPassword(userName, 0);
			bool flag = userInfoPassword == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool isEncrypted = userInfoPassword.IsEncrypted;
				if (isEncrypted)
				{
					result = (!string.IsNullOrEmpty(userInfoPassword.Password) && userInfoPassword.Password == password);
				}
				else
				{
					try
					{
						result = PasswordHashFactory.GetHashingProvider(eHashingType.ClockWorkDefault).ValidatePassword(password, userInfoPassword.Password, null);
					}
					catch
					{
						result = (!string.IsNullOrEmpty(userInfoPassword.Password) && userInfoPassword.Password == password);
					}
				}
			}
			return result;
		}

		// Token: 0x060003BC RID: 956 RVA: 0x00021418 File Offset: 0x0001F618
		public bool Exists(string username)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter parameter = databaseLayer.GetParameter("@username", DbType.Binary, databaseLayer.Encryption.Encrypt(username));
			object obj = databaseLayer.ExecuteScalar("select 1 from Messaging_Users Where username = @username", new DbParameter[]
			{
				parameter
			});
			return obj != null && obj is int && (int)obj > 0;
		}

		// Token: 0x060003BD RID: 957 RVA: 0x0002148C File Offset: 0x0001F68C
		public bool ChangeUserPassword(string UserName, string CurrentPassword, string NewPassword)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			PersonBase personBase = this.Authenticate(UserName, CurrentPassword);
			bool flag = personBase == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				string password = PasswordHashFactory.GetHashingProvider(eHashingType.ClockWorkDefault).CreateHash(NewPassword, null);
				DbParameter[] parameters = new DbParameter[]
				{
					databaseLayer.GetParameter("@username", DbType.Binary, databaseLayer.Encryption.Encrypt(UserName.ToUpper())),
					databaseLayer.GetParameter("@passwordnew", DbType.Binary, password.PasswordToBytes()),
					databaseLayer.GetParameter("@isencrypted", DbType.Boolean, false)
				};
				object obj = databaseLayer.ExecuteScalar("UPDATE userinfo SET pass=@passwordnew,requirepasswordchange=0,lastpasswordchangedate=getdate(),passwordexpirydate=NULL,isencrypted=@isencrypted\r\nWHERE username=@username; \r\nSELECT personid FROM userinfo WHERE username=@username AND pass=@passwordnew;", parameters);
				bool flag2 = obj == null || !(obj is int);
				result = !flag2;
			}
			return result;
		}

		// Token: 0x060003BE RID: 958 RVA: 0x00021564 File Offset: 0x0001F764
		public bool UserMustChangePassword(string UserName)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@username", DbType.Binary, databaseLayer.Encryption.Encrypt(UserName.ToUpper()))
			};
			object obj = databaseLayer.ExecuteScalar("SELECT requirepasswordchange FROM userinfo \r\nWHERE username=@username AND NOT requirepasswordchange IS NULL AND requirepasswordchange=1", parameters);
			bool flag = obj == null || !(obj is bool);
			return !flag && (bool)obj;
		}

		// Token: 0x060003BF RID: 959 RVA: 0x000215E8 File Offset: 0x0001F7E8
		public bool ClearUserPassword(string UserName)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@username", DbType.Binary, databaseLayer.Encryption.Encrypt(UserName.ToUpper()))
			};
			object obj = databaseLayer.ExecuteScalar("DELETE FROM userinfo \r\nWHERE username=@username;\r\nSELECT personid FROM userinfo WHERE username=@username", parameters);
			return obj == null;
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x0002164C File Offset: 0x0001F84C
		public bool SetUserPassword(string userName, string newPassword)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			string password = PasswordHashFactory.GetHashingProvider(eHashingType.ClockWorkDefault).CreateHash(newPassword, null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@username", DbType.Binary, databaseLayer.Encryption.Encrypt(userName.ToUpper())),
				databaseLayer.GetParameter("@passwordnew", DbType.Binary, password.PasswordToBytes()),
				databaseLayer.GetParameter("@isencrypted", DbType.Boolean, false)
			};
			object obj = databaseLayer.ExecuteScalar("IF EXISTS(SELECT personid FROM userinfo WHERE username=@username)\r\n    UPDATE userinfo SET pass=@passwordnew,isencrypted=@isencrypted WHERE username=@username\r\nELSE\r\nBEGIN\r\n    DECLARE @pid int\r\n    SET @pid = (SELECT TOP 1 personid FROM people WHERE isactive=1 AND student_no=@username)\r\n    IF NOT @pid IS NULL\r\n        INSERT INTO userinfo (personid,username,pass,isencrypted) VALUES (@pid,@username,@passwordnew,@isencrypted)\r\nEND\r\nSELECT personid FROM userinfo WHERE username=@username AND pass=@passwordnew;", parameters);
			return obj is int;
		}
	}
}
