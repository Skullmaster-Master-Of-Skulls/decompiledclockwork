using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Telerik.Web.UI
{
	// Token: 0x020019A7 RID: 6567
	internal class ListViewDataSourceHelper
	{
		// Token: 0x0600FE00 RID: 65024 RVA: 0x00390434 File Offset: 0x0038E634
		public virtual ListViewEnumerableBase GetResolvedDataSource(RadListView ownerListView, object dataSource, string dataMember)
		{
			if (dataSource == null)
			{
				return ListViewEnumerableBase.Null;
			}
			IEnumerable enumerableFromSource = this.GetEnumerableFromSource(dataSource, dataMember);
			if (enumerableFromSource != null)
			{
				return new ListViewPagableEnumerable(ownerListView, enumerableFromSource);
			}
			return ListViewEnumerableBase.Null;
		}

		// Token: 0x0600FE01 RID: 65025 RVA: 0x00390464 File Offset: 0x0038E664
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

		// Token: 0x0600FE02 RID: 65026 RVA: 0x00390515 File Offset: 0x0038E715
		public virtual IEnumerable GetEnumerableFromSource(object dataSource)
		{
			return this.GetEnumerableFromSource(dataSource, string.Empty);
		}
	}
}
