using System;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;

namespace System.Net.Cache
{
	// Token: 0x02000563 RID: 1379
	internal class FtpRequestCacheValidator : HttpRequestCacheValidator
	{
		// Token: 0x170008B5 RID: 2229
		// (get) Token: 0x06002A32 RID: 10802 RVA: 0x000B1879 File Offset: 0x000B0879
		private bool HttpProxyMode
		{
			get
			{
				return this.m_HttpProxyMode;
			}
		}

		// Token: 0x170008B6 RID: 2230
		// (get) Token: 0x06002A33 RID: 10803 RVA: 0x000B1881 File Offset: 0x000B0881
		internal new RequestCachePolicy Policy
		{
			get
			{
				return base.Policy;
			}
		}

		// Token: 0x06002A34 RID: 10804 RVA: 0x000B1889 File Offset: 0x000B0889
		private void ZeroPrivateVars()
		{
			this.m_LastModified = DateTime.MinValue;
			this.m_HttpProxyMode = false;
		}

		// Token: 0x06002A35 RID: 10805 RVA: 0x000B189D File Offset: 0x000B089D
		internal override RequestCacheValidator CreateValidator()
		{
			return new FtpRequestCacheValidator(base.StrictCacheErrors, base.UnspecifiedMaxAge);
		}

		// Token: 0x06002A36 RID: 10806 RVA: 0x000B18B0 File Offset: 0x000B08B0
		internal FtpRequestCacheValidator(bool strictCacheErrors, TimeSpan unspecifiedMaxAge) : base(strictCacheErrors, unspecifiedMaxAge)
		{
		}

		// Token: 0x06002A37 RID: 10807 RVA: 0x000B18BC File Offset: 0x000B08BC
		protected internal override CacheValidationStatus ValidateRequest()
		{
			this.ZeroPrivateVars();
			if (base.Request is HttpWebRequest)
			{
				this.m_HttpProxyMode = true;
				if (Logging.On)
				{
					Logging.PrintInfo(Logging.RequestCache, SR.GetString("net_log_cache_ftp_proxy_doesnt_support_partial"));
				}
				return base.ValidateRequest();
			}
			if (this.Policy.Level == RequestCacheLevel.BypassCache)
			{
				return CacheValidationStatus.DoNotUseCache;
			}
			string text = base.Request.Method.ToUpper(CultureInfo.InvariantCulture);
			if (Logging.On)
			{
				Logging.PrintInfo(Logging.RequestCache, SR.GetString("net_log_cache_ftp_method", new object[]
				{
					text
				}));
			}
			string a;
			if ((a = text) != null)
			{
				if (a == "RETR")
				{
					base.RequestMethod = HttpMethod.Get;
					goto IL_105;
				}
				if (a == "STOR")
				{
					base.RequestMethod = HttpMethod.Put;
					goto IL_105;
				}
				if (a == "APPE")
				{
					base.RequestMethod = HttpMethod.Put;
					goto IL_105;
				}
				if (a == "RENAME")
				{
					base.RequestMethod = HttpMethod.Put;
					goto IL_105;
				}
				if (a == "DELE")
				{
					base.RequestMethod = HttpMethod.Delete;
					goto IL_105;
				}
			}
			base.RequestMethod = HttpMethod.Other;
			IL_105:
			if ((base.RequestMethod != HttpMethod.Get || !((FtpWebRequest)base.Request).UseBinary) && this.Policy.Level == RequestCacheLevel.CacheOnly)
			{
				this.FailRequest(WebExceptionStatus.RequestProhibitedByCachePolicy);
			}
			if (text != "RETR")
			{
				return CacheValidationStatus.DoNotTakeFromCache;
			}
			if (!((FtpWebRequest)base.Request).UseBinary)
			{
				if (Logging.On)
				{
					Logging.PrintWarning(Logging.RequestCache, SR.GetString("net_log_cache_ftp_supports_bin_only"));
				}
				return CacheValidationStatus.DoNotUseCache;
			}
			if (this.Policy.Level >= RequestCacheLevel.Reload)
			{
				return CacheValidationStatus.DoNotTakeFromCache;
			}
			return CacheValidationStatus.Continue;
		}

