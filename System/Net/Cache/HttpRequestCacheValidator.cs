using System;
using System.Collections;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;

namespace System.Net.Cache
{
	// Token: 0x0200055D RID: 1373
	internal class HttpRequestCacheValidator : RequestCacheValidator
	{
		// Token: 0x17000896 RID: 2198
		// (get) Token: 0x060029D9 RID: 10713 RVA: 0x000AF3E1 File Offset: 0x000AE3E1
		// (set) Token: 0x060029DA RID: 10714 RVA: 0x000AF3E9 File Offset: 0x000AE3E9
		internal HttpStatusCode CacheStatusCode
		{
			get
			{
				return this.m_StatusCode;
			}
			set
			{
				this.m_StatusCode = value;
			}
		}

		// Token: 0x17000897 RID: 2199
		// (get) Token: 0x060029DB RID: 10715 RVA: 0x000AF3F2 File Offset: 0x000AE3F2
		// (set) Token: 0x060029DC RID: 10716 RVA: 0x000AF3FA File Offset: 0x000AE3FA
		internal string CacheStatusDescription
		{
			get
			{
				return this.m_StatusDescription;
			}
			set
			{
				this.m_StatusDescription = value;
			}
		}

		// Token: 0x17000898 RID: 2200
		// (get) Token: 0x060029DD RID: 10717 RVA: 0x000AF403 File Offset: 0x000AE403
		// (set) Token: 0x060029DE RID: 10718 RVA: 0x000AF40B File Offset: 0x000AE40B
		internal Version CacheHttpVersion
		{
			get
			{
				return this.m_HttpVersion;
			}
			set
			{
				this.m_HttpVersion = value;
			}
		}

		// Token: 0x17000899 RID: 2201
		// (get) Token: 0x060029DF RID: 10719 RVA: 0x000AF414 File Offset: 0x000AE414
		// (set) Token: 0x060029E0 RID: 10720 RVA: 0x000AF41C File Offset: 0x000AE41C
		internal WebHeaderCollection CacheHeaders
		{
			get
			{
				return this.m_Headers;
			}
			set
			{
				this.m_Headers = value;
			}
		}

		// Token: 0x1700089A RID: 2202
		// (get) Token: 0x060029E1 RID: 10721 RVA: 0x000AF428 File Offset: 0x000AE428
		internal new HttpRequestCachePolicy Policy
		{
			get
			{
				if (this.m_HttpPolicy != null)
				{
					return this.m_HttpPolicy;
				}
				this.m_HttpPolicy = (base.Policy as HttpRequestCachePolicy);
				if (this.m_HttpPolicy != null)
				{
					return this.m_HttpPolicy;
				}
				this.m_HttpPolicy = new HttpRequestCachePolicy((HttpRequestCacheLevel)base.Policy.Level);
				return this.m_HttpPolicy;
			}
		}

		// Token: 0x1700089B RID: 2203
		// (get) Token: 0x060029E2 RID: 10722 RVA: 0x000AF480 File Offset: 0x000AE480
		// (set) Token: 0x060029E3 RID: 10723 RVA: 0x000AF488 File Offset: 0x000AE488
		internal NameValueCollection SystemMeta
		{
			get
			{
				return this.m_SystemMeta;
			}
			set
			{
				this.m_SystemMeta = value;
			}
		}

		// Token: 0x1700089C RID: 2204
		// (get) Token: 0x060029E4 RID: 10724 RVA: 0x000AF491 File Offset: 0x000AE491
		// (set) Token: 0x060029E5 RID: 10725 RVA: 0x000AF49E File Offset: 0x000AE49E
		internal HttpMethod RequestMethod
		{
			get
			{
				return this.m_RequestVars.Method;
			}
			set
			{
				this.m_RequestVars.Method = value;
			}
		}

		// Token: 0x1700089D RID: 2205
		// (get) Token: 0x060029E6 RID: 10726 RVA: 0x000AF4AC File Offset: 0x000AE4AC
		// (set) Token: 0x060029E7 RID: 10727 RVA: 0x000AF4B9 File Offset: 0x000AE4B9
		internal bool RequestRangeCache
		{
			get
			{
				return this.m_RequestVars.IsCacheRange;
			}
			set
			{
				this.m_RequestVars.IsCacheRange = value;
			}
		}

		// Token: 0x1700089E RID: 2206
		// (get) Token: 0x060029E8 RID: 10728 RVA: 0x000AF4C7 File Offset: 0x000AE4C7
		// (set) Token: 0x060029E9 RID: 10729 RVA: 0x000AF4D4 File Offset: 0x000AE4D4
		internal bool RequestRangeUser
		{
			get
			{
				return this.m_RequestVars.IsUserRange;
			}
			set
			{
				this.m_RequestVars.IsUserRange = value;
			}
		}

		// Token: 0x1700089F RID: 2207
		// (get) Token: 0x060029EA RID: 10730 RVA: 0x000AF4E2 File Offset: 0x000AE4E2
		// (set) Token: 0x060029EB RID: 10731 RVA: 0x000AF4EF File Offset: 0x000AE4EF
		internal string RequestIfHeader1
		{
			get
			{
				return this.m_RequestVars.IfHeader1;
			}
			set
			{
				this.m_RequestVars.IfHeader1 = value;
			}
		}

		// Token: 0x170008A0 RID: 2208
		// (get) Token: 0x060029EC RID: 10732 RVA: 0x000AF4FD File Offset: 0x000AE4FD
		// (set) Token: 0x060029ED RID: 10733 RVA: 0x000AF50A File Offset: 0x000AE50A
		internal string RequestValidator1
		{
			get
			{
				return this.m_RequestVars.Validator1;
			}
			set
			{
				this.m_RequestVars.Validator1 = value;
			}
		}

		// Token: 0x170008A1 RID: 2209
		// (get) Token: 0x060029EE RID: 10734 RVA: 0x000AF518 File Offset: 0x000AE518
		// (set) Token: 0x060029EF RID: 10735 RVA: 0x000AF525 File Offset: 0x000AE525
		internal string RequestIfHeader2
		{
			get
			{
				return this.m_RequestVars.IfHeader2;
			}
			set
			{
				this.m_RequestVars.IfHeader2 = value;
			}
		}

		// Token: 0x170008A2 RID: 2210
		// (get) Token: 0x060029F0 RID: 10736 RVA: 0x000AF533 File Offset: 0x000AE533
		// (set) Token: 0x060029F1 RID: 10737 RVA: 0x000AF540 File Offset: 0x000AE540
		internal string RequestValidator2
		{
			get
			{
				return this.m_RequestVars.Validator2;
			}
			set
			{
				this.m_RequestVars.Validator2 = value;
			}
		}

		// Token: 0x170008A3 RID: 2211
		// (get) Token: 0x060029F2 RID: 10738 RVA: 0x000AF54E File Offset: 0x000AE54E
		// (set) Token: 0x060029F3 RID: 10739 RVA: 0x000AF556 File Offset: 0x000AE556
		internal bool CacheDontUpdateHeaders
		{
			get
			{
				return this.m_DontUpdateHeaders;
			}
			set
			{
				this.m_DontUpdateHeaders = value;
			}
		}

		// Token: 0x170008A4 RID: 2212
		// (get) Token: 0x060029F4 RID: 10740 RVA: 0x000AF55F File Offset: 0x000AE55F
		// (set) Token: 0x060029F5 RID: 10741 RVA: 0x000AF56C File Offset: 0x000AE56C
		internal DateTime CacheDate
		{
			get
			{
				return this.m_CacheVars.Date;
			}
			set
			{
				this.m_CacheVars.Date = value;
			}
		}

