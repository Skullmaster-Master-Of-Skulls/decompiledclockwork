using System;
using System.Collections.Generic;
using System.Threading;

namespace OracleInternal.Common
{
	// Token: 0x020000A7 RID: 167
	internal class SQLLocalParsePrimaryKeyInfoPool
	{
		// Token: 0x060006E2 RID: 1762 RVA: 0x0003FE24 File Offset: 0x0003E024
		public SQLLocalParsePrimaryKeyInfoPool(int maxCacheSize)
		{
			this.m_maxCacheSize = maxCacheSize;
			this.m_cache = new Dictionary<string, SQLLocalParsePrimaryKeyInfo>(this.m_maxCacheSize);
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x0003FE50 File Offset: 0x0003E050
		private void RemoveLRU()
		{
			string text = null;
			long num = long.MaxValue;
			foreach (KeyValuePair<string, SQLLocalParsePrimaryKeyInfo> keyValuePair in this.m_cache)
			{
				if (keyValuePair.Value.m_lastUsedCount < num)
				{
					num = keyValuePair.Value.m_lastUsedCount;
					text = keyValuePair.Key;
				}
			}
			if (text != null)
			{
				this.m_cache[text].bIsPooled = false;
				this.m_cache.Remove(text);
			}
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x0003FEF0 File Offset: 0x0003E0F0
		public void Put(string cmdText, SQLLocalParsePrimaryKeyInfo metaInfo)
		{
			if (metaInfo.bIsPooled)
			{
				return;
			}
			lock (this.m_sync)
			{
				if (this.m_cache.ContainsKey(cmdText))
				{
					SQLLocalParsePrimaryKeyInfo sqllocalParsePrimaryKeyInfo = this.m_cache[cmdText];
					if (metaInfo.bPkFetched && !sqllocalParsePrimaryKeyInfo.bPkFetched)
					{
						sqllocalParsePrimaryKeyInfo.CopyPrimaryKeyInfoFrom(metaInfo);
						sqllocalParsePrimaryKeyInfo.bPkFetched = true;
					}
					metaInfo = sqllocalParsePrimaryKeyInfo;
				}
				else
				{
					if (this.m_cache.Count >= this.m_maxCacheSize)
					{
						this.RemoveLRU();
					}
					metaInfo.bIsPooled = true;
					metaInfo.m_lastUsedCount = (this.m_lastUsedCount += 1L);
					this.m_cache.Add(cmdText, metaInfo);
				}
			}
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x0003FFB8 File Offset: 0x0003E1B8
		public SQLLocalParsePrimaryKeyInfo Get(string cmdText)
		{
			SQLLocalParsePrimaryKeyInfo sqllocalParsePrimaryKeyInfo = null;
			SQLLocalParsePrimaryKeyInfo result;
			try
			{
				if (this.m_cache.TryGetValue(cmdText, out sqllocalParsePrimaryKeyInfo))
				{
					sqllocalParsePrimaryKeyInfo.m_lastUsedCount = Interlocked.Increment(ref this.m_lastUsedCount);
					result = sqllocalParsePrimaryKeyInfo;
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

		// Token: 0x0400093C RID: 2364
		private object m_sync = new object();

		// Token: 0x0400093D RID: 2365
		private long m_lastUsedCount;

		// Token: 0x0400093E RID: 2366
		private Dictionary<string, SQLLocalParsePrimaryKeyInfo> m_cache;

		// Token: 0x0400093F RID: 2367
		public int m_maxCacheSize;
	}
}
