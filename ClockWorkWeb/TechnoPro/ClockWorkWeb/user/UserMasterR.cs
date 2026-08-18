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
	// Token: 0x0200001F RID: 31
	public class UserMasterR : MasterPage, IClockWorkMasterPageAuth
	{
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00005436 File Offset: 0x00003636
		public bool IsExemptFromRequiredSessionFormCheck
		{
			get
			{
				return this._isExemptFromRequiredSessionFormCheck;
			}
		}

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x060000B8 RID: 184 RVA: 0x00005440 File Offset: 0x00003640
		// (remove) Token: 0x060000B9 RID: 185 RVA: 0x00005478 File Offset: 0x00003678
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<IsExemptFromRequiredSessionFormCheckEventArgs> OnGetIsExemptFromRequiredSessionFormCheck;

		// Token: 0x060000BA RID: 186 RVA: 0x000054B0 File Offset: 0x000036B0
		private bool FireOnGetIsExemptFromRequiredSessionFormCheckEventArgs()
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

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000BB RID: 187 RVA: 0x00005570 File Offset: 0x00003770
		public bool IsExemptFromAuthentication
		{
			get
			{
				return this._isExemptFromAuthentication;
			}
		}

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x060000BC RID: 188 RVA: 0x00005578 File Offset: 0x00003778
		// (remove) Token: 0x060000BD RID: 189 RVA: 0x000055B0 File Offset: 0x000037B0
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<IsExemptFromAuthenticationEventArgs> OnGetIsExemptFromAuthenticationEventArgs;

		// Token: 0x060000BE RID: 190 RVA: 0x000055E8 File Offset: 0x000037E8
		private bool FireOnGetIsExemptFromAuthenticationEventArgs()
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

		// Token: 0x060000BF RID: 191 RVA: 0x000056B9 File Offset: 0x000038B9
		private void ShowHideLoginInfo(bool show)
		{
			this.img_loggedin.Visible = show;
			this.lbl_loggedin.Visible = show;
			this.lbl_loggedinseparator.Visible = show;
			this.link_logout.Visible = show;
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x000056F0 File Offset: 0x000038F0
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

		// Token: 0x060000C1 RID: 193 RVA: 0x000057E4 File Offset: 0x000039E4
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
					base.Response.Redirect("~/user/misc/notallowed.aspx?code=disabledip", false);
					return;
				}
			}
			bool flag5 = !this.Page.IsPostBack;
			if (flag5)
			{
				IWebAuthenticationAuthorizationWebClientManager webAuthenticationAuthorizationWebClientManager = new WebAuthenticationAuthorizationWebClientManager();
				ClockWorkIdentity currentClockWorkIdentity = webAuthenticationAuthorizationWebClientManager.GetCurrentClockWorkIdentity(this.Page);
				bool flag6 = !this._isExemptFromAuthentication && !string.IsNullOrEmpty((currentClockWorkIdentity != null) ? currentClockWorkIdentity.UserName : null);
				if (flag6)
				{
					string userName = currentClockWorkIdentity.UserName;
					this.lbl_loggedin.Text = userName;
					this.ShowHideLoginInfo(true);
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
				else
				{
					this.ShowHideLoginInfo(false);
				}
			}
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00005A04 File Offset: 0x00003C04
		public string getLocation()
		{
			return "~/custom/";
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00005A1C File Offset: 0x00003C1C
		protected void link_logout_Click(object sender, EventArgs e)
		{
			IWebAuthenticationAuthorizationWebClientManager webAuthenticationAuthorizationWebClientManager = new WebAuthenticationAuthorizationWebClientManager();
			webAuthenticationAuthorizationWebClientManager.Logout();
		}

		// Token: 0x0400006E RID: 110
		private bool _isExemptFromRequiredSessionFormCheck = false;

		// Token: 0x04000070 RID: 112
		private bool _isExemptFromAuthentication = false;

		// Token: 0x04000072 RID: 114
		protected HtmlHead Head1;

		// Token: 0x04000073 RID: 115
		protected ContentPlaceHolder head;

		// Token: 0x04000074 RID: 116
		protected ContentPlaceHolder myPlaceholder;

		// Token: 0x04000075 RID: 117
		protected ContentPlaceHolder header;

		// Token: 0x04000076 RID: 118
		protected HtmlForm form1;

		// Token: 0x04000077 RID: 119
		protected ContentPlaceHolder placeholder_topbar_left;

		// Token: 0x04000078 RID: 120
		protected HyperLink link_home;

		// Token: 0x04000079 RID: 121
		protected Image img_home;

		// Token: 0x0400007A RID: 122
		protected Panel p_loggedin;

		// Token: 0x0400007B RID: 123
		protected Image img_loggedin;

		// Token: 0x0400007C RID: 124
		protected Label lbl_loggedin;

		// Token: 0x0400007D RID: 125
		protected Label lbl_loggedinseparator;

		// Token: 0x0400007E RID: 126
		protected LinkButton link_logout;

		// Token: 0x0400007F RID: 127
		protected ContentPlaceHolder placeholder_main;

		// Token: 0x04000080 RID: 128
		protected ContentPlaceHolder placeholder_navigation;

		// Token: 0x04000081 RID: 129
		protected ContentPlaceHolder placeholder_menu;

		// Token: 0x04000082 RID: 130
		protected ContentPlaceHolder placeholder_footer;
	}
}
