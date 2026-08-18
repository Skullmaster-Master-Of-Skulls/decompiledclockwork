using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Licensing;
using Telerik.Web.UI.Functions;

namespace Telerik.Web.UI
{
	// Token: 0x02001967 RID: 6503
	[Designer("Telerik.Web.Design.RadDataPagerDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[RequiredScript(typeof(MaterialRipple))]
	[TelerikToolboxCategory("Data")]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[ClientScriptResource("Telerik.Web.UI.RadDataPager", "Telerik.Web.UI.ListView.DataPager.RadDataPagerScripts.js")]
	[ParseChildren(true)]
	[PersistChildren(false)]
	[DefaultProperty("Fields")]
	[ToolboxBitmap(typeof(RadDataPager), "Telerik.Web.UI.DataPager.png")]
	[EmbeddedSkin("DataPager", "Default", typeof(RadDataPager))]
	[LightweightRendering]
	[EmbeddedSkin("DataPager", typeof(RadDataPager))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.MaterialRipple.css", RenderMode.Lightweight, typeof(RadTreeList))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Lightweight, typeof(RadDataPager))]
	[AdaptiveRendering]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Mobile, typeof(RadDataPager))]
	public class RadDataPager : RadWebControl, INamingContainer, ICompositeControlDesignerAccessor, IPostBackEventHandler, ILocalizableControl
	{
		// Token: 0x0600FBA8 RID: 64424 RVA: 0x0038B79C File Offset: 0x0038999C
		static RadDataPager()
		{
			RadDataPager.EventFieldCreating = new object();
			RadDataPager.EventFieldCreated = new object();
			RadDataPager.EventCommand = new object();
			RadDataPager.EventTotalRowCountRequest = new object();
			RadDataPager.EventPageIndexChanged = new object();
		}

		// Token: 0x0600FBA9 RID: 64425 RVA: 0x0038B81F File Offset: 0x00389A1F
		public RadDataPager()
		{
			this._maximumRows = 10;
		}

		// Token: 0x140001D5 RID: 469
		// (add) Token: 0x0600FBAA RID: 64426 RVA: 0x0038B837 File Offset: 0x00389A37
		// (remove) Token: 0x0600FBAB RID: 64427 RVA: 0x0038B84A File Offset: 0x00389A4A
		[Category("Action")]
		[Description("Raised when custom pager field is creating on postback")]
		public event DataPagerFieldCreatingEventHandler<RadDataPagerFieldCreatingEventArgs> FieldCreating
		{
			add
			{
				base.Events.AddHandler(RadDataPager.EventFieldCreating, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadDataPager.EventFieldCreating, value);
			}
		}

		// Token: 0x0600FBAC RID: 64428 RVA: 0x0038B85D File Offset: 0x00389A5D
		internal void CallOnFieldCreating(RadDataPagerFieldCreatingEventArgs e)
		{
			this.OnFieldCreating(e);
		}

		// Token: 0x0600FBAD RID: 64429 RVA: 0x0038B868 File Offset: 0x00389A68
		protected virtual void OnFieldCreating(RadDataPagerFieldCreatingEventArgs e)
		{
			DataPagerFieldCreatingEventHandler<RadDataPagerFieldCreatingEventArgs> dataPagerFieldCreatingEventHandler = base.Events[RadDataPager.EventFieldCreating] as DataPagerFieldCreatingEventHandler<RadDataPagerFieldCreatingEventArgs>;
			if (dataPagerFieldCreatingEventHandler != null)
			{
				dataPagerFieldCreatingEventHandler(this, e);
			}
		}

		// Token: 0x140001D6 RID: 470
		// (add) Token: 0x0600FBAE RID: 64430 RVA: 0x0038B896 File Offset: 0x00389A96
		// (remove) Token: 0x0600FBAF RID: 64431 RVA: 0x0038B8A9 File Offset: 0x00389AA9
		[Category("Action")]
		[Description("Raised when pager field item is created.")]
		public event DataPagerFieldCreatedEventHandler<RadDataPagerFieldCreatedEventArgs> FieldCreated
		{
			add
			{
				base.Events.AddHandler(RadDataPager.EventFieldCreated, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadDataPager.EventFieldCreated, value);
			}
		}

		// Token: 0x0600FBB0 RID: 64432 RVA: 0x0038B8BC File Offset: 0x00389ABC
		protected virtual void OnFieldCreated(RadDataPagerFieldCreatedEventArgs e)
		{
			DataPagerFieldCreatedEventHandler<RadDataPagerFieldCreatedEventArgs> dataPagerFieldCreatedEventHandler = base.Events[RadDataPager.EventFieldCreated] as DataPagerFieldCreatedEventHandler<RadDataPagerFieldCreatedEventArgs>;
			if (dataPagerFieldCreatedEventHandler != null)
			{
				dataPagerFieldCreatedEventHandler(this, e);
			}
		}

		// Token: 0x140001D7 RID: 471
		// (add) Token: 0x0600FBB1 RID: 64433 RVA: 0x0038B8EA File Offset: 0x00389AEA
		// (remove) Token: 0x0600FBB2 RID: 64434 RVA: 0x0038B8FD File Offset: 0x00389AFD
		[Category("Action")]
		[Description("Raised when a button in a RadDataPager control is clicked.")]
		public event EventHandler<RadDataPagerCommandEventArgs> Command
		{
			add
			{
				base.Events.AddHandler(RadDataPager.EventCommand, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadDataPager.EventCommand, value);
			}
		}

		// Token: 0x0600FBB3 RID: 64435 RVA: 0x0038B910 File Offset: 0x00389B10
		protected virtual void CallCommand(RadDataPagerCommandEventArgs e)
		{
			EventHandler<RadDataPagerCommandEventArgs> eventHandler = base.Events[RadDataPager.EventCommand] as EventHandler<RadDataPagerCommandEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x140001D8 RID: 472
		// (add) Token: 0x0600FBB4 RID: 64436 RVA: 0x0038B93E File Offset: 0x00389B3E
		// (remove) Token: 0x0600FBB5 RID: 64437 RVA: 0x0038B951 File Offset: 0x00389B51
		[Description("Raised when RadDataPager is not attached to pageable container and need information regarding total count of the items to page.")]
		[Category("Action")]
		public event EventHandler<RadDataPagerTotalRowCountRequestEventArgs> TotalRowCountRequest
		{
			add
			{
				base.Events.AddHandler(RadDataPager.EventTotalRowCountRequest, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadDataPager.EventTotalRowCountRequest, value);
			}
		}

		// Token: 0x0600FBB6 RID: 64438 RVA: 0x0038B964 File Offset: 0x00389B64
		protected virtual void OnTotalRowCountRequest(RadDataPagerTotalRowCountRequestEventArgs e)
		{
			EventHandler<RadDataPagerTotalRowCountRequestEventArgs> eventHandler = base.Events[RadDataPager.EventTotalRowCountRequest] as EventHandler<RadDataPagerTotalRowCountRequestEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x140001D9 RID: 473
		// (add) Token: 0x0600FBB7 RID: 64439 RVA: 0x0038B992 File Offset: 0x00389B92
		// (remove) Token: 0x0600FBB8 RID: 64440 RVA: 0x0038B9A5 File Offset: 0x00389BA5
		[Description("Raised when current page index is changed.")]
		[Category("Action")]
		public event EventHandler<RadDataPagerPageIndexChangeEventArgs> PageIndexChanged
		{
			add
			{
				base.Events.AddHandler(RadDataPager.EventPageIndexChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadDataPager.EventPageIndexChanged, value);
			}
		}

		// Token: 0x0600FBB9 RID: 64441 RVA: 0x0038B9B8 File Offset: 0x00389BB8
		protected virtual void OnPageIndexChanged(RadDataPagerPageIndexChangeEventArgs e)
		{
			EventHandler<RadDataPagerPageIndexChangeEventArgs> eventHandler = base.Events[RadDataPager.EventPageIndexChanged] as EventHandler<RadDataPagerPageIndexChangeEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x17004C0A RID: 19466
		// (get) Token: 0x0600FBBA RID: 64442 RVA: 0x0038B9E8 File Offset: 0x00389BE8
		// (set) Token: 0x0600FBBB RID: 64443 RVA: 0x0038BA15 File Offset: 0x00389C15
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		[IDReferenceProperty(typeof(IRadPageableItemContainer))]
		public virtual string PagedControlID
		{
			get
			{
				object obj = this.ViewState["PagedControlID"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["PagedControlID"] = value;
			}
		}

		// Token: 0x17004C0B RID: 19467
		// (get) Token: 0x0600FBBC RID: 64444 RVA: 0x0038BA28 File Offset: 0x00389C28
		[MergableProperty(false)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Default")]
		[NotifyParentProperty(true)]
		[Editor("Telerik.Web.Design.RadDataPagerFieldsEditorForm, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public RadDataPagerFieldCollection Fields
		{
			get
			{
				if (this._fields == null)
				{
					this._fields = new RadDataPagerFieldCollection(this);
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._fields).TrackViewState();
					}
				}
				return this._fields;
			}
		}

		// Token: 0x17004C0C RID: 19468
		// (get) Token: 0x0600FBBD RID: 64445 RVA: 0x0038BA57 File Offset: 0x00389C57
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Client")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public RadDataPagerClientEvents ClientEvents
		{
			get
			{
				if (this._clientEvents == null)
				{
					this._clientEvents = new RadDataPagerClientEvents();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._clientEvents).TrackViewState();
					}
				}
				return this._clientEvents;
			}
		}

		// Token: 0x17004C0D RID: 19469
		// (get) Token: 0x0600FBBE RID: 64446 RVA: 0x0038BA85 File Offset: 0x00389C85
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public IRadPageableItemContainer PageableItemContainer
		{
			get
			{
				return this._pageableItemContainer;
			}
		}

		// Token: 0x17004C0E RID: 19470
		// (get) Token: 0x0600FBBF RID: 64447 RVA: 0x0038BA8D File Offset: 0x00389C8D
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual PageableItemContainerLocator ContainerLocator
		{
			get
			{
				if (this._containerLocator == null)
				{
					this._containerLocator = new PageableItemContainerLocator();
				}
				return this._containerLocator;
			}
		}

		// Token: 0x17004C0F RID: 19471
		// (get) Token: 0x0600FBC0 RID: 64448 RVA: 0x0038BAA8 File Offset: 0x00389CA8
		// (set) Token: 0x0600FBC1 RID: 64449 RVA: 0x0038BAB0 File Offset: 0x00389CB0
		[SimplePersistenceSetting]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public int StartRowIndex
		{
			get
			{
				return this._startRowIndex;
			}
			internal set
			{
				this._startRowIndex = value;
				this.ApplySettingsOnContainer(this._startRowIndex, this.PageSize, true);
			}
		}

		// Token: 0x17004C10 RID: 19472
		// (get) Token: 0x0600FBC2 RID: 64450 RVA: 0x0038BACC File Offset: 0x00389CCC
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int TotalRowCount
		{
			get
			{
				return this._totalRowCount;
			}
		}

		// Token: 0x17004C11 RID: 19473
		// (get) Token: 0x0600FBC3 RID: 64451 RVA: 0x0038BAD4 File Offset: 0x00389CD4
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public int MaximumRows
		{
			get
			{
				return this._maximumRows;
			}
		}

		// Token: 0x17004C12 RID: 19474
		// (get) Token: 0x0600FBC4 RID: 64452 RVA: 0x0038BADC File Offset: 0x00389CDC
		// (set) Token: 0x0600FBC5 RID: 64453 RVA: 0x0038BB0C File Offset: 0x00389D0C
		internal int OriginalPageSize
		{
			get
			{
				if (this.ViewState["OriginalPageSize"] != null)
				{
					return int.Parse(this.ViewState["OriginalPageSize"].ToString());
				}
				return -1;
			}
			set
			{
				this.ViewState["OriginalPageSize"] = value.ToString();
			}
		}

		// Token: 0x17004C13 RID: 19475
		// (get) Token: 0x0600FBC6 RID: 64454 RVA: 0x0038BB25 File Offset: 0x00389D25
		// (set) Token: 0x0600FBC7 RID: 64455 RVA: 0x0038BB30 File Offset: 0x00389D30
		[DefaultValue(10)]
		[SimplePersistenceSetting]
		public int PageSize
		{
			get
			{
				return this._maximumRows;
			}
			set
			{
				if (value < 1)
				{
					throw new ArgumentOutOfRangeException("value", "Value must be bigger then 0.");
				}
				if (this.OriginalPageSize == -1)
				{
					this.OriginalPageSize = value;
				}
				if (this._maximumRows != value)
				{
					this._maximumRows = value;
					if (this._isControlInitialized)
					{
						this.CreateDataPagerFields();
						this.FireCommand("PageSizeChange", this._maximumRows.ToString());
					}
				}
			}
		}

		// Token: 0x17004C14 RID: 19476
		// (get) Token: 0x0600FBC8 RID: 64456 RVA: 0x0038BB95 File Offset: 0x00389D95
		// (set) Token: 0x0600FBC9 RID: 64457 RVA: 0x0038BBA4 File Offset: 0x00389DA4
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public int CurrentPageIndex
		{
			get
			{
				return this.StartRowIndex / this.PageSize;
			}
			set
			{
				if (value >= 0)
				{
					this.StartRowIndex = value * this.PageSize;
				}
			}
		}

		// Token: 0x17004C15 RID: 19477
		// (get) Token: 0x0600FBCA RID: 64458 RVA: 0x0038BBB8 File Offset: 0x00389DB8
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int PageCount
		{
			get
			{
				int num = this.TotalRowCount / this.PageSize;
				if (this.TotalRowCount % this.PageSize != 0)
				{
					num++;
				}
				return num;
			}
		}

		// Token: 0x17004C16 RID: 19478
		// (get) Token: 0x0600FBCB RID: 64459 RVA: 0x0038BBE7 File Offset: 0x00389DE7
		// (set) Token: 0x0600FBCC RID: 64460 RVA: 0x0038BC11 File Offset: 0x00389E11
		[NotifyParentProperty(true)]
		[DefaultValue("PageTo")]
		public string SEOPagingQueryPageKey
		{
			get
			{
				string result;
				if ((result = (string)this.ViewState["SEOPagingQueryPageKey"]) == null)
				{
					result = (this.PagedControlID ?? "PageTo");
				}
				return result;
			}
			set
			{
				this.ViewState["SEOPagingQueryPageKey"] = value;
			}
		}

		// Token: 0x17004C17 RID: 19479
		// (get) Token: 0x0600FBCD RID: 64461 RVA: 0x0038BC24 File Offset: 0x00389E24
		// (set) Token: 0x0600FBCE RID: 64462 RVA: 0x0038BC5C File Offset: 0x00389E5C
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		public bool AllowSEOPaging
		{
			get
			{
				return this.ViewState["AllowSEOPaging"] != null && (bool)this.ViewState["AllowSEOPaging"];
			}
			set
			{
				this.ViewState["AllowSEOPaging"] = value;
			}
		}

		// Token: 0x17004C18 RID: 19480
		// (get) Token: 0x0600FBCF RID: 64463 RVA: 0x0038BC74 File Offset: 0x00389E74
		// (set) Token: 0x0600FBD0 RID: 64464 RVA: 0x0038BCAC File Offset: 0x00389EAC
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		public bool RemoveUrlFromDisabledHyperLinkButtons
		{
			get
			{
				return this.ViewState["RemoveDisabledUrl"] != null && (bool)this.ViewState["RemoveDisabledUrl"];
			}
			set
			{
				this.ViewState["RemoveDisabledUrl"] = value;
			}
		}

		// Token: 0x17004C19 RID: 19481
		// (get) Token: 0x0600FBD1 RID: 64465 RVA: 0x0038BCC4 File Offset: 0x00389EC4
		// (set) Token: 0x0600FBD2 RID: 64466 RVA: 0x0038BCE5 File Offset: 0x00389EE5
		[DefaultValue(false)]
		[Category("Behavior")]
		[Description("When set to true enables support for WAI-ARIA")]
		public bool EnableAriaSupport
		{
			get
			{
				return (bool)(this.ViewState["EnableAriaSupport"] ?? false);
			}
			set
			{
				this.ViewState["EnableAriaSupport"] = value;
			}
		}

		// Token: 0x0600FBD3 RID: 64467 RVA: 0x0038BD00 File Offset: 0x00389F00
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			if (base.DesignMode)
			{
				return;
			}
			this._pageableItemContainer = this.ContainerLocator.RetrievePageableItemContainer(this, this.PagedControlID);
			if (this._pageableItemContainer != null)
			{
				this.AddListenersToContainer();
				this.ApplySettingsOnContainer(this._startRowIndex, this._maximumRows, false);
				this._isPagePropertiesSet = true;
			}
			if (this.Page != null)
			{
				this.Page.RegisterRequiresControlState(this);
			}
			this._isControlInitialized = true;
		}

		// Token: 0x0600FBD4 RID: 64468 RVA: 0x0038BD78 File Offset: 0x00389F78
		protected internal virtual bool BrowserIsCrawler()
		{
			return this.Page != null && this.Page.Request != null && this.Page.Request.Browser.Crawler;
		}

		// Token: 0x0600FBD5 RID: 64469 RVA: 0x0038BDA8 File Offset: 0x00389FA8
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			if (this._pageableItemContainer == null)
			{
				this._pageableItemContainer = this.ContainerLocator.RetrievePageableItemContainer(this, this.PagedControlID);
				if (this._pageableItemContainer == null)
				{
					return;
				}
			}
			if (!this._isPagePropertiesSet)
			{
				this.AddListenersToContainer();
				this.ApplySettingsOnContainer(this._startRowIndex, this._maximumRows, false);
				this._isPagePropertiesSet = true;
			}
		}

		// Token: 0x0600FBD6 RID: 64470 RVA: 0x0038BE10 File Offset: 0x0038A010
		public void FireCommand(string commandName, string commandArgument)
		{
			RadDataPagerCommandEventArgs args = new RadDataPagerCommandEventArgs(this, commandName, commandArgument);
			this.OnBubbleEvent(this, args);
		}

		// Token: 0x0600FBD7 RID: 64471 RVA: 0x0038BE2F File Offset: 0x0038A02F
		public void FireCommand(RadDataPagerCommandEventArgs commandArgs)
		{
			this.OnBubbleEvent(this, commandArgs);
		}

		// Token: 0x17004C1A RID: 19482
		// (get) Token: 0x0600FBD8 RID: 64472 RVA: 0x0038BE3C File Offset: 0x0038A03C
		// (set) Token: 0x0600FBD9 RID: 64473 RVA: 0x0038BE65 File Offset: 0x0038A065
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		public bool AllowRouting
		{
			get
			{
				object obj = this.ViewState["AllowRouting"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["AllowRouting"] = value;
			}
		}

		// Token: 0x17004C1B RID: 19483
		// (get) Token: 0x0600FBDA RID: 64474 RVA: 0x0038BE7D File Offset: 0x0038A07D
		// (set) Token: 0x0600FBDB RID: 64475 RVA: 0x0038BE9D File Offset: 0x0038A09D
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		public string RouteName
		{
			get
			{
				return ((string)this.ViewState["RouteName"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["RouteName"] = value;
			}
		}

		// Token: 0x17004C1C RID: 19484
		// (get) Token: 0x0600FBDC RID: 64476 RVA: 0x0038BEB0 File Offset: 0x0038A0B0
		// (set) Token: 0x0600FBDD RID: 64477 RVA: 0x0038BED0 File Offset: 0x0038A0D0
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		public string RoutePageIndexParameterName
		{
			get
			{
				return ((string)this.ViewState["RoutePageIndexParameterName"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["RoutePageIndexParameterName"] = value;
			}
		}

		// Token: 0x17004C1D RID: 19485
		// (get) Token: 0x0600FBDE RID: 64478 RVA: 0x0038BEE3 File Offset: 0x0038A0E3
		protected bool IsPageableItemContainerAttached
		{
			get
			{
				return !string.IsNullOrEmpty(this.PagedControlID) || this.PageableItemContainer != null;
			}
		}

		// Token: 0x0600FBDF RID: 64479 RVA: 0x0038BEFF File Offset: 0x0038A0FF
		protected virtual void AddListenersToContainer()
		{
			this.PageableItemContainer.TotalRowCountAvailable += this.OnTotalRowCountAvailable;
		}

		// Token: 0x0600FBE0 RID: 64480 RVA: 0x0038BF1C File Offset: 0x0038A11C
		protected virtual void OnTotalRowCountAvailable(object sender, RadDataPagerPageEventArgs e)
		{
			this._startRowIndex = e.StartRowIndex;
			this._maximumRows = e.MaximumRows;
			this._totalRowCount = e.TotalRowCount;
			if (this.TotalRowCount > 0 && this.StartRowIndex >= this.TotalRowCount)
			{
				this.FireCommand("Page", "Last");
				return;
			}
			if (!this._creatingPagerFieldsInProgress)
			{
				this.CreateDataPagerFields();
			}
		}

		// Token: 0x0600FBE1 RID: 64481 RVA: 0x0038BF83 File Offset: 0x0038A183
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			if (!this.IsPageableItemContainerAttached)
			{
				this.ApplySettingsOnContainer(this.StartRowIndex, this.PageSize, false);
			}
		}

		// Token: 0x0600FBE2 RID: 64482 RVA: 0x0038BFA8 File Offset: 0x0038A1A8
		protected virtual void CreateDataPagerFields()
		{
			this._creatingPagerFieldsInProgress = true;
			this.Controls.Clear();
			foreach (RadDataPagerField radDataPagerField in this.Fields)
			{
				RadDataPagerFieldItem radDataPagerFieldItem = new RadDataPagerFieldItem(this, radDataPagerField);
				this.Controls.Add(radDataPagerFieldItem);
				radDataPagerField.InitializeFieldControls(radDataPagerFieldItem);
				radDataPagerFieldItem.Visible = radDataPagerField.Visible;
				RadDataPagerFieldCreatedEventArgs e = new RadDataPagerFieldCreatedEventArgs(radDataPagerFieldItem);
				this.OnFieldCreated(e);
				radDataPagerFieldItem.DataBind();
			}
			this._creatingPagerFieldsInProgress = false;
			if (this.AllowSEOPaging)
			{
				this.HandleSEOPaging();
			}
		}

		// Token: 0x0600FBE3 RID: 64483 RVA: 0x0038C050 File Offset: 0x0038A250
		private void ClearControlsRecursive(Control control)
		{
			foreach (object obj in control.Controls)
			{
				Control control2 = (Control)obj;
				this.ClearControlsRecursive(control2);
			}
			control.Controls.Clear();
		}

		// Token: 0x0600FBE4 RID: 64484 RVA: 0x0038C0B4 File Offset: 0x0038A2B4
		private string SEOPagingQueryStringKey()
		{
			if (!string.IsNullOrEmpty(this.SEOPagingQueryPageKey))
			{
				return this.SEOPagingQueryPageKey;
			}
			return string.Format("{0}ChangePage", this.ClientID);
		}

		// Token: 0x0600FBE5 RID: 64485 RVA: 0x0038C0DC File Offset: 0x0038A2DC
		internal string GeneratePagingStateAttributeLink(int pageSize)
		{
			int pageIndex = this.CurrentPageIndex + 1;
			return this.GeneratePagingStateAttributeLink(pageIndex, pageSize);
		}

		// Token: 0x0600FBE6 RID: 64486 RVA: 0x0038C0FC File Offset: 0x0038A2FC
		internal string GeneratePagingStateAttributeLink(int pageIndex, int pageSize)
		{
			if (this.AllowRouting && !string.IsNullOrEmpty(this.RoutePageIndexParameterName))
			{
				return this.FixRoutedStringUrl(pageIndex.ToString(), new int?(pageSize));
			}
			string url = base.DesignMode ? "" : this.Page.Response.ApplyAppPathModifier(this.Page.Request.RawUrl);
			return this.AppendKeyValuePairToQueryString(url, this.SEOPagingQueryStringKey(), this.GetSeoPageUrlParameter(pageIndex.ToString(), new int?(pageSize)));
		}

		// Token: 0x0600FBE7 RID: 64487 RVA: 0x0038C184 File Offset: 0x0038A384
		[SuppressMessage("Microsoft.Globalization", "CA1307:SpecifyStringComparison", MessageId = "System.String.EndsWith(System.String)")]
		[SuppressMessage("Microsoft.Globalization", "CA1307:SpecifyStringComparison", MessageId = "System.String.IndexOf(System.String)")]
		internal string FixRoutedStringUrl(string pageIdx, int? pageSize)
		{
			string text = string.Empty;
			string text2 = this.Page.Response.ApplyAppPathModifier(this.Page.Request.RawUrl);
			string routePageIndexParameterName = this.RoutePageIndexParameterName;
			text = this.BuildRouteUrl(this.RouteName, routePageIndexParameterName, this.GetSeoPageUrlParameter(pageIdx, pageSize));
			if (text2.IndexOf("?") > -1 && !text2.EndsWith("?"))
			{
				string text3 = text2.Substring(text2.IndexOf("?"));
				if (text.IndexOf("?") > -1)
				{
					text = text.Remove(text.IndexOf("?"));
					text += this.AppendKeyValuePairToQueryString(text3, routePageIndexParameterName, pageIdx);
				}
				else
				{
					text += text3;
				}
			}
			return text;
		}

		// Token: 0x0600FBE8 RID: 64488 RVA: 0x0038C240 File Offset: 0x0038A440
		private string BuildRouteUrl(string routeName, string pageIndexParameterName, string urlParam)
		{
			RouteValueDictionary routeValueDictionary = new RouteValueDictionary();
			foreach (KeyValuePair<string, object> keyValuePair in this.Page.RouteData.Values)
			{
				routeValueDictionary.Add(keyValuePair.Key, keyValuePair.Value);
			}
			routeValueDictionary[pageIndexParameterName] = urlParam;
			VirtualPathData virtualPath;
			if (!string.IsNullOrEmpty(routeName))
			{
				virtualPath = RouteTable.Routes.GetVirtualPath(null, routeName, routeValueDictionary);
			}
			else
			{
				virtualPath = RouteTable.Routes.GetVirtualPath(null, routeValueDictionary);
			}
			if (virtualPath == null)
			{
				return string.Empty;
			}
			return virtualPath.VirtualPath;
		}

		// Token: 0x0600FBE9 RID: 64489 RVA: 0x0038C2F0 File Offset: 0x0038A4F0
		private string GetSeoPageUrlParameter(string url, int? pageSize)
		{
			if (!(pageSize == this._defaultPageSize) && pageSize != null)
			{
				return string.Format("{0}_{1}", url, pageSize);
			}
			return url;
		}

		// Token: 0x0600FBEA RID: 64490 RVA: 0x0038C338 File Offset: 0x0038A538
		private string AppendKeyValuePairToQueryString(string url, string key, string value)
		{
			string arg = string.Empty;
			url = this.RemoveKeyValuePairFromQueryString(url, key);
			if (url.IndexOf("?") > -1)
			{
				if (!url.EndsWith("?") && !url.EndsWith("&"))
				{
					arg = "&";
				}
			}
			else
			{
				arg = "?";
			}
			return url + string.Format("{0}{1}={2}", arg, key, value);
		}

		// Token: 0x0600FBEB RID: 64491 RVA: 0x0038C3A0 File Offset: 0x0038A5A0
		private string RemoveKeyValuePairFromQueryString(string url, string keyName)
		{
			int num = url.IndexOf(keyName + "=");
			if (num > -1)
			{
				int num2 = url.IndexOf("&", num);
				if (num2 > -1)
				{
					url = url.Remove(num, num2 - num + 1);
				}
				else
				{
					url = url.Remove(num, url.Length - num);
				}
				if (url.EndsWith("&") || url.EndsWith("?"))
				{
					url = url.Remove(url.Length - 1, 1);
				}
			}
			return url;
		}

		// Token: 0x0600FBEC RID: 64492 RVA: 0x0038C424 File Offset: 0x0038A624
		protected void HandleSEOPaging()
		{
			int? num = new int?(this.PageSize);
			if (this._seoPagingHandled)
			{
				return;
			}
			this._seoPagingHandled = true;
			if (this.Page == null)
			{
				return;
			}
			string text = string.Empty;
			if (this.AllowRouting)
			{
				text = ((this.Page.RouteData.Values[this.RoutePageIndexParameterName] == null) ? string.Empty : this.Page.RouteData.Values[this.RoutePageIndexParameterName].ToString());
				if (string.IsNullOrEmpty(text))
				{
					text = this.Page.Request.QueryString[this.RoutePageIndexParameterName];
				}
			}
			else
			{
				text = this.Page.Request.QueryString[this.SEOPagingQueryPageKey];
			}
			int num2;
			if (string.IsNullOrEmpty(text))
			{
				num2 = 0;
			}
			else if (text.Contains("_"))
			{
				string[] array = text.Split(new char[]
				{
					'_'
				}, StringSplitOptions.RemoveEmptyEntries);
				int value;
				if (int.TryParse(array[1], out value))
				{
					num = new int?(value);
				}
				else
				{
					num = new int?(this._defaultPageSize);
				}
				int num3;
				if (int.TryParse(array[0], out num3))
				{
					num3 = ((--num3 < 0) ? 0 : num3);
					num3 *= num.Value;
					num2 = num3;
				}
				else
				{
					num2 = this.StartRowIndex;
				}
			}
			else if (!int.TryParse(text, out num2))
			{
				num2 = this.StartRowIndex;
			}
			else
			{
				num2 = ((--num2 < 0) ? 0 : num2);
				num2 *= num.Value;
			}
			if (num2 != this.StartRowIndex || num != this.PageSize)
			{
				if (num2 > this.TotalRowCount)
				{
					num2 = 0;
				}
				this.ApplySettingsOnContainer(num2, num.Value, true);
			}
		}

		// Token: 0x0600FBED RID: 64493 RVA: 0x0038C5F0 File Offset: 0x0038A7F0
		protected override bool OnBubbleEvent(object source, EventArgs args)
		{
			bool flag = false;
			RadDataPagerCommandEventArgs radDataPagerCommandEventArgs = args as RadDataPagerCommandEventArgs;
			if (radDataPagerCommandEventArgs != null)
			{
				this.CallCommand(radDataPagerCommandEventArgs);
				flag = this.CallCommandToContainer(radDataPagerCommandEventArgs.CommandName, (string)radDataPagerCommandEventArgs.CommandArgument);
				if (!flag)
				{
					flag = true;
					base.RaiseBubbleEvent(source, radDataPagerCommandEventArgs);
				}
			}
			return flag;
		}

		// Token: 0x0600FBEE RID: 64494 RVA: 0x0038C638 File Offset: 0x0038A838
		protected virtual bool CallCommandToContainer(string commandName, string commandArgument)
		{
			int startRowIndex = 0;
			int pageSize = this.PageSize;
			if (commandName != "Page" && commandName != "PageSizeChange")
			{
				return false;
			}
			if (commandName == "Page")
			{
				startRowIndex = this.CalculateStartRowIndex(commandArgument);
			}
			else if (commandName == "PageSizeChange")
			{
				if (!int.TryParse(commandArgument, out pageSize))
				{
					throw new ArgumentOutOfRangeException("value", "Value must be bigger then 0.");
				}
				startRowIndex = 0;
			}
			this.ApplySettingsOnContainer(startRowIndex, pageSize, true);
			return true;
		}

		// Token: 0x0600FBEF RID: 64495 RVA: 0x0038C6B8 File Offset: 0x0038A8B8
		internal int CalculateStartRowIndex(string commandArgument)
		{
			int num;
			if (commandArgument != null)
			{
				if (!(commandArgument == "Next"))
				{
					if (commandArgument == "Prev")
					{
						num = this.StartRowIndex - this.PageSize;
						goto IL_CB;
					}
					if (commandArgument == "First")
					{
						num = 0;
						goto IL_CB;
					}
					if (commandArgument == "Last")
					{
						int pageCount = this.PageCount;
						num = this.PageSize * (pageCount - 1);
						goto IL_CB;
					}
				}
				else
				{
					num = this.StartRowIndex + this.PageSize;
					if (num >= this.TotalRowCount)
					{
						int pageCount = this.PageCount;
						num = this.PageSize * (pageCount - 1);
						goto IL_CB;
					}
					goto IL_CB;
				}
			}
			if (int.TryParse(commandArgument, out num))
			{
				num *= this.PageSize;
				if (num < 0)
				{
					num = 0;
				}
				if (num >= this.TotalRowCount)
				{
					int pageCount = this.PageCount;
					num = this.PageSize * (pageCount - 1);
				}
			}
			IL_CB:
			if (num < 0)
			{
				num = 0;
			}
			return num;
		}

		// Token: 0x0600FBF0 RID: 64496 RVA: 0x0038C798 File Offset: 0x0038A998
		protected void ApplySettingsOnContainer(int startRowIndex, int pageSize, bool shouldBind)
		{
			if (!this.IsPageableItemContainerAttached)
			{
				RadDataPagerTotalRowCountRequestEventArgs radDataPagerTotalRowCountRequestEventArgs = new RadDataPagerTotalRowCountRequestEventArgs();
				this.OnTotalRowCountRequest(radDataPagerTotalRowCountRequestEventArgs);
				if (radDataPagerTotalRowCountRequestEventArgs.TotalRowCount < this.TotalRowCount)
				{
					startRowIndex = 0;
				}
				this.CallOnPageIndexChanged(startRowIndex, pageSize);
				this.OnTotalRowCountAvailable(null, new RadDataPagerPageEventArgs(startRowIndex, pageSize, radDataPagerTotalRowCountRequestEventArgs.TotalRowCount));
				return;
			}
			this.CallOnPageIndexChanged(startRowIndex, pageSize);
			this.PageableItemContainer.SetPageProperties(startRowIndex, pageSize, shouldBind);
		}

		// Token: 0x0600FBF1 RID: 64497 RVA: 0x0038C800 File Offset: 0x0038AA00
		protected void CallOnPageIndexChanged(int startRowIndex, int pageSize)
		{
			if (startRowIndex != this.StartRowIndex || this.AllowSEOPaging)
			{
				RadDataPagerPageIndexChangeEventArgs e = new RadDataPagerPageIndexChangeEventArgs(startRowIndex / pageSize, startRowIndex);
				this.OnPageIndexChanged(e);
			}
		}

		// Token: 0x0600FBF2 RID: 64498 RVA: 0x0038C830 File Offset: 0x0038AA30
		protected override void LoadControlState(object savedState)
		{
			this.LoadControlInternalState(savedState);
			if (this._pageableItemContainer == null)
			{
				this._pageableItemContainer = this.ContainerLocator.RetrievePageableItemContainer(this, this.PagedControlID);
				if (this._pageableItemContainer == null)
				{
					return;
				}
				this.AddListenersToContainer();
			}
			this.ApplySettingsOnContainer(this._startRowIndex, this._maximumRows, false);
			this._isPagePropertiesSet = true;
		}

		// Token: 0x0600FBF3 RID: 64499 RVA: 0x0038C88D File Offset: 0x0038AA8D
		protected override object SaveControlState()
		{
			return this.SaveControlInternalState();
		}

		// Token: 0x0600FBF4 RID: 64500 RVA: 0x0038C898 File Offset: 0x0038AA98
		protected virtual object SaveControlInternalState()
		{
			object obj = base.SaveControlState();
			if (obj != null || this._startRowIndex != 0 || this._maximumRows != 10 || this._totalRowCount != -1)
			{
				return new object[]
				{
					obj,
					this._startRowIndex,
					this._maximumRows,
					this._totalRowCount
				};
			}
			return null;
		}

		// Token: 0x0600FBF5 RID: 64501 RVA: 0x0038C904 File Offset: 0x0038AB04
		protected virtual void LoadControlInternalState(object savedState)
		{
			this._startRowIndex = 0;
			this._maximumRows = 10;
			this._totalRowCount = -1;
			object[] array = savedState as object[];
			if (array != null)
			{
				base.LoadControlState(array[0]);
				if (array[1] != null)
				{
					this._startRowIndex = (int)array[1];
				}
				if (array[2] != null)
				{
					this._maximumRows = (int)array[2];
				}
				if (array[3] != null)
				{
					this._totalRowCount = (int)array[3];
					return;
				}
			}
			else
			{
				base.LoadControlState(savedState);
			}
		}

		// Token: 0x0600FBF6 RID: 64502 RVA: 0x0038C97C File Offset: 0x0038AB7C
		protected override void LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				object[] array = (object[])savedState;
				base.LoadViewState(array[0]);
				((IStateManager)this.Fields).LoadViewState(array[1]);
				return;
			}
			base.LoadViewState(savedState);
		}

		// Token: 0x0600FBF7 RID: 64503 RVA: 0x0038C9B4 File Offset: 0x0038ABB4
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.Fields).SaveViewState()
			};
		}

		// Token: 0x0600FBF8 RID: 64504 RVA: 0x0038C9E0 File Offset: 0x0038ABE0
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.Fields).TrackViewState();
		}

