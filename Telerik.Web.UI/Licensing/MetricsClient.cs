using System;
using System.ComponentModel;
using System.IO;
using System.Net;

namespace Telerik.Licensing
{
	// Token: 0x02000435 RID: 1077
	internal class MetricsClient : Client
	{
		// Token: 0x060026A1 RID: 9889 RVA: 0x0007E404 File Offset: 0x0007C604
		public MetricsClient(Config config, ISerializationService service, string accessToken) : base(config, service)
		{
			this._accessToken = accessToken;
		}

		// Token: 0x17000C67 RID: 3175
		// (get) Token: 0x060026A2 RID: 9890 RVA: 0x0007E415 File Offset: 0x0007C615
		protected override HttpWebRequest WebClient
		{
			get
			{
				if (this._webRequest == null)
				{
					this._webRequest = this.InitializeRequest(base.Config.MetricsEndpoint);
				}
				return (HttpWebRequest)this._webRequest;
			}
		}

		// Token: 0x17000C68 RID: 3176
		// (get) Token: 0x060026A3 RID: 9891 RVA: 0x0007E441 File Offset: 0x0007C641
		protected string AccessToken
		{
			get
			{
				return this._accessToken;
			}
		}

		// Token: 0x060026A4 RID: 9892 RVA: 0x0007E449 File Offset: 0x0007C649
		public override void Post(object payload)
		{
			base.Worker.RunWorkerAsync(payload);
		}

		// Token: 0x060026A5 RID: 9893 RVA: 0x0007E458 File Offset: 0x0007C658
		protected override void DoWork(object sender, DoWorkEventArgs e)
		{
			object argument = e.Argument;
			HttpWebRequest webClient = this.WebClient;
			using (StreamWriter streamWriter = new StreamWriter(webClient.GetRequestStream()))
			{
				streamWriter.Write(base.Serialization.SerializeToJson<object>(argument));
			}
			using (HttpWebResponse httpWebResponse = (HttpWebResponse)webClient.GetResponse())
			{
				e.Result = httpWebResponse.StatusCode.ToString();
			}
		}

		// Token: 0x060026A6 RID: 9894 RVA: 0x0007E4E8 File Offset: 0x0007C6E8
		protected override WebRequest InitializeRequest(Uri endpoint)
		{
			WebRequest webRequest = base.InitializeRequest(endpoint);
			webRequest.Headers[HttpRequestHeader.Authorization] = "Bearer " + this.AccessToken;
			return webRequest;
		}

		// Token: 0x040009E7 RID: 2535
		private readonly string _accessToken;

		// Token: 0x040009E8 RID: 2536
		private WebRequest _webRequest;
	}
}
