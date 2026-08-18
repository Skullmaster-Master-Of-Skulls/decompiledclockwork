using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Web.Util;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000514 RID: 1300
	[ParseChildren(true)]
	[PersistChildren(false)]
	[Themeable(true)]
	public class WebControl : Control, IAttributeAccessor
	{
		// Token: 0x06004138 RID: 16696 RVA: 0x000D58A2 File Offset: 0x000D3AA2
		protected WebControl() : this(HtmlTextWriterTag.Span)
		{
		}

		// Token: 0x06004139 RID: 16697 RVA: 0x000D58AC File Offset: 0x000D3AAC
		public WebControl(HtmlTextWriterTag tag)
		{
			this.tagKey = tag;
		}

		// Token: 0x0600413A RID: 16698 RVA: 0x000D58BB File Offset: 0x000D3ABB
		protected WebControl(string tag)
		{
			this.tagKey = HtmlTextWriterTag.Unknown;
			this.tagName = tag;
		}

		// Token: 0x17001312 RID: 4882
		// (get) Token: 0x0600413B RID: 16699 RVA: 0x000D58D4 File Offset: 0x000D3AD4
		// (set) Token: 0x0600413C RID: 16700 RVA: 0x000D590F File Offset: 0x000D3B0F
		[DefaultValue("")]
		[WebCategory("Accessibility")]
		[WebSysDescription("WebControl_AccessKey")]
		public virtual string AccessKey
		{
			get
			{
				if (this._webControlFlags[4])
				{
					string text = (string)this.ViewState["AccessKey"];
					if (text != null)
					{
						return text;
					}
				}
				return string.Empty;
			}
			set
			{
				if (value != null && value.Length > 1)
				{
					throw new ArgumentOutOfRangeException("value", SR.GetString("WebControl_InvalidAccessKey"));
				}
				this.ViewState["AccessKey"] = value;
				this._webControlFlags.Set(4);
			}
		}

		// Token: 0x17001313 RID: 4883
		// (get) Token: 0x0600413D RID: 16701 RVA: 0x000D5950 File Offset: 0x000D3B50
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("WebControl_Attributes")]
		public AttributeCollection Attributes
		{
			get
			{
				if (this.attrColl == null)
				{
					if (this.attrState == null)
					{
						this.attrState = new StateBag(true);
						if (base.IsTrackingViewState)
						{
							this.attrState.TrackViewState();
						}
					}
					this.attrColl = new AttributeCollection(this.attrState);
				}
				return this.attrColl;
			}
		}

		// Token: 0x17001314 RID: 4884
		// (get) Token: 0x0600413E RID: 16702 RVA: 0x000D59A3 File Offset: 0x000D3BA3
		// (set) Token: 0x0600413F RID: 16703 RVA: 0x000D59BE File Offset: 0x000D3BBE
		[WebCategory("Appearance")]
		[DefaultValue(typeof(Color), "")]
		[WebSysDescription("WebControl_BackColor")]
		[TypeConverter(typeof(WebColorConverter))]
		public virtual Color BackColor
		{
			get
			{
				if (!this.ControlStyleCreated)
				{
					return Color.Empty;
				}
				return this.ControlStyle.BackColor;
			}
			set
			{
				this.ControlStyle.BackColor = value;
			}
		}

		// Token: 0x17001315 RID: 4885
		// (get) Token: 0x06004140 RID: 16704 RVA: 0x000D59CC File Offset: 0x000D3BCC
		// (set) Token: 0x06004141 RID: 16705 RVA: 0x000D59E7 File Offset: 0x000D3BE7
		[WebCategory("Appearance")]
		[DefaultValue(typeof(Color), "")]
		[WebSysDescription("WebControl_BorderColor")]
		[TypeConverter(typeof(WebColorConverter))]
		public virtual Color BorderColor
		{
			get
			{
				if (!this.ControlStyleCreated)
				{
					return Color.Empty;
				}
				return this.ControlStyle.BorderColor;
			}
			set
			{
				this.ControlStyle.BorderColor = value;
			}
		}

		// Token: 0x17001316 RID: 4886
		// (get) Token: 0x06004142 RID: 16706 RVA: 0x000D59F5 File Offset: 0x000D3BF5
		// (set) Token: 0x06004143 RID: 16707 RVA: 0x000D5A10 File Offset: 0x000D3C10
		[WebCategory("Appearance")]
		[DefaultValue(typeof(Unit), "")]
		[WebSysDescription("WebControl_BorderWidth")]
		public virtual Unit BorderWidth
		{
			get
			{
				if (!this.ControlStyleCreated)
				{
					return Unit.Empty;
				}
				return this.ControlStyle.BorderWidth;
			}
			set
			{
				this.ControlStyle.BorderWidth = value;
			}
		}

		// Token: 0x17001317 RID: 4887
		// (get) Token: 0x06004144 RID: 16708 RVA: 0x000D5A1E File Offset: 0x000D3C1E
		// (set) Token: 0x06004145 RID: 16709 RVA: 0x000D5A35 File Offset: 0x000D3C35
		[WebCategory("Appearance")]
		[DefaultValue(BorderStyle.NotSet)]
		[WebSysDescription("WebControl_BorderStyle")]
		public virtual BorderStyle BorderStyle
		{
			get
			{
				if (!this.ControlStyleCreated)
				{
					return BorderStyle.NotSet;
				}
				return this.ControlStyle.BorderStyle;
			}
			set
			{
				this.ControlStyle.BorderStyle = value;
			}
		}

		// Token: 0x17001318 RID: 4888
		// (get) Token: 0x06004146 RID: 16710 RVA: 0x000D5A44 File Offset: 0x000D3C44
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("WebControl_ControlStyle")]
		public Style ControlStyle
		{
			get
			{
				if (this.controlStyle == null)
				{
					this.controlStyle = this.CreateControlStyle();
					if (base.IsTrackingViewState)
					{
						this.controlStyle.TrackViewState();
					}
					if (this._webControlFlags[1])
					{
						this._webControlFlags.Clear(1);
						this.controlStyle.LoadViewState(null);
					}
				}
				return this.controlStyle;
			}
		}

		// Token: 0x17001319 RID: 4889
		// (get) Token: 0x06004147 RID: 16711 RVA: 0x000D5AA4 File Offset: 0x000D3CA4
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[WebSysDescription("WebControl_ControlStyleCreated")]
		public bool ControlStyleCreated
		{
			get
			{
				return this.controlStyle != null;
			}
		}

		// Token: 0x1700131A RID: 4890
		// (get) Token: 0x06004148 RID: 16712 RVA: 0x000D5AAF File Offset: 0x000D3CAF
		// (set) Token: 0x06004149 RID: 16713 RVA: 0x000D5ACA File Offset: 0x000D3CCA
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[WebSysDescription("WebControl_CSSClassName")]
		[CssClassProperty]
		public virtual string CssClass
		{
			get
			{
				if (!this.ControlStyleCreated)
				{
					return string.Empty;
				}
				return this.ControlStyle.CssClass;
			}
			set
			{
				this.ControlStyle.CssClass = value;
			}
		}

		// Token: 0x1700131B RID: 4891
		// (get) Token: 0x0600414A RID: 16714 RVA: 0x000D5AD8 File Offset: 0x000D3CD8
		// (set) Token: 0x0600414B RID: 16715 RVA: 0x000D5AE8 File Offset: 0x000D3CE8
		public static string DisabledCssClass
		{
			get
			{
				return WebControl._disabledCssClass ?? string.Empty;
			}
			set
			{
				WebControl._disabledCssClass = value;
			}
		}

		// Token: 0x1700131C RID: 4892
		// (get) Token: 0x0600414C RID: 16716 RVA: 0x000D5AF0 File Offset: 0x000D3CF0
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("WebControl_Style")]
		public CssStyleCollection Style
		{
			get
			{
				return this.Attributes.CssStyle;
			}
		}

		// Token: 0x1700131D RID: 4893
		// (get) Token: 0x0600414D RID: 16717 RVA: 0x000D5AFD File Offset: 0x000D3CFD
		// (set) Token: 0x0600414E RID: 16718 RVA: 0x000D5B14 File Offset: 0x000D3D14
		[Bindable(true)]
		[WebCategory("Behavior")]
		[Themeable(false)]
		[DefaultValue(true)]
		[WebSysDescription("WebControl_Enabled")]
		public virtual bool Enabled
		{
			get
			{
				return !this.flags[524288];
			}
			set
			{
				bool flag = !this.flags[524288];
				if (flag != value)
				{
					if (!value)
					{
						this.flags.Set(524288);
					}
					else
					{
						this.flags.Clear(524288);
					}
					if (base.IsTrackingViewState)
					{
						this._webControlFlags.Set(2);
					}
				}
			}
		}

		// Token: 0x1700131E RID: 4894
		// (get) Token: 0x0600414F RID: 16719 RVA: 0x00075E05 File Offset: 0x00074005
		// (set) Token: 0x06004150 RID: 16720 RVA: 0x00075E0D File Offset: 0x0007400D
		[Browsable(true)]
		public override bool EnableTheming
		{
			get
			{
				return base.EnableTheming;
			}
			set
			{
				base.EnableTheming = value;
			}
		}

		// Token: 0x1700131F RID: 4895
		// (get) Token: 0x06004151 RID: 16721 RVA: 0x000D5B72 File Offset: 0x000D3D72
		[WebCategory("Appearance")]
		[WebSysDescription("WebControl_Font")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		public virtual FontInfo Font
		{
			get
			{
				return this.ControlStyle.Font;
			}
		}

		// Token: 0x17001320 RID: 4896
		// (get) Token: 0x06004152 RID: 16722 RVA: 0x000D5B7F File Offset: 0x000D3D7F
		// (set) Token: 0x06004153 RID: 16723 RVA: 0x000D5B9A File Offset: 0x000D3D9A
		[WebCategory("Appearance")]
		[DefaultValue(typeof(Color), "")]
		[WebSysDescription("WebControl_ForeColor")]
		[TypeConverter(typeof(WebColorConverter))]
		public virtual Color ForeColor
		{
			get
			{
				if (!this.ControlStyleCreated)
				{
					return Color.Empty;
				}
				return this.ControlStyle.ForeColor;
			}
			set
			{
				this.ControlStyle.ForeColor = value;
			}
		}

		// Token: 0x17001321 RID: 4897
		// (get) Token: 0x06004154 RID: 16724 RVA: 0x000D5BA8 File Offset: 0x000D3DA8
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool HasAttributes
		{
			get
			{
				return (this.attrColl != null && this.attrColl.Count > 0) || (this.attrState != null && this.attrState.Count > 0);
			}
		}

		// Token: 0x17001322 RID: 4898
		// (get) Token: 0x06004155 RID: 16725 RVA: 0x000D5BDA File Offset: 0x000D3DDA
		// (set) Token: 0x06004156 RID: 16726 RVA: 0x000D5BF5 File Offset: 0x000D3DF5
		[WebCategory("Layout")]
		[DefaultValue(typeof(Unit), "")]
		[WebSysDescription("WebControl_Height")]
		public virtual Unit Height
		{
			get
			{
				if (!this.ControlStyleCreated)
				{
					return Unit.Empty;
				}
				return this.ControlStyle.Height;
			}
			set
			{
				this.ControlStyle.Height = value;
			}
		}

		// Token: 0x17001323 RID: 4899
		// (get) Token: 0x06004157 RID: 16727 RVA: 0x000D5C04 File Offset: 0x000D3E04
		protected internal bool IsEnabled
		{
			get
			{
				for (Control control = this; control != null; control = control.Parent)
				{
					if (control.flags[524288])
					{
						return false;
					}
				}
				return true;
			}
		}

		// Token: 0x17001324 RID: 4900
		// (get) Token: 0x06004158 RID: 16728 RVA: 0x000097B7 File Offset: 0x000079B7
		[Browsable(false)]
		public virtual bool SupportsDisabledAttribute
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001325 RID: 4901
		// (get) Token: 0x06004159 RID: 16729 RVA: 0x00007722 File Offset: 0x00005922
		internal virtual bool RequiresLegacyRendering
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001326 RID: 4902
		// (get) Token: 0x0600415A RID: 16730 RVA: 0x000B11D8 File Offset: 0x000AF3D8
		// (set) Token: 0x0600415B RID: 16731 RVA: 0x000B11E0 File Offset: 0x000AF3E0
		[Browsable(true)]
		public override string SkinID
		{
			get
			{
				return base.SkinID;
			}
			set
			{
				base.SkinID = value;
			}
		}

		// Token: 0x17001327 RID: 4903
		// (get) Token: 0x0600415C RID: 16732 RVA: 0x000D5C34 File Offset: 0x000D3E34
		// (set) Token: 0x0600415D RID: 16733 RVA: 0x000D5C6C File Offset: 0x000D3E6C
		[DefaultValue(0)]
		[WebCategory("Accessibility")]
		[WebSysDescription("WebControl_TabIndex")]
		public virtual short TabIndex
		{
			get
			{
				if (this._webControlFlags[16])
				{
					object obj = this.ViewState["TabIndex"];
					if (obj != null)
					{
						return (short)obj;
					}
				}
				return 0;
			}
			set
			{
				this.ViewState["TabIndex"] = value;
				this._webControlFlags.Set(16);
			}
		}

		// Token: 0x17001328 RID: 4904
		// (get) Token: 0x0600415E RID: 16734 RVA: 0x000D5C91 File Offset: 0x000D3E91
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		protected virtual HtmlTextWriterTag TagKey
		{
			get
			{
				return this.tagKey;
			}
		}

		// Token: 0x17001329 RID: 4905
		// (get) Token: 0x0600415F RID: 16735 RVA: 0x000D5C9C File Offset: 0x000D3E9C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		protected virtual string TagName
		{
			get
			{
				if (this.tagName == null && this.TagKey != HtmlTextWriterTag.Unknown)
				{
					this.tagName = Enum.Format(typeof(HtmlTextWriterTag), this.TagKey, "G").ToLower(CultureInfo.InvariantCulture);
				}
				return this.tagName;
			}
		}

		// Token: 0x1700132A RID: 4906
		// (get) Token: 0x06004160 RID: 16736 RVA: 0x000D5CF0 File Offset: 0x000D3EF0
		// (set) Token: 0x06004161 RID: 16737 RVA: 0x000D5D2B File Offset: 0x000D3F2B
		[WebCategory("Behavior")]
		[Localizable(true)]
		[DefaultValue("")]
		[WebSysDescription("WebControl_Tooltip")]
		public virtual string ToolTip
		{
			get
			{
				if (this._webControlFlags[8])
				{
					string text = (string)this.ViewState["ToolTip"];
					if (text != null)
					{
						return text;
					}
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["ToolTip"] = value;
				this._webControlFlags.Set(8);
			}
		}

		// Token: 0x1700132B RID: 4907
		// (get) Token: 0x06004162 RID: 16738 RVA: 0x000D5D4A File Offset: 0x000D3F4A
		// (set) Token: 0x06004163 RID: 16739 RVA: 0x000D5D65 File Offset: 0x000D3F65
		[WebCategory("Layout")]
		[DefaultValue(typeof(Unit), "")]
		[WebSysDescription("WebControl_Width")]
		public virtual Unit Width
		{
			get
			{
				if (!this.ControlStyleCreated)
				{
					return Unit.Empty;
				}
				return this.ControlStyle.Width;
			}
			set
			{
				this.ControlStyle.Width = value;
			}
		}

		// Token: 0x06004164 RID: 16740 RVA: 0x000D5D74 File Offset: 0x000D3F74
		protected virtual void AddAttributesToRender(HtmlTextWriter writer)
		{
			if (this.ID != null)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
			}
			if (this._webControlFlags[4])
			{
				string accessKey = this.AccessKey;
				if (accessKey.Length > 0)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Accesskey, accessKey);
				}
			}
			if (!this.Enabled)
			{
				if (this.SupportsDisabledAttribute)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled");
				}
				if (this.RenderingCompatibility >= VersionUtil.Framework40 && !string.IsNullOrEmpty(WebControl.DisabledCssClass))
				{
					if (string.IsNullOrEmpty(this.CssClass))
					{
						this.ControlStyle.CssClass = WebControl.DisabledCssClass;
					}
					else
					{
						this.ControlStyle.CssClass = WebControl.DisabledCssClass + " " + this.CssClass;
					}
				}
			}
			if (this._webControlFlags[16])
			{
				int tabIndex = (int)this.TabIndex;
				if (tabIndex != 0)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Tabindex, tabIndex.ToString(NumberFormatInfo.InvariantInfo));
				}
			}
			if (this._webControlFlags[8])
			{
				string toolTip = this.ToolTip;
				if (toolTip.Length > 0)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Title, toolTip);
				}
			}
			if (this.TagKey == HtmlTextWriterTag.Span || this.TagKey == HtmlTextWriterTag.A)
			{
				this.AddDisplayInlineBlockIfNeeded(writer);
			}
			if (this.ControlStyleCreated && !this.ControlStyle.IsEmpty)
			{
				this.ControlStyle.AddAttributesToRender(writer, this);
			}
			if (this.attrState != null)
			{
				AttributeCollection attributes = this.Attributes;
				foreach (object obj in attributes.Keys)
				{
					string text = (string)obj;
					writer.AddAttribute(text, attributes[text]);
				}
			}
		}

		// Token: 0x06004165 RID: 16741 RVA: 0x000D5F0C File Offset: 0x000D410C
		internal void AddDisplayInlineBlockIfNeeded(HtmlTextWriter writer)
		{
			if ((!this.RequiresLegacyRendering || !base.EnableLegacyRendering) && (this.BorderStyle != BorderStyle.NotSet || !this.BorderWidth.IsEmpty || !this.Height.IsEmpty || !this.Width.IsEmpty))
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "inline-block");
			}
		}

		// Token: 0x06004166 RID: 16742 RVA: 0x000D5F6E File Offset: 0x000D416E
		public void ApplyStyle(Style s)
		{
			if (s != null && !s.IsEmpty)
			{
				this.ControlStyle.CopyFrom(s);
			}
		}

		// Token: 0x06004167 RID: 16743 RVA: 0x000D5F88 File Offset: 0x000D4188
		public void CopyBaseAttributes(WebControl controlSrc)
		{
			if (controlSrc == null)
			{
				throw new ArgumentNullException("controlSrc");
			}
			if (controlSrc._webControlFlags[4])
			{
				this.AccessKey = controlSrc.AccessKey;
			}
			if (!controlSrc.Enabled)
			{
				this.Enabled = false;
			}
			if (controlSrc._webControlFlags[8])
			{
				this.ToolTip = controlSrc.ToolTip;
			}
			if (controlSrc._webControlFlags[16])
			{
				this.TabIndex = controlSrc.TabIndex;
			}
			if (controlSrc.HasAttributes)
			{
				foreach (object obj in controlSrc.Attributes.Keys)
				{
					string key = (string)obj;
					this.Attributes[key] = controlSrc.Attributes[key];
				}
			}
		}

		// Token: 0x06004168 RID: 16744 RVA: 0x000D606C File Offset: 0x000D426C
		protected virtual Style CreateControlStyle()
		{
			return new Style(this.ViewState);
		}

		// Token: 0x06004169 RID: 16745 RVA: 0x000D607C File Offset: 0x000D427C
		protected override void LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				Pair pair = (Pair)savedState;
				base.LoadViewState(pair.First);
				if (this.ControlStyleCreated || this.ViewState["_!SB"] != null)
				{
					this.ControlStyle.LoadViewState(null);
				}
				else
				{
					this._webControlFlags.Set(1);
				}
				if (pair.Second != null)
				{
					if (this.attrState == null)
					{
						this.attrState = new StateBag(true);
						this.attrState.TrackViewState();
					}
					this.attrState.LoadViewState(pair.Second);
				}
			}
			object obj = this.ViewState["Enabled"];
			if (obj != null)
			{
				if (!(bool)obj)
				{
					this.flags.Set(524288);
				}
				else
				{
					this.flags.Clear(524288);
				}
				this._webControlFlags.Set(2);
			}
			if (((IDictionary)this.ViewState).Contains("AccessKey"))
			{
				this._webControlFlags.Set(4);
			}
			if (((IDictionary)this.ViewState).Contains("TabIndex"))
			{
				this._webControlFlags.Set(16);
			}
			if (((IDictionary)this.ViewState).Contains("ToolTip"))
			{
				this._webControlFlags.Set(8);
			}
		}

		// Token: 0x0600416A RID: 16746 RVA: 0x000D61B0 File Offset: 0x000D43B0
		protected override void TrackViewState()
		{
			base.TrackViewState();
			if (this.ControlStyleCreated)
			{
				this.ControlStyle.TrackViewState();
			}
			if (this.attrState != null)
			{
				this.attrState.TrackViewState();
			}
		}

		// Token: 0x0600416B RID: 16747 RVA: 0x000D61DE File Offset: 0x000D43DE
		public void MergeStyle(Style s)
		{
			if (s != null && !s.IsEmpty)
			{
				this.ControlStyle.MergeWith(s);
			}
		}

		// Token: 0x0600416C RID: 16748 RVA: 0x000D61F7 File Offset: 0x000D43F7
		protected internal override void Render(HtmlTextWriter writer)
		{
			this.RenderBeginTag(writer);
			this.RenderContents(writer);
			this.RenderEndTag(writer);
		}

		// Token: 0x0600416D RID: 16749 RVA: 0x000D6210 File Offset: 0x000D4410
		public virtual void RenderBeginTag(HtmlTextWriter writer)
		{
			this.AddAttributesToRender(writer);
			HtmlTextWriterTag htmlTextWriterTag = this.TagKey;
			if (htmlTextWriterTag != HtmlTextWriterTag.Unknown)
			{
				writer.RenderBeginTag(htmlTextWriterTag);
				return;
			}
			writer.RenderBeginTag(this.TagName);
		}

		// Token: 0x0600416E RID: 16750 RVA: 0x000D6242 File Offset: 0x000D4442
		public virtual void RenderEndTag(HtmlTextWriter writer)
		{
			writer.RenderEndTag();
		}

		// Token: 0x0600416F RID: 16751 RVA: 0x000D624A File Offset: 0x000D444A
		protected internal virtual void RenderContents(HtmlTextWriter writer)
		{
			base.Render(writer);
		}

		// Token: 0x06004170 RID: 16752 RVA: 0x000D6254 File Offset: 0x000D4454
		protected override object SaveViewState()
		{
			Pair result = null;
			if (this._webControlFlags[2])
			{
				this.ViewState["Enabled"] = !this.flags[524288];
			}
			if (this.ControlStyleCreated)
			{
				this.ControlStyle.SaveViewState();
			}
			object obj = base.SaveViewState();
			object obj2 = null;
			if (this.attrState != null)
			{
				obj2 = this.attrState.SaveViewState();
			}
			if (obj != null || obj2 != null)
			{
				result = new Pair(obj, obj2);
			}
			return result;
		}

		// Token: 0x06004171 RID: 16753 RVA: 0x000D62D9 File Offset: 0x000D44D9
		string IAttributeAccessor.GetAttribute(string name)
		{
			if (this.attrState == null)
			{
				return null;
			}
			return (string)this.attrState[name];
		}

		// Token: 0x06004172 RID: 16754 RVA: 0x000D62F6 File Offset: 0x000D44F6
		void IAttributeAccessor.SetAttribute(string name, string value)
		{
			this.Attributes[name] = value;
		}

		// Token: 0x0400250A RID: 9482
		private static string _disabledCssClass = "aspNetDisabled";

		// Token: 0x0400250B RID: 9483
		private string tagName;

		// Token: 0x0400250C RID: 9484
		private HtmlTextWriterTag tagKey;

		// Token: 0x0400250D RID: 9485
		private AttributeCollection attrColl;

		// Token: 0x0400250E RID: 9486
		private StateBag attrState;

		// Token: 0x0400250F RID: 9487
		private Style controlStyle;

		// Token: 0x04002510 RID: 9488
		private SimpleBitVector32 _webControlFlags;

		// Token: 0x04002511 RID: 9489
		private const int deferStyleLoadViewState = 1;

		// Token: 0x04002512 RID: 9490
		private const int disabledDirty = 2;

		// Token: 0x04002513 RID: 9491
		private const int accessKeySet = 4;

		// Token: 0x04002514 RID: 9492
		private const int toolTipSet = 8;

		// Token: 0x04002515 RID: 9493
		private const int tabIndexSet = 16;
	}
}
