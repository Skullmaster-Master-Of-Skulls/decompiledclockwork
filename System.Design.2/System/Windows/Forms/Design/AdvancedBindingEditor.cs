using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;

namespace System.Windows.Forms.Design
{
	// Token: 0x02000290 RID: 656
	internal class AdvancedBindingEditor : UITypeEditor
	{
		// Token: 0x060018F8 RID: 6392 RVA: 0x0008BE4C File Offset: 0x0008A04C
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			if (provider != null)
			{
				IWindowsFormsEditorService windowsFormsEditorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
				IDesignerHost designerHost = provider.GetService(typeof(IDesignerHost)) as IDesignerHost;
				if (windowsFormsEditorService != null && designerHost != null)
				{
					if (this.bindingFormattingDialog == null)
					{
						this.bindingFormattingDialog = DpiHelper.CreateInstanceInSystemAwareContext<BindingFormattingDialog>(() => new BindingFormattingDialog());
					}
					this.bindingFormattingDialog.Context = context;
					this.bindingFormattingDialog.Bindings = (ControlBindingsCollection)value;
					this.bindingFormattingDialog.Host = designerHost;
					using (DesignerTransaction designerTransaction = designerHost.CreateTransaction())
					{
						windowsFormsEditorService.ShowDialog(this.bindingFormattingDialog);
						if (this.bindingFormattingDialog.Dirty)
						{
							TypeDescriptor.Refresh(((ControlBindingsCollection)context.Instance).BindableComponent);
							if (designerTransaction != null)
							{
								designerTransaction.Commit();
							}
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

		// Token: 0x060018F9 RID: 6393 RVA: 0x00009D4C File Offset: 0x00007F4C
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.Modal;
		}

		// Token: 0x04001555 RID: 5461
		private BindingFormattingDialog bindingFormattingDialog;
	}
}
