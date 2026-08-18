using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Web.UI
{
	// Token: 0x02000F84 RID: 3972
	public class RadListViewInsertionObject : ICustomTypeDescriptor
	{
		// Token: 0x06009837 RID: 38967 RVA: 0x00220D17 File Offset: 0x0021EF17
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public RadListViewInsertionObject(PropertyDescriptorCollection properties)
		{
			this._propertyValues = new Hashtable();
			this._properties = new PropertyDescriptorCollection(new PropertyDescriptor[0]);
			this.AddPropertyDescriptors(properties);
		}

		// Token: 0x06009838 RID: 38968 RVA: 0x00220D42 File Offset: 0x0021EF42
		AttributeCollection ICustomTypeDescriptor.GetAttributes()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06009839 RID: 38969 RVA: 0x00220D49 File Offset: 0x0021EF49
		string ICustomTypeDescriptor.GetClassName()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600983A RID: 38970 RVA: 0x00220D50 File Offset: 0x0021EF50
		string ICustomTypeDescriptor.GetComponentName()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600983B RID: 38971 RVA: 0x00220D57 File Offset: 0x0021EF57
		TypeConverter ICustomTypeDescriptor.GetConverter()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600983C RID: 38972 RVA: 0x00220D5E File Offset: 0x0021EF5E
		EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600983D RID: 38973 RVA: 0x00220D65 File Offset: 0x0021EF65
		PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600983E RID: 38974 RVA: 0x00220D6C File Offset: 0x0021EF6C
		object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600983F RID: 38975 RVA: 0x00220D73 File Offset: 0x0021EF73
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06009840 RID: 38976 RVA: 0x00220D7A File Offset: 0x0021EF7A
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06009841 RID: 38977 RVA: 0x00220D81 File Offset: 0x0021EF81
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
		{
			if (attributes.Length != 0)
			{
				return new PropertyDescriptorCollection(new PropertyDescriptor[0]);
			}
			return this._properties;
		}

		// Token: 0x06009842 RID: 38978 RVA: 0x00220D9A File Offset: 0x0021EF9A
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
		{
			return this._properties;
		}

		// Token: 0x06009843 RID: 38979 RVA: 0x00220DA2 File Offset: 0x0021EFA2
		object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06009844 RID: 38980 RVA: 0x00220DAC File Offset: 0x0021EFAC
		protected virtual void AddPropertyDescriptors(IEnumerable newProperties)
		{
			foreach (object obj in newProperties)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
				RadListViewInsertionObject.DummyPropertyDescriptor dummyPropertyDescriptor = new RadListViewInsertionObject.DummyPropertyDescriptor(propertyDescriptor.Name, propertyDescriptor.IsReadOnly, propertyDescriptor.PropertyType);
				dummyPropertyDescriptor.Initialize(this);
				this._properties.Add(dummyPropertyDescriptor);
			}
		}

		// Token: 0x06009845 RID: 38981 RVA: 0x00220E28 File Offset: 0x0021F028
		public void SetupValues(IDictionary values)
		{
			if (values == null)
			{
				return;
			}
			this._propertyValues = values;
			IEnumerable<PropertyDescriptor> newProperties = this.PropertyDescriptorsFromDictionary(values);
			this.AddPropertyDescriptors(newProperties);
		}

		// Token: 0x06009846 RID: 38982 RVA: 0x0022103C File Offset: 0x0021F23C
		private IEnumerable<PropertyDescriptor> PropertyDescriptorsFromDictionary(IDictionary values)
		{
			foreach (object obj in values)
			{
				DictionaryEntry entry = (DictionaryEntry)obj;
				DictionaryEntry dictionaryEntry = entry;
				string propertyName = dictionaryEntry.Key.ToString();
				if (this._properties.Find(propertyName, false) == null)
				{
					yield return new RadListViewInsertionObject.DummyPropertyDescriptor(propertyName, true, typeof(object));
				}
			}
			yield break;
		}

		// Token: 0x06009847 RID: 38983 RVA: 0x00221060 File Offset: 0x0021F260
		private object GetPropertyValue(string propertyName)
		{
			if (this._propertyValues.Contains(propertyName))
			{
				return this._propertyValues[propertyName];
			}
			PropertyDescriptor propertyDescriptor = this._properties.Find(propertyName, false);
			if (propertyDescriptor != null)
			{
				return RadListViewInsertionObject.DefaultValue(propertyDescriptor.PropertyType);
			}
			return null;
		}

		// Token: 0x06009848 RID: 38984 RVA: 0x002210A6 File Offset: 0x0021F2A6
		private static object DefaultValue(Type type)
		{
			if (type.IsValueType)
			{
				return Activator.CreateInstance(type);
			}
			return null;
		}

		// Token: 0x04002B7A RID: 11130
		private readonly PropertyDescriptorCollection _properties;

		// Token: 0x04002B7B RID: 11131
		private IDictionary _propertyValues;

		// Token: 0x02000F85 RID: 3973
		private class DummyPropertyDescriptor : PropertyDescriptor
		{
			// Token: 0x06009849 RID: 38985 RVA: 0x002210B8 File Offset: 0x0021F2B8
			public DummyPropertyDescriptor(string propertyName, bool readOnly, Type propertyType) : base(propertyName, null)
			{
				this._isReadOnly = readOnly;
				this._dataType = propertyType;
			}

			// Token: 0x0600984A RID: 38986 RVA: 0x002210D0 File Offset: 0x0021F2D0
			public void Initialize(RadListViewInsertionObject owner)
			{
				this._owner = owner;
			}

			// Token: 0x1700302B RID: 12331
			// (get) Token: 0x0600984B RID: 38987 RVA: 0x002210D9 File Offset: 0x0021F2D9
			public override Type ComponentType
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x1700302C RID: 12332
			// (get) Token: 0x0600984C RID: 38988 RVA: 0x002210E0 File Offset: 0x0021F2E0
			public override bool IsReadOnly
			{
				get
				{
					return this._isReadOnly;
				}
			}

			// Token: 0x1700302D RID: 12333
			// (get) Token: 0x0600984D RID: 38989 RVA: 0x002210E8 File Offset: 0x0021F2E8
			public override Type PropertyType
			{
				get
				{
					return this._dataType;
				}
			}

			// Token: 0x0600984E RID: 38990 RVA: 0x002210F0 File Offset: 0x0021F2F0
			public override bool CanResetValue(object component)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600984F RID: 38991 RVA: 0x002210F7 File Offset: 0x0021F2F7
			public override object GetValue(object component)
			{
				if (this._owner != null)
				{
					return this._owner.GetPropertyValue(this.Name);
				}
				return DBNull.Value;
			}

			// Token: 0x06009850 RID: 38992 RVA: 0x00221118 File Offset: 0x0021F318
			public override void ResetValue(object component)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06009851 RID: 38993 RVA: 0x0022111F File Offset: 0x0021F31F
			public override void SetValue(object component, object value)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06009852 RID: 38994 RVA: 0x00221126 File Offset: 0x0021F326
			public override bool ShouldSerializeValue(object component)
			{
				throw new NotImplementedException();
			}

			// Token: 0x04002B7C RID: 11132
			private readonly Type _dataType;

			// Token: 0x04002B7D RID: 11133
			private RadListViewInsertionObject _owner;

			// Token: 0x04002B7E RID: 11134
			private bool _isReadOnly;
		}
	}
}
