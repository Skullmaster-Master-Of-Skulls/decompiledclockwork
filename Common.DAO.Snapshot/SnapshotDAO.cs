using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Snapshot;
using TechnoPro.Common.Public.Entities.Snapshot.AppointmentTypes;
using TechnoPro.Common.Public.Entities.Snapshot.DynamicControls;
using TechnoPro.Common.Public.Entities.Snapshot.MailMergeTemplates;
using TechnoPro.Common.Public.Entities.Snapshot.OldSettingsAndPermissions;
using TechnoPro.Common.Public.Entities.Snapshot.PeopleAndGroups;
using TechnoPro.Common.Public.Entities.Snapshot.Reports;
using TechnoPro.Common.Public.Entities.Snapshot.WebSettings;

namespace TechnoPro.Common.DAO.Snapshot
{
	// Token: 0x02000002 RID: 2
	public class SnapshotDAO : ISnapshotDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public SnapshotDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x0000205F File Offset: 0x0000025F
		// (set) Token: 0x06000003 RID: 3 RVA: 0x00002067 File Offset: 0x00000267
		public OperationContext OpContext { get; set; }

		// Token: 0x06000004 RID: 4 RVA: 0x00002070 File Offset: 0x00000270
		private IList<string> GenerateSqlQueriesToReproduceDynamicControlsAndForms2(string DestinationClockWorkDatabasePassword, params int[] ScreenNums)
		{
			if (ScreenNums == null || ScreenNums.Length < 1)
			{
				ScreenNums = this.LoadAllScreenNums();
			}
			List<string> list = new List<string>();
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] array = new DbParameter[1];
			array[0] = databaseLayer.GetParameter("@screennums", DbType.String, string.Join(",", (from g in ScreenNums
			select g.ToString()).ToArray<string>()));
			DbParameter[] parameters = array;
			IEncryption encryption = EncryptionFactory.GetEncryption(databaseLayer.Encryption.Name, DestinationClockWorkDatabasePassword);
			Encryptors encryptors = new Encryptors
			{
				BatchDecryptorSource = databaseLayer.Encryption.GetBatchDecryptor(),
				BatchEncryptorDestination = encryption.GetBatchEncryptor()
			};
			list.Add(this.GetSectionPrefix("LookupGroups"));
			list.AddRange(this.GenerateSqls<SnapshotLookupGroups>(this.LoadItems<SnapshotLookupGroups>(databaseLayer, "SELECT * FROM LookupGroups", parameters), "LookupGroups", "LookupGroupID", encryptors, Array.Empty<string>()));
			list.Add(this.GetSectionSuffix("LookupGroups"));
			list.Add(this.GetSectionPrefix("LookupLists"));
			list.AddRange(this.GenerateSqls<SnapshotLookupLists>(this.LoadItems<SnapshotLookupLists>(databaseLayer, "SELECT * FROM LookupLists", parameters), "LookupLists", "LookupListID", encryptors, Array.Empty<string>()));
			list.Add(this.GetSectionSuffix("LookupLists"));
			list.Add(this.GetSectionPrefix("DynamicControls"));
			list.AddRange(this.GenerateSqls<SnapshotDynamicControl>(this.LoadItems<SnapshotDynamicControl>(databaseLayer, "SELECT * FROM dynamiccontrols WHERE controlid IN (SELECT controlid FROM dynamicscreencontrols WHERE screennum IN (SELECT orderid AS screennum FROM splitorderids(@screennums,',')))", parameters), "dynamiccontrols", "controlid", encryptors, Array.Empty<string>()));
			list.Add(this.GetSectionSuffix("DynamicControls"));
			list.Add(this.GetSectionPrefix("Screens"));
			list.AddRange(this.GenerateSqls<SnapshotScreen>(this.LoadItems<SnapshotScreen>(databaseLayer, "SELECT * FROM screens WHERE screennum IN (SELECT orderid AS screennum FROM splitorderids(@screennums,','))", parameters), "screens", "screennum", encryptors, Array.Empty<string>()));
			list.Add(this.GetSectionSuffix("Screens"));
			list.Add(this.GetSectionPrefix("DynamicScreenControls"));
			list.AddRange(this.GenerateSqls<SnapshotControlScreenMapping>(this.LoadItems<SnapshotControlScreenMapping>(databaseLayer, "SELECT * FROM dynamicscreencontrols WHERE screennum IN (SELECT orderid AS screennum FROM splitorderids(@screennums,','))", parameters), "dynamicscreencontrols", "dynamicscreencontrolid", encryptors, Array.Empty<string>()));
			list.Add(this.GetSectionSuffix("DynamicScreenControls"));
			return list;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000022AC File Offset: 0x000004AC
		private IList<string> GenerateSqlQueriesToReproduceWebSettings2(string DestinationClockWorkDatabasePassword, params string[] InstanceNames)
		{
			List<string> list = new List<string>();
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@instancenames", DbType.String, (InstanceNames == null || InstanceNames.Length < 1) ? DBNull.Value : string.Join(",", InstanceNames.ToArray<string>()))
			};
			IEncryption encryption = EncryptionFactory.GetEncryption(databaseLayer.Encryption.Name, DestinationClockWorkDatabasePassword);
			Encryptors encryptors = new Encryptors
			{
				BatchDecryptorSource = databaseLayer.Encryption.GetBatchDecryptor(),
				BatchEncryptorDestination = encryption.GetBatchEncryptor()
			};
			list.Add(this.GetSectionPrefix("WebSettings"));
			list.AddRange(this.GenerateSqls<SnapshotWebSettings2>(this.LoadItems<SnapshotWebSettings2>(databaseLayer, "SELECT * FROM websettings2 WHERE @instancenames IS NULL OR instancename IN (SELECT orderid AS instancename FROM splitorderids(@instancenames,','))", parameters), "websettings2", "websettingid", encryptors, new string[]
			{
				"settingstringvalue"
			}));
			list.Add(this.GetSectionSuffix("WebSettings"));
			return list;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000239C File Offset: 0x0000059C
		private int[] LoadAllScreenNums()
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			return DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null).ExecuteQuery("SELECT screennum FROM screens").Rows.Cast<DataRow>().Select(delegate(DataRow dr)
			{
				if (!(dr["screennum"] is DBNull))
				{
					return (int)dr["screennum"];
				}
				return 0;
			}).ToArray<int>();
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000023FE File Offset: 0x000005FE
		private IList<T> LoadItems<T>(DatabaseLayer dm, string sql, DbParameter[] parameters)
		{
			return (from DataRow dr in ((parameters == null || parameters.Length < 1) ? dm.ExecuteQuery(sql) : dm.ExecuteQuery(sql, parameters)).Rows
			select this.ExtractItemFromDataRow<T>(dr)).ToList<T>();
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000243C File Offset: 0x0000063C
		private T ExtractItemFromDataRow<T>(DataRow dr)
		{
			PropertyInfo[] properties = typeof(T).GetProperties();
			T t = Activator.CreateInstance<T>();
			foreach (object obj in dr.Table.Columns)
			{
				DataColumn dataColumn = (DataColumn)obj;
				string colName = dataColumn.ColumnName;
				PropertyInfo propertyInfo = properties.FirstOrDefault((PropertyInfo g) => g.Name.Equals(colName, StringComparison.OrdinalIgnoreCase));
				if (propertyInfo != null)
				{
					object value = (dr[colName] is DBNull) ? null : dr[colName];
					propertyInfo.SetValue(t, value, null);
				}
			}
			return t;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002518 File Offset: 0x00000718
		private IEnumerable<string> GenerateSqls<T>(IEnumerable<T> items, string tableName, string primaryKeyColumnName, Encryptors encryptors, params string[] encryptedColumnNames)
		{
			List<string> list = (from g in items
			select this.GenerateSql<T>(g, tableName, primaryKeyColumnName, encryptors, encryptedColumnNames)).ToList<string>();
			list.Insert(0, string.Concat(new string[]
			{
				"IF OBJECTPROPERTY(OBJECT_ID('",
				tableName,
				"'), 'TableHasIdentity') = 1\r\n SET IDENTITY_INSERT ",
				tableName,
				" ON"
			}));
			list.Add(string.Concat(new string[]
			{
				"IF OBJECTPROPERTY(OBJECT_ID('",
				tableName,
				"'), 'TableHasIdentity') = 1\r\n SET IDENTITY_INSERT ",
				tableName,
				" OFF"
			}));
			list.Add("GO");
			return list;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000025EC File Offset: 0x000007EC
		private string GenerateSql<T>(T item, string tableName, string primaryKeyColumnName, Encryptors encryptors, params string[] encryptedColumnNames)
		{
			Dictionary<string, object> source = typeof(T).GetProperties().ToDictionary((PropertyInfo g) => g.Name, (PropertyInfo g) => g.GetValue(item, null));
			string[] array = new string[7];
			array[0] = "INSERT INTO ";
			array[1] = tableName;
			array[2] = " (";
			array[3] = string.Join(",", source.Select(delegate(KeyValuePair<string, object> g)
			{
				if (!g.Key.Equals("description", StringComparison.OrdinalIgnoreCase))
				{
					return g.Key;
				}
				return "[description]";
			}).ToArray<string>());
			array[4] = ") VALUES (";
			array[5] = string.Join(",", (from g in source
			select this.ObjectToStringForSql(g.Key, g.Value, encryptors, encryptedColumnNames)).ToArray<string>());
			array[6] = ")";
			return string.Concat(array);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000026E8 File Offset: 0x000008E8
		private string ObjectToStringForSql(string name, object obj, Encryptors encryptors, params string[] encryptedColumnNames)
		{
			if (obj == null || obj is DBNull)
			{
				return "NULL";
			}
			if (obj is DateTime)
			{
				return "'" + ((DateTime)obj).ToString("yyyy-MM-dd H:mm") + "'";
			}
			if (obj is bool)
			{
				if (!(bool)obj)
				{
					return "0";
				}
				return "1";
			}
			else
			{
				if (!(obj is byte[]))
				{
					return "'" + obj.ToString().Replace("'", "''") + "'";
				}
				byte[] array = (byte[])obj;
				if (encryptedColumnNames != null && encryptedColumnNames.FirstOrDefault((string g) => g.Equals(name, StringComparison.OrdinalIgnoreCase)) != null)
				{
					array = encryptors.BatchEncryptorDestination.Encrypt(encryptors.BatchDecryptorSource.Decrypt(array));
				}
				if (array == null)
				{
					return "NULL";
				}
				return "0x" + BitConverter.ToString(array).Replace("-", "");
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000027E8 File Offset: 0x000009E8
		private string dateStamp
		{
			get
			{
				string result;
				if ((result = this._dateStamp) == null)
				{
					result = (this._dateStamp = DateTime.Now.ToString("yyyy-MM-dd h:mm tt"));
				}
				return result;
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000281A File Offset: 0x00000A1A
		private string GetSectionPrefix(string sectionName)
		{
			return string.Concat(new string[]
			{
				"-- BEGIN ",
				sectionName,
				"  ",
				this.dateStamp,
				"\r\n"
			});
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000284C File Offset: 0x00000A4C
		private string GetSectionSuffix(string sectionName)
		{
			return "-- END " + sectionName + "\r\n";
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002860 File Offset: 0x00000A60
		public IList<string> GenerateSqlQueries(string DestinationClockWorkDatabasePassword, params eSnapshotArea[] areas)
		{
			List<string> list = new List<string>();
			Type typeFromHandle = typeof(SnapshotDAO);
			for (int i = 0; i < areas.Length; i++)
			{
				SnapshotAreaAttribute attribute = areas[i].GetAttribute<SnapshotAreaAttribute>();
				IList<string> collection = (IList<string>)typeFromHandle.GetMethod(attribute.GenerateQueriesMethodName).Invoke(this, new object[]
				{
					DestinationClockWorkDatabasePassword
				});
				list.AddRange(collection);
			}
			return list;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000028CC File Offset: 0x00000ACC
		public IList<string> GenerateSqlQueriesToReproduceReports(string DestinationClockWorkDatabasePassword)
		{
			List<string> list = new List<string>();
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			IEncryption encryption = EncryptionFactory.GetEncryption(databaseLayer.Encryption.Name, DestinationClockWorkDatabasePassword);
			Encryptors encryptors = new Encryptors
			{
				BatchDecryptorSource = databaseLayer.Encryption.GetBatchDecryptor(),
				BatchEncryptorDestination = encryption.GetBatchEncryptor()
			};
			list.Add(this.GetSectionPrefix("SearchDynamicControls"));
			list.AddRange(this.GenerateSqls<SnapshotSearchDynamicControls>(this.LoadItems<SnapshotSearchDynamicControls>(databaseLayer, "SELECT * FROM SearchDynamicControls WHERE controlid IN (SELECT controlid FROM searchdynamicscreencontrols)", null), "SearchDynamicControls", "ControlID", encryptors, Array.Empty<string>()));
			list.Add(this.GetSectionSuffix("SearchDynamicControls"));
			list.Add(this.GetSectionPrefix("SearchDynamicScreenControls"));
			list.AddRange(this.GenerateSqls<SnapshotSearchDynamicScreenControls>(this.LoadItems<SnapshotSearchDynamicScreenControls>(databaseLayer, "SELECT * FROM SearchDynamicScreenControls WHERE controlid IN (SELECT controlid FROM searchdynamiccontrols) AND screennum IN (SELECT screennum FROM searchdynamicscreens)", null), "SearchDynamicScreenControls", "DynamicScreenControlID", encryptors, Array.Empty<string>()));
			list.Add(this.GetSectionSuffix("SearchDynamicScreenControls"));
			list.Add(this.GetSectionPrefix("SearchDynamicScreens"));
			list.AddRange(this.GenerateSqls<SnapshotSearchDynamicScreens>(this.LoadItems<SnapshotSearchDynamicScreens>(databaseLayer, "SELECT * FROM SearchDynamicScreens WHERE screennum IN (SELECT screennum FROM searchdynamicscreencontrols)", null), "SearchDynamicScreens", "screenNum", encryptors, Array.Empty<string>()));
			list.Add(this.GetSectionSuffix("SearchDynamicScreens"));
			list.Add(this.GetSectionPrefix("SearchGroupInfo"));
			list.AddRange(this.GenerateSqls<SnapshotSearchGroupInfo>(this.LoadItems<SnapshotSearchGroupInfo>(databaseLayer, "SELECT * FROM SearchGroupInfo", null), "SearchGroupInfo", "SearchGroupInfoID", encryptors, Array.Empty<string>()));
			list.Add(this.GetSectionSuffix("SearchGroupInfo"));
			list.Add(this.GetSectionPrefix("SearchInfo"));
			list.AddRange(this.GenerateSqls<SnapshotSearchInfo>(this.LoadItems<SnapshotSearchInfo>(databaseLayer, "SELECT * FROM SearchInfo", null), "SearchInfo", "SearchInfoID", encryptors, Array.Empty<string>()));
			list.Add(this.GetSectionSuffix("SearchInfo"));
			list.Add(this.GetSectionPrefix("SearchFunctions"));
			list.AddRange(this.GenerateSqls<SnapshotSearchFunctions>(this.LoadItems<SnapshotSearchFunctions>(databaseLayer, "SELECT * FROM SearchFunctions", null), "SearchFunctions", "SearchFunctionID", encryptors, Array.Empty<string>()));
			list.Add(this.GetSectionSuffix("SearchFunctions"));
			return list;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002AF0 File Offset: 0x00000CF0
		public IList<string> GenerateSqlQueriesToReproduceMailMergeTemplates(string DestinationClockWorkDatabasePassword)
		{
			List<string> list = new List<string>();
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			IEncryption encryption = EncryptionFactory.GetEncryption(databaseLayer.Encryption.Name, DestinationClockWorkDatabasePassword);
			Encryptors encryptors = new Encryptors
			{
				BatchDecryptorSource = databaseLayer.Encryption.GetBatchDecryptor(),
				BatchEncryptorDestination = encryption.GetBatchEncryptor()
			};
			list.Add(this.GetSectionPrefix("EmailTemplateGroups"));
			list.AddRange(this.GenerateSqls<SnapshotEmailTemplateGroups>(this.LoadItems<SnapshotEmailTemplateGroups>(databaseLayer, "SELECT * FROM EmailTemplateGroups", null), "EmailTemplateGroups", "templategroupid", encryptors, Array.Empty<string>()));
			list.Add(this.GetSectionSuffix("EmailTemplateGroups"));
			list.Add(this.GetSectionPrefix("EmailTemplates"));
			list.AddRange(this.GenerateSqls<SnapshotEmailTemplates>(this.LoadItems<SnapshotEmailTemplates>(databaseLayer, "SELECT * FROM EmailTemplates", null), "EmailTemplates", "templateid", encryptors, Array.Empty<string>()));
			list.Add(this.GetSectionSuffix("EmailTemplates"));
			list.Add(this.GetSectionPrefix("EmailTemplateFiles"));
			list.AddRange(this.GenerateSqls<SnapshotEmailTemplateFiles>(this.LoadItems<SnapshotEmailTemplateFiles>(databaseLayer, "SELECT * FROM EmailTemplateFiles", null), "EmailTemplateFiles", "FileId", encryptors, Array.Empty<string>()));
			list.Add(this.GetSectionSuffix("EmailTemplateFiles"));
			return list;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002C34 File Offset: 0x00000E34
		public IList<string> GenerateSqlQueriesToReproduceAppointmentTypes(string DestinationClockWorkDatabasePassword)
		{
			List<string> list = new List<string>();
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			IEncryption encryption = EncryptionFactory.GetEncryption(databaseLayer.Encryption.Name, DestinationClockWorkDatabasePassword);
			Encryptors encryptors = new Encryptors
			{
				BatchDecryptorSource = databaseLayer.Encryption.GetBatchDecryptor(),
				BatchEncryptorDestination = encryption.GetBatchEncryptor()
			};
			list.Add(this.GetSectionPrefix("AppointmentTypeGroups"));
			list.AddRange(this.GenerateSqls<SnapshotAppointmentTypeGroup>(this.LoadItems<SnapshotAppointmentTypeGroup>(databaseLayer, "SELECT * FROM AppointmentTypeGroups WHERE appointmenttypegroupid IN (SELECT appointmenttypegroupid FROM appointmenttypes WHERE isactive=1)", null), "appointmenttypegroups", "appointmenttypegroupid", encryptors, Array.Empty<string>()));
			list.Add(this.GetSectionSuffix("AppointmentTypeGroups"));
			list.Add(this.GetSectionPrefix("AppointmentTypes"));
			list.AddRange(this.GenerateSqls<SnapshotAppointmentType>(this.LoadItems<SnapshotAppointmentType>(databaseLayer, "SELECT * FROM AppointmentTypes WHERE isactive=1", null), "appointmenttypes", "apptypeid", encryptors, Array.Empty<string>()));
			list.Add(this.GetSectionSuffix("AppointmentTypes"));
			list.Add(this.GetSectionPrefix("Workshops"));
			list.AddRange(this.GenerateSqls<SnapshotWorkshop>(this.LoadItems<SnapshotWorkshop>(databaseLayer, "SELECT * FROM workshops WHERE isactive=1 AND apptypeid IN (SELECT apptypeid FROM appointmenttypes WHERE isactive=1)", null), "workshops", "workshopid", encryptors, Array.Empty<string>()));
			list.Add(this.GetSectionSuffix("Workshops"));
			return list;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002D78 File Offset: 0x00000F78
		public IList<string> GenerateSqlQueriesToReproduceOldSettingsAndPermissions(string DestinationClockWorkDatabasePassword)
		{
			List<string> list = new List<string>();
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			IEncryption encryption = EncryptionFactory.GetEncryption(databaseLayer.Encryption.Name, DestinationClockWorkDatabasePassword);
			Encryptors encryptors = new Encryptors
			{
				BatchDecryptorSource = databaseLayer.Encryption.GetBatchDecryptor(),
				BatchEncryptorDestination = encryption.GetBatchEncryptor()
			};
			list.Add(this.GetSectionPrefix("Settings"));
			list.AddRange(this.GenerateSqls<SnapshotSettings>(this.LoadItems<SnapshotSettings>(databaseLayer, "SELECT * FROM settings WHERE personid IN (SELECT personid FROM people WHERE isactive=1)", null), "settings", "settingid", encryptors, Array.Empty<string>()));
			list.Add(this.GetSectionSuffix("Settings"));
			list.Add(this.GetSectionPrefix("SettingsGroups"));
			list.AddRange(this.GenerateSqls<SnapshotSettingsGroups>(this.LoadItems<SnapshotSettingsGroups>(databaseLayer, "SELECT * FROM settingsgroups WHERE groupid=-1 OR groupid IN (SELECT groupid FROM groups)", null), "settingsgroups", "settinggroupid", encryptors, Array.Empty<string>()));
			list.Add(this.GetSectionSuffix("SettingsGroups"));
			list.Add(this.GetSectionPrefix("Permissions"));
			list.AddRange(this.GenerateSqls<SnapshotPermission>(this.LoadItems<SnapshotPermission>(databaseLayer, "SELECT * FROM permissions WHERE personid IN (SELECT personid FROM people WHERE isactive=1)", null), "settingsgroups", "settinggroupid", encryptors, Array.Empty<string>()));
			list.Add(this.GetSectionSuffix("Permissions"));
			list.Add(this.GetSectionPrefix("PermissionsGroups"));
			list.AddRange(this.GenerateSqls<SnapshotPermissionGroup>(this.LoadItems<SnapshotPermissionGroup>(databaseLayer, "SELECT * FROM permissionsgroups WHERE groupid=-1 OR groupid IN (SELECT groupid FROM groups)", null), "permissionsgroups", "permissiongroupid", encryptors, Array.Empty<string>()));
			list.Add(this.GetSectionSuffix("PermissionsGroups"));
			return list;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002F04 File Offset: 0x00001104
		public IList<string> GenerateSqlQueriesToReproducePeopleAndGroups(string DestinationClockWorkDatabasePassword)
		{
			List<string> list = new List<string>();
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			IEncryption encryption = EncryptionFactory.GetEncryption(databaseLayer.Encryption.Name, DestinationClockWorkDatabasePassword);
			Encryptors encryptors = new Encryptors
			{
				BatchDecryptorSource = databaseLayer.Encryption.GetBatchDecryptor(),
				BatchEncryptorDestination = encryption.GetBatchEncryptor()
			};
			list.Add(this.GetSectionPrefix("Groups"));
			list.AddRange(this.GenerateSqls<SnapshotGroup>(this.LoadItems<SnapshotGroup>(databaseLayer, "SELECT * FROM groups", null), "groups", "groupid", encryptors, Array.Empty<string>()));
			list.Add(this.GetSectionSuffix("Groups"));
			list.Add(this.GetSectionPrefix("People"));
			list.AddRange(this.GenerateSqls<SnapshotPeople>(this.LoadItems<SnapshotPeople>(databaseLayer, "SELECT * FROM people WHERE isactive=1", null), "people", "personid", encryptors, new string[]
			{
				"firstname",
				"middlename",
				"lastname",
				"student_no"
			}));
			list.Add(this.GetSectionSuffix("People"));
			list.Add(this.GetSectionPrefix("PeopleGroups"));
			list.AddRange(this.GenerateSqls<SnapshotPeopleGroup>(this.LoadItems<SnapshotPeopleGroup>(databaseLayer, "SELECT * FROM peoplegroups WHERE personid IN (SELECT personid FROM people WHERE isactive=1)", null), "peoplegroups", "persongroupid", encryptors, Array.Empty<string>()));
			list.Add(this.GetSectionSuffix("PeopleGroups"));
			list.Add(this.GetSectionPrefix("UserInfo"));
			list.AddRange(this.GenerateSqls<SnapShotUserInfo>(this.LoadItems<SnapShotUserInfo>(databaseLayer, "SELECT * FROM userinfo WHERE isencrypted=1 AND personid IN (SELECT personid FROM people WHERE isactive=1)", null), "userinfo", "username", encryptors, new string[]
			{
				"username",
				"pass"
			}));
			list.Add(this.GetSectionSuffix("UserInfo"));
			list.Add(this.GetSectionPrefix("UserInfo"));
			list.AddRange(this.GenerateSqls<SnapShotUserInfo>(this.LoadItems<SnapShotUserInfo>(databaseLayer, "SELECT * FROM userinfo WHERE isencrypted=0 AND personid IN (SELECT personid FROM people WHERE isactive=1)", null), "userinfo", "username", encryptors, new string[]
			{
				"username"
			}));
			list.Add(this.GetSectionSuffix("UserInfo"));
			return list;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00003118 File Offset: 0x00001318
		public IList<string> GenerateSqlQueriesToReproduceWebSettings(string DestinationClockWorkDatabasePassword)
		{
			return this.GenerateSqlQueriesToReproduceWebSettings2(DestinationClockWorkDatabasePassword, null);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00003122 File Offset: 0x00001322
		public IList<string> GenerateSqlQueriesToReproduceDynamicControlsAndForms(string DestinationClockWorkDatabasePassword)
		{
			return this.GenerateSqlQueriesToReproduceDynamicControlsAndForms2(DestinationClockWorkDatabasePassword, null);
		}

		// Token: 0x04000002 RID: 2
		private string _dateStamp;
	}
}