		// Token: 0x170008A5 RID: 2213
		// (get) Token: 0x060029F6 RID: 10742 RVA: 0x000AF57A File Offset: 0x000AE57A
		// (set) Token: 0x060029F7 RID: 10743 RVA: 0x000AF587 File Offset: 0x000AE587
		internal DateTime CacheExpires
		{
			get
			{
				return this.m_CacheVars.Expires;
			}
			set
			{
				this.m_CacheVars.Expires = value;
			}
		}

		// Token: 0x170008A6 RID: 2214
		// (get) Token: 0x060029F8 RID: 10744 RVA: 0x000AF595 File Offset: 0x000AE595
		// (set) Token: 0x060029F9 RID: 10745 RVA: 0x000AF5A2 File Offset: 0x000AE5A2
		internal DateTime CacheLastModified
		{
			get
			{
				return this.m_CacheVars.LastModified;
			}
			set
			{
				this.m_CacheVars.LastModified = value;
			}
		}

		// Token: 0x170008A7 RID: 2215
		// (get) Token: 0x060029FA RID: 10746 RVA: 0x000AF5B0 File Offset: 0x000AE5B0
		// (set) Token: 0x060029FB RID: 10747 RVA: 0x000AF5BD File Offset: 0x000AE5BD
		internal long CacheEntityLength
		{
			get
			{
				return this.m_CacheVars.EntityLength;
			}
			set
			{
				this.m_CacheVars.EntityLength = value;
			}
		}

		// Token: 0x170008A8 RID: 2216
		// (get) Token: 0x060029FC RID: 10748 RVA: 0x000AF5CB File Offset: 0x000AE5CB
		// (set) Token: 0x060029FD RID: 10749 RVA: 0x000AF5D8 File Offset: 0x000AE5D8
		internal TimeSpan CacheAge
		{
			get
			{
				return this.m_CacheVars.Age;
			}
			set
			{
				this.m_CacheVars.Age = value;
			}
		}

		// Token: 0x170008A9 RID: 2217
		// (get) Token: 0x060029FE RID: 10750 RVA: 0x000AF5E6 File Offset: 0x000AE5E6
		// (set) Token: 0x060029FF RID: 10751 RVA: 0x000AF5F3 File Offset: 0x000AE5F3
		internal TimeSpan CacheMaxAge
		{
			get
			{
				return this.m_CacheVars.MaxAge;
			}
			set
			{
				this.m_CacheVars.MaxAge = value;
			}
		}

		// Token: 0x170008AA RID: 2218
		// (get) Token: 0x06002A00 RID: 10752 RVA: 0x000AF601 File Offset: 0x000AE601
		// (set) Token: 0x06002A01 RID: 10753 RVA: 0x000AF609 File Offset: 0x000AE609
		internal bool HeuristicExpiration
		{
			get
			{
				return this.m_HeuristicExpiration;
			}
			set
			{
				this.m_HeuristicExpiration = value;
			}
		}

		// Token: 0x170008AB RID: 2219
		// (get) Token: 0x06002A02 RID: 10754 RVA: 0x000AF612 File Offset: 0x000AE612
		// (set) Token: 0x06002A03 RID: 10755 RVA: 0x000AF61F File Offset: 0x000AE61F
		internal ResponseCacheControl CacheCacheControl
		{
			get
			{
				return this.m_CacheVars.CacheControl;
			}
			set
			{
				this.m_CacheVars.CacheControl = value;
			}
		}

		// Token: 0x170008AC RID: 2220
		// (get) Token: 0x06002A04 RID: 10756 RVA: 0x000AF62D File Offset: 0x000AE62D
		// (set) Token: 0x06002A05 RID: 10757 RVA: 0x000AF63A File Offset: 0x000AE63A
		internal DateTime ResponseDate
		{
			get
			{
				return this.m_ResponseVars.Date;
			}
			set
			{
				this.m_ResponseVars.Date = value;
			}
		}

		// Token: 0x170008AD RID: 2221
		// (get) Token: 0x06002A06 RID: 10758 RVA: 0x000AF648 File Offset: 0x000AE648
		// (set) Token: 0x06002A07 RID: 10759 RVA: 0x000AF655 File Offset: 0x000AE655
		internal DateTime ResponseExpires
		{
			get
			{
				return this.m_ResponseVars.Expires;
			}
			set
			{
				this.m_ResponseVars.Expires = value;
			}
		}

		// Token: 0x170008AE RID: 2222
		// (get) Token: 0x06002A08 RID: 10760 RVA: 0x000AF663 File Offset: 0x000AE663
		// (set) Token: 0x06002A09 RID: 10761 RVA: 0x000AF670 File Offset: 0x000AE670
		internal DateTime ResponseLastModified
		{
			get
			{
				return this.m_ResponseVars.LastModified;
			}
			set
			{
				this.m_ResponseVars.LastModified = value;
			}
		}

		// Token: 0x170008AF RID: 2223
		// (get) Token: 0x06002A0A RID: 10762 RVA: 0x000AF67E File Offset: 0x000AE67E
		// (set) Token: 0x06002A0B RID: 10763 RVA: 0x000AF68B File Offset: 0x000AE68B
		internal long ResponseEntityLength
		{
			get
			{
				return this.m_ResponseVars.EntityLength;
			}
			set
			{
				this.m_ResponseVars.EntityLength = value;
			}
		}

		// Token: 0x170008B0 RID: 2224
		// (get) Token: 0x06002A0C RID: 10764 RVA: 0x000AF699 File Offset: 0x000AE699
		// (set) Token: 0x06002A0D RID: 10765 RVA: 0x000AF6A6 File Offset: 0x000AE6A6
		internal long ResponseRangeStart
		{
			get
			{
				return this.m_ResponseVars.RangeStart;
			}
			set
			{
				this.m_ResponseVars.RangeStart = value;
			}
		}

		// Token: 0x170008B1 RID: 2225
		// (get) Token: 0x06002A0E RID: 10766 RVA: 0x000AF6B4 File Offset: 0x000AE6B4
		// (set) Token: 0x06002A0F RID: 10767 RVA: 0x000AF6C1 File Offset: 0x000AE6C1
		internal long ResponseRangeEnd
		{
			get
			{
				return this.m_ResponseVars.RangeEnd;
			}
			set
			{
				this.m_ResponseVars.RangeEnd = value;
			}
		}

		// Token: 0x170008B2 RID: 2226
		// (get) Token: 0x06002A10 RID: 10768 RVA: 0x000AF6CF File Offset: 0x000AE6CF
		// (set) Token: 0x06002A11 RID: 10769 RVA: 0x000AF6DC File Offset: 0x000AE6DC
		internal TimeSpan ResponseAge
		{
			get
			{
				return this.m_ResponseVars.Age;
			}
			set
			{
				this.m_ResponseVars.Age = value;
			}
		}

		// Token: 0x170008B3 RID: 2227
		// (get) Token: 0x06002A12 RID: 10770 RVA: 0x000AF6EA File Offset: 0x000AE6EA
		// (set) Token: 0x06002A13 RID: 10771 RVA: 0x000AF6F7 File Offset: 0x000AE6F7
		internal ResponseCacheControl ResponseCacheControl
		{
			get
			{
				return this.m_ResponseVars.CacheControl;
			}
			set
			{
				this.m_ResponseVars.CacheControl = value;
			}
		}

