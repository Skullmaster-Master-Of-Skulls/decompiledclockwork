using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;
using System.Web.Util;

namespace System.Web.UI.WebControls
{
	// Token: 0x020004F7 RID: 1271
	[ControlBuilder(typeof(TextBoxControlBuilder))]
	[ControlValueProperty("Text")]
	[DataBindingHandler("System.Web.UI.Design.TextDataBindingHandler, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultProperty("Text")]
	[ValidationProperty("Text")]
	[DefaultEvent("TextChanged")]
	[Designer("System.Web.UI.Design.WebControls.PreviewControlDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ParseChildren(true, "Text")]
	[SupportsEventValidation]
	public class TextBox : WebControl, IPostBackDataHandler, IEditableTextControl, ITextControl
	{
		// Token: 0x06003F38 RID: 16184 RVA: 0x00087CE0 File Offset: 0x00085EE0
		public TextBox() : base(HtmlTextWriterTag.Input)
		{
		}

		// Token: 0x17001273 RID: 4723
		// (get) Token: 0x06003F39 RID: 16185 RVA: 0x000CAFF8 File Offset: 0x000C91F8
		// (set) Token: 0x06003F3A RID: 16186 RVA: 0x000CB021 File Offset: 0x000C9221
		[DefaultValue(AutoCompleteType.None)]
		[Themeable(false)]
		[WebCategory("Behavior")]
		[WebSysDescription("TextBox_AutoCompleteType")]
		public virtual AutoCompleteType AutoCompleteType
		{
			get
			{
				object obj = this.ViewState["AutoCompleteType"];
				if (obj != null)
				{
					return (AutoCompleteType)obj;
				}
				return AutoCompleteType.None;
			}
			set
			{
				if (value < AutoCompleteType.None || value > AutoCompleteType.Enabled)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["AutoCompleteType"] = value;
			}
		}

		// Token: 0x17001274 RID: 4724
		// (get) Token: 0x06003F3B RID: 16187 RVA: 0x000CB050 File Offset: 0x000C9250
		// (set) Token: 0x06003F3C RID: 16188 RVA: 0x0008D869 File Offset: 0x0008BA69
		[DefaultValue(false)]
		[Themeable(false)]
		[WebCategory("Behavior")]
		[WebSysDescription("TextBox_AutoPostBack")]
		public virtual bool AutoPostBack
		{
			get
			{
				object obj = this.ViewState["AutoPostBack"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["AutoPostBack"] = value;
			}
		}

		// Token: 0x17001275 RID: 4725
		// (get) Token: 0x06003F3D RID: 16189 RVA: 0x000CB07C File Offset: 0x000C927C
		// (set) Token: 0x06003F3E RID: 16190 RVA: 0x0007E239 File Offset: 0x0007C439
		[DefaultValue(false)]
		[Themeable(false)]
		[WebCategory("Behavior")]
		[WebSysDescription("AutoPostBackControl_CausesValidation")]
		public virtual bool CausesValidation
		{
			get
			{
				object obj = this.ViewState["CausesValidation"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["CausesValidation"] = value;
			}
		}

		// Token: 0x17001276 RID: 4726
		// (get) Token: 0x06003F3F RID: 16191 RVA: 0x000CB0A8 File Offset: 0x000C92A8
		// (set) Token: 0x06003F40 RID: 16192 RVA: 0x000CB0D1 File Offset: 0x000C92D1
		[WebCategory("Appearance")]
		[DefaultValue(0)]
		[WebSysDescription("TextBox_Columns")]
		public virtual int Columns
		{
			get
			{
				object obj = this.ViewState["Columns"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 0;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("Columns", SR.GetString("TextBox_InvalidColumns"));
				}
				this.ViewState["Columns"] = value;
			}
		}

		// Token: 0x17001277 RID: 4727
		// (get) Token: 0x06003F41 RID: 16193 RVA: 0x000CB104 File Offset: 0x000C9304
		// (set) Token: 0x06003F42 RID: 16194 RVA: 0x000CB12D File Offset: 0x000C932D
		[DefaultValue(0)]
		[Themeable(false)]
		[WebCategory("Behavior")]
		[WebSysDescription("TextBox_MaxLength")]
		public virtual int MaxLength
		{
			get
			{
				object obj = this.ViewState["MaxLength"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 0;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["MaxLength"] = value;
			}
		}

		// Token: 0x17001278 RID: 4728
		// (get) Token: 0x06003F43 RID: 16195 RVA: 0x000CB154 File Offset: 0x000C9354
		// (set) Token: 0x06003F44 RID: 16196 RVA: 0x000CB17D File Offset: 0x000C937D
		[DefaultValue(TextBoxMode.SingleLine)]
		[Themeable(false)]
		[WebCategory("Behavior")]
		[WebSysDescription("TextBox_TextMode")]
		public virtual TextBoxMode TextMode
		{
			get
			{
				object obj = this.ViewState["Mode"];
				if (obj != null)
				{
					return (TextBoxMode)obj;
				}
				return TextBoxMode.SingleLine;
			}
			set
			{
				if (value < TextBoxMode.SingleLine || value > TextBoxMode.Week)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["Mode"] = value;
			}
		}

		// Token: 0x17001279 RID: 4729
		// (get) Token: 0x06003F45 RID: 16197 RVA: 0x000CB1AC File Offset: 0x000C93AC
		// (set) Token: 0x06003F46 RID: 16198 RVA: 0x000CB1D5 File Offset: 0x000C93D5
		[Bindable(true)]
		[DefaultValue(false)]
		[Themeable(false)]
		[WebCategory("Behavior")]
		[WebSysDescription("TextBox_ReadOnly")]
		public virtual bool ReadOnly
		{
			get
			{
				object obj = this.ViewState["ReadOnly"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["ReadOnly"] = value;
			}
		}

		// Token: 0x1700127A RID: 4730
		// (get) Token: 0x06003F47 RID: 16199 RVA: 0x000CB1F0 File Offset: 0x000C93F0
		// (set) Token: 0x06003F48 RID: 16200 RVA: 0x000CB219 File Offset: 0x000C9419
		[DefaultValue(0)]
		[Themeable(false)]
		[WebCategory("Behavior")]
		[WebSysDescription("TextBox_Rows")]
		public virtual int Rows
		{
			get
			{
				object obj = this.ViewState["Rows"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 0;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("Rows", SR.GetString("TextBox_InvalidRows"));
				}
				this.ViewState["Rows"] = value;
			}
		}

		// Token: 0x1700127B RID: 4731
		// (get) Token: 0x06003F49 RID: 16201 RVA: 0x000CB24C File Offset: 0x000C944C
		private bool SaveTextViewState
		{
			get
			{
				return this.TextMode != TextBoxMode.Password && (base.Events[TextBox.EventTextChanged] != null || !base.IsEnabled || !this.Visible || this.ReadOnly || base.GetType() != typeof(TextBox));
			}
		}

		// Token: 0x1700127C RID: 4732
		// (get) Token: 0x06003F4A RID: 16202 RVA: 0x000CB2A8 File Offset: 0x000C94A8
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				if (this.TextMode == TextBoxMode.MultiLine)
				{
					return HtmlTextWriterTag.Textarea;
				}
				return HtmlTextWriterTag.Input;
			}
		}

		// Token: 0x1700127D RID: 4733
		// (get) Token: 0x06003F4B RID: 16203 RVA: 0x000CB2B8 File Offset: 0x000C94B8
		// (set) Token: 0x06003F4C RID: 16204 RVA: 0x00087E45 File Offset: 0x00086045
		[Localizable(true)]
		[Bindable(true, BindingDirection.TwoWay)]
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[WebSysDescription("TextBox_Text")]
		[PersistenceMode(PersistenceMode.EncodedInnerDefaultProperty)]
		[Editor("System.ComponentModel.Design.MultilineStringEditor,System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public virtual string Text
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

		// Token: 0x1700127E RID: 4734
		// (get) Token: 0x06003F4D RID: 16205 RVA: 0x000CB2E8 File Offset: 0x000C94E8
		// (set) Token: 0x06003F4E RID: 16206 RVA: 0x0007E369 File Offset: 0x0007C569
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

		// Token: 0x1700127F RID: 4735
		// (get) Token: 0x06003F4F RID: 16207 RVA: 0x000CB318 File Offset: 0x000C9518
		// (set) Token: 0x06003F50 RID: 16208 RVA: 0x000CB341 File Offset: 0x000C9541
		[WebCategory("Layout")]
		[DefaultValue(true)]
		[WebSysDescription("TextBox_Wrap")]
		public virtual bool Wrap
		{
			get
			{
				object obj = this.ViewState["Wrap"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["Wrap"] = value;
			}
		}

		// Token: 0x17001280 RID: 4736
		// (get) Token: 0x06003F51 RID: 16209 RVA: 0x000CB359 File Offset: 0x000C9559
		internal virtual bool SupportsVCard
		{
			get
			{
				return this.Context != null && this.Context.Request.Browser["supportsVCard"] == "true";
			}
		}

		// Token: 0x140000FD RID: 253
		// (add) Token: 0x06003F52 RID: 16210 RVA: 0x000CB389 File Offset: 0x000C9589
		// (remove) Token: 0x06003F53 RID: 16211 RVA: 0x000CB39C File Offset: 0x000C959C
		[WebCategory("Action")]
		[WebSysDescription("TextBox_OnTextChanged")]
		public event EventHandler TextChanged
		{
			add
			{
				base.Events.AddHandler(TextBox.EventTextChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(TextBox.EventTextChanged, value);
			}
		}

		// Token: 0x06003F54 RID: 16212 RVA: 0x000CB3B0 File Offset: 0x000C95B0
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			Page page = this.Page;
			if (page != null)
			{
				page.VerifyRenderingInServerForm(this);
			}
			string uniqueID = this.UniqueID;
			if (uniqueID != null)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Name, uniqueID);
			}
			TextBoxMode textMode = this.TextMode;
			if (textMode == TextBoxMode.MultiLine)
			{
				int num = this.Rows;
				int num2 = this.Columns;
				bool flag = false;
				if (!base.EnableLegacyRendering)
				{
					if (num == 0)
					{
						num = 2;
					}
					if (num2 == 0)
					{
						num2 = 20;
					}
				}
				if (num > 0 || flag)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Rows, num.ToString(NumberFormatInfo.InvariantInfo));
				}
				if (num2 > 0 || flag)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Cols, num2.ToString(NumberFormatInfo.InvariantInfo));
				}
				if (!this.Wrap)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Wrap, "off");
				}
				if (BinaryCompatibility.Current.TargetsAtLeastFramework472 && this.MaxLength > 0)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Maxlength, this.MaxLength.ToString(NumberFormatInfo.InvariantInfo));
				}
			}
			else
			{
				if (textMode != TextBoxMode.SingleLine || string.IsNullOrEmpty(base.Attributes["type"]))
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Type, TextBox.GetTypeAttributeValue(textMode));
				}
				AutoCompleteType autoCompleteType = this.AutoCompleteType;
				if (textMode == TextBoxMode.SingleLine && autoCompleteType != AutoCompleteType.None && autoCompleteType != AutoCompleteType.Enabled && autoCompleteType != AutoCompleteType.Disabled && this.SupportsVCard)
				{
					string vcardAttributeValue = TextBox.GetVCardAttributeValue(autoCompleteType);
					writer.AddAttribute(HtmlTextWriterAttribute.VCardName, vcardAttributeValue);
				}
				if (autoCompleteType == AutoCompleteType.Disabled && (this.RenderingCompatibility >= VersionUtil.Framework45 || textMode >= TextBoxMode.Color || (this.SupportsVCard && textMode == TextBoxMode.SingleLine)))
				{
					writer.AddAttribute(HtmlTextWriterAttribute.AutoComplete, "off");
				}
				if (autoCompleteType == AutoCompleteType.Enabled)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.AutoComplete, "on");
				}
				if (textMode != TextBoxMode.Password)
				{
					string text = this.Text;
					if (text.Length > 0)
					{
						writer.AddAttribute(HtmlTextWriterAttribute.Value, text);
					}
				}
				int num3 = this.MaxLength;
				if (num3 > 0)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Maxlength, num3.ToString(NumberFormatInfo.InvariantInfo));
				}
				num3 = this.Columns;
				if (num3 > 0)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Size, num3.ToString(NumberFormatInfo.InvariantInfo));
				}
			}
			if (this.ReadOnly)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.ReadOnly, "readonly");
			}
			if (this.AutoPostBack && page != null && page.ClientSupportsJavaScript)
			{
				string text2 = null;
				if (base.HasAttributes)
				{
					text2 = base.Attributes["onchange"];
					if (text2 != null)
					{
						text2 = Util.EnsureEndWithSemiColon(text2);
						base.Attributes.Remove("onchange");
					}
				}
				PostBackOptions postBackOptions = new PostBackOptions(this, string.Empty);
				if (this.CausesValidation)
				{
					postBackOptions.PerformValidation = true;
					postBackOptions.ValidationGroup = this.ValidationGroup;
				}
				if (page.Form != null)
				{
					postBackOptions.AutoPostBack = true;
				}
				text2 = Util.MergeScript(text2, page.ClientScript.GetPostBackEventReference(postBackOptions, true));
				writer.AddAttribute(HtmlTextWriterAttribute.Onchange, text2);
				if (textMode != TextBoxMode.MultiLine)
				{
					string text3 = "if (WebForm_TextBoxKeyHandler(event) == false) return false;";
					if (base.HasAttributes)
					{
						string text4 = base.Attributes["onkeypress"];
						if (text4 != null)
						{
							text3 += text4;
							base.Attributes.Remove("onkeypress");
						}
					}
					writer.AddAttribute("onkeypress", text3);
				}
				if (base.EnableLegacyRendering)
				{
					writer.AddAttribute("language", "javascript", false);
				}
			}
			else if (page != null)
			{
				page.ClientScript.RegisterForEventValidation(this.UniqueID, string.Empty);
			}
			if (this.Enabled && !base.IsEnabled && this.SupportsDisabledAttribute)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled");
			}
			base.AddAttributesToRender(writer);
		}

		// Token: 0x06003F55 RID: 16213 RVA: 0x000CB720 File Offset: 0x000C9920
		protected override void AddParsedSubObject(object obj)
		{
			if (obj is LiteralControl)
			{
				this.Text = ((LiteralControl)obj).Text;
				return;
			}
			throw new HttpException(SR.GetString("Cannot_Have_Children_Of_Type", new object[]
			{
				"TextBox",
				obj.GetType().Name.ToString(CultureInfo.InvariantCulture)
			}));
		}

		// Token: 0x06003F56 RID: 16214 RVA: 0x000CB77C File Offset: 0x000C997C
		internal static string GetTypeAttributeValue(TextBoxMode mode)
		{
			switch (mode)
			{
			case TextBoxMode.SingleLine:
				return "text";
			case TextBoxMode.Password:
				return "password";
			case TextBoxMode.Color:
				return "color";
			case TextBoxMode.Date:
				return "date";
			case TextBoxMode.DateTime:
				return "datetime";
			case TextBoxMode.DateTimeLocal:
				return "datetime-local";
			case TextBoxMode.Email:
				return "email";
			case TextBoxMode.Month:
				return "month";
			case TextBoxMode.Number:
				return "number";
			case TextBoxMode.Range:
				return "range";
			case TextBoxMode.Search:
				return "search";
			case TextBoxMode.Phone:
				return "tel";
			case TextBoxMode.Time:
				return "time";
			case TextBoxMode.Url:
				return "url";
			case TextBoxMode.Week:
				return "week";
			}
			throw new InvalidOperationException();
		}

		// Token: 0x06003F57 RID: 16215 RVA: 0x000CB830 File Offset: 0x000C9A30
		internal static string GetVCardAttributeValue(AutoCompleteType type)
		{
			if (type <= AutoCompleteType.HomeCountryRegion)
			{
				if (type > AutoCompleteType.Disabled)
				{
					if (type != AutoCompleteType.HomeCountryRegion)
					{
						goto IL_37;
					}
					return "HomeCountry";
				}
			}
			else
			{
				if (type == AutoCompleteType.BusinessCountryRegion)
				{
					return "BusinessCountry";
				}
				if (type == AutoCompleteType.Search)
				{
					return "search";
				}
				if (type != AutoCompleteType.Enabled)
				{
					goto IL_37;
				}
			}
			throw new InvalidOperationException();
			IL_37:
			string text = Enum.Format(typeof(AutoCompleteType), type, "G");
			if (text.StartsWith("Business", StringComparison.Ordinal))
			{
				text = text.Insert(8, ".");
			}
			else if (text.StartsWith("Home", StringComparison.Ordinal))
			{
				text = text.Insert(4, ".");
			}
			return "vCard." + text;
		}

		// Token: 0x06003F58 RID: 16216 RVA: 0x000CB8D4 File Offset: 0x000C9AD4
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			Page page = this.Page;
			if (page != null && base.IsEnabled)
			{
				if (!this.SaveTextViewState)
				{
					page.RegisterEnabledControl(this);
				}
				if (this.AutoPostBack)
				{
					page.RegisterWebFormsScript();
					page.RegisterPostBackScript();
					page.RegisterFocusScript();
				}
			}
		}

		// Token: 0x06003F59 RID: 16217 RVA: 0x000CB923 File Offset: 0x000C9B23
		bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			return this.LoadPostData(postDataKey, postCollection);
		}

		// Token: 0x06003F5A RID: 16218 RVA: 0x000CB930 File Offset: 0x000C9B30
		protected virtual bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			base.ValidateEvent(postDataKey);
			string text = this.Text;
			string text2 = postCollection[postDataKey];
			if (!this.ReadOnly && !text.Equals(text2, StringComparison.Ordinal))
			{
				this.Text = text2;
				return true;
			}
			return false;
		}

		// Token: 0x06003F5B RID: 16219 RVA: 0x000CB970 File Offset: 0x000C9B70
		protected virtual void OnTextChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[TextBox.EventTextChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06003F5C RID: 16220 RVA: 0x000CB99E File Offset: 0x000C9B9E
		void IPostBackDataHandler.RaisePostDataChangedEvent()
		{
			this.RaisePostDataChangedEvent();
		}

		// Token: 0x06003F5D RID: 16221 RVA: 0x000CB9A8 File Offset: 0x000C9BA8
		protected virtual void RaisePostDataChangedEvent()
		{
			if (this.AutoPostBack && !this.Page.IsPostBackEventControlRegistered)
			{
				this.Page.AutoPostBackControl = this;
				if (this.CausesValidation)
				{
					this.Page.Validate(this.ValidationGroup);
				}
			}
			this.OnTextChanged(EventArgs.Empty);
		}

		// Token: 0x06003F5E RID: 16222 RVA: 0x000CB9FC File Offset: 0x000C9BFC
		protected internal override void Render(HtmlTextWriter writer)
		{
			this.RenderBeginTag(writer);
			if (this.TextMode == TextBoxMode.MultiLine)
			{
				if (this.RenderingCompatibility >= VersionUtil.Framework45)
				{
					writer.Write(Environment.NewLine);
					HttpUtility.HtmlEncode(this.Text, writer);
				}
				else
				{
					HttpUtility.HtmlEncode(Environment.NewLine + this.Text, writer);
				}
			}
			this.RenderEndTag(writer);
		}

		// Token: 0x06003F5F RID: 16223 RVA: 0x000CBA61 File Offset: 0x000C9C61
		protected override object SaveViewState()
		{
			if (!this.SaveTextViewState)
			{
				this.ViewState.SetItemDirty("Text", false);
			}
			return base.SaveViewState();
		}

		// Token: 0x0400243A RID: 9274
		private static readonly object EventTextChanged = new object();

		// Token: 0x0400243B RID: 9275
		private const string _textBoxKeyHandlerCall = "if (WebForm_TextBoxKeyHandler(event) == false) return false;";

		// Token: 0x0400243C RID: 9276
		private const int DefaultMutliLineRows = 2;

		// Token: 0x0400243D RID: 9277
		private const int DefaultMutliLineColumns = 20;
	}
}
