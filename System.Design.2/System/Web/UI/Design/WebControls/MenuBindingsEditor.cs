using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020000E5 RID: 229
	public class MenuBindingsEditor : UITypeEditor
	{
		// Token: 0x060007B3 RID: 1971 RVA: 0x0002A168 File Offset: 0x00028368
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			IDesignerHost designerHost = (IDesignerHost)context.GetService(typeof(IDesignerHost));
			Menu component = (Menu)context.Instance;
			MenuDesigner menuDesigner = (MenuDesigner)designerHost.GetDesigner(component);
			menuDesigner.InvokeMenuBindingsEditor();
			return value;
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x00009D4C File Offset: 0x00007F4C
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.Modal;
		}
	}
}
