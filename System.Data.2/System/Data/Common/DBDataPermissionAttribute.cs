using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Data.Common
{
	// Token: 0x020002EE RID: 750
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public abstract class DBDataPermissionAttribute : CodeAccessSecurityAttribute
	{
		// Token: 0x06002FB9 RID: 12217 RVA: 0x0012DAA4 File Offset: 0x0012CEA4
		protected DBDataPermissionAttribute(SecurityAction action) : base(action)
		{
		}

		// Token: 0x170007C1 RID: 1985
		// (get) Token: 0x06002FBA RID: 12218 RVA: 0x0012DAB8 File Offset: 0x0012CEB8
		// (set) Token: 0x06002FBB RID: 12219 RVA: 0x0012DACC File Offset: 0x0012CECC
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

		// Token: 0x170007C2 RID: 1986
		// (get) Token: 0x06002FBC RID: 12220 RVA: 0x0012DAE0 File Offset: 0x0012CEE0
		// (set) Token: 0x06002FBD RID: 12221 RVA: 0x0012DB00 File Offset: 0x0012CF00
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

		// Token: 0x170007C3 RID: 1987
		// (get) Token: 0x06002FBE RID: 12222 RVA: 0x0012DB14 File Offset: 0x0012CF14
		// (set) Token: 0x06002FBF RID: 12223 RVA: 0x0012DB28 File Offset: 0x0012CF28
		public KeyRestrictionBehavior KeyRestrictionBehavior
		{
			get
			{
				return this._behavior;
			}
			set
			{
				if (value <= KeyRestrictionBehavior.PreventUsage)
				{
					this._behavior = value;
					return;
				}
				throw ADP.InvalidKeyRestrictionBehavior(value);
			}
		}

		// Token: 0x170007C4 RID: 1988
		// (get) Token: 0x06002FC0 RID: 12224 RVA: 0x0012DB48 File Offset: 0x0012CF48
		// (set) Token: 0x06002FC1 RID: 12225 RVA: 0x0012DB68 File Offset: 0x0012CF68
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

		// Token: 0x06002FC2 RID: 12226 RVA: 0x0012DB7C File Offset: 0x0012CF7C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeConnectionString()
		{
			return this._connectionString != null;
		}

		// Token: 0x06002FC3 RID: 12227 RVA: 0x0012DB94 File Offset: 0x0012CF94
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeKeyRestrictions()
		{
			return this._restrictions != null;
		}

		// Token: 0x04001D2A RID: 7466
		private bool _allowBlankPassword;

		// Token: 0x04001D2B RID: 7467
		private string _connectionString;

		// Token: 0x04001D2C RID: 7468
		private string _restrictions;

		// Token: 0x04001D2D RID: 7469
		private KeyRestrictionBehavior _behavior;
	}
}
