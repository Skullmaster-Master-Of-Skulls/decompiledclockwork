using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI.Functions;
using Telerik.Web.UI.Grid;

namespace Telerik.Web.UI
{
	// Token: 0x020011A2 RID: 4514
	[ToolboxItem(false)]
	public class GridGroupPanel : WebControl, INamingContainer
	{
		// Token: 0x0600B974 RID: 47476 RVA: 0x00290C2B File Offset: 0x0028EE2B
		public GridGroupPanel()
		{
			this.Visible = false;
		}

		// Token: 0x17003BE5 RID: 15333
		// (get) Token: 0x0600B975 RID: 47477 RVA: 0x00290C3A File Offset: 0x0028EE3A
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x0600B976 RID: 47478 RVA: 0x00290C3E File Offset: 0x0028EE3E
		private string GetLocalizationString(TFunc<GridStrings, string> extractor, string defaultValue)
		{
			if (this.ownerGrid != null)
			{
				return extractor(this.ownerGrid.Localization);
			}
			return defaultValue;
		}

		// Token: 0x0600B977 RID: 47479 RVA: 0x00290C5B File Offset: 0x0028EE5B
		protected override void AddParsedSubObject(object obj)
		{
		}

		// Token: 0x0600B978 RID: 47480 RVA: 0x00290C5D File Offset: 0x0028EE5D
		protected override void CreateChildControls()
		{
			this.Controls.Clear();
			this.CreateMainTable();
		}

		// Token: 0x0600B979 RID: 47481 RVA: 0x00290C70 File Offset: 0x0028EE70
		private void CreateMainTable()
		{
			if (this.ownerGrid == null || this.ownerGrid.ResolvedRenderMode != RenderMode.Lightweight)
			{
				Table table = new Table();
				table.EnableViewState = false;
				if (this.ownerGrid != null)
				{
					AccessibilityHelper.AddSummary(table, this.ownerGrid.GroupingSettings.MainTableSummary);
					AccessibilityHelper.AddCaption(table, this.ownerGrid.GroupingSettings.MainTableCaption);
					AccessibilityHelper.AddAccessibilityRow(table, this.ownerGrid.MasterTableView.Caption);
				}
				table.ID = "TB";
				this.Controls.Add(table);
				this.SetCellSpacing(table, 0);
				table.Style.Add("border-collapse", "separate");
				GridTableRow gridTableRow = new GridTableRow();
				table.Rows.Add(gridTableRow);
				TableCell tableCell = new GridTableCell();
				gridTableRow.Cells.Add(tableCell);
				Table table2 = new Table();
				if (this.ownerGrid != null)
				{
					AccessibilityHelper.AddSummary(table2, this.ownerGrid.GroupingSettings.NestedTableSummary);
					AccessibilityHelper.AddCaption(table2, this.ownerGrid.GroupingSettings.NestedTableCaption);
					AccessibilityHelper.AddAccessibilityRow(table2, this.ownerGrid.MasterTableView.Caption);
				}
				tableCell.Controls.Add(table2);
			}
		}

