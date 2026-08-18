using System;
using System.Runtime.InteropServices;
using System.Threading;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using OracleInternal.Common;
using OracleInternal.MTS;

namespace OracleInternal.ConnectionPool
{
	// Token: 0x020000CB RID: 203
	[StructLayout(LayoutKind.Sequential)]
	internal class PoolResource<PM, CP, PR> : IOraclePoolResource where PM : PoolManager<PM, CP, PR>, new() where CP : Pool<PM, CP, PR>, new() where PR : PoolResource<PM, CP, PR>, new()
	{
		// Token: 0x060007EA RID: 2026 RVA: 0x00054768 File Offset: 0x00052968
		public PoolResource()
		{
			this.m_id = this.GetHashCode().ToString();
			this.m_criteriaIds = new uint[3];
			this.m_criteriaIds[(int)((UIntPtr)0)] = 0U;
			this.m_criteriaIds[(int)((UIntPtr)1)] = 0U;
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x000547D4 File Offset: 0x000529D4
		public virtual void Connect(ConnectionString cs, bool bForPoolPopulation, CriteriaCtx criteriaCtx, string instanceName = null)
		{
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x000547D8 File Offset: 0x000529D8
		public virtual void AttachServerProcess(long sessionFlags, bool bUseDRCPMultiTag, ref long s2cSessionFlags)
		{
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x000547DC File Offset: 0x000529DC
		public virtual void DetachServerProcess(string drcpTagName, bool bUseDRCPMultiTag)
		{
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x000547E0 File Offset: 0x000529E0
		public virtual void DisConnect(CriteriaCtx criteriaCtx)
		{
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x000547E4 File Offset: 0x000529E4
		public virtual bool Dump()
		{
			return true;
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x000547E8 File Offset: 0x000529E8
		internal virtual bool PingServer()
		{
			return true;
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x000547EC File Offset: 0x000529EC
		internal virtual bool IsTAFEnabled()
		{
			return false;
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x000547F0 File Offset: 0x000529F0
		internal virtual bool TransportAlive()
		{
			return true;
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x000547F4 File Offset: 0x000529F4
		internal virtual void GetAttributes()
		{
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x000547F8 File Offset: 0x000529F8
		internal virtual void UpdateAttributes()
		{
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x000547FC File Offset: 0x000529FC
		internal virtual void GetConStrDefaults()
		{
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x00054800 File Offset: 0x00052A00
		internal virtual string GetDefaultEditionName()
		{
			return null;
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x00054804 File Offset: 0x00052A04
		internal virtual bool[] ProcessCriteriaCtx(CriteriaCtx ctx)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			bool[] result;
			try
			{
				if (!this.m_isDb12cR1OrHigher && !string.IsNullOrEmpty(ctx.m_pdbName))
				{
					throw new OracleException(-7500, string.Empty, string.Empty, OracleStringResourceManager.GetErrorMesg(-7500, new string[]
					{
						"12c",
						"Multitenant"
					}));
				}
				if (!this.m_isDb11gR1OrHigher && !string.IsNullOrEmpty(ctx.m_edition))
				{
					throw new OracleException(-7500, string.Empty, string.Empty, OracleStringResourceManager.GetErrorMesg(-7500, new string[]
					{
						"11g",
						"Edition Based Redefinition"
					}));
				}
				bool[] array = null;
				if (ctx != null && !ctx.m_bPrelimAuthSession)
				{
					bool flag = false;
					if (string.IsNullOrEmpty(ctx.m_pdbName) && this.m_pm != null)
					{
						if (string.IsNullOrEmpty(this.m_pm.m_conStrPdbName))
						{
							this.GetConStrDefaults();
						}
						ctx.m_pdbName = this.m_pm.m_conStrPdbName;
						ctx.m_serviceName = this.m_pm.m_conStrServiceName;
					}
					if (string.IsNullOrEmpty(ctx.m_pdbName))
					{
						flag = true;
					}
					if (ctx.m_pdbName != null && this.PdbName != null && ctx.m_pdbName.Equals(this.PdbName, StringComparison.InvariantCultureIgnoreCase) && (string.IsNullOrEmpty(ctx.m_serviceName) || ctx.m_serviceName.Equals(this.ServiceName, StringComparison.InvariantCultureIgnoreCase)))
					{
						flag = true;
					}
					bool flag2;
					if (!this.m_isDb11gR1OrHigher)
					{
						flag2 = true;
					}
					else
					{
						string text = this.EditionName;
						string text2 = null;
						if (!string.IsNullOrEmpty(ctx.m_edition))
						{
							if (ctx.m_edition[0] == '"')
							{
								int num = ctx.m_edition.IndexOf('"', 1);
								text2 = ctx.m_edition.Substring(1, num - 1);
							}
							else
							{
								text2 = ctx.m_edition.ToUpper();
							}
						}
						if (text2 == text)
						{
							flag2 = true;
						}
						else
						{
							if (text == null)
							{
								text = this.GetDefaultEditionName();
							}
							else if (text2 == null)
							{
								text2 = this.GetDefaultEditionName();
							}
							flag2 = (text == text2);
						}
					}
					array = new bool[]
					{
						!flag,
						!flag2
					};
				}
				result = array;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				OracleException ex2 = new OracleException(-7505, string.Empty, string.Empty, OracleStringResourceManager.GetErrorMesg(-7505, new string[0]), ex);
				throw ex2;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x00054AE0 File Offset: 0x00052CE0
		internal virtual bool AlterSession(bool[] alterConnectionTuple, CriteriaCtx criteriaCtx)
		{
			return false;
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x060007F9 RID: 2041 RVA: 0x00054AE4 File Offset: 0x00052CE4
		internal virtual string ServiceName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x060007FA RID: 2042 RVA: 0x00054AE8 File Offset: 0x00052CE8
		internal virtual string PdbName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x060007FB RID: 2043 RVA: 0x00054AEC File Offset: 0x00052CEC
		internal virtual string EditionName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x00054AF0 File Offset: 0x00052CF0
		internal virtual CriteriaCtx GetDTCCriticalCtx()
		{
			CriteriaCtx criteriaCtx = new CriteriaCtx();
			if (!string.IsNullOrEmpty(this.ServiceName))
			{
				criteriaCtx.m_serviceName = this.ServiceName;
			}
			return criteriaCtx;
		}

		// Token: 0x04000A9D RID: 2717
		public DateTime m_creationTime;

		// Token: 0x04000A9E RID: 2718
		public string m_databaseDomainName;

		// Token: 0x04000A9F RID: 2719
		public string m_instanceName;

		// Token: 0x04000AA0 RID: 2720
		public string m_databaseName;

		// Token: 0x04000AA1 RID: 2721
		public string m_hostName;

		// Token: 0x04000AA2 RID: 2722
		public string m_password;

		// Token: 0x04000AA3 RID: 2723
		public string m_proxyPassword;

		// Token: 0x04000AA4 RID: 2724
		public string m_newPassword;

		// Token: 0x04000AA5 RID: 2725
		public PM m_pm;

		// Token: 0x04000AA6 RID: 2726
		public CP m_cp;

		// Token: 0x04000AA7 RID: 2727
		public DeletionRequestor m_deletionRequestor;

		// Token: 0x04000AA8 RID: 2728
		public ConnectionString m_cs;

		// Token: 0x04000AA9 RID: 2729
		public ManualResetEventSlim m_eventConCreated;

		// Token: 0x04000AAA RID: 2730
		public ManualResetEventSlim m_eventConTimeout;

		// Token: 0x04000AAB RID: 2731
		public bool m_bTimedOut;

		// Token: 0x04000AAC RID: 2732
		public int m_conTimeout;

		// Token: 0x04000AAD RID: 2733
		public Exception m_exception;

		// Token: 0x04000AAE RID: 2734
		public string m_id;

		// Token: 0x04000AAF RID: 2735
		public DateTime m_lastCheckOutTime;

		// Token: 0x04000AB0 RID: 2736
		public bool m_bEndUserSessionEstablished;

		// Token: 0x04000AB1 RID: 2737
		public int m_endUserSessionId = -1;

		// Token: 0x04000AB2 RID: 2738
		internal int m_endUserSerialNum = -1;

		// Token: 0x04000AB3 RID: 2739
		public int m_pxyUserSessionId = -1;

		// Token: 0x04000AB4 RID: 2740
		internal int m_pxyUserSerialNum = -1;

		// Token: 0x04000AB5 RID: 2741
		internal bool m_bPutCompleted;

		// Token: 0x04000AB6 RID: 2742
		internal bool m_bCheckedOutByApp;

		// Token: 0x04000AB7 RID: 2743
		internal bool m_bCheckedOutByDTC;

		// Token: 0x04000AB8 RID: 2744
		internal bool m_bClosedWithReplacement;

		// Token: 0x04000AB9 RID: 2745
		internal bool m_bDynamicallyEnlisted;

		// Token: 0x04000ABA RID: 2746
		internal MTSTxnCtx m_mtsTxnCtx;

		// Token: 0x04000ABB RID: 2747
		internal TransactionContext<PM, CP, PR> m_txnCtx;

		// Token: 0x04000ABC RID: 2748
		internal SessionType m_sessionType;

		// Token: 0x04000ABD RID: 2749
		internal int m_dbMajorVersion;

		// Token: 0x04000ABE RID: 2750
		internal int m_dbMinorVersion;

		// Token: 0x04000ABF RID: 2751
		internal int m_dbPatchsetVersion;

		// Token: 0x04000AC0 RID: 2752
		internal string m_localTxnId;

		// Token: 0x04000AC1 RID: 2753
		internal string m_affinityInstance;

		// Token: 0x04000AC2 RID: 2754
		internal OracleIntervalDS m_sessionTimeZone;

		// Token: 0x04000AC3 RID: 2755
		internal int m_resPoolRefCount;

		// Token: 0x04000AC4 RID: 2756
		internal bool m_bTxnCtxPrimaryCon;

		// Token: 0x04000AC5 RID: 2757
		internal bool m_isDb10gR2OrHigher;

		// Token: 0x04000AC6 RID: 2758
		internal bool m_isDb11gR1OrHigher;

		// Token: 0x04000AC7 RID: 2759
		internal bool m_isDb12cR1OrHigher;

		// Token: 0x04000AC8 RID: 2760
		internal string m_connectionClass;

		// Token: 0x04000AC9 RID: 2761
		internal bool bGotMatchingServerProcess;

		// Token: 0x04000ACA RID: 2762
		internal bool bDRCPServerProcessAttached;

		// Token: 0x04000ACB RID: 2763
		internal uint[] m_criteriaIds;

		// Token: 0x04000ACC RID: 2764
		internal bool bSessionSwitched;

		// Token: 0x04000ACD RID: 2765
		internal bool m_bCheckIfAlterSessionReqd = true;

		// Token: 0x04000ACE RID: 2766
		internal string m_preFailoverInstName;

		// Token: 0x04000ACF RID: 2767
		internal bool m_failoverOccured;

		// Token: 0x04000AD0 RID: 2768
		internal int requestingThreadId;
	}
}