		// Token: 0x06002A38 RID: 10808 RVA: 0x000B1A50 File Offset: 0x000B0A50
		protected internal override CacheFreshnessStatus ValidateFreshness()
		{
			if (this.HttpProxyMode)
			{
				if (base.CacheStream != Stream.Null)
				{
					if (Logging.On)
					{
						Logging.PrintInfo(Logging.RequestCache, SR.GetString("net_log_cache_replacing_entry_with_HTTP_200"));
					}
					if (base.CacheEntry.EntryMetadata == null)
					{
						base.CacheEntry.EntryMetadata = new StringCollection();
					}
					base.CacheEntry.EntryMetadata.Clear();
					base.CacheEntry.EntryMetadata.Add("HTTP/1.1 200 OK");
				}
				return base.ValidateFreshness();
			}
			DateTime utcNow = DateTime.UtcNow;
			if (Logging.On)
			{
				Logging.PrintInfo(Logging.RequestCache, SR.GetString("net_log_cache_now_time", new object[]
				{
					utcNow.ToString("r", CultureInfo.InvariantCulture)
				}));
			}
			if (base.CacheEntry.ExpiresUtc != DateTime.MinValue)
			{
				if (Logging.On)
				{
					Logging.PrintWarning(Logging.RequestCache, SR.GetString("net_log_cache_max_age_absolute", new object[]
					{
						base.CacheEntry.ExpiresUtc.ToString("r", CultureInfo.InvariantCulture)
					}));
				}
				if (base.CacheEntry.ExpiresUtc < utcNow)
				{
					return CacheFreshnessStatus.Stale;
				}
				return CacheFreshnessStatus.Fresh;
			}
			else
			{
				TimeSpan t = TimeSpan.MaxValue;
				if (base.CacheEntry.LastSynchronizedUtc != DateTime.MinValue)
				{
					t = utcNow - base.CacheEntry.LastSynchronizedUtc;
					if (Logging.On)
					{
						Logging.PrintInfo(Logging.RequestCache, SR.GetString("net_log_cache_age1", new object[]
						{
							((int)t.TotalSeconds).ToString(NumberFormatInfo.InvariantInfo),
							base.CacheEntry.LastSynchronizedUtc.ToString("r", CultureInfo.InvariantCulture)
						}));
					}
				}
				if (base.CacheEntry.LastModifiedUtc != DateTime.MinValue)
				{
					int num = (int)((utcNow - base.CacheEntry.LastModifiedUtc).TotalSeconds / 10.0);
					if (Logging.On)
					{
						Logging.PrintInfo(Logging.RequestCache, SR.GetString("net_log_cache_no_max_age_use_10_percent", new object[]
						{
							num.ToString(NumberFormatInfo.InvariantInfo),
							base.CacheEntry.LastModifiedUtc.ToString("r", CultureInfo.InvariantCulture)
						}));
					}
					if (t.TotalSeconds < (double)num)
					{
						return CacheFreshnessStatus.Fresh;
					}
					return CacheFreshnessStatus.Stale;
				}
				else
				{
					if (Logging.On)
					{
						Logging.PrintWarning(Logging.RequestCache, SR.GetString("net_log_cache_no_max_age_use_default", new object[]
						{
							((int)base.UnspecifiedMaxAge.TotalSeconds).ToString(NumberFormatInfo.InvariantInfo)
						}));
					}
					if (base.UnspecifiedMaxAge >= t)
					{
						return CacheFreshnessStatus.Fresh;
					}
					return CacheFreshnessStatus.Stale;
				}
			}
		}

		// Token: 0x06002A39 RID: 10809 RVA: 0x000B1D1C File Offset: 0x000B0D1C
		protected internal override CacheValidationStatus ValidateCache()
		{
			if (this.HttpProxyMode)
			{
				return base.ValidateCache();
			}
			if (this.Policy.Level >= RequestCacheLevel.Reload)
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
			if (base.CacheStream == Stream.Null || base.CacheEntry.IsPartialEntry)
			{
				if (this.Policy.Level == RequestCacheLevel.CacheOnly)
				{
					this.FailRequest(WebExceptionStatus.CacheEntryNotFound);
				}
				if (base.CacheStream == Stream.Null)
				{
					return CacheValidationStatus.DoNotTakeFromCache;
				}
			}
			base.CacheStreamOffset = 0L;
			base.CacheStreamLength = base.CacheEntry.StreamSize;
			if (this.Policy.Level == RequestCacheLevel.Revalidate || base.CacheEntry.IsPartialEntry)
			{
				return this.TryConditionalRequest();
			}
			long num = (base.Request is FtpWebRequest) ? ((FtpWebRequest)base.Request).ContentOffset : 0L;
			if (base.CacheFreshnessStatus == CacheFreshnessStatus.Fresh || this.Policy.Level == RequestCacheLevel.CacheOnly || this.Policy.Level == RequestCacheLevel.CacheIfAvailable)
			{
				if (num != 0L)
				{
					if (num >= base.CacheStreamLength)
					{
						if (this.Policy.Level == RequestCacheLevel.CacheOnly)
						{
							this.FailRequest(WebExceptionStatus.CacheEntryNotFound);
						}
						return CacheValidationStatus.DoNotTakeFromCache;
					}
					base.CacheStreamOffset = num;
				}
				return CacheValidationStatus.ReturnCachedResponse;
			}
			return CacheValidationStatus.DoNotTakeFromCache;
		}

