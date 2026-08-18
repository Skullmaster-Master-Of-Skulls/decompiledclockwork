using System;

namespace System.Web.ApplicationServices
{
	// Token: 0x0200011C RID: 284
	public class AuthenticatingEventArgs : EventArgs
	{
		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x06000EDF RID: 3807 RVA: 0x00035DF5 File Offset: 0x00033FF5
		// (set) Token: 0x06000EE0 RID: 3808 RVA: 0x00035DFD File Offset: 0x00033FFD
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

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x06000EE1 RID: 3809 RVA: 0x00035E06 File Offset: 0x00034006
		// (set) Token: 0x06000EE2 RID: 3810 RVA: 0x00035E0E File Offset: 0x0003400E
		public bool AuthenticationIsComplete
		{
			get
			{
				return this._authenticationIsComplete;
			}
			set
			{
				this._authenticationIsComplete = value;
			}
		}

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x06000EE3 RID: 3811 RVA: 0x00035E17 File Offset: 0x00034017
		public string UserName
		{
			get
			{
				return this._userName;
			}
		}

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x06000EE4 RID: 3812 RVA: 0x00035E1F File Offset: 0x0003401F
		public string Password
		{
			get
			{
				return this._password;
			}
		}

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x06000EE5 RID: 3813 RVA: 0x00035E27 File Offset: 0x00034027
		public string CustomCredential
		{
			get
			{
				return this._customCredential;
			}
		}

		// Token: 0x06000EE6 RID: 3814 RVA: 0x00035E2F File Offset: 0x0003402F
		internal AuthenticatingEventArgs(string username, string password, string customCredential)
		{
			this._authenticated = false;
			this._authenticationIsComplete = false;
			this._userName = username;
			this._password = password;
			this._customCredential = customCredential;
		}

		// Token: 0x06000EE7 RID: 3815 RVA: 0x00035E5A File Offset: 0x0003405A
		private AuthenticatingEventArgs()
		{
		}

		// Token: 0x04000430 RID: 1072
		private bool _authenticated;

		// Token: 0x04000431 RID: 1073
		private bool _authenticationIsComplete;

		// Token: 0x04000432 RID: 1074
		private string _userName;

		// Token: 0x04000433 RID: 1075
		private string _password;

		// Token: 0x04000434 RID: 1076
		private string _customCredential;
	}
}
