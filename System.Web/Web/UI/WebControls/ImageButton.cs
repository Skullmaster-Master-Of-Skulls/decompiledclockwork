using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000532 RID: 1330
	[DefaultEvent("Click")]
	[Designer("System.Web.UI.Design.WebControls.PreviewControlDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[SupportsEventValidation]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class ImageButton : Image, IPostBackDataHandler, IPostBackEventHandler, IButtonControl
	{
		// Token: 0x17000FC0 RID: 4032
		// (get) Token: 0x06004165 RID: 16741 RVA: 0x0010F394 File Offset: 0x0010E394
		// (set) Token: 0x06004166 RID: 16742 RVA: 0x0010F3C1 File Offset: 0x0010E3C1
		[DefaultValue("")]
		[Themeable(false)]
		[WebSysDescription("WebControl_CommandName")]
		[WebCategory("Behavior")]
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

		// Token: 0x17000FC1 RID: 4033
		// (get) Token: 0x06004167 RID: 16743 RVA: 0x0010F3D4 File Offset: 0x0010E3D4
		// (set) Token: 0x06004168 RID: 16744 RVA: 0x0010F401 File Offset: 0x0010E401
		[Bindable(true)]
		[DefaultValue("")]
		[Themeable(false)]
		[WebSysDescription("WebControl_CommandArgument")]
		[WebCategory("Behavior")]
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

		// Token: 0x17000FC2 RID: 4034
		// (get) Token: 0x06004169 RID: 16745 RVA: 0x0010F414 File Offset: 0x0010E414
		// (set) Token: 0x0600416A RID: 16746 RVA: 0x0010F43D File Offset: 0x0010E43D
		[Themeable(false)]
		[WebSysDescription("Button_CausesValidation")]
		[DefaultValue(true)]
		[WebCategory("Behavior")]
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

		// Token: 0x17000FC3 RID: 4035
		// (get) Token: 0x0600416B RID: 16747 RVA: 0x0010F455 File Offset: 0x0010E455
		// (set) Token: 0x0600416C RID: 16748 RVA: 0x0010F45D File Offset: 0x0010E45D
		[DefaultValue(true)]
		[Browsable(true)]
		[WebCategory("Behavior")]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[Bindable(true)]
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

		// Token: 0x17000FC4 RID: 4036
		// (get) Token: 0x0600416D RID: 16749 RVA: 0x0010F466 File Offset: 0x0010E466
		// (set) Token: 0x0600416E RID: 16750 RVA: 0x0010F470 File Offset: 0x0010E470
		[Themeable(false)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

		// Token: 0x17000FC5 RID: 4037
		// (get) Token: 0x0600416F RID: 16751 RVA: 0x0010F4AC File Offset: 0x0010E4AC
		// (set) Token: 0x06004170 RID: 16752 RVA: 0x0010F4D9 File Offset: 0x0010E4D9
		[WebCategory("Behavior")]
		[WebSysDescription("Button_OnClientClick")]
		[DefaultValue("")]
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

		// Token: 0x17000FC6 RID: 4038
		// (get) Token: 0x06004171 RID: 16753 RVA: 0x0010F4EC File Offset: 0x0010E4EC
		// (set) Token: 0x06004172 RID: 16754 RVA: 0x0010F519 File Offset: 0x0010E519
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[DefaultValue("")]
		[WebSysDescription("Button_PostBackUrl")]
		[Themeable(false)]
		[UrlProperty("*.aspx")]
		[WebCategory("Behavior")]
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

		// Token: 0x17000FC7 RID: 4039
		// (get) Token: 0x06004173 RID: 16755 RVA: 0x0010F52C File Offset: 0x0010E52C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Input;
			}
		}

		// Token: 0x17000FC8 RID: 4040
		// (get) Token: 0x06004174 RID: 16756 RVA: 0x0010F530 File Offset: 0x0010E530
		// (set) Token: 0x06004175 RID: 16757 RVA: 0x0010F55D File Offset: 0x0010E55D
		[WebSysDescription("PostBackControl_ValidationGroup")]
		[WebCategory("Behavior")]
		[Themeable(false)]
		[DefaultValue("")]
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

		// Token: 0x14000090 RID: 144
		// (add) Token: 0x06004176 RID: 16758 RVA: 0x0010F570 File Offset: 0x0010E570
		// (remove) Token: 0x06004177 RID: 16759 RVA: 0x0010F583 File Offset: 0x0010E583
		[WebSysDescription("ImageButton_OnClick")]
		[WebCategory("Action")]
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

		// Token: 0x14000091 RID: 145
		// (add) Token: 0x06004178 RID: 16760 RVA: 0x0010F596 File Offset: 0x0010E596
		// (remove) Token: 0x06004179 RID: 16761 RVA: 0x0010F5A9 File Offset: 0x0010E5A9
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

		// Token: 0x14000092 RID: 146
		// (add) Token: 0x0600417A RID: 16762 RVA: 0x0010F5BC File Offset: 0x0010E5BC
		// (remove) Token: 0x0600417B RID: 16763 RVA: 0x0010F5CF File Offset: 0x0010E5CF
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

		// Token: 0x0600417C RID: 16764 RVA: 0x0010F5E4 File Offset: 0x0010E5E4
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
			if (this.Enabled && !isEnabled)
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

		// Token: 0x0600417D RID: 16765 RVA: 0x0010F718 File Offset: 0x0010E718
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

		// Token: 0x0600417E RID: 16766 RVA: 0x0010F799 File Offset: 0x0010E799
		bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			return this.LoadPostData(postDataKey, postCollection);
		}

		// Token: 0x0600417F RID: 16767 RVA: 0x0010F7A4 File Offset: 0x0010E7A4
		protected virtual bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			string uniqueID = this.UniqueID;
			string text = postCollection[uniqueID + ".x"];
			string text2 = postCollection[uniqueID + ".y"];
			if (text != null && text2 != null && text.Length > 0 && text2.Length > 0)
			{
				this.x = (int)ImageButton.ReadPositionFromPost(text);
				this.y = (int)ImageButton.ReadPositionFromPost(text2);
				if (this.Page != null)
				{
					this.Page.RegisterRequiresRaiseEvent(this);
				}
			}
			return false;
		}

		// Token: 0x06004180 RID: 16768 RVA: 0x0010F824 File Offset: 0x0010E824
		private static double ReadPositionFromPost(string requestValue)
		{
			NumberStyles style = NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint;
			return double.Parse(requestValue, style, CultureInfo.InvariantCulture);
		}

		// Token: 0x06004181 RID: 16769 RVA: 0x0010F840 File Offset: 0x0010E840
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

		// Token: 0x06004182 RID: 16770 RVA: 0x0010F890 File Offset: 0x0010E890
		protected virtual void OnCommand(CommandEventArgs e)
		{
			CommandEventHandler commandEventHandler = (CommandEventHandler)base.Events[ImageButton.EventCommand];
			if (commandEventHandler != null)
			{
				commandEventHandler(this, e);
			}
			base.RaiseBubbleEvent(this, e);
		}

		// Token: 0x06004183 RID: 16771 RVA: 0x0010F8C8 File Offset: 0x0010E8C8
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

		// Token: 0x06004184 RID: 16772 RVA: 0x0010F931 File Offset: 0x0010E931
		void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
		{
			this.RaisePostBackEvent(eventArgument);
		}

		// Token: 0x06004185 RID: 16773 RVA: 0x0010F93C File Offset: 0x0010E93C
		protected virtual void RaisePostBackEvent(string eventArgument)
		{
			base.ValidateEvent(this.UniqueID, eventArgument);
			if (this.CausesValidation)
			{
				this.Page.Validate(this.ValidationGroup);
			}
			this.OnClick(new ImageClickEventArgs(this.x, this.y));
			this.OnCommand(new CommandEventArgs(this.CommandName, this.CommandArgument));
		}

		// Token: 0x06004186 RID: 16774 RVA: 0x0010F99D File Offset: 0x0010E99D
		void IPostBackDataHandler.RaisePostDataChangedEvent()
		{
			this.RaisePostDataChangedEvent();
		}

		// Token: 0x06004187 RID: 16775 RVA: 0x0010F9A5 File Offset: 0x0010E9A5
		protected virtual void RaisePostDataChangedEvent()
		{
		}

		// Token: 0x17000FC9 RID: 4041
		// (get) Token: 0x06004188 RID: 16776 RVA: 0x0010F9A7 File Offset: 0x0010E9A7
		// (set) Token: 0x06004189 RID: 16777 RVA: 0x0010F9AF File Offset: 0x0010E9AF
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

		// Token: 0x17000FCA RID: 4042
		// (get) Token: 0x0600418A RID: 16778 RVA: 0x0010F9B8 File Offset: 0x0010E9B8
		// (set) Token: 0x0600418B RID: 16779 RVA: 0x0010F9C0 File Offset: 0x0010E9C0
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

		// Token: 0x040028B0 RID: 10416
		private static readonly object EventClick = new object();

		// Token: 0x040028B1 RID: 10417
		private static readonly object EventButtonClick = new object();

		// Token: 0x040028B2 RID: 10418
		private static readonly object EventCommand = new object();

		// Token: 0x040028B3 RID: 10419
		private int x;

		// Token: 0x040028B4 RID: 10420
		private int y;
	}
}
