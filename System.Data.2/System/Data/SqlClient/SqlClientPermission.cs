using System;
using System.Data.Common;
using System.Security;
using System.Security.Permissions;

namespace System.Data.SqlClient
{
	// Token: 0x020001B0 RID: 432
	[Serializable]
	public sealed class SqlClientPermission : DBDataPermission
	{
		// Token: 0x06001935 RID: 6453 RVA: 0x000B1EB8 File Offset: 0x000B12B8
		[Obsolete("SqlClientPermission() has been deprecated.  Use the SqlClientPermission(PermissionState.None) constructor.  http://go.microsoft.com/fwlink/?linkid=14202", true)]
		public SqlClientPermission() : this(PermissionState.None)
		{
		}

		// Token: 0x06001936 RID: 6454 RVA: 0x000B1ECC File Offset: 0x000B12CC
		public SqlClientPermission(PermissionState state) : base(state)
		{
		}

		// Token: 0x06001937 RID: 6455 RVA: 0x000B1EE0 File Offset: 0x000B12E0
		[Obsolete("SqlClientPermission(PermissionState state, Boolean allowBlankPassword) has been deprecated.  Use the SqlClientPermission(PermissionState.None) constructor.  http://go.microsoft.com/fwlink/?linkid=14202", true)]
		public SqlClientPermission(PermissionState state, bool allowBlankPassword) : this(state)
		{
			base.AllowBlankPassword = allowBlankPassword;
		}

		// Token: 0x06001938 RID: 6456 RVA: 0x000B1EFC File Offset: 0x000B12FC
		private SqlClientPermission(SqlClientPermission permission) : base(permission)
		{
		}

		// Token: 0x06001939 RID: 6457 RVA: 0x000B1F10 File Offset: 0x000B1310
		internal SqlClientPermission(SqlClientPermissionAttribute permissionAttribute) : base(permissionAttribute)
		{
		}

		// Token: 0x0600193A RID: 6458 RVA: 0x000B1F24 File Offset: 0x000B1324
		internal SqlClientPermission(SqlConnectionString constr) : base(constr)
		{
			if (constr == null || constr.IsEmpty)
			{
				base.Add(ADP.StrEmpty, ADP.StrEmpty, KeyRestrictionBehavior.AllowOnly);
			}
		}

		// Token: 0x0600193B RID: 6459 RVA: 0x000B1F54 File Offset: 0x000B1354
		public override void Add(string connectionString, string restrictions, KeyRestrictionBehavior behavior)
		{
			DBConnectionString entry = new DBConnectionString(connectionString, restrictions, behavior, SqlConnectionString.GetParseSynonyms(), false);
			base.AddPermissionEntry(entry);
		}

		// Token: 0x0600193C RID: 6460 RVA: 0x000B1F78 File Offset: 0x000B1378
		public override IPermission Copy()
		{
			return new SqlClientPermission(this);
		}
	}
}
