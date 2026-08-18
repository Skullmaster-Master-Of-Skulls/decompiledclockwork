using System;
using System.Data.Common;
using System.Security;
using System.Security.Permissions;

namespace System.Data.SqlClient
{
	// Token: 0x020002BE RID: 702
	[Serializable]
	public sealed class SqlClientPermission : DBDataPermission
	{
		// Token: 0x06002367 RID: 9063 RVA: 0x00290CC8 File Offset: 0x002900C8
		[Obsolete("SqlClientPermission() has been deprecated.  Use the SqlClientPermission(PermissionState.None) constructor.  http://go.microsoft.com/fwlink/?linkid=14202", true)]
		public SqlClientPermission() : this(PermissionState.None)
		{
		}

		// Token: 0x06002368 RID: 9064 RVA: 0x00290CE8 File Offset: 0x002900E8
		public SqlClientPermission(PermissionState state) : base(state)
		{
		}

		// Token: 0x06002369 RID: 9065 RVA: 0x00290D08 File Offset: 0x00290108
		[Obsolete("SqlClientPermission(PermissionState state, Boolean allowBlankPassword) has been deprecated.  Use the SqlClientPermission(PermissionState.None) constructor.  http://go.microsoft.com/fwlink/?linkid=14202", true)]
		public SqlClientPermission(PermissionState state, bool allowBlankPassword) : this(state)
		{
			base.AllowBlankPassword = allowBlankPassword;
		}

		// Token: 0x0600236A RID: 9066 RVA: 0x00290D28 File Offset: 0x00290128
		private SqlClientPermission(SqlClientPermission permission) : base(permission)
		{
		}

		// Token: 0x0600236B RID: 9067 RVA: 0x00290D48 File Offset: 0x00290148
		internal SqlClientPermission(SqlClientPermissionAttribute permissionAttribute) : base(permissionAttribute)
		{
		}

		// Token: 0x0600236C RID: 9068 RVA: 0x00290D68 File Offset: 0x00290168
		internal SqlClientPermission(SqlConnectionString constr) : base(constr)
		{
			if (constr == null || constr.IsEmpty)
			{
				base.Add(ADP.StrEmpty, ADP.StrEmpty, KeyRestrictionBehavior.AllowOnly);
			}
		}

		// Token: 0x0600236D RID: 9069 RVA: 0x00290D98 File Offset: 0x00290198
		public override void Add(string connectionString, string restrictions, KeyRestrictionBehavior behavior)
		{
			DBConnectionString entry = new DBConnectionString(connectionString, restrictions, behavior, SqlConnectionString.GetParseSynonyms(), false);
			base.AddPermissionEntry(entry);
		}

		// Token: 0x0600236E RID: 9070 RVA: 0x00290DC8 File Offset: 0x002901C8
		public override IPermission Copy()
		{
			return new SqlClientPermission(this);
		}
	}
}
