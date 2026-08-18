using System;
using System.Collections.Specialized;
using System.Web.Hosting;
using System.Web.Util;

namespace System.Web.Management
{
	// Token: 0x02000170 RID: 368
	public sealed class IisTraceWebEventProvider : WebEventProvider
	{
		// Token: 0x06001473 RID: 5235 RVA: 0x0003CED0 File Offset: 0x0003B0D0
		public IisTraceWebEventProvider()
		{
			HttpContext httpContext = HttpContext.Current;
			if (httpContext != null && !HttpRuntime.UseIntegratedPipeline && !(httpContext.WorkerRequest is ISAPIWorkerRequestInProcForIIS7))
			{
				throw new PlatformNotSupportedException(SR.GetString("Requires_Iis_7"));
			}
		}

		// Token: 0x06001474 RID: 5236 RVA: 0x0003CF10 File Offset: 0x0003B110
		public override void Initialize(string name, NameValueCollection config)
		{
			base.Initialize(name, config);
			ProviderUtil.CheckUnrecognizedAttributes(config, name);
		}

		// Token: 0x06001475 RID: 5237 RVA: 0x0003CF24 File Offset: 0x0003B124
		public override void ProcessEvent(WebBaseEvent eventRaised)
		{
			HttpContext httpContext = HttpContext.Current;
			if (httpContext != null)
			{
				httpContext.WorkerRequest.RaiseTraceEvent(eventRaised);
			}
		}

		// Token: 0x06001476 RID: 5238 RVA: 0x00006164 File Offset: 0x00004364
		public override void Flush()
		{
		}

		// Token: 0x06001477 RID: 5239 RVA: 0x00006164 File Offset: 0x00004364
		public override void Shutdown()
		{
		}
	}
}
