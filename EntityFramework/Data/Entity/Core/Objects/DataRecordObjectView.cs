using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x0200054C RID: 1356
	internal sealed class DataRecordObjectView : ObjectView<DbDataRecord>, ITypedList
	{
		// Token: 0x060034A5 RID: 13477 RVA: 0x000F8E7C File Offset: 0x000F707C
		internal DataRecordObjectView(IObjectViewData<DbDataRecord> viewData, object eventDataSource, RowType rowType, Type propertyComponentType) : base(viewData, eventDataSource)
		{
			if (!typeof(IDataRecord).IsAssignableFrom(propertyComponentType))
			{
				propertyComponentType = typeof(IDataRecord);
			}
			this._rowType = rowType;
			this._propertyDescriptorsCache = MaterializedDataRecord.CreatePropertyDescriptorCollection(this._rowType, propertyComponentType, true);
		}

		// Token: 0x060034A6 RID: 13478 RVA: 0x000F8ED4 File Offset: 0x000F70D4
		private static PropertyInfo GetTypedIndexer(Type type)
		{
			PropertyInfo propertyInfo = null;
			if (typeof(IList).IsAssignableFrom(type) || typeof(ITypedList).IsAssignableFrom(type) || typeof(IListSource).IsAssignableFrom(type))
			{
				IEnumerable<PropertyInfo> enumerable = from p in type.GetInstanceProperties()
				where p.IsPublic()
				select p;
				foreach (PropertyInfo propertyInfo2 in enumerable)
				{
					if (propertyInfo2.GetIndexParameters().Length > 0 && propertyInfo2.PropertyType != typeof(object))
					{
						propertyInfo = propertyInfo2;
						if (propertyInfo.Name == "Item")
						{
							break;
						}
					}
				}
			}
			return propertyInfo;
		}

		// Token: 0x060034A7 RID: 13479 RVA: 0x000F8FB0 File Offset: 0x000F71B0
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

		// Token: 0x060034A8 RID: 13480 RVA: 0x000F8FF4 File Offset: 0x000F71F4
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

		// Token: 0x060034A9 RID: 13481 RVA: 0x000F907F File Offset: 0x000F727F
		string ITypedList.GetListName(PropertyDescriptor[] listAccessors)
		{
			return this._rowType.Name;
		}

		// Token: 0x040013B1 RID: 5041
		private readonly PropertyDescriptorCollection _propertyDescriptorsCache;

		// Token: 0x040013B2 RID: 5042
		private readonly RowType _rowType;
	}
}
