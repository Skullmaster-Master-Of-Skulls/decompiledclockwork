using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Web.Util;

namespace System.Web.Management
{
	// Token: 0x020001A2 RID: 418
	public sealed class TraceWebEventProvider : WebEventProvider, IInternalWebEventProvider
	{
		// Token: 0x060015FA RID: 5626 RVA: 0x0003C6E9 File Offset: 0x0003A8E9
		internal TraceWebEventProvider()
		{
		}

		// Token: 0x060015FB RID: 5627 RVA: 0x0003CF10 File Offset: 0x0003B110
		public override void Initialize(string name, NameValueCollection config)
		{
			base.Initialize(name, config);
			ProviderUtil.CheckUnrecognizedAttributes(config, name);
		}

		// Token: 0x060015FC RID: 5628 RVA: 0x00043C50 File Offset: 0x00041E50
		public override void ProcessEvent(WebBaseEvent eventRaised)
		{
			if (eventRaised is WebBaseErrorEvent)
			{
				Trace.TraceError(eventRaised.ToString());
				return;
			}
			Trace.TraceInformation(eventRaised.ToString());
		}

		// Token: 0x060015FD RID: 5629 RVA: 0x00006164 File Offset: 0x00004364
		public override void Flush()
		{
		}

		// Token: 0x060015FE RID: 5630 RVA: 0x00006164 File Offset: 0x00004364
		public override void Shutdown()
		{
		}
	}
}
