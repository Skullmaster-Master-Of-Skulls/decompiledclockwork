using System;
using System.Collections;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace System.Net.Cache
{
	// Token: 0x02000309 RID: 777
	internal class HttpRequestCacheValidator : RequestCacheValidator
	{
		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x06001B85 RID: 7045 RVA: 0x000822D4 File Offset: 0x000804D4
		// (set) Token: 0x06001B86 RID: 7046 RVA: 0x000822DC File Offset: 0x000804DC
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

		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x06001B87 RID: 7047 RVA: 0x000822E5 File Offset: 0x000804E5
		// (set) Token: 0x06001B88 RID: 7048 RVA: 0x000822ED File Offset: 0x000804ED
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

		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x06001B89 RID: 7049 RVA: 0x000822F6 File Offset: 0x000804F6
		// (set) Token: 0x06001B8A RID: 7050 RVA: 0x000822FE File Offset: 0x000804FE
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

		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x06001B8B RID: 7051 RVA: 0x00082307 File Offset: 0x00080507
		// (set) Token: 0x06001B8C RID: 7052 RVA: 0x0008230F File Offset: 0x0008050F
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

		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x06001B8D RID: 7053 RVA: 0x00082318 File Offset: 0x00080518
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

		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x06001B8E RID: 7054 RVA: 0x00082370 File Offset: 0x00080570
		// (set) Token: 0x06001B8F RID: 7055 RVA: 0x00082378 File Offset: 0x00080578
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

		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x06001B90 RID: 7056 RVA: 0x00082381 File Offset: 0x00080581
		// (set) Token: 0x06001B91 RID: 7057 RVA: 0x0008238E File Offset: 0x0008058E
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

		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x06001B92 RID: 7058 RVA: 0x0008239C File Offset: 0x0008059C
		// (set) Token: 0x06001B93 RID: 7059 RVA: 0x000823A9 File Offset: 0x000805A9
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

		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x06001B94 RID: 7060 RVA: 0x000823B7 File Offset: 0x000805B7
		// (set) Token: 0x06001B95 RID: 7061 RVA: 0x000823C4 File Offset: 0x000805C4
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

		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x06001B96 RID: 7062 RVA: 0x000823D2 File Offset: 0x000805D2
		// (set) Token: 0x06001B97 RID: 7063 RVA: 0x000823DF File Offset: 0x000805DF
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

		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x06001B98 RID: 7064 RVA: 0x000823ED File Offset: 0x000805ED
		// (set) Token: 0x06001B99 RID: 7065 RVA: 0x000823FA File Offset: 0x000805FA
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

		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x06001B9A RID: 7066 RVA: 0x00082408 File Offset: 0x00080608
		// (set) Token: 0x06001B9B RID: 7067 RVA: 0x00082415 File Offset: 0x00080615
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

		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x06001B9C RID: 7068 RVA: 0x00082423 File Offset: 0x00080623
		// (set) Token: 0x06001B9D RID: 7069 RVA: 0x00082430 File Offset: 0x00080630
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

		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x06001B9E RID: 7070 RVA: 0x0008243E File Offset: 0x0008063E
		// (set) Token: 0x06001B9F RID: 7071 RVA: 0x00082446 File Offset: 0x00080646
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

		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x06001BA0 RID: 7072 RVA: 0x0008244F File Offset: 0x0008064F
		// (set) Token: 0x06001BA1 RID: 7073 RVA: 0x0008245C File Offset: 0x0008065C
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

		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x06001BA2 RID: 7074 RVA: 0x0008246A File Offset: 0x0008066A
		// (set) Token: 0x06001BA3 RID: 7075 RVA: 0x00082477 File Offset: 0x00080677
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

		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x06001BA4 RID: 7076 RVA: 0x00082485 File Offset: 0x00080685
		// (set) Token: 0x06001BA5 RID: 7077 RVA: 0x00082492 File Offset: 0x00080692
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

		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x06001BA6 RID: 7078 RVA: 0x000824A0 File Offset: 0x000806A0
		// (set) Token: 0x06001BA7 RID: 7079 RVA: 0x000824AD File Offset: 0x000806AD
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

		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x06001BA8 RID: 7080 RVA: 0x000824BB File Offset: 0x000806BB
		// (set) Token: 0x06001BA9 RID: 7081 RVA: 0x000824C8 File Offset: 0x000806C8
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

		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x06001BAA RID: 7082 RVA: 0x000824D6 File Offset: 0x000806D6
		// (set) Token: 0x06001BAB RID: 7083 RVA: 0x000824E3 File Offset: 0x000806E3
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

		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x06001BAC RID: 7084 RVA: 0x000824F1 File Offset: 0x000806F1
		// (set) Token: 0x06001BAD RID: 7085 RVA: 0x000824F9 File Offset: 0x000806F9
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

		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x06001BAE RID: 7086 RVA: 0x00082502 File Offset: 0x00080702
		// (set) Token: 0x06001BAF RID: 7087 RVA: 0x0008250F File Offset: 0x0008070F
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

		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x06001BB0 RID: 7088 RVA: 0x0008251D File Offset: 0x0008071D
		// (set) Token: 0x06001BB1 RID: 7089 RVA: 0x0008252A File Offset: 0x0008072A
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

		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x06001BB2 RID: 7090 RVA: 0x00082538 File Offset: 0x00080738
		// (set) Token: 0x06001BB3 RID: 7091 RVA: 0x00082545 File Offset: 0x00080745
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

		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x06001BB4 RID: 7092 RVA: 0x00082553 File Offset: 0x00080753
		// (set) Token: 0x06001BB5 RID: 7093 RVA: 0x00082560 File Offset: 0x00080760
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

		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x06001BB6 RID: 7094 RVA: 0x0008256E File Offset: 0x0008076E
		// (set) Token: 0x06001BB7 RID: 7095 RVA: 0x0008257B File Offset: 0x0008077B
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

		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x06001BB8 RID: 7096 RVA: 0x00082589 File Offset: 0x00080789
		// (set) Token: 0x06001BB9 RID: 7097 RVA: 0x00082596 File Offset: 0x00080796
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

		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x06001BBA RID: 7098 RVA: 0x000825A4 File Offset: 0x000807A4
		// (set) Token: 0x06001BBB RID: 7099 RVA: 0x000825B1 File Offset: 0x000807B1
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

		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x06001BBC RID: 7100 RVA: 0x000825BF File Offset: 0x000807BF
		// (set) Token: 0x06001BBD RID: 7101 RVA: 0x000825CC File Offset: 0x000807CC
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

		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x06001BBE RID: 7102 RVA: 0x000825DA File Offset: 0x000807DA
		// (set) Token: 0x06001BBF RID: 7103 RVA: 0x000825E7 File Offset: 0x000807E7
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

		// Token: 0x06001BC0 RID: 7104 RVA: 0x000825F8 File Offset: 0x000807F8
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

		// Token: 0x06001BC1 RID: 7105 RVA: 0x00082677 File Offset: 0x00080877
		internal override RequestCacheValidator CreateValidator()
		{
			return new HttpRequestCacheValidator(base.StrictCacheErrors, base.UnspecifiedMaxAge);
		}

		// Token: 0x06001BC2 RID: 7106 RVA: 0x0008268A File Offset: 0x0008088A
		internal HttpRequestCacheValidator(bool strictCacheErrors, TimeSpan unspecifiedMaxAge) : base(strictCacheErrors, unspecifiedMaxAge)
		{
		}

		// Token: 0x06001BC3 RID: 7107 RVA: 0x00082694 File Offset: 0x00080894
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
			uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
			if (num <= 1929554311U)
			{
				if (num <= 811237315U)
				{
					if (num != 746199118U)
					{
						if (num == 811237315U)
						{
							if (text == "HEAD")
							{
								this.RequestMethod = HttpMethod.Head;
								goto IL_190;
							}
						}
					}
					else if (text == "TRACE")
					{
						this.RequestMethod = HttpMethod.Trace;
						goto IL_190;
					}
				}
				else if (num != 827600069U)
				{
					if (num == 1929554311U)
					{
						if (text == "POST")
						{
							this.RequestMethod = HttpMethod.Post;
							goto IL_190;
						}
					}
				}
				else if (text == "OPTIONS")
				{
					this.RequestMethod = HttpMethod.Options;
					goto IL_190;
				}
			}
			else if (num <= 2531704439U)
			{
				if (num != 2016099545U)
				{
					if (num == 2531704439U)
					{
						if (text == "GET")
						{
							this.RequestMethod = HttpMethod.Get;
							goto IL_190;
						}
					}
				}
				else if (text == "CONNECT")
				{
					this.RequestMethod = HttpMethod.Connect;
					goto IL_190;
				}
			}
			else if (num != 3995708942U)
			{
				if (num == 4168191690U)
				{
					if (text == "DELETE")
					{
						this.RequestMethod = HttpMethod.Delete;
						goto IL_190;
					}
				}
			}
			else if (text == "PUT")
			{
				this.RequestMethod = HttpMethod.Put;
				goto IL_190;
			}
			this.RequestMethod = HttpMethod.Other;
			IL_190:
			return Rfc2616.OnValidateRequest(this);
		}

		// Token: 0x06001BC4 RID: 7108 RVA: 0x00082838 File Offset: 0x00080A38
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
			this.CreateCacheHeaders(this.CacheStatusCode > (HttpStatusCode)0);
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

		// Token: 0x06001BC5 RID: 7109 RVA: 0x00082930 File Offset: 0x00080B30
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

		// Token: 0x06001BC6 RID: 7110 RVA: 0x00082AE0 File Offset: 0x00080CE0
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

		// Token: 0x06001BC7 RID: 7111 RVA: 0x00082C2C File Offset: 0x00080E2C
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
				Logging.PrintInfo(Logging.RequestCache, string.Concat(new string[]
				{
					"StatusCode=",
					((int)httpWebResponse.StatusCode).ToString(CultureInfo.InvariantCulture),
					" ",
					httpWebResponse.StatusCode.ToString(),
					(httpWebResponse.StatusCode == HttpStatusCode.PartialContent) ? (", Content-Range: " + httpWebResponse.Headers["Content-Range"]) : string.Empty
				}));
			}
			return Rfc2616.OnValidateResponse(this);
		}

		// Token: 0x06001BC8 RID: 7112 RVA: 0x00082D5C File Offset: 0x00080F5C
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

		// Token: 0x06001BC9 RID: 7113 RVA: 0x00082E3C File Offset: 0x0008103C
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

		// Token: 0x06001BCA RID: 7114 RVA: 0x00082FF4 File Offset: 0x000811F4
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

		// Token: 0x06001BCB RID: 7115 RVA: 0x000830AC File Offset: 0x000812AC
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

		// Token: 0x06001BCC RID: 7116 RVA: 0x00083268 File Offset: 0x00081468
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

		// Token: 0x06001BCD RID: 7117 RVA: 0x00083318 File Offset: 0x00081518
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

		// Token: 0x06001BCE RID: 7118 RVA: 0x000833D0 File Offset: 0x000815D0
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
					while (++num < text2.Length && (text2[num] == ' ' || text2[num] == '\t'))
					{
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

		// Token: 0x06001BCF RID: 7119 RVA: 0x000834E8 File Offset: 0x000816E8
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

		// Token: 0x06001BD0 RID: 7120 RVA: 0x000837E0 File Offset: 0x000819E0
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
				fixed (string text = s)
				{
					char* ptr = text;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
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

		// Token: 0x06001BD1 RID: 7121 RVA: 0x00084438 File Offset: 0x00082638
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

		// Token: 0x06001BD2 RID: 7122 RVA: 0x000844A9 File Offset: 0x000826A9
		private static void ParseWarningsCallbackMethod(string s, int start, int end, IList list)
		{
			if (end >= start && s[start] != '1')
			{
				HttpRequestCacheValidator.ParseValuesCallbackMethod(s, start, end, list);
			}
		}

		// Token: 0x06001BD3 RID: 7123 RVA: 0x000844C3 File Offset: 0x000826C3
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

		// Token: 0x06001BD4 RID: 7124 RVA: 0x000844F4 File Offset: 0x000826F4
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

		// Token: 0x04001B04 RID: 6916
		internal const string Warning_110 = "110 Response is stale";

		// Token: 0x04001B05 RID: 6917
		internal const string Warning_111 = "111 Revalidation failed";

		// Token: 0x04001B06 RID: 6918
		internal const string Warning_112 = "112 Disconnected operation";

		// Token: 0x04001B07 RID: 6919
		internal const string Warning_113 = "113 Heuristic expiration";

		// Token: 0x04001B08 RID: 6920
		private HttpRequestCachePolicy m_HttpPolicy;

		// Token: 0x04001B09 RID: 6921
		private HttpStatusCode m_StatusCode;

		// Token: 0x04001B0A RID: 6922
		private string m_StatusDescription;

		// Token: 0x04001B0B RID: 6923
		private Version m_HttpVersion;

		// Token: 0x04001B0C RID: 6924
		private WebHeaderCollection m_Headers;

		// Token: 0x04001B0D RID: 6925
		private NameValueCollection m_SystemMeta;

		// Token: 0x04001B0E RID: 6926
		private bool m_DontUpdateHeaders;

		// Token: 0x04001B0F RID: 6927
		private bool m_HeuristicExpiration;

		// Token: 0x04001B10 RID: 6928
		private HttpRequestCacheValidator.Vars m_CacheVars;

		// Token: 0x04001B11 RID: 6929
		private HttpRequestCacheValidator.Vars m_ResponseVars;

		// Token: 0x04001B12 RID: 6930
		private HttpRequestCacheValidator.RequestVars m_RequestVars;

		// Token: 0x04001B13 RID: 6931
		private const long LO = 9007336695791648L;

		// Token: 0x04001B14 RID: 6932
		private const int LOI = 2097184;

		// Token: 0x04001B15 RID: 6933
		private const long _prox = 33777473954119792L;

		// Token: 0x04001B16 RID: 6934
		private const long _y_re = 28429462276997241L;

		// Token: 0x04001B17 RID: 6935
		private const long _vali = 29555336417443958L;

		// Token: 0x04001B18 RID: 6936
		private const long _date = 28429470870339684L;

		// Token: 0x04001B19 RID: 6937
		private const long _publ = 30399718399213680L;

		// Token: 0x04001B1A RID: 6938
		private const int _ic = 6488169;

		// Token: 0x04001B1B RID: 6939
		private const long _priv = 33214498230894704L;

		// Token: 0x04001B1C RID: 6940
		private const int _at = 7602273;

		// Token: 0x04001B1D RID: 6941
		private const long _no_c = 27866215975157870L;

		// Token: 0x04001B1E RID: 6942
		private const long _ache = 28429419330863201L;

		// Token: 0x04001B1F RID: 6943
		private const long _no_s = 32369815602528366L;

		// Token: 0x04001B20 RID: 6944
		private const long _tore = 28429462281322612L;

		// Token: 0x04001B21 RID: 6945
		private const long _must = 32651591227342957L;

		// Token: 0x04001B22 RID: 6946
		private const long __rev = 33214481051025453L;

		// Token: 0x04001B23 RID: 6947
		private const long _alid = 28147948649709665L;

		// Token: 0x04001B24 RID: 6948
		private const long _max_ = 12666889354412141L;

		// Token: 0x04001B25 RID: 6949
		private const int _ag = 6750305;

		// Token: 0x04001B26 RID: 6950
		private const long _s_ma = 27303540895318131L;

		// Token: 0x04001B27 RID: 6951
		private const long _xage = 28429415035764856L;

		// Token: 0x04001B28 RID: 6952
		private static readonly HttpRequestCacheValidator.ParseCallback ParseWarningsCallback = new HttpRequestCacheValidator.ParseCallback(HttpRequestCacheValidator.ParseWarningsCallbackMethod);

		// Token: 0x04001B29 RID: 6953
		internal static readonly HttpRequestCacheValidator.ParseCallback ParseValuesCallback = new HttpRequestCacheValidator.ParseCallback(HttpRequestCacheValidator.ParseValuesCallbackMethod);

		// Token: 0x020007B0 RID: 1968
		private struct RequestVars
		{
			// Token: 0x0400340B RID: 13323
			internal HttpMethod Method;

			// Token: 0x0400340C RID: 13324
			internal bool IsCacheRange;

			// Token: 0x0400340D RID: 13325
			internal bool IsUserRange;

			// Token: 0x0400340E RID: 13326
			internal string IfHeader1;

			// Token: 0x0400340F RID: 13327
			internal string Validator1;

			// Token: 0x04003410 RID: 13328
			internal string IfHeader2;

			// Token: 0x04003411 RID: 13329
			internal string Validator2;
		}

		// Token: 0x020007B1 RID: 1969
		private struct Vars
		{
			// Token: 0x06004335 RID: 17205 RVA: 0x00119F08 File Offset: 0x00118108
			internal void Initialize()
			{
				this.EntityLength = (this.RangeStart = (this.RangeEnd = -1L));
				this.Date = DateTime.MinValue;
				this.Expires = DateTime.MinValue;
				this.LastModified = DateTime.MinValue;
				this.Age = TimeSpan.MinValue;
				this.MaxAge = TimeSpan.MinValue;
			}

			// Token: 0x04003412 RID: 13330
			internal DateTime Date;

			// Token: 0x04003413 RID: 13331
			internal DateTime Expires;

			// Token: 0x04003414 RID: 13332
			internal DateTime LastModified;

			// Token: 0x04003415 RID: 13333
			internal long EntityLength;

			// Token: 0x04003416 RID: 13334
			internal TimeSpan Age;

			// Token: 0x04003417 RID: 13335
			internal TimeSpan MaxAge;

			// Token: 0x04003418 RID: 13336
			internal ResponseCacheControl CacheControl;

			// Token: 0x04003419 RID: 13337
			internal long RangeStart;

			// Token: 0x0400341A RID: 13338
			internal long RangeEnd;
		}

		// Token: 0x020007B2 RID: 1970
		// (Invoke) Token: 0x06004337 RID: 17207
		internal delegate void ParseCallback(string s, int start, int end, IList list);
	}
}
