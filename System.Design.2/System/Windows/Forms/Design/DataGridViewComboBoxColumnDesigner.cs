using System;
using System.Collections;
using System.ComponentModel;

namespace System.Windows.Forms.Design
{
	// Token: 0x020002C1 RID: 705
	internal class DataGridViewComboBoxColumnDesigner : DataGridViewColumnDesigner
	{
		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x06001BFA RID: 7162 RVA: 0x000A8E14 File Offset: 0x000A7014
		// (set) Token: 0x06001BFB RID: 7163 RVA: 0x000A8E34 File Offset: 0x000A7034
		private string ValueMember
		{
			get
			{
				DataGridViewComboBoxColumn dataGridViewComboBoxColumn = (DataGridViewComboBoxColumn)base.Component;
				return dataGridViewComboBoxColumn.ValueMember;
			}
			set
			{
				DataGridViewComboBoxColumn dataGridViewComboBoxColumn = (DataGridViewComboBoxColumn)base.Component;
				if (dataGridViewComboBoxColumn.DataSource == null)
				{
					return;
				}
				if (DataGridViewComboBoxColumnDesigner.ValidDataMember(dataGridViewComboBoxColumn.DataSource, value))
				{
					dataGridViewComboBoxColumn.ValueMember = value;
				}
			}
		}

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x06001BFC RID: 7164 RVA: 0x000A8E6C File Offset: 0x000A706C
		// (set) Token: 0x06001BFD RID: 7165 RVA: 0x000A8E8C File Offset: 0x000A708C
		private string DisplayMember
		{
			get
			{
				DataGridViewComboBoxColumn dataGridViewComboBoxColumn = (DataGridViewComboBoxColumn)base.Component;
				return dataGridViewComboBoxColumn.DisplayMember;
			}
			set
			{
				DataGridViewComboBoxColumn dataGridViewComboBoxColumn = (DataGridViewComboBoxColumn)base.Component;
				if (dataGridViewComboBoxColumn.DataSource == null)
				{
					return;
				}
				if (DataGridViewComboBoxColumnDesigner.ValidDataMember(dataGridViewComboBoxColumn.DataSource, value))
				{
					dataGridViewComboBoxColumn.DisplayMember = value;
				}
			}
		}

		// Token: 0x06001BFE RID: 7166 RVA: 0x000A8EC4 File Offset: 0x000A70C4
		private bool ShouldSerializeDisplayMember()
		{
			DataGridViewComboBoxColumn dataGridViewComboBoxColumn = (DataGridViewComboBoxColumn)base.Component;
			return !string.IsNullOrEmpty(dataGridViewComboBoxColumn.DisplayMember);
		}

		// Token: 0x06001BFF RID: 7167 RVA: 0x000A8EEC File Offset: 0x000A70EC
		private bool ShouldSerializeValueMember()
		{
			DataGridViewComboBoxColumn dataGridViewComboBoxColumn = (DataGridViewComboBoxColumn)base.Component;
			return !string.IsNullOrEmpty(dataGridViewComboBoxColumn.ValueMember);
		}

		// Token: 0x06001C00 RID: 7168 RVA: 0x000A8F14 File Offset: 0x000A7114
		private static bool ValidDataMember(object dataSource, string dataMember)
		{
			if (string.IsNullOrEmpty(dataMember))
			{
				return true;
			}
			if (DataGridViewComboBoxColumnDesigner.bc == null)
			{
				DataGridViewComboBoxColumnDesigner.bc = new BindingContext();
			}
			BindingMemberInfo bindingMemberInfo = new BindingMemberInfo(dataMember);
			BindingManagerBase bindingManagerBase;
			try
			{
				bindingManagerBase = DataGridViewComboBoxColumnDesigner.bc[dataSource, bindingMemberInfo.BindingPath];
			}
			catch (ArgumentException)
			{
				return false;
			}
			if (bindingManagerBase == null)
			{
				return false;
			}
			PropertyDescriptorCollection itemProperties = bindingManagerBase.GetItemProperties();
			return itemProperties != null && itemProperties[bindingMemberInfo.BindingField] != null;
		}

		// Token: 0x06001C01 RID: 7169 RVA: 0x000A8F98 File Offset: 0x000A7198
		protected override void PreFilterProperties(IDictionary properties)
		{
			base.PreFilterProperties(properties);
			PropertyDescriptor propertyDescriptor = (PropertyDescriptor)properties["ValueMember"];
			if (propertyDescriptor != null)
			{
				properties["ValueMember"] = TypeDescriptor.CreateProperty(typeof(DataGridViewComboBoxColumnDesigner), propertyDescriptor, new Attribute[0]);
			}
			propertyDescriptor = (PropertyDescriptor)properties["DisplayMember"];
			if (propertyDescriptor != null)
			{
				properties["DisplayMember"] = TypeDescriptor.CreateProperty(typeof(DataGridViewComboBoxColumnDesigner), propertyDescriptor, new Attribute[0]);
			}
		}

		// Token: 0x040016C0 RID: 5824
		private static BindingContext bc;
	}
}
