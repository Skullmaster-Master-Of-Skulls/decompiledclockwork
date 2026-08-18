using System;
using System.Web.Mvc.Properties;

namespace System.Web.Mvc
{
	// Token: 0x020001E0 RID: 480
	public class RedirectResult : ActionResult
	{
		// Token: 0x06000E6B RID: 3691 RVA: 0x0002613F File Offset: 0x0002433F
		public RedirectResult(string url) : this(url, false)
		{
		}

		// Token: 0x06000E6C RID: 3692 RVA: 0x00026149 File Offset: 0x00024349
		public RedirectResult(string url, bool permanent)
		{
			if (string.IsNullOrEmpty(url))
			{
				throw new ArgumentException(MvcResources.Common_NullOrEmpty, "url");
			}
			this.Permanent = permanent;
			this.Url = url;
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000E6D RID: 3693 RVA: 0x00026177 File Offset: 0x00024377
		// (set) Token: 0x06000E6E RID: 3694 RVA: 0x0002617F File Offset: 0x0002437F
		public bool Permanent { get; private set; }

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000E6F RID: 3695 RVA: 0x00026188 File Offset: 0x00024388
		// (set) Token: 0x06000E70 RID: 3696 RVA: 0x00026190 File Offset: 0x00024390
		public string Url { get; private set; }

		// Token: 0x06000E71 RID: 3697 RVA: 0x0002619C File Offset: 0x0002439C
		public override void ExecuteResult(ControllerContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			if (context.IsChildAction)
			{
				throw new InvalidOperationException(MvcResources.RedirectAction_CannotRedirectInChildAction);
			}
			string url = UrlHelper.GenerateContentUrl(this.Url, context.HttpContext);
			context.Controller.TempData.Keep();
			if (this.Permanent)
			{
				context.HttpContext.Response.RedirectPermanent(url, false);
				return;
			}
			context.HttpContext.Response.Redirect(url, false);
		}
	}
}
