using System;
using System.Runtime.InteropServices;

namespace System.IdentityModel
{
	// Token: 0x0200008F RID: 143
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
	internal struct AuthIdentityEx
	{
		// Token: 0x060004D2 RID: 1234 RVA: 0x00011DF0 File Offset: 0x0000FFF0
		internal AuthIdentityEx(string userName, string password, string domain, params string[] additionalPackages)
		{
			this.Version = AuthIdentityEx.WinNTAuthIdentityVersion;
			this.Length = Marshal.SizeOf(typeof(AuthIdentityEx));
			this.UserName = userName;
			this.UserNameLength = ((userName == null) ? 0 : userName.Length);
			this.Password = password;
			this.PasswordLength = ((password == null) ? 0 : password.Length);
			this.Domain = domain;
			this.DomainLength = ((domain == null) ? 0 : domain.Length);
			this.Flags = 2;
			if (additionalPackages == null)
			{
				this.PackageList = null;
				this.PackageListLength = 0;
				return;
			}
			this.PackageList = string.Join(",", additionalPackages);
			this.PackageListLength = this.PackageList.Length;
		}

		// Token: 0x0400042E RID: 1070
		internal int Version;

		// Token: 0x0400042F RID: 1071
		internal int Length;

		// Token: 0x04000430 RID: 1072
		internal string UserName;

		// Token: 0x04000431 RID: 1073
		internal int UserNameLength;

		// Token: 0x04000432 RID: 1074
		internal string Domain;

		// Token: 0x04000433 RID: 1075
		internal int DomainLength;

		// Token: 0x04000434 RID: 1076
		internal string Password;

		// Token: 0x04000435 RID: 1077
		internal int PasswordLength;

		// Token: 0x04000436 RID: 1078
		internal int Flags;

		// Token: 0x04000437 RID: 1079
		internal string PackageList;

		// Token: 0x04000438 RID: 1080
		internal int PackageListLength;

		// Token: 0x04000439 RID: 1081
		private static readonly int WinNTAuthIdentityVersion = 512;
	}
}
