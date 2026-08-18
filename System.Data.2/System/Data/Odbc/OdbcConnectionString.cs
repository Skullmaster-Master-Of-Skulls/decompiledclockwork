using System;
using System.Data.Common;
using System.Security;
using System.Security.Permissions;

namespace System.Data.Odbc
{
	// Token: 0x02000295 RID: 661
	internal sealed class OdbcConnectionString : DbConnectionOptions
	{
		// Token: 0x0600283D RID: 10301 RVA: 0x0010CE7C File Offset: 0x0010C27C
		internal OdbcConnectionString(string connectionString, bool validate) : base(connectionString, null, true)
		{
			if (!validate)
			{
				string text = null;
				int num = 0;
				this._expandedConnectionString = base.ExpandDataDirectories(ref text, ref num);
			}
			if ((validate || this._expandedConnectionString == null) && connectionString != null && 1024 < connectionString.Length)
			{
				throw ODBC.ConnectionStringTooLong();
			}
		}

		// Token: 0x0600283E RID: 10302 RVA: 0x0010CECC File Offset: 0x0010C2CC
		protected internal override PermissionSet CreatePermissionSet()
		{
			PermissionSet permissionSet;
			if (base.ContainsKey("savefile"))
			{
				permissionSet = new NamedPermissionSet("FullTrust");
			}
			else
			{
				permissionSet = new PermissionSet(PermissionState.None);
				permissionSet.AddPermission(new OdbcPermission(this));
			}
			return permissionSet;
		}

		// Token: 0x0600283F RID: 10303 RVA: 0x0010CF08 File Offset: 0x0010C308
		protected internal override string Expand()
		{
			if (this._expandedConnectionString != null)
			{
				return this._expandedConnectionString;
			}
			return base.Expand();
		}

		// Token: 0x04001A7F RID: 6783
		private readonly string _expandedConnectionString;
	}
}
