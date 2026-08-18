using System;
using System.Collections;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02001843 RID: 6211
	internal class DataSourceHelper
	{
		// Token: 0x0600F146 RID: 61766 RVA: 0x0036D75B File Offset: 0x0036B95B
		public DataSourceHelper(PropertyDescriptorCache cache)
		{
			this._propertyDescriptors = cache;
		}

		// Token: 0x0600F147 RID: 61767 RVA: 0x0036D76C File Offset: 0x0036B96C
		internal static IList CopyDataSource(IEnumerable data)
		{
			ArrayList arrayList = new ArrayList();
			foreach (object value in data)
			{
				arrayList.Add(value);
			}
			return arrayList;
		}

		// Token: 0x0600F148 RID: 61768 RVA: 0x0036D7C4 File Offset: 0x0036B9C4
		internal IList FilterRootDataItems(string dataFieldParentID, IList data)
		{
			ArrayList arrayList = new ArrayList();
			foreach (object obj in data)
			{
				if (this.IsRootDataItem(dataFieldParentID, obj))
				{
					arrayList.Add(obj);
				}
			}
			foreach (object value in arrayList)
			{
				data.Remove(value);
			}
			return arrayList;
		}

		// Token: 0x0600F149 RID: 61769 RVA: 0x0036D870 File Offset: 0x0036BA70
		internal bool IsRootDataItem(string dataFieldParentID, object dataItem)
		{
			object propertyValue = this._propertyDescriptors.GetPropertyValue(dataItem, dataFieldParentID);
			if (propertyValue == null || propertyValue is DBNull)
			{
				return true;
			}
			object objB = null;
			PropertyDescriptor propertyDescriptor = this._propertyDescriptors.GetPropertyDescriptor(dataItem, dataFieldParentID);
			if (propertyDescriptor.PropertyType.IsValueType)
			{
				objB = Activator.CreateInstance(propertyDescriptor.PropertyType);
			}
			return object.Equals(propertyValue, objB);
		}

		// Token: 0x0600F14A RID: 61770 RVA: 0x0036D8C8 File Offset: 0x0036BAC8
		internal IList FilterChildren(string dataFieldID, string dataFieldParentID, object parentDataItem, IList data)
		{
			ArrayList arrayList = new ArrayList();
			object propertyValue = this._propertyDescriptors.GetPropertyValue(parentDataItem, dataFieldID);
			foreach (object obj in data)
			{
				object propertyValue2 = this._propertyDescriptors.GetPropertyValue(obj, dataFieldParentID);
				if (object.Equals(propertyValue, propertyValue2))
				{
					arrayList.Add(obj);
				}
			}
			foreach (object value in arrayList)
			{
				data.Remove(value);
			}
			return arrayList;
		}

		// Token: 0x0600F14B RID: 61771 RVA: 0x0036D994 File Offset: 0x0036BB94
		internal IList FilterChildren(string dataFieldID, string dataFieldParentID, object id, object parentDataItem, IList data)
		{
			ArrayList arrayList = new ArrayList();
			foreach (object obj in data)
			{
				object propertyValue = this._propertyDescriptors.GetPropertyValue(obj, dataFieldParentID);
				if (object.Equals(id, propertyValue))
				{
					arrayList.Add(obj);
				}
			}
			foreach (object value in arrayList)
			{
				data.Remove(value);
			}
			return arrayList;
		}

		// Token: 0x04004569 RID: 17769
		private readonly PropertyDescriptorCache _propertyDescriptors;
	}
}
