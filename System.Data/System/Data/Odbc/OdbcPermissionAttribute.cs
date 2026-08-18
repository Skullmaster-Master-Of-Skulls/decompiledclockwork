using System;
using System.Data.Common;
using System.Security;
using System.Security.Permissions;

namespace System.Data.Odbc
{
	// Token: 0x020001FB RID: 507
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class OdbcPermissionAttribute : DBDataPermissionAttribute
	{
		// Token: 0x06001C52 RID: 7250 RVA: 0x00268C08 File Offset: 0x00268008
		public OdbcPermissionAttribute(SecurityAction action) : base(action)
		{
		}

		// Token: 0x06001C53 RID: 7251 RVA: 0x00268C28 File Offset: 0x00268028
		public override IPermission CreatePermission()
		{
			return new OdbcPermission(this);
		}
	}
}
