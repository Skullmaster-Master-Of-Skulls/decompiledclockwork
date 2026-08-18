using System;
using System.Collections;
using System.ComponentModel;
using System.Web.Util;

namespace System.Web.UI.WebControls
{
	// Token: 0x020003CC RID: 972
	[ControlValueProperty("SelectedValue")]
	[Editor("System.Web.UI.Design.WebControls.DataListComponentEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(ComponentEditor))]
	[Designer("System.Web.UI.Design.WebControls.DataListDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public class DataList : BaseDataList, INamingContainer, IRepeatInfoUser, IWizardSideBarListControl
	{
		// Token: 0x06002EBD RID: 11965 RVA: 0x00098DA9 File Offset: 0x00096FA9
		public DataList()
		{
			this.offset = 0;
			this.visibleItemCount = -1;
		}

		// Token: 0x17000D62 RID: 3426
		// (get) Token: 0x06002EBE RID: 11966 RVA: 0x00098DC6 File Offset: 0x00096FC6
		[WebCategory("Styles")]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("DataList_AlternatingItemStyle")]
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

		// Token: 0x17000D63 RID: 3427
		// (get) Token: 0x06002EBF RID: 11967 RVA: 0x00098DF4 File Offset: 0x00096FF4
		// (set) Token: 0x06002EC0 RID: 11968 RVA: 0x00098DFC File Offset: 0x00096FFC
		[Browsable(false)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(DataListItem))]
		[WebSysDescription("DataList_AlternatingItemTemplate")]
		public virtual ITemplate AlternatingItemTemplate
		{
			get
			{
				return this.alternatingItemTemplate;
			}
			set
			{
				this.alternatingItemTemplate = value;
			}
		}

		// Token: 0x17000D64 RID: 3428
		// (get) Token: 0x06002EC1 RID: 11969 RVA: 0x00098E08 File Offset: 0x00097008
		// (set) Token: 0x06002EC2 RID: 11970 RVA: 0x000964D5 File Offset: 0x000946D5
		[WebCategory("Default")]
		[DefaultValue(-1)]
		[WebSysDescription("DataList_EditItemIndex")]
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

		// Token: 0x17000D65 RID: 3429
		// (get) Token: 0x06002EC3 RID: 11971 RVA: 0x00098E31 File Offset: 0x00097031
		[WebCategory("Styles")]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("DataList_EditItemStyle")]
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

		// Token: 0x17000D66 RID: 3430
		// (get) Token: 0x06002EC4 RID: 11972 RVA: 0x00098E5F File Offset: 0x0009705F
		// (set) Token: 0x06002EC5 RID: 11973 RVA: 0x00098E67 File Offset: 0x00097067
		[Browsable(false)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(DataListItem))]
		[WebSysDescription("DataList_EditItemTemplate")]
		public virtual ITemplate EditItemTemplate
		{
			get
			{
				return this.editItemTemplate;
			}
			set
			{
				this.editItemTemplate = value;
			}
		}

		// Token: 0x17000D67 RID: 3431
		// (get) Token: 0x06002EC6 RID: 11974 RVA: 0x00098E70 File Offset: 0x00097070
		// (set) Token: 0x06002EC7 RID: 11975 RVA: 0x00098E99 File Offset: 0x00097099
		[WebCategory("Layout")]
		[DefaultValue(false)]
		[WebSysDescription("DataList_ExtractTemplateRows")]
		public virtual bool ExtractTemplateRows
		{
			get
			{
				object obj = this.ViewState["ExtractTemplateRows"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["ExtractTemplateRows"] = value;
			}
		}

		// Token: 0x17000D68 RID: 3432
		// (get) Token: 0x06002EC8 RID: 11976 RVA: 0x00098EB1 File Offset: 0x000970B1
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

		// Token: 0x17000D69 RID: 3433
		// (get) Token: 0x06002EC9 RID: 11977 RVA: 0x00098EDF File Offset: 0x000970DF
		// (set) Token: 0x06002ECA RID: 11978 RVA: 0x00098EE7 File Offset: 0x000970E7
		[Browsable(false)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(DataListItem))]
		[WebSysDescription("DataList_FooterTemplate")]
		public virtual ITemplate FooterTemplate
		{
			get
			{
				return this.footerTemplate;
			}
			set
			{
				this.footerTemplate = value;
			}
		}

		// Token: 0x17000D6A RID: 3434
		// (get) Token: 0x06002ECB RID: 11979 RVA: 0x00098EF0 File Offset: 0x000970F0
		// (set) Token: 0x06002ECC RID: 11980 RVA: 0x00098F0C File Offset: 0x0009710C
		[DefaultValue(GridLines.None)]
		public override GridLines GridLines
		{
			get
			{
				if (!base.ControlStyleCreated)
				{
					return GridLines.None;
				}
				return ((TableStyle)base.ControlStyle).GridLines;
			}
			set
			{
				base.GridLines = value;
			}
		}

		// Token: 0x17000D6B RID: 3435
		// (get) Token: 0x06002ECD RID: 11981 RVA: 0x00098F15 File Offset: 0x00097115
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

		// Token: 0x17000D6C RID: 3436
		// (get) Token: 0x06002ECE RID: 11982 RVA: 0x00098F43 File Offset: 0x00097143
		// (set) Token: 0x06002ECF RID: 11983 RVA: 0x00098F4B File Offset: 0x0009714B
		[Browsable(false)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(DataListItem))]
		[WebSysDescription("DataList_HeaderTemplate")]
		public virtual ITemplate HeaderTemplate
		{
			get
			{
				return this.headerTemplate;
			}
			set
			{
				this.headerTemplate = value;
			}
		}

