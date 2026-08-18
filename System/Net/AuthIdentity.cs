using System;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x020004F9 RID: 1273
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
	internal struct AuthIdentity
	{
		// Token: 0x060027E3 RID: 10211 RVA: 0x000A49EC File Offset: 0x000A39EC
		internal AuthIdentity(string userName, string password, string domain)
		{
			this.UserName = userName;
			this.UserNameLength = ((userName == null) ? 0 : userName.Length);
			this.Password = password;
			this.PasswordLength = ((password == null) ? 0 : password.Length);
			this.Domain = domain;
			this.DomainLength = ((domain == null) ? 0 : domain.Length);
			this.Flags = (ComNetOS.IsWin9x ? 1 : 2);
		}

		// Token: 0x060027E4 RID: 10212 RVA: 0x000A4A55 File Offset: 0x000A3A55
		public override string ToString()
		{
			return ValidationHelper.ToString(this.Domain) + "\\" + ValidationHelper.ToString(this.UserName);
		}

		// Token: 0x04002710 RID: 10000
		internal string UserName;

		// Token: 0x04002711 RID: 10001
		internal int UserNameLength;

		// Token: 0x04002712 RID: 10002
		internal string Domain;

		// Token: 0x04002713 RID: 10003
		internal int DomainLength;

		// Token: 0x04002714 RID: 10004
		internal string Password;

		// Token: 0x04002715 RID: 10005
		internal int PasswordLength;

		// Token: 0x04002716 RID: 10006
		internal int Flags;
	}
}
