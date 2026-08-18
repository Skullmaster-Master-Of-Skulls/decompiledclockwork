using System;
using System.Collections;
using System.ComponentModel;

namespace System.Web.UI
{
	// Token: 0x0200027C RID: 636
	internal sealed class DataSourceHelper
	{
		// Token: 0x06001E22 RID: 7714 RVA: 0x000030B5 File Offset: 0x000012B5
		private DataSourceHelper()
		{
		}

		// Token: 0x06001E23 RID: 7715 RVA: 0x00061288 File Offset: 0x0005F488
		internal static IEnumerable GetResolvedDataSource(object dataSource, string dataMember)
		{
			if (dataSource == null)
			{
				return null;
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
					if (itemProperties != null && itemProperties.Count != 0)
					{
						PropertyDescriptor propertyDescriptor;
						if (string.IsNullOrEmpty(dataMember))
						{
							propertyDescriptor = itemProperties[0];
						}
						else
						{
							propertyDescriptor = itemProperties.Find(dataMember, true);
						}
						if (propertyDescriptor != null)
						{
							object component = list[0];
							object value = propertyDescriptor.GetValue(component);
							if (value != null && value is IEnumerable)
							{
								return (IEnumerable)value;
							}
						}
						throw new HttpException(SR.GetString("ListSource_Missing_DataMember", new object[]
						{
							dataMember
						}));
					}
					throw new HttpException(SR.GetString("ListSource_Without_DataMembers"));
				}
			}
			if (dataSource is IEnumerable)
			{
				return (IEnumerable)dataSource;
			}
			return null;
		}
	}
}
