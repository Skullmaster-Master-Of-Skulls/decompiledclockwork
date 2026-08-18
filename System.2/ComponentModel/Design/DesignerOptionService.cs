using System;
using System.Collections;
using System.Globalization;
using System.Security.Permissions;

namespace System.ComponentModel.Design
{
	// Token: 0x020005D4 RID: 1492
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public abstract class DesignerOptionService : IDesignerOptionService
	{
		// Token: 0x17000D5F RID: 3423
		// (get) Token: 0x06003789 RID: 14217 RVA: 0x000F0879 File Offset: 0x000EEA79
		public DesignerOptionService.DesignerOptionCollection Options
		{
			get
			{
				if (this._options == null)
				{
					this._options = new DesignerOptionService.DesignerOptionCollection(this, null, string.Empty, null);
				}
				return this._options;
			}
		}

		// Token: 0x0600378A RID: 14218 RVA: 0x000F089C File Offset: 0x000EEA9C
		protected DesignerOptionService.DesignerOptionCollection CreateOptionCollection(DesignerOptionService.DesignerOptionCollection parent, string name, object value)
		{
			if (parent == null)
			{
				throw new ArgumentNullException("parent");
			}
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException(SR.GetString("InvalidArgument", new object[]
				{
					name.Length.ToString(CultureInfo.CurrentCulture),
					0.ToString(CultureInfo.CurrentCulture)
				}), "name.Length");
			}
			return new DesignerOptionService.DesignerOptionCollection(this, parent, name, value);
		}

		// Token: 0x0600378B RID: 14219 RVA: 0x000F0918 File Offset: 0x000EEB18
		private PropertyDescriptor GetOptionProperty(string pageName, string valueName)
		{
			if (pageName == null)
			{
				throw new ArgumentNullException("pageName");
			}
			if (valueName == null)
			{
				throw new ArgumentNullException("valueName");
			}
			string[] array = pageName.Split(new char[]
			{
				'\\'
			});
			DesignerOptionService.DesignerOptionCollection designerOptionCollection = this.Options;
			foreach (string name in array)
			{
				designerOptionCollection = designerOptionCollection[name];
				if (designerOptionCollection == null)
				{
					return null;
				}
			}
			return designerOptionCollection.Properties[valueName];
		}

		// Token: 0x0600378C RID: 14220 RVA: 0x000F0989 File Offset: 0x000EEB89
		protected virtual void PopulateOptionCollection(DesignerOptionService.DesignerOptionCollection options)
		{
		}

		// Token: 0x0600378D RID: 14221 RVA: 0x000F098B File Offset: 0x000EEB8B
		protected virtual bool ShowDialog(DesignerOptionService.DesignerOptionCollection options, object optionObject)
		{
			return false;
		}

		// Token: 0x0600378E RID: 14222 RVA: 0x000F0990 File Offset: 0x000EEB90
		object IDesignerOptionService.GetOptionValue(string pageName, string valueName)
		{
			PropertyDescriptor optionProperty = this.GetOptionProperty(pageName, valueName);
			if (optionProperty != null)
			{
				return optionProperty.GetValue(null);
			}
			return null;
		}

		// Token: 0x0600378F RID: 14223 RVA: 0x000F09B4 File Offset: 0x000EEBB4
		void IDesignerOptionService.SetOptionValue(string pageName, string valueName, object value)
		{
			PropertyDescriptor optionProperty = this.GetOptionProperty(pageName, valueName);
			if (optionProperty != null)
			{
				optionProperty.SetValue(null, value);
			}
		}

		// Token: 0x04002AFC RID: 11004
		private DesignerOptionService.DesignerOptionCollection _options;

