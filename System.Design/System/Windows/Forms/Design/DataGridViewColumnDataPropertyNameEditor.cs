using System;
using System.ComponentModel;
using System.Drawing.Design;

namespace System.Windows.Forms.Design
{
	// Token: 0x020001E7 RID: 487
	internal class DataGridViewColumnDataPropertyNameEditor : UITypeEditor
	{
		// Token: 0x060012CE RID: 4814 RVA: 0x00060047 File Offset: 0x0005F047
		private DataGridViewColumnDataPropertyNameEditor()
		{
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x060012CF RID: 4815 RVA: 0x0006004F File Offset: 0x0005F04F
		public override bool IsDropDownResizable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060012D0 RID: 4816 RVA: 0x00060054 File Offset: 0x0005F054
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			if (provider != null && context.Instance != null)
			{
				DataGridViewColumnCollectionDialog.ListBoxItem listBoxItem = context.Instance as DataGridViewColumnCollectionDialog.ListBoxItem;
				DataGridView dataGridView;
				if (listBoxItem != null)
				{
					dataGridView = listBoxItem.DataGridViewColumn.DataGridView;
				}
				else
				{
					DataGridViewColumn dataGridViewColumn = context.Instance as DataGridViewColumn;
					if (dataGridViewColumn != null)
					{
						dataGridView = dataGridViewColumn.DataGridView;
					}
					else
					{
						dataGridView = null;
					}
				}
				if (dataGridView == null)
				{
					return value;
				}
				object dataSource = dataGridView.DataSource;
				string text = dataGridView.DataMember;
				string text2 = (string)value;
				string dataMember = text + "." + text2;
				if (dataSource == null)
				{
					text = string.Empty;
					dataMember = text2;
				}
				if (this.designBindingPicker == null)
				{
					this.designBindingPicker = new DesignBindingPicker();
				}
				DesignBinding initialSelectedItem = new DesignBinding(dataSource, dataMember);
				DesignBinding designBinding = this.designBindingPicker.Pick(context, provider, false, true, false, dataSource, text, initialSelectedItem);
				if (dataSource != null && designBinding != null)
				{
					value = designBinding.DataField;
				}
			}
			return value;
		}

		// Token: 0x060012D1 RID: 4817 RVA: 0x00060128 File Offset: 0x0005F128
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.DropDown;
		}

		// Token: 0x0400117A RID: 4474
		private DesignBindingPicker designBindingPicker;
	}
}
