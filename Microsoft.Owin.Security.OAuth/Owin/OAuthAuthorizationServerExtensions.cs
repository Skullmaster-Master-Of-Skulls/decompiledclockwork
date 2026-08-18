using System;
using Microsoft.Owin.Security.OAuth;

namespace Owin
{
	// Token: 0x0200000B RID: 11
	public static class OAuthAuthorizationServerExtensions
	{
		// Token: 0x06000052 RID: 82 RVA: 0x00005DF8 File Offset: 0x00003FF8
		public static IAppBuilder UseOAuthAuthorizationServer(this IAppBuilder app, OAuthAuthorizationServerOptions options)
		{
			if (app == null)
			{
				throw new ArgumentNullException("app");
			}
			app.Use(typeof(OAuthAuthorizationServerMiddleware), new object[]
			{
				app,
				options
			});
			return app;
		}
	}
}
