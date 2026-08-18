using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;

namespace System.Windows.Forms.Design
{
	// Token: 0x02000317 RID: 791
	internal class MaskPropertyEditor : UITypeEditor
	{
		// Token: 0x06001F38 RID: 7992 RVA: 0x000BB7D4 File Offset: 0x000B99D4
		internal static string EditMask(ITypeDiscoveryService discoverySvc, IUIService uiSvc, MaskedTextBox instance, IHelpService helpService)
		{
			string result = null;
			MaskDesignerDialog maskDesignerDialog = DpiHelper.CreateInstanceInSystemAwareContext<MaskDesignerDialog>(() => new MaskDesignerDialog(instance, helpService));
			try
			{
				maskDesignerDialog.DiscoverMaskDescriptors(discoverySvc);
				DialogResult dialogResult = (uiSvc != null) ? uiSvc.ShowDialog(maskDesignerDialog) : maskDesignerDialog.ShowDialog();
				if (dialogResult == DialogResult.OK)
				{
					result = maskDesignerDialog.Mask;
					if (maskDesignerDialog.ValidatingType != instance.ValidatingType)
					{
						instance.ValidatingType = maskDesignerDialog.ValidatingType;
					}
				}
			}
			finally
			{
				maskDesignerDialog.Dispose();
			}
			return result;
		}

		// Token: 0x06001F39 RID: 7993 RVA: 0x000BB874 File Offset: 0x000B9A74
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			if (context != null && provider != null)
			{
				ITypeDiscoveryService discoverySvc = (ITypeDiscoveryService)provider.GetService(typeof(ITypeDiscoveryService));
				IUIService uiSvc = (IUIService)provider.GetService(typeof(IUIService));
				IHelpService helpService = (IHelpService)provider.GetService(typeof(IHelpService));
				string text = MaskPropertyEditor.EditMask(discoverySvc, uiSvc, context.Instance as MaskedTextBox, helpService);
				if (text != null)
				{
					return text;
				}
			}
			return value;
		}

		// Token: 0x06001F3A RID: 7994 RVA: 0x0000445B File Offset: 0x0000265B
		public override bool GetPaintValueSupported(ITypeDescriptorContext context)
		{
			return false;
		}

		// Token: 0x06001F3B RID: 7995 RVA: 0x00009D4C File Offset: 0x00007F4C
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.Modal;
		}
	}
}
