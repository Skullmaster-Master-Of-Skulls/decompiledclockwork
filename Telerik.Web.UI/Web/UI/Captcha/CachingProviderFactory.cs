using System;
using System.Configuration;
using System.Web;

namespace Telerik.Web.UI.Captcha
{
	// Token: 0x0200019E RID: 414
	internal class CachingProviderFactory
	{
		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x06000F07 RID: 3847 RVA: 0x000390D7 File Offset: 0x000372D7
		// (set) Token: 0x06000F08 RID: 3848 RVA: 0x000390DF File Offset: 0x000372DF
		public ICaptchaCachingProvider Provider { get; private set; }

		// Token: 0x06000F09 RID: 3849 RVA: 0x000390E8 File Offset: 0x000372E8
		public static CachingProviderFactory GetProviderByStorageType(CaptchaImageStorage storage)
		{
			return new CachingProviderFactory(storage);
		}

		// Token: 0x06000F0A RID: 3850 RVA: 0x000390F0 File Offset: 0x000372F0
		private CachingProviderFactory(CaptchaImageStorage storage)
		{
			if (storage == CaptchaImageStorage.Cache)
			{
				this.Provider = new CacheCachingProvider(HttpContext.Current);
				return;
			}
			if (storage == CaptchaImageStorage.Session)
			{
				this.Provider = new SessionCachingProvider(HttpContext.Current);
				return;
			}
			if (storage == CaptchaImageStorage.Custom)
			{
				string text = ConfigurationManager.AppSettings["Telerik.Web.CaptchaImageStorageProviderTypeName"];
				Type type = Type.GetType(text);
				try
				{
					this.Provider = (ICaptchaCachingProvider)Activator.CreateInstance(type, new object[]
					{
						HttpContext.Current
					});
					if (this.Provider == null)
					{
						throw new ApplicationException(string.Format("Unable to instantiate object of type {0}.", text));
					}
				}
				catch (ArgumentNullException)
				{
					throw new ArgumentNullException("Unable to read application setting. Please make sure that you have set Telerik.Web.CaptchaImageStorageProviderTypeName in the web.config.");
				}
			}
		}
	}
}
