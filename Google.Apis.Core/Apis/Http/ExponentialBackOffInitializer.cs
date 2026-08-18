using System;

namespace Google.Apis.Http
{
	// Token: 0x02000028 RID: 40
	public class ExponentialBackOffInitializer : IConfigurableHttpClientInitializer
	{
		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x000040AE File Offset: 0x000022AE
		// (set) Token: 0x060000E8 RID: 232 RVA: 0x000040B6 File Offset: 0x000022B6
		private ExponentialBackOffPolicy Policy { get; set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x000040BF File Offset: 0x000022BF
		// (set) Token: 0x060000EA RID: 234 RVA: 0x000040C7 File Offset: 0x000022C7
		private Func<BackOffHandler> CreateBackOff { get; set; }

		// Token: 0x060000EB RID: 235 RVA: 0x000040D0 File Offset: 0x000022D0
		public ExponentialBackOffInitializer(ExponentialBackOffPolicy policy, Func<BackOffHandler> createBackOff)
		{
			this.Policy = policy;
			this.CreateBackOff = createBackOff;
		}

		// Token: 0x060000EC RID: 236 RVA: 0x000040E8 File Offset: 0x000022E8
		public void Initialize(ConfigurableHttpClient httpClient)
		{
			BackOffHandler handler = this.CreateBackOff();
			if ((this.Policy & ExponentialBackOffPolicy.Exception) == ExponentialBackOffPolicy.Exception)
			{
				httpClient.MessageHandler.AddExceptionHandler(handler);
			}
			if ((this.Policy & ExponentialBackOffPolicy.UnsuccessfulResponse503) == ExponentialBackOffPolicy.UnsuccessfulResponse503)
			{
				httpClient.MessageHandler.AddUnsuccessfulResponseHandler(handler);
			}
		}
	}
}
