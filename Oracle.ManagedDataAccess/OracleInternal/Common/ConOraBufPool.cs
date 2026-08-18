using System;
using System.Diagnostics;
using OracleInternal.Network;

namespace OracleInternal.Common
{
	// Token: 0x020000A2 RID: 162
	internal class ConOraBufPool
	{
		// Token: 0x060006CA RID: 1738 RVA: 0x0003EE94 File Offset: 0x0003D094
		public ConOraBufPool(OraBufPool obp)
		{
			lock (ConOraBufPool.m_idSync)
			{
				this.m_poolId = ++ConOraBufPool.m_sId;
			}
			this.m_obp = obp;
			this.m_bufPoolerCapacity = 127;
			this.m_smallBufPooler = new OraBuf[this.m_bufPoolerCapacity + 1];
			this.m_largeBufPooler = new OraBuf[this.m_bufPoolerCapacity + 1];
			this.m_smallBufPoolerSync = new object();
			this.m_largeBufPoolerSync = new object();
			this.m_smallBufPoolerPos = -1;
			this.m_largeBufPoolerPos = -1;
			this.m_smallBufPoolerMin = 8;
			this.m_largeBufPoolerMin = 0;
			this.m_smallBufPoolerMax = 16;
			this.m_largeBufPoolerMax = 4;
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				StackTrace stackTrace = new StackTrace();
				string name = stackTrace.GetFrame(1).GetMethod().Name;
				string name2 = stackTrace.GetFrame(1).GetMethod().ReflectedType.Name;
				string text = string.Format("(COBP.CTOR) (poolid:{0}) (parentpoolid:{1}) ({2}.{3})", new object[]
				{
					this.m_poolId,
					this.m_obp.m_poolId,
					name2,
					name
				});
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.BUF, new string[]
				{
					text
				});
			}
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x0003EFF0 File Offset: 0x0003D1F0
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

