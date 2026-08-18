using System;
using System.Security.Permissions;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020004F0 RID: 1264
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class TreeNodeStyleCollectionEditor : StyleCollectionEditor
	{
		// Token: 0x06002D31 RID: 11569 RVA: 0x000FF915 File Offset: 0x000FE915
		public TreeNodeStyleCollectionEditor(Type type) : base(type)
		{
		}

		// Token: 0x06002D32 RID: 11570 RVA: 0x000FF91E File Offset: 0x000FE91E
		protected override Type CreateCollectionItemType()
		{
			return typeof(TreeNodeStyle);
		}
	}
}
