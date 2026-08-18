using System;

namespace System.Web.Security
{
	// Token: 0x020005F1 RID: 1521
	public sealed class RoleManagerEventArgs : EventArgs
	{
		// Token: 0x17001698 RID: 5784
		// (get) Token: 0x06004CBD RID: 19645 RVA: 0x001065E9 File Offset: 0x001047E9
		// (set) Token: 0x06004CBE RID: 19646 RVA: 0x001065F1 File Offset: 0x001047F1
		public bool RolesPopulated
		{
			get
			{
				return this._RolesPopulated;
			}
			set
			{
				this._RolesPopulated = value;
			}
		}

		// Token: 0x17001699 RID: 5785
		// (get) Token: 0x06004CBF RID: 19647 RVA: 0x001065FA File Offset: 0x001047FA
		public HttpContext Context
		{
			get
			{
				return this._Context;
			}
		}

		// Token: 0x06004CC0 RID: 19648 RVA: 0x00106602 File Offset: 0x00104802
		public RoleManagerEventArgs(HttpContext context)
		{
			this._Context = context;
		}

		// Token: 0x04002914 RID: 10516
		private HttpContext _Context;

		// Token: 0x04002915 RID: 10517
		private bool _RolesPopulated;
	}
}