		// Token: 0x060006CC RID: 1740 RVA: 0x0003F098 File Offset: 0x0003D298
		public OraBuf Get(int key, OracleCommunication oracleCommunication, bool bReceive)
		{
			OraBuf oraBuf = null;
			int count = 0;
			if (key == this.m_smallBufSize)
			{
				if (this.m_smallBufPoolerPos < 0)
				{
					goto IL_DB;
				}
				lock (this.m_smallBufPoolerSync)
				{
					if (this.m_smallBufPoolerPos >= 0)
					{
						oraBuf = this.m_smallBufPooler[this.m_smallBufPoolerPos];
						count = this.m_smallBufPoolerPos;
						this.m_smallBufPooler[this.m_smallBufPoolerPos--] = null;
					}
					goto IL_DB;
				}
			}
			if (key == this.m_largeBufSize && this.m_largeBufPoolerPos >= 0)
			{
				lock (this.m_largeBufPoolerSync)
				{
					if (this.m_largeBufPoolerPos >= 0)
					{
						oraBuf = this.m_largeBufPooler[this.m_largeBufPoolerPos];
						count = this.m_largeBufPoolerPos;
						this.m_largeBufPooler[this.m_largeBufPoolerPos--] = null;
					}
				}
			}
			IL_DB:
			if (oraBuf == null)
			{
				if (this.m_obp == null)
				{
					oraBuf = new OraBuf(oracleCommunication, key, bReceive);
				}
				else
				{
					oraBuf = this.m_obp.Get(key, oracleCommunication, bReceive);
				}
			}
			else
			{
				oraBuf.ReInit(bReceive);
			}
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				this.Output("COBP.GET", oraBuf.m_id, key, count);
			}
			return oraBuf;
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x0003F1E8 File Offset: 0x0003D3E8
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
						count = this.m_largeBufPoolerPos + 1;
					}
				}
			}
			IL_DD:
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				this.Output("COBP.PUT", oraBuf.m_id, oraBuf.m_size, count);
			}
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x0003F310 File Offset: 0x0003D510
		public void Init(OracleCommunication oc)
		{
			OraBuf oraBuf = null;
			int count = 0;
			int bufId = 0;
			for (int i = 0; i < 2; i++)
			{
				OraBuf[] array;
				object obj;
				int num;
				int num2;
				int num3;
				int num4;
				if (i == 0)
				{
					array = this.m_smallBufPooler;
					obj = this.m_smallBufPoolerSync;
					num = this.m_smallBufPoolerPos + 1;
					num2 = this.m_smallBufPoolerMin;
					num3 = this.m_smallBufPoolerMax;
					if (this.m_smallBufSize == 0)
					{
						this.m_smallBufSize = oc.m_sessionCtx.m_sessionDataUnit;
					}
					num4 = this.m_smallBufSize;
				}
				else
				{
					array = this.m_largeBufPooler;
					obj = this.m_largeBufPoolerSync;
					num = this.m_largeBufPoolerPos + 1;
					num2 = this.m_largeBufPoolerMin;
					num3 = this.m_largeBufPoolerMax;
					if (this.m_largeBufSize == 0)
					{
						this.m_largeBufSize = oc.m_sessionCtx.m_sessionDataUnit * 4;
					}
					num4 = this.m_largeBufSize;
				}
				if (num < num2)
				{
					int num5 = num2 - num;
					int j = 0;
					while (j < num5)
					{
						if (this.m_obp != null)
						{
							lock (obj)
							{
								oraBuf = this.m_obp.GetUninitialized(num4, oc);
								if (i == 0)
								{
									array[++this.m_smallBufPoolerPos] = oraBuf;
									count = this.m_smallBufPoolerPos + 1;
								}
								else
								{
									array[++this.m_largeBufPoolerPos] = oraBuf;
									count = this.m_largeBufPoolerPos + 1;
								}
								bufId = oraBuf.m_id;
								goto IL_1BD;
							}
							goto IL_146;
						}
						goto IL_146;
						IL_1BD:
						if (ProviderConfig.m_bTraceLevelPrivate)
						{
							this.Output("COBP.PUT", bufId, num4, count);
						}
						j++;
						continue;
						IL_146:
						lock (obj)
						{
							oraBuf = new OraBuf(oc, num4);
							if (i == 0)
							{
								array[++this.m_smallBufPoolerPos] = oraBuf;
								count = this.m_smallBufPoolerPos + 1;
							}
							else
							{
								array[++this.m_largeBufPoolerPos] = oraBuf;
								count = this.m_largeBufPoolerPos + 1;
							}
							bufId = oraBuf.m_id;
						}
						goto IL_1BD;
					}
				}
				else if (num > num3)
				{
					int num6 = num - num3;
					for (int k = 0; k < num6; k++)
					{
						int num7;
						if (i == 0)
						{
							num7 = this.m_smallBufPoolerPos;
						}
						else
						{
							num7 = this.m_largeBufPoolerPos;
						}
						if (num7 >= 0)
						{
							lock (obj)
							{
								if (i == 0)
								{
									num7 = this.m_smallBufPoolerPos;
								}
								else
								{
									num7 = this.m_largeBufPoolerPos;
								}
								if (num7 >= 0)
								{
									if (i == 0)
									{
										oraBuf = array[this.m_smallBufPoolerPos];
										count = this.m_smallBufPoolerPos;
										array[this.m_smallBufPoolerPos--] = null;
									}
									else
									{
										oraBuf = array[this.m_largeBufPoolerPos];
										count = this.m_largeBufPoolerPos;
										array[this.m_largeBufPoolerPos--] = null;
									}
									bufId = oraBuf.m_id;
								}
							}
						}
						if (ProviderConfig.m_bTraceLevelPrivate)
						{
							this.Output("COBP.GET", bufId, num4, count);
						}
						if (this.m_obp != null)
						{
							this.m_obp.Put(num4, oraBuf);
						}
					}
				}
			}
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x0003F648 File Offset: 0x0003D848
		public void ReturnAll()
		{
			int count = 0;
			int bufId = 0;
			GC.SuppressFinalize(this);
			if (this.m_smallBufPoolerPos >= 0)
			{
				lock (this.m_smallBufPoolerSync)
				{
					if (this.m_smallBufPoolerPos >= 0)
					{
						while (this.m_smallBufPoolerPos >= 0)
						{
							OraBuf oraBuf = this.m_smallBufPooler[this.m_smallBufPoolerPos];
							if (ProviderConfig.m_bTraceLevelPrivate)
							{
								this.Output("COBP.RETURNALL", bufId, this.m_smallBufSize, count);
							}
							this.m_obp.Put(this.m_smallBufSize, oraBuf);
							count = this.m_smallBufPoolerPos;
							this.m_smallBufPooler[this.m_smallBufPoolerPos] = null;
							bufId = oraBuf.m_id;
							this.m_smallBufPoolerPos--;
						}
					}
				}
			}
			if (this.m_largeBufPoolerPos >= 0)
			{
				lock (this.m_largeBufPoolerSync)
				{
					if (this.m_largeBufPoolerPos >= 0)
					{
						while (this.m_largeBufPoolerPos >= 0)
						{
							OraBuf oraBuf2 = this.m_largeBufPooler[this.m_largeBufPoolerPos];
							if (ProviderConfig.m_bTraceLevelPrivate)
							{
								this.Output("COBP.RETURNALL", bufId, this.m_largeBufSize, count);
							}
							this.m_obp.Put(this.m_largeBufSize, oraBuf2);
							count = this.m_largeBufPoolerPos;
							this.m_largeBufPooler[this.m_largeBufPoolerPos] = null;
							bufId = oraBuf2.m_id;
							this.m_largeBufPoolerPos--;
						}
					}
				}
			}
		}

		// Token: 0x0400091F RID: 2335
		private OraBufPool m_obp;

		// Token: 0x04000920 RID: 2336
		private static int m_sId = 0;

		// Token: 0x04000921 RID: 2337
		private static object m_idSync = new object();

		// Token: 0x04000922 RID: 2338
		internal int m_poolId;

		// Token: 0x04000923 RID: 2339
		private OraBuf[] m_smallBufPooler;

		// Token: 0x04000924 RID: 2340
		private OraBuf[] m_largeBufPooler;

		// Token: 0x04000925 RID: 2341
		private object m_smallBufPoolerSync;

		// Token: 0x04000926 RID: 2342
		private object m_largeBufPoolerSync;

		// Token: 0x04000927 RID: 2343
		private int m_smallBufPoolerPos;

		// Token: 0x04000928 RID: 2344
		private int m_largeBufPoolerPos;

		// Token: 0x04000929 RID: 2345
		private int m_smallBufPoolerMin;

		// Token: 0x0400092A RID: 2346
		private int m_smallBufPoolerMax;

		// Token: 0x0400092B RID: 2347
		private int m_largeBufPoolerMin;

		// Token: 0x0400092C RID: 2348
		private int m_largeBufPoolerMax;

		// Token: 0x0400092D RID: 2349
		internal int m_smallBufSize;

		// Token: 0x0400092E RID: 2350
		internal int m_largeBufSize;

		// Token: 0x0400092F RID: 2351
		private int m_bufPoolerCapacity;
	}
}
