using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web.ApplicationServices;
using System.Web.Resources;
using System.Web.Script.Services;
using System.Web.Security;
using System.Web.UI;

namespace System.Web.Handlers
{
	// Token: 0x020000DD RID: 221
	public class ScriptModule : IHttpModule
	{
		// Token: 0x06000C5F RID: 3167 RVA: 0x00029BD8 File Offset: 0x00027DD8
		private static bool ShouldSkipAuthorization(HttpContext context)
		{
			if (context == null || context.Request == null)
			{
				return false;
			}
			string filePath = context.Request.FilePath;
			if (ScriptResourceHandler.IsScriptResourceRequest(filePath))
			{
				return true;
			}
			if (!ApplicationServiceHelper.AuthenticationServiceEnabled || !RestHandlerFactory.IsRestRequest(context))
			{
				return false;
			}
			if (context.SkipAuthorization)
			{
				return true;
			}
			if (filePath == null || !filePath.EndsWith(".axd", StringComparison.OrdinalIgnoreCase))
			{
				return false;
			}
			WebServiceData webServiceData = WebServiceData.GetWebServiceData(context, filePath, false, false);
			return webServiceData != null && ScriptModule._authenticationServiceType == webServiceData.TypeData.Type;
		}

		// Token: 0x06000C60 RID: 3168 RVA: 0x000032F4 File Offset: 0x000014F4
		protected virtual void Dispose()
		{
		}

		// Token: 0x06000C61 RID: 3169 RVA: 0x00029C60 File Offset: 0x00027E60
		private void AuthenticateRequestHandler(object sender, EventArgs e)
		{
			HttpApplication httpApplication = (HttpApplication)sender;
			if (httpApplication != null && ScriptModule.ShouldSkipAuthorization(httpApplication.Context))
			{
				httpApplication.Context.SetSkipAuthorizationNoDemand(true, false);
			}
		}

		// Token: 0x06000C62 RID: 3170 RVA: 0x00029C94 File Offset: 0x00027E94
		private void EndRequestHandler(object sender, EventArgs e)
		{
			HttpApplication httpApplication = (HttpApplication)sender;
			HttpContext context = httpApplication.Context;
			object obj = context.Items["System.Web.UI.PageRequestManager:AsyncPostBackError"];
			if (obj != null && (bool)obj)
			{
				context.ClearError();
				context.Response.ClearHeaders();
				context.Response.Clear();
				context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
				context.Response.ContentType = "text/plain";
				string content = (string)context.Items["System.Web.UI.PageRequestManager:AsyncPostBackErrorMessage"];
				obj = context.Items["System.Web.UI.PageRequestManager:AsyncPostBackErrorHttpCode"];
				int num = (obj is int) ? ((int)obj) : 500;
				PageRequestManager.EncodeString(context.Response.Output, "error", num.ToString(CultureInfo.InvariantCulture), content);
			}
		}

		// Token: 0x06000C63 RID: 3171 RVA: 0x00029D70 File Offset: 0x00027F70
		protected virtual void Init(HttpApplication app)
		{
			if (app.Context.Items[ScriptModule._contextKey] != null)
			{
				return;
			}
			app.Context.Items[ScriptModule._contextKey] = ScriptModule._contextKey;
			if (Interlocked.Exchange(ref ScriptModule._isHandlerRegistered, 1) == 0)
			{
				HttpResponse.Redirecting += ScriptModule.HttpResponse_Redirecting;
			}
			app.PostAcquireRequestState += this.OnPostAcquireRequestState;
			app.AuthenticateRequest += this.AuthenticateRequestHandler;
			app.EndRequest += this.EndRequestHandler;
		}

		// Token: 0x06000C64 RID: 3172 RVA: 0x00029E04 File Offset: 0x00028004
		private static void HttpResponse_Redirecting(object sender, EventArgs e)
		{
			HttpResponse httpResponse = (HttpResponse)sender;
			HttpContext context = httpResponse.Context;
			if (PageRequestManager.IsAsyncPostBackRequest(new HttpRequestWrapper(context.Request)))
			{
				string text = httpResponse.RedirectLocation;
				List<HttpCookie> list = new List<HttpCookie>(httpResponse.Cookies.Count);
				for (int i = 0; i < httpResponse.Cookies.Count; i++)
				{
					list.Add(httpResponse.Cookies[i]);
				}
				httpResponse.ClearContent();
				httpResponse.ClearHeaders();
				for (int j = 0; j < list.Count; j++)
				{
					httpResponse.AppendCookie(list[j]);
				}
				httpResponse.Cache.SetCacheability(HttpCacheability.NoCache);
				httpResponse.ContentType = "text/plain";
				context.Items["System.Web.UI.PageRequestManager:AsyncPostBackRedirectLocation"] = text;
				httpResponse.IsRequestBeingRedirected = true;
				PageRequestManager.EncodeString(httpResponse.Output, "#", string.Empty, "4");
				text = string.Join(" ", from part in text.Split(new char[]
				{
					' '
				})
				select HttpUtility.UrlEncode(part));
				PageRequestManager.EncodeString(httpResponse.Output, "pageRedirect", string.Empty, text);
				return;
			}
			if (RestHandlerFactory.IsRestRequest(context))
			{
				RestHandler.WriteExceptionJsonString(context, new InvalidOperationException(AtlasWeb.WebService_RedirectError), 401);
			}
		}

		// Token: 0x06000C65 RID: 3173 RVA: 0x00029F68 File Offset: 0x00028168
		private void OnPostAcquireRequestState(object sender, EventArgs eventArgs)
		{
			HttpApplication httpApplication = (HttpApplication)sender;
			HttpRequest request = httpApplication.Context.Request;
			if (httpApplication.Context.Handler is Page && RestHandlerFactory.IsRestMethodCall(request))
			{
				WebServiceData webServiceData = WebServiceData.GetWebServiceData(HttpContext.Current, request.FilePath, false, true);
				string methodName = request.PathInfo.Substring(1);
				WebServiceMethodData methodData = webServiceData.GetMethodData(methodName);
				RestHandler.ExecuteWebServiceCall(HttpContext.Current, methodData);
				httpApplication.CompleteRequest();
			}
		}

		// Token: 0x06000C66 RID: 3174 RVA: 0x00029FDD File Offset: 0x000281DD
		void IHttpModule.Dispose()
		{
			this.Dispose();
		}

		// Token: 0x06000C67 RID: 3175 RVA: 0x00029FE5 File Offset: 0x000281E5
		void IHttpModule.Init(HttpApplication context)
		{
			this.Init(context);
		}

		// Token: 0x0400036A RID: 874
		private static readonly object _contextKey = new object();

		// Token: 0x0400036B RID: 875
		private static Type _authenticationServiceType = typeof(System.Web.Security.AuthenticationService);

		// Token: 0x0400036C RID: 876
		private static int _isHandlerRegistered;
	}
}
