using System;
using System.Collections;

namespace Oracle.DataAccess.Client
{
	// Token: 0x02000026 RID: 38
	public class OracleNotificationRequest
	{
		// Token: 0x06000182 RID: 386 RVA: 0x000146DA File Offset: 0x000136DA
		static OracleNotificationRequest()
		{
			if (!OracleInit.bSetDllDirectoryInvoked)
			{
				OracleInit.Initialize();
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000183 RID: 387 RVA: 0x00014717 File Offset: 0x00013717
		internal string Id
		{
			get
			{
				return this.m_id;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000184 RID: 388 RVA: 0x0001471F File Offset: 0x0001371F
		// (set) Token: 0x06000185 RID: 389 RVA: 0x00014727 File Offset: 0x00013727
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

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000186 RID: 390 RVA: 0x00014730 File Offset: 0x00013730
		// (set) Token: 0x06000187 RID: 391 RVA: 0x00014738 File Offset: 0x00013738
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

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000188 RID: 392 RVA: 0x00014741 File Offset: 0x00013741
		private string Service
		{
			get
			{
				return this.m_service;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000189 RID: 393 RVA: 0x00014749 File Offset: 0x00013749
		// (set) Token: 0x0600018A RID: 394 RVA: 0x00014751 File Offset: 0x00013751
		public bool GroupingNotificationEnabled
		{
			get
			{
				return this.m_bGroupingNotificationEnabled;
			}
			set
			{
				this.m_bGroupingNotificationEnabled = value;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600018B RID: 395 RVA: 0x0001475A File Offset: 0x0001375A
		// (set) Token: 0x0600018C RID: 396 RVA: 0x00014762 File Offset: 0x00013762
		public OracleAQNotificationGroupingType GroupingType
		{
			get
			{
				return this.m_groupingType;
			}
			set
			{
				this.m_groupingType = value;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600018D RID: 397 RVA: 0x0001476B File Offset: 0x0001376B
		// (set) Token: 0x0600018E RID: 398 RVA: 0x00014773 File Offset: 0x00013773
		public int GroupingInterval
		{
			get
			{
				return this.m_groupingInterval;
			}
			set
			{
				this.m_groupingInterval = value;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600018F RID: 399 RVA: 0x0001477C File Offset: 0x0001377C
		// (set) Token: 0x06000190 RID: 400 RVA: 0x00014784 File Offset: 0x00013784
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

		// Token: 0x06000191 RID: 401 RVA: 0x000147A2 File Offset: 0x000137A2
		internal OracleNotificationRequest(string service, string id, bool isNotifiedOnce, long timeout, bool isPersistent, OpoSubscrCtx opoSubscrCtx)
		{
			this.m_service = service;
			this.m_id = id;
			this.m_bIsNotifiedOnce = isNotifiedOnce;
			this.m_bIsPersistent = isPersistent;
			this.m_timeout = timeout;
			this.m_opoSubscrCtx = opoSubscrCtx;
		}

		// Token: 0x06000192 RID: 402 RVA: 0x000147D7 File Offset: 0x000137D7
		internal OracleNotificationRequest(bool isNotifiedOnce, long timeout, bool isPersistent, bool groupingNotificationEnabled, OracleAQNotificationGroupingType groupingType, int groupingInterval)
		{
			this.m_bIsNotifiedOnce = isNotifiedOnce;
			this.m_timeout = timeout;
			this.m_bIsPersistent = isPersistent;
			this.m_bGroupingNotificationEnabled = groupingNotificationEnabled;
			this.m_groupingType = groupingType;
			this.m_groupingInterval = groupingInterval;
		}

		// Token: 0x06000193 RID: 403 RVA: 0x0001480C File Offset: 0x0001380C
		internal static IntPtr PopulateChgNTFNSubscrCtx(OracleCommand cmd, bool isRowidReq, out OracleDependency dep)
		{
			IntPtr result = IntPtr.Zero;
			OracleConnection connection = cmd.Connection;
			OracleNotificationRequest notification = cmd.Notification;
			int num = 0;
			dep = null;
			if (cmd.m_NTFNAutoEnlist && notification != null)
			{
				if (!connection.IsDBVer10gR2OrHigher)
				{
					throw new OracleException(ErrRes.NTFN_CHGNTFN_DBVERSION, connection.DataSource, string.Empty, OpoErrResManager.GetErrorMesg(ErrRes.NTFN_CHGNTFN_DBVERSION, new string[0]));
				}
				OracleDependency oracleDependencyFromNTFNId;
				dep = (oracleDependencyFromNTFNId = OracleDependency.GetOracleDependencyFromNTFNId(notification.m_id));
				if (oracleDependencyFromNTFNId == null)
				{
					throw new OracleException(ErrRes.NTFN_DEP_NOTEXIST, connection.DataSource, string.Empty, OpoErrResManager.GetErrorMesg(ErrRes.NTFN_DEP_NOTEXIST, new string[0]));
				}
				result = notification.m_opoSubscrCtx.opsSubscrCtx;
				if (!dep.m_bIsRegistered)
				{
					if (dep.m_OracleRowidInfo == OracleRowidInfo.Include)
					{
						isRowidReq = true;
					}
					else if (dep.m_OracleRowidInfo == OracleRowidInfo.Exclude)
					{
						isRowidReq = false;
					}
					try
					{
						try
						{
							num = OpsSubscr.SetChgNTFN(OracleDependency.s_opsEnvCtx, notification.m_opoSubscrCtx.opsSubscrCtx, notification.m_opoSubscrCtx.opsErrCtx, notification.m_id, notification.m_bIsPersistent ? 1 : 0, notification.m_bIsNotifiedOnce ? 1 : 0, isRowidReq ? 1 : 0, (uint)notification.m_timeout);
						}
						catch (Exception ex)
						{
							if (OraTrace.m_TraceLevel != 0U)
							{
								OraTrace.TraceExceptionInfo(ex);
							}
							throw;
						}
						return result;
					}
					finally
					{
						if (num != 0)
						{
							throw new OracleException(num, connection.DataSource, string.Empty, OpoErrResManager.GetErrorMesg(num, new string[0]));
						}
					}
				}
				if (dep.m_dataSource != connection.DataSource || dep.m_userName != connection.m_opoConCtx.opoConRefCtx.userID)
				{
					throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_DIFFERENT_CONNECTIONS, new string[0]));
				}
			}
			return result;
		}

		// Token: 0x04000101 RID: 257
		internal static Hashtable s_idTable = Hashtable.Synchronized(new Hashtable());

		// Token: 0x04000102 RID: 258
		internal static bool s_bDefIsNotifiedOnce = true;

		// Token: 0x04000103 RID: 259
		internal static int s_DefRegTimeout = 50000;

		// Token: 0x04000104 RID: 260
		internal static bool s_bDefIsPersistent = false;

		// Token: 0x04000105 RID: 261
		internal static string s_ChangedNotificationName = "OracleDatabaseChangedNotificationService";

		// Token: 0x04000106 RID: 262
		internal OpoSubscrCtx m_opoSubscrCtx;

		// Token: 0x04000107 RID: 263
		internal string m_id;

		// Token: 0x04000108 RID: 264
		internal string m_service;

		// Token: 0x04000109 RID: 265
		internal bool m_bIsNotifiedOnce;

		// Token: 0x0400010A RID: 266
		internal bool m_bIsPersistent;

		// Token: 0x0400010B RID: 267
		internal long m_timeout;

		// Token: 0x0400010C RID: 268
		internal bool m_bGroupingNotificationEnabled;

		// Token: 0x0400010D RID: 269
		internal OracleAQNotificationGroupingType m_groupingType;

		// Token: 0x0400010E RID: 270
		internal int m_groupingInterval;
	}
}
