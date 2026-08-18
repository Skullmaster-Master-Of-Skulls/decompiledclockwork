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
	// Token: 0x020000FE RID: 254
	public class StaffMasterR : MasterPage, IClockWorkMasterPageAuth
	{
		// Token: 0x0600075A RID: 1882 RVA: 0x00038238 File Offset: 0x00036438
		protected void link_logout_Click(object sender, EventArgs e)
		{
			IWebAuthenticationAuthorizationWebClientManager webAuthenticationAuthorizationWebClientManager = new WebAuthenticationAuthorizationWebClientManager();
			webAuthenticationAuthorizationWebClientManager.Logout();
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x0600075B RID: 1883 RVA: 0x00038253 File Offset: 0x00036453
		public bool IsExemptFromRequiredSessionFormCheck
		{
			get
			{
				return this._isExemptFromRequiredSessionFormCheck;
			}
		}

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x0600075C RID: 1884 RVA: 0x0003825C File Offset: 0x0003645C
		// (remove) Token: 0x0600075D RID: 1885 RVA: 0x00038294 File Offset: 0x00036494
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<IsExemptFromRequiredSessionFormCheckEventArgs> OnGetIsExemptFromRequiredSessionFormCheck;

		// Token: 0x0600075E RID: 1886 RVA: 0x000382CC File Offset: 0x000364CC
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

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x0600075F RID: 1887 RVA: 0x0003839C File Offset: 0x0003659C
		public bool IsExemptFromAuthentication
		{
			get
			{
				return this._isExemptFromAuthentication;
			}
		}

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x06000760 RID: 1888 RVA: 0x000383A4 File Offset: 0x000365A4
		// (remove) Token: 0x06000761 RID: 1889 RVA: 0x000383DC File Offset: 0x000365DC
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<IsExemptFromAuthenticationEventArgs> OnGetIsExemptFromAuthenticationEventArgs;

		// Token: 0x06000762 RID: 1890 RVA: 0x00038414 File Offset: 0x00036614
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

		// Token: 0x06000763 RID: 1891 RVA: 0x000384E8 File Offset: 0x000366E8
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

		// Token: 0x06000764 RID: 1892 RVA: 0x000385B0 File Offset: 0x000367B0
		public string getLocation()
		{
			return "~/custom/";
		}

		// Token: 0x040005B4 RID: 1460
		private bool _isExemptFromRequiredSessionFormCheck = false;

		// Token: 0x040005B6 RID: 1462
		private bool _isExemptFromAuthentication = false;

		// Token: 0x040005B8 RID: 1464
		protected HtmlHead Head1;

		// Token: 0x040005B9 RID: 1465
		protected ContentPlaceHolder header;

		// Token: 0x040005BA RID: 1466
		protected HtmlForm form1;

		// Token: 0x040005BB RID: 1467
		protected ContentPlaceHolder placeholder_topbar_left;

		// Token: 0x040005BC RID: 1468
		protected HyperLink link_home;

		// Token: 0x040005BD RID: 1469
		protected Image img_home;

		// Token: 0x040005BE RID: 1470
		protected Panel p_loggedin;

		// Token: 0x040005BF RID: 1471
		protected Label lbl_loggedin;

		// Token: 0x040005C0 RID: 1472
		protected Label lbl_loggedinseparator;

		// Token: 0x040005C1 RID: 1473
		protected LinkButton link_logout;

		// Token: 0x040005C2 RID: 1474
		protected ContentPlaceHolder ContentPlaceHolder1;

		// Token: 0x040005C3 RID: 1475
		protected ContentPlaceHolder placeholder_main;

		// Token: 0x040005C4 RID: 1476
		protected ContentPlaceHolder placeholder_menu;

		// Token: 0x040005C5 RID: 1477
		protected ContentPlaceHolder placeholder_footer;
	}
}