		// Token: 0x17000D6D RID: 3437
		// (get) Token: 0x06002ED0 RID: 11984 RVA: 0x00098F54 File Offset: 0x00097154
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("DataList_Items")]
		public virtual DataListItemCollection Items
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
					this.itemsCollection = new DataListItemCollection(this.itemsArray);
				}
				return this.itemsCollection;
			}
		}

		// Token: 0x17000D6E RID: 3438
		// (get) Token: 0x06002ED1 RID: 11985 RVA: 0x00098FA1 File Offset: 0x000971A1
		[WebCategory("Styles")]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("DataList_ItemStyle")]
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

		// Token: 0x17000D6F RID: 3439
		// (get) Token: 0x06002ED2 RID: 11986 RVA: 0x00098FCF File Offset: 0x000971CF
		// (set) Token: 0x06002ED3 RID: 11987 RVA: 0x00098FD7 File Offset: 0x000971D7
		[Browsable(false)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(DataListItem))]
		[WebSysDescription("DataList_ItemTemplate")]
		public virtual ITemplate ItemTemplate
		{
			get
			{
				return this.itemTemplate;
			}
			set
			{
				this.itemTemplate = value;
			}
		}

		// Token: 0x17000D70 RID: 3440
		// (get) Token: 0x06002ED4 RID: 11988 RVA: 0x00098FE0 File Offset: 0x000971E0
		// (set) Token: 0x06002ED5 RID: 11989 RVA: 0x0008E71D File Offset: 0x0008C91D
		[WebCategory("Layout")]
		[DefaultValue(0)]
		[WebSysDescription("DataList_RepeatColumns")]
		public virtual int RepeatColumns
		{
			get
			{
				object obj = this.ViewState["RepeatColumns"];
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
				this.ViewState["RepeatColumns"] = value;
			}
		}

		// Token: 0x17000D71 RID: 3441
		// (get) Token: 0x06002ED6 RID: 11990 RVA: 0x0009900C File Offset: 0x0009720C
		// (set) Token: 0x06002ED7 RID: 11991 RVA: 0x0008E76D File Offset: 0x0008C96D
		[WebCategory("Layout")]
		[DefaultValue(RepeatDirection.Vertical)]
		[WebSysDescription("Item_RepeatDirection")]
		public virtual RepeatDirection RepeatDirection
		{
			get
			{
				object obj = this.ViewState["RepeatDirection"];
				if (obj != null)
				{
					return (RepeatDirection)obj;
				}
				return RepeatDirection.Vertical;
			}
			set
			{
				if (value < RepeatDirection.Horizontal || value > RepeatDirection.Vertical)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["RepeatDirection"] = value;
			}
		}

		// Token: 0x17000D72 RID: 3442
		// (get) Token: 0x06002ED8 RID: 11992 RVA: 0x00099038 File Offset: 0x00097238
		// (set) Token: 0x06002ED9 RID: 11993 RVA: 0x00099064 File Offset: 0x00097264
		[WebCategory("Layout")]
		[DefaultValue(RepeatLayout.Table)]
		[WebSysDescription("WebControl_RepeatLayout")]
		public virtual RepeatLayout RepeatLayout
		{
			get
			{
				object obj = this.ViewState["RepeatLayout"];
				if (obj != null)
				{
					return (RepeatLayout)obj;
				}
				return RepeatLayout.Table;
			}
			set
			{
				if (value == RepeatLayout.UnorderedList || value == RepeatLayout.OrderedList)
				{
					throw new ArgumentOutOfRangeException("value", SR.GetString("DataList_LayoutNotSupported", new object[]
					{
						value
					}));
				}
				EnumerationRangeValidationUtil.ValidateRepeatLayout(value);
				this.ViewState["RepeatLayout"] = value;
			}
		}

		// Token: 0x17000D73 RID: 3443
		// (get) Token: 0x06002EDA RID: 11994 RVA: 0x000990BC File Offset: 0x000972BC
		// (set) Token: 0x06002EDB RID: 11995 RVA: 0x000990E8 File Offset: 0x000972E8
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
						DataListItem dataListItem = (DataListItem)this.itemsArray[selectedIndex];
						if (dataListItem.ItemType != ListItemType.EditItem)
						{
							ListItemType itemType = ListItemType.Item;
							if (selectedIndex % 2 != 0)
							{
								itemType = ListItemType.AlternatingItem;
							}
							dataListItem.SetItemType(itemType);
						}
					}
					if (value != -1 && this.itemsArray.Count > value)
					{
						DataListItem dataListItem = (DataListItem)this.itemsArray[value];
						if (dataListItem.ItemType != ListItemType.EditItem)
						{
							dataListItem.SetItemType(ListItemType.SelectedItem);
						}
					}
				}
			}
		}

		// Token: 0x17000D74 RID: 3444
		// (get) Token: 0x06002EDC RID: 11996 RVA: 0x0009919C File Offset: 0x0009739C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("DataList_SelectedItem")]
		public virtual DataListItem SelectedItem
		{
			get
			{
				int selectedIndex = this.SelectedIndex;
				DataListItem result = null;
				if (selectedIndex != -1)
				{
					result = this.Items[selectedIndex];
				}
				return result;
			}
		}

		// Token: 0x17000D75 RID: 3445
		// (get) Token: 0x06002EDD RID: 11997 RVA: 0x000991C4 File Offset: 0x000973C4
		[WebCategory("Styles")]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("DataList_SelectedItemStyle")]
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

		// Token: 0x17000D76 RID: 3446
		// (get) Token: 0x06002EDE RID: 11998 RVA: 0x000991F2 File Offset: 0x000973F2
		// (set) Token: 0x06002EDF RID: 11999 RVA: 0x000991FA File Offset: 0x000973FA
		[Browsable(false)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(DataListItem))]
		[WebSysDescription("DataList_SelectedItemTemplate")]
		public virtual ITemplate SelectedItemTemplate
		{
			get
			{
				return this.selectedItemTemplate;
			}
			set
			{
				this.selectedItemTemplate = value;
			}
		}

		// Token: 0x17000D77 RID: 3447
		// (get) Token: 0x06002EE0 RID: 12000 RVA: 0x00099204 File Offset: 0x00097404
		[Browsable(false)]
		public object SelectedValue
		{
			get
			{
				if (this.DataKeyField.Length == 0)
				{
					throw new InvalidOperationException(SR.GetString("DataList_DataKeyFieldMustBeSpecified", new object[]
					{
						this.ID
					}));
				}
				DataKeyCollection dataKeys = base.DataKeys;
				int selectedIndex = this.SelectedIndex;
				if (dataKeys != null && selectedIndex < dataKeys.Count && selectedIndex > -1)
				{
					return dataKeys[selectedIndex];
				}
				return null;
			}
		}

		// Token: 0x17000D78 RID: 3448
		// (get) Token: 0x06002EE1 RID: 12001 RVA: 0x00099264 File Offset: 0x00097464
		[WebCategory("Styles")]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("DataList_SeparatorStyle")]
		public virtual TableItemStyle SeparatorStyle
		{
			get
			{
				if (this.separatorStyle == null)
				{
					this.separatorStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.separatorStyle).TrackViewState();
					}
				}
				return this.separatorStyle;
			}
		}

		// Token: 0x17000D79 RID: 3449
		// (get) Token: 0x06002EE2 RID: 12002 RVA: 0x00099292 File Offset: 0x00097492
		// (set) Token: 0x06002EE3 RID: 12003 RVA: 0x0009929A File Offset: 0x0009749A
		[Browsable(false)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(DataListItem))]
		[WebSysDescription("DataList_SeparatorTemplate")]
		public virtual ITemplate SeparatorTemplate
		{
			get
			{
				return this.separatorTemplate;
			}
			set
			{
				this.separatorTemplate = value;
			}
		}

		// Token: 0x17000D7A RID: 3450
		// (get) Token: 0x06002EE4 RID: 12004 RVA: 0x000992A4 File Offset: 0x000974A4
		// (set) Token: 0x06002EE5 RID: 12005 RVA: 0x00096825 File Offset: 0x00094A25
		[WebCategory("Appearance")]
		[DefaultValue(true)]
		[WebSysDescription("DataControls_ShowFooter")]
		public virtual bool ShowFooter
		{
			get
			{
				object obj = this.ViewState["ShowFooter"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["ShowFooter"] = value;
			}
		}

		// Token: 0x17000D7B RID: 3451
		// (get) Token: 0x06002EE6 RID: 12006 RVA: 0x000992D0 File Offset: 0x000974D0
		// (set) Token: 0x06002EE7 RID: 12007 RVA: 0x00096869 File Offset: 0x00094A69
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

		// Token: 0x17000D7C RID: 3452
		// (get) Token: 0x06002EE8 RID: 12008 RVA: 0x000992F9 File Offset: 0x000974F9
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				if (this.RepeatLayout != RepeatLayout.Table)
				{
					return HtmlTextWriterTag.Span;
				}
				return HtmlTextWriterTag.Table;
			}
		}

		// Token: 0x14000075 RID: 117
		// (add) Token: 0x06002EE9 RID: 12009 RVA: 0x00099308 File Offset: 0x00097508
		// (remove) Token: 0x06002EEA RID: 12010 RVA: 0x0009931B File Offset: 0x0009751B
		[WebCategory("Action")]
		[WebSysDescription("DataList_OnCancelCommand")]
		public event DataListCommandEventHandler CancelCommand
		{
			add
			{
				base.Events.AddHandler(DataList.EventCancelCommand, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataList.EventCancelCommand, value);
			}
		}

		// Token: 0x14000076 RID: 118
		// (add) Token: 0x06002EEB RID: 12011 RVA: 0x0009932E File Offset: 0x0009752E
		// (remove) Token: 0x06002EEC RID: 12012 RVA: 0x00099341 File Offset: 0x00097541
		[WebCategory("Action")]
		[WebSysDescription("DataList_OnDeleteCommand")]
		public event DataListCommandEventHandler DeleteCommand
		{
			add
			{
				base.Events.AddHandler(DataList.EventDeleteCommand, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataList.EventDeleteCommand, value);
			}
		}

		// Token: 0x14000077 RID: 119
		// (add) Token: 0x06002EED RID: 12013 RVA: 0x00099354 File Offset: 0x00097554
		// (remove) Token: 0x06002EEE RID: 12014 RVA: 0x00099367 File Offset: 0x00097567
		[WebCategory("Action")]
		[WebSysDescription("DataList_OnEditCommand")]
		public event DataListCommandEventHandler EditCommand
		{
			add
			{
				base.Events.AddHandler(DataList.EventEditCommand, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataList.EventEditCommand, value);
			}
		}

		// Token: 0x14000078 RID: 120
		// (add) Token: 0x06002EEF RID: 12015 RVA: 0x0009937A File Offset: 0x0009757A
		// (remove) Token: 0x06002EF0 RID: 12016 RVA: 0x0009938D File Offset: 0x0009758D
		[WebCategory("Action")]
		[WebSysDescription("DataList_OnItemCommand")]
		public event DataListCommandEventHandler ItemCommand
		{
			add
			{
				base.Events.AddHandler(DataList.EventItemCommand, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataList.EventItemCommand, value);
			}
		}

		// Token: 0x14000079 RID: 121
		// (add) Token: 0x06002EF1 RID: 12017 RVA: 0x000993A0 File Offset: 0x000975A0
		// (remove) Token: 0x06002EF2 RID: 12018 RVA: 0x000993B3 File Offset: 0x000975B3
		[WebCategory("Behavior")]
		[WebSysDescription("DataControls_OnItemCreated")]
		public event DataListItemEventHandler ItemCreated
		{
			add
			{
				base.Events.AddHandler(DataList.EventItemCreated, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataList.EventItemCreated, value);
			}
		}

		// Token: 0x1400007A RID: 122
		// (add) Token: 0x06002EF3 RID: 12019 RVA: 0x000993C6 File Offset: 0x000975C6
		// (remove) Token: 0x06002EF4 RID: 12020 RVA: 0x000993D9 File Offset: 0x000975D9
		[WebCategory("Behavior")]
		[WebSysDescription("DataControls_OnItemDataBound")]
		public event DataListItemEventHandler ItemDataBound
		{
			add
			{
				base.Events.AddHandler(DataList.EventItemDataBound, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataList.EventItemDataBound, value);
			}
		}

		// Token: 0x1400007B RID: 123
		// (add) Token: 0x06002EF5 RID: 12021 RVA: 0x000993EC File Offset: 0x000975EC
		// (remove) Token: 0x06002EF6 RID: 12022 RVA: 0x000993FF File Offset: 0x000975FF
		[WebCategory("Action")]
		[WebSysDescription("DataList_OnUpdateCommand")]
		public event DataListCommandEventHandler UpdateCommand
		{
			add
			{
				base.Events.AddHandler(DataList.EventUpdateCommand, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataList.EventUpdateCommand, value);
			}
		}

		// Token: 0x06002EF7 RID: 12023 RVA: 0x00099414 File Offset: 0x00097614
		protected override void CreateControlHierarchy(bool useDataSource)
		{
			IEnumerable enumerable = null;
			int num = -1;
			ArrayList dataKeysArray = base.DataKeysArray;
			this.extractTemplateRows = this.ExtractTemplateRows;
			if (this.itemsArray != null)
			{
				this.itemsArray.Clear();
			}
			else
			{
				this.itemsArray = new ArrayList();
			}
			if (!useDataSource)
			{
				num = (int)this.ViewState["_!ItemCount"];
				if (num != -1)
				{
					enumerable = new DummyDataSource(num);
					this.itemsArray.Capacity = num;
				}
			}
			else
			{
				dataKeysArray.Clear();
				enumerable = this.GetData();
				ICollection collection = enumerable as ICollection;
				if (collection != null)
				{
					dataKeysArray.Capacity = collection.Count;
					this.itemsArray.Capacity = collection.Count;
				}
			}
			if (enumerable != null)
			{
				ControlCollection controls = this.Controls;
				int num2 = 0;
				bool flag = this.separatorTemplate != null;
				int editItemIndex = this.EditItemIndex;
				int selectedIndex = this.SelectedIndex;
				string dataKeyField = this.DataKeyField;
				bool flag2 = useDataSource && dataKeyField.Length != 0;
				num = 0;
				if (this.headerTemplate != null)
				{
					this.CreateItem(-1, ListItemType.Header, useDataSource, null);
				}
				foreach (object obj in enumerable)
				{
					if (flag2)
					{
						object propertyValue = DataBinder.GetPropertyValue(obj, dataKeyField);
						dataKeysArray.Add(propertyValue);
					}
					ListItemType itemType = ListItemType.Item;
					if (num2 == editItemIndex)
					{
						itemType = ListItemType.EditItem;
					}
					else if (num2 == selectedIndex)
					{
						itemType = ListItemType.SelectedItem;
					}
					else if (num2 % 2 != 0)
					{
						itemType = ListItemType.AlternatingItem;
					}
					DataListItem value = this.CreateItem(num2, itemType, useDataSource, obj);
					this.itemsArray.Add(value);
					if (flag)
					{
						this.CreateItem(num2, ListItemType.Separator, useDataSource, null);
					}
					num++;
					num2++;
				}
				if (this.footerTemplate != null)
				{
					this.CreateItem(-1, ListItemType.Footer, useDataSource, null);
				}
			}
			if (useDataSource)
			{
				this.ViewState["_!ItemCount"] = ((enumerable != null) ? num : -1);
			}
		}

		// Token: 0x06002EF8 RID: 12024 RVA: 0x00099604 File Offset: 0x00097804
		protected override Style CreateControlStyle()
		{
			return new TableStyle
			{
				CellSpacing = 0
			};
		}

		// Token: 0x06002EF9 RID: 12025 RVA: 0x00099620 File Offset: 0x00097820
		private DataListItem CreateItem(int itemIndex, ListItemType itemType, bool dataBind, object dataItem)
		{
			DataListItem dataListItem = this.CreateItem(itemIndex, itemType);
			DataListItemEventArgs e = new DataListItemEventArgs(dataListItem);
			this.InitializeItem(dataListItem);
			if (dataBind)
			{
				dataListItem.DataItem = dataItem;
			}
			this.OnItemCreated(e);
			this.Controls.Add(dataListItem);
			if (dataBind)
			{
				dataListItem.DataBind();
				this.OnItemDataBound(e);
				dataListItem.DataItem = null;
			}
			return dataListItem;
		}

		// Token: 0x06002EFA RID: 12026 RVA: 0x0009967A File Offset: 0x0009787A
		protected virtual DataListItem CreateItem(int itemIndex, ListItemType itemType)
		{
			return new DataListItem(itemIndex, itemType);
		}

		// Token: 0x06002EFB RID: 12027 RVA: 0x00099684 File Offset: 0x00097884
		private DataListItem GetItem(ListItemType itemType, int repeatIndex)
		{
			DataListItem result = null;
			switch (itemType)
			{
			case ListItemType.Header:
				result = (DataListItem)this.Controls[0];
				break;
			case ListItemType.Footer:
				result = (DataListItem)this.Controls[this.Controls.Count - 1];
				break;
			case ListItemType.Item:
			case ListItemType.AlternatingItem:
			case ListItemType.SelectedItem:
			case ListItemType.EditItem:
				result = (DataListItem)this.itemsArray[repeatIndex];
				break;
			case ListItemType.Separator:
			{
				int num = repeatIndex * 2 + 1;
				if (this.headerTemplate != null)
				{
					num++;
				}
				result = (DataListItem)this.Controls[num];
				break;
			}
			}
			return result;
		}

		// Token: 0x06002EFC RID: 12028 RVA: 0x00099724 File Offset: 0x00097924
		protected virtual void InitializeItem(DataListItem item)
		{
			ITemplate template = this.itemTemplate;
			switch (item.ItemType)
			{
			case ListItemType.Header:
				template = this.headerTemplate;
				goto IL_A4;
			case ListItemType.Footer:
				template = this.footerTemplate;
				goto IL_A4;
			case ListItemType.Item:
				goto IL_A4;
			case ListItemType.AlternatingItem:
				break;
			case ListItemType.SelectedItem:
				goto IL_55;
			case ListItemType.EditItem:
				if (this.editItemTemplate != null)
				{
					template = this.editItemTemplate;
					goto IL_A4;
				}
				if (item.ItemIndex == this.SelectedIndex)
				{
					goto IL_55;
				}
				if (item.ItemIndex % 2 == 0)
				{
					goto IL_A4;
				}
				break;
			case ListItemType.Separator:
				template = this.separatorTemplate;
				goto IL_A4;
			default:
				goto IL_A4;
			}
			IL_44:
			if (this.alternatingItemTemplate != null)
			{
				template = this.alternatingItemTemplate;
				goto IL_A4;
			}
			goto IL_A4;
			IL_55:
			if (this.selectedItemTemplate != null)
			{
				template = this.selectedItemTemplate;
			}
			else if (item.ItemIndex % 2 != 0)
			{
				goto IL_44;
			}
			IL_A4:
			if (template != null)
			{
				template.InstantiateIn(item);
			}
		}

		// Token: 0x06002EFD RID: 12029 RVA: 0x000997E0 File Offset: 0x000979E0
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
					((IStateManager)this.ItemStyle).LoadViewState(array[1]);
				}
				if (array[2] != null)
				{
					((IStateManager)this.SelectedItemStyle).LoadViewState(array[2]);
				}
				if (array[3] != null)
				{
					((IStateManager)this.AlternatingItemStyle).LoadViewState(array[3]);
				}
				if (array[4] != null)
				{
					((IStateManager)this.EditItemStyle).LoadViewState(array[4]);
				}
				if (array[5] != null)
				{
					((IStateManager)this.SeparatorStyle).LoadViewState(array[5]);
				}
				if (array[6] != null)
				{
					((IStateManager)this.HeaderStyle).LoadViewState(array[6]);
				}
				if (array[7] != null)
				{
					((IStateManager)this.FooterStyle).LoadViewState(array[7]);
				}
				if (array[8] != null)
				{
					((IStateManager)base.ControlStyle).LoadViewState(array[8]);
				}
			}
		}

		// Token: 0x06002EFE RID: 12030 RVA: 0x000998A0 File Offset: 0x00097AA0
		protected override bool OnBubbleEvent(object source, EventArgs e)
		{
			bool result = false;
			if (e is DataListCommandEventArgs)
			{
				DataListCommandEventArgs dataListCommandEventArgs = (DataListCommandEventArgs)e;
				this.OnItemCommand(dataListCommandEventArgs);
				result = true;
				string commandName = dataListCommandEventArgs.CommandName;
				if (StringUtil.EqualsIgnoreCase(commandName, "Select"))
				{
					this.SelectedIndex = dataListCommandEventArgs.Item.ItemIndex;
					this.OnSelectedIndexChanged(EventArgs.Empty);
				}
				else if (StringUtil.EqualsIgnoreCase(commandName, "Edit"))
				{
					this.OnEditCommand(dataListCommandEventArgs);
				}
				else if (StringUtil.EqualsIgnoreCase(commandName, "Delete"))
				{
					this.OnDeleteCommand(dataListCommandEventArgs);
				}
				else if (StringUtil.EqualsIgnoreCase(commandName, "Update"))
				{
					this.OnUpdateCommand(dataListCommandEventArgs);
				}
				else if (StringUtil.EqualsIgnoreCase(commandName, "Cancel"))
				{
					this.OnCancelCommand(dataListCommandEventArgs);
				}
			}
			return result;
		}

		// Token: 0x06002EFF RID: 12031 RVA: 0x00099954 File Offset: 0x00097B54
		protected virtual void OnCancelCommand(DataListCommandEventArgs e)
		{
			DataListCommandEventHandler dataListCommandEventHandler = (DataListCommandEventHandler)base.Events[DataList.EventCancelCommand];
			if (dataListCommandEventHandler != null)
			{
				dataListCommandEventHandler(this, e);
			}
		}

		// Token: 0x06002F00 RID: 12032 RVA: 0x00099984 File Offset: 0x00097B84
		protected virtual void OnDeleteCommand(DataListCommandEventArgs e)
		{
			DataListCommandEventHandler dataListCommandEventHandler = (DataListCommandEventHandler)base.Events[DataList.EventDeleteCommand];
			if (dataListCommandEventHandler != null)
			{
				dataListCommandEventHandler(this, e);
			}
		}

		// Token: 0x06002F01 RID: 12033 RVA: 0x000999B4 File Offset: 0x00097BB4
		protected virtual void OnEditCommand(DataListCommandEventArgs e)
		{
			DataListCommandEventHandler dataListCommandEventHandler = (DataListCommandEventHandler)base.Events[DataList.EventEditCommand];
			if (dataListCommandEventHandler != null)
			{
				dataListCommandEventHandler(this, e);
			}
		}

		// Token: 0x06002F02 RID: 12034 RVA: 0x000999E2 File Offset: 0x00097BE2
		protected internal override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			if (this.Page != null && this.DataKeyField.Length > 0)
			{
				this.Page.RegisterRequiresViewStateEncryption();
			}
		}

		// Token: 0x06002F03 RID: 12035 RVA: 0x00099A0C File Offset: 0x00097C0C
		protected virtual void OnItemCommand(DataListCommandEventArgs e)
		{
			DataListCommandEventHandler dataListCommandEventHandler = (DataListCommandEventHandler)base.Events[DataList.EventItemCommand];
			if (dataListCommandEventHandler != null)
			{
				dataListCommandEventHandler(this, e);
			}
		}

		// Token: 0x06002F04 RID: 12036 RVA: 0x00099A3C File Offset: 0x00097C3C
		protected virtual void OnItemCreated(DataListItemEventArgs e)
		{
			DataListItemEventHandler dataListItemEventHandler = (DataListItemEventHandler)base.Events[DataList.EventItemCreated];
			if (dataListItemEventHandler != null)
			{
				dataListItemEventHandler(this, e);
			}
		}

		// Token: 0x06002F05 RID: 12037 RVA: 0x00099A6C File Offset: 0x00097C6C
		protected virtual void OnItemDataBound(DataListItemEventArgs e)
		{
			DataListItemEventHandler dataListItemEventHandler = (DataListItemEventHandler)base.Events[DataList.EventItemDataBound];
			if (dataListItemEventHandler != null)
			{
				dataListItemEventHandler(this, e);
			}
			EventHandler<WizardSideBarListControlItemEventArgs> eventHandler = (EventHandler<WizardSideBarListControlItemEventArgs>)base.Events[DataList.EventWizardListItemDataBound];
			if (eventHandler != null)
			{
				DataListItem item = e.Item;
				WizardSideBarListControlItemEventArgs e2 = new WizardSideBarListControlItemEventArgs(new WizardSideBarListControlItem(item.DataItem, item.ItemType, item.ItemIndex, item));
				eventHandler(this, e2);
			}
		}

		// Token: 0x06002F06 RID: 12038 RVA: 0x00099AE0 File Offset: 0x00097CE0
		protected virtual void OnUpdateCommand(DataListCommandEventArgs e)
		{
			DataListCommandEventHandler dataListCommandEventHandler = (DataListCommandEventHandler)base.Events[DataList.EventUpdateCommand];
			if (dataListCommandEventHandler != null)
			{
				dataListCommandEventHandler(this, e);
			}
		}

		// Token: 0x06002F07 RID: 12039 RVA: 0x00099B10 File Offset: 0x00097D10
		protected internal override void PrepareControlHierarchy()
		{
			ControlCollection controls = this.Controls;
			int count = controls.Count;
			if (count == 0)
			{
				return;
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
			for (int i = 0; i < count; i++)
			{
				DataListItem dataListItem = (DataListItem)controls[i];
				Style style2 = null;
				switch (dataListItem.ItemType)
				{
				case ListItemType.Header:
					if (this.ShowHeader)
					{
						style2 = this.headerStyle;
					}
					break;
				case ListItemType.Footer:
					if (this.ShowFooter)
					{
						style2 = this.footerStyle;
					}
					break;
				case ListItemType.Item:
					style2 = this.itemStyle;
					break;
				case ListItemType.AlternatingItem:
					style2 = style;
					break;
				case ListItemType.SelectedItem:
					style2 = new TableItemStyle();
					if (dataListItem.ItemIndex % 2 != 0)
					{
						style2.CopyFrom(style);
					}
					else
					{
						style2.CopyFrom(this.itemStyle);
					}
					style2.CopyFrom(this.selectedItemStyle);
					break;
				case ListItemType.EditItem:
					style2 = new TableItemStyle();
					if (dataListItem.ItemIndex % 2 != 0)
					{
						style2.CopyFrom(style);
					}
					else
					{
						style2.CopyFrom(this.itemStyle);
					}
					if (dataListItem.ItemIndex == this.SelectedIndex)
					{
						style2.CopyFrom(this.selectedItemStyle);
					}
					style2.CopyFrom(this.editItemStyle);
					break;
				case ListItemType.Separator:
					style2 = this.separatorStyle;
					break;
				}
				if (style2 != null)
				{
					if (!this.extractTemplateRows)
					{
						dataListItem.MergeStyle(style2);
					}
					else
					{
						foreach (object obj in dataListItem.Controls)
						{
							Control control = (Control)obj;
							if (control is Table)
							{
								foreach (object obj2 in ((Table)control).Rows)
								{
									((TableRow)obj2).MergeStyle(style2);
								}
								break;
							}
						}
					}
				}
			}
		}

		// Token: 0x06002F08 RID: 12040 RVA: 0x00099D00 File Offset: 0x00097F00
		protected internal override void RenderContents(HtmlTextWriter writer)
		{
			if (this.Controls.Count == 0)
			{
				return;
			}
			RepeatInfo repeatInfo = new RepeatInfo();
			Table table = null;
			Style controlStyle = base.ControlStyle;
			if (this.extractTemplateRows)
			{
				repeatInfo.RepeatDirection = RepeatDirection.Vertical;
				repeatInfo.RepeatLayout = RepeatLayout.Flow;
				repeatInfo.RepeatColumns = 1;
				repeatInfo.OuterTableImplied = true;
				table = new Table();
				table.ID = this.ClientID;
				table.CopyBaseAttributes(this);
				table.Caption = this.Caption;
				table.CaptionAlign = this.CaptionAlign;
				table.ApplyStyle(controlStyle);
				table.RenderBeginTag(writer);
			}
			else
			{
				repeatInfo.RepeatDirection = this.RepeatDirection;
				repeatInfo.RepeatLayout = this.RepeatLayout;
				repeatInfo.RepeatColumns = this.RepeatColumns;
				if (repeatInfo.RepeatLayout == RepeatLayout.Table)
				{
					repeatInfo.Caption = this.Caption;
					repeatInfo.CaptionAlign = this.CaptionAlign;
					repeatInfo.UseAccessibleHeader = this.UseAccessibleHeader;
				}
				else
				{
					repeatInfo.EnableLegacyRendering = base.EnableLegacyRendering;
				}
			}
			repeatInfo.RenderRepeater(writer, this, controlStyle, this);
			if (table != null)
			{
				table.RenderEndTag(writer);
			}
		}

		// Token: 0x06002F09 RID: 12041 RVA: 0x00099E04 File Offset: 0x00098004
		protected override object SaveViewState()
		{
			object obj = base.SaveViewState();
			object obj2 = (this.itemStyle != null) ? ((IStateManager)this.itemStyle).SaveViewState() : null;
			object obj3 = (this.selectedItemStyle != null) ? ((IStateManager)this.selectedItemStyle).SaveViewState() : null;
			object obj4 = (this.alternatingItemStyle != null) ? ((IStateManager)this.alternatingItemStyle).SaveViewState() : null;
			object obj5 = (this.editItemStyle != null) ? ((IStateManager)this.editItemStyle).SaveViewState() : null;
			object obj6 = (this.separatorStyle != null) ? ((IStateManager)this.separatorStyle).SaveViewState() : null;
			object obj7 = (this.headerStyle != null) ? ((IStateManager)this.headerStyle).SaveViewState() : null;
			object obj8 = (this.footerStyle != null) ? ((IStateManager)this.footerStyle).SaveViewState() : null;
			object obj9 = base.ControlStyleCreated ? ((IStateManager)base.ControlStyle).SaveViewState() : null;
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
				obj9
			};
		}

		// Token: 0x06002F0A RID: 12042 RVA: 0x00099F14 File Offset: 0x00098114
		protected override void TrackViewState()
		{
			base.TrackViewState();
			if (this.itemStyle != null)
			{
				((IStateManager)this.itemStyle).TrackViewState();
			}
			if (this.selectedItemStyle != null)
			{
				((IStateManager)this.selectedItemStyle).TrackViewState();
			}
			if (this.alternatingItemStyle != null)
			{
				((IStateManager)this.alternatingItemStyle).TrackViewState();
			}
			if (this.editItemStyle != null)
			{
				((IStateManager)this.editItemStyle).TrackViewState();
			}
			if (this.separatorStyle != null)
			{
				((IStateManager)this.separatorStyle).TrackViewState();
			}
			if (this.headerStyle != null)
			{
				((IStateManager)this.headerStyle).TrackViewState();
			}
			if (this.footerStyle != null)
			{
				((IStateManager)this.footerStyle).TrackViewState();
			}
			if (base.ControlStyleCreated)
			{
				((IStateManager)base.ControlStyle).TrackViewState();
			}
		}

		// Token: 0x17000D7D RID: 3453
		// (get) Token: 0x06002F0B RID: 12043 RVA: 0x00099FBF File Offset: 0x000981BF
		bool IRepeatInfoUser.HasFooter
		{
			get
			{
				return this.ShowFooter && this.footerTemplate != null;
			}
		}

		// Token: 0x17000D7E RID: 3454
		// (get) Token: 0x06002F0C RID: 12044 RVA: 0x00099FD4 File Offset: 0x000981D4
		bool IRepeatInfoUser.HasHeader
		{
			get
			{
				return this.ShowHeader && this.headerTemplate != null;
			}
		}

		// Token: 0x17000D7F RID: 3455
		// (get) Token: 0x06002F0D RID: 12045 RVA: 0x00099FE9 File Offset: 0x000981E9
		bool IRepeatInfoUser.HasSeparators
		{
			get
			{
				return this.separatorTemplate != null;
			}
		}

		// Token: 0x17000D80 RID: 3456
		// (get) Token: 0x06002F0E RID: 12046 RVA: 0x00099FF4 File Offset: 0x000981F4
		int IRepeatInfoUser.RepeatedItemCount
		{
			get
			{
				if (this.visibleItemCount != -1)
				{
					return this.visibleItemCount;
				}
				if (this.itemsArray == null)
				{
					return 0;
				}
				return this.itemsArray.Count;
			}
		}

		// Token: 0x06002F0F RID: 12047 RVA: 0x0009A01C File Offset: 0x0009821C
		Style IRepeatInfoUser.GetItemStyle(ListItemType itemType, int repeatIndex)
		{
			DataListItem item = this.GetItem(itemType, repeatIndex);
			if (item != null && item.ControlStyleCreated)
			{
				return item.ControlStyle;
			}
			return null;
		}

		// Token: 0x06002F10 RID: 12048 RVA: 0x0009A048 File Offset: 0x00098248
		void IRepeatInfoUser.RenderItem(ListItemType itemType, int repeatIndex, RepeatInfo repeatInfo, HtmlTextWriter writer)
		{
			DataListItem item = this.GetItem(itemType, repeatIndex + this.offset);
			if (item != null)
			{
				item.RenderItem(writer, this.extractTemplateRows, repeatInfo.RepeatLayout == RepeatLayout.Table);
			}
		}

		// Token: 0x17000D81 RID: 3457
		// (get) Token: 0x06002F11 RID: 12049 RVA: 0x0009A07F File Offset: 0x0009827F
		IEnumerable IWizardSideBarListControl.Items
		{
			get
			{
				return this.Items;
			}
		}

		// Token: 0x1400007C RID: 124
		// (add) Token: 0x06002F12 RID: 12050 RVA: 0x0009A087 File Offset: 0x00098287
		// (remove) Token: 0x06002F13 RID: 12051 RVA: 0x0009A09B File Offset: 0x0009829B
		event CommandEventHandler IWizardSideBarListControl.ItemCommand
		{
			add
			{
				this.ItemCommand += new DataListCommandEventHandler(value.Invoke);
			}
			remove
			{
				this.ItemCommand -= new DataListCommandEventHandler(value.Invoke);
			}
		}

		// Token: 0x1400007D RID: 125
		// (add) Token: 0x06002F14 RID: 12052 RVA: 0x0009A0AF File Offset: 0x000982AF
		// (remove) Token: 0x06002F15 RID: 12053 RVA: 0x0009A0C2 File Offset: 0x000982C2
		event EventHandler<WizardSideBarListControlItemEventArgs> IWizardSideBarListControl.ItemDataBound
		{
			add
			{
				base.Events.AddHandler(DataList.EventWizardListItemDataBound, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataList.EventWizardListItemDataBound, value);
			}
		}

		// Token: 0x04002004 RID: 8196
		private static readonly object EventItemCreated = new object();

		// Token: 0x04002005 RID: 8197
		private static readonly object EventItemDataBound = new object();

		// Token: 0x04002006 RID: 8198
		private static readonly object EventItemCommand = new object();

		// Token: 0x04002007 RID: 8199
		private static readonly object EventEditCommand = new object();

		// Token: 0x04002008 RID: 8200
		private static readonly object EventUpdateCommand = new object();

		// Token: 0x04002009 RID: 8201
		private static readonly object EventCancelCommand = new object();

		// Token: 0x0400200A RID: 8202
		private static readonly object EventDeleteCommand = new object();

		// Token: 0x0400200B RID: 8203
		private static readonly object EventWizardListItemDataBound = new object();

		// Token: 0x0400200C RID: 8204
		public const string SelectCommandName = "Select";

		// Token: 0x0400200D RID: 8205
		public const string EditCommandName = "Edit";

		// Token: 0x0400200E RID: 8206
		public const string UpdateCommandName = "Update";

		// Token: 0x0400200F RID: 8207
		public const string CancelCommandName = "Cancel";

		// Token: 0x04002010 RID: 8208
		public const string DeleteCommandName = "Delete";

		// Token: 0x04002011 RID: 8209
		private TableItemStyle itemStyle;

		// Token: 0x04002012 RID: 8210
		private TableItemStyle alternatingItemStyle;

		// Token: 0x04002013 RID: 8211
		private TableItemStyle selectedItemStyle;

		// Token: 0x04002014 RID: 8212
		private TableItemStyle editItemStyle;

		// Token: 0x04002015 RID: 8213
		private TableItemStyle separatorStyle;

		// Token: 0x04002016 RID: 8214
		private TableItemStyle headerStyle;

		// Token: 0x04002017 RID: 8215
		private TableItemStyle footerStyle;

		// Token: 0x04002018 RID: 8216
		private ITemplate itemTemplate;

		// Token: 0x04002019 RID: 8217
		private ITemplate alternatingItemTemplate;

		// Token: 0x0400201A RID: 8218
		private ITemplate selectedItemTemplate;

		// Token: 0x0400201B RID: 8219
		private ITemplate editItemTemplate;

		// Token: 0x0400201C RID: 8220
		private ITemplate separatorTemplate;

		// Token: 0x0400201D RID: 8221
		private ITemplate headerTemplate;

		// Token: 0x0400201E RID: 8222
		private ITemplate footerTemplate;

		// Token: 0x0400201F RID: 8223
		private bool extractTemplateRows;

		// Token: 0x04002020 RID: 8224
		private ArrayList itemsArray;

		// Token: 0x04002021 RID: 8225
		private DataListItemCollection itemsCollection;

		// Token: 0x04002022 RID: 8226
		private int offset;

		// Token: 0x04002023 RID: 8227
		private int visibleItemCount = -1;
	}
}
