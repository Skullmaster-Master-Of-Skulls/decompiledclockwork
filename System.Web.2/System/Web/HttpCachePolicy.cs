using System;
using System.Collections;
using System.Globalization;
using System.Text;
using System.Web.Caching;
using System.Web.Configuration;
using System.Web.Security.Cryptography;
using System.Web.Util;

namespace System.Web
{
	// Token: 0x0200008A RID: 138
	public sealed class HttpCachePolicy
	{
		// Token: 0x0600084B RID: 2123 RVA: 0x000119E4 File Offset: 0x0000FBE4
		internal HttpCachePolicy()
		{
			this._varyByContentEncodings = new HttpCacheVaryByContentEncodings();
			this._varyByHeaders = new HttpCacheVaryByHeaders();
			this._varyByParams = new HttpCacheVaryByParams();
			this.Reset();
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x00011A14 File Offset: 0x0000FC14
		internal void Reset()
		{
			this._varyByContentEncodings.Reset();
			this._varyByHeaders.Reset();
			this._varyByParams.Reset();
			this._isModified = false;
			this._hasSetCookieHeader = false;
			this._noServerCaching = false;
			this._cacheExtension = null;
			this._noTransforms = false;
			this._ignoreRangeRequests = false;
			this._varyByCustom = null;
			this._cacheability = (HttpCacheability)6;
			this._noStore = false;
			this._privateFields = null;
			this._noCacheFields = null;
			this._utcExpires = DateTime.MinValue;
			this._isExpiresSet = false;
			this._maxAge = TimeSpan.Zero;
			this._isMaxAgeSet = false;
			this._proxyMaxAge = TimeSpan.Zero;
			this._isProxyMaxAgeSet = false;
			this._slidingExpiration = -1;
			this._slidingDelta = TimeSpan.Zero;
			this._utcTimestampCreated = DateTime.MinValue;
			this._utcTimestampRequest = DateTime.MinValue;
			this._validUntilExpires = -1;
			this._allowInHistory = -1;
			this._revalidation = HttpCacheRevalidation.None;
			this._utcLastModified = DateTime.MinValue;
			this._isLastModifiedSet = false;
			this._etag = null;
			this._generateLastModifiedFromFiles = false;
			this._generateEtagFromFiles = false;
			this._validationCallbackInfo = null;
			this._useCachedHeaders = false;
			this._headerCacheControl = null;
			this._headerPragma = null;
			this._headerExpires = null;
			this._headerLastModified = null;
			this._headerEtag = null;
			this._headerVaryBy = null;
			this._noMaxAgeInCacheControl = false;
			this._hasUserProvidedDependencies = false;
			this._omitVaryStar = -1;
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x00011B78 File Offset: 0x0000FD78
		internal void ResetFromHttpCachePolicySettings(HttpCachePolicySettings settings, DateTime utcTimestampRequest)
		{
			this._utcTimestampRequest = utcTimestampRequest;
			this._varyByContentEncodings.SetContentEncodings(settings.VaryByContentEncodings);
			this._varyByHeaders.SetHeaders(settings.VaryByHeaders);
			this._varyByParams.SetParams(settings.VaryByParams);
			this._isModified = settings.IsModified;
			this._hasSetCookieHeader = settings.hasSetCookieHeader;
			this._noServerCaching = settings.NoServerCaching;
			this._cacheExtension = settings.CacheExtension;
			this._noTransforms = settings.NoTransforms;
			this._ignoreRangeRequests = settings.IgnoreRangeRequests;
			this._varyByCustom = settings.VaryByCustom;
			this._cacheability = settings.CacheabilityInternal;
			this._noStore = settings.NoStore;
			this._utcExpires = settings.UtcExpires;
			this._isExpiresSet = settings.IsExpiresSet;
			this._maxAge = settings.MaxAge;
			this._isMaxAgeSet = settings.IsMaxAgeSet;
			this._proxyMaxAge = settings.ProxyMaxAge;
			this._isProxyMaxAgeSet = settings.IsProxyMaxAgeSet;
			this._slidingExpiration = settings.SlidingExpirationInternal;
			this._slidingDelta = settings.SlidingDelta;
			this._utcTimestampCreated = settings.UtcTimestampCreated;
			this._validUntilExpires = settings.ValidUntilExpiresInternal;
			this._allowInHistory = settings.AllowInHistoryInternal;
			this._revalidation = settings.Revalidation;
			this._utcLastModified = settings.UtcLastModified;
			this._isLastModifiedSet = settings.IsLastModifiedSet;
			this._etag = settings.ETag;
			this._generateLastModifiedFromFiles = settings.GenerateLastModifiedFromFiles;
			this._generateEtagFromFiles = settings.GenerateEtagFromFiles;
			this._omitVaryStar = settings.OmitVaryStarInternal;
			this._hasUserProvidedDependencies = settings.HasUserProvidedDependencies;
			this._useCachedHeaders = true;
			this._headerCacheControl = settings.HeaderCacheControl;
			this._headerPragma = settings.HeaderPragma;
			this._headerExpires = settings.HeaderExpires;
			this._headerLastModified = settings.HeaderLastModified;
			this._headerEtag = settings.HeaderEtag;
			this._headerVaryBy = settings.HeaderVaryBy;
			this._noMaxAgeInCacheControl = false;
			string[] array = settings.PrivateFields;
			if (array != null)
			{
				this._privateFields = new HttpDictionary();
				int i = 0;
				int num = array.Length;
				while (i < num)
				{
					this._privateFields.SetValue(array[i], array[i]);
					i++;
				}
			}
			array = settings.NoCacheFields;
			if (array != null)
			{
				this._noCacheFields = new HttpDictionary();
				int i = 0;
				int num = array.Length;
				while (i < num)
				{
					this._noCacheFields.SetValue(array[i], array[i]);
					i++;
				}
			}
			if (settings.ValidationCallbackInfo != null)
			{
				this._validationCallbackInfo = new ArrayList();
				int i = 0;
				int num = settings.ValidationCallbackInfo.Length;
				while (i < num)
				{
					this._validationCallbackInfo.Add(new ValidationCallbackInfo(settings.ValidationCallbackInfo[i].handler, settings.ValidationCallbackInfo[i].data));
					i++;
				}
			}
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x00011E24 File Offset: 0x00010024
		public bool IsModified()
		{
			return this._isModified || this._varyByContentEncodings.IsModified() || this._varyByHeaders.IsModified() || this._varyByParams.IsModified();
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x00011E55 File Offset: 0x00010055
		private void Dirtied()
		{
			this._isModified = true;
			this._useCachedHeaders = false;
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x00011E65 File Offset: 0x00010065
		internal static void AppendValueToHeader(StringBuilder s, string value)
		{
			if (!string.IsNullOrEmpty(value))
			{
				if (s.Length > 0)
				{
					s.Append(", ");
				}
				s.Append(value);
			}
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x00011E8C File Offset: 0x0001008C
		private DateTime UpdateLastModifiedTimeFromDependency(CacheDependency dep)
		{
			DateTime dateTime = dep.UtcLastModified;
			if (dateTime < this._utcLastModified)
			{
				dateTime = this._utcLastModified;
			}
			DateTime utcNow = DateTime.UtcNow;
			if (dateTime > utcNow)
			{
				dateTime = utcNow;
			}
			return dateTime;
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x00011EC8 File Offset: 0x000100C8
		private void UpdateFromDependencies(HttpResponse response)
		{
			CacheDependency cacheDependency = null;
			if (this._etag == null && this._generateEtagFromFiles)
			{
				cacheDependency = response.CreateCacheDependencyForResponse();
				if (cacheDependency == null)
				{
					return;
				}
				string uniqueID = cacheDependency.GetUniqueID();
				if (uniqueID == null)
				{
					throw new HttpException(SR.GetString("No_UniqueId_Cache_Dependency"));
				}
				DateTime dateTime = this.UpdateLastModifiedTimeFromDependency(cacheDependency);
				StringBuilder stringBuilder = new StringBuilder(256);
				stringBuilder.Append(HttpRuntime.AppDomainIdInternal);
				stringBuilder.Append(uniqueID);
				stringBuilder.Append("+LM");
				stringBuilder.Append(dateTime.Ticks.ToString(CultureInfo.InvariantCulture));
				this._etag = Convert.ToBase64String(CryptoUtil.ComputeSHA256Hash(Encoding.UTF8.GetBytes(stringBuilder.ToString())));
				this._etag = "\"" + this._etag + "\"";
			}
			if (this._generateLastModifiedFromFiles)
			{
				if (cacheDependency == null)
				{
					cacheDependency = response.CreateCacheDependencyForResponse();
					if (cacheDependency == null)
					{
						return;
					}
				}
				DateTime utcDate = this.UpdateLastModifiedTimeFromDependency(cacheDependency);
				this.UtcSetLastModified(utcDate);
			}
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x00011FC4 File Offset: 0x000101C4
		private void UpdateCachedHeaders(HttpResponse response)
		{
			if (this._useCachedHeaders)
			{
				return;
			}
			if (this._utcTimestampCreated == DateTime.MinValue)
			{
				this._utcTimestampCreated = response.Context.UtcTimestamp;
			}
			this._utcTimestampRequest = response.Context.UtcTimestamp;
			if (this._slidingExpiration != 1)
			{
				this._slidingDelta = TimeSpan.Zero;
			}
			else if (this._isMaxAgeSet)
			{
				this._slidingDelta = this._maxAge;
			}
			else if (this._isExpiresSet)
			{
				this._slidingDelta = this._utcExpires - this._utcTimestampCreated;
			}
			else
			{
				this._slidingDelta = TimeSpan.Zero;
			}
			this._headerCacheControl = null;
			this._headerPragma = null;
			this._headerExpires = null;
			this._headerLastModified = null;
			this._headerEtag = null;
			this._headerVaryBy = null;
			this.UpdateFromDependencies(response);
			StringBuilder stringBuilder = new StringBuilder();
			HttpCacheability httpCacheability;
			if (this._cacheability == (HttpCacheability)6)
			{
				httpCacheability = HttpCacheability.Private;
			}
			else
			{
				httpCacheability = this._cacheability;
			}
			HttpCachePolicy.AppendValueToHeader(stringBuilder, HttpCachePolicy.s_cacheabilityTokens[(int)httpCacheability]);
			if (httpCacheability == HttpCacheability.Public && this._privateFields != null)
			{
				HttpCachePolicy.AppendValueToHeader(stringBuilder, "private=\"");
				stringBuilder.Append(this._privateFields.GetKey(0));
				int i = 1;
				int size = this._privateFields.Size;
				while (i < size)
				{
					HttpCachePolicy.AppendValueToHeader(stringBuilder, this._privateFields.GetKey(i));
					i++;
				}
				stringBuilder.Append('"');
			}
			if (httpCacheability != HttpCacheability.NoCache && httpCacheability != HttpCacheability.Server && this._noCacheFields != null)
			{
				HttpCachePolicy.AppendValueToHeader(stringBuilder, "no-cache=\"");
				stringBuilder.Append(this._noCacheFields.GetKey(0));
				int i = 1;
				int size = this._noCacheFields.Size;
				while (i < size)
				{
					HttpCachePolicy.AppendValueToHeader(stringBuilder, this._noCacheFields.GetKey(i));
					i++;
				}
				stringBuilder.Append('"');
			}
			if (this._noStore)
			{
				HttpCachePolicy.AppendValueToHeader(stringBuilder, "no-store");
			}
			HttpCachePolicy.AppendValueToHeader(stringBuilder, HttpCachePolicy.s_revalidationTokens[(int)this._revalidation]);
			if (this._noTransforms)
			{
				HttpCachePolicy.AppendValueToHeader(stringBuilder, "no-transform");
			}
			if (this._cacheExtension != null)
			{
				HttpCachePolicy.AppendValueToHeader(stringBuilder, this._cacheExtension);
			}
			if (this._slidingExpiration == 1 && httpCacheability != HttpCacheability.NoCache && httpCacheability != HttpCacheability.Server)
			{
				if (this._isMaxAgeSet && !this._noMaxAgeInCacheControl)
				{
					HttpCachePolicy.AppendValueToHeader(stringBuilder, "max-age=" + ((long)this._maxAge.TotalSeconds).ToString(CultureInfo.InvariantCulture));
				}
				if (this._isProxyMaxAgeSet && !this._noMaxAgeInCacheControl)
				{
					HttpCachePolicy.AppendValueToHeader(stringBuilder, "s-maxage=" + ((long)this._proxyMaxAge.TotalSeconds).ToString(CultureInfo.InvariantCulture));
				}
			}
			if (stringBuilder.Length > 0)
			{
				this._headerCacheControl = new HttpResponseHeader(0, stringBuilder.ToString());
			}
			if (httpCacheability == HttpCacheability.NoCache || httpCacheability == HttpCacheability.Server)
			{
				if (HttpCachePolicy.s_headerPragmaNoCache == null)
				{
					HttpCachePolicy.s_headerPragmaNoCache = new HttpResponseHeader(4, "no-cache");
				}
				this._headerPragma = HttpCachePolicy.s_headerPragmaNoCache;
				if (this._allowInHistory != 1)
				{
					if (HttpCachePolicy.s_headerExpiresMinus1 == null)
					{
						HttpCachePolicy.s_headerExpiresMinus1 = new HttpResponseHeader(18, "-1");
					}
					this._headerExpires = HttpCachePolicy.s_headerExpiresMinus1;
				}
			}
			else
			{
				if (this._isExpiresSet && this._slidingExpiration != 1)
				{
					string value = HttpUtility.FormatHttpDateTimeUtc(this._utcExpires);
					this._headerExpires = new HttpResponseHeader(18, value);
				}
				if (this._isLastModifiedSet)
				{
					string value2 = HttpUtility.FormatHttpDateTimeUtc(this._utcLastModified);
					this._headerLastModified = new HttpResponseHeader(19, value2);
				}
				if (httpCacheability != HttpCacheability.Private)
				{
					if (this._etag != null)
					{
						this._headerEtag = new HttpResponseHeader(22, this._etag);
					}
					string text = null;
					bool flag;
					if (this._omitVaryStar != -1)
					{
						flag = (this._omitVaryStar == 1);
					}
					else
					{
						RuntimeConfig lkgconfig = RuntimeConfig.GetLKGConfig(response.Context);
						OutputCacheSection outputCache = lkgconfig.OutputCache;
						flag = (outputCache != null && outputCache.OmitVaryStar);
					}
					if (!flag && (this._varyByCustom != null || (this._varyByParams.IsModified() && !this._varyByParams.IgnoreParams)))
					{
						text = "*";
					}
					if (text == null)
					{
						text = this._varyByHeaders.ToHeaderString();
					}
					if (text != null)
					{
						this._headerVaryBy = new HttpResponseHeader(28, text);
					}
				}
			}
			this._useCachedHeaders = true;
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x000123D8 File Offset: 0x000105D8
		internal void GetHeaders(ArrayList headers, HttpResponse response)
		{
			this.UpdateCachedHeaders(response);
			HttpResponseHeader httpResponseHeader = this._headerExpires;
			HttpResponseHeader httpResponseHeader2 = this._headerCacheControl;
			if (this._cacheability != HttpCacheability.NoCache && this._cacheability != HttpCacheability.Server)
			{
				if (this._slidingExpiration == 1)
				{
					if (this._isExpiresSet)
					{
						DateTime dt = this._utcTimestampRequest + this._slidingDelta;
						string value = HttpUtility.FormatHttpDateTimeUtc(dt);
						httpResponseHeader = new HttpResponseHeader(18, value);
					}
				}
				else if (this._isMaxAgeSet || this._isProxyMaxAgeSet)
				{
					StringBuilder stringBuilder;
					if (httpResponseHeader2 != null)
					{
						stringBuilder = new StringBuilder(httpResponseHeader2.Value);
					}
					else
					{
						stringBuilder = new StringBuilder();
					}
					TimeSpan t = this._utcTimestampRequest - this._utcTimestampCreated;
					if (this._isMaxAgeSet)
					{
						TimeSpan t2 = this._maxAge - t;
						if (t2 < TimeSpan.Zero)
						{
							t2 = TimeSpan.Zero;
						}
						if (!this._noMaxAgeInCacheControl)
						{
							HttpCachePolicy.AppendValueToHeader(stringBuilder, "max-age=" + ((long)t2.TotalSeconds).ToString(CultureInfo.InvariantCulture));
						}
					}
					if (this._isProxyMaxAgeSet)
					{
						TimeSpan t3 = this._proxyMaxAge - t;
						if (t3 < TimeSpan.Zero)
						{
							t3 = TimeSpan.Zero;
						}
						if (!this._noMaxAgeInCacheControl)
						{
							HttpCachePolicy.AppendValueToHeader(stringBuilder, "s-maxage=" + ((long)t3.TotalSeconds).ToString(CultureInfo.InvariantCulture));
						}
					}
					httpResponseHeader2 = new HttpResponseHeader(0, stringBuilder.ToString());
				}
			}
			if (httpResponseHeader2 != null)
			{
				headers.Add(httpResponseHeader2);
			}
			if (this._headerPragma != null)
			{
				headers.Add(this._headerPragma);
			}
			if (httpResponseHeader != null)
			{
				headers.Add(httpResponseHeader);
			}
			if (this._headerLastModified != null)
			{
				headers.Add(this._headerLastModified);
			}
			if (this._headerEtag != null)
			{
				headers.Add(this._headerEtag);
			}
			if (this._headerVaryBy != null)
			{
				headers.Add(this._headerVaryBy);
			}
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x000125BC File Offset: 0x000107BC
		internal HttpCachePolicySettings GetCurrentSettings(HttpResponse response)
		{
			this.UpdateCachedHeaders(response);
			string[] contentEncodings = this._varyByContentEncodings.GetContentEncodings();
			string[] headers = this._varyByHeaders.GetHeaders();
			string[] @params = this._varyByParams.GetParams();
			string[] privateFields;
			if (this._privateFields != null)
			{
				privateFields = this._privateFields.GetAllKeys();
			}
			else
			{
				privateFields = null;
			}
			string[] noCacheFields;
			if (this._noCacheFields != null)
			{
				noCacheFields = this._noCacheFields.GetAllKeys();
			}
			else
			{
				noCacheFields = null;
			}
			ValidationCallbackInfo[] array;
			if (this._validationCallbackInfo != null)
			{
				array = new ValidationCallbackInfo[this._validationCallbackInfo.Count];
				this._validationCallbackInfo.CopyTo(0, array, 0, this._validationCallbackInfo.Count);
			}
			else
			{
				array = null;
			}
			return new HttpCachePolicySettings(this._isModified, array, this._hasSetCookieHeader, this._noServerCaching, this._cacheExtension, this._noTransforms, this._ignoreRangeRequests, contentEncodings, headers, @params, this._varyByCustom, this._cacheability, this._noStore, privateFields, noCacheFields, this._utcExpires, this._isExpiresSet, this._maxAge, this._isMaxAgeSet, this._proxyMaxAge, this._isProxyMaxAgeSet, this._slidingExpiration, this._slidingDelta, this._utcTimestampCreated, this._validUntilExpires, this._allowInHistory, this._revalidation, this._utcLastModified, this._isLastModifiedSet, this._etag, this._generateLastModifiedFromFiles, this._generateEtagFromFiles, this._omitVaryStar, this._headerCacheControl, this._headerPragma, this._headerExpires, this._headerLastModified, this._headerEtag, this._headerVaryBy, this._hasUserProvidedDependencies);
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x00012738 File Offset: 0x00010938
		internal bool HasValidationPolicy()
		{
			return this._generateLastModifiedFromFiles || this._generateEtagFromFiles || this._validationCallbackInfo != null || (this._validUntilExpires == 1 && this._slidingExpiration != 1);
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x0001276B File Offset: 0x0001096B
		internal bool HasExpirationPolicy()
		{
			return this._slidingExpiration != 1 && (this._isExpiresSet || this._isMaxAgeSet);
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x00012788 File Offset: 0x00010988
		internal bool IsKernelCacheable(HttpRequest request, bool enableKernelCacheForVaryByStar)
		{
			return this._cacheability == HttpCacheability.Public && !this._hasUserProvidedDependencies && !this._hasSetCookieHeader && !this._noServerCaching && this.HasExpirationPolicy() && this._cacheExtension == null && !this._varyByContentEncodings.IsModified() && !this._varyByHeaders.IsModified() && (!this._varyByParams.IsModified() || this._varyByParams.IgnoreParams || (this._varyByParams.IsVaryByStar && enableKernelCacheForVaryByStar)) && !this._noStore && this._varyByCustom == null && this._privateFields == null && this._noCacheFields == null && this._validationCallbackInfo == null && request != null && request.HttpVerb == HttpVerb.GET;
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06000859 RID: 2137 RVA: 0x00012850 File Offset: 0x00010A50
		internal bool IsVaryByStar
		{
			get
			{
				return this._varyByParams.IsVaryByStar;
			}
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x00012860 File Offset: 0x00010A60
		internal DateTime UtcGetAbsoluteExpiration()
		{
			DateTime result = Cache.NoAbsoluteExpiration;
			if (this._slidingExpiration != 1)
			{
				if (this._isMaxAgeSet)
				{
					result = this._utcTimestampCreated + this._maxAge;
				}
				else if (this._isExpiresSet)
				{
					result = this._utcExpires;
				}
			}
			return result;
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x000128A8 File Offset: 0x00010AA8
		internal IEnumerable GetValidationCallbacks()
		{
			if (this._validationCallbackInfo == null)
			{
				return new ArrayList();
			}
			return this._validationCallbackInfo;
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x000128BE File Offset: 0x00010ABE
		public void SetNoServerCaching()
		{
			this.Dirtied();
			this._noServerCaching = true;
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x000128CD File Offset: 0x00010ACD
		public bool GetNoServerCaching()
		{
			return this._noServerCaching;
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x000128D5 File Offset: 0x00010AD5
		internal void SetHasSetCookieHeader()
		{
			this.Dirtied();
			this._hasSetCookieHeader = true;
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x000128E4 File Offset: 0x00010AE4
		public void SetVaryByCustom(string custom)
		{
			if (custom == null)
			{
				throw new ArgumentNullException("custom");
			}
			if (this._varyByCustom != null)
			{
				throw new InvalidOperationException(SR.GetString("VaryByCustom_already_set"));
			}
			this.Dirtied();
			this._varyByCustom = custom;
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x00012919 File Offset: 0x00010B19
		public string GetVaryByCustom()
		{
			return this._varyByCustom;
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x00012921 File Offset: 0x00010B21
		public void AppendCacheExtension(string extension)
		{
			if (extension == null)
			{
				throw new ArgumentNullException("extension");
			}
			this.Dirtied();
			if (this._cacheExtension == null)
			{
				this._cacheExtension = extension;
				return;
			}
			this._cacheExtension = this._cacheExtension + ", " + extension;
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x0001295E File Offset: 0x00010B5E
		public string GetCacheExtensions()
		{
			return this._cacheExtension;
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x00012966 File Offset: 0x00010B66
		public void SetNoTransforms()
		{
			this.Dirtied();
			this._noTransforms = true;
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x00012975 File Offset: 0x00010B75
		public bool GetNoTransforms()
		{
			return this._noTransforms;
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x0001297D File Offset: 0x00010B7D
		internal void SetIgnoreRangeRequests()
		{
			this.Dirtied();
			this._ignoreRangeRequests = true;
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x0001298C File Offset: 0x00010B8C
		public bool GetIgnoreRangeRequests()
		{
			return this._ignoreRangeRequests;
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06000867 RID: 2151 RVA: 0x00012994 File Offset: 0x00010B94
		public HttpCacheVaryByContentEncodings VaryByContentEncodings
		{
			get
			{
				return this._varyByContentEncodings;
			}
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06000868 RID: 2152 RVA: 0x0001299C File Offset: 0x00010B9C
		public HttpCacheVaryByHeaders VaryByHeaders
		{
			get
			{
				return this._varyByHeaders;
			}
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06000869 RID: 2153 RVA: 0x000129A4 File Offset: 0x00010BA4
		public HttpCacheVaryByParams VaryByParams
		{
			get
			{
				return this._varyByParams;
			}
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x000129AC File Offset: 0x00010BAC
		public void SetCacheability(HttpCacheability cacheability)
		{
			if (cacheability < HttpCacheability.NoCache || HttpCacheability.ServerAndPrivate < cacheability)
			{
				throw new ArgumentOutOfRangeException("cacheability");
			}
			if (HttpCachePolicy.s_cacheabilityValues[(int)cacheability] < HttpCachePolicy.s_cacheabilityValues[(int)this._cacheability])
			{
				this.Dirtied();
				this._cacheability = cacheability;
			}
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x000129E3 File Offset: 0x00010BE3
		public HttpCacheability GetCacheability()
		{
			return this._cacheability;
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x000129EC File Offset: 0x00010BEC
		public void SetCacheability(HttpCacheability cacheability, string field)
		{
			if (field == null)
			{
				throw new ArgumentNullException("field");
			}
			if (cacheability != HttpCacheability.NoCache)
			{
				if (cacheability != HttpCacheability.Private)
				{
					throw new ArgumentException(SR.GetString("Cacheability_for_field_must_be_private_or_nocache"), "cacheability");
				}
				if (this._privateFields == null)
				{
					this._privateFields = new HttpDictionary();
				}
				this._privateFields.SetValue(field, field);
			}
			else
			{
				if (this._noCacheFields == null)
				{
					this._noCacheFields = new HttpDictionary();
				}
				this._noCacheFields.SetValue(field, field);
			}
			this.Dirtied();
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x00012A6E File Offset: 0x00010C6E
		public void SetNoStore()
		{
			this.Dirtied();
			this._noStore = true;
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x00012A7D File Offset: 0x00010C7D
		internal void SetDependencies(bool hasUserProvidedDependencies)
		{
			this.Dirtied();
			this._hasUserProvidedDependencies = hasUserProvidedDependencies;
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x00012A8C File Offset: 0x00010C8C
		public bool GetNoStore()
		{
			return this._noStore;
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x00012A94 File Offset: 0x00010C94
		public void SetExpires(DateTime date)
		{
			DateTime dateTime = DateTimeUtil.ConvertToUniversalTime(date);
			DateTime utcNow = DateTime.UtcNow;
			if (dateTime - utcNow > HttpCachePolicy.s_oneYear)
			{
				dateTime = utcNow + HttpCachePolicy.s_oneYear;
			}
			if (!this._isExpiresSet || dateTime < this._utcExpires)
			{
				this.Dirtied();
				this._utcExpires = dateTime;
				this._isExpiresSet = true;
			}
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x00012AF7 File Offset: 0x00010CF7
		public DateTime GetExpires()
		{
			return this._utcExpires;
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x00012B00 File Offset: 0x00010D00
		public void SetMaxAge(TimeSpan delta)
		{
			if (delta < TimeSpan.Zero)
			{
				throw new ArgumentOutOfRangeException("delta");
			}
			if (HttpCachePolicy.s_oneYear < delta)
			{
				delta = HttpCachePolicy.s_oneYear;
			}
			if (!this._isMaxAgeSet || delta < this._maxAge)
			{
				this.Dirtied();
				this._maxAge = delta;
				this._isMaxAgeSet = true;
			}
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x00012B63 File Offset: 0x00010D63
		public TimeSpan GetMaxAge()
		{
			return this._maxAge;
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x00012B6B File Offset: 0x00010D6B
		internal void SetNoMaxAgeInCacheControl()
		{
			this._noMaxAgeInCacheControl = true;
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x00012B74 File Offset: 0x00010D74
		public void SetProxyMaxAge(TimeSpan delta)
		{
			if (delta < TimeSpan.Zero)
			{
				throw new ArgumentOutOfRangeException("delta");
			}
			if (!this._isProxyMaxAgeSet || delta < this._proxyMaxAge)
			{
				this.Dirtied();
				this._proxyMaxAge = delta;
				this._isProxyMaxAgeSet = true;
			}
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x00012BC3 File Offset: 0x00010DC3
		public TimeSpan GetProxyMaxAge()
		{
			return this._proxyMaxAge;
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x00012BCB File Offset: 0x00010DCB
		public void SetSlidingExpiration(bool slide)
		{
			if (this._slidingExpiration == -1 || this._slidingExpiration == 1)
			{
				this.Dirtied();
				this._slidingExpiration = (slide ? 1 : 0);
			}
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x00012BF2 File Offset: 0x00010DF2
		public bool HasSlidingExpiration()
		{
			return this._slidingExpiration == 1;
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x00012BFD File Offset: 0x00010DFD
		public void SetValidUntilExpires(bool validUntilExpires)
		{
			if (this._validUntilExpires == -1 || this._validUntilExpires == 1)
			{
				this.Dirtied();
				this._validUntilExpires = (validUntilExpires ? 1 : 0);
			}
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x00012C24 File Offset: 0x00010E24
		public bool IsValidUntilExpires()
		{
			return this._validUntilExpires == 1;
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x00012C2F File Offset: 0x00010E2F
		public void SetAllowResponseInBrowserHistory(bool allow)
		{
			if (this._allowInHistory == -1 || this._allowInHistory == 1)
			{
				this.Dirtied();
				this._allowInHistory = (allow ? 1 : 0);
			}
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x00012C56 File Offset: 0x00010E56
		public void SetRevalidation(HttpCacheRevalidation revalidation)
		{
			if (revalidation < HttpCacheRevalidation.AllCaches || HttpCacheRevalidation.None < revalidation)
			{
				throw new ArgumentOutOfRangeException("revalidation");
			}
			if (revalidation < this._revalidation)
			{
				this.Dirtied();
				this._revalidation = revalidation;
			}
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x00012C81 File Offset: 0x00010E81
		public HttpCacheRevalidation GetRevalidation()
		{
			return this._revalidation;
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x00012C8C File Offset: 0x00010E8C
		public void SetETag(string etag)
		{
			if (etag == null)
			{
				throw new ArgumentNullException("etag");
			}
			if (this._etag != null)
			{
				throw new InvalidOperationException(SR.GetString("Etag_already_set"));
			}
			if (this._generateEtagFromFiles)
			{
				throw new InvalidOperationException(SR.GetString("Cant_both_set_and_generate_Etag"));
			}
			this.Dirtied();
			this._etag = etag;
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x00012CE4 File Offset: 0x00010EE4
		public string GetETag()
		{
			return this._etag;
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x00012CEC File Offset: 0x00010EEC
		public void SetLastModified(DateTime date)
		{
			DateTime utcDate = DateTimeUtil.ConvertToUniversalTime(date);
			this.UtcSetLastModified(utcDate);
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x00012D08 File Offset: 0x00010F08
		private void UtcSetLastModified(DateTime utcDate)
		{
			DateTime utcNow = DateTime.UtcNow;
			if (utcDate > utcNow)
			{
				utcDate = utcNow;
			}
			utcDate = new DateTime(utcDate.Ticks - utcDate.Ticks % 10000000L);
			if (!this._isLastModifiedSet || utcDate > this._utcLastModified)
			{
				this.Dirtied();
				this._utcLastModified = utcDate;
				this._isLastModifiedSet = true;
			}
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x00012D6E File Offset: 0x00010F6E
		public DateTime GetUtcLastModified()
		{
			return this._utcLastModified;
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x00012D76 File Offset: 0x00010F76
		public void SetLastModifiedFromFileDependencies()
		{
			this.Dirtied();
			this._generateLastModifiedFromFiles = true;
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x00012D85 File Offset: 0x00010F85
		public bool GetLastModifiedFromFileDependencies()
		{
			return this._generateLastModifiedFromFiles;
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x00012D8D File Offset: 0x00010F8D
		public void SetETagFromFileDependencies()
		{
			if (this._etag != null)
			{
				throw new InvalidOperationException(SR.GetString("Cant_both_set_and_generate_Etag"));
			}
			this.Dirtied();
			this._generateEtagFromFiles = true;
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x00012DB4 File Offset: 0x00010FB4
		public bool GetETagFromFileDependencies()
		{
			return this._generateEtagFromFiles;
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x00012DBC File Offset: 0x00010FBC
		public void SetOmitVaryStar(bool omit)
		{
			this.Dirtied();
			if (this._omitVaryStar == -1 || this._omitVaryStar == 1)
			{
				this.Dirtied();
				this._omitVaryStar = (omit ? 1 : 0);
			}
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x00012DE9 File Offset: 0x00010FE9
		public int GetOmitVaryStar()
		{
			return this._omitVaryStar;
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x00012DF1 File Offset: 0x00010FF1
		public void AddValidationCallback(HttpCacheValidateHandler handler, object data)
		{
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			this.Dirtied();
			if (this._validationCallbackInfo == null)
			{
				this._validationCallbackInfo = new ArrayList();
			}
			this._validationCallbackInfo.Add(new ValidationCallbackInfo(handler, data));
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x0600088A RID: 2186 RVA: 0x00012E2D File Offset: 0x0001102D
		// (set) Token: 0x0600088B RID: 2187 RVA: 0x00012E35 File Offset: 0x00011035
		public DateTime UtcTimestampCreated
		{
			get
			{
				return this._utcTimestampCreated;
			}
			set
			{
				this._utcTimestampCreated = value;
			}
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x00012E40 File Offset: 0x00011040
		// Note: this type is marked as 'beforefieldinit'.
		static HttpCachePolicy()
		{
			string[] array = new string[7];
			array[1] = "no-cache";
			array[2] = "private";
			array[3] = "no-cache";
			array[4] = "public";
			array[5] = "private";
			HttpCachePolicy.s_cacheabilityTokens = array;
			string[] array2 = new string[4];
			array2[1] = "must-revalidate";
			array2[2] = "proxy-revalidate";
			HttpCachePolicy.s_revalidationTokens = array2;
			HttpCachePolicy.s_cacheabilityValues = new int[]
			{
				-1,
				0,
				2,
				1,
				4,
				3,
				100
			};
		}

		// Token: 0x040002EB RID: 747
		private static TimeSpan s_oneYear = new TimeSpan(315360000000000L);

		// Token: 0x040002EC RID: 748
		private static HttpResponseHeader s_headerPragmaNoCache;

		// Token: 0x040002ED RID: 749
		private static HttpResponseHeader s_headerExpiresMinus1;

		// Token: 0x040002EE RID: 750
		private bool _isModified;

		// Token: 0x040002EF RID: 751
		private bool _hasSetCookieHeader;

		// Token: 0x040002F0 RID: 752
		private bool _noServerCaching;

		// Token: 0x040002F1 RID: 753
		private string _cacheExtension;

		// Token: 0x040002F2 RID: 754
		private bool _noTransforms;

		// Token: 0x040002F3 RID: 755
		private bool _ignoreRangeRequests;

		// Token: 0x040002F4 RID: 756
		private HttpCacheVaryByContentEncodings _varyByContentEncodings;

		// Token: 0x040002F5 RID: 757
		private HttpCacheVaryByHeaders _varyByHeaders;

		// Token: 0x040002F6 RID: 758
		private HttpCacheVaryByParams _varyByParams;

		// Token: 0x040002F7 RID: 759
		private string _varyByCustom;

		// Token: 0x040002F8 RID: 760
		private HttpCacheability _cacheability;

		// Token: 0x040002F9 RID: 761
		private bool _noStore;

		// Token: 0x040002FA RID: 762
		private HttpDictionary _privateFields;

		// Token: 0x040002FB RID: 763
		private HttpDictionary _noCacheFields;

		// Token: 0x040002FC RID: 764
		private DateTime _utcExpires;

		// Token: 0x040002FD RID: 765
		private bool _isExpiresSet;

		// Token: 0x040002FE RID: 766
		private TimeSpan _maxAge;

		// Token: 0x040002FF RID: 767
		private bool _isMaxAgeSet;

		// Token: 0x04000300 RID: 768
		private TimeSpan _proxyMaxAge;

		// Token: 0x04000301 RID: 769
		private bool _isProxyMaxAgeSet;

		// Token: 0x04000302 RID: 770
		private int _slidingExpiration;

		// Token: 0x04000303 RID: 771
		private DateTime _utcTimestampCreated;

		// Token: 0x04000304 RID: 772
		private TimeSpan _slidingDelta;

		// Token: 0x04000305 RID: 773
		private DateTime _utcTimestampRequest;

		// Token: 0x04000306 RID: 774
		private int _validUntilExpires;

		// Token: 0x04000307 RID: 775
		private int _allowInHistory;

		// Token: 0x04000308 RID: 776
		private HttpCacheRevalidation _revalidation;

		// Token: 0x04000309 RID: 777
		private DateTime _utcLastModified;

		// Token: 0x0400030A RID: 778
		private bool _isLastModifiedSet;

		// Token: 0x0400030B RID: 779
		private string _etag;

		// Token: 0x0400030C RID: 780
		private bool _generateLastModifiedFromFiles;

		// Token: 0x0400030D RID: 781
		private bool _generateEtagFromFiles;

		// Token: 0x0400030E RID: 782
		private int _omitVaryStar;

		// Token: 0x0400030F RID: 783
		private ArrayList _validationCallbackInfo;

		// Token: 0x04000310 RID: 784
		private bool _useCachedHeaders;

		// Token: 0x04000311 RID: 785
		private HttpResponseHeader _headerCacheControl;

		// Token: 0x04000312 RID: 786
		private HttpResponseHeader _headerPragma;

		// Token: 0x04000313 RID: 787
		private HttpResponseHeader _headerExpires;

		// Token: 0x04000314 RID: 788
		private HttpResponseHeader _headerLastModified;

		// Token: 0x04000315 RID: 789
		private HttpResponseHeader _headerEtag;

		// Token: 0x04000316 RID: 790
		private HttpResponseHeader _headerVaryBy;

		// Token: 0x04000317 RID: 791
		private bool _noMaxAgeInCacheControl;

		// Token: 0x04000318 RID: 792
		private bool _hasUserProvidedDependencies;

		// Token: 0x04000319 RID: 793
		private static readonly string[] s_cacheabilityTokens;

		// Token: 0x0400031A RID: 794
		private static readonly string[] s_revalidationTokens;

		// Token: 0x0400031B RID: 795
		private static readonly int[] s_cacheabilityValues;
	}
}
