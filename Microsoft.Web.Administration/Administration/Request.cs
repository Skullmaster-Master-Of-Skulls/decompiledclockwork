using System;

namespace Microsoft.Web.Administration
{
	// Token: 0x02000064 RID: 100
	public sealed class Request : ConfigurationElement
	{
		// Token: 0x06000293 RID: 659 RVA: 0x00007251 File Offset: 0x00006251
		internal Request(int processId)
		{
			this._processId = processId;
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000294 RID: 660 RVA: 0x00007260 File Offset: 0x00006260
		public string ClientIPAddr
		{
			get
			{
				return (string)base.GetAttributeValue("clientIpAddress");
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000295 RID: 661 RVA: 0x00007272 File Offset: 0x00006272
		public string ConnectionId
		{
			get
			{
				return (string)base.GetAttributeValue("connectionId");
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000296 RID: 662 RVA: 0x00007284 File Offset: 0x00006284
		public string CurrentModule
		{
			get
			{
				return (string)base.GetAttributeValue("currentModule");
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000297 RID: 663 RVA: 0x00007296 File Offset: 0x00006296
		public string HostName
		{
			get
			{
				return (string)base.GetAttributeValue("hostName");
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000298 RID: 664 RVA: 0x000072A8 File Offset: 0x000062A8
		public string LocalIPAddress
		{
			get
			{
				return (string)base.GetAttributeValue("localIpAddress");
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000299 RID: 665 RVA: 0x000072BA File Offset: 0x000062BA
		public int LocalPort
		{
			get
			{
				return (int)((long)base.GetAttributeValue("localPort"));
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x0600029A RID: 666 RVA: 0x000072CD File Offset: 0x000062CD
		public PipelineState PipelineState
		{
			get
			{
				return (PipelineState)((long)base.GetAttributeValue("pipeLineState"));
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x0600029B RID: 667 RVA: 0x000072E0 File Offset: 0x000062E0
		public int ProcessId
		{
			get
			{
				return this._processId;
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x0600029C RID: 668 RVA: 0x000072E8 File Offset: 0x000062E8
		public string RequestId
		{
			get
			{
				return (string)base.GetAttributeValue("requestId");
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x0600029D RID: 669 RVA: 0x000072FA File Offset: 0x000062FA
		public int SiteId
		{
			get
			{
				return (int)((long)base.GetAttributeValue("siteId"));
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x0600029E RID: 670 RVA: 0x0000730D File Offset: 0x0000630D
		public int TimeElapsed
		{
			get
			{
				return (int)((long)base.GetAttributeValue("timeElapsed"));
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x0600029F RID: 671 RVA: 0x00007320 File Offset: 0x00006320
		public int TimeInModule
		{
			get
			{
				return (int)((long)base.GetAttributeValue("timeInModule"));
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x00007333 File Offset: 0x00006333
		public int TimeInState
		{
			get
			{
				return (int)((long)base.GetAttributeValue("timeInState"));
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x00007346 File Offset: 0x00006346
		public string Url
		{
			get
			{
				return (string)base.GetAttributeValue("url");
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x00007358 File Offset: 0x00006358
		public string Verb
		{
			get
			{
				return (string)base.GetAttributeValue("verb");
			}
		}

		// Token: 0x040000FA RID: 250
		private int _processId;
	}
}
