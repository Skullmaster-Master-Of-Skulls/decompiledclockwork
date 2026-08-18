using System;
using System.Threading;
using System.Web.Configuration;
using System.Web.Hosting;

namespace System.Web.Management
{
	// Token: 0x0200019F RID: 415
	internal class HealthMonitoringManager
	{
		// Token: 0x060015E6 RID: 5606 RVA: 0x00043810 File Offset: 0x00041A10
		internal static HealthMonitoringManager Manager()
		{
			if (HealthMonitoringManager.s_initing)
			{
				return null;
			}
			if (HealthMonitoringManager.s_inited)
			{
				return HealthMonitoringManager.s_manager;
			}
			object obj = HealthMonitoringManager.s_lockObject;
			lock (obj)
			{
				if (HealthMonitoringManager.s_inited)
				{
					return HealthMonitoringManager.s_manager;
				}
				try
				{
					HealthMonitoringManager.s_initing = true;
					HealthMonitoringManager.s_manager = new HealthMonitoringManager();
				}
				finally
				{
					HealthMonitoringManager.s_initing = false;
					HealthMonitoringManager.s_inited = true;
				}
			}
			return HealthMonitoringManager.s_manager;
		}

		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x060015E7 RID: 5607 RVA: 0x000438A0 File Offset: 0x00041AA0
		internal static bool Enabled
		{
			get
			{
				if (HostingEnvironment.InClientBuildManager)
				{
					return false;
				}
				HealthMonitoringManager healthMonitoringManager = HealthMonitoringManager.Manager();
				return healthMonitoringManager != null && healthMonitoringManager._enabled;
			}
		}

		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x060015E8 RID: 5608 RVA: 0x000438C7 File Offset: 0x00041AC7
		// (set) Token: 0x060015E9 RID: 5609 RVA: 0x000438CE File Offset: 0x00041ACE
		internal static bool IsCacheDisposed
		{
			get
			{
				return HealthMonitoringManager.s_isCacheDisposed;
			}
			set
			{
				HealthMonitoringManager.s_isCacheDisposed = value;
			}
		}

		// Token: 0x060015EA RID: 5610 RVA: 0x000438D8 File Offset: 0x00041AD8
		internal static void StartHealthMonitoringHeartbeat()
		{
			HealthMonitoringManager healthMonitoringManager = HealthMonitoringManager.Manager();
			if (healthMonitoringManager == null)
			{
				return;
			}
			if (!healthMonitoringManager._enabled)
			{
				return;
			}
			healthMonitoringManager.StartHeartbeatTimer();
		}

		// Token: 0x060015EB RID: 5611 RVA: 0x000438FE File Offset: 0x00041AFE
		private HealthMonitoringManager()
		{
			this._sectionHelper = HealthMonitoringSectionHelper.GetHelper();
			this._enabled = this._sectionHelper.Enabled;
			bool enabled = this._enabled;
		}

		// Token: 0x060015EC RID: 5612 RVA: 0x00043929 File Offset: 0x00041B29
		internal static void Shutdown()
		{
			WebEventManager.Shutdown();
			HealthMonitoringManager.Dispose();
		}

		// Token: 0x060015ED RID: 5613 RVA: 0x00043938 File Offset: 0x00041B38
		internal static void Dispose()
		{
			try
			{
				if (HealthMonitoringManager.s_heartbeatTimer != null)
				{
					HealthMonitoringManager.s_heartbeatTimer.Dispose();
					HealthMonitoringManager.s_heartbeatTimer = null;
				}
			}
			catch
			{
			}
		}

		// Token: 0x060015EE RID: 5614 RVA: 0x00043974 File Offset: 0x00041B74
		internal void HeartbeatCallback(object state)
		{
			WebBaseEvent.RaiseSystemEvent(null, 1005);
		}

		// Token: 0x060015EF RID: 5615 RVA: 0x00043984 File Offset: 0x00041B84
		internal void StartHeartbeatTimer()
		{
			TimeSpan heartbeatInterval = this._sectionHelper.HealthMonitoringSection.HeartbeatInterval;
			if (heartbeatInterval == TimeSpan.Zero)
			{
				return;
			}
			HealthMonitoringManager.s_heartbeatTimer = new Timer(new TimerCallback(this.HeartbeatCallback), null, TimeSpan.Zero, heartbeatInterval);
		}

		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x060015F0 RID: 5616 RVA: 0x000439D0 File Offset: 0x00041BD0
		internal static HealthMonitoringSectionHelper.ProviderInstances ProviderInstances
		{
			get
			{
				HealthMonitoringManager healthMonitoringManager = HealthMonitoringManager.Manager();
				if (healthMonitoringManager == null)
				{
					return null;
				}
				if (!healthMonitoringManager._enabled)
				{
					return null;
				}
				return healthMonitoringManager._sectionHelper._providerInstances;
			}
		}

		// Token: 0x0400166D RID: 5741
		internal HealthMonitoringSectionHelper _sectionHelper;

		// Token: 0x0400166E RID: 5742
		internal bool _enabled;

		// Token: 0x0400166F RID: 5743
		private static Timer s_heartbeatTimer = null;

		// Token: 0x04001670 RID: 5744
		private static HealthMonitoringManager s_manager = null;

		// Token: 0x04001671 RID: 5745
		private static bool s_inited = false;

		// Token: 0x04001672 RID: 5746
		private static bool s_initing = false;

		// Token: 0x04001673 RID: 5747
		private static object s_lockObject = new object();

		// Token: 0x04001674 RID: 5748
		private static bool s_isCacheDisposed = false;
	}
}
