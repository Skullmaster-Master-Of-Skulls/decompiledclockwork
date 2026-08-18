using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;
using System.Drawing.Design;

namespace System.Windows.Forms.Design
{
	// Token: 0x020001E6 RID: 486
	internal class DataGridViewColumnCollectionEditor : UITypeEditor
	{
		// Token: 0x060012CB RID: 4811 RVA: 0x0005FF71 File Offset: 0x0005EF71
		private DataGridViewColumnCollectionEditor()
		{
		}

		// Token: 0x060012CC RID: 4812 RVA: 0x0005FF7C File Offset: 0x0005EF7C
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
						this.dataGridViewColumnCollectionDialog = new DataGridViewColumnCollectionDialog();
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

		// Token: 0x060012CD RID: 4813 RVA: 0x00060044 File Offset: 0x0005F044
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.Modal;
		}

		// Token: 0x04001179 RID: 4473
		private DataGridViewColumnCollectionDialog dataGridViewColumnCollectionDialog;
	}
}
