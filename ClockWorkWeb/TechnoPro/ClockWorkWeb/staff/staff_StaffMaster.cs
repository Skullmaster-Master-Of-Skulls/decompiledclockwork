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
	// Token: 0x020000FC RID: 252
	public class staff_StaffMaster : MasterPage, IClockWorkMasterPageAuth
	{
		// Token: 0x06000742 RID: 1858 RVA: 0x00037AE8 File Offset: 0x00035CE8
		protected void link_logout_Click(object sender, EventArgs e)
		{
			IWebAuthenticationAuthorizationWebClientManager webAuthenticationAuthorizationWebClientManager = new WebAuthenticationAuthorizationWebClientManager();
			webAuthenticationAuthorizationWebClientManager.Logout();
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000743 RID: 1859 RVA: 0x00037B03 File Offset: 0x00035D03
		public bool IsExemptFromRequiredSessionFormCheck
		{
			get
			{
				return this._isExemptFromRequiredSessionFormCheck;
			}
		}

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06000744 RID: 1860 RVA: 0x00037B0C File Offset: 0x00035D0C
		// (remove) Token: 0x06000745 RID: 1861 RVA: 0x00037B44 File Offset: 0x00035D44
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<IsExemptFromRequiredSessionFormCheckEventArgs> OnGetIsExemptFromRequiredSessionFormCheck;

		// Token: 0x06000746 RID: 1862 RVA: 0x00037B7C File Offset: 0x00035D7C
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

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000747 RID: 1863 RVA: 0x00037C4C File Offset: 0x00035E4C
		public bool IsExemptFromAuthentication
		{
			get
			{
				return this._isExemptFromAuthentication;
			}
		}

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x06000748 RID: 1864 RVA: 0x00037C54 File Offset: 0x00035E54
		// (remove) Token: 0x06000749 RID: 1865 RVA: 0x00037C8C File Offset: 0x00035E8C
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<IsExemptFromAuthenticationEventArgs> OnGetIsExemptFromAuthenticationEventArgs;

		// Token: 0x0600074A RID: 1866 RVA: 0x00037CC4 File Offset: 0x00035EC4
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

		// Token: 0x0600074B RID: 1867 RVA: 0x00037D98 File Offset: 0x00035F98
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

		// Token: 0x0600074C RID: 1868 RVA: 0x00037E60 File Offset: 0x00036060
		public string getLocation()
		{
			return "~/custom/";
		}

		// Token: 0x0400058D RID: 1421
		private bool _isExemptFromRequiredSessionFormCheck = false;

		// Token: 0x0400058F RID: 1423
		private bool _isExemptFromAuthentication = false;

		// Token: 0x04000591 RID: 1425
		protected HtmlHead Head1;

		// Token: 0x04000592 RID: 1426
		protected ContentPlaceHolder head;

		// Token: 0x04000593 RID: 1427
		protected ContentPlaceHolder myPlaceholder;

		// Token: 0x04000594 RID: 1428
		protected ContentPlaceHolder header;

		// Token: 0x04000595 RID: 1429
		protected HtmlForm form1;

		// Token: 0x04000596 RID: 1430
		protected ContentPlaceHolder placeholder_topbar_left;

		// Token: 0x04000597 RID: 1431
		protected HyperLink link_home;

		// Token: 0x04000598 RID: 1432
		protected Image img_home;

		// Token: 0x04000599 RID: 1433
		protected Panel p_loggedin;

		// Token: 0x0400059A RID: 1434
		protected Image img_loggedin;

		// Token: 0x0400059B RID: 1435
		protected Label lbl_loggedin;

		// Token: 0x0400059C RID: 1436
		protected Label lbl_loggedinseparator;

		// Token: 0x0400059D RID: 1437
		protected LinkButton link_logout;

		// Token: 0x0400059E RID: 1438
		protected ContentPlaceHolder placeholder_navigation;

		// Token: 0x0400059F RID: 1439
		protected ContentPlaceHolder placeholder_main;

		// Token: 0x040005A0 RID: 1440
		protected ContentPlaceHolder placeholder_menu;

		// Token: 0x040005A1 RID: 1441
		protected ContentPlaceHolder placeholder_footer;
	}
}
