using System;
using System.IO;

namespace System.Net.Cache
{
	// Token: 0x0200055C RID: 1372
	internal abstract class RequestCacheValidator
	{
		// Token: 0x060029BA RID: 10682
		internal abstract RequestCacheValidator CreateValidator();

		// Token: 0x060029BB RID: 10683 RVA: 0x000AF221 File Offset: 0x000AE221
		protected RequestCacheValidator(bool strictCacheErrors, TimeSpan unspecifiedMaxAge)
		{
			this._StrictCacheErrors = strictCacheErrors;
			this._UnspecifiedMaxAge = unspecifiedMaxAge;
			this._ValidationStatus = CacheValidationStatus.DoNotUseCache;
			this._CacheFreshnessStatus = CacheFreshnessStatus.Undefined;
		}

		// Token: 0x17000888 RID: 2184
		// (get) Token: 0x060029BC RID: 10684 RVA: 0x000AF245 File Offset: 0x000AE245
		internal bool StrictCacheErrors
		{
			get
			{
				return this._StrictCacheErrors;
			}
		}

		// Token: 0x17000889 RID: 2185
		// (get) Token: 0x060029BD RID: 10685 RVA: 0x000AF24D File Offset: 0x000AE24D
		internal TimeSpan UnspecifiedMaxAge
		{
			get
			{
				return this._UnspecifiedMaxAge;
			}
		}

		// Token: 0x1700088A RID: 2186
		// (get) Token: 0x060029BE RID: 10686 RVA: 0x000AF255 File Offset: 0x000AE255
		protected internal Uri Uri
		{
			get
			{
				return this._Uri;
			}
		}

		// Token: 0x1700088B RID: 2187
		// (get) Token: 0x060029BF RID: 10687 RVA: 0x000AF25D File Offset: 0x000AE25D
		protected internal WebRequest Request
		{
			get
			{
				return this._Request;
			}
		}

		// Token: 0x1700088C RID: 2188
		// (get) Token: 0x060029C0 RID: 10688 RVA: 0x000AF265 File Offset: 0x000AE265
		protected internal WebResponse Response
		{
			get
			{
				return this._Response;
			}
		}

		// Token: 0x1700088D RID: 2189
		// (get) Token: 0x060029C1 RID: 10689 RVA: 0x000AF26D File Offset: 0x000AE26D
		protected internal RequestCachePolicy Policy
		{
			get
			{
				return this._Policy;
			}
		}

		// Token: 0x1700088E RID: 2190
		// (get) Token: 0x060029C2 RID: 10690 RVA: 0x000AF275 File Offset: 0x000AE275
		protected internal int ResponseCount
		{
			get
			{
				return this._ResponseCount;
			}
		}

		// Token: 0x1700088F RID: 2191
		// (get) Token: 0x060029C3 RID: 10691 RVA: 0x000AF27D File Offset: 0x000AE27D
		protected internal CacheValidationStatus ValidationStatus
		{
			get
			{
				return this._ValidationStatus;
			}
		}

		// Token: 0x17000890 RID: 2192
		// (get) Token: 0x060029C4 RID: 10692 RVA: 0x000AF285 File Offset: 0x000AE285
		protected internal CacheFreshnessStatus CacheFreshnessStatus
		{
			get
			{
				return this._CacheFreshnessStatus;
			}
		}

		// Token: 0x17000891 RID: 2193
		// (get) Token: 0x060029C5 RID: 10693 RVA: 0x000AF28D File Offset: 0x000AE28D
		protected internal RequestCacheEntry CacheEntry
		{
			get
			{
				return this._CacheEntry;
			}
		}

		// Token: 0x17000892 RID: 2194
		// (get) Token: 0x060029C6 RID: 10694 RVA: 0x000AF295 File Offset: 0x000AE295
		// (set) Token: 0x060029C7 RID: 10695 RVA: 0x000AF29D File Offset: 0x000AE29D
		protected internal Stream CacheStream
		{
			get
			{
				return this._CacheStream;
			}
			set
			{
				this._CacheStream = value;
			}
		}

		// Token: 0x17000893 RID: 2195
		// (get) Token: 0x060029C8 RID: 10696 RVA: 0x000AF2A6 File Offset: 0x000AE2A6
		// (set) Token: 0x060029C9 RID: 10697 RVA: 0x000AF2AE File Offset: 0x000AE2AE
		protected internal long CacheStreamOffset
		{
			get
			{
				return this._CacheStreamOffset;
			}
			set
			{
				this._CacheStreamOffset = value;
			}
		}

		// Token: 0x17000894 RID: 2196
		// (get) Token: 0x060029CA RID: 10698 RVA: 0x000AF2B7 File Offset: 0x000AE2B7
		// (set) Token: 0x060029CB RID: 10699 RVA: 0x000AF2BF File Offset: 0x000AE2BF
		protected internal long CacheStreamLength
		{
			get
			{
				return this._CacheStreamLength;
			}
			set
			{
				this._CacheStreamLength = value;
			}
		}

