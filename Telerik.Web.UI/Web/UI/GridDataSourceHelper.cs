using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Web.UI
{
	// Token: 0x020010E9 RID: 4329
	internal class GridDataSourceHelper
	{
		// Token: 0x0600B14A RID: 45386 RVA: 0x00265FED File Offset: 0x002641ED
		private GridDataSourceHelper()
		{
		}

		// Token: 0x0600B14B RID: 45387 RVA: 0x00265FF5 File Offset: 0x002641F5
		public static bool IsOpenAccess(Type enumerableType)
		{
			return enumerableType.Name.Contains("PureEnumerable");
		}

		// Token: 0x0600B14C RID: 45388 RVA: 0x00266007 File Offset: 0x00264207
		public static bool IsEntity(Type enumerableType)
		{
			return enumerableType.Name.Contains("EntityDataSourceWrapperCollection");
		}

		// Token: 0x0600B14D RID: 45389 RVA: 0x0026601C File Offset: 0x0026421C
		public static GridEnumerableBase CreateGridEnumerable(GridTableView owner, IEnumerable enumerable, bool caseSensitive, bool autoGenerateColumns, GridColumnCollection presentColumns, string[] additionalField, bool retrieveAllFields, bool enableSplitHeaderText)
		{
			DataView dataView = enumerable as DataView;
			if (dataView != null)
			{
				return new GridEnumerableFromDataView(owner, dataView, autoGenerateColumns, presentColumns, additionalField, retrieveAllFields, enableSplitHeaderText);
			}
			return new GridEnumerableFromDataView(owner, enumerable, caseSensitive, autoGenerateColumns, presentColumns, additionalField, retrieveAllFields, enableSplitHeaderText);
		}

		// Token: 0x0600B14E RID: 45390 RVA: 0x00266058 File Offset: 0x00264258
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		public static GridEnumerableBase GetResolvedDataSource(GridTableView owner, object dataSource, string dataMember, bool caseSensitive, bool autoGenerateColumns, GridColumnCollection presentColumns, string[] additionalField, bool retrieveAllFields, bool enableSplitHeaderText)
		{
			if (dataSource != null)
			{
				IListSource listSource = dataSource as IListSource;
				if (listSource != null)
				{
					IList list = null;
					try
					{
						list = listSource.GetList();
					}
					catch
					{
						if (dataSource is IEnumerable)
						{
							return GridDataSourceHelper.CreateGridEnumerable(owner, (IEnumerable)dataSource, caseSensitive, autoGenerateColumns, presentColumns, additionalField, retrieveAllFields, enableSplitHeaderText);
						}
					}
					if (!listSource.ContainsListCollection)
					{
						return GridDataSourceHelper.CreateGridEnumerable(owner, list, caseSensitive, autoGenerateColumns, presentColumns, additionalField, retrieveAllFields, enableSplitHeaderText);
					}
					if (list == null || !(list is ITypedList))
					{
						goto IL_EF;
					}
					ITypedList typedList = (ITypedList)list;
					PropertyDescriptorCollection itemProperties = typedList.GetItemProperties(new PropertyDescriptor[0]);
					if (itemProperties != null)
					{
						int count = itemProperties.Count;
					}
					PropertyDescriptor propertyDescriptor;
					if (dataMember == null || dataMember.Length == 0)
					{
						propertyDescriptor = itemProperties[0];
					}
					else
					{
						propertyDescriptor = itemProperties.Find(dataMember, true);
					}
					if (propertyDescriptor == null)
					{
						goto IL_EF;
					}
					object component = list[0];
					object value = propertyDescriptor.GetValue(component);
					if (value != null && value is IEnumerable)
					{
						return GridDataSourceHelper.CreateGridEnumerable(owner, (IEnumerable)value, caseSensitive, autoGenerateColumns, presentColumns, additionalField, retrieveAllFields, enableSplitHeaderText);
					}
				}
				IL_EF:
				if (dataSource is IEnumerable)
				{
					return GridDataSourceHelper.CreateGridEnumerable(owner, (IEnumerable)dataSource, caseSensitive, autoGenerateColumns, presentColumns, additionalField, retrieveAllFields, enableSplitHeaderText);
				}
			}
			return GridEnumerableBase.Null;
		}
	}
}
