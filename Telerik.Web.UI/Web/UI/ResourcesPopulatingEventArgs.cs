using System;
using System.ComponentModel;
using System.Net;

namespace Telerik.Web.UI
{
	// Token: 0x02001A16 RID: 6678
	public class ResourcesPopulatingEventArgs : CancelEventArgs
	{
		// Token: 0x17004DFE RID: 19966
		// (get) Token: 0x06010268 RID: 66152 RVA: 0x0039FAEE File Offset: 0x0039DCEE
		// (set) Token: 0x06010269 RID: 66153 RVA: 0x0039FAF6 File Offset: 0x0039DCF6
		public ISchedulerInfo SchedulerInfo { get; set; }

		// Token: 0x17004DFF RID: 19967
		// (get) Token: 0x0601026A RID: 66154 RVA: 0x0039FAFF File Offset: 0x0039DCFF
		// (set) Token: 0x0601026B RID: 66155 RVA: 0x0039FB07 File Offset: 0x0039DD07
		public string ServicePath { get; set; }

		// Token: 0x17004E00 RID: 19968
		// (get) Token: 0x0601026C RID: 66156 RVA: 0x0039FB10 File Offset: 0x0039DD10
		// (set) Token: 0x0601026D RID: 66157 RVA: 0x0039FB18 File Offset: 0x0039DD18
		public WebHeaderCollection Headers { get; private set; }

		// Token: 0x17004E01 RID: 19969
		// (get) Token: 0x0601026E RID: 66158 RVA: 0x0039FB21 File Offset: 0x0039DD21
		// (set) Token: 0x0601026F RID: 66159 RVA: 0x0039FB29 File Offset: 0x0039DD29
		public ICredentials Credentials { get; set; }

		// Token: 0x17004E02 RID: 19970
		// (get) Token: 0x06010270 RID: 66160 RVA: 0x0039FB32 File Offset: 0x0039DD32
		// (set) Token: 0x06010271 RID: 66161 RVA: 0x0039FB3A File Offset: 0x0039DD3A
		public IWebProxy Proxy { get; set; }

		// Token: 0x06010272 RID: 66162 RVA: 0x0039FB43 File Offset: 0x0039DD43
		public ResourcesPopulatingEventArgs(ISchedulerInfo schedulerInfo) : this(schedulerInfo, null, null)
		{
		}

		// Token: 0x06010273 RID: 66163 RVA: 0x0039FB4E File Offset: 0x0039DD4E
		public ResourcesPopulatingEventArgs(ISchedulerInfo schedulerInfo, string servicePath, WebHeaderCollection headers)
		{
			this.SchedulerInfo = schedulerInfo;
			this.ServicePath = servicePath;
			this.Headers = headers;
		}
	}
}