		// Token: 0x06002A3A RID: 10810 RVA: 0x000B1E6C File Offset: 0x000B0E6C
		protected internal override CacheValidationStatus RevalidateCache()
		{
			if (this.HttpProxyMode)
			{
				return base.RevalidateCache();
			}
			if (this.Policy.Level >= RequestCacheLevel.Reload)
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
			if (base.CacheStream == Stream.Null)
			{
				return CacheValidationStatus.DoNotTakeFromCache;
			}
			FtpWebResponse ftpWebResponse = base.Response as FtpWebResponse;
			if (ftpWebResponse == null)
			{
				return CacheValidationStatus.DoNotTakeFromCache;
			}
			CacheValidationStatus result;
			if (ftpWebResponse.StatusCode == FtpStatusCode.FileStatus)
			{
				if (Logging.On)
				{
					Logging.PrintInfo(Logging.RequestCache, SR.GetString("net_log_cache_response_last_modified", new object[]
					{
						ftpWebResponse.LastModified.ToUniversalTime().ToString("r", CultureInfo.InvariantCulture),
						ftpWebResponse.ContentLength
					}));
				}
				if (Logging.On)
				{
					Logging.PrintInfo(Logging.RequestCache, SR.GetString("net_log_cache_cache_last_modified", new object[]
					{
						base.CacheEntry.LastModifiedUtc.ToString("r", CultureInfo.InvariantCulture),
						base.CacheEntry.StreamSize
					}));
				}
				if (base.CacheStreamOffset != 0L && base.CacheEntry.IsPartialEntry)
				{
					if (Logging.On)
					{
						Logging.PrintError(Logging.RequestCache, SR.GetString("net_log_cache_partial_and_non_zero_content_offset", new object[]
						{
							base.CacheStreamOffset.ToString(CultureInfo.InvariantCulture)
						}));
					}
				}
				if (ftpWebResponse.LastModified.ToUniversalTime() == base.CacheEntry.LastModifiedUtc)
				{
					if (base.CacheEntry.IsPartialEntry)
					{
						if (ftpWebResponse.ContentLength > 0L)
						{
							base.CacheStreamLength = ftpWebResponse.ContentLength;
						}
						else
						{
							base.CacheStreamLength = -1L;
						}
						result = CacheValidationStatus.CombineCachedAndServerResponse;
					}
					else if (ftpWebResponse.ContentLength == base.CacheEntry.StreamSize)
					{
						result = CacheValidationStatus.ReturnCachedResponse;
					}
					else
					{
						result = CacheValidationStatus.DoNotTakeFromCache;
					}
				}
				else
				{
					result = CacheValidationStatus.DoNotTakeFromCache;
				}
			}
			else
			{
				result = CacheValidationStatus.DoNotTakeFromCache;
			}
			return result;
		}

		// Token: 0x06002A3B RID: 10811 RVA: 0x000B2078 File Offset: 0x000B1078
		protected internal override CacheValidationStatus ValidateResponse()
		{
			if (this.HttpProxyMode)
			{
				return base.ValidateResponse();
			}
			if (this.Policy.Level != RequestCacheLevel.Default && this.Policy.Level != RequestCacheLevel.Revalidate)
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
			FtpWebResponse ftpWebResponse = base.Response as FtpWebResponse;
			if (ftpWebResponse == null)
			{
				if (Logging.On)
				{
					Logging.PrintWarning(Logging.RequestCache, SR.GetString("net_log_cache_null_response_failure"));
				}
				return CacheValidationStatus.Continue;
			}
			if (Logging.On)
			{
				Logging.PrintInfo(Logging.RequestCache, SR.GetString("net_log_cache_ftp_response_status", new object[]
				{
					((int)ftpWebResponse.StatusCode).ToString(CultureInfo.InvariantCulture),
					ftpWebResponse.StatusCode.ToString()
				}));
			}
			if (base.ResponseCount > 1)
			{
				if (Logging.On)
				{
					Logging.PrintInfo(Logging.RequestCache, SR.GetString("net_log_cache_resp_valid_based_on_retry", new object[]
					{
						base.ResponseCount
					}));
				}
				return CacheValidationStatus.Continue;
			}
			if (ftpWebResponse.StatusCode != FtpStatusCode.OpeningData && ftpWebResponse.StatusCode != FtpStatusCode.FileStatus)
			{
				return CacheValidationStatus.RetryResponseFromServer;
			}
			return CacheValidationStatus.Continue;
		}

