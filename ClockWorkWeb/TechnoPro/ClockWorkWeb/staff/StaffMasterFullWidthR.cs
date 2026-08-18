using System;
using System.Diagnostics;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Authentication;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;
using TechnoPro.Common.UI.Web.Entity;
using TechnoPro.Common.UI.Web.Entity.AuthenticationAuthorization;

namespace TechnoPro.ClockWorkWeb.staff
{
	// Token: 0x020000FD RID: 253
	public class StaffMasterFullWidthR : MasterPage, IClockWorkMasterPageAuth
	{
		// Token: 0x0600074E RID: 1870 RVA: 0x00037E90 File Offset: 0x00036090
		protected void link_logout_Click(object sender, EventArgs e)
		{
			IWebAuthenticationAuthorizationWebClientManager webAuthenticationAuthorizationWebClientManager = new WebAuthenticationAuthorizationWebClientManager();
			webAuthenticationAuthorizationWebClientManager.Logout();
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x0600074F RID: 1871 RVA: 0x00037EAB File Offset: 0x000360AB
		public bool IsExemptFromRequiredSessionFormCheck
		{
			get
			{
				return this._isExemptFromRequiredSessionFormCheck;
			}
		}

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x06000750 RID: 1872 RVA: 0x00037EB4 File Offset: 0x000360B4
		// (remove) Token: 0x06000751 RID: 1873 RVA: 0x00037EEC File Offset: 0x000360EC
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<IsExemptFromRequiredSessionFormCheckEventArgs> OnGetIsExemptFromRequiredSessionFormCheck;

		// Token: 0x06000752 RID: 1874 RVA: 0x00037F24 File Offset: 0x00036124
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

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000753 RID: 1875 RVA: 0x00037FF4 File Offset: 0x000361F4
		public bool IsExemptFromAuthentication
		{
			get
			{
				return this._isExemptFromAuthentication;
			}
		}

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x06000754 RID: 1876 RVA: 0x00037FFC File Offset: 0x000361FC
		// (remove) Token: 0x06000755 RID: 1877 RVA: 0x00038034 File Offset: 0x00036234
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<IsExemptFromAuthenticationEventArgs> OnGetIsExemptFromAuthenticationEventArgs;

		// Token: 0x06000756 RID: 1878 RVA: 0x0003806C File Offset: 0x0003626C
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

		// Token: 0x06000757 RID: 1879 RVA: 0x00038140 File Offset: 0x00036340
		protected void Page_Load(object sender, EventArgs e)
		{
			NavigatorClientManager.CurrentInstance.EnsurePageNotCached();
			this._isExemptFromAuthentication = this.FireOnGetIsExemptFromAuthenticationEventArgs();
			bool flag = !this._isExemptFromAuthentication;
			if (flag)
			{
				WebAuthenticationAuthorizationWebClientManager.CurrentInstance.ForceAuthenticate(this.Page);
			}
			this._isExemptFromRequiredSessionFormCheck = this.FireOnGetIsExemptFromRequiredSessionFormCheckEventArgs();
			bool flag2 = !this.Page.IsPostBack;
			if (flag2)
			{
				IWebAuthenticationAuthorizationWebClientManager webAuthenticationAuthorizationWebClientManager = new WebAuthenticationAuthorizationWebClientManager();
				ClockWorkIdentity currentClockWorkIdentity = webAuthenticationAuthorizationWebClientManager.GetCurrentClockWorkIdentity(this.Page);
				bool flag3 = !string.IsNullOrEmpty((currentClockWorkIdentity != null) ? currentClockWorkIdentity.UserName : null);
				if (flag3)
				{
					string userName = currentClockWorkIdentity.UserName;
					this.lbl_loggedin.Text = userName;
					this.p_loggedin.Visible = true;
				}
				else
				{
					this.p_loggedin.Visible = false;
				}
			}
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x00038208 File Offset: 0x00036408
		public string getLocation()
		{
			return "~/custom/";
		}

		// Token: 0x040005A2 RID: 1442
		private bool _isExemptFromRequiredSessionFormCheck = false;

		// Token: 0x040005A4 RID: 1444
		private bool _isExemptFromAuthentication = false;

		// Token: 0x040005A6 RID: 1446
		protected HtmlHead Head1;

		// Token: 0x040005A7 RID: 1447
		protected ContentPlaceHolder header;

		// Token: 0x040005A8 RID: 1448
		protected HtmlForm form1;

		// Token: 0x040005A9 RID: 1449
		protected ContentPlaceHolder placeholder_topbar_left;

		// Token: 0x040005AA RID: 1450
		protected HyperLink link_home;

		// Token: 0x040005AB RID: 1451
		protected Image img_home;

		// Token: 0x040005AC RID: 1452
		protected Panel p_loggedin;

		// Token: 0x040005AD RID: 1453
		protected Label lbl_loggedin;

		// Token: 0x040005AE RID: 1454
		protected Label lbl_loggedinseparator;

		// Token: 0x040005AF RID: 1455
		protected LinkButton link_logout;

		// Token: 0x040005B0 RID: 1456
		protected ContentPlaceHolder ContentPlaceHolder1;

		// Token: 0x040005B1 RID: 1457
		protected ContentPlaceHolder placeholder_main;

		// Token: 0x040005B2 RID: 1458
		protected ContentPlaceHolder placeholder_menu;

		// Token: 0x040005B3 RID: 1459
		protected ContentPlaceHolder placeholder_footer;
	}
}
