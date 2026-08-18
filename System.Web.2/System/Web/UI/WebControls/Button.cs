using System;
using System.ComponentModel;
using System.Drawing.Design;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000387 RID: 903
	[DataBindingHandler("System.Web.UI.Design.TextDataBindingHandler, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultEvent("Click")]
	[DefaultProperty("Text")]
	[Designer("System.Web.UI.Design.WebControls.ButtonDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ToolboxData("<{0}:Button runat=\"server\" Text=\"Button\"></{0}:Button>")]
	[SupportsEventValidation]
	public class Button : WebControl, IButtonControl, IPostBackEventHandler
	{
		// Token: 0x060029F7 RID: 10743 RVA: 0x00087CE0 File Offset: 0x00085EE0
		public Button() : base(HtmlTextWriterTag.Input)
		{
		}

		// Token: 0x17000BAC RID: 2988
		// (get) Token: 0x060029F8 RID: 10744 RVA: 0x00087CEC File Offset: 0x00085EEC
		// (set) Token: 0x060029F9 RID: 10745 RVA: 0x0007E239 File Offset: 0x0007C439
		[DefaultValue(true)]
		[Themeable(false)]
		[WebCategory("Behavior")]
		[WebSysDescription("Button_CausesValidation")]
		public virtual bool CausesValidation
		{
			get
			{
				object obj = this.ViewState["CausesValidation"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["CausesValidation"] = value;
			}
		}

		// Token: 0x17000BAD RID: 2989
		// (get) Token: 0x060029FA RID: 10746 RVA: 0x00087D18 File Offset: 0x00085F18
		// (set) Token: 0x060029FB RID: 10747 RVA: 0x00087D45 File Offset: 0x00085F45
		[DefaultValue("")]
		[Themeable(false)]
		[WebCategory("Behavior")]
		[WebSysDescription("WebControl_CommandName")]
		public string CommandName
		{
			get
			{
				string text = (string)this.ViewState["CommandName"];
				if (text != null)
				{
					return text;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["CommandName"] = value;
			}
		}

		// Token: 0x17000BAE RID: 2990
		// (get) Token: 0x060029FC RID: 10748 RVA: 0x00087D58 File Offset: 0x00085F58
		// (set) Token: 0x060029FD RID: 10749 RVA: 0x00087D85 File Offset: 0x00085F85
		[Bindable(true)]
		[DefaultValue("")]
		[Themeable(false)]
		[WebCategory("Behavior")]
		[WebSysDescription("WebControl_CommandArgument")]
		public string CommandArgument
		{
			get
			{
				string text = (string)this.ViewState["CommandArgument"];
				if (text != null)
				{
					return text;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["CommandArgument"] = value;
			}
		}

		// Token: 0x17000BAF RID: 2991
		// (get) Token: 0x060029FE RID: 10750 RVA: 0x00087D98 File Offset: 0x00085F98
		// (set) Token: 0x060029FF RID: 10751 RVA: 0x00087DC5 File Offset: 0x00085FC5
		[DefaultValue("")]
		[Themeable(false)]
		[WebCategory("Behavior")]
		[WebSysDescription("Button_OnClientClick")]
		public virtual string OnClientClick
		{
			get
			{
				string text = (string)this.ViewState["OnClientClick"];
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				this.ViewState["OnClientClick"] = value;
			}
		}

		// Token: 0x17000BB0 RID: 2992
		// (get) Token: 0x06002A00 RID: 10752 RVA: 0x00087DD8 File Offset: 0x00085FD8
		// (set) Token: 0x06002A01 RID: 10753 RVA: 0x00087E05 File Offset: 0x00086005
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[Themeable(false)]
		[UrlProperty("*.aspx")]
		[WebCategory("Behavior")]
		[WebSysDescription("Button_PostBackUrl")]
		public virtual string PostBackUrl
		{
			get
			{
				string text = (string)this.ViewState["PostBackUrl"];
				if (text != null)
				{
					return text;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["PostBackUrl"] = value;
			}
		}

		// Token: 0x17000BB1 RID: 2993
		// (get) Token: 0x06002A02 RID: 10754 RVA: 0x00087E18 File Offset: 0x00086018
		// (set) Token: 0x06002A03 RID: 10755 RVA: 0x00087E45 File Offset: 0x00086045
		[Bindable(true)]
		[Localizable(true)]
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[WebSysDescription("Button_Text")]
		public string Text
		{
			get
			{
				string text = (string)this.ViewState["Text"];
				if (text != null)
				{
					return text;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["Text"] = value;
			}
		}

		// Token: 0x17000BB2 RID: 2994
		// (get) Token: 0x06002A04 RID: 10756 RVA: 0x00087E58 File Offset: 0x00086058
		// (set) Token: 0x06002A05 RID: 10757 RVA: 0x00087E81 File Offset: 0x00086081
		[DefaultValue(true)]
		[Themeable(false)]
		[WebCategory("Behavior")]
		[WebSysDescription("Button_UseSubmitBehavior")]
		public virtual bool UseSubmitBehavior
		{
			get
			{
				object obj = this.ViewState["UseSubmitBehavior"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["UseSubmitBehavior"] = value;
			}
		}

		// Token: 0x17000BB3 RID: 2995
		// (get) Token: 0x06002A06 RID: 10758 RVA: 0x00087E9C File Offset: 0x0008609C
		// (set) Token: 0x06002A07 RID: 10759 RVA: 0x0007E369 File Offset: 0x0007C569
		[DefaultValue("")]
		[Themeable(false)]
		[WebCategory("Behavior")]
		[WebSysDescription("PostBackControl_ValidationGroup")]
		public virtual string ValidationGroup
		{
			get
			{
				string text = (string)this.ViewState["ValidationGroup"];
				if (text != null)
				{
					return text;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["ValidationGroup"] = value;
			}
		}

		// Token: 0x1400004E RID: 78
		// (add) Token: 0x06002A08 RID: 10760 RVA: 0x00087EC9 File Offset: 0x000860C9
		// (remove) Token: 0x06002A09 RID: 10761 RVA: 0x00087EDC File Offset: 0x000860DC
		[WebCategory("Action")]
		[WebSysDescription("Button_OnClick")]
		public event EventHandler Click
		{
			add
			{
				base.Events.AddHandler(Button.EventClick, value);
			}
			remove
			{
				base.Events.RemoveHandler(Button.EventClick, value);
			}
		}

		// Token: 0x1400004F RID: 79
		// (add) Token: 0x06002A0A RID: 10762 RVA: 0x00087EEF File Offset: 0x000860EF
		// (remove) Token: 0x06002A0B RID: 10763 RVA: 0x00087F02 File Offset: 0x00086102
		[WebCategory("Action")]
		[WebSysDescription("Button_OnCommand")]
		public event CommandEventHandler Command
		{
			add
			{
				base.Events.AddHandler(Button.EventCommand, value);
			}
			remove
			{
				base.Events.RemoveHandler(Button.EventCommand, value);
			}
		}

		// Token: 0x06002A0C RID: 10764 RVA: 0x00087F18 File Offset: 0x00086118
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			bool useSubmitBehavior = this.UseSubmitBehavior;
			if (this.Page != null)
			{
				this.Page.VerifyRenderingInServerForm(this);
			}
			if (useSubmitBehavior)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Type, "submit");
			}
			else
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Type, "button");
			}
			PostBackOptions postBackOptions = this.GetPostBackOptions();
			string uniqueID = this.UniqueID;
			if (uniqueID != null && (postBackOptions == null || postBackOptions.TargetControl == this))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Name, uniqueID);
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Value, this.Text);
			bool isEnabled = base.IsEnabled;
			string text = string.Empty;
			if (isEnabled)
			{
				text = Util.EnsureEndWithSemiColon(this.OnClientClick);
				if (base.HasAttributes)
				{
					string text2 = base.Attributes["onclick"];
					if (text2 != null)
					{
						text += Util.EnsureEndWithSemiColon(text2);
						base.Attributes.Remove("onclick");
					}
				}
				if (this.Page != null)
				{
					string postBackEventReference = this.Page.ClientScript.GetPostBackEventReference(postBackOptions, false);
					if (postBackEventReference != null)
					{
						text = Util.MergeScript(text, postBackEventReference);
					}
				}
			}
			if (this.Page != null)
			{
				this.Page.ClientScript.RegisterForEventValidation(postBackOptions);
			}
			if (text.Length > 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Onclick, text);
				if (base.EnableLegacyRendering)
				{
					writer.AddAttribute("language", "javascript", false);
				}
			}
			if (this.Enabled && !isEnabled && this.SupportsDisabledAttribute)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled");
			}
			base.AddAttributesToRender(writer);
		}

		// Token: 0x06002A0D RID: 10765 RVA: 0x00088088 File Offset: 0x00086288
		protected virtual PostBackOptions GetPostBackOptions()
		{
			PostBackOptions postBackOptions = new PostBackOptions(this, string.Empty);
			postBackOptions.ClientSubmit = false;
			if (this.Page != null)
			{
				if (this.CausesValidation && this.Page.GetValidators(this.ValidationGroup).Count > 0)
				{
					postBackOptions.PerformValidation = true;
					postBackOptions.ValidationGroup = this.ValidationGroup;
				}
				if (!string.IsNullOrEmpty(this.PostBackUrl))
				{
					postBackOptions.ActionUrl = HttpUtility.UrlPathEncode(base.ResolveClientUrl(this.PostBackUrl));
				}
				postBackOptions.ClientSubmit = !this.UseSubmitBehavior;
			}
			return postBackOptions;
		}

		// Token: 0x06002A0E RID: 10766 RVA: 0x00088118 File Offset: 0x00086318
		protected virtual void OnClick(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Button.EventClick];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06002A0F RID: 10767 RVA: 0x00088148 File Offset: 0x00086348
		protected virtual void OnCommand(CommandEventArgs e)
		{
			CommandEventHandler commandEventHandler = (CommandEventHandler)base.Events[Button.EventCommand];
			if (commandEventHandler != null)
			{
				commandEventHandler(this, e);
			}
			base.RaiseBubbleEvent(this, e);
		}

		// Token: 0x06002A10 RID: 10768 RVA: 0x00088180 File Offset: 0x00086380
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (this.Page != null && base.IsEnabled)
			{
				if ((this.CausesValidation && this.Page.GetValidators(this.ValidationGroup).Count > 0) || !string.IsNullOrEmpty(this.PostBackUrl))
				{
					this.Page.RegisterWebFormsScript();
					return;
				}
				if (!this.UseSubmitBehavior)
				{
					this.Page.RegisterPostBackScript();
				}
			}
		}

		// Token: 0x06002A11 RID: 10769 RVA: 0x00006164 File Offset: 0x00004364
		protected internal override void RenderContents(HtmlTextWriter writer)
		{
		}

		// Token: 0x06002A12 RID: 10770 RVA: 0x000881F1 File Offset: 0x000863F1
		void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
		{
			this.RaisePostBackEvent(eventArgument);
		}

		// Token: 0x06002A13 RID: 10771 RVA: 0x000881FC File Offset: 0x000863FC
		protected virtual void RaisePostBackEvent(string eventArgument)
		{
			base.ValidateEvent(this.UniqueID, eventArgument);
			if (this.CausesValidation)
			{
				this.Page.Validate(this.ValidationGroup);
			}
			this.OnClick(EventArgs.Empty);
			this.OnCommand(new CommandEventArgs(this.CommandName, this.CommandArgument));
		}

		// Token: 0x04001E94 RID: 7828
		private static readonly object EventClick = new object();

		// Token: 0x04001E95 RID: 7829
		private static readonly object EventCommand = new object();
	}
}
