using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Web.Util;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000391 RID: 913
	[ControlValueProperty("Checked")]
	[DataBindingHandler("System.Web.UI.Design.TextDataBindingHandler, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultEvent("CheckedChanged")]
	[Designer("System.Web.UI.Design.WebControls.CheckBoxDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultProperty("Text")]
	[SupportsEventValidation]
	public class CheckBox : WebControl, IPostBackDataHandler, ICheckBoxControl
	{
		// Token: 0x06002B55 RID: 11093 RVA: 0x00087CE0 File Offset: 0x00085EE0
		public CheckBox() : base(HtmlTextWriterTag.Input)
		{
		}

		// Token: 0x17000C35 RID: 3125
		// (get) Token: 0x06002B56 RID: 11094 RVA: 0x0008D840 File Offset: 0x0008BA40
		// (set) Token: 0x06002B57 RID: 11095 RVA: 0x0008D869 File Offset: 0x0008BA69
		[DefaultValue(false)]
		[WebCategory("Behavior")]
		[WebSysDescription("CheckBox_AutoPostBack")]
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

		// Token: 0x17000C36 RID: 3126
		// (get) Token: 0x06002B58 RID: 11096 RVA: 0x0008D884 File Offset: 0x0008BA84
		// (set) Token: 0x06002B59 RID: 11097 RVA: 0x0007E239 File Offset: 0x0007C439
		[DefaultValue(false)]
		[WebCategory("Behavior")]
		[WebSysDescription("AutoPostBackControl_CausesValidation")]
		[Themeable(false)]
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

		// Token: 0x17000C37 RID: 3127
		// (get) Token: 0x06002B5A RID: 11098 RVA: 0x0008D8B0 File Offset: 0x0008BAB0
		// (set) Token: 0x06002B5B RID: 11099 RVA: 0x0008D8D9 File Offset: 0x0008BAD9
		[Bindable(true, BindingDirection.TwoWay)]
		[DefaultValue(false)]
		[Themeable(false)]
		[WebSysDescription("CheckBox_Checked")]
		public virtual bool Checked
		{
			get
			{
				object obj = this.ViewState["Checked"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["Checked"] = value;
			}
		}

		// Token: 0x17000C38 RID: 3128
		// (get) Token: 0x06002B5C RID: 11100 RVA: 0x0008D8F4 File Offset: 0x0008BAF4
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("CheckBox_InputAttributes")]
		public AttributeCollection InputAttributes
		{
			get
			{
				if (this._inputAttributes == null)
				{
					if (this._inputAttributesState == null)
					{
						this._inputAttributesState = new StateBag(true);
						if (base.IsTrackingViewState)
						{
							this._inputAttributesState.TrackViewState();
						}
					}
					this._inputAttributes = new AttributeCollection(this._inputAttributesState);
				}
				return this._inputAttributes;
			}
		}

		// Token: 0x17000C39 RID: 3129
		// (get) Token: 0x06002B5D RID: 11101 RVA: 0x0008D948 File Offset: 0x0008BB48
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("CheckBox_LabelAttributes")]
		public AttributeCollection LabelAttributes
		{
			get
			{
				if (this._labelAttributes == null)
				{
					if (this._labelAttributesState == null)
					{
						this._labelAttributesState = new StateBag(true);
						if (base.IsTrackingViewState)
						{
							this._labelAttributesState.TrackViewState();
						}
					}
					this._labelAttributes = new AttributeCollection(this._labelAttributesState);
				}
				return this._labelAttributes;
			}
		}

		// Token: 0x17000C3A RID: 3130
		// (get) Token: 0x06002B5E RID: 11102 RVA: 0x000097B7 File Offset: 0x000079B7
		internal override bool RequiresLegacyRendering
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002B5F RID: 11103 RVA: 0x0008D99C File Offset: 0x0008BB9C
		private bool SaveCheckedViewState(bool autoPostBack)
		{
			if (base.Events[CheckBox.EventCheckedChanged] != null || !base.IsEnabled || !this.Visible || (autoPostBack && this.Page != null && !this.Page.ClientSupportsJavaScript))
			{
				return true;
			}
			Type type = base.GetType();
			return !(type == typeof(CheckBox)) && !(type == typeof(RadioButton));
		}

		// Token: 0x17000C3B RID: 3131
		// (get) Token: 0x06002B60 RID: 11104 RVA: 0x0008DA14 File Offset: 0x0008BC14
		// (set) Token: 0x06002B61 RID: 11105 RVA: 0x00087E45 File Offset: 0x00086045
		[Bindable(true)]
		[Localizable(true)]
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[WebSysDescription("CheckBox_Text")]
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

		// Token: 0x17000C3C RID: 3132
		// (get) Token: 0x06002B62 RID: 11106 RVA: 0x0008DA44 File Offset: 0x0008BC44
		// (set) Token: 0x06002B63 RID: 11107 RVA: 0x0008DA6D File Offset: 0x0008BC6D
		[WebCategory("Appearance")]
		[DefaultValue(TextAlign.Right)]
		[WebSysDescription("WebControl_TextAlign")]
		public virtual TextAlign TextAlign
		{
			get
			{
				object obj = this.ViewState["TextAlign"];
				if (obj != null)
				{
					return (TextAlign)obj;
				}
				return TextAlign.Right;
			}
			set
			{
				if (value < TextAlign.Left || value > TextAlign.Right)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["TextAlign"] = value;
			}
		}

		// Token: 0x17000C3D RID: 3133
		// (get) Token: 0x06002B64 RID: 11108 RVA: 0x0008DA98 File Offset: 0x0008BC98
		// (set) Token: 0x06002B65 RID: 11109 RVA: 0x0007E369 File Offset: 0x0007C569
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

		// Token: 0x1400005A RID: 90
		// (add) Token: 0x06002B66 RID: 11110 RVA: 0x0008DAC5 File Offset: 0x0008BCC5
		// (remove) Token: 0x06002B67 RID: 11111 RVA: 0x0008DAD8 File Offset: 0x0008BCD8
		[WebCategory("Action")]
		[WebSysDescription("Control_OnServerCheckChanged")]
		public event EventHandler CheckedChanged
		{
			add
			{
				base.Events.AddHandler(CheckBox.EventCheckedChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(CheckBox.EventCheckedChanged, value);
			}
		}

		// Token: 0x06002B68 RID: 11112 RVA: 0x0008DAEB File Offset: 0x0008BCEB
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddDisplayInlineBlockIfNeeded(writer);
		}

		// Token: 0x06002B69 RID: 11113 RVA: 0x0008DAF4 File Offset: 0x0008BCF4
		protected override void LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				Triplet triplet = (Triplet)savedState;
				base.LoadViewState(triplet.First);
				if (triplet.Second != null)
				{
					if (this._inputAttributesState == null)
					{
						this._inputAttributesState = new StateBag();
						this._inputAttributesState.TrackViewState();
					}
					this._inputAttributesState.LoadViewState(triplet.Second);
				}
				if (triplet.Third != null)
				{
					if (this._labelAttributesState == null)
					{
						this._labelAttributesState = new StateBag();
						this._labelAttributesState.TrackViewState();
					}
					this._labelAttributesState.LoadViewState(BinaryCompatibility.Current.TargetsAtLeastFramework48 ? triplet.Third : triplet.Second);
				}
			}
		}

		// Token: 0x06002B6A RID: 11114 RVA: 0x0008DB9C File Offset: 0x0008BD9C
		protected virtual void OnCheckedChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[CheckBox.EventCheckedChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06002B6B RID: 11115 RVA: 0x0008DBCC File Offset: 0x0008BDCC
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			bool autoPostBack = this.AutoPostBack;
			if (this.Page != null && base.IsEnabled)
			{
				this.Page.RegisterRequiresPostBack(this);
				if (autoPostBack)
				{
					this.Page.RegisterPostBackScript();
					this.Page.RegisterFocusScript();
					if (this.CausesValidation && this.Page.GetValidators(this.ValidationGroup).Count > 0)
					{
						this.Page.RegisterWebFormsScript();
					}
				}
			}
			if (!this.SaveCheckedViewState(autoPostBack))
			{
				this.ViewState.SetItemDirty("Checked", false);
				if (this.Page != null && base.IsEnabled)
				{
					this.Page.RegisterEnabledControl(this);
				}
			}
		}

		// Token: 0x06002B6C RID: 11116 RVA: 0x0008DC80 File Offset: 0x0008BE80
		protected override object SaveViewState()
		{
			object obj = base.SaveViewState();
			object obj2 = null;
			object obj3 = null;
			object result = null;
			if (this._inputAttributesState != null)
			{
				obj2 = this._inputAttributesState.SaveViewState();
			}
			if (this._labelAttributesState != null)
			{
				obj3 = this._labelAttributesState.SaveViewState();
			}
			if (obj != null || obj2 != null || obj3 != null)
			{
				result = new Triplet(obj, obj2, obj3);
			}
			return result;
		}

		// Token: 0x06002B6D RID: 11117 RVA: 0x0008DCD5 File Offset: 0x0008BED5
		protected override void TrackViewState()
		{
			base.TrackViewState();
			if (this._inputAttributesState != null)
			{
				this._inputAttributesState.TrackViewState();
			}
			if (this._labelAttributesState != null)
			{
				this._labelAttributesState.TrackViewState();
			}
		}

		// Token: 0x06002B6E RID: 11118 RVA: 0x0008DD04 File Offset: 0x0008BF04
		protected internal override void Render(HtmlTextWriter writer)
		{
			this.AddAttributesToRender(writer);
			if (this.Page != null)
			{
				this.Page.VerifyRenderingInServerForm(this);
			}
			bool flag = false;
			if (!base.IsEnabled)
			{
				if (this.RenderingCompatibility < VersionUtil.Framework40)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled");
					flag = true;
				}
				else if (!this.Enabled && !string.IsNullOrEmpty(WebControl.DisabledCssClass))
				{
					if (string.IsNullOrEmpty(this.CssClass))
					{
						base.ControlStyle.CssClass = WebControl.DisabledCssClass;
					}
					else
					{
						base.ControlStyle.CssClass = WebControl.DisabledCssClass + " " + this.CssClass;
					}
					flag = true;
				}
			}
			if (base.ControlStyleCreated)
			{
				Style controlStyle = base.ControlStyle;
				if (!controlStyle.IsEmpty)
				{
					controlStyle.AddAttributesToRender(writer, this);
					flag = true;
				}
			}
			string toolTip = this.ToolTip;
			if (toolTip.Length > 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Title, toolTip);
				flag = true;
			}
			string text = null;
			if (base.HasAttributes)
			{
				AttributeCollection attributes = base.Attributes;
				string text2 = attributes["value"];
				if (text2 != null)
				{
					attributes.Remove("value");
				}
				text = attributes["onclick"];
				if (text != null)
				{
					text = Util.EnsureEndWithSemiColon(text);
					attributes.Remove("onclick");
				}
				if (attributes.Count != 0)
				{
					attributes.AddAttributes(writer);
					flag = true;
				}
				if (text2 != null)
				{
					attributes["value"] = text2;
				}
			}
			if (flag)
			{
				writer.RenderBeginTag(HtmlTextWriterTag.Span);
			}
			string text3 = this.Text;
			string clientID = this.ClientID;
			if (text3.Length != 0)
			{
				if (this.TextAlign == TextAlign.Left)
				{
					this.RenderLabel(writer, text3, clientID);
					this.RenderInputTag(writer, clientID, text);
				}
				else
				{
					this.RenderInputTag(writer, clientID, text);
					this.RenderLabel(writer, text3, clientID);
				}
			}
			else
			{
				this.RenderInputTag(writer, clientID, text);
			}
			if (flag)
			{
				writer.RenderEndTag();
			}
		}

		// Token: 0x06002B6F RID: 11119 RVA: 0x0008DED0 File Offset: 0x0008C0D0
		private void RenderLabel(HtmlTextWriter writer, string text, string clientID)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.For, clientID);
			if (BinaryCompatibility.Current.TargetsAtLeastFramework48 && this._labelAttributesState != null && this._labelAttributesState.Count != 0)
			{
				this.LabelAttributes.AddAttributes(writer);
			}
			else if (this._labelAttributes != null && this._labelAttributes.Count != 0)
			{
				this._labelAttributes.AddAttributes(writer);
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Label);
			writer.Write(text);
			writer.RenderEndTag();
		}

		// Token: 0x06002B70 RID: 11120 RVA: 0x0008DF4C File Offset: 0x0008C14C
		internal virtual void RenderInputTag(HtmlTextWriter writer, string clientID, string onClick)
		{
			if (clientID != null)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Id, clientID);
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Type, "checkbox");
			if (this.UniqueID != null)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Name, this.UniqueID);
			}
			if (this._valueAttribute != null)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Value, this._valueAttribute);
			}
			if (this.Checked)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Checked, "checked");
			}
			if (!base.IsEnabled && this.SupportsDisabledAttribute)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled");
			}
			if (this.AutoPostBack && this.Page != null && this.Page.ClientSupportsJavaScript)
			{
				PostBackOptions postBackOptions = new PostBackOptions(this, string.Empty);
				if (this.CausesValidation && this.Page.GetValidators(this.ValidationGroup).Count > 0)
				{
					postBackOptions.PerformValidation = true;
					postBackOptions.ValidationGroup = this.ValidationGroup;
				}
				if (this.Page.Form != null)
				{
					postBackOptions.AutoPostBack = true;
				}
				onClick = Util.MergeScript(onClick, this.Page.ClientScript.GetPostBackEventReference(postBackOptions, true));
				writer.AddAttribute(HtmlTextWriterAttribute.Onclick, onClick);
				if (base.EnableLegacyRendering)
				{
					writer.AddAttribute("language", "javascript", false);
				}
			}
			else
			{
				if (this.Page != null)
				{
					this.Page.ClientScript.RegisterForEventValidation(this.UniqueID);
				}
				if (onClick != null)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Onclick, onClick);
				}
			}
			string accessKey = this.AccessKey;
			if (accessKey.Length > 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Accesskey, accessKey);
			}
			int tabIndex = (int)this.TabIndex;
			if (tabIndex != 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Tabindex, tabIndex.ToString(NumberFormatInfo.InvariantInfo));
			}
			if (BinaryCompatibility.Current.TargetsAtLeastFramework48 && this._inputAttributesState != null && this._inputAttributesState.Count != 0)
			{
				this.InputAttributes.AddAttributes(writer);
			}
			else if (this._inputAttributes != null && this._inputAttributes.Count != 0)
			{
				this._inputAttributes.AddAttributes(writer);
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Input);
			writer.RenderEndTag();
		}

		// Token: 0x06002B71 RID: 11121 RVA: 0x0008E146 File Offset: 0x0008C346
		bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			return this.LoadPostData(postDataKey, postCollection);
		}

		// Token: 0x06002B72 RID: 11122 RVA: 0x0008E150 File Offset: 0x0008C350
		protected virtual bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			string value = postCollection[postDataKey];
			bool flag = !string.IsNullOrEmpty(value);
			if (flag)
			{
				base.ValidateEvent(postDataKey);
			}
			bool result = flag != this.Checked;
			this.Checked = flag;
			return result;
		}

		// Token: 0x06002B73 RID: 11123 RVA: 0x0008E190 File Offset: 0x0008C390
		void IPostBackDataHandler.RaisePostDataChangedEvent()
		{
			this.RaisePostDataChangedEvent();
		}

		// Token: 0x06002B74 RID: 11124 RVA: 0x0008E198 File Offset: 0x0008C398
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
			this.OnCheckedChanged(EventArgs.Empty);
		}

		// Token: 0x04001F12 RID: 7954
		internal AttributeCollection _inputAttributes;

		// Token: 0x04001F13 RID: 7955
		private StateBag _inputAttributesState;

		// Token: 0x04001F14 RID: 7956
		private AttributeCollection _labelAttributes;

		// Token: 0x04001F15 RID: 7957
		private StateBag _labelAttributesState;

		// Token: 0x04001F16 RID: 7958
		private string _valueAttribute;

		// Token: 0x04001F17 RID: 7959
		private static readonly object EventCheckedChanged = new object();
	}
}
