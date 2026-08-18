using System;
using System.Net;
using System.Net.Http;
using Google.Apis.Logging;

namespace Google.Apis.Http
{
	// Token: 0x02000029 RID: 41
	public class HttpClientFactory : IHttpClientFactory
	{
		// Token: 0x060000ED RID: 237 RVA: 0x00004130 File Offset: 0x00002330
		public ConfigurableHttpClient CreateHttpClient(CreateHttpClientArgs args)
		{
			ConfigurableHttpClient configurableHttpClient = new ConfigurableHttpClient(new ConfigurableMessageHandler(this.CreateHandler(args))
			{
				ApplicationName = args.ApplicationName
			});
			foreach (IConfigurableHttpClientInitializer configurableHttpClientInitializer in args.Initializers)
			{
				configurableHttpClientInitializer.Initialize(configurableHttpClient);
			}
			return configurableHttpClient;
		}

		// Token: 0x060000EE RID: 238 RVA: 0x0000419C File Offset: 0x0000239C
		protected virtual HttpMessageHandler CreateHandler(CreateHttpClientArgs args)
		{
			HttpClientHandler httpClientHandler = new HttpClientHandler();
			if (httpClientHandler.SupportsRedirectConfiguration)
			{
				httpClientHandler.AllowAutoRedirect = false;
			}
			if (httpClientHandler.SupportsAutomaticDecompression && args.GZipEnabled)
			{
				httpClientHandler.AutomaticDecompression = (DecompressionMethods.GZip | DecompressionMethods.Deflate);
			}
			HttpClientFactory.Logger.Debug("Handler was created. SupportsRedirectConfiguration={0}, SupportsAutomaticDecompression={1}", new object[]
			{
				httpClientHandler.SupportsRedirectConfiguration,
				httpClientHandler.SupportsAutomaticDecompression
			});
			return httpClientHandler;
		}

		// Token: 0x0400005B RID: 91
		private static readonly ILogger Logger = ApplicationContext.Logger.ForType<HttpClientFactory>();
	}
}
