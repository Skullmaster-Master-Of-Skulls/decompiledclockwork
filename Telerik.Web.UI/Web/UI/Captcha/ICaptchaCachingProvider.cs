using System;
using System.Web.Caching;

namespace Telerik.Web.UI.Captcha
{
	// Token: 0x0200019B RID: 411
	internal interface ICaptchaCachingProvider
	{
		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x06000EED RID: 3821
		bool ShouldAddCacheDependecy { get; }

		// Token: 0x06000EEE RID: 3822
		void Save(string key, CaptchaImage image);

		// Token: 0x06000EEF RID: 3823
		CaptchaImage Load(string key);

		// Token: 0x06000EF0 RID: 3824
		void Clear(string key);

		// Token: 0x06000EF1 RID: 3825
		void CacheExpirationCallback(string key, object value, CacheItemRemovedReason reason);
	}
}
