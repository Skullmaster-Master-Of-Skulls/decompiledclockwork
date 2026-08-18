using System;
using System.Web.Configuration;

namespace System.Web
{
	// Token: 0x02000107 RID: 263
	internal sealed class UrlMappingsModule : IHttpModule
	{
		// Token: 0x0600104A RID: 4170 RVA: 0x000030B5 File Offset: 0x000012B5
		internal UrlMappingsModule()
		{
		}

		// Token: 0x0600104B RID: 4171 RVA: 0x0002D6E0 File Offset: 0x0002B8E0
		public void Init(HttpApplication application)
		{
			UrlMappingsSection urlMappings = RuntimeConfig.GetConfig().UrlMappings;
			bool flag = urlMappings.IsEnabled && urlMappings.UrlMappings.Count > 0;
			if (flag)
			{
				application.BeginRequest += this.OnEnter;
			}
		}

		// Token: 0x0600104C RID: 4172 RVA: 0x00006164 File Offset: 0x00004364
		public void Dispose()
		{
		}

		// Token: 0x0600104D RID: 4173 RVA: 0x0002D72C File Offset: 0x0002B92C
		internal void OnEnter(object source, EventArgs eventArgs)
		{
			HttpApplication httpApplication = (HttpApplication)source;
			UrlMappingsModule.UrlMappingRewritePath(httpApplication.Context);
		}

		// Token: 0x0600104E RID: 4174 RVA: 0x0002D74C File Offset: 0x0002B94C
		internal static void UrlMappingRewritePath(HttpContext context)
		{
			HttpRequest request = context.Request;
			UrlMappingsSection urlMappings = RuntimeConfig.GetAppConfig().UrlMappings;
			string path = request.Path;
			string text = null;
			string queryStringText = request.QueryStringText;
			if (!string.IsNullOrEmpty(queryStringText))
			{
				text = urlMappings.HttpResolveMapping(path + "?" + queryStringText);
			}
			if (text == null)
			{
				text = urlMappings.HttpResolveMapping(path);
			}
			if (!string.IsNullOrEmpty(text))
			{
				context.RewritePath(text, false);
			}
		}
	}
}
