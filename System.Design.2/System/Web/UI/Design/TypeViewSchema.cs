using System;
using System.Security.Permissions;

namespace System.Web.UI.Design
{
	// Token: 0x0200007B RID: 123
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	internal sealed class TypeViewSchema : BaseTypeViewSchema
	{
		// Token: 0x060003D0 RID: 976 RVA: 0x00012324 File Offset: 0x00010524
		public TypeViewSchema(string viewName, Type type) : base(viewName, type)
		{
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x0001283F File Offset: 0x00010A3F
		protected override Type GetRowType(Type objectType)
		{
			return objectType;
		}
	}
}
