using System;
using System.Collections.Generic;

namespace System.Web.Caching
{
	// Token: 0x02000888 RID: 2184
	[Serializable]
	internal class OutputCacheEntry : IOutputCacheEntry
	{
		// Token: 0x17001CC2 RID: 7362
		// (get) Token: 0x060066C8 RID: 26312 RVA: 0x0016A7A0 File Offset: 0x001689A0
		internal Guid CachedVaryId
		{
			get
			{
				return this._cachedVaryId;
			}
		}

		// Token: 0x17001CC3 RID: 7363
		// (get) Token: 0x060066C9 RID: 26313 RVA: 0x0016A7A8 File Offset: 0x001689A8
		internal HttpCachePolicySettings Settings
		{
			get
			{
				return this._settings;
			}
		}

		// Token: 0x17001CC4 RID: 7364
		// (get) Token: 0x060066CA RID: 26314 RVA: 0x0016A7B0 File Offset: 0x001689B0
		internal string KernelCacheUrl
		{
			get
			{
				return this._kernelCacheUrl;
			}
		}

		// Token: 0x17001CC5 RID: 7365
		// (get) Token: 0x060066CB RID: 26315 RVA: 0x0016A7B8 File Offset: 0x001689B8
		internal string DependenciesKey
		{
			get
			{
				return this._dependenciesKey;
			}
		}

		// Token: 0x17001CC6 RID: 7366
		// (get) Token: 0x060066CC RID: 26316 RVA: 0x0016A7C0 File Offset: 0x001689C0
		internal string[] Dependencies
		{
			get
			{
				return this._dependencies;
			}
		}

		// Token: 0x17001CC7 RID: 7367
		// (get) Token: 0x060066CD RID: 26317 RVA: 0x0016A7C8 File Offset: 0x001689C8
		internal int StatusCode
		{
			get
			{
				return this._statusCode;
			}
		}

		// Token: 0x17001CC8 RID: 7368
		// (get) Token: 0x060066CE RID: 26318 RVA: 0x0016A7D0 File Offset: 0x001689D0
		internal string StatusDescription
		{
			get
			{
				return this._statusDescription;
			}
		}

		// Token: 0x17001CC9 RID: 7369
		// (get) Token: 0x060066CF RID: 26319 RVA: 0x0016A7D8 File Offset: 0x001689D8
		// (set) Token: 0x060066D0 RID: 26320 RVA: 0x0016A7E0 File Offset: 0x001689E0
		public List<HeaderElement> HeaderElements
		{
			get
			{
				return this._headerElements;
			}
			set
			{
				this._headerElements = value;
			}
		}

		// Token: 0x17001CCA RID: 7370
		// (get) Token: 0x060066D1 RID: 26321 RVA: 0x0016A7E9 File Offset: 0x001689E9
		// (set) Token: 0x060066D2 RID: 26322 RVA: 0x0016A7F1 File Offset: 0x001689F1
		public List<ResponseElement> ResponseElements
		{
			get
			{
				return this._responseElements;
			}
			set
			{
				this._responseElements = value;
			}
		}

		// Token: 0x060066D3 RID: 26323 RVA: 0x000030B5 File Offset: 0x000012B5
		private OutputCacheEntry()
		{
		}

		// Token: 0x060066D4 RID: 26324 RVA: 0x0016A7FC File Offset: 0x001689FC
		internal OutputCacheEntry(Guid cachedVaryId, HttpCachePolicySettings settings, string kernelCacheUrl, string dependenciesKey, string[] dependencies, int statusCode, string statusDescription, List<HeaderElement> headerElements, List<ResponseElement> responseElements)
		{
			this._cachedVaryId = cachedVaryId;
			this._settings = settings;
			this._kernelCacheUrl = kernelCacheUrl;
			this._dependenciesKey = dependenciesKey;
			this._dependencies = dependencies;
			this._statusCode = statusCode;
			this._statusDescription = statusDescription;
			this._headerElements = headerElements;
			this._responseElements = responseElements;
		}

		// Token: 0x040034FC RID: 13564
		private Guid _cachedVaryId;

		// Token: 0x040034FD RID: 13565
		private HttpCachePolicySettings _settings;

		// Token: 0x040034FE RID: 13566
		private string _kernelCacheUrl;

		// Token: 0x040034FF RID: 13567
		private string _dependenciesKey;

		// Token: 0x04003500 RID: 13568
		private string[] _dependencies;

		// Token: 0x04003501 RID: 13569
		private int _statusCode;

		// Token: 0x04003502 RID: 13570
		private string _statusDescription;

		// Token: 0x04003503 RID: 13571
		private List<HeaderElement> _headerElements;

		// Token: 0x04003504 RID: 13572
		private List<ResponseElement> _responseElements;
	}
}
