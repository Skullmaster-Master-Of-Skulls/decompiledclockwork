using System;
using System.Collections.Generic;
using OracleInternal.ServiceObjects;

namespace OracleInternal.Common
{
	// Token: 0x020000A6 RID: 166
	internal class StatementCache
	{
		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x060006DB RID: 1755 RVA: 0x0003FAC0 File Offset: 0x0003DCC0
		internal int Count
		{
			get
			{
				if (this.m_cache != null)
				{
					return this.m_cache.Count;
				}
				return 0;
			}
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x0003FAD8 File Offset: 0x0003DCD8
		internal List<long> Purge(int targetSize = 0)
		{
			List<long> list = new List<long>();
			lock (this.m_sync)
			{
				for (int i = this.m_cache.Count; i > targetSize; i--)
				{
					CachedStatement cachedStatement = this.RemoveLRU();
					if (cachedStatement != null)
					{
						list.Add((long)cachedStatement.m_cursorId);
					}
				}
			}
			return list;
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x0003FB4C File Offset: 0x0003DD4C
		internal StatementCache(int maxCacheSize)
		{
			this.m_maxCacheSize = maxCacheSize;
			this.m_cache = new Dictionary<string, CachedStatement>(this.m_maxCacheSize);
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x0003FB78 File Offset: 0x0003DD78
		internal CachedStatement RemoveLRU()
		{
			string text = null;
			ulong num = ulong.MaxValue;
			CachedStatement cachedStatement = null;
			foreach (KeyValuePair<string, CachedStatement> keyValuePair in this.m_cache)
			{
				if (keyValuePair.Value.m_lastUsedCount < num)
				{
					num = keyValuePair.Value.m_lastUsedCount;
					text = keyValuePair.Key;
				}
			}
			if (text != null)
			{
				cachedStatement = this.m_cache[text];
				cachedStatement.m_bIsPooled = false;
				if (!cachedStatement.m_hasExclusiveOwnershipOfCursorInfo)
				{
					cachedStatement = null;
				}
				this.m_cache.Remove(text);
			}
			return cachedStatement;
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x0003FC20 File Offset: 0x0003DE20
		internal CachedStatement Put(string cmdText, CachedStatement cachedStmnt)
		{
			if (cachedStmnt.m_bIsPooled)
			{
				cachedStmnt.m_lastUsedCount = (this.m_lastUsedCount += 1UL);
				cachedStmnt.m_hasExclusiveOwnershipOfCursorInfo = true;
				return null;
			}
			CachedStatement cachedStatement = null;
			CachedStatement result;
			lock (this.m_sync)
			{
				if (this.m_cache.TryGetValue(cmdText, out cachedStatement))
				{
					if (cachedStatement.m_hasExclusiveOwnershipOfCursorInfo)
					{
						result = cachedStmnt;
					}
					else
					{
						cachedStmnt.m_hasExclusiveOwnershipOfCursorInfo = true;
						cachedStmnt.m_lastUsedCount = (this.m_lastUsedCount += 1UL);
						this.m_cache[cmdText] = cachedStmnt;
						result = null;
					}
				}
				else
				{
					if (this.m_cache.Count >= this.m_maxCacheSize)
					{
						cachedStatement = this.RemoveLRU();
					}
					cachedStmnt.m_hasExclusiveOwnershipOfCursorInfo = true;
					cachedStmnt.m_lastUsedCount = (this.m_lastUsedCount += 1UL);
					cachedStmnt.m_bIsPooled = true;
					this.m_cache.Add(cmdText, cachedStmnt);
					result = cachedStatement;
				}
			}
			return result;
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x0003FD2C File Offset: 0x0003DF2C
		internal void Get(string cmdText, out CachedStatement cachedStmnt, out SQLMetaData metadata, out SQLInfo sqlInfo)
		{
			sqlInfo = null;
			metadata = null;
			cachedStmnt = null;
			lock (this.m_sync)
			{
				if (this.m_cache.TryGetValue(cmdText, out cachedStmnt))
				{
					sqlInfo = cachedStmnt.sqlInfo;
					metadata = cachedStmnt.statementdata;
					CachedStatement cachedStatement = cachedStmnt;
					ulong lastUsedCount;
					this.m_lastUsedCount = (lastUsedCount = this.m_lastUsedCount) + 1UL;
					cachedStatement.m_lastUsedCount = lastUsedCount;
					if (!cachedStmnt.m_hasExclusiveOwnershipOfCursorInfo)
					{
						cachedStmnt = null;
					}
					else
					{
						cachedStmnt.m_hasExclusiveOwnershipOfCursorInfo = false;
					}
				}
			}
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x0003FDC4 File Offset: 0x0003DFC4
		internal bool PeekForSQLMetaInfo(string cmdText, out SQLInfo info, out SQLMetaData data)
		{
			data = null;
			info = null;
			try
			{
				CachedStatement cachedStatement = null;
				if (this.m_cache.TryGetValue(cmdText, out cachedStatement))
				{
					data = cachedStatement.statementdata;
					info = cachedStatement.sqlInfo;
				}
			}
			catch
			{
				data = null;
				info = null;
			}
			return data != null && info != null;
		}

		// Token: 0x04000938 RID: 2360
		private object m_sync = new object();

		// Token: 0x04000939 RID: 2361
		private ulong m_lastUsedCount;

		// Token: 0x0400093A RID: 2362
		private Dictionary<string, CachedStatement> m_cache;

		// Token: 0x0400093B RID: 2363
		internal int m_maxCacheSize;
	}
}
