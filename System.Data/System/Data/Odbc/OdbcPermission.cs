using System;
using System.Data.Common;
using System.Security;
using System.Security.Permissions;

namespace System.Data.Odbc
{
	// Token: 0x020001FA RID: 506
	[Serializable]
	public sealed class OdbcPermission : DBDataPermission
	{
		// Token: 0x06001C4A RID: 7242 RVA: 0x00268AF8 File Offset: 0x00267EF8
		[Obsolete("OdbcPermission() has been deprecated.  Use the OdbcPermission(PermissionState.None) constructor.  http://go.microsoft.com/fwlink/?linkid=14202", true)]
		public OdbcPermission() : this(PermissionState.None)
		{
		}

		// Token: 0x06001C4B RID: 7243 RVA: 0x00268B18 File Offset: 0x00267F18
		public OdbcPermission(PermissionState state) : base(state)
		{
		}

		// Token: 0x06001C4C RID: 7244 RVA: 0x00268B38 File Offset: 0x00267F38
		[Obsolete("OdbcPermission(PermissionState state, Boolean allowBlankPassword) has been deprecated.  Use the OdbcPermission(PermissionState.None) constructor.  http://go.microsoft.com/fwlink/?linkid=14202", true)]
		public OdbcPermission(PermissionState state, bool allowBlankPassword) : this(state)
		{
			base.AllowBlankPassword = allowBlankPassword;
		}

		// Token: 0x06001C4D RID: 7245 RVA: 0x00268B58 File Offset: 0x00267F58
		private OdbcPermission(OdbcPermission permission) : base(permission)
		{
		}

		// Token: 0x06001C4E RID: 7246 RVA: 0x00268B78 File Offset: 0x00267F78
		internal OdbcPermission(OdbcPermissionAttribute permissionAttribute) : base(permissionAttribute)
		{
		}

		// Token: 0x06001C4F RID: 7247 RVA: 0x00268B98 File Offset: 0x00267F98
		internal OdbcPermission(OdbcConnectionString constr) : base(constr)
		{
			if (constr == null || constr.IsEmpty)
			{
				base.Add(ADP.StrEmpty, ADP.StrEmpty, KeyRestrictionBehavior.AllowOnly);
			}
		}

		// Token: 0x06001C50 RID: 7248 RVA: 0x00268BC8 File Offset: 0x00267FC8
		public override void Add(string connectionString, string restrictions, KeyRestrictionBehavior behavior)
		{
			DBConnectionString entry = new DBConnectionString(connectionString, restrictions, behavior, null, true);
			base.AddPermissionEntry(entry);
		}

		// Token: 0x06001C51 RID: 7249 RVA: 0x00268BE8 File Offset: 0x00267FE8
		public override IPermission Copy()
		{
			return new OdbcPermission(this);
		}
	}
}
