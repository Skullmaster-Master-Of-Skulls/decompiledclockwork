using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;

namespace System.Windows.Forms.Design
{
	// Token: 0x020002BF RID: 703
	internal class DataGridViewColumnTypeEditor : UITypeEditor
	{
		// Token: 0x06001BED RID: 7149 RVA: 0x00003939 File Offset: 0x00001B39
		private DataGridViewColumnTypeEditor()
		{
		}

		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x06001BEE RID: 7150 RVA: 0x00003B0F File Offset: 0x00001D0F
		public override bool IsDropDownResizable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001BEF RID: 7151 RVA: 0x000A8A30 File Offset: 0x000A6C30
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

		// Token: 0x06001BF0 RID: 7152 RVA: 0x0003DFAE File Offset: 0x0003C1AE
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.DropDown;
		}

		// Token: 0x040016B9 RID: 5817
		private DataGridViewColumnTypePicker columnTypePicker;
	}
}
