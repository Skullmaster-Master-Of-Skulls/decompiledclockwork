using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Web.UI
{
	// Token: 0x0200020B RID: 523
	public class RadDataFormInsertionObject : ICustomTypeDescriptor
	{
		// Token: 0x06001351 RID: 4945 RVA: 0x00044497 File Offset: 0x00042697
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public RadDataFormInsertionObject(PropertyDescriptorCollection properties)
		{
			this._propertyValues = new Hashtable();
			this._properties = new PropertyDescriptorCollection(new PropertyDescriptor[0]);
			this.AddPropertyDescriptors(properties);
		}

		// Token: 0x06001352 RID: 4946 RVA: 0x000444C2 File Offset: 0x000426C2
		AttributeCollection ICustomTypeDescriptor.GetAttributes()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001353 RID: 4947 RVA: 0x000444C9 File Offset: 0x000426C9
		string ICustomTypeDescriptor.GetClassName()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001354 RID: 4948 RVA: 0x000444D0 File Offset: 0x000426D0
		string ICustomTypeDescriptor.GetComponentName()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001355 RID: 4949 RVA: 0x000444D7 File Offset: 0x000426D7
		TypeConverter ICustomTypeDescriptor.GetConverter()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001356 RID: 4950 RVA: 0x000444DE File Offset: 0x000426DE
		EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001357 RID: 4951 RVA: 0x000444E5 File Offset: 0x000426E5
		PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001358 RID: 4952 RVA: 0x000444EC File Offset: 0x000426EC
		object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001359 RID: 4953 RVA: 0x000444F3 File Offset: 0x000426F3
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600135A RID: 4954 RVA: 0x000444FA File Offset: 0x000426FA
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600135B RID: 4955 RVA: 0x00044501 File Offset: 0x00042701
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
		{
			if (attributes.Length != 0)
			{
				return new PropertyDescriptorCollection(new PropertyDescriptor[0]);
			}
			return this._properties;
		}

		// Token: 0x0600135C RID: 4956 RVA: 0x0004451A File Offset: 0x0004271A
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
		{
			return this._properties;
		}

		// Token: 0x0600135D RID: 4957 RVA: 0x00044522 File Offset: 0x00042722
		object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600135E RID: 4958 RVA: 0x0004452C File Offset: 0x0004272C
		protected virtual void AddPropertyDescriptors(IEnumerable newProperties)
		{
			foreach (object obj in newProperties)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
				RadDataFormInsertionObject.DummyPropertyDescriptor dummyPropertyDescriptor = new RadDataFormInsertionObject.DummyPropertyDescriptor(propertyDescriptor.Name, propertyDescriptor.IsReadOnly, propertyDescriptor.PropertyType);
				dummyPropertyDescriptor.Initialize(this);
				this._properties.Add(dummyPropertyDescriptor);
			}
		}

		// Token: 0x0600135F RID: 4959 RVA: 0x000445A8 File Offset: 0x000427A8
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

		// Token: 0x06001360 RID: 4960 RVA: 0x000447BC File Offset: 0x000429BC
		private IEnumerable<PropertyDescriptor> PropertyDescriptorsFromDictionary(IDictionary values)
		{
			foreach (object obj in values)
			{
				DictionaryEntry entry = (DictionaryEntry)obj;
				DictionaryEntry dictionaryEntry = entry;
				string propertyName = dictionaryEntry.Key.ToString();
				if (this._properties.Find(propertyName, false) == null)
				{
					yield return new RadDataFormInsertionObject.DummyPropertyDescriptor(propertyName, true, typeof(object));
				}
			}
			yield break;
		}

		// Token: 0x06001361 RID: 4961 RVA: 0x000447E0 File Offset: 0x000429E0
		private object GetPropertyValue(string propertyName)
		{
			if (this._propertyValues.Contains(propertyName))
			{
				return this._propertyValues[propertyName];
			}
			PropertyDescriptor propertyDescriptor = this._properties.Find(propertyName, false);
			if (propertyDescriptor != null)
			{
				return RadDataFormInsertionObject.DefaultValue(propertyDescriptor.PropertyType);
			}
			return null;
		}

		// Token: 0x06001362 RID: 4962 RVA: 0x00044826 File Offset: 0x00042A26
		private static object DefaultValue(Type type)
		{
			if (type.IsValueType)
			{
				return Activator.CreateInstance(type);
			}
			return null;
		}

		// Token: 0x04000569 RID: 1385
		private readonly PropertyDescriptorCollection _properties;

		// Token: 0x0400056A RID: 1386
		private IDictionary _propertyValues;

		// Token: 0x0200020C RID: 524
		private class DummyPropertyDescriptor : PropertyDescriptor
		{
			// Token: 0x06001363 RID: 4963 RVA: 0x00044838 File Offset: 0x00042A38
			public DummyPropertyDescriptor(string propertyName, bool readOnly, Type propertyType) : base(propertyName, null)
			{
				this._isReadOnly = readOnly;
				this._dataType = propertyType;
			}

			// Token: 0x06001364 RID: 4964 RVA: 0x00044850 File Offset: 0x00042A50
			public void Initialize(RadDataFormInsertionObject owner)
			{
				this._owner = owner;
			}

			// Token: 0x17000659 RID: 1625
			// (get) Token: 0x06001365 RID: 4965 RVA: 0x00044859 File Offset: 0x00042A59
			public override Type ComponentType
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x1700065A RID: 1626
			// (get) Token: 0x06001366 RID: 4966 RVA: 0x00044860 File Offset: 0x00042A60
			public override bool IsReadOnly
			{
				get
				{
					return this._isReadOnly;
				}
			}

			// Token: 0x1700065B RID: 1627
			// (get) Token: 0x06001367 RID: 4967 RVA: 0x00044868 File Offset: 0x00042A68
			public override Type PropertyType
			{
				get
				{
					return this._dataType;
				}
			}

			// Token: 0x06001368 RID: 4968 RVA: 0x00044870 File Offset: 0x00042A70
			public override bool CanResetValue(object component)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06001369 RID: 4969 RVA: 0x00044877 File Offset: 0x00042A77
			public override object GetValue(object component)
			{
				if (this._owner != null)
				{
					return this._owner.GetPropertyValue(this.Name);
				}
				return DBNull.Value;
			}

			// Token: 0x0600136A RID: 4970 RVA: 0x00044898 File Offset: 0x00042A98
			public override void ResetValue(object component)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600136B RID: 4971 RVA: 0x0004489F File Offset: 0x00042A9F
			public override void SetValue(object component, object value)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600136C RID: 4972 RVA: 0x000448A6 File Offset: 0x00042AA6
			public override bool ShouldSerializeValue(object component)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0400056B RID: 1387
			private readonly Type _dataType;

			// Token: 0x0400056C RID: 1388
			private RadDataFormInsertionObject _owner;

			// Token: 0x0400056D RID: 1389
			private bool _isReadOnly;
		}
	}
}
