using System;
using System.ComponentModel.Design;
using System.Security.Permissions;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020000DA RID: 218
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class ListItemsCollectionEditor : CollectionEditor
	{
		// Token: 0x06000769 RID: 1897 RVA: 0x00023ABB File Offset: 0x00021CBB
		public ListItemsCollectionEditor(Type type) : base(type)
		{
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x0000445B File Offset: 0x0000265B
		protected override bool CanSelectMultipleInstances()
		{
			return false;
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x0600076B RID: 1899 RVA: 0x00028BA3 File Offset: 0x00026DA3
		protected override string HelpTopic
		{
			get
			{
				return "net.ComponentModel.CollectionEditor";
			}
		}
	}
}
