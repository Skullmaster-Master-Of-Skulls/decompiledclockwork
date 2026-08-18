using System;
using System.ComponentModel;

namespace System.Data
{
	// Token: 0x020000DB RID: 219
	internal sealed class DataViewManagerListItemTypeDescriptor : ICustomTypeDescriptor
	{
		// Token: 0x06000EDC RID: 3804 RVA: 0x00078828 File Offset: 0x00077C28
		internal DataViewManagerListItemTypeDescriptor(DataViewManager dataViewManager)
		{
			this.dataViewManager = dataViewManager;
		}

		// Token: 0x06000EDD RID: 3805 RVA: 0x00078844 File Offset: 0x00077C44
		internal void Reset()
		{
			this.propsCollection = null;
		}

		// Token: 0x06000EDE RID: 3806 RVA: 0x00078858 File Offset: 0x00077C58
		internal DataView GetDataView(DataTable table)
		{
			DataView dataView = new DataView(table);
			dataView.SetDataViewManager(this.dataViewManager);
			return dataView;
		}

		// Token: 0x06000EDF RID: 3807 RVA: 0x0007887C File Offset: 0x00077C7C
		AttributeCollection ICustomTypeDescriptor.GetAttributes()
		{
			return new AttributeCollection(null);
		}

		// Token: 0x06000EE0 RID: 3808 RVA: 0x00078890 File Offset: 0x00077C90
		string ICustomTypeDescriptor.GetClassName()
		{
			return null;
		}

		// Token: 0x06000EE1 RID: 3809 RVA: 0x000788A0 File Offset: 0x00077CA0
		string ICustomTypeDescriptor.GetComponentName()
		{
			return null;
		}

		// Token: 0x06000EE2 RID: 3810 RVA: 0x000788B0 File Offset: 0x00077CB0
		TypeConverter ICustomTypeDescriptor.GetConverter()
		{
			return null;
		}

		// Token: 0x06000EE3 RID: 3811 RVA: 0x000788C0 File Offset: 0x00077CC0
		EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
		{
			return null;
		}

		// Token: 0x06000EE4 RID: 3812 RVA: 0x000788D0 File Offset: 0x00077CD0
		PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
		{
			return null;
		}

		// Token: 0x06000EE5 RID: 3813 RVA: 0x000788E0 File Offset: 0x00077CE0
		object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
		{
			return null;
		}

		// Token: 0x06000EE6 RID: 3814 RVA: 0x000788F0 File Offset: 0x00077CF0
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
		{
			return new EventDescriptorCollection(null);
		}

		// Token: 0x06000EE7 RID: 3815 RVA: 0x00078904 File Offset: 0x00077D04
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
		{
			return new EventDescriptorCollection(null);
		}

		// Token: 0x06000EE8 RID: 3816 RVA: 0x00078918 File Offset: 0x00077D18
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
		{
			return ((ICustomTypeDescriptor)this).GetProperties(null);
		}

		// Token: 0x06000EE9 RID: 3817 RVA: 0x0007892C File Offset: 0x00077D2C
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

		// Token: 0x06000EEA RID: 3818 RVA: 0x00078998 File Offset: 0x00077D98
		object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
		{
			return this;
		}

		// Token: 0x04000442 RID: 1090
		private DataViewManager dataViewManager;

		// Token: 0x04000443 RID: 1091
		private PropertyDescriptorCollection propsCollection;
	}
}
