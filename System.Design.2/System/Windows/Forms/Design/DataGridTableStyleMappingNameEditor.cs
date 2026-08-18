using System;
using System.ComponentModel;
using System.Drawing.Design;

namespace System.Windows.Forms.Design
{
	// Token: 0x020002B7 RID: 695
	internal class DataGridTableStyleMappingNameEditor : UITypeEditor
	{
		// Token: 0x06001B7F RID: 7039 RVA: 0x00003939 File Offset: 0x00001B39
		private DataGridTableStyleMappingNameEditor()
		{
		}

		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x06001B80 RID: 7040 RVA: 0x00003B0F File Offset: 0x00001D0F
		public override bool IsDropDownResizable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001B81 RID: 7041 RVA: 0x000A3688 File Offset: 0x000A1888
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			if (provider != null && context != null && context.Instance != null)
			{
				DataGridTableStyle dataGridTableStyle = (DataGridTableStyle)context.Instance;
				if (dataGridTableStyle.DataGrid == null)
				{
					return value;
				}
				PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(dataGridTableStyle.DataGrid)["DataSource"];
				if (propertyDescriptor != null)
				{
					object value2 = propertyDescriptor.GetValue(dataGridTableStyle.DataGrid);
					if (this.designBindingPicker == null)
					{
						this.designBindingPicker = new DesignBindingPicker();
					}
					DesignBinding initialSelectedItem = new DesignBinding(value2, (string)value);
					DesignBinding designBinding = this.designBindingPicker.Pick(context, provider, false, true, true, value2, string.Empty, initialSelectedItem);
					if (value2 != null && designBinding != null)
					{
						if (string.IsNullOrEmpty(designBinding.DataMember) || designBinding.DataMember == null)
						{
							value = "";
						}
						else
						{
							value = designBinding.DataField;
						}
					}
				}
			}
			return value;
		}

		// Token: 0x06001B82 RID: 7042 RVA: 0x0003DFAE File Offset: 0x0003C1AE
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.DropDown;
		}

		// Token: 0x04001652 RID: 5714
		private DesignBindingPicker designBindingPicker;
	}
}
