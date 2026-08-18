using System;
using System.Web;

namespace Telerik.Web.UI.Captcha
{
	// Token: 0x0200019F RID: 415
	public class SessionCachingProvider : BaseCaptchaCachingProvider
	{
		// Token: 0x06000F0B RID: 3851 RVA: 0x000391A0 File Offset: 0x000373A0
		public SessionCachingProvider(HttpContext context) : base(context)
		{
			this.Context = context;
			this.DependencyKey = "RadCaptchaCache";
		}

		// Token: 0x06000F0C RID: 3852 RVA: 0x000391BB File Offset: 0x000373BB
		public SessionCachingProvider(HttpContext context, string dependencyKey) : base(context, dependencyKey)
		{
			this.Context = context;
			this.DependencyKey = dependencyKey;
		}

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x06000F0D RID: 3853 RVA: 0x000391D3 File Offset: 0x000373D3
		// (set) Token: 0x06000F0E RID: 3854 RVA: 0x000391DB File Offset: 0x000373DB
		private HttpContext Context { get; set; }

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x06000F0F RID: 3855 RVA: 0x000391E4 File Offset: 0x000373E4
		// (set) Token: 0x06000F10 RID: 3856 RVA: 0x000391EC File Offset: 0x000373EC
		private string DependencyKey { get; set; }

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x06000F11 RID: 3857 RVA: 0x000391F5 File Offset: 0x000373F5
		public override bool ShouldAddCacheDependecy
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000F12 RID: 3858 RVA: 0x000391F8 File Offset: 0x000373F8
		public override void Save(string key, CaptchaImage image)
		{
			this.Context.Session[key] = image;
		}

		// Token: 0x06000F13 RID: 3859 RVA: 0x0003920C File Offset: 0x0003740C
		public override CaptchaImage Load(string key)
		{
			return (CaptchaImage)this.Context.Session[key];
		}

		// Token: 0x06000F14 RID: 3860 RVA: 0x00039224 File Offset: 0x00037424
		public override void Clear(string key)
		{
			if (this.Context.Session != null)
			{
				this.Context.Session.Remove(key);
			}
		}
	}
}
