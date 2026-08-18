using System;

namespace System.Web.ClientServices.Providers
{
	// Token: 0x02000118 RID: 280
	public class UserValidatedEventArgs : EventArgs
	{
		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x06000EB4 RID: 3764 RVA: 0x0003535A File Offset: 0x0003355A
		public string UserName
		{
			get
			{
				return this._UserName;
			}
		}

		// Token: 0x06000EB5 RID: 3765 RVA: 0x00035362 File Offset: 0x00033562
		public UserValidatedEventArgs(string username)
		{
			this._UserName = username;
		}

		// Token: 0x04000426 RID: 1062
		private string _UserName;
	}
}
