using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x020011A7 RID: 4519
	[ToolboxItem(false)]
	public class GridHeaderContextMenu : GridContextMenu
	{
		// Token: 0x0600B9B1 RID: 47537 RVA: 0x00292718 File Offset: 0x00290918
		public GridHeaderContextMenu() : this(null)
		{
		}

		// Token: 0x0600B9B2 RID: 47538 RVA: 0x00292721 File Offset: 0x00290921
		public GridHeaderContextMenu(RadGrid ownerGrid)
		{
			this._ownerGrid = ownerGrid;
			this.EnableImageSprites = true;
		}

		// Token: 0x17003BFB RID: 15355
		// (get) Token: 0x0600B9B3 RID: 47539 RVA: 0x00292737 File Offset: 0x00290937
		// (set) Token: 0x0600B9B4 RID: 47540 RVA: 0x0029273F File Offset: 0x0029093F
		[DefaultValue(true)]
		public override bool EnableImageSprites
		{
			get
			{
				return base.EnableImageSprites;
			}
			set
			{
				base.EnableImageSprites = value;
			}
		}

		// Token: 0x17003BFC RID: 15356
		// (get) Token: 0x0600B9B5 RID: 47541 RVA: 0x00292748 File Offset: 0x00290948
		[Browsable(false)]
		public new AnimationSettings CollapseAnimation
		{
			get
			{
				return base.CollapseAnimation;
			}
		}

		// Token: 0x0600B9B6 RID: 47542 RVA: 0x00292750 File Offset: 0x00290950
		internal void GenerateMenuItems()
		{
			bool flag = base.FindItemByValue("ColumnsContainer") != null;
			bool flag2 = this._ownerGrid.ResolvedRenderMode == RenderMode.Lightweight;
			base.Items.Clear();
			if (this._ownerGrid.AllowSorting)
			{
				RadMenuItem radMenuItem = new RadMenuItem();
				radMenuItem.Attributes["ColumnName"] = string.Empty;
				radMenuItem.Attributes["TableID"] = string.Empty;
				radMenuItem.Value = "SortAsc";
				radMenuItem.CssClass = "rgHCMSortAsc";
				if (flag2)
				{
					radMenuItem.SpriteCssClass = "rmIcon rgHCMSortAscsIcon";
				}
				radMenuItem.Text = this.Localization.HeaderContextMenuSortAsc;
				radMenuItem.PostBack = (string.IsNullOrEmpty(this._ownerGrid.ClientSettings.DataBinding.Location) && string.IsNullOrEmpty(this._ownerGrid.ClientDataSourceID));
				base.Items.Add(radMenuItem);
				radMenuItem = new RadMenuItem();
				radMenuItem.Attributes["ColumnName"] = string.Empty;
				radMenuItem.Attributes["TableID"] = string.Empty;
				radMenuItem.Value = "SortDesc";
				radMenuItem.CssClass = "rgHCMSortDesc";
				if (flag2)
				{
					radMenuItem.SpriteCssClass = "rmIcon rgHCMSortDescIcon";
				}
				radMenuItem.Text = this.Localization.HeaderContextMenuSortDesc;
				radMenuItem.PostBack = (string.IsNullOrEmpty(this._ownerGrid.ClientSettings.DataBinding.Location) && string.IsNullOrEmpty(this._ownerGrid.ClientDataSourceID));
				base.Items.Add(radMenuItem);
				if (this._ownerGrid.MasterTableView.AllowNaturalSort)
				{
					radMenuItem = new RadMenuItem();
					radMenuItem.Attributes["ColumnName"] = string.Empty;
					radMenuItem.Attributes["TableID"] = string.Empty;
					radMenuItem.Value = "SortNone";
					radMenuItem.CssClass = "rgHCMUnsort";
					if (flag2)
					{
						radMenuItem.SpriteCssClass = "rmIcon rgHCMUnsortIcon";
					}
					radMenuItem.Text = this.Localization.HeaderContextMenuSortClear;
					radMenuItem.PostBack = (string.IsNullOrEmpty(this._ownerGrid.ClientSettings.DataBinding.Location) && string.IsNullOrEmpty(this._ownerGrid.ClientDataSourceID));
					base.Items.Add(radMenuItem);
				}
			}
			if (this._ownerGrid.GroupingEnabled)
			{
				RadMenuItem radMenuItem2 = new RadMenuItem();
				radMenuItem2.IsSeparator = true;
				radMenuItem2.Value = "topGroupSeperator";
				base.Items.Add(radMenuItem2);
				RadMenuItem radMenuItem3 = new RadMenuItem();
				radMenuItem3.Attributes["ColumnName"] = string.Empty;
				radMenuItem3.Attributes["TableID"] = string.Empty;
				radMenuItem3.Value = "GroupBy";
				radMenuItem3.CssClass = "rgHCMGroup";
				if (flag2)
				{
					radMenuItem3.SpriteCssClass = "rmIcon rgHCMGroupIcon";
				}
				radMenuItem3.Text = this.Localization.HeaderContextMenuGroupBy;
				radMenuItem3.PostBack = false;
				base.Items.Add(radMenuItem3);
				RadMenuItem radMenuItem4 = new RadMenuItem();
				radMenuItem4.Attributes["ColumnName"] = string.Empty;
				radMenuItem4.Attributes["TableID"] = string.Empty;
				radMenuItem4.Value = "UnGroupBy";
				radMenuItem4.CssClass = "rgHCMUngroup";
				if (flag2)
				{
					radMenuItem4.SpriteCssClass = "rmIcon rgHCMUngroupIcon";
				}
				radMenuItem4.Text = this.Localization.HeaderContextMenuUnGroupBy;
				radMenuItem4.PostBack = false;
				base.Items.Add(radMenuItem4);
				radMenuItem2 = new RadMenuItem();
				radMenuItem2.IsSeparator = true;
				radMenuItem2.Value = "bottomGroupSeperator";
				base.Items.Add(radMenuItem2);
			}
			if (this._ownerGrid.ClientSettings.Resizing.AllowColumnResize && this._ownerGrid.ClientSettings.Resizing.AllowResizeToFit)
			{
				RadMenuItem radMenuItem5 = new RadMenuItem();
				radMenuItem5.Text = this.Localization.HeaderContextMenuBestFitText;
				radMenuItem5.Value = "BestFit";
				radMenuItem5.CssClass = "rgHCMCols";
				if (flag2)
				{
					radMenuItem5.SpriteCssClass = "rmIcon rgHCMColsIcon";
				}
				radMenuItem5.PostBack = false;
				base.Items.Add(radMenuItem5);
				RadMenuItem radMenuItem6 = new RadMenuItem();
				radMenuItem6.IsSeparator = true;
				radMenuItem6.Value = "bestFitSeparator";
				base.Items.Add(radMenuItem6);
			}
			if (this._ownerGrid.ClientSettings.Scrolling.EnableColumnClientFreeze)
			{
				RadMenuItem radMenuItem7 = new RadMenuItem();
				radMenuItem7.Attributes["ColumnName"] = string.Empty;
				radMenuItem7.Text = "Freeze";
				radMenuItem7.Value = "Freeze";
				radMenuItem7.CssClass = "rgFreeze";
				if (flag2)
				{
					radMenuItem7.SpriteCssClass = "rmIcon rgFreezeIcon";
				}
				radMenuItem7.PostBack = false;
				base.Items.Add(radMenuItem7);
				RadMenuItem radMenuItem8 = new RadMenuItem();
				radMenuItem8.IsSeparator = true;
				radMenuItem8.Value = "FreezeSeparator";
				base.Items.Add(radMenuItem8);
			}
			RadMenuItem radMenuItem9 = new RadMenuItem();
			radMenuItem9.Text = this.Localization.HeaderContextMenuColumns;
			radMenuItem9.Value = "ColumnsContainer";
			radMenuItem9.CssClass = "rgHCMCols";
			if (flag2)
			{
				radMenuItem9.SpriteCssClass = "rmIcon rgHCMColsIcon";
			}
			radMenuItem9.PostBack = false;
			base.Items.Add(radMenuItem9);
			if (flag)
			{
				GridHeaderContextMenu.BuildColumnsMenu(radMenuItem9, this._ownerGrid.MasterTableView);
			}
			this._filterMenuEnabled = this.ShouldHeaderContextFilterMenuBeCreated(this._ownerGrid.MasterTableView);
			if (this._filterMenuEnabled && this._ownerGrid.FilterType != GridFilterType.HeaderContext)
			{
				this.BuildFilterMenu(this._ownerGrid.MasterTableView);
			}
			this.BuildAggregatesMenu();
			if (this._ownerGrid.FilterType == GridFilterType.HeaderContext && this._filterMenuEnabled)
			{
				RadMenuItem radMenuItem10 = new RadMenuItem();
				radMenuItem10.Value = "filterMenuSeparator";
				radMenuItem10.IsSeparator = true;
				base.Items.Add(radMenuItem10);
				RadMenuItem radMenuItem11 = new RadMenuItem();
				radMenuItem11.CssClass = "rgFilterListMenu rgHCMFilter";
				radMenuItem11.Value = "FilterList";
				radMenuItem11.Template = new RadGrid.ListBoxMenuTemplate(this._ownerGrid, true);
				base.Items.Add(radMenuItem11);
				this.BuildFilterMenu(this._ownerGrid.MasterTableView);
			}
		}

		// Token: 0x0600B9B7 RID: 47543 RVA: 0x00292D90 File Offset: 0x00290F90
		private void BuildAggregatesMenu()
		{
			if (this.IsAggregatesMenuEnabled(this._ownerGrid.MasterTableView))
			{
				RadMenuItem radMenuItem = new RadMenuItem();
				radMenuItem.IsSeparator = true;
				radMenuItem.Value = "agContainerSeperator";
				base.Items.Add(radMenuItem);
				RadMenuItem radMenuItem2 = new RadMenuItem();
				radMenuItem2.Text = this.Localization.HeaderContextMenuAggregates;
				radMenuItem2.Value = "AggregatesContainer";
				radMenuItem2.PostBack = false;
				base.Items.Add(radMenuItem2);
				foreach (string text in Enum.GetNames(typeof(GridAggregateFunction)))
				{
					RadMenuItem item = new RadMenuItem
					{
						Text = this.GetAggregatesMenuItemText(text),
						Value = text,
						PostBack = false
					};
					radMenuItem2.Items.Add(item);
				}
			}
		}

		// Token: 0x0600B9B8 RID: 47544 RVA: 0x00292E6C File Offset: 0x0029106C
		private bool IsAggregatesMenuEnabled(GridTableView gridTableView)
		{
			if (gridTableView.EnableHeaderContextAggregatesMenu)
			{
				return true;
			}
			if (gridTableView.HasDetailTables)
			{
				foreach (GridTableView gridTableView2 in gridTableView.DetailTables)
				{
					if (this.IsAggregatesMenuEnabled(gridTableView2))
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x0600B9B9 RID: 47545 RVA: 0x00292EDC File Offset: 0x002910DC
		internal string GetAggregatesMenuItemText(string gridAggregateFunctionName)
		{
			return this.Localization.GetStringFromViewState(string.Format("HeaderContextMenu{0}AggregateText", gridAggregateFunctionName));
		}

		// Token: 0x17003BFD RID: 15357
		// (get) Token: 0x0600B9BA RID: 47546 RVA: 0x00292EF4 File Offset: 0x002910F4
		private GridStrings Localization
		{
			get
			{
				return this._ownerGrid.Localization;
			}
		}

		// Token: 0x0600B9BB RID: 47547 RVA: 0x00292F04 File Offset: 0x00291104
		public string GetColumnNameForHeaderContextFilteringMenu()
		{
			string result = string.Empty;
			RadMenuItem radMenuItem = base.FindItemByValue("FilterMenuContainer");
			if (radMenuItem != null)
			{
				result = radMenuItem.Attributes["ColumnName"];
			}
			return result;
		}

		// Token: 0x0600B9BC RID: 47548 RVA: 0x00292F38 File Offset: 0x00291138
		private bool ShouldHeaderContextFilterMenuBeCreated(GridTableView tableView)
		{
			bool result = false;
			if ((tableView.EnableHeaderContextFilterMenu || tableView.OwnerGrid.FilterType == GridFilterType.HeaderContext) && tableView.AllowFilteringByColumn)
			{
				result = true;
			}
			else if (tableView.HasDetailTables)
			{
				foreach (GridTableView tableView2 in tableView.DetailTables)
				{
					if (this.ShouldHeaderContextFilterMenuBeCreated(tableView2))
					{
						result = true;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x0600B9BD RID: 47549 RVA: 0x00292FC0 File Offset: 0x002911C0
		private void BuildFilterMenu(GridTableView tableView)
		{
			RadMenuItem radMenuItem2;
			if (tableView.OwnerGrid.FilterType == GridFilterType.HeaderContext)
			{
				RadMenuItem radMenuItem = base.FindItemByValue("FilterMenuParent");
				if (radMenuItem != null)
				{
					base.Items.Remove(radMenuItem);
					radMenuItem2 = new RadMenuItem();
					radMenuItem2.Text = this.Localization.HeaderContextMenuFilterItemText;
					radMenuItem2.Value = "FilterMenuParent";
					radMenuItem2.CssClass = "rgHCMFilter";
					radMenuItem2.PostBack = false;
					radMenuItem2.Text = "";
					radMenuItem2.CssClass = "rgFilterMenu";
					radMenuItem2.ItemTemplate = new GridHeaderContextMenu.GridContextFilterTemplate(tableView);
					base.Items.Add(radMenuItem2);
					return;
				}
			}
			else
			{
				RadMenuItem radMenuItem3 = new RadMenuItem();
				radMenuItem3.Value = "filterMenuSeparator";
				radMenuItem3.IsSeparator = true;
				base.Items.Add(radMenuItem3);
			}
			radMenuItem2 = new RadMenuItem();
			radMenuItem2.Text = this.Localization.HeaderContextMenuFilterItemText;
			radMenuItem2.Value = "FilterMenuParent";
			radMenuItem2.CssClass = "rgHCMFilter";
			if (this._ownerGrid.ResolvedRenderMode == RenderMode.Lightweight)
			{
				radMenuItem2.SpriteCssClass = "rmIcon rgHCMFilterIcon";
			}
			radMenuItem2.PostBack = false;
			if (tableView.OwnerGrid.FilterType == GridFilterType.HeaderContext)
			{
				radMenuItem2.Text = "";
				radMenuItem2.CssClass = "rgFilterMenu";
				radMenuItem2.ItemTemplate = new GridHeaderContextMenu.GridContextFilterTemplate(tableView);
				base.Items.Add(radMenuItem2);
				return;
			}
			base.Items.Add(radMenuItem2);
			RadMenuItem radMenuItem4 = new RadMenuItem();
			radMenuItem4.CssClass = "rgHCMItem";
			radMenuItem4.Value = "FilterMenuContainer";
			radMenuItem4.PostBack = false;
			radMenuItem4.ItemTemplate = new GridHeaderContextMenu.GridContextFilterTemplate(tableView);
			radMenuItem2.Items.Add(radMenuItem4);
		}

		// Token: 0x0600B9BE RID: 47550 RVA: 0x00293194 File Offset: 0x00291394
		private static void BuildColumnsMenu(IRadMenuItemContainer columnsParentItem, GridTableView tableView)
		{
			if (tableView.EnableHeaderContextMenu)
			{
				GridColumn[] array = (GridColumn[])tableView.RenderColumns.Clone();
				if (tableView.SortHeaderContextMenuColumns)
				{
					Array.Sort<GridColumn>(array, (GridColumn c1, GridColumn c2) => (string.IsNullOrEmpty(c1.HeaderText) ? c1.UniqueName : c1.HeaderText).CompareTo(string.IsNullOrEmpty(c2.HeaderText) ? c2.UniqueName : c2.HeaderText));
				}
				foreach (GridColumn gridColumn in array)
				{
					if (!(gridColumn is GridGroupSplitterColumn) && !(gridColumn is GridExpandColumn) && !(gridColumn is GridRowIndicatorColumn) && !(gridColumn is GridDragDropColumn) && gridColumn.Visible)
					{
						RadMenuItem radMenuItem = new RadMenuItem();
						radMenuItem.Value = string.Format("{0}|{1}", tableView.ClientID, gridColumn.UniqueName);
						radMenuItem.ItemTemplate = new GridHeaderContextMenu.ContextItemTemplate(gridColumn);
						radMenuItem.PostBack = false;
						columnsParentItem.Items.Add(radMenuItem);
					}
				}
			}
			if (tableView.HasDetailTables)
			{
				GridItem[] items = tableView.GetItems(new GridItemType[]
				{
					GridItemType.NestedView
				});
				foreach (GridNestedViewItem gridNestedViewItem in items)
				{
					if ((gridNestedViewItem.ParentItem != null && gridNestedViewItem.ParentItem.Expanded) || tableView.HierarchyLoadMode == GridChildLoadMode.Client)
					{
						foreach (GridTableView tableView2 in gridNestedViewItem.NestedTableViews)
						{
							GridHeaderContextMenu.BuildColumnsMenu(columnsParentItem, tableView2);
						}
					}
				}
			}
		}

		// Token: 0x17003BFE RID: 15358
		// (get) Token: 0x0600B9BF RID: 47551 RVA: 0x002932FC File Offset: 0x002914FC
		public RadNumericTextBox FilterNumericBoxFirstCondition
		{
			get
			{
				if (this._filterMenuEnabled)
				{
					string controlID = GridHeaderContextMenu.GridContextFilterTemplate.GetControlID(GridHeaderContextMenu.GridContextFilterTemplate.FilterControl.RadNumericBox, GridHeaderContextMenu.GridContextFilterTemplate.IdSuffix.FirstCond);
					Control control = base.FindItemByValue("FilterMenuContainer");
					if (control == null)
					{
						control = base.FindItemByValue("FilterMenuParent");
					}
					if (control != null)
					{
						return control.FindControl(controlID) as RadNumericTextBox;
					}
				}
				return null;
			}
		}

		// Token: 0x17003BFF RID: 15359
		// (get) Token: 0x0600B9C0 RID: 47552 RVA: 0x00293345 File Offset: 0x00291545
		// (set) Token: 0x0600B9C1 RID: 47553 RVA: 0x0029334D File Offset: 0x0029154D
		[DefaultValue(true)]
		[Description("A value indicating if an automatic scroll is applied if the groups are larger then the screen height")]
		public new bool EnableAutoScroll
		{
			get
			{
				return base.EnableAutoScroll;
			}
			set
			{
				base.EnableAutoScroll = value;
			}
		}

		// Token: 0x04003107 RID: 12551
		private readonly RadGrid _ownerGrid;

		// Token: 0x04003108 RID: 12552
		private bool _filterMenuEnabled;

		// Token: 0x020011A8 RID: 4520
		internal class ContextItemTemplate : ITemplate
		{
			// Token: 0x0600B9C3 RID: 47555 RVA: 0x00293356 File Offset: 0x00291556
			public ContextItemTemplate(GridColumn column)
			{
				this._column = column;
			}

			// Token: 0x0600B9C4 RID: 47556 RVA: 0x00293368 File Offset: 0x00291568
			public void InstantiateIn(Control container)
			{
				string text = this._column.HeaderText;
				if (string.IsNullOrEmpty(this._column.HeaderText))
				{
					text = this._column.UniqueName;
				}
				CheckBox checkBox = new CheckBox();
				checkBox.ID = string.Format("chk{0}{1}", this._column.Owner.UniqueID, this._column.UniqueName);
				checkBox.Text = text;
				checkBox.Style["white-space"] = "nowrap";
				RadMenuItem radMenuItem = container as RadMenuItem;
				if (radMenuItem != null)
				{
					radMenuItem.Text = text;
				}
				container.Controls.Add(checkBox);
			}

			// Token: 0x0400310A RID: 12554
			private readonly GridColumn _column;
		}

		// Token: 0x020011A9 RID: 4521
		[SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
		public class GridContextFilterTemplate : ITemplate
		{
			// Token: 0x17003C00 RID: 15360
			// (get) Token: 0x0600B9C5 RID: 47557 RVA: 0x00293409 File Offset: 0x00291609
			private GridStrings Localization
			{
				get
				{
					return this.tableView.OwnerGrid.Localization;
				}
			}

			// Token: 0x0600B9C6 RID: 47558 RVA: 0x0029341B File Offset: 0x0029161B
			public GridContextFilterTemplate(GridTableView tv)
			{
				this.tableView = tv;
				this.SetSpecialControlsDictionaryEntries(false);
				this.enableFilterMenu = this.CheckForSpecialColumns(this.tableView);
			}

			// Token: 0x0600B9C7 RID: 47559 RVA: 0x00293450 File Offset: 0x00291650
			public void InstantiateIn(Control container)
			{
				RadMenuItem radMenuItem = container as RadMenuItem;
				this.filterMenuParent = (radMenuItem.Owner as RadMenuItem);
				if (this.filterMenuParent == null)
				{
					this.filterMenuParent = radMenuItem;
					this.contextMenu = (this.filterMenuParent.Parent as RadContextMenu);
				}
				else
				{
					this.contextMenu = this.filterMenuParent.Menu;
				}
				if (this.tableView.OwnerGrid.FilterType != GridFilterType.HeaderContext)
				{
					this.SetUpClearFilterButtonControl(radMenuItem);
				}
				this.SetUpShowRowsLabelControl(radMenuItem);
				this.SetUpComboBoxControls(radMenuItem, GridHeaderContextMenu.GridContextFilterTemplate.IdSuffix.FirstCond);
				this.SetUpFilterValueControls(radMenuItem, GridHeaderContextMenu.GridContextFilterTemplate.IdSuffix.FirstCond);
				this.SetUpAndLabelControl(radMenuItem);
				this.SetUpComboBoxControls(radMenuItem, GridHeaderContextMenu.GridContextFilterTemplate.IdSuffix.SecondCond);
				this.SetUpFilterValueControls(radMenuItem, GridHeaderContextMenu.GridContextFilterTemplate.IdSuffix.SecondCond);
				if (this.tableView.OwnerGrid.FilterType != GridFilterType.HeaderContext)
				{
					this.SetUpFilterButtonControl(radMenuItem);
				}
				else
				{
					this.SetUpFilterButtonControl(radMenuItem);
					this.SetUpClearFilterButtonControl(radMenuItem);
				}
				this.filterMenuParent.Enabled = this.enableFilterMenu;
			}

			// Token: 0x0600B9C8 RID: 47560 RVA: 0x00293530 File Offset: 0x00291730
			public static string GetControlID(GridHeaderContextMenu.GridContextFilterTemplate.FilterControl filterControl, GridHeaderContextMenu.GridContextFilterTemplate.IdSuffix idSuffix)
			{
				string result = "";
				switch (filterControl)
				{
				case GridHeaderContextMenu.GridContextFilterTemplate.FilterControl.CheckBox:
					if (idSuffix == GridHeaderContextMenu.GridContextFilterTemplate.IdSuffix.FirstCond)
					{
						result = "HCFMCBFirstCond";
					}
					else
					{
						result = "HCFMCBSecondCond";
					}
					break;
				case GridHeaderContextMenu.GridContextFilterTemplate.FilterControl.TableCell:
					if (idSuffix == GridHeaderContextMenu.GridContextFilterTemplate.IdSuffix.FirstCond)
					{
						result = "HCFMControlsCellFirstCond";
					}
					else
					{
						result = "HCFMControlsCellSecondCond";
					}
					break;
				case GridHeaderContextMenu.GridContextFilterTemplate.FilterControl.RadTextBox:
					if (idSuffix == GridHeaderContextMenu.GridContextFilterTemplate.IdSuffix.FirstCond)
					{
						result = "HCFMRTBFirstCond";
					}
					else
					{
						result = "HCFMRTBSecondCond";
					}
					break;
				case GridHeaderContextMenu.GridContextFilterTemplate.FilterControl.RadComboBox:
					if (idSuffix == GridHeaderContextMenu.GridContextFilterTemplate.IdSuffix.FirstCond)
					{
						result = "HCFMRCMBFirstCond";
					}
					else
					{
						result = "HCFMRCMBSecondCond";
					}
					break;
				case GridHeaderContextMenu.GridContextFilterTemplate.FilterControl.RadDateInput:
					if (idSuffix == GridHeaderContextMenu.GridContextFilterTemplate.IdSuffix.FirstCond)
					{
						result = "HCFMRDIFirstCond";
					}
					else
					{
						result = "HCFMRDISecondCond";
					}
					break;
				case GridHeaderContextMenu.GridContextFilterTemplate.FilterControl.RadDatePicker:
					if (idSuffix == GridHeaderContextMenu.GridContextFilterTemplate.IdSuffix.FirstCond)
					{
						result = "HCFMRDPFirstCond";
					}
					else
					{
						result = "HCFMRDPSecondCond";
					}
					break;
				case GridHeaderContextMenu.GridContextFilterTemplate.FilterControl.RadDateTimePicker:
					if (idSuffix == GridHeaderContextMenu.GridContextFilterTemplate.IdSuffix.FirstCond)
					{
						result = "HCFMRDTPFirstCond";
					}
					else
					{
						result = "HCFMRDTPSecondCond";
					}
					break;
				case GridHeaderContextMenu.GridContextFilterTemplate.FilterControl.RadTimePicker:
					if (idSuffix == GridHeaderContextMenu.GridContextFilterTemplate.IdSuffix.FirstCond)
					{
						result = "HCFMRTPFirstCond";
					}
					else
					{
						result = "HCFMRTPSecondCond";
					}
					break;
				case GridHeaderContextMenu.GridContextFilterTemplate.FilterControl.RadNumericBox:
					if (idSuffix == GridHeaderContextMenu.GridContextFilterTemplate.IdSuffix.FirstCond)
					{
						result = "HCFMRNTBFirstCond";
					}
					else
					{
						result = "HCFMRNTBSecondCond";
					}
					break;
				case GridHeaderContextMenu.GridContextFilterTemplate.FilterControl.RadMaskedBox:
					if (idSuffix == GridHeaderContextMenu.GridContextFilterTemplate.IdSuffix.FirstCond)
					{
						result = "HCFMRMTBFirstCond";
					}
					else
					{
						result = "HCFMRMTBSecondCond";
					}
					break;
				case GridHeaderContextMenu.GridContextFilterTemplate.FilterControl.FilterButton:
					result = "HCFMFilterButton";
					break;
				case GridHeaderContextMenu.GridContextFilterTemplate.FilterControl.ClearFilterButton:
					result = "HCFMClearFilterButton";
					break;
				}
				return result;
			}

			// Token: 0x0600B9C9 RID: 47561 RVA: 0x00293668 File Offset: 0x00291868
			private void SetSpecialControlsDictionaryEntries(bool value)
			{
				this.specialControls.Add("BoundColumn", value);
				this.specialControls.Add("CheckBoxColumn", value);
				this.specialControls.Add("MaskedColumn", value);
				this.specialControls.Add("NumericColumn", value);
				this.specialControls.Add("DatePicker", value);
				this.specialControls.Add("TimePicker", value);
				this.specialControls.Add("DateTimePicker", value);
				this.specialControls.Add("None", value);
			}

			// Token: 0x0600B9CA RID: 47562 RVA: 0x00293700 File Offset: 0x00291900
			[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
			private bool CheckForSpecialColumns(GridTableView currentTableView)
			{
				bool flag = false;
				foreach (GridColumn gridColumn in currentTableView.RenderColumns)
				{
					if (!flag)
					{
						flag = gridColumn.SupportsFiltering();
					}
					if (!(gridColumn is GridGroupSplitterColumn) && !(gridColumn is GridExpandColumn) && !(gridColumn is GridRowIndicatorColumn) && gridColumn.Visible)
					{
						if (gridColumn is GridDateTimeColumn)
						{
							switch (((GridDateTimeColumn)gridColumn).PickerType)
							{
							case GridDateTimeColumnPickerType.None:
								this.specialControls["None"] = true;
								break;
							case GridDateTimeColumnPickerType.DatePicker:
								this.specialControls["DatePicker"] = true;
								break;
							case GridDateTimeColumnPickerType.TimePicker:
								this.specialControls["TimePicker"] = true;
								break;
							case GridDateTimeColumnPickerType.DateTimePicker:
								this.specialControls["DateTimePicker"] = true;
								break;
							}
						}
						else if (gridColumn is GridMaskedColumn)
						{
							this.specialControls["MaskedColumn"] = true;
						}
						else if (gridColumn is GridNumericColumn || gridColumn is GridRatingColumn)
						{
							this.specialControls["NumericColumn"] = true;
						}
						else if (gridColumn is GridCheckBoxColumn || gridColumn.DataType == typeof(bool))
						{
							this.specialControls["CheckBoxColumn"] = true;
						}
						else
						{
							this.specialControls["BoundColumn"] = true;
						}
					}
				}
				if (currentTableView.HasDetailTables)
				{
					foreach (GridTableView currentTableView2 in currentTableView.DetailTables)
					{
						flag |= this.CheckForSpecialColumns(currentTableView2);
					}
				}
				return flag;
			}

			// Token: 0x0600B9CB RID: 47563 RVA: 0x002938D0 File Offset: 0x00291AD0
			private void SetUpClearFilterButtonControl(RadMenuItem filterMenuContainer)
			{
				Button button;
				if (this.tableView.OwnerGrid.ResolvedRenderMode == RenderMode.Classic)
				{
					button = new Button();
					button.CssClass = string.Empty;
				}
				else
				{
					button = new ElasticButton(string.Empty, "t-text rgButtonText");
					button.CssClass = "t-button ";
				}
				button.ID = GridHeaderContextMenu.GridContextFilterTemplate.GetControlID(GridHeaderContextMenu.GridContextFilterTemplate.FilterControl.ClearFilterButton, GridHeaderContextMenu.GridContextFilterTemplate.IdSuffix.FirstCond);
				button.UseSubmitBehavior = false;
				button.CausesValidation = false;
				Button button2 = button;
				button2.CssClass += "rgHCMClear";
				button.Text = this.Localization.HeaderContextMenuClearButton;
				if (this.tableView.OwnerGrid.FilterType == GridFilterType.HeaderContext)
				{
					button.OnClientClick = "return false;";
				}
				filterMenuContainer.Controls.Add(button);
			}

			// Token: 0x0600B9CC RID: 47564 RVA: 0x0029398C File Offset: 0x00291B8C
			private void SetUpShowRowsLabelControl(RadMenuItem filterMenuContainer)
			{
				LiteralControl literalControl = new LiteralControl();
				literalControl.Text = string.Format("<label class=\"rgHCMShow\">{0}</label>", this.Localization.HeaderContextMenuRowsLabel);
				filterMenuContainer.Controls.Add(literalControl);
			}

			// Token: 0x0600B9CD RID: 47565 RVA: 0x002939C8 File Offset: 0x00291BC8
			private void ConfigureSkinnableControl(ISkinnableControl control)
			{
				control.EnableEmbeddedScripts = this.tableView.OwnerGrid.EnableEmbeddedScripts;
				control.EnableEmbeddedSkins = this.tableView.OwnerGrid.EnableEmbeddedSkins;
				control.EnableEmbeddedBaseStylesheet = this.tableView.OwnerGrid.EnableEmbeddedBaseStylesheet;
			}

			// Token: 0x0600B9CE RID: 47566 RVA: 0x00293A18 File Offset: 0x00291C18
			private void SetUpComboBoxControls(RadMenuItem filterMenuContainer, GridHeaderContextMenu.GridContextFilterTemplate.IdSuffix idSuffix)
			{
				RadComboBox radComboBox = new RadComboBox();
				radComboBox.RenderMode = this.tableView.OwnerGrid.RenderMode;
				radComboBox.ID = GridHeaderContextMenu.GridContextFilterTemplate.GetControlID(GridHeaderContextMenu.GridContextFilterTemplate.FilterControl.RadComboBox, idSuffix);
				this.ConfigureSkinnableControl(radComboBox);
				radComboBox.PreRender += this.SkinableControl_PreRender;
				foreach (string text in Enum.GetNames(typeof(GridKnownFunction)))
				{
					if (text != "NotBetween" && text != "Between")
					{
						RadComboBoxItem item = new RadComboBoxItem(this.tableView.OwnerGrid.GetFilterMenuItemText(text), text);
						radComboBox.Items.Add(item);
					}
				}
				radComboBox.ZIndex = 10000;
				radComboBox.OnClientDropDownClosing = "function(sender,args){  var e = args.get_domEvent().rawEvent? args.get_domEvent().rawEvent : args.get_domEvent();  e.cancelBubble = true;  e.returnValue = false;  if(e.preventDefault)  {    e.preventDefault();    e.stopPropagation();  }}";
				radComboBox.PreRender += this.combo_PreRender;
				filterMenuContainer.Controls.Add(radComboBox);
			}

			// Token: 0x0600B9CF RID: 47567 RVA: 0x00293B00 File Offset: 0x00291D00
			private void combo_PreRender(object sender, EventArgs e)
			{
				RadComboBox radComboBox = sender as RadComboBox;
				radComboBox.OnClientDropDownOpening = string.Format("function(sender,args){{$find(\"{0}\").findItemByValue(\"FilterMenuParent\")._popUpOpened = true;}}", this.contextMenu.ClientID);
				radComboBox.OnClientDropDownClosed = string.Format("function(sender,args){{$find(\"{0}\").findItemByValue(\"FilterMenuParent\")._popUpOpened = false;}}", this.contextMenu.ClientID);
			}

			// Token: 0x0600B9D0 RID: 47568 RVA: 0x00293B4A File Offset: 0x00291D4A
			private void SetUpFilterValueControls(RadMenuItem filterMenuContainer, GridHeaderContextMenu.GridContextFilterTemplate.IdSuffix idSuffix)
			{
				this.SetUpBoundColumnFilterControls(filterMenuContainer, idSuffix);
				this.SetUpDateTimeColumnFilterControls(filterMenuContainer, idSuffix);
				this.SetUpNumericColumnFilterControls(filterMenuContainer, idSuffix);
				this.SetUpMakedColumnFilterControls(filterMenuContainer, idSuffix);
				this.SetUpCheckBoxColumnFilterControls(filterMenuContainer, idSuffix);
			}

			// Token: 0x0600B9D1 RID: 47569 RVA: 0x00293B74 File Offset: 0x00291D74
			private void SetUpAndLabelControl(RadMenuItem filterMenuContainer)
			{
				LiteralControl literalControl = new LiteralControl();
				literalControl.Text = string.Format("<label class=\"rgHCMAnd\">{0}</label>", this.Localization.HeaderContextMenuAndLabel);
				filterMenuContainer.Controls.Add(literalControl);
			}

			// Token: 0x0600B9D2 RID: 47570 RVA: 0x00293BB0 File Offset: 0x00291DB0
			private void SetUpFilterButtonControl(RadMenuItem filterMenuContainer)
			{
				Button button;
				if (this.tableView.OwnerGrid.ResolvedRenderMode == RenderMode.Classic)
				{
					button = new Button();
					button.CssClass = string.Empty;
				}
				else
				{
					button = new ElasticButton(string.Empty, "t-text rgButtonText");
					button.CssClass = "t-button ";
				}
				button.ID = GridHeaderContextMenu.GridContextFilterTemplate.GetControlID(GridHeaderContextMenu.GridContextFilterTemplate.FilterControl.FilterButton, GridHeaderContextMenu.GridContextFilterTemplate.IdSuffix.FirstCond);
				button.CausesValidation = false;
				Button button2 = button;
				button2.CssClass += "rgHCMFilter";
				button.Text = this.Localization.HeaderContextMenuFilterButton;
				if (this.tableView.OwnerGrid.FilterType == GridFilterType.HeaderContext)
				{
					button.OnClientClick = "return false;";
				}
				filterMenuContainer.Controls.Add(button);
			}

			// Token: 0x0600B9D3 RID: 47571 RVA: 0x00293C64 File Offset: 0x00291E64
			private void SetUpBoundColumnFilterControls(RadMenuItem filterMenuContainer, GridHeaderContextMenu.GridContextFilterTemplate.IdSuffix idSuffix)
			{
				if (this.specialControls["BoundColumn"])
				{
					RadTextBox radTextBox = new RadTextBox();
					radTextBox.RenderMode = this.tableView.OwnerGrid.RenderMode;
					this.ConfigureSkinnableControl(radTextBox);
					this.ConfigureSkinnableControl(radTextBox);
					radTextBox.PreRender += this.SkinableControl_PreRender;
					radTextBox.ID = GridHeaderContextMenu.GridContextFilterTemplate.GetControlID(GridHeaderContextMenu.GridContextFilterTemplate.FilterControl.RadTextBox, idSuffix);
					filterMenuContainer.Controls.Add(radTextBox);
				}
			}

			// Token: 0x0600B9D4 RID: 47572 RVA: 0x00293CD8 File Offset: 0x00291ED8
			private void SetUpDateTimeColumnFilterControls(RadMenuItem filterMenuContainer, GridHeaderContextMenu.GridContextFilterTemplate.IdSuffix idSuffix)
			{
				if (this.specialControls["None"])
				{
					RadDateInput radDateInput = new RadDateInput();
					radDateInput.ID = GridHeaderContextMenu.GridContextFilterTemplate.GetControlID(GridHeaderContextMenu.GridContextFilterTemplate.FilterControl.RadDateInput, idSuffix);
					radDateInput.RenderMode = this.tableView.OwnerGrid.RenderMode;
					radDateInput.Width = Unit.Pixel(160);
					filterMenuContainer.Controls.Add(radDateInput);
					this.ConfigureSkinnableControl(radDateInput);
					radDateInput.PreRender += this.SkinableControl_PreRender;
				}
				if (this.specialControls["DatePicker"])
				{
					RadDatePicker radDatePicker = GridDateTimeColumnHelper.InstantiatePickerFactory(GridDateTimeColumnPickerType.DatePicker);
					radDatePicker.ID = GridHeaderContextMenu.GridContextFilterTemplate.GetControlID(GridHeaderContextMenu.GridContextFilterTemplate.FilterControl.RadDatePicker, idSuffix);
					radDatePicker.RenderMode = this.tableView.OwnerGrid.RenderMode;
					radDatePicker.Width = Unit.Pixel(160);
					radDatePicker.ZIndex = 20000;
					filterMenuContainer.Controls.Add(radDatePicker);
					this.ConfigureSkinnableControl(radDatePicker);
					radDatePicker.PreRender += this.SkinablePicker_PreRender;
				}
				if (this.specialControls["DateTimePicker"])
				{
					RadDatePicker radDatePicker = GridDateTimeColumnHelper.InstantiatePickerFactory(GridDateTimeColumnPickerType.DateTimePicker);
					radDatePicker.ID = GridHeaderContextMenu.GridContextFilterTemplate.GetControlID(GridHeaderContextMenu.GridContextFilterTemplate.FilterControl.RadDateTimePicker, idSuffix);
					radDatePicker.RenderMode = this.tableView.OwnerGrid.RenderMode;
					radDatePicker.Width = Unit.Pixel(160);
					radDatePicker.ZIndex = 20000;
					filterMenuContainer.Controls.Add(radDatePicker);
					this.ConfigureSkinnableControl(radDatePicker);
					radDatePicker.PreRender += this.SkinablePicker_PreRender;
				}
				if (this.specialControls["TimePicker"])
				{
					RadDatePicker radDatePicker = GridDateTimeColumnHelper.InstantiatePickerFactory(GridDateTimeColumnPickerType.TimePicker);
					radDatePicker.ID = GridHeaderContextMenu.GridContextFilterTemplate.GetControlID(GridHeaderContextMenu.GridContextFilterTemplate.FilterControl.RadTimePicker, idSuffix);
					radDatePicker.RenderMode = this.tableView.OwnerGrid.RenderMode;
					radDatePicker.Width = Unit.Pixel(160);
					radDatePicker.ZIndex = 20000;
					filterMenuContainer.Controls.Add(radDatePicker);
					this.ConfigureSkinnableControl(radDatePicker);
					radDatePicker.PreRender += this.SkinablePicker_PreRender;
				}
			}

			// Token: 0x0600B9D5 RID: 47573 RVA: 0x00293EC9 File Offset: 0x002920C9
			private void SkinableControl_PreRender(object sender, EventArgs e)
			{
				((ISkinnableControl)sender).Skin = this.tableView.OwnerGrid.RuntimeSkin;
			}

			// Token: 0x0600B9D6 RID: 47574 RVA: 0x00293EE8 File Offset: 0x002920E8
			private void SkinablePicker_PreRender(object sender, EventArgs e)
			{
				RadDatePicker radDatePicker = sender as RadDatePicker;
				radDatePicker.Skin = this.tableView.OwnerGrid.RuntimeSkin;
				radDatePicker.ClientEvents.OnPopupOpening = string.Format("function(sender,args){{$find(\"{0}\").findItemByValue(\"FilterMenuParent\")._popUpOpened = true;}}", this.contextMenu.ClientID);
				radDatePicker.ClientEvents.OnPopupClosing = string.Format("function(sender,args){{$find(\"{0}\").findItemByValue(\"FilterMenuParent\")._popUpOpened = false;}}", this.contextMenu.ClientID);
			}

			// Token: 0x0600B9D7 RID: 47575 RVA: 0x00293F54 File Offset: 0x00292154
			private void SetUpMakedColumnFilterControls(RadMenuItem filterMenuContainer, GridHeaderContextMenu.GridContextFilterTemplate.IdSuffix idSuffix)
			{
				if (this.specialControls["MaskedColumn"])
				{
					RadMaskedTextBox radMaskedTextBox = new RadMaskedTextBox();
					radMaskedTextBox.ID = GridHeaderContextMenu.GridContextFilterTemplate.GetControlID(GridHeaderContextMenu.GridContextFilterTemplate.FilterControl.RadMaskedBox, idSuffix);
					radMaskedTextBox.RenderMode = this.tableView.OwnerGrid.RenderMode;
					radMaskedTextBox.Width = Unit.Pixel(160);
					filterMenuContainer.Controls.Add(radMaskedTextBox);
					radMaskedTextBox.AllowEmptyEnumerations = true;
					this.ConfigureSkinnableControl(radMaskedTextBox);
					radMaskedTextBox.PreRender += this.SkinableControl_PreRender;
				}
			}

			// Token: 0x0600B9D8 RID: 47576 RVA: 0x00293FDC File Offset: 0x002921DC
			private void SetUpNumericColumnFilterControls(RadMenuItem filterMenuContainer, GridHeaderContextMenu.GridContextFilterTemplate.IdSuffix idSuffix)
			{
				if (this.specialControls["NumericColumn"])
				{
					RadNumericTextBox radNumericTextBox = new RadNumericTextBox();
					radNumericTextBox.ID = GridHeaderContextMenu.GridContextFilterTemplate.GetControlID(GridHeaderContextMenu.GridContextFilterTemplate.FilterControl.RadNumericBox, idSuffix);
					radNumericTextBox.Width = Unit.Pixel(160);
					radNumericTextBox.RenderMode = this.tableView.OwnerGrid.RenderMode;
					filterMenuContainer.Controls.Add(radNumericTextBox);
					this.ConfigureSkinnableControl(radNumericTextBox);
					radNumericTextBox.PreRender += this.SkinableControl_PreRender;
				}
			}

			// Token: 0x0600B9D9 RID: 47577 RVA: 0x0029405C File Offset: 0x0029225C
			private void SetUpCheckBoxColumnFilterControls(RadMenuItem filterMenuContainer, GridHeaderContextMenu.GridContextFilterTemplate.IdSuffix idSuffix)
			{
				if (this.specialControls["CheckBoxColumn"])
				{
					CheckBox checkBox = new CheckBox();
					checkBox.ID = GridHeaderContextMenu.GridContextFilterTemplate.GetControlID(GridHeaderContextMenu.GridContextFilterTemplate.FilterControl.CheckBox, idSuffix);
					checkBox.InputAttributes.Add("class", "skipCheckBox");
					filterMenuContainer.Controls.Add(checkBox);
				}
			}

			// Token: 0x0400310B RID: 12555
			private readonly GridTableView tableView;

			// Token: 0x0400310C RID: 12556
			private RadMenuItem filterMenuParent;

			// Token: 0x0400310D RID: 12557
			private RadMenu contextMenu;

			// Token: 0x0400310E RID: 12558
			private Dictionary<string, bool> specialControls = new Dictionary<string, bool>();

			// Token: 0x0400310F RID: 12559
			private bool enableFilterMenu;

			// Token: 0x020011AA RID: 4522
			[SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
			public enum FilterControl
			{
				// Token: 0x04003111 RID: 12561
				CheckBox,
				// Token: 0x04003112 RID: 12562
				TableCell,
				// Token: 0x04003113 RID: 12563
				RadTextBox,
				// Token: 0x04003114 RID: 12564
				RadComboBox,
				// Token: 0x04003115 RID: 12565
				RadDateInput,
				// Token: 0x04003116 RID: 12566
				RadDatePicker,
				// Token: 0x04003117 RID: 12567
				RadDateTimePicker,
				// Token: 0x04003118 RID: 12568
				RadTimePicker,
				// Token: 0x04003119 RID: 12569
				RadNumericBox,
				// Token: 0x0400311A RID: 12570
				RadMaskedBox,
				// Token: 0x0400311B RID: 12571
				FilterButton,
				// Token: 0x0400311C RID: 12572
				ClearFilterButton
			}

			// Token: 0x020011AB RID: 4523
			[SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
			public enum IdSuffix
			{
				// Token: 0x0400311E RID: 12574
				FirstCond,
				// Token: 0x0400311F RID: 12575
				SecondCond
			}
		}
	}
}
