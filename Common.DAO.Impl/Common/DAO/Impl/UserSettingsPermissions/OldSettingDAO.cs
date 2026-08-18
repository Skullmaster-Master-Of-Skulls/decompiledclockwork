using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Databases;
using TechnoPro.Common.DAO.UserSettingsPermissions;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions.OldUserSettings;

namespace TechnoPro.Common.DAO.Impl.UserSettingsPermissions
{
	// Token: 0x02000027 RID: 39
	public class OldSettingDAO : IOldSettingDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x00006D19 File Offset: 0x00004F19
		// (set) Token: 0x060000EA RID: 234 RVA: 0x00006D21 File Offset: 0x00004F21
		public OperationContext OpContext { get; set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00006D2A File Offset: 0x00004F2A
		// (set) Token: 0x060000EC RID: 236 RVA: 0x00006D32 File Offset: 0x00004F32
		public DatabaseLayer DatabaseManager { get; private set; }

		// Token: 0x060000ED RID: 237 RVA: 0x00006D3B File Offset: 0x00004F3B
		public OldSettingDAO()
		{
			this.OpContext = null;
			this.DatabaseManager = DatabaseLayerFactory.ClockWork;
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00006D59 File Offset: 0x00004F59
		public OldSettingDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext2 = this.OpContext;
			this.DatabaseManager = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext2 != null) ? opContext2.TenantId : null);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00006D8C File Offset: 0x00004F8C
		private static OldUserSetting GetOldSettingFromRecord(IDataReader record)
		{
			bool flag = record == null;
			OldUserSetting result;
			if (flag)
			{
				result = null;
			}
			else
			{
				int num = (record["settingcode"] is DBNull) ? 0 : ((int)record["settingcode"]);
				bool flag2 = num < 1 || !Enum.IsDefined(typeof(eSettingCode), num);
				if (flag2)
				{
					result = null;
				}
				else
				{
					int num2 = 0;
					bool flag3 = record["settingid"] != DBNull.Value;
					if (flag3)
					{
						num2 = (int)record["settingid"];
					}
					bool flag4 = num2 < 1 && record["settinggroupid"] != DBNull.Value;
					if (flag4)
					{
						num2 = (int)record["settinggroupid"];
					}
					int num3 = (record["personid"] is DBNull) ? 0 : ((int)record["personid"]);
					int num4 = (record["groupid"] is DBNull) ? 0 : ((int)record["groupid"]);
					bool flag5 = num3 > 0;
					eOldUserSettingType settingType;
					if (flag5)
					{
						settingType = eOldUserSettingType.PersonSetting;
					}
					else
					{
						bool flag6 = num4 == -1;
						if (flag6)
						{
							settingType = eOldUserSettingType.EveryoneSetting;
						}
						else
						{
							bool flag7 = num4 > 0;
							if (flag7)
							{
								settingType = eOldUserSettingType.GroupSetting;
							}
							else
							{
								settingType = eOldUserSettingType.Unknown;
							}
						}
					}
					result = new OldUserSetting
					{
						SettingIdOrSettingGroupId = num2,
						SettingCode = (eSettingCode)num,
						IntVal = ((record["settingvalue"] is DBNull) ? 0 : ((int)record["settingvalue"])),
						StringVal = ((record["settingstringvalue"] is DBNull) ? "" : ((string)record["settingstringvalue"])),
						SettingType = settingType,
						PersonOrGroupId = ((num3 > 0) ? num3 : num4),
						ModificationStatus = eDataItemModificationStatus.NoChange,
						OrderNum = ((record["ordernum"] is DBNull) ? 0 : ((int)record["ordernum"]))
					};
				}
			}
			return result;
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00006FAC File Offset: 0x000051AC
		public IList<OldUserSetting> LoadPersonSettings(int PersonId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@pid", DbType.Int32, PersonId)
			};
			IList<OldUserSetting> result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT\t0 AS settinggroupid,s.settingid,0 AS groupid,s.personid,s.settingcode,s.settingvalue,s.settingstringvalue,0 As ordernum\r\nFROM\tsettings s \r\nWHERE\ts.personid=@pid\r\nORDER BY s.settingcode", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<OldUserSetting> list = new List<OldUserSetting>();
					while (dataReader.Read())
					{
						OldUserSetting oldSettingFromRecord = OldSettingDAO.GetOldSettingFromRecord(dataReader);
						bool flag2 = oldSettingFromRecord != null;
						if (flag2)
						{
							list.Add(oldSettingFromRecord);
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00007064 File Offset: 0x00005264
		public IList<OldUserSetting> LoadGroupSettings(int GroupId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@gid", DbType.Int32, GroupId)
			};
			IList<OldUserSetting> result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT\tsg.settinggroupid,0 AS settingid,g.groupid,0 AS personid,sg.settingcode,sg.settingvalue,sg.settingstringvalue,g.ordernum\r\nFROM\tsettingsgroups sg LEFT JOIN groups g ON sg.groupID=g.GroupID \r\nWHERE\tsg.groupid=@gid\r\nORDER BY g.ordernum,sg.settingcode", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<OldUserSetting> list = new List<OldUserSetting>();
					while (dataReader.Read())
					{
						OldUserSetting oldSettingFromRecord = OldSettingDAO.GetOldSettingFromRecord(dataReader);
						bool flag2 = oldSettingFromRecord != null;
						if (flag2)
						{
							list.Add(oldSettingFromRecord);
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x0000711C File Offset: 0x0000531C
		public IList<OldUserSetting> LoadEveryoneSettings()
		{
			return this.LoadGroupSettings(-1);
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00007138 File Offset: 0x00005338
		public List<OldUserSetting> LoadAllUserSettings(int WhoAmI)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@pid", DbType.Int32, WhoAmI)
			};
			List<OldUserSetting> result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("IF EXISTS(SELECT * FROM sysobjects WHERE id = object_id(N'[LoadUserSettings]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)\r\n            BEGIN\r\n\t            EXEC LoadUserSettings @pid\r\n            END\r\n            ELSE\r\n            BEGIN\r\n\t            SELECT DISTINCT x.* FROM\r\n\t\t            (\r\n\t\t\t            SELECT\t0 AS settinggroupid,s.settingid,0 AS groupid,s.personid,s.settingcode,s.settingvalue,s.settingstringvalue,0 As ordernum\r\n\t\t\t            FROM\tsettings s \r\n\t\t\t            WHERE\ts.personid=@pid\r\n\r\n\t\t\t            UNION\r\n\r\n\t\t\t            SELECT\tsg.settinggroupid,0 AS settingid,pg.groupid,0 AS personid,sg.settingcode,sg.settingvalue,sg.settingstringvalue,g.ordernum\r\n\t\t\t            FROM\tpeoplegroups pg LEFT JOIN groups g ON g.groupid=pg.groupid\r\n\t\t\t\t\t            LEFT JOIN settingsgroups sg ON sg.groupID=pg.GroupID \r\n\t\t\t            WHERE\tpg.personid=@pid\r\n\r\n\t\t\t            UNION\r\n\r\n\t\t\t            SELECT\tsg.settinggroupid,0 AS settingid,sg.groupid,0 AS personid,sg.settingcode,sg.settingvalue,sg.settingstringvalue,0 AS ordernum\r\n\t\t\t            FROM\tsettingsgroups sg \r\n\t\t\t            WHERE\tsg.groupid=-1 \r\n\t\t            ) x \r\n\t\t            ORDER BY x.personid DESC,x.groupid DESC,x.ordernum\r\n            END", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<OldUserSetting> list = new List<OldUserSetting>();
					while (dataReader.Read())
					{
						OldUserSetting oldSettingFromRecord = OldSettingDAO.GetOldSettingFromRecord(dataReader);
						bool flag2 = oldSettingFromRecord != null;
						if (flag2)
						{
							list.Add(oldSettingFromRecord);
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x000071F0 File Offset: 0x000053F0
		[DebuggerStepThrough]
		public Task<List<OldUserSetting>> LoadAllUserSettingsAsync(int WhoAmI)
		{
			OldSettingDAO.<LoadAllUserSettingsAsync>d__15 <LoadAllUserSettingsAsync>d__ = new OldSettingDAO.<LoadAllUserSettingsAsync>d__15();
			<LoadAllUserSettingsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<List<OldUserSetting>>.Create();
			<LoadAllUserSettingsAsync>d__.<>4__this = this;
			<LoadAllUserSettingsAsync>d__.WhoAmI = WhoAmI;
			<LoadAllUserSettingsAsync>d__.<>1__state = -1;
			<LoadAllUserSettingsAsync>d__.<>t__builder.Start<OldSettingDAO.<LoadAllUserSettingsAsync>d__15>(ref <LoadAllUserSettingsAsync>d__);
			return <LoadAllUserSettingsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x0000723C File Offset: 0x0000543C
		public int CreateOrUpdatePersonSettingValue(OldUserSetting Setting)
		{
			bool flag = Setting.PersonOrGroupId < 1;
			if (flag)
			{
				throw new Exception("Trying to save person setting with no personid.");
			}
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@pid", DbType.Int32, Setting.PersonOrGroupId),
				this.DatabaseManager.GetParameter("@settingcode", DbType.Int32, (int)Setting.SettingCode),
				this.DatabaseManager.GetParameter("@settingstringvalue", DbType.String, Setting.StringVal ?? ""),
				this.DatabaseManager.GetParameter("@settingvalue", DbType.Int32, Setting.IntVal)
			};
			object obj = this.DatabaseManager.ExecuteScalar("IF EXISTS(SELECT settingid FROM settings WHERE personid=@pid AND settingcode=@settingcode)\r\nBEGIN\r\n    UPDATE settings SET settingvalue=@settingvalue,settingstringvalue=@settingstringvalue WHERE personid=@pid AND settingcode=@settingcode\r\n    SELECT settingid FROM settings WHERE personid=@pid AND settingcode=@settingcode\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO settings (personid,settingcode,settingvalue,settingstringvalue) VALUES (@pid,@settingcode,@settingvalue,@settingstringvalue);\r\n    SELECT CAST(SCOPE_IDENTITY() AS int) AS settingid;\r\nEND", parameters);
			return (int)obj;
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x0000730C File Offset: 0x0000550C
		public int CreateOrUpdateGroupSettingValue(OldUserSetting Setting)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@gid", DbType.Int32, (Setting.SettingType == eOldUserSettingType.EveryoneSetting) ? -1 : Setting.PersonOrGroupId),
				this.DatabaseManager.GetParameter("@settingcode", DbType.Int32, (int)Setting.SettingCode),
				this.DatabaseManager.GetParameter("@settingstringvalue", DbType.String, Setting.StringVal ?? ""),
				this.DatabaseManager.GetParameter("@settingvalue", DbType.Int32, Setting.IntVal)
			};
			object obj = this.DatabaseManager.ExecuteScalar("IF EXISTS(SELECT settinggroupid FROM settingsgroups WHERE groupid=@gid AND settingcode=@settingcode)\r\nBEGIN\r\n    UPDATE settingsgroups SET settingvalue=@settingvalue,settingstringvalue=@settingstringvalue WHERE groupid=@gid AND settingcode=@settingcode\r\n    SELECT settinggroupid FROM settingsgroups WHERE groupid=@gid AND settingcode=@settingcode\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO settingsgroups (groupid,settingcode,settingvalue,settingstringvalue) VALUES (@gid,@settingcode,@settingvalue,@settingstringvalue);\r\n    SELECT CAST(SCOPE_IDENTITY() AS int) AS settinggroupid;\r\nEND", parameters);
			return (int)obj;
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x000073D0 File Offset: 0x000055D0
		public void DeletePersonSettingValue(OldUserSetting Setting)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@pid", DbType.Int32, Setting.PersonOrGroupId),
				this.DatabaseManager.GetParameter("@settingcode", DbType.Int32, (int)Setting.SettingCode)
			};
			this.DatabaseManager.ExecuteNonQuery("DELETE FROM settings WHERE personid=@pid AND settingcode=@settingcode", parameters);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00007438 File Offset: 0x00005638
		public void DeleteGroupSettingValue(OldUserSetting Setting)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@gid", DbType.Int32, Setting.PersonOrGroupId),
				this.DatabaseManager.GetParameter("@settingcode", DbType.Int32, (int)Setting.SettingCode)
			};
			this.DatabaseManager.ExecuteNonQuery("DELETE FROM settingsgroups WHERE groupid=@gid AND settingcode=@settingcode", parameters);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x000074A0 File Offset: 0x000056A0
		public OldUserSetting GetUserPersonalSettingValue(int PersonId, eSettingCode SettingCode)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@pid", DbType.Int32, PersonId),
				databaseLayer.GetParameter("@settingcode", DbType.Int32, (int)SettingCode)
			};
			OldUserSetting result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT\t0 AS settinggroupid,s.settingid,0 AS groupid,s.personid,s.settingcode,s.settingvalue,s.settingstringvalue,0 As ordernum\r\nFROM\tsettings s \r\nWHERE\ts.personid=@pid AND s.settingcode=@settingcode\r\nORDER BY s.settingcode", parameters))
			{
				bool flag = dataReader == null || !dataReader.Read();
				if (flag)
				{
					result = null;
				}
				else
				{
					result = OldSettingDAO.GetOldSettingFromRecord(dataReader);
				}
			}
			return result;
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00007548 File Offset: 0x00005748
		public void SetUserPersonalSettingValue(int PersonId, eSettingCode SettingCode, int IntVal, string StringVal)
		{
			bool flag = IntVal == 0 && (StringVal == null || StringVal.Trim().Length < 1);
			if (flag)
			{
				this.DeleteUserPersonalSettingValue(PersonId, SettingCode);
			}
			else
			{
				eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
				OperationContext opContext = this.OpContext;
				DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
				DbParameter[] parameters = new DbParameter[]
				{
					databaseLayer.GetParameter("@pid", DbType.Int32, PersonId),
					databaseLayer.GetParameter("@settingcode", DbType.Int32, (int)SettingCode),
					databaseLayer.GetParameter("@settingvalue", DbType.Int32, IntVal),
					databaseLayer.GetParameter("@settingstringvalue", DbType.String, StringVal ?? "")
				};
				databaseLayer.ExecuteNonQuery("IF EXISTS(SELECT settingid FROM settings WHERE personid=@pid AND settingcode=@settingcode)\r\nBEGIN\r\n    UPDATE settings SET settingvalue=@settingvalue,settingstringvalue=@settingstringvalue WHERE personid=@pid AND settingcode=@settingcode\r\n    SELECT settingid FROM settings WHERE personid=@pid AND settingcode=@settingcode\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO settings (personid,settingcode,settingvalue,settingstringvalue) VALUES (@pid,@settingcode,@settingvalue,@settingstringvalue);\r\n    SELECT CAST(SCOPE_IDENTITY() AS int) AS settingid;\r\nEND", parameters);
			}
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00007610 File Offset: 0x00005810
		private void DeleteUserPersonalSettingValue(int PersonId, eSettingCode SettingCode)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@pid", DbType.Int32, PersonId),
				databaseLayer.GetParameter("@settingcode", DbType.Int32, (int)SettingCode)
			};
			databaseLayer.ExecuteNonQuery("DELETE FROM settings WHERE personid=@pid AND settingcode=@settingcode", parameters);
		}
	}
}