		// Token: 0x0600B97A RID: 47482 RVA: 0x00290DA4 File Offset: 0x0028EFA4
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		[SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow")]
		private bool AddLightweightGroups(GridTableView tableView, int level, ref int tableIndex)
		{
			Panel panel = new Panel();
			if (tableView.GroupByExpressions.Count > 0)
			{
				this.Controls.Add(panel);
			}
			foreach (GridGroupByExpression gridGroupByExpression in tableView.GroupByExpressions)
			{
				GroupItemCell groupItemCell = new GroupItemCell();
				string text = tableIndex + ":" + gridGroupByExpression.Index.ToString();
				List<Control> list = new List<Control>();
				foreach (object obj in gridGroupByExpression.GroupByFields)
				{
					GridGroupByField gridGroupByField = (GridGroupByField)obj;
					GridGroupByField gridGroupByField2 = gridGroupByField;
					foreach (object obj2 in gridGroupByExpression.SelectFields)
					{
						GridGroupByField gridGroupByField3 = (GridGroupByField)obj2;
						if (gridGroupByField3.FieldName == gridGroupByField.FieldName)
						{
							gridGroupByField2 = gridGroupByField3;
							break;
						}
					}
					ElasticButton elasticButton = new ElasticButton
					{
						FirstSpanClass = "t-font-icon rgIcon ",
						CssClass = "t-button rgActionButton "
					};
					Literal literal = new Literal();
					literal.Text = gridGroupByField2.GetHeaderText() + "&nbsp;";
					switch (gridGroupByField.SortOrder)
					{
					case GridSortOrder.None:
						elasticButton.Visible = false;
						break;
					case GridSortOrder.Ascending:
					{
						elasticButton.ToolTip = this.ownerGrid.SortingSettings.SortedAscToolTip;
						ElasticButton elasticButton2 = elasticButton;
						elasticButton2.CssClass += "rgSortAsc";
						ElasticButton elasticButton3 = elasticButton;
						elasticButton3.FirstSpanClass += "rgSortAscIcon";
						elasticButton.Text = "Sort Ascending";
						break;
					}
					case GridSortOrder.Descending:
					{
						elasticButton.ToolTip = this.ownerGrid.SortingSettings.SortedDescToolTip;
						ElasticButton elasticButton4 = elasticButton;
						elasticButton4.CssClass += "rgSortDesc";
						ElasticButton elasticButton5 = elasticButton;
						elasticButton5.FirstSpanClass += "rgSortDescIcon";
						elasticButton.Text = "Sort Descending";
						break;
					}
					}
					if (this.ownerGrid.EnableAriaSupport)
					{
						elasticButton.Attributes.Add("aria-label", elasticButton.ToolTip);
					}
					elasticButton.CommandName = "ChangeSort";
					elasticButton.CommandArgument = text + ":" + gridGroupByField.FieldName;
					groupItemCell.Controls.Add(literal);
					groupItemCell.Controls.Add(elasticButton);
					groupItemCell.Controls.Add(new LiteralControl("&nbsp;"));
					list.AddRange(new Control[]
					{
						literal,
						elasticButton,
						new LiteralControl("&nbsp;")
					});
					if (this.ownerGrid.GroupingSettings.ShowUnGroupButton)
					{
						WebControl webControl = this.CreateUngroupButton(text);
						groupItemCell.Controls.Add(webControl);
						list.Add(webControl);
					}
					this._groupPanelItems.Add(groupItemCell);
					if (gridGroupByExpression.GroupByFields.Count == 1)
					{
						groupItemCell.DataField = gridGroupByField.FieldName;
					}
				}
				groupItemCell.HierarchicalIndex = text;
				groupItemCell.ToolTip = this.ownerGrid.GroupingSettings.UnGroupTooltip;
				groupItemCell.MergeStyle(this.panelItemsStyle);
				groupItemCell.Wrap = false;
				HtmlGenericControl child = this.CreateLightweightGroupPanelCell(groupItemCell, list);
				panel.Controls.Add(child);
			}
			bool flag = tableView.GroupByExpressions.Count > 0;
			foreach (GridTableView tableView2 in tableView.DetailTables)
			{
				tableIndex++;
				flag |= this.AddLightweightGroups(tableView2, level + 1, ref tableIndex);
			}
			return flag;
		}

		// Token: 0x0600B97B RID: 47483 RVA: 0x002911F8 File Offset: 0x0028F3F8
		private HtmlGenericControl CreateLightweightGroupPanelCell(GroupItemCell groupItemCell, List<Control> innerControls)
		{
			HtmlGenericControl htmlGenericControl = new HtmlGenericControl("span");
			foreach (Control child in innerControls)
			{
				htmlGenericControl.Controls.Add(child);
			}
			htmlGenericControl.Attributes.Add("title", groupItemCell.ToolTip);
			htmlGenericControl.Attributes.Add("class", this.PanelItemsStyle.CssClass);
			return htmlGenericControl;
		}

		// Token: 0x0600B97C RID: 47484 RVA: 0x00291288 File Offset: 0x0028F488
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		private bool AddGroupByFromTableView(GridTableView tableView, int level, ref int tableIndex)
		{
			Table table = new Table();
			AccessibilityHelper.AddSummary(table, this.ownerGrid.GroupingSettings.GroupItemsWrapperTableSummary);
			AccessibilityHelper.AddCaption(table, this.ownerGrid.GroupingSettings.GroupItemsWrapperTableCaption);
			table.BorderStyle = BorderStyle.None;
			table.Width = Unit.Percentage(100.0);
			table.CellPadding = this.panelItemsStyle.CellPadding;
			this.SetCellSpacing(table, this.panelItemsStyle.CellSpacing);
			AccessibilityHelper.AddAccessibilityRow(table, tableView.Caption);
			TableRow tableRow = new GridTableRow();
			for (int i = 0; i < level; i++)
			{
				TableCell tableCell = new GridTableCell();
				tableCell.Width = Unit.Pixel(13);
				tableRow.Cells.Add(tableCell);
			}
			bool flag = true;
			foreach (GridGroupByExpression gridGroupByExpression in tableView.GroupByExpressions)
			{
				if (!flag)
				{
					TableCell tableCell2 = new GridTableCell();
					tableCell2.Text = "-";
					tableRow.Cells.Add(tableCell2);
				}
				flag = false;
				GroupItemCell groupItemCell = new GroupItemCell();
				string text = tableIndex + ":" + gridGroupByExpression.Index.ToString();
				foreach (object obj in gridGroupByExpression.GroupByFields)
				{
					GridGroupByField gridGroupByField = (GridGroupByField)obj;
					if (gridGroupByExpression.GroupByFields.Count == 1)
					{
						groupItemCell.DataField = gridGroupByField.FieldName;
					}
					GridGroupByField gridGroupByField2 = gridGroupByField;
					foreach (object obj2 in gridGroupByExpression.SelectFields)
					{
						GridGroupByField gridGroupByField3 = (GridGroupByField)obj2;
						if (gridGroupByField3.FieldName == gridGroupByField.FieldName)
						{
							gridGroupByField2 = gridGroupByField3;
							break;
						}
					}
					Literal literal = new Literal();
					literal.Text = gridGroupByField2.GetHeaderText() + "&nbsp;";
					WebControl webControl;
					if (this.ownerGrid.EmptySkin() || (!this.ownerGrid.EnableEmbeddedSkins && !string.IsNullOrEmpty(this.ownerGrid.ImagesPath.TrimStart(new char[0]).TrimEnd(new char[0]))))
					{
						webControl = new GridGroupPanelImageButton();
					}
					else
					{
						webControl = new Button();
						if (!string.IsNullOrEmpty(this.ownerGrid.ClientDataSourceID))
						{
							((Button)webControl).OnClientClick = "return false;";
						}
						((Button)webControl).Text = " ";
					}
					switch (gridGroupByField.SortOrder)
					{
					case GridSortOrder.None:
						webControl.Visible = false;
						break;
					case GridSortOrder.Ascending:
						webControl.ToolTip = this.ownerGrid.SortingSettings.SortedAscToolTip;
						if (this.ownerGrid.EmptySkin() || (!this.ownerGrid.EnableEmbeddedSkins && !string.IsNullOrEmpty(this.ownerGrid.ImagesPath.TrimStart(new char[0]).TrimEnd(new char[0]))))
						{
							((ImageButton)webControl).ImageUrl = this.ownerGrid.ResolveGridImageUrl("SortAsc.gif");
						}
						else
						{
							((Button)webControl).CssClass = "rgSortAsc";
						}
						break;
					case GridSortOrder.Descending:
						webControl.ToolTip = this.ownerGrid.SortingSettings.SortedDescToolTip;
						if (this.ownerGrid.EmptySkin() || (!this.ownerGrid.EnableEmbeddedSkins && !string.IsNullOrEmpty(this.ownerGrid.ImagesPath.TrimStart(new char[0]).TrimEnd(new char[0]))))
						{
							((ImageButton)webControl).ImageUrl = this.ownerGrid.ResolveGridImageUrl("SortDesc.gif");
						}
						else
						{
							((Button)webControl).CssClass = "rgSortDesc";
						}
						break;
					}
					((IButtonControl)webControl).CommandName = "ChangeSort";
					((IButtonControl)webControl).CommandArgument = text + ":" + gridGroupByField.FieldName;
					groupItemCell.Controls.Add(literal);
					groupItemCell.Controls.Add(webControl);
					groupItemCell.Controls.Add(new LiteralControl("&nbsp;"));
					if (this.ownerGrid.GroupingSettings.ShowUnGroupButton)
					{
						WebControl child = this.CreateUngroupButton(text);
						groupItemCell.Controls.Add(child);
					}
					this._groupPanelItems.Add(groupItemCell);
				}
				groupItemCell.HierarchicalIndex = text;
				groupItemCell.ToolTip = this.ownerGrid.GroupingSettings.UnGroupTooltip;
				groupItemCell.MergeStyle(this.panelItemsStyle);
				groupItemCell.Wrap = false;
				tableRow.Cells.Add(groupItemCell);
			}
			bool flag2 = tableView.GroupByExpressions.Count > 0;
			if (flag2)
			{
				TableCell tableCell3 = new GridTableCell();
				tableCell3.Width = Unit.Percentage(100.0);
				tableRow.Cells.Add(tableCell3);
				table.Rows.Add(tableRow);
				TableRow tableRow2 = new GridTableRow();
				TableCell tableCell4 = new GridTableCell();
				tableCell4.Controls.Add(table);
				tableRow2.Cells.Add(tableCell4);
				this.MainTable.Rows.Add(tableRow2);
			}
			foreach (GridTableView tableView2 in tableView.DetailTables)
			{
				tableIndex++;
				flag2 |= this.AddGroupByFromTableView(tableView2, level + 1, ref tableIndex);
			}
			return flag2;
		}

		// Token: 0x0600B97D RID: 47485 RVA: 0x00291898 File Offset: 0x0028FA98
		private WebControl CreateUngroupButton(string hierarchicalIndex)
		{
			string onClientClick = string.IsNullOrEmpty(this.ownerGrid.ClientDataSourceID) ? string.Format("$find('{0}').get_masterTableView()._ungroupByExpression('{1}'); return false;", this.ownerGrid.ClientID, hierarchicalIndex) : "return false;";
			WebControl webControl;
			if (this.ownerGrid.ResolvedRenderMode == RenderMode.Lightweight)
			{
				ElasticButton elasticButton = new ElasticButton
				{
					FirstSpanClass = "t-font-icon rgIcon rgUngroupIcon"
				};
				webControl = elasticButton;
				elasticButton.CausesValidation = false;
				elasticButton.Text = "Ungroup";
				if (this.ownerGrid.EnableAriaSupport)
				{
					elasticButton.Attributes.Add("aria-label", elasticButton.Text);
				}
				webControl.CssClass = "t-button rgActionButton rgUngroup";
				elasticButton.OnClientClick = onClientClick;
			}
			else if (this.ownerGrid.EmptySkin() || (!this.ownerGrid.EnableEmbeddedSkins && !string.IsNullOrEmpty(this.ownerGrid.ImagesPath.TrimStart(new char[0]).TrimEnd(new char[0]))))
			{
				ImageButton imageButton = new GridGroupPanelImageButton();
				webControl = imageButton;
				imageButton.ImageUrl = this.ownerGrid.ResolveGridImageUrl("Ungroup.gif");
				imageButton.OnClientClick = onClientClick;
			}
			else
			{
				Button button = new Button();
				webControl = button;
				button.CausesValidation = false;
				button.Text = " ";
				webControl.CssClass = "rgUngroup";
				button.OnClientClick = onClientClick;
			}
			webControl.ToolTip = this.ownerGrid.GroupingSettings.UnGroupButtonTooltip;
			return webControl;
		}

		// Token: 0x17003BE6 RID: 15334
		// (get) Token: 0x0600B97E RID: 47486 RVA: 0x002919F9 File Offset: 0x0028FBF9
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public List<GroupItemCell> GroupPanelItems
		{
			get
			{
				return this._groupPanelItems;
			}
		}

		// Token: 0x17003BE7 RID: 15335
		// (get) Token: 0x0600B97F RID: 47487 RVA: 0x00291A0C File Offset: 0x0028FC0C
		// (set) Token: 0x0600B980 RID: 47488 RVA: 0x00291A5C File Offset: 0x0028FC5C
		[Description("RadGrid_GroupPanel_Text")]
		[DefaultValue("Drag a column header and drop it here to group by that column")]
		[Category("Grouping")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		public virtual string Text
		{
			get
			{
				object obj = this.ViewState["Text"];
				if (obj != null)
				{
					return (string)obj;
				}
				return this.GetLocalizationString((GridStrings loc) => loc.GroupPanelText, "Drag a column header and drop it here to group by that column");
			}
			set
			{
				this.ViewState["Text"] = value;
			}
		}

		// Token: 0x0600B981 RID: 47489 RVA: 0x00291A70 File Offset: 0x0028FC70
		protected override void Render(HtmlTextWriter writer)
		{
			if (this.ownerGrid != null && !this.ownerGrid.EmptySkin() && this.PanelStyle.CssClass.IndexOf("rgGroupPanel") == -1)
			{
				this.PanelStyle.CssClass = this.ownerGrid.FormatCssClass("rgGroupPanel", this.PanelStyle.CssClass);
			}
			if (this.ownerGrid != null && !this.ownerGrid.EmptySkin() && this.PanelItemsStyle.CssClass.IndexOf("rgGroupItem") == -1)
			{
				this.PanelItemsStyle.CssClass = this.ownerGrid.FormatCssClass("rgGroupItem", this.PanelItemsStyle.CssClass);
			}
			if (this.ownerGrid.ResolvedRenderMode == RenderMode.Lightweight)
			{
				this.CssClass = this.PanelStyle.CssClass;
				this.ApplyGroupItemCellCssClass(this);
			}
			else
			{
				this.WrappingTable.CssClass = this.PanelStyle.CssClass;
				this.ApplyGroupItemCellCssClass(this);
			}
			base.Render(writer);
		}

		// Token: 0x0600B982 RID: 47490 RVA: 0x00291B70 File Offset: 0x0028FD70
		internal void ApplyGroupItemCellCssClass(Control control)
		{
			foreach (object obj in control.Controls)
			{
				Control control2 = (Control)obj;
				if (control2.HasControls())
				{
					this.ApplyGroupItemCellCssClass(control2);
				}
				if (this.ownerGrid.ResolvedRenderMode == RenderMode.Lightweight)
				{
					HtmlGenericControl htmlGenericControl = control2 as HtmlGenericControl;
					if (htmlGenericControl != null && htmlGenericControl.TagName == "span")
					{
						htmlGenericControl.Attributes["class"] = this.PanelItemsStyle.CssClass;
					}
				}
				else
				{
					GroupItemCell groupItemCell = control2 as GroupItemCell;
					if (groupItemCell != null)
					{
						groupItemCell.CssClass = this.PanelItemsStyle.CssClass;
					}
				}
			}
		}

		// Token: 0x17003BE8 RID: 15336
		// (get) Token: 0x0600B983 RID: 47491 RVA: 0x00291C3C File Offset: 0x0028FE3C
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Description("RadGrid_GroupPanelStyle")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Category("Grouping")]
		[NotifyParentProperty(true)]
		public GridGroupPanelStyle PanelStyle
		{
			get
			{
				if (this._panelStyle == null)
				{
					this._panelStyle = new GridGroupPanelStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._panelStyle).TrackViewState();
					}
				}
				return this._panelStyle;
			}
		}

