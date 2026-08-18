using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Telerik.Web.UI
{
	// Token: 0x020001EC RID: 492
	internal class DataFormDataSourceHelper
	{
		// Token: 0x06001160 RID: 4448 RVA: 0x0003F21C File Offset: 0x0003D41C
		public virtual DataFormEnumerableBase GetResolvedDataSource(RadDataForm ownerDataForm, object dataSource, string dataMember)
		{
			if (dataSource == null)
			{
				return DataFormEnumerableBase.Null;
			}
			IEnumerable enumerableFromSource = this.GetEnumerableFromSource(dataSource, dataMember);
			if (enumerableFromSource != null)
			{
				return new DataFormPagableEnumerable(ownerDataForm, enumerableFromSource);
			}
			return DataFormEnumerableBase.Null;
		}

		// Token: 0x06001161 RID: 4449 RVA: 0x0003F24C File Offset: 0x0003D44C
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		public virtual IEnumerable GetEnumerableFromSource(object dataSource, string dataMember)
		{
			if (dataSource is IQueryable)
			{
				return (IEnumerable)dataSource;
			}
			IListSource listSource = dataSource as IListSource;
			IEnumerable enumerable = dataSource as IEnumerable;
			if (listSource != null)
			{
				IList list = listSource.GetList();
				if (!listSource.ContainsListCollection)
				{
					return list;
				}
				ITypedList typedList = list as ITypedList;
				if (typedList != null)
				{
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
							IEnumerable result = value as IEnumerable;
							if (value != null)
							{
								return result;
							}
						}
					}
				}
			}
			else if (enumerable != null)
			{
				return enumerable;
			}
			return null;
		}

		// Token: 0x06001162 RID: 4450 RVA: 0x0003F2FD File Offset: 0x0003D4FD
		public virtual IEnumerable GetEnumerableFromSource(object dataSource)
		{
			return this.GetEnumerableFromSource(dataSource, string.Empty);
		}
	}
}
