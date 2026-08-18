using System;
using System.ComponentModel;
using System.Data.Common;
using System.Security;
using System.Security.Permissions;

namespace System.Data.OleDb
{
	// Token: 0x0200025B RID: 603
	[Serializable]
	public sealed class OleDbPermission : DBDataPermission
	{
		// Token: 0x06002644 RID: 9796 RVA: 0x00103730 File Offset: 0x00102B30
		[Obsolete("OleDbPermission() has been deprecated.  Use the OleDbPermission(PermissionState.None) constructor.  http://go.microsoft.com/fwlink/?linkid=14202", true)]
		public OleDbPermission() : this(PermissionState.None)
		{
		}

		// Token: 0x06002645 RID: 9797 RVA: 0x00103744 File Offset: 0x00102B44
		public OleDbPermission(PermissionState state) : base(state)
		{
		}

		// Token: 0x06002646 RID: 9798 RVA: 0x00103758 File Offset: 0x00102B58
		[Obsolete("OleDbPermission(PermissionState state, Boolean allowBlankPassword) has been deprecated.  Use the OleDbPermission(PermissionState.None) constructor.  http://go.microsoft.com/fwlink/?linkid=14202", true)]
		public OleDbPermission(PermissionState state, bool allowBlankPassword) : this(state)
		{
			base.AllowBlankPassword = allowBlankPassword;
		}

		// Token: 0x06002647 RID: 9799 RVA: 0x00103774 File Offset: 0x00102B74
		private OleDbPermission(OleDbPermission permission) : base(permission)
		{
		}

		// Token: 0x06002648 RID: 9800 RVA: 0x00103788 File Offset: 0x00102B88
		internal OleDbPermission(OleDbPermissionAttribute permissionAttribute) : base(permissionAttribute)
		{
		}

		// Token: 0x06002649 RID: 9801 RVA: 0x0010379C File Offset: 0x00102B9C
		internal OleDbPermission(OleDbConnectionString constr) : base(constr)
		{
			if (constr == null || constr.IsEmpty)
			{
				base.Add(ADP.StrEmpty, ADP.StrEmpty, KeyRestrictionBehavior.AllowOnly);
			}
		}

		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x0600264A RID: 9802 RVA: 0x001037CC File Offset: 0x00102BCC
		// (set) Token: 0x0600264B RID: 9803 RVA: 0x0010381C File Offset: 0x00102C1C
		[Obsolete("Provider property has been deprecated.  Use the Add method.  http://go.microsoft.com/fwlink/?linkid=14202")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public string Provider
		{
			get
			{
				string text = this._providers;
				if (text == null)
				{
					string[] providerRestriction = this._providerRestriction;
					if (providerRestriction != null && providerRestriction.Length != 0)
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

		// Token: 0x0600264C RID: 9804 RVA: 0x0010385C File Offset: 0x00102C5C
		public override IPermission Copy()
		{
			return new OleDbPermission(this);
		}

		// Token: 0x0400176B RID: 5995
		private string[] _providerRestriction;

		// Token: 0x0400176C RID: 5996
		private string _providers;
	}
}
