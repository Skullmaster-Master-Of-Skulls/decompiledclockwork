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
	// Token: 0x0200001E RID: 30
	public class UserMasterNoMenuR : MasterPage, IClockWorkMasterPageAuth
	{
		// Token: 0x060000AA RID: 170 RVA: 0x00004E5C File Offset: 0x0000305C
		protected void link_logout_Click(object sender, EventArgs e)
		{
			IWebAuthenticationAuthorizationWebClientManager webAuthenticationAuthorizationWebClientManager = new WebAuthenticationAuthorizationWebClientManager();
			webAuthenticationAuthorizationWebClientManager.Logout();
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000AB RID: 171 RVA: 0x00004E77 File Offset: 0x00003077
		public bool IsExemptFromRequiredSessionFormCheck
		{
			get
			{
				return this._isExemptFromRequiredSessionFormCheck;
			}
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x060000AC RID: 172 RVA: 0x00004E80 File Offset: 0x00003080
		// (remove) Token: 0x060000AD RID: 173 RVA: 0x00004EB8 File Offset: 0x000030B8
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<IsExemptFromRequiredSessionFormCheckEventArgs> OnGetIsExemptFromRequiredSessionFormCheck;

		// Token: 0x060000AE RID: 174 RVA: 0x00004EF0 File Offset: 0x000030F0
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

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00004FC0 File Offset: 0x000031C0
		public bool IsExemptFromAuthentication
		{
			get
			{
				return this._isExemptFromAuthentication;
			}
		}

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x060000B0 RID: 176 RVA: 0x00004FC8 File Offset: 0x000031C8
		// (remove) Token: 0x060000B1 RID: 177 RVA: 0x00005000 File Offset: 0x00003200
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<IsExemptFromAuthenticationEventArgs> OnGetIsExemptFromAuthenticationEventArgs;

		// Token: 0x060000B2 RID: 178 RVA: 0x00005038 File Offset: 0x00003238
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

		// Token: 0x060000B3 RID: 179 RVA: 0x000050FC File Offset: 0x000032FC
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

		// Token: 0x060000B4 RID: 180 RVA: 0x000051F0 File Offset: 0x000033F0
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

		// Token: 0x060000B5 RID: 181 RVA: 0x00005408 File Offset: 0x00003608
		public string getLocation()
		{
			return "~/custom/";
		}

		// Token: 0x0400005E RID: 94
		private bool _isExemptFromRequiredSessionFormCheck = false;

		// Token: 0x04000060 RID: 96
		private bool _isExemptFromAuthentication = false;

		// Token: 0x04000062 RID: 98
		protected HtmlHead Head1;

		// Token: 0x04000063 RID: 99
		protected HtmlForm form1;

		// Token: 0x04000064 RID: 100
		protected ContentPlaceHolder placeholder_topbar_left;

		// Token: 0x04000065 RID: 101
		protected HyperLink link_home;

		// Token: 0x04000066 RID: 102
		protected Panel p_loggedin;

		// Token: 0x04000067 RID: 103
		protected Label lbl_loggedin;

		// Token: 0x04000068 RID: 104
		protected Label lbl_loggedinseparator;

		// Token: 0x04000069 RID: 105
		protected LinkButton link_logout;

		// Token: 0x0400006A RID: 106
		protected ContentPlaceHolder placeholder_navigation;

		// Token: 0x0400006B RID: 107
		protected ContentPlaceHolder placeholder_main;

		// Token: 0x0400006C RID: 108
		protected ContentPlaceHolder placeholder_menu;

		// Token: 0x0400006D RID: 109
		protected ContentPlaceHolder placeholder_footer;
	}
}
