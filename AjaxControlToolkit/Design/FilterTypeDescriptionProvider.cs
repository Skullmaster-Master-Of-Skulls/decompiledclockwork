using System;
using System.ComponentModel;

namespace AjaxControlToolkit.Design
{
	// Token: 0x0200008C RID: 140
	internal class FilterTypeDescriptionProvider<T> : TypeDescriptionProvider, ICustomTypeDescriptor
	{
		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000489 RID: 1161 RVA: 0x0000CAB0 File Offset: 0x0000ACB0
		// (set) Token: 0x0600048A RID: 1162 RVA: 0x0000CAB8 File Offset: 0x0000ACB8
		protected bool FilterExtendedProperties
		{
			get
			{
				return this._extended;
			}
			set
			{
				this._extended = value;
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x0600048B RID: 1163 RVA: 0x0000CAC1 File Offset: 0x0000ACC1
		protected T Target
		{
			get
			{
				return this._target;
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x0600048C RID: 1164 RVA: 0x0000CACC File Offset: 0x0000ACCC
		private ICustomTypeDescriptor BaseDescriptor
		{
			get
			{
				if (this._baseDescriptor == null)
				{
					if (this.FilterExtendedProperties)
					{
						this._baseDescriptor = this._baseProvider.GetExtendedTypeDescriptor(this.Target);
					}
					else
					{
						this._baseDescriptor = this._baseProvider.GetTypeDescriptor(this.Target);
					}
				}
				return this._baseDescriptor;
			}
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x0000CB29 File Offset: 0x0000AD29
		public FilterTypeDescriptionProvider(T target) : base(TypeDescriptor.GetProvider(target))
		{
			this._target = target;
			this._baseProvider = TypeDescriptor.GetProvider(target);
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x0000CB54 File Offset: 0x0000AD54
		public void Dispose()
		{
			this._target = default(T);
			this._baseDescriptor = null;
			this._baseProvider = null;
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x0000CB70 File Offset: 0x0000AD70
		public AttributeCollection GetAttributes()
		{
			return this.BaseDescriptor.GetAttributes();
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x0000CB7D File Offset: 0x0000AD7D
		public string GetClassName()
		{
			return this.BaseDescriptor.GetClassName();
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x0000CB8A File Offset: 0x0000AD8A
		public string GetComponentName()
		{
			return this.BaseDescriptor.GetComponentName();
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x0000CB97 File Offset: 0x0000AD97
		public TypeConverter GetConverter()
		{
			return this.BaseDescriptor.GetConverter();
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x0000CBA4 File Offset: 0x0000ADA4
		public EventDescriptor GetDefaultEvent()
		{
			return this.BaseDescriptor.GetDefaultEvent();
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x0000CBB1 File Offset: 0x0000ADB1
		public PropertyDescriptor GetDefaultProperty()
		{
			return this.BaseDescriptor.GetDefaultProperty();
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x0000CBBE File Offset: 0x0000ADBE
		public object GetEditor(Type editorBaseType)
		{
			return this.BaseDescriptor.GetEditor(editorBaseType);
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x0000CBCC File Offset: 0x0000ADCC
		public EventDescriptorCollection GetEvents(Attribute[] attributes)
		{
			return this.BaseDescriptor.GetEvents(attributes);
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x0000CBDA File Offset: 0x0000ADDA
		public EventDescriptorCollection GetEvents()
		{
			return this.BaseDescriptor.GetEvents();
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x0000CBE7 File Offset: 0x0000ADE7
		public object GetPropertyOwner(PropertyDescriptor pd)
		{
			return this.BaseDescriptor.GetPropertyOwner(pd);
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x0000CBF8 File Offset: 0x0000ADF8
		public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
		{
			PropertyDescriptorCollection properties = this.BaseDescriptor.GetProperties(attributes);
			return this.FilterProperties(properties);
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x0000CC1B File Offset: 0x0000AE1B
		public PropertyDescriptorCollection GetProperties()
		{
			return this.FilterProperties(this.BaseDescriptor.GetProperties());
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x0000CC30 File Offset: 0x0000AE30
		private PropertyDescriptorCollection FilterProperties(PropertyDescriptorCollection props)
		{
			PropertyDescriptor[] array = new PropertyDescriptor[props.Count];
			props.CopyTo(array, 0);
			bool flag = false;
			for (int i = 0; i < array.Length; i++)
			{
				PropertyDescriptor propertyDescriptor = this.ProcessProperty(array[i]);
				if (propertyDescriptor != array[i])
				{
					flag = true;
					array[i] = propertyDescriptor;
				}
			}
			if (flag)
			{
				props = new PropertyDescriptorCollection(array);
			}
			return props;
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x0000CC83 File Offset: 0x0000AE83
		protected virtual PropertyDescriptor ProcessProperty(PropertyDescriptor baseProp)
		{
			return baseProp;
		}

		// Token: 0x04000297 RID: 663
		private T _target;

		// Token: 0x04000298 RID: 664
		private TypeDescriptionProvider _baseProvider;

		// Token: 0x04000299 RID: 665
		private ICustomTypeDescriptor _baseDescriptor;

		// Token: 0x0400029A RID: 666
		private bool _extended;
	}
}
