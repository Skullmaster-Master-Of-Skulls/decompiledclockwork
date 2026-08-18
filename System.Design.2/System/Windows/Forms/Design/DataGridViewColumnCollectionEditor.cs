using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;
using System.Drawing.Design;

namespace System.Windows.Forms.Design
{
	// Token: 0x020002BC RID: 700
	internal class DataGridViewColumnCollectionEditor : UITypeEditor
	{
		// Token: 0x06001BD5 RID: 7125 RVA: 0x00003939 File Offset: 0x00001B39
		private DataGridViewColumnCollectionEditor()
		{
		}

		// Token: 0x06001BD6 RID: 7126 RVA: 0x000A833C File Offset: 0x000A653C
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			if (provider != null)
			{
				IWindowsFormsEditorService windowsFormsEditorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
				if (windowsFormsEditorService != null && context.Instance != null)
				{
					IDesignerHost designerHost = (IDesignerHost)provider.GetService(typeof(IDesignerHost));
					if (designerHost == null)
					{
						return value;
					}
					if (this.dataGridViewColumnCollectionDialog == null)
					{
						this.dataGridViewColumnCollectionDialog = DpiHelper.CreateInstanceInSystemAwareContext<DataGridViewColumnCollectionDialog>(() => new DataGridViewColumnCollectionDialog(((DataGridView)context.Instance).Site));
					}
					this.dataGridViewColumnCollectionDialog.SetLiveDataGridView((DataGridView)context.Instance);
					using (DesignerTransaction designerTransaction = designerHost.CreateTransaction(SR.GetString("DataGridViewColumnCollectionTransaction")))
					{
						if (windowsFormsEditorService.ShowDialog(this.dataGridViewColumnCollectionDialog) == DialogResult.OK)
						{
							designerTransaction.Commit();
						}
						else
						{
							designerTransaction.Cancel();
						}
					}
				}
			}
			return value;
		}

		// Token: 0x06001BD7 RID: 7127 RVA: 0x00009D4C File Offset: 0x00007F4C
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.Modal;
		}

		// Token: 0x040016AF RID: 5807
		private DataGridViewColumnCollectionDialog dataGridViewColumnCollectionDialog;
	}
}
