using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace AutoComboBox.MyControls.CustomTableControls
{
	// Token: 0x02000023 RID: 35
	public class TablePropertyEditorEditor : UITypeEditor
	{
		// Token: 0x0600010A RID: 266 RVA: 0x0000BD3C File Offset: 0x0000AD3C
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			UITypeEditorEditStyle result;
			if (context != null)
			{
				result = UITypeEditorEditStyle.Modal;
			}
			else
			{
				result = base.GetEditStyle(context);
			}
			return result;
		}

		// Token: 0x0600010B RID: 267 RVA: 0x0000BD64 File Offset: 0x0000AD64
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			if (context != null && provider != null)
			{
				IWindowsFormsEditorService windowsFormsEditorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
				if (windowsFormsEditorService != null)
				{
					TablePropertyEditorForm tablePropertyEditorForm = new TablePropertyEditorForm();
					tablePropertyEditorForm.XmlDefinition = (string)value;
					if (windowsFormsEditorService.ShowDialog(tablePropertyEditorForm) == DialogResult.OK)
					{
						return tablePropertyEditorForm.XmlDefinition;
					}
				}
			}
			return base.EditValue(context, provider, value);
		}
	}
}
