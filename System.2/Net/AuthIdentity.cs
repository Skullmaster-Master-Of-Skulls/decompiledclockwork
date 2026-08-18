using System;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x020001D0 RID: 464
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
	internal struct AuthIdentity
	{
		// Token: 0x06001272 RID: 4722 RVA: 0x000627E0 File Offset: 0x000609E0
		internal AuthIdentity(string userName, string password, string domain)
		{
			this.UserName = userName;
			this.UserNameLength = ((userName == null) ? 0 : userName.Length);
			this.Password = password;
			this.PasswordLength = ((password == null) ? 0 : password.Length);
			this.Domain = domain;
			this.DomainLength = ((domain == null) ? 0 : domain.Length);
			this.Flags = 2;
		}

		// Token: 0x06001273 RID: 4723 RVA: 0x0006283F File Offset: 0x00060A3F
		public override string ToString()
		{
			return ValidationHelper.ToString(this.Domain) + "\\" + ValidationHelper.ToString(this.UserName);
		}

		// Token: 0x040014CF RID: 5327
		internal string UserName;

		// Token: 0x040014D0 RID: 5328
		internal int UserNameLength;

		// Token: 0x040014D1 RID: 5329
		internal string Domain;

		// Token: 0x040014D2 RID: 5330
		internal int DomainLength;

		// Token: 0x040014D3 RID: 5331
		internal string Password;

		// Token: 0x040014D4 RID: 5332
		internal int PasswordLength;

		// Token: 0x040014D5 RID: 5333
		internal int Flags;
	}
}
