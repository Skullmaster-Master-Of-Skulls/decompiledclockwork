using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;

namespace System.Windows.Forms.Design
{
	// Token: 0x020002BA RID: 698
	internal class DataGridViewCellStyleEditor : UITypeEditor
	{
		// Token: 0x06001BAE RID: 7086 RVA: 0x000A5E44 File Offset: 0x000A4044
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			if (provider != null)
			{
				IWindowsFormsEditorService windowsFormsEditorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
				IUIService iuiservice = (IUIService)provider.GetService(typeof(IUIService));
				IComponent comp = context.Instance as IComponent;
				if (windowsFormsEditorService != null)
				{
					if (this.builderDialog == null)
					{
						this.builderDialog = DpiHelper.CreateInstanceInSystemAwareContext<DataGridViewCellStyleBuilder>(() => new DataGridViewCellStyleBuilder(provider, comp));
					}
					if (iuiservice != null)
					{
						this.builderDialog.Font = (Font)iuiservice.Styles["DialogFont"];
					}
					DataGridViewCellStyle dataGridViewCellStyle = value as DataGridViewCellStyle;
					if (dataGridViewCellStyle != null)
					{
						this.builderDialog.CellStyle = dataGridViewCellStyle;
					}
					this.builderDialog.Context = context;
					if (this.builderDialog.ShowDialog() == DialogResult.OK)
					{
						value = this.builderDialog.CellStyle;
					}
				}
			}
			return value;
		}

		// Token: 0x06001BAF RID: 7087 RVA: 0x00009D4C File Offset: 0x00007F4C
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.Modal;
		}

		// Token: 0x0400168A RID: 5770
		private DataGridViewCellStyleBuilder builderDialog;
	}
}
