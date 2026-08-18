using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

namespace Telerik.Web.UI
{
	// Token: 0x02001845 RID: 6213
	internal class PropertyDescriptorCache
	{
		// Token: 0x0600F14C RID: 61772 RVA: 0x0036DA50 File Offset: 0x0036BC50
		public PropertyDescriptor GetPropertyDescriptor(object target, string propertyName)
		{
			Type type = target.GetType();
			Dictionary<string, PropertyDescriptor> dictionary;
			if (!this._cache.TryGetValue(type, out dictionary))
			{
				dictionary = new Dictionary<string, PropertyDescriptor>();
				this._cache[type] = dictionary;
			}
			PropertyDescriptor propertyDescriptor;
			if (!dictionary.TryGetValue(propertyName, out propertyDescriptor))
			{
				propertyDescriptor = TypeDescriptor.GetProperties(target).Find(propertyName, true);
				dictionary[propertyName] = propertyDescriptor;
			}
			return propertyDescriptor;
		}

		// Token: 0x0600F14D RID: 61773 RVA: 0x0036DAAC File Offset: 0x0036BCAC
		public object GetPropertyValue(object dataItem, string propertyName)
		{
			if (dataItem == null)
			{
				throw new NotSupportedException("Cannot databind to collection which contains null.");
			}
			DataRowView dataRowView = dataItem as DataRowView;
			if (dataRowView != null)
			{
				return dataRowView[propertyName];
			}
			PropertyDescriptor propertyDescriptor = this.GetPropertyDescriptor(dataItem, propertyName);
			if (propertyDescriptor == null)
			{
				throw new NotSupportedException(string.Concat(new object[]
				{
					"Object of type ",
					dataItem.GetType(),
					" does not have a ",
					propertyName,
					" property."
				}));
			}
			return propertyDescriptor.GetValue(dataItem);
		}

		// Token: 0x0600F14E RID: 61774 RVA: 0x0036DB24 File Offset: 0x0036BD24
		public string GetPropertyValue(object dataItem, string propertyName, string format)
		{
			object propertyValue = this.GetPropertyValue(dataItem, propertyName);
			if (propertyValue == null || propertyValue == DBNull.Value)
			{
				return string.Empty;
			}
			if (string.IsNullOrEmpty(format))
			{
				return propertyValue.ToString();
			}
			return string.Format(format, propertyValue);
		}

		// Token: 0x0400456A RID: 17770
		private readonly Dictionary<Type, Dictionary<string, PropertyDescriptor>> _cache = new Dictionary<Type, Dictionary<string, PropertyDescriptor>>();
	}
}