		// Token: 0x17003BE9 RID: 15337
		// (get) Token: 0x0600B984 RID: 47492 RVA: 0x00291C6A File Offset: 0x0028FE6A
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Category("Grouping")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Description("RadGrid_GroupPanelItemsStyle")]
		public GridPanelItemsStyle PanelItemsStyle
		{
			get
			{
				if (this._panelItemsStyle == null)
				{
					this._panelItemsStyle = new GridPanelItemsStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._panelItemsStyle).TrackViewState();
					}
				}
				return this._panelItemsStyle;
			}
		}

		// Token: 0x0600B985 RID: 47493 RVA: 0x00291C98 File Offset: 0x0028FE98
		protected override void LoadViewState(object savedState)
		{
			object[] array = (object[])savedState;
			base.LoadViewState(array[0]);
			if (array[1] != null)
			{
				((IStateManager)this.PanelStyle).LoadViewState(array[1]);
			}
			if (array[2] != null)
			{
				((IStateManager)this.PanelItemsStyle).LoadViewState(array[2]);
			}
		}

		// Token: 0x0600B986 RID: 47494 RVA: 0x00291CDC File Offset: 0x0028FEDC
		protected override object SaveViewState()
		{
			object[] array = new object[3];
			array[0] = base.SaveViewState();
			array[1] = null;
			if (this._panelStyle != null)
			{
				array[1] = ((IStateManager)this._panelStyle).SaveViewState();
			}
			array[2] = null;
			if (this._panelItemsStyle != null)
			{
				array[2] = ((IStateManager)this._panelItemsStyle).SaveViewState();
			}
			return array;
		}

		// Token: 0x0600B987 RID: 47495 RVA: 0x00291D30 File Offset: 0x0028FF30
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (this.ownerGrid.ResolvedRenderMode != RenderMode.Lightweight && !this.hasItems && this.ownerGrid != null && this.MainTable != null && this.ownerGrid.ClientSettings.AllowDragToGroup)
			{
				if (this.MainTable.Rows.Count > 0 && this.MainTable.Rows[1].Cells.Count > 0)
				{
					this.MainTable.Rows[1].Cells[0].Text = HttpUtility.HtmlEncode(this.Text);
					return;
				}
				this.MainTable.Rows[1].Cells[0].Text = "";
			}
		}

		// Token: 0x0600B988 RID: 47496 RVA: 0x00291E0F File Offset: 0x0029000F
		internal void CallTrackViewState()
		{
			base.TrackViewState();
		}

		// Token: 0x0600B989 RID: 47497 RVA: 0x00291E17 File Offset: 0x00290017
		protected override void TrackViewState()
		{
			base.TrackViewState();
			if (this._panelStyle != null)
			{
				((IStateManager)this._panelStyle).TrackViewState();
			}
			if (this._panelItemsStyle != null)
			{
				((IStateManager)this._panelItemsStyle).TrackViewState();
			}
		}

		// Token: 0x17003BEA RID: 15338
		// (get) Token: 0x0600B98A RID: 47498 RVA: 0x00291E48 File Offset: 0x00290048
		private GridGroupPanelStyle panelStyle
		{
			get
			{
				if (this.cachedPanelStyle != null)
				{
					return this.cachedPanelStyle;
				}
				GridGroupPanelStyle gridGroupPanelStyle = new GridGroupPanelStyle();
				if (this.PanelStyle.IsDefault)
				{
					if (this.ownerGrid.ResolvedRenderMode != RenderMode.Lightweight)
					{
						gridGroupPanelStyle.Width = Unit.Percentage(100.0);
					}
				}
				else
				{
					gridGroupPanelStyle.CopyFrom(this.PanelStyle);
				}
				this.cachedPanelStyle = gridGroupPanelStyle;
				return gridGroupPanelStyle;
			}
		}

		// Token: 0x17003BEB RID: 15339
		// (get) Token: 0x0600B98B RID: 47499 RVA: 0x00291EB0 File Offset: 0x002900B0
		private GridPanelItemsStyle panelItemsStyle
		{
			get
			{
				if (this.cachedPanelItemsStyle != null)
				{
					return this.cachedPanelItemsStyle;
				}
				GridPanelItemsStyle gridPanelItemsStyle = new GridPanelItemsStyle();
				if (this.PanelItemsStyle.IsDefault)
				{
					gridPanelItemsStyle.CellSpacing = 0;
				}
				else
				{
					gridPanelItemsStyle.CopyFrom(this.PanelItemsStyle);
				}
				this.cachedPanelItemsStyle = gridPanelItemsStyle;
				return gridPanelItemsStyle;
			}
		}

		// Token: 0x0600B98C RID: 47500 RVA: 0x00291EFC File Offset: 0x002900FC
		public void InitializeIn(RadGrid grid, bool FromViewState)
		{
			this._groupPanelItems = new List<GroupItemCell>();
			this.cachedPanelStyle = null;
			this.ownerGrid = grid;
			this.Controls.Clear();
			this.CreateMainTable();
			int num = 0;
			if (this.ownerGrid.ResolvedRenderMode == RenderMode.Lightweight)
			{
				this.EnsureChildControls();
				this.hasItems = this.AddLightweightGroups(this.ownerGrid.MasterTableView, 0, ref num);
				if (!this.hasItems)
				{
					Literal literal = new Literal();
					literal.Text = (this.ownerGrid.ClientSettings.AllowDragToGroup ? HttpUtility.HtmlEncode(this.Text) : string.Empty);
					this.Controls.Add(literal);
					return;
				}
			}
			else
			{
				this.WrappingTable.ApplyStyle(this.panelStyle);
				this.SetCellSpacing(this.MainTable, 0);
				this.MainTable.Width = Unit.Percentage(100.0);
				this.MainTable.Font.CopyFrom(this.panelStyle.Font);
				this.hasItems = this.AddGroupByFromTableView(this.ownerGrid.MasterTableView, 0, ref num);
				if (!this.hasItems)
				{
					TableRow tableRow = new GridTableRow();
					TableCell tableCell = new GridTableCell();
					if (this.ownerGrid.ClientSettings.AllowDragToGroup)
					{
						tableCell.Text = HttpUtility.HtmlEncode(this.Text);
					}
					else
					{
						tableCell.Text = "";
					}
					tableRow.Cells.Add(tableCell);
					this.MainTable.Rows.Add(tableRow);
				}
			}
		}

		// Token: 0x0600B98D RID: 47501 RVA: 0x0029207A File Offset: 0x0029027A
		private void SetCellSpacing(Table table, int cellSpacing)
		{
			if (GridTableViewHelper.IsBrowser("IE") && !GridTableViewHelper.IsBrowserVersionNewer("IE", 7))
			{
				table.CellSpacing = cellSpacing;
				return;
			}
			table.Style["border-spacing"] = cellSpacing.ToString();
		}

		// Token: 0x17003BEC RID: 15340
		// (get) Token: 0x0600B98E RID: 47502 RVA: 0x002920B4 File Offset: 0x002902B4
		internal Table WrappingTable
		{
			get
			{
				this.EnsureChildControls();
				return (Table)this.Controls[0];
			}
		}

		// Token: 0x17003BED RID: 15341
		// (get) Token: 0x0600B98F RID: 47503 RVA: 0x002920CD File Offset: 0x002902CD
		internal Panel LightGroupItemsPlaceHolder
		{
			get
			{
				this.EnsureChildControls();
				return (Panel)this.Controls[0];
			}
		}

		// Token: 0x17003BEE RID: 15342
		// (get) Token: 0x0600B990 RID: 47504 RVA: 0x002920E6 File Offset: 0x002902E6
		private TableCell MainCell
		{
			get
			{
				this.EnsureChildControls();
				return this.WrappingTable.Rows[1].Cells[0];
			}
		}

		// Token: 0x17003BEF RID: 15343
		// (get) Token: 0x0600B991 RID: 47505 RVA: 0x0029210A File Offset: 0x0029030A
		private Table MainTable
		{
			get
			{
				this.EnsureChildControls();
				return this.MainCell.Controls[0] as Table;
			}
		}

		// Token: 0x0600B992 RID: 47506 RVA: 0x00292128 File Offset: 0x00290328
		protected override bool OnBubbleEvent(object source, EventArgs args)
		{
			CommandEventArgs commandEventArgs = args as CommandEventArgs;
			if (commandEventArgs != null)
			{
				if (commandEventArgs.CommandName == "ChangeSort")
				{
					try
					{
						string[] array = ((string)commandEventArgs.CommandArgument).Split(new char[]
						{
							':'
						});
						this.ChangeSortOrder(int.Parse(array[0]), int.Parse(array[1]), array[2]);
						this.ownerGrid.MasterTableView.ClearEditItems();
					}
					catch (Exception)
					{
						throw new GridGroupByException("Invalid command argument of ChangeSort command event");
					}
				}
				base.OnBubbleEvent(source, args);
				return true;
			}
			return base.OnBubbleEvent(source, args);
		}

		// Token: 0x0600B993 RID: 47507 RVA: 0x002921CC File Offset: 0x002903CC
		private GridTableView FindTableViewIn(GridTableView ownerView, ref int tableIndex, int SearchTableIndex)
		{
			GridTableView gridTableView = null;
			if (SearchTableIndex == tableIndex)
			{
				gridTableView = ownerView;
			}
			else
			{
				foreach (GridTableView ownerView2 in ownerView.DetailTables)
				{
					tableIndex++;
					gridTableView = this.FindTableViewIn(ownerView2, ref tableIndex, SearchTableIndex);
					if (gridTableView != null)
					{
						break;
					}
				}
			}
			return gridTableView;
		}

		// Token: 0x0600B994 RID: 47508 RVA: 0x00292238 File Offset: 0x00290438
		private void ChangeSortOrder(int tableIndex, int expressionIndex, string fieldName)
		{
			int num = 0;
			GridTableView gridTableView = this.FindTableViewIn(this.ownerGrid.MasterTableView, ref num, tableIndex);
			GridGroupByExpression gridGroupByExpression = gridTableView.GroupByExpressions[expressionIndex];
			GridGroupByField gridGroupByField = gridGroupByExpression.GroupByFields.FindByName(fieldName);
			GridGroupsChangingEventArgs gridGroupsChangingEventArgs = new GridGroupsChangingEventArgs(gridTableView, gridGroupByExpression, gridGroupByField);
			this.ownerGrid.CallOnGroupsChanging(gridGroupsChangingEventArgs);
			if (gridGroupsChangingEventArgs.Canceled)
			{
				return;
			}
			if (gridGroupByField == null)
			{
				return;
			}
			if (gridGroupByField.SortOrder == GridSortOrder.Ascending)
			{
				gridGroupByField.SortOrder = GridSortOrder.Descending;
			}
			else
			{
				gridGroupByField.SortOrder = GridSortOrder.Ascending;
			}
			this.ownerGrid.Rebind();
		}

		// Token: 0x0600B995 RID: 47509 RVA: 0x002922C0 File Offset: 0x002904C0
		public void Ungroup(string index)
		{
			int searchTableIndex;
			int index2;
			try
			{
				string[] array = index.Split(new char[]
				{
					':'
				});
				searchTableIndex = int.Parse(array[0]);
				index2 = int.Parse(array[1]);
			}
			catch (Exception)
			{
				throw new GridGroupByException("Invalid command argument of Ungroup event");
			}
			int num = 0;
			GridTableView gridTableView = this.FindTableViewIn(this.ownerGrid.MasterTableView, ref num, searchTableIndex);
			GridGroupByExpression gridGroupByExpression = gridTableView.GroupByExpressions[index2];
			GridGroupsChangingEventArgs gridGroupsChangingEventArgs = new GridGroupsChangingEventArgs(gridTableView, gridGroupByExpression, GridGroupsChangingAction.Ungroup);
			this.ownerGrid.CallOnGroupsChanging(gridGroupsChangingEventArgs);
			if (gridGroupsChangingEventArgs.Canceled)
			{
				return;
			}
			gridTableView.GroupByExpressions.Remove(gridGroupByExpression);
			gridTableView.ResetRenderColumns();
			this.ownerGrid.Rebind();
		}

		// Token: 0x0600B996 RID: 47510 RVA: 0x00292380 File Offset: 0x00290580
		public void Swap(string index1, string index2, RadGrid ownerGrid)
		{
			int searchTableIndex;
			int num;
			int num2;
			try
			{
				string[] array = index1.Split(new char[]
				{
					':'
				});
				searchTableIndex = int.Parse(array[0]);
				num = int.Parse(array[1]);
				array = index2.Split(new char[]
				{
					':'
				});
				num2 = int.Parse(array[1]);
			}
			catch (Exception)
			{
				throw new GridGroupByException("Invalid command argument of Ungroup event");
			}
			if (num == num2)
			{
				return;
			}
			if (num < 0 || num2 < 0)
			{
				return;
			}
			int num3 = 0;
			if (ownerGrid.ResolvedRenderMode == RenderMode.Mobile && this.ownerGrid == null)
			{
				this.ownerGrid = ownerGrid;
			}
			GridTableView gridTableView = this.FindTableViewIn(this.ownerGrid.MasterTableView, ref num3, searchTableIndex);
			GridGroupByExpression gridGroupByExpression = gridTableView.GroupByExpressions[num];
			GridGroupByExpression gridGroupByExpression2 = gridTableView.GroupByExpressions[num2];
			GridGroupsChangingEventArgs gridGroupsChangingEventArgs = new GridGroupsChangingEventArgs(gridTableView, gridGroupByExpression, gridGroupByExpression2, GridGroupsChangingAction.Swap);
			this.ownerGrid.CallOnGroupsChanging(gridGroupsChangingEventArgs);
			if (gridGroupsChangingEventArgs.Canceled)
			{
				return;
			}
			try
			{
				gridTableView.GroupByExpressions.RemoveAt(gridGroupByExpression.Index);
				gridTableView.GroupByExpressions.RemoveAt(gridGroupByExpression2.Index);
				int num4 = Math.Min(num, num2);
				if (num4 == num)
				{
					gridTableView.GroupByExpressions.Insert(num, gridGroupByExpression2);
					gridTableView.GroupByExpressions.Insert(num2, gridGroupByExpression);
				}
				else
				{
					gridTableView.GroupByExpressions.Insert(num2, gridGroupByExpression);
					gridTableView.GroupByExpressions.Insert(num, gridGroupByExpression2);
				}
			}
			catch (Exception)
			{
			}
			gridTableView.ResetRenderColumns();
			this.ownerGrid.Rebind();
		}

		// Token: 0x17003BF0 RID: 15344
		// (get) Token: 0x0600B997 RID: 47511 RVA: 0x0029250C File Offset: 0x0029070C
		// (set) Token: 0x0600B998 RID: 47512 RVA: 0x00292514 File Offset: 0x00290714
		[DefaultValue(false)]
		[Browsable(false)]
		public override bool Visible
		{
			get
			{
				return base.Visible;
			}
			set
			{
				base.Visible = value;
			}
		}

		// Token: 0x0600B999 RID: 47513 RVA: 0x00292520 File Offset: 0x00290720
		internal string SerializeItemsToJavaScript()
		{
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			javaScriptSerializer.RegisterConverters(new JavaScriptConverter[]
			{
				new GridGroupPanelJavaScriptConverter()
			});
			List<GroupItemCell> list = new List<GroupItemCell>();
			List<string> list2 = new List<string>();
			if (this._groupPanelItems != null)
			{
				foreach (GroupItemCell groupItemCell in this._groupPanelItems)
				{
					if (!list2.Contains(groupItemCell.HierarchicalIndex))
					{
						list2.Add(groupItemCell.HierarchicalIndex);
						list.Add(groupItemCell);
					}
				}
			}
			return javaScriptSerializer.Serialize(list).ToString();
		}

		// Token: 0x040030FE RID: 12542
		private const string defText = "Drag a column header and drop it here to group by that column";

		// Token: 0x040030FF RID: 12543
		private RadGrid ownerGrid;

		// Token: 0x04003100 RID: 12544
		private GridGroupPanelStyle _panelStyle;

		// Token: 0x04003101 RID: 12545
		private GridGroupPanelStyle cachedPanelStyle;

		// Token: 0x04003102 RID: 12546
		private GridPanelItemsStyle _panelItemsStyle;

		// Token: 0x04003103 RID: 12547
		private GridPanelItemsStyle cachedPanelItemsStyle;

		// Token: 0x04003104 RID: 12548
		private List<GroupItemCell> _groupPanelItems;

		// Token: 0x04003105 RID: 12549
		private bool hasItems;
	}
}