		// Token: 0x06002A14 RID: 10772 RVA: 0x000AF708 File Offset: 0x000AE708
		private void ZeroPrivateVars()
		{
			this.m_RequestVars = default(HttpRequestCacheValidator.RequestVars);
			this.m_HttpPolicy = null;
			this.m_StatusCode = (HttpStatusCode)0;
			this.m_StatusDescription = null;
			this.m_HttpVersion = null;
			this.m_Headers = null;
			this.m_SystemMeta = null;
			this.m_DontUpdateHeaders = false;
			this.m_HeuristicExpiration = false;
			this.m_CacheVars = default(HttpRequestCacheValidator.Vars);
			this.m_CacheVars.Initialize();
			this.m_ResponseVars = default(HttpRequestCacheValidator.Vars);
			this.m_ResponseVars.Initialize();
		}

		// Token: 0x06002A15 RID: 10773 RVA: 0x000AF787 File Offset: 0x000AE787
		internal override RequestCacheValidator CreateValidator()
		{
			return new HttpRequestCacheValidator(base.StrictCacheErrors, base.UnspecifiedMaxAge);
		}

		// Token: 0x06002A16 RID: 10774 RVA: 0x000AF79A File Offset: 0x000AE79A
		internal HttpRequestCacheValidator(bool strictCacheErrors, TimeSpan unspecifiedMaxAge) : base(strictCacheErrors, unspecifiedMaxAge)
		{
		}

		// Token: 0x06002A17 RID: 10775 RVA: 0x000AF7A4 File Offset: 0x000AE7A4
		protected internal override CacheValidationStatus ValidateRequest()
		{
			this.ZeroPrivateVars();
			string text = base.Request.Method.ToUpper(CultureInfo.InvariantCulture);
			if (Logging.On)
			{
				Logging.PrintInfo(Logging.RequestCache, SR.GetString("net_log_cache_request_method", new object[]
				{
					text
				}));
			}
			string key;
			switch (key = text)
			{
			case "GET":
				this.RequestMethod = HttpMethod.Get;
				goto IL_149;
			case "POST":
				this.RequestMethod = HttpMethod.Post;
				goto IL_149;
			case "HEAD":
				this.RequestMethod = HttpMethod.Head;
				goto IL_149;
			case "PUT":
				this.RequestMethod = HttpMethod.Put;
				goto IL_149;
			case "DELETE":
				this.RequestMethod = HttpMethod.Delete;
				goto IL_149;
			case "OPTIONS":
				this.RequestMethod = HttpMethod.Options;
				goto IL_149;
			case "TRACE":
				this.RequestMethod = HttpMethod.Trace;
				goto IL_149;
			case "CONNECT":
				this.RequestMethod = HttpMethod.Connect;
				goto IL_149;
			}
			this.RequestMethod = HttpMethod.Other;
			IL_149:
			return Rfc2616.OnValidateRequest(this);
		}

		// Token: 0x06002A18 RID: 10776 RVA: 0x000AF900 File Offset: 0x000AE900
		protected internal override CacheFreshnessStatus ValidateFreshness()
		{
			string text = this.ParseStatusLine();
			if (Logging.On)
			{
				if (this.CacheStatusCode == (HttpStatusCode)0)
				{
					Logging.PrintError(Logging.RequestCache, SR.GetString("net_log_cache_http_status_parse_failure", new object[]
					{
						(text == null) ? "null" : text
					}));
				}
				else
				{
					Logging.PrintInfo(Logging.RequestCache, SR.GetString("net_log_cache_http_status_line", new object[]
					{
						(this.CacheHttpVersion != null) ? this.CacheHttpVersion.ToString() : "null",
						(int)this.CacheStatusCode,
						this.CacheStatusDescription
					}));
				}
			}
			this.CreateCacheHeaders(this.CacheStatusCode != (HttpStatusCode)0);
			this.CreateSystemMeta();
			this.FetchHeaderValues(true);
			if (Logging.On)
			{
				Logging.PrintInfo(Logging.RequestCache, SR.GetString("net_log_cache_cache_control", new object[]
				{
					this.CacheCacheControl.ToString()
				}));
			}
			return Rfc2616.OnValidateFreshness(this);
		}

		// Token: 0x06002A19 RID: 10777 RVA: 0x000AFA00 File Offset: 0x000AEA00
		protected internal override CacheValidationStatus ValidateCache()
		{
			if (this.Policy.Level != HttpRequestCacheLevel.Revalidate && base.Policy.Level >= RequestCacheLevel.Reload)
			{
				if (Logging.On)
				{
					Logging.PrintError(Logging.RequestCache, SR.GetString("net_log_cache_validator_invalid_for_policy", new object[]
					{
						this.Policy.ToString()
					}));
				}
				return CacheValidationStatus.DoNotTakeFromCache;
			}
			if (base.CacheStream == Stream.Null || this.CacheStatusCode == (HttpStatusCode)0 || this.CacheStatusCode == HttpStatusCode.NotModified)
			{
				if (this.Policy.Level == HttpRequestCacheLevel.CacheOnly)
				{
					this.FailRequest(WebExceptionStatus.CacheEntryNotFound);
				}
				return CacheValidationStatus.DoNotTakeFromCache;
			}
			if (this.RequestMethod == HttpMethod.Head)
			{
				base.CacheStream.Close();
				base.CacheStream = new SyncMemoryStream(new byte[0]);
			}
			this.RemoveWarnings_1xx();
			base.CacheStreamOffset = 0L;
			base.CacheStreamLength = base.CacheEntry.StreamSize;
			CacheValidationStatus cacheValidationStatus = Rfc2616.OnValidateCache(this);
			if (cacheValidationStatus != CacheValidationStatus.ReturnCachedResponse && this.Policy.Level == HttpRequestCacheLevel.CacheOnly)
			{
				this.FailRequest(WebExceptionStatus.CacheEntryNotFound);
			}
			if (cacheValidationStatus == CacheValidationStatus.ReturnCachedResponse)
			{
				if (base.CacheFreshnessStatus == CacheFreshnessStatus.Stale)
				{
					this.CacheHeaders.Add("Warning", "110 Response is stale");
				}
				if (base.Policy.Level == RequestCacheLevel.CacheOnly)
				{
					this.CacheHeaders.Add("Warning", "112 Disconnected operation");
				}
				if (this.HeuristicExpiration && (int)this.CacheAge.TotalSeconds >= 86400)
				{
					this.CacheHeaders.Add("Warning", "113 Heuristic expiration");
				}
			}
			if (cacheValidationStatus == CacheValidationStatus.DoNotTakeFromCache)
			{
				this.CacheStatusCode = (HttpStatusCode)0;
			}
			else if (cacheValidationStatus == CacheValidationStatus.ReturnCachedResponse)
			{
				this.CacheHeaders["Age"] = ((int)this.CacheAge.TotalSeconds).ToString(NumberFormatInfo.InvariantInfo);
			}
			return cacheValidationStatus;
		}

