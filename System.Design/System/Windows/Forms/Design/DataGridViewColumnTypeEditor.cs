using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;

namespace System.Windows.Forms.Design
{
	// Token: 0x020001EA RID: 490
	internal class DataGridViewColumnTypeEditor : UITypeEditor
	{
		// Token: 0x060012E6 RID: 4838 RVA: 0x000607AA File Offset: 0x0005F7AA
		private DataGridViewColumnTypeEditor()
		{
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x060012E7 RID: 4839 RVA: 0x000607B2 File Offset: 0x0005F7B2
		public override bool IsDropDownResizable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060012E8 RID: 4840 RVA: 0x000607B8 File Offset: 0x0005F7B8
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			if (provider != null)
			{
				IWindowsFormsEditorService windowsFormsEditorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
				if (windowsFormsEditorService != null && context.Instance != null)
				{
					if (this.columnTypePicker == null)
					{
						this.columnTypePicker = new DataGridViewColumnTypePicker();
					}
					DataGridViewColumnCollectionDialog.ListBoxItem listBoxItem = (DataGridViewColumnCollectionDialog.ListBoxItem)context.Instance;
					IDesignerHost designerHost = (IDesignerHost)provider.GetService(typeof(IDesignerHost));
					ITypeDiscoveryService discoveryService = null;
					if (designerHost != null)
					{
						discoveryService = (ITypeDiscoveryService)designerHost.GetService(typeof(ITypeDiscoveryService));
					}
					this.columnTypePicker.Start(windowsFormsEditorService, discoveryService, listBoxItem.DataGridViewColumn.GetType());
					windowsFormsEditorService.DropDownControl(this.columnTypePicker);
					if (this.columnTypePicker.SelectedType != null)
					{
						value = this.columnTypePicker.SelectedType;
					}
				}
			}
			return value;
		}

		// Token: 0x060012E9 RID: 4841 RVA: 0x00060881 File Offset: 0x0005F881
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.DropDown;
		}

		// Token: 0x04001183 RID: 4483
		private DataGridViewColumnTypePicker columnTypePicker;
	}
}
