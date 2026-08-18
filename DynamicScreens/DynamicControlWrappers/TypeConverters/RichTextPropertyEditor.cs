using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace DynamicScreens.DynamicControlWrappers.TypeConverters
{
	// Token: 0x02000054 RID: 84
	public class RichTextPropertyEditor : UITypeEditor
	{
		// Token: 0x06000497 RID: 1175 RVA: 0x0003EF08 File Offset: 0x0003DF08
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

		// Token: 0x06000498 RID: 1176 RVA: 0x0003EF30 File Offset: 0x0003DF30
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			if (context != null && provider != null)
			{
				IWindowsFormsEditorService windowsFormsEditorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
				if (windowsFormsEditorService != null)
				{
					RichTextPropertyEditorForm richTextPropertyEditorForm = new RichTextPropertyEditorForm();
					richTextPropertyEditorForm.RichText = (string)value;
					if (windowsFormsEditorService.ShowDialog(richTextPropertyEditorForm) == DialogResult.OK)
					{
						return richTextPropertyEditorForm.RichText;
					}
				}
			}
			return base.EditValue(context, provider, value);
		}
	}
}