		// Token: 0x06002A1A RID: 10778 RVA: 0x000AFBB4 File Offset: 0x000AEBB4
		protected internal override CacheValidationStatus RevalidateCache()
		{
			if (this.Policy.Level != HttpRequestCacheLevel.Revalidate && base.Policy.Level >= RequestCacheLevel.Reload)
			{
				if (Logging.On)
				{
					Logging.PrintError(Logging.RequestCache, SR.GetString("net_log_cache_validator_invalid_for_policy", new object[]
					{
						this.Policy.ToString()
					}));
				}
				return CacheValidationStatus.DoNotTakeFromCache;
			}
			if (base.CacheStream == Stream.Null || this.CacheStatusCode == (HttpStatusCode)0 || this.CacheStatusCode == HttpStatusCode.NotModified)
			{
				return CacheValidationStatus.DoNotTakeFromCache;
			}
			CacheValidationStatus cacheValidationStatus = CacheValidationStatus.DoNotTakeFromCache;
			HttpWebResponse httpWebResponse = base.Response as HttpWebResponse;
			if (httpWebResponse == null)
			{
				return CacheValidationStatus.DoNotTakeFromCache;
			}
			if (httpWebResponse.StatusCode >= HttpStatusCode.InternalServerError)
			{
				if (Rfc2616.Common.ValidateCacheOn5XXResponse(this) == CacheValidationStatus.ReturnCachedResponse)
				{
					if (base.CacheFreshnessStatus == CacheFreshnessStatus.Stale)
					{
						this.CacheHeaders.Add("Warning", "110 Response is stale");
					}
					if (this.HeuristicExpiration && (int)this.CacheAge.TotalSeconds >= 86400)
					{
						this.CacheHeaders.Add("Warning", "113 Heuristic expiration");
					}
				}
			}
			else if (base.ResponseCount > 1)
			{
				cacheValidationStatus = CacheValidationStatus.DoNotTakeFromCache;
			}
			else
			{
				this.CacheAge = TimeSpan.Zero;
				cacheValidationStatus = Rfc2616.Common.ValidateCacheAfterResponse(this, httpWebResponse);
			}
			if (cacheValidationStatus == CacheValidationStatus.ReturnCachedResponse)
			{
				this.CacheHeaders["Age"] = ((int)this.CacheAge.TotalSeconds).ToString(NumberFormatInfo.InvariantInfo);
			}
			return cacheValidationStatus;
		}

		// Token: 0x06002A1B RID: 10779 RVA: 0x000AFD04 File Offset: 0x000AED04
		protected internal override CacheValidationStatus ValidateResponse()
		{
			if (this.Policy.Level != HttpRequestCacheLevel.CacheOrNextCacheOnly && this.Policy.Level != HttpRequestCacheLevel.Default && this.Policy.Level != HttpRequestCacheLevel.Revalidate)
			{
				if (Logging.On)
				{
					Logging.PrintInfo(Logging.RequestCache, SR.GetString("net_log_cache_response_valid_based_on_policy", new object[]
					{
						this.Policy.ToString()
					}));
				}
				return CacheValidationStatus.Continue;
			}
			HttpWebResponse httpWebResponse = base.Response as HttpWebResponse;
			if (httpWebResponse == null)
			{
				if (Logging.On)
				{
					Logging.PrintWarning(Logging.RequestCache, SR.GetString("net_log_cache_null_response_failure"));
				}
				return CacheValidationStatus.Continue;
			}
			this.FetchHeaderValues(false);
			if (Logging.On)
			{
				Logging.PrintInfo(Logging.RequestCache, string.Concat(new object[]
				{
					"StatusCode=",
					((int)httpWebResponse.StatusCode).ToString(CultureInfo.InvariantCulture),
					' ',
					httpWebResponse.StatusCode.ToString(),
					(httpWebResponse.StatusCode == HttpStatusCode.PartialContent) ? (", Content-Range: " + httpWebResponse.Headers["Content-Range"]) : string.Empty
				}));
			}
			return Rfc2616.OnValidateResponse(this);
		}

		// Token: 0x06002A1C RID: 10780 RVA: 0x000AFE38 File Offset: 0x000AEE38
		protected internal override CacheValidationStatus UpdateCache()
		{
			if (this.Policy.Level == HttpRequestCacheLevel.NoCacheNoStore)
			{
				if (Logging.On)
				{
					Logging.PrintInfo(Logging.RequestCache, SR.GetString("net_log_cache_removed_existing_based_on_policy", new object[]
					{
						this.Policy.ToString()
					}));
				}
				return CacheValidationStatus.RemoveFromCache;
			}
			if (this.Policy.Level == HttpRequestCacheLevel.CacheOnly)
			{
				if (Logging.On)
				{
					Logging.PrintInfo(Logging.RequestCache, SR.GetString("net_log_cache_not_updated_based_on_policy", new object[]
					{
						this.Policy.ToString()
					}));
				}
				return CacheValidationStatus.DoNotUpdateCache;
			}
			if (this.CacheHeaders == null)
			{
				this.CacheHeaders = new WebHeaderCollection();
			}
			if (this.SystemMeta == null)
			{
				this.SystemMeta = new NameValueCollection(1, CaseInsensitiveAscii.StaticInstance);
			}
			if (this.ResponseCacheControl == null)
			{
				this.FetchHeaderValues(false);
			}
			CacheValidationStatus cacheValidationStatus = Rfc2616.OnUpdateCache(this);
			if (cacheValidationStatus == CacheValidationStatus.UpdateResponseInformation || cacheValidationStatus == CacheValidationStatus.CacheResponse)
			{
				this.FinallyUpdateCacheEntry();
			}
			return cacheValidationStatus;
		}

		// Token: 0x06002A1D RID: 10781 RVA: 0x000AFF1C File Offset: 0x000AEF1C
		private void FinallyUpdateCacheEntry()
		{
			base.CacheEntry.EntryMetadata = null;
			base.CacheEntry.SystemMetadata = null;
			if (this.CacheHeaders == null)
			{
				return;
			}
			base.CacheEntry.EntryMetadata = new StringCollection();
			base.CacheEntry.SystemMetadata = new StringCollection();
			if (this.CacheHttpVersion == null)
			{
				if (Logging.On)
				{
					Logging.PrintWarning(Logging.RequestCache, SR.GetString("net_log_cache_invalid_http_version"));
				}
				this.CacheHttpVersion = new Version(1, 0);
			}
			StringBuilder stringBuilder = new StringBuilder(this.CacheStatusDescription.Length + 20);
			stringBuilder.Append("HTTP/");
			stringBuilder.Append(this.CacheHttpVersion.ToString(2));
			stringBuilder.Append(' ');
			stringBuilder.Append(((int)this.CacheStatusCode).ToString(NumberFormatInfo.InvariantInfo));
			stringBuilder.Append(' ');
			stringBuilder.Append(this.CacheStatusDescription);
			base.CacheEntry.EntryMetadata.Add(stringBuilder.ToString());
			HttpRequestCacheValidator.UpdateStringCollection(base.CacheEntry.EntryMetadata, this.CacheHeaders, false);
			if (this.SystemMeta != null)
			{
				HttpRequestCacheValidator.UpdateStringCollection(base.CacheEntry.SystemMetadata, this.SystemMeta, true);
			}
			if (this.ResponseExpires != DateTime.MinValue)
			{
				base.CacheEntry.ExpiresUtc = this.ResponseExpires;
			}
			if (this.ResponseLastModified != DateTime.MinValue)
			{
				base.CacheEntry.LastModifiedUtc = this.ResponseLastModified;
			}
			if (this.Policy.Level == HttpRequestCacheLevel.Default)
			{
				base.CacheEntry.MaxStale = this.Policy.MaxStale;
			}
			base.CacheEntry.LastSynchronizedUtc = DateTime.UtcNow;
		}

		// Token: 0x06002A1E RID: 10782 RVA: 0x000B00D4 File Offset: 0x000AF0D4
		private static void UpdateStringCollection(StringCollection result, NameValueCollection cc, bool winInetCompat)
		{
			for (int i = 0; i < cc.Count; i++)
			{
				StringBuilder stringBuilder = new StringBuilder(40);
				string key = cc.GetKey(i);
				stringBuilder.Append(key).Append(':');
				string[] values = cc.GetValues(i);
				if (values.Length != 0)
				{
					if (winInetCompat)
					{
						stringBuilder.Append(values[0]);
					}
					else
					{
						stringBuilder.Append(' ').Append(values[0]);
					}
				}
				for (int j = 1; j < values.Length; j++)
				{
					stringBuilder.Append(key).Append(", ").Append(values[j]);
				}
				result.Add(stringBuilder.ToString());
			}
			result.Add(string.Empty);
		}

