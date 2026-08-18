using System;
using System.Collections;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02001112 RID: 4370
	public class GridInsertionObject : ICustomTypeDescriptor
	{
		// Token: 0x0600B2EA RID: 45802 RVA: 0x0026E59C File Offset: 0x0026C79C
		public GridInsertionObject(ArrayList properties)
		{
			this._properties = new PropertyDescriptorCollection(new PropertyDescriptor[0]);
			this._propertyValues = new Hashtable();
			this.AddPropertyDescriptors(properties);
		}

		// Token: 0x0600B2EB RID: 45803 RVA: 0x0026E5C7 File Offset: 0x0026C7C7
		AttributeCollection ICustomTypeDescriptor.GetAttributes()
		{
			throw new Exception("The method or operation is not implemented.");
		}

		// Token: 0x0600B2EC RID: 45804 RVA: 0x0026E5D3 File Offset: 0x0026C7D3
		string ICustomTypeDescriptor.GetClassName()
		{
			throw new Exception("The method or operation is not implemented.");
		}

		// Token: 0x0600B2ED RID: 45805 RVA: 0x0026E5DF File Offset: 0x0026C7DF
		string ICustomTypeDescriptor.GetComponentName()
		{
			throw new Exception("The method or operation is not implemented.");
		}

		// Token: 0x0600B2EE RID: 45806 RVA: 0x0026E5EB File Offset: 0x0026C7EB
		TypeConverter ICustomTypeDescriptor.GetConverter()
		{
			throw new Exception("The method or operation is not implemented.");
		}

		// Token: 0x0600B2EF RID: 45807 RVA: 0x0026E5F7 File Offset: 0x0026C7F7
		EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
		{
			throw new Exception("The method or operation is not implemented.");
		}

		// Token: 0x0600B2F0 RID: 45808 RVA: 0x0026E603 File Offset: 0x0026C803
		PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
		{
			throw new Exception("The method or operation is not implemented.");
		}

		// Token: 0x0600B2F1 RID: 45809 RVA: 0x0026E60F File Offset: 0x0026C80F
		object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		// Token: 0x0600B2F2 RID: 45810 RVA: 0x0026E61B File Offset: 0x0026C81B
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		// Token: 0x0600B2F3 RID: 45811 RVA: 0x0026E627 File Offset: 0x0026C827
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
		{
			throw new Exception("The method or operation is not implemented.");
		}

		// Token: 0x0600B2F4 RID: 45812 RVA: 0x0026E633 File Offset: 0x0026C833
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
		{
			if (attributes.Length == 0)
			{
				return this._properties;
			}
			return new PropertyDescriptorCollection(new PropertyDescriptor[0]);
		}

		// Token: 0x0600B2F5 RID: 45813 RVA: 0x0026E64C File Offset: 0x0026C84C
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
		{
			return this._properties;
		}

		// Token: 0x0600B2F6 RID: 45814 RVA: 0x0026E654 File Offset: 0x0026C854
		object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		// Token: 0x0600B2F7 RID: 45815 RVA: 0x0026E660 File Offset: 0x0026C860
		public void SetupValues(IDictionary values)
		{
			if (values == null)
			{
				return;
			}
			this._propertyValues = values;
			ArrayList newProperties = this.PropertyDescriptorsFromDictionary(values);
			this.AddPropertyDescriptors(newProperties);
		}

		// Token: 0x0600B2F8 RID: 45816 RVA: 0x0026E688 File Offset: 0x0026C888
		private void AddPropertyDescriptors(ArrayList newProperties)
		{
			PropertyDescriptor[] array = new PropertyDescriptor[this._properties.Count + newProperties.Count];
			this._properties.CopyTo(array, 0);
			newProperties.CopyTo(array, this._properties.Count);
			foreach (object obj in newProperties)
			{
				GridPropertyDescriptor gridPropertyDescriptor = (GridPropertyDescriptor)obj;
				gridPropertyDescriptor.Initialize(this);
			}
			this._properties = new PropertyDescriptorCollection(array);
		}

		// Token: 0x0600B2F9 RID: 45817 RVA: 0x0026E720 File Offset: 0x0026C920
		private ArrayList PropertyDescriptorsFromDictionary(IDictionary values)
		{
			ArrayList arrayList = new ArrayList();
			foreach (object obj in values)
			{
				string text = ((DictionaryEntry)obj).Key.ToString();
				if (this._properties.Find(text, false) == null)
				{
					GridPropertyDescriptor value = new GridPropertyDescriptor(text, true, typeof(object));
					arrayList.Add(value);
				}
			}
			return arrayList;
		}

		// Token: 0x0600B2FA RID: 45818 RVA: 0x0026E7B4 File Offset: 0x0026C9B4
		public object GetPropertyValue(string name)
		{
			object obj = this._propertyValues[name];
			if (this._properties[name].PropertyType.Name == "Boolean" && obj == null)
			{
				obj = false;
			}
			if (obj == null)
			{
				obj = DBNull.Value;
			}
			return obj;
		}

		// Token: 0x04002F22 RID: 12066
		private IDictionary _propertyValues;

		// Token: 0x04002F23 RID: 12067
		private PropertyDescriptorCollection _properties;
	}
}
