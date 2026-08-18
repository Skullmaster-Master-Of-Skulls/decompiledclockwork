using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x020004D6 RID: 1238
	[SupportsEventValidation]
	[DefaultEvent("Click")]
	[DataBindingHandler("System.Web.UI.Design.TextDataBindingHandler, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultProperty("Text")]
	[Designer("System.Web.UI.Design.WebControls.ButtonDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ToolboxData("<{0}:Button runat=\"server\" Text=\"Button\"></{0}:Button>")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class Button : WebControl, IButtonControl, IPostBackEventHandler
	{
		// Token: 0x06003B72 RID: 15218 RVA: 0x000FA1C0 File Offset: 0x000F91C0
		public Button() : base(HtmlTextWriterTag.Input)
		{
		}

		// Token: 0x17000DA3 RID: 3491
		// (get) Token: 0x06003B73 RID: 15219 RVA: 0x000FA1CC File Offset: 0x000F91CC
		// (set) Token: 0x06003B74 RID: 15220 RVA: 0x000FA1F5 File Offset: 0x000F91F5
		[WebSysDescription("Button_CausesValidation")]
		[WebCategory("Behavior")]
		[DefaultValue(true)]
		[Themeable(false)]
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

		// Token: 0x17000DA4 RID: 3492
		// (get) Token: 0x06003B75 RID: 15221 RVA: 0x000FA210 File Offset: 0x000F9210
		// (set) Token: 0x06003B76 RID: 15222 RVA: 0x000FA23D File Offset: 0x000F923D
		[Themeable(false)]
		[WebSysDescription("WebControl_CommandName")]
		[WebCategory("Behavior")]
		[DefaultValue("")]
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

		// Token: 0x17000DA5 RID: 3493
		// (get) Token: 0x06003B77 RID: 15223 RVA: 0x000FA250 File Offset: 0x000F9250
		// (set) Token: 0x06003B78 RID: 15224 RVA: 0x000FA27D File Offset: 0x000F927D
		[Bindable(true)]
		[DefaultValue("")]
		[WebCategory("Behavior")]
		[WebSysDescription("WebControl_CommandArgument")]
		[Themeable(false)]
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

		// Token: 0x17000DA6 RID: 3494
		// (get) Token: 0x06003B79 RID: 15225 RVA: 0x000FA290 File Offset: 0x000F9290
		// (set) Token: 0x06003B7A RID: 15226 RVA: 0x000FA2BD File Offset: 0x000F92BD
		[WebCategory("Behavior")]
		[DefaultValue("")]
		[WebSysDescription("Button_OnClientClick")]
		[Themeable(false)]
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

		// Token: 0x17000DA7 RID: 3495
		// (get) Token: 0x06003B7B RID: 15227 RVA: 0x000FA2D0 File Offset: 0x000F92D0
		// (set) Token: 0x06003B7C RID: 15228 RVA: 0x000FA2FD File Offset: 0x000F92FD
		[WebSysDescription("Button_PostBackUrl")]
		[UrlProperty("*.aspx")]
		[WebCategory("Behavior")]
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[Themeable(false)]
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

		// Token: 0x17000DA8 RID: 3496
		// (get) Token: 0x06003B7D RID: 15229 RVA: 0x000FA310 File Offset: 0x000F9310
		// (set) Token: 0x06003B7E RID: 15230 RVA: 0x000FA33D File Offset: 0x000F933D
		[Bindable(true)]
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[WebSysDescription("Button_Text")]
		[Localizable(true)]
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

		// Token: 0x17000DA9 RID: 3497
		// (get) Token: 0x06003B7F RID: 15231 RVA: 0x000FA350 File Offset: 0x000F9350
		// (set) Token: 0x06003B80 RID: 15232 RVA: 0x000FA379 File Offset: 0x000F9379
		[WebSysDescription("Button_UseSubmitBehavior")]
		[Themeable(false)]
		[WebCategory("Behavior")]
		[DefaultValue(true)]
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

		// Token: 0x17000DAA RID: 3498
		// (get) Token: 0x06003B81 RID: 15233 RVA: 0x000FA394 File Offset: 0x000F9394
		// (set) Token: 0x06003B82 RID: 15234 RVA: 0x000FA3C1 File Offset: 0x000F93C1
		[Themeable(false)]
		[DefaultValue("")]
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

		// Token: 0x1400006C RID: 108
		// (add) Token: 0x06003B83 RID: 15235 RVA: 0x000FA3D4 File Offset: 0x000F93D4
		// (remove) Token: 0x06003B84 RID: 15236 RVA: 0x000FA3E7 File Offset: 0x000F93E7
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

		// Token: 0x1400006D RID: 109
		// (add) Token: 0x06003B85 RID: 15237 RVA: 0x000FA3FA File Offset: 0x000F93FA
		// (remove) Token: 0x06003B86 RID: 15238 RVA: 0x000FA40D File Offset: 0x000F940D
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

		// Token: 0x06003B87 RID: 15239 RVA: 0x000FA420 File Offset: 0x000F9420
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
			if (this.Enabled && !isEnabled)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled");
			}
			base.AddAttributesToRender(writer);
		}

		// Token: 0x06003B88 RID: 15240 RVA: 0x000FA588 File Offset: 0x000F9588
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

		// Token: 0x06003B89 RID: 15241 RVA: 0x000FA618 File Offset: 0x000F9618
		protected virtual void OnClick(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Button.EventClick];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06003B8A RID: 15242 RVA: 0x000FA648 File Offset: 0x000F9648
		protected virtual void OnCommand(CommandEventArgs e)
		{
			CommandEventHandler commandEventHandler = (CommandEventHandler)base.Events[Button.EventCommand];
			if (commandEventHandler != null)
			{
				commandEventHandler(this, e);
			}
			base.RaiseBubbleEvent(this, e);
		}

		// Token: 0x06003B8B RID: 15243 RVA: 0x000FA680 File Offset: 0x000F9680
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

		// Token: 0x06003B8C RID: 15244 RVA: 0x000FA6F1 File Offset: 0x000F96F1
		protected internal override void RenderContents(HtmlTextWriter writer)
		{
		}

		// Token: 0x06003B8D RID: 15245 RVA: 0x000FA6F3 File Offset: 0x000F96F3
		void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
		{
			this.RaisePostBackEvent(eventArgument);
		}

		// Token: 0x06003B8E RID: 15246 RVA: 0x000FA6FC File Offset: 0x000F96FC
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

		// Token: 0x040026C9 RID: 9929
		private static readonly object EventClick = new object();

		// Token: 0x040026CA RID: 9930
		private static readonly object EventCommand = new object();
	}
}
