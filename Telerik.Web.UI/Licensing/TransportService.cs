using System;
using System.ComponentModel;
using System.Net;
using Telerik.Licensing.Serialization;

namespace Telerik.Licensing
{
	// Token: 0x02000436 RID: 1078
	internal class TransportService : ITransportService
	{
		// Token: 0x060026A7 RID: 9895 RVA: 0x0007E51B File Offset: 0x0007C71B
		public TransportService(Config config)
		{
			this._config = config;
		}

		// Token: 0x17000C69 RID: 3177
		// (get) Token: 0x060026A8 RID: 9896 RVA: 0x0007E52A File Offset: 0x0007C72A
		protected Config Config
		{
			get
			{
				return this._config;
			}
		}

		// Token: 0x17000C6A RID: 3178
		// (get) Token: 0x060026A9 RID: 9897 RVA: 0x0007E532 File Offset: 0x0007C732
		protected string AccessToken
		{
			get
			{
				return TransportService.accessToken;
			}
		}

		// Token: 0x060026AA RID: 9898 RVA: 0x0007E558 File Offset: 0x0007C758
		public virtual void CallHome(RequestPayload data)
		{
			Action action = delegate()
			{
				this.RequestMetrics(data, 0);
			};
			if (string.IsNullOrEmpty(this.AccessToken))
			{
				this.EnsureAccessToken(action);
				return;
			}
			action();
		}

		// Token: 0x060026AB RID: 9899 RVA: 0x0007E5A1 File Offset: 0x0007C7A1
		protected virtual Client GetAccessTokenClient()
		{
			return new AccessTokenClient(this.Config, SerializationService.GetInstance());
		}

		// Token: 0x060026AC RID: 9900 RVA: 0x0007E5B3 File Offset: 0x0007C7B3
		protected virtual Client GetMetricsClient(string token)
		{
			return new MetricsClient(this.Config, SerializationService.GetInstance(), token);
		}

		// Token: 0x060026AD RID: 9901 RVA: 0x0007E62C File Offset: 0x0007C82C
		private void RequestMetrics(RequestPayload data, int counter)
		{
			if (counter > 5)
			{
				return;
			}
			Client metricsClient = this.GetMetricsClient(this.AccessToken);
			metricsClient.RequestCompleted += delegate(object metricsSender, RunWorkerCompletedEventArgs metricsArgs)
			{
				if (this.IsAccessUnauthorized(metricsArgs.Error))
				{
					Action doneCallback = delegate()
					{
						this.RequestMetrics(data, counter + 1);
					};
					this.EnsureAccessToken(doneCallback);
				}
			};
			metricsClient.Post(data);
		}

		// Token: 0x060026AE RID: 9902 RVA: 0x0007E6F4 File Offset: 0x0007C8F4
		private void EnsureAccessToken(Action doneCallback)
		{
			Client accessTokenClient = this.GetAccessTokenClient();
			accessTokenClient.RequestCompleted += delegate(object tokenSender, RunWorkerCompletedEventArgs tokenArgs)
			{
				if (tokenArgs.Error != null)
				{
					return;
				}
				lock (TransportService.accessTokenLock)
				{
					TransportService.accessToken = (string)tokenArgs.Result;
				}
				doneCallback();
			};
			accessTokenClient.Post(new AccessTokenPayload());
		}

		// Token: 0x060026AF RID: 9903 RVA: 0x0007E734 File Offset: 0x0007C934
		private bool IsAccessUnauthorized(Exception error)
		{
			WebException ex = error as WebException;
			if (ex != null)
			{
				HttpWebResponse httpWebResponse = ex.Response as HttpWebResponse;
				if (httpWebResponse != null && httpWebResponse.StatusCode == HttpStatusCode.Unauthorized)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x040009E9 RID: 2537
		private static readonly object accessTokenLock = new object();

		// Token: 0x040009EA RID: 2538
		private static string accessToken;

		// Token: 0x040009EB RID: 2539
		private readonly Config _config;
	}
}
