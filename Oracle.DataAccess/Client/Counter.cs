using System;
using System.Threading;

namespace Oracle.DataAccess.Client
{
	// Token: 0x02000133 RID: 307
	internal class Counter
	{
		// Token: 0x06000C4B RID: 3147 RVA: 0x00079320 File Offset: 0x00078320
		public Counter(bool bOwnedByCPCtx)
		{
			this.bOwnedByCPCtx = bOwnedByCPCtx;
		}

		// Token: 0x06000C4C RID: 3148 RVA: 0x00079330 File Offset: 0x00078330
		public void UpdateTotalCount(ConnectionPool conPool, int val, bool bForPotential)
		{
			lock (this)
			{
				this.total += val;
				if (bForPotential)
				{
					this.UpdatePotentialTotalCount(val);
				}
				if (OraTrace.m_TraceLevel != 0U && !this.bOwnedByCPCtx)
				{
					if (conPool.m_cpCtx != null)
					{
						OraTrace.Trace(2U, new string[]
						{
							string.Concat(new object[]
							{
								" (POOL)  Num of cons in (CP id: ",
								conPool.m_cpCtx.GetHashCode(),
								", Inst CP id: ",
								conPool.GetHashCode(),
								") : (",
								conPool.m_cpCtx.m_counter.total,
								", ",
								this.total,
								")\n"
							})
						});
					}
					else
					{
						OraTrace.Trace(2U, new string[]
						{
							string.Concat(new object[]
							{
								" (POOL)  Total number of connections for pool (id: ",
								conPool.m_clonedCtx.conString.GetHashCode(),
								") : ",
								this.total.ToString(),
								"\n"
							})
						});
					}
				}
			}
		}

		// Token: 0x06000C4D RID: 3149 RVA: 0x000794A0 File Offset: 0x000784A0
		public void UpdatePotentialTotalCount(int val)
		{
			lock (this)
			{
				this.potentialTotal += val;
			}
		}

		// Token: 0x06000C4E RID: 3150 RVA: 0x000794E4 File Offset: 0x000784E4
		public void UpdateThreadWaitCount(ConnectionPool conPool, int val)
		{
			bool flag = false;
			int num = 0;
			lock (this)
			{
				this.threadWait += val;
				if (!conPool.m_bGridRac)
				{
					if (val > 0 && this.potentialTotal < conPool.m_clonedCtx.maxPoolSize && this.potentialTotal <= this.total + this.threadWait && this.totalAvailable <= 0)
					{
						int num2 = 0;
						if (conPool.m_clonedCtx.minPoolSize > conPool.m_counter.potentialTotal)
						{
							num2 = conPool.m_clonedCtx.minPoolSize - conPool.m_counter.potentialTotal;
						}
						if (this.potentialTotal + conPool.m_clonedCtx.poolIncSize > conPool.m_clonedCtx.maxPoolSize)
						{
							num = conPool.m_clonedCtx.maxPoolSize - this.potentialTotal;
						}
						else
						{
							num = conPool.m_clonedCtx.poolIncSize;
						}
						if (num2 > num)
						{
							num = num2;
						}
						this.UpdatePotentialTotalCount(num);
						if (num > 0)
						{
							flag = true;
						}
					}
					if (val > 0)
					{
						Interlocked.Decrement(ref this.totalAvailable);
					}
				}
				else if (conPool.m_cpCtx != null)
				{
					if (val > 0 && conPool.m_cpCtx.m_counter.potentialTotal < conPool.m_clonedCtx.maxPoolSize && conPool.m_cpCtx.m_counter.potentialTotal <= conPool.m_cpCtx.m_counter.total + conPool.m_cpCtx.m_counter.threadWait && conPool.m_cpCtx.totalAvaliableConnections <= 0)
					{
						int num3 = 0;
						if (conPool.m_clonedCtx.minPoolSize > conPool.m_cpCtx.m_counter.potentialTotal)
						{
							num3 = conPool.m_clonedCtx.minPoolSize - conPool.m_cpCtx.m_counter.potentialTotal;
						}
						if (conPool.m_cpCtx.m_counter.potentialTotal + conPool.m_clonedCtx.poolIncSize > conPool.m_clonedCtx.maxPoolSize)
						{
							num = conPool.m_clonedCtx.maxPoolSize - conPool.m_cpCtx.m_counter.potentialTotal;
						}
						else
						{
							num = conPool.m_clonedCtx.poolIncSize;
						}
						if (num3 > num)
						{
							num = num3;
						}
						conPool.UpdatePotentialTotalCount(num);
						if (num > 0)
						{
							flag = true;
						}
					}
					if (val > 0 && this.bOwnedByCPCtx)
					{
						Interlocked.Decrement(ref conPool.m_cpCtx.totalAvaliableConnections);
						Interlocked.Decrement(ref conPool.m_counter.totalAvailable);
					}
				}
			}
			if (flag)
			{
				ThreadPool.QueueUserWorkItem(new WaitCallback(conPool.PopulatePool), num);
			}
		}

		// Token: 0x040009AA RID: 2474
		public int total;

		// Token: 0x040009AB RID: 2475
		public int potentialTotal;

		// Token: 0x040009AC RID: 2476
		public int threadWait;

		// Token: 0x040009AD RID: 2477
		public int totalAvailable;

		// Token: 0x040009AE RID: 2478
		public bool bOwnedByCPCtx;
	}
}
