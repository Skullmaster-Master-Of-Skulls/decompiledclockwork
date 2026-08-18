using System;
using System.Collections;
using System.ComponentModel;
using System.Data.Common;
using System.Data.Metadata.Edm;
using System.Reflection;

namespace System.Data.Objects
{
	// Token: 0x02000155 RID: 341
	internal sealed class DataRecordObjectView : ObjectView<DbDataRecord>, ITypedList
	{
		// Token: 0x0600195F RID: 6495 RVA: 0x00058ED8 File Offset: 0x000570D8
		internal DataRecordObjectView(IObjectViewData<DbDataRecord> viewData, object eventDataSource, RowType rowType, Type propertyComponentType) : base(viewData, eventDataSource)
		{
			if (!typeof(IDataRecord).IsAssignableFrom(propertyComponentType))
			{
				propertyComponentType = typeof(IDataRecord);
			}
			this._rowType = rowType;
			this._propertyDescriptorsCache = MaterializedDataRecord.CreatePropertyDescriptorCollection(this._rowType, propertyComponentType, true);
		}

		// Token: 0x06001960 RID: 6496 RVA: 0x00058F28 File Offset: 0x00057128
		private static PropertyInfo GetTypedIndexer(Type type)
		{
			PropertyInfo propertyInfo = null;
			if (typeof(IList).IsAssignableFrom(type) || typeof(ITypedList).IsAssignableFrom(type) || typeof(IListSource).IsAssignableFrom(type))
			{
				PropertyInfo[] properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
				for (int i = 0; i < properties.Length; i++)
				{
					if (properties[i].GetIndexParameters().Length != 0 && properties[i].PropertyType != typeof(object))
					{
						propertyInfo = properties[i];
						if (propertyInfo.Name == "Item")
						{
							break;
						}
					}
				}
			}
			return propertyInfo;
		}

		// Token: 0x06001961 RID: 6497 RVA: 0x00058FC0 File Offset: 0x000571C0
		private static Type GetListItemType(Type type)
		{
			Type result;
			if (typeof(Array).IsAssignableFrom(type))
			{
				result = type.GetElementType();
			}
			else
			{
				PropertyInfo typedIndexer = DataRecordObjectView.GetTypedIndexer(type);
				if (typedIndexer != null)
				{
					result = typedIndexer.PropertyType;
				}
				else
				{
					result = type;
				}
			}
			return result;
		}

		// Token: 0x06001962 RID: 6498 RVA: 0x00059004 File Offset: 0x00057204
		PropertyDescriptorCollection ITypedList.GetItemProperties(PropertyDescriptor[] listAccessors)
		{
			PropertyDescriptorCollection result;
			if (listAccessors == null || listAccessors.Length == 0)
			{
				result = this._propertyDescriptorsCache;
			}
			else
			{
				PropertyDescriptor propertyDescriptor = listAccessors[listAccessors.Length - 1];
				FieldDescriptor fieldDescriptor = propertyDescriptor as FieldDescriptor;
				if (fieldDescriptor != null && fieldDescriptor.EdmProperty != null && fieldDescriptor.EdmProperty.TypeUsage.EdmType.BuiltInTypeKind == BuiltInTypeKind.RowType)
				{
					result = MaterializedDataRecord.CreatePropertyDescriptorCollection((RowType)fieldDescriptor.EdmProperty.TypeUsage.EdmType, typeof(IDataRecord), true);
				}
				else
				{
					result = TypeDescriptor.GetProperties(DataRecordObjectView.GetListItemType(propertyDescriptor.PropertyType));
				}
			}
			return result;
		}

		// Token: 0x06001963 RID: 6499 RVA: 0x0005908E File Offset: 0x0005728E
		string ITypedList.GetListName(PropertyDescriptor[] listAccessors)
		{
			return this._rowType.Name;
		}

		// Token: 0x04000AE6 RID: 2790
		private PropertyDescriptorCollection _propertyDescriptorsCache;

		// Token: 0x04000AE7 RID: 2791
		private RowType _rowType;
	}
}
