using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Telerik.Web.UI
{
	// Token: 0x020000C8 RID: 200
	internal class ProxyBoundControlDataSourceHelper
	{
		// Token: 0x060007A8 RID: 1960 RVA: 0x0001CDBC File Offset: 0x0001AFBC
		public virtual ProxyBoundControlEnumerableBase GetResolvedDataSource(RadProxyBoundControl ownerProxyBoundControl, object dataSource, string dataMember)
		{
			if (dataSource == null)
			{
				return ProxyBoundControlEnumerableBase.Null;
			}
			IEnumerable enumerableFromSource = this.GetEnumerableFromSource(dataSource, dataMember);
			if (enumerableFromSource != null)
			{
				return new ProxyBoundControlPagableEnumerable(ownerProxyBoundControl, enumerableFromSource);
			}
			return ProxyBoundControlEnumerableBase.Null;
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x0001CDEC File Offset: 0x0001AFEC
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

		// Token: 0x060007AA RID: 1962 RVA: 0x0001CE9D File Offset: 0x0001B09D
		public virtual IEnumerable GetEnumerableFromSource(object dataSource)
		{
			return this.GetEnumerableFromSource(dataSource, string.Empty);
		}
	}
}
