using System;
using Microsoft.Owin.Extensions;
using Microsoft.Owin.Security.OAuth;

namespace Owin
{
	// Token: 0x0200000F RID: 15
	public static class OAuthBearerAuthenticationExtensions
	{
		// Token: 0x06000077 RID: 119 RVA: 0x00006660 File Offset: 0x00004860
		public static IAppBuilder UseOAuthBearerAuthentication(this IAppBuilder app, OAuthBearerAuthenticationOptions options)
		{
			if (app == null)
			{
				throw new ArgumentNullException("app");
			}
			app.Use(typeof(OAuthBearerAuthenticationMiddleware), new object[]
			{
				app,
				options
			});
			app.UseStageMarker(PipelineStage.Authenticate);
			return app;
		}
	}
}
