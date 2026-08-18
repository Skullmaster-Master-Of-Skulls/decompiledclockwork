using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x0200124C RID: 4684
	public class TreeListDataItem : TreeListEditableItem, IDataItemContainer, INamingContainer
	{
		// Token: 0x0600C109 RID: 49417 RVA: 0x002B0221 File Offset: 0x002AE421
		public TreeListDataItem(RadTreeList ownerTreeList, TreeListItemType itemType, int displayIndex, bool isDataBinding) : base(ownerTreeList, itemType, isDataBinding)
		{
			this.DisplayIndex = displayIndex;
		}

		// Token: 0x0600C10A RID: 49418 RVA: 0x002B0234 File Offset: 0x002AE434
		public TreeListDataItem(RadTreeList ownerTreeList, int displayIndex, bool isDataBinding) : this(ownerTreeList, TreeListItemType.Item, displayIndex, isDataBinding)
		{
		}

		// Token: 0x17003E39 RID: 15929
		// (get) Token: 0x0600C10B RID: 49419 RVA: 0x002B0240 File Offset: 0x002AE440
		public bool CanExpand
		{
			get
			{
				return this.ItemState.HasChildItems;
			}
		}

		// Token: 0x17003E3A RID: 15930
		// (get) Token: 0x0600C10C RID: 49420 RVA: 0x002B024D File Offset: 0x002AE44D
		// (set) Token: 0x0600C10D RID: 49421 RVA: 0x002B0255 File Offset: 0x002AE455
		public bool Expanded
		{
			get
			{
				return this.IsExpanded;
			}
			set
			{
				this.SetExpandedState(value);
			}
		}

		// Token: 0x17003E3B RID: 15931
		// (get) Token: 0x0600C10E RID: 49422 RVA: 0x002B0260 File Offset: 0x002AE460
		// (set) Token: 0x0600C10F RID: 49423 RVA: 0x002B0289 File Offset: 0x002AE489
		internal bool IsCombineExpanded
		{
			get
			{
				object obj = this.ViewState["IsComebineExpanded"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["IsComebineExpanded"] = value;
			}
		}

		// Token: 0x17003E3C RID: 15932
		// (get) Token: 0x0600C110 RID: 49424 RVA: 0x002B02A1 File Offset: 0x002AE4A1
		// (set) Token: 0x0600C111 RID: 49425 RVA: 0x002B02A9 File Offset: 0x002AE4A9
		internal bool TreeListInitializedExpandCollapse { get; set; }

		// Token: 0x17003E3D RID: 15933
		// (get) Token: 0x0600C112 RID: 49426 RVA: 0x002B02B2 File Offset: 0x002AE4B2
		protected bool IsExpanded
		{
			get
			{
				return base.OwnerTreeList.ExpandedIndexes.Contains(this.HierarchyIndex);
			}
		}

		// Token: 0x17003E3E RID: 15934
		// (get) Token: 0x0600C113 RID: 49427 RVA: 0x002B02CA File Offset: 0x002AE4CA
		internal bool ExpandedOnRender
		{
			get
			{
				return base.OwnerTreeList.ExpandHash.Contains(this.HierarchyIndex);
			}
		}

		// Token: 0x0600C114 RID: 49428 RVA: 0x002B02E4 File Offset: 0x002AE4E4
		protected void SetExpandedState(bool shouldExpand)
		{
			if (!this.CanExpand)
			{
				return;
			}
			if (shouldExpand)
			{
				if (!this.IsExpanded)
				{
					if (base.OwnerTreeList.AllowLoadOnDemand)
					{
						base.OwnerTreeList.LoadOnDemandContext.InsertExpandedDataKeyValue(this);
					}
					base.OwnerTreeList.ExpandedIndexes.Add(this.HierarchyIndex);
					base.OwnerTreeList.SetRequiresDataBindingIfInitialized();
					return;
				}
			}
			else if (this.IsExpanded)
			{
				if (base.OwnerTreeList.AllowLoadOnDemand)
				{
					base.OwnerTreeList.LoadOnDemandContext.RemoveExpandedDataKeyValue(this);
				}
				base.OwnerTreeList.ExpandedIndexes.Remove(this.HierarchyIndex);
				base.OwnerTreeList.SetRequiresDataBindingIfInitialized();
			}
		}

		// Token: 0x17003E3F RID: 15935
		// (get) Token: 0x0600C115 RID: 49429 RVA: 0x002B0390 File Offset: 0x002AE590
		// (set) Token: 0x0600C116 RID: 49430 RVA: 0x002B0398 File Offset: 0x002AE598
		public TreeListHierarchyIndex HierarchyIndex { get; internal set; }

		// Token: 0x17003E40 RID: 15936
		// (get) Token: 0x0600C117 RID: 49431 RVA: 0x002B03A1 File Offset: 0x002AE5A1
		// (set) Token: 0x0600C118 RID: 49432 RVA: 0x002B03A9 File Offset: 0x002AE5A9
		internal TreeListSourceItem SourceItem { get; set; }

		// Token: 0x17003E41 RID: 15937
		// (get) Token: 0x0600C119 RID: 49433 RVA: 0x002B03B2 File Offset: 0x002AE5B2
		// (set) Token: 0x0600C11A RID: 49434 RVA: 0x002B03BA File Offset: 0x002AE5BA
		internal TreeListItemState ItemState { get; set; }

		// Token: 0x17003E42 RID: 15938
		// (get) Token: 0x0600C11B RID: 49435 RVA: 0x002B03C3 File Offset: 0x002AE5C3
		public override bool IsInEditMode
		{
			get
			{
				return base.ItemType == TreeListItemType.EditItem;
			}
		}

		// Token: 0x17003E43 RID: 15939
		// (get) Token: 0x0600C11C RID: 49436 RVA: 0x002B03D2 File Offset: 0x002AE5D2
		// (set) Token: 0x0600C11D RID: 49437 RVA: 0x002B03EA File Offset: 0x002AE5EA
		public override bool Edit
		{
			get
			{
				return base.OwnerTreeList.EditIndexes.Contains(this.HierarchyIndex);
			}
			set
			{
				if (value)
				{
					base.OwnerTreeList.SaveEditIndexState(this.HierarchyIndex);
					return;
				}
				base.OwnerTreeList.RemoveEditIndexState(this.HierarchyIndex);
			}
		}

		// Token: 0x17003E44 RID: 15940
		// (get) Token: 0x0600C11E RID: 49438 RVA: 0x002B0412 File Offset: 0x002AE612
		// (set) Token: 0x0600C11F RID: 49439 RVA: 0x002B042A File Offset: 0x002AE62A
		public virtual bool IsChildInserted
		{
			get
			{
				return base.OwnerTreeList.InsertIndexes.Contains(this.HierarchyIndex);
			}
			set
			{
				if (value)
				{
					base.OwnerTreeList.SaveInsertIndexState(this.HierarchyIndex);
					return;
				}
				base.OwnerTreeList.RemoveInsertIndexState(this.HierarchyIndex);
			}
		}

		// Token: 0x0600C120 RID: 49440 RVA: 0x002B0452 File Offset: 0x002AE652
		public void InsertChildItem()
		{
			this.InsertChildItem(null);
		}

		// Token: 0x0600C121 RID: 49441 RVA: 0x002B045B File Offset: 0x002AE65B
		public virtual void InsertChildItem(object newDataItem)
		{
			base.OwnerTreeList.InsertChildItem(this, newDataItem);
		}

		// Token: 0x0600C122 RID: 49442 RVA: 0x002B046C File Offset: 0x002AE66C
		public override void Initialize(IList<TreeListColumn> columns)
		{
			int num = this.HierarchyIndex.NestedLevel + 1;
			if (this.CanExpand)
			{
				num--;
			}
			for (int i = 0; i < num; i++)
			{
				this.Cells.Add(this.CreateCellObject());
			}
			if (this.CanExpand)
			{
				Button button = this.CreateExpandCollapseButton("ExpandCollapseButton");
				if (base.OwnerTreeList.ExpandCollapseMode == TreeListExpandCollapseMode.Combined && base.OwnerTreeList._treeListInitializedExpandCollapseIndexes.Contains(this.HierarchyIndex))
				{
					this.TreeListInitializedExpandCollapse = true;
				}
				if (base.OwnerTreeList.ExpandCollapseMode == TreeListExpandCollapseMode.Client || (base.OwnerTreeList.ExpandCollapseMode == TreeListExpandCollapseMode.Combined && this.ExpandedOnRender))
				{
					if (base.OwnerTreeList.ExpandCollapseMode == TreeListExpandCollapseMode.Combined)
					{
						this.IsCombineExpanded = true;
						if (this.ExpandedOnRender && !this.Page.IsPostBack)
						{
							base.OwnerTreeList.ClientExpandedIndexes.Add(this.HierarchyIndex);
						}
					}
					string onClientClick = string.Format("$find(\"{0}\").toggleExpandCollapse(); return false;", this.ClientID + "__" + this.DisplayIndex);
					button.OnClientClick = onClientClick;
					button.UseSubmitBehavior = false;
				}
				if (this.ExpandedOnRender)
				{
					if (base.OwnerTreeList.ResolvedRenderMode == RenderMode.Lightweight || base.OwnerTreeList.ResolvedRenderMode == RenderMode.Mobile)
					{
						ElasticButton elasticButton = button as ElasticButton;
						ElasticButton elasticButton2 = elasticButton;
						elasticButton2.CssClass += " rtlCollapse";
						elasticButton.FirstSpanClass = "t-font-icon rtlIcon rtlCollapseIcon";
						elasticButton.Text = "Collapse Button";
						elasticButton.UseSubmitBehavior = false;
						if (base.OwnerTreeList.EnableAriaSupport)
						{
							elasticButton.Attributes.Add("aria-label", string.IsNullOrEmpty(base.OwnerTreeList.ClientSettings.ClientMessages.CollapseToolTip) ? "Collapse" : base.OwnerTreeList.ClientSettings.ClientMessages.CollapseToolTip);
						}
					}
					else
					{
						button.CssClass = "rtlCollapse";
					}
					button.ToolTip = base.OwnerTreeList.ClientSettings.ClientMessages.CollapseToolTip;
				}
				else
				{
					if (base.OwnerTreeList.ResolvedRenderMode == RenderMode.Lightweight || base.OwnerTreeList.ResolvedRenderMode == RenderMode.Mobile)
					{
						ElasticButton elasticButton3 = button as ElasticButton;
						ElasticButton elasticButton4 = elasticButton3;
						elasticButton4.CssClass += " rtlExpand";
						elasticButton3.FirstSpanClass = "t-font-icon rtlIcon rtlExpandIcon";
						elasticButton3.Text = "Expand Button";
						elasticButton3.UseSubmitBehavior = false;
						if (base.OwnerTreeList.EnableAriaSupport)
						{
							elasticButton3.Attributes.Add("aria-label", string.IsNullOrEmpty(base.OwnerTreeList.ClientSettings.ClientMessages.ExpandToolTip) ? "Expand" : base.OwnerTreeList.ClientSettings.ClientMessages.ExpandToolTip);
						}
					}
					else
					{
						button.CssClass = "rtlExpand";
					}
					button.ToolTip = base.OwnerTreeList.ClientSettings.ClientMessages.ExpandToolTip;
				}
				TableCell tableCell = this.CreateCellObject();
				tableCell.Controls.Add(button);
				this.Cells.Add(tableCell);
			}
			if (this.IsInEditMode && base.OwnerTreeList.EditMode == TreeListEditMode.InPlace)
			{
				this.InitializeInEditMode(columns);
				return;
			}
			base.Initialize(columns);
		}

		// Token: 0x0600C123 RID: 49443 RVA: 0x002B079C File Offset: 0x002AE99C
		public virtual void InitializeInEditMode(IList<TreeListColumn> columns)
		{
			TableCellCollection cells = this.Cells;
			for (int i = 0; i < columns.Count; i++)
			{
				TableCell cell = this.CreateCellObject();
				cells.Add(cell);
				TreeListColumn treeListColumn = columns[i];
				TreeListEditableColumn treeListEditableColumn = treeListColumn as TreeListEditableColumn;
				if (treeListEditableColumn != null && treeListEditableColumn.IsEditable)
				{
					this.InitializeColumnEditor(cell, i, treeListEditableColumn);
				}
				else
				{
					treeListColumn.InitializeCell(cell, i, this);
				}
			}
			this.CallOnItemCreated();
			if (this.IsDataBinding)
			{
				this.DataBind();
				this.CellsDataBound(columns);
				this.CallOnItemDataBound();
			}
		}

		// Token: 0x0600C124 RID: 49444 RVA: 0x002B0823 File Offset: 0x002AEA23
		public override ITreeListColumnEditor GetColumnEditor(TreeListEditableColumn editableColumn)
		{
			if (this.IsInEditMode && base.OwnerTreeList.EditMode == TreeListEditMode.InPlace)
			{
				return base.GetColumnEditor(editableColumn);
			}
			return null;
		}

		// Token: 0x17003E45 RID: 15941
		// (get) Token: 0x0600C125 RID: 49445 RVA: 0x002B0843 File Offset: 0x002AEA43
		// (set) Token: 0x0600C126 RID: 49446 RVA: 0x002B084B File Offset: 0x002AEA4B
		public TreeListEditFormItem EditFormItem { get; internal set; }

		// Token: 0x17003E46 RID: 15942
		// (get) Token: 0x0600C127 RID: 49447 RVA: 0x002B0854 File Offset: 0x002AEA54
		// (set) Token: 0x0600C128 RID: 49448 RVA: 0x002B085C File Offset: 0x002AEA5C
		public TreeListEditableItem InsertItem { get; internal set; }

		// Token: 0x0600C129 RID: 49449 RVA: 0x002B0868 File Offset: 0x002AEA68
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		protected Button CreateExpandCollapseButton(string id)
		{
			Button button = new Button();
			if (base.OwnerTreeList.ResolvedRenderMode == RenderMode.Lightweight || base.OwnerTreeList.ResolvedRenderMode == RenderMode.Mobile)
			{
				button = new ElasticButton
				{
					CssClass = "t-button rtlActionButton"
				};
			}
			button.ID = id;
			button.Text = " ";
			button.CommandName = "ExpandCollapse";
			button.CausesValidation = false;
			return button;
		}

		// Token: 0x17003E47 RID: 15943
		[SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")]
		[SuppressMessage("Microsoft.Globalization", "CA1304:SpecifyCultureInfo", MessageId = "System.String.ToUpper")]
		public virtual TableCell this[string columnUniqueName]
		{
			get
			{
				TreeListColumn[] renderColumns = base.OwnerTreeList.RenderColumns;
				int num = this.HierarchyIndex.NestedLevel + 1;
				int num2 = 0;
				bool flag = false;
				foreach (TreeListColumn treeListColumn in renderColumns)
				{
					if (treeListColumn.UniqueName.Trim().ToUpper() == columnUniqueName.Trim().ToUpper())
					{
						flag = true;
						break;
					}
					num2++;
				}
				if (flag)
				{
					return this.Cells[num2 + num];
				}
				throw new Exception("Cannot find a cell bound to column name '" + columnUniqueName + "'");
			}
		}

		// Token: 0x17003E48 RID: 15944
		// (get) Token: 0x0600C12B RID: 49451 RVA: 0x002B096A File Offset: 0x002AEB6A
		// (set) Token: 0x0600C12C RID: 49452 RVA: 0x002B0972 File Offset: 0x002AEB72
		public int DataItemIndex { get; internal set; }

		// Token: 0x17003E49 RID: 15945
		// (get) Token: 0x0600C12D RID: 49453 RVA: 0x002B097B File Offset: 0x002AEB7B
		// (set) Token: 0x0600C12E RID: 49454 RVA: 0x002B0983 File Offset: 0x002AEB83
		public int DisplayIndex { get; protected set; }

		// Token: 0x0600C12F RID: 49455 RVA: 0x002B098C File Offset: 0x002AEB8C
		public object GetDataKeyValue(string keyName)
		{
			object obj = base.OwnerTreeList.DataKeyValues[this.DisplayIndex][keyName];
			if (obj == null && base.OwnerTreeList.ClientDataKeyValues.Count > 0)
			{
				return base.OwnerTreeList.ClientDataKeyValues[this.DisplayIndex][keyName];
			}
			return obj;
		}

		// Token: 0x0600C130 RID: 49456 RVA: 0x002B09EA File Offset: 0x002AEBEA
		public object GetParentDataKeyValue(string keyName)
		{
			return base.OwnerTreeList.ParentDataKeyValues[this.DisplayIndex][keyName];
		}

		// Token: 0x17003E4A RID: 15946
		// (get) Token: 0x0600C131 RID: 49457 RVA: 0x002B0A08 File Offset: 0x002AEC08
		// (set) Token: 0x0600C132 RID: 49458 RVA: 0x002B0A7C File Offset: 0x002AEC7C
		public bool Selected
		{
			get
			{
				if (base.OwnerTreeList.AllowLoadOnDemand)
				{
					Hashtable hashtable = new Hashtable();
					foreach (string text in base.OwnerTreeList.DataKeyNames)
					{
						hashtable.Add(text, this.GetDataKeyValue(text));
					}
					return base.OwnerTreeList.LoadOnDemandContext.ItemNeedsToBeSelected(hashtable);
				}
				return base.OwnerTreeList.SelectedIndexes.Contains(this.HierarchyIndex);
			}
			set
			{
				if (value)
				{
					if (!base.OwnerTreeList.AllowMultiItemSelection)
					{
						foreach (TreeListDataItem treeListDataItem in base.OwnerTreeList.Items)
						{
							treeListDataItem.Selected = false;
						}
						base.OwnerTreeList.SelectedIndexes.Clear();
					}
					if (base.OwnerTreeList.AllowLoadOnDemand)
					{
						base.OwnerTreeList.LoadOnDemandContext.InsertSelectedDataKeyValue(this);
					}
					base.OwnerTreeList.SelectedIndexes.Add(this.HierarchyIndex);
				}
				else
				{
					if (base.OwnerTreeList.AllowLoadOnDemand)
					{
						base.OwnerTreeList.LoadOnDemandContext.RemoveSelectedDataKeyValue(this);
					}
					if (base.OwnerTreeList.SelectedIndexes.Contains(this.HierarchyIndex))
					{
						base.OwnerTreeList.SelectedIndexes.Remove(this.HierarchyIndex);
					}
				}
				this.SetSelected(value);
				if (base.OwnerTreeList.AllowRecursiveSelection)
				{
					base.OwnerTreeList.ApplyRecursiveSelection(this.HierarchyIndex, value);
				}
			}
		}

		// Token: 0x0600C133 RID: 49459 RVA: 0x002B0BA0 File Offset: 0x002AEDA0
		protected void SetSelected(bool isSelected)
		{
			if (this.IsInEditMode)
			{
				base.ItemType = TreeListItemType.EditItem;
			}
			else if (isSelected)
			{
				base.ItemType = TreeListItemType.SelectedItem;
			}
			else if (this.IsAlternatingItem())
			{
				base.ItemType = TreeListItemType.AlternatingItem;
			}
			else
			{
				base.ItemType = TreeListItemType.Item;
			}
			this.SetupDecorator();
		}

		// Token: 0x0600C134 RID: 49460 RVA: 0x002B0BEC File Offset: 0x002AEDEC
		internal bool IsAlternatingItem()
		{
			return this.DisplayIndex % 2 != 0;
		}

		// Token: 0x17003E4B RID: 15947
		// (get) Token: 0x0600C135 RID: 49461 RVA: 0x002B0BFC File Offset: 0x002AEDFC
		// (set) Token: 0x0600C136 RID: 49462 RVA: 0x002B0C04 File Offset: 0x002AEE04
		public TreeListDetailTemplateItem DetailItem { get; internal set; }

		// Token: 0x17003E4C RID: 15948
		// (get) Token: 0x0600C137 RID: 49463 RVA: 0x002B0C0D File Offset: 0x002AEE0D
		public TreeListDataItem ParentItem
		{
			get
			{
				if (this._parentItem == null)
				{
					this._parentItem = this.GetParentItemByHierarchyIndex(this.ItemState.ParentHierarchyIndex);
				}
				return this._parentItem;
			}
		}

		// Token: 0x17003E4D RID: 15949
		// (get) Token: 0x0600C138 RID: 49464 RVA: 0x002B0C34 File Offset: 0x002AEE34
		public List<TreeListDataItem> ChildItems
		{
			get
			{
				if (this._childItems == null)
				{
					this._childItems = this.GetChildItems();
				}
				return this._childItems;
			}
		}

		// Token: 0x0600C139 RID: 49465 RVA: 0x002B0C50 File Offset: 0x002AEE50
		private TreeListDataItem GetParentItemByHierarchyIndex(TreeListHierarchyIndex index)
		{
			foreach (TreeListDataItem treeListDataItem in base.OwnerTreeList.Items)
			{
				if (treeListDataItem.HierarchyIndex == index)
				{
					return treeListDataItem;
				}
			}
			return null;
		}

		// Token: 0x0600C13A RID: 49466 RVA: 0x002B0CB8 File Offset: 0x002AEEB8
		private List<TreeListDataItem> GetChildItems()
		{
			List<TreeListDataItem> list = new List<TreeListDataItem>();
			foreach (TreeListDataItem treeListDataItem in base.OwnerTreeList.Items)
			{
				if (treeListDataItem.ItemState.ParentHierarchyIndex == this.HierarchyIndex)
				{
					list.Add(treeListDataItem);
				}
			}
			return list;
		}

		// Token: 0x0600C13B RID: 49467 RVA: 0x002B0D30 File Offset: 0x002AEF30
		internal List<TreeListDataItem> GetChildItemsRecursive()
		{
			List<TreeListDataItem> list = new List<TreeListDataItem>();
			List<TreeListDataItem> childItems = this.GetChildItems();
			foreach (TreeListDataItem treeListDataItem in childItems)
			{
				list.Add(treeListDataItem);
				list.AddRange(treeListDataItem.GetChildItemsRecursive());
			}
			return list;
		}

		// Token: 0x0600C13C RID: 49468 RVA: 0x002B0D98 File Offset: 0x002AEF98
		internal bool HasKeys(IDictionary keys)
		{
			DataKey dataKey = base.OwnerTreeList.DataKeyValues[this.DisplayIndex];
			foreach (object obj in keys)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				if (!dataKey.ContainsKey(dictionaryEntry.Key))
				{
					return false;
				}
				object obj2 = dataKey[dictionaryEntry.Key];
				if ((obj2 == null && dictionaryEntry.Value != null) || !obj2.Equals(dictionaryEntry.Value))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x040032D0 RID: 13008
		internal object InsertDataObject;

		// Token: 0x040032D1 RID: 13009
		private TreeListDataItem _parentItem;

		// Token: 0x040032D2 RID: 13010
		private List<TreeListDataItem> _childItems;
	}
}
