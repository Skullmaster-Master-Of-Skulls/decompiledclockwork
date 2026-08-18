using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing.Design;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000443 RID: 1091
	[DefaultEvent("Click")]
	[Designer("System.Web.UI.Design.WebControls.PreviewControlDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[SupportsEventValidation]
	public class ImageButton : Image, IPostBackDataHandler, IPostBackEventHandler, IButtonControl
	{
		// Token: 0x17000F50 RID: 3920
		// (get) Token: 0x060034AF RID: 13487 RVA: 0x000AB230 File Offset: 0x000A9430
		// (set) Token: 0x060034B0 RID: 13488 RVA: 0x00087D45 File Offset: 0x00085F45
		[DefaultValue("")]
		[WebCategory("Behavior")]
		[WebSysDescription("WebControl_CommandName")]
		[Themeable(false)]
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

		// Token: 0x17000F51 RID: 3921
		// (get) Token: 0x060034B1 RID: 13489 RVA: 0x000AB260 File Offset: 0x000A9460
		// (set) Token: 0x060034B2 RID: 13490 RVA: 0x00087D85 File Offset: 0x00085F85
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

		// Token: 0x17000F52 RID: 3922
		// (get) Token: 0x060034B3 RID: 13491 RVA: 0x000AB290 File Offset: 0x000A9490
		// (set) Token: 0x060034B4 RID: 13492 RVA: 0x000AB2B9 File Offset: 0x000A94B9
		[Themeable(false)]
		[DefaultValue(true)]
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

		// Token: 0x17000F53 RID: 3923
		// (get) Token: 0x060034B5 RID: 13493 RVA: 0x000AB2D1 File Offset: 0x000A94D1
		// (set) Token: 0x060034B6 RID: 13494 RVA: 0x000AB2D9 File Offset: 0x000A94D9
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[Bindable(true)]
		[WebCategory("Behavior")]
		[DefaultValue(true)]
		[WebSysDescription("WebControl_Enabled")]
		public override bool Enabled
		{
			get
			{
				return base.Enabled;
			}
			set
			{
				base.Enabled = value;
			}
		}

		// Token: 0x17000F54 RID: 3924
		// (get) Token: 0x060034B7 RID: 13495 RVA: 0x000AB2E2 File Offset: 0x000A94E2
		// (set) Token: 0x060034B8 RID: 13496 RVA: 0x000AB2EA File Offset: 0x000A94EA
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Themeable(false)]
		public override bool GenerateEmptyAlternateText
		{
			get
			{
				return base.GenerateEmptyAlternateText;
			}
			set
			{
				throw new NotSupportedException(SR.GetString("Property_Set_Not_Supported", new object[]
				{
					"GenerateEmptyAlternateText",
					base.GetType().ToString()
				}));
			}
		}

		// Token: 0x17000F55 RID: 3925
		// (get) Token: 0x060034B9 RID: 13497 RVA: 0x000AB318 File Offset: 0x000A9518
		// (set) Token: 0x060034BA RID: 13498 RVA: 0x000AB345 File Offset: 0x000A9545
		[DefaultValue("")]
		[WebCategory("Behavior")]
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

		// Token: 0x17000F56 RID: 3926
		// (get) Token: 0x060034BB RID: 13499 RVA: 0x000AB358 File Offset: 0x000A9558
		// (set) Token: 0x060034BC RID: 13500 RVA: 0x00087E05 File Offset: 0x00086005
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

		// Token: 0x17000F57 RID: 3927
		// (get) Token: 0x060034BD RID: 13501 RVA: 0x000097B7 File Offset: 0x000079B7
		public override bool SupportsDisabledAttribute
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000F58 RID: 3928
		// (get) Token: 0x060034BE RID: 13502 RVA: 0x000AB385 File Offset: 0x000A9585
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Input;
			}
		}

		// Token: 0x17000F59 RID: 3929
		// (get) Token: 0x060034BF RID: 13503 RVA: 0x000AB38C File Offset: 0x000A958C
		// (set) Token: 0x060034C0 RID: 13504 RVA: 0x0007E369 File Offset: 0x0007C569
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

		// Token: 0x140000A8 RID: 168
		// (add) Token: 0x060034C1 RID: 13505 RVA: 0x000AB3B9 File Offset: 0x000A95B9
		// (remove) Token: 0x060034C2 RID: 13506 RVA: 0x000AB3CC File Offset: 0x000A95CC
		[WebCategory("Action")]
		[WebSysDescription("ImageButton_OnClick")]
		public event ImageClickEventHandler Click
		{
			add
			{
				base.Events.AddHandler(ImageButton.EventClick, value);
			}
			remove
			{
				base.Events.RemoveHandler(ImageButton.EventClick, value);
			}
		}

		// Token: 0x140000A9 RID: 169
		// (add) Token: 0x060034C3 RID: 13507 RVA: 0x000AB3DF File Offset: 0x000A95DF
		// (remove) Token: 0x060034C4 RID: 13508 RVA: 0x000AB3F2 File Offset: 0x000A95F2
		event EventHandler IButtonControl.Click
		{
			add
			{
				base.Events.AddHandler(ImageButton.EventButtonClick, value);
			}
			remove
			{
				base.Events.RemoveHandler(ImageButton.EventButtonClick, value);
			}
		}

		// Token: 0x140000AA RID: 170
		// (add) Token: 0x060034C5 RID: 13509 RVA: 0x000AB405 File Offset: 0x000A9605
		// (remove) Token: 0x060034C6 RID: 13510 RVA: 0x000AB418 File Offset: 0x000A9618
		[WebCategory("Action")]
		[WebSysDescription("ImageButton_OnCommand")]
		public event CommandEventHandler Command
		{
			add
			{
				base.Events.AddHandler(ImageButton.EventCommand, value);
			}
			remove
			{
				base.Events.RemoveHandler(ImageButton.EventCommand, value);
			}
		}

		// Token: 0x060034C7 RID: 13511 RVA: 0x000AB42C File Offset: 0x000A962C
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			Page page = this.Page;
			if (page != null)
			{
				page.VerifyRenderingInServerForm(this);
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Type, "image");
			string uniqueID = this.UniqueID;
			PostBackOptions postBackOptions = this.GetPostBackOptions();
			if (uniqueID != null && (postBackOptions == null || postBackOptions.TargetControl == this))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Name, uniqueID);
			}
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
			}
			if (this.Enabled && !isEnabled && this.SupportsDisabledAttribute)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled");
			}
			base.AddAttributesToRender(writer);
			if (page != null && postBackOptions != null)
			{
				page.ClientScript.RegisterForEventValidation(postBackOptions);
				if (isEnabled)
				{
					string postBackEventReference = page.ClientScript.GetPostBackEventReference(postBackOptions, false);
					if (!string.IsNullOrEmpty(postBackEventReference))
					{
						text = Util.MergeScript(text, postBackEventReference);
						if (postBackOptions.ClientSubmit)
						{
							text = Util.EnsureEndWithSemiColon(text) + "return false;";
						}
					}
				}
			}
			if (text.Length > 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Onclick, text);
				if (base.EnableLegacyRendering)
				{
					writer.AddAttribute("language", "javascript", false);
				}
			}
		}

		// Token: 0x060034C8 RID: 13512 RVA: 0x000AB580 File Offset: 0x000A9780
		protected virtual PostBackOptions GetPostBackOptions()
		{
			PostBackOptions postBackOptions = new PostBackOptions(this, string.Empty);
			postBackOptions.ClientSubmit = false;
			if (!string.IsNullOrEmpty(this.PostBackUrl))
			{
				postBackOptions.ActionUrl = HttpUtility.UrlPathEncode(base.ResolveClientUrl(this.PostBackUrl));
			}
			if (this.CausesValidation && this.Page != null && this.Page.GetValidators(this.ValidationGroup).Count > 0)
			{
				postBackOptions.PerformValidation = true;
				postBackOptions.ValidationGroup = this.ValidationGroup;
			}
			return postBackOptions;
		}

		// Token: 0x060034C9 RID: 13513 RVA: 0x000AB601 File Offset: 0x000A9801
		bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			return this.LoadPostData(postDataKey, postCollection);
		}

		// Token: 0x060034CA RID: 13514 RVA: 0x000AB60C File Offset: 0x000A980C
		protected virtual bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			string uniqueID = this.UniqueID;
			string text = postCollection[uniqueID + ".x"];
			string text2 = postCollection[uniqueID + ".y"];
			if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2))
			{
				this.xRaw = ImageButton.ReadPositionFromPost(text);
				this.yRaw = ImageButton.ReadPositionFromPost(text2);
				this.x = (int)this.xRaw;
				this.y = (int)this.yRaw;
				if (this.Page != null)
				{
					this.Page.RegisterRequiresRaiseEvent(this);
				}
			}
			return false;
		}

		// Token: 0x060034CB RID: 13515 RVA: 0x000AB69C File Offset: 0x000A989C
		internal static double ReadPositionFromPost(string requestValue)
		{
			double result;
			if (HttpUtility.TryParseCoordinates(requestValue, out result))
			{
				return result;
			}
			return 0.0;
		}

		// Token: 0x060034CC RID: 13516 RVA: 0x000AB6C0 File Offset: 0x000A98C0
		protected virtual void OnClick(ImageClickEventArgs e)
		{
			ImageClickEventHandler imageClickEventHandler = (ImageClickEventHandler)base.Events[ImageButton.EventClick];
			if (imageClickEventHandler != null)
			{
				imageClickEventHandler(this, e);
			}
			EventHandler eventHandler = (EventHandler)base.Events[ImageButton.EventButtonClick];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060034CD RID: 13517 RVA: 0x000AB710 File Offset: 0x000A9910
		protected virtual void OnCommand(CommandEventArgs e)
		{
			CommandEventHandler commandEventHandler = (CommandEventHandler)base.Events[ImageButton.EventCommand];
			if (commandEventHandler != null)
			{
				commandEventHandler(this, e);
			}
			base.RaiseBubbleEvent(this, e);
		}

		// Token: 0x060034CE RID: 13518 RVA: 0x000AB748 File Offset: 0x000A9948
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (this.Page != null)
			{
				this.Page.RegisterRequiresPostBack(this);
				if (base.IsEnabled && ((this.CausesValidation && this.Page.GetValidators(this.ValidationGroup).Count > 0) || !string.IsNullOrEmpty(this.PostBackUrl)))
				{
					this.Page.RegisterWebFormsScript();
				}
			}
		}

		// Token: 0x060034CF RID: 13519 RVA: 0x000AB7B1 File Offset: 0x000A99B1
		void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
		{
			this.RaisePostBackEvent(eventArgument);
		}

		// Token: 0x060034D0 RID: 13520 RVA: 0x000AB7BC File Offset: 0x000A99BC
		protected virtual void RaisePostBackEvent(string eventArgument)
		{
			base.ValidateEvent(this.UniqueID, eventArgument);
			if (this.CausesValidation)
			{
				this.Page.Validate(this.ValidationGroup);
			}
			this.OnClick(new ImageClickEventArgs(this.x, this.y, this.xRaw, this.yRaw));
			this.OnCommand(new CommandEventArgs(this.CommandName, this.CommandArgument));
		}

		// Token: 0x060034D1 RID: 13521 RVA: 0x000AB829 File Offset: 0x000A9A29
		void IPostBackDataHandler.RaisePostDataChangedEvent()
		{
			this.RaisePostDataChangedEvent();
		}

		// Token: 0x060034D2 RID: 13522 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void RaisePostDataChangedEvent()
		{
		}

		// Token: 0x17000F5A RID: 3930
		// (get) Token: 0x060034D3 RID: 13523 RVA: 0x000AB831 File Offset: 0x000A9A31
		// (set) Token: 0x060034D4 RID: 13524 RVA: 0x000AB839 File Offset: 0x000A9A39
		string IButtonControl.Text
		{
			get
			{
				return this.Text;
			}
			set
			{
				this.Text = value;
			}
		}

		// Token: 0x17000F5B RID: 3931
		// (get) Token: 0x060034D5 RID: 13525 RVA: 0x000AB842 File Offset: 0x000A9A42
		// (set) Token: 0x060034D6 RID: 13526 RVA: 0x000AB84A File Offset: 0x000A9A4A
		protected virtual string Text
		{
			get
			{
				return this.AlternateText;
			}
			set
			{
				this.AlternateText = value;
			}
		}

		// Token: 0x040021AB RID: 8619
		private static readonly object EventClick = new object();

		// Token: 0x040021AC RID: 8620
		private static readonly object EventButtonClick = new object();

		// Token: 0x040021AD RID: 8621
		private static readonly object EventCommand = new object();

		// Token: 0x040021AE RID: 8622
		private int x;

		// Token: 0x040021AF RID: 8623
		private int y;

		// Token: 0x040021B0 RID: 8624
		private double xRaw;

		// Token: 0x040021B1 RID: 8625
		private double yRaw;
	}
}
