using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.Common.DAO.Impl.Settings.BinarySerializers;
using TechnoPro.Common.DAO.Settings;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.OperationContexts;
using TechnoPro.Common.Public.Entities.Settings;

namespace TechnoPro.Common.DAO.Impl.Settings
{
	// Token: 0x0200004C RID: 76
	public class SettingDAO : ISettingDAO, IBaseOperationContext<SettingsOperationContext>
	{
		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x00011AD0 File Offset: 0x0000FCD0
		// (set) Token: 0x060001F9 RID: 505 RVA: 0x00011AD8 File Offset: 0x0000FCD8
		public DatabaseLayer DatabaseManager { get; private set; }

		// Token: 0x060001FA RID: 506 RVA: 0x00011AE1 File Offset: 0x0000FCE1
		public SettingDAO(SettingsOperationContext opContext)
		{
			this.OpContext = opContext;
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			SettingsOperationContext opContext2 = this.OpContext;
			this.DatabaseManager = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext2 != null) ? opContext2.TenantId : null);
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060001FB RID: 507 RVA: 0x00011B12 File Offset: 0x0000FD12
		// (set) Token: 0x060001FC RID: 508 RVA: 0x00011B1A File Offset: 0x0000FD1A
		public SettingsOperationContext OpContext { get; set; }

		// Token: 0x060001FD RID: 509 RVA: 0x00011B24 File Offset: 0x0000FD24
		public IList<AppSetting> GetSettings(Group group)
		{
			List<AppSetting> list = new List<AppSetting>();
			int num = (int)(group + 10000);
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@instance", DbType.String, this.OpContext.InstanceName),
				this.DatabaseManager.GetParameter("@start", DbType.Int32, (int)group),
				this.DatabaseManager.GetParameter("@end", DbType.Int32, num)
			};
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader(QueryStorageSettings.QS_SETTINGS_BY_GROUP, parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					while (dataReader.Read())
					{
						AppSetting setting = SettingDAO.GetSetting(dataReader, this.DatabaseManager.Encryption);
						bool flag2 = setting != null;
						if (flag2)
						{
							list.Add(setting);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00011C20 File Offset: 0x0000FE20
		public AppSetting GetSetting(Setting setting)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@instance", DbType.String, this.OpContext.InstanceName),
				this.DatabaseManager.GetParameter("@settingcode", DbType.Int32, (int)setting)
			};
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader(QueryStorageSettings.QS_SINGLE_SETTING, parameters))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					return SettingDAO.GetSetting(dataReader, this.DatabaseManager.Encryption);
				}
			}
			return null;
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00011CCC File Offset: 0x0000FECC
		public AppSetting GetSetting(Setting setting, string sValue)
		{
			AppSetting result;
			try
			{
				ISettingBinarySerializer binarySerializer = SettingBinarySerializerFactory.GetBinarySerializer(setting);
				eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
				SettingsOperationContext opContext = this.OpContext;
				IEncryption encryption = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null).Encryption;
				object value = binarySerializer.Deserialize(setting, this.DatabaseManager.Encryption.Encrypt(sValue ?? ""), encryption);
				result = new AppSetting
				{
					LookupSetting = new LookupSetting(setting),
					Value = value
				};
			}
			catch (Exception innerException)
			{
				throw new FormatException(string.Format("Wrong string value format: {0}", sValue), innerException);
			}
			return result;
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00011D68 File Offset: 0x0000FF68
		public void Save(AppSetting setting)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			SettingsOperationContext opContext = this.OpContext;
			IEncryption encryption = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null).Encryption;
			ISettingBinarySerializer binarySerializer = SettingBinarySerializerFactory.GetBinarySerializer(setting.LookupSetting.Setting);
			byte[] value = binarySerializer.Serialize(setting.LookupSetting.Setting, setting.Value, encryption);
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@settingvalue", DbType.Binary, value),
				this.DatabaseManager.GetParameter("@instance", DbType.String, this.OpContext.InstanceName),
				this.DatabaseManager.GetParameter("@settingcode", DbType.Int32, (int)setting.LookupSetting.Setting)
			};
			this.DatabaseManager.ExecuteNonQuery(QueryStorageSettings.IUS_INSERT_OR_UPDATE_SETTING, parameters);
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00011E34 File Offset: 0x00010034
		public void SetStringValue(AppSetting setting, string sValue)
		{
			try
			{
				ISettingBinarySerializer binarySerializer = SettingBinarySerializerFactory.GetBinarySerializer(setting.LookupSetting.Setting);
				setting.Value = binarySerializer.Deserialize(setting.LookupSetting.Setting, this.DatabaseManager.Encryption.Encrypt(sValue), this.DatabaseManager.Encryption);
			}
			catch (Exception innerException)
			{
				throw new FormatException(string.Format("Wrong string value format: {0}", sValue), innerException);
			}
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00011EB0 File Offset: 0x000100B0
		public IList<string> GetInstanceNames()
		{
			List<string> list = new List<string>();
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT DISTINCT instancename FROM websettings2"))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					while (dataReader.Read())
					{
						list.Add(dataReader.GetString(0));
					}
				}
			}
			return list;
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00011F20 File Offset: 0x00010120
		private static AppSetting GetSetting(IDataRecord record, IEncryption encryption)
		{
			int num = (int)record["settingcode"];
			bool flag = !Enum.IsDefined(typeof(Setting), num);
			AppSetting result;
			if (flag)
			{
				result = null;
			}
			else
			{
				Setting setting = (Setting)num;
				byte[] binaryValue = (record["settingstringvalue"] == DBNull.Value) ? new byte[0] : ((byte[])record["settingstringvalue"]);
				ISettingBinarySerializer binarySerializer = SettingBinarySerializerFactory.GetBinarySerializer(setting);
				AppSetting appSetting = new AppSetting
				{
					LookupSetting = new LookupSetting(setting),
					Value = binarySerializer.Deserialize(setting, binaryValue, encryption)
				};
				result = appSetting;
			}
			return result;
		}
	}
}
