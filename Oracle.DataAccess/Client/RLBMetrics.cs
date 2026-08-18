using System;

namespace Oracle.DataAccess.Client
{
	// Token: 0x02000139 RID: 313
	internal class RLBMetrics
	{
		// Token: 0x06000C93 RID: 3219 RVA: 0x00082D84 File Offset: 0x00081D84
		public RLBMetrics(string instanceName, double precentage, int frequency, RLBMetricsFlag flag)
		{
			this.m_InstanceName = instanceName;
			this.m_Percentage = precentage;
			this.m_MaxDistribFreq = frequency;
			this.m_CurDistribFreq = frequency;
			this.m_Flag = flag;
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000C94 RID: 3220 RVA: 0x00082DBD File Offset: 0x00081DBD
		public string InstanceName
		{
			get
			{
				return this.m_InstanceName;
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000C95 RID: 3221 RVA: 0x00082DC5 File Offset: 0x00081DC5
		public RLBMetricsFlag Flag
		{
			get
			{
				return this.m_Flag;
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000C96 RID: 3222 RVA: 0x00082DCD File Offset: 0x00081DCD
		// (set) Token: 0x06000C97 RID: 3223 RVA: 0x00082DD5 File Offset: 0x00081DD5
		public int CurDistribFreq
		{
			get
			{
				return this.m_CurDistribFreq;
			}
			set
			{
				this.m_CurDistribFreq = value;
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000C98 RID: 3224 RVA: 0x00082DDE File Offset: 0x00081DDE
		// (set) Token: 0x06000C99 RID: 3225 RVA: 0x00082DE6 File Offset: 0x00081DE6
		public int MaxDistribFreq
		{
			get
			{
				return this.m_MaxDistribFreq;
			}
			set
			{
				this.m_MaxDistribFreq = value;
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000C9A RID: 3226 RVA: 0x00082DEF File Offset: 0x00081DEF
		// (set) Token: 0x06000C9B RID: 3227 RVA: 0x00082DF7 File Offset: 0x00081DF7
		public int StdDevViolation
		{
			get
			{
				return this.m_StdDevViolation;
			}
			set
			{
				this.m_StdDevViolation = value;
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000C9C RID: 3228 RVA: 0x00082E00 File Offset: 0x00081E00
		// (set) Token: 0x06000C9D RID: 3229 RVA: 0x00082E08 File Offset: 0x00081E08
		public double Percentage
		{
			get
			{
				return this.m_Percentage;
			}
			set
			{
				this.m_Percentage = value;
			}
		}

		// Token: 0x040009E4 RID: 2532
		private string m_InstanceName;

		// Token: 0x040009E5 RID: 2533
		private int m_CurDistribFreq;

		// Token: 0x040009E6 RID: 2534
		private int m_MaxDistribFreq;

		// Token: 0x040009E7 RID: 2535
		private int m_StdDevViolation;

		// Token: 0x040009E8 RID: 2536
		private double m_Percentage;

		// Token: 0x040009E9 RID: 2537
		private RLBMetricsFlag m_Flag;
	}
}
