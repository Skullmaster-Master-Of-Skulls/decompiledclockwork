using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web.ApplicationServices;
using System.Web.Resources;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace System.Web.UI
{
	// Token: 0x0200006C RID: 108
	[DefaultProperty("Path")]
	[TypeConverter(typeof(EmptyStringExpandableObjectConverter))]
	public class RoleServiceManager
	{
		// Token: 0x060003C4 RID: 964 RVA: 0x00013D5C File Offset: 0x00011F5C
		internal static void ConfigureRoleService(ref StringBuilder sb, HttpContext context, ScriptManager scriptManager, List<ScriptManagerProxy> proxies)
		{
			string text = null;
			bool loadRoles = false;
			if (scriptManager.HasRoleServiceManager)
			{
				RoleServiceManager roleService = scriptManager.RoleService;
				loadRoles = roleService.LoadRoles;
				text = roleService.Path.Trim();
				if (text.Length > 0)
				{
					text = scriptManager.ResolveClientUrl(text);
				}
			}
			if (proxies != null)
			{
				foreach (ScriptManagerProxy scriptManagerProxy in proxies)
				{
					if (scriptManagerProxy.HasRoleServiceManager)
					{
						RoleServiceManager roleService = scriptManagerProxy.RoleService;
						if (roleService.LoadRoles)
						{
							loadRoles = true;
						}
						text = ApplicationServiceManager.MergeServiceUrls(roleService.Path, text, scriptManagerProxy);
					}
				}
			}
			RoleServiceManager.GenerateInitializationScript(ref sb, context, scriptManager, text, loadRoles);
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x00013E14 File Offset: 0x00012014
		private static void GenerateInitializationScript(ref StringBuilder sb, HttpContext context, ScriptManager scriptManager, string serviceUrl, bool loadRoles)
		{
			bool roleServiceEnabled = ApplicationServiceHelper.RoleServiceEnabled;
			string text = null;
			if (roleServiceEnabled)
			{
				if (sb == null)
				{
					sb = new StringBuilder(128);
				}
				text = scriptManager.ResolveClientUrl("~/Role_JSON_AppService.axd");
				sb.Append("Sys.Services._RoleService.DefaultWebServicePath = '");
				sb.Append(HttpUtility.JavaScriptStringEncode(text));
				sb.Append("';\n");
			}
			bool flag = !string.IsNullOrEmpty(serviceUrl);
			if (flag)
			{
				if (text == null)
				{
					text = scriptManager.ResolveClientUrl("~/Role_JSON_AppService.axd");
				}
				if (loadRoles && !string.Equals(serviceUrl, text, StringComparison.OrdinalIgnoreCase))
				{
					throw new InvalidOperationException(AtlasWeb.RoleServiceManager_LoadRolesWithNonDefaultPath);
				}
				if (sb == null)
				{
					sb = new StringBuilder(128);
				}
				sb.Append("Sys.Services.RoleService.set_path('");
				sb.Append(HttpUtility.JavaScriptStringEncode(serviceUrl));
				sb.Append("');\n");
			}
			if (loadRoles)
			{
				if (scriptManager.DesignMode)
				{
					if (sb == null)
					{
						sb = new StringBuilder(128);
					}
					sb.Append("// loadRoles\n");
					return;
				}
				string[] rolesForUser = Roles.GetRolesForUser();
				if (rolesForUser != null && rolesForUser.Length != 0)
				{
					if (sb == null)
					{
						sb = new StringBuilder(128);
					}
					sb.Append("Sys.Services.RoleService._roles = ");
					sb.Append(new JavaScriptSerializer().Serialize(rolesForUser, JavaScriptSerializer.SerializationFormat.JavaScript));
					sb.Append(";\n");
				}
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060003C6 RID: 966 RVA: 0x00013F52 File Offset: 0x00012152
		// (set) Token: 0x060003C7 RID: 967 RVA: 0x00013F5A File Offset: 0x0001215A
		[DefaultValue(false)]
		[Category("Behavior")]
		[NotifyParentProperty(true)]
		[ResourceDescription("RoleServiceManager_LoadRoles")]
		public bool LoadRoles
		{
			get
			{
				return this._loadRoles;
			}
			set
			{
				this._loadRoles = value;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060003C8 RID: 968 RVA: 0x00013F63 File Offset: 0x00012163
		// (set) Token: 0x060003C9 RID: 969 RVA: 0x00013F74 File Offset: 0x00012174
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

		// Token: 0x04000172 RID: 370
		private bool _loadRoles;

		// Token: 0x04000173 RID: 371
		private string _path;
	}
}
