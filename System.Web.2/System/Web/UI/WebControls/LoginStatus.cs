using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;
using System.Security.Permissions;
using System.Web.Security;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000463 RID: 1123
	[Bindable(false)]
	[DefaultEvent("LoggingOut")]
	[Designer("System.Web.UI.Design.WebControls.LoginStatusDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public class LoginStatus : CompositeControl
	{
		// Token: 0x17000FE1 RID: 4065
		// (get) Token: 0x06003688 RID: 13960 RVA: 0x000B0720 File Offset: 0x000AE920
		// (set) Token: 0x06003689 RID: 13961 RVA: 0x000B0728 File Offset: 0x000AE928
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

		// Token: 0x17000FE2 RID: 4066
		// (get) Token: 0x0600368A RID: 13962 RVA: 0x000B0734 File Offset: 0x000AE934
		// (set) Token: 0x0600368B RID: 13963 RVA: 0x000B0761 File Offset: 0x000AE961
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[WebSysDescription("LoginStatus_LoginImageUrl")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
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

		// Token: 0x17000FE3 RID: 4067
		// (get) Token: 0x0600368C RID: 13964 RVA: 0x000B0774 File Offset: 0x000AE974
		// (set) Token: 0x0600368D RID: 13965 RVA: 0x000B07A6 File Offset: 0x000AE9A6
		[Localizable(true)]
		[WebCategory("Appearance")]
		[WebSysDefaultValue("LoginStatus_DefaultLoginText")]
		[WebSysDescription("LoginStatus_LoginText")]
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

		// Token: 0x17000FE4 RID: 4068
		// (get) Token: 0x0600368E RID: 13966 RVA: 0x000B07BC File Offset: 0x000AE9BC
		// (set) Token: 0x0600368F RID: 13967 RVA: 0x000B07E5 File Offset: 0x000AE9E5
		[WebCategory("Behavior")]
		[DefaultValue(LogoutAction.Refresh)]
		[Themeable(false)]
		[WebSysDescription("LoginStatus_LogoutAction")]
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

		// Token: 0x17000FE5 RID: 4069
		// (get) Token: 0x06003690 RID: 13968 RVA: 0x000B0810 File Offset: 0x000AEA10
		// (set) Token: 0x06003691 RID: 13969 RVA: 0x000B083D File Offset: 0x000AEA3D
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[WebSysDescription("LoginStatus_LogoutImageUrl")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[UrlProperty]
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

		// Token: 0x17000FE6 RID: 4070
		// (get) Token: 0x06003692 RID: 13970 RVA: 0x000B0850 File Offset: 0x000AEA50
		// (set) Token: 0x06003693 RID: 13971 RVA: 0x000B087D File Offset: 0x000AEA7D
		[WebCategory("Behavior")]
		[DefaultValue("")]
		[WebSysDescription("LoginStatus_LogoutPageUrl")]
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[Themeable(false)]
		[UrlProperty]
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

		// Token: 0x17000FE7 RID: 4071
		// (get) Token: 0x06003694 RID: 13972 RVA: 0x000B0890 File Offset: 0x000AEA90
		// (set) Token: 0x06003695 RID: 13973 RVA: 0x000B08C2 File Offset: 0x000AEAC2
		[Localizable(true)]
		[WebCategory("Appearance")]
		[WebSysDefaultValue("LoginStatus_DefaultLogoutText")]
		[WebSysDescription("LoginStatus_LogoutText")]
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

		// Token: 0x17000FE8 RID: 4072
		// (get) Token: 0x06003696 RID: 13974 RVA: 0x000B08D5 File Offset: 0x000AEAD5
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

		// Token: 0x17000FE9 RID: 4073
		// (get) Token: 0x06003697 RID: 13975 RVA: 0x000097B7 File Offset: 0x000079B7
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.A;
			}
		}

		// Token: 0x140000B6 RID: 182
		// (add) Token: 0x06003698 RID: 13976 RVA: 0x000B08EC File Offset: 0x000AEAEC
		// (remove) Token: 0x06003699 RID: 13977 RVA: 0x000B08FF File Offset: 0x000AEAFF
		[WebCategory("Action")]
		[WebSysDescription("LoginStatus_LoggedOut")]
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

		// Token: 0x140000B7 RID: 183
		// (add) Token: 0x0600369A RID: 13978 RVA: 0x000B0912 File Offset: 0x000AEB12
		// (remove) Token: 0x0600369B RID: 13979 RVA: 0x000B0925 File Offset: 0x000AEB25
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

		// Token: 0x0600369C RID: 13980 RVA: 0x000B0938 File Offset: 0x000AEB38
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

		// Token: 0x0600369D RID: 13981 RVA: 0x000B0A9C File Offset: 0x000AEC9C
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
					this.Page.Response.Redirect(this.Page.Request.ClientFilePath.VirtualPathString, false);
					return;
				}
				this.Page.Response.Redirect(this.Page.Request.RawUrl, false);
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

		// Token: 0x0600369E RID: 13982 RVA: 0x000B0BC9 File Offset: 0x000AEDC9
		private void LoginClicked(object Source, CommandEventArgs e)
		{
			this.Page.Response.Redirect(base.ResolveClientUrl(this.NavigateUrl), false);
		}

		// Token: 0x0600369F RID: 13983 RVA: 0x000B0BE8 File Offset: 0x000AEDE8
		protected virtual void OnLoggedOut(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[LoginStatus.EventLoggedOut];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060036A0 RID: 13984 RVA: 0x000B0C18 File Offset: 0x000AEE18
		protected virtual void OnLoggingOut(LoginCancelEventArgs e)
		{
			LoginCancelEventHandler loginCancelEventHandler = (LoginCancelEventHandler)base.Events[LoginStatus.EventLoggingOut];
			if (loginCancelEventHandler != null)
			{
				loginCancelEventHandler(this, e);
			}
		}

		// Token: 0x060036A1 RID: 13985 RVA: 0x000B0C46 File Offset: 0x000AEE46
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			this.LoggedIn = this.Page.Request.IsAuthenticated;
		}

		// Token: 0x060036A2 RID: 13986 RVA: 0x000B0C65 File Offset: 0x000AEE65
		protected internal override void Render(HtmlTextWriter writer)
		{
			this.RenderContents(writer);
		}

		// Token: 0x060036A3 RID: 13987 RVA: 0x000B0C70 File Offset: 0x000AEE70
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

		// Token: 0x060036A4 RID: 13988 RVA: 0x000B0CC4 File Offset: 0x000AEEC4
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

		// Token: 0x060036A5 RID: 13989 RVA: 0x000B0DCC File Offset: 0x000AEFCC
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

		// Token: 0x0400220C RID: 8716
		private static readonly object EventLoggingOut = new object();

		// Token: 0x0400220D RID: 8717
		private static readonly object EventLoggedOut = new object();

		// Token: 0x0400220E RID: 8718
		private LinkButton _logInLinkButton;

		// Token: 0x0400220F RID: 8719
		private ImageButton _logInImageButton;

		// Token: 0x04002210 RID: 8720
		private LinkButton _logOutLinkButton;

		// Token: 0x04002211 RID: 8721
		private ImageButton _logOutImageButton;

		// Token: 0x04002212 RID: 8722
		private bool _loggedIn;
	}
}
