using System;
using System.ComponentModel;
using System.Drawing.Design;

namespace System.Windows.Forms.Design
{
	// Token: 0x020002B5 RID: 693
	internal class DataGridColumnStyleMappingNameEditor : UITypeEditor
	{
		// Token: 0x06001B75 RID: 7029 RVA: 0x00003939 File Offset: 0x00001B39
		private DataGridColumnStyleMappingNameEditor()
		{
		}

		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x06001B76 RID: 7030 RVA: 0x00003B0F File Offset: 0x00001D0F
		public override bool IsDropDownResizable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001B77 RID: 7031 RVA: 0x000A32F0 File Offset: 0x000A14F0
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			if (provider != null && context != null && context.Instance != null)
			{
				DataGridColumnStyle dataGridColumnStyle = (DataGridColumnStyle)context.Instance;
				if (dataGridColumnStyle.DataGridTableStyle == null || dataGridColumnStyle.DataGridTableStyle.DataGrid == null)
				{
					return value;
				}
				PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(dataGridColumnStyle.DataGridTableStyle.DataGrid)["DataSource"];
				if (propertyDescriptor != null)
				{
					object value2 = propertyDescriptor.GetValue(dataGridColumnStyle.DataGridTableStyle.DataGrid);
					if (this.designBindingPicker == null)
					{
						this.designBindingPicker = new DesignBindingPicker();
					}
					DesignBinding initialSelectedItem = new DesignBinding(null, (string)value);
					DesignBinding designBinding = this.designBindingPicker.Pick(context, provider, false, true, false, value2, string.Empty, initialSelectedItem);
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

		// Token: 0x06001B78 RID: 7032 RVA: 0x0003DFAE File Offset: 0x0003C1AE
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.DropDown;
		}

		// Token: 0x0400164F RID: 5711
		private DesignBindingPicker designBindingPicker;
	}
}
