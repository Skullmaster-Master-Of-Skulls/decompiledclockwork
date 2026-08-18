using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Google.Apis.Http;
using Google.Apis.Requests;

namespace Google.Apis.Services
{
	// Token: 0x02000011 RID: 17
	public interface IClientService : IDisposable
	{
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000095 RID: 149
		ConfigurableHttpClient HttpClient { get; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000096 RID: 150
		IConfigurableHttpClientInitializer HttpClientInitializer { get; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000097 RID: 151
		string Name { get; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000098 RID: 152
		string BaseUri { get; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000099 RID: 153
		string BasePath { get; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600009A RID: 154
		IList<string> Features { get; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600009B RID: 155
		bool GZipEnabled { get; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600009C RID: 156
		string ApiKey { get; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600009D RID: 157
		string ApplicationName { get; }

		// Token: 0x0600009E RID: 158
		void SetRequestSerailizedContent(HttpRequestMessage request, object body);

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600009F RID: 159
		ISerializer Serializer { get; }

		// Token: 0x060000A0 RID: 160
		string SerializeObject(object data);

		// Token: 0x060000A1 RID: 161
		Task<T> DeserializeResponse<T>(HttpResponseMessage response);

		// Token: 0x060000A2 RID: 162
		Task<RequestError> DeserializeError(HttpResponseMessage response);
	}
}
