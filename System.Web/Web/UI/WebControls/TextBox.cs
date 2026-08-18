using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000665 RID: 1637
	[ControlValueProperty("Text")]
	[ControlBuilder(typeof(TextBoxControlBuilder))]
	[DefaultProperty("Text")]
	[SupportsEventValidation]
	[ValidationProperty("Text")]
	[DataBindingHandler("System.Web.UI.Design.TextDataBindingHandler, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultEvent("TextChanged")]
	[Designer("System.Web.UI.Design.WebControls.PreviewControlDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ParseChildren(true, "Text")]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class TextBox : WebControl, IPostBackDataHandler, IEditableTextControl, ITextControl
	{
		// Token: 0x06004FEB RID: 20459 RVA: 0x001409C4 File Offset: 0x0013F9C4
		public TextBox() : base(HtmlTextWriterTag.Input)
		{
		}

		// Token: 0x17001438 RID: 5176
		// (get) Token: 0x06004FEC RID: 20460 RVA: 0x001409D0 File Offset: 0x0013F9D0
		// (set) Token: 0x06004FED RID: 20461 RVA: 0x001409F9 File Offset: 0x0013F9F9
		[WebCategory("Behavior")]
		[WebSysDescription("TextBox_AutoCompleteType")]
		[Themeable(false)]
		[DefaultValue(AutoCompleteType.None)]
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
				if (value < AutoCompleteType.None || value > AutoCompleteType.Search)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["AutoCompleteType"] = value;
			}
		}

		// Token: 0x17001439 RID: 5177
		// (get) Token: 0x06004FEE RID: 20462 RVA: 0x00140A28 File Offset: 0x0013FA28
		// (set) Token: 0x06004FEF RID: 20463 RVA: 0x00140A51 File Offset: 0x0013FA51
		[DefaultValue(false)]
		[WebCategory("Behavior")]
		[WebSysDescription("TextBox_AutoPostBack")]
		[Themeable(false)]
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

		// Token: 0x1700143A RID: 5178
		// (get) Token: 0x06004FF0 RID: 20464 RVA: 0x00140A6C File Offset: 0x0013FA6C
		// (set) Token: 0x06004FF1 RID: 20465 RVA: 0x00140A95 File Offset: 0x0013FA95
		[WebSysDescription("AutoPostBackControl_CausesValidation")]
		[Themeable(false)]
		[WebCategory("Behavior")]
		[DefaultValue(false)]
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

		// Token: 0x1700143B RID: 5179
		// (get) Token: 0x06004FF2 RID: 20466 RVA: 0x00140AB0 File Offset: 0x0013FAB0
		// (set) Token: 0x06004FF3 RID: 20467 RVA: 0x00140AD9 File Offset: 0x0013FAD9
		[DefaultValue(0)]
		[WebSysDescription("TextBox_Columns")]
		[WebCategory("Appearance")]
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

		// Token: 0x1700143C RID: 5180
		// (get) Token: 0x06004FF4 RID: 20468 RVA: 0x00140B0C File Offset: 0x0013FB0C
		// (set) Token: 0x06004FF5 RID: 20469 RVA: 0x00140B35 File Offset: 0x0013FB35
		[DefaultValue(0)]
		[Themeable(false)]
		[WebSysDescription("TextBox_MaxLength")]
		[WebCategory("Behavior")]
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

		// Token: 0x1700143D RID: 5181
		// (get) Token: 0x06004FF6 RID: 20470 RVA: 0x00140B5C File Offset: 0x0013FB5C
		// (set) Token: 0x06004FF7 RID: 20471 RVA: 0x00140B85 File Offset: 0x0013FB85
		[WebCategory("Behavior")]
		[WebSysDescription("TextBox_TextMode")]
		[Themeable(false)]
		[DefaultValue(TextBoxMode.SingleLine)]
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
				if (value < TextBoxMode.SingleLine || value > TextBoxMode.Password)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["Mode"] = value;
			}
		}

		// Token: 0x1700143E RID: 5182
		// (get) Token: 0x06004FF8 RID: 20472 RVA: 0x00140BB0 File Offset: 0x0013FBB0
		// (set) Token: 0x06004FF9 RID: 20473 RVA: 0x00140BD9 File Offset: 0x0013FBD9
		[DefaultValue(false)]
		[WebCategory("Behavior")]
		[WebSysDescription("TextBox_ReadOnly")]
		[Bindable(true)]
		[Themeable(false)]
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

		// Token: 0x1700143F RID: 5183
		// (get) Token: 0x06004FFA RID: 20474 RVA: 0x00140BF4 File Offset: 0x0013FBF4
		// (set) Token: 0x06004FFB RID: 20475 RVA: 0x00140C1D File Offset: 0x0013FC1D
		[WebSysDescription("TextBox_Rows")]
		[DefaultValue(0)]
		[Themeable(false)]
		[WebCategory("Behavior")]
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

		// Token: 0x17001440 RID: 5184
		// (get) Token: 0x06004FFC RID: 20476 RVA: 0x00140C50 File Offset: 0x0013FC50
		private bool SaveTextViewState
		{
			get
			{
				return this.TextMode != TextBoxMode.Password && (base.Events[TextBox.EventTextChanged] != null || !base.IsEnabled || !this.Visible || this.ReadOnly || base.GetType() != typeof(TextBox));
			}
		}

		// Token: 0x17001441 RID: 5185
		// (get) Token: 0x06004FFD RID: 20477 RVA: 0x00140CA7 File Offset: 0x0013FCA7
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

		// Token: 0x17001442 RID: 5186
		// (get) Token: 0x06004FFE RID: 20478 RVA: 0x00140CB8 File Offset: 0x0013FCB8
		// (set) Token: 0x06004FFF RID: 20479 RVA: 0x00140CE5 File Offset: 0x0013FCE5
		[WebCategory("Appearance")]
		[Bindable(true, BindingDirection.TwoWay)]
		[WebSysDescription("TextBox_Text")]
		[PersistenceMode(PersistenceMode.EncodedInnerDefaultProperty)]
		[Editor("System.ComponentModel.Design.MultilineStringEditor,System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[Localizable(true)]
		[DefaultValue("")]
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

		// Token: 0x17001443 RID: 5187
		// (get) Token: 0x06005000 RID: 20480 RVA: 0x00140CF8 File Offset: 0x0013FCF8
		// (set) Token: 0x06005001 RID: 20481 RVA: 0x00140D25 File Offset: 0x0013FD25
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

		// Token: 0x17001444 RID: 5188
		// (get) Token: 0x06005002 RID: 20482 RVA: 0x00140D38 File Offset: 0x0013FD38
		// (set) Token: 0x06005003 RID: 20483 RVA: 0x00140D61 File Offset: 0x0013FD61
		[DefaultValue(true)]
		[WebCategory("Layout")]
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

		// Token: 0x14000101 RID: 257
		// (add) Token: 0x06005004 RID: 20484 RVA: 0x00140D79 File Offset: 0x0013FD79
		// (remove) Token: 0x06005005 RID: 20485 RVA: 0x00140D8C File Offset: 0x0013FD8C
		[WebSysDescription("TextBox_OnTextChanged")]
		[WebCategory("Action")]
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

		// Token: 0x06005006 RID: 20486 RVA: 0x00140DA0 File Offset: 0x0013FDA0
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
			}
			else
			{
				if (textMode == TextBoxMode.SingleLine)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Type, "text");
					if (this.AutoCompleteType != AutoCompleteType.None && this.Context != null && this.Context.Request.Browser["supportsVCard"] == "true")
					{
						if (this.AutoCompleteType == AutoCompleteType.Disabled)
						{
							writer.AddAttribute(HtmlTextWriterAttribute.AutoComplete, "off");
						}
						else if (this.AutoCompleteType == AutoCompleteType.Search)
						{
							writer.AddAttribute(HtmlTextWriterAttribute.VCardName, "search");
						}
						else if (this.AutoCompleteType == AutoCompleteType.HomeCountryRegion)
						{
							writer.AddAttribute(HtmlTextWriterAttribute.VCardName, "HomeCountry");
						}
						else if (this.AutoCompleteType == AutoCompleteType.BusinessCountryRegion)
						{
							writer.AddAttribute(HtmlTextWriterAttribute.VCardName, "BusinessCountry");
						}
						else
						{
							string text = Enum.Format(typeof(AutoCompleteType), this.AutoCompleteType, "G");
							if (text.StartsWith("Business", StringComparison.Ordinal))
							{
								text = text.Insert(8, ".");
							}
							else if (text.StartsWith("Home", StringComparison.Ordinal))
							{
								text = text.Insert(4, ".");
							}
							writer.AddAttribute(HtmlTextWriterAttribute.VCardName, "vCard." + text);
						}
					}
					string text2 = this.Text;
					if (text2.Length > 0)
					{
						writer.AddAttribute(HtmlTextWriterAttribute.Value, text2);
					}
				}
				else if (textMode == TextBoxMode.Password)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Type, "password");
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
				string text3 = null;
				if (base.HasAttributes)
				{
					text3 = base.Attributes["onchange"];
					if (text3 != null)
					{
						text3 = Util.EnsureEndWithSemiColon(text3);
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
				text3 = Util.MergeScript(text3, page.ClientScript.GetPostBackEventReference(postBackOptions, true));
				writer.AddAttribute(HtmlTextWriterAttribute.Onchange, text3);
				if (textMode != TextBoxMode.MultiLine)
				{
					string text4 = "if (WebForm_TextBoxKeyHandler(event) == false) return false;";
					if (base.HasAttributes)
					{
						string text5 = base.Attributes["onkeypress"];
						if (text5 != null)
						{
							text4 += text5;
							base.Attributes.Remove("onkeypress");
						}
					}
					writer.AddAttribute("onkeypress", text4);
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
			if (this.Enabled && !base.IsEnabled)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled");
			}
			base.AddAttributesToRender(writer);
		}

		// Token: 0x06005007 RID: 20487 RVA: 0x0014116C File Offset: 0x0014016C
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

		// Token: 0x06005008 RID: 20488 RVA: 0x001411CC File Offset: 0x001401CC
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

		// Token: 0x06005009 RID: 20489 RVA: 0x0014121B File Offset: 0x0014021B
		bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			return this.LoadPostData(postDataKey, postCollection);
		}

		// Token: 0x0600500A RID: 20490 RVA: 0x00141228 File Offset: 0x00140228
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

		// Token: 0x0600500B RID: 20491 RVA: 0x00141268 File Offset: 0x00140268
		protected virtual void OnTextChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[TextBox.EventTextChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600500C RID: 20492 RVA: 0x00141296 File Offset: 0x00140296
		void IPostBackDataHandler.RaisePostDataChangedEvent()
		{
			this.RaisePostDataChangedEvent();
		}

		// Token: 0x0600500D RID: 20493 RVA: 0x001412A0 File Offset: 0x001402A0
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

		// Token: 0x0600500E RID: 20494 RVA: 0x001412F2 File Offset: 0x001402F2
		protected internal override void Render(HtmlTextWriter writer)
		{
			this.RenderBeginTag(writer);
			if (this.TextMode == TextBoxMode.MultiLine)
			{
				HttpUtility.HtmlEncode(this.Text, writer);
			}
			this.RenderEndTag(writer);
		}

		// Token: 0x0600500F RID: 20495 RVA: 0x00141317 File Offset: 0x00140317
		protected override object SaveViewState()
		{
			if (!this.SaveTextViewState)
			{
				this.ViewState.SetItemDirty("Text", false);
			}
			return base.SaveViewState();
		}

		// Token: 0x04002D07 RID: 11527
		private const string _textBoxKeyHandlerCall = "if (WebForm_TextBoxKeyHandler(event) == false) return false;";

		// Token: 0x04002D08 RID: 11528
		private const int DefaultMutliLineRows = 2;

		// Token: 0x04002D09 RID: 11529
		private const int DefaultMutliLineColumns = 20;

		// Token: 0x04002D0A RID: 11530
		private static readonly object EventTextChanged = new object();
	}
}
