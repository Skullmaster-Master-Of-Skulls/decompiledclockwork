using System;
using System.Net.Configuration;

namespace System.Net.Cache
{
	// Token: 0x02000569 RID: 1385
	internal sealed class RequestCacheManager
	{
		// Token: 0x06002A8F RID: 10895 RVA: 0x000B4E40 File Offset: 0x000B3E40
		private RequestCacheManager()
		{
		}

		// Token: 0x06002A90 RID: 10896 RVA: 0x000B4E48 File Offset: 0x000B3E48
		internal static RequestCacheBinding GetBinding(string internedScheme)
		{
			if (internedScheme == null)
			{
				throw new ArgumentNullException("uriScheme");
			}
			if (RequestCacheManager.s_CacheConfigSettings == null)
			{
				RequestCacheManager.LoadConfigSettings();
			}
			if (RequestCacheManager.s_CacheConfigSettings.DisableAllCaching)
			{
				return RequestCacheManager.s_BypassCacheBinding;
			}
			if (internedScheme.Length == 0)
			{
				return RequestCacheManager.s_DefaultGlobalBinding;
			}
			if (internedScheme == Uri.UriSchemeHttp || internedScheme == Uri.UriSchemeHttps)
			{
				return RequestCacheManager.s_DefaultHttpBinding;
			}
			if (internedScheme == Uri.UriSchemeFtp)
			{
				return RequestCacheManager.s_DefaultFtpBinding;
			}
			return RequestCacheManager.s_BypassCacheBinding;
		}

		// Token: 0x170008CB RID: 2251
		// (get) Token: 0x06002A91 RID: 10897 RVA: 0x000B4EB8 File Offset: 0x000B3EB8
		internal static bool IsCachingEnabled
		{
			get
			{
				if (RequestCacheManager.s_CacheConfigSettings == null)
				{
					RequestCacheManager.LoadConfigSettings();
				}
				return !RequestCacheManager.s_CacheConfigSettings.DisableAllCaching;
			}
		}

		// Token: 0x06002A92 RID: 10898 RVA: 0x000B4ED4 File Offset: 0x000B3ED4
		internal static void SetBinding(string uriScheme, RequestCacheBinding binding)
		{
			if (uriScheme == null)
			{
				throw new ArgumentNullException("uriScheme");
			}
			if (RequestCacheManager.s_CacheConfigSettings == null)
			{
				RequestCacheManager.LoadConfigSettings();
			}
			if (RequestCacheManager.s_CacheConfigSettings.DisableAllCaching)
			{
				return;
			}
			if (uriScheme.Length == 0)
			{
				RequestCacheManager.s_DefaultGlobalBinding = binding;
				return;
			}
			if (uriScheme == Uri.UriSchemeHttp || uriScheme == Uri.UriSchemeHttps)
			{
				RequestCacheManager.s_DefaultHttpBinding = binding;
				return;
			}
			if (uriScheme == Uri.UriSchemeFtp)
			{
				RequestCacheManager.s_DefaultFtpBinding = binding;
			}
		}

		// Token: 0x06002A93 RID: 10899 RVA: 0x000B4F4C File Offset: 0x000B3F4C
		private static void LoadConfigSettings()
		{
			lock (RequestCacheManager.s_BypassCacheBinding)
			{
				if (RequestCacheManager.s_CacheConfigSettings == null)
				{
					RequestCachingSectionInternal section = RequestCachingSectionInternal.GetSection();
					RequestCacheManager.s_DefaultGlobalBinding = new RequestCacheBinding(section.DefaultCache, section.DefaultHttpValidator, section.DefaultCachePolicy);
					RequestCacheManager.s_DefaultHttpBinding = new RequestCacheBinding(section.DefaultCache, section.DefaultHttpValidator, section.DefaultHttpCachePolicy);
					RequestCacheManager.s_DefaultFtpBinding = new RequestCacheBinding(section.DefaultCache, section.DefaultFtpValidator, section.DefaultFtpCachePolicy);
					RequestCacheManager.s_CacheConfigSettings = section;
				}
			}
		}

		// Token: 0x04002910 RID: 10512
		private static RequestCachingSectionInternal s_CacheConfigSettings;

		// Token: 0x04002911 RID: 10513
		private static readonly RequestCacheBinding s_BypassCacheBinding = new RequestCacheBinding(null, null, new RequestCachePolicy(RequestCacheLevel.BypassCache));

		// Token: 0x04002912 RID: 10514
		private static RequestCacheBinding s_DefaultGlobalBinding;

		// Token: 0x04002913 RID: 10515
		private static RequestCacheBinding s_DefaultHttpBinding;

		// Token: 0x04002914 RID: 10516
		private static RequestCacheBinding s_DefaultFtpBinding;
	}
}
