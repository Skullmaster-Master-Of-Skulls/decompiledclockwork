using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x020008FE RID: 2302
	[EditorBrowsable(EditorBrowsableState.Never)]
	[ToolboxBitmap(typeof(RadBaseTile), "Telerik.Web.UI.TileList.png")]
	[Browsable(false)]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Lightweight, typeof(RadBaseTile))]
	[RequiredScript(typeof(RadBaseTileScripts))]
	[TelerikToolboxCategory("Navigation")]
	[EmbeddedSkin("Tile", "Default")]
	[LightweightRendering]
	[EmbeddedSkin("Tile")]
	[ClientScriptResource("Telerik.Web.UI.RadBaseTile", "Telerik.Web.UI.Tile.RadTileScripts.js")]
	public abstract class RadBaseTile : RadWebControl, IMarkableStateManager, IStateManager, IPostBackEventHandler, INamingContainer
	{
		// Token: 0x17001CB7 RID: 7351
		// (get) Token: 0x060056D1 RID: 22225 RVA: 0x00109DC0 File Offset: 0x00107FC0
		// (set) Token: 0x060056D2 RID: 22226 RVA: 0x00109DC8 File Offset: 0x00107FC8
		[Editor(typeof(ColorEditor), typeof(UITypeEditor))]
		[NotifyParentProperty(true)]
		public override Color BackColor
		{
			get
			{
				return base.BackColor;
			}
			set
			{
				base.BackColor = value;
			}
		}

		// Token: 0x17001CB8 RID: 7352
		// (get) Token: 0x060056D3 RID: 22227 RVA: 0x00109DD1 File Offset: 0x00107FD1
		// (set) Token: 0x060056D4 RID: 22228 RVA: 0x00109DD9 File Offset: 0x00107FD9
		[NotifyParentProperty(true)]
		[Editor(typeof(ColorEditor), typeof(UITypeEditor))]
		public override Color BorderColor
		{
			get
			{
				return base.BorderColor;
			}
			set
			{
				base.BorderColor = value;
			}
		}

		// Token: 0x17001CB9 RID: 7353
		// (get) Token: 0x060056D5 RID: 22229 RVA: 0x00109DE2 File Offset: 0x00107FE2
		// (set) Token: 0x060056D6 RID: 22230 RVA: 0x00109DEA File Offset: 0x00107FEA
		[Editor(typeof(ColorEditor), typeof(UITypeEditor))]
		[NotifyParentProperty(true)]
		public override Color ForeColor
		{
			get
			{
				return base.ForeColor;
			}
			set
			{
				base.ForeColor = value;
			}
		}

		// Token: 0x17001CBA RID: 7354
		// (get) Token: 0x060056D7 RID: 22231 RVA: 0x00109DF4 File Offset: 0x00107FF4
		protected override string CssClassFormatString
		{
			get
			{
				string text = this.Selected ? " rtileSelected" : "";
				return string.Format("RadTile RadTile_{0} {1} {2} {3}", new object[]
				{
					base.RuntimeSkin,
					this.ShapeCssClass,
					this.TileTypeCssClass,
					text
				}).Trim();
			}
		}

		// Token: 0x17001CBB RID: 7355
		// (get) Token: 0x060056D8 RID: 22232 RVA: 0x00109E4C File Offset: 0x0010804C
		protected virtual string TileTypeCssClass
		{
			get
			{
				return string.Format("{0}", this.TileType);
			}
		}

		// Token: 0x17001CBC RID: 7356
		// (get) Token: 0x060056D9 RID: 22233
		internal abstract string TileType { get; }

		// Token: 0x17001CBD RID: 7357
		// (get) Token: 0x060056DA RID: 22234 RVA: 0x00109E5E File Offset: 0x0010805E
		private string ShapeCssClass
		{
			get
			{
				if (this.Shape != TileShape.Square)
				{
					return "rtileWide";
				}
				return "rtileSquare";
			}
		}

		// Token: 0x17001CBE RID: 7358
		// (get) Token: 0x060056DB RID: 22235 RVA: 0x00109E73 File Offset: 0x00108073
		// (set) Token: 0x060056DC RID: 22236 RVA: 0x00109E85 File Offset: 0x00108085
		[ClientControlProperty]
		[UrlProperty]
		[Description("Gets or sets the URL of the page to navigate to, without posting the page back to the server.")]
		[DefaultValue("")]
		public string NavigateUrl
		{
			get
			{
				return base.GetViewStateValue<string>("NavigateUrl", string.Empty);
			}
			set
			{
				this.ViewState["NavigateUrl"] = value;
			}
		}

		// Token: 0x17001CBF RID: 7359
		// (get) Token: 0x060056DD RID: 22237 RVA: 0x00109E98 File Offset: 0x00108098
		// (set) Token: 0x060056DE RID: 22238 RVA: 0x00109EB8 File Offset: 0x001080B8
		[TypeConverter(typeof(TargetConverter))]
		[Description("Gets or sets the target window or frame in which to display the Web page content linked to when the NavigateUrl property when the control is clicked.")]
		[ClientControlProperty]
		[ClientPropertyName("target")]
		[DefaultValue("")]
		public string Target
		{
			get
			{
				return ((string)this.ViewState["Target"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["Target"] = value;
			}
		}

		// Token: 0x17001CC0 RID: 7360
		// (get) Token: 0x060056DF RID: 22239 RVA: 0x00109ECB File Offset: 0x001080CB
		// (set) Token: 0x060056E0 RID: 22240 RVA: 0x00109ED9 File Offset: 0x001080D9
		[Category("Behavior")]
		[Description("Gets or sets the shape of the tile.")]
		[DefaultValue(TileShape.Square)]
		[ClientControlProperty]
		public TileShape Shape
		{
			get
			{
				return base.GetViewStateValue<TileShape>("Shape", TileShape.Square);
			}
			set
			{
				this.ViewState["Shape"] = value;
			}
		}

		// Token: 0x17001CC1 RID: 7361
		// (get) Token: 0x060056E1 RID: 22241 RVA: 0x00109EF1 File Offset: 0x001080F1
		// (set) Token: 0x060056E2 RID: 22242 RVA: 0x00109F09 File Offset: 0x00108109
		[Description("Gets or sets the selected state of the tile.")]
		[DefaultValue(false)]
		[Category("Behavior")]
		[ClientControlProperty]
		public bool Selected
		{
			get
			{
				return this.EnableSelection && base.GetViewStateValue<bool>("Selected", false);
			}
			set
			{
				this.ViewState["Selected"] = value;
			}
		}

		// Token: 0x17001CC2 RID: 7362
		// (get) Token: 0x060056E3 RID: 22243 RVA: 0x00109F21 File Offset: 0x00108121
		// (set) Token: 0x060056E4 RID: 22244 RVA: 0x00109F2F File Offset: 0x0010812F
		[ClientControlProperty]
		[DefaultValue(false)]
		[Description("Gets or sets value determinig if selection of the tile is enabled.")]
		[Category("Behavior")]
		public bool EnableSelection
		{
			get
			{
				return base.GetViewStateValue<bool>("EnableSelection", false);
			}
			set
			{
				if (!value)
				{
					this.Selected = false;
				}
				this.ViewState["EnableSelection"] = value;
			}
		}

		// Token: 0x17001CC3 RID: 7363
		// (get) Token: 0x060056E5 RID: 22245 RVA: 0x00109F51 File Offset: 0x00108151
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[NotifyParentProperty(true)]
		[Browsable(true)]
		[Category("Behavior")]
		[Description("Defines the badge rendered in the bottom right corner of the tile.")]
		public TileBadge Badge
		{
			get
			{
				if (this._badge == null)
				{
					this._badge = new TileBadge();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._badge).TrackViewState();
					}
				}
				return this._badge;
			}
		}

		// Token: 0x17001CC4 RID: 7364
		// (get) Token: 0x060056E6 RID: 22246 RVA: 0x00109F7F File Offset: 0x0010817F
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[NotifyParentProperty(true)]
		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Category("Behavior")]
		[Description("Defines the title rendered in the bottom left corner of the tile.")]
		[DefaultValue(null)]
		public TileTitle Title
		{
			get
			{
				if (this._title == null)
				{
					this._title = new TileTitle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._title).TrackViewState();
					}
				}
				return this._title;
			}
		}

		// Token: 0x17001CC5 RID: 7365
		// (get) Token: 0x060056E7 RID: 22247 RVA: 0x00109FAD File Offset: 0x001081AD
		[Description("Defines the peek template configuration settings.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[DefaultValue(null)]
		[Browsable(true)]
		[NotifyParentProperty(true)]
		[Category("Behavior")]
		public TilePeekTemplateSettings PeekTemplateSettings
		{
			get
			{
				if (this._peekTemplateSettings == null)
				{
					this._peekTemplateSettings = new TilePeekTemplateSettings();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._peekTemplateSettings).TrackViewState();
					}
				}
				return this._peekTemplateSettings;
			}
		}

		// Token: 0x17001CC6 RID: 7366
		// (get) Token: 0x060056E8 RID: 22248 RVA: 0x00109FDB File Offset: 0x001081DB
		// (set) Token: 0x060056E9 RID: 22249 RVA: 0x00109FFB File Offset: 0x001081FB
		[ClientControlProperty]
		[DefaultValue("")]
		[Description("Gets or sets the Name proerty of a tile.")]
		[Category("Behavior")]
		public string Name
		{
			get
			{
				return ((string)this.ViewState["Name"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["Name"] = value;
			}
		}

		// Token: 0x17001CC7 RID: 7367
		// (get) Token: 0x060056EA RID: 22250 RVA: 0x0010A00E File Offset: 0x0010820E
		// (set) Token: 0x060056EB RID: 22251 RVA: 0x0010A02F File Offset: 0x0010822F
		[DefaultValue(false)]
		[ClientControlProperty]
		[Description("Whether to postback after the selection changes")]
		[Bindable(false)]
		[Category("Behavior")]
		public bool AutoPostBack
		{
			get
			{
				return (bool)(this.ViewState["AutoPostBack"] ?? false);
			}
			set
			{
				this.ViewState["AutoPostBack"] = value;
			}
		}

		// Token: 0x17001CC8 RID: 7368
		// (get) Token: 0x060056EC RID: 22252 RVA: 0x0010A047 File Offset: 0x00108247
		// (set) Token: 0x060056ED RID: 22253 RVA: 0x0010A067 File Offset: 0x00108267
		[DefaultValue("")]
		[UrlProperty("*.aspx")]
		[Themeable(false)]
		[Category("Behavior")]
		public virtual string PostBackUrl
		{
			get
			{
				return (string)(this.ViewState["PostBackUrl"] ?? "");
			}
			set
			{
				this.ViewState["PostBackUrl"] = value;
			}
		}

		// Token: 0x17001CC9 RID: 7369
		// (get) Token: 0x060056EE RID: 22254 RVA: 0x0010A07A File Offset: 0x0010827A
		[Browsable(false)]
		public Panel PeekContentContainer
		{
			get
			{
				this.EnsureChildControls();
				return this._peekContentContainer;
			}
		}

		// Token: 0x17001CCA RID: 7370
		// (get) Token: 0x060056EF RID: 22255 RVA: 0x0010A088 File Offset: 0x00108288
		// (set) Token: 0x060056F0 RID: 22256 RVA: 0x0010A090 File Offset: 0x00108290
		[Bindable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[TemplateInstance(TemplateInstance.Single)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(false)]
		[TemplateContainer(typeof(RadBaseTile))]
		public ITemplate PeekTemplate
		{
			get
			{
				return this._peekTemplate;
			}
			set
			{
				this._peekTemplate = value;
				this.InstantiatePeekTemplate();
			}
		}

		// Token: 0x17001CCB RID: 7371
		// (get) Token: 0x060056F1 RID: 22257 RVA: 0x0010A09F File Offset: 0x0010829F
		// (set) Token: 0x060056F2 RID: 22258 RVA: 0x0010A0C0 File Offset: 0x001082C0
		internal bool IsDeclarative
		{
			get
			{
				return (bool)(this.ViewState["IsDeclarative"] ?? true);
			}
			set
			{
				this.ViewState["IsDeclarative"] = value;
			}
		}

		// Token: 0x17001CCC RID: 7372
		// (get) Token: 0x060056F3 RID: 22259 RVA: 0x0010A0D8 File Offset: 0x001082D8
		// (set) Token: 0x060056F4 RID: 22260 RVA: 0x0010A0E0 File Offset: 0x001082E0
		[Browsable(false)]
		public object DataItem
		{
			get
			{
				return this._dataItem;
			}
			set
			{
				this._dataItem = value;
			}
		}

		// Token: 0x17001CCD RID: 7373
		// (get) Token: 0x060056F5 RID: 22261 RVA: 0x0010A0E9 File Offset: 0x001082E9
		// (set) Token: 0x060056F6 RID: 22262 RVA: 0x0010A109 File Offset: 0x00108309
		[DefaultValue("")]
		[ClientControlEvent]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientPropertyName("load")]
		[Category("Client-side events")]
		public virtual string OnClientLoad
		{
			get
			{
				return ((string)this.ViewState["OnClientLoad"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientLoad"] = value;
			}
		}

		// Token: 0x17001CCE RID: 7374
		// (get) Token: 0x060056F7 RID: 22263 RVA: 0x0010A11C File Offset: 0x0010831C
		// (set) Token: 0x060056F8 RID: 22264 RVA: 0x0010A13C File Offset: 0x0010833C
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("selecting")]
		[Description("The JavaScript function executed before a tile is selected")]
		[Category("Client-side events")]
		[DefaultValue("")]
		public string OnClientSelecting
		{
			get
			{
				return (string)(this.ViewState["OnClientSelecting"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientSelecting"] = value;
			}
		}

		// Token: 0x17001CCF RID: 7375
		// (get) Token: 0x060056F9 RID: 22265 RVA: 0x0010A14F File Offset: 0x0010834F
		// (set) Token: 0x060056FA RID: 22266 RVA: 0x0010A16F File Offset: 0x0010836F
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("The JavaScript function executed after a tile is selected")]
		[ClientControlEvent]
		[Category("Client-side events")]
		[DefaultValue("")]
		[ClientPropertyName("selected")]
		public string OnClientSelected
		{
			get
			{
				return (string)(this.ViewState["OnClientSelected"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientSelected"] = value;
			}
		}

		// Token: 0x17001CD0 RID: 7376
		// (get) Token: 0x060056FB RID: 22267 RVA: 0x0010A182 File Offset: 0x00108382
		// (set) Token: 0x060056FC RID: 22268 RVA: 0x0010A1A2 File Offset: 0x001083A2
		[ClientControlEvent]
		[ClientPropertyName("clicking")]
		[Description("Gets or sets the name of the JavaScript function that will be called when a tile in a RadTileList is clicked. The event is cancelable.")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Client-side events")]
		public string OnClientClicking
		{
			get
			{
				return ((string)this.ViewState["OnClientClicking"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientClicking"] = value;
			}
		}

		// Token: 0x17001CD1 RID: 7377
		// (get) Token: 0x060056FD RID: 22269 RVA: 0x0010A1B5 File Offset: 0x001083B5
		// (set) Token: 0x060056FE RID: 22270 RVA: 0x0010A1D5 File Offset: 0x001083D5
		[DefaultValue("")]
		[Category("Client-side events")]
		[ClientPropertyName("clicked")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[Description("Gets or sets the name of the JavaScript function that will be called when a tile in a RadTileList is clicked, after the OnClientClicking event.")]
		public string OnClientClicked
		{
			get
			{
				return ((string)this.ViewState["OnClientClicked"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientClicked"] = value;
			}
		}

		// Token: 0x140000CE RID: 206
		// (add) Token: 0x060056FF RID: 22271 RVA: 0x0010A1E8 File Offset: 0x001083E8
		// (remove) Token: 0x06005700 RID: 22272 RVA: 0x0010A1FB File Offset: 0x001083FB
		public event TileSelectionStateChangedEventHandler SelectionStateChanged
		{
			add
			{
				base.Events.AddHandler(RadBaseTile.selectionStateChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadBaseTile.selectionStateChangedEvent, value);
			}
		}

		// Token: 0x06005701 RID: 22273 RVA: 0x0010A210 File Offset: 0x00108410
		internal virtual void OnSelectionStateChanged(EventArgs e)
		{
			TileSelectionStateChangedEventHandler tileSelectionStateChangedEventHandler = (TileSelectionStateChangedEventHandler)base.Events[RadBaseTile.selectionStateChangedEvent];
			if (tileSelectionStateChangedEventHandler != null)
			{
				tileSelectionStateChangedEventHandler(this, e);
			}
		}

		// Token: 0x140000CF RID: 207
		// (add) Token: 0x06005702 RID: 22274 RVA: 0x0010A23E File Offset: 0x0010843E
		// (remove) Token: 0x06005703 RID: 22275 RVA: 0x0010A251 File Offset: 0x00108451
		[Description("Fired when the RadButton control is clicked.")]
		[Category("Action")]
		public event EventHandler Click
		{
			add
			{
				base.Events.AddHandler(RadBaseTile.clickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadBaseTile.clickEvent, value);
			}
		}

		// Token: 0x06005704 RID: 22276 RVA: 0x0010A264 File Offset: 0x00108464
		protected virtual void OnClick(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[RadBaseTile.clickEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06005705 RID: 22277 RVA: 0x0010A292 File Offset: 0x00108492
		public RadBaseTile()
		{
		}

		// Token: 0x17001CD2 RID: 7378
		// (get) Token: 0x06005706 RID: 22278 RVA: 0x0010A2B6 File Offset: 0x001084B6
		protected internal override bool SupportsRenderingMode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005707 RID: 22279 RVA: 0x0010A2BC File Offset: 0x001084BC
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			List<JavaScriptConverter> list = new List<JavaScriptConverter>();
			list.Add(new TileListTypesConverter());
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			javaScriptSerializer.RegisterConverters(list);
			descriptor.AddScriptProperty("titleData", javaScriptSerializer.Serialize(this.Title));
			descriptor.AddScriptProperty("badgeData", javaScriptSerializer.Serialize(this.Badge));
			descriptor.AddScriptProperty("peekTemplateSettingsData", javaScriptSerializer.Serialize(this.PeekTemplateSettings));
			descriptor.AddScriptProperty("_postBackReference", "\"" + this.GetPostbackEventReference() + "\"");
			if (this.OriginalGroupIndex >= 0)
			{
				base.DescribeProperty<int>(descriptor, "originalGroupIndex", this.OriginalGroupIndex, -1);
			}
			if (this.OriginalGroupId >= 0)
			{
				base.DescribeProperty<int>(descriptor, "originalGroupId", this.OriginalGroupId, -1);
			}
			if (this.OriginalAllTilesIndex >= 0)
			{
				base.DescribeProperty<int>(descriptor, "originalAllTilesIndex", this.OriginalAllTilesIndex, -1);
			}
			if (this.GroupIndex >= 0)
			{
				base.DescribeProperty<int>(descriptor, "groupIndex", this.GroupIndex, -1);
			}
		}

		// Token: 0x06005708 RID: 22280 RVA: 0x0010A3C3 File Offset: 0x001085C3
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (!this.PeekContentContainerHasControls())
			{
				this.Controls.Remove(this._peekContentContainer);
			}
		}

		// Token: 0x06005709 RID: 22281 RVA: 0x0010A3E5 File Offset: 0x001085E5
		public bool PeekContentContainerHasControls()
		{
			return this._peekContentContainer != null && this._peekContentContainer.Controls.Count > 0;
		}

		// Token: 0x0600570A RID: 22282 RVA: 0x0010A404 File Offset: 0x00108604
		protected virtual void InstantiatePeekTemplate()
		{
			if (!this._peekTemplateInstantiating && !this._peekTemplateInstantiated)
			{
				this._peekTemplateInstantiating = true;
				if (this._peekTemplate != null)
				{
					this._peekTemplate.InstantiateIn(this.PeekContentContainer);
					this._peekTemplateInstantiated = true;
				}
				this._peekTemplateInstantiating = false;
			}
		}

		// Token: 0x17001CD3 RID: 7379
		// (get) Token: 0x0600570B RID: 22283 RVA: 0x0010A444 File Offset: 0x00108644
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x0600570C RID: 22284 RVA: 0x0010A448 File Offset: 0x00108648
		protected override void RenderContents(HtmlTextWriter writer)
		{
			base.RenderContents(writer);
			this.RenderTitle(writer);
		}

		// Token: 0x0600570D RID: 22285 RVA: 0x0010A458 File Offset: 0x00108658
		protected virtual void RenderTitle(HtmlTextWriter writer)
		{
			this.RenderSelectedIcon(writer);
			this.RenderTileContent(writer);
			this.RenderTileTitle(writer);
			this.RenderTileBadge(writer);
		}

		// Token: 0x0600570E RID: 22286 RVA: 0x0010A476 File Offset: 0x00108676
		protected void RenderSelectedIcon(HtmlTextWriter writer)
		{
			if (this.Selected)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rtileSelectedIcon");
				writer.RenderBeginTag(HtmlTextWriterTag.Div);
				writer.RenderEndTag();
			}
		}

		// Token: 0x0600570F RID: 22287 RVA: 0x0010A49B File Offset: 0x0010869B
		protected virtual void RenderTileContent(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rtileContent");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			this.RenderTileBody(writer);
			writer.RenderEndTag();
		}

		// Token: 0x06005710 RID: 22288
		protected abstract void RenderTileBody(HtmlTextWriter writer);

		// Token: 0x06005711 RID: 22289 RVA: 0x0010A4BF File Offset: 0x001086BF
		protected virtual void RenderTileTitle(HtmlTextWriter writer)
		{
			if (string.IsNullOrEmpty(this.Title.ImageUrl))
			{
				this.RenderTitleText(writer);
				return;
			}
			this.RenderTitleImage(writer);
		}

		// Token: 0x06005712 RID: 22290 RVA: 0x0010A4E4 File Offset: 0x001086E4
		private void RenderTitleText(HtmlTextWriter writer)
		{
			if (!string.IsNullOrEmpty(this.Title.Text))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rtileTitle");
				Color foreColor = this.ForeColor;
				if (this.ForeColor != Color.Empty)
				{
					string value = (this.ForeColor.ToKnownColor() == (KnownColor)0) ? string.Format("#{0:x2}{1:x2}{2:x2}", this.ForeColor.R, this.ForeColor.G, this.ForeColor.B) : this.ForeColor.Name;
					writer.AddStyleAttribute(HtmlTextWriterStyle.Color, value);
				}
				writer.RenderBeginTag(HtmlTextWriterTag.H6);
				writer.WriteEncodedText(this.Title.Text);
				writer.RenderEndTag();
			}
		}

		// Token: 0x06005713 RID: 22291 RVA: 0x0010A5BC File Offset: 0x001087BC
		private void RenderTitleImage(HtmlTextWriter writer)
		{
			if (!string.IsNullOrEmpty(this.Title.ImageUrl))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rtileTitle");
				writer.AddAttribute(HtmlTextWriterAttribute.Src, base.ResolveClientUrl(this.Title.ImageUrl));
				writer.AddAttribute(HtmlTextWriterAttribute.Alt, this.Title.Text);
				writer.RenderBeginTag(HtmlTextWriterTag.Img);
				writer.RenderEndTag();
			}
		}

		// Token: 0x06005714 RID: 22292 RVA: 0x0010A624 File Offset: 0x00108824
		protected virtual void RenderTileBadge(HtmlTextWriter writer)
		{
			if (this.Badge.Value != null)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rtileBadge");
				writer.RenderBeginTag(HtmlTextWriterTag.Div);
				writer.Write(string.Format("{0}", this.Badge.Value));
				writer.RenderEndTag();
				return;
			}
			if (this.Badge.PredefinedType != TileBadgeType.None)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, string.Format("rtileBadge rtileBadgeIcon rtile{0}Badge", this.Badge.PredefinedType.ToString()));
				writer.RenderBeginTag(HtmlTextWriterTag.Div);
				writer.RenderEndTag();
				return;
			}
			string badgeImageUrl = this.Badge.GetBadgeImageUrl();
			if (!string.IsNullOrEmpty(badgeImageUrl))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rtileBadge");
				writer.AddAttribute(HtmlTextWriterAttribute.Src, base.ResolveClientUrl(badgeImageUrl));
				writer.AddAttribute(HtmlTextWriterAttribute.Alt, "");
				writer.RenderBeginTag(HtmlTextWriterTag.Img);
				writer.RenderEndTag();
			}
		}

		// Token: 0x06005715 RID: 22293 RVA: 0x0010A70F File Offset: 0x0010890F
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			this.CreatePeekContentContainer();
		}

		// Token: 0x06005716 RID: 22294 RVA: 0x0010A720 File Offset: 0x00108920
		private void CreatePeekContentContainer()
		{
			this._peekContentContainer = new Panel();
			this._peekContentContainer.ID = "P";
			this._peekContentContainer.Attributes["class"] = "rtilePeekContent";
			this._peekContentContainer.Attributes["style"] = "display:none;position:absolute;z-index:1;";
			this.Controls.Add(this._peekContentContainer);
		}

		// Token: 0x06005717 RID: 22295 RVA: 0x0010A78D File Offset: 0x0010898D
		void IMarkableStateManager.SetDirty()
		{
			this.SetDirty();
		}

		// Token: 0x06005718 RID: 22296 RVA: 0x0010A795 File Offset: 0x00108995
		internal virtual void SetDirty()
		{
			this.ViewState.SetDirty(true);
			this.Badge.SetDirty();
			this.Title.SetDirty();
			this.PeekTemplateSettings.SetDirty();
		}

		// Token: 0x06005719 RID: 22297 RVA: 0x0010A7C4 File Offset: 0x001089C4
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.Badge).SaveViewState(),
				((IStateManager)this.Title).SaveViewState(),
				((IStateManager)this.PeekTemplateSettings).SaveViewState()
			};
		}

		// Token: 0x0600571A RID: 22298 RVA: 0x0010A80C File Offset: 0x00108A0C
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.Badge).TrackViewState();
			((IStateManager)this.Title).TrackViewState();
			((IStateManager)this.PeekTemplateSettings).TrackViewState();
		}

		// Token: 0x0600571B RID: 22299 RVA: 0x0010A838 File Offset: 0x00108A38
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			base.LoadViewState(array[0]);
			if (array[1] != null)
			{
				((IStateManager)this.Badge).LoadViewState(array[1]);
			}
			if (array[2] != null)
			{
				((IStateManager)this.Title).LoadViewState(array[2]);
			}
			if (array[3] != null)
			{
				((IStateManager)this.PeekTemplateSettings).LoadViewState(array[3]);
			}
		}

		// Token: 0x17001CD4 RID: 7380
		// (get) Token: 0x0600571C RID: 22300 RVA: 0x0010A88E File Offset: 0x00108A8E
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return base.IsTrackingViewState;
			}
		}

		// Token: 0x0600571D RID: 22301 RVA: 0x0010A898 File Offset: 0x00108A98
		void IStateManager.LoadViewState(object state)
		{
			object[] savedState = (object[])state;
			this.LoadViewState(savedState);
		}

		// Token: 0x0600571E RID: 22302 RVA: 0x0010A8B3 File Offset: 0x00108AB3
		object IStateManager.SaveViewState()
		{
			return this.SaveViewState();
		}

		// Token: 0x0600571F RID: 22303 RVA: 0x0010A8BB File Offset: 0x00108ABB
		void IStateManager.TrackViewState()
		{
			this.TrackViewState();
		}

		// Token: 0x06005720 RID: 22304 RVA: 0x0010A8C4 File Offset: 0x00108AC4
		protected virtual string GetPostbackEventReference()
		{
			string postBackEventReference = this.Page.ClientScript.GetPostBackEventReference(this.GetPostBackOptions(this, "arguments", this.PostBackUrl));
			return postBackEventReference.Replace("\"", "'");
		}

		// Token: 0x06005721 RID: 22305 RVA: 0x0010A904 File Offset: 0x00108B04
		internal PostBackOptions GetPostBackOptions(Control control, string argument, string postBackUrl)
		{
			PostBackOptions postBackOptions = new PostBackOptions(control, argument)
			{
				ClientSubmit = true
			};
			if (this.Page != null && !string.IsNullOrEmpty(postBackUrl))
			{
				postBackOptions.ActionUrl = postBackUrl;
			}
			return postBackOptions;
		}

		// Token: 0x06005722 RID: 22306 RVA: 0x0010A93C File Offset: 0x00108B3C
		protected override bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			bool result = false;
			this.EnsureChildControls();
			string text = postCollection[base.ClientStateFieldID];
			if (string.IsNullOrEmpty(text))
			{
				return false;
			}
			RadTileClientState radTileClientState = null;
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			try
			{
				radTileClientState = javaScriptSerializer.Deserialize<RadTileClientState>(text);
			}
			catch (InvalidOperationException)
			{
			}
			catch (ArgumentException)
			{
			}
			if (radTileClientState != null)
			{
				bool flag = radTileClientState.Visible ?? true;
				if (this.Visible != flag)
				{
					this.Visible = flag;
					result = true;
				}
				if (this.Selected != radTileClientState.Selected)
				{
					this.Selected = radTileClientState.Selected;
					this._selectionStateChanged = true;
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06005723 RID: 22307 RVA: 0x0010A9F4 File Offset: 0x00108BF4
		protected override void RaisePostDataChangedEvent()
		{
			base.RaisePostDataChangedEvent();
			this.OnSelectionStateChanged(new EventArgs());
		}

		// Token: 0x06005724 RID: 22308 RVA: 0x0010AA07 File Offset: 0x00108C07
		void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
		{
			this.RaisePostBackEvent(eventArgument);
		}

		// Token: 0x06005725 RID: 22309 RVA: 0x0010AA10 File Offset: 0x00108C10
		protected internal virtual void RaisePostBackEvent(string eventArgument)
		{
			TilePostBackCommand tilePostBackCommand = null;
			try
			{
				tilePostBackCommand = new JavaScriptSerializer().Deserialize<TilePostBackCommand>(eventArgument);
			}
			catch (InvalidOperationException)
			{
			}
			catch (ArgumentException)
			{
			}
			if (tilePostBackCommand == null)
			{
				return;
			}
			TileCommand type = tilePostBackCommand.Type;
			if (type != TileCommand.TileClicked)
			{
				return;
			}
			this.OnClick(new EventArgs());
		}

		// Token: 0x06005726 RID: 22310 RVA: 0x0010AA6C File Offset: 0x00108C6C
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeProperty<bool>(descriptor, "autoPostBack", this.AutoPostBack, false);
			base.DescribeProperty<bool>(descriptor, "enableSelection", this.EnableSelection, false);
			base.DescribeProperty<string>(descriptor, "name", this.Name, "");
			base.DescribeProperty<string>(descriptor, "navigateUrl", base.ResolveClientUrl(this.NavigateUrl), "");
			base.DescribeProperty<bool>(descriptor, "selected", this.Selected, false);
			base.DescribeProperty<TileShape>(descriptor, "shape", this.Shape, TileShape.Square);
			base.DescribeProperty<string>(descriptor, "target", this.Target, "");
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x06005727 RID: 22311 RVA: 0x0010AB18 File Offset: 0x00108D18
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			RadWebControl.DescribeEvent(descriptor, "clicked", this.OnClientClicked);
			RadWebControl.DescribeEvent(descriptor, "clicking", this.OnClientClicking);
			RadWebControl.DescribeEvent(descriptor, "load", this.OnClientLoad);
			RadWebControl.DescribeEvent(descriptor, "selected", this.OnClientSelected);
			RadWebControl.DescribeEvent(descriptor, "selecting", this.OnClientSelecting);
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x0400153E RID: 5438
		private TileBadge _badge;

		// Token: 0x0400153F RID: 5439
		private TileTitle _title;

		// Token: 0x04001540 RID: 5440
		private TilePeekTemplateSettings _peekTemplateSettings;

		// Token: 0x04001541 RID: 5441
		private Panel _peekContentContainer;

		// Token: 0x04001542 RID: 5442
		private bool _peekTemplateInstantiated;

		// Token: 0x04001543 RID: 5443
		private bool _peekTemplateInstantiating;

		// Token: 0x04001544 RID: 5444
		private ITemplate _peekTemplate;

		// Token: 0x04001545 RID: 5445
		private object _dataItem;

		// Token: 0x04001546 RID: 5446
		private static readonly object clickEvent = new object();

		// Token: 0x04001547 RID: 5447
		private static readonly object selectionStateChangedEvent = new object();

		// Token: 0x04001548 RID: 5448
		internal int OriginalGroupIndex = -1;

		// Token: 0x04001549 RID: 5449
		internal int OriginalAllTilesIndex = -1;

		// Token: 0x0400154A RID: 5450
		internal int OriginalGroupId = -1;

		// Token: 0x0400154B RID: 5451
		internal int GroupIndex = -1;

		// Token: 0x0400154C RID: 5452
		internal bool _selectionStateChanged;
	}
}