		// Token: 0x06002A1F RID: 10783 RVA: 0x000B018C File Offset: 0x000AF18C
		private string ParseStatusLine()
		{
			this.CacheStatusCode = (HttpStatusCode)0;
			if (base.CacheEntry.EntryMetadata == null || base.CacheEntry.EntryMetadata.Count == 0)
			{
				return null;
			}
			string text = base.CacheEntry.EntryMetadata[0];
			if (text == null)
			{
				return null;
			}
			int num = 0;
			char c = '\0';
			while (++num < text.Length && (c = text[num]) != '/')
			{
			}
			if (num == text.Length)
			{
				return text;
			}
			int num2 = -1;
			int num3 = -1;
			int num4 = -1;
			while (++num < text.Length && (c = text[num]) >= '0' && c <= '9')
			{
				num2 = ((num2 < 0) ? 0 : (num2 * 10)) + (int)(c - '0');
			}
			if (num2 < 0 || c != '.')
			{
				return text;
			}
			while (++num < text.Length && (c = text[num]) >= '0' && c <= '9')
			{
				num3 = ((num3 < 0) ? 0 : (num3 * 10)) + (int)(c - '0');
			}
			if (num3 < 0 || (c != ' ' && c != '\t'))
			{
				return text;
			}
			while (++num < text.Length && ((c = text[num]) == ' ' || c == '\t'))
			{
			}
			if (num >= text.Length)
			{
				return text;
			}
			while (c >= '0' && c <= '9')
			{
				num4 = ((num4 < 0) ? 0 : (num4 * 10)) + (int)(c - '0');
				if (++num == text.Length)
				{
					break;
				}
				c = text[num];
			}
			if (num4 < 0 || (num <= text.Length && c != ' ' && c != '\t'))
			{
				return text;
			}
			while (num < text.Length && (text[num] == ' ' || text[num] == '\t'))
			{
				num++;
			}
			this.CacheStatusDescription = text.Substring(num);
			this.CacheHttpVersion = new Version(num2, num3);
			this.CacheStatusCode = (HttpStatusCode)num4;
			return text;
		}

		// Token: 0x06002A20 RID: 10784 RVA: 0x000B0348 File Offset: 0x000AF348
		private void CreateCacheHeaders(bool ignoreFirstString)
		{
			if (this.CacheHeaders == null)
			{
				this.CacheHeaders = new WebHeaderCollection();
			}
			if (base.CacheEntry.EntryMetadata == null || base.CacheEntry.EntryMetadata.Count == 0)
			{
				if (Logging.On)
				{
					Logging.PrintWarning(Logging.RequestCache, SR.GetString("net_log_cache_no_http_response_header"));
				}
				return;
			}
			string text = this.ParseNameValues(this.CacheHeaders, base.CacheEntry.EntryMetadata, ignoreFirstString ? 1 : 0);
			if (text != null)
			{
				if (Logging.On)
				{
					Logging.PrintWarning(Logging.RequestCache, SR.GetString("net_log_cache_http_header_parse_error", new object[]
					{
						text
					}));
				}
				this.CacheHeaders.Clear();
			}
		}

		// Token: 0x06002A21 RID: 10785 RVA: 0x000B03F8 File Offset: 0x000AF3F8
		private void CreateSystemMeta()
		{
			if (this.SystemMeta == null)
			{
				this.SystemMeta = new NameValueCollection((base.CacheEntry.EntryMetadata == null || base.CacheEntry.EntryMetadata.Count == 0) ? 2 : base.CacheEntry.EntryMetadata.Count, CaseInsensitiveAscii.StaticInstance);
			}
			if (base.CacheEntry.EntryMetadata == null || base.CacheEntry.EntryMetadata.Count == 0)
			{
				return;
			}
			string text = this.ParseNameValues(this.SystemMeta, base.CacheEntry.SystemMetadata, 0);
			if (text != null && Logging.On)
			{
				Logging.PrintWarning(Logging.RequestCache, SR.GetString("net_log_cache_metadata_name_value_parse_error", new object[]
				{
					text
				}));
			}
		}

		// Token: 0x06002A22 RID: 10786 RVA: 0x000B04B4 File Offset: 0x000AF4B4
		private string ParseNameValues(NameValueCollection cc, StringCollection sc, int start)
		{
			WebHeaderCollection webHeaderCollection = cc as WebHeaderCollection;
			string text = null;
			if (sc != null)
			{
				for (int i = start; i < sc.Count; i++)
				{
					string text2 = sc[i];
					if (text2 == null || text2.Length == 0)
					{
						return null;
					}
					if (text2[0] == ' ' || text2[0] == '\t')
					{
						if (text == null)
						{
							return text2;
						}
						if (webHeaderCollection != null)
						{
							webHeaderCollection.AddInternal(text, text2);
						}
						else
						{
							cc.Add(text, text2);
						}
					}
					int num = text2.IndexOf(':');
					if (num < 0)
					{
						return text2;
					}
					text = text2.Substring(0, num);
					while (++num < text2.Length)
					{
						if (text2[num] != ' ' && text2[num] != '\t')
						{
							break;
						}
					}
					try
					{
						if (webHeaderCollection != null)
						{
							webHeaderCollection.AddInternal(text, text2.Substring(num));
						}
						else
						{
							cc.Add(text, text2.Substring(num));
						}
					}
					catch (Exception ex)
					{
						if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
						{
							throw;
						}
						return text2;
					}
				}
			}
			return null;
		}

