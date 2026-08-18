using System;
using System.Data.Common;
using System.Security;
using System.Security.Permissions;

namespace System.Data.Odbc
{
	// Token: 0x020002A8 RID: 680
	[Serializable]
	public sealed class OdbcPermission : DBDataPermission
	{
		// Token: 0x0600299C RID: 10652 RVA: 0x0011499C File Offset: 0x00113D9C
		[Obsolete("OdbcPermission() has been deprecated.  Use the OdbcPermission(PermissionState.None) constructor.  http://go.microsoft.com/fwlink/?linkid=14202", true)]
		public OdbcPermission() : this(PermissionState.None)
		{
		}

		// Token: 0x0600299D RID: 10653 RVA: 0x001149B0 File Offset: 0x00113DB0
		public OdbcPermission(PermissionState state) : base(state)
		{
		}

		// Token: 0x0600299E RID: 10654 RVA: 0x001149C4 File Offset: 0x00113DC4
		[Obsolete("OdbcPermission(PermissionState state, Boolean allowBlankPassword) has been deprecated.  Use the OdbcPermission(PermissionState.None) constructor.  http://go.microsoft.com/fwlink/?linkid=14202", true)]
		public OdbcPermission(PermissionState state, bool allowBlankPassword) : this(state)
		{
			base.AllowBlankPassword = allowBlankPassword;
		}

		// Token: 0x0600299F RID: 10655 RVA: 0x001149E0 File Offset: 0x00113DE0
		private OdbcPermission(OdbcPermission permission) : base(permission)
		{
		}

		// Token: 0x060029A0 RID: 10656 RVA: 0x001149F4 File Offset: 0x00113DF4
		internal OdbcPermission(OdbcPermissionAttribute permissionAttribute) : base(permissionAttribute)
		{
		}

		// Token: 0x060029A1 RID: 10657 RVA: 0x00114A08 File Offset: 0x00113E08
		internal OdbcPermission(OdbcConnectionString constr) : base(constr)
		{
			if (constr == null || constr.IsEmpty)
			{
				base.Add(ADP.StrEmpty, ADP.StrEmpty, KeyRestrictionBehavior.AllowOnly);
			}
		}

		// Token: 0x060029A2 RID: 10658 RVA: 0x00114A38 File Offset: 0x00113E38
		public override void Add(string connectionString, string restrictions, KeyRestrictionBehavior behavior)
		{
			DBConnectionString entry = new DBConnectionString(connectionString, restrictions, behavior, null, true);
			base.AddPermissionEntry(entry);
		}

		// Token: 0x060029A3 RID: 10659 RVA: 0x00114A58 File Offset: 0x00113E58
		public override IPermission Copy()
		{
			return new OdbcPermission(this);
		}
	}
}
