using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02001163 RID: 4451
	public class GridPropertyEvaluator
	{
		// Token: 0x0600B580 RID: 46464 RVA: 0x0027FB44 File Offset: 0x0027DD44
		public static Pair GetDescriptorObject(object target, string dataField)
		{
			if (string.IsNullOrEmpty(dataField) || target == null)
			{
				return null;
			}
			PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(target).Find(dataField, true);
			if (propertyDescriptor != null)
			{
				return new Pair(propertyDescriptor, target);
			}
			string[] array = dataField.Split(new char[]
			{
				'.'
			});
			object obj = target;
			PropertyDescriptor propertyDescriptor2 = null;
			for (int i = 0; i < array.Length; i++)
			{
				propertyDescriptor2 = TypeDescriptor.GetProperties(obj).Find(array[i], true);
				if (propertyDescriptor2 == null)
				{
					return null;
				}
				if (i < array.Length - 1)
				{
					obj = propertyDescriptor2.GetValue(obj);
				}
				if (obj == null)
				{
					return null;
				}
			}
			return new Pair(propertyDescriptor2, obj);
		}

		// Token: 0x0600B581 RID: 46465 RVA: 0x0027FBD8 File Offset: 0x0027DDD8
		public static PropertyDescriptor GetDescriptor(object target, string dataField)
		{
			Pair descriptorObject = GridPropertyEvaluator.GetDescriptorObject(target, dataField);
			if (descriptorObject == null)
			{
				return null;
			}
			return (PropertyDescriptor)descriptorObject.First;
		}

		// Token: 0x0600B582 RID: 46466 RVA: 0x0027FC00 File Offset: 0x0027DE00
		public static object GetPropertyObject(object target, string dataField)
		{
			Pair descriptorObject = GridPropertyEvaluator.GetDescriptorObject(target, dataField);
			if (descriptorObject == null)
			{
				return null;
			}
			return descriptorObject.Second;
		}

		// Token: 0x0600B583 RID: 46467 RVA: 0x0027FC20 File Offset: 0x0027DE20
		public static object GetPropertyValue(object target, string dataField)
		{
			return GridPropertyEvaluator.GetPropertyValue(target, dataField, null);
		}

		// Token: 0x0600B584 RID: 46468 RVA: 0x0027FC2C File Offset: 0x0027DE2C
		public static object GetPropertyValue(object target, string dataField, object nullValue)
		{
			DataRowView dataRowView = target as DataRowView;
			if (dataRowView != null)
			{
				if (dataRowView.Row.Table.Columns.Contains(dataField))
				{
					return dataRowView[dataField];
				}
				return nullValue;
			}
			else
			{
				DataRow dataRow = target as DataRow;
				if (dataRow != null)
				{
					if (dataRow.Table.Columns.Contains(dataField))
					{
						return dataRow[dataField];
					}
					return nullValue;
				}
				else
				{
					Pair descriptorObject = GridPropertyEvaluator.GetDescriptorObject(target, dataField);
					if (descriptorObject == null)
					{
						return null;
					}
					object value = ((PropertyDescriptor)descriptorObject.First).GetValue(descriptorObject.Second);
					if (value != null)
					{
						return value;
					}
					return nullValue;
				}
			}
		}

		// Token: 0x0600B585 RID: 46469 RVA: 0x0027FCB8 File Offset: 0x0027DEB8
		public object GetCachedPropertyValue(object target, string dataField, object nullValue)
		{
			DataRowView dataRowView = target as DataRowView;
			if (dataRowView != null)
			{
				if (dataRowView.Row.Table.Columns.Contains(dataField))
				{
					return dataRowView[dataField];
				}
				return nullValue;
			}
			else
			{
				DataRow dataRow = target as DataRow;
				if (dataRow != null)
				{
					if (dataRow.Table.Columns.Contains(dataField))
					{
						return dataRow[dataField];
					}
					return nullValue;
				}
				else
				{
					Pair cachedDescriptorObject = this.GetCachedDescriptorObject(target, dataField);
					if (cachedDescriptorObject == null)
					{
						return null;
					}
					object value = ((PropertyDescriptor)cachedDescriptorObject.First).GetValue(cachedDescriptorObject.Second);
					if (value != null)
					{
						return value;
					}
					return nullValue;
				}
			}
		}

		// Token: 0x0600B586 RID: 46470 RVA: 0x0027FD44 File Offset: 0x0027DF44
		public Pair GetCachedDescriptorObject(object target, string dataField)
		{
			if (string.IsNullOrEmpty(dataField) || target == null)
			{
				return null;
			}
			Dictionary<string, PropertyDescriptor> dictionary = new Dictionary<string, PropertyDescriptor>();
			if (this.propertyCache.ContainsKey(target.GetType()))
			{
				dictionary = this.propertyCache[target.GetType()];
			}
			else
			{
				this.propertyCache[target.GetType()] = dictionary;
			}
			if (dictionary.ContainsKey(dataField))
			{
				return new Pair(dictionary[dataField], target);
			}
			PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(target).Find(dataField, true);
			if (propertyDescriptor != null)
			{
				dictionary[dataField] = propertyDescriptor;
				return new Pair(propertyDescriptor, target);
			}
			string[] array = dataField.Split(new char[]
			{
				'.'
			});
			object obj = target;
			PropertyDescriptor propertyDescriptor2 = null;
			for (int i = 0; i < array.Length; i++)
			{
				propertyDescriptor2 = TypeDescriptor.GetProperties(obj).Find(array[i], true);
				if (propertyDescriptor2 == null)
				{
					return null;
				}
				if (i < array.Length - 1)
				{
					obj = propertyDescriptor2.GetValue(obj);
				}
				if (obj == null)
				{
					return null;
				}
			}
			return new Pair(propertyDescriptor2, obj);
		}

		// Token: 0x04002FE8 RID: 12264
		private Dictionary<Type, Dictionary<string, PropertyDescriptor>> propertyCache = new Dictionary<Type, Dictionary<string, PropertyDescriptor>>();
	}
}
