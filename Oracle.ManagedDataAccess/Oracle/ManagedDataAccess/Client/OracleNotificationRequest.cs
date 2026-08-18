using System;
using System.Collections;
using OracleInternal.Common;
using OracleInternal.ServiceObjects;

namespace Oracle.ManagedDataAccess.Client
{
	// Token: 0x0200006F RID: 111
	public class OracleNotificationRequest
	{
		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000587 RID: 1415 RVA: 0x000315EC File Offset: 0x0002F7EC
		internal long Id
		{
			get
			{
				return this.m_id;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000588 RID: 1416 RVA: 0x000315F4 File Offset: 0x0002F7F4
		// (set) Token: 0x06000589 RID: 1417 RVA: 0x000315FC File Offset: 0x0002F7FC
		public bool IsNotifiedOnce
		{
			get
			{
				return this.m_bIsNotifiedOnce;
			}
			set
			{
				this.m_bIsNotifiedOnce = value;
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x0600058A RID: 1418 RVA: 0x00031608 File Offset: 0x0002F808
		// (set) Token: 0x0600058B RID: 1419 RVA: 0x00031610 File Offset: 0x0002F810
		public bool IsPersistent
		{
			get
			{
				return this.m_bIsPersistent;
			}
			set
			{
				this.m_bIsPersistent = value;
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x0600058C RID: 1420 RVA: 0x0003161C File Offset: 0x0002F81C
		// (set) Token: 0x0600058D RID: 1421 RVA: 0x00031624 File Offset: 0x0002F824
		public long Timeout
		{
			get
			{
				return this.m_timeout;
			}
			set
			{
				if (value < 0L || value > (long)((ulong)-1))
				{
					throw new ArgumentOutOfRangeException("Timeout");
				}
				this.m_timeout = value;
			}
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x00031644 File Offset: 0x0002F844
		internal OracleNotificationRequest(string service, OracleDependencyImpl orclDependencyImpl)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				this.m_service = service;
				this.m_id = orclDependencyImpl.m_clientRegistrationId;
				this.m_bIsNotifiedOnce = orclDependencyImpl.m_bIsNotifiedOnce;
				this.m_bIsPersistent = orclDependencyImpl.m_bIsPersistent;
				this.m_timeout = orclDependencyImpl.m_timeout;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x04000664 RID: 1636
		internal static Hashtable s_idTable = Hashtable.Synchronized(new Hashtable());

		// Token: 0x04000665 RID: 1637
		internal static bool s_bDefIsNotifiedOnce = true;

		// Token: 0x04000666 RID: 1638
		internal static int s_DefRegTimeout = 50000;

		// Token: 0x04000667 RID: 1639
		internal static bool s_bDefIsPersistent = false;

		// Token: 0x04000668 RID: 1640
		internal static string s_ChangedNotificationName = "OracleDatabaseChangedNotificationService";

		// Token: 0x04000669 RID: 1641
		internal long m_id;

		// Token: 0x0400066A RID: 1642
		internal string m_service;

		// Token: 0x0400066B RID: 1643
		internal bool m_bIsNotifiedOnce;

		// Token: 0x0400066C RID: 1644
		internal bool m_bIsPersistent;

		// Token: 0x0400066D RID: 1645
		internal long m_timeout;
	}
}
