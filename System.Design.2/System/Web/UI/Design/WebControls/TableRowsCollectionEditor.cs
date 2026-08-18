using System;
using System.ComponentModel.Design;
using System.Reflection;
using System.Security.Permissions;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x02000126 RID: 294
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class TableRowsCollectionEditor : CollectionEditor
	{
		// Token: 0x06000AA0 RID: 2720 RVA: 0x00023ABB File Offset: 0x00021CBB
		public TableRowsCollectionEditor(Type type) : base(type)
		{
		}

		// Token: 0x06000AA1 RID: 2721 RVA: 0x0000445B File Offset: 0x0000265B
		protected override bool CanSelectMultipleInstances()
		{
			return false;
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x0002CBAB File Offset: 0x0002ADAB
		protected override object CreateInstance(Type itemType)
		{
			return Activator.CreateInstance(itemType, BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance, null, null, null);
		}
	}
}
