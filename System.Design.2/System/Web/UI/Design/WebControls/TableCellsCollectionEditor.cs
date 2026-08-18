using System;
using System.ComponentModel.Design;
using System.Reflection;
using System.Security.Permissions;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x02000124 RID: 292
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class TableCellsCollectionEditor : CollectionEditor
	{
		// Token: 0x06000A9B RID: 2715 RVA: 0x00023ABB File Offset: 0x00021CBB
		public TableCellsCollectionEditor(Type type) : base(type)
		{
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x0000445B File Offset: 0x0000265B
		protected override bool CanSelectMultipleInstances()
		{
			return false;
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x0002CBAB File Offset: 0x0002ADAB
		protected override object CreateInstance(Type itemType)
		{
			return Activator.CreateInstance(itemType, BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance, null, null, null);
		}
	}
}
