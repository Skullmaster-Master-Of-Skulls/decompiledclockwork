using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000F18 RID: 3864
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")]
	public abstract class TreeListColumn : StateManager, IComparable
	{
		// Token: 0x0600936A RID: 37738 RVA: 0x00211C6C File Offset: 0x0020FE6C
		public virtual void InitializeCell(TableCell cell, int columnIndex, TreeListItem inItem)
		{
			TreeListItemType itemType = inItem.ItemType;
			if (itemType == TreeListItemType.HeaderItem)
			{
				this.InitializeHeaderCells(cell, columnIndex, inItem as TreeListHeaderItem);
				return;
			}
			if (itemType != TreeListItemType.FooterItem)
			{
				TreeListDataItem treeListDataItem = inItem as TreeListDataItem;
				if (treeListDataItem != null)
				{
					this.InitializeDataCells(cell, columnIndex, treeListDataItem);
				}
				return;
			}
			this.InitializeFooterCells(cell, columnIndex, inItem as TreeListFooterItem);
		}

		// Token: 0x0600936B RID: 37739 RVA: 0x00211CC0 File Offset: 0x0020FEC0
		protected virtual void InitializeFooterCells(TableCell cell, int columnIndex, TreeListFooterItem inItem)
		{
			if (inItem.HierarchyIndex != null && inItem.OwnerTreeList.CalculatedAggregates != null)
			{
				string key = inItem.HierarchyIndex.LevelIndex.ToString() + inItem.HierarchyIndex.NestedLevel.ToString();
				if (inItem.OwnerTreeList.CalculatedAggregates.ContainsKey(key) && inItem.OwnerTreeList.CalculatedAggregates[key].ContainsKey(this.UniqueName))
				{
					cell.Text = string.Format("{0}{1}", this.FooterText, inItem.OwnerTreeList.CalculatedAggregates[key][this.UniqueName].ToString());
				}
			}
		}

		// Token: 0x0600936C RID: 37740 RVA: 0x00211D84 File Offset: 0x0020FF84
		protected virtual void InitializeHeaderCells(TableCell cell, int columnIndex, TreeListHeaderItem inItem)
		{
			if (this.Owner.AllowSorting && this.Sortable)
			{
				LinkButton linkButton = new LinkButton();
				cell.Controls.Add(linkButton);
				linkButton.Text = this.HeaderText;
				linkButton.CausesValidation = false;
				linkButton.CommandName = "Sort";
				linkButton.CommandArgument = this.GetSortExpression();
				linkButton.ToolTip = this.HeaderTooltip;
				if (!string.IsNullOrEmpty(this.Owner.SortingSettings.SortToolTip))
				{
					linkButton.ToolTip = this.Owner.SortingSettings.SortToolTip;
				}
				if (this.Owner.SortExpressions.ContainsExpression(this.GetSortExpression()))
				{
					cell.Controls.Add(new LiteralControl("&nbsp;"));
					if (this.Owner.ResolvedRenderMode == RenderMode.Lightweight || this.Owner.ResolvedRenderMode == RenderMode.Mobile)
					{
						LinkButton linkButton2 = new LinkButton();
						linkButton2.ID = string.Format(CultureInfo.InvariantCulture, "{0}_SortIconButton", new object[]
						{
							this.UniqueName
						});
						cell.Controls.Add(linkButton2);
						linkButton2.Text = " ";
						linkButton2.ToolTip = this.HeaderTooltip;
						if (!string.IsNullOrEmpty(this.Owner.SortingSettings.SortToolTip))
						{
							linkButton2.ToolTip = this.Owner.SortingSettings.SortToolTip;
						}
						linkButton2.CausesValidation = false;
						linkButton2.CommandName = "Sort";
						linkButton2.CommandArgument = this.GetSortExpression();
					}
					else
					{
						Button button = new Button();
						button.ID = string.Format(CultureInfo.InvariantCulture, "{0}_SortIconButton", new object[]
						{
							this.UniqueName
						});
						cell.Controls.Add(button);
						button.Text = " ";
						button.ToolTip = this.HeaderTooltip;
						if (!string.IsNullOrEmpty(this.Owner.SortingSettings.SortToolTip))
						{
							button.ToolTip = this.Owner.SortingSettings.SortToolTip;
						}
						button.CausesValidation = false;
						button.CommandName = "Sort";
						button.CommandArgument = this.GetSortExpression();
					}
				}
			}
			else if (!string.IsNullOrEmpty(this.HeaderText))
			{
				cell.Text = this.HeaderText;
			}
			cell.ToolTip = this.HeaderTooltip;
		}

		// Token: 0x0600936D RID: 37741
		protected abstract void InitializeDataCells(TableCell cell, int columnIndex, TreeListDataItem inItem);

		// Token: 0x0600936E RID: 37742 RVA: 0x00211FD3 File Offset: 0x002101D3
		internal static TreeListItem GetBindingParentItem(Control control)
		{
			if (control.Parent != null && control.NamingContainer is TreeListItem)
			{
				return (TreeListItem)control.NamingContainer;
			}
			return TreeListColumn.GetBindingParentItem(control.NamingContainer);
		}

		// Token: 0x17002E9B RID: 11931
		// (get) Token: 0x0600936F RID: 37743 RVA: 0x00212001 File Offset: 0x00210201
		// (set) Token: 0x06009370 RID: 37744 RVA: 0x00212009 File Offset: 0x00210209
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public RadTreeList Owner { get; internal set; }

		// Token: 0x06009371 RID: 37745 RVA: 0x00212012 File Offset: 0x00210212
		internal void SetOwner(RadTreeList owner)
		{
			this.Owner = owner;
		}

		// Token: 0x17002E9C RID: 11932
		// (get) Token: 0x06009372 RID: 37746 RVA: 0x0021201C File Offset: 0x0021021C
		protected bool DesignMode
		{
			get
			{
				bool result = false;
				if (this.Owner != null && this.Owner.Site != null)
				{
					result = this.Owner.Site.DesignMode;
				}
				return result;
			}
		}

		// Token: 0x17002E9D RID: 11933
		// (get) Token: 0x06009373 RID: 37747 RVA: 0x00212052 File Offset: 0x00210252
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public string ColumnType
		{
			get
			{
				return base.GetType().Name;
			}
		}

		// Token: 0x17002E9E RID: 11934
		// (get) Token: 0x06009374 RID: 37748 RVA: 0x00212060 File Offset: 0x00210260
		// (set) Token: 0x06009375 RID: 37749 RVA: 0x0021208D File Offset: 0x0021028D
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		public string HeaderText
		{
			get
			{
				object obj = base.ViewState["HeaderText"];
				if (obj == null)
				{
					obj = string.Empty;
				}
				return obj.ToString();
			}
			set
			{
				base.ViewState["HeaderText"] = value;
			}
		}

		// Token: 0x17002E9F RID: 11935
		// (get) Token: 0x06009376 RID: 37750 RVA: 0x002120A0 File Offset: 0x002102A0
		// (set) Token: 0x06009377 RID: 37751 RVA: 0x002120C0 File Offset: 0x002102C0
		[NotifyParentProperty(true)]
		[Description("Gets or sets the tooltip of the header cell.")]
		[DefaultValue("")]
		[Localizable(true)]
		public virtual string HeaderTooltip
		{
			get
			{
				return (base.ViewState["HeaderTooltip"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["HeaderTooltip"] = value;
			}
		}

		// Token: 0x17002EA0 RID: 11936
		// (get) Token: 0x06009378 RID: 37752 RVA: 0x002120D4 File Offset: 0x002102D4
		// (set) Token: 0x06009379 RID: 37753 RVA: 0x00212101 File Offset: 0x00210301
		[Category("Appearance")]
		[DefaultValue("")]
		[Description("TreeListColumn_FooterText")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		public virtual string FooterText
		{
			get
			{
				object obj = base.ViewState["FooterText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["FooterText"] = value;
			}
		}

		// Token: 0x0600937A RID: 37754 RVA: 0x00212114 File Offset: 0x00210314
		private void GetUniqueName()
		{
			this._uniqueName = this.GenerateUniqueName();
		}

		// Token: 0x0600937B RID: 37755 RVA: 0x00212124 File Offset: 0x00210324
		protected bool IsUniqueName(string testName)
		{
			if (this.Owner != null)
			{
				foreach (TreeListColumn treeListColumn in this.Owner.Columns)
				{
					if (this != treeListColumn && treeListColumn._uniqueName == testName)
					{
						return false;
					}
				}
				foreach (TreeListColumn treeListColumn2 in this.Owner.AutoGeneratedColumns)
				{
					if (this != treeListColumn2 && treeListColumn2._uniqueName == testName)
					{
						return false;
					}
				}
				return true;
			}
			return true;
		}

		// Token: 0x17002EA1 RID: 11937
		// (get) Token: 0x0600937C RID: 37756 RVA: 0x002121D0 File Offset: 0x002103D0
		protected virtual bool IsDefaultUniqueName
		{
			get
			{
				return this._isDefaultUniqueName;
			}
		}

		// Token: 0x0600937D RID: 37757 RVA: 0x002121D8 File Offset: 0x002103D8
		protected string GenerateUniqueNameBase(string Base)
		{
			string text = (!string.IsNullOrEmpty(Base)) ? Base : "column";
			string text2 = text;
			if (this.Owner != null)
			{
				for (int i = 0; i < 500; i++)
				{
					text2 = text + ((i != 0) ? i.ToString(CultureInfo.InvariantCulture) : string.Empty);
					if (this.IsUniqueName(text2))
					{
						break;
					}
				}
			}
			else
			{
				this._isDefaultUniqueName = true;
			}
			return text2;
		}

		// Token: 0x0600937E RID: 37758 RVA: 0x00212240 File Offset: 0x00210440
		protected virtual string GenerateUniqueName()
		{
			return this.GenerateUniqueNameBase("column");
		}

		// Token: 0x0600937F RID: 37759 RVA: 0x0021224D File Offset: 0x0021044D
		internal virtual void PrepareCell(TableCell cell, TreeListItem item)
		{
		}

		// Token: 0x17002EA2 RID: 11938
		// (get) Token: 0x06009380 RID: 37760 RVA: 0x0021224F File Offset: 0x0021044F
		// (set) Token: 0x06009381 RID: 37761 RVA: 0x0021226A File Offset: 0x0021046A
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		[Browsable(true)]
		public virtual string UniqueName
		{
			get
			{
				if (string.IsNullOrEmpty(this._uniqueName))
				{
					this.GetUniqueName();
				}
				return this._uniqueName;
			}
			set
			{
				this._uniqueName = value;
				this._isDefaultUniqueName = false;
			}
		}

		// Token: 0x06009382 RID: 37762 RVA: 0x0021227A File Offset: 0x0021047A
		protected void UpdateUniqueNameIfDefault(string value)
		{
			if (this.IsDefaultUniqueName)
			{
				this._uniqueName = this.GenerateUniqueNameBase(value);
			}
		}

		// Token: 0x06009383 RID: 37763 RVA: 0x00212294 File Offset: 0x00210494
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		[SuppressMessage("Microsoft.Design", "CA1007:UseGenericsWhereAppropriate")]
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
		internal bool TryExtractDataValue(object dataItem, string name, out object value)
		{
			if (dataItem is DataRowView)
			{
				if ((dataItem as DataRowView).Row.Table.Columns.Contains(name))
				{
					value = ((DataRowView)dataItem)[name];
				}
				else
				{
					value = null;
				}
			}
			else if (dataItem is DataRow)
			{
				value = ((DataRow)dataItem)[name];
			}
			else
			{
				if (name.Contains("."))
				{
					try
					{
						value = DataBinder.GetPropertyValue(dataItem, name);
						goto IL_7F;
					}
					catch
					{
						value = DataBinder.Eval(dataItem, name);
						goto IL_7F;
					}
				}
				value = DataBinder.GetPropertyValue(dataItem, name);
			}
			IL_7F:
			return value != null && value != DBNull.Value;
		}

		// Token: 0x17002EA3 RID: 11939
		// (get) Token: 0x06009384 RID: 37764 RVA: 0x00212340 File Offset: 0x00210540
		[NotifyParentProperty(true)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Category("Style")]
		public virtual TableItemStyle HeaderStyle
		{
			get
			{
				if (this._headerStyle == null)
				{
					this._headerStyle = new TableItemStyle();
					if (this.IsTrackingViewState)
					{
						((IStateManager)this._headerStyle).TrackViewState();
					}
				}
				return this._headerStyle;
			}
		}

		// Token: 0x17002EA4 RID: 11940
		// (get) Token: 0x06009385 RID: 37765 RVA: 0x0021236E File Offset: 0x0021056E
		[Category("Style")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		public virtual TableItemStyle ItemStyle
		{
			get
			{
				if (this._itemStyle == null)
				{
					this._itemStyle = new TableItemStyle();
					if (this.IsTrackingViewState)
					{
						((IStateManager)this._itemStyle).TrackViewState();
					}
				}
				return this._itemStyle;
			}
		}

		// Token: 0x17002EA5 RID: 11941
		// (get) Token: 0x06009386 RID: 37766 RVA: 0x0021239C File Offset: 0x0021059C
		// (set) Token: 0x06009387 RID: 37767 RVA: 0x002123C5 File Offset: 0x002105C5
		[NotifyParentProperty(true)]
		[DefaultValue(true)]
		[Category("Behavior")]
		public virtual bool Visible
		{
			get
			{
				object obj = base.ViewState["Visible"];
				return obj == null || (bool)obj;
			}
			set
			{
				base.ViewState["Visible"] = value;
			}
		}

		// Token: 0x17002EA6 RID: 11942
		// (get) Token: 0x06009388 RID: 37768 RVA: 0x002123DD File Offset: 0x002105DD
		protected virtual bool Sortable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002EA7 RID: 11943
		// (get) Token: 0x06009389 RID: 37769 RVA: 0x002123E0 File Offset: 0x002105E0
		// (set) Token: 0x0600938A RID: 37770 RVA: 0x00212409 File Offset: 0x00210609
		[DefaultValue(true)]
		[Description("Gets or sets a value indicating whether the column can be resized client-side")]
		[Category("Behavior")]
		[NotifyParentProperty(true)]
		public virtual bool Resizable
		{
			get
			{
				object obj = base.ViewState["Resizable"];
				return obj == null || (bool)obj;
			}
			set
			{
				base.ViewState["Resizable"] = value;
			}
		}

		// Token: 0x17002EA8 RID: 11944
		// (get) Token: 0x0600938B RID: 37771 RVA: 0x00212424 File Offset: 0x00210624
		// (set) Token: 0x0600938C RID: 37772 RVA: 0x0021244D File Offset: 0x0021064D
		[DefaultValue(true)]
		[Description("Gets or sets a value indicating whether the column can be reordered client-side")]
		[NotifyParentProperty(true)]
		[Category("Behavior")]
		public virtual bool Reorderable
		{
			get
			{
				object obj = base.ViewState["Reorderable"];
				return obj == null || (bool)obj;
			}
			set
			{
				base.ViewState["Reorderable"] = value;
			}
		}

		// Token: 0x17002EA9 RID: 11945
		// (get) Token: 0x0600938D RID: 37773 RVA: 0x00212468 File Offset: 0x00210668
		// (set) Token: 0x0600938E RID: 37774 RVA: 0x00212495 File Offset: 0x00210695
		[Description("Gets or sets minimum width of the column. Used when resizing.")]
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(Unit), "")]
		[Category("Behavior")]
		public virtual Unit MinWidth
		{
			get
			{
				object obj = base.ViewState["MinWidth"];
				if (obj != null)
				{
					return (Unit)obj;
				}
				return Unit.Empty;
			}
			set
			{
				base.ViewState["MinWidth"] = value;
			}
		}

		// Token: 0x17002EAA RID: 11946
		// (get) Token: 0x0600938F RID: 37775 RVA: 0x002124B0 File Offset: 0x002106B0
		// (set) Token: 0x06009390 RID: 37776 RVA: 0x002124DD File Offset: 0x002106DD
		[DefaultValue(typeof(Unit), "")]
		[NotifyParentProperty(true)]
		[Description("Gets or sets maximum width of the column. Used when resizing.")]
		[Category("Behavior")]
		public virtual Unit MaxWidth
		{
			get
			{
				object obj = base.ViewState["MaxWidth"];
				if (obj != null)
				{
					return (Unit)obj;
				}
				return Unit.Empty;
			}
			set
			{
				base.ViewState["MaxWidth"] = value;
			}
		}

		// Token: 0x17002EAB RID: 11947
		// (get) Token: 0x06009391 RID: 37777 RVA: 0x002124F8 File Offset: 0x002106F8
		// (set) Token: 0x06009392 RID: 37778 RVA: 0x00212521 File Offset: 0x00210721
		[Description("Gets or sets a value that indicates whether to hide the cells for this column (with display:none)")]
		[Category("Behavior")]
		[NotifyParentProperty(true)]
		[DefaultValue(true)]
		public bool Display
		{
			get
			{
				object obj = base.ViewState["Display"];
				return obj == null || (bool)obj;
			}
			set
			{
				base.ViewState["Display"] = value;
			}
		}

		// Token: 0x17002EAC RID: 11948
		// (get) Token: 0x06009393 RID: 37779 RVA: 0x0021253C File Offset: 0x0021073C
		// (set) Token: 0x06009394 RID: 37780 RVA: 0x00212565 File Offset: 0x00210765
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public int OrderIndex
		{
			get
			{
				object obj = base.ViewState["OrderIndex"];
				if (obj != null)
				{
					return (int)obj;
				}
				return -1;
			}
			set
			{
				if (value != -1)
				{
					base.ViewState["OrderIndex"] = value;
					return;
				}
				base.ViewState["OrderIndex"] = null;
			}
		}

		// Token: 0x06009395 RID: 37781 RVA: 0x00212594 File Offset: 0x00210794
		public int CompareTo(object obj)
		{
			TreeListColumn treeListColumn = obj as TreeListColumn;
			if (treeListColumn == null)
			{
				return 1;
			}
			return this.OrderIndex.CompareTo(treeListColumn.OrderIndex);
		}

		// Token: 0x06009396 RID: 37782 RVA: 0x002125C1 File Offset: 0x002107C1
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		protected virtual string GetSortExpression()
		{
			return this.SortExpression;
		}

		// Token: 0x06009397 RID: 37783 RVA: 0x002125C9 File Offset: 0x002107C9
		internal string GetSortExpressionInternal()
		{
			return this.GetSortExpression();
		}

		// Token: 0x17002EAD RID: 11949
		// (get) Token: 0x06009398 RID: 37784 RVA: 0x002125D4 File Offset: 0x002107D4
		// (set) Token: 0x06009399 RID: 37785 RVA: 0x00212601 File Offset: 0x00210801
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[Category("Behavior")]
		[DefaultValue("")]
		public virtual string SortExpression
		{
			get
			{
				object obj = base.ViewState["SortExpression"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["SortExpression"] = value;
			}
		}

		// Token: 0x0600939A RID: 37786 RVA: 0x00212614 File Offset: 0x00210814
		protected override void TrackViewState()
		{
			base.TrackViewState();
			if (this._headerStyle != null)
			{
				((IStateManager)this._headerStyle).TrackViewState();
			}
		}

		// Token: 0x0600939B RID: 37787 RVA: 0x00212630 File Offset: 0x00210830
		protected override object SaveViewState()
		{
			object obj = null;
			if (this._headerStyle != null)
			{
				this._headerStyle.SetDirty();
				obj = ((IStateManager)this._headerStyle).SaveViewState();
			}
			return new object[]
			{
				base.SaveViewState(),
				obj,
				this._uniqueName
			};
		}

		// Token: 0x0600939C RID: 37788 RVA: 0x0021267C File Offset: 0x0021087C
		protected override void LoadViewState(object state)
		{
			if (state != null)
			{
				object[] array = (object[])state;
				base.LoadViewState(array[0]);
				if (array[1] != null)
				{
					this.HeaderStyle.Width = Unit.Empty;
					((IStateManager)this.HeaderStyle).LoadViewState(array[1]);
				}
				if (array[2] != null)
				{
					this._uniqueName = (string)array[2];
					return;
				}
			}
			else
			{
				base.LoadViewState(state);
			}
		}

		// Token: 0x04002A52 RID: 10834
		private string _uniqueName;

		// Token: 0x04002A53 RID: 10835
		private bool _isDefaultUniqueName;

		// Token: 0x04002A54 RID: 10836
		private TableItemStyle _headerStyle;

		// Token: 0x04002A55 RID: 10837
		private TableItemStyle _itemStyle;
	}
}
