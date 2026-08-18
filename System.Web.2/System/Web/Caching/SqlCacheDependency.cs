using System;
using System.Collections;
using System.Data.SqlClient;
using System.Globalization;
using System.Security;
using System.Security.Permissions;
using System.Web.Hosting;
using System.Web.Util;

namespace System.Web.Caching
{
	// Token: 0x0200088E RID: 2190
	public sealed class SqlCacheDependency : CacheDependency
	{
		// Token: 0x060066EA RID: 26346 RVA: 0x0016A984 File Offset: 0x00168B84
		public SqlCacheDependency(string databaseEntryName, string tableName) : base(0, null, new string[]
		{
			SqlCacheDependency.GetDependKey(databaseEntryName, tableName)
		})
		{
			this._sql7DatabaseState = SqlCacheDependencyManager.AddRef(databaseEntryName);
			this._sql7DepInfo._database = databaseEntryName;
			this._sql7DepInfo._table = tableName;
			object obj = HttpRuntime.Cache.InternalCache.Get(SqlCacheDependency.GetDependKey(databaseEntryName, tableName));
			if (obj == null)
			{
				this._sql7ChangeId = -1;
			}
			else
			{
				this._sql7ChangeId = (int)obj;
			}
			base.FinishInit();
			this.InitUniqueID();
		}

		// Token: 0x060066EB RID: 26347 RVA: 0x0016AA08 File Offset: 0x00168C08
		protected override void DependencyDispose()
		{
			if (this._sql7DatabaseState != null)
			{
				SqlCacheDependencyManager.Release(this._sql7DatabaseState);
			}
		}

		// Token: 0x060066EC RID: 26348 RVA: 0x0016AA20 File Offset: 0x00168C20
		public SqlCacheDependency(SqlCommand sqlCmd)
		{
			HttpContext httpContext = HttpContext.Current;
			if (sqlCmd == null)
			{
				throw new ArgumentNullException("sqlCmd");
			}
			if (httpContext != null && httpContext.SqlDependencyCookie != null && sqlCmd.NotificationAutoEnlist)
			{
				throw new HttpException(SR.GetString("SqlCacheDependency_OutputCache_Conflict"));
			}
			this.CreateSqlDep(sqlCmd);
			this.InitUniqueID();
		}

		// Token: 0x060066ED RID: 26349 RVA: 0x0016AA78 File Offset: 0x00168C78
		private void InitUniqueID()
		{
			if (this._sqlYukonDep != null)
			{
				this._uniqueID = Guid.NewGuid().ToString("N", CultureInfo.InvariantCulture);
				return;
			}
			if (this._sql7ChangeId == -1)
			{
				this._uniqueID = null;
				return;
			}
			this._uniqueID = string.Concat(new string[]
			{
				this._sql7DepInfo._database,
				":",
				this._sql7DepInfo._table,
				":",
				this._sql7ChangeId.ToString(CultureInfo.InvariantCulture)
			});
		}

		// Token: 0x060066EE RID: 26350 RVA: 0x0016AB0C File Offset: 0x00168D0C
		public override string GetUniqueID()
		{
			return this._uniqueID;
		}

		// Token: 0x060066EF RID: 26351 RVA: 0x0016AB14 File Offset: 0x00168D14
		private static void CheckPermission()
		{
			if (!SqlCacheDependency.s_hasSqlClientPermissionInited)
			{
				if (!HostingEnvironment.IsHosted)
				{
					try
					{
						new SqlClientPermission(PermissionState.Unrestricted).Demand();
						SqlCacheDependency.s_hasSqlClientPermission = true;
						goto IL_2E;
					}
					catch (SecurityException)
					{
						goto IL_2E;
					}
				}
				SqlCacheDependency.s_hasSqlClientPermission = Permission.HasSqlClientPermission();
				IL_2E:
				SqlCacheDependency.s_hasSqlClientPermissionInited = true;
			}
			if (!SqlCacheDependency.s_hasSqlClientPermission)
			{
				throw new HttpException(SR.GetString("SqlCacheDependency_permission_denied"));
			}
		}

		// Token: 0x060066F0 RID: 26352 RVA: 0x00168907 File Offset: 0x00166B07
		private void OnSQL9SqlDependencyChanged(object sender, SqlNotificationEventArgs e)
		{
			base.NotifyDependencyChanged(sender, e);
		}

