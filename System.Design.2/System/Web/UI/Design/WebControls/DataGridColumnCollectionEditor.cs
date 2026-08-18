using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Security.Permissions;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020000AA RID: 170
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class DataGridColumnCollectionEditor : UITypeEditor
	{
		// Token: 0x0600052B RID: 1323 RVA: 0x00018F9C File Offset: 0x0001719C
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			IDesignerHost designerHost = (IDesignerHost)context.GetService(typeof(IDesignerHost));
			DataGrid component = (DataGrid)context.Instance;
			BaseDataListDesigner baseDataListDesigner = (BaseDataListDesigner)designerHost.GetDesigner(component);
			baseDataListDesigner.InvokePropertyBuilder(DataGridComponentEditor.IDX_COLUMNS);
			return value;
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x00009D4C File Offset: 0x00007F4C
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.Modal;
		}
	}
}
