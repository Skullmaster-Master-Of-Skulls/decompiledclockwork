using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace System.Windows.Forms.PropertyGridInternal
{
	// Token: 0x0200050F RID: 1295
	internal class MergePropertyDescriptor : PropertyDescriptor
	{
		// Token: 0x060054D1 RID: 21713 RVA: 0x001636FC File Offset: 0x001618FC
		public MergePropertyDescriptor(PropertyDescriptor[] descriptors) : base(descriptors[0].Name, null)
		{
			this.descriptors = descriptors;
		}

		// Token: 0x17001455 RID: 5205
		// (get) Token: 0x060054D2 RID: 21714 RVA: 0x00163714 File Offset: 0x00161914
		public override Type ComponentType
		{
			get
			{
				return this.descriptors[0].ComponentType;
			}
		}

		// Token: 0x17001456 RID: 5206
		// (get) Token: 0x060054D3 RID: 21715 RVA: 0x00163723 File Offset: 0x00161923
		public override TypeConverter Converter
		{
			get
			{
				return this.descriptors[0].Converter;
			}
		}

		// Token: 0x17001457 RID: 5207
		// (get) Token: 0x060054D4 RID: 21716 RVA: 0x00163732 File Offset: 0x00161932
		public override string DisplayName
		{
			get
			{
				return this.descriptors[0].DisplayName;
			}
		}

		// Token: 0x17001458 RID: 5208
		// (get) Token: 0x060054D5 RID: 21717 RVA: 0x00163744 File Offset: 0x00161944
		public override bool IsLocalizable
		{
			get
			{
				if (this.localizable == MergePropertyDescriptor.TriState.Unknown)
				{
					this.localizable = MergePropertyDescriptor.TriState.Yes;
					foreach (PropertyDescriptor propertyDescriptor in this.descriptors)
					{
						if (!propertyDescriptor.IsLocalizable)
						{
							this.localizable = MergePropertyDescriptor.TriState.No;
							break;
						}
					}
				}
				return this.localizable == MergePropertyDescriptor.TriState.Yes;
			}
		}

		// Token: 0x17001459 RID: 5209
		// (get) Token: 0x060054D6 RID: 21718 RVA: 0x00163794 File Offset: 0x00161994
		public override bool IsReadOnly
		{
			get
			{
				if (this.readOnly == MergePropertyDescriptor.TriState.Unknown)
				{
					this.readOnly = MergePropertyDescriptor.TriState.No;
					foreach (PropertyDescriptor propertyDescriptor in this.descriptors)
					{
						if (propertyDescriptor.IsReadOnly)
						{
							this.readOnly = MergePropertyDescriptor.TriState.Yes;
							break;
						}
					}
				}
				return this.readOnly == MergePropertyDescriptor.TriState.Yes;
			}
		}

		// Token: 0x1700145A RID: 5210
		// (get) Token: 0x060054D7 RID: 21719 RVA: 0x001637E3 File Offset: 0x001619E3
		public override Type PropertyType
		{
			get
			{
				return this.descriptors[0].PropertyType;
			}
		}

		// Token: 0x1700145B RID: 5211
		public PropertyDescriptor this[int index]
		{
			get
			{
				return this.descriptors[index];
			}
		}

		// Token: 0x060054D9 RID: 21721 RVA: 0x001637FC File Offset: 0x001619FC
		public override bool CanResetValue(object component)
		{
			if (this.canReset == MergePropertyDescriptor.TriState.Unknown)
			{
				this.canReset = MergePropertyDescriptor.TriState.Yes;
				Array a = (Array)component;
				for (int i = 0; i < this.descriptors.Length; i++)
				{
					if (!this.descriptors[i].CanResetValue(this.GetPropertyOwnerForComponent(a, i)))
					{
						this.canReset = MergePropertyDescriptor.TriState.No;
						break;
					}
				}
			}
			return this.canReset == MergePropertyDescriptor.TriState.Yes;
		}

		// Token: 0x060054DA RID: 21722 RVA: 0x0016385C File Offset: 0x00161A5C
		private object CopyValue(object value)
		{
			if (value == null)
			{
				return value;
			}
			Type type = value.GetType();
			if (type.IsValueType)
			{
				return value;
			}
			object obj = null;
			ICloneable cloneable = value as ICloneable;
			if (cloneable != null)
			{
				obj = cloneable.Clone();
			}
			if (obj == null)
			{
				TypeConverter converter = TypeDescriptor.GetConverter(value);
				if (converter.CanConvertTo(typeof(InstanceDescriptor)))
				{
					InstanceDescriptor instanceDescriptor = (InstanceDescriptor)converter.ConvertTo(null, CultureInfo.InvariantCulture, value, typeof(InstanceDescriptor));
					if (instanceDescriptor != null && instanceDescriptor.IsComplete)
					{
						obj = instanceDescriptor.Invoke();
					}
				}
				if (obj == null && converter.CanConvertTo(typeof(string)) && converter.CanConvertFrom(typeof(string)))
				{
					object obj2 = converter.ConvertToInvariantString(value);
					obj = converter.ConvertFromInvariantString((string)obj2);
				}
			}
			if (obj == null && type.IsSerializable)
			{
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				MemoryStream memoryStream = new MemoryStream();
				binaryFormatter.Serialize(memoryStream, value);
				memoryStream.Position = 0L;
				obj = binaryFormatter.Deserialize(memoryStream);
			}
			if (obj != null)
			{
				return obj;
			}
			return value;
		}

		// Token: 0x060054DB RID: 21723 RVA: 0x0016395E File Offset: 0x00161B5E
		protected override AttributeCollection CreateAttributeCollection()
		{
			return new MergePropertyDescriptor.MergedAttributeCollection(this);
		}

		// Token: 0x060054DC RID: 21724 RVA: 0x00163968 File Offset: 0x00161B68
		private object GetPropertyOwnerForComponent(Array a, int i)
		{
			object obj = a.GetValue(i);
			if (obj is ICustomTypeDescriptor)
			{
				obj = ((ICustomTypeDescriptor)obj).GetPropertyOwner(this.descriptors[i]);
			}
			return obj;
		}

		// Token: 0x060054DD RID: 21725 RVA: 0x0016399A File Offset: 0x00161B9A
		public override object GetEditor(Type editorBaseType)
		{
			return this.descriptors[0].GetEditor(editorBaseType);
		}

		// Token: 0x060054DE RID: 21726 RVA: 0x001639AC File Offset: 0x00161BAC
		public override object GetValue(object component)
		{
			bool flag;
			return this.GetValue((Array)component, out flag);
		}

		// Token: 0x060054DF RID: 21727 RVA: 0x001639C8 File Offset: 0x00161BC8
		public object GetValue(Array components, out bool allEqual)
		{
			allEqual = true;
			object value = this.descriptors[0].GetValue(this.GetPropertyOwnerForComponent(components, 0));
			if (value is ICollection)
			{
				if (this.collection == null)
				{
					this.collection = new MergePropertyDescriptor.MultiMergeCollection((ICollection)value);
				}
				else
				{
					if (this.collection.Locked)
					{
						return this.collection;
					}
					this.collection.SetItems((ICollection)value);
				}
			}
			for (int i = 1; i < this.descriptors.Length; i++)
			{
				object value2 = this.descriptors[i].GetValue(this.GetPropertyOwnerForComponent(components, i));
				if (this.collection != null)
				{
					if (!this.collection.MergeCollection((ICollection)value2))
					{
						allEqual = false;
						return null;
					}
				}
				else if ((value != null || value2 != null) && (value == null || !value.Equals(value2)))
				{
					allEqual = false;
					return null;
				}
			}
			if (allEqual && this.collection != null && this.collection.Count == 0)
			{
				return null;
			}
			if (this.collection == null)
			{
				return value;
			}
			return this.collection;
		}

		// Token: 0x060054E0 RID: 21728 RVA: 0x00163AC4 File Offset: 0x00161CC4
		internal object[] GetValues(Array components)
		{
			object[] array = new object[components.Length];
			for (int i = 0; i < components.Length; i++)
			{
				array[i] = this.descriptors[i].GetValue(this.GetPropertyOwnerForComponent(components, i));
			}
			return array;
		}

		// Token: 0x060054E1 RID: 21729 RVA: 0x00163B08 File Offset: 0x00161D08
		public override void ResetValue(object component)
		{
			Array a = (Array)component;
			for (int i = 0; i < this.descriptors.Length; i++)
			{
				this.descriptors[i].ResetValue(this.GetPropertyOwnerForComponent(a, i));
			}
		}

		// Token: 0x060054E2 RID: 21730 RVA: 0x00163B44 File Offset: 0x00161D44
		private void SetCollectionValues(Array a, IList listValue)
		{
			try
			{
				if (this.collection != null)
				{
					this.collection.Locked = true;
				}
				object[] array = new object[listValue.Count];
				listValue.CopyTo(array, 0);
				for (int i = 0; i < this.descriptors.Length; i++)
				{
					IList list = this.descriptors[i].GetValue(this.GetPropertyOwnerForComponent(a, i)) as IList;
					if (list != null)
					{
						list.Clear();
						foreach (object value in array)
						{
							list.Add(value);
						}
					}
				}
			}
			finally
			{
				if (this.collection != null)
				{
					this.collection.Locked = false;
				}
			}
		}

		// Token: 0x060054E3 RID: 21731 RVA: 0x00163BFC File Offset: 0x00161DFC
		public override void SetValue(object component, object value)
		{
			Array a = (Array)component;
			if (value is IList && typeof(IList).IsAssignableFrom(this.PropertyType))
			{
				this.SetCollectionValues(a, (IList)value);
				return;
			}
			for (int i = 0; i < this.descriptors.Length; i++)
			{
				object value2 = this.CopyValue(value);
				this.descriptors[i].SetValue(this.GetPropertyOwnerForComponent(a, i), value2);
			}
		}

		// Token: 0x060054E4 RID: 21732 RVA: 0x00163C70 File Offset: 0x00161E70
		public override bool ShouldSerializeValue(object component)
		{
			Array a = (Array)component;
			for (int i = 0; i < this.descriptors.Length; i++)
			{
				if (!this.descriptors[i].ShouldSerializeValue(this.GetPropertyOwnerForComponent(a, i)))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0400372A RID: 14122
		private PropertyDescriptor[] descriptors;

		// Token: 0x0400372B RID: 14123
		private MergePropertyDescriptor.TriState localizable;

		// Token: 0x0400372C RID: 14124
		private MergePropertyDescriptor.TriState readOnly;

		// Token: 0x0400372D RID: 14125
		private MergePropertyDescriptor.TriState canReset;

		// Token: 0x0400372E RID: 14126
		private MergePropertyDescriptor.MultiMergeCollection collection;

		// Token: 0x02000890 RID: 2192
		private enum TriState
		{
			// Token: 0x040044C5 RID: 17605
			Unknown,
			// Token: 0x040044C6 RID: 17606
			Yes,
			// Token: 0x040044C7 RID: 17607
			No
		}

		// Token: 0x02000891 RID: 2193
		private class MultiMergeCollection : ICollection, IEnumerable
		{
			// Token: 0x0600723A RID: 29242 RVA: 0x001A2FFC File Offset: 0x001A11FC
			public MultiMergeCollection(ICollection original)
			{
				this.SetItems(original);
			}

			// Token: 0x1700190E RID: 6414
			// (get) Token: 0x0600723B RID: 29243 RVA: 0x001A300B File Offset: 0x001A120B
			public int Count
			{
				get
				{
					if (this.items != null)
					{
						return this.items.Length;
					}
					return 0;
				}
			}

			// Token: 0x1700190F RID: 6415
			// (get) Token: 0x0600723C RID: 29244 RVA: 0x001A301F File Offset: 0x001A121F
			// (set) Token: 0x0600723D RID: 29245 RVA: 0x001A3027 File Offset: 0x001A1227
			public bool Locked
			{
				get
				{
					return this.locked;
				}
				set
				{
					this.locked = value;
				}
			}

			// Token: 0x17001910 RID: 6416
			// (get) Token: 0x0600723E RID: 29246 RVA: 0x00006C59 File Offset: 0x00004E59
			object ICollection.SyncRoot
			{
				get
				{
					return this;
				}
			}

			// Token: 0x17001911 RID: 6417
			// (get) Token: 0x0600723F RID: 29247 RVA: 0x00011A20 File Offset: 0x0000FC20
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06007240 RID: 29248 RVA: 0x001A3030 File Offset: 0x001A1230
			public void CopyTo(Array array, int index)
			{
				if (this.items == null)
				{
					return;
				}
				Array.Copy(this.items, 0, array, index, this.items.Length);
			}

			// Token: 0x06007241 RID: 29249 RVA: 0x001A3051 File Offset: 0x001A1251
			public IEnumerator GetEnumerator()
			{
				if (this.items != null)
				{
					return this.items.GetEnumerator();
				}
				return new object[0].GetEnumerator();
			}

			// Token: 0x06007242 RID: 29250 RVA: 0x001A3074 File Offset: 0x001A1274
			public bool MergeCollection(ICollection newCollection)
			{
				if (this.locked)
				{
					return true;
				}
				if (this.items.Length != newCollection.Count)
				{
					this.items = new object[0];
					return false;
				}
				object[] array = new object[newCollection.Count];
				newCollection.CopyTo(array, 0);
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i] == null != (this.items[i] == null) || (this.items[i] != null && !this.items[i].Equals(array[i])))
					{
						this.items = new object[0];
						return false;
					}
				}
				return true;
			}

			// Token: 0x06007243 RID: 29251 RVA: 0x001A3109 File Offset: 0x001A1309
			public void SetItems(ICollection collection)
			{
				if (this.locked)
				{
					return;
				}
				this.items = new object[collection.Count];
				collection.CopyTo(this.items, 0);
			}

			// Token: 0x040044C8 RID: 17608
			private object[] items;

			// Token: 0x040044C9 RID: 17609
			private bool locked;
		}

		// Token: 0x02000892 RID: 2194
		private class MergedAttributeCollection : AttributeCollection
		{
			// Token: 0x06007244 RID: 29252 RVA: 0x001A3132 File Offset: 0x001A1332
			public MergedAttributeCollection(MergePropertyDescriptor owner) : base(null)
			{
				this.owner = owner;
			}

			// Token: 0x17001912 RID: 6418
			public override Attribute this[Type attributeType]
			{
				get
				{
					return this.GetCommonAttribute(attributeType);
				}
			}

			// Token: 0x06007246 RID: 29254 RVA: 0x001A314C File Offset: 0x001A134C
			private Attribute GetCommonAttribute(Type attributeType)
			{
				if (this.attributeCollections == null)
				{
					this.attributeCollections = new AttributeCollection[this.owner.descriptors.Length];
					for (int i = 0; i < this.owner.descriptors.Length; i++)
					{
						this.attributeCollections[i] = this.owner.descriptors[i].Attributes;
					}
				}
				if (this.attributeCollections.Length == 0)
				{
					return base.GetDefaultAttribute(attributeType);
				}
				Attribute attribute;
				if (this.foundAttributes != null)
				{
					attribute = (this.foundAttributes[attributeType] as Attribute);
					if (attribute != null)
					{
						return attribute;
					}
				}
				attribute = this.attributeCollections[0][attributeType];
				if (attribute == null)
				{
					return null;
				}
				for (int j = 1; j < this.attributeCollections.Length; j++)
				{
					Attribute obj = this.attributeCollections[j][attributeType];
					if (!attribute.Equals(obj))
					{
						attribute = base.GetDefaultAttribute(attributeType);
						break;
					}
				}
				if (this.foundAttributes == null)
				{
					this.foundAttributes = new Hashtable();
				}
				this.foundAttributes[attributeType] = attribute;
				return attribute;
			}

			// Token: 0x040044CA RID: 17610
			private MergePropertyDescriptor owner;

			// Token: 0x040044CB RID: 17611
			private AttributeCollection[] attributeCollections;

			// Token: 0x040044CC RID: 17612
			private IDictionary foundAttributes;
		}
	}
}