		// Token: 0x06002A23 RID: 10787 RVA: 0x000B05CC File Offset: 0x000AF5CC
		private void FetchHeaderValues(bool forCache)
		{
			WebHeaderCollection webHeaderCollection = forCache ? this.CacheHeaders : base.Response.Headers;
			this.FetchCacheControl(webHeaderCollection.CacheControl, forCache);
			string text = webHeaderCollection.Date;
			DateTime dateTime = DateTime.MinValue;
			if (text != null && HttpDateParse.ParseHttpDate(text, out dateTime))
			{
				dateTime = dateTime.ToUniversalTime();
			}
			if (forCache)
			{
				this.CacheDate = dateTime;
			}
			else
			{
				this.ResponseDate = dateTime;
			}
			text = webHeaderCollection.Expires;
			dateTime = DateTime.MinValue;
			if (text != null && HttpDateParse.ParseHttpDate(text, out dateTime))
			{
				dateTime = dateTime.ToUniversalTime();
			}
			if (forCache)
			{
				this.CacheExpires = dateTime;
			}
			else
			{
				this.ResponseExpires = dateTime;
			}
			text = webHeaderCollection.LastModified;
			dateTime = DateTime.MinValue;
			if (text != null && HttpDateParse.ParseHttpDate(text, out dateTime))
			{
				dateTime = dateTime.ToUniversalTime();
			}
			if (forCache)
			{
				this.CacheLastModified = dateTime;
			}
			else
			{
				this.ResponseLastModified = dateTime;
			}
			long num = -1L;
			long responseRangeStart = -1L;
			long responseRangeEnd = -1L;
			HttpWebResponse httpWebResponse = base.Response as HttpWebResponse;
			if ((forCache ? this.CacheStatusCode : httpWebResponse.StatusCode) != HttpStatusCode.PartialContent)
			{
				text = webHeaderCollection.ContentLength;
				if (text != null && text.Length != 0)
				{
					int num2 = 0;
					char c = text[0];
					while (num2 < text.Length && c == ' ')
					{
						c = text[++num2];
					}
					if (num2 != text.Length && c >= '0' && c <= '9')
					{
						num = (long)(c - '0');
						while (++num2 < text.Length && (c = text[num2]) >= '0')
						{
							if (c > '9')
							{
								break;
							}
							num = num * 10L + (long)(c - '0');
						}
					}
				}
			}
			else
			{
				text = webHeaderCollection["Content-Range"];
				if (text == null || !Rfc2616.Common.GetBytesRange(text, ref responseRangeStart, ref responseRangeEnd, ref num, false))
				{
					if (Logging.On)
					{
						Logging.PrintError(Logging.RequestCache, SR.GetString("net_log_cache_content_range_error", new object[]
						{
							(text == null) ? "<null>" : text
						}));
					}
					responseRangeEnd = (responseRangeStart = (num = -1L));
				}
				else if (forCache && num == base.CacheEntry.StreamSize)
				{
					responseRangeStart = -1L;
					responseRangeEnd = -1L;
					this.CacheStatusCode = HttpStatusCode.OK;
					this.CacheStatusDescription = "OK";
				}
			}
			if (forCache)
			{
				this.CacheEntityLength = num;
				this.ResponseRangeStart = responseRangeStart;
				this.ResponseRangeEnd = responseRangeEnd;
			}
			else
			{
				this.ResponseEntityLength = num;
				this.ResponseRangeStart = responseRangeStart;
				this.ResponseRangeEnd = responseRangeEnd;
			}
			TimeSpan timeSpan = TimeSpan.MinValue;
			text = webHeaderCollection["Age"];
			if (text != null)
			{
				int i = 0;
				int num3 = 0;
				while (i < text.Length)
				{
					if (text[i++] != ' ')
					{
						break;
					}
				}
				while (i < text.Length && text[i] >= '0' && text[i] <= '9')
				{
					num3 = num3 * 10 + (int)(text[i++] - '0');
				}
				timeSpan = TimeSpan.FromSeconds((double)num3);
			}
			if (forCache)
			{
				this.CacheAge = timeSpan;
				return;
			}
			this.ResponseAge = timeSpan;
		}