		// Token: 0x06002A3C RID: 10812 RVA: 0x000B21BC File Offset: 0x000B11BC
		protected internal override CacheValidationStatus UpdateCache()
		{
			if (this.HttpProxyMode)
			{
				return base.UpdateCache();
			}
			base.CacheStreamOffset = 0L;
			if (base.RequestMethod == HttpMethod.Other)
			{
				if (Logging.On)
				{
					Logging.PrintInfo(Logging.RequestCache, SR.GetString("net_log_cache_not_updated_based_on_policy", new object[]
					{
						base.Request.Method
					}));
				}
				return CacheValidationStatus.DoNotUpdateCache;
			}
			if (base.ValidationStatus == CacheValidationStatus.RemoveFromCache)
			{
				if (Logging.On)
				{
					Logging.PrintInfo(Logging.RequestCache, SR.GetString("net_log_cache_removed_existing_invalid_entry"));
				}
				return CacheValidationStatus.RemoveFromCache;
			}
			if (this.Policy.Level == RequestCacheLevel.CacheOnly)
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
			FtpWebResponse ftpWebResponse = base.Response as FtpWebResponse;
			if (ftpWebResponse == null)
			{
				if (Logging.On)
				{
					Logging.PrintWarning(Logging.RequestCache, SR.GetString("net_log_cache_not_updated_because_no_response"));
				}
				return CacheValidationStatus.DoNotUpdateCache;
			}
			if (base.RequestMethod == HttpMethod.Delete || base.RequestMethod == HttpMethod.Put)
			{
				if (base.RequestMethod == HttpMethod.Delete || ftpWebResponse.StatusCode == FtpStatusCode.OpeningData || ftpWebResponse.StatusCode == FtpStatusCode.DataAlreadyOpen || ftpWebResponse.StatusCode == FtpStatusCode.FileActionOK || ftpWebResponse.StatusCode == FtpStatusCode.ClosingData)
				{
					if (Logging.On)
					{
						Logging.PrintInfo(Logging.RequestCache, SR.GetString("net_log_cache_removed_existing_based_on_method", new object[]
						{
							base.Request.Method
						}));
					}
					return CacheValidationStatus.RemoveFromCache;
				}
				if (Logging.On)
				{
					Logging.PrintWarning(Logging.RequestCache, SR.GetString("net_log_cache_existing_not_removed_because_unexpected_response_status", new object[]
					{
						(int)ftpWebResponse.StatusCode,
						ftpWebResponse.StatusCode.ToString()
					}));
				}
				return CacheValidationStatus.DoNotUpdateCache;
			}
			else
			{
				if (this.Policy.Level == RequestCacheLevel.NoCacheNoStore)
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
				if (base.ValidationStatus == CacheValidationStatus.ReturnCachedResponse)
				{
					return this.UpdateCacheEntryOnRevalidate();
				}
				if (ftpWebResponse.StatusCode != FtpStatusCode.OpeningData && ftpWebResponse.StatusCode != FtpStatusCode.DataAlreadyOpen && ftpWebResponse.StatusCode != FtpStatusCode.ClosingData)
				{
					if (Logging.On)
					{
						Logging.PrintInfo(Logging.RequestCache, SR.GetString("net_log_cache_not_updated_based_on_ftp_response_status", new object[]
						{
							string.Concat(new string[]
							{
								FtpStatusCode.OpeningData.ToString(),
								"|",
								FtpStatusCode.DataAlreadyOpen.ToString(),
								"|",
								FtpStatusCode.ClosingData.ToString()
							}),
							ftpWebResponse.StatusCode.ToString()
						}));
					}
					return CacheValidationStatus.DoNotUpdateCache;
				}
				if (((FtpWebRequest)base.Request).ContentOffset == 0L)
				{
					return this.UpdateCacheEntryOnStore();
				}
				if (Logging.On)
				{
					Logging.PrintWarning(Logging.RequestCache, SR.GetString("net_log_cache_update_not_supported_for_ftp_restart", new object[]
					{
						((FtpWebRequest)base.Request).ContentOffset.ToString(CultureInfo.InvariantCulture)
					}));
				}
				if (base.CacheEntry.LastModifiedUtc != DateTime.MinValue && ftpWebResponse.LastModified.ToUniversalTime() != base.CacheEntry.LastModifiedUtc)
				{
					if (Logging.On)
					{
						Logging.PrintWarning(Logging.RequestCache, SR.GetString("net_log_cache_removed_entry_because_ftp_restart_response_changed", new object[]
						{
							base.CacheEntry.LastModifiedUtc.ToString("r", CultureInfo.InvariantCulture),
							ftpWebResponse.LastModified.ToUniversalTime().ToString("r", CultureInfo.InvariantCulture)
						}));
					}
					return CacheValidationStatus.RemoveFromCache;
				}
				return CacheValidationStatus.DoNotUpdateCache;
			}
		}

