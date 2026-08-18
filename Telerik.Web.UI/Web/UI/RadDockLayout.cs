using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using Telerik.Licensing;
using Telerik.Web.UI.Common;
using Telerik.Web.UI.Dock;
using Telerik.Web.UI.PersistenceFramework;

namespace Telerik.Web.UI
{
	// Token: 0x02000FB6 RID: 4022
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Lightweight, typeof(RadDockLayout))]
	[PersistChildren(true)]
	[ToolboxData("<{0}:RadDockLayout Runat=\"server\"></{0}:RadDockLayout>")]
	[TelerikToolboxCategory("Container")]
	[ToolboxBitmap(typeof(RadDockLayout), "Telerik.Web.UI.Dock.png")]
	[Designer("Telerik.Web.Design.RadDockLayoutDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[EmbeddedSkin("Dock")]
	[EmbeddedSkin("Dock", "Default")]
	public class RadDockLayout : Control, IDockLayout, ISkinnableControl, IControl
	{
		// Token: 0x06009AEE RID: 39662 RVA: 0x00227EA8 File Offset: 0x002260A8
		protected override void OnInit(EventArgs e)
		{
			this.Page.RegisterViewStateHandler();
			this.Page.Init += this.Page_Init;
			this.Page.InitComplete += this.Page_InitComplete;
			this.Page.SaveStateComplete += this.Page_SaveStateComplete;
			this.Page.PreLoad += this.Page_PreLoad;
			base.OnInit(e);
		}

		// Token: 0x06009AEF RID: 39663 RVA: 0x00227F24 File Offset: 0x00226124
		protected void Page_Init(object sender, EventArgs e)
		{
			if (this.EnableLayoutPersistence && this.LayoutPersistenceRepositoryType != DockLayoutPersistenceRepository.None)
			{
				this.SaveDockLayout += this.RadDockLayout_SaveDockLayout;
				this.LoadDockLayout += this.RadDockLayout_LoadDockLayout;
				this.storageProvider = this.GetStorageProvider();
			}
		}

		// Token: 0x06009AF0 RID: 39664 RVA: 0x00227F74 File Offset: 0x00226174
		protected void RadDockLayout_SaveDockLayout(object sender, DockLayoutEventArgs e)
		{
			string text = string.Empty;
			JavaScriptSerializer serializer = this.GetSerializer();
			foreach (DockState obj in this.GetRegisteredDocksState())
			{
				text = text + serializer.Serialize(obj) + "***";
			}
			this.storageProvider.SaveStateToStorage(this.LayoutRepositoryID, text);
		}

		// Token: 0x06009AF1 RID: 39665 RVA: 0x00227FF4 File Offset: 0x002261F4
		protected void RadDockLayout_LoadDockLayout(object sender, DockLayoutEventArgs e)
		{
			JavaScriptSerializer serializer = this.GetSerializer();
			string text = string.Empty;
			try
			{
				text = this.storageProvider.LoadStateFromStorage(this.LayoutRepositoryID);
			}
			catch (Exception)
			{
			}
			foreach (string input in text.Split(new string[]
			{
				"***"
			}, StringSplitOptions.RemoveEmptyEntries))
			{
				DockState dockState = serializer.Deserialize<DockState>(input);
				RadDock radDock = this.FindControl(dockState.UniqueName) as RadDock;
				if (radDock != null)
				{
					radDock.ApplyState(dockState);
					e.Positions[dockState.UniqueName] = dockState.DockZoneID;
					e.Indices[dockState.UniqueName] = dockState.Index;
				}
			}
		}

		// Token: 0x06009AF2 RID: 39666 RVA: 0x002280C0 File Offset: 0x002262C0
		private JavaScriptSerializer GetSerializer()
		{
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			javaScriptSerializer.RegisterConverters(new List<JavaScriptConverter>
			{
				new UnitConverter()
			});
			return javaScriptSerializer;
		}

		// Token: 0x06009AF3 RID: 39667 RVA: 0x002280EC File Offset: 0x002262EC
		private IStateStorageProvider GetStorageProvider()
		{
			IStateStorageProvider result = null;
			switch (this.LayoutPersistenceRepositoryType)
			{
			case DockLayoutPersistenceRepository.Cookies:
				result = new CookieStateStorageProvider();
				break;
			case DockLayoutPersistenceRepository.FileSystem:
			{
				string stateFileLocation = HttpContext.Current.Server.MapPath("~/App_Data/");
				result = new AppDataStorageProvider(stateFileLocation);
				break;
			}
			case DockLayoutPersistenceRepository.Custom:
				result = this.StorageProvider;
				break;
			}
			return result;
		}

		// Token: 0x06009AF4 RID: 39668 RVA: 0x0022814C File Offset: 0x0022634C
		private void Page_InitComplete(object sender, EventArgs e)
		{
			DockLayoutEventArgs dockLayoutEventArgs = new DockLayoutEventArgs(new Dictionary<string, string>(), new Dictionary<string, int>());
			this.OnLoadDockLayout(dockLayoutEventArgs);
			this.SetRegisteredDockParents(dockLayoutEventArgs.Positions, dockLayoutEventArgs.Indices);
		}

		// Token: 0x06009AF5 RID: 39669 RVA: 0x00228182 File Offset: 0x00226382
		protected override void LoadViewState(object savedState)
		{
			base.LoadViewState(savedState);
			if (this.StoreLayoutInViewState)
			{
				this.SetRegisteredDockParents(this.StoredPositions, this.StoredIndices);
			}
		}

		// Token: 0x06009AF6 RID: 39670 RVA: 0x002281A8 File Offset: 0x002263A8
		private void Dock_DockPositionChanged(object sender, DockPositionChangedEventArgs e)
		{
			string uniqueName = (sender as RadDock).GetUniqueName();
			this._clientPositions[uniqueName] = e.DockZoneID;
			this._clientIndices[uniqueName] = e.Index;
		}

		// Token: 0x06009AF7 RID: 39671 RVA: 0x002281E5 File Offset: 0x002263E5
		private void Page_PreLoad(object sender, EventArgs e)
		{
			this.SetRegisteredDockParents(this._clientPositions, this._clientIndices);
		}

		// Token: 0x06009AF8 RID: 39672 RVA: 0x002281FC File Offset: 0x002263FC
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (!string.IsNullOrEmpty(this.Skin))
			{
				this._registeredDocks.ForEach(new Action<RadDock>(this.SetSkinProperties));
				this._registeredZones.ForEach(new Action<RadDockZone>(this.SetSkinProperties));
			}
		}

		// Token: 0x06009AF9 RID: 39673 RVA: 0x0022824C File Offset: 0x0022644C
		protected override void RenderChildren(HtmlTextWriter writer)
		{
			base.RenderChildren(writer);
			foreach (RadDock radDock in this._registeredDocks)
			{
				if (radDock.Parent is RadDockZone && string.IsNullOrEmpty(radDock.DockZoneID))
				{
					radDock.RenderControlAlways(writer);
				}
			}
		}

		// Token: 0x06009AFA RID: 39674 RVA: 0x002282C0 File Offset: 0x002264C0
		private void SetSkinProperties(ISkinnableControl control)
		{
			if (control.Skin == "Default")
			{
				control.Skin = this.Skin;
				control.EnableEmbeddedSkins = this.EnableEmbeddedSkins;
				control.EnableEmbeddedBaseStylesheet = this.EnableEmbeddedBaseStylesheet;
			}
		}

		// Token: 0x06009AFB RID: 39675 RVA: 0x002282F8 File Offset: 0x002264F8
		protected override object SaveViewState()
		{
			if (this.StoreLayoutInViewState)
			{
				this.StoredPositions = this.GetRegisteredDocksParents();
				this.StoredIndices = this.GetRegisteredDocksIndices();
			}
			return base.SaveViewState();
		}

		// Token: 0x06009AFC RID: 39676 RVA: 0x00228320 File Offset: 0x00226520
		private void Page_SaveStateComplete(object sender, EventArgs e)
		{
			Dictionary<string, string> registeredDocksParents = this.GetRegisteredDocksParents();
			Dictionary<string, int> registeredDocksIndices = this.GetRegisteredDocksIndices();
			this.OnSaveDockLayout(new DockLayoutEventArgs(registeredDocksParents, registeredDocksIndices));
		}

		// Token: 0x06009AFD RID: 39677 RVA: 0x002283B4 File Offset: 0x002265B4
		public void SetRegisteredDockParents(Dictionary<string, string> parents, Dictionary<string, int> indices)
		{
			List<string> uniqueNames = new List<string>();
			foreach (RadDock radDock in this._registeredDocks)
			{
				string key = this.EnsureUniqueName(radDock, uniqueNames);
				if (parents.ContainsKey(key))
				{
					string text = parents[key];
					if (text != radDock.DockZoneID)
					{
						this.SetDockParent(radDock, text);
					}
				}
			}
			foreach (RadDockZone radDockZone in this._registeredZones)
			{
				radDockZone.Docks.Sort(delegate(RadDock dock1, RadDock dock2)
				{
					string uniqueName = dock1.GetUniqueName();
					string uniqueName2 = dock2.GetUniqueName();
					int num = indices.ContainsKey(uniqueName) ? indices[uniqueName] : dock1.Index;
					int num2 = indices.ContainsKey(uniqueName2) ? indices[uniqueName2] : dock2.Index;
					return num - num2;
				});
			}
		}

		// Token: 0x06009AFE RID: 39678 RVA: 0x002284C4 File Offset: 0x002266C4
		protected virtual void SetDockParent(RadDock dock, string newParentClientID)
		{
			dock.Undock();
			RadDockZone radDockZone = this._registeredZones.Find((RadDockZone zone) => zone.ClientID == newParentClientID);
			if (radDockZone != null)
			{
				dock.Dock(radDockZone);
			}
		}

		// Token: 0x06009AFF RID: 39679 RVA: 0x00228544 File Offset: 0x00226744
		protected virtual Dictionary<string, string> GetRegisteredDocksParents()
		{
			Dictionary<string, string> parents = new Dictionary<string, string>();
			List<string> uniqueNames = new List<string>();
			this._registeredDocks.ForEach(delegate(RadDock dock)
			{
				string key = this.EnsureUniqueName(dock, uniqueNames);
				parents[key] = dock.DockZoneID;
			});
			return parents;
		}

		// Token: 0x06009B00 RID: 39680 RVA: 0x002285D0 File Offset: 0x002267D0
		protected virtual Dictionary<string, int> GetRegisteredDocksIndices()
		{
			Dictionary<string, int> indices = new Dictionary<string, int>();
			List<string> uniqueNames = new List<string>();
			this._registeredDocks.ForEach(delegate(RadDock dock)
			{
				string key = this.EnsureUniqueName(dock, uniqueNames);
				indices[key] = dock.Index;
			});
			return indices;
		}

		// Token: 0x06009B01 RID: 39681 RVA: 0x00228648 File Offset: 0x00226848
		public List<DockState> GetRegisteredDocksState(bool omitClosedDocks)
		{
			List<DockState> states = new List<DockState>();
			this._registeredDocks.ForEach(delegate(RadDock dock)
			{
				if (!omitClosedDocks || !dock.Closed)
				{
					states.Add(dock.GetState());
				}
			});
			return states;
		}

		// Token: 0x1700310E RID: 12558
		// (get) Token: 0x06009B02 RID: 39682 RVA: 0x0022868A File Offset: 0x0022688A
		[Description("Defines the collection of registered docks with this RadDockLayout control.")]
		public ReadOnlyCollection<RadDock> RegisteredDocks
		{
			get
			{
				return this._registeredDocks.AsReadOnly();
			}
		}

		// Token: 0x1700310F RID: 12559
		// (get) Token: 0x06009B03 RID: 39683 RVA: 0x00228697 File Offset: 0x00226897
		[Description("Defines the collection of registered dock zones with this RadDockLayout control.")]
		public ReadOnlyCollection<RadDockZone> RegisteredZones
		{
			get
			{
				return this._registeredZones.AsReadOnly();
			}
		}

		// Token: 0x06009B04 RID: 39684 RVA: 0x002286A4 File Offset: 0x002268A4
		public List<DockState> GetRegisteredDocksState()
		{
			return this.GetRegisteredDocksState(false);
		}

		// Token: 0x14000172 RID: 370
		// (add) Token: 0x06009B05 RID: 39685 RVA: 0x002286AD File Offset: 0x002268AD
		// (remove) Token: 0x06009B06 RID: 39686 RVA: 0x002286C0 File Offset: 0x002268C0
		public event DockLayoutEventHandler LoadDockLayout
		{
			add
			{
				base.Events.AddHandler(RadDockLayout.LoadDockLayoutEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadDockLayout.LoadDockLayoutEvent, value);
			}
		}

		// Token: 0x06009B07 RID: 39687 RVA: 0x002286D4 File Offset: 0x002268D4
		protected virtual void OnLoadDockLayout(DockLayoutEventArgs e)
		{
			DockLayoutEventHandler dockLayoutEventHandler = (DockLayoutEventHandler)base.Events[RadDockLayout.LoadDockLayoutEvent];
			if (dockLayoutEventHandler != null)
			{
				dockLayoutEventHandler(this, e);
			}
		}

		// Token: 0x14000173 RID: 371
		// (add) Token: 0x06009B08 RID: 39688 RVA: 0x00228702 File Offset: 0x00226902
		// (remove) Token: 0x06009B09 RID: 39689 RVA: 0x00228715 File Offset: 0x00226915
		public event DockLayoutEventHandler SaveDockLayout
		{
			add
			{
				base.Events.AddHandler(RadDockLayout.SaveDockLayoutEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadDockLayout.SaveDockLayoutEvent, value);
			}
		}

		// Token: 0x06009B0A RID: 39690 RVA: 0x00228728 File Offset: 0x00226928
		protected virtual void OnSaveDockLayout(DockLayoutEventArgs e)
		{
			DockLayoutEventHandler dockLayoutEventHandler = (DockLayoutEventHandler)base.Events[RadDockLayout.SaveDockLayoutEvent];
			if (dockLayoutEventHandler != null)
			{
				dockLayoutEventHandler(this, e);
			}
		}

		// Token: 0x06009B0B RID: 39691 RVA: 0x00228758 File Offset: 0x00226958
		protected virtual string EnsureUniqueName(RadDock dock, List<string> uniqueNames)
		{
			string uniqueName = dock.GetUniqueName();
			if (!uniqueNames.Contains(uniqueName))
			{
				uniqueNames.Add(uniqueName);
				return uniqueName;
			}
			if (string.IsNullOrEmpty(dock.UniqueName))
			{
				throw new InvalidOperationException(string.Format("The ID of RadDock with ID='{0}' is not unique to RadDockLayout with ID='{1}'. Please, set the UniqueName property of RadDock with ID='{0}' with a value, which is unique to RadDockLayout with ID='{1}'.", dock.ID, this.ID));
			}
			throw new InvalidOperationException(string.Format("Please, ensure that the UniqueName property of RadDock with ID='{0}' is unique to RadDockLayout with ID='{1}'.", dock.ID, this.ID));
		}

		// Token: 0x06009B0C RID: 39692 RVA: 0x002287C4 File Offset: 0x002269C4
		void IDockLayout.RegisterDock(RadDock dock)
		{
			if (this._registeredDocks.Contains(dock))
			{
				throw new InvalidOperationException(string.Format("RadDock with ID='{0}' is already registered in RadDockLayout with ID='{1}'", dock.ID, this.ID));
			}
			this._registeredDocks.Add(dock);
			dock.DockPositionChanged += this.Dock_DockPositionChanged;
		}

		// Token: 0x06009B0D RID: 39693 RVA: 0x00228819 File Offset: 0x00226A19
		void IDockLayout.RegisterDockZone(RadDockZone zone)
		{
			if (this._registeredZones.Contains(zone))
			{
				throw new InvalidOperationException(string.Format("RadDockZone with ID='{0}' is already registered in RadDockLayout with ID='{1}'", zone.ID, this.ID));
			}
			this._registeredZones.Add(zone);
		}

		// Token: 0x06009B0E RID: 39694 RVA: 0x00228851 File Offset: 0x00226A51
		void IDockLayout.SetDockParent(RadDock dock, string newParentClientID)
		{
			this.SetDockParent(dock, newParentClientID);
		}

		// Token: 0x06009B0F RID: 39695 RVA: 0x0022885B File Offset: 0x00226A5B
		void IDockLayout.UnRegisterDock(RadDock dock)
		{
			if (this._registeredDocks.Contains(dock))
			{
				this._registeredDocks.Remove(dock);
			}
		}

		// Token: 0x06009B10 RID: 39696 RVA: 0x00228878 File Offset: 0x00226A78
		void IDockLayout.UnRegisterDockZone(RadDockZone zone)
		{
			if (this._registeredZones.Contains(zone))
			{
				this._registeredZones.Remove(zone);
			}
		}

		// Token: 0x17003110 RID: 12560
		// (get) Token: 0x06009B11 RID: 39697 RVA: 0x00228898 File Offset: 0x00226A98
		// (set) Token: 0x06009B12 RID: 39698 RVA: 0x002288C8 File Offset: 0x00226AC8
		private Dictionary<string, string> StoredPositions
		{
			get
			{
				JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
				return javaScriptSerializer.Deserialize<Dictionary<string, string>>((string)this.ViewState["StoredPositions"]);
			}
			set
			{
				JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
				string value2 = javaScriptSerializer.Serialize(value);
				this.ViewState["StoredPositions"] = value2;
			}
		}

		// Token: 0x17003111 RID: 12561
		// (get) Token: 0x06009B13 RID: 39699 RVA: 0x002288F4 File Offset: 0x00226AF4
		// (set) Token: 0x06009B14 RID: 39700 RVA: 0x00228924 File Offset: 0x00226B24
		private Dictionary<string, int> StoredIndices
		{
			get
			{
				JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
				return javaScriptSerializer.Deserialize<Dictionary<string, int>>((string)this.ViewState["StoredIndices"]);
			}
			set
			{
				JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
				string value2 = javaScriptSerializer.Serialize(value);
				this.ViewState["StoredIndices"] = value2;
			}
		}

		// Token: 0x06009B15 RID: 39701 RVA: 0x00228950 File Offset: 0x00226B50
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public List<string> GetEmbeddedSkinNames()
		{
			return SkinRegistrar.GetEmbeddedSkinNames(base.GetType());
		}

		// Token: 0x06009B16 RID: 39702 RVA: 0x0022895D File Offset: 0x00226B5D
		void IControl.DescribeComponent(IScriptDescriptor descriptor)
		{
		}

		// Token: 0x06009B17 RID: 39703 RVA: 0x0022895F File Offset: 0x00226B5F
		void IControl.EnsureChildControlsCreated()
		{
			this.EnsureChildControls();
		}

		// Token: 0x17003112 RID: 12562
		// (get) Token: 0x06009B18 RID: 39704 RVA: 0x00228967 File Offset: 0x00226B67
		// (set) Token: 0x06009B19 RID: 39705 RVA: 0x0022896A File Offset: 0x00226B6A
		bool IControl.RegisterWithScriptManager
		{
			get
			{
				return true;
			}
			set
			{
			}
		}

		// Token: 0x17003113 RID: 12563
		// (get) Token: 0x06009B1A RID: 39706 RVA: 0x0022896C File Offset: 0x00226B6C
		// (set) Token: 0x06009B1B RID: 39707 RVA: 0x0022896F File Offset: 0x00226B6F
		bool ISkinnableControl.EnableEmbeddedScripts
		{
			get
			{
				return true;
			}
			set
			{
			}
		}

		// Token: 0x06009B1C RID: 39708 RVA: 0x00228971 File Offset: 0x00226B71
		public RenderMode PreferredRenderMode(RenderModeBrowserAdaptor browser)
		{
			return RenderMode.Classic;
		}

		// Token: 0x06009B1D RID: 39709 RVA: 0x00228974 File Offset: 0x00226B74
		public string GetSkinSuffix()
		{
			return "";
		}

		// Token: 0x17003114 RID: 12564
		// (get) Token: 0x06009B1E RID: 39710 RVA: 0x0022897B File Offset: 0x00226B7B
		// (set) Token: 0x06009B1F RID: 39711 RVA: 0x00228983 File Offset: 0x00226B83
		string ISkinnableControl.AjaxCssRegistrations
		{
			get
			{
				return this._ajaxCssRegistrations;
			}
			set
			{
				this._ajaxCssRegistrations = value;
			}
		}

		// Token: 0x17003115 RID: 12565
		// (get) Token: 0x06009B20 RID: 39712 RVA: 0x0022898C File Offset: 0x00226B8C
		// (set) Token: 0x06009B21 RID: 39713 RVA: 0x00228994 File Offset: 0x00226B94
		bool ISkinnableControl.EnableAjaxSkinRendering
		{
			get
			{
				return this.EnableAjaxSkinRendering;
			}
			set
			{
				this.EnableAjaxSkinRendering = value;
			}
		}

		// Token: 0x17003116 RID: 12566
		// (get) Token: 0x06009B22 RID: 39714 RVA: 0x0022899D File Offset: 0x00226B9D
		// (set) Token: 0x06009B23 RID: 39715 RVA: 0x002289A5 File Offset: 0x00226BA5
		RenderMode ISkinnableControl.RenderMode
		{
			get
			{
				return this.RenderingMode;
			}
			set
			{
				this.RenderingMode = value;
			}
		}

		// Token: 0x17003117 RID: 12567
		// (get) Token: 0x06009B24 RID: 39716 RVA: 0x002289B0 File Offset: 0x00226BB0
		RenderMode ISkinnableControl.ResolvedRenderMode
		{
			get
			{
				RenderMode renderMode = ((ISkinnableControl)this).RenderMode;
				if (renderMode == RenderMode.Auto)
				{
					renderMode = this.PreferredRenderMode(RenderModeBrowserAdaptor.Instance);
				}
				return renderMode;
			}
		}

		// Token: 0x17003118 RID: 12568
		// (get) Token: 0x06009B25 RID: 39717 RVA: 0x002289D4 File Offset: 0x00226BD4
		// (set) Token: 0x06009B26 RID: 39718 RVA: 0x002289FF File Offset: 0x00226BFF
		[Description("Specifies whether the RadDockLayout will store the positions of its inner docks in the ViewState.")]
		public bool StoreLayoutInViewState
		{
			get
			{
				return this.ViewState["StoreLayoutInViewState"] == null || (bool)this.ViewState["StoreLayoutInViewState"];
			}
			set
			{
				this.ViewState["StoreLayoutInViewState"] = value;
			}
		}

		// Token: 0x17003119 RID: 12569
		// (get) Token: 0x06009B27 RID: 39719 RVA: 0x00228A17 File Offset: 0x00226C17
		// (set) Token: 0x06009B28 RID: 39720 RVA: 0x00228A46 File Offset: 0x00226C46
		[TypeConverter("Telerik.Web.Design.SkinTypeConverter, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
		[Description("Gets or sets the skin name for the child controls' user interface.")]
		[DefaultValue("")]
		[Category("Appearance")]
		public string Skin
		{
			get
			{
				if (this.ViewState["Skin"] == null)
				{
					return string.Empty;
				}
				return (string)this.ViewState["Skin"];
			}
			set
			{
				this.ViewState["Skin"] = value;
			}
		}

		// Token: 0x1700311A RID: 12570
		// (get) Token: 0x06009B29 RID: 39721 RVA: 0x00228A59 File Offset: 0x00226C59
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public bool IsSkinSet
		{
			get
			{
				return this.ViewState["Skin"] != null;
			}
		}

		// Token: 0x1700311B RID: 12571
		// (get) Token: 0x06009B2A RID: 39722 RVA: 0x00228A71 File Offset: 0x00226C71
		// (set) Token: 0x06009B2B RID: 39723 RVA: 0x00228AA1 File Offset: 0x00226CA1
		[Category("Appearance")]
		[Description("Gets or sets the value, indicating whether to render links to the embedded skins or not.")]
		[DefaultValue(true)]
		public bool EnableEmbeddedSkins
		{
			get
			{
				if (this.ViewState["EnableEmbeddedSkins"] == null)
				{
					return BaseClass.GetGlobalEnableEmbeddedSkins(this);
				}
				return (bool)this.ViewState["EnableEmbeddedSkins"];
			}
			set
			{
				this.ViewState["EnableEmbeddedSkins"] = value;
			}
		}

		// Token: 0x1700311C RID: 12572
		// (get) Token: 0x06009B2C RID: 39724 RVA: 0x00228AB9 File Offset: 0x00226CB9
		// (set) Token: 0x06009B2D RID: 39725 RVA: 0x00228AE4 File Offset: 0x00226CE4
		[DefaultValue(true)]
		[Description("Specifies whether to register the base control skin file automatically.")]
		[Category("Appearance")]
		public virtual bool EnableEmbeddedBaseStylesheet
		{
			get
			{
				return this.ViewState["EnableEmbeddedBaseStylesheet"] == null || (bool)this.ViewState["EnableEmbeddedBaseStylesheet"];
			}
			set
			{
				this.ViewState["EnableEmbeddedBaseStylesheet"] = value;
			}
		}

		// Token: 0x1700311D RID: 12573
		// (get) Token: 0x06009B2E RID: 39726 RVA: 0x00228AFC File Offset: 0x00226CFC
		// (set) Token: 0x06009B2F RID: 39727 RVA: 0x00228B27 File Offset: 0x00226D27
		[DefaultValue(true)]
		[Description("Specifies whether to register the skin CSS during Ajax requests.")]
		[Category("Appearance")]
		public virtual bool EnableAjaxSkinRendering
		{
			get
			{
				return this.ViewState["EnableAjaxSkinRendering"] == null || (bool)this.ViewState["EnableAjaxSkinRendering"];
			}
			set
			{
				this.ViewState["EnableAjaxSkinRendering"] = value;
			}
		}

		// Token: 0x1700311E RID: 12574
		// (get) Token: 0x06009B30 RID: 39728 RVA: 0x00228B3F File Offset: 0x00226D3F
		// (set) Token: 0x06009B31 RID: 39729 RVA: 0x00228B61 File Offset: 0x00226D61
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[Description("Gets or sets the StateStorageProvider instance that will be used for the built-in state storing.")]
		public IStateStorageProvider StorageProvider
		{
			get
			{
				if (object.Equals(null, this.storageProvider))
				{
					this.storageProvider = this.GetStorageProvider();
				}
				return this.storageProvider;
			}
			set
			{
				this.storageProvider = value;
			}
		}

		// Token: 0x1700311F RID: 12575
		// (get) Token: 0x06009B32 RID: 39730 RVA: 0x00228B6A File Offset: 0x00226D6A
		// (set) Token: 0x06009B33 RID: 39731 RVA: 0x00228B8B File Offset: 0x00226D8B
		[Description("Specifies whether the built-in state storing should be enabled.")]
		[DefaultValue(false)]
		public bool EnableLayoutPersistence
		{
			get
			{
				return (bool)(this.ViewState["EnableLayoutPersistence"] ?? false);
			}
			set
			{
				this.ViewState["EnableLayoutPersistence"] = value;
			}
		}

		// Token: 0x17003120 RID: 12576
		// (get) Token: 0x06009B34 RID: 39732 RVA: 0x00228BA3 File Offset: 0x00226DA3
		// (set) Token: 0x06009B35 RID: 39733 RVA: 0x00228BC4 File Offset: 0x00226DC4
		[DefaultValue(DockLayoutPersistenceRepository.None)]
		[Description("Gets or sets the type of the data repository to be used for storing the state.")]
		public DockLayoutPersistenceRepository LayoutPersistenceRepositoryType
		{
			get
			{
				return (DockLayoutPersistenceRepository)(this.ViewState["LayoutPersistenceRepositoryType"] ?? DockLayoutPersistenceRepository.None);
			}
			set
			{
				this.ViewState["LayoutPersistenceRepositoryType"] = value;
			}
		}

		// Token: 0x17003121 RID: 12577
		// (get) Token: 0x06009B36 RID: 39734 RVA: 0x00228BDC File Offset: 0x00226DDC
		// (set) Token: 0x06009B37 RID: 39735 RVA: 0x00228BFC File Offset: 0x00226DFC
		[Description("Gets or sets the key identifier of the stored RadDocks' states.")]
		[DefaultValue("RadDockLayout")]
		public string LayoutRepositoryID
		{
			get
			{
				return (string)(this.ViewState["LayoutRepositoryID"] ?? "RadDockLayout");
			}
			set
			{
				this.ViewState["LayoutRepositoryID"] = value;
			}
		}

		// Token: 0x17003122 RID: 12578
		// (get) Token: 0x06009B38 RID: 39736 RVA: 0x00228C0F File Offset: 0x00226E0F
		// (set) Token: 0x06009B39 RID: 39737 RVA: 0x00228C30 File Offset: 0x00226E30
		[NotifyParentProperty(true)]
		[Description("Specifies the rendering mode of the control")]
		[DefaultValue(RenderMode.Classic)]
		[Category("Appearance")]
		[ClientControlProperty]
		public virtual RenderMode RenderingMode
		{
			get
			{
				return (RenderMode)(this.ViewState["RenderingMode"] ?? RenderMode.Classic);
			}
			set
			{
				this.ViewState["RenderingMode"] = value;
			}
		}

		// Token: 0x06009B3B RID: 39739 RVA: 0x00228C48 File Offset: 0x00226E48
		// Note: this type is marked as 'beforefieldinit'.
		static RadDockLayout()
		{
			RadDockLayout.LoadDockLayoutEvent = new object();
			RadDockLayout.SaveDockLayoutEvent = new object();
		}

		// Token: 0x04002BD4 RID: 11220
		private IStateStorageProvider storageProvider;

		// Token: 0x04002BD7 RID: 11223
		private Dictionary<string, string> _clientPositions = new Dictionary<string, string>();

		// Token: 0x04002BD8 RID: 11224
		private Dictionary<string, int> _clientIndices = new Dictionary<string, int>();

		// Token: 0x04002BD9 RID: 11225
		private List<RadDock> _registeredDocks = new List<RadDock>();

		// Token: 0x04002BDA RID: 11226
		private List<RadDockZone> _registeredZones = new List<RadDockZone>();

		// Token: 0x04002BDB RID: 11227
		private string _ajaxCssRegistrations;
	}
}
