using System;
using System.Collections;
using System.Data;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x0200101F RID: 4127
	internal class ControlDataBinder
	{
		// Token: 0x0600A2F8 RID: 41720 RVA: 0x002445B8 File Offset: 0x002427B8
		public ControlDataBinder(IItemContainer control)
		{
			this._control = control;
			this._cache = new PropertyDescriptorCache();
			this._dataSourceHelper = new DataSourceHelper(this._cache);
		}

		// Token: 0x0600A2F9 RID: 41721 RVA: 0x002445E4 File Offset: 0x002427E4
		public void BindToEnumerableData(IEnumerable dataSource)
		{
			foreach (object dataItem in dataSource)
			{
				this.BindItem(this._control.Children, dataItem, "", 0);
			}
		}

		// Token: 0x0600A2FA RID: 41722 RVA: 0x00244648 File Offset: 0x00242848
		private IItem BindItem(IList items, object dataItem, string dataMember, int depth)
		{
			return this.BindItem(items, dataItem, dataItem, dataMember, depth);
		}

		// Token: 0x0600A2FB RID: 41723 RVA: 0x00244658 File Offset: 0x00242858
		private IItem BindItem(IList items, object dataObject, object dataItem, string dataMember, int depth)
		{
			IItem item = this._control.CreateItem();
			items.Add(item);
			this.SetItemPropeties(item, dataObject, dataMember, depth);
			this.RaiseItemDataBound(item, dataItem);
			return item;
		}

		// Token: 0x0600A2FC RID: 41724 RVA: 0x0024468E File Offset: 0x0024288E
		private void RaiseItemDataBound(IItem item, object dataItem)
		{
			item.DataItem = dataItem;
			item.DataBind();
			this._control.RaiseItemDataBound(item);
			item.DataItem = null;
		}

		// Token: 0x0600A2FD RID: 41725 RVA: 0x002446B0 File Offset: 0x002428B0
		private void SetItemPropeties(IItem item, object dataItem, string dataMember, int depth)
		{
			item.PopulateFromDataItem(this._cache, dataItem, dataMember, depth);
		}

		// Token: 0x0600A2FE RID: 41726 RVA: 0x002446C2 File Offset: 0x002428C2
		public void BindToHierarchicalData(IHierarchicalEnumerable enumerable)
		{
			this.BindToHierarchicalEnumerable(this._control.Children, enumerable, 0);
		}

		// Token: 0x0600A2FF RID: 41727 RVA: 0x002446D8 File Offset: 0x002428D8
		private void BindToHierarchicalEnumerable(IList items, IHierarchicalEnumerable enumerable, int depth)
		{
			IHierarchicalItemContainer hierarchicalItemContainer = (IHierarchicalItemContainer)this._control;
			checked
			{
				if (hierarchicalItemContainer.MaxDataBindDepth != -1 && depth + 1 > hierarchicalItemContainer.MaxDataBindDepth)
				{
					return;
				}
				foreach (object obj in enumerable)
				{
					IHierarchyData hierarchyData = enumerable.GetHierarchyData(obj);
					if (hierarchyData == null)
					{
						break;
					}
					IItem item = this.BindItem(items, obj, hierarchyData.Item, hierarchyData.Type, depth);
					if (hierarchyData.HasChildren)
					{
						IHierarchicalEnumerable children = hierarchyData.GetChildren();
						if (children != null)
						{
							this.BindToHierarchicalEnumerable(item.Children, children, depth + 1);
						}
					}
				}
			}
		}

		// Token: 0x0600A300 RID: 41728 RVA: 0x00244790 File Offset: 0x00242990
		public void BindToDataTable(DataTable table, string dataFieldID, string dataFieldParentID)
		{
			DataColumn dataColumn = table.Columns[dataFieldID];
			DataColumn dataColumn2 = table.Columns[dataFieldParentID];
			if (dataColumn == null || dataColumn2 == null)
			{
				throw new ArgumentException("Columns specified by DataFieldID/DataFieldParentID not found.");
			}
			this.BindRootItems(dataFieldParentID, table, new DataSet
			{
				Tables = 
				{
					table
				},
				Relations = 
				{
					{
						dataColumn,
						dataColumn2
					}
				}
			}.Relations[0]);
		}

		// Token: 0x0600A301 RID: 41729 RVA: 0x002447FC File Offset: 0x002429FC
		private void BindRootItems(string parentColumnName, DataTable view, DataRelation relation)
		{
			view.DefaultView.Sort = relation.ParentColumns[0].ColumnName;
			foreach (object obj in view.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				if (dataRow[parentColumnName] is DBNull)
				{
					this.BindChildItems(dataRow, this._control.Children, relation, 0);
				}
			}
		}

		// Token: 0x0600A302 RID: 41730 RVA: 0x00244888 File Offset: 0x00242A88
		private void BindChildItems(DataRow row, IList items, DataRelation relation, int depth)
		{
			IHierarchicalItemContainer hierarchicalItemContainer = (IHierarchicalItemContainer)this._control;
			checked
			{
				if (hierarchicalItemContainer.MaxDataBindDepth != -1 && depth + 1 > hierarchicalItemContainer.MaxDataBindDepth)
				{
					return;
				}
				DataRowView dataItem = row.Table.DefaultView.FindRows(row[relation.ParentColumns[0].ColumnName])[0];
				IItem item = this.BindItem(items, dataItem, "", depth);
				foreach (DataRow row2 in row.GetChildRows(relation))
				{
					this.BindChildItems(row2, item.Children, relation, depth + 1);
				}
			}
		}

		// Token: 0x0600A303 RID: 41731 RVA: 0x00244924 File Offset: 0x00242B24
		public void BindToEnumerableData(IEnumerable data, string dataFieldID, string dataFieldParentID)
		{
			if (string.IsNullOrEmpty(dataFieldID) || string.IsNullOrEmpty(dataFieldParentID))
			{
				this.BindToEnumerableData(data);
				return;
			}
			IList data2 = DataSourceHelper.CopyDataSource(data);
			IList list = this._dataSourceHelper.FilterRootDataItems(dataFieldParentID, data2);
			foreach (object dataItem in list)
			{
				this.BindChildItems(dataItem, data2, this._control.Children, 0, dataFieldID, dataFieldParentID);
			}
		}

		// Token: 0x0600A304 RID: 41732 RVA: 0x002449B4 File Offset: 0x00242BB4
		private void BindChildItems(object dataItem, IList data, IList items, int depth, string dataFieldID, string dataFieldParentID)
		{
			IHierarchicalItemContainer hierarchicalItemContainer = (IHierarchicalItemContainer)this._control;
			checked
			{
				if (hierarchicalItemContainer.MaxDataBindDepth != -1 && depth + 1 > hierarchicalItemContainer.MaxDataBindDepth)
				{
					return;
				}
				IItem item = this.BindItem(items, dataItem, "", depth);
				object propertyValue = this._cache.GetPropertyValue(dataItem, dataFieldID);
				IList list = this._dataSourceHelper.FilterChildren(dataFieldID, dataFieldParentID, propertyValue, dataItem, data);
				foreach (object dataItem2 in list)
				{
					this.BindChildItems(dataItem2, data, item.Children, depth + 1, dataFieldID, dataFieldParentID);
				}
			}
		}

		// Token: 0x04002D4C RID: 11596
		private readonly IItemContainer _control;

		// Token: 0x04002D4D RID: 11597
		private readonly PropertyDescriptorCache _cache;

		// Token: 0x04002D4E RID: 11598
		private readonly DataSourceHelper _dataSourceHelper;
	}
}
