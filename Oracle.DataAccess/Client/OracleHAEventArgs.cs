using System;

namespace Oracle.DataAccess.Client
{
	// Token: 0x02000131 RID: 305
	public sealed class OracleHAEventArgs : EventArgs
	{
		// Token: 0x06000C2C RID: 3116 RVA: 0x00079021 File Offset: 0x00078021
		static OracleHAEventArgs()
		{
			if (!OracleInit.bSetDllDirectoryInvoked)
			{
				OracleInit.Initialize();
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000C2D RID: 3117 RVA: 0x0007902F File Offset: 0x0007802F
		public OracleHAEventSource Source
		{
			get
			{
				return this.m_source;
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000C2E RID: 3118 RVA: 0x00079037 File Offset: 0x00078037
		public OracleHAEventStatus Status
		{
			get
			{
				return this.m_status;
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000C2F RID: 3119 RVA: 0x0007903F File Offset: 0x0007803F
		public string ServiceName
		{
			get
			{
				return this.m_service;
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000C30 RID: 3120 RVA: 0x00079047 File Offset: 0x00078047
		public string DatabaseName
		{
			get
			{
				return this.m_database;
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000C31 RID: 3121 RVA: 0x0007904F File Offset: 0x0007804F
		public string DatabaseDomainName
		{
			get
			{
				return this.m_databaseDomain;
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000C32 RID: 3122 RVA: 0x00079057 File Offset: 0x00078057
		public string HostName
		{
			get
			{
				return this.m_host;
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000C33 RID: 3123 RVA: 0x0007905F File Offset: 0x0007805F
		public string InstanceName
		{
			get
			{
				return this.m_instance;
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000C34 RID: 3124 RVA: 0x00079067 File Offset: 0x00078067
		public DateTime Time
		{
			get
			{
				return this.m_time;
			}
		}

		// Token: 0x06000C35 RID: 3125 RVA: 0x00079070 File Offset: 0x00078070
		internal OracleHAEventArgs(OpoHACtx opoHACtx)
		{
			this.m_source = opoHACtx.source;
			this.m_status = opoHACtx.status;
			this.m_service = opoHACtx.serviceName;
			this.m_database = opoHACtx.dbName;
			this.m_databaseDomain = opoHACtx.dbDomainName;
			this.m_instance = opoHACtx.instName;
			this.m_host = opoHACtx.hostName;
			this.m_time = new DateTime((int)opoHACtx.year, (int)opoHACtx.month, (int)opoHACtx.day, (int)opoHACtx.hour, (int)opoHACtx.min, (int)opoHACtx.sec, (int)(opoHACtx.fsec / 1000000U));
		}

		// Token: 0x04000999 RID: 2457
		private OracleHAEventSource m_source;

		// Token: 0x0400099A RID: 2458
		private OracleHAEventStatus m_status;

		// Token: 0x0400099B RID: 2459
		private string m_service;

		// Token: 0x0400099C RID: 2460
		private string m_database;

		// Token: 0x0400099D RID: 2461
		private string m_databaseDomain;

		// Token: 0x0400099E RID: 2462
		private string m_host;

		// Token: 0x0400099F RID: 2463
		private string m_instance;

		// Token: 0x040009A0 RID: 2464
		private DateTime m_time;
	}
}
