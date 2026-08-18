using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020000E8 RID: 232
	public class MenuItemCollectionEditor : UITypeEditor
	{
		// Token: 0x060007F2 RID: 2034 RVA: 0x0002BB74 File Offset: 0x00029D74
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			IDesignerHost designerHost = (IDesignerHost)context.GetService(typeof(IDesignerHost));
			Menu component = (Menu)context.Instance;
			MenuDesigner menuDesigner = (MenuDesigner)designerHost.GetDesigner(component);
			menuDesigner.InvokeMenuItemCollectionEditor();
			return value;
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x00009D4C File Offset: 0x00007F4C
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.Modal;
		}
	}
}
