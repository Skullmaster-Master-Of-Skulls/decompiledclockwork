using System;

namespace System.Web.ClientServices.Providers
{
	// Token: 0x0200010E RID: 270
	public class ClientFormsAuthenticationCredentials
	{
		// Token: 0x06000E04 RID: 3588 RVA: 0x000312C0 File Offset: 0x0002F4C0
		public ClientFormsAuthenticationCredentials(string username, string password, bool rememberMe)
		{
			this._UserName = username;
			this._Password = password;
			this._RememberMe = rememberMe;
		}

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x06000E05 RID: 3589 RVA: 0x000312DD File Offset: 0x0002F4DD
		// (set) Token: 0x06000E06 RID: 3590 RVA: 0x000312E5 File Offset: 0x0002F4E5
		public string UserName
		{
			get
			{
				return this._UserName;
			}
			set
			{
				this._UserName = value;
			}
		}

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x06000E07 RID: 3591 RVA: 0x000312EE File Offset: 0x0002F4EE
		// (set) Token: 0x06000E08 RID: 3592 RVA: 0x000312F6 File Offset: 0x0002F4F6
		public string Password
		{
			get
			{
				return this._Password;
			}
			set
			{
				this._Password = value;
			}
		}

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x06000E09 RID: 3593 RVA: 0x000312FF File Offset: 0x0002F4FF
		// (set) Token: 0x06000E0A RID: 3594 RVA: 0x00031307 File Offset: 0x0002F507
		public bool RememberMe
		{
			get
			{
				return this._RememberMe;
			}
			set
			{
				this._RememberMe = value;
			}
		}

		// Token: 0x040003F6 RID: 1014
		private string _UserName;

		// Token: 0x040003F7 RID: 1015
		private string _Password;

		// Token: 0x040003F8 RID: 1016
		private bool _RememberMe;
	}
}
