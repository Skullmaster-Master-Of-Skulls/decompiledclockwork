using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x020010BE RID: 4286
	internal class GridHyperLinkColumnDataEvaluator
	{
		// Token: 0x0600AF11 RID: 44817 RVA: 0x0025E707 File Offset: 0x0025C907
		public GridHyperLinkColumnDataEvaluator(GridHyperLinkColumn owner)
		{
			if (owner == null)
			{
				throw new ArgumentNullException("owner");
			}
			this._column = owner;
			this._cachedPropertyDescriptors = new Dictionary<Type, PropertyDescriptorCollection>();
		}

		// Token: 0x0600AF12 RID: 44818 RVA: 0x0025E730 File Offset: 0x0025C930
		public object GetDataTextFieldValue(object dataItem)
		{
			if (dataItem == null)
			{
				return null;
			}
			PropertyDescriptor propertyDescriptor = this.GetPropertyDescriptors(dataItem).Find(this._column.DataTextField, true);
			if (propertyDescriptor != null)
			{
				return propertyDescriptor.GetValue(dataItem);
			}
			return this.ExtractPropertyValue(dataItem, this._column.DataTextField);
		}

		// Token: 0x0600AF13 RID: 44819 RVA: 0x0025E778 File Offset: 0x0025C978
		public object[] GetDataUrlFieldValues(object dataItem)
		{
			if (dataItem != null)
			{
				object[] array = new object[this._column.DataNavigateUrlFields.Length];
				PropertyDescriptorCollection propertyDescriptors = this.GetPropertyDescriptors(dataItem);
				for (int i = 0; i < this._column.DataNavigateUrlFields.Length; i++)
				{
					PropertyDescriptor propertyDescriptor = propertyDescriptors.Find(this._column.DataNavigateUrlFields[i], true);
					if (propertyDescriptor != null)
					{
						array[i] = propertyDescriptor.GetValue(dataItem);
					}
					else
					{
						array[i] = this.ExtractPropertyValue(dataItem, this._column.DataNavigateUrlFields[i]);
					}
				}
				return array;
			}
			return new object[0];
		}

		// Token: 0x0600AF14 RID: 44820 RVA: 0x0025E7FE File Offset: 0x0025C9FE
		public void ClearCache()
		{
			this._cachedPropertyDescriptors.Clear();
		}

		// Token: 0x0600AF15 RID: 44821 RVA: 0x0025E80C File Offset: 0x0025CA0C
		private PropertyDescriptorCollection GetPropertyDescriptors(object dataItem)
		{
			Type type = dataItem.GetType();
			if (this._cachedPropertyDescriptors.ContainsKey(type))
			{
				return this._cachedPropertyDescriptors[type];
			}
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(dataItem);
			this._cachedPropertyDescriptors[type] = properties;
			return properties;
		}

		// Token: 0x0600AF16 RID: 44822 RVA: 0x0025E850 File Offset: 0x0025CA50
		private object ExtractPropertyValue(object dataObject, string dataFieldName)
		{
			object result = null;
			if (!string.IsNullOrEmpty(dataFieldName))
			{
				if (dataFieldName.IndexOf(".") > -1)
				{
					try
					{
						return DataBinder.Eval(dataObject, dataFieldName);
					}
					catch
					{
						if (!GridBaseDataList.IsBindableType(dataObject.GetType()))
						{
							result = null;
						}
						return result;
					}
				}
				try
				{
					result = DataBinder.GetPropertyValue(dataObject, dataFieldName);
				}
				catch
				{
					try
					{
						result = DataBinder.Eval(dataObject, dataFieldName);
					}
					catch
					{
						if (!GridBaseDataList.IsBindableType(dataObject.GetType()))
						{
							result = null;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x04002E24 RID: 11812
		private GridHyperLinkColumn _column;

		// Token: 0x04002E25 RID: 11813
		private Dictionary<Type, PropertyDescriptorCollection> _cachedPropertyDescriptors;
	}
}
