using System;
using System.ComponentModel.Design;
using System.Security.Permissions;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x02000100 RID: 256
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class RoleGroupCollectionEditor : CollectionEditor
	{
		// Token: 0x0600090E RID: 2318 RVA: 0x00023ABB File Offset: 0x00021CBB
		public RoleGroupCollectionEditor(Type type) : base(type)
		{
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x0000445B File Offset: 0x0000265B
		protected override bool CanSelectMultipleInstances()
		{
			return false;
		}
	}
}
