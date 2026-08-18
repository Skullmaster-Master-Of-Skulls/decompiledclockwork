using System;
using OracleInternal.Common;

namespace Oracle.ManagedDataAccess.Client
{
	// Token: 0x0200006C RID: 108
	public sealed class OracleHAEventArgs : EventArgs
	{
		// Token: 0x0600056F RID: 1391 RVA: 0x0003122C File Offset: 0x0002F42C
		internal OracleHAEventArgs(OracleHAEventSource source, OracleHAEventStatus status, string service, string database, string databaseDomain, string instance, string host, string reason, DateTime time, int drain_timeout)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				this.m_source = source;
				this.m_status = status;
				if (service != null && service.Length > 0)
				{
					this.m_service = service;
				}
				if (database != null && database.Length > 0)
				{
					this.m_database = database;
				}
				if (databaseDomain != null && databaseDomain.Length > 0)
				{
					this.m_databaseDomain = databaseDomain;
				}
				if (instance != null && instance.Length > 0)
				{
					this.m_instance = instance;
				}
				this.m_host = host;
				this.m_reason = reason;
				this.m_time = time;
				this.m_drain_timeout = drain_timeout;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000570 RID: 1392 RVA: 0x0003130C File Offset: 0x0002F50C
		public OracleHAEventSource Source
		{
			get
			{
				return this.m_source;
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000571 RID: 1393 RVA: 0x00031314 File Offset: 0x0002F514
		public OracleHAEventStatus Status
		{
			get
			{
				return this.m_status;
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000572 RID: 1394 RVA: 0x0003131C File Offset: 0x0002F51C
		public string ServiceName
		{
			get
			{
				return this.m_service;
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000573 RID: 1395 RVA: 0x00031324 File Offset: 0x0002F524
		public string DatabaseName
		{
			get
			{
				return this.m_database;
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000574 RID: 1396 RVA: 0x0003132C File Offset: 0x0002F52C
		public string DatabaseDomainName
		{
			get
			{
				return this.m_databaseDomain;
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000575 RID: 1397 RVA: 0x00031334 File Offset: 0x0002F534
		public string HostName
		{
			get
			{
				return this.m_host;
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000576 RID: 1398 RVA: 0x0003133C File Offset: 0x0002F53C
		public string InstanceName
		{
			get
			{
				return this.m_instance;
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000577 RID: 1399 RVA: 0x00031344 File Offset: 0x0002F544
		public string Reason
		{
			get
			{
				return this.m_reason;
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000578 RID: 1400 RVA: 0x0003134C File Offset: 0x0002F54C
		public DateTime Time
		{
			get
			{
				return this.m_time;
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000579 RID: 1401 RVA: 0x00031354 File Offset: 0x0002F554
		public int DrainTimeout
		{
			get
			{
				return this.m_drain_timeout;
			}
		}

		// Token: 0x04000654 RID: 1620
		private OracleHAEventSource m_source;

		// Token: 0x04000655 RID: 1621
		private OracleHAEventStatus m_status;

		// Token: 0x04000656 RID: 1622
		private string m_service;

		// Token: 0x04000657 RID: 1623
		private string m_database;

		// Token: 0x04000658 RID: 1624
		private string m_databaseDomain;

		// Token: 0x04000659 RID: 1625
		private string m_host;

		// Token: 0x0400065A RID: 1626
		private string m_instance;

		// Token: 0x0400065B RID: 1627
		private string m_reason;

		// Token: 0x0400065C RID: 1628
		private DateTime m_time;

		// Token: 0x0400065D RID: 1629
		private int m_drain_timeout;

		// Token: 0x0400065E RID: 1630
		internal bool m_bFireHADotNetEvent = true;
	}
}
