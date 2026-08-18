using System;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TechnoPro.Common.Configuration;
using TechnoPro.Common.Public.Entities.RequiredSessionForm;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Authentication;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;
using TechnoPro.Common.UI.Web.Entity;
using TechnoPro.Common.UI.Web.Entity.AuthenticationAuthorization;

namespace TechnoPro.ClockWorkWeb.user
{
	// Token: 0x0200001D RID: 29
	public class user_UserMasterNoMenu : MasterPage, IClockWorkMasterPageAuth
	{
		// Token: 0x0600009D RID: 157 RVA: 0x00004880 File Offset: 0x00002A80
		protected void link_logout_Click(object sender, EventArgs e)
		{
			IWebAuthenticationAuthorizationWebClientManager webAuthenticationAuthorizationWebClientManager = new WebAuthenticationAuthorizationWebClientManager();
			webAuthenticationAuthorizationWebClientManager.Logout();
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600009E RID: 158 RVA: 0x0000489B File Offset: 0x00002A9B
		public bool IsExemptFromRequiredSessionFormCheck
		{
			get
			{
				return this._isExemptFromRequiredSessionFormCheck;
			}
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x0600009F RID: 159 RVA: 0x000048A4 File Offset: 0x00002AA4
		// (remove) Token: 0x060000A0 RID: 160 RVA: 0x000048DC File Offset: 0x00002ADC
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<IsExemptFromRequiredSessionFormCheckEventArgs> OnGetIsExemptFromRequiredSessionFormCheck;

		// Token: 0x060000A1 RID: 161 RVA: 0x00004914 File Offset: 0x00002B14
		private bool FireOnGetIsExemptFromRequiredSessionFormCheckEventArgs()
		{
			string rawUrl = base.Request.RawUrl;
			string[] array = (from g in rawUrl.Split(new char[]
			{
				'/'
			})
			select g.Trim().ToLower() into h
			where h.Length > 0
			select h).ToArray<string>();
			bool flag = array.Length > 1 && array[1].ToLower() == "custom";
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				EventHandler<IsExemptFromRequiredSessionFormCheckEventArgs> onGetIsExemptFromRequiredSessionFormCheck = this.OnGetIsExemptFromRequiredSessionFormCheck;
				bool flag2 = onGetIsExemptFromRequiredSessionFormCheck == null;
				if (flag2)
				{
					result = false;
				}
				else
				{
					IsExemptFromRequiredSessionFormCheckEventArgs isExemptFromRequiredSessionFormCheckEventArgs = new IsExemptFromRequiredSessionFormCheckEventArgs();
					onGetIsExemptFromRequiredSessionFormCheck(this, isExemptFromRequiredSessionFormCheckEventArgs);
					result = isExemptFromRequiredSessionFormCheckEventArgs.IsExempt;
				}
			}
			return result;
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x000049E4 File Offset: 0x00002BE4
		public bool IsExemptFromAuthentication
		{
			get
			{
				return this._isExemptFromAuthentication;
			}
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x060000A3 RID: 163 RVA: 0x000049EC File Offset: 0x00002BEC
		// (remove) Token: 0x060000A4 RID: 164 RVA: 0x00004A24 File Offset: 0x00002C24
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<IsExemptFromAuthenticationEventArgs> OnGetIsExemptFromAuthenticationEventArgs;

		// Token: 0x060000A5 RID: 165 RVA: 0x00004A5C File Offset: 0x00002C5C
		private bool FireOnGetIsExemptFromAuthenticationEventArgs()
		{
			string rawUrl = base.Request.RawUrl;
			string[] source = (from g in rawUrl.Split(new char[]
			{
				'/'
			})
			select g.Trim().ToLower() into h
			where h.Length > 0
			select h).ToArray<string>();
			bool flag = source.Contains("custom");
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				EventHandler<IsExemptFromAuthenticationEventArgs> onGetIsExemptFromAuthenticationEventArgs = this.OnGetIsExemptFromAuthenticationEventArgs;
				bool flag2 = onGetIsExemptFromAuthenticationEventArgs == null;
				if (flag2)
				{
					result = false;
				}
				else
				{
					IsExemptFromAuthenticationEventArgs isExemptFromAuthenticationEventArgs = new IsExemptFromAuthenticationEventArgs();
					onGetIsExemptFromAuthenticationEventArgs(this, isExemptFromAuthenticationEventArgs);
					result = isExemptFromAuthenticationEventArgs.IsExempt;
				}
			}
			return result;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00004B20 File Offset: 0x00002D20
		private void RegisterStartupSessionTimeoutExtenderScript()
		{
			Configuration configuration = WebConfigurationManager.OpenWebConfiguration(HostingEnvironment.ApplicationVirtualPath);
			SessionStateSection sessionStateSection = (SessionStateSection)configuration.GetSection("system.web/sessionState");
			int num = Math.Max((int)sessionStateSection.Timeout.TotalMinutes, 1) * 60;
			int num2 = (num > 180) ? ((num - 180) * 1000) : (Convert.ToInt32(num / 2) * 1000);
			bool flag = num2 < 1;
			if (flag)
			{
				num2 = 20000;
			}
			string str = string.Concat(new string[]
			{
				"// Helper variable used to prevent caching on some browsers\r\nvar counter;\r\ncounter = 0;\r\n\r\nfunction KeepSessionAlive() {\r\n    // Increase counter value, so we'll always get unique URL\r\n    counter++;\r\n\r\n    // Gets reference of image\r\n    var img = document.getElementById(\"imgSessionAlive\");\r\n\r\n    var src = \"../SessionKeepAlive.aspx?c=\" + counter;\r\n\r\nalert( \"Your session is about to expire.  Click OK to extend your session\" );\r\n\r\n    // Set new src value, which will cause request to server, so session will stay alive\r\n    img.src = src;\r\n\r\n    // Schedule new call of KeepSessionAlive function\r\n    setTimeout(KeepSessionAlive, ",
				num2.ToString(),
				");\r\n}\r\n\r\n// Run function for a first time\r\nsetTimeout(KeepSessionAlive, ",
				num2.ToString(),
				");"
			});
			string text = "\n<script type=\"text/javascript\" language=\"Javascript\" id=\"EventScriptBlock\">\n";
			text += str;
			text += "\n\n </script>";
			this.Page.ClientScript.RegisterStartupScript(base.GetType(), "sessionTimeoutScript", text, false);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00004C14 File Offset: 0x00002E14
		protected void Page_Load(object sender, EventArgs e)
		{
			this._isExemptFromAuthentication = this.FireOnGetIsExemptFromAuthenticationEventArgs();
			bool flag = !this._isExemptFromAuthentication;
			if (flag)
			{
				WebAuthenticationAuthorizationWebClientManager.CurrentInstance.ForceAuthenticate(this.Page);
				this.RegisterStartupSessionTimeoutExtenderScript();
			}
			this._isExemptFromRequiredSessionFormCheck = this.FireOnGetIsExemptFromRequiredSessionFormCheckEventArgs();
			string appSettingsByNameUsingProtection = ClockWorkConfigurationManager.GetAppSettingsByNameUsingProtection("allowedips");
			bool flag2 = !string.IsNullOrEmpty(appSettingsByNameUsingProtection) && appSettingsByNameUsingProtection.Length > 0 && base.Request.Url.ToString().ToLower().IndexOf("notallowed.aspx") < 0;
			if (flag2)
			{
				string myip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"] ?? "NULL";
				string myipProxy = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? "NULL";
				string[] source = appSettingsByNameUsingProtection.Split(new char[]
				{
					','
				});
				bool flag3 = source.Any((string ip) => ip.Equals(myip) || ip.Equals(myipProxy));
				bool flag4 = !flag3;
				if (flag4)
				{
					base.Response.Redirect("~/user/misc/notallowed.aspx?code=disabledip", true);
				}
			}
			bool flag5 = !this.Page.IsPostBack;
			if (flag5)
			{
				IWebAuthenticationAuthorizationWebClientManager webAuthenticationAuthorizationWebClientManager = new WebAuthenticationAuthorizationWebClientManager();
				ClockWorkIdentity currentClockWorkIdentity = webAuthenticationAuthorizationWebClientManager.GetCurrentClockWorkIdentity(this.Page);
				bool flag6 = !string.IsNullOrEmpty((currentClockWorkIdentity != null) ? currentClockWorkIdentity.UserName : null);
				if (flag6)
				{
					string userName = currentClockWorkIdentity.UserName;
					this.lbl_loggedin.Text = userName;
					this.p_loggedin.Visible = true;
				}
				else
				{
					this.p_loggedin.Visible = false;
				}
				bool flag7 = currentClockWorkIdentity != null && !this._isExemptFromRequiredSessionFormCheck;
				if (flag7)
				{
					RequiredSessionFormItem requiredSessionFormForStudentToFillIn = webAuthenticationAuthorizationWebClientManager.GetRequiredSessionFormForStudentToFillIn(this.Page, currentClockWorkIdentity.PersonId, this._isExemptFromAuthentication);
					bool flag8 = requiredSessionFormForStudentToFillIn != null;
					if (flag8)
					{
						NavigatorClientManager.CurrentInstance.SetReturnUrl();
						HttpContext.Current.Response.Redirect("~/user/student/ReqForm.aspx", true);
					}
				}
			}
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00004E2C File Offset: 0x0000302C
		public string getLocation()
		{
			return "~/custom/";
		}

		// Token: 0x04000049 RID: 73
		private bool _isExemptFromRequiredSessionFormCheck = false;

		// Token: 0x0400004B RID: 75
		private bool _isExemptFromAuthentication = false;

		// Token: 0x0400004D RID: 77
		protected HtmlHead Head1;

		// Token: 0x0400004E RID: 78
		protected ContentPlaceHolder head;

		// Token: 0x0400004F RID: 79
		protected ContentPlaceHolder myPlaceholder;

		// Token: 0x04000050 RID: 80
		protected ContentPlaceHolder header;

		// Token: 0x04000051 RID: 81
		protected HtmlForm form1;

		// Token: 0x04000052 RID: 82
		protected ContentPlaceHolder placeholder_topbar_left;

		// Token: 0x04000053 RID: 83
		protected HyperLink link_home;

		// Token: 0x04000054 RID: 84
		protected Image img_home;

		// Token: 0x04000055 RID: 85
		protected Panel p_loggedin;

		// Token: 0x04000056 RID: 86
		protected Image img_loggedin;

		// Token: 0x04000057 RID: 87
		protected Label lbl_loggedin;

		// Token: 0x04000058 RID: 88
		protected Label lbl_loggedinseparator;

		// Token: 0x04000059 RID: 89
		protected LinkButton link_logout;

		// Token: 0x0400005A RID: 90
		protected ContentPlaceHolder placeholder_navigation;

		// Token: 0x0400005B RID: 91
		protected ContentPlaceHolder placeholder_main;

		// Token: 0x0400005C RID: 92
		protected ContentPlaceHolder placeholder_menu;

		// Token: 0x0400005D RID: 93
		protected ContentPlaceHolder placeholder_footer;
	}
}
