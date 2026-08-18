using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Testing;

namespace Google.Apis.Http
{
	// Token: 0x02000034 RID: 52
	[VisibleForTestOnly]
	public class MaxUrlLengthInterceptor : IHttpExecuteInterceptor
	{
		// Token: 0x06000117 RID: 279 RVA: 0x0000439D File Offset: 0x0000259D
		public MaxUrlLengthInterceptor(uint maxUrlLength)
		{
			this.maxUrlLength = maxUrlLength;
		}

		// Token: 0x06000118 RID: 280 RVA: 0x000043AC File Offset: 0x000025AC
		public Task InterceptAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			if (request.Method != HttpMethod.Get || (long)request.RequestUri.AbsoluteUri.Length <= (long)((ulong)this.maxUrlLength))
			{
				return Task.Delay(0);
			}
			request.Method = HttpMethod.Post;
			string query = request.RequestUri.Query;
			if (!string.IsNullOrEmpty(query))
			{
				request.Content = new StringContent(query.Substring(1));
				request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
				string text = request.RequestUri.ToString();
				request.RequestUri = new Uri(text.Remove(text.IndexOf("?")));
			}
			request.Headers.Add("X-HTTP-Method-Override", "GET");
			return Task.Delay(0);
		}

		// Token: 0x0400006E RID: 110
		private readonly uint maxUrlLength;
	}
}