		// Token: 0x060066F1 RID: 26353 RVA: 0x0016AB7C File Offset: 0x00168D7C
		private SqlCacheDependency()
		{
			this.CreateSqlDep(null);
			this.InitUniqueID();
		}

		// Token: 0x060066F2 RID: 26354 RVA: 0x0016AB91 File Offset: 0x00168D91
		private void CreateSqlDep(SqlCommand sqlCmd)
		{
			this._sqlYukonDep = new SqlDependency();
			if (sqlCmd != null)
			{
				this._sqlYukonDep.AddCommandDependency(sqlCmd);
			}
			this._sqlYukonDep.OnChange += this.OnSQL9SqlDependencyChanged;
		}

		// Token: 0x060066F3 RID: 26355 RVA: 0x0016ABC4 File Offset: 0x00168DC4
		internal static void ValidateOutputCacheDependencyString(string depString, bool page)
		{
			if (depString == null)
			{
				throw new HttpException(SR.GetString("Invalid_sqlDependency_argument", new object[]
				{
					depString
				}));
			}
			if (StringUtil.EqualsIgnoreCase(depString, "CommandNotification"))
			{
				if (!page)
				{
					throw new HttpException(SR.GetString("Attrib_Sql9_not_allowed"));
				}
			}
			else
			{
				SqlCacheDependency.ParseSql7OutputCacheDependency(depString);
			}
		}

		// Token: 0x060066F4 RID: 26356 RVA: 0x0016AC18 File Offset: 0x00168E18
		public static CacheDependency CreateOutputCacheDependency(string dependency)
		{
			if (dependency == null)
			{
				throw new HttpException(SR.GetString("Invalid_sqlDependency_argument", new object[]
				{
					dependency
				}));
			}
			if (StringUtil.EqualsIgnoreCase(dependency, "CommandNotification"))
			{
				HttpContext httpContext = HttpContext.Current;
				SqlCacheDependency sqlCacheDependency = new SqlCacheDependency();
				httpContext.SqlDependencyCookie = sqlCacheDependency._sqlYukonDep.Id;
				return sqlCacheDependency;
			}
			ArrayList arrayList = SqlCacheDependency.ParseSql7OutputCacheDependency(dependency);
			if (arrayList.Count == 1)
			{
				SqlCacheDependency.Sql7DependencyInfo sql7DependencyInfo = (SqlCacheDependency.Sql7DependencyInfo)arrayList[0];
				return SqlCacheDependency.CreateSql7SqlCacheDependencyForOutputCache(sql7DependencyInfo._database, sql7DependencyInfo._table, dependency);
			}
			AggregateCacheDependency aggregateCacheDependency = new AggregateCacheDependency();
			for (int i = 0; i < arrayList.Count; i++)
			{
				SqlCacheDependency.Sql7DependencyInfo sql7DependencyInfo = (SqlCacheDependency.Sql7DependencyInfo)arrayList[i];
				aggregateCacheDependency.Add(new CacheDependency[]
				{
					SqlCacheDependency.CreateSql7SqlCacheDependencyForOutputCache(sql7DependencyInfo._database, sql7DependencyInfo._table, dependency)
				});
			}
			return aggregateCacheDependency;
		}

		// Token: 0x060066F5 RID: 26357 RVA: 0x0016ACF4 File Offset: 0x00168EF4
		private static SqlCacheDependency CreateSql7SqlCacheDependencyForOutputCache(string database, string table, string depString)
		{
			SqlCacheDependency result;
			try
			{
				result = new SqlCacheDependency(database, table);
			}
			catch (HttpException ex)
			{
				HttpException ex2 = new HttpException(SR.GetString("Invalid_sqlDependency_argument2", new object[]
				{
					depString,
					ex.Message
				}), ex);
				ex2.SetFormatter(new UseLastUnhandledErrorFormatter(ex2));
				throw ex2;
			}
			return result;
		}

		// Token: 0x060066F6 RID: 26358 RVA: 0x0016AD50 File Offset: 0x00168F50
		private static string GetDependKey(string database, string tableName)
		{
			SqlCacheDependency.CheckPermission();
			if (database == null)
			{
				throw new ArgumentNullException("database");
			}
			if (tableName == null)
			{
				throw new ArgumentNullException("tableName");
			}
			if (tableName.Length == 0)
			{
				throw new ArgumentException(SR.GetString("Cache_null_table"));
			}
			string moniterKey = SqlCacheDependencyManager.GetMoniterKey(database, tableName);
			SqlCacheDependencyManager.EnsureTableIsRegisteredAndPolled(database, tableName);
			return moniterKey;
		}

