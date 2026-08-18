using System;
using System.ComponentModel;
using System.Data.Common;
using System.Security;
using System.Security.Permissions;

namespace System.Data.OleDb
{
	// Token: 0x02000235 RID: 565
	[Serializable]
	public sealed class OleDbPermission : DBDataPermission
	{
		// Token: 0x06002032 RID: 8242 RVA: 0x0027ECF8 File Offset: 0x0027E0F8
		[Obsolete("OleDbPermission() has been deprecated.  Use the OleDbPermission(PermissionState.None) constructor.  http://go.microsoft.com/fwlink/?linkid=14202", true)]
		public OleDbPermission() : this(PermissionState.None)
		{
		}

		// Token: 0x06002033 RID: 8243 RVA: 0x0027ED18 File Offset: 0x0027E118
		public OleDbPermission(PermissionState state) : base(state)
		{
		}

		// Token: 0x06002034 RID: 8244 RVA: 0x0027ED38 File Offset: 0x0027E138
		[Obsolete("OleDbPermission(PermissionState state, Boolean allowBlankPassword) has been deprecated.  Use the OleDbPermission(PermissionState.None) constructor.  http://go.microsoft.com/fwlink/?linkid=14202", true)]
		public OleDbPermission(PermissionState state, bool allowBlankPassword) : this(state)
		{
			base.AllowBlankPassword = allowBlankPassword;
		}

		// Token: 0x06002035 RID: 8245 RVA: 0x0027ED58 File Offset: 0x0027E158
		private OleDbPermission(OleDbPermission permission) : base(permission)
		{
		}

		// Token: 0x06002036 RID: 8246 RVA: 0x0027ED78 File Offset: 0x0027E178
		internal OleDbPermission(OleDbPermissionAttribute permissionAttribute) : base(permissionAttribute)
		{
		}

		// Token: 0x06002037 RID: 8247 RVA: 0x0027ED98 File Offset: 0x0027E198
		internal OleDbPermission(OleDbConnectionString constr) : base(constr)
		{
			if (constr == null || constr.IsEmpty)
			{
				base.Add(ADP.StrEmpty, ADP.StrEmpty, KeyRestrictionBehavior.AllowOnly);
			}
		}

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x06002038 RID: 8248 RVA: 0x0027EDC8 File Offset: 0x0027E1C8
		// (set) Token: 0x06002039 RID: 8249 RVA: 0x0027EE28 File Offset: 0x0027E228
		[Browsable(false)]
		[Obsolete("Provider property has been deprecated.  Use the Add method.  http://go.microsoft.com/fwlink/?linkid=14202")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public string Provider
		{
			get
			{
				string text = this._providers;
				if (text == null)
				{
					string[] providerRestriction = this._providerRestriction;
					if (providerRestriction != null && 0 < providerRestriction.Length)
					{
						text = providerRestriction[0];
						for (int i = 1; i < providerRestriction.Length; i++)
						{
							text = text + ";" + providerRestriction[i];
						}
					}
				}
				if (text == null)
				{
					return ADP.StrEmpty;
				}
				return text;
			}
			set
			{
				string[] array = null;
				if (!ADP.IsEmpty(value))
				{
					array = value.Split(new char[]
					{
						';'
					});
					array = DBConnectionString.RemoveDuplicates(array);
				}
				this._providerRestriction = array;
				this._providers = value;
			}
		}

		// Token: 0x0600203A RID: 8250 RVA: 0x0027EE68 File Offset: 0x0027E268
		public override IPermission Copy()
		{
			return new OleDbPermission(this);
		}

		// Token: 0x04001458 RID: 5208
		private string[] _providerRestriction;

		// Token: 0x04001459 RID: 5209
		private string _providers;
	}
}
