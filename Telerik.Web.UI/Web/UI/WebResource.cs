using System;
using System.Collections;
using System.Configuration;
using System.Reflection;
using System.Security.Permissions;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x020011C6 RID: 4550
	public class WebResource : Page
	{
		// Token: 0x0600BBFF RID: 48127 RVA: 0x0029A910 File Offset: 0x00298B10
		public static bool IsIIS7Request(HttpContext context)
		{
			HttpWorkerRequest httpWorkerRequest = (HttpWorkerRequest)((IServiceProvider)context).GetService(typeof(HttpWorkerRequest));
			return httpWorkerRequest.GetType().ToString().Contains("System.Web.Hosting.IIS7WorkerRequest");
		}

		// Token: 0x0600BC00 RID: 48128 RVA: 0x0029A94C File Offset: 0x00298B4C
		internal static bool Exists(HttpContext context, string path, string applicationPath)
		{
			bool flag = SecurityHelper.IsPermissionGranted(new ConfigurationPermission(PermissionState.Unrestricted));
			bool flag2 = SecurityHelper.IsPermissionGranted(new SecurityPermission(PermissionState.Unrestricted));
			if (!flag || !flag2)
			{
				return true;
			}
			path = VirtualPathUtility.MakeRelative(VirtualPathUtility.AppendTrailingSlash(applicationPath), path);
			if (WebResource.IsIIS7Request(context))
			{
				try
				{
					Type type = Type.GetType("Microsoft.Web.Administration.WebConfigurationManager, Microsoft.Web.Administration, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35");
					if (type == null)
					{
						return true;
					}
					object obj = type.InvokeMember("GetSection", BindingFlags.InvokeMethod, null, null, new object[]
					{
						"system.webServer/handlers"
					});
					IEnumerable enumerable = (IEnumerable)obj.GetType().InvokeMember("GetCollection", BindingFlags.InvokeMethod, null, obj, new object[0]);
					foreach (object obj2 in enumerable)
					{
						string a = (string)obj2.GetType().InvokeMember("GetAttributeValue", BindingFlags.InvokeMethod, null, obj2, new object[]
						{
							"path"
						});
						string typeName = (string)obj2.GetType().InvokeMember("GetAttributeValue", BindingFlags.InvokeMethod, null, obj2, new object[]
						{
							"type"
						});
						Type type2 = Type.GetType(typeName);
						if (a == path && (type2.Equals(typeof(WebResource)) || type2.Equals(typeof(WebResourceSession))))
						{
							return true;
						}
					}
					return false;
				}
				catch
				{
					return true;
				}
			}
			HttpHandlersSection httpHandlersSection = (HttpHandlersSection)ConfigurationManager.GetSection("system.web/httpHandlers");
			foreach (object obj3 in httpHandlersSection.Handlers)
			{
				HttpHandlerAction httpHandlerAction = (HttpHandlerAction)obj3;
				Type type3 = Type.GetType(httpHandlerAction.Type);
				if (httpHandlerAction.Path == path && (type3.Equals(typeof(WebResource)) || type3.Equals(typeof(WebResourceSession))))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600BC01 RID: 48129 RVA: 0x0029ABD0 File Offset: 0x00298DD0
		public override void ProcessRequest(HttpContext context)
		{
			HandlerRouter handlerRouter = new HandlerRouter();
			if (!handlerRouter.ProcessHandler(context))
			{
				CombinedScriptWriter.WriteCombinedScriptFile(this, context);
			}
		}

		// Token: 0x0600BC02 RID: 48130 RVA: 0x0029ABF3 File Offset: 0x00298DF3
		internal static ArgumentException GetHttpHandlerUrlNotAppRelative()
		{
			return new ArgumentException("HttpHandlerUrl must be application-relative (i.e. it should start with \"~/\"");
		}

		// Token: 0x04003165 RID: 12645
		private const string WebConfigurationManagerTypeName = "Microsoft.Web.Administration.WebConfigurationManager, Microsoft.Web.Administration, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35";
	}
}
