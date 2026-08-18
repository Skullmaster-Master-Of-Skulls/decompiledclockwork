using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;
using System.Security.Permissions;
using System.Web.Security;

namespace System.Web.UI.WebControls
{
	// Token: 0x020005D8 RID: 1496
	[DefaultEvent("LoggingOut")]
	[Bindable(false)]
	[Designer("System.Web.UI.Design.WebControls.LoginStatusDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class LoginStatus : CompositeControl
	{
		// Token: 0x17001225 RID: 4645
		// (get) Token: 0x0600493A RID: 18746 RVA: 0x0012A888 File Offset: 0x00129888
		// (set) Token: 0x0600493B RID: 18747 RVA: 0x0012A890 File Offset: 0x00129890
		private bool LoggedIn
		{
			get
			{
				return this._loggedIn;
			}
			set
			{
				this._loggedIn = value;
			}
		}

		// Token: 0x17001226 RID: 4646
		// (get) Token: 0x0600493C RID: 18748 RVA: 0x0012A89C File Offset: 0x0012989C
		// (set) Token: 0x0600493D RID: 18749 RVA: 0x0012A8C9 File Offset: 0x001298C9
		[WebSysDescription("LoginStatus_LoginImageUrl")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[UrlProperty]
		public virtual string LoginImageUrl
		{
			get
			{
				object obj = this.ViewState["LoginImageUrl"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["LoginImageUrl"] = value;
			}
		}

		// Token: 0x17001227 RID: 4647
		// (get) Token: 0x0600493E RID: 18750 RVA: 0x0012A8DC File Offset: 0x001298DC
		// (set) Token: 0x0600493F RID: 18751 RVA: 0x0012A90E File Offset: 0x0012990E
		[WebSysDescription("LoginStatus_LoginText")]
		[Localizable(true)]
		[WebCategory("Appearance")]
		[WebSysDefaultValue("LoginStatus_DefaultLoginText")]
		public virtual string LoginText
		{
			get
			{
				object obj = this.ViewState["LoginText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return SR.GetString("LoginStatus_DefaultLoginText");
			}
			set
			{
				this.ViewState["LoginText"] = value;
			}
		}

		// Token: 0x17001228 RID: 4648
		// (get) Token: 0x06004940 RID: 18752 RVA: 0x0012A924 File Offset: 0x00129924
		// (set) Token: 0x06004941 RID: 18753 RVA: 0x0012A94D File Offset: 0x0012994D
		[WebCategory("Behavior")]
		[WebSysDescription("LoginStatus_LogoutAction")]
		[DefaultValue(LogoutAction.Refresh)]
		[Themeable(false)]
		public virtual LogoutAction LogoutAction
		{
			get
			{
				object obj = this.ViewState["LogoutAction"];
				if (obj != null)
				{
					return (LogoutAction)obj;
				}
				return LogoutAction.Refresh;
			}
			set
			{
				if (value < LogoutAction.Refresh || value > LogoutAction.RedirectToLoginPage)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["LogoutAction"] = value;
			}
		}

		// Token: 0x17001229 RID: 4649
		// (get) Token: 0x06004942 RID: 18754 RVA: 0x0012A978 File Offset: 0x00129978
		// (set) Token: 0x06004943 RID: 18755 RVA: 0x0012A9A5 File Offset: 0x001299A5
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[UrlProperty]
		[WebCategory("Appearance")]
		[WebSysDescription("LoginStatus_LogoutImageUrl")]
		public virtual string LogoutImageUrl
		{
			get
			{
				object obj = this.ViewState["LogoutImageUrl"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["LogoutImageUrl"] = value;
			}
		}

		// Token: 0x1700122A RID: 4650
		// (get) Token: 0x06004944 RID: 18756 RVA: 0x0012A9B8 File Offset: 0x001299B8
		// (set) Token: 0x06004945 RID: 18757 RVA: 0x0012A9E5 File Offset: 0x001299E5
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[UrlProperty]
		[Themeable(false)]
		[WebCategory("Behavior")]
		[DefaultValue("")]
		[WebSysDescription("LoginStatus_LogoutPageUrl")]
		public virtual string LogoutPageUrl
		{
			get
			{
				object obj = this.ViewState["LogoutPageUrl"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["LogoutPageUrl"] = value;
			}
		}

		// Token: 0x1700122B RID: 4651
		// (get) Token: 0x06004946 RID: 18758 RVA: 0x0012A9F8 File Offset: 0x001299F8
		// (set) Token: 0x06004947 RID: 18759 RVA: 0x0012AA2A File Offset: 0x00129A2A
		[Localizable(true)]
		[WebSysDefaultValue("LoginStatus_DefaultLogoutText")]
		[WebSysDescription("LoginStatus_LogoutText")]
		[WebCategory("Appearance")]
		public virtual string LogoutText
		{
			get
			{
				object obj = this.ViewState["LogoutText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return SR.GetString("LoginStatus_DefaultLogoutText");
			}
			set
			{
				this.ViewState["LogoutText"] = value;
			}
		}

		// Token: 0x1700122C RID: 4652
		// (get) Token: 0x06004948 RID: 18760 RVA: 0x0012AA3D File Offset: 0x00129A3D
		private string NavigateUrl
		{
			get
			{
				if (!base.DesignMode)
				{
					return FormsAuthentication.GetLoginPage(null, true);
				}
				return "url";
			}
		}

		// Token: 0x1700122D RID: 4653
		// (get) Token: 0x06004949 RID: 18761 RVA: 0x0012AA54 File Offset: 0x00129A54
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.A;
			}
		}

		// Token: 0x140000D2 RID: 210
		// (add) Token: 0x0600494A RID: 18762 RVA: 0x0012AA57 File Offset: 0x00129A57
		// (remove) Token: 0x0600494B RID: 18763 RVA: 0x0012AA6A File Offset: 0x00129A6A
		[WebSysDescription("LoginStatus_LoggedOut")]
		[WebCategory("Action")]
		public event EventHandler LoggedOut
		{
			add
			{
				base.Events.AddHandler(LoginStatus.EventLoggedOut, value);
			}
			remove
			{
				base.Events.RemoveHandler(LoginStatus.EventLoggedOut, value);
			}
		}

		// Token: 0x140000D3 RID: 211
		// (add) Token: 0x0600494C RID: 18764 RVA: 0x0012AA7D File Offset: 0x00129A7D
		// (remove) Token: 0x0600494D RID: 18765 RVA: 0x0012AA90 File Offset: 0x00129A90
		[WebCategory("Action")]
		[WebSysDescription("LoginStatus_LoggingOut")]
		public event LoginCancelEventHandler LoggingOut
		{
			add
			{
				base.Events.AddHandler(LoginStatus.EventLoggingOut, value);
			}
			remove
			{
				base.Events.RemoveHandler(LoginStatus.EventLoggingOut, value);
			}
		}

		// Token: 0x0600494E RID: 18766 RVA: 0x0012AAA4 File Offset: 0x00129AA4
		protected internal override void CreateChildControls()
		{
			this.Controls.Clear();
			this._logInLinkButton = new LinkButton();
			this._logInImageButton = new ImageButton();
			this._logOutLinkButton = new LinkButton();
			this._logOutImageButton = new ImageButton();
			this._logInLinkButton.EnableViewState = false;
			this._logInImageButton.EnableViewState = false;
			this._logOutLinkButton.EnableViewState = false;
			this._logOutImageButton.EnableViewState = false;
			this._logInLinkButton.EnableTheming = false;
			this._logInImageButton.EnableTheming = false;
			this._logInLinkButton.CausesValidation = false;
			this._logInImageButton.CausesValidation = false;
			this._logOutLinkButton.EnableTheming = false;
			this._logOutImageButton.EnableTheming = false;
			this._logOutLinkButton.CausesValidation = false;
			this._logOutImageButton.CausesValidation = false;
			CommandEventHandler value = new CommandEventHandler(this.LogoutClicked);
			this._logOutLinkButton.Command += value;
			this._logOutImageButton.Command += value;
			value = new CommandEventHandler(this.LoginClicked);
			this._logInLinkButton.Command += value;
			this._logInImageButton.Command += value;
			this.Controls.Add(this._logOutLinkButton);
			this.Controls.Add(this._logOutImageButton);
			this.Controls.Add(this._logInLinkButton);
			this.Controls.Add(this._logInImageButton);
		}

		// Token: 0x0600494F RID: 18767 RVA: 0x0012AC08 File Offset: 0x00129C08
		private void LogoutClicked(object Source, CommandEventArgs e)
		{
			LoginCancelEventArgs loginCancelEventArgs = new LoginCancelEventArgs();
			this.OnLoggingOut(loginCancelEventArgs);
			if (loginCancelEventArgs.Cancel)
			{
				return;
			}
			FormsAuthentication.SignOut();
			this.Page.Response.Clear();
			this.Page.Response.StatusCode = 200;
			this.OnLoggedOut(EventArgs.Empty);
			switch (this.LogoutAction)
			{
			case LogoutAction.Refresh:
				if (this.Page.Form != null && string.Equals(this.Page.Form.Method, "get", StringComparison.OrdinalIgnoreCase))
				{
					this.Page.Response.Redirect(this.Page.Request.Path, false);
					return;
				}
				this.Page.Response.Redirect(this.Page.Request.PathWithQueryString, false);
				return;
			case LogoutAction.Redirect:
			{
				string text = this.LogoutPageUrl;
				if (!string.IsNullOrEmpty(text))
				{
					text = base.ResolveClientUrl(text);
				}
				else
				{
					text = FormsAuthentication.LoginUrl;
				}
				this.Page.Response.Redirect(text, false);
				return;
			}
			case LogoutAction.RedirectToLoginPage:
				this.Page.Response.Redirect(FormsAuthentication.LoginUrl, false);
				return;
			default:
				return;
			}
		}

		// Token: 0x06004950 RID: 18768 RVA: 0x0012AD30 File Offset: 0x00129D30
		private void LoginClicked(object Source, CommandEventArgs e)
		{
			this.Page.Response.Redirect(base.ResolveClientUrl(this.NavigateUrl), false);
		}

		// Token: 0x06004951 RID: 18769 RVA: 0x0012AD50 File Offset: 0x00129D50
		protected virtual void OnLoggedOut(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[LoginStatus.EventLoggedOut];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06004952 RID: 18770 RVA: 0x0012AD80 File Offset: 0x00129D80
		protected virtual void OnLoggingOut(LoginCancelEventArgs e)
		{
			LoginCancelEventHandler loginCancelEventHandler = (LoginCancelEventHandler)base.Events[LoginStatus.EventLoggingOut];
			if (loginCancelEventHandler != null)
			{
				loginCancelEventHandler(this, e);
			}
		}

		// Token: 0x06004953 RID: 18771 RVA: 0x0012ADAE File Offset: 0x00129DAE
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			this.LoggedIn = this.Page.Request.IsAuthenticated;
		}

		// Token: 0x06004954 RID: 18772 RVA: 0x0012ADCD File Offset: 0x00129DCD
		protected internal override void Render(HtmlTextWriter writer)
		{
			this.RenderContents(writer);
		}

		// Token: 0x06004955 RID: 18773 RVA: 0x0012ADD8 File Offset: 0x00129DD8
		protected internal override void RenderContents(HtmlTextWriter writer)
		{
			if (this.Page != null)
			{
				this.Page.VerifyRenderingInServerForm(this);
			}
			this.SetChildProperties();
			if (this.ID != null && this.ID.Length != 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
			}
			base.RenderContents(writer);
		}

		// Token: 0x06004956 RID: 18774 RVA: 0x0012AE2C File Offset: 0x00129E2C
		private void SetChildProperties()
		{
			this.EnsureChildControls();
			this._logInLinkButton.Visible = false;
			this._logInImageButton.Visible = false;
			this._logOutLinkButton.Visible = false;
			this._logOutImageButton.Visible = false;
			bool loggedIn = this.LoggedIn;
			WebControl webControl;
			if (loggedIn)
			{
				string logoutImageUrl = this.LogoutImageUrl;
				if (logoutImageUrl.Length > 0)
				{
					this._logOutImageButton.AlternateText = this.LogoutText;
					this._logOutImageButton.ImageUrl = logoutImageUrl;
					webControl = this._logOutImageButton;
				}
				else
				{
					this._logOutLinkButton.Text = this.LogoutText;
					webControl = this._logOutLinkButton;
				}
			}
			else
			{
				string loginImageUrl = this.LoginImageUrl;
				if (loginImageUrl.Length > 0)
				{
					this._logInImageButton.AlternateText = this.LoginText;
					this._logInImageButton.ImageUrl = loginImageUrl;
					webControl = this._logInImageButton;
				}
				else
				{
					this._logInLinkButton.Text = this.LoginText;
					webControl = this._logInLinkButton;
				}
			}
			webControl.CopyBaseAttributes(this);
			webControl.ApplyStyle(base.ControlStyle);
			webControl.Visible = true;
		}

		// Token: 0x06004957 RID: 18775 RVA: 0x0012AF34 File Offset: 0x00129F34
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		protected override void SetDesignModeState(IDictionary data)
		{
			if (data != null)
			{
				object obj = data["LoggedIn"];
				if (obj != null)
				{
					this.LoggedIn = (bool)obj;
				}
			}
		}

		// Token: 0x04002B1E RID: 11038
		private static readonly object EventLoggingOut = new object();

		// Token: 0x04002B1F RID: 11039
		private static readonly object EventLoggedOut = new object();

		// Token: 0x04002B20 RID: 11040
		private LinkButton _logInLinkButton;

		// Token: 0x04002B21 RID: 11041
		private ImageButton _logInImageButton;

		// Token: 0x04002B22 RID: 11042
		private LinkButton _logOutLinkButton;

		// Token: 0x04002B23 RID: 11043
		private ImageButton _logOutImageButton;

		// Token: 0x04002B24 RID: 11044
		private bool _loggedIn;
	}
}
