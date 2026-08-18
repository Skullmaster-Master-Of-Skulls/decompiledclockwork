using System;
using System.ComponentModel;
using System.Data.Common;
using System.Security;
using System.Security.Permissions;

namespace System.Data.OleDb
{
	// Token: 0x0200025C RID: 604
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class OleDbPermissionAttribute : DBDataPermissionAttribute
	{
		// Token: 0x0600264D RID: 9805 RVA: 0x00103870 File Offset: 0x00102C70
		public OleDbPermissionAttribute(SecurityAction action) : base(action)
		{
		}

		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x0600264E RID: 9806 RVA: 0x00103884 File Offset: 0x00102C84
		// (set) Token: 0x0600264F RID: 9807 RVA: 0x001038A4 File Offset: 0x00102CA4
		[Browsable(false)]
		[Obsolete("Provider property has been deprecated.  Use the Add method.  http://go.microsoft.com/fwlink/?linkid=14202")]
		[EditorBrowsable(EditorBrowsableState.Never)]
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

		// Token: 0x06002650 RID: 9808 RVA: 0x001038B8 File Offset: 0x00102CB8
		public override IPermission CreatePermission()
		{
			return new OleDbPermission(this);
		}

		// Token: 0x0400176D RID: 5997
		private string _providers;
	}
}
