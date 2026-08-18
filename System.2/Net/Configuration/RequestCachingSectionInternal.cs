using System;
using System.Configuration;
using System.Net.Cache;
using System.Threading;
using Microsoft.Win32;

namespace System.Net.Configuration
{
	// Token: 0x0200033E RID: 830
	internal sealed class RequestCachingSectionInternal
	{
		// Token: 0x06001DA9 RID: 7593 RVA: 0x0008C65C File Offset: 0x0008A85C
		private RequestCachingSectionInternal()
		{
		}

		// Token: 0x06001DAA RID: 7594 RVA: 0x0008C664 File Offset: 0x0008A864
		internal RequestCachingSectionInternal(RequestCachingSection section)
		{
			if (!section.DisableAllCaching)
			{
				this.defaultCachePolicy = new RequestCachePolicy(section.DefaultPolicyLevel);
				this.isPrivateCache = section.IsPrivateCache;
				this.unspecifiedMaximumAge = section.UnspecifiedMaximumAge;
			}
			else
			{
				this.disableAllCaching = true;
			}
			this.httpRequestCacheValidator = new HttpRequestCacheValidator(false, this.UnspecifiedMaximumAge);
			this.ftpRequestCacheValidator = new FtpRequestCacheValidator(false, this.UnspecifiedMaximumAge);
			this.defaultCache = new WinInetCache(this.IsPrivateCache, true, true);
			if (section.DisableAllCaching)
			{
				return;
			}
			HttpCachePolicyElement httpCachePolicyElement = section.DefaultHttpCachePolicy;
			if (httpCachePolicyElement.WasReadFromConfig)
			{
				if (httpCachePolicyElement.PolicyLevel == HttpRequestCacheLevel.Default)
				{
					HttpCacheAgeControl cacheAgeControl = (httpCachePolicyElement.MinimumFresh != TimeSpan.MinValue) ? HttpCacheAgeControl.MaxAgeAndMinFresh : HttpCacheAgeControl.MaxAgeAndMaxStale;
					this.defaultHttpCachePolicy = new HttpRequestCachePolicy(cacheAgeControl, httpCachePolicyElement.MaximumAge, (httpCachePolicyElement.MinimumFresh != TimeSpan.MinValue) ? httpCachePolicyElement.MinimumFresh : httpCachePolicyElement.MaximumStale);
				}
				else
				{
					this.defaultHttpCachePolicy = new HttpRequestCachePolicy(httpCachePolicyElement.PolicyLevel);
				}
			}
			FtpCachePolicyElement ftpCachePolicyElement = section.DefaultFtpCachePolicy;
			if (ftpCachePolicyElement.WasReadFromConfig)
			{
				this.defaultFtpCachePolicy = new RequestCachePolicy(ftpCachePolicyElement.PolicyLevel);
			}
		}

		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x06001DAB RID: 7595 RVA: 0x0008C784 File Offset: 0x0008A984
		internal static object ClassSyncObject
		{
			get
			{
				if (RequestCachingSectionInternal.classSyncObject == null)
				{
					object value = new object();
					Interlocked.CompareExchange(ref RequestCachingSectionInternal.classSyncObject, value, null);
				}
				return RequestCachingSectionInternal.classSyncObject;
			}
		}

		// Token: 0x1700077C RID: 1916
		// (get) Token: 0x06001DAC RID: 7596 RVA: 0x0008C7B0 File Offset: 0x0008A9B0
		internal bool DisableAllCaching
		{
			get
			{
				return this.disableAllCaching;
			}
		}

		// Token: 0x1700077D RID: 1917
		// (get) Token: 0x06001DAD RID: 7597 RVA: 0x0008C7B8 File Offset: 0x0008A9B8
		internal RequestCache DefaultCache
		{
			get
			{
				return this.defaultCache;
			}
		}

		// Token: 0x1700077E RID: 1918
		// (get) Token: 0x06001DAE RID: 7598 RVA: 0x0008C7C0 File Offset: 0x0008A9C0
		internal RequestCachePolicy DefaultCachePolicy
		{
			get
			{
				return this.defaultCachePolicy;
			}
		}

