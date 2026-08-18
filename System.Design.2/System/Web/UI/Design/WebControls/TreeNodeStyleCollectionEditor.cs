using System;
using System.Security.Permissions;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x0200012A RID: 298
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class TreeNodeStyleCollectionEditor : StyleCollectionEditor
	{
		// Token: 0x06000ABC RID: 2748 RVA: 0x00044495 File Offset: 0x00042695
		public TreeNodeStyleCollectionEditor(Type type) : base(type)
		{
		}

		// Token: 0x06000ABD RID: 2749 RVA: 0x0004449E File Offset: 0x0004269E
		protected override Type CreateCollectionItemType()
		{
			return typeof(TreeNodeStyle);
		}
	}
}