		// Token: 0x060066F7 RID: 26359 RVA: 0x0016ADA8 File Offset: 0x00168FA8
		private static string VerifyAndRemoveEscapeCharacters(string s)
		{
			bool flag = false;
			for (int i = 0; i < s.Length; i++)
			{
				if (flag)
				{
					if (s[i] != '\\' && s[i] != ':' && s[i] != ';')
					{
						throw new ArgumentException();
					}
					flag = false;
				}
				else if (s[i] == '\\')
				{
					if (i + 1 == s.Length)
					{
						throw new ArgumentException();
					}
					flag = true;
					s = s.Remove(i, 1);
					i--;
				}
			}
			return s;
		}

		// Token: 0x060066F8 RID: 26360 RVA: 0x0016AE24 File Offset: 0x00169024
		internal static ArrayList ParseSql7OutputCacheDependency(string outputCacheString)
		{
			bool flag = false;
			int num = 0;
			int num2 = -1;
			string text = null;
			ArrayList arrayList = null;
			ArrayList result;
			try
			{
				for (int i = 0; i < outputCacheString.Length + 1; i++)
				{
					if (flag)
					{
						flag = false;
					}
					else if (i != outputCacheString.Length && outputCacheString[i] == '\\')
					{
						flag = true;
					}
					else
					{
						if (i == outputCacheString.Length || outputCacheString[i] == ';')
						{
							if (text == null)
							{
								throw new ArgumentException();
							}
							int num3 = i - num2;
							if (num3 == 0)
							{
								throw new ArgumentException();
							}
							SqlCacheDependency.Sql7DependencyInfo sql7DependencyInfo = default(SqlCacheDependency.Sql7DependencyInfo);
							sql7DependencyInfo._database = SqlCacheDependency.VerifyAndRemoveEscapeCharacters(text);
							sql7DependencyInfo._table = SqlCacheDependency.VerifyAndRemoveEscapeCharacters(outputCacheString.Substring(num2, num3));
							if (arrayList == null)
							{
								arrayList = new ArrayList(1);
							}
							arrayList.Add(sql7DependencyInfo);
							num = i + 1;
							text = null;
						}
						if (i == outputCacheString.Length)
						{
							break;
						}
						if (outputCacheString[i] == ':')
						{
							if (text != null)
							{
								throw new ArgumentException();
							}
							int num3 = i - num;
							if (num3 == 0)
							{
								throw new ArgumentException();
							}
							text = outputCacheString.Substring(num, num3);
							num2 = i + 1;
						}
					}
				}
				result = arrayList;
			}
			catch (ArgumentException)
			{
				throw new ArgumentException(SR.GetString("Invalid_sqlDependency_argument", new object[]
				{
					outputCacheString
				}));
			}
			return result;
		}

		// Token: 0x04003505 RID: 13573
		internal static bool s_hasSqlClientPermission;

		// Token: 0x04003506 RID: 13574
		internal static bool s_hasSqlClientPermissionInited;

		// Token: 0x04003507 RID: 13575
		private const string SQL9_CACHE_DEPENDENCY_DIRECTIVE = "CommandNotification";

		// Token: 0x04003508 RID: 13576
		internal const string SQL9_OUTPUT_CACHE_DEPENDENCY_COOKIE = "MS.SqlDependencyCookie";

		// Token: 0x04003509 RID: 13577
		private SqlDependency _sqlYukonDep;

		// Token: 0x0400350A RID: 13578
		private DatabaseNotifState _sql7DatabaseState;

		// Token: 0x0400350B RID: 13579
		private string _uniqueID;

		// Token: 0x0400350C RID: 13580
		private SqlCacheDependency.Sql7DependencyInfo _sql7DepInfo;

		// Token: 0x0400350D RID: 13581
		private int _sql7ChangeId;

		// Token: 0x02000A77 RID: 2679
		private struct Sql7DependencyInfo
		{
			// Token: 0x04003BBC RID: 15292
			internal string _database;

			// Token: 0x04003BBD RID: 15293
			internal string _table;
		}
	}
}
