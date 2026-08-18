using System;

namespace System.Web.WebPages
{
	// Token: 0x02000039 RID: 57
	public static class HttpContextExtensions
	{
		// Token: 0x06000190 RID: 400 RVA: 0x000056F5 File Offset: 0x000038F5
		public static void RedirectLocal(this HttpContextBase context, string url)
		{
			if (context.Request.IsUrlLocalToHost(url))
			{
				context.Response.Redirect(url);
				return;
			}
			context.Response.Redirect("~/");
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00005722 File Offset: 0x00003922
		public static void RegisterForDispose(this HttpContextBase context, IDisposable resource)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			RequestResourceTracker.RegisterForDispose(context, resource);
		}
	}
}
