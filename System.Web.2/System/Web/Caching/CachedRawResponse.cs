using System;

namespace System.Web.Caching
{
	// Token: 0x02000898 RID: 2200
	internal class CachedRawResponse
	{
		// Token: 0x0600672B RID: 26411 RVA: 0x0016C3E9 File Offset: 0x0016A5E9
		internal CachedRawResponse(HttpRawResponse rawResponse, HttpCachePolicySettings settings, string kernelCacheUrl, Guid cachedVaryId)
		{
			this._rawResponse = rawResponse;
			this._settings = settings;
			this._kernelCacheUrl = kernelCacheUrl;
			this._cachedVaryId = cachedVaryId;
		}

		// Token: 0x0400354D RID: 13645
		internal Guid _cachedVaryId;

		// Token: 0x0400354E RID: 13646
		internal readonly HttpRawResponse _rawResponse;

		// Token: 0x0400354F RID: 13647
		internal readonly HttpCachePolicySettings _settings;

		// Token: 0x04003550 RID: 13648
		internal readonly string _kernelCacheUrl;
	}
}
