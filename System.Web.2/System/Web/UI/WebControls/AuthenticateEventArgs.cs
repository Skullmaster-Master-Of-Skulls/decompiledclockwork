using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000376 RID: 886
	public class AuthenticateEventArgs : EventArgs
	{
		// Token: 0x060028DE RID: 10462 RVA: 0x000843A9 File Offset: 0x000825A9
		public AuthenticateEventArgs() : this(false)
		{
		}

		// Token: 0x060028DF RID: 10463 RVA: 0x000843B2 File Offset: 0x000825B2
		public AuthenticateEventArgs(bool authenticated)
		{
			this._authenticated = authenticated;
		}

		// Token: 0x17000B56 RID: 2902
		// (get) Token: 0x060028E0 RID: 10464 RVA: 0x000843C1 File Offset: 0x000825C1
		// (set) Token: 0x060028E1 RID: 10465 RVA: 0x000843C9 File Offset: 0x000825C9
		public bool Authenticated
		{
			get
			{
				return this._authenticated;
			}
			set
			{
				this._authenticated = value;
			}
		}

		// Token: 0x04001E1F RID: 7711
		private bool _authenticated;
	}
}
