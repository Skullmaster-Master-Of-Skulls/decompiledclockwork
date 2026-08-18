using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web.ApplicationServices;

namespace System.Web.UI
{
	// Token: 0x02000045 RID: 69
	[DefaultProperty("Path")]
	[TypeConverter(typeof(EmptyStringExpandableObjectConverter))]
	public class AuthenticationServiceManager
	{
		// Token: 0x060002B5 RID: 693 RVA: 0x000113B4 File Offset: 0x0000F5B4
		internal static void ConfigureAuthenticationService(ref StringBuilder sb, HttpContext context, ScriptManager scriptManager, List<ScriptManagerProxy> proxies)
		{
			string text = null;
			if (scriptManager.HasAuthenticationServiceManager)
			{
				AuthenticationServiceManager authenticationService = scriptManager.AuthenticationService;
				text = authenticationService.Path.Trim();
				if (text.Length > 0)
				{
					text = scriptManager.ResolveUrl(text);
				}
			}
			if (proxies != null)
			{
				foreach (ScriptManagerProxy scriptManagerProxy in proxies)
				{
					if (scriptManagerProxy.HasAuthenticationServiceManager)
					{
						AuthenticationServiceManager authenticationService = scriptManagerProxy.AuthenticationService;
						text = ApplicationServiceManager.MergeServiceUrls(authenticationService.Path, text, scriptManagerProxy);
					}
				}
			}
			AuthenticationServiceManager.GenerateInitializationScript(ref sb, context, scriptManager, text);
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x00011454 File Offset: 0x0000F654
		private static void GenerateInitializationScript(ref StringBuilder sb, HttpContext context, ScriptManager scriptManager, string serviceUrl)
		{
			bool authenticationServiceEnabled = ApplicationServiceHelper.AuthenticationServiceEnabled;
			if (authenticationServiceEnabled)
			{
				if (sb == null)
				{
					sb = new StringBuilder(128);
				}
				string value = scriptManager.ResolveClientUrl("~/Authentication_JSON_AppService.axd");
				sb.Append("Sys.Services._AuthenticationService.DefaultWebServicePath = '");
				sb.Append(HttpUtility.JavaScriptStringEncode(value));
				sb.Append("';\n");
			}
			bool flag = !string.IsNullOrEmpty(serviceUrl);
			if (flag)
			{
				if (sb == null)
				{
					sb = new StringBuilder(128);
				}
				sb.Append("Sys.Services.AuthenticationService.set_path('");
				sb.Append(HttpUtility.JavaScriptStringEncode(serviceUrl));
				sb.Append("');\n");
			}
			if ((authenticationServiceEnabled || flag) && context != null && context.Request.IsAuthenticated)
			{
				sb.Append("Sys.Services.AuthenticationService._setAuthenticated(true);\n");
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x00011515 File Offset: 0x0000F715
		// (set) Token: 0x060002B8 RID: 696 RVA: 0x00011526 File Offset: 0x0000F726
		[DefaultValue("")]
		[Category("Behavior")]
		[NotifyParentProperty(true)]
		[ResourceDescription("ApplicationServiceManager_Path")]
		[UrlProperty]
		public string Path
		{
			get
			{
				return this._path ?? string.Empty;
			}
			set
			{
				this._path = value;
			}
		}

		// Token: 0x04000108 RID: 264
		private string _path;
	}
}
