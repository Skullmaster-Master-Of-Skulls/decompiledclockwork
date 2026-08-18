using System;
using System.Data.Common;
using System.Security;
using System.Security.Permissions;

namespace Oracle.DataAccess.Client
{
	// Token: 0x02000073 RID: 115
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class OraclePermissionAttribute : DBDataPermissionAttribute
	{
		// Token: 0x0600052E RID: 1326 RVA: 0x0003ACA2 File Offset: 0x00039CA2
		static OraclePermissionAttribute()
		{
			if (!OracleInit.bSetDllDirectoryInvoked)
			{
				OracleInit.Initialize();
			}
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x0003ACB0 File Offset: 0x00039CB0
		public OraclePermissionAttribute(SecurityAction action) : base(action)
		{
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x0003ACB9 File Offset: 0x00039CB9
		public override IPermission CreatePermission()
		{
			return new OraclePermission(this);
		}
	}
}
