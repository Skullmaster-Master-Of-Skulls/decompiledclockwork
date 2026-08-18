using System;
using System.Threading;
using OracleInternal.Common;
using OracleInternal.ServiceObjects;

namespace OracleInternal.ConnectionPool
{
	// Token: 0x020000CE RID: 206
	internal class ServiceCtx
	{
		// Token: 0x06000803 RID: 2051 RVA: 0x00054B68 File Offset: 0x00052D68
		internal ServiceCtx(string serviceName)
		{
			this.m_serviceName = serviceName;
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x00054BD8 File Offset: 0x00052DD8
		internal void CheckAndUpdateServiceMemberDOWNNames_RLB(RLB rlb)
		{
			if (rlb.m_lastUpdateTime.CompareTo(this.m_serviceDownLastUpdateUtcTime) >= 0)
			{
				lock (this.m_sync)
				{
					if (rlb.m_lastUpdateTime.CompareTo(this.m_serviceDownLastUpdateUtcTime) >= 0 && rlb.m_instances.Length > 0)
					{
						this.m_serviceDown = false;
						this.m_serviceDownLastUpdateUtcTime = rlb.m_lastUpdateTime;
					}
				}
			}
			if (rlb.m_lastUpdateTime.CompareTo(this.m_serviceMemberDownLastUpdateUtcTime) >= 0)
			{
				lock (this.m_sync)
				{
					if (rlb.m_lastUpdateTime.CompareTo(this.m_serviceMemberDownLastUpdateUtcTime) >= 0)
					{
						for (int i = 0; i < rlb.m_instances.Length; i++)
						{
							string t = rlb.m_instances[i].ToLowerInvariant();
							if (this.m_serviceMemberDownInstNames.IndexOf(t) >= 0)
							{
								this.m_serviceMemberDownInstNames.Remove(t);
							}
						}
						this.m_serviceMemberDownLastUpdateUtcTime = rlb.m_lastUpdateTime;
					}
				}
			}
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x00054CF8 File Offset: 0x00052EF8
		internal void CheckAndUpdateServiceMemberDOWNNames_HA(string instName, bool serviceMemberDown, DateTime eventTime)
		{
			if (eventTime.CompareTo(this.m_serviceMemberDownLastUpdateUtcTime) >= 0)
			{
				lock (this.m_sync)
				{
					if (eventTime.CompareTo(this.m_serviceMemberDownLastUpdateUtcTime) >= 0)
					{
						if (serviceMemberDown)
						{
							this.m_serviceMemberDownInstNames.AddIfNotExist(instName);
							this.m_serviceMemberDownLastUpdateUtcTime = eventTime;
						}
						else
						{
							this.m_serviceMemberDownInstNames.Remove(instName);
							this.m_serviceMemberDownLastUpdateUtcTime = eventTime;
							this.m_serviceDown = false;
						}
					}
				}
			}
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x00054D88 File Offset: 0x00052F88
		internal void UpdateServiceDown(bool isServiceDown, DateTime eventTime)
		{
			if (eventTime.CompareTo(this.m_serviceDownLastUpdateUtcTime) >= 0)
			{
				lock (this.m_sync)
				{
					if (eventTime.CompareTo(this.m_serviceDownLastUpdateUtcTime) >= 0)
					{
						this.m_serviceDown = isServiceDown;
						this.m_serviceDownLastUpdateUtcTime = eventTime;
					}
				}
			}
		}

		// Token: 0x04000ADE RID: 2782
		internal string m_databaseName;

		// Token: 0x04000ADF RID: 2783
		internal ManualResetEventSlim m_serviceUpEvent = new ManualResetEventSlim(true);

		// Token: 0x04000AE0 RID: 2784
		internal DateTime m_serviceDownTime = DateTime.Now;

		// Token: 0x04000AE1 RID: 2785
		internal bool m_bWaitedForSvcReloc;

		// Token: 0x04000AE2 RID: 2786
		internal RoundRobin m_roundRobin = new RoundRobin();

		// Token: 0x04000AE3 RID: 2787
		internal OracleGlobalizationImpl m_orclGlobImpl;

		// Token: 0x04000AE4 RID: 2788
		internal bool m_serviceDown;

		// Token: 0x04000AE5 RID: 2789
		internal SyncQueueList<string> m_serviceMemberDownInstNames = new SyncQueueList<string>(int.MaxValue);

		// Token: 0x04000AE6 RID: 2790
		internal DateTime m_serviceMemberDownLastUpdateUtcTime = DateTime.Now;

		// Token: 0x04000AE7 RID: 2791
		internal DateTime m_serviceDownLastUpdateUtcTime = DateTime.Now;

		// Token: 0x04000AE8 RID: 2792
		internal object m_sync = new object();

		// Token: 0x04000AE9 RID: 2793
		internal string m_serviceName;
	}
}
