using System;
using System.Configuration;
using System.Net.Cache;
using System.Threading;
using Microsoft.Win32;

namespace System.Net.Configuration
{
	// Token: 0x0200065D RID: 1629
	internal sealed class RequestCachingSectionInternal
	{
		// Token: 0x06003253 RID: 12883 RVA: 0x000D634C File Offset: 0x000D534C
		private RequestCachingSectionInternal()
		{
		}

		// Token: 0x06003254 RID: 12884 RVA: 0x000D6354 File Offset: 0x000D5354
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

		// Token: 0x17000BA8 RID: 2984
		// (get) Token: 0x06003255 RID: 12885 RVA: 0x000D6474 File Offset: 0x000D5474
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

		// Token: 0x17000BA9 RID: 2985
		// (get) Token: 0x06003256 RID: 12886 RVA: 0x000D64A0 File Offset: 0x000D54A0
		internal bool DisableAllCaching
		{
			get
			{
				return this.disableAllCaching;
			}
		}

		// Token: 0x17000BAA RID: 2986
		// (get) Token: 0x06003257 RID: 12887 RVA: 0x000D64A8 File Offset: 0x000D54A8
		internal RequestCache DefaultCache
		{
			get
			{
				return this.defaultCache;
			}
		}

		// Token: 0x17000BAB RID: 2987
		// (get) Token: 0x06003258 RID: 12888 RVA: 0x000D64B0 File Offset: 0x000D54B0
		internal RequestCachePolicy DefaultCachePolicy
		{
			get
			{
				return this.defaultCachePolicy;
			}
		}

		// Token: 0x17000BAC RID: 2988
		// (get) Token: 0x06003259 RID: 12889 RVA: 0x000D64B8 File Offset: 0x000D54B8
		internal bool IsPrivateCache
		{
			get
			{
				return this.isPrivateCache;
			}
		}

		// Token: 0x17000BAD RID: 2989
		// (get) Token: 0x0600325A RID: 12890 RVA: 0x000D64C0 File Offset: 0x000D54C0
		internal TimeSpan UnspecifiedMaximumAge
		{
			get
			{
				return this.unspecifiedMaximumAge;
			}
		}

		// Token: 0x17000BAE RID: 2990
		// (get) Token: 0x0600325B RID: 12891 RVA: 0x000D64C8 File Offset: 0x000D54C8
		internal HttpRequestCachePolicy DefaultHttpCachePolicy
		{
			get
			{
				return this.defaultHttpCachePolicy;
			}
		}

		// Token: 0x17000BAF RID: 2991
		// (get) Token: 0x0600325C RID: 12892 RVA: 0x000D64D0 File Offset: 0x000D54D0
		internal RequestCachePolicy DefaultFtpCachePolicy
		{
			get
			{
				return this.defaultFtpCachePolicy;
			}
		}

		// Token: 0x17000BB0 RID: 2992
		// (get) Token: 0x0600325D RID: 12893 RVA: 0x000D64D8 File Offset: 0x000D54D8
		internal HttpRequestCacheValidator DefaultHttpValidator
		{
			get
			{
				return this.httpRequestCacheValidator;
			}
		}

		// Token: 0x17000BB1 RID: 2993
		// (get) Token: 0x0600325E RID: 12894 RVA: 0x000D64E0 File Offset: 0x000D54E0
		internal FtpRequestCacheValidator DefaultFtpValidator
		{
			get
			{
				return this.ftpRequestCacheValidator;
			}
		}

		// Token: 0x0600325F RID: 12895 RVA: 0x000D64E8 File Offset: 0x000D54E8
		internal static RequestCachingSectionInternal GetSection()
		{
			RequestCachingSectionInternal result;
			lock (RequestCachingSectionInternal.ClassSyncObject)
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
					catch
					{
						throw new ConfigurationErrorsException(SR.GetString("net_config_requestcaching"), new Exception(SR.GetString("net_nonClsCompliantException")));
					}
				}
			}
			return result;
		}

		// Token: 0x04002F2E RID: 12078
		private static object classSyncObject;

		// Token: 0x04002F2F RID: 12079
		private RequestCache defaultCache;

		// Token: 0x04002F30 RID: 12080
		private HttpRequestCachePolicy defaultHttpCachePolicy;

		// Token: 0x04002F31 RID: 12081
		private RequestCachePolicy defaultFtpCachePolicy;

		// Token: 0x04002F32 RID: 12082
		private RequestCachePolicy defaultCachePolicy;

		// Token: 0x04002F33 RID: 12083
		private bool disableAllCaching;

		// Token: 0x04002F34 RID: 12084
		private HttpRequestCacheValidator httpRequestCacheValidator;

		// Token: 0x04002F35 RID: 12085
		private FtpRequestCacheValidator ftpRequestCacheValidator;

		// Token: 0x04002F36 RID: 12086
		private bool isPrivateCache;

		// Token: 0x04002F37 RID: 12087
		private TimeSpan unspecifiedMaximumAge;
	}
}
