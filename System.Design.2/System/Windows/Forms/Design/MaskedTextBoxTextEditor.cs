using System;
using System.ComponentModel;
using System.Drawing.Design;

namespace System.Windows.Forms.Design
{
	// Token: 0x02000315 RID: 789
	internal class MaskedTextBoxTextEditor : UITypeEditor
	{
		// Token: 0x06001F2E RID: 7982 RVA: 0x000BB598 File Offset: 0x000B9798
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			if (context != null && context.Instance != null && provider != null)
			{
				IWindowsFormsEditorService windowsFormsEditorService = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;
				if (windowsFormsEditorService != null && context.Instance != null)
				{
					MaskedTextBox maskedTextBox = context.Instance as MaskedTextBox;
					if (maskedTextBox == null)
					{
						maskedTextBox = new MaskedTextBox();
						maskedTextBox.Text = (value as string);
					}
					MaskedTextBoxTextEditorDropDown maskedTextBoxTextEditorDropDown = new MaskedTextBoxTextEditorDropDown(maskedTextBox);
					windowsFormsEditorService.DropDownControl(maskedTextBoxTextEditorDropDown);
					if (maskedTextBoxTextEditorDropDown.Value != null)
					{
						value = maskedTextBoxTextEditorDropDown.Value;
					}
				}
			}
			return value;
		}

		// Token: 0x06001F2F RID: 7983 RVA: 0x000BB616 File Offset: 0x000B9816
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			if (context != null && context.Instance != null)
			{
				return UITypeEditorEditStyle.DropDown;
			}
			return base.GetEditStyle(context);
		}

		// Token: 0x06001F30 RID: 7984 RVA: 0x000BB62C File Offset: 0x000B982C
		public override bool GetPaintValueSupported(ITypeDescriptorContext context)
		{
			return (context == null || context.Instance == null) && base.GetPaintValueSupported(context);
		}

		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x06001F31 RID: 7985 RVA: 0x0000445B File Offset: 0x0000265B
		public override bool IsDropDownResizable
		{
			get
			{
				return false;
			}
		}
	}
}
