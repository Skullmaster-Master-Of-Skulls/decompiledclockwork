using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;

namespace System.Windows.Forms.Design
{
	// Token: 0x020002E7 RID: 743
	internal class FormatStringEditor : UITypeEditor
	{
		// Token: 0x06001DCD RID: 7629 RVA: 0x000B512C File Offset: 0x000B332C
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			if (provider != null)
			{
				IWindowsFormsEditorService windowsFormsEditorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
				if (windowsFormsEditorService != null)
				{
					DataGridViewCellStyle dataGridViewCellStyle = context.Instance as DataGridViewCellStyle;
					ListControl listControl = context.Instance as ListControl;
					if (this.formatStringDialog == null)
					{
						this.formatStringDialog = DpiHelper.CreateInstanceInSystemAwareContext<FormatStringDialog>(() => new FormatStringDialog(context));
					}
					if (listControl != null)
					{
						this.formatStringDialog.ListControl = listControl;
					}
					else
					{
						this.formatStringDialog.DataGridViewCellStyle = dataGridViewCellStyle;
					}
					IComponentChangeService componentChangeService = (IComponentChangeService)provider.GetService(typeof(IComponentChangeService));
					if (componentChangeService != null)
					{
						if (dataGridViewCellStyle != null)
						{
							componentChangeService.OnComponentChanging(dataGridViewCellStyle, TypeDescriptor.GetProperties(dataGridViewCellStyle)["Format"]);
							componentChangeService.OnComponentChanging(dataGridViewCellStyle, TypeDescriptor.GetProperties(dataGridViewCellStyle)["NullValue"]);
							componentChangeService.OnComponentChanging(dataGridViewCellStyle, TypeDescriptor.GetProperties(dataGridViewCellStyle)["FormatProvider"]);
						}
						else
						{
							componentChangeService.OnComponentChanging(listControl, TypeDescriptor.GetProperties(listControl)["FormatString"]);
							componentChangeService.OnComponentChanging(listControl, TypeDescriptor.GetProperties(listControl)["FormatInfo"]);
						}
					}
					windowsFormsEditorService.ShowDialog(this.formatStringDialog);
					this.formatStringDialog.End();
					if (this.formatStringDialog.Dirty)
					{
						TypeDescriptor.Refresh(context.Instance);
						if (componentChangeService != null)
						{
							if (dataGridViewCellStyle != null)
							{
								componentChangeService.OnComponentChanged(dataGridViewCellStyle, TypeDescriptor.GetProperties(dataGridViewCellStyle)["Format"], null, null);
								componentChangeService.OnComponentChanged(dataGridViewCellStyle, TypeDescriptor.GetProperties(dataGridViewCellStyle)["NullValue"], null, null);
								componentChangeService.OnComponentChanged(dataGridViewCellStyle, TypeDescriptor.GetProperties(dataGridViewCellStyle)["FormatProvider"], null, null);
							}
							else
							{
								componentChangeService.OnComponentChanged(listControl, TypeDescriptor.GetProperties(listControl)["FormatString"], null, null);
								componentChangeService.OnComponentChanged(listControl, TypeDescriptor.GetProperties(listControl)["FormatInfo"], null, null);
							}
						}
					}
				}
			}
			return value;
		}

		// Token: 0x06001DCE RID: 7630 RVA: 0x00009D4C File Offset: 0x00007F4C
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.Modal;
		}

		// Token: 0x040017A9 RID: 6057
		private FormatStringDialog formatStringDialog;
	}
}
