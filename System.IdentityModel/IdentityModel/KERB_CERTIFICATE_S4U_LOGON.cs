using System;
using System.Runtime.InteropServices;

namespace System.IdentityModel
{
	// Token: 0x0200005B RID: 91
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal struct KERB_CERTIFICATE_S4U_LOGON
	{
		// Token: 0x040002F7 RID: 759
		internal KERB_LOGON_SUBMIT_TYPE MessageType;

		// Token: 0x040002F8 RID: 760
		internal uint Flags;

		// Token: 0x040002F9 RID: 761
		internal UNICODE_INTPTR_STRING UserPrincipalName;

		// Token: 0x040002FA RID: 762
		internal UNICODE_INTPTR_STRING DomainName;

		// Token: 0x040002FB RID: 763
		internal uint CertificateLength;

		// Token: 0x040002FC RID: 764
		internal IntPtr Certificate;

		// Token: 0x040002FD RID: 765
		internal static int Size = Marshal.SizeOf(typeof(KERB_CERTIFICATE_S4U_LOGON));
	}
}
