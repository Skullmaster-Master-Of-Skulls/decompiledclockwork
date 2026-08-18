using System;
using System.ComponentModel;
using System.Data.Common;
using System.Security;
using System.Security.Permissions;

namespace System.Data.OleDb
{
	// Token: 0x02000236 RID: 566
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class OleDbPermissionAttribute : DBDataPermissionAttribute
	{
		// Token: 0x0600203B RID: 8251 RVA: 0x0027EE88 File Offset: 0x0027E288
		public OleDbPermissionAttribute(SecurityAction action) : base(action)
		{
		}

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x0600203C RID: 8252 RVA: 0x0027EEA8 File Offset: 0x0027E2A8
		// (set) Token: 0x0600203D RID: 8253 RVA: 0x0027EEC8 File Offset: 0x0027E2C8
		[Obsolete("Provider property has been deprecated.  Use the Add method.  http://go.microsoft.com/fwlink/?linkid=14202")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public string Provider
		{
			get
			{
				string providers = this._providers;
				if (providers == null)
				{
					return ADP.StrEmpty;
				}
				return providers;
			}
			set
			{
				this._providers = value;
			}
		}

		// Token: 0x0600203E RID: 8254 RVA: 0x0027EEE8 File Offset: 0x0027E2E8
		public override IPermission CreatePermission()
		{
			return new OleDbPermission(this);
		}

		// Token: 0x0400145A RID: 5210
		private string _providers;
	}
}
