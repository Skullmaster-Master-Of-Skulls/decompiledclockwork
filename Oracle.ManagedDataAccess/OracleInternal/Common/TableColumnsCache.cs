using System;
using System.Collections.Generic;
using System.Threading;

namespace OracleInternal.Common
{
	// Token: 0x020000A8 RID: 168
	internal class TableColumnsCache
	{
		// Token: 0x060006E6 RID: 1766 RVA: 0x00040008 File Offset: 0x0003E208
		internal void Put(string serviceName, string schemaName, string tableName, OracleLpTableColumns tableColumns)
		{
			string arg = string.Empty;
			if (this.tableColumnsCache.Count >= ConfigBaseClass.m_ColumnCacheSize)
			{
				arg = string.Concat(new string[]
				{
					"Column Cache FULL. Cannot add table columns to cache for service: ",
					serviceName,
					", table: ",
					tableName,
					", Schema: ",
					schemaName
				});
				arg = arg + ", Column Cache Size: " + this.tableColumnsCache.Count;
				return;
			}
			Tuple<string, string, string> key = new Tuple<string, string, string>(serviceName, schemaName, tableName);
			if (this.tableColumnsCache.ContainsKey(key))
			{
				return;
			}
			this.m_sync_rw.EnterWriteLock();
			try
			{
				if (this.tableColumnsCache.Count >= ConfigBaseClass.m_ColumnCacheSize || this.tableColumnsCache.ContainsKey(key))
				{
					return;
				}
				this.tableColumnsCache.Add(key, tableColumns);
			}
			finally
			{
				this.m_sync_rw.ExitWriteLock();
			}
			arg = string.Concat(new string[]
			{
				"Added table columns to cache for service: ",
				serviceName,
				", table: ",
				tableName,
				", Schema: ",
				schemaName
			});
			arg = arg + ", Column Cache Size: " + this.tableColumnsCache.Count;
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x0004013C File Offset: 0x0003E33C
		internal OracleLpTableColumns Get(string serviceName, string schemaName, string tableName)
		{
			OracleLpTableColumns result = null;
			string empty = string.Empty;
			if (this.tableColumnsCache.Count == 0)
			{
				return result;
			}
			Tuple<string, string, string> key = new Tuple<string, string, string>(serviceName, schemaName, tableName);
			try
			{
				if (this.tableColumnsCache.ContainsKey(key))
				{
					this.m_sync_rw.EnterReadLock();
					try
					{
						if (this.tableColumnsCache.ContainsKey(key))
						{
							result = this.tableColumnsCache[key];
						}
					}
					finally
					{
						this.m_sync_rw.ExitReadLock();
					}
					string.Concat(new string[]
					{
						"Found table columns in cache for service: ",
						serviceName,
						", table: ",
						tableName,
						", Schema: ",
						schemaName
					});
				}
				else
				{
					string.Concat(new string[]
					{
						"Cannot find table columns in cache for service: ",
						serviceName,
						", table: ",
						tableName,
						", Schema: ",
						schemaName
					});
				}
			}
			catch
			{
			}
			return result;
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x00040234 File Offset: 0x0003E434
		internal void Clear()
		{
			this.m_sync_rw.EnterWriteLock();
			try
			{
				this.tableColumnsCache.Clear();
			}
			finally
			{
				this.m_sync_rw.ExitWriteLock();
			}
		}

		// Token: 0x04000940 RID: 2368
		private Dictionary<Tuple<string, string, string>, OracleLpTableColumns> tableColumnsCache = new Dictionary<Tuple<string, string, string>, OracleLpTableColumns>();

		// Token: 0x04000941 RID: 2369
		private ReaderWriterLockSlim m_sync_rw = new ReaderWriterLockSlim();
	}
}
