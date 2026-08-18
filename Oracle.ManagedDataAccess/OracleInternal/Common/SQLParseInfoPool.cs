using System;
using System.Collections.Generic;
using System.Threading;

namespace OracleInternal.Common
{
	// Token: 0x020000A9 RID: 169
	internal class SQLParseInfoPool
	{
		// Token: 0x060006EA RID: 1770 RVA: 0x00040298 File Offset: 0x0003E498
		internal SQLParseInfoPool(int maxCacheSize)
		{
			this.m_maxCacheSize = maxCacheSize;
			this.m_cache = new Dictionary<string, SQLParseInfoPool.SQLParseInfo>(this.m_maxCacheSize);
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x000402C4 File Offset: 0x0003E4C4
		private void RemoveLRU()
		{
			string text = null;
			long num = long.MaxValue;
			foreach (KeyValuePair<string, SQLParseInfoPool.SQLParseInfo> keyValuePair in this.m_cache)
			{
				if (keyValuePair.Value.m_lastUsedCount < num)
				{
					num = keyValuePair.Value.m_lastUsedCount;
					text = keyValuePair.Key;
				}
			}
			if (text != null)
			{
				this.m_cache.Remove(text);
			}
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x00040350 File Offset: 0x0003E550
		internal void Put(string cmdText, string cmdTextWithRowId, bool hadRowId)
		{
			if (this.m_cache.ContainsKey(cmdText))
			{
				return;
			}
			lock (this.m_sync)
			{
				if (!this.m_cache.ContainsKey(cmdText))
				{
					if (this.m_cache.Count >= this.m_maxCacheSize)
					{
						this.RemoveLRU();
					}
					this.m_cache.Add(cmdText, new SQLParseInfoPool.SQLParseInfo
					{
						hasRowId = hadRowId,
						sqlWithRowId = cmdTextWithRowId,
						m_lastUsedCount = (this.m_LastUsedCount += 1L)
					});
				}
			}
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x000403FC File Offset: 0x0003E5FC
		internal string Get(string cmdText, out bool hadRowId)
		{
			hadRowId = false;
			SQLParseInfoPool.SQLParseInfo sqlparseInfo = null;
			string result;
			try
			{
				if (this.m_cache.TryGetValue(cmdText, out sqlparseInfo))
				{
					sqlparseInfo.m_lastUsedCount = Interlocked.Increment(ref this.m_LastUsedCount);
					hadRowId = sqlparseInfo.hasRowId;
					result = sqlparseInfo.sqlWithRowId;
				}
				else
				{
					result = null;
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x04000942 RID: 2370
		private object m_sync = new object();

		// Token: 0x04000943 RID: 2371
		private long m_LastUsedCount;

		// Token: 0x04000944 RID: 2372
		private Dictionary<string, SQLParseInfoPool.SQLParseInfo> m_cache;

		// Token: 0x04000945 RID: 2373
		internal int m_maxCacheSize;

		// Token: 0x020000AA RID: 170
		private class SQLParseInfo
		{
			// Token: 0x04000946 RID: 2374
			internal bool hasRowId;

			// Token: 0x04000947 RID: 2375
			internal string sqlWithRowId;

			// Token: 0x04000948 RID: 2376
			internal long m_lastUsedCount;
		}
	}
}
