using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web;

namespace Telerik.Web.UI
{
	// Token: 0x02001233 RID: 4659
	internal class TreeListDataSourceHelper
	{
		// Token: 0x0600C02A RID: 49194 RVA: 0x002AA6C4 File Offset: 0x002A88C4
		public virtual TreeListEnumerableBase GetResolvedDataSource(RadTreeList ownerTreeList, object dataSource, string dataMember)
		{
			if (dataSource == null)
			{
				return TreeListEnumerableBase.Null;
			}
			IEnumerable enumerableFromSource = TreeListDataSourceHelper.GetEnumerableFromSource(dataSource, dataMember);
			if (enumerableFromSource != null)
			{
				return new TreeListEnumerable(enumerableFromSource, ownerTreeList.DataKeyNames, ownerTreeList.ParentDataKeyNames, ownerTreeList.ExpandedIndexes)
				{
					AutogenerateColumns = ownerTreeList.AutoGenerateColumns,
					OwnerTreeList = ownerTreeList
				};
			}
			return TreeListEnumerableBase.Null;
		}

		// Token: 0x0600C02B RID: 49195 RVA: 0x002AA718 File Offset: 0x002A8918
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		public static IEnumerable GetEnumerableFromSource(object dataSource, string dataMember = "")
		{
			if (dataSource is IQueryable)
			{
				return (IEnumerable)dataSource;
			}
			IListSource listSource = dataSource as IListSource;
			if (listSource != null)
			{
				IList list = listSource.GetList();
				if (!listSource.ContainsListCollection)
				{
					return list;
				}
				if (list != null && list is ITypedList)
				{
					ITypedList typedList = (ITypedList)list;
					PropertyDescriptorCollection itemProperties = typedList.GetItemProperties(new PropertyDescriptor[0]);
					if (itemProperties != null)
					{
						int count = itemProperties.Count;
					}
					if (itemProperties != null)
					{
						PropertyDescriptor propertyDescriptor = string.IsNullOrEmpty(dataMember) ? itemProperties[0] : itemProperties.Find(dataMember, true);
						if (propertyDescriptor != null)
						{
							object component = list[0];
							object value = propertyDescriptor.GetValue(component);
							if (value != null && value is IEnumerable)
							{
								return (IEnumerable)value;
							}
						}
					}
				}
			}
			else if (dataSource is IEnumerable)
			{
				SiteMapNodeCollection siteMapNodeCollection = dataSource as SiteMapNodeCollection;
				if (siteMapNodeCollection != null)
				{
					IList result = new List<SiteMapNodeWrapper>();
					foreach (object obj in siteMapNodeCollection)
					{
						SiteMapNode currentNode = (SiteMapNode)obj;
						TreeListDataSourceHelper.TraverseSiteMapNodeCollection(ref result, currentNode);
					}
					return result;
				}
				try
				{
					Array array = dataSource as Array;
					DataTable dataTable = null;
					if (array != null)
					{
						foreach (object obj2 in array)
						{
							DataRow dataRow = obj2 as DataRow;
							if (dataRow != null)
							{
								if (dataTable == null)
								{
									dataTable = new DataTable();
									foreach (object obj3 in dataRow.Table.Columns)
									{
										DataColumn dataColumn = (DataColumn)obj3;
										dataTable.Columns.Add(dataColumn.ColumnName, dataColumn.DataType);
									}
								}
								dataTable.LoadDataRow(dataRow.ItemArray, true);
							}
						}
					}
					return dataTable.DefaultView;
				}
				catch (Exception)
				{
				}
				return (IEnumerable)dataSource;
			}
			return null;
		}

		// Token: 0x0600C02C RID: 49196 RVA: 0x002AA95C File Offset: 0x002A8B5C
		private static void TraverseSiteMapNodeCollection(ref IList flatCollection, SiteMapNode currentNode)
		{
			flatCollection.Add(new SiteMapNodeWrapper(currentNode));
			if (currentNode.HasChildNodes)
			{
				foreach (object obj in currentNode.ChildNodes)
				{
					SiteMapNode currentNode2 = (SiteMapNode)obj;
					TreeListDataSourceHelper.TraverseSiteMapNodeCollection(ref flatCollection, currentNode2);
				}
			}
		}
	}
}
