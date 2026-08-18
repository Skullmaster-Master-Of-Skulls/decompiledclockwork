using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x020005BB RID: 1467
	[Designer("System.Web.UI.Design.WebControls.WebParts.WebZoneDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[Bindable(false)]
	public abstract class WebZone : CompositeControl
	{
		// Token: 0x06004A69 RID: 19049 RVA: 0x0008D7C4 File Offset: 0x0008B9C4
		internal WebZone()
		{
		}

		// Token: 0x170015EB RID: 5611
		// (get) Token: 0x06004A6A RID: 19050 RVA: 0x000F7480 File Offset: 0x000F5680
		// (set) Token: 0x06004A6B RID: 19051 RVA: 0x000F74AD File Offset: 0x000F56AD
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[UrlProperty]
		[WebCategory("Appearance")]
		[WebSysDescription("WebControl_BackImageUrl")]
		public virtual string BackImageUrl
		{
			get
			{
				string text = (string)this.ViewState["BackImageUrl"];
				if (text != null)
				{
					return text;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["BackImageUrl"] = value;
			}
		}

		// Token: 0x170015EC RID: 5612
		// (get) Token: 0x06004A6C RID: 19052 RVA: 0x000F74C0 File Offset: 0x000F56C0
		// (set) Token: 0x06004A6D RID: 19053 RVA: 0x000DD332 File Offset: 0x000DB532
		[Localizable(true)]
		[WebSysDefaultValue("")]
		[WebCategory("Behavior")]
		[WebSysDescription("Zone_EmptyZoneText")]
		public virtual string EmptyZoneText
		{
			get
			{
				string text = (string)this.ViewState["EmptyZoneText"];
				if (text != null)
				{
					return text;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["EmptyZoneText"] = value;
			}
		}

		// Token: 0x170015ED RID: 5613
		// (get) Token: 0x06004A6E RID: 19054 RVA: 0x000F74ED File Offset: 0x000F56ED
		[DefaultValue(null)]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebCategory("Styles")]
		[WebSysDescription("Zone_EmptyZoneTextStyle")]
		public Style EmptyZoneTextStyle
		{
			get
			{
				if (this._emptyZoneTextStyle == null)
				{
					this._emptyZoneTextStyle = new Style();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._emptyZoneTextStyle).TrackViewState();
					}
				}
				return this._emptyZoneTextStyle;
			}
		}

		// Token: 0x170015EE RID: 5614
		// (get) Token: 0x06004A6F RID: 19055 RVA: 0x000F751B File Offset: 0x000F571B
		[DefaultValue(null)]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebCategory("Styles")]
		[WebSysDescription("Zone_ErrorStyle")]
		public Style ErrorStyle
		{
			get
			{
				if (this._errorStyle == null)
				{
					this._errorStyle = new ErrorStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._errorStyle).TrackViewState();
					}
				}
				return this._errorStyle;
			}
		}

		// Token: 0x170015EF RID: 5615
		// (get) Token: 0x06004A70 RID: 19056 RVA: 0x000F7549 File Offset: 0x000F5749
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebCategory("Styles")]
		[WebSysDescription("Zone_FooterStyle")]
		public TitleStyle FooterStyle
		{
			get
			{
				if (this._footerStyle == null)
				{
					this._footerStyle = new TitleStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._footerStyle).TrackViewState();
					}
				}
				return this._footerStyle;
			}
		}

		// Token: 0x170015F0 RID: 5616
		// (get) Token: 0x06004A71 RID: 19057 RVA: 0x000097B7 File Offset: 0x000079B7
		protected virtual bool HasFooter
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170015F1 RID: 5617
		// (get) Token: 0x06004A72 RID: 19058 RVA: 0x000097B7 File Offset: 0x000079B7
		protected virtual bool HasHeader
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170015F2 RID: 5618
		// (get) Token: 0x06004A73 RID: 19059 RVA: 0x000F7578 File Offset: 0x000F5778
		// (set) Token: 0x06004A74 RID: 19060 RVA: 0x0009AB69 File Offset: 0x00098D69
		[Localizable(true)]
		[WebSysDefaultValue("")]
		[WebCategory("Appearance")]
		[WebSysDescription("Zone_HeaderText")]
		public virtual string HeaderText
		{
			get
			{
				string text = (string)this.ViewState["HeaderText"];
				if (text != null)
				{
					return text;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["HeaderText"] = value;
			}
		}

		// Token: 0x170015F3 RID: 5619
		// (get) Token: 0x06004A75 RID: 19061 RVA: 0x000F75A5 File Offset: 0x000F57A5
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebCategory("Styles")]
		[WebSysDescription("Zone_HeaderStyle")]
		public TitleStyle HeaderStyle
		{
			get
			{
				if (this._headerStyle == null)
				{
					this._headerStyle = new TitleStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._headerStyle).TrackViewState();
					}
				}
				return this._headerStyle;
			}
		}

		// Token: 0x170015F4 RID: 5620
		// (get) Token: 0x06004A76 RID: 19062 RVA: 0x000F75D4 File Offset: 0x000F57D4
		// (set) Token: 0x06004A77 RID: 19063 RVA: 0x000F7602 File Offset: 0x000F5802
		[DefaultValue(typeof(Unit), "5px")]
		[WebCategory("WebPart")]
		[WebSysDescription("Zone_PartChromePadding")]
		public Unit PartChromePadding
		{
			get
			{
				object obj = this.ViewState["PartChromePadding"];
				if (obj != null)
				{
					return (Unit)obj;
				}
				return Unit.Pixel(5);
			}
			set
			{
				if (value.Value < 0.0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["PartChromePadding"] = value;
			}
		}

		// Token: 0x170015F5 RID: 5621
		// (get) Token: 0x06004A78 RID: 19064 RVA: 0x000F7637 File Offset: 0x000F5837
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebCategory("WebPart")]
		[WebSysDescription("Zone_PartChromeStyle")]
		public Style PartChromeStyle
		{
			get
			{
				if (this._partChromeStyle == null)
				{
					this._partChromeStyle = new Style();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._partChromeStyle).TrackViewState();
					}
				}
				return this._partChromeStyle;
			}
		}

		// Token: 0x170015F6 RID: 5622
		// (get) Token: 0x06004A79 RID: 19065 RVA: 0x000F7668 File Offset: 0x000F5868
		// (set) Token: 0x06004A7A RID: 19066 RVA: 0x000F7691 File Offset: 0x000F5891
		[DefaultValue(PartChromeType.Default)]
		[WebCategory("WebPart")]
		[WebSysDescription("Zone_PartChromeType")]
		public virtual PartChromeType PartChromeType
		{
			get
			{
				object obj = this.ViewState["PartChromeType"];
				if (obj == null)
				{
					return PartChromeType.Default;
				}
				return (PartChromeType)((int)obj);
			}
			set
			{
				if (value < PartChromeType.Default || value > PartChromeType.BorderOnly)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["PartChromeType"] = (int)value;
			}
		}

		// Token: 0x170015F7 RID: 5623
		// (get) Token: 0x06004A7B RID: 19067 RVA: 0x000F76BC File Offset: 0x000F58BC
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebCategory("WebPart")]
		[WebSysDescription("Zone_PartStyle")]
		public TableStyle PartStyle
		{
			get
			{
				if (this._partStyle == null)
				{
					this._partStyle = new TableStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._partStyle).TrackViewState();
					}
				}
				return this._partStyle;
			}
		}

		// Token: 0x170015F8 RID: 5624
		// (get) Token: 0x06004A7C RID: 19068 RVA: 0x000F76EA File Offset: 0x000F58EA
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebCategory("WebPart")]
		[WebSysDescription("Zone_PartTitleStyle")]
		public TitleStyle PartTitleStyle
		{
			get
			{
				if (this._partTitleStyle == null)
				{
					this._partTitleStyle = new TitleStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._partTitleStyle).TrackViewState();
					}
				}
				return this._partTitleStyle;
			}
		}

		// Token: 0x170015F9 RID: 5625
		// (get) Token: 0x06004A7D RID: 19069 RVA: 0x0008BDAD File Offset: 0x00089FAD
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Table;
			}
		}

		// Token: 0x170015FA RID: 5626
		// (get) Token: 0x06004A7E RID: 19070 RVA: 0x000F7718 File Offset: 0x000F5918
		// (set) Token: 0x06004A7F RID: 19071 RVA: 0x000F7741 File Offset: 0x000F5941
		[DefaultValue(2)]
		[WebCategory("Layout")]
		[WebSysDescription("Zone_Padding")]
		public virtual int Padding
		{
			get
			{
				object obj = this.ViewState["Padding"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 2;
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["Padding"] = value;
			}
		}

		// Token: 0x06004A80 RID: 19072 RVA: 0x000F7768 File Offset: 0x000F5968
		internal void RenderBodyTableBeginTag(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
			int padding = this.Padding;
			if (padding >= 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, padding.ToString(CultureInfo.InvariantCulture));
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Border, "0");
			string backImageUrl = this.BackImageUrl;
			if (backImageUrl.Trim().Length > 0)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.BackgroundImage, "url(" + base.ResolveClientUrl(backImageUrl) + ")");
			}
			writer.AddStyleAttribute(HtmlTextWriterStyle.Width, "100%");
			writer.AddStyleAttribute(HtmlTextWriterStyle.Height, "100%");
			writer.RenderBeginTag(HtmlTextWriterTag.Table);
		}

		// Token: 0x06004A81 RID: 19073 RVA: 0x000F77FF File Offset: 0x000F59FF
		internal static void RenderBodyTableEndTag(HtmlTextWriter writer)
		{
			writer.RenderEndTag();
		}

		// Token: 0x06004A82 RID: 19074 RVA: 0x000F7808 File Offset: 0x000F5A08
		internal void RenderDesignerRegionBeginTag(HtmlTextWriter writer, Orientation orientation)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Valign, "top");
			writer.RenderBeginTag(HtmlTextWriterTag.Tr);
			if (orientation == Orientation.Horizontal)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.WhiteSpace, "nowrap");
			}
			writer.AddAttribute(HtmlTextWriterAttribute.DesignerRegion, "0");
			writer.RenderBeginTag(HtmlTextWriterTag.Td);
			writer.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
			writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, this.Padding.ToString(CultureInfo.InvariantCulture));
			writer.AddAttribute(HtmlTextWriterAttribute.Border, "0");
			if (orientation == Orientation.Vertical)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Width, "100%");
			}
			else
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Height, "100%");
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Table);
		}

		// Token: 0x06004A83 RID: 19075 RVA: 0x000F78A9 File Offset: 0x000F5AA9
		internal static void RenderDesignerRegionEndTag(HtmlTextWriter writer)
		{
			writer.RenderEndTag();
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x170015FB RID: 5627
		// (get) Token: 0x06004A84 RID: 19076 RVA: 0x000F78C0 File Offset: 0x000F5AC0
		protected internal bool RenderClientScript
		{
			get
			{
				bool result = false;
				if (base.DesignMode)
				{
					result = true;
				}
				else if (this.WebPartManager != null)
				{
					result = this.WebPartManager.RenderClientScript;
				}
				return result;
			}
		}

		// Token: 0x170015FC RID: 5628
		// (get) Token: 0x06004A85 RID: 19077 RVA: 0x000F78F0 File Offset: 0x000F5AF0
		// (set) Token: 0x06004A86 RID: 19078 RVA: 0x000F7919 File Offset: 0x000F5B19
		[DefaultValue(ButtonType.Button)]
		[WebCategory("Appearance")]
		[WebSysDescription("Zone_VerbButtonType")]
		public virtual ButtonType VerbButtonType
		{
			get
			{
				object obj = this.ViewState["VerbButtonType"];
				if (obj != null)
				{
					return (ButtonType)obj;
				}
				return ButtonType.Button;
			}
			set
			{
				if (value < ButtonType.Button || value > ButtonType.Link)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["VerbButtonType"] = value;
			}
		}

		// Token: 0x170015FD RID: 5629
		// (get) Token: 0x06004A87 RID: 19079 RVA: 0x000F7944 File Offset: 0x000F5B44
		[DefaultValue(null)]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebCategory("Styles")]
		[WebSysDescription("Zone_VerbStyle")]
		public Style VerbStyle
		{
			get
			{
				if (this._verbStyle == null)
				{
					this._verbStyle = new Style();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._verbStyle).TrackViewState();
					}
				}
				return this._verbStyle;
			}
		}

		// Token: 0x06004A88 RID: 19080 RVA: 0x000F7974 File Offset: 0x000F5B74
		public virtual PartChromeType GetEffectiveChromeType(Part part)
		{
			if (part == null)
			{
				throw new ArgumentNullException("part");
			}
			PartChromeType partChromeType = part.ChromeType;
			if (partChromeType == PartChromeType.Default)
			{
				PartChromeType partChromeType2 = this.PartChromeType;
				if (partChromeType2 == PartChromeType.Default)
				{
					partChromeType = PartChromeType.TitleAndBorder;
				}
				else
				{
					partChromeType = partChromeType2;
				}
			}
			return partChromeType;
		}

		// Token: 0x06004A89 RID: 19081 RVA: 0x000F79AC File Offset: 0x000F5BAC
		protected override void LoadViewState(object savedState)
		{
			if (savedState == null)
			{
				base.LoadViewState(null);
				return;
			}
			object[] array = (object[])savedState;
			if (array.Length != 9)
			{
				throw new ArgumentException(SR.GetString("ViewState_InvalidViewState"));
			}
			base.LoadViewState(array[0]);
			if (array[1] != null)
			{
				((IStateManager)this.EmptyZoneTextStyle).LoadViewState(array[1]);
			}
			if (array[2] != null)
			{
				((IStateManager)this.FooterStyle).LoadViewState(array[2]);
			}
			if (array[3] != null)
			{
				((IStateManager)this.PartStyle).LoadViewState(array[3]);
			}
			if (array[4] != null)
			{
				((IStateManager)this.PartChromeStyle).LoadViewState(array[4]);
			}
			if (array[5] != null)
			{
				((IStateManager)this.PartTitleStyle).LoadViewState(array[5]);
			}
			if (array[6] != null)
			{
				((IStateManager)this.HeaderStyle).LoadViewState(array[6]);
			}
			if (array[7] != null)
			{
				((IStateManager)this.VerbStyle).LoadViewState(array[7]);
			}
			if (array[8] != null)
			{
				((IStateManager)this.ErrorStyle).LoadViewState(array[8]);
			}
		}

		// Token: 0x06004A8A RID: 19082 RVA: 0x000F7A84 File Offset: 0x000F5C84
		protected internal override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			Page page = this.Page;
			if (page != null)
			{
				if (page.ControlState >= ControlState.Initialized && !base.DesignMode)
				{
					throw new InvalidOperationException(SR.GetString("Zone_AddedTooLate"));
				}
				if (!base.DesignMode)
				{
					this._webPartManager = WebPartManager.GetCurrentWebPartManager(page);
					if (this._webPartManager == null)
					{
						throw new InvalidOperationException(SR.GetString("WebPartManagerRequired"));
					}
					this._webPartManager.RegisterZone(this);
				}
			}
		}

		// Token: 0x06004A8B RID: 19083 RVA: 0x000F7AFC File Offset: 0x000F5CFC
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			Control parent = this.Parent;
			if (parent != null && (parent is WebZone || parent is Part))
			{
				throw new InvalidOperationException(SR.GetString("Zone_InvalidParent"));
			}
		}

		// Token: 0x06004A8C RID: 19084 RVA: 0x000F7B3C File Offset: 0x000F5D3C
		public override void RenderBeginTag(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
			writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0");
			writer.AddAttribute(HtmlTextWriterAttribute.Border, "0");
			if (!base.DesignMode && this.Page != null && this.Page.Request.Browser.Type == "IE5" && this.Page.Request.Browser.Platform == "MacPPC" && (!base.ControlStyleCreated || base.ControlStyle.Height == Unit.Empty))
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Height, "1px");
			}
			base.RenderBeginTag(writer);
		}

		// Token: 0x06004A8D RID: 19085 RVA: 0x000F7BF4 File Offset: 0x000F5DF4
		protected internal override void RenderContents(HtmlTextWriter writer)
		{
			if (this.HasHeader)
			{
				writer.RenderBeginTag(HtmlTextWriterTag.Tr);
				TitleStyle headerStyle = this.HeaderStyle;
				if (!headerStyle.IsEmpty)
				{
					headerStyle.AddAttributesToRender(writer, this);
				}
				writer.RenderBeginTag(HtmlTextWriterTag.Td);
				this.RenderHeader(writer);
				writer.RenderEndTag();
				writer.RenderEndTag();
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Tr);
			writer.AddStyleAttribute(HtmlTextWriterStyle.Height, "100%");
			writer.RenderBeginTag(HtmlTextWriterTag.Td);
			this.RenderBody(writer);
			writer.RenderEndTag();
			writer.RenderEndTag();
			if (this.HasFooter)
			{
				writer.RenderBeginTag(HtmlTextWriterTag.Tr);
				TitleStyle footerStyle = this.FooterStyle;
				if (!footerStyle.IsEmpty)
				{
					footerStyle.AddAttributesToRender(writer, this);
				}
				writer.RenderBeginTag(HtmlTextWriterTag.Td);
				this.RenderFooter(writer);
				writer.RenderEndTag();
				writer.RenderEndTag();
			}
		}

		// Token: 0x06004A8E RID: 19086 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void RenderHeader(HtmlTextWriter writer)
		{
		}

		// Token: 0x06004A8F RID: 19087 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void RenderBody(HtmlTextWriter writer)
		{
		}

		// Token: 0x06004A90 RID: 19088 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void RenderFooter(HtmlTextWriter writer)
		{
		}

		// Token: 0x06004A91 RID: 19089 RVA: 0x000F7CB8 File Offset: 0x000F5EB8
		protected override object SaveViewState()
		{
			object[] array = new object[]
			{
				base.SaveViewState(),
				(this._emptyZoneTextStyle != null) ? ((IStateManager)this._emptyZoneTextStyle).SaveViewState() : null,
				(this._footerStyle != null) ? ((IStateManager)this._footerStyle).SaveViewState() : null,
				(this._partStyle != null) ? ((IStateManager)this._partStyle).SaveViewState() : null,
				(this._partChromeStyle != null) ? ((IStateManager)this._partChromeStyle).SaveViewState() : null,
				(this._partTitleStyle != null) ? ((IStateManager)this._partTitleStyle).SaveViewState() : null,
				(this._headerStyle != null) ? ((IStateManager)this._headerStyle).SaveViewState() : null,
				(this._verbStyle != null) ? ((IStateManager)this._verbStyle).SaveViewState() : null,
				(this._errorStyle != null) ? ((IStateManager)this._errorStyle).SaveViewState() : null
			};
			for (int i = 0; i < 9; i++)
			{
				if (array[i] != null)
				{
					return array;
				}
			}
			return null;
		}

		// Token: 0x06004A92 RID: 19090 RVA: 0x000F7DB4 File Offset: 0x000F5FB4
		protected override void TrackViewState()
		{
			base.TrackViewState();
			if (this._emptyZoneTextStyle != null)
			{
				((IStateManager)this._emptyZoneTextStyle).TrackViewState();
			}
			if (this._footerStyle != null)
			{
				((IStateManager)this._footerStyle).TrackViewState();
			}
			if (this._partStyle != null)
			{
				((IStateManager)this._partStyle).TrackViewState();
			}
			if (this._partChromeStyle != null)
			{
				((IStateManager)this._partChromeStyle).TrackViewState();
			}
			if (this._partTitleStyle != null)
			{
				((IStateManager)this._partTitleStyle).TrackViewState();
			}
			if (this._headerStyle != null)
			{
				((IStateManager)this._headerStyle).TrackViewState();
			}
			if (this._verbStyle != null)
			{
				((IStateManager)this._verbStyle).TrackViewState();
			}
			if (this._errorStyle != null)
			{
				((IStateManager)this._errorStyle).TrackViewState();
			}
		}

		// Token: 0x170015FE RID: 5630
		// (get) Token: 0x06004A93 RID: 19091 RVA: 0x000F7E5F File Offset: 0x000F605F
		protected WebPartManager WebPartManager
		{
			get
			{
				return this._webPartManager;
			}
		}

		// Token: 0x04002804 RID: 10244
		private WebPartManager _webPartManager;

		// Token: 0x04002805 RID: 10245
		private const int baseIndex = 0;

		// Token: 0x04002806 RID: 10246
		private const int emptyZoneTextStyleIndex = 1;

		// Token: 0x04002807 RID: 10247
		private const int footerStyleIndex = 2;

		// Token: 0x04002808 RID: 10248
		private const int partStyleIndex = 3;

		// Token: 0x04002809 RID: 10249
		private const int partChromeStyleIndex = 4;

		// Token: 0x0400280A RID: 10250
		private const int partTitleStyleIndex = 5;

		// Token: 0x0400280B RID: 10251
		private const int headerStyleIndex = 6;

		// Token: 0x0400280C RID: 10252
		private const int verbStyleIndex = 7;

		// Token: 0x0400280D RID: 10253
		private const int errorStyleIndex = 8;

		// Token: 0x0400280E RID: 10254
		private const int viewStateArrayLength = 9;

		// Token: 0x0400280F RID: 10255
		private Style _emptyZoneTextStyle;

		// Token: 0x04002810 RID: 10256
		private TitleStyle _footerStyle;

		// Token: 0x04002811 RID: 10257
		private TableStyle _partStyle;

		// Token: 0x04002812 RID: 10258
		private Style _partChromeStyle;

		// Token: 0x04002813 RID: 10259
		private TitleStyle _partTitleStyle;

		// Token: 0x04002814 RID: 10260
		private TitleStyle _headerStyle;

		// Token: 0x04002815 RID: 10261
		private Style _verbStyle;

		// Token: 0x04002816 RID: 10262
		private Style _errorStyle;
	}
}
