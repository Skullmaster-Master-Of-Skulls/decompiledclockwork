using System;
using System.Web;
using System.Web.Caching;

namespace Telerik.Web.UI.Captcha
{
	// Token: 0x0200019C RID: 412
	public abstract class BaseCaptchaCachingProvider : ICaptchaCachingProvider
	{
		// Token: 0x06000EF2 RID: 3826 RVA: 0x00038FA8 File Offset: 0x000371A8
		public BaseCaptchaCachingProvider(HttpContext context)
		{
			this.Context = context;
			this.DependencyKey = "RadCaptchaCache";
		}

		// Token: 0x06000EF3 RID: 3827 RVA: 0x00038FC2 File Offset: 0x000371C2
		public BaseCaptchaCachingProvider(HttpContext context, string dependencyKey)
		{
			this.Context = context;
			this.DependencyKey = dependencyKey;
		}

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x06000EF4 RID: 3828 RVA: 0x00038FD8 File Offset: 0x000371D8
		// (set) Token: 0x06000EF5 RID: 3829 RVA: 0x00038FE0 File Offset: 0x000371E0
		private HttpContext Context { get; set; }

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x06000EF6 RID: 3830 RVA: 0x00038FE9 File Offset: 0x000371E9
		// (set) Token: 0x06000EF7 RID: 3831 RVA: 0x00038FF1 File Offset: 0x000371F1
		private string DependencyKey { get; set; }

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x06000EF8 RID: 3832 RVA: 0x00038FFA File Offset: 0x000371FA
		public virtual bool ShouldAddCacheDependecy
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000EF9 RID: 3833 RVA: 0x00038FFD File Offset: 0x000371FD
		public virtual void Save(string key, CaptchaImage image)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000EFA RID: 3834 RVA: 0x00039004 File Offset: 0x00037204
		public virtual CaptchaImage Load(string key)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000EFB RID: 3835 RVA: 0x0003900B File Offset: 0x0003720B
		public virtual void Clear(string key)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000EFC RID: 3836 RVA: 0x00039014 File Offset: 0x00037214
		public virtual void CacheExpirationCallback(string key, object value, CacheItemRemovedReason reason)
		{
			string text = key.Replace("CaptchaRemoval_", "");
			Lockables.Remove(text);
			this.Clear(text);
		}
	}
}
