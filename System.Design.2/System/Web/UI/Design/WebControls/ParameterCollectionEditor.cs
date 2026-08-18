using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;
using System.Drawing.Design;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020000F5 RID: 245
	public class ParameterCollectionEditor : UITypeEditor
	{
		// Token: 0x06000883 RID: 2179 RVA: 0x0003027C File Offset: 0x0002E47C
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			ParameterCollection parameterCollection = value as ParameterCollection;
			if (parameterCollection == null)
			{
				throw new ArgumentException(SR.GetString("ParameterCollectionEditor_InvalidParameters"), "value");
			}
			Control control = context.Instance as Control;
			ControlDesigner designer = null;
			if (control != null && control.Site != null)
			{
				IDesignerHost designerHost = (IDesignerHost)control.Site.GetService(typeof(IDesignerHost));
				if (designerHost != null)
				{
					designer = (designerHost.GetDesigner(control) as ControlDesigner);
				}
			}
			ParameterCollectionEditorForm parameterCollectionEditorForm = new ParameterCollectionEditorForm(provider, parameterCollection, designer);
			DialogResult dialogResult = parameterCollectionEditorForm.ShowDialog();
			if (dialogResult == DialogResult.OK && context != null)
			{
				context.OnComponentChanged();
			}
			return value;
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x00009D4C File Offset: 0x00007F4C
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.Modal;
		}
	}
}
