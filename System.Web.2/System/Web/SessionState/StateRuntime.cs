using System;
using System.Security.Permissions;
using System.Web.Configuration;

namespace System.Web.SessionState
{
	// Token: 0x02000138 RID: 312
	[SecurityPermission(SecurityAction.LinkDemand, Unrestricted = true)]
	public sealed class StateRuntime : IStateRuntime
	{
		// Token: 0x060012B6 RID: 4790 RVA: 0x000357C4 File Offset: 0x000339C4
		static StateRuntime()
		{
			WebConfigurationFileMap fileMap = new WebConfigurationFileMap();
			UserMapPath configMapPath = new UserMapPath(fileMap);
			HttpConfigurationSystem.EnsureInit(configMapPath, false, true);
			StateApplication customApplication = new StateApplication();
			HttpApplicationFactory.SetCustomApplication(customApplication);
			PerfCounters.OpenStateCounters();
			StateRuntime.ResetStateServerCounters();
		}

		// Token: 0x060012B8 RID: 4792 RVA: 0x000357FC File Offset: 0x000339FC
		public void StopProcessing()
		{
			StateRuntime.ResetStateServerCounters();
			HttpRuntime.Close();
		}

		// Token: 0x060012B9 RID: 4793 RVA: 0x00035808 File Offset: 0x00033A08
		private static void ResetStateServerCounters()
		{
			PerfCounters.SetStateServiceCounter(StateServicePerfCounter.STATE_SERVICE_SESSIONS_TOTAL, 0);
			PerfCounters.SetStateServiceCounter(StateServicePerfCounter.STATE_SERVICE_SESSIONS_ACTIVE, 0);
			PerfCounters.SetStateServiceCounter(StateServicePerfCounter.STATE_SERVICE_SESSIONS_TIMED_OUT, 0);
			PerfCounters.SetStateServiceCounter(StateServicePerfCounter.STATE_SERVICE_SESSIONS_ABANDONED, 0);
		}

		// Token: 0x060012BA RID: 4794 RVA: 0x0003582C File Offset: 0x00033A2C
		public void ProcessRequest(IntPtr tracker, int verb, string uri, int exclusive, int timeout, int lockCookieExists, int lockCookie, int contentLength, IntPtr content)
		{
			this.ProcessRequest(tracker, verb, uri, exclusive, 0, timeout, lockCookieExists, lockCookie, contentLength, content);
		}

		// Token: 0x060012BB RID: 4795 RVA: 0x00035850 File Offset: 0x00033A50
		public void ProcessRequest(IntPtr tracker, int verb, string uri, int exclusive, int extraFlags, int timeout, int lockCookieExists, int lockCookie, int contentLength, IntPtr content)
		{
			StateHttpWorkerRequest wr = new StateHttpWorkerRequest(tracker, (UnsafeNativeMethods.StateProtocolVerb)verb, uri, (UnsafeNativeMethods.StateProtocolExclusive)exclusive, extraFlags, timeout, lockCookieExists, lockCookie, contentLength, content);
			HttpRuntime.ProcessRequest(wr);
		}
	}
}
