using System;
using System.Runtime.InteropServices;

namespace System.IdentityModel
{
	// Token: 0x02000053 RID: 83
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal class SEC_WINNT_AUTH_IDENTITY_EX
	{
		// Token: 0x040002D7 RID: 727
		public uint Version;

		// Token: 0x040002D8 RID: 728
		public uint Length;

		// Token: 0x040002D9 RID: 729
		public string User;

		// Token: 0x040002DA RID: 730
		public uint UserLength;

		// Token: 0x040002DB RID: 731
		public string Domain;

		// Token: 0x040002DC RID: 732
		public uint DomainLength;

		// Token: 0x040002DD RID: 733
		public string Password;

		// Token: 0x040002DE RID: 734
		public uint PasswordLength;

		// Token: 0x040002DF RID: 735
		public uint Flags;

		// Token: 0x040002E0 RID: 736
		public string PackageList;

		// Token: 0x040002E1 RID: 737
		public uint PackageListLength;
	}
}
