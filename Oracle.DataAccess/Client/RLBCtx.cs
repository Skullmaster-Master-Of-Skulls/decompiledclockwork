using System;
using System.Collections;
using System.Text;

namespace Oracle.DataAccess.Client
{
	// Token: 0x02000138 RID: 312
	internal class RLBCtx
	{
		// Token: 0x06000C87 RID: 3207 RVA: 0x00082A48 File Offset: 0x00081A48
		public RLBCtx(string serviceName)
		{
			this.m_htConToInst = Hashtable.Synchronized(new Hashtable());
			this.m_ServiceName = serviceName;
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000C88 RID: 3208 RVA: 0x00082A67 File Offset: 0x00081A67
		// (set) Token: 0x06000C89 RID: 3209 RVA: 0x00082A6F File Offset: 0x00081A6F
		public string ServiceName
		{
			get
			{
				return this.m_ServiceName;
			}
			set
			{
				this.m_ServiceName = value;
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000C8A RID: 3210 RVA: 0x00082A78 File Offset: 0x00081A78
		// (set) Token: 0x06000C8B RID: 3211 RVA: 0x00082A80 File Offset: 0x00081A80
		public Hashtable htConToInst
		{
			get
			{
				return this.m_htConToInst;
			}
			set
			{
				this.m_htConToInst = value;
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000C8C RID: 3212 RVA: 0x00082A89 File Offset: 0x00081A89
		// (set) Token: 0x06000C8D RID: 3213 RVA: 0x00082A91 File Offset: 0x00081A91
		public ArrayList RLBMetricsList
		{
			get
			{
				return this.m_RLBMetricsList;
			}
			set
			{
				this.m_RLBMetricsList = value;
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000C8E RID: 3214 RVA: 0x00082A9A File Offset: 0x00081A9A
		// (set) Token: 0x06000C8F RID: 3215 RVA: 0x00082AA2 File Offset: 0x00081AA2
		public string timeStamp
		{
			get
			{
				return this.m_timeStamp;
			}
			set
			{
				this.m_timeStamp = value;
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000C90 RID: 3216 RVA: 0x00082AAB File Offset: 0x00081AAB
		// (set) Token: 0x06000C91 RID: 3217 RVA: 0x00082AB3 File Offset: 0x00081AB3
		public bool bNeedNormalization
		{
			get
			{
				return this.m_bNeedNormalization;
			}
			set
			{
				this.m_bNeedNormalization = value;
			}
		}

		// Token: 0x06000C92 RID: 3218 RVA: 0x00082ABC File Offset: 0x00081ABC
		public void NormalizeCounters(RLBCtx rlbCtx, CPCtx cpCtx)
		{
			if (cpCtx != null)
			{
				for (int i = 0; i < rlbCtx.RLBMetricsList.Count; i++)
				{
					ConnectionPool connectionPool = (ConnectionPool)cpCtx.htInstToCp[((RLBMetrics)rlbCtx.RLBMetricsList[i]).InstanceName];
					if (connectionPool != null)
					{
						int num = 1;
						if (connectionPool.m_counter.total > connectionPool.m_connections.Count)
						{
							num = connectionPool.m_counter.total - connectionPool.m_connections.Count;
						}
						if (num < 1)
						{
							num = 1;
						}
						if (num * ((RLBMetrics)rlbCtx.RLBMetricsList[i]).MaxDistribFreq < 0 || num * ((RLBMetrics)rlbCtx.RLBMetricsList[i]).MaxDistribFreq >= 1073741822)
						{
							((RLBMetrics)rlbCtx.RLBMetricsList[i]).CurDistribFreq = 1073741822;
						}
						else
						{
							((RLBMetrics)rlbCtx.RLBMetricsList[i]).CurDistribFreq = num * ((RLBMetrics)rlbCtx.RLBMetricsList[i]).MaxDistribFreq;
						}
					}
				}
				rlbCtx.bNeedNormalization = false;
				if ((OraTrace.m_TraceLevel & 32U) == 32U)
				{
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.Append(" (GRID) (RLB) (NORMALIZE) ");
					for (int j = 0; j < rlbCtx.RLBMetricsList.Count; j++)
					{
						stringBuilder.Append("(");
						stringBuilder.Append(((RLBMetrics)rlbCtx.RLBMetricsList[j]).InstanceName);
						ConnectionPool connectionPool2 = (ConnectionPool)cpCtx.htInstToCp[((RLBMetrics)rlbCtx.RLBMetricsList[j]).InstanceName];
						if (connectionPool2 != null)
						{
							stringBuilder.Append(": used=");
							stringBuilder.Append(connectionPool2.m_counter.total - connectionPool2.m_connections.Count);
							stringBuilder.Append("; idle=");
							stringBuilder.Append(connectionPool2.m_connections.Count);
							stringBuilder.Append("; tot=");
							stringBuilder.Append(connectionPool2.m_counter.total);
						}
						else
						{
							stringBuilder.Append(": N/A");
						}
						stringBuilder.Append("; counter=");
						stringBuilder.Append(((RLBMetrics)rlbCtx.RLBMetricsList[j]).CurDistribFreq);
						stringBuilder.Append("/");
						stringBuilder.Append(((RLBMetrics)rlbCtx.RLBMetricsList[j]).MaxDistribFreq);
						stringBuilder.Append(") ");
					}
					stringBuilder.Append(")\n");
					OraTrace.Trace(32U, new string[]
					{
						stringBuilder.ToString()
					});
				}
			}
		}

		// Token: 0x040009DF RID: 2527
		private string m_ServiceName;

		// Token: 0x040009E0 RID: 2528
		private Hashtable m_htConToInst;

		// Token: 0x040009E1 RID: 2529
		private ArrayList m_RLBMetricsList;

		// Token: 0x040009E2 RID: 2530
		private string m_timeStamp;

		// Token: 0x040009E3 RID: 2531
		private bool m_bNeedNormalization;
	}
}