		// Token: 0x020008AD RID: 2221
		[TypeConverter(typeof(DesignerOptionService.DesignerOptionConverter))]
		[Editor("", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public sealed class DesignerOptionCollection : IList, ICollection, IEnumerable
		{
			// Token: 0x06004608 RID: 17928 RVA: 0x0012491C File Offset: 0x00122B1C
			internal DesignerOptionCollection(DesignerOptionService service, DesignerOptionService.DesignerOptionCollection parent, string name, object value)
			{
				this._service = service;
				this._parent = parent;
				this._name = name;
				this._value = value;
				if (this._parent != null)
				{
					if (this._parent._children == null)
					{
						this._parent._children = new ArrayList(1);
					}
					this._parent._children.Add(this);
				}
			}

			// Token: 0x17000FD3 RID: 4051
			// (get) Token: 0x06004609 RID: 17929 RVA: 0x00124984 File Offset: 0x00122B84
			public int Count
			{
				get
				{
					this.EnsurePopulated();
					return this._children.Count;
				}
			}

			// Token: 0x17000FD4 RID: 4052
			// (get) Token: 0x0600460A RID: 17930 RVA: 0x00124997 File Offset: 0x00122B97
			public string Name
			{
				get
				{
					return this._name;
				}
			}

			// Token: 0x17000FD5 RID: 4053
			// (get) Token: 0x0600460B RID: 17931 RVA: 0x0012499F File Offset: 0x00122B9F
			public DesignerOptionService.DesignerOptionCollection Parent
			{
				get
				{
					return this._parent;
				}
			}

			// Token: 0x17000FD6 RID: 4054
			// (get) Token: 0x0600460C RID: 17932 RVA: 0x001249A8 File Offset: 0x00122BA8
			public PropertyDescriptorCollection Properties
			{
				get
				{
					if (this._properties == null)
					{
						ArrayList arrayList;
						if (this._value != null)
						{
							PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this._value);
							arrayList = new ArrayList(properties.Count);
							using (IEnumerator enumerator = properties.GetEnumerator())
							{
								while (enumerator.MoveNext())
								{
									object obj = enumerator.Current;
									PropertyDescriptor property = (PropertyDescriptor)obj;
									arrayList.Add(new DesignerOptionService.DesignerOptionCollection.WrappedPropertyDescriptor(property, this._value));
								}
								goto IL_7A;
							}
						}
						arrayList = new ArrayList(1);
						IL_7A:
						this.EnsurePopulated();
						foreach (object obj2 in this._children)
						{
							DesignerOptionService.DesignerOptionCollection designerOptionCollection = (DesignerOptionService.DesignerOptionCollection)obj2;
							arrayList.AddRange(designerOptionCollection.Properties);
						}
						PropertyDescriptor[] properties2 = (PropertyDescriptor[])arrayList.ToArray(typeof(PropertyDescriptor));
						this._properties = new PropertyDescriptorCollection(properties2, true);
					}
					return this._properties;
				}
			}

			// Token: 0x17000FD7 RID: 4055
			public DesignerOptionService.DesignerOptionCollection this[int index]
			{
				get
				{
					this.EnsurePopulated();
					if (index < 0 || index >= this._children.Count)
					{
						throw new IndexOutOfRangeException("index");
					}
					return (DesignerOptionService.DesignerOptionCollection)this._children[index];
				}
			}

			// Token: 0x17000FD8 RID: 4056
			public DesignerOptionService.DesignerOptionCollection this[string name]
			{
				get
				{
					this.EnsurePopulated();
					foreach (object obj in this._children)
					{
						DesignerOptionService.DesignerOptionCollection designerOptionCollection = (DesignerOptionService.DesignerOptionCollection)obj;
						if (string.Compare(designerOptionCollection.Name, name, true, CultureInfo.InvariantCulture) == 0)
						{
							return designerOptionCollection;
						}
					}
					return null;
				}
			}

			// Token: 0x0600460F RID: 17935 RVA: 0x00124B70 File Offset: 0x00122D70
			public void CopyTo(Array array, int index)
			{
				this.EnsurePopulated();
				this._children.CopyTo(array, index);
			}

			// Token: 0x06004610 RID: 17936 RVA: 0x00124B85 File Offset: 0x00122D85
			private void EnsurePopulated()
			{
				if (this._children == null)
				{
					this._service.PopulateOptionCollection(this);
					if (this._children == null)
					{
						this._children = new ArrayList(1);
					}
				}
			}

			// Token: 0x06004611 RID: 17937 RVA: 0x00124BAF File Offset: 0x00122DAF
			public IEnumerator GetEnumerator()
			{
				this.EnsurePopulated();
				return this._children.GetEnumerator();
			}

			// Token: 0x06004612 RID: 17938 RVA: 0x00124BC2 File Offset: 0x00122DC2
			public int IndexOf(DesignerOptionService.DesignerOptionCollection value)
			{
				this.EnsurePopulated();
				return this._children.IndexOf(value);
			}

			// Token: 0x06004613 RID: 17939 RVA: 0x00124BD8 File Offset: 0x00122DD8
			private static object RecurseFindValue(DesignerOptionService.DesignerOptionCollection options)
			{
				if (options._value != null)
				{
					return options._value;
				}
				foreach (object obj in options)
				{
					DesignerOptionService.DesignerOptionCollection options2 = (DesignerOptionService.DesignerOptionCollection)obj;
					object obj2 = DesignerOptionService.DesignerOptionCollection.RecurseFindValue(options2);
					if (obj2 != null)
					{
						return obj2;
					}
				}
				return null;
			}

			// Token: 0x06004614 RID: 17940 RVA: 0x00124C48 File Offset: 0x00122E48
			public bool ShowDialog()
			{
				object obj = DesignerOptionService.DesignerOptionCollection.RecurseFindValue(this);
				return obj != null && this._service.ShowDialog(this, obj);
			}

			// Token: 0x17000FD9 RID: 4057
			// (get) Token: 0x06004615 RID: 17941 RVA: 0x00124C6E File Offset: 0x00122E6E
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000FDA RID: 4058
			// (get) Token: 0x06004616 RID: 17942 RVA: 0x00124C71 File Offset: 0x00122E71
			object ICollection.SyncRoot
			{
				get
				{
					return this;
				}
			}

			// Token: 0x17000FDB RID: 4059
			// (get) Token: 0x06004617 RID: 17943 RVA: 0x00124C74 File Offset: 0x00122E74
			bool IList.IsFixedSize
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000FDC RID: 4060
			// (get) Token: 0x06004618 RID: 17944 RVA: 0x00124C77 File Offset: 0x00122E77
			bool IList.IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000FDD RID: 4061
			object IList.this[int index]
			{
				get
				{
					return this[index];
				}
				set
				{
					throw new NotSupportedException();
				}
			}

			// Token: 0x0600461B RID: 17947 RVA: 0x00124C8A File Offset: 0x00122E8A
			int IList.Add(object value)
			{
				throw new NotSupportedException();
			}

			// Token: 0x0600461C RID: 17948 RVA: 0x00124C91 File Offset: 0x00122E91
			void IList.Clear()
			{
				throw new NotSupportedException();
			}

			// Token: 0x0600461D RID: 17949 RVA: 0x00124C98 File Offset: 0x00122E98
			bool IList.Contains(object value)
			{
				this.EnsurePopulated();
				return this._children.Contains(value);
			}

			// Token: 0x0600461E RID: 17950 RVA: 0x00124CAC File Offset: 0x00122EAC
			int IList.IndexOf(object value)
			{
				this.EnsurePopulated();
				return this._children.IndexOf(value);
			}

			// Token: 0x0600461F RID: 17951 RVA: 0x00124CC0 File Offset: 0x00122EC0
			void IList.Insert(int index, object value)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06004620 RID: 17952 RVA: 0x00124CC7 File Offset: 0x00122EC7
			void IList.Remove(object value)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06004621 RID: 17953 RVA: 0x00124CCE File Offset: 0x00122ECE
			void IList.RemoveAt(int index)
			{
				throw new NotSupportedException();
			}

			// Token: 0x04003805 RID: 14341
			private DesignerOptionService _service;

			// Token: 0x04003806 RID: 14342
			private DesignerOptionService.DesignerOptionCollection _parent;

			// Token: 0x04003807 RID: 14343
			private string _name;

			// Token: 0x04003808 RID: 14344
			private object _value;

			// Token: 0x04003809 RID: 14345
			private ArrayList _children;

			// Token: 0x0400380A RID: 14346
			private PropertyDescriptorCollection _properties;

			// Token: 0x02000938 RID: 2360
			private sealed class WrappedPropertyDescriptor : PropertyDescriptor
			{
				// Token: 0x060046DD RID: 18141 RVA: 0x00128254 File Offset: 0x00126454
				internal WrappedPropertyDescriptor(PropertyDescriptor property, object target) : base(property.Name, null)
				{
					this.property = property;
					this.target = target;
				}

				// Token: 0x17000FF1 RID: 4081
				// (get) Token: 0x060046DE RID: 18142 RVA: 0x00128271 File Offset: 0x00126471
				public override AttributeCollection Attributes
				{
					get
					{
						return this.property.Attributes;
					}
				}

				// Token: 0x17000FF2 RID: 4082
				// (get) Token: 0x060046DF RID: 18143 RVA: 0x0012827E File Offset: 0x0012647E
				public override Type ComponentType
				{
					get
					{
						return this.property.ComponentType;
					}
				}

				// Token: 0x17000FF3 RID: 4083
				// (get) Token: 0x060046E0 RID: 18144 RVA: 0x0012828B File Offset: 0x0012648B
				public override bool IsReadOnly
				{
					get
					{
						return this.property.IsReadOnly;
					}
				}

				// Token: 0x17000FF4 RID: 4084
				// (get) Token: 0x060046E1 RID: 18145 RVA: 0x00128298 File Offset: 0x00126498
				public override Type PropertyType
				{
					get
					{
						return this.property.PropertyType;
					}
				}

				// Token: 0x060046E2 RID: 18146 RVA: 0x001282A5 File Offset: 0x001264A5
				public override bool CanResetValue(object component)
				{
					return this.property.CanResetValue(this.target);
				}

				// Token: 0x060046E3 RID: 18147 RVA: 0x001282B8 File Offset: 0x001264B8
				public override object GetValue(object component)
				{
					return this.property.GetValue(this.target);
				}

				// Token: 0x060046E4 RID: 18148 RVA: 0x001282CB File Offset: 0x001264CB
				public override void ResetValue(object component)
				{
					this.property.ResetValue(this.target);
				}

				// Token: 0x060046E5 RID: 18149 RVA: 0x001282DE File Offset: 0x001264DE
				public override void SetValue(object component, object value)
				{
					this.property.SetValue(this.target, value);
				}

				// Token: 0x060046E6 RID: 18150 RVA: 0x001282F2 File Offset: 0x001264F2
				public override bool ShouldSerializeValue(object component)
				{
					return this.property.ShouldSerializeValue(this.target);
				}

				// Token: 0x04003DF1 RID: 15857
				private object target;

				// Token: 0x04003DF2 RID: 15858
				private PropertyDescriptor property;
			}
		}

		// Token: 0x020008AE RID: 2222
		internal sealed class DesignerOptionConverter : TypeConverter
		{
			// Token: 0x06004622 RID: 17954 RVA: 0x00124CD5 File Offset: 0x00122ED5
			public override bool GetPropertiesSupported(ITypeDescriptorContext cxt)
			{
				return true;
			}

			// Token: 0x06004623 RID: 17955 RVA: 0x00124CD8 File Offset: 0x00122ED8
			public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext cxt, object value, Attribute[] attributes)
			{
				PropertyDescriptorCollection propertyDescriptorCollection = new PropertyDescriptorCollection(null);
				DesignerOptionService.DesignerOptionCollection designerOptionCollection = value as DesignerOptionService.DesignerOptionCollection;
				if (designerOptionCollection == null)
				{
					return propertyDescriptorCollection;
				}
				foreach (object obj in designerOptionCollection)
				{
					DesignerOptionService.DesignerOptionCollection option = (DesignerOptionService.DesignerOptionCollection)obj;
					propertyDescriptorCollection.Add(new DesignerOptionService.DesignerOptionConverter.OptionPropertyDescriptor(option));
				}
				foreach (object obj2 in designerOptionCollection.Properties)
				{
					PropertyDescriptor value2 = (PropertyDescriptor)obj2;
					propertyDescriptorCollection.Add(value2);
				}
				return propertyDescriptorCollection;
			}

			// Token: 0x06004624 RID: 17956 RVA: 0x00124D9C File Offset: 0x00122F9C
			public override object ConvertTo(ITypeDescriptorContext cxt, CultureInfo culture, object value, Type destinationType)
			{
				if (destinationType == typeof(string))
				{
					return SR.GetString("CollectionConverterText");
				}
				return base.ConvertTo(cxt, culture, value, destinationType);
			}

			// Token: 0x02000939 RID: 2361
			private class OptionPropertyDescriptor : PropertyDescriptor
			{
				// Token: 0x060046E7 RID: 18151 RVA: 0x00128305 File Offset: 0x00126505
				internal OptionPropertyDescriptor(DesignerOptionService.DesignerOptionCollection option) : base(option.Name, null)
				{
					this._option = option;
				}

				// Token: 0x17000FF5 RID: 4085
				// (get) Token: 0x060046E8 RID: 18152 RVA: 0x0012831B File Offset: 0x0012651B
				public override Type ComponentType
				{
					get
					{
						return this._option.GetType();
					}
				}

				// Token: 0x17000FF6 RID: 4086
				// (get) Token: 0x060046E9 RID: 18153 RVA: 0x00128328 File Offset: 0x00126528
				public override bool IsReadOnly
				{
					get
					{
						return true;
					}
				}

				// Token: 0x17000FF7 RID: 4087
				// (get) Token: 0x060046EA RID: 18154 RVA: 0x0012832B File Offset: 0x0012652B
				public override Type PropertyType
				{
					get
					{
						return this._option.GetType();
					}
				}

				// Token: 0x060046EB RID: 18155 RVA: 0x00128338 File Offset: 0x00126538
				public override bool CanResetValue(object component)
				{
					return false;
				}

				// Token: 0x060046EC RID: 18156 RVA: 0x0012833B File Offset: 0x0012653B
				public override object GetValue(object component)
				{
					return this._option;
				}

				// Token: 0x060046ED RID: 18157 RVA: 0x00128343 File Offset: 0x00126543
				public override void ResetValue(object component)
				{
				}

				// Token: 0x060046EE RID: 18158 RVA: 0x00128345 File Offset: 0x00126545
				public override void SetValue(object component, object value)
				{
				}

				// Token: 0x060046EF RID: 18159 RVA: 0x00128347 File Offset: 0x00126547
				public override bool ShouldSerializeValue(object component)
				{
					return false;
				}

				// Token: 0x04003DF3 RID: 15859
				private DesignerOptionService.DesignerOptionCollection _option;
			}
		}
	}
}
