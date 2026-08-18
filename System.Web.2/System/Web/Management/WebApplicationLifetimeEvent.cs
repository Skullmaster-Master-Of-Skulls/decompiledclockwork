using System;

namespace System.Web.Management
{
	// Token: 0x0200018E RID: 398
	public class WebApplicationLifetimeEvent : WebManagementEvent
	{
		// Token: 0x06001566 RID: 5478 RVA: 0x00041FBF File Offset: 0x000401BF
		protected internal WebApplicationLifetimeEvent(string message, object eventSource, int eventCode) : base(message, eventSource, eventCode)
		{
		}

		// Token: 0x06001567 RID: 5479 RVA: 0x00041FCA File Offset: 0x000401CA
		protected internal WebApplicationLifetimeEvent(string message, object eventSource, int eventCode, int eventDetailCode) : base(message, eventSource, eventCode, eventDetailCode)
		{
		}

		// Token: 0x06001568 RID: 5480 RVA: 0x00041F4B File Offset: 0x0004014B
		internal WebApplicationLifetimeEvent()
		{
		}

		// Token: 0x06001569 RID: 5481 RVA: 0x00041FD8 File Offset: 0x000401D8
		internal static int DetailCodeFromShutdownReason(ApplicationShutdownReason reason)
		{
			switch (reason)
			{
			case ApplicationShutdownReason.HostingEnvironment:
				return 50002;
			case ApplicationShutdownReason.ChangeInGlobalAsax:
				return 50003;
			case ApplicationShutdownReason.ConfigurationChange:
				return 50004;
			case ApplicationShutdownReason.UnloadAppDomainCalled:
				return 50005;
			case ApplicationShutdownReason.ChangeInSecurityPolicyFile:
				return 50006;
			case ApplicationShutdownReason.BinDirChangeOrDirectoryRename:
				return 50007;
			case ApplicationShutdownReason.BrowsersDirChangeOrDirectoryRename:
				return 50008;
			case ApplicationShutdownReason.CodeDirChangeOrDirectoryRename:
				return 50009;
			case ApplicationShutdownReason.ResourcesDirChangeOrDirectoryRename:
				return 50010;
			case ApplicationShutdownReason.IdleTimeout:
				return 50011;
			case ApplicationShutdownReason.PhysicalApplicationPathChanged:
				return 50012;
			case ApplicationShutdownReason.HttpRuntimeClose:
				return 50013;
			case ApplicationShutdownReason.InitializationError:
				return 50014;
			case ApplicationShutdownReason.MaxRecompilationsReached:
				return 50015;
			case ApplicationShutdownReason.BuildManagerChange:
				return 50017;
			default:
				return 50001;
			}
		}

		// Token: 0x0600156A RID: 5482 RVA: 0x0004208A File Offset: 0x0004028A
		protected internal override void IncrementPerfCounters()
		{
			base.IncrementPerfCounters();
			PerfCounters.IncrementCounter(AppPerfCounter.EVENTS_APP);
		}
	}
}
