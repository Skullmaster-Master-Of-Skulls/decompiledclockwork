using System;
using System.Web.Security;

namespace System.Web.UI.WebControls
{
	// Token: 0x020003A8 RID: 936
	public class CreateUserErrorEventArgs : EventArgs
	{
		// Token: 0x06002C8A RID: 11402 RVA: 0x0009115C File Offset: 0x0008F35C
		public CreateUserErrorEventArgs(MembershipCreateStatus s)
		{
			this._error = s;
		}

		// Token: 0x17000C9D RID: 3229
		// (get) Token: 0x06002C8B RID: 11403 RVA: 0x0009116B File Offset: 0x0008F36B
		// (set) Token: 0x06002C8C RID: 11404 RVA: 0x00091173 File Offset: 0x0008F373
		public MembershipCreateStatus CreateUserError
		{
			get
			{
				return this._error;
			}
			set
			{
				this._error = value;
			}
		}

		// Token: 0x04001F3B RID: 7995
		private MembershipCreateStatus _error;
	}
}
