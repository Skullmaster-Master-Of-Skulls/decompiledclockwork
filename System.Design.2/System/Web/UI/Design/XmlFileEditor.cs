using System;
using System.ComponentModel;
using System.Design;
using System.Drawing.Design;
using System.Security.Permissions;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace System.Web.UI.Design
{
	// Token: 0x0200008E RID: 142
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class XmlFileEditor : UITypeEditor
	{
		// Token: 0x0600045A RID: 1114 RVA: 0x00013F50 File Offset: 0x00012150
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			if (provider != null)
			{
				IWindowsFormsEditorService windowsFormsEditorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
				if (windowsFormsEditorService != null)
				{
					if (this.fileDialog == null)
					{
						this.fileDialog = new OpenFileDialog();
						this.fileDialog.Title = SR.GetString("XMLFilePicker_Caption");
						this.fileDialog.Filter = SR.GetString("XMLFilePicker_Filter");
					}
					if (value != null)
					{
						this.fileDialog.FileName = value.ToString();
					}
					if (this.fileDialog.ShowDialog() == DialogResult.OK)
					{
						value = this.fileDialog.FileName;
					}
				}
			}
			return value;
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x00009D4C File Offset: 0x00007F4C
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.Modal;
		}

		// Token: 0x040001C5 RID: 453
		internal FileDialog fileDialog;
	}
}