		// Token: 0x06002A3D RID: 10813 RVA: 0x000B25B0 File Offset: 0x000B15B0
		private CacheValidationStatus UpdateCacheEntryOnStore()
		{
			base.CacheEntry.EntryMetadata = null;
			base.CacheEntry.SystemMetadata = null;
			FtpWebResponse ftpWebResponse = base.Response as FtpWebResponse;
			if (ftpWebResponse.LastModified != DateTime.MinValue)
			{
				base.CacheEntry.LastModifiedUtc = ftpWebResponse.LastModified.ToUniversalTime();
			}
			base.ResponseEntityLength = base.Response.ContentLength;
			base.CacheEntry.StreamSize = base.ResponseEntityLength;
			base.CacheEntry.LastSynchronizedUtc = DateTime.UtcNow;
			return CacheValidationStatus.CacheResponse;
		}

		// Token: 0x06002A3E RID: 10814 RVA: 0x000B2640 File Offset: 0x000B1640
		private CacheValidationStatus UpdateCacheEntryOnRevalidate()
		{
			if (Logging.On)
			{
				Logging.PrintInfo(Logging.RequestCache, SR.GetString("net_log_cache_last_synchronized", new object[]
				{
					base.CacheEntry.LastSynchronizedUtc.ToString("r", CultureInfo.InvariantCulture)
				}));
			}
			DateTime utcNow = DateTime.UtcNow;
			if (base.CacheEntry.LastSynchronizedUtc + TimeSpan.FromMinutes(1.0) >= utcNow)
			{
				if (Logging.On)
				{
					Logging.PrintInfo(Logging.RequestCache, SR.GetString("net_log_cache_suppress_update_because_synched_last_minute"));
				}
				return CacheValidationStatus.DoNotUpdateCache;
			}
			base.CacheEntry.EntryMetadata = null;
			base.CacheEntry.SystemMetadata = null;
			base.CacheEntry.LastSynchronizedUtc = utcNow;
			if (Logging.On)
			{
				Logging.PrintInfo(Logging.RequestCache, SR.GetString("net_log_cache_updating_last_synchronized", new object[]
				{
					base.CacheEntry.LastSynchronizedUtc.ToString("r", CultureInfo.InvariantCulture)
				}));
			}
			return CacheValidationStatus.UpdateResponseInformation;
		}

		// Token: 0x06002A3F RID: 10815 RVA: 0x000B2744 File Offset: 0x000B1744
		private CacheValidationStatus TryConditionalRequest()
		{
			FtpWebRequest ftpWebRequest = base.Request as FtpWebRequest;
			if (ftpWebRequest == null || !ftpWebRequest.UseBinary)
			{
				return CacheValidationStatus.DoNotTakeFromCache;
			}
			if (ftpWebRequest.ContentOffset != 0L)
			{
				if (base.CacheEntry.IsPartialEntry || ftpWebRequest.ContentOffset >= base.CacheStreamLength)
				{
					return CacheValidationStatus.DoNotTakeFromCache;
				}
				base.CacheStreamOffset = ftpWebRequest.ContentOffset;
			}
			return CacheValidationStatus.Continue;
		}

		// Token: 0x040028E8 RID: 10472
		private DateTime m_LastModified;

		// Token: 0x040028E9 RID: 10473
		private bool m_HttpProxyMode;
	}
}
