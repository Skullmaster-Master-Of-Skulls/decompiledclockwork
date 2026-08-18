using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.Design
{
	// Token: 0x02000042 RID: 66
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class HierarchicalDataSourceConverter : DataSourceConverter
	{
		// Token: 0x0600023D RID: 573 RVA: 0x0000F3D0 File Offset: 0x0000D5D0
		protected override bool IsValidDataSource(IComponent component)
		{
			Control control = component as Control;
			return control != null && !string.IsNullOrEmpty(control.ID) && component is IHierarchicalEnumerable;
		}
	}
}
