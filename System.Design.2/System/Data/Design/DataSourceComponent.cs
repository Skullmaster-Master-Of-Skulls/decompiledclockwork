using System;
using System.Collections.Specialized;
using System.ComponentModel;

namespace System.Data.Design
{
	// Token: 0x02000222 RID: 546
	internal abstract class DataSourceComponent : Component, ICustomTypeDescriptor, IObjectWithParent, IDataSourceCollectionMember, IDataSourceRenamableObject
	{
		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x06001455 RID: 5205 RVA: 0x0007547C File Offset: 0x0007367C
		// (set) Token: 0x06001456 RID: 5206 RVA: 0x00075484 File Offset: 0x00073684
		protected internal virtual DataSourceCollectionBase CollectionParent
		{
			get
			{
				return this.collectionParent;
			}
			set
			{
				this.collectionParent = value;
			}
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x06001457 RID: 5207 RVA: 0x00003598 File Offset: 0x00001798
		protected virtual object ExternalPropertyHost
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x06001458 RID: 5208 RVA: 0x0007547C File Offset: 0x0007367C
		[Browsable(false)]
		public virtual object Parent
		{
			get
			{
				return this.collectionParent;
			}
		}

		// Token: 0x06001459 RID: 5209 RVA: 0x0007548D File Offset: 0x0007368D
		AttributeCollection ICustomTypeDescriptor.GetAttributes()
		{
			return TypeDescriptor.GetAttributes(base.GetType());
		}

		// Token: 0x0600145A RID: 5210 RVA: 0x0007549A File Offset: 0x0007369A
		string ICustomTypeDescriptor.GetClassName()
		{
			if (this is IDataSourceNamedObject)
			{
				return ((IDataSourceNamedObject)this).PublicTypeName;
			}
			return null;
		}

		// Token: 0x0600145B RID: 5211 RVA: 0x000754B4 File Offset: 0x000736B4
		string ICustomTypeDescriptor.GetComponentName()
		{
			INamedObject namedObject = this as INamedObject;
			if (namedObject == null)
			{
				return null;
			}
			return namedObject.Name;
		}

		// Token: 0x0600145C RID: 5212 RVA: 0x000754D3 File Offset: 0x000736D3
		TypeConverter ICustomTypeDescriptor.GetConverter()
		{
			return TypeDescriptor.GetConverter(base.GetType());
		}

		// Token: 0x0600145D RID: 5213 RVA: 0x000754E0 File Offset: 0x000736E0
		EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
		{
			return TypeDescriptor.GetDefaultEvent(base.GetType());
		}

		// Token: 0x0600145E RID: 5214 RVA: 0x000754ED File Offset: 0x000736ED
		PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
		{
			return TypeDescriptor.GetDefaultProperty(base.GetType());
		}

		// Token: 0x0600145F RID: 5215 RVA: 0x000754FA File Offset: 0x000736FA
		object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
		{
			return TypeDescriptor.GetEditor(base.GetType(), editorBaseType);
		}

		// Token: 0x06001460 RID: 5216 RVA: 0x00075508 File Offset: 0x00073708
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
		{
			return TypeDescriptor.GetEvents(base.GetType());
		}

		// Token: 0x06001461 RID: 5217 RVA: 0x00075515 File Offset: 0x00073715
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
		{
			return TypeDescriptor.GetEvents(base.GetType(), attributes);
		}

		// Token: 0x06001462 RID: 5218 RVA: 0x00075523 File Offset: 0x00073723
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
		{
			return this.GetProperties(null);
		}

		// Token: 0x06001463 RID: 5219 RVA: 0x0007552C File Offset: 0x0007372C
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
		{
			return this.GetProperties(attributes);
		}

		// Token: 0x06001464 RID: 5220 RVA: 0x00075535 File Offset: 0x00073735
		private PropertyDescriptorCollection GetProperties(Attribute[] attributes)
		{
			return TypeDescriptor.GetProperties(base.GetType(), attributes);
		}

		// Token: 0x06001465 RID: 5221 RVA: 0x0000CA50 File Offset: 0x0000AC50
		object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
		{
			return this;
		}

		// Token: 0x06001466 RID: 5222 RVA: 0x00075544 File Offset: 0x00073744
		protected override object GetService(Type service)
		{
			DataSourceComponent dataSourceComponent = this;
			while (dataSourceComponent != null && dataSourceComponent.Site == null)
			{
				if (dataSourceComponent.CollectionParent != null)
				{
					dataSourceComponent = dataSourceComponent.CollectionParent.CollectionHost;
				}
				else if (dataSourceComponent.Parent != null && dataSourceComponent.Parent is DataSourceComponent)
				{
					dataSourceComponent = (dataSourceComponent.Parent as DataSourceComponent);
				}
				else
				{
					dataSourceComponent = null;
				}
			}
			if (dataSourceComponent != null && dataSourceComponent.Site != null)
			{
				return dataSourceComponent.Site.GetService(service);
			}
			return null;
		}

		// Token: 0x06001467 RID: 5223 RVA: 0x000755B4 File Offset: 0x000737B4
		public virtual void SetCollection(DataSourceCollectionBase collection)
		{
			this.CollectionParent = collection;
		}

		// Token: 0x06001468 RID: 5224 RVA: 0x00003937 File Offset: 0x00001B37
		internal void SetPropertyValue(string propertyName, object value)
		{
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x06001469 RID: 5225 RVA: 0x00003598 File Offset: 0x00001798
		internal virtual StringCollection NamingPropertyNames
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x0600146A RID: 5226 RVA: 0x00003598 File Offset: 0x00001798
		[Browsable(false)]
		public virtual string GeneratorName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x04000AD6 RID: 2774
		private DataSourceCollectionBase collectionParent;
	}
}
