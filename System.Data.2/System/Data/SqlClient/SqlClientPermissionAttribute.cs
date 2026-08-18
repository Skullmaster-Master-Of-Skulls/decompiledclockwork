using System;
using System.Data.Common;
using System.Security;
using System.Security.Permissions;

namespace System.Data.SqlClient
{
	// Token: 0x020001B1 RID: 433
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class SqlClientPermissionAttribute : DBDataPermissionAttribute
	{
		// Token: 0x0600193D RID: 6461 RVA: 0x000B1F8C File Offset: 0x000B138C
		public SqlClientPermissionAttribute(SecurityAction action) : base(action)
		{
		}

		// Token: 0x0600193E RID: 6462 RVA: 0x000B1FA0 File Offset: 0x000B13A0
		public override IPermission CreatePermission()
		{
			return new SqlClientPermission(this);
		}
	}
}
