using System;
using System.Diagnostics;
using OracleInternal.Network;

namespace OracleInternal.Common
{
	// Token: 0x020000A1 RID: 161
	internal class OraBufPool
	{
		// Token: 0x060006C4 RID: 1732 RVA: 0x0003E810 File Offset: 0x0003CA10
		public OraBufPool(int maxSubCacheSize)
		{
			this.m_poolId = this.GetHashCode().ToString();
			this.m_bufPoolerCapacity = maxSubCacheSize - 1;
			this.m_smallBufPooler = new OraBuf[this.m_bufPoolerCapacity + 1];
			this.m_largeBufPooler = new OraBuf[this.m_bufPoolerCapacity + 1];
			this.m_sduChangeSync = new object();
			this.m_smallBufPoolerSync = new object();
			this.m_largeBufPoolerSync = new object();
			this.m_smallBufPoolerPos = -1;
			this.m_largeBufPoolerPos = -1;
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				StackTrace stackTrace = new StackTrace();
				string name = stackTrace.GetFrame(2).GetMethod().Name;
				string name2 = stackTrace.GetFrame(2).GetMethod().ReflectedType.Name;
				string text = string.Format("(OBP.CTOR) (poolid:{0}) ({1}.{2})", this.m_poolId, name2, name);
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.BUF, new string[]
				{
					text
				});
			}
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x0003E8FC File Offset: 0x0003CAFC
		public void Output(string method, int bufId, int key, int count)
		{
			StackTrace stackTrace = new StackTrace();
			string name = stackTrace.GetFrame(2).GetMethod().Name;
			string name2 = stackTrace.GetFrame(2).GetMethod().ReflectedType.Name;
			string text = string.Format("({0}) (poolid:{1}) (key:{2}) (bufid:{3}) (count:{4}) ({5}.{6})", new object[]
			{
				method,
				this.m_poolId,
				key,
				bufId,
				count,
				name2,
				name
			});
			Trace.Write(OracleTraceLevel.Private, OracleTraceTag.BUF, new string[]
			{
				text
			});
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x0003E9A0 File Offset: 0x0003CBA0
		internal void UpdateBufSizes(OracleCommunication oc)
		{
			lock (this.m_sduChangeSync)
			{
				if (this.m_smallBufSize != oc.m_sessionCtx.m_sessionDataUnit)
				{
					int sessionDataUnit = oc.m_sessionCtx.m_sessionDataUnit;
					int num = sessionDataUnit * 4;
					if (sessionDataUnit != this.m_smallBufSize && sessionDataUnit != this.m_largeBufSize)
					{
						lock (this.m_smallBufPoolerSync)
						{
							while (this.m_smallBufPoolerPos >= 0)
							{
								this.m_smallBufPooler[this.m_smallBufPoolerPos] = null;
								this.m_smallBufPoolerPos--;
							}
						}
					}
					if (num != this.m_smallBufSize && num != this.m_largeBufSize)
					{
						lock (this.m_largeBufPoolerSync)
						{
							while (this.m_largeBufPoolerPos >= 0)
							{
								this.m_largeBufPooler[this.m_largeBufPoolerPos] = null;
								this.m_largeBufPoolerPos--;
							}
						}
					}
					this.m_smallBufSize = sessionDataUnit;
					this.m_largeBufSize = num;
				}
			}
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x0003EADC File Offset: 0x0003CCDC
		public OraBuf Get(int key, OracleCommunication oc, bool bReceive)
		{
			OraBuf oraBuf = null;
			int count = 0;
			if (key == this.m_smallBufSize)
			{
				if (this.m_smallBufPoolerPos < 0)
				{
					goto IL_F4;
				}
				lock (this.m_smallBufPoolerSync)
				{
					if (key == this.m_smallBufSize && this.m_smallBufPoolerPos >= 0)
					{
						oraBuf = this.m_smallBufPooler[this.m_smallBufPoolerPos];
						this.m_smallBufPooler[this.m_smallBufPoolerPos--] = null;
						count = this.m_smallBufPoolerPos + 1;
					}
					goto IL_F4;
				}
			}
			if (key == this.m_largeBufSize && this.m_largeBufPoolerPos >= 0)
			{
				lock (this.m_largeBufPoolerSync)
				{
					if (key == this.m_largeBufSize && this.m_largeBufPoolerPos >= 0)
					{
						oraBuf = this.m_largeBufPooler[this.m_largeBufPoolerPos];
						this.m_largeBufPooler[this.m_largeBufPoolerPos--] = null;
						count = this.m_largeBufPoolerPos + 1;
					}
				}
			}
			IL_F4:
			if (oraBuf == null)
			{
				oraBuf = new OraBuf(oc, key, bReceive);
			}
			else
			{
				oraBuf.ReInit(bReceive);
			}
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				this.Output("OBP.GET", oraBuf.m_id, key, count);
			}
			return oraBuf;
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x0003EC2C File Offset: 0x0003CE2C
		public OraBuf GetUninitialized(int key, OracleCommunication oc)
		{
			OraBuf oraBuf = null;
			int count = 0;
			if (key == this.m_smallBufSize)
			{
				if (this.m_smallBufPoolerPos < 0)
				{
					goto IL_ED;
				}
				lock (this.m_smallBufPoolerSync)
				{
					if (key == this.m_smallBufSize && this.m_smallBufPoolerPos >= 0)
					{
						oraBuf = this.m_smallBufPooler[this.m_smallBufPoolerPos];
						count = this.m_smallBufPoolerPos;
						this.m_smallBufPooler[this.m_smallBufPoolerPos--] = null;
					}
					goto IL_ED;
				}
			}
			if (key == this.m_largeBufSize && this.m_largeBufPoolerPos >= 0)
			{
				lock (this.m_largeBufPoolerSync)
				{
					if (key == this.m_largeBufSize && this.m_largeBufPoolerPos >= 0)
					{
						oraBuf = this.m_largeBufPooler[this.m_largeBufPoolerPos];
						count = this.m_smallBufPoolerPos;
						this.m_largeBufPooler[this.m_largeBufPoolerPos--] = null;
					}
				}
			}
			IL_ED:
			if (oraBuf == null)
			{
				oraBuf = new OraBuf(oc, key, true);
			}
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				this.Output("OBP.GET", oraBuf.m_id, key, count);
			}
			return oraBuf;
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x0003ED6C File Offset: 0x0003CF6C
		public void Put(int key, OraBuf oraBuf)
		{
			int count = 0;
			if (oraBuf.m_size == this.m_smallBufSize)
			{
				if (this.m_smallBufPoolerPos >= this.m_bufPoolerCapacity)
				{
					goto IL_DD;
				}
				lock (this.m_smallBufPoolerSync)
				{
					if (this.m_smallBufPoolerPos < this.m_bufPoolerCapacity)
					{
						this.m_smallBufPooler[++this.m_smallBufPoolerPos] = oraBuf;
						count = this.m_smallBufPoolerPos + 1;
					}
					goto IL_DD;
				}
			}
			if (oraBuf.m_size == this.m_largeBufSize && this.m_largeBufPoolerPos < this.m_bufPoolerCapacity)
			{
				lock (this.m_largeBufPoolerSync)
				{
					if (this.m_largeBufPoolerPos < this.m_bufPoolerCapacity)
					{
						this.m_largeBufPooler[++this.m_largeBufPoolerPos] = oraBuf;
						count = this.m_smallBufPoolerPos + 1;
					}
				}
			}
			IL_DD:
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				this.Output("OBP.PUT", oraBuf.m_id, oraBuf.m_size, count);
			}
		}

		// Token: 0x04000914 RID: 2324
		internal string m_poolId;

		// Token: 0x04000915 RID: 2325
		private OraBuf[] m_smallBufPooler;

		// Token: 0x04000916 RID: 2326
		private OraBuf[] m_largeBufPooler;

		// Token: 0x04000917 RID: 2327
		private object m_smallBufPoolerSync;

		// Token: 0x04000918 RID: 2328
		private object m_largeBufPoolerSync;

		// Token: 0x04000919 RID: 2329
		private object m_sduChangeSync;

		// Token: 0x0400091A RID: 2330
		private int m_smallBufPoolerPos;

		// Token: 0x0400091B RID: 2331
		private int m_largeBufPoolerPos;

		// Token: 0x0400091C RID: 2332
		internal int m_smallBufSize;

		// Token: 0x0400091D RID: 2333
		internal int m_largeBufSize;

		// Token: 0x0400091E RID: 2334
		private int m_bufPoolerCapacity;
	}
}
