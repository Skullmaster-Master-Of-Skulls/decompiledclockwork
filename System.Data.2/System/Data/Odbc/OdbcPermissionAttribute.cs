using System;
using System.Data.Common;
using System.Security;
using System.Security.Permissions;

namespace System.Data.Odbc
{
	// Token: 0x020002A9 RID: 681
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class OdbcPermissionAttribute : DBDataPermissionAttribute
	{
		// Token: 0x060029A4 RID: 10660 RVA: 0x00114A6C File Offset: 0x00113E6C
		public OdbcPermissionAttribute(SecurityAction action) : base(action)
		{
		}

		// Token: 0x060029A5 RID: 10661 RVA: 0x00114A80 File Offset: 0x00113E80
		public override IPermission CreatePermission()
		{
			return new OdbcPermission(this);
		}
	}
}
