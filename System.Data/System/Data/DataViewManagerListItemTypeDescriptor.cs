using System;
using System.ComponentModel;

namespace System.Data
{
	// Token: 0x020000AC RID: 172
	internal sealed class DataViewManagerListItemTypeDescriptor : ICustomTypeDescriptor
	{
		// Token: 0x06000BD5 RID: 3029 RVA: 0x0020EF18 File Offset: 0x0020E318
		internal DataViewManagerListItemTypeDescriptor(DataViewManager dataViewManager)
		{
			this.dataViewManager = dataViewManager;
		}

		// Token: 0x06000BD6 RID: 3030 RVA: 0x0020EF38 File Offset: 0x0020E338
		internal void Reset()
		{
			this.propsCollection = null;
		}

		// Token: 0x06000BD7 RID: 3031 RVA: 0x0020EF58 File Offset: 0x0020E358
		internal DataView GetDataView(DataTable table)
		{
			DataView dataView = new DataView(table);
			dataView.SetDataViewManager(this.dataViewManager);
			return dataView;
		}

		// Token: 0x06000BD8 RID: 3032 RVA: 0x0020EF88 File Offset: 0x0020E388
		AttributeCollection ICustomTypeDescriptor.GetAttributes()
		{
			return new AttributeCollection(null);
		}

		// Token: 0x06000BD9 RID: 3033 RVA: 0x0020EFA8 File Offset: 0x0020E3A8
		string ICustomTypeDescriptor.GetClassName()
		{
			return null;
		}

		// Token: 0x06000BDA RID: 3034 RVA: 0x0020EFB8 File Offset: 0x0020E3B8
		string ICustomTypeDescriptor.GetComponentName()
		{
			return null;
		}

		// Token: 0x06000BDB RID: 3035 RVA: 0x0020EFC8 File Offset: 0x0020E3C8
		TypeConverter ICustomTypeDescriptor.GetConverter()
		{
			return null;
		}

		// Token: 0x06000BDC RID: 3036 RVA: 0x0020EFD8 File Offset: 0x0020E3D8
		EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
		{
			return null;
		}

		// Token: 0x06000BDD RID: 3037 RVA: 0x0020EFE8 File Offset: 0x0020E3E8
		PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
		{
			return null;
		}

		// Token: 0x06000BDE RID: 3038 RVA: 0x0020EFF8 File Offset: 0x0020E3F8
		object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
		{
			return null;
		}

		// Token: 0x06000BDF RID: 3039 RVA: 0x0020F008 File Offset: 0x0020E408
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
		{
			return new EventDescriptorCollection(null);
		}

		// Token: 0x06000BE0 RID: 3040 RVA: 0x0020F028 File Offset: 0x0020E428
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
		{
			return new EventDescriptorCollection(null);
		}

		// Token: 0x06000BE1 RID: 3041 RVA: 0x0020F048 File Offset: 0x0020E448
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
		{
			return ((ICustomTypeDescriptor)this).GetProperties(null);
		}

		// Token: 0x06000BE2 RID: 3042 RVA: 0x0020F068 File Offset: 0x0020E468
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
		{
			if (this.propsCollection == null)
			{
				PropertyDescriptor[] array = null;
				DataSet dataSet = this.dataViewManager.DataSet;
				if (dataSet != null)
				{
					int count = dataSet.Tables.Count;
					array = new PropertyDescriptor[count];
					for (int i = 0; i < count; i++)
					{
						array[i] = new DataTablePropertyDescriptor(dataSet.Tables[i]);
					}
				}
				this.propsCollection = new PropertyDescriptorCollection(array);
			}
			return this.propsCollection;
		}

		// Token: 0x06000BE3 RID: 3043 RVA: 0x0020F0D8 File Offset: 0x0020E4D8
		object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
		{
			return this;
		}

		// Token: 0x04000866 RID: 2150
		private DataViewManager dataViewManager;

		// Token: 0x04000867 RID: 2151
		private PropertyDescriptorCollection propsCollection;
	}
}
