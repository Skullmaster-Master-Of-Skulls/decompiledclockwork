using System;
using System.ComponentModel;
using System.Design;
using System.Drawing.Design;

namespace System.Windows.Forms.Design
{
	// Token: 0x020002E0 RID: 736
	public class FileNameEditor : UITypeEditor
	{
		// Token: 0x06001D7D RID: 7549 RVA: 0x000B2018 File Offset: 0x000B0218
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			if (provider != null)
			{
				IWindowsFormsEditorService windowsFormsEditorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
				if (windowsFormsEditorService != null)
				{
					if (this.openFileDialog == null)
					{
						this.openFileDialog = new OpenFileDialog();
						this.InitializeDialog(this.openFileDialog);
					}
					if (value is string)
					{
						this.openFileDialog.FileName = (string)value;
					}
					if (this.openFileDialog.ShowDialog() == DialogResult.OK)
					{
						value = this.openFileDialog.FileName;
					}
				}
			}
			return value;
		}

		// Token: 0x06001D7E RID: 7550 RVA: 0x00009D4C File Offset: 0x00007F4C
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.Modal;
		}

		// Token: 0x06001D7F RID: 7551 RVA: 0x000B2095 File Offset: 0x000B0295
		protected virtual void InitializeDialog(OpenFileDialog openFileDialog)
		{
			openFileDialog.Filter = SR.GetString("GenericFileFilter");
			openFileDialog.Title = SR.GetString("GenericOpenFile");
		}

		// Token: 0x04001776 RID: 6006
		private OpenFileDialog openFileDialog;
	}
}
