using System;
using System.Collections.Generic;
using System.Threading;
using Oracle.ManagedDataAccess.Client;

namespace OracleInternal.Common
{
	// Token: 0x020000AB RID: 171
	internal class DeriveParamInfoPool
	{
		// Token: 0x060006EF RID: 1775 RVA: 0x00040464 File Offset: 0x0003E664
		internal DeriveParamInfoPool(int maxCacheSize)
		{
			this.m_maxCacheSize = maxCacheSize;
			this.m_cache = new Dictionary<string, DeriveParamInfo>(this.m_maxCacheSize);
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x00040490 File Offset: 0x0003E690
		private void RemoveLRU()
		{
			string text = null;
			long num = long.MaxValue;
			foreach (KeyValuePair<string, DeriveParamInfo> keyValuePair in this.m_cache)
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

		// Token: 0x170001B2 RID: 434
		internal DeriveParamInfo this[string cmdText]
		{
			get
			{
				DeriveParamInfo result;
				try
				{
					if (cmdText == null)
					{
						result = null;
					}
					else
					{
						cmdText = cmdText.Trim();
						if (cmdText.Length == 0)
						{
							result = null;
						}
						else
						{
							DeriveParamInfo deriveParamInfo = null;
							if (this.m_cache.TryGetValue(cmdText, out deriveParamInfo))
							{
								deriveParamInfo.m_lastUsedCount = Interlocked.Increment(ref this.m_LastUsedCount);
								result = deriveParamInfo;
							}
							else
							{
								result = null;
							}
						}
					}
				}
				catch
				{
					result = null;
				}
				return result;
			}
			set
			{
				if (value == null || cmdText == null)
				{
					return;
				}
				cmdText = cmdText.Trim();
				if (cmdText.Length == 0)
				{
					return;
				}
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
						value.m_lastUsedCount = (this.m_LastUsedCount += 1L);
						this.m_cache.Add(cmdText, value);
					}
				}
			}
		}

		// Token: 0x04000949 RID: 2377
		private object m_sync = new object();

		// Token: 0x0400094A RID: 2378
		private long m_LastUsedCount;

		// Token: 0x0400094B RID: 2379
		private Dictionary<string, DeriveParamInfo> m_cache;

		// Token: 0x0400094C RID: 2380
		private int m_maxCacheSize;
	}
}