		// Token: 0x17000895 RID: 2197
		// (get) Token: 0x060029CC RID: 10700 RVA: 0x000AF2C8 File Offset: 0x000AE2C8
		protected internal string CacheKey
		{
			get
			{
				return this._CacheKey;
			}
		}

		// Token: 0x060029CD RID: 10701
		protected internal abstract CacheValidationStatus ValidateRequest();

		// Token: 0x060029CE RID: 10702
		protected internal abstract CacheFreshnessStatus ValidateFreshness();

		// Token: 0x060029CF RID: 10703
		protected internal abstract CacheValidationStatus ValidateCache();

		// Token: 0x060029D0 RID: 10704
		protected internal abstract CacheValidationStatus ValidateResponse();

		// Token: 0x060029D1 RID: 10705
		protected internal abstract CacheValidationStatus RevalidateCache();

		// Token: 0x060029D2 RID: 10706
		protected internal abstract CacheValidationStatus UpdateCache();

		// Token: 0x060029D3 RID: 10707 RVA: 0x000AF2D0 File Offset: 0x000AE2D0
		protected internal virtual void FailRequest(WebExceptionStatus webStatus)
		{
			if (Logging.On)
			{
				Logging.PrintError(Logging.RequestCache, SR.GetString("net_log_cache_failing_request_with_exception", new object[]
				{
					webStatus.ToString()
				}));
			}
			if (webStatus == WebExceptionStatus.CacheEntryNotFound)
			{
				throw ExceptionHelper.CacheEntryNotFoundException;
			}
			if (webStatus == WebExceptionStatus.RequestProhibitedByCachePolicy)
			{
				throw ExceptionHelper.RequestProhibitedByCachePolicyException;
			}
			throw new WebException(NetRes.GetWebStatusString("net_requestaborted", webStatus), webStatus);
		}

		// Token: 0x060029D4 RID: 10708 RVA: 0x000AF338 File Offset: 0x000AE338
		internal void FetchRequest(Uri uri, WebRequest request)
		{
			this._Request = request;
			this._Policy = request.CachePolicy;
			this._Response = null;
			this._ResponseCount = 0;
			this._ValidationStatus = CacheValidationStatus.DoNotUseCache;
			this._CacheFreshnessStatus = CacheFreshnessStatus.Undefined;
			this._CacheStream = null;
			this._CacheStreamOffset = 0L;
			this._CacheStreamLength = 0L;
			if (!uri.Equals(this._Uri))
			{
				this._CacheKey = uri.GetParts(UriComponents.AbsoluteUri, UriFormat.Unescaped);
			}
			this._Uri = uri;
		}

		// Token: 0x060029D5 RID: 10709 RVA: 0x000AF3AF File Offset: 0x000AE3AF
		internal void FetchCacheEntry(RequestCacheEntry fetchEntry)
		{
			this._CacheEntry = fetchEntry;
		}

		// Token: 0x060029D6 RID: 10710 RVA: 0x000AF3B8 File Offset: 0x000AE3B8
		internal void FetchResponse(WebResponse fetchResponse)
		{
			this._ResponseCount++;
			this._Response = fetchResponse;
		}

		// Token: 0x060029D7 RID: 10711 RVA: 0x000AF3CF File Offset: 0x000AE3CF
		internal void SetFreshnessStatus(CacheFreshnessStatus status)
		{
			this._CacheFreshnessStatus = status;
		}

		// Token: 0x060029D8 RID: 10712 RVA: 0x000AF3D8 File Offset: 0x000AE3D8
		internal void SetValidationStatus(CacheValidationStatus status)
		{
			this._ValidationStatus = status;
		}

		// Token: 0x04002890 RID: 10384
		internal WebRequest _Request;

		// Token: 0x04002891 RID: 10385
		internal WebResponse _Response;

		// Token: 0x04002892 RID: 10386
		internal Stream _CacheStream;

		// Token: 0x04002893 RID: 10387
		private RequestCachePolicy _Policy;

		// Token: 0x04002894 RID: 10388
		private Uri _Uri;

		// Token: 0x04002895 RID: 10389
		private string _CacheKey;

		// Token: 0x04002896 RID: 10390
		private RequestCacheEntry _CacheEntry;

		// Token: 0x04002897 RID: 10391
		private int _ResponseCount;

		// Token: 0x04002898 RID: 10392
		private CacheValidationStatus _ValidationStatus;

		// Token: 0x04002899 RID: 10393
		private CacheFreshnessStatus _CacheFreshnessStatus;

		// Token: 0x0400289A RID: 10394
		private long _CacheStreamOffset;

		// Token: 0x0400289B RID: 10395
		private long _CacheStreamLength;

		// Token: 0x0400289C RID: 10396
		private bool _StrictCacheErrors;

		// Token: 0x0400289D RID: 10397
		private TimeSpan _UnspecifiedMaxAge;
	}
}
