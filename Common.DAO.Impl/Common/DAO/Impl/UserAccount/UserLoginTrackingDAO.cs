using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.Common.DAO.UserAccount;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.UserAccount.LoginTracking;
using TechnoPro.Common.Win32;

namespace TechnoPro.Common.DAO.Impl.UserAccount
{
	// Token: 0x0200002C RID: 44
	public class UserLoginTrackingDAO : IUserLoginTrackingDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600010A RID: 266 RVA: 0x0000818B File Offset: 0x0000638B
		public UserLoginTrackingDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600010B RID: 267 RVA: 0x0000819D File Offset: 0x0000639D
		// (set) Token: 0x0600010C RID: 268 RVA: 0x000081A5 File Offset: 0x000063A5
		public OperationContext OpContext { get; set; }

		// Token: 0x0600010D RID: 269 RVA: 0x000081B0 File Offset: 0x000063B0
		private IList<LoginInfo> GetLoginInfosFromReader(IDataReader reader, IEncryption tripleDes)
		{
			bool flag = reader == null;
			IList<LoginInfo> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				List<LoginInfo> list = new List<LoginInfo>();
				IBatchDecryptor batchDecryptor = tripleDes.GetBatchDecryptor();
				while (reader.Read())
				{
					LoginInfo loginInfoFromRecord = this.GetLoginInfoFromRecord(reader, batchDecryptor);
					bool flag2 = loginInfoFromRecord != null;
					if (flag2)
					{
						list.Add(loginInfoFromRecord);
					}
				}
				result = list;
			}
			return result;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x0000820C File Offset: 0x0000640C
		private LoginInfo GetLoginInfoFromRecord(IDataReader record, IBatchDecryptor batchDecryptor = null)
		{
			bool flag = record == null;
			LoginInfo result;
			if (flag)
			{
				result = null;
			}
			else
			{
				int num = (record["PersonId"] is DBNull) ? 0 : ((int)record["PersonId"]);
				bool flag2 = num < 1;
				if (flag2)
				{
					result = null;
				}
				else
				{
					LoginInfo loginInfo = new LoginInfo();
					loginInfo.PersonId = num;
					LoginInfo loginInfo2 = loginInfo;
					string ip;
					if (!(record["ip"] is DBNull))
					{
						if (batchDecryptor == null)
						{
							eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
							OperationContext opContext = this.OpContext;
							ip = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null).Encryption.Decrypt((byte[])record["ip"]);
						}
						else
						{
							ip = batchDecryptor.Decrypt((byte[])record["ip"]);
						}
					}
					else
					{
						ip = "";
					}
					loginInfo2.Ip = ip;
					loginInfo.LoginDate = (DateTime)record["LoginDate"];
					loginInfo.ClockWorkVersion = record["clockworkversion"].ToString().Trim().DeserializeVersionFromString();
					loginInfo.NetVersions = record["netversion"].ToString().Trim().SplitEnumValues<DotNetVersion>();
					result = loginInfo;
				}
			}
			return result;
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00008340 File Offset: 0x00006540
		public void RecordNewLogin(LoginInfo LoginInfo)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@pid", DbType.Int32, (this.OpContext.WhoAmI > 0) ? this.OpContext.WhoAmI : LoginInfo.PersonId),
				databaseLayer.GetParameter("@ip", DbType.Binary, databaseLayer.Encryption.Encrypt(LoginInfo.Ip ?? "")),
				databaseLayer.GetParameter("@clockworkversion", DbType.String, (LoginInfo.ClockWorkVersion == null) ? "" : LoginInfo.ClockWorkVersion.ToString()),
				databaseLayer.GetParameter("@netversion", DbType.String, (LoginInfo.NetVersions == null) ? "" : LoginInfo.NetVersions.CommaSeparatedValuesWithoutSpace<DotNetVersion>())
			};
			databaseLayer.ExecuteNonQuery("IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'UserLogins')\r\nBEGIN\r\nIF EXISTS(SELECT personid FROM UserLogins WHERE personid=@pid)\r\n    UPDATE UserLogins SET LoginDate=getdate(),ip=@ip,ClockWorkVersion=@clockworkversion,NetVersion=@netversion WHERE PersonId=@pid\r\nELSE\r\n    INSERT INTO UserLogins (PersonId,LoginDate,ip,ClockWorkVersion,NetVersion) VALUES (@pid,getdate(),@ip,@clockworkversion,@netversion)\r\nEND", parameters);
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00008434 File Offset: 0x00006634
		public LoginInfo LoadLoginInfoByPersonId(int PersonId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@pid", DbType.Int32, PersonId)
			};
			LoginInfo result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'UserLogins')\r\nBEGIN\r\nSELECT PersonId,LoginDate,ip,ClockWorkVersion,NetVersion FROM UserLogins WHERE PersonId=@pid\r\nEND\r\nELSE\r\nBEGIN\r\nselect 1 where 0=1\r\nEND\r\n", parameters))
			{
				bool flag = dataReader == null || !dataReader.Read();
				if (flag)
				{
					result = null;
				}
				else
				{
					result = this.GetLoginInfoFromRecord(dataReader, null);
				}
			}
			return result;
		}

		// Token: 0x06000111 RID: 273 RVA: 0x000084C8 File Offset: 0x000066C8
		public IList<LoginInfo> LoadAllLoginInfos()
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			IList<LoginInfo> loginInfosFromReader;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'UserLogins')\r\nBEGIN\r\nSELECT PersonId,LoginDate,ip,ClockWorkVersion,NetVersion FROM UserLogins ORDER BY LoginDate desc\r\nEND\r\nELSE\r\nBEGIN\r\nselect 1 where 0=1\r\nEND"))
			{
				loginInfosFromReader = this.GetLoginInfosFromReader(dataReader, databaseLayer.Encryption);
			}
			return loginInfosFromReader;
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00008528 File Offset: 0x00006728
		public IList<LoginInfo> LoadLoginInfosByDateRange(DateTime StartDate, DateTime EndDate)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@sdate", DbType.DateTime, StartDate.Date),
				databaseLayer.GetParameter("@edate", DbType.DateTime, EndDate.Date.AddDays(1.0))
			};
			IList<LoginInfo> loginInfosFromReader;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'UserLogins')\r\nBEGIN\r\nSELECT PersonId,LoginDate,ip,ClockWorkVersion,NetVersion FROM UserLogins WHERE LoginDate>=@sdate AND LoginDate<@edate ORDER BY LoginDate desc\r\nEND\r\nELSE\r\nBEGIN\r\nselect 1 where 0=1\r\nEND", parameters))
			{
				loginInfosFromReader = this.GetLoginInfosFromReader(dataReader, databaseLayer.Encryption);
			}
			return loginInfosFromReader;
		}
	}
}
