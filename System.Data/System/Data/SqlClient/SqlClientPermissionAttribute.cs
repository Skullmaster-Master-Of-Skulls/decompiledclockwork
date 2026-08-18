using System;
using System.Data.Common;
using System.Security;
using System.Security.Permissions;

namespace System.Data.SqlClient
{
	// Token: 0x020002BF RID: 703
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class SqlClientPermissionAttribute : DBDataPermissionAttribute
	{
		// Token: 0x0600236F RID: 9071 RVA: 0x00290DE8 File Offset: 0x002901E8
		public SqlClientPermissionAttribute(SecurityAction action) : base(action)
		{
		}

		// Token: 0x06002370 RID: 9072 RVA: 0x00290E08 File Offset: 0x00290208
		public override IPermission CreatePermission()
		{
			return new SqlClientPermission(this);
		}
	}
}