		// Token: 0x0600FBF9 RID: 64505 RVA: 0x0038C9F3 File Offset: 0x0038ABF3
		public void RecreateChildControls()
		{
			base.ChildControlsCreated = false;
			this.EnsureChildControls();
		}

		// Token: 0x0600FBFA RID: 64506 RVA: 0x0038CA02 File Offset: 0x0038AC02
		void ICompositeControlDesignerAccessor.RecreateChildControls()
		{
			this.RecreateChildControls();
		}

		// Token: 0x17004C1E RID: 19486
		// (get) Token: 0x0600FBFB RID: 64507 RVA: 0x0038CA0A File Offset: 0x0038AC0A
		public override ControlCollection Controls
		{
			get
			{
				this.EnsureChildControls();
				return base.Controls;
			}
		}

		// Token: 0x0600FBFC RID: 64508 RVA: 0x0038CA18 File Offset: 0x0038AC18
		protected override void Render(HtmlTextWriter writer)
		{
			if (base.DesignMode)
			{
				this.EnsureChildControls();
				this.OnTotalRowCountAvailable(null, new RadDataPagerPageEventArgs(0, this.PageSize, 101));
			}
			base.Render(writer);
		}

		// Token: 0x17004C1F RID: 19487
		// (get) Token: 0x0600FBFD RID: 64509 RVA: 0x0038CA44 File Offset: 0x0038AC44
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x0600FBFE RID: 64510 RVA: 0x0038CA48 File Offset: 0x0038AC48
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			if (!string.IsNullOrEmpty(this.CssClass))
			{
				this.CssClass = string.Format(" RadDataPager RadDataPager_{0} {1}", base.RuntimeSkin, this.CssClass);
			}
			else
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, string.Format("RadDataPager RadDataPager_{0}", base.RuntimeSkin));
			}
			base.AddAttributesToRender(writer);
		}

		// Token: 0x0600FBFF RID: 64511 RVA: 0x0038CA9F File Offset: 0x0038AC9F
		protected override void RenderContents(HtmlTextWriter writer)
		{
			if (base.DesignMode)
			{
				writer.Write(SkinRegistrar.GetDesignTimeStyleSheet(this));
			}
			base.RenderContents(writer);
		}

		// Token: 0x0600FC00 RID: 64512 RVA: 0x0038CABC File Offset: 0x0038ACBC
		internal void CallFieldsChanged()
		{
			if (this._isControlInitialized)
			{
				this.ApplySettingsOnContainer(this.StartRowIndex, this.PageSize, true);
			}
		}

		// Token: 0x17004C20 RID: 19488
		// (get) Token: 0x0600FC01 RID: 64513 RVA: 0x0038CAD9 File Offset: 0x0038ACD9
		protected internal override bool SupportsRenderingMode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600FC02 RID: 64514 RVA: 0x0038CAF4 File Offset: 0x0038ACF4
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			descriptor.AddProperty("_startRowIndex", this.StartRowIndex);
			descriptor.AddProperty("_pageSize", this.PageSize);
			descriptor.AddProperty("_totalRowCount", this.TotalRowCount);
			descriptor.AddProperty("_currentPageIndex", this.CurrentPageIndex);
			descriptor.AddProperty("_pageCount", this.PageCount);
			descriptor.AddProperty("_uniqueID", this.UniqueID);
			base.DescribeRenderMode(descriptor);
			if (this.EnableAriaSupport)
			{
				descriptor.AddProperty("_enableAriaSupport", this.EnableAriaSupport);
			}
			this.RegisterClientSideEvents(delegate(string eventName, string eventValue)
			{
				RadWebControl.DescribeEvent(descriptor, eventName, eventValue);
			});
		}

		// Token: 0x0600FC03 RID: 64515 RVA: 0x0038CC08 File Offset: 0x0038AE08
		private void RegisterClientSideEvents(TAction<string, string> eventData)
		{
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this.ClientEvents);
			foreach (object obj in properties)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
				if (!(propertyDescriptor.DisplayName == "ViewState"))
				{
					string text = propertyDescriptor.DisplayName.Replace("On", string.Empty);
					text = Regex.Replace(text, "^[A-Z]", (Match match) => match.ToString().ToLower());
					string text2 = propertyDescriptor.GetValue(this.ClientEvents).ToString();
					if (!string.IsNullOrEmpty(text2))
					{
						eventData(text, text2);
					}
				}
			}
		}

		// Token: 0x0600FC04 RID: 64516 RVA: 0x0038CCE4 File Offset: 0x0038AEE4
		public void RaisePostBackEvent(string eventArgument)
		{
			if (eventArgument.Contains("FireCommand:"))
			{
				this.HandleClientCommand(RadDataPager.parseFireCommandEventName(eventArgument), RadDataPager.parseFireCommandArgs(eventArgument));
			}
		}

		// Token: 0x0600FC05 RID: 64517 RVA: 0x0038CD10 File Offset: 0x0038AF10
		protected virtual void HandleClientCommand(string commandName, string commandArgs)
		{
			if (commandName != null)
			{
				if (!(commandName == "Page") && !(commandName == "PageSizeChange"))
				{
					return;
				}
				this.FireCommand(commandName, commandArgs);
			}
		}

		// Token: 0x17004C21 RID: 19489
		// (get) Token: 0x0600FC06 RID: 64518 RVA: 0x0038CD45 File Offset: 0x0038AF45
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		internal DataPagerStrings Localization
		{
			get
			{
				if (this._localization == null)
				{
					this._localization = new DataPagerStrings(new LocalizationProvider("RadDataPager.Main", this, this.LocalizationPath));
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._localization).TrackViewState();
					}
				}
				return this._localization;
			}
		}

		// Token: 0x17004C22 RID: 19490
		// (get) Token: 0x0600FC07 RID: 64519 RVA: 0x0038CD84 File Offset: 0x0038AF84
		// (set) Token: 0x0600FC08 RID: 64520 RVA: 0x0038CDA4 File Offset: 0x0038AFA4
		[DefaultValue(typeof(CultureInfo), "en-US")]
		[Category("Appearance")]
		[Description("The selected culture. Localization strings will be loaded based on this value.")]
		public CultureInfo Culture
		{
			get
			{
				return ((CultureInfo)this.ViewState["Culture"]) ?? CultureInfo.CurrentUICulture;
			}
			set
			{
				if (value != this.ViewState["Culture"])
				{
					this._localization = null;
				}
				this.ViewState["Culture"] = value;
			}
		}

		// Token: 0x17004C23 RID: 19491
		// (get) Token: 0x0600FC09 RID: 64521 RVA: 0x0038CDD1 File Offset: 0x0038AFD1
		// (set) Token: 0x0600FC0A RID: 64522 RVA: 0x0038CDF4 File Offset: 0x0038AFF4
		[DefaultValue("")]
		[Description("Gets or sets a value indicating where RadDataPager will look for its .resx localization files.")]
		[Category("Misc")]
		public string LocalizationPath
		{
			get
			{
				return ((string)this.ViewState["LocalizationPath"]) ?? string.Empty;
			}
			set
			{
				string text = value.Replace("\\", "/");
				if (text.Length > 0 && !text.EndsWith("/"))
				{
					text += "/";
				}
				this.ViewState["LocalizationPath"] = text;
			}
		}

		// Token: 0x04004796 RID: 18326
		public const string PageSizeChangeCommandName = "PageSizeChange";

		// Token: 0x04004797 RID: 18327
		public const string PageCommandName = "Page";

		// Token: 0x04004798 RID: 18328
		public const string PageNextCommandArgument = "Next";

		// Token: 0x04004799 RID: 18329
		public const string PagePrevCommandArgument = "Prev";

		// Token: 0x0400479A RID: 18330
		public const string PageFirstCommandArgument = "First";

		// Token: 0x0400479B RID: 18331
		public const string PageLastCommandArgument = "Last";

		// Token: 0x0400479C RID: 18332
		internal const string ClientPostbackFunctionFormat = "FireCommand:{0}|;{1}|;";

		// Token: 0x0400479D RID: 18333
		private static readonly object EventFieldCreating;

		// Token: 0x0400479E RID: 18334
		private static readonly object EventFieldCreated;

		// Token: 0x0400479F RID: 18335
		private static readonly object EventCommand;

		// Token: 0x040047A0 RID: 18336
		private static readonly object EventTotalRowCountRequest;

		// Token: 0x040047A1 RID: 18337
		private static readonly object EventPageIndexChanged;

		// Token: 0x040047A2 RID: 18338
		private PageableItemContainerLocator _containerLocator;

		// Token: 0x040047A3 RID: 18339
		private RadDataPagerFieldCollection _fields;

		// Token: 0x040047A4 RID: 18340
		private IRadPageableItemContainer _pageableItemContainer;

		// Token: 0x040047A5 RID: 18341
		private RadDataPagerClientEvents _clientEvents;

		// Token: 0x040047A6 RID: 18342
		private bool _isControlInitialized;

		// Token: 0x040047A7 RID: 18343
		private bool _isPagePropertiesSet;

		// Token: 0x040047A8 RID: 18344
		private bool _creatingPagerFieldsInProgress;

		// Token: 0x040047A9 RID: 18345
		private int _startRowIndex;

		// Token: 0x040047AA RID: 18346
		private int _totalRowCount;

		// Token: 0x040047AB RID: 18347
		private int _maximumRows;

		// Token: 0x040047AC RID: 18348
		private bool _seoPagingHandled;

		// Token: 0x040047AD RID: 18349
		private DataPagerStrings _localization;

		// Token: 0x040047AE RID: 18350
		internal int _defaultPageSize = 10;

		// Token: 0x040047AF RID: 18351
		private static TFunc<string, string> parseFireCommandArgs = delegate(string input)
		{
			string input2 = input.Split(new char[]
			{
				':'
			})[1];
			return new Regex("(\\|;)").Split(input2)[2];
		};

		// Token: 0x040047B0 RID: 18352
		private static TFunc<string, string> parseFireCommandEventName = delegate(string input)
		{
			string input2 = input.Split(new char[]
			{
				':'
			})[1];
			return new Regex("(\\|;)").Split(input2)[0];
		};
	}
}
