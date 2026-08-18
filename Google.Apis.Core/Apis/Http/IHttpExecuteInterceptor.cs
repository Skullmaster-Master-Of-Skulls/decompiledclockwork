using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Google.Apis.Http
{
	// Token: 0x02000031 RID: 49
	public interface IHttpExecuteInterceptor
	{
		// Token: 0x06000109 RID: 265
		Task InterceptAsync(HttpRequestMessage request, CancellationToken cancellationToken);
	}
}
