using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.Common.DAO.Impl.Adapters;
using TechnoPro.Common.DAO.Impl.People;
using TechnoPro.Common.DAO.People;
using TechnoPro.Common.DAO.UserAccount;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.People;
using TechnoPro.Common.Public.Entities.UserAccount;
using TechnoPro.Common.Security.Hashing;

namespace TechnoPro.Common.DAO.Impl.UserAccount
{
	// Token: 0x0200002D RID: 45
	public class UserAccountDAO : IUserAccountDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000113 RID: 275 RVA: 0x000085DC File Offset: 0x000067DC
		// (set) Token: 0x06000114 RID: 276 RVA: 0x000085E4 File Offset: 0x000067E4
		public OperationContext OpContext { get; set; }

		// Token: 0x06000115 RID: 277 RVA: 0x000085ED File Offset: 0x000067ED
		public UserAccountDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext2 = this.OpContext;
			this.DatabaseManager = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext2 != null) ? opContext2.TenantId : null);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00008620 File Offset: 0x00006820
		internal static UserInfoPassword GetUserInfoPasswordFromRecord(IDataReader reader, OperationContext opContext, IBatchDecryptor decryptor = null)
		{
			int num = (reader["personid"] is DBNull) ? 0 : ((int)reader["personid"]);
			bool flag = num < 1;
			UserInfoPassword result;
			if (flag)
			{
				result = null;
			}
			else
			{
				DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, (opContext != null) ? opContext.TenantId : null);
				IEncryption encryption = databaseLayer.Encryption;
				bool flag2 = (bool)reader["isencrypted"];
				bool flag3 = flag2;
				string password;
				if (flag3)
				{
					password = ((decryptor == null) ? encryption.Decrypt((byte[])reader["pass"]) : decryptor.Decrypt((byte[])reader["pass"]));
				}
				else
				{
					password = ((byte[])reader["pass"]).PasswordToString();
				}
				result = new UserInfoPassword
				{
					PersonId = num,
					UserName = ((decryptor == null) ? databaseLayer.Encryption.Decrypt((byte[])reader["username"]) : decryptor.Decrypt((byte[])reader["username"])),
					Password = password,
					LastPasswordChangeDate = ((reader["lastpasswordchangedate"] is DBNull) ? DateTime.MinValue : ((DateTime)reader["lastpasswordchangedate"])),
					RequiresPasswordChange = (!(reader["requirepasswordchange"] is DBNull) && Convert.ToBoolean(reader["requirepasswordchange"])),
					PasswordExpiryDate = ((reader["PasswordExpiryDate"] is DBNull) ? null : new DateTime?((DateTime)reader["PasswordExpiryDate"])),
					IsEncrypted = flag2
				};
			}
			return result;
		}

		// Token: 0x06000117 RID: 279 RVA: 0x000087F0 File Offset: 0x000069F0
		public void RemovePassword(int PersonId, string UserName)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@pid", DbType.Int32, PersonId),
				this.DatabaseManager.GetParameter("@username", DbType.Binary, this.DatabaseManager.Encryption.Encrypt(UserName))
			};
			this.DatabaseManager.ExecuteNonQuery("DELETE FROM userinfo WHERE personid=@pid AND username=@username", parameters);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00008858 File Offset: 0x00006A58
		public void CreatePassword(UserInfoPassword PasswordInfo)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			string password = PasswordHashFactory.GetHashingProvider(eHashingType.ClockWorkDefault).CreateHash(PasswordInfo.Password, null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@pid", DbType.Int32, PasswordInfo.PersonId),
				databaseLayer.GetParameter("@username", DbType.Binary, this.DatabaseManager.Encryption.Encrypt(PasswordInfo.UserName)),
				databaseLayer.GetParameter("@password", DbType.Binary, password.PasswordToBytes()),
				databaseLayer.GetParameter("@requirepasswordchange", DbType.Boolean, PasswordInfo.RequiresPasswordChange),
				databaseLayer.GetParameter("@isencrypted", DbType.Boolean, 0)
			};
			databaseLayer.ExecuteNonQuery("INSERT INTO userinfo (username,personid,pass,requirepasswordchange,isencrypted) VALUES (@username,@pid,@password,@requirepasswordchange,@isencrypted)", parameters);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x0000892C File Offset: 0x00006B2C
		public void UpdatePasswordRequireChange(int PersonId, string UserName, bool NewDoesRequirePasswordChange)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@pid", DbType.Int32, PersonId),
				this.DatabaseManager.GetParameter("@username", DbType.Binary, this.DatabaseManager.Encryption.Encrypt(UserName)),
				this.DatabaseManager.GetParameter("@requirepasswordchange", DbType.Boolean, NewDoesRequirePasswordChange)
			};
			this.DatabaseManager.ExecuteNonQuery("UPDATE userinfo SET requirepasswordchange=@requirepasswordchange,lastpasswordchangedate=getdate() WHERE personid=@pid AND username=@username", parameters);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x000089B0 File Offset: 0x00006BB0
		public void UpdatePassword(int PersonId, string UserName, string NewPassword)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			string password = PasswordHashFactory.GetHashingProvider(eHashingType.ClockWorkDefault).CreateHash(NewPassword, null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@pid", DbType.Int32, PersonId),
				databaseLayer.GetParameter("@username", DbType.Binary, this.DatabaseManager.Encryption.Encrypt(UserName)),
				databaseLayer.GetParameter("@password", DbType.Binary, password.PasswordToBytes()),
				databaseLayer.GetParameter("@isencrypted", DbType.Boolean, 0)
			};
			databaseLayer.ExecuteNonQuery("IF NOT EXISTS(SELECT 1 FROM userinfo WHERE personid=@pid AND username=@username)\r\n    INSERT INTO userinfo(personid,username,pass,isencrypted) VALUES (@pid,@username,@password,@isencrypted)\r\nELSE \r\n    UPDATE userinfo SET pass=@password,lastpasswordchangedate=getdate(),isencrypted=@isencrypted WHERE personid=@pid AND username=@username", parameters);
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00008A5C File Offset: 0x00006C5C
		public void UpdatePassword2(string UserName, UserInfoPassword PasswordInfo)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			string password = PasswordHashFactory.GetHashingProvider(eHashingType.ClockWorkDefault).CreateHash(PasswordInfo.Password, null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@pid", DbType.Int32, PasswordInfo.PersonId),
				databaseLayer.GetParameter("@username", DbType.Binary, this.DatabaseManager.Encryption.Encrypt(UserName)),
				databaseLayer.GetParameter("@password", DbType.Binary, password.PasswordToBytes()),
				databaseLayer.GetParameter("@requirepasswordchange", DbType.Boolean, PasswordInfo.RequiresPasswordChange),
				databaseLayer.GetParameter("@passwordexpirydate", DbType.DateTime, (PasswordInfo.PasswordExpiryDate != null) ? PasswordInfo.PasswordExpiryDate.Value : DBNull.Value),
				databaseLayer.GetParameter("@isencrypted", DbType.Boolean, 0)
			};
			databaseLayer.ExecuteNonQuery("IF NOT EXISTS(SELECT 1 FROM userinfo WHERE personid=@pid AND username=@username)\r\n    INSERT INTO userinfo(personid,username,pass,requirepasswordchange,passwordexpirydate,isencrypted) VALUES (@pid,@username,@password,@requirepasswordchange,@passwordexpirydate,@isencrypted)\r\nELSE \r\n    UPDATE userinfo SET pass=@password,lastpasswordchangedate=getdate(),requirepasswordchange=@requirepasswordchange,passwordexpirydate=@passwordexpirydate,isencrypted=@isencrypted WHERE personid=@pid AND username=@username", parameters);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00008B64 File Offset: 0x00006D64
		public void ClearAllPasswords(int PersonId, bool ClearPrimaryPassword = true)
		{
			if (ClearPrimaryPassword)
			{
				DbParameter[] parameters = new DbParameter[]
				{
					this.DatabaseManager.GetParameter("@pid", DbType.Int32, PersonId)
				};
				this.DatabaseManager.ExecuteNonQuery("DELETE FROM userinfo WHERE personid=@pid", parameters);
			}
			else
			{
				IPeopleDAO peopleDAO = new PeopleDAO(this.OpContext);
				PersonBase personBase = peopleDAO.LoadPerson(PersonId);
				DbParameter[] parameters = new DbParameter[]
				{
					this.DatabaseManager.GetParameter("@pid", DbType.Int32, PersonId),
					this.DatabaseManager.GetParameter("@username", DbType.Binary, this.DatabaseManager.Encryption.Encrypt(personBase.Student_no))
				};
				this.DatabaseManager.ExecuteNonQuery("DELETE FROM userinfo WHERE personid=@pid AND NOT username=@username", parameters);
			}
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00008C24 File Offset: 0x00006E24
		public UserInfoPassword LoadPassword(string UserName, int PersonId = 0)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@pid", DbType.Int32, PersonId),
				this.DatabaseManager.GetParameter("@username", DbType.Binary, this.DatabaseManager.Encryption.Encrypt(UserName))
			};
			UserInfoPassword result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT username,pass,personid,requirepasswordchange,lastpasswordchangedate,passwordexpirydate,isencrypted FROM userinfo WHERE username=@username AND (@pid=0 OR personid=@pid)", parameters))
			{
				bool flag = dataReader == null || !dataReader.Read();
				if (flag)
				{
					result = null;
				}
				else
				{
					result = UserAccountDAO.GetUserInfoPasswordFromRecord(dataReader, this.OpContext, null);
				}
			}
			return result;
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00008CD0 File Offset: 0x00006ED0
		public void UpdatePrimaryPasswordExpiry(int PersonId, string UserName, DateTime? NewExpiryDate)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@pid", DbType.Int32, PersonId),
				this.DatabaseManager.GetParameter("@username", DbType.Binary, this.DatabaseManager.Encryption.Encrypt(UserName)),
				this.DatabaseManager.GetParameter("@passwordexpiry", DbType.DateTime, (NewExpiryDate != null) ? NewExpiryDate.Value : DBNull.Value)
			};
			this.DatabaseManager.ExecuteNonQuery("UPDATE userinfo SET passwordexpirydate=@passwordexpiry WHERE personid=@pid AND username=@username", parameters);
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00008D68 File Offset: 0x00006F68
		public IList<int> LoadPersonIdsWithUsername(string Username, bool includeDeletedAccounts = false)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@username", DbType.Binary, this.DatabaseManager.Encryption.Encrypt(Username)),
				this.DatabaseManager.GetParameter("@includedeleted", DbType.Boolean, includeDeletedAccounts)
			};
			IList<int> result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT personid FROM userinfo WHERE username=@username AND (( NOT @includedeleted IS NULL AND @includedeleted=1) OR NOT personid IN (SELECT personid FROM people WHERE isactive=0))", parameters))
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
						int num = (dataReader["personid"] is DBNull) ? 0 : ((int)dataReader["personid"]);
						bool flag2 = num > 0 && !list.Contains(num);
						if (flag2)
						{
							list.Add(num);
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x04000063 RID: 99
		private DatabaseLayer DatabaseManager;
	}
}
