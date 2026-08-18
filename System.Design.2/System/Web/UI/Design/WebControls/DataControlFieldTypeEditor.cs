using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;
using System.Drawing.Design;
using System.Web.UI.Design.Util;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020000B7 RID: 183
	public class DataControlFieldTypeEditor : UITypeEditor
	{
		// Token: 0x060005E0 RID: 1504 RVA: 0x0001EF24 File Offset: 0x0001D124
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			DataBoundControl dataBoundControl = context.Instance as DataBoundControl;
			if (dataBoundControl != null)
			{
				IDesignerHost designerHost = (IDesignerHost)provider.GetService(typeof(IDesignerHost));
				DataBoundControlDesigner designer = (DataBoundControlDesigner)designerHost.GetDesigner(dataBoundControl);
				IComponentChangeService changeService = (IComponentChangeService)provider.GetService(typeof(IComponentChangeService));
				ControlDesigner.InvokeTransactedChange(dataBoundControl, delegate(object callbackContext)
				{
					DataControlFieldsEditor form = new DataControlFieldsEditor(designer);
					DialogResult dialogResult = UIServiceHelper.ShowDialog(provider, form);
					if (dialogResult == DialogResult.OK && changeService != null)
					{
						changeService.OnComponentChanged(dataBoundControl, null, null, null);
					}
					return dialogResult == DialogResult.OK;
				}, null, SR.GetString("GridView_EditFieldsTransaction"));
				return value;
			}
			return null;
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x00009D4C File Offset: 0x00007F4C
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.Modal;
		}
	}
}
