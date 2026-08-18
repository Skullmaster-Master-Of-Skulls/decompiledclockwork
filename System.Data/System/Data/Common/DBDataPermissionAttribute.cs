using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Data.Common
{
	// Token: 0x02000135 RID: 309
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public abstract class DBDataPermissionAttribute : CodeAccessSecurityAttribute
	{
		// Token: 0x06001471 RID: 5233 RVA: 0x00240BC8 File Offset: 0x0023FFC8
		protected DBDataPermissionAttribute(SecurityAction action) : base(action)
		{
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06001472 RID: 5234 RVA: 0x00240BE8 File Offset: 0x0023FFE8
		// (set) Token: 0x06001473 RID: 5235 RVA: 0x00240C08 File Offset: 0x00240008
		public bool AllowBlankPassword
		{
			get
			{
				return this._allowBlankPassword;
			}
			set
			{
				this._allowBlankPassword = value;
			}
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06001474 RID: 5236 RVA: 0x00240C28 File Offset: 0x00240028
		// (set) Token: 0x06001475 RID: 5237 RVA: 0x00240C48 File Offset: 0x00240048
		public string ConnectionString
		{
			get
			{
				string connectionString = this._connectionString;
				if (connectionString == null)
				{
					return string.Empty;
				}
				return connectionString;
			}
			set
			{
				this._connectionString = value;
			}
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06001476 RID: 5238 RVA: 0x00240C68 File Offset: 0x00240068
		// (set) Token: 0x06001477 RID: 5239 RVA: 0x00240C88 File Offset: 0x00240088
		public KeyRestrictionBehavior KeyRestrictionBehavior
		{
			get
			{
				return this._behavior;
			}
			set
			{
				switch (value)
				{
				case KeyRestrictionBehavior.AllowOnly:
				case KeyRestrictionBehavior.PreventUsage:
					this._behavior = value;
					return;
				default:
					throw ADP.InvalidKeyRestrictionBehavior(value);
				}
			}
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06001478 RID: 5240 RVA: 0x00240CB8 File Offset: 0x002400B8
		// (set) Token: 0x06001479 RID: 5241 RVA: 0x00240CD8 File Offset: 0x002400D8
		public string KeyRestrictions
		{
			get
			{
				string restrictions = this._restrictions;
				if (restrictions == null)
				{
					return ADP.StrEmpty;
				}
				return restrictions;
			}
			set
			{
				this._restrictions = value;
			}
		}

		// Token: 0x0600147A RID: 5242 RVA: 0x00240CF8 File Offset: 0x002400F8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeConnectionString()
		{
			return null != this._connectionString;
		}

		// Token: 0x0600147B RID: 5243 RVA: 0x00240D18 File Offset: 0x00240118
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeKeyRestrictions()
		{
			return null != this._restrictions;
		}

		// Token: 0x04000C50 RID: 3152
		private bool _allowBlankPassword;

		// Token: 0x04000C51 RID: 3153
		private string _connectionString;

		// Token: 0x04000C52 RID: 3154
		private string _restrictions;

		// Token: 0x04000C53 RID: 3155
		private KeyRestrictionBehavior _behavior;
	}
}