		// Token: 0x06002A24 RID: 10788 RVA: 0x000B08CC File Offset: 0x000AF8CC
		private unsafe void FetchCacheControl(string s, bool forCache)
		{
			ResponseCacheControl responseCacheControl = new ResponseCacheControl();
			if (forCache)
			{
				this.CacheCacheControl = responseCacheControl;
			}
			else
			{
				this.ResponseCacheControl = responseCacheControl;
			}
			if (s != null && s.Length != 0)
			{
				fixed (char* ptr = s)
				{
					int length = s.Length;
					for (int i = 0; i < length - 4; i++)
					{
						if (ptr[i] < ' ' || ptr[i] >= '\u007f')
						{
							if (Logging.On)
							{
								Logging.PrintError(Logging.RequestCache, SR.GetString("net_log_cache_cache_control_error", new object[]
								{
									s
								}));
							}
							return;
						}
						if (ptr[i] != ' ' && ptr[i] != ',')
						{
							if (IntPtr.Size == 4)
							{
								long* ptr2 = (long*)(ptr + i);
								long num = *ptr2 | 9007336695791648L;
								if (num <= 30399718399213680L)
								{
									if (num <= 27303540895318131L)
									{
										if (num != 12666889354412141L)
										{
											if (num == 27303540895318131L)
											{
												if (i + 8 > length)
												{
													return;
												}
												if ((ptr2[1] | 2097184L) == 28429415035764856L)
												{
													i += 8;
													while (i < length && ptr[i] == ' ')
													{
														i++;
													}
													if (i == length || ptr[(IntPtr)(i++) * 2] != '=')
													{
														return;
													}
													while (i < length && ptr[i] == ' ')
													{
														i++;
													}
													if (i == length)
													{
														return;
													}
													responseCacheControl.SMaxAge = 0;
													while (i < length && ptr[i] >= '0' && ptr[i] <= '9')
													{
														responseCacheControl.SMaxAge = responseCacheControl.SMaxAge * 10 + (int)(ptr[(IntPtr)(i++) * 2] - '0');
													}
													i--;
												}
											}
										}
										else
										{
											if (i + 7 > length)
											{
												return;
											}
											if ((*(int*)(ptr2 + 1) | 2097184) == 6750305 && (ptr[i + 6] | ' ') == 'e')
											{
												i += 7;
												while (i < length && ptr[i] == ' ')
												{
													i++;
												}
												if (i == length || ptr[(IntPtr)(i++) * 2] != '=')
												{
													return;
												}
												while (i < length && ptr[i] == ' ')
												{
													i++;
												}
												if (i == length)
												{
													return;
												}
												responseCacheControl.MaxAge = 0;
												while (i < length && ptr[i] >= '0' && ptr[i] <= '9')
												{
													responseCacheControl.MaxAge = responseCacheControl.MaxAge * 10 + (int)(ptr[(IntPtr)(i++) * 2] - '0');
												}
												i--;
											}
										}
									}
									else if (num != 27866215975157870L)
									{
										if (num == 30399718399213680L)
										{
											if (i + 6 > length)
											{
												return;
											}
											if ((*(int*)(ptr2 + 1) | 2097184) == 6488169)
											{
												responseCacheControl.Public = true;
												i += 5;
											}
										}
									}
									else
									{
										if (i + 8 > length)
										{
											return;
										}
										if ((ptr2[1] | 2097184L) == 28429419330863201L)
										{
											responseCacheControl.NoCache = true;
											i += 7;
											while (i < length && ptr[i] == ' ')
											{
												i++;
											}
											if (i >= length || ptr[i] != '=')
											{
												i--;
											}
											else
											{
												while (i < length && ptr[(IntPtr)(++i) * 2] == ' ')
												{
												}
												if (i >= length || ptr[i] != '"')
												{
													i--;
												}
												else
												{
													ArrayList arrayList = new ArrayList();
													i++;
													while (i < length && ptr[i] != '"')
													{
														while (i < length && ptr[i] == ' ')
														{
															i++;
														}
														int num2 = i;
														while (i < length && ptr[i] != ' ' && ptr[i] != ',' && ptr[i] != '"')
														{
															i++;
														}
														if (num2 != i)
														{
															arrayList.Add(s.Substring(num2, i - num2));
														}
														while (i < length && ptr[i] != ',' && ptr[i] != '"')
														{
															i++;
														}
													}
													if (arrayList.Count != 0)
													{
														responseCacheControl.NoCacheHeaders = (string[])arrayList.ToArray(typeof(string));
													}
												}
											}
										}
									}
								}
								else if (num <= 32651591227342957L)
								{
									if (num != 32369815602528366L)
									{
										if (num == 32651591227342957L)
										{
											if (i + 15 <= length && (ptr2[1] | 9007336695791648L) == 33214481051025453L && (ptr2[2] | 9007336695791648L) == 28147948649709665L && (*(int*)(ptr2 + 3) | 2097184) == 7602273 && (ptr[i + 14] | ' ') == 'e')
											{
												responseCacheControl.MustRevalidate = true;
												i += 14;
											}
										}
									}
									else
									{
										if (i + 8 > length)
										{
											return;
										}
										if ((ptr2[1] | 2097184L) == 28429462281322612L)
										{
											responseCacheControl.NoStore = true;
											i += 7;
										}
									}
								}
								else if (num != 33214498230894704L)
								{
									if (num == 33777473954119792L && i + 16 <= length && (ptr2[1] | 9007336695791648L) == 28429462276997241L && (ptr2[2] | 9007336695791648L) == 29555336417443958L && (ptr2[3] | 9007336695791648L) == 28429470870339684L)
									{
										responseCacheControl.ProxyRevalidate = true;
										i += 15;
									}
								}
								else
								{
									if (i + 7 > length)
									{
										return;
									}
									if ((*(int*)(ptr2 + 1) | 2097184) == 7602273 && (ptr[i + 6] | ' ') == 'e')
									{
										responseCacheControl.Private = true;
										i += 6;
										while (i < length && ptr[i] == ' ')
										{
											i++;
										}
										if (i >= length || ptr[i] != '=')
										{
											i--;
										}
										else
										{
											while (i < length && ptr[(IntPtr)(++i) * 2] == ' ')
											{
											}
											if (i >= length || ptr[i] != '"')
											{
												i--;
											}
											else
											{
												ArrayList arrayList2 = new ArrayList();
												i++;
												while (i < length && ptr[i] != '"')
												{
													while (i < length && ptr[i] == ' ')
													{
														i++;
													}
													int num3 = i;
													while (i < length && ptr[i] != ' ' && ptr[i] != ',' && ptr[i] != '"')
													{
														i++;
													}
													if (num3 != i)
													{
														arrayList2.Add(s.Substring(num3, i - num3));
													}
													while (i < length && ptr[i] != ',' && ptr[i] != '"')
													{
														i++;
													}
												}
												if (arrayList2.Count != 0)
												{
													responseCacheControl.PrivateHeaders = (string[])arrayList2.ToArray(typeof(string));
												}
											}
										}
									}
								}
							}
							else if (Rfc2616.Common.UnsafeAsciiLettersNoCaseEqual(ptr, i, length, "proxy-revalidate"))
							{
								responseCacheControl.ProxyRevalidate = true;
								i += 15;
							}
							else if (Rfc2616.Common.UnsafeAsciiLettersNoCaseEqual(ptr, i, length, "public"))
							{
								responseCacheControl.Public = true;
								i += 5;
							}
							else if (Rfc2616.Common.UnsafeAsciiLettersNoCaseEqual(ptr, i, length, "private"))
							{
								responseCacheControl.Private = true;
								i += 6;
								while (i < length && ptr[i] == ' ')
								{
									i++;
								}
								if (i >= length || ptr[i] != '=')
								{
									i--;
									break;
								}
								while (i < length && ptr[(IntPtr)(++i) * 2] == ' ')
								{
								}
								if (i >= length || ptr[i] != '"')
								{
									i--;
									break;
								}
								ArrayList arrayList3 = new ArrayList();
								i++;
								while (i < length && ptr[i] != '"')
								{
									while (i < length && ptr[i] == ' ')
									{
										i++;
									}
									int num4 = i;
									while (i < length && ptr[i] != ' ' && ptr[i] != ',' && ptr[i] != '"')
									{
										i++;
									}
									if (num4 != i)
									{
										arrayList3.Add(s.Substring(num4, i - num4));
									}
									while (i < length && ptr[i] != ',' && ptr[i] != '"')
									{
										i++;
									}
								}
								if (arrayList3.Count != 0)
								{
									responseCacheControl.PrivateHeaders = (string[])arrayList3.ToArray(typeof(string));
								}
							}
							else if (Rfc2616.Common.UnsafeAsciiLettersNoCaseEqual(ptr, i, length, "no-cache"))
							{
								responseCacheControl.NoCache = true;
								i += 7;
								while (i < length && ptr[i] == ' ')
								{
									i++;
								}
								if (i >= length || ptr[i] != '=')
								{
									i--;
									break;
								}
								while (i < length && ptr[(IntPtr)(++i) * 2] == ' ')
								{
								}
								if (i >= length || ptr[i] != '"')
								{
									i--;
									break;
								}
								ArrayList arrayList4 = new ArrayList();
								i++;
								while (i < length && ptr[i] != '"')
								{
									while (i < length && ptr[i] == ' ')
									{
										i++;
									}
									int num5 = i;
									while (i < length && ptr[i] != ' ' && ptr[i] != ',' && ptr[i] != '"')
									{
										i++;
									}
									if (num5 != i)
									{
										arrayList4.Add(s.Substring(num5, i - num5));
									}
									while (i < length && ptr[i] != ',' && ptr[i] != '"')
									{
										i++;
									}
								}
								if (arrayList4.Count != 0)
								{
									responseCacheControl.NoCacheHeaders = (string[])arrayList4.ToArray(typeof(string));
								}
							}
							else if (Rfc2616.Common.UnsafeAsciiLettersNoCaseEqual(ptr, i, length, "no-store"))
							{
								responseCacheControl.NoStore = true;
								i += 7;
							}
							else if (Rfc2616.Common.UnsafeAsciiLettersNoCaseEqual(ptr, i, length, "must-revalidate"))
							{
								responseCacheControl.MustRevalidate = true;
								i += 14;
							}
							else if (Rfc2616.Common.UnsafeAsciiLettersNoCaseEqual(ptr, i, length, "max-age"))
							{
								i += 7;
								while (i < length && ptr[i] == ' ')
								{
									i++;
								}
								if (i == length || ptr[(IntPtr)(i++) * 2] != '=')
								{
									return;
								}
								while (i < length && ptr[i] == ' ')
								{
									i++;
								}
								if (i == length)
								{
									return;
								}
								responseCacheControl.MaxAge = 0;
								while (i < length && ptr[i] >= '0' && ptr[i] <= '9')
								{
									responseCacheControl.MaxAge = responseCacheControl.MaxAge * 10 + (int)(ptr[(IntPtr)(i++) * 2] - '0');
								}
								i--;
							}
							else if (Rfc2616.Common.UnsafeAsciiLettersNoCaseEqual(ptr, i, length, "smax-age"))
							{
								i += 8;
								while (i < length && ptr[i] == ' ')
								{
									i++;
								}
								if (i == length || ptr[(IntPtr)(i++) * 2] != '=')
								{
									return;
								}
								while (i < length && ptr[i] == ' ')
								{
									i++;
								}
								if (i == length)
								{
									return;
								}
								responseCacheControl.SMaxAge = 0;
								while (i < length && ptr[i] >= '0' && ptr[i] <= '9')
								{
									responseCacheControl.SMaxAge = responseCacheControl.SMaxAge * 10 + (int)(ptr[(IntPtr)(i++) * 2] - '0');
								}
								i--;
							}
						}
					}
				}
			}
		}

		// Token: 0x06002A25 RID: 10789 RVA: 0x000B143C File Offset: 0x000B043C
		private void RemoveWarnings_1xx()
		{
			string[] values = this.CacheHeaders.GetValues("Warning");
			if (values == null)
			{
				return;
			}
			ArrayList arrayList = new ArrayList();
			HttpRequestCacheValidator.ParseHeaderValues(values, HttpRequestCacheValidator.ParseWarningsCallback, arrayList);
			this.CacheHeaders.Remove("Warning");
			for (int i = 0; i < arrayList.Count; i++)
			{
				this.CacheHeaders.Add("Warning", (string)arrayList[i]);
			}
		}

		// Token: 0x06002A26 RID: 10790 RVA: 0x000B14AD File Offset: 0x000B04AD
		private static void ParseWarningsCallbackMethod(string s, int start, int end, IList list)
		{
			if (end >= start && s[start] != '1')
			{
				HttpRequestCacheValidator.ParseValuesCallbackMethod(s, start, end, list);
			}
		}

