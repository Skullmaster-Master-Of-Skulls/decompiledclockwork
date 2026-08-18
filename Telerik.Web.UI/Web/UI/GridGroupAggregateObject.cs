using System;
using System.Collections;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02000B7B RID: 2939
	public class GridGroupAggregateObject : ICustomTypeDescriptor
	{
		// Token: 0x06006EFD RID: 28413 RVA: 0x0019BB23 File Offset: 0x00199D23
		public GridGroupAggregateObject(IDictionary values)
		{
			if (values == null)
			{
				return;
			}
			this._propertyValues = values;
			this._properties = new PropertyDescriptorCollection(new PropertyDescriptor[0]);
			this.AddPropertyDescriptors(this.PropertyDescriptorsFromDictionary(values));
		}

		// Token: 0x06006EFE RID: 28414 RVA: 0x0019BB54 File Offset: 0x00199D54
		private void AddPropertyDescriptors(ArrayList newProperties)
		{
			PropertyDescriptor[] array = new PropertyDescriptor[this._properties.Count + newProperties.Count];
			this._properties.CopyTo(array, 0);
			newProperties.CopyTo(array, this._properties.Count);
			foreach (object obj in newProperties)
			{
				RadGridGroupAggregatePropertyDescriptor radGridGroupAggregatePropertyDescriptor = (RadGridGroupAggregatePropertyDescriptor)obj;
				radGridGroupAggregatePropertyDescriptor.Initialize(this);
			}
			this._properties = new PropertyDescriptorCollection(array);
		}

		// Token: 0x06006EFF RID: 28415 RVA: 0x0019BBEC File Offset: 0x00199DEC
		private ArrayList PropertyDescriptorsFromDictionary(IDictionary values)
		{
			ArrayList arrayList = new ArrayList();
			foreach (object obj in values)
			{
				string text = ((DictionaryEntry)obj).Key.ToString();
				if (this._properties.Find(text, false) == null)
				{
					RadGridGroupAggregatePropertyDescriptor value = new RadGridGroupAggregatePropertyDescriptor(text, true, typeof(object));
					arrayList.Add(value);
				}
			}
			return arrayList;
		}

		// Token: 0x06006F00 RID: 28416 RVA: 0x0019BC80 File Offset: 0x00199E80
		public object GetPropertyValue(string name)
		{
			object obj = this._propertyValues[name];
			if (obj == null)
			{
				obj = DBNull.Value;
			}
			return obj;
		}

		// Token: 0x06006F01 RID: 28417 RVA: 0x0019BCA4 File Offset: 0x00199EA4
		public AttributeCollection GetAttributes()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06006F02 RID: 28418 RVA: 0x0019BCAB File Offset: 0x00199EAB
		public string GetClassName()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06006F03 RID: 28419 RVA: 0x0019BCB2 File Offset: 0x00199EB2
		public string GetComponentName()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06006F04 RID: 28420 RVA: 0x0019BCB9 File Offset: 0x00199EB9
		public TypeConverter GetConverter()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06006F05 RID: 28421 RVA: 0x0019BCC0 File Offset: 0x00199EC0
		public EventDescriptor GetDefaultEvent()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06006F06 RID: 28422 RVA: 0x0019BCC7 File Offset: 0x00199EC7
		public PropertyDescriptor GetDefaultProperty()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06006F07 RID: 28423 RVA: 0x0019BCCE File Offset: 0x00199ECE
		public object GetEditor(Type editorBaseType)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06006F08 RID: 28424 RVA: 0x0019BCD5 File Offset: 0x00199ED5
		public EventDescriptorCollection GetEvents(Attribute[] attributes)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06006F09 RID: 28425 RVA: 0x0019BCDC File Offset: 0x00199EDC
		public EventDescriptorCollection GetEvents()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06006F0A RID: 28426 RVA: 0x0019BCE3 File Offset: 0x00199EE3
		public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
		{
			if (attributes.Length == 0)
			{
				return this._properties;
			}
			return new PropertyDescriptorCollection(new PropertyDescriptor[0]);
		}

		// Token: 0x06006F0B RID: 28427 RVA: 0x0019BCFC File Offset: 0x00199EFC
		public PropertyDescriptorCollection GetProperties()
		{
			return this._properties;
		}

		// Token: 0x06006F0C RID: 28428 RVA: 0x0019BD04 File Offset: 0x00199F04
		public object GetPropertyOwner(PropertyDescriptor pd)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04001DF3 RID: 7667
		private IDictionary _propertyValues;

		// Token: 0x04001DF4 RID: 7668
		private PropertyDescriptorCollection _properties;
	}
}
