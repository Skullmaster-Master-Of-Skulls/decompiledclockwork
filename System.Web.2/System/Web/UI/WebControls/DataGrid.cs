using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;
using System.Reflection;
using System.Web.Util;

namespace System.Web.UI.WebControls
{
	// Token: 0x020003BC RID: 956
	[Editor("System.Web.UI.Design.WebControls.DataGridComponentEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(ComponentEditor))]
	[Designer("System.Web.UI.Design.WebControls.DataGridDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public class DataGrid : BaseDataList, INamingContainer
	{
		// Token: 0x17000D1E RID: 3358
		// (get) Token: 0x06002E05 RID: 11781 RVA: 0x000962B0 File Offset: 0x000944B0
		// (set) Token: 0x06002E06 RID: 11782 RVA: 0x000962D9 File Offset: 0x000944D9
		[WebCategory("Paging")]
		[DefaultValue(false)]
		[WebSysDescription("DataGrid_AllowCustomPaging")]
		public virtual bool AllowCustomPaging
		{
			get
			{
				object obj = this.ViewState["AllowCustomPaging"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["AllowCustomPaging"] = value;
			}
		}

		// Token: 0x17000D1F RID: 3359
		// (get) Token: 0x06002E07 RID: 11783 RVA: 0x000962F4 File Offset: 0x000944F4
		// (set) Token: 0x06002E08 RID: 11784 RVA: 0x0009631D File Offset: 0x0009451D
		[WebCategory("Paging")]
		[DefaultValue(false)]
		[WebSysDescription("DataGrid_AllowPaging")]
		public virtual bool AllowPaging
		{
			get
			{
				object obj = this.ViewState["AllowPaging"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["AllowPaging"] = value;
			}
		}

		// Token: 0x17000D20 RID: 3360
		// (get) Token: 0x06002E09 RID: 11785 RVA: 0x00096338 File Offset: 0x00094538
		// (set) Token: 0x06002E0A RID: 11786 RVA: 0x00096361 File Offset: 0x00094561
		[WebCategory("Behavior")]
		[DefaultValue(false)]
		[WebSysDescription("DataGrid_AllowSorting")]
		public virtual bool AllowSorting
		{
			get
			{
				object obj = this.ViewState["AllowSorting"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["AllowSorting"] = value;
			}
		}

		// Token: 0x17000D21 RID: 3361
		// (get) Token: 0x06002E0B RID: 11787 RVA: 0x00096379 File Offset: 0x00094579
		[WebCategory("Styles")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("DataGrid_AlternatingItemStyle")]
		public virtual TableItemStyle AlternatingItemStyle
		{
			get
			{
				if (this.alternatingItemStyle == null)
				{
					this.alternatingItemStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.alternatingItemStyle).TrackViewState();
					}
				}
				return this.alternatingItemStyle;
			}
		}

		// Token: 0x17000D22 RID: 3362
		// (get) Token: 0x06002E0C RID: 11788 RVA: 0x000963A8 File Offset: 0x000945A8
		// (set) Token: 0x06002E0D RID: 11789 RVA: 0x000963D1 File Offset: 0x000945D1
		[WebCategory("Behavior")]
		[DefaultValue(true)]
		[WebSysDescription("DataControls_AutoGenerateColumns")]
		public virtual bool AutoGenerateColumns
		{
			get
			{
				object obj = this.ViewState["AutoGenerateColumns"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["AutoGenerateColumns"] = value;
			}
		}

		// Token: 0x17000D23 RID: 3363
		// (get) Token: 0x06002E0E RID: 11790 RVA: 0x000963E9 File Offset: 0x000945E9
		// (set) Token: 0x06002E0F RID: 11791 RVA: 0x00096409 File Offset: 0x00094609
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[UrlProperty]
		[WebSysDescription("WebControl_BackImageUrl")]
		public virtual string BackImageUrl
		{
			get
			{
				if (!base.ControlStyleCreated)
				{
					return string.Empty;
				}
				return ((TableStyle)base.ControlStyle).BackImageUrl;
			}
			set
			{
				((TableStyle)base.ControlStyle).BackImageUrl = value;
			}
		}

		// Token: 0x17000D24 RID: 3364
		// (get) Token: 0x06002E10 RID: 11792 RVA: 0x0009641C File Offset: 0x0009461C
		// (set) Token: 0x06002E11 RID: 11793 RVA: 0x00096445 File Offset: 0x00094645
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("DataGrid_CurrentPageIndex")]
		public int CurrentPageIndex
		{
			get
			{
				object obj = this.ViewState["CurrentPageIndex"];
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
				this.ViewState["CurrentPageIndex"] = value;
			}
		}

		// Token: 0x17000D25 RID: 3365
		// (get) Token: 0x06002E12 RID: 11794 RVA: 0x0009646C File Offset: 0x0009466C
		[DefaultValue(null)]
		[Editor("System.Web.UI.Design.WebControls.DataGridColumnCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[MergableProperty(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebCategory("Default")]
		[WebSysDescription("DataControls_Columns")]
		public virtual DataGridColumnCollection Columns
		{
			get
			{
				if (this.columnCollection == null)
				{
					this.columns = new ArrayList();
					this.columnCollection = new DataGridColumnCollection(this, this.columns);
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.columnCollection).TrackViewState();
					}
				}
				return this.columnCollection;
			}
		}

		// Token: 0x17000D26 RID: 3366
		// (get) Token: 0x06002E13 RID: 11795 RVA: 0x000964AC File Offset: 0x000946AC
		// (set) Token: 0x06002E14 RID: 11796 RVA: 0x000964D5 File Offset: 0x000946D5
		[WebCategory("Default")]
		[DefaultValue(-1)]
		[WebSysDescription("DataGrid_EditItemIndex")]
		public virtual int EditItemIndex
		{
			get
			{
				object obj = this.ViewState["EditItemIndex"];
				if (obj != null)
				{
					return (int)obj;
				}
				return -1;
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["EditItemIndex"] = value;
			}
		}

		// Token: 0x17000D27 RID: 3367
		// (get) Token: 0x06002E15 RID: 11797 RVA: 0x000964FC File Offset: 0x000946FC
		[WebCategory("Styles")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("DataGrid_EditItemStyle")]
		public virtual TableItemStyle EditItemStyle
		{
			get
			{
				if (this.editItemStyle == null)
				{
					this.editItemStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.editItemStyle).TrackViewState();
					}
				}
				return this.editItemStyle;
			}
		}

		// Token: 0x17000D28 RID: 3368
		// (get) Token: 0x06002E16 RID: 11798 RVA: 0x0009652A File Offset: 0x0009472A
		[WebCategory("Styles")]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("DataControls_FooterStyle")]
		public virtual TableItemStyle FooterStyle
		{
			get
			{
				if (this.footerStyle == null)
				{
					this.footerStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.footerStyle).TrackViewState();
					}
				}
				return this.footerStyle;
			}
		}

		// Token: 0x17000D29 RID: 3369
		// (get) Token: 0x06002E17 RID: 11799 RVA: 0x00096558 File Offset: 0x00094758
		[WebCategory("Styles")]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("DataControls_HeaderStyle")]
		public virtual TableItemStyle HeaderStyle
		{
			get
			{
				if (this.headerStyle == null)
				{
					this.headerStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.headerStyle).TrackViewState();
					}
				}
				return this.headerStyle;
			}
		}

		// Token: 0x17000D2A RID: 3370
		// (get) Token: 0x06002E18 RID: 11800 RVA: 0x00096588 File Offset: 0x00094788
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("DataGrid_Items")]
		public virtual DataGridItemCollection Items
		{
			get
			{
				if (this.itemsCollection == null)
				{
					if (this.itemsArray == null)
					{
						this.EnsureChildControls();
					}
					if (this.itemsArray == null)
					{
						this.itemsArray = new ArrayList();
					}
					this.itemsCollection = new DataGridItemCollection(this.itemsArray);
				}
				return this.itemsCollection;
			}
		}

		// Token: 0x17000D2B RID: 3371
		// (get) Token: 0x06002E19 RID: 11801 RVA: 0x000965D5 File Offset: 0x000947D5
		[WebCategory("Styles")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("DataGrid_ItemStyle")]
		public virtual TableItemStyle ItemStyle
		{
			get
			{
				if (this.itemStyle == null)
				{
					this.itemStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.itemStyle).TrackViewState();
					}
				}
				return this.itemStyle;
			}
		}

		// Token: 0x17000D2C RID: 3372
		// (get) Token: 0x06002E1A RID: 11802 RVA: 0x00096604 File Offset: 0x00094804
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("DataGrid_PageCount")]
		public int PageCount
		{
			get
			{
				if (this.pagedDataSource != null)
				{
					return this.pagedDataSource.PageCount;
				}
				object obj = this.ViewState["PageCount"];
				if (obj == null)
				{
					return 0;
				}
				return (int)obj;
			}
		}

		// Token: 0x17000D2D RID: 3373
		// (get) Token: 0x06002E1B RID: 11803 RVA: 0x00096641 File Offset: 0x00094841
		[WebCategory("Styles")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("DataGrid_PagerStyle")]
		public virtual DataGridPagerStyle PagerStyle
		{
			get
			{
				if (this.pagerStyle == null)
				{
					this.pagerStyle = new DataGridPagerStyle(this);
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.pagerStyle).TrackViewState();
					}
				}
				return this.pagerStyle;
			}
		}

		// Token: 0x17000D2E RID: 3374
		// (get) Token: 0x06002E1C RID: 11804 RVA: 0x00096670 File Offset: 0x00094870
		// (set) Token: 0x06002E1D RID: 11805 RVA: 0x0009669A File Offset: 0x0009489A
		[WebCategory("Paging")]
		[DefaultValue(10)]
		[WebSysDescription("DataGrid_PageSize")]
		public virtual int PageSize
		{
			get
			{
				object obj = this.ViewState["PageSize"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 10;
			}
			set
			{
				if (value < 1)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["PageSize"] = value;
			}
		}

		// Token: 0x17000D2F RID: 3375
		// (get) Token: 0x06002E1E RID: 11806 RVA: 0x000966C4 File Offset: 0x000948C4
		// (set) Token: 0x06002E1F RID: 11807 RVA: 0x000966F0 File Offset: 0x000948F0
		[Bindable(true)]
		[DefaultValue(-1)]
		[WebSysDescription("WebControl_SelectedIndex")]
		public virtual int SelectedIndex
		{
			get
			{
				object obj = this.ViewState["SelectedIndex"];
				if (obj != null)
				{
					return (int)obj;
				}
				return -1;
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				int selectedIndex = this.SelectedIndex;
				this.ViewState["SelectedIndex"] = value;
				if (this.itemsArray != null)
				{
					if (selectedIndex != -1 && this.itemsArray.Count > selectedIndex)
					{
						DataGridItem dataGridItem = (DataGridItem)this.itemsArray[selectedIndex];
						if (dataGridItem.ItemType != ListItemType.EditItem)
						{
							ListItemType itemType = ListItemType.Item;
							if (selectedIndex % 2 != 0)
							{
								itemType = ListItemType.AlternatingItem;
							}
							dataGridItem.SetItemType(itemType);
						}
					}
					if (value != -1 && this.itemsArray.Count > value)
					{
						DataGridItem dataGridItem = (DataGridItem)this.itemsArray[value];
						if (dataGridItem.ItemType != ListItemType.EditItem)
						{
							dataGridItem.SetItemType(ListItemType.SelectedItem);
						}
					}
				}
			}
		}

		// Token: 0x17000D30 RID: 3376
		// (get) Token: 0x06002E20 RID: 11808 RVA: 0x000967A4 File Offset: 0x000949A4
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("DataGrid_SelectedItem")]
		public virtual DataGridItem SelectedItem
		{
			get
			{
				int selectedIndex = this.SelectedIndex;
				DataGridItem result = null;
				if (selectedIndex != -1)
				{
					result = this.Items[selectedIndex];
				}
				return result;
			}
		}

		// Token: 0x17000D31 RID: 3377
		// (get) Token: 0x06002E21 RID: 11809 RVA: 0x000967CC File Offset: 0x000949CC
		[WebCategory("Styles")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("DataGrid_SelectedItemStyle")]
		public virtual TableItemStyle SelectedItemStyle
		{
			get
			{
				if (this.selectedItemStyle == null)
				{
					this.selectedItemStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.selectedItemStyle).TrackViewState();
					}
				}
				return this.selectedItemStyle;
			}
		}

		// Token: 0x17000D32 RID: 3378
		// (get) Token: 0x06002E22 RID: 11810 RVA: 0x000967FC File Offset: 0x000949FC
		// (set) Token: 0x06002E23 RID: 11811 RVA: 0x00096825 File Offset: 0x00094A25
		[WebCategory("Appearance")]
		[DefaultValue(false)]
		[WebSysDescription("DataControls_ShowFooter")]
		public virtual bool ShowFooter
		{
			get
			{
				object obj = this.ViewState["ShowFooter"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["ShowFooter"] = value;
			}
		}

		// Token: 0x17000D33 RID: 3379
		// (get) Token: 0x06002E24 RID: 11812 RVA: 0x00096840 File Offset: 0x00094A40
		// (set) Token: 0x06002E25 RID: 11813 RVA: 0x00096869 File Offset: 0x00094A69
		[WebCategory("Appearance")]
		[DefaultValue(true)]
		[WebSysDescription("DataControls_ShowHeader")]
		public virtual bool ShowHeader
		{
			get
			{
				object obj = this.ViewState["ShowHeader"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["ShowHeader"] = value;
			}
		}

		// Token: 0x17000D34 RID: 3380
		// (get) Token: 0x06002E26 RID: 11814 RVA: 0x0008BDAD File Offset: 0x00089FAD
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Table;
			}
		}

		// Token: 0x17000D35 RID: 3381
		// (get) Token: 0x06002E27 RID: 11815 RVA: 0x00096884 File Offset: 0x00094A84
		// (set) Token: 0x06002E28 RID: 11816 RVA: 0x000968AD File Offset: 0x00094AAD
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("DataGrid_VisibleItemCount")]
		public virtual int VirtualItemCount
		{
			get
			{
				object obj = this.ViewState["VirtualItemCount"];
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
				this.ViewState["VirtualItemCount"] = value;
			}
		}

		// Token: 0x1400006C RID: 108
		// (add) Token: 0x06002E29 RID: 11817 RVA: 0x000968D4 File Offset: 0x00094AD4
		// (remove) Token: 0x06002E2A RID: 11818 RVA: 0x000968E7 File Offset: 0x00094AE7
		[WebCategory("Action")]
		[WebSysDescription("DataGrid_OnCancelCommand")]
		public event DataGridCommandEventHandler CancelCommand
		{
			add
			{
				base.Events.AddHandler(DataGrid.EventCancelCommand, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGrid.EventCancelCommand, value);
			}
		}

		// Token: 0x1400006D RID: 109
		// (add) Token: 0x06002E2B RID: 11819 RVA: 0x000968FA File Offset: 0x00094AFA
		// (remove) Token: 0x06002E2C RID: 11820 RVA: 0x0009690D File Offset: 0x00094B0D
		[WebCategory("Action")]
		[WebSysDescription("DataGrid_OnDeleteCommand")]
		public event DataGridCommandEventHandler DeleteCommand
		{
			add
			{
				base.Events.AddHandler(DataGrid.EventDeleteCommand, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGrid.EventDeleteCommand, value);
			}
		}

		// Token: 0x1400006E RID: 110
		// (add) Token: 0x06002E2D RID: 11821 RVA: 0x00096920 File Offset: 0x00094B20
		// (remove) Token: 0x06002E2E RID: 11822 RVA: 0x00096933 File Offset: 0x00094B33
		[WebCategory("Action")]
		[WebSysDescription("DataGrid_OnEditCommand")]
		public event DataGridCommandEventHandler EditCommand
		{
			add
			{
				base.Events.AddHandler(DataGrid.EventEditCommand, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGrid.EventEditCommand, value);
			}
		}

		// Token: 0x1400006F RID: 111
		// (add) Token: 0x06002E2F RID: 11823 RVA: 0x00096946 File Offset: 0x00094B46
		// (remove) Token: 0x06002E30 RID: 11824 RVA: 0x00096959 File Offset: 0x00094B59
		[WebCategory("Action")]
		[WebSysDescription("DataGrid_OnItemCommand")]
		public event DataGridCommandEventHandler ItemCommand
		{
			add
			{
				base.Events.AddHandler(DataGrid.EventItemCommand, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGrid.EventItemCommand, value);
			}
		}

		// Token: 0x14000070 RID: 112
		// (add) Token: 0x06002E31 RID: 11825 RVA: 0x0009696C File Offset: 0x00094B6C
		// (remove) Token: 0x06002E32 RID: 11826 RVA: 0x0009697F File Offset: 0x00094B7F
		[WebCategory("Behavior")]
		[WebSysDescription("DataControls_OnItemCreated")]
		public event DataGridItemEventHandler ItemCreated
		{
			add
			{
				base.Events.AddHandler(DataGrid.EventItemCreated, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGrid.EventItemCreated, value);
			}
		}

		// Token: 0x14000071 RID: 113
		// (add) Token: 0x06002E33 RID: 11827 RVA: 0x00096992 File Offset: 0x00094B92
		// (remove) Token: 0x06002E34 RID: 11828 RVA: 0x000969A5 File Offset: 0x00094BA5
		[WebCategory("Behavior")]
		[WebSysDescription("DataControls_OnItemDataBound")]
		public event DataGridItemEventHandler ItemDataBound
		{
			add
			{
				base.Events.AddHandler(DataGrid.EventItemDataBound, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGrid.EventItemDataBound, value);
			}
		}

		// Token: 0x14000072 RID: 114
		// (add) Token: 0x06002E35 RID: 11829 RVA: 0x000969B8 File Offset: 0x00094BB8
		// (remove) Token: 0x06002E36 RID: 11830 RVA: 0x000969CB File Offset: 0x00094BCB
		[WebCategory("Action")]
		[WebSysDescription("DataGrid_OnPageIndexChanged")]
		public event DataGridPageChangedEventHandler PageIndexChanged
		{
			add
			{
				base.Events.AddHandler(DataGrid.EventPageIndexChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGrid.EventPageIndexChanged, value);
			}
		}

		// Token: 0x14000073 RID: 115
		// (add) Token: 0x06002E37 RID: 11831 RVA: 0x000969DE File Offset: 0x00094BDE
		// (remove) Token: 0x06002E38 RID: 11832 RVA: 0x000969F1 File Offset: 0x00094BF1
		[WebCategory("Action")]
		[WebSysDescription("DataGrid_OnSortCommand")]
		public event DataGridSortCommandEventHandler SortCommand
		{
			add
			{
				base.Events.AddHandler(DataGrid.EventSortCommand, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGrid.EventSortCommand, value);
			}
		}

		// Token: 0x14000074 RID: 116
		// (add) Token: 0x06002E39 RID: 11833 RVA: 0x00096A04 File Offset: 0x00094C04
		// (remove) Token: 0x06002E3A RID: 11834 RVA: 0x00096A17 File Offset: 0x00094C17
		[WebCategory("Action")]
		[WebSysDescription("DataGrid_OnUpdateCommand")]
		public event DataGridCommandEventHandler UpdateCommand
		{
			add
			{
				base.Events.AddHandler(DataGrid.EventUpdateCommand, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGrid.EventUpdateCommand, value);
			}
		}

		// Token: 0x06002E3B RID: 11835 RVA: 0x00096A2A File Offset: 0x00094C2A
		internal void StoreEnumerator(IEnumerator dataSource, object firstDataItem)
		{
			this.storedData = dataSource;
			this.firstDataItem = firstDataItem;
			this.storedDataValid = true;
		}

		// Token: 0x06002E3C RID: 11836 RVA: 0x00096A44 File Offset: 0x00094C44
		private ArrayList CreateAutoGeneratedColumns(PagedDataSource dataSource)
		{
			if (dataSource == null)
			{
				return null;
			}
			ArrayList arrayList = new ArrayList();
			bool flag = true;
			PropertyDescriptorCollection propertyDescriptorCollection = ((ITypedList)dataSource).GetItemProperties(new PropertyDescriptor[0]);
			if (propertyDescriptorCollection == null)
			{
				Type type = null;
				object obj = null;
				IEnumerable dataSource2 = dataSource.DataSource;
				Type type2 = dataSource2.GetType();
				PropertyInfo property = type2.GetProperty("Item", BindingFlags.Instance | BindingFlags.Public, null, null, new Type[]
				{
					typeof(int)
				}, null);
				if (property != null)
				{
					type = property.PropertyType;
				}
				if (type == null || type == typeof(object))
				{
					IEnumerator enumerator = dataSource.GetEnumerator();
					if (enumerator.MoveNext())
					{
						obj = enumerator.Current;
					}
					else
					{
						flag = false;
					}
					if (obj != null)
					{
						type = obj.GetType();
					}
					this.StoreEnumerator(enumerator, obj);
				}
				if (obj != null && obj is ICustomTypeDescriptor)
				{
					propertyDescriptorCollection = TypeDescriptor.GetProperties(obj);
				}
				else if (type != null)
				{
					if (BaseDataList.IsBindableType(type))
					{
						BoundColumn boundColumn = new BoundColumn();
						((IStateManager)boundColumn).TrackViewState();
						boundColumn.HeaderText = "Item";
						boundColumn.DataField = BoundColumn.thisExpr;
						boundColumn.SortExpression = "Item";
						boundColumn.SetOwner(this);
						arrayList.Add(boundColumn);
					}
					else
					{
						propertyDescriptorCollection = TypeDescriptor.GetProperties(type);
					}
				}
			}
			if (propertyDescriptorCollection != null && propertyDescriptorCollection.Count != 0)
			{
				foreach (object obj2 in propertyDescriptorCollection)
				{
					PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj2;
					Type propertyType = propertyDescriptor.PropertyType;
					if (BaseDataList.IsBindableType(propertyType))
					{
						BoundColumn boundColumn2 = new BoundColumn();
						((IStateManager)boundColumn2).TrackViewState();
						boundColumn2.HeaderText = propertyDescriptor.Name;
						boundColumn2.DataField = propertyDescriptor.Name;
						boundColumn2.SortExpression = propertyDescriptor.Name;
						boundColumn2.ReadOnly = propertyDescriptor.IsReadOnly;
						boundColumn2.SetOwner(this);
						arrayList.Add(boundColumn2);
					}
				}
			}
			if (arrayList.Count == 0 && flag)
			{
				throw new HttpException(SR.GetString("DataGrid_NoAutoGenColumns", new object[]
				{
					this.ID
				}));
			}
			return arrayList;
		}

		// Token: 0x06002E3D RID: 11837 RVA: 0x00096C74 File Offset: 0x00094E74
		protected virtual ArrayList CreateColumnSet(PagedDataSource dataSource, bool useDataSource)
		{
			ArrayList arrayList = new ArrayList();
			DataGridColumn[] array = new DataGridColumn[this.Columns.Count];
			this.Columns.CopyTo(array, 0);
			for (int i = 0; i < array.Length; i++)
			{
				arrayList.Add(array[i]);
			}
			if (this.AutoGenerateColumns)
			{
				ArrayList arrayList2;
				if (useDataSource)
				{
					arrayList2 = this.CreateAutoGeneratedColumns(dataSource);
					this.autoGenColumnsArray = arrayList2;
				}
				else
				{
					arrayList2 = this.autoGenColumnsArray;
				}
				if (arrayList2 != null)
				{
					int count = arrayList2.Count;
					for (int i = 0; i < count; i++)
					{
						arrayList.Add(arrayList2[i]);
					}
				}
			}
			return arrayList;
		}

		// Token: 0x06002E3E RID: 11838 RVA: 0x00096D0C File Offset: 0x00094F0C
		protected override void CreateControlHierarchy(bool useDataSource)
		{
			this.pagedDataSource = this.CreatePagedDataSource();
			IEnumerator enumerator = null;
			int num = -1;
			ArrayList dataKeysArray = base.DataKeysArray;
			ArrayList arrayList = null;
			if (this.itemsArray != null)
			{
				this.itemsArray.Clear();
			}
			else
			{
				this.itemsArray = new ArrayList();
			}
			this.itemsCollection = null;
			if (!useDataSource)
			{
				num = (int)this.ViewState["_!ItemCount"];
				int dataItemCount = (int)this.ViewState["_!DataSourceItemCount"];
				if (num != -1)
				{
					if (this.pagedDataSource.IsCustomPagingEnabled)
					{
						this.pagedDataSource.DataSource = new DummyDataSource(num);
					}
					else
					{
						this.pagedDataSource.DataSource = new DummyDataSource(dataItemCount);
					}
					enumerator = this.pagedDataSource.GetEnumerator();
					arrayList = this.CreateColumnSet(null, false);
					this.itemsArray.Capacity = num;
				}
			}
			else
			{
				dataKeysArray.Clear();
				IEnumerable data = this.GetData();
				if (data != null)
				{
					ICollection collection = data as ICollection;
					if (collection == null && this.pagedDataSource.IsPagingEnabled && !this.pagedDataSource.IsCustomPagingEnabled)
					{
						throw new HttpException(SR.GetString("DataGrid_Missing_VirtualItemCount", new object[]
						{
							this.ID
						}));
					}
					this.pagedDataSource.DataSource = data;
					if (this.pagedDataSource.IsPagingEnabled && (this.pagedDataSource.CurrentPageIndex < 0 || this.pagedDataSource.CurrentPageIndex >= this.pagedDataSource.PageCount))
					{
						throw new HttpException(SR.GetString("Invalid_CurrentPageIndex"));
					}
					arrayList = this.CreateColumnSet(this.pagedDataSource, useDataSource);
					if (this.storedDataValid)
					{
						enumerator = this.storedData;
					}
					else
					{
						enumerator = this.pagedDataSource.GetEnumerator();
					}
					if (collection != null)
					{
						int count = this.pagedDataSource.Count;
						dataKeysArray.Capacity = count;
						this.itemsArray.Capacity = count;
					}
				}
			}
			int num2 = 0;
			if (arrayList != null)
			{
				num2 = arrayList.Count;
			}
			if (num2 > 0)
			{
				DataGridColumn[] array = new DataGridColumn[num2];
				arrayList.CopyTo(array, 0);
				Table table = new ChildTable(string.IsNullOrEmpty(this.ID) ? null : this.ClientID);
				this.Controls.Add(table);
				for (int i = 0; i < array.Length; i++)
				{
					array[i].Initialize();
				}
				TableRowCollection rows = table.Rows;
				int num3 = 0;
				int num4 = 0;
				string dataKeyField = this.DataKeyField;
				bool flag = useDataSource && dataKeyField.Length != 0;
				bool isPagingEnabled = this.pagedDataSource.IsPagingEnabled;
				int editItemIndex = this.EditItemIndex;
				int selectedIndex = this.SelectedIndex;
				if (this.pagedDataSource.IsPagingEnabled)
				{
					num4 = this.pagedDataSource.FirstIndexInPage;
				}
				num = 0;
				if (isPagingEnabled)
				{
					this.CreateItem(-1, -1, ListItemType.Pager, false, null, array, rows, this.pagedDataSource);
				}
				this.CreateItem(-1, -1, ListItemType.Header, useDataSource, null, array, rows, null);
				if (this.storedDataValid && this.firstDataItem != null)
				{
					if (flag)
					{
						object propertyValue = DataBinder.GetPropertyValue(this.firstDataItem, dataKeyField);
						dataKeysArray.Add(propertyValue);
					}
					ListItemType itemType = ListItemType.Item;
					if (num3 == editItemIndex)
					{
						itemType = ListItemType.EditItem;
					}
					else if (num3 == selectedIndex)
					{
						itemType = ListItemType.SelectedItem;
					}
					DataGridItem value = this.CreateItem(0, num4, itemType, useDataSource, this.firstDataItem, array, rows, null);
					this.itemsArray.Add(value);
					num++;
					num3++;
					num4++;
					this.storedDataValid = false;
					this.firstDataItem = null;
				}
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					if (flag)
					{
						object propertyValue2 = DataBinder.GetPropertyValue(obj, dataKeyField);
						dataKeysArray.Add(propertyValue2);
					}
					ListItemType itemType = ListItemType.Item;
					if (num3 == editItemIndex)
					{
						itemType = ListItemType.EditItem;
					}
					else if (num3 == selectedIndex)
					{
						itemType = ListItemType.SelectedItem;
					}
					else if (num3 % 2 != 0)
					{
						itemType = ListItemType.AlternatingItem;
					}
					DataGridItem value = this.CreateItem(num3, num4, itemType, useDataSource, obj, array, rows, null);
					this.itemsArray.Add(value);
					num++;
					num4++;
					num3++;
				}
				this.CreateItem(-1, -1, ListItemType.Footer, useDataSource, null, array, rows, null);
				if (isPagingEnabled)
				{
					this.CreateItem(-1, -1, ListItemType.Pager, false, null, array, rows, this.pagedDataSource);
				}
			}
			if (useDataSource)
			{
				if (enumerator != null)
				{
					this.ViewState["_!ItemCount"] = num;
					if (this.pagedDataSource.IsPagingEnabled)
					{
						this.ViewState["PageCount"] = this.pagedDataSource.PageCount;
						this.ViewState["_!DataSourceItemCount"] = this.pagedDataSource.DataSourceCount;
					}
					else
					{
						this.ViewState["PageCount"] = 1;
						this.ViewState["_!DataSourceItemCount"] = num;
					}
				}
				else
				{
					this.ViewState["_!ItemCount"] = -1;
					this.ViewState["_!DataSourceItemCount"] = -1;
					this.ViewState["PageCount"] = 0;
				}
			}
			this.pagedDataSource = null;
		}

		// Token: 0x06002E3F RID: 11839 RVA: 0x0009720C File Offset: 0x0009540C
		protected override Style CreateControlStyle()
		{
			return new TableStyle
			{
				GridLines = GridLines.Both,
				CellSpacing = 0
			};
		}

		// Token: 0x06002E40 RID: 11840 RVA: 0x00097230 File Offset: 0x00095430
		private DataGridItem CreateItem(int itemIndex, int dataSourceIndex, ListItemType itemType, bool dataBind, object dataItem, DataGridColumn[] columns, TableRowCollection rows, PagedDataSource pagedDataSource)
		{
			DataGridItem dataGridItem = this.CreateItem(itemIndex, dataSourceIndex, itemType);
			DataGridItemEventArgs e = new DataGridItemEventArgs(dataGridItem);
			if (itemType != ListItemType.Pager)
			{
				this.InitializeItem(dataGridItem, columns);
				if (dataBind)
				{
					dataGridItem.DataItem = dataItem;
				}
				this.OnItemCreated(e);
				rows.Add(dataGridItem);
				if (dataBind)
				{
					dataGridItem.DataBind();
					this.OnItemDataBound(e);
					dataGridItem.DataItem = null;
				}
			}
			else
			{
				this.InitializePager(dataGridItem, columns.Length, pagedDataSource);
				this.OnItemCreated(e);
				rows.Add(dataGridItem);
			}
			return dataGridItem;
		}

		// Token: 0x06002E41 RID: 11841 RVA: 0x000972AF File Offset: 0x000954AF
		protected virtual DataGridItem CreateItem(int itemIndex, int dataSourceIndex, ListItemType itemType)
		{
			return new DataGridItem(itemIndex, dataSourceIndex, itemType);
		}

		// Token: 0x06002E42 RID: 11842 RVA: 0x000972BC File Offset: 0x000954BC
		private PagedDataSource CreatePagedDataSource()
		{
			return new PagedDataSource
			{
				CurrentPageIndex = this.CurrentPageIndex,
				PageSize = this.PageSize,
				AllowPaging = this.AllowPaging,
				AllowCustomPaging = this.AllowCustomPaging,
				VirtualCount = this.VirtualItemCount
			};
		}

		// Token: 0x06002E43 RID: 11843 RVA: 0x0009730C File Offset: 0x0009550C
		protected virtual void InitializeItem(DataGridItem item, DataGridColumn[] columns)
		{
			TableCellCollection cells = item.Cells;
			for (int i = 0; i < columns.Length; i++)
			{
				TableCell tableCell;
				if (item.ItemType == ListItemType.Header && this.UseAccessibleHeader)
				{
					tableCell = new TableHeaderCell();
					tableCell.Attributes["scope"] = "col";
				}
				else
				{
					tableCell = new TableCell();
				}
				columns[i].InitializeCell(tableCell, i, item.ItemType);
				cells.Add(tableCell);
			}
		}

		// Token: 0x06002E44 RID: 11844 RVA: 0x0009737C File Offset: 0x0009557C
		protected virtual void InitializePager(DataGridItem item, int columnSpan, PagedDataSource pagedDataSource)
		{
			TableCell tableCell = new TableCell();
			if (columnSpan > 1)
			{
				tableCell.ColumnSpan = columnSpan;
			}
			DataGridPagerStyle dataGridPagerStyle = this.PagerStyle;
			if (dataGridPagerStyle.Mode == PagerMode.NextPrev)
			{
				if (!pagedDataSource.IsFirstPage)
				{
					LinkButton linkButton = new DataGridLinkButton();
					linkButton.Text = dataGridPagerStyle.PrevPageText;
					linkButton.CommandName = "Page";
					linkButton.CommandArgument = "Prev";
					linkButton.CausesValidation = false;
					tableCell.Controls.Add(linkButton);
				}
				else
				{
					Label label = new Label();
					label.Text = dataGridPagerStyle.PrevPageText;
					tableCell.Controls.Add(label);
				}
				tableCell.Controls.Add(new LiteralControl("&nbsp;"));
				if (!pagedDataSource.IsLastPage)
				{
					LinkButton linkButton2 = new DataGridLinkButton();
					linkButton2.Text = dataGridPagerStyle.NextPageText;
					linkButton2.CommandName = "Page";
					linkButton2.CommandArgument = "Next";
					linkButton2.CausesValidation = false;
					tableCell.Controls.Add(linkButton2);
				}
				else
				{
					Label label2 = new Label();
					label2.Text = dataGridPagerStyle.NextPageText;
					tableCell.Controls.Add(label2);
				}
			}
			else
			{
				int pageCount = pagedDataSource.PageCount;
				int num = pagedDataSource.CurrentPageIndex + 1;
				int pageButtonCount = dataGridPagerStyle.PageButtonCount;
				int num2 = pageButtonCount;
				if (pageCount < num2)
				{
					num2 = pageCount;
				}
				int num3 = 1;
				int num4 = num2;
				if (num > num4)
				{
					int num5 = pagedDataSource.CurrentPageIndex / pageButtonCount;
					num3 = num5 * pageButtonCount + 1;
					num4 = num3 + pageButtonCount - 1;
					if (num4 > pageCount)
					{
						num4 = pageCount;
					}
					if (num4 - num3 + 1 < pageButtonCount)
					{
						num3 = Math.Max(1, num4 - pageButtonCount + 1);
					}
				}
				if (num3 != 1)
				{
					LinkButton linkButton3 = new DataGridLinkButton();
					linkButton3.Text = "...";
					linkButton3.CommandName = "Page";
					linkButton3.CommandArgument = (num3 - 1).ToString(NumberFormatInfo.InvariantInfo);
					linkButton3.CausesValidation = false;
					tableCell.Controls.Add(linkButton3);
					tableCell.Controls.Add(new LiteralControl("&nbsp;"));
				}
				for (int i = num3; i <= num4; i++)
				{
					string text = i.ToString(NumberFormatInfo.InvariantInfo);
					if (i == num)
					{
						Label label3 = new Label();
						label3.Text = text;
						tableCell.Controls.Add(label3);
					}
					else
					{
						LinkButton linkButton3 = new DataGridLinkButton();
						linkButton3.Text = text;
						linkButton3.CommandName = "Page";
						linkButton3.CommandArgument = text;
						linkButton3.CausesValidation = false;
						tableCell.Controls.Add(linkButton3);
					}
					if (i < num4)
					{
						tableCell.Controls.Add(new LiteralControl("&nbsp;"));
					}
				}
				if (pageCount > num4)
				{
					tableCell.Controls.Add(new LiteralControl("&nbsp;"));
					LinkButton linkButton3 = new DataGridLinkButton();
					linkButton3.Text = "...";
					linkButton3.CommandName = "Page";
					linkButton3.CommandArgument = (num4 + 1).ToString(NumberFormatInfo.InvariantInfo);
					linkButton3.CausesValidation = false;
					tableCell.Controls.Add(linkButton3);
				}
			}
			item.Cells.Add(tableCell);
		}

		// Token: 0x06002E45 RID: 11845 RVA: 0x0009768C File Offset: 0x0009588C
		protected override void LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				object[] array = (object[])savedState;
				if (array[0] != null)
				{
					base.LoadViewState(array[0]);
				}
				if (array[1] != null)
				{
					((IStateManager)this.Columns).LoadViewState(array[1]);
				}
				if (array[2] != null)
				{
					((IStateManager)this.PagerStyle).LoadViewState(array[2]);
				}
				if (array[3] != null)
				{
					((IStateManager)this.HeaderStyle).LoadViewState(array[3]);
				}
				if (array[4] != null)
				{
					((IStateManager)this.FooterStyle).LoadViewState(array[4]);
				}
				if (array[5] != null)
				{
					((IStateManager)this.ItemStyle).LoadViewState(array[5]);
				}
				if (array[6] != null)
				{
					((IStateManager)this.AlternatingItemStyle).LoadViewState(array[6]);
				}
				if (array[7] != null)
				{
					((IStateManager)this.SelectedItemStyle).LoadViewState(array[7]);
				}
				if (array[8] != null)
				{
					((IStateManager)this.EditItemStyle).LoadViewState(array[8]);
				}
				if (array[9] != null)
				{
					((IStateManager)base.ControlStyle).LoadViewState(array[9]);
				}
				if (array[10] != null)
				{
					object[] array2 = (object[])array[10];
					int num = array2.Length;
					if (num != 0)
					{
						this.autoGenColumnsArray = new ArrayList();
					}
					else
					{
						this.autoGenColumnsArray = null;
					}
					for (int i = 0; i < num; i++)
					{
						BoundColumn boundColumn = new BoundColumn();
						((IStateManager)boundColumn).TrackViewState();
						((IStateManager)boundColumn).LoadViewState(array2[i]);
						boundColumn.SetOwner(this);
						this.autoGenColumnsArray.Add(boundColumn);
					}
				}
			}
		}

		// Token: 0x06002E46 RID: 11846 RVA: 0x000977C8 File Offset: 0x000959C8
		protected override bool OnBubbleEvent(object source, EventArgs e)
		{
			bool result = false;
			if (e is DataGridCommandEventArgs)
			{
				DataGridCommandEventArgs dataGridCommandEventArgs = (DataGridCommandEventArgs)e;
				this.OnItemCommand(dataGridCommandEventArgs);
				result = true;
				string commandName = dataGridCommandEventArgs.CommandName;
				if (StringUtil.EqualsIgnoreCase(commandName, "Select"))
				{
					this.SelectedIndex = dataGridCommandEventArgs.Item.ItemIndex;
					this.OnSelectedIndexChanged(EventArgs.Empty);
				}
				else if (StringUtil.EqualsIgnoreCase(commandName, "Page"))
				{
					string text = (string)dataGridCommandEventArgs.CommandArgument;
					int num = this.CurrentPageIndex;
					if (StringUtil.EqualsIgnoreCase(text, "Next"))
					{
						num++;
					}
					else if (StringUtil.EqualsIgnoreCase(text, "Prev"))
					{
						num--;
					}
					else
					{
						num = int.Parse(text, CultureInfo.InvariantCulture) - 1;
					}
					DataGridPageChangedEventArgs e2 = new DataGridPageChangedEventArgs(source, num);
					this.OnPageIndexChanged(e2);
				}
				else if (StringUtil.EqualsIgnoreCase(commandName, "Sort"))
				{
					DataGridSortCommandEventArgs e3 = new DataGridSortCommandEventArgs(source, dataGridCommandEventArgs);
					this.OnSortCommand(e3);
				}
				else if (StringUtil.EqualsIgnoreCase(commandName, "Edit"))
				{
					this.OnEditCommand(dataGridCommandEventArgs);
				}
				else if (StringUtil.EqualsIgnoreCase(commandName, "Update"))
				{
					this.OnUpdateCommand(dataGridCommandEventArgs);
				}
				else if (StringUtil.EqualsIgnoreCase(commandName, "Cancel"))
				{
					this.OnCancelCommand(dataGridCommandEventArgs);
				}
				else if (StringUtil.EqualsIgnoreCase(commandName, "Delete"))
				{
					this.OnDeleteCommand(dataGridCommandEventArgs);
				}
			}
			return result;
		}

		// Token: 0x06002E47 RID: 11847 RVA: 0x0009790C File Offset: 0x00095B0C
		internal void OnColumnsChanged()
		{
			if (base.Initialized)
			{
				base.RequiresDataBinding = true;
			}
		}

		// Token: 0x06002E48 RID: 11848 RVA: 0x00097920 File Offset: 0x00095B20
		protected virtual void OnCancelCommand(DataGridCommandEventArgs e)
		{
			DataGridCommandEventHandler dataGridCommandEventHandler = (DataGridCommandEventHandler)base.Events[DataGrid.EventCancelCommand];
			if (dataGridCommandEventHandler != null)
			{
				dataGridCommandEventHandler(this, e);
			}
		}

		// Token: 0x06002E49 RID: 11849 RVA: 0x00097950 File Offset: 0x00095B50
		protected virtual void OnDeleteCommand(DataGridCommandEventArgs e)
		{
			DataGridCommandEventHandler dataGridCommandEventHandler = (DataGridCommandEventHandler)base.Events[DataGrid.EventDeleteCommand];
			if (dataGridCommandEventHandler != null)
			{
				dataGridCommandEventHandler(this, e);
			}
		}

		// Token: 0x06002E4A RID: 11850 RVA: 0x00097980 File Offset: 0x00095B80
		protected virtual void OnEditCommand(DataGridCommandEventArgs e)
		{
			DataGridCommandEventHandler dataGridCommandEventHandler = (DataGridCommandEventHandler)base.Events[DataGrid.EventEditCommand];
			if (dataGridCommandEventHandler != null)
			{
				dataGridCommandEventHandler(this, e);
			}
		}

		// Token: 0x06002E4B RID: 11851 RVA: 0x000979B0 File Offset: 0x00095BB0
		protected virtual void OnItemCommand(DataGridCommandEventArgs e)
		{
			DataGridCommandEventHandler dataGridCommandEventHandler = (DataGridCommandEventHandler)base.Events[DataGrid.EventItemCommand];
			if (dataGridCommandEventHandler != null)
			{
				dataGridCommandEventHandler(this, e);
			}
		}

		// Token: 0x06002E4C RID: 11852 RVA: 0x000979E0 File Offset: 0x00095BE0
		protected virtual void OnItemCreated(DataGridItemEventArgs e)
		{
			DataGridItemEventHandler dataGridItemEventHandler = (DataGridItemEventHandler)base.Events[DataGrid.EventItemCreated];
			if (dataGridItemEventHandler != null)
			{
				dataGridItemEventHandler(this, e);
			}
		}

		// Token: 0x06002E4D RID: 11853 RVA: 0x00097A10 File Offset: 0x00095C10
		protected virtual void OnItemDataBound(DataGridItemEventArgs e)
		{
			DataGridItemEventHandler dataGridItemEventHandler = (DataGridItemEventHandler)base.Events[DataGrid.EventItemDataBound];
			if (dataGridItemEventHandler != null)
			{
				dataGridItemEventHandler(this, e);
			}
		}

		// Token: 0x06002E4E RID: 11854 RVA: 0x00097A40 File Offset: 0x00095C40
		protected virtual void OnPageIndexChanged(DataGridPageChangedEventArgs e)
		{
			DataGridPageChangedEventHandler dataGridPageChangedEventHandler = (DataGridPageChangedEventHandler)base.Events[DataGrid.EventPageIndexChanged];
			if (dataGridPageChangedEventHandler != null)
			{
				dataGridPageChangedEventHandler(this, e);
			}
		}

		// Token: 0x06002E4F RID: 11855 RVA: 0x00006164 File Offset: 0x00004364
		internal void OnPagerChanged()
		{
		}

		// Token: 0x06002E50 RID: 11856 RVA: 0x00097A70 File Offset: 0x00095C70
		protected virtual void OnSortCommand(DataGridSortCommandEventArgs e)
		{
			DataGridSortCommandEventHandler dataGridSortCommandEventHandler = (DataGridSortCommandEventHandler)base.Events[DataGrid.EventSortCommand];
			if (dataGridSortCommandEventHandler != null)
			{
				dataGridSortCommandEventHandler(this, e);
			}
		}

		// Token: 0x06002E51 RID: 11857 RVA: 0x00097AA0 File Offset: 0x00095CA0
		protected virtual void OnUpdateCommand(DataGridCommandEventArgs e)
		{
			DataGridCommandEventHandler dataGridCommandEventHandler = (DataGridCommandEventHandler)base.Events[DataGrid.EventUpdateCommand];
			if (dataGridCommandEventHandler != null)
			{
				dataGridCommandEventHandler(this, e);
			}
		}

		// Token: 0x06002E52 RID: 11858 RVA: 0x00097AD0 File Offset: 0x00095CD0
		protected internal override void PrepareControlHierarchy()
		{
			if (this.Controls.Count == 0)
			{
				return;
			}
			Table table = (Table)this.Controls[0];
			table.CopyBaseAttributes(this);
			table.Caption = this.Caption;
			table.CaptionAlign = this.CaptionAlign;
			if (base.ControlStyleCreated)
			{
				table.ApplyStyle(base.ControlStyle);
			}
			else
			{
				table.GridLines = GridLines.Both;
				table.CellSpacing = 0;
			}
			TableRowCollection rows = table.Rows;
			int count = rows.Count;
			if (count == 0)
			{
				return;
			}
			int count2 = this.Columns.Count;
			DataGridColumn[] array = new DataGridColumn[count2];
			if (count2 > 0)
			{
				this.Columns.CopyTo(array, 0);
			}
			Style style;
			if (this.alternatingItemStyle != null)
			{
				style = new TableItemStyle();
				style.CopyFrom(this.itemStyle);
				style.CopyFrom(this.alternatingItemStyle);
			}
			else
			{
				style = this.itemStyle;
			}
			int num = 0;
			bool flag = true;
			int i = 0;
			while (i < count)
			{
				DataGridItem dataGridItem = (DataGridItem)rows[i];
				switch (dataGridItem.ItemType)
				{
				case ListItemType.Header:
					if (!this.ShowHeader)
					{
						dataGridItem.Visible = false;
					}
					else
					{
						if (this.headerStyle != null)
						{
							dataGridItem.MergeStyle(this.headerStyle);
							goto IL_29E;
						}
						goto IL_29E;
					}
					break;
				case ListItemType.Footer:
					if (this.ShowFooter)
					{
						dataGridItem.MergeStyle(this.footerStyle);
						goto IL_29E;
					}
					dataGridItem.Visible = false;
					break;
				case ListItemType.Item:
					dataGridItem.MergeStyle(this.itemStyle);
					goto IL_29E;
				case ListItemType.AlternatingItem:
					dataGridItem.MergeStyle(style);
					goto IL_29E;
				case ListItemType.SelectedItem:
				{
					Style style2 = new TableItemStyle();
					if (dataGridItem.ItemIndex % 2 != 0)
					{
						style2.CopyFrom(style);
					}
					else
					{
						style2.CopyFrom(this.itemStyle);
					}
					style2.CopyFrom(this.selectedItemStyle);
					dataGridItem.MergeStyle(style2);
					goto IL_29E;
				}
				case ListItemType.EditItem:
				{
					Style style3 = new TableItemStyle();
					if (dataGridItem.ItemIndex % 2 != 0)
					{
						style3.CopyFrom(style);
					}
					else
					{
						style3.CopyFrom(this.itemStyle);
					}
					if (dataGridItem.ItemIndex == this.SelectedIndex)
					{
						style3.CopyFrom(this.selectedItemStyle);
					}
					style3.CopyFrom(this.editItemStyle);
					dataGridItem.MergeStyle(style3);
					goto IL_29E;
				}
				case ListItemType.Separator:
					goto IL_29E;
				case ListItemType.Pager:
					if (this.pagerStyle.Visible)
					{
						if (i == 0)
						{
							if (!this.pagerStyle.IsPagerOnTop)
							{
								dataGridItem.Visible = false;
								break;
							}
						}
						else if (!this.pagerStyle.IsPagerOnBottom)
						{
							dataGridItem.Visible = false;
							break;
						}
						dataGridItem.MergeStyle(this.pagerStyle);
						goto IL_29E;
					}
					dataGridItem.Visible = false;
					break;
				default:
					goto IL_29E;
				}
				IL_375:
				i++;
				continue;
				IL_29E:
				TableCellCollection cells = dataGridItem.Cells;
				int count3 = cells.Count;
				if (count2 <= 0 || dataGridItem.ItemType == ListItemType.Pager)
				{
					goto IL_375;
				}
				int num2 = count3;
				if (count2 < count3)
				{
					num2 = count2;
				}
				for (int j = 0; j < num2; j++)
				{
					if (!array[j].Visible)
					{
						cells[j].Visible = false;
					}
					else
					{
						if (dataGridItem.ItemType == ListItemType.Item && flag)
						{
							num++;
						}
						ListItemType itemType = dataGridItem.ItemType;
						Style s;
						if (itemType != ListItemType.Header)
						{
							if (itemType != ListItemType.Footer)
							{
								s = array[j].ItemStyleInternal;
							}
							else
							{
								s = array[j].FooterStyleInternal;
							}
						}
						else
						{
							s = array[j].HeaderStyleInternal;
						}
						cells[j].MergeStyle(s);
					}
				}
				if (dataGridItem.ItemType == ListItemType.Item)
				{
					flag = false;
					goto IL_375;
				}
				goto IL_375;
			}
			if (this.Items.Count > 0 && num != this.Items[0].Cells.Count && this.AllowPaging)
			{
				for (int k = 0; k < count; k++)
				{
					DataGridItem dataGridItem2 = (DataGridItem)rows[k];
					if (dataGridItem2.ItemType == ListItemType.Pager && dataGridItem2.Cells.Count > 0)
					{
						dataGridItem2.Cells[0].ColumnSpan = num;
					}
				}
			}
		}

		// Token: 0x06002E53 RID: 11859 RVA: 0x00097EDC File Offset: 0x000960DC
		protected override object SaveViewState()
		{
			object obj = base.SaveViewState();
			object obj2 = (this.columnCollection != null) ? ((IStateManager)this.columnCollection).SaveViewState() : null;
			object obj3 = (this.pagerStyle != null) ? ((IStateManager)this.pagerStyle).SaveViewState() : null;
			object obj4 = (this.headerStyle != null) ? ((IStateManager)this.headerStyle).SaveViewState() : null;
			object obj5 = (this.footerStyle != null) ? ((IStateManager)this.footerStyle).SaveViewState() : null;
			object obj6 = (this.itemStyle != null) ? ((IStateManager)this.itemStyle).SaveViewState() : null;
			object obj7 = (this.alternatingItemStyle != null) ? ((IStateManager)this.alternatingItemStyle).SaveViewState() : null;
			object obj8 = (this.selectedItemStyle != null) ? ((IStateManager)this.selectedItemStyle).SaveViewState() : null;
			object obj9 = (this.editItemStyle != null) ? ((IStateManager)this.editItemStyle).SaveViewState() : null;
			object obj10 = base.ControlStyleCreated ? ((IStateManager)base.ControlStyle).SaveViewState() : null;
			object[] array = null;
			if (this.autoGenColumnsArray != null && this.autoGenColumnsArray.Count != 0)
			{
				array = new object[this.autoGenColumnsArray.Count];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = ((IStateManager)this.autoGenColumnsArray[i]).SaveViewState();
				}
			}
			return new object[]
			{
				obj,
				obj2,
				obj3,
				obj4,
				obj5,
				obj6,
				obj7,
				obj8,
				obj9,
				obj10,
				array
			};
		}

		// Token: 0x06002E54 RID: 11860 RVA: 0x0009806C File Offset: 0x0009626C
		protected override void TrackViewState()
		{
			base.TrackViewState();
			if (this.columnCollection != null)
			{
				((IStateManager)this.columnCollection).TrackViewState();
			}
			if (this.pagerStyle != null)
			{
				((IStateManager)this.pagerStyle).TrackViewState();
			}
			if (this.headerStyle != null)
			{
				((IStateManager)this.headerStyle).TrackViewState();
			}
			if (this.footerStyle != null)
			{
				((IStateManager)this.footerStyle).TrackViewState();
			}
			if (this.itemStyle != null)
			{
				((IStateManager)this.itemStyle).TrackViewState();
			}
			if (this.alternatingItemStyle != null)
			{
				((IStateManager)this.alternatingItemStyle).TrackViewState();
			}
			if (this.selectedItemStyle != null)
			{
				((IStateManager)this.selectedItemStyle).TrackViewState();
			}
			if (this.editItemStyle != null)
			{
				((IStateManager)this.editItemStyle).TrackViewState();
			}
			if (base.ControlStyleCreated)
			{
				((IStateManager)base.ControlStyle).TrackViewState();
			}
		}

		// Token: 0x04001FC8 RID: 8136
		private static readonly object EventCancelCommand = new object();

		// Token: 0x04001FC9 RID: 8137
		private static readonly object EventDeleteCommand = new object();

		// Token: 0x04001FCA RID: 8138
		private static readonly object EventEditCommand = new object();

		// Token: 0x04001FCB RID: 8139
		private static readonly object EventItemCommand = new object();

		// Token: 0x04001FCC RID: 8140
		private static readonly object EventItemCreated = new object();

		// Token: 0x04001FCD RID: 8141
		private static readonly object EventItemDataBound = new object();

		// Token: 0x04001FCE RID: 8142
		private static readonly object EventPageIndexChanged = new object();

		// Token: 0x04001FCF RID: 8143
		private static readonly object EventSortCommand = new object();

		// Token: 0x04001FD0 RID: 8144
		private static readonly object EventUpdateCommand = new object();

		// Token: 0x04001FD1 RID: 8145
		public const string SortCommandName = "Sort";

		// Token: 0x04001FD2 RID: 8146
		public const string SelectCommandName = "Select";

		// Token: 0x04001FD3 RID: 8147
		public const string EditCommandName = "Edit";

		// Token: 0x04001FD4 RID: 8148
		public const string DeleteCommandName = "Delete";

		// Token: 0x04001FD5 RID: 8149
		public const string UpdateCommandName = "Update";

		// Token: 0x04001FD6 RID: 8150
		public const string CancelCommandName = "Cancel";

		// Token: 0x04001FD7 RID: 8151
		public const string PageCommandName = "Page";

		// Token: 0x04001FD8 RID: 8152
		public const string NextPageCommandArgument = "Next";

		// Token: 0x04001FD9 RID: 8153
		public const string PrevPageCommandArgument = "Prev";

		// Token: 0x04001FDA RID: 8154
		internal const string DataSourceItemCountViewStateKey = "_!DataSourceItemCount";

		// Token: 0x04001FDB RID: 8155
		private IEnumerator storedData;

		// Token: 0x04001FDC RID: 8156
		private object firstDataItem;

		// Token: 0x04001FDD RID: 8157
		private bool storedDataValid;

		// Token: 0x04001FDE RID: 8158
		private PagedDataSource pagedDataSource;

		// Token: 0x04001FDF RID: 8159
		private ArrayList columns;

		// Token: 0x04001FE0 RID: 8160
		private DataGridColumnCollection columnCollection;

		// Token: 0x04001FE1 RID: 8161
		private TableItemStyle headerStyle;

		// Token: 0x04001FE2 RID: 8162
		private TableItemStyle footerStyle;

		// Token: 0x04001FE3 RID: 8163
		private TableItemStyle itemStyle;

		// Token: 0x04001FE4 RID: 8164
		private TableItemStyle alternatingItemStyle;

		// Token: 0x04001FE5 RID: 8165
		private TableItemStyle selectedItemStyle;

		// Token: 0x04001FE6 RID: 8166
		private TableItemStyle editItemStyle;

		// Token: 0x04001FE7 RID: 8167
		private DataGridPagerStyle pagerStyle;

		// Token: 0x04001FE8 RID: 8168
		private ArrayList itemsArray;

		// Token: 0x04001FE9 RID: 8169
		private DataGridItemCollection itemsCollection;

		// Token: 0x04001FEA RID: 8170
		private ArrayList autoGenColumnsArray;
	}
}
