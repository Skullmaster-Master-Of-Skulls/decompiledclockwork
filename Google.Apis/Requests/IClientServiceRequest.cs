using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Discovery;
using Google.Apis.Services;

namespace Google.Apis.Requests
{
	// Token: 0x02000015 RID: 21
	public interface IClientServiceRequest
	{
		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000CD RID: 205
		string MethodName { get; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000CE RID: 206
		string RestPath { get; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000CF RID: 207
		string HttpMethod { get; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000D0 RID: 208
		IDictionary<string, IParameter> RequestParameters { get; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000D1 RID: 209
		IClientService Service { get; }

		// Token: 0x060000D2 RID: 210
		HttpRequestMessage CreateRequest(bool? overrideGZipEnabled = null);

		// Token: 0x060000D3 RID: 211
		Task<Stream> ExecuteAsStreamAsync();

		// Token: 0x060000D4 RID: 212
		Task<Stream> ExecuteAsStreamAsync(CancellationToken cancellationToken);

		// Token: 0x060000D5 RID: 213
		Stream ExecuteAsStream();
	}
}
