using System;

namespace Google.Apis.Http
{
	// Token: 0x0200002E RID: 46
	public interface IHttpClientFactory
	{
		// Token: 0x060000FB RID: 251
		ConfigurableHttpClient CreateHttpClient(CreateHttpClientArgs args);
	}
}
