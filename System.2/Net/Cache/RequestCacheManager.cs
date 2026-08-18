using System;
using System.Net.Configuration;

namespace System.Net.Cache
{
	// Token: 0x0200030F RID: 783
	internal sealed class RequestCacheManager
	{
		// Token: 0x06001C0F RID: 7183 RVA: 0x00085C84 File Offset: 0x00083E84
		private RequestCacheManager()
		{
		}

		// Token: 0x06001C10 RID: 7184 RVA: 0x00085C8C File Offset: 0x00083E8C
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

		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x06001C11 RID: 7185 RVA: 0x00085D06 File Offset: 0x00083F06
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

		// Token: 0x06001C12 RID: 7186 RVA: 0x00085D28 File Offset: 0x00083F28
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

		// Token: 0x06001C13 RID: 7187 RVA: 0x00085DAC File Offset: 0x00083FAC
		private static void LoadConfigSettings()
		{
			RequestCacheBinding obj = RequestCacheManager.s_BypassCacheBinding;
			lock (obj)
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

		// Token: 0x04001B4F RID: 6991
		private static volatile RequestCachingSectionInternal s_CacheConfigSettings;

		// Token: 0x04001B50 RID: 6992
		private static readonly RequestCacheBinding s_BypassCacheBinding = new RequestCacheBinding(null, null, new RequestCachePolicy(RequestCacheLevel.BypassCache));

		// Token: 0x04001B51 RID: 6993
		private static volatile RequestCacheBinding s_DefaultGlobalBinding;

		// Token: 0x04001B52 RID: 6994
		private static volatile RequestCacheBinding s_DefaultHttpBinding;

		// Token: 0x04001B53 RID: 6995
		private static volatile RequestCacheBinding s_DefaultFtpBinding;
	}
}
