using System;
using System.Web;

namespace Telerik.Web.UI.Captcha
{
	// Token: 0x0200019D RID: 413
	internal class CacheCachingProvider : BaseCaptchaCachingProvider
	{
		// Token: 0x06000EFD RID: 3837 RVA: 0x0003903F File Offset: 0x0003723F
		public CacheCachingProvider(HttpContext context) : base(context)
		{
			this.Context = context;
			this.DependencyKey = "RadCaptchaCache";
		}

		// Token: 0x06000EFE RID: 3838 RVA: 0x0003905A File Offset: 0x0003725A
		public CacheCachingProvider(HttpContext context, string dependencyKey) : base(context, dependencyKey)
		{
			this.Context = context;
			this.DependencyKey = dependencyKey;
		}

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x06000EFF RID: 3839 RVA: 0x00039072 File Offset: 0x00037272
		// (set) Token: 0x06000F00 RID: 3840 RVA: 0x0003907A File Offset: 0x0003727A
		private HttpContext Context { get; set; }

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x06000F01 RID: 3841 RVA: 0x00039083 File Offset: 0x00037283
		// (set) Token: 0x06000F02 RID: 3842 RVA: 0x0003908B File Offset: 0x0003728B
		private string DependencyKey { get; set; }

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x06000F03 RID: 3843 RVA: 0x00039094 File Offset: 0x00037294
		public override bool ShouldAddCacheDependecy
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000F04 RID: 3844 RVA: 0x00039097 File Offset: 0x00037297
		public override void Save(string key, CaptchaImage image)
		{
			this.Context.Cache[key] = image;
		}

		// Token: 0x06000F05 RID: 3845 RVA: 0x000390AB File Offset: 0x000372AB
		public override CaptchaImage Load(string key)
		{
			return (CaptchaImage)this.Context.Cache[key];
		}

		// Token: 0x06000F06 RID: 3846 RVA: 0x000390C3 File Offset: 0x000372C3
		public override void Clear(string key)
		{
			this.Context.Cache.Remove(key);
		}
	}
}
