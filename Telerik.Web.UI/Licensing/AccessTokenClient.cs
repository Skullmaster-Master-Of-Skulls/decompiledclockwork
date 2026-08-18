using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Text;

namespace Telerik.Licensing
{
	// Token: 0x02000430 RID: 1072
	internal class AccessTokenClient : Client
	{
		// Token: 0x0600268B RID: 9867 RVA: 0x0007E135 File Offset: 0x0007C335
		public AccessTokenClient(Config config, ISerializationService service) : base(config, service)
		{
		}

		// Token: 0x17000C60 RID: 3168
		// (get) Token: 0x0600268C RID: 9868 RVA: 0x0007E13F File Offset: 0x0007C33F
		protected override HttpWebRequest WebClient
		{
			get
			{
				if (this._webRequest == null)
				{
					this._webRequest = this.InitializeRequest(base.Config.TokenEndpoint);
				}
				return (HttpWebRequest)this._webRequest;
			}
		}

		// Token: 0x0600268D RID: 9869 RVA: 0x0007E16B File Offset: 0x0007C36B
		public override void Post(object payload)
		{
			base.Worker.RunWorkerAsync(payload);
		}

		// Token: 0x0600268E RID: 9870 RVA: 0x0007E17C File Offset: 0x0007C37C
		protected override void DoWork(object sender, DoWorkEventArgs e)
		{
			object argument = e.Argument;
			HttpWebRequest webClient = this.WebClient;
			using (StreamWriter streamWriter = new StreamWriter(webClient.GetRequestStream()))
			{
				streamWriter.Write(base.Serialization.Serialize<object>(argument));
			}
			using (WebResponse response = webClient.GetResponse())
			{
				using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
				{
					string serializedObj = streamReader.ReadToEnd();
					e.Result = base.Serialization.Deserialize<AccessTokenResponse>(serializedObj).Access_Token;
				}
			}
		}

		// Token: 0x0600268F RID: 9871 RVA: 0x0007E238 File Offset: 0x0007C438
		protected override WebRequest InitializeRequest(Uri endpoint)
		{
			WebRequest webRequest = base.InitializeRequest(endpoint);
			string text = string.Format("{0}:{1}", Uri.EscapeDataString(base.Config.ClientId), base.Config.ClientSecret);
			text = Convert.ToBase64String(Encoding.UTF8.GetBytes(text));
			webRequest.Headers[HttpRequestHeader.Authorization] = "Basic " + text;
			return webRequest;
		}

		// Token: 0x040009DA RID: 2522
		private WebRequest _webRequest;
	}
}
