using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Security.Permissions;
using System.Text;
using System.Web.Util;

namespace System.Web.Management
{
	// Token: 0x02000179 RID: 377
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.High)]
	public static class SqlServices
	{
		// Token: 0x060014B5 RID: 5301 RVA: 0x0003E400 File Offset: 0x0003C600
		public static void Install(string server, string user, string password, string database, SqlFeatures features)
		{
			SqlServices.SetupApplicationServices(server, user, password, false, null, database, null, features, true);
		}

		// Token: 0x060014B6 RID: 5302 RVA: 0x0003E41C File Offset: 0x0003C61C
		public static void Install(string server, string database, SqlFeatures features)
		{
			SqlServices.SetupApplicationServices(server, null, null, true, null, database, null, features, true);
		}

		// Token: 0x060014B7 RID: 5303 RVA: 0x0003E438 File Offset: 0x0003C638
		internal static void Install(string database, string dbFileName, string connectionString)
		{
			SqlServices.SetupApplicationServices(null, null, null, false, connectionString, database, dbFileName, SqlFeatures.All, true);
		}

		// Token: 0x060014B8 RID: 5304 RVA: 0x0003E458 File Offset: 0x0003C658
		public static void Install(string database, SqlFeatures features, string connectionString)
		{
			SqlServices.SetupApplicationServices(null, null, null, true, connectionString, database, null, features, true);
		}

		// Token: 0x060014B9 RID: 5305 RVA: 0x0003E474 File Offset: 0x0003C674
		public static void Uninstall(string server, string user, string password, string database, SqlFeatures features)
		{
			SqlServices.SetupApplicationServices(server, user, password, false, null, database, null, features, false);
		}

		// Token: 0x060014BA RID: 5306 RVA: 0x0003E490 File Offset: 0x0003C690
		public static void Uninstall(string server, string database, SqlFeatures features)
		{
			SqlServices.SetupApplicationServices(server, null, null, true, null, database, null, features, false);
		}

		// Token: 0x060014BB RID: 5307 RVA: 0x0003E4AC File Offset: 0x0003C6AC
		public static void Uninstall(string database, SqlFeatures features, string connectionString)
		{
			SqlServices.SetupApplicationServices(null, null, null, true, connectionString, database, null, features, false);
		}

		// Token: 0x060014BC RID: 5308 RVA: 0x0003E4C7 File Offset: 0x0003C6C7
		public static void InstallSessionState(string server, string user, string password, string customDatabase, SessionStateType type)
		{
			SqlServices.SetupSessionState(server, user, password, false, null, customDatabase, type, true);
		}

		// Token: 0x060014BD RID: 5309 RVA: 0x0003E4D7 File Offset: 0x0003C6D7
		public static void InstallSessionState(string server, string customDatabase, SessionStateType type)
		{
			SqlServices.SetupSessionState(server, null, null, true, null, customDatabase, type, true);
		}

		// Token: 0x060014BE RID: 5310 RVA: 0x0003E4E6 File Offset: 0x0003C6E6
		public static void InstallSessionState(string customDatabase, SessionStateType type, string connectionString)
		{
			SqlServices.SetupSessionState(null, null, null, true, connectionString, customDatabase, type, true);
		}

		// Token: 0x060014BF RID: 5311 RVA: 0x0003E4F5 File Offset: 0x0003C6F5
		public static void UninstallSessionState(string server, string user, string password, string customDatabase, SessionStateType type)
		{
			SqlServices.SetupSessionState(server, user, password, false, null, customDatabase, type, false);
		}

		// Token: 0x060014C0 RID: 5312 RVA: 0x0003E505 File Offset: 0x0003C705
		public static void UninstallSessionState(string server, string customDatabase, SessionStateType type)
		{
			SqlServices.SetupSessionState(server, null, null, true, null, customDatabase, type, false);
		}

		// Token: 0x060014C1 RID: 5313 RVA: 0x0003E514 File Offset: 0x0003C714
		public static void UninstallSessionState(string customDatabase, SessionStateType type, string connectionString)
		{
			SqlServices.SetupSessionState(null, null, null, true, connectionString, customDatabase, type, false);
		}

		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x060014C2 RID: 5314 RVA: 0x0003E524 File Offset: 0x0003C724
		internal static ArrayList ApplicationServiceTables
		{
			get
			{
				ArrayList arrayList = new ArrayList();
				for (int i = 0; i < SqlServices.s_featureInfos.Length; i++)
				{
					arrayList.InsertRange(arrayList.Count, SqlServices.s_featureInfos[i]._tablesRemovedInUninstall);
				}
				return arrayList;
			}
		}

		// Token: 0x060014C3 RID: 5315 RVA: 0x0003E568 File Offset: 0x0003C768
		public static string GenerateSessionStateScripts(bool install, SessionStateType type, string customDatabase)
		{
			SqlServices.SessionStateParamCheck(type, ref customDatabase);
			string path = Path.Combine(HttpRuntime.AspInstallDirectory, install ? SqlServices.SESSION_STATE_INSTALL_FILE : SqlServices.SESSION_STATE_UNINSTALL_FILE);
			string content = File.ReadAllText(path);
			return SqlServices.FixContent(content, customDatabase, null, true, type);
		}

		// Token: 0x060014C4 RID: 5316 RVA: 0x0003E5A8 File Offset: 0x0003C7A8
		private static ArrayList GetFiles(bool install, SqlFeatures features)
		{
			ArrayList arrayList = new ArrayList();
			bool flag = false;
			for (int i = 0; i < SqlServices.s_featureInfos.Length; i++)
			{
				string[] array = null;
				if ((SqlServices.s_featureInfos[i]._feature & features) == SqlServices.s_featureInfos[i]._feature)
				{
					if (install)
					{
						array = SqlServices.s_featureInfos[i]._installFiles;
					}
					else
					{
						array = SqlServices.s_featureInfos[i]._uninstallFiles;
					}
				}
				if (array != null)
				{
					foreach (string text in array)
					{
						if (text != null && (!(text == SqlServices.INSTALL_COMMON_SQL) || !flag))
						{
							arrayList.Add(text);
							if (!flag && text == SqlServices.INSTALL_COMMON_SQL)
							{
								flag = true;
							}
						}
					}
				}
			}
			return arrayList;
		}

		// Token: 0x060014C5 RID: 5317 RVA: 0x0003E670 File Offset: 0x0003C870
		private static string FixContent(string content, string database, string dbFileName, bool sessionState, SessionStateType sessionStatetype)
		{
			if (database != null)
			{
				database = SqlServices.RemoveSquareBrackets(database);
			}
			if (sessionState)
			{
				if (sessionStatetype != SessionStateType.Temporary)
				{
					if (sessionStatetype == SessionStateType.Persisted)
					{
						content = content.Replace("'sstype_temp'", "'" + SqlServices.SSTYPE_PERSISTED + "'");
						content = content.Replace("[tempdb]", "[" + SqlServices.ASPSTATE_DB + "]");
					}
					else if (sessionStatetype == SessionStateType.Custom)
					{
						content = content.Replace("'sstype_temp'", "'" + SqlServices.SSTYPE_CUSTOM + "'");
						content = content.Replace("[tempdb]", "[" + database + "]");
						content = content.Replace("'ASPState'", "'" + database + "'");
						content = content.Replace("[ASPState]", "[" + database + "]");
					}
				}
			}
			else
			{
				content = content.Replace("'aspnetdb'", "'" + database.Replace("'", "''") + "'");
				content = content.Replace("[aspnetdb]", "[" + database + "]");
			}
			if (dbFileName != null)
			{
				if (dbFileName.Contains("[") || dbFileName.Contains("]") || dbFileName.Contains("'"))
				{
					throw new ArgumentException(SR.GetString("DbFileName_can_not_contain_invalid_chars"));
				}
				database = database.TrimStart(new char[]
				{
					'['
				});
				database = database.TrimEnd(new char[]
				{
					']'
				});
				string text = database + "_DAT";
				if (!char.IsLetter(text[0]))
				{
					text = "A" + text;
				}
				string str = string.Concat(new string[]
				{
					"ON ( NAME = ",
					text,
					", FILENAME = ''",
					dbFileName,
					"'', SIZE = 10MB, FILEGROWTH = 5MB )"
				});
				content = content.Replace("SET @dboptions = N'/**/'", "SET @dboptions = N'" + str + "'");
			}
			return content;
		}

		// Token: 0x060014C6 RID: 5318 RVA: 0x0003E87F File Offset: 0x0003CA7F
		private static void ExecuteSessionFile(string file, string server, string database, string dbFileName, SqlConnection connection, bool isInstall, SessionStateType sessionStatetype)
		{
			SqlServices.ExecuteFile(file, server, database, dbFileName, connection, true, isInstall, sessionStatetype);
		}

		// Token: 0x060014C7 RID: 5319 RVA: 0x0003E894 File Offset: 0x0003CA94
		private static void ExecuteFile(string file, string server, string database, string dbFileName, SqlConnection connection, bool sessionState, bool isInstall, SessionStateType sessionStatetype)
		{
			string path = Path.Combine(HttpRuntime.AspInstallDirectory, file);
			string text = File.ReadAllText(path);
			string text2 = null;
			if (file.Equals(SqlServices.INSTALL_COMMON_SQL))
			{
				text = SqlServices.FixContent(text, database, dbFileName, sessionState, sessionStatetype);
			}
			else
			{
				text = SqlServices.FixContent(text, database, null, sessionState, sessionStatetype);
			}
			StringReader stringReader = new StringReader(text);
			SqlCommand sqlCommand = new SqlCommand(null, connection);
			string text3;
			do
			{
				bool flag = false;
				text3 = stringReader.ReadLine();
				if (text3 == null)
				{
					flag = true;
				}
				else if (StringUtil.EqualsIgnoreCase(text3.Trim(), "GO"))
				{
					flag = true;
				}
				else
				{
					if (text2 != null)
					{
						text2 += "\n";
					}
					text2 += text3;
				}
				if (flag & text2 != null)
				{
					sqlCommand.CommandText = text2;
					try
					{
						sqlCommand.ExecuteNonQuery();
					}
					catch (Exception ex)
					{
						SqlException ex2 = ex as SqlException;
						if (ex2 != null)
						{
							int num = -1;
							if (text2.IndexOf("sp_add_category", StringComparison.Ordinal) > -1)
							{
								num = 14261;
							}
							else if (text2.IndexOf("sp_delete_job", StringComparison.Ordinal) > -1)
							{
								num = 14262;
								if (sessionState && !isInstall)
								{
									throw new SqlExecutionException(SR.GetString("SQL_Services_Error_Deleting_Session_Job"), server, database, file, text2, ex2);
								}
							}
							if (ex2.Number != num)
							{
								throw new SqlExecutionException(SR.GetString("SQL_Services_Error_Executing_Command", new object[]
								{
									file,
									ex2.Number.ToString(CultureInfo.CurrentCulture),
									ex2.Message
								}), server, database, file, text2, ex2);
							}
						}
					}
					catch
					{
						throw;
					}
					text2 = null;
				}
			}
			while (text3 != null);
		}

		// Token: 0x060014C8 RID: 5320 RVA: 0x0003EA30 File Offset: 0x0003CC30
		private static void ApplicationServicesParamCheck(SqlFeatures features, ref string database)
		{
			if (features == SqlFeatures.None)
			{
				return;
			}
			if ((features & SqlFeatures.All) != features)
			{
				throw new ArgumentException(SR.GetString("SQL_Services_Invalid_Feature"));
			}
			SqlServices.CheckDatabaseName(ref database);
		}

		// Token: 0x060014C9 RID: 5321 RVA: 0x0003EA58 File Offset: 0x0003CC58
		private static void CheckDatabaseName(ref string database)
		{
			if (database != null)
			{
				database = database.TrimEnd(new char[0]);
				if (database.Length == 0)
				{
					throw new ArgumentException(SR.GetString("SQL_Services_Database_Empty_Or_Space_Only_Arg"));
				}
				database = SqlServices.RemoveSquareBrackets(database);
				if (database.Contains("'") || database.Contains("[") || database.Contains("]"))
				{
					throw new ArgumentException(SR.GetString("SQL_Services_Database_contains_invalid_chars"));
				}
			}
			if (database == null)
			{
				database = SqlServices.DEFAULT_DB;
				return;
			}
			database = "[" + database + "]";
		}

		// Token: 0x060014CA RID: 5322 RVA: 0x0003EAF4 File Offset: 0x0003CCF4
		public static string GenerateApplicationServicesScripts(bool install, SqlFeatures features, string database)
		{
			StringBuilder stringBuilder = new StringBuilder();
			SqlServices.ApplicationServicesParamCheck(features, ref database);
			ArrayList files = SqlServices.GetFiles(install, features);
			foreach (object obj in files)
			{
				string path = (string)obj;
				string path2 = Path.Combine(HttpRuntime.AspInstallDirectory, path);
				string content = File.ReadAllText(path2);
				stringBuilder.Append(SqlServices.FixContent(content, database, null, false, SessionStateType.Temporary));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060014CB RID: 5323 RVA: 0x0003EB88 File Offset: 0x0003CD88
		private static string RemoveSquareBrackets(string database)
		{
			if (database != null && StringUtil.StringStartsWith(database, '[') && StringUtil.StringEndsWith(database, ']'))
			{
				return database.Substring(1, database.Length - 2);
			}
			return database;
		}

		// Token: 0x060014CC RID: 5324 RVA: 0x0003EBB4 File Offset: 0x0003CDB4
		private static void EnsureDatabaseExists(string database, SqlConnection sqlConnection)
		{
			string text = SqlServices.RemoveSquareBrackets(database);
			object obj = new SqlCommand("SELECT DB_ID(@database)", sqlConnection)
			{
				Parameters = 
				{
					new SqlParameter("@database", text)
				}
			}.ExecuteScalar();
			if (obj == null || obj == DBNull.Value)
			{
				throw new HttpException(SR.GetString("SQL_Services_Error_Cant_Uninstall_Nonexisting_Database", new object[]
				{
					text
				}));
			}
		}

		// Token: 0x060014CD RID: 5325 RVA: 0x0003EC18 File Offset: 0x0003CE18
		private static void SetupApplicationServices(string server, string user, string password, bool trusted, string connectionString, string database, string dbFileName, SqlFeatures features, bool install)
		{
			SqlConnection sqlConnection = null;
			SqlServices.ApplicationServicesParamCheck(features, ref database);
			ArrayList files = SqlServices.GetFiles(install, features);
			try
			{
				sqlConnection = SqlServices.GetSqlConnection(server, user, password, trusted, connectionString);
				if (!install)
				{
					SqlServices.EnsureDatabaseExists(database, sqlConnection);
					string text = SqlServices.RemoveSquareBrackets(database);
					if (sqlConnection.Database != text)
					{
						sqlConnection.ChangeDatabase(text);
					}
					int num = 0;
					for (int i = 0; i < SqlServices.s_featureInfos.Length; i++)
					{
						if ((SqlServices.s_featureInfos[i]._feature & features) == SqlServices.s_featureInfos[i]._feature)
						{
							num |= SqlServices.s_featureInfos[i]._dataCheckBitMask;
						}
					}
					SqlCommand sqlCommand = new SqlCommand("dbo.aspnet_AnyDataInTables", sqlConnection);
					sqlCommand.Parameters.Add(new SqlParameter("@TablesToCheck", num));
					sqlCommand.CommandType = CommandType.StoredProcedure;
					string text2 = null;
					try
					{
						text2 = (sqlCommand.ExecuteScalar() as string);
					}
					catch (SqlException ex)
					{
						if (ex.Number != 2812)
						{
							throw;
						}
					}
					if (!string.IsNullOrEmpty(text2))
					{
						throw new NotSupportedException(SR.GetString("SQL_Services_Error_Cant_Uninstall_Nonempty_Table", new object[]
						{
							text2,
							database
						}));
					}
				}
				foreach (object obj in files)
				{
					string file = (string)obj;
					SqlServices.ExecuteFile(file, server, database, dbFileName, sqlConnection, false, false, SessionStateType.Temporary);
				}
			}
			finally
			{
				if (sqlConnection != null)
				{
					try
					{
						sqlConnection.Close();
					}
					catch
					{
					}
					finally
					{
						sqlConnection = null;
					}
				}
			}
		}

		// Token: 0x060014CE RID: 5326 RVA: 0x0003EE20 File Offset: 0x0003D020
		private static void SessionStateParamCheck(SessionStateType type, ref string customDatabase)
		{
			if (type == SessionStateType.Custom && string.IsNullOrEmpty(customDatabase))
			{
				throw new ArgumentException(SR.GetString("SQL_Services_Error_missing_custom_database"), "customDatabase");
			}
			if (type != SessionStateType.Custom && customDatabase != null)
			{
				throw new ArgumentException(SR.GetString("SQL_Services_Error_Cant_use_custom_database"), "customDatabase");
			}
			SqlServices.CheckDatabaseName(ref customDatabase);
		}

		// Token: 0x060014CF RID: 5327 RVA: 0x0003EE74 File Offset: 0x0003D074
		private static void SetupSessionState(string server, string user, string password, bool trusted, string connectionString, string customDatabase, SessionStateType type, bool install)
		{
			SqlConnection sqlConnection = null;
			SqlServices.SessionStateParamCheck(type, ref customDatabase);
			try
			{
				sqlConnection = SqlServices.GetSqlConnection(server, user, password, trusted, connectionString);
				if (!install && type == SessionStateType.Custom)
				{
					SqlServices.EnsureDatabaseExists(customDatabase, sqlConnection);
				}
				SqlServices.ExecuteSessionFile(install ? SqlServices.SESSION_STATE_INSTALL_FILE : SqlServices.SESSION_STATE_UNINSTALL_FILE, server, customDatabase, null, sqlConnection, install, type);
			}
			finally
			{
				if (sqlConnection != null)
				{
					try
					{
						sqlConnection.Close();
					}
					catch
					{
					}
					finally
					{
						sqlConnection = null;
					}
				}
			}
		}

		// Token: 0x060014D0 RID: 5328 RVA: 0x0003EF04 File Offset: 0x0003D104
		private static string ConstructConnectionString(string server, string user, string password, bool trusted)
		{
			string text = null;
			if (string.IsNullOrEmpty(server))
			{
				throw ExceptionUtil.ParameterNullOrEmpty("server");
			}
			text = text + "server=" + server;
			if (trusted)
			{
				text += ";Trusted_Connection=true;";
			}
			else
			{
				if (string.IsNullOrEmpty(user))
				{
					throw ExceptionUtil.ParameterNullOrEmpty("user");
				}
				text = string.Concat(new string[]
				{
					text,
					";UID=",
					user,
					";PWD=",
					password,
					";"
				});
			}
			return text;
		}

		// Token: 0x060014D1 RID: 5329 RVA: 0x0003EF88 File Offset: 0x0003D188
		private static SqlConnection GetSqlConnection(string server, string user, string password, bool trusted, string connectionString)
		{
			if (connectionString == null)
			{
				connectionString = SqlServices.ConstructConnectionString(server, user, password, trusted);
			}
			SqlConnection sqlConnection;
			try
			{
				sqlConnection = new SqlConnection(connectionString);
				sqlConnection.Open();
			}
			catch (Exception innerException)
			{
				sqlConnection = null;
				throw new HttpException(SR.GetString("SQL_Services_Cant_connect_sql_database"), innerException);
			}
			return sqlConnection;
		}

		// Token: 0x04001580 RID: 5504
		private static string INSTALL_COMMON_SQL = "InstallCommon.sql";

		// Token: 0x04001581 RID: 5505
		private static SqlServices.FeatureInfo[] s_featureInfos = new SqlServices.FeatureInfo[]
		{
			new SqlServices.FeatureInfo(SqlFeatures.Membership, new string[]
			{
				SqlServices.INSTALL_COMMON_SQL,
				"InstallMembership.sql"
			}, new string[]
			{
				"UninstallMembership.sql"
			}, new string[]
			{
				"aspnet_Membership"
			}, 1),
			new SqlServices.FeatureInfo(SqlFeatures.Profile, new string[]
			{
				SqlServices.INSTALL_COMMON_SQL,
				"InstallProfile.sql"
			}, new string[]
			{
				"UninstallProfile.sql"
			}, new string[]
			{
				"aspnet_Profile"
			}, 4),
			new SqlServices.FeatureInfo(SqlFeatures.RoleManager, new string[]
			{
				SqlServices.INSTALL_COMMON_SQL,
				"InstallRoles.sql"
			}, new string[]
			{
				"UninstallRoles.sql"
			}, new string[]
			{
				"aspnet_Roles",
				"aspnet_UsersInRoles"
			}, 2),
			new SqlServices.FeatureInfo(SqlFeatures.Personalization, new string[]
			{
				SqlServices.INSTALL_COMMON_SQL,
				"InstallPersonalization.sql"
			}, new string[]
			{
				"UninstallPersonalization.sql"
			}, new string[]
			{
				"aspnet_PersonalizationPerUser",
				"aspnet_Paths",
				"aspnet_PersonalizationAllUsers"
			}, 8),
			new SqlServices.FeatureInfo(SqlFeatures.SqlWebEventProvider, new string[]
			{
				SqlServices.INSTALL_COMMON_SQL,
				"InstallWebEventSqlProvider.sql"
			}, new string[]
			{
				"UninstallWebEventSqlProvider.sql"
			}, new string[]
			{
				"aspnet_WebEvent_Events"
			}, 16),
			new SqlServices.FeatureInfo(SqlFeatures.All, new string[0], new string[]
			{
				"UninstallCommon.sql"
			}, new string[]
			{
				"aspnet_Applications",
				"aspnet_Users",
				"aspnet_SchemaVersions"
			}, int.MaxValue)
		};

		// Token: 0x04001582 RID: 5506
		private static string DEFAULT_DB = "aspnetdb";

		// Token: 0x04001583 RID: 5507
		private static string ASPSTATE_DB = "ASPState";

		// Token: 0x04001584 RID: 5508
		private static string SSTYPE_PERSISTED = "sstype_persisted";

		// Token: 0x04001585 RID: 5509
		private static string SSTYPE_CUSTOM = "sstype_custom";

		// Token: 0x04001586 RID: 5510
		private static string SESSION_STATE_INSTALL_FILE = "InstallSqlState.sql";

		// Token: 0x04001587 RID: 5511
		private static string SESSION_STATE_UNINSTALL_FILE = "UninstallSqlState.sql";

		// Token: 0x0200090E RID: 2318
		internal struct FeatureInfo
		{
			// Token: 0x060068F8 RID: 26872 RVA: 0x00175F0E File Offset: 0x0017410E
			internal FeatureInfo(SqlFeatures feature, string[] installFiles, string[] uninstallFiles, string[] tablesRemovedInUninstall, int dataCheckBitMask)
			{
				this._feature = feature;
				this._installFiles = installFiles;
				this._uninstallFiles = uninstallFiles;
				this._tablesRemovedInUninstall = tablesRemovedInUninstall;
				this._dataCheckBitMask = dataCheckBitMask;
			}

			// Token: 0x0400371A RID: 14106
			internal SqlFeatures _feature;

			// Token: 0x0400371B RID: 14107
			internal string[] _installFiles;

			// Token: 0x0400371C RID: 14108
			internal string[] _uninstallFiles;

			// Token: 0x0400371D RID: 14109
			internal string[] _tablesRemovedInUninstall;

			// Token: 0x0400371E RID: 14110
			internal int _dataCheckBitMask;
		}
	}
}
