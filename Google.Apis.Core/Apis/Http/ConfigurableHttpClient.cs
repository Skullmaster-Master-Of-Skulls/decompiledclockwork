using System;
using System.Net.Http;

namespace Google.Apis.Http
{
	// Token: 0x02000025 RID: 37
	public class ConfigurableHttpClient : HttpClient
	{
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00003AE6 File Offset: 0x00001CE6
		// (set) Token: 0x060000C7 RID: 199 RVA: 0x00003AEE File Offset: 0x00001CEE
		public ConfigurableMessageHandler MessageHandler { get; private set; }

		// Token: 0x060000C8 RID: 200 RVA: 0x00003AF7 File Offset: 0x00001CF7
		public ConfigurableHttpClient(ConfigurableMessageHandler handler) : base(handler)
		{
			this.MessageHandler = handler;
			base.DefaultRequestHeaders.ExpectContinue = new bool?(false);
		}
	}
}
