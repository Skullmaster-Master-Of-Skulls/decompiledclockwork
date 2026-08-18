using System;
using System.ComponentModel;
using System.Drawing.Design;

namespace System.Windows.Forms.Design
{
	// Token: 0x020002BD RID: 701
	internal class DataGridViewColumnDataPropertyNameEditor : UITypeEditor
	{
		// Token: 0x06001BD8 RID: 7128 RVA: 0x00003939 File Offset: 0x00001B39
		private DataGridViewColumnDataPropertyNameEditor()
		{
		}

		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x06001BD9 RID: 7129 RVA: 0x00003B0F File Offset: 0x00001D0F
		public override bool IsDropDownResizable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001BDA RID: 7130 RVA: 0x000A8428 File Offset: 0x000A6628
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			if (provider != null && context != null && context.Instance != null)
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

		// Token: 0x06001BDB RID: 7131 RVA: 0x0003DFAE File Offset: 0x0003C1AE
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.DropDown;
		}

		// Token: 0x040016B0 RID: 5808
		private DesignBindingPicker designBindingPicker;
	}
}
