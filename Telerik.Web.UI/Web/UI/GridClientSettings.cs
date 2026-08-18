using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x020010D2 RID: 4306
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class GridClientSettings : ObjectWithState
	{
		// Token: 0x0600B07B RID: 45179 RVA: 0x00262C17 File Offset: 0x00260E17
		public GridClientSettings(StateBag OwnerStateBag, RadGrid owner) : base("cs_", OwnerStateBag)
		{
			this.owner = owner;
		}

		// Token: 0x0600B07C RID: 45180 RVA: 0x00262C2C File Offset: 0x00260E2C
		public bool IsSet()
		{
			return !new GridDefaultValueChecker(this).IsDefault;
		}

		// Token: 0x17003923 RID: 14627
		// (get) Token: 0x0600B07D RID: 45181 RVA: 0x00262C3C File Offset: 0x00260E3C
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Client")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public GridClientDataBinding DataBinding
		{
			get
			{
				if (this._dataBinding == null)
				{
					this._dataBinding = new GridClientDataBinding(base.OwnerViewState);
				}
				return this._dataBinding;
			}
		}

		// Token: 0x17003924 RID: 14628
		// (get) Token: 0x0600B07E RID: 45182 RVA: 0x00262C5D File Offset: 0x00260E5D
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Category("Client")]
		public GridSelecting Selecting
		{
			get
			{
				if (this._selecting == null)
				{
					this._selecting = new GridSelecting(base.OwnerViewState);
				}
				return this._selecting;
			}
		}

		// Token: 0x17003925 RID: 14629
		// (get) Token: 0x0600B07F RID: 45183 RVA: 0x00262C7E File Offset: 0x00260E7E
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Client")]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public GridClientEvents ClientEvents
		{
			get
			{
				if (this._events == null)
				{
					this._events = new GridClientEvents(base.OwnerViewState);
				}
				return this._events;
			}
		}

		// Token: 0x17003926 RID: 14630
		// (get) Token: 0x0600B080 RID: 45184 RVA: 0x00262C9F File Offset: 0x00260E9F
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Category("Client")]
		public GridClientMessages ClientMessages
		{
			get
			{
				if (this._clientMessages == null)
				{
					this._clientMessages = new GridClientMessages(this.owner, base.OwnerViewState);
				}
				return this._clientMessages;
			}
		}

		// Token: 0x17003927 RID: 14631
		// (get) Token: 0x0600B081 RID: 45185 RVA: 0x00262CC6 File Offset: 0x00260EC6
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Client")]
		public GridKeyboardNavigationSettings KeyboardNavigationSettings
		{
			get
			{
				if (this._keyboardNavigationSettings == null)
				{
					this._keyboardNavigationSettings = new GridKeyboardNavigationSettings(base.OwnerViewState);
				}
				return this._keyboardNavigationSettings;
			}
		}

		// Token: 0x17003928 RID: 14632
		// (get) Token: 0x0600B082 RID: 45186 RVA: 0x00262CE8 File Offset: 0x00260EE8
		// (set) Token: 0x0600B083 RID: 45187 RVA: 0x00262D15 File Offset: 0x00260F15
		[DefaultValue("")]
		[Description("Gets or sets the index of the RadGrid active row.")]
		[NotifyParentProperty(true)]
		[Category("Client")]
		public string ActiveRowIndex
		{
			get
			{
				object obj = base.ViewState["ActiveRowIndex"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				base.ViewState["ActiveRowIndex"] = value;
			}
		}

		// Token: 0x17003929 RID: 14633
		// (get) Token: 0x0600B084 RID: 45188 RVA: 0x00262D28 File Offset: 0x00260F28
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Client")]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public GridScrolling Scrolling
		{
			get
			{
				if (this._scrolling == null)
				{
					this._scrolling = new GridScrolling(base.OwnerViewState, this.owner);
				}
				return this._scrolling;
			}
		}

		// Token: 0x1700392A RID: 14634
		// (get) Token: 0x0600B085 RID: 45189 RVA: 0x00262D4F File Offset: 0x00260F4F
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Category("Client")]
		public GridVirtualization Virtualization
		{
			get
			{
				if (this._virtualization == null)
				{
					this._virtualization = new GridVirtualization(base.OwnerViewState, this.owner);
				}
				return this._virtualization;
			}
		}

		// Token: 0x1700392B RID: 14635
		// (get) Token: 0x0600B086 RID: 45190 RVA: 0x00262D76 File Offset: 0x00260F76
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Client")]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public GridResizing Resizing
		{
			get
			{
				if (this._resizing == null)
				{
					this._resizing = new GridResizing(base.OwnerViewState);
				}
				return this._resizing;
			}
		}

		// Token: 0x1700392C RID: 14636
		// (get) Token: 0x0600B087 RID: 45191 RVA: 0x00262D98 File Offset: 0x00260F98
		// (set) Token: 0x0600B088 RID: 45192 RVA: 0x00262DC1 File Offset: 0x00260FC1
		[Description("")]
		[Category("Client")]
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		public virtual bool EnableRowHoverStyle
		{
			get
			{
				object obj = base.ViewState["EnableRowHoverStyle"];
				return obj != null && (bool)obj;
			}
			set
			{
				base.ViewState["EnableRowHoverStyle"] = value;
			}
		}

		// Token: 0x1700392D RID: 14637
		// (get) Token: 0x0600B089 RID: 45193 RVA: 0x00262DDC File Offset: 0x00260FDC
		// (set) Token: 0x0600B08A RID: 45194 RVA: 0x00262E05 File Offset: 0x00261005
		[NotifyParentProperty(true)]
		[Description("")]
		[Category("Client")]
		[DefaultValue(true)]
		public virtual bool EnableAlternatingItems
		{
			get
			{
				object obj = base.ViewState["EnableAlternatingItems"];
				return obj == null || (bool)obj;
			}
			set
			{
				base.ViewState["EnableAlternatingItems"] = value;
			}
		}

		// Token: 0x1700392E RID: 14638
		// (get) Token: 0x0600B08B RID: 45195 RVA: 0x00262E20 File Offset: 0x00261020
		// (set) Token: 0x0600B08C RID: 45196 RVA: 0x00262E49 File Offset: 0x00261049
		[DefaultValue(false)]
		[Description("Gets or sets the value determining if the rows could be dragged and dropped.")]
		[NotifyParentProperty(true)]
		[Category("Client")]
		public virtual bool AllowRowsDragDrop
		{
			get
			{
				object obj = base.ViewState["AllowRowsDragDrop"];
				return obj != null && (bool)obj;
			}
			set
			{
				base.ViewState["AllowRowsDragDrop"] = value;
			}
		}

		// Token: 0x1700392F RID: 14639
		// (get) Token: 0x0600B08D RID: 45197 RVA: 0x00262E64 File Offset: 0x00261064
		// (set) Token: 0x0600B08E RID: 45198 RVA: 0x00262E8D File Offset: 0x0026108D
		[Description("Gets or sets the value dermeming if a row click will trigger a postback")]
		[Category("Client")]
		[NotifyParentProperty(true)]
		[DefaultValue(false)]
		public virtual bool EnablePostBackOnRowClick
		{
			get
			{
				object obj = base.ViewState["EnablePostBackOnRowClick"];
				return obj != null && (bool)obj;
			}
			set
			{
				base.ViewState["EnablePostBackOnRowClick"] = value;
			}
		}

		// Token: 0x17003930 RID: 14640
		// (get) Token: 0x0600B08F RID: 45199 RVA: 0x00262EA8 File Offset: 0x002610A8
		// (set) Token: 0x0600B090 RID: 45200 RVA: 0x00262ED1 File Offset: 0x002610D1
		[DefaultValue(false)]
		[Description("")]
		[NotifyParentProperty(true)]
		[Category("Client")]
		public virtual bool AllowKeyboardNavigation
		{
			get
			{
				object obj = base.ViewState["AllowKeyboardNavigation"];
				return obj != null && (bool)obj;
			}
			set
			{
				base.ViewState["AllowKeyboardNavigation"] = value;
			}
		}

		// Token: 0x17003931 RID: 14641
		// (get) Token: 0x0600B091 RID: 45201 RVA: 0x00262EEC File Offset: 0x002610EC
		// (set) Token: 0x0600B092 RID: 45202 RVA: 0x00262F15 File Offset: 0x00261115
		[Category("Client")]
		[Description("RadGrid_AllowDragToGroup")]
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		public virtual bool AllowDragToGroup
		{
			get
			{
				object obj = base.ViewState["AllowDragToGroup"];
				return obj != null && (bool)obj;
			}
			set
			{
				base.ViewState["AllowDragToGroup"] = value;
			}
		}

		// Token: 0x17003932 RID: 14642
		// (get) Token: 0x0600B093 RID: 45203 RVA: 0x00262F30 File Offset: 0x00261130
		// (set) Token: 0x0600B094 RID: 45204 RVA: 0x00262F59 File Offset: 0x00261159
		[DefaultValue(false)]
		[Category("Client")]
		[Description("RadGrid_AllowColumnsReorder")]
		[NotifyParentProperty(true)]
		public virtual bool AllowColumnsReorder
		{
			get
			{
				object obj = base.ViewState["AllowColumnsReorder"];
				return obj != null && (bool)obj;
			}
			set
			{
				base.ViewState["AllowColumnsReorder"] = value;
			}
		}

		// Token: 0x17003933 RID: 14643
		// (get) Token: 0x0600B095 RID: 45205 RVA: 0x00262F74 File Offset: 0x00261174
		// (set) Token: 0x0600B096 RID: 45206 RVA: 0x00262F9D File Offset: 0x0026119D
		[Category("Client")]
		[Description("RadGrid_AllowAutoScrollOnDragDrop")]
		[DefaultValue(true)]
		[NotifyParentProperty(true)]
		public virtual bool AllowAutoScrollOnDragDrop
		{
			get
			{
				object obj = base.ViewState["AllowAutoScrollOnDragDrop"];
				return obj == null || (bool)obj;
			}
			set
			{
				base.ViewState["AllowAutoScrollOnDragDrop"] = value;
			}
		}

		// Token: 0x17003934 RID: 14644
		// (get) Token: 0x0600B097 RID: 45207 RVA: 0x00262FB8 File Offset: 0x002611B8
		// (set) Token: 0x0600B098 RID: 45208 RVA: 0x00262FE1 File Offset: 0x002611E1
		[NotifyParentProperty(true)]
		[DefaultValue(false)]
		[Description("RadGrid_ReorderColumnsOnClient")]
		[Category("Client")]
		public virtual bool ReorderColumnsOnClient
		{
			get
			{
				object obj = base.ViewState["ReorderColumnsOnClient"];
				return obj != null && (bool)obj;
			}
			set
			{
				base.ViewState["ReorderColumnsOnClient"] = value;
			}
		}

		// Token: 0x17003935 RID: 14645
		// (get) Token: 0x0600B099 RID: 45209 RVA: 0x00262FFC File Offset: 0x002611FC
		// (set) Token: 0x0600B09A RID: 45210 RVA: 0x00263025 File Offset: 0x00261225
		[Category("Client")]
		[Description("Gets or sets the columns reorder method determining behavior when reordering method.")]
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(GridClientSettings.GridColumnsReorderMethod), "Swap")]
		public virtual GridClientSettings.GridColumnsReorderMethod ColumnsReorderMethod
		{
			get
			{
				object obj = base.ViewState["ColumnsReorderMethod"];
				if (obj != null)
				{
					return (GridClientSettings.GridColumnsReorderMethod)obj;
				}
				return GridClientSettings.GridColumnsReorderMethod.Swap;
			}
			set
			{
				base.ViewState["ColumnsReorderMethod"] = value;
			}
		}

		// Token: 0x17003936 RID: 14646
		// (get) Token: 0x0600B09B RID: 45211 RVA: 0x00263040 File Offset: 0x00261240
		// (set) Token: 0x0600B09C RID: 45212 RVA: 0x00263069 File Offset: 0x00261269
		[NotifyParentProperty(true)]
		[DefaultValue(false)]
		[Category("Client")]
		[Description("Gets or sets a value which determines whether the client print grid functionality will be enabled.")]
		public virtual bool EnableClientPrint
		{
			get
			{
				object obj = base.ViewState["EnableClientPrint"];
				return obj != null && (bool)obj;
			}
			set
			{
				base.ViewState["EnableClientPrint"] = value;
			}
		}

		// Token: 0x17003937 RID: 14647
		// (get) Token: 0x0600B09D RID: 45213 RVA: 0x00263084 File Offset: 0x00261284
		// (set) Token: 0x0600B09E RID: 45214 RVA: 0x002630AD File Offset: 0x002612AD
		[NotifyParentProperty(true)]
		[Category("Client")]
		[Description("Gets or sets a value which determines if the RadGrid rows could be hidden.")]
		[DefaultValue(false)]
		public virtual bool AllowRowHide
		{
			get
			{
				object obj = base.ViewState["AllowRowHide"];
				return obj != null && (bool)obj;
			}
			set
			{
				base.ViewState["AllowRowHide"] = value;
			}
		}

		// Token: 0x17003938 RID: 14648
		// (get) Token: 0x0600B09F RID: 45215 RVA: 0x002630C8 File Offset: 0x002612C8
		// (set) Token: 0x0600B0A0 RID: 45216 RVA: 0x002630F1 File Offset: 0x002612F1
		[Description("Gets or sets the property determining if the RadGrid columns could be hidden.")]
		[NotifyParentProperty(true)]
		[Category("Client")]
		[DefaultValue(false)]
		public virtual bool AllowColumnHide
		{
			get
			{
				object obj = base.ViewState["AllowColumnHide"];
				return obj != null && (bool)obj;
			}
			set
			{
				base.ViewState["AllowColumnHide"] = value;
			}
		}

		// Token: 0x17003939 RID: 14649
		// (get) Token: 0x0600B0A1 RID: 45217 RVA: 0x0026310C File Offset: 0x0026130C
		// (set) Token: 0x0600B0A2 RID: 45218 RVA: 0x00263135 File Offset: 0x00261335
		[Category("Client")]
		[Description("RadGrid_AllowExpandCollapse")]
		[NotifyParentProperty(true)]
		[DefaultValue(true)]
		public virtual bool AllowExpandCollapse
		{
			get
			{
				object obj = base.ViewState["AllowExpandCollapse"];
				return obj == null || (bool)obj;
			}
			set
			{
				base.ViewState["AllowExpandCollapse"] = value;
			}
		}

		// Token: 0x1700393A RID: 14650
		// (get) Token: 0x0600B0A3 RID: 45219 RVA: 0x00263150 File Offset: 0x00261350
		// (set) Token: 0x0600B0A4 RID: 45220 RVA: 0x00263179 File Offset: 0x00261379
		[DefaultValue(true)]
		[Description("RadGrid_AllowGroupExpandCollapse")]
		[NotifyParentProperty(true)]
		[Category("Client")]
		public virtual bool AllowGroupExpandCollapse
		{
			get
			{
				object obj = base.ViewState["AllowGroupExpandCollapse"];
				return obj == null || (bool)obj;
			}
			set
			{
				base.ViewState["AllowGroupExpandCollapse"] = value;
			}
		}

		// Token: 0x1700393B RID: 14651
		// (get) Token: 0x0600B0A5 RID: 45221 RVA: 0x00263191 File Offset: 0x00261391
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public GridPostBackReferences PostBackReferences
		{
			get
			{
				if (this._postBackReferences == null)
				{
					this._postBackReferences = new GridPostBackReferences(base.OwnerViewState, this.owner);
				}
				return this._postBackReferences;
			}
		}

		// Token: 0x1700393C RID: 14652
		// (get) Token: 0x0600B0A6 RID: 45222 RVA: 0x002631B8 File Offset: 0x002613B8
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public string PostBackFunction
		{
			get
			{
				return "__doPostBack('{0}','{1}')";
			}
		}

		// Token: 0x1700393D RID: 14653
		// (get) Token: 0x0600B0A7 RID: 45223 RVA: 0x002631BF File Offset: 0x002613BF
		[Category("Client")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public GridAnimationSettings Animation
		{
			get
			{
				if (this._animation == null)
				{
					this._animation = new GridAnimationSettings(base.OwnerViewState);
				}
				return this._animation;
			}
		}

		// Token: 0x1700393E RID: 14654
		// (get) Token: 0x0600B0A8 RID: 45224 RVA: 0x002631E0 File Offset: 0x002613E0
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public int FirstDataRowClientRowIndex
		{
			get
			{
				int result = 0;
				if (this.owner.Items.Count > 0)
				{
					result = this.owner.Items[0].ClientRowIndex;
				}
				return result;
			}
		}

		// Token: 0x1700393F RID: 14655
		// (get) Token: 0x0600B0A9 RID: 45225 RVA: 0x0026321C File Offset: 0x0026141C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public bool ShouldCreateRows
		{
			get
			{
				return this.Resizing.AllowRowResize || this.AllowExpandCollapse || this.AllowGroupExpandCollapse || this.Selecting.AllowRowSelect || this.AllowKeyboardNavigation || this.AllowRowHide || this.Virtualization.EnableVirtualization || !string.IsNullOrEmpty(this.ClientEvents.OnRowCreating) || !string.IsNullOrEmpty(this.ClientEvents.OnRowCreated) || !string.IsNullOrEmpty(this.ClientEvents.OnRowDestroying) || !string.IsNullOrEmpty(this.ClientEvents.OnRowResizing) || !string.IsNullOrEmpty(this.ClientEvents.OnRowResized) || !string.IsNullOrEmpty(this.ClientEvents.OnRowHiding) || !string.IsNullOrEmpty(this.ClientEvents.OnRowHidden) || !string.IsNullOrEmpty(this.ClientEvents.OnRowShowing) || !string.IsNullOrEmpty(this.ClientEvents.OnRowShown) || !string.IsNullOrEmpty(this.ClientEvents.OnRowClick) || !string.IsNullOrEmpty(this.ClientEvents.OnRowDblClick) || !string.IsNullOrEmpty(this.ClientEvents.OnRowMouseOver) || !string.IsNullOrEmpty(this.ClientEvents.OnRowMouseOut) || !string.IsNullOrEmpty(this.ClientEvents.OnRowContextMenu) || this.ShouldCreateDetailTablesOnClient(this.owner.MasterTableView);
			}
		}

		// Token: 0x0600B0AA RID: 45226 RVA: 0x002633AC File Offset: 0x002615AC
		private bool ShouldCreateDetailTablesOnClient(GridTableView currTableView)
		{
			bool flag = false;
			foreach (GridTableView gridTableView in currTableView.DetailTables)
			{
				if ((gridTableView.HierarchyLoadMode == GridChildLoadMode.Client && gridTableView.OwnerGrid.ClientSettings.AllowExpandCollapse) || gridTableView.ShowHeader)
				{
					flag = true;
					break;
				}
				foreach (GridTableView currTableView2 in gridTableView.DetailTables)
				{
					flag = this.ShouldCreateDetailTablesOnClient(currTableView2);
					if (flag)
					{
						break;
					}
				}
			}
			return flag;
		}

		// Token: 0x0600B0AB RID: 45227 RVA: 0x00263474 File Offset: 0x00261674
		internal bool HasClientDeleteColumn(GridTableView view)
		{
			foreach (GridColumn gridColumn in view.RenderColumns)
			{
				if (gridColumn is GridClientDeleteColumn)
				{
					return true;
				}
			}
			if (view.HasDetailTables)
			{
				using (GridTableViewCollection.GridDataTableEnumerator enumerator = view.DetailTables.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						GridTableView view2 = enumerator.Current;
						return this.HasClientDeleteColumn(view2);
					}
				}
			}
			return false;
		}

		// Token: 0x0600B0AC RID: 45228 RVA: 0x00263508 File Offset: 0x00261708
		internal bool IsFilteringEnabled(GridTableView view)
		{
			if (view.AllowFilteringByColumn)
			{
				return true;
			}
			if (view.HasDetailTables)
			{
				using (GridTableViewCollection.GridDataTableEnumerator enumerator = view.DetailTables.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						GridTableView view2 = enumerator.Current;
						return this.IsFilteringEnabled(view2);
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x04002E59 RID: 11865
		private readonly RadGrid owner;

		// Token: 0x04002E5A RID: 11866
		private GridSelecting _selecting;

		// Token: 0x04002E5B RID: 11867
		private GridClientDataBinding _dataBinding;

		// Token: 0x04002E5C RID: 11868
		private GridClientEvents _events;

		// Token: 0x04002E5D RID: 11869
		private GridClientMessages _clientMessages;

		// Token: 0x04002E5E RID: 11870
		private GridKeyboardNavigationSettings _keyboardNavigationSettings;

		// Token: 0x04002E5F RID: 11871
		private GridScrolling _scrolling;

		// Token: 0x04002E60 RID: 11872
		private GridVirtualization _virtualization;

		// Token: 0x04002E61 RID: 11873
		private GridResizing _resizing;

		// Token: 0x04002E62 RID: 11874
		private GridPostBackReferences _postBackReferences;

		// Token: 0x04002E63 RID: 11875
		private GridAnimationSettings _animation;

		// Token: 0x020010D3 RID: 4307
		[SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
		public enum GridColumnsReorderMethod
		{
			// Token: 0x04002E65 RID: 11877
			Swap,
			// Token: 0x04002E66 RID: 11878
			Reorder
		}
	}
}
