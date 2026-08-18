using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Web.Util;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000452 RID: 1106
	[ControlBuilder(typeof(LinkButtonControlBuilder))]
	[DataBindingHandler("System.Web.UI.Design.TextDataBindingHandler, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultEvent("Click")]
	[DefaultProperty("Text")]
	[ToolboxData("<{0}:LinkButton runat=\"server\">LinkButton</{0}:LinkButton>")]
	[Designer("System.Web.UI.Design.WebControls.LinkButtonDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ParseChildren(false)]
	[SupportsEventValidation]
	public class LinkButton : WebControl, IButtonControl, IPostBackEventHandler
	{
		// Token: 0x06003542 RID: 13634 RVA: 0x000A9D56 File Offset: 0x000A7F56
		public LinkButton() : base(HtmlTextWriterTag.A)
		{
		}

		// Token: 0x17000F7B RID: 3963
		// (get) Token: 0x06003543 RID: 13635 RVA: 0x000ACA50 File Offset: 0x000AAC50
		// (set) Token: 0x06003544 RID: 13636 RVA: 0x00087D45 File Offset: 0x00085F45
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

		// Token: 0x17000F7C RID: 3964
		// (get) Token: 0x06003545 RID: 13637 RVA: 0x000ACA80 File Offset: 0x000AAC80
		// (set) Token: 0x06003546 RID: 13638 RVA: 0x00087D85 File Offset: 0x00085F85
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

		// Token: 0x17000F7D RID: 3965
		// (get) Token: 0x06003547 RID: 13639 RVA: 0x000ACAB0 File Offset: 0x000AACB0
		// (set) Token: 0x06003548 RID: 13640 RVA: 0x0007E239 File Offset: 0x0007C439
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

		// Token: 0x17000F7E RID: 3966
		// (get) Token: 0x06003549 RID: 13641 RVA: 0x000ACADC File Offset: 0x000AACDC
		// (set) Token: 0x0600354A RID: 13642 RVA: 0x00087DC5 File Offset: 0x00085FC5
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

		// Token: 0x17000F7F RID: 3967
		// (get) Token: 0x0600354B RID: 13643 RVA: 0x000853AC File Offset: 0x000835AC
		public override bool SupportsDisabledAttribute
		{
			get
			{
				return this.RenderingCompatibility < VersionUtil.Framework40;
			}
		}

		// Token: 0x17000F80 RID: 3968
		// (get) Token: 0x0600354C RID: 13644 RVA: 0x000097B7 File Offset: 0x000079B7
		internal override bool RequiresLegacyRendering
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000F81 RID: 3969
		// (get) Token: 0x0600354D RID: 13645 RVA: 0x000ACB0C File Offset: 0x000AAD0C
		// (set) Token: 0x0600354E RID: 13646 RVA: 0x000A9ECD File Offset: 0x000A80CD
		[Localizable(true)]
		[Bindable(true)]
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[WebSysDescription("LinkButton_Text")]
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		public virtual string Text
		{
			get
			{
				object obj = this.ViewState["Text"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				if (this.HasControls())
				{
					this.Controls.Clear();
				}
				this.ViewState["Text"] = value;
			}
		}

		// Token: 0x17000F82 RID: 3970
		// (get) Token: 0x0600354F RID: 13647 RVA: 0x000ACB3C File Offset: 0x000AAD3C
		// (set) Token: 0x06003550 RID: 13648 RVA: 0x00087E05 File Offset: 0x00086005
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

		// Token: 0x17000F83 RID: 3971
		// (get) Token: 0x06003551 RID: 13649 RVA: 0x000ACB6C File Offset: 0x000AAD6C
		// (set) Token: 0x06003552 RID: 13650 RVA: 0x0007E369 File Offset: 0x0007C569
		[WebCategory("Behavior")]
		[Themeable(false)]
		[DefaultValue("")]
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

		// Token: 0x140000AE RID: 174
		// (add) Token: 0x06003553 RID: 13651 RVA: 0x000ACB99 File Offset: 0x000AAD99
		// (remove) Token: 0x06003554 RID: 13652 RVA: 0x000ACBAC File Offset: 0x000AADAC
		[WebCategory("Action")]
		[WebSysDescription("LinkButton_OnClick")]
		public event EventHandler Click
		{
			add
			{
				base.Events.AddHandler(LinkButton.EventClick, value);
			}
			remove
			{
				base.Events.RemoveHandler(LinkButton.EventClick, value);
			}
		}

		// Token: 0x140000AF RID: 175
		// (add) Token: 0x06003555 RID: 13653 RVA: 0x000ACBBF File Offset: 0x000AADBF
		// (remove) Token: 0x06003556 RID: 13654 RVA: 0x000ACBD2 File Offset: 0x000AADD2
		[WebCategory("Action")]
		[WebSysDescription("Button_OnCommand")]
		public event CommandEventHandler Command
		{
			add
			{
				base.Events.AddHandler(LinkButton.EventCommand, value);
			}
			remove
			{
				base.Events.RemoveHandler(LinkButton.EventCommand, value);
			}
		}

		// Token: 0x06003557 RID: 13655 RVA: 0x000ACBE8 File Offset: 0x000AADE8
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			if (this.Page != null)
			{
				this.Page.VerifyRenderingInServerForm(this);
			}
			string text = Util.EnsureEndWithSemiColon(this.OnClientClick);
			if (base.HasAttributes)
			{
				string text2 = base.Attributes["onclick"];
				if (text2 != null)
				{
					text += Util.EnsureEndWithSemiColon(text2);
					base.Attributes.Remove("onclick");
				}
			}
			if (text.Length > 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Onclick, text);
			}
			bool isEnabled = base.IsEnabled;
			if (this.Enabled && !isEnabled && this.SupportsDisabledAttribute)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled");
			}
			base.AddAttributesToRender(writer);
			if (isEnabled && this.Page != null)
			{
				PostBackOptions postBackOptions = this.GetPostBackOptions();
				string value = null;
				if (postBackOptions != null)
				{
					value = this.Page.ClientScript.GetPostBackEventReference(postBackOptions, true);
				}
				if (string.IsNullOrEmpty(value))
				{
					value = "javascript:void(0)";
				}
				writer.AddAttribute(HtmlTextWriterAttribute.Href, value);
			}
		}

		// Token: 0x06003558 RID: 13656 RVA: 0x000ACCD4 File Offset: 0x000AAED4
		protected override void AddParsedSubObject(object obj)
		{
			if (this.HasControls())
			{
				base.AddParsedSubObject(obj);
				return;
			}
			if (obj is LiteralControl)
			{
				if (this._textSetByAddParsedSubObject)
				{
					this.Text += ((LiteralControl)obj).Text;
				}
				else
				{
					this.Text = ((LiteralControl)obj).Text;
				}
				this._textSetByAddParsedSubObject = true;
				return;
			}
			string text = this.Text;
			if (text.Length != 0)
			{
				this.Text = string.Empty;
				base.AddParsedSubObject(new LiteralControl(text));
			}
			base.AddParsedSubObject(obj);
		}

		// Token: 0x06003559 RID: 13657 RVA: 0x000ACD68 File Offset: 0x000AAF68
		protected virtual PostBackOptions GetPostBackOptions()
		{
			PostBackOptions postBackOptions = new PostBackOptions(this, string.Empty);
			postBackOptions.RequiresJavaScriptProtocol = true;
			if (!string.IsNullOrEmpty(this.PostBackUrl))
			{
				postBackOptions.ActionUrl = HttpUtility.UrlPathEncode(base.ResolveClientUrl(this.PostBackUrl));
				if (!base.DesignMode && this.Page != null && string.Equals(this.Page.Request.Browser.Browser, "IE", StringComparison.OrdinalIgnoreCase))
				{
					postBackOptions.ActionUrl = Util.QuoteJScriptString(postBackOptions.ActionUrl, true);
				}
			}
			if (this.CausesValidation && this.Page.GetValidators(this.ValidationGroup).Count > 0)
			{
				postBackOptions.PerformValidation = true;
				postBackOptions.ValidationGroup = this.ValidationGroup;
			}
			return postBackOptions;
		}

		// Token: 0x0600355A RID: 13658 RVA: 0x000ACE28 File Offset: 0x000AB028
		protected override void LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				base.LoadViewState(savedState);
				string text = (string)this.ViewState["Text"];
				if (text != null && this.HasControls())
				{
					this.Controls.Clear();
				}
			}
		}

		// Token: 0x0600355B RID: 13659 RVA: 0x000ACE6C File Offset: 0x000AB06C
		protected virtual void OnClick(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[LinkButton.EventClick];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600355C RID: 13660 RVA: 0x000ACE9C File Offset: 0x000AB09C
		protected virtual void OnCommand(CommandEventArgs e)
		{
			CommandEventHandler commandEventHandler = (CommandEventHandler)base.Events[LinkButton.EventCommand];
			if (commandEventHandler != null)
			{
				commandEventHandler(this, e);
			}
			base.RaiseBubbleEvent(this, e);
		}

		// Token: 0x0600355D RID: 13661 RVA: 0x000ACED2 File Offset: 0x000AB0D2
		void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
		{
			this.RaisePostBackEvent(eventArgument);
		}

		// Token: 0x0600355E RID: 13662 RVA: 0x000ACEDC File Offset: 0x000AB0DC
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

		// Token: 0x0600355F RID: 13663 RVA: 0x000ACF34 File Offset: 0x000AB134
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (this.Page != null && this.Enabled)
			{
				this.Page.RegisterPostBackScript();
				if ((this.CausesValidation && this.Page.GetValidators(this.ValidationGroup).Count > 0) || !string.IsNullOrEmpty(this.PostBackUrl))
				{
					this.Page.RegisterWebFormsScript();
				}
			}
		}

		// Token: 0x06003560 RID: 13664 RVA: 0x000ACF9C File Offset: 0x000AB19C
		protected internal override void RenderContents(HtmlTextWriter writer)
		{
			if (base.HasRenderingData())
			{
				base.RenderContents(writer);
				return;
			}
			writer.Write(this.Text);
		}

		// Token: 0x040021BD RID: 8637
		private bool _textSetByAddParsedSubObject;

		// Token: 0x040021BE RID: 8638
		private static readonly object EventClick = new object();

		// Token: 0x040021BF RID: 8639
		private static readonly object EventCommand = new object();
	}
}
