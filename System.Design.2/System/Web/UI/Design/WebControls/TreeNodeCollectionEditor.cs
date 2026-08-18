using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x02000128 RID: 296
	public class TreeNodeCollectionEditor : UITypeEditor
	{
		// Token: 0x06000AA6 RID: 2726 RVA: 0x00043550 File Offset: 0x00041750
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			IDesignerHost designerHost = (IDesignerHost)context.GetService(typeof(IDesignerHost));
			TreeView component = (TreeView)context.Instance;
			TreeViewDesigner treeViewDesigner = (TreeViewDesigner)designerHost.GetDesigner(component);
			treeViewDesigner.InvokeTreeNodeCollectionEditor();
			return value;
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x00009D4C File Offset: 0x00007F4C
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.Modal;
		}
	}
}
