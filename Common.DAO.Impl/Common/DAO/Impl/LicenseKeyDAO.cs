using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using ClockWorkLogger;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.Common.DAO.Impl.Properties;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Exceptions;

namespace TechnoPro.Common.DAO.Impl
{
	// Token: 0x02000018 RID: 24
	public class LicenseKeyDAO : ILicenseKeyDAO
	{
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00004B98 File Offset: 0x00002D98
		// (set) Token: 0x06000091 RID: 145 RVA: 0x00004BA0 File Offset: 0x00002DA0
		public DatabaseLayer DatabaseManager { get; private set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00004BA9 File Offset: 0x00002DA9
		private IEncryption Encryption { get; }

		// Token: 0x06000093 RID: 147 RVA: 0x00004BB4 File Offset: 0x00002DB4
		public LicenseKeyDAO()
		{
			this.DatabaseManager = DatabaseLayerFactory.ClockWork;
			MiscSafeDAO miscSafeDAO = new MiscSafeDAO();
			string text = miscSafeDAO.GetValue("institutionguid");
			bool flag = string.IsNullOrEmpty(text);
			if (flag)
			{
				miscSafeDAO.Save("institutionguid", text = Guid.NewGuid().ToString());
			}
			MemoryStream memoryStream = new MemoryStream();
			Resource.clock.Save(memoryStream);
			this.Encryption = EncryptionFactory.GetEncryption(this.DatabaseManager.Encryption.Name, Convert.ToBase64String(memoryStream.ToArray()) + text);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00004C54 File Offset: 0x00002E54
		public LicenseKeyInfo Get(string key)
		{
			DbParameter parameter = this.DatabaseManager.GetParameter("@serial", DbType.String, this.Encryption.Encrypt(key));
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("select * from [LicenseSystem_LicenseInfo] where LicenseKey=@serial", new DbParameter[]
			{
				parameter
			}))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					return this.GetLicenseKeyInfoFromDB(dataReader);
				}
			}
			return null;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00004CDC File Offset: 0x00002EDC
		public IDictionary<string, LicenseKeyInfo> FromFile(string filename)
		{
			IDictionary<string, LicenseKeyInfo> result;
			try
			{
				using (FileStream fileStream = new FileStream(filename, FileMode.Open))
				{
					BinaryFormatter binaryFormatter = new BinaryFormatter();
					Dictionary<string, LicenseKeyInfo> dictionary = (Dictionary<string, LicenseKeyInfo>)binaryFormatter.Deserialize(fileStream);
					result = dictionary;
				}
			}
			catch (Exception ex)
			{
				throw new InvalidLicenseKeyException(string.Format("Invalid key file.{0}", Environment.NewLine + ex.Message), ex);
			}
			return result;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00004D5C File Offset: 0x00002F5C
		public void Save(LicenseKeyInfo licenseKeyInfo)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@productname", DbType.Binary, this.Encryption.Encrypt(licenseKeyInfo.ProductName)),
				this.DatabaseManager.GetParameter("@licensekey", DbType.Binary, this.Encryption.Encrypt(licenseKeyInfo.LicenseKey)),
				this.DatabaseManager.GetParameter("@issueddate", DbType.Binary, this.Encryption.Encrypt(licenseKeyInfo.IssuedDate.ToString("yyyy-MM-dd"))),
				this.DatabaseManager.GetParameter("@expirydate", DbType.Binary, (licenseKeyInfo.ExpiryDate != null) ? this.Encryption.Encrypt(licenseKeyInfo.ExpiryDate.Value.ToString("yyyy-MM-dd")) : DBNull.Value),
				this.DatabaseManager.GetParameter("@licensetype", DbType.Binary, this.Encryption.Encrypt(licenseKeyInfo.LicenseType.ToString())),
				this.DatabaseManager.GetParameter("@nlicenses", DbType.Binary, this.Encryption.Encrypt(licenseKeyInfo.NLicenses.ToString())),
				this.DatabaseManager.GetParameter("@licensedto", DbType.Binary, this.Encryption.Encrypt(licenseKeyInfo.LicensedTo))
			};
			this.DatabaseManager.ExecuteNonQuery("if not exists(select 1 from [LicenseSystem_LicenseInfo] where ProductName=@productname)\r\n\tbegin\r\n\t\tInsert into [LicenseSystem_LicenseInfo] (ProductName, LicenseKey, IssuedDate, ExpiryDate, LicenseType, NLicenses, LicensedTo)\r\n\t\tvalues (@productname, @licensekey, @issueddate, @expirydate, @licensetype, @nlicenses, @licensedto)\r\n\tend\r\nelse\r\n\tbegin\r\n\t\tupdate [LicenseSystem_LicenseInfo] \r\n\t\tset LicenseKey=@licensekey, \r\n            IssuedDate=@issueddate, \r\n            ExpiryDate=@expirydate, \r\n            NLicenses=@nlicenses, \r\n            LicensedTo=@licensedto,\r\n            LicenseType=@licensetype\r\n\t\twhere ProductName=@productname\r\n\tend", parameters);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00004ED8 File Offset: 0x000030D8
		public LicenseKeyInfo GetProductKey(string productName)
		{
			LicenseKeyInfo result;
			try
			{
				DbParameter parameter = this.DatabaseManager.GetParameter("@productname", DbType.Binary, this.Encryption.Encrypt(productName));
				using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("Select * from [LicenseSystem_LicenseInfo] Where ProductName=@productname", new DbParameter[]
				{
					parameter
				}))
				{
					bool flag = dataReader != null && dataReader.Read();
					if (flag)
					{
						result = this.GetLicenseKeyInfoFromDB(dataReader);
					}
					else
					{
						result = null;
					}
				}
			}
			catch (Exception ex)
			{
				CWLogger.Logger.ErrorException(string.Format("LicenseKeyDAO::GetProductKey: {0}", ex.ToString()), ex);
				result = null;
			}
			return result;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00004F90 File Offset: 0x00003190
		public LicenseKeyInfo GetSupportPlanKey()
		{
			DbParameter parameter = this.DatabaseManager.GetParameter("@licensetype", DbType.Binary, this.Encryption.Encrypt(LicenseType.SupportPlan.ToString()));
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("select * from [LicenseSystem_LicenseInfo] where LicenseType=@licensetype", new DbParameter[]
			{
				parameter
			}))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					try
					{
						return this.GetLicenseKeyInfoFromDB(dataReader);
					}
					catch (InvalidLicenseKeyException)
					{
						return null;
					}
				}
			}
			return null;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x0000503C File Offset: 0x0000323C
		public List<LicenseKeyInfo> GetKeys()
		{
			List<LicenseKeyInfo> list = new List<LicenseKeyInfo>();
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("select * from [LicenseSystem_LicenseInfo]"))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					while (dataReader.Read())
					{
						try
						{
							list.Add(this.GetLicenseKeyInfoFromDB(dataReader));
						}
						catch (InvalidLicenseKeyException)
						{
						}
					}
				}
			}
			return list;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x000050C4 File Offset: 0x000032C4
		public List<LicenseProductInfo> GetProductsInfo()
		{
			List<LicenseProductInfo> list = new List<LicenseProductInfo>();
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("select * from [LicenseSystem_ProductInfo]"))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					return list;
				}
				while (dataReader.Read())
				{
					try
					{
						list.Add(this.GetProductKeyInfoFromDB(dataReader));
					}
					catch (InvalidLicenseKeyException)
					{
					}
				}
			}
			return list;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x0000514C File Offset: 0x0000334C
		public List<string> GetProductNames()
		{
			List<string> list = new List<string>();
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("select ProductName from [LicensingSystem_ProductInfo]"))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					while (dataReader.Read())
					{
						list.Add(this.Encryption.Decrypt((byte[])dataReader["ProductName"]));
					}
				}
			}
			return list;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000051D4 File Offset: 0x000033D4
		public void SaveValidationParameters(string productName, string validationParameters)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@productname", DbType.String, productName),
				this.DatabaseManager.GetParameter("@productparameters", DbType.String, validationParameters)
			};
			this.DatabaseManager.ExecuteNonQuery("if not exists (select 1 from LicenseSystem_ProductInfo where ProductName = @productname)\r\n                begin\r\n\t                insert into LicenseSystem_ProductInfo (ProductName, ProductParameters) values(@productname, @productparameters)\t\r\n                end", parameters);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00005228 File Offset: 0x00003428
		private LicenseKeyInfo GetLicenseKeyInfoFromDB(IDataRecord record)
		{
			LicenseKeyInfo result;
			try
			{
				result = new LicenseKeyInfo
				{
					ProductName = this.Encryption.Decrypt((byte[])record["ProductName"]),
					LicenseKey = this.Encryption.Decrypt((byte[])record["LicenseKey"]),
					IssuedDate = DateTime.Parse(this.Encryption.Decrypt((byte[])record["IssuedDate"])),
					ExpiryDate = ((record["ExpiryDate"] is DBNull) ? null : new DateTime?(DateTime.Parse(this.Encryption.Decrypt((byte[])record["ExpiryDate"])))),
					LicenseType = (LicenseType)Enum.Parse(typeof(LicenseType), this.Encryption.Decrypt((byte[])record["LicenseType"])),
					NLicenses = int.Parse(this.Encryption.Decrypt((byte[])record["NLicenses"])),
					LicensedTo = this.Encryption.Decrypt((byte[])record["LicensedTo"])
				};
			}
			catch (Exception ex)
			{
				throw new InvalidLicenseKeyException(ex.Message, ex);
			}
			return result;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000053A0 File Offset: 0x000035A0
		private LicenseProductInfo GetProductKeyInfoFromDB(IDataRecord record)
		{
			LicenseProductInfo result;
			try
			{
				result = new LicenseProductInfo
				{
					ProductName = (string)record["ProductName"],
					ProductParameters = (string)record["ProductParameters"]
				};
			}
			catch (Exception ex)
			{
				throw new InvalidLicenseProductInfoException(ex.Message, ex);
			}
			return result;
		}

		// Token: 0x04000033 RID: 51
		private const string DatetimeFormat = "yyyy-MM-dd";
	}
}
