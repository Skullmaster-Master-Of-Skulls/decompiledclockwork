using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Google.Apis.Requests
{
	// Token: 0x02000018 RID: 24
	public sealed class PageStreamer<TResource, TRequest, TResponse, TToken> where TRequest : IClientServiceRequest<TResponse> where TToken : class
	{
		// Token: 0x060000DB RID: 219 RVA: 0x00003D78 File Offset: 0x00001F78
		public PageStreamer(Action<TRequest, TToken> requestModifier, Func<TResponse, TToken> tokenExtractor, Func<TResponse, IEnumerable<TResource>> resourceExtractor)
		{
			if (requestModifier == null)
			{
				throw new ArgumentNullException("requestProvider");
			}
			if (tokenExtractor == null)
			{
				throw new ArgumentNullException("tokenExtractor");
			}
			if (resourceExtractor == null)
			{
				throw new ArgumentNullException("resourceExtractor");
			}
			this.requestModifier = requestModifier;
			this.tokenExtractor = tokenExtractor;
			this.resourceExtractor = resourceExtractor;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00003DCA File Offset: 0x00001FCA
		public IEnumerable<TResource> Fetch(TRequest request)
		{
			if (request == null)
			{
				throw new ArgumentNullException("request");
			}
			TToken token;
			do
			{
				TResponse arg = request.Execute();
				token = this.tokenExtractor(arg);
				this.requestModifier(request, token);
				foreach (TResource tresource in (this.resourceExtractor(arg) ?? PageStreamer<TResource, TRequest, TResponse, TToken>.emptyResources))
				{
					yield return tresource;
				}
				IEnumerator<TResource> enumerator = null;
			}
			while (token != null);
			yield break;
			yield break;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00003DE4 File Offset: 0x00001FE4
		public async Task<IList<TResource>> FetchAllAsync(TRequest request, CancellationToken cancellationToken)
		{
			if (request == null)
			{
				throw new ArgumentNullException("request");
			}
			List<TResource> results = new List<TResource>();
			TToken ttoken;
			do
			{
				cancellationToken.ThrowIfCancellationRequested();
				TResponse arg = await request.ExecuteAsync(cancellationToken).ConfigureAwait(false);
				ttoken = this.tokenExtractor(arg);
				this.requestModifier(request, ttoken);
				results.AddRange(this.resourceExtractor(arg) ?? PageStreamer<TResource, TRequest, TResponse, TToken>.emptyResources);
			}
			while (ttoken != null);
			return results;
		}

		// Token: 0x04000050 RID: 80
		private static readonly TResource[] emptyResources = new TResource[0];

		// Token: 0x04000051 RID: 81
		private readonly Action<TRequest, TToken> requestModifier;

		// Token: 0x04000052 RID: 82
		private readonly Func<TResponse, TToken> tokenExtractor;

		// Token: 0x04000053 RID: 83
		private readonly Func<TResponse, IEnumerable<TResource>> resourceExtractor;
	}
}
