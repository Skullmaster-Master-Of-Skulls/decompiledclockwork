using System;
using Microsoft.Web.Administration.Interop;

namespace Microsoft.Web.Administration
{
	// Token: 0x02000070 RID: 112
	public sealed class SiteDefaults : ConfigurationElement
	{
		// Token: 0x0600032B RID: 811 RVA: 0x000087F2 File Offset: 0x000077F2
		internal SiteDefaults()
		{
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x0600032C RID: 812 RVA: 0x000087FC File Offset: 0x000077FC
		public SiteLimits Limits
		{
			get
			{
				if (this._limits == null)
				{
					IAppHostElement elementByName = base.AppHostElement.GetElementByName("limits");
					this._limits = new SiteLimits();
					this._limits.Initialize(base.Configuration, elementByName);
				}
				return this._limits;
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x0600032D RID: 813 RVA: 0x00008848 File Offset: 0x00007848
		public SiteLogFile LogFile
		{
			get
			{
				if (this._logfile == null)
				{
					IAppHostElement elementByName = base.AppHostElement.GetElementByName("logFile");
					this._logfile = new SiteLogFile();
					this._logfile.Initialize(base.Configuration, elementByName);
				}
				return this._logfile;
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x0600032E RID: 814 RVA: 0x00008891 File Offset: 0x00007891
		// (set) Token: 0x0600032F RID: 815 RVA: 0x000088A3 File Offset: 0x000078A3
		public bool ServerAutoStart
		{
			get
			{
				return (bool)base["serverAutoStart"];
			}
			set
			{
				base["serverAutoStart"] = value;
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000330 RID: 816 RVA: 0x000088B8 File Offset: 0x000078B8
		public SiteTraceFailedRequestsLogging TraceFailedRequestsLogging
		{
			get
			{
				if (this._traceFailedRequestsLogging == null)
				{
					IAppHostElement elementByName = base.AppHostElement.GetElementByName("traceFailedRequestsLogging");
					this._traceFailedRequestsLogging = new SiteTraceFailedRequestsLogging();
					this._traceFailedRequestsLogging.Initialize(base.Configuration, elementByName);
				}
				return this._traceFailedRequestsLogging;
			}
		}

		// Token: 0x04000127 RID: 295
		private SiteLimits _limits;

		// Token: 0x04000128 RID: 296
		private SiteLogFile _logfile;

		// Token: 0x04000129 RID: 297
		private SiteTraceFailedRequestsLogging _traceFailedRequestsLogging;
	}
}
