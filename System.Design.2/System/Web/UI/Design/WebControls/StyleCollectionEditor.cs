using System;
using System.ComponentModel.Design;
using System.Reflection;
using System.Security.Permissions;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x02000121 RID: 289
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class StyleCollectionEditor : CollectionEditor
	{
		// Token: 0x06000A92 RID: 2706 RVA: 0x00023ABB File Offset: 0x00021CBB
		public StyleCollectionEditor(Type type) : base(type)
		{
		}

		// Token: 0x06000A93 RID: 2707 RVA: 0x0002CBAB File Offset: 0x0002ADAB
		protected override object CreateInstance(Type itemType)
		{
			return Activator.CreateInstance(itemType, BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance, null, null, null);
		}
	}
}
