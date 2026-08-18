using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x0200114D RID: 4429
	public class GridNestedViewItem : GridItem
	{
		// Token: 0x0600B453 RID: 46163 RVA: 0x00277530 File Offset: 0x00275730
		public GridNestedViewItem(GridTableView ownerTableView, int itemIndex, int dataSetIndex) : base(ownerTableView, itemIndex, dataSetIndex, GridItemType.NestedView)
		{
		}

		// Token: 0x17003A46 RID: 14918
		// (get) Token: 0x0600B454 RID: 46164 RVA: 0x0027753D File Offset: 0x0027573D
		public TableCell NestedViewCell
		{
			get
			{
				return this._nestedViewCell;
			}
		}

		// Token: 0x17003A47 RID: 14919
		// (get) Token: 0x0600B455 RID: 46165 RVA: 0x00277548 File Offset: 0x00275748
		[SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
		public GridTableView[] NestedTableViews
		{
			get
			{
				ArrayList arrayList = new ArrayList();
				foreach (object obj in this.NestedViewCell.Controls)
				{
					Control control = (Control)obj;
					GridTableView gridTableView = control as GridTableView;
					if (gridTableView != null)
					{
						arrayList.Add(gridTableView);
					}
				}
				GridTableView[] array = new GridTableView[arrayList.Count];
				arrayList.CopyTo(array);
				return array;
			}
		}

		// Token: 0x17003A48 RID: 14920
		// (get) Token: 0x0600B456 RID: 46166 RVA: 0x002775D4 File Offset: 0x002757D4
		// (set) Token: 0x0600B457 RID: 46167 RVA: 0x002775DC File Offset: 0x002757DC
		public GridDataItem ParentItem
		{
			get
			{
				return this._parentItem;
			}
			protected internal set
			{
				this._parentItem = value;
			}
		}

		// Token: 0x0600B458 RID: 46168 RVA: 0x002775E5 File Offset: 0x002757E5
		public override void PrepareItemStyle()
		{
			if (this.NestedViewCell == null)
			{
				return;
			}
			this.NestedViewCell.ColumnSpan = base.CalcColSpan(base.OwnerTableView.RenderColumns, this.calculatedColumnIndex, -1);
			base.PrepareItemStyle();
		}

		// Token: 0x0600B459 RID: 46169 RVA: 0x0027761C File Offset: 0x0027581C
		public override void Initialize(GridColumn[] columns)
		{
			int i;
			for (i = 0; i < base.OwnerTableView.GroupByExpressions.Count; i++)
			{
				this.Cells.Add(this.CreateCellObject());
			}
			this.Cells.Add(this.CreateCellObject());
			this.calculatedColumnIndex = i + 1;
			TableCell tableCell = this.CreateCellObject();
			this.Cells.Add(tableCell);
			tableCell.ColumnSpan = base.CalcColSpan(columns, this.calculatedColumnIndex, -1);
			tableCell.VerticalAlign = VerticalAlign.Top;
			if (base.OwnerTableView.Dir == GridTableTextDirection.LTR)
			{
				tableCell.HorizontalAlign = HorizontalAlign.Left;
			}
			else
			{
				tableCell.HorizontalAlign = HorizontalAlign.Right;
			}
			if (base.OwnerTableView.nestedViewTemplate != null)
			{
				PlaceHolder placeHolder = new PlaceHolder();
				placeHolder.ID = "NestedViewTemplatePlaceHolder";
				tableCell.Controls.Add(placeHolder);
				base.OwnerTableView.nestedViewTemplate.InstantiateIn(placeHolder);
			}
			this._nestedViewCell = tableCell;
		}

		// Token: 0x0600B45A RID: 46170 RVA: 0x00277700 File Offset: 0x00275900
		public override void SetupItem(bool dataBind, object dataItem, GridColumn[] columns, ControlCollection rows)
		{
			rows.Add(this);
			this.Initialize(columns);
			GridItemEventArgs e = new GridItemEventArgs(this, new GridItemCreated());
			base.OwnerTableView.OwnerGrid.CallOnItemCreated(e);
			if (base.OwnerTableView.nestedViewTemplate != null)
			{
				if (string.IsNullOrEmpty(base.OwnerTableView.NestedViewSettings.DataSourceID))
				{
					this.DataItem = this.GetParentDataItem(rows);
					if (this.DataItem != null)
					{
						this.DataBind();
						return;
					}
				}
				else if (((base.OwnerTableView.HierarchyLoadMode != GridChildLoadMode.ServerOnDemand && base.OwnerTableView.HierarchyLoadMode != GridChildLoadMode.Conditional) || this.ParentItem.Expanded) && dataBind)
				{
					this.PerformDataBindWithDataSource();
				}
			}
		}

		// Token: 0x0600B45B RID: 46171 RVA: 0x002777AC File Offset: 0x002759AC
		protected void BindDataItem(IEnumerable data)
		{
			if (data != null)
			{
				IEnumerator enumerator = data.GetEnumerator();
				if (enumerator.MoveNext())
				{
					this.DataItem = enumerator.Current;
				}
			}
		}

		// Token: 0x0600B45C RID: 46172 RVA: 0x002777D8 File Offset: 0x002759D8
		internal void PerformDataBindWithDataSource()
		{
			IDataSource dataSource = this.SetDataSourceParameters() as IDataSource;
			if (dataSource != null)
			{
				dataSource.GetView("DefaultView").Select(new DataSourceSelectArguments(), new DataSourceViewSelectCallback(this.BindDataItem));
			}
			this.DataBind();
		}

		// Token: 0x0600B45D RID: 46173 RVA: 0x0027781C File Offset: 0x00275A1C
		public override void DataBind()
		{
			if (this.DataItem != null)
			{
				base.DataBind();
				GridItemEventArgs e = new GridItemEventArgs(this, new GridItemDataBound());
				base.OwnerTableView.OwnerGrid.CallOnItemDataBound(e);
			}
		}

		// Token: 0x0600B45E RID: 46174 RVA: 0x00277854 File Offset: 0x00275A54
		protected Control SetDataSourceParameters()
		{
			Control control = DataSourceControlHelper.FindControl(this, base.OwnerTableView.NestedViewSettings.DataSourceID);
			ParameterCollection parameterCollection = GridPropertyEvaluator.GetPropertyValue(control, "WhereParameters") as ParameterCollection;
			if (parameterCollection == null)
			{
				parameterCollection = (GridPropertyEvaluator.GetPropertyValue(control, "SelectParameters") as ParameterCollection);
				if (parameterCollection == null)
				{
					parameterCollection = (GridPropertyEvaluator.GetPropertyValue(control, "QueryParameters") as ParameterCollection);
				}
			}
			if (parameterCollection != null && parameterCollection.Count > 0)
			{
				foreach (object obj in parameterCollection)
				{
					Parameter parameter = (Parameter)obj;
					GridRelationFields gridRelationFields = null;
					foreach (GridRelationFields gridRelationFields2 in base.OwnerTableView.NestedViewSettings.ParentTableRelation)
					{
						if (parameter.Name.ToUpper() == gridRelationFields2.DetailKeyField.ToUpper())
						{
							gridRelationFields = gridRelationFields2;
							break;
						}
					}
					if (gridRelationFields != null)
					{
						GridDataKeyArray dataKeyValues = this.ParentItem.OwnerTableView.DataKeyValues;
						if (dataKeyValues.Count > 0)
						{
							DataKey dataKey = dataKeyValues[this.ParentItem.ItemIndex];
							object obj2 = dataKey[gridRelationFields.MasterKeyField];
							if (obj2 != null)
							{
								parameter.DefaultValue = obj2.ToString();
							}
						}
					}
				}
			}
			return control;
		}

		// Token: 0x0600B45F RID: 46175 RVA: 0x002779DC File Offset: 0x00275BDC
		internal object GetParentDataItem(ControlCollection rows)
		{
			object result = null;
			for (int i = rows.Count - 2; i > 0; i--)
			{
				if (rows[i] is GridDataItem)
				{
					result = ((GridDataItem)rows[i]).DataItem;
					break;
				}
			}
			return result;
		}

		// Token: 0x04002F85 RID: 12165
		private TableCell _nestedViewCell;

		// Token: 0x04002F86 RID: 12166
		private int calculatedColumnIndex;

		// Token: 0x04002F87 RID: 12167
		private GridDataItem _parentItem;
	}
}
