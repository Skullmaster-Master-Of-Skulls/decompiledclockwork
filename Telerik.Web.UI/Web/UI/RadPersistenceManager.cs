using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.UI;
using Telerik.Licensing;
using Telerik.Web.UI.PersistenceFramework;

namespace Telerik.Web.UI
{
	// Token: 0x02000495 RID: 1173
	[ToolboxData("<{0}:RadPersistenceManager runat=\"server\"></{0}:RadPersistenceManager>")]
	[ToolboxBitmap(typeof(RadDropDownTree), "Telerik.Web.UI.PersistenceManager.png")]
	[Designer("Telerik.Web.Design.RadPersistenceManagerDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[ParseChildren(true)]
	[PersistChildren(false)]
	[TelerikToolboxCategory("Data")]
	public class RadPersistenceManager : Control
	{
		// Token: 0x17000D68 RID: 3432
		// (get) Token: 0x06002982 RID: 10626 RVA: 0x00085983 File Offset: 0x00083B83
		internal RadTreeViewStatePersister TreeViewStatePersister
		{
			get
			{
				if (this.treeViewStatePersister == null)
				{
					this.treeViewStatePersister = new RadTreeViewStatePersister();
				}
				return this.treeViewStatePersister;
			}
		}

		// Token: 0x17000D69 RID: 3433
		// (get) Token: 0x06002983 RID: 10627 RVA: 0x0008599E File Offset: 0x00083B9E
		internal RadButtonStatePersister ButtonStatePersister
		{
			get
			{
				if (this.buttonStatePersister == null)
				{
					this.buttonStatePersister = new RadButtonStatePersister();
				}
				return this.buttonStatePersister;
			}
		}

		// Token: 0x17000D6A RID: 3434
		// (get) Token: 0x06002984 RID: 10628 RVA: 0x000859B9 File Offset: 0x00083BB9
		internal RadSliderStatePersister SliderStatePersister
		{
			get
			{
				if (this.sliderStatePersister == null)
				{
					this.sliderStatePersister = new RadSliderStatePersister();
				}
				return this.sliderStatePersister;
			}
		}

		// Token: 0x17000D6B RID: 3435
		// (get) Token: 0x06002985 RID: 10629 RVA: 0x000859D4 File Offset: 0x00083BD4
		internal RadPaneStatePersister PaneStatePersister
		{
			get
			{
				if (this.paneStatePersister == null)
				{
					this.paneStatePersister = new RadPaneStatePersister();
				}
				return this.paneStatePersister;
			}
		}

		// Token: 0x17000D6C RID: 3436
		// (get) Token: 0x06002986 RID: 10630 RVA: 0x000859EF File Offset: 0x00083BEF
		internal RadSlidingPaneStatePersister SlidingPaneStatePersister
		{
			get
			{
				if (this.slidingPaneStatePersister == null)
				{
					this.slidingPaneStatePersister = new RadSlidingPaneStatePersister();
				}
				return this.slidingPaneStatePersister;
			}
		}

		// Token: 0x17000D6D RID: 3437
		// (get) Token: 0x06002987 RID: 10631 RVA: 0x00085A0A File Offset: 0x00083C0A
		internal RadSlidingZoneStatePersister SlidingZoneStatePersister
		{
			get
			{
				if (this.slidingZoneStatePersister == null)
				{
					this.slidingZoneStatePersister = new RadSlidingZoneStatePersister();
				}
				return this.slidingZoneStatePersister;
			}
		}

		// Token: 0x17000D6E RID: 3438
		// (get) Token: 0x06002988 RID: 10632 RVA: 0x00085A25 File Offset: 0x00083C25
		internal RadDockStatePersister DockStatePersister
		{
			get
			{
				if (this.dockStatePersister == null)
				{
					this.dockStatePersister = new RadDockStatePersister();
				}
				return this.dockStatePersister;
			}
		}

		// Token: 0x17000D6F RID: 3439
		// (get) Token: 0x06002989 RID: 10633 RVA: 0x00085A40 File Offset: 0x00083C40
		internal RadColorPickerStatePersister ColorPickerStatePersister
		{
			get
			{
				if (this.colorPickerStatePersister == null)
				{
					this.colorPickerStatePersister = new RadColorPickerStatePersister();
				}
				return this.colorPickerStatePersister;
			}
		}

		// Token: 0x17000D70 RID: 3440
		// (get) Token: 0x0600298A RID: 10634 RVA: 0x00085A5B File Offset: 0x00083C5B
		internal RadComboBoxStatePersister ComboBoxStatePersister
		{
			get
			{
				if (this.comboBoxStatePersister == null)
				{
					this.comboBoxStatePersister = new RadComboBoxStatePersister();
				}
				return this.comboBoxStatePersister;
			}
		}

		// Token: 0x17000D71 RID: 3441
		// (get) Token: 0x0600298B RID: 10635 RVA: 0x00085A76 File Offset: 0x00083C76
		internal RadDropDownListStatePersister DropDownListStatePersister
		{
			get
			{
				if (this.dropDownListStatePersister == null)
				{
					this.dropDownListStatePersister = new RadDropDownListStatePersister();
				}
				return this.dropDownListStatePersister;
			}
		}

		// Token: 0x17000D72 RID: 3442
		// (get) Token: 0x0600298C RID: 10636 RVA: 0x00085A91 File Offset: 0x00083C91
		internal RadListBoxStatePersister ListBoxStatePersister
		{
			get
			{
				if (this.listBoxStatePersister == null)
				{
					this.listBoxStatePersister = new RadListBoxStatePersister();
				}
				return this.listBoxStatePersister;
			}
		}

		// Token: 0x17000D73 RID: 3443
		// (get) Token: 0x0600298D RID: 10637 RVA: 0x00085AAC File Offset: 0x00083CAC
		internal RadMenuStatePersister MenuStatePersister
		{
			get
			{
				if (this.menuStatePersister == null)
				{
					this.menuStatePersister = new RadMenuStatePersister();
				}
				return this.menuStatePersister;
			}
		}

		// Token: 0x17000D74 RID: 3444
		// (get) Token: 0x0600298E RID: 10638 RVA: 0x00085AC7 File Offset: 0x00083CC7
		internal RadOrgChartStatePersister OrgChartStatePersister
		{
			get
			{
				if (this.orgChartStatePersister == null)
				{
					this.orgChartStatePersister = new RadOrgChartStatePersister();
				}
				return this.orgChartStatePersister;
			}
		}

		// Token: 0x17000D75 RID: 3445
		// (get) Token: 0x0600298F RID: 10639 RVA: 0x00085AE2 File Offset: 0x00083CE2
		internal RadPanelBarStatePersister PanelBarStatePersister
		{
			get
			{
				if (this.panelBarStatePersister == null)
				{
					this.panelBarStatePersister = new RadPanelBarStatePersister();
				}
				return this.panelBarStatePersister;
			}
		}

		// Token: 0x17000D76 RID: 3446
		// (get) Token: 0x06002990 RID: 10640 RVA: 0x00085AFD File Offset: 0x00083CFD
		internal RadSchedulerStatePersister SchedulerStatePersister
		{
			get
			{
				if (this.schedulerStatePersister == null)
				{
					this.schedulerStatePersister = new RadSchedulerStatePersister();
				}
				return this.schedulerStatePersister;
			}
		}

		// Token: 0x17000D77 RID: 3447
		// (get) Token: 0x06002991 RID: 10641 RVA: 0x00085B18 File Offset: 0x00083D18
		internal RadTabStripStatePersister TabStripStatePersister
		{
			get
			{
				if (this.tabStripStatePersister == null)
				{
					this.tabStripStatePersister = new RadTabStripStatePersister();
				}
				return this.tabStripStatePersister;
			}
		}

		// Token: 0x17000D78 RID: 3448
		// (get) Token: 0x06002992 RID: 10642 RVA: 0x00085B33 File Offset: 0x00083D33
		internal RadGridStatePersister GridStatePersister
		{
			get
			{
				if (this.gridStatePersister == null)
				{
					this.gridStatePersister = new RadGridStatePersister();
				}
				return this.gridStatePersister;
			}
		}

		// Token: 0x17000D79 RID: 3449
		// (get) Token: 0x06002993 RID: 10643 RVA: 0x00085B4E File Offset: 0x00083D4E
		internal RadCalendarStatePersister CalendarStatePersister
		{
			get
			{
				if (this.calendarStatePersister == null)
				{
					this.calendarStatePersister = new RadCalendarStatePersister();
				}
				return this.calendarStatePersister;
			}
		}

		// Token: 0x17000D7A RID: 3450
		// (get) Token: 0x06002994 RID: 10644 RVA: 0x00085B69 File Offset: 0x00083D69
		internal RadListViewStatePersister ListViewStatePersister
		{
			get
			{
				if (this.listViewStatePersister == null)
				{
					this.listViewStatePersister = new RadListViewStatePersister();
				}
				return this.listViewStatePersister;
			}
		}

		// Token: 0x17000D7B RID: 3451
		// (get) Token: 0x06002995 RID: 10645 RVA: 0x00085B84 File Offset: 0x00083D84
		internal RadSkinManagerStatePersister SkinManagerStatePersister
		{
			get
			{
				if (this.skinManagerStatePersister == null)
				{
					this.skinManagerStatePersister = new RadSkinManagerStatePersister();
				}
				return this.skinManagerStatePersister;
			}
		}

		// Token: 0x17000D7C RID: 3452
		// (get) Token: 0x06002996 RID: 10646 RVA: 0x00085B9F File Offset: 0x00083D9F
		internal RadDataPagerStatePersister DataPagerStatePersister
		{
			get
			{
				if (this.dataPagerStatePersister == null)
				{
					this.dataPagerStatePersister = new RadDataPagerStatePersister();
				}
				return this.dataPagerStatePersister;
			}
		}

		// Token: 0x17000D7D RID: 3453
		// (get) Token: 0x06002997 RID: 10647 RVA: 0x00085BBA File Offset: 0x00083DBA
		internal RadTreeListStatePersister TreeListStatePersister
		{
			get
			{
				if (this.treeListStatePersister == null)
				{
					this.treeListStatePersister = new RadTreeListStatePersister();
				}
				return this.treeListStatePersister;
			}
		}

		// Token: 0x17000D7E RID: 3454
		// (get) Token: 0x06002998 RID: 10648 RVA: 0x00085BD5 File Offset: 0x00083DD5
		internal RadToolBarStatePersister ToolBarStatePersister
		{
			get
			{
				if (this.toolBarStatePersister == null)
				{
					this.toolBarStatePersister = new RadToolBarStatePersister();
				}
				return this.toolBarStatePersister;
			}
		}

		// Token: 0x17000D7F RID: 3455
		// (get) Token: 0x06002999 RID: 10649 RVA: 0x00085BF0 File Offset: 0x00083DF0
		internal RadRibbonBarStatePersister RibbonBarStatePersister
		{
			get
			{
				if (this.ribbonBarStatePersister == null)
				{
					this.ribbonBarStatePersister = new RadRibbonBarStatePersister();
				}
				return this.ribbonBarStatePersister;
			}
		}

		// Token: 0x17000D80 RID: 3456
		// (get) Token: 0x0600299A RID: 10650 RVA: 0x00085C0B File Offset: 0x00083E0B
		internal RadFilterStatePersister FilterStatePersister
		{
			get
			{
				if (this.filterStatePersister == null)
				{
					this.filterStatePersister = new RadFilterStatePersister();
				}
				return this.filterStatePersister;
			}
		}

		// Token: 0x17000D81 RID: 3457
		// (get) Token: 0x0600299B RID: 10651 RVA: 0x00085C26 File Offset: 0x00083E26
		internal RadPivotGridStatePersister PivotGridStatePersister
		{
			get
			{
				if (this.pivotGridStatePersister == null)
				{
					this.pivotGridStatePersister = new RadPivotGridStatePersister();
				}
				return this.pivotGridStatePersister;
			}
		}

		// Token: 0x17000D82 RID: 3458
		// (get) Token: 0x0600299C RID: 10652 RVA: 0x00085C41 File Offset: 0x00083E41
		internal RadTileListStatePersister TileListStatePersister
		{
			get
			{
				if (this.tileListStatePersister == null)
				{
					this.tileListStatePersister = new RadTileListStatePersister();
				}
				return this.tileListStatePersister;
			}
		}

		// Token: 0x17000D83 RID: 3459
		// (get) Token: 0x0600299D RID: 10653 RVA: 0x00085C5C File Offset: 0x00083E5C
		internal RadDropDownTreeStatePersister DropDownTreeStatePersister
		{
			get
			{
				if (this.dropDownTreeStatePersister == null)
				{
					this.dropDownTreeStatePersister = new RadDropDownTreeStatePersister();
				}
				return this.dropDownTreeStatePersister;
			}
		}

		// Token: 0x0600299E RID: 10654 RVA: 0x00085C78 File Offset: 0x00083E78
		internal RadStatePersister GetPersisterFromControlType(Control control)
		{
			if (control is RadTreeView)
			{
				return this.TreeViewStatePersister;
			}
			if (control is RadButton)
			{
				return this.ButtonStatePersister;
			}
			if (control is RadColorPicker)
			{
				return this.ColorPickerStatePersister;
			}
			if (control is RadSlider)
			{
				return this.SliderStatePersister;
			}
			if (control is RadPane)
			{
				return this.PaneStatePersister;
			}
			if (control is RadSlidingPane)
			{
				return this.SlidingPaneStatePersister;
			}
			if (control is RadSlidingZone)
			{
				return this.SlidingZoneStatePersister;
			}
			if (control is RadDock)
			{
				return this.DockStatePersister;
			}
			if (control is RadComboBox)
			{
				return this.ComboBoxStatePersister;
			}
			if (control is RadDropDownList)
			{
				return this.DropDownListStatePersister;
			}
			if (control is RadListBox)
			{
				return this.ListBoxStatePersister;
			}
			if (control is RadMenu)
			{
				return this.MenuStatePersister;
			}
			if (control is RadOrgChart)
			{
				return this.OrgChartStatePersister;
			}
			if (control is RadPanelBar)
			{
				return this.PanelBarStatePersister;
			}
			if (control is RadScheduler)
			{
				return this.SchedulerStatePersister;
			}
			if (control is RadTabStrip)
			{
				return this.TabStripStatePersister;
			}
			if (control is RadGrid)
			{
				return this.GridStatePersister;
			}
			if (control is RadCalendar)
			{
				return this.CalendarStatePersister;
			}
			if (control is RadListView)
			{
				return this.ListViewStatePersister;
			}
			if (control is RadSkinManager)
			{
				return this.SkinManagerStatePersister;
			}
			if (control is RadDataPager)
			{
				return this.DataPagerStatePersister;
			}
			if (control is RadTreeList)
			{
				return this.TreeListStatePersister;
			}
			if (control is RadToolBar)
			{
				return this.ToolBarStatePersister;
			}
			if (control is RadRibbonBar)
			{
				return this.RibbonBarStatePersister;
			}
			if (control is RadFilter)
			{
				return this.FilterStatePersister;
			}
			if (control is RadPivotGrid)
			{
				return this.PivotGridStatePersister;
			}
			if (control is RadTileList)
			{
				return this.TileListStatePersister;
			}
			if (control is RadDropDownTree)
			{
				return this.DropDownTreeStatePersister;
			}
			throw new PersistenceFrameworkException("The control of " + control.GetType().FullName + " could not be persisted. Please review the online documetation for the supported controls.");
		}

		// Token: 0x0600299F RID: 10655 RVA: 0x00085E48 File Offset: 0x00084048
		public RadPersistenceManager()
		{
			this.EnsureLicensing();
			this.persistenceSettings = new PersistenceSettingsCollection();
			this.EnablePersistence = true;
			this.StorageProviderKey = this.defaultStorageProviderKey;
		}

		// Token: 0x060029A0 RID: 10656 RVA: 0x00085E98 File Offset: 0x00084098
		private void EnsureLicensing()
		{
			if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
			{
				try
				{
					LicenseManager.Validate(base.GetType());
				}
				catch
				{
				}
			}
		}

		// Token: 0x17000D84 RID: 3460
		// (get) Token: 0x060029A1 RID: 10657 RVA: 0x00085ED0 File Offset: 0x000840D0
		private string AppDataPath
		{
			get
			{
				if (!base.DesignMode)
				{
					return Path.Combine(this.Context.Request.PhysicalApplicationPath, "App_Data");
				}
				return string.Empty;
			}
		}

		// Token: 0x17000D85 RID: 3461
		// (get) Token: 0x060029A2 RID: 10658 RVA: 0x00085EFA File Offset: 0x000840FA
		internal static string CustomSettingsId
		{
			get
			{
				return "Telerik.Web.UI_PersistanceManager_CustomSettings";
			}
		}

		// Token: 0x17000D86 RID: 3462
		// (get) Token: 0x060029A3 RID: 10659 RVA: 0x00085F01 File Offset: 0x00084101
		// (set) Token: 0x060029A4 RID: 10660 RVA: 0x00085F09 File Offset: 0x00084109
		[DefaultValue("TelerikAspNetRadControlsPersistedState")]
		public string StorageProviderKey { get; set; }

		// Token: 0x17000D87 RID: 3463
		// (get) Token: 0x060029A5 RID: 10661 RVA: 0x00085F12 File Offset: 0x00084112
		// (set) Token: 0x060029A6 RID: 10662 RVA: 0x00085F28 File Offset: 0x00084128
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue(typeof(XmlStateSerializer))]
		public IStateSerializer SerializationProvider
		{
			get
			{
				if (this.serializationProvider == null)
				{
					return new XmlStateSerializer();
				}
				return this.serializationProvider;
			}
			set
			{
				this.serializationProvider = value;
			}
		}

		// Token: 0x17000D88 RID: 3464
		// (get) Token: 0x060029A7 RID: 10663 RVA: 0x00085F31 File Offset: 0x00084131
		// (set) Token: 0x060029A8 RID: 10664 RVA: 0x00085F61 File Offset: 0x00084161
		public IStateStorageProvider StorageProvider
		{
			get
			{
				if (this.storageProvider == null)
				{
					this.EnsureAppDataFolderExists();
					return new AppDataStorageProvider(HttpContext.Current.Server.MapPath("~/App_Data/"));
				}
				return this.storageProvider;
			}
			set
			{
				this.storageProvider = value;
			}
		}

		// Token: 0x17000D89 RID: 3465
		// (get) Token: 0x060029A9 RID: 10665 RVA: 0x00085F6A File Offset: 0x0008416A
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public PersistenceSettingsCollection PersistenceSettings
		{
			get
			{
				return this.persistenceSettings;
			}
		}

		// Token: 0x17000D8A RID: 3466
		// (get) Token: 0x060029AA RID: 10666 RVA: 0x00085F72 File Offset: 0x00084172
		// (set) Token: 0x060029AB RID: 10667 RVA: 0x00085F8D File Offset: 0x0008418D
		internal Dictionary<string, RadPersistenceManagerProxy> PersisterProxies
		{
			get
			{
				if (this.persisterProxies == null)
				{
					this.persisterProxies = new Dictionary<string, RadPersistenceManagerProxy>();
				}
				return this.persisterProxies;
			}
			set
			{
				this.persisterProxies = value;
			}
		}

		// Token: 0x17000D8B RID: 3467
		// (get) Token: 0x060029AC RID: 10668 RVA: 0x00085F96 File Offset: 0x00084196
		// (set) Token: 0x060029AD RID: 10669 RVA: 0x00085FC1 File Offset: 0x000841C1
		[DefaultValue(true)]
		public virtual bool EnablePersistence
		{
			get
			{
				return this.ViewState["EnablePersistence"] != null && (bool)this.ViewState["EnablePersistence"];
			}
			set
			{
				this.ViewState["EnablePersistence"] = value;
			}
		}

		// Token: 0x14000089 RID: 137
		// (add) Token: 0x060029AE RID: 10670 RVA: 0x00085FD9 File Offset: 0x000841D9
		// (remove) Token: 0x060029AF RID: 10671 RVA: 0x00085FEC File Offset: 0x000841EC
		[Category("Action")]
		public event EventHandler<PersistenceManagerLoadStateEventArgs> LoadCustomSettings
		{
			add
			{
				base.Events.AddHandler(RadPersistenceManager.EventLoadCustomSettings, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadPersistenceManager.EventLoadCustomSettings, value);
			}
		}

		// Token: 0x1400008A RID: 138
		// (add) Token: 0x060029B0 RID: 10672 RVA: 0x00085FFF File Offset: 0x000841FF
		// (remove) Token: 0x060029B1 RID: 10673 RVA: 0x00086012 File Offset: 0x00084212
		[Category("Action")]
		public event EventHandler<PersistenceManagerSaveStateEventArgs> SaveCustomSettings
		{
			add
			{
				base.Events.AddHandler(RadPersistenceManager.EventSaveCustomSettings, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadPersistenceManager.EventSaveCustomSettings, value);
			}
		}

		// Token: 0x1400008B RID: 139
		// (add) Token: 0x060029B2 RID: 10674 RVA: 0x00086025 File Offset: 0x00084225
		// (remove) Token: 0x060029B3 RID: 10675 RVA: 0x00086038 File Offset: 0x00084238
		[Category("Action")]
		public event EventHandler<PersistenceManagerSaveAllStateEventArgs> SaveSettings
		{
			add
			{
				base.Events.AddHandler(RadPersistenceManager.EventSaveSettings, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadPersistenceManager.EventSaveSettings, value);
			}
		}

		// Token: 0x1400008C RID: 140
		// (add) Token: 0x060029B4 RID: 10676 RVA: 0x0008604B File Offset: 0x0008424B
		// (remove) Token: 0x060029B5 RID: 10677 RVA: 0x0008605E File Offset: 0x0008425E
		[Category("Action")]
		public event EventHandler<PersistenceManagerLoadAllStateEventArgs> LoadSettings
		{
			add
			{
				base.Events.AddHandler(RadPersistenceManager.EventLoadSettings, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadPersistenceManager.EventLoadSettings, value);
			}
		}

		// Token: 0x060029B6 RID: 10678 RVA: 0x00086074 File Offset: 0x00084274
		protected virtual void OnLoadCustomSettings(PersistenceManagerLoadStateEventArgs e)
		{
			EventHandler<PersistenceManagerLoadStateEventArgs> eventHandler = base.Events[RadPersistenceManager.EventLoadCustomSettings] as EventHandler<PersistenceManagerLoadStateEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060029B7 RID: 10679 RVA: 0x000860A4 File Offset: 0x000842A4
		protected virtual void OnSaveCustomSettings(PersistenceManagerSaveStateEventArgs e)
		{
			EventHandler<PersistenceManagerSaveStateEventArgs> eventHandler = base.Events[RadPersistenceManager.EventSaveCustomSettings] as EventHandler<PersistenceManagerSaveStateEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060029B8 RID: 10680 RVA: 0x000860D4 File Offset: 0x000842D4
		protected virtual void OnSaveSettings(PersistenceManagerSaveAllStateEventArgs e)
		{
			EventHandler<PersistenceManagerSaveAllStateEventArgs> eventHandler = base.Events[RadPersistenceManager.EventSaveSettings] as EventHandler<PersistenceManagerSaveAllStateEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060029B9 RID: 10681 RVA: 0x00086104 File Offset: 0x00084304
		protected virtual void OnLoadSettings(PersistenceManagerLoadAllStateEventArgs e)
		{
			EventHandler<PersistenceManagerLoadAllStateEventArgs> eventHandler = base.Events[RadPersistenceManager.EventLoadSettings] as EventHandler<PersistenceManagerLoadAllStateEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060029BA RID: 10682 RVA: 0x00086132 File Offset: 0x00084332
		private void FindControlsFromPersisterSetting(PersistenceSetting setting, Control startPoint)
		{
			this.FindControlsFromPersisterSetting(setting, startPoint, string.Empty);
		}

		// Token: 0x060029BB RID: 10683 RVA: 0x00086144 File Offset: 0x00084344
		private void FindControlsFromPersisterSetting(PersistenceSetting setting, Control startPoint, string proxyUniqueKey)
		{
			if (setting.SettingType == PersistenceSettingType.ControlID)
			{
				Control control = startPoint.FindControl(setting.ControlID);
				if (control == null)
				{
					throw new PersistenceFrameworkNullReferenceException(string.Format("RadPersistanceManager could not find control with ID '{0}', referenced by the persistence setting.", setting.ControlID));
				}
				this.persistedControls.Add(new PersistedControl
				{
					Control = control,
					Prefix = proxyUniqueKey
				});
				return;
			}
			else
			{
				if (setting.SettingType != PersistenceSettingType.ControlInstance)
				{
					this.FindControlsByType(setting.ControlType, startPoint, proxyUniqueKey);
					return;
				}
				if (setting.ControlInstance == null)
				{
					throw new PersistenceFrameworkNullReferenceException(string.Format("Control instance should not be null!", new object[0]));
				}
				this.persistedControls.Add(new PersistedControl
				{
					Control = setting.ControlInstance,
					Prefix = proxyUniqueKey
				});
				return;
			}
		}

		// Token: 0x060029BC RID: 10684 RVA: 0x000861FC File Offset: 0x000843FC
		private void FindControlsByType(Type controlType, Control currentControl, string proxyUniqueKey)
		{
			foreach (object obj in currentControl.Controls)
			{
				Control control = (Control)obj;
				if (controlType.IsInstanceOfType(control))
				{
					this.persistedControls.Add(new PersistedControl
					{
						Control = control,
						Prefix = proxyUniqueKey
					});
				}
				if (control.HasControls() && !(control is INamingContainer))
				{
					this.FindControlsByType(controlType, control, proxyUniqueKey);
				}
			}
		}

		// Token: 0x060029BD RID: 10685 RVA: 0x00086290 File Offset: 0x00084490
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			if (this.Page != null)
			{
				this.Page.InitComplete += this.Page_InitComplete;
			}
		}

		// Token: 0x060029BE RID: 10686 RVA: 0x000862B8 File Offset: 0x000844B8
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			List<string> list = new List<string>();
			foreach (KeyValuePair<string, RadPersistenceManagerProxy> keyValuePair in this.PersisterProxies)
			{
				if (string.IsNullOrEmpty(keyValuePair.Value.UniqueKey))
				{
					keyValuePair.Value.UniqueKey = keyValuePair.Value.UniqueID;
				}
				if (list.Contains(keyValuePair.Value.UniqueKey))
				{
					throw new PersistenceFrameworkArgumentException("RadPersistenceManagerProxy controls should have unique UniqueKey properties!");
				}
				list.Add(keyValuePair.Value.UniqueKey);
			}
			if (list.Count != this.PersisterProxies.Count)
			{
				throw new PersistenceFrameworkArgumentException("Either all proxies should have UniqueKey set, or none of them.");
			}
		}

		// Token: 0x060029BF RID: 10687 RVA: 0x0008638C File Offset: 0x0008458C
		protected void Page_InitComplete(object sender, EventArgs e)
		{
			this.Page.InitComplete -= this.Page_InitComplete;
			if (RadPersistenceManager.GetCurrent(this.Page) != null)
			{
				throw new PersistenceFrameworkInvalidOperationException("Only one instance of RadStatePersisterManager can be added to the page!");
			}
			this.Page.Items[typeof(RadPersistenceManager)] = this;
		}

		// Token: 0x060029C0 RID: 10688 RVA: 0x00086424 File Offset: 0x00084624
		public void SaveState()
		{
			if (this.EnablePersistence)
			{
				List<RadControlState> controlStates = new List<RadControlState>();
				List<string> list = new List<string>();
				Control startPoint;
				if (this.NamingContainer == null)
				{
					startPoint = this.Parent;
				}
				else
				{
					startPoint = this.NamingContainer;
				}
				foreach (object obj in this.PersistenceSettings)
				{
					PersistenceSetting setting = (PersistenceSetting)obj;
					this.FindControlsFromPersisterSetting(setting, startPoint);
				}
				foreach (KeyValuePair<string, RadPersistenceManagerProxy> keyValuePair in this.PersisterProxies)
				{
					foreach (object obj2 in keyValuePair.Value.PersistenceSettings)
					{
						PersistenceSetting setting2 = (PersistenceSetting)obj2;
						if (!string.IsNullOrEmpty(keyValuePair.Value.UniqueKey))
						{
							list.Add(keyValuePair.Value.UniqueKey);
						}
						this.FindControlsFromPersisterSetting(setting2, keyValuePair.Value.NamingContainer, keyValuePair.Value.UniqueKey);
					}
				}
				if (list.Count > 0)
				{
					if (list.Count < this.PersisterProxies.Count)
					{
						throw new PersistenceFrameworkArgumentException("Either all proxies should have UniqueKey set or none of them!");
					}
					try
					{
						controlStates = this.SerializationProvider.DeserializeCollection(this.StorageProvider.LoadStateFromStorage(this.StorageProviderKey));
					}
					catch (PersistenceFrameworkStorageException)
					{
					}
					this.RemoveSettingsForActiveProxies(ref controlStates, list);
				}
				List<ControlSetting> customSettings = new List<ControlSetting>();
				this.OnSaveCustomSettings(new PersistenceManagerSaveStateEventArgs
				{
					CustomSettings = customSettings
				});
				RadControlState item = new RadControlState(customSettings, RadPersistenceManager.CustomSettingsId);
				controlStates.Add(item);
				using (List<PersistedControl>.Enumerator enumerator4 = this.persistedControls.GetEnumerator())
				{
					while (enumerator4.MoveNext())
					{
						PersistedControl ctrl = enumerator4.Current;
						RadStatePersister persisterFromControlType = this.GetPersisterFromControlType(ctrl.Control);
						if (persisterFromControlType != null)
						{
							StateSaveEventHandler value = delegate(object sender, StatePersisterEventArgs e)
							{
								e.State.UniqueKey = ctrl.Prefix;
								controlStates.Add(e.State);
							};
							persisterFromControlType.StateSave += value;
							persisterFromControlType.SaveState(ctrl.Control);
							persisterFromControlType.StateSave -= value;
						}
					}
				}
				this.OnSaveSettings(new PersistenceManagerSaveAllStateEventArgs
				{
					Settings = controlStates
				});
				this.StorageProvider.SaveStateToStorage(this.StorageProviderKey, this.SerializationProvider.Serialize(controlStates));
			}
		}

		// Token: 0x060029C1 RID: 10689 RVA: 0x00086728 File Offset: 0x00084928
		private void RemoveSettingsForActiveProxies(ref List<RadControlState> controlState, List<string> uniqueKeys)
		{
			List<RadControlState> list = new List<RadControlState>();
			foreach (RadControlState radControlState in controlState)
			{
				if (!uniqueKeys.Contains(radControlState.UniqueKey) && !string.IsNullOrEmpty(radControlState.UniqueKey) && radControlState.UniqueId != RadPersistenceManager.CustomSettingsId)
				{
					list.Add(radControlState);
				}
			}
			controlState = list;
		}

		// Token: 0x060029C2 RID: 10690 RVA: 0x000867B0 File Offset: 0x000849B0
		private void RemoveSettingsForInactiveProxies(ref List<RadControlState> controlState, List<string> uniqueKeys)
		{
			List<RadControlState> list = new List<RadControlState>();
			foreach (RadControlState radControlState in controlState)
			{
				if (uniqueKeys.Contains(radControlState.UniqueKey) || string.IsNullOrEmpty(radControlState.UniqueKey) || radControlState.UniqueId == RadPersistenceManager.CustomSettingsId)
				{
					list.Add(radControlState);
				}
			}
			controlState = list;
		}

		// Token: 0x060029C3 RID: 10691 RVA: 0x00086838 File Offset: 0x00084A38
		public void LoadState()
		{
			if (this.EnablePersistence)
			{
				List<RadControlState> list = this.SerializationProvider.DeserializeCollection(this.StorageProvider.LoadStateFromStorage(this.StorageProviderKey));
				List<string> list2 = new List<string>();
				foreach (KeyValuePair<string, RadPersistenceManagerProxy> keyValuePair in this.PersisterProxies)
				{
					if (!string.IsNullOrEmpty(keyValuePair.Value.UniqueKey))
					{
						list2.Add(keyValuePair.Value.UniqueKey);
					}
				}
				if (list2.Count > 0)
				{
					this.RemoveSettingsForInactiveProxies(ref list, list2);
				}
				this.OnLoadCustomSettings(new PersistenceManagerLoadStateEventArgs
				{
					CustomSettings = this.FindCustomSettingsState(list)
				});
				this.OnLoadSettings(new PersistenceManagerLoadAllStateEventArgs
				{
					Settings = list
				});
				foreach (RadControlState radControlState in list)
				{
					if (radControlState.UniqueId != RadPersistenceManager.CustomSettingsId)
					{
						Control control = this.Page.FindControl(radControlState.UniqueId);
						if (control != null)
						{
							RadStatePersister persisterFromControlType = this.GetPersisterFromControlType(control);
							if (persisterFromControlType != null)
							{
								persisterFromControlType.LoadState(control, radControlState);
							}
						}
					}
				}
			}
		}

		// Token: 0x060029C4 RID: 10692 RVA: 0x00086998 File Offset: 0x00084B98
		internal RadControlState FindCustomSettingsState(List<RadControlState> controlsState)
		{
			RadControlState result = new RadControlState();
			foreach (RadControlState radControlState in controlsState)
			{
				if (radControlState.UniqueId == RadPersistenceManager.CustomSettingsId)
				{
					result = radControlState;
					break;
				}
			}
			return result;
		}

		// Token: 0x060029C5 RID: 10693 RVA: 0x000869FC File Offset: 0x00084BFC
		internal void RegisterStatePersisterProxy(RadPersistenceManagerProxy radStatePersisterManagerProxy)
		{
			if (!this.PersisterProxies.ContainsKey(radStatePersisterManagerProxy.UniqueID))
			{
				this.PersisterProxies.Add(radStatePersisterManagerProxy.UniqueID, radStatePersisterManagerProxy);
			}
		}

		// Token: 0x060029C6 RID: 10694 RVA: 0x00086A23 File Offset: 0x00084C23
		public static RadPersistenceManager GetCurrent(Page page)
		{
			if (page == null)
			{
				throw new PersistenceFrameworkArgumentNullException("page");
			}
			return page.Items[typeof(RadPersistenceManager)] as RadPersistenceManager;
		}

		// Token: 0x060029C7 RID: 10695 RVA: 0x00086A4D File Offset: 0x00084C4D
		protected internal virtual void EnsureAppDataFolderExists()
		{
			if (!Directory.Exists(this.AppDataPath))
			{
				this.CreateAppDataFolder();
			}
		}

		// Token: 0x060029C8 RID: 10696 RVA: 0x00086A64 File Offset: 0x00084C64
		private void CreateAppDataFolder()
		{
			try
			{
				if (!base.DesignMode)
				{
					Directory.CreateDirectory(this.AppDataPath);
				}
			}
			catch (UnauthorizedAccessException ex)
			{
				throw new PersistenceFrameworkException("RadPersistenceManager could not create App_Data folder. Ensure the App_Data's location is writable.", ex.InnerException);
			}
		}

		// Token: 0x04000A92 RID: 2706
		private RadTreeViewStatePersister treeViewStatePersister;

		// Token: 0x04000A93 RID: 2707
		private RadButtonStatePersister buttonStatePersister;

		// Token: 0x04000A94 RID: 2708
		private RadSliderStatePersister sliderStatePersister;

		// Token: 0x04000A95 RID: 2709
		private RadPaneStatePersister paneStatePersister;

		// Token: 0x04000A96 RID: 2710
		private RadSlidingPaneStatePersister slidingPaneStatePersister;

		// Token: 0x04000A97 RID: 2711
		private RadSlidingZoneStatePersister slidingZoneStatePersister;

		// Token: 0x04000A98 RID: 2712
		private RadDockStatePersister dockStatePersister;

		// Token: 0x04000A99 RID: 2713
		private RadColorPickerStatePersister colorPickerStatePersister;

		// Token: 0x04000A9A RID: 2714
		private RadComboBoxStatePersister comboBoxStatePersister;

		// Token: 0x04000A9B RID: 2715
		private RadDropDownListStatePersister dropDownListStatePersister;

		// Token: 0x04000A9C RID: 2716
		private RadListBoxStatePersister listBoxStatePersister;

		// Token: 0x04000A9D RID: 2717
		private RadMenuStatePersister menuStatePersister;

		// Token: 0x04000A9E RID: 2718
		private RadOrgChartStatePersister orgChartStatePersister;

		// Token: 0x04000A9F RID: 2719
		private RadPanelBarStatePersister panelBarStatePersister;

		// Token: 0x04000AA0 RID: 2720
		private RadSchedulerStatePersister schedulerStatePersister;

		// Token: 0x04000AA1 RID: 2721
		private RadTabStripStatePersister tabStripStatePersister;

		// Token: 0x04000AA2 RID: 2722
		private RadGridStatePersister gridStatePersister;

		// Token: 0x04000AA3 RID: 2723
		private RadCalendarStatePersister calendarStatePersister;

		// Token: 0x04000AA4 RID: 2724
		private RadListViewStatePersister listViewStatePersister;

		// Token: 0x04000AA5 RID: 2725
		private RadSkinManagerStatePersister skinManagerStatePersister;

		// Token: 0x04000AA6 RID: 2726
		private RadDataPagerStatePersister dataPagerStatePersister;

		// Token: 0x04000AA7 RID: 2727
		private RadTreeListStatePersister treeListStatePersister;

		// Token: 0x04000AA8 RID: 2728
		private RadToolBarStatePersister toolBarStatePersister;

		// Token: 0x04000AA9 RID: 2729
		private RadRibbonBarStatePersister ribbonBarStatePersister;

		// Token: 0x04000AAA RID: 2730
		private RadFilterStatePersister filterStatePersister;

		// Token: 0x04000AAB RID: 2731
		private RadPivotGridStatePersister pivotGridStatePersister;

		// Token: 0x04000AAC RID: 2732
		private RadTileListStatePersister tileListStatePersister;

		// Token: 0x04000AAD RID: 2733
		private RadDropDownTreeStatePersister dropDownTreeStatePersister;

		// Token: 0x04000AAE RID: 2734
		private Dictionary<string, RadPersistenceManagerProxy> persisterProxies;

		// Token: 0x04000AAF RID: 2735
		private PersistenceSettingsCollection persistenceSettings;

		// Token: 0x04000AB0 RID: 2736
		private List<PersistedControl> persistedControls = new List<PersistedControl>();

		// Token: 0x04000AB1 RID: 2737
		private static readonly object EventLoadCustomSettings = new object();

		// Token: 0x04000AB2 RID: 2738
		private static readonly object EventSaveCustomSettings = new object();

		// Token: 0x04000AB3 RID: 2739
		private static readonly object EventSaveSettings = new object();

		// Token: 0x04000AB4 RID: 2740
		private static readonly object EventLoadSettings = new object();

		// Token: 0x04000AB5 RID: 2741
		private IStateSerializer serializationProvider;

		// Token: 0x04000AB6 RID: 2742
		private IStateStorageProvider storageProvider;

		// Token: 0x04000AB7 RID: 2743
		private string defaultStorageProviderKey = "TelerikAspNetRadControlsPersistedState";
	}
}