		// Token: 0x1700077F RID: 1919
		// (get) Token: 0x06001DAF RID: 7599 RVA: 0x0008C7C8 File Offset: 0x0008A9C8
		internal bool IsPrivateCache
		{
			get
			{
				return this.isPrivateCache;
			}
		}

		// Token: 0x17000780 RID: 1920
		// (get) Token: 0x06001DB0 RID: 7600 RVA: 0x0008C7D0 File Offset: 0x0008A9D0
		internal TimeSpan UnspecifiedMaximumAge
		{
			get
			{
				return this.unspecifiedMaximumAge;
			}
		}

		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x06001DB1 RID: 7601 RVA: 0x0008C7D8 File Offset: 0x0008A9D8
		internal HttpRequestCachePolicy DefaultHttpCachePolicy
		{
			get
			{
				return this.defaultHttpCachePolicy;
			}
		}

		// Token: 0x17000782 RID: 1922
		// (get) Token: 0x06001DB2 RID: 7602 RVA: 0x0008C7E0 File Offset: 0x0008A9E0
		internal RequestCachePolicy DefaultFtpCachePolicy
		{
			get
			{
				return this.defaultFtpCachePolicy;
			}
		}

		// Token: 0x17000783 RID: 1923
		// (get) Token: 0x06001DB3 RID: 7603 RVA: 0x0008C7E8 File Offset: 0x0008A9E8
		internal HttpRequestCacheValidator DefaultHttpValidator
		{
			get
			{
				return this.httpRequestCacheValidator;
			}
		}

		// Token: 0x17000784 RID: 1924
		// (get) Token: 0x06001DB4 RID: 7604 RVA: 0x0008C7F0 File Offset: 0x0008A9F0
		internal FtpRequestCacheValidator DefaultFtpValidator
		{
			get
			{
				return this.ftpRequestCacheValidator;
			}
		}

		// Token: 0x06001DB5 RID: 7605 RVA: 0x0008C7F8 File Offset: 0x0008A9F8
		internal static RequestCachingSectionInternal GetSection()
		{
			object obj = RequestCachingSectionInternal.ClassSyncObject;
			RequestCachingSectionInternal result;
			lock (obj)
			{
				RequestCachingSection requestCachingSection = PrivilegedConfigurationManager.GetSection(ConfigurationStrings.RequestCachingSectionPath) as RequestCachingSection;
				if (requestCachingSection == null)
				{
					result = null;
				}
				else
				{
					try
					{
						result = new RequestCachingSectionInternal(requestCachingSection);
					}
					catch (Exception ex)
					{
						if (NclUtilities.IsFatal(ex))
						{
							throw;
						}
						throw new ConfigurationErrorsException(SR.GetString("net_config_requestcaching"), ex);
					}
				}
			}
			return result;
		}

		// Token: 0x04001C63 RID: 7267
		private static object classSyncObject;

		// Token: 0x04001C64 RID: 7268
		private RequestCache defaultCache;

		// Token: 0x04001C65 RID: 7269
		private HttpRequestCachePolicy defaultHttpCachePolicy;

		// Token: 0x04001C66 RID: 7270
		private RequestCachePolicy defaultFtpCachePolicy;

		// Token: 0x04001C67 RID: 7271
		private RequestCachePolicy defaultCachePolicy;

		// Token: 0x04001C68 RID: 7272
		private bool disableAllCaching;

		// Token: 0x04001C69 RID: 7273
		private HttpRequestCacheValidator httpRequestCacheValidator;

		// Token: 0x04001C6A RID: 7274
		private FtpRequestCacheValidator ftpRequestCacheValidator;

		// Token: 0x04001C6B RID: 7275
		private bool isPrivateCache;

		// Token: 0x04001C6C RID: 7276
		private TimeSpan unspecifiedMaximumAge;
	}
}
