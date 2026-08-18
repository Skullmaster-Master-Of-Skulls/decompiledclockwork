using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Security.Permissions;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020000C7 RID: 199
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class EmbeddedMailObjectCollectionEditor : CollectionEditor
	{
		// Token: 0x06000690 RID: 1680 RVA: 0x00023ABB File Offset: 0x00021CBB
		public EmbeddedMailObjectCollectionEditor(Type type) : base(type)
		{
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x00023AC4 File Offset: 0x00021CC4
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			object result;
			try
			{
				context.OnComponentChanging();
				result = base.EditValue(context, provider, value);
			}
			finally
			{
				context.OnComponentChanged();
			}
			return result;
		}
	}
}