		// Token: 0x06002A27 RID: 10791 RVA: 0x000B14C7 File Offset: 0x000B04C7
		private static void ParseValuesCallbackMethod(string s, int start, int end, IList list)
		{
			while (end >= start && s[end] == ' ')
			{
				end--;
			}
			if (end >= start)
			{
				list.Add(s.Substring(start, end - start + 1));
			}
		}

		// Token: 0x06002A28 RID: 10792 RVA: 0x000B14F8 File Offset: 0x000B04F8
		internal static void ParseHeaderValues(string[] values, HttpRequestCacheValidator.ParseCallback calback, IList list)
		{
			if (values == null)
			{
				return;
			}
			foreach (string text in values)
			{
				int j = 0;
				int num = 0;
				while (j < text.Length)
				{
					while (num < text.Length && text[num] == ' ')
					{
						num++;
					}
					if (num != text.Length)
					{
						j = num;
						for (;;)
						{
							if (j >= text.Length || text[j] == ',' || text[j] == '"')
							{
								if (j == text.Length)
								{
									goto Block_6;
								}
								if (text[j] != '"')
								{
									break;
								}
								while (++j < text.Length && text[j] != '"')
								{
								}
								if (j == text.Length)
								{
									goto Block_8;
								}
							}
							else
							{
								j++;
							}
						}
						calback(text, num, j - 1, list);
						while (++j < text.Length && text[j] == ' ')
						{
						}
						if (j < text.Length)
						{
							num = j;
							continue;
						}
						break;
						Block_6:
						calback(text, num, j - 1, list);
						break;
						Block_8:
						calback(text, num, j - 1, list);
						break;
					}
					break;
				}
			}
		}

		// Token: 0x0400289E RID: 10398
		internal const string Warning_110 = "110 Response is stale";

		// Token: 0x0400289F RID: 10399
		internal const string Warning_111 = "111 Revalidation failed";

		// Token: 0x040028A0 RID: 10400
		internal const string Warning_112 = "112 Disconnected operation";

		// Token: 0x040028A1 RID: 10401
		internal const string Warning_113 = "113 Heuristic expiration";

		// Token: 0x040028A2 RID: 10402
		private const long LO = 9007336695791648L;

		// Token: 0x040028A3 RID: 10403
		private const int LOI = 2097184;

		// Token: 0x040028A4 RID: 10404
		private const long _prox = 33777473954119792L;

		// Token: 0x040028A5 RID: 10405
		private const long _y_re = 28429462276997241L;

		// Token: 0x040028A6 RID: 10406
		private const long _vali = 29555336417443958L;

		// Token: 0x040028A7 RID: 10407
		private const long _date = 28429470870339684L;

		// Token: 0x040028A8 RID: 10408
		private const long _publ = 30399718399213680L;

		// Token: 0x040028A9 RID: 10409
		private const int _ic = 6488169;

		// Token: 0x040028AA RID: 10410
		private const long _priv = 33214498230894704L;

		// Token: 0x040028AB RID: 10411
		private const int _at = 7602273;

		// Token: 0x040028AC RID: 10412
		private const long _no_c = 27866215975157870L;

		// Token: 0x040028AD RID: 10413
		private const long _ache = 28429419330863201L;

		// Token: 0x040028AE RID: 10414
		private const long _no_s = 32369815602528366L;

		// Token: 0x040028AF RID: 10415
		private const long _tore = 28429462281322612L;

		// Token: 0x040028B0 RID: 10416
		private const long _must = 32651591227342957L;

		// Token: 0x040028B1 RID: 10417
		private const long __rev = 33214481051025453L;

		// Token: 0x040028B2 RID: 10418
		private const long _alid = 28147948649709665L;

		// Token: 0x040028B3 RID: 10419
		private const long _max_ = 12666889354412141L;

		// Token: 0x040028B4 RID: 10420
		private const int _ag = 6750305;

		// Token: 0x040028B5 RID: 10421
		private const long _s_ma = 27303540895318131L;

		// Token: 0x040028B6 RID: 10422
		private const long _xage = 28429415035764856L;

		// Token: 0x040028B7 RID: 10423
		private HttpRequestCachePolicy m_HttpPolicy;

		// Token: 0x040028B8 RID: 10424
		private HttpStatusCode m_StatusCode;

		// Token: 0x040028B9 RID: 10425
		private string m_StatusDescription;

		// Token: 0x040028BA RID: 10426
		private Version m_HttpVersion;

		// Token: 0x040028BB RID: 10427
		private WebHeaderCollection m_Headers;

		// Token: 0x040028BC RID: 10428
		private NameValueCollection m_SystemMeta;

		// Token: 0x040028BD RID: 10429
		private bool m_DontUpdateHeaders;

		// Token: 0x040028BE RID: 10430
		private bool m_HeuristicExpiration;

		// Token: 0x040028BF RID: 10431
		private HttpRequestCacheValidator.Vars m_CacheVars;

		// Token: 0x040028C0 RID: 10432
		private HttpRequestCacheValidator.Vars m_ResponseVars;

		// Token: 0x040028C1 RID: 10433
		private HttpRequestCacheValidator.RequestVars m_RequestVars;

		// Token: 0x040028C2 RID: 10434
		private static readonly HttpRequestCacheValidator.ParseCallback ParseWarningsCallback = new HttpRequestCacheValidator.ParseCallback(HttpRequestCacheValidator.ParseWarningsCallbackMethod);

		// Token: 0x040028C3 RID: 10435
		internal static readonly HttpRequestCacheValidator.ParseCallback ParseValuesCallback = new HttpRequestCacheValidator.ParseCallback(HttpRequestCacheValidator.ParseValuesCallbackMethod);

		// Token: 0x0200055E RID: 1374
		private struct RequestVars
		{
			// Token: 0x040028C4 RID: 10436
			internal HttpMethod Method;

			// Token: 0x040028C5 RID: 10437
			internal bool IsCacheRange;

			// Token: 0x040028C6 RID: 10438
			internal bool IsUserRange;

			// Token: 0x040028C7 RID: 10439
			internal string IfHeader1;

			// Token: 0x040028C8 RID: 10440
			internal string Validator1;

			// Token: 0x040028C9 RID: 10441
			internal string IfHeader2;

			// Token: 0x040028CA RID: 10442
			internal string Validator2;
		}

		// Token: 0x0200055F RID: 1375
		private struct Vars
		{
			// Token: 0x06002A2A RID: 10794 RVA: 0x000B1628 File Offset: 0x000B0628
			internal void Initialize()
			{
				this.EntityLength = (this.RangeStart = (this.RangeEnd = -1L));
				this.Date = DateTime.MinValue;
				this.Expires = DateTime.MinValue;
				this.LastModified = DateTime.MinValue;
				this.Age = TimeSpan.MinValue;
				this.MaxAge = TimeSpan.MinValue;
			}

			// Token: 0x040028CB RID: 10443
			internal DateTime Date;

			// Token: 0x040028CC RID: 10444
			internal DateTime Expires;

			// Token: 0x040028CD RID: 10445
			internal DateTime LastModified;

			// Token: 0x040028CE RID: 10446
			internal long EntityLength;

			// Token: 0x040028CF RID: 10447
			internal TimeSpan Age;

			// Token: 0x040028D0 RID: 10448
			internal TimeSpan MaxAge;

			// Token: 0x040028D1 RID: 10449
			internal ResponseCacheControl CacheControl;

			// Token: 0x040028D2 RID: 10450
			internal long RangeStart;

			// Token: 0x040028D3 RID: 10451
			internal long RangeEnd;
		}

		// Token: 0x02000560 RID: 1376
		// (Invoke) Token: 0x06002A2C RID: 10796
		internal delegate void ParseCallback(string s, int start, int end, IList list);
	}
}
