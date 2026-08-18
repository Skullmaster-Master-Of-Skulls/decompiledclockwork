using System;
using System.IO;

namespace System.Net.Cache
{
	// Token: 0x02000318 RID: 792
	internal abstract class RequestCacheValidator
	{
		// Token: 0x06001C2C RID: 7212
		internal abstract RequestCacheValidator CreateValidator();

		// Token: 0x06001C2D RID: 7213 RVA: 0x00086209 File Offset: 0x00084409
		protected RequestCacheValidator(bool strictCacheErrors, TimeSpan unspecifiedMaxAge)
		{
			this._StrictCacheErrors = strictCacheErrors;
			this._UnspecifiedMaxAge = unspecifiedMaxAge;
			this._ValidationStatus = CacheValidationStatus.DoNotUseCache;
			this._CacheFreshnessStatus = CacheFreshnessStatus.Undefined;
		}

		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x06001C2E RID: 7214 RVA: 0x0008622D File Offset: 0x0008442D
		internal bool StrictCacheErrors
		{
			get
			{
				return this._StrictCacheErrors;
			}
		}

		// Token: 0x170006EB RID: 1771
		// (get) Token: 0x06001C2F RID: 7215 RVA: 0x00086235 File Offset: 0x00084435
		internal TimeSpan UnspecifiedMaxAge
		{
			get
			{
				return this._UnspecifiedMaxAge;
			}
		}

		// Token: 0x170006EC RID: 1772
		// (get) Token: 0x06001C30 RID: 7216 RVA: 0x0008623D File Offset: 0x0008443D
		protected internal Uri Uri
		{
			get
			{
				return this._Uri;
			}
		}

		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x06001C31 RID: 7217 RVA: 0x00086245 File Offset: 0x00084445
		protected internal WebRequest Request
		{
			get
			{
				return this._Request;
			}
		}

		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x06001C32 RID: 7218 RVA: 0x0008624D File Offset: 0x0008444D
		protected internal WebResponse Response
		{
			get
			{
				return this._Response;
			}
		}

		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x06001C33 RID: 7219 RVA: 0x00086255 File Offset: 0x00084455
		protected internal RequestCachePolicy Policy
		{
			get
			{
				return this._Policy;
			}
		}

		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x06001C34 RID: 7220 RVA: 0x0008625D File Offset: 0x0008445D
		protected internal int ResponseCount
		{
			get
			{
				return this._ResponseCount;
			}
		}

		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x06001C35 RID: 7221 RVA: 0x00086265 File Offset: 0x00084465
		protected internal CacheValidationStatus ValidationStatus
		{
			get
			{
				return this._ValidationStatus;
			}
		}

		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x06001C36 RID: 7222 RVA: 0x0008626D File Offset: 0x0008446D
		protected internal CacheFreshnessStatus CacheFreshnessStatus
		{
			get
			{
				return this._CacheFreshnessStatus;
			}
		}

		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x06001C37 RID: 7223 RVA: 0x00086275 File Offset: 0x00084475
		protected internal RequestCacheEntry CacheEntry
		{
			get
			{
				return this._CacheEntry;
			}
		}

		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x06001C38 RID: 7224 RVA: 0x0008627D File Offset: 0x0008447D
		// (set) Token: 0x06001C39 RID: 7225 RVA: 0x00086285 File Offset: 0x00084485
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

		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x06001C3A RID: 7226 RVA: 0x0008628E File Offset: 0x0008448E
		// (set) Token: 0x06001C3B RID: 7227 RVA: 0x00086296 File Offset: 0x00084496
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

		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x06001C3C RID: 7228 RVA: 0x0008629F File Offset: 0x0008449F
		// (set) Token: 0x06001C3D RID: 7229 RVA: 0x000862A7 File Offset: 0x000844A7
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

		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x06001C3E RID: 7230 RVA: 0x000862B0 File Offset: 0x000844B0
		protected internal string CacheKey
		{
			get
			{
				return this._CacheKey;
			}
		}

		// Token: 0x06001C3F RID: 7231
		protected internal abstract CacheValidationStatus ValidateRequest();

		// Token: 0x06001C40 RID: 7232
		protected internal abstract CacheFreshnessStatus ValidateFreshness();

		// Token: 0x06001C41 RID: 7233
		protected internal abstract CacheValidationStatus ValidateCache();

		// Token: 0x06001C42 RID: 7234
		protected internal abstract CacheValidationStatus ValidateResponse();

		// Token: 0x06001C43 RID: 7235
		protected internal abstract CacheValidationStatus RevalidateCache();

		// Token: 0x06001C44 RID: 7236
		protected internal abstract CacheValidationStatus UpdateCache();

		// Token: 0x06001C45 RID: 7237 RVA: 0x000862B8 File Offset: 0x000844B8
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

		// Token: 0x06001C46 RID: 7238 RVA: 0x00086320 File Offset: 0x00084520
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

		// Token: 0x06001C47 RID: 7239 RVA: 0x00086397 File Offset: 0x00084597
		internal void FetchCacheEntry(RequestCacheEntry fetchEntry)
		{
			this._CacheEntry = fetchEntry;
		}

		// Token: 0x06001C48 RID: 7240 RVA: 0x000863A0 File Offset: 0x000845A0
		internal void FetchResponse(WebResponse fetchResponse)
		{
			this._ResponseCount++;
			this._Response = fetchResponse;
		}

		// Token: 0x06001C49 RID: 7241 RVA: 0x000863B7 File Offset: 0x000845B7
		internal void SetFreshnessStatus(CacheFreshnessStatus status)
		{
			this._CacheFreshnessStatus = status;
		}

		// Token: 0x06001C4A RID: 7242 RVA: 0x000863C0 File Offset: 0x000845C0
		internal void SetValidationStatus(CacheValidationStatus status)
		{
			this._ValidationStatus = status;
		}

		// Token: 0x04001B88 RID: 7048
		internal WebRequest _Request;

		// Token: 0x04001B89 RID: 7049
		internal WebResponse _Response;

		// Token: 0x04001B8A RID: 7050
		internal Stream _CacheStream;

		// Token: 0x04001B8B RID: 7051
		private RequestCachePolicy _Policy;

		// Token: 0x04001B8C RID: 7052
		private Uri _Uri;

		// Token: 0x04001B8D RID: 7053
		private string _CacheKey;

		// Token: 0x04001B8E RID: 7054
		private RequestCacheEntry _CacheEntry;

		// Token: 0x04001B8F RID: 7055
		private int _ResponseCount;

		// Token: 0x04001B90 RID: 7056
		private CacheValidationStatus _ValidationStatus;

		// Token: 0x04001B91 RID: 7057
		private CacheFreshnessStatus _CacheFreshnessStatus;

		// Token: 0x04001B92 RID: 7058
		private long _CacheStreamOffset;

		// Token: 0x04001B93 RID: 7059
		private long _CacheStreamLength;

		// Token: 0x04001B94 RID: 7060
		private bool _StrictCacheErrors;

		// Token: 0x04001B95 RID: 7061
		private TimeSpan _UnspecifiedMaxAge;
	}
}
