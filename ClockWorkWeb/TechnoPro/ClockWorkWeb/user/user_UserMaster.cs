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
	// Token: 0x0200001C RID: 28
	public class user_UserMaster : MasterPage, IClockWorkMasterPageAuth
	{
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00004266 File Offset: 0x00002466
		public bool IsExemptFromRequiredSessionFormCheck
		{
			get
			{
				return this._isExemptFromRequiredSessionFormCheck;
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000090 RID: 144 RVA: 0x00004270 File Offset: 0x00002470
		// (remove) Token: 0x06000091 RID: 145 RVA: 0x000042A8 File Offset: 0x000024A8
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<IsExemptFromRequiredSessionFormCheckEventArgs> OnGetIsExemptFromRequiredSessionFormCheck;

		// Token: 0x06000092 RID: 146 RVA: 0x000042E0 File Offset: 0x000024E0
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

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000093 RID: 147 RVA: 0x000043A0 File Offset: 0x000025A0
		public bool IsExemptFromAuthentication
		{
			get
			{
				return this._isExemptFromAuthentication;
			}
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000094 RID: 148 RVA: 0x000043A8 File Offset: 0x000025A8
		// (remove) Token: 0x06000095 RID: 149 RVA: 0x000043E0 File Offset: 0x000025E0
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<IsExemptFromAuthenticationEventArgs> OnGetIsExemptFromAuthenticationEventArgs;

		// Token: 0x06000096 RID: 150 RVA: 0x00004418 File Offset: 0x00002618
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

		// Token: 0x06000097 RID: 151 RVA: 0x000044E9 File Offset: 0x000026E9
		private void ShowHideLoginInfo(bool show)
		{
			this.img_loggedin.Visible = show;
			this.lbl_loggedin.Visible = show;
			this.lbl_loggedinseparator.Visible = show;
			this.link_logout.Visible = show;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00004520 File Offset: 0x00002720
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

		// Token: 0x06000099 RID: 153 RVA: 0x00004614 File Offset: 0x00002814
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

		// Token: 0x0600009A RID: 154 RVA: 0x00004834 File Offset: 0x00002A34
		public string getLocation()
		{
			return "~/custom/";
		}

		// Token: 0x0600009B RID: 155 RVA: 0x0000484C File Offset: 0x00002A4C
		protected void link_logout_Click(object sender, EventArgs e)
		{
			IWebAuthenticationAuthorizationWebClientManager webAuthenticationAuthorizationWebClientManager = new WebAuthenticationAuthorizationWebClientManager();
			webAuthenticationAuthorizationWebClientManager.Logout();
		}

		// Token: 0x04000034 RID: 52
		private bool _isExemptFromRequiredSessionFormCheck = false;

		// Token: 0x04000036 RID: 54
		private bool _isExemptFromAuthentication = false;

		// Token: 0x04000038 RID: 56
		protected HtmlHead Head1;

		// Token: 0x04000039 RID: 57
		protected ContentPlaceHolder head;

		// Token: 0x0400003A RID: 58
		protected ContentPlaceHolder myPlaceholder;

		// Token: 0x0400003B RID: 59
		protected ContentPlaceHolder header;

		// Token: 0x0400003C RID: 60
		protected HtmlForm form1;

		// Token: 0x0400003D RID: 61
		protected ContentPlaceHolder placeholder_topbar_left;

		// Token: 0x0400003E RID: 62
		protected HyperLink link_home;

		// Token: 0x0400003F RID: 63
		protected Image img_home;

		// Token: 0x04000040 RID: 64
		protected Panel p_loggedin;

		// Token: 0x04000041 RID: 65
		protected Image img_loggedin;

		// Token: 0x04000042 RID: 66
		protected Label lbl_loggedin;

		// Token: 0x04000043 RID: 67
		protected Label lbl_loggedinseparator;

		// Token: 0x04000044 RID: 68
		protected LinkButton link_logout;

		// Token: 0x04000045 RID: 69
		protected ContentPlaceHolder placeholder_main;

		// Token: 0x04000046 RID: 70
		protected ContentPlaceHolder placeholder_navigation;

		// Token: 0x04000047 RID: 71
		protected ContentPlaceHolder placeholder_menu;

		// Token: 0x04000048 RID: 72
		protected ContentPlaceHolder placeholder_footer;
	}
}
