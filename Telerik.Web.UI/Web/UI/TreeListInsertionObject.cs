using System;
using System.Collections;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02001270 RID: 4720
	public class TreeListInsertionObject : ICustomTypeDescriptor
	{
		// Token: 0x0600C460 RID: 50272 RVA: 0x002BF33D File Offset: 0x002BD53D
		public TreeListInsertionObject(IDictionary values)
		{
			if (values == null)
			{
				return;
			}
			this._propertyValues = values;
			this._properties = new PropertyDescriptorCollection(new PropertyDescriptor[0]);
			this.AddPropertyDescriptors(this.PropertyDescriptorsFromDictionary(values));
		}

		// Token: 0x0600C461 RID: 50273 RVA: 0x002BF370 File Offset: 0x002BD570
		private void AddPropertyDescriptors(ArrayList newProperties)
		{
			PropertyDescriptor[] array = new PropertyDescriptor[this._properties.Count + newProperties.Count];
			this._properties.CopyTo(array, 0);
			newProperties.CopyTo(array, this._properties.Count);
			foreach (object obj in newProperties)
			{
				TreeListPropertyDescriptor treeListPropertyDescriptor = (TreeListPropertyDescriptor)obj;
				treeListPropertyDescriptor.Initialize(this);
			}
			this._properties = new PropertyDescriptorCollection(array);
		}

		// Token: 0x0600C462 RID: 50274 RVA: 0x002BF408 File Offset: 0x002BD608
		private ArrayList PropertyDescriptorsFromDictionary(IDictionary values)
		{
			ArrayList arrayList = new ArrayList();
			foreach (object obj in values)
			{
				string text = ((DictionaryEntry)obj).Key.ToString();
				if (this._properties.Find(text, false) == null)
				{
					TreeListPropertyDescriptor value = new TreeListPropertyDescriptor(text, true, typeof(object));
					arrayList.Add(value);
				}
			}
			return arrayList;
		}

		// Token: 0x0600C463 RID: 50275 RVA: 0x002BF49C File Offset: 0x002BD69C
		public object GetPropertyValue(string name)
		{
			object obj = this._propertyValues[name];
			if (obj == null)
			{
				obj = DBNull.Value;
			}
			return obj;
		}

		// Token: 0x0600C464 RID: 50276 RVA: 0x002BF4C0 File Offset: 0x002BD6C0
		public AttributeCollection GetAttributes()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600C465 RID: 50277 RVA: 0x002BF4C7 File Offset: 0x002BD6C7
		public string GetClassName()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600C466 RID: 50278 RVA: 0x002BF4CE File Offset: 0x002BD6CE
		public string GetComponentName()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600C467 RID: 50279 RVA: 0x002BF4D5 File Offset: 0x002BD6D5
		public TypeConverter GetConverter()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600C468 RID: 50280 RVA: 0x002BF4DC File Offset: 0x002BD6DC
		public EventDescriptor GetDefaultEvent()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600C469 RID: 50281 RVA: 0x002BF4E3 File Offset: 0x002BD6E3
		public PropertyDescriptor GetDefaultProperty()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600C46A RID: 50282 RVA: 0x002BF4EA File Offset: 0x002BD6EA
		public object GetEditor(Type editorBaseType)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600C46B RID: 50283 RVA: 0x002BF4F1 File Offset: 0x002BD6F1
		public EventDescriptorCollection GetEvents(Attribute[] attributes)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600C46C RID: 50284 RVA: 0x002BF4F8 File Offset: 0x002BD6F8
		public EventDescriptorCollection GetEvents()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600C46D RID: 50285 RVA: 0x002BF4FF File Offset: 0x002BD6FF
		public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
		{
			if (attributes.Length == 0)
			{
				return this._properties;
			}
			return new PropertyDescriptorCollection(new PropertyDescriptor[0]);
		}

		// Token: 0x0600C46E RID: 50286 RVA: 0x002BF518 File Offset: 0x002BD718
		public PropertyDescriptorCollection GetProperties()
		{
			return this._properties;
		}

		// Token: 0x0600C46F RID: 50287 RVA: 0x002BF520 File Offset: 0x002BD720
		public object GetPropertyOwner(PropertyDescriptor pd)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0400340F RID: 13327
		private IDictionary _propertyValues;

		// Token: 0x04003410 RID: 13328
		private PropertyDescriptorCollection _properties;
	}
}
