using System;
using System.Runtime.InteropServices;

namespace System.IdentityModel
{
	// Token: 0x02000056 RID: 86
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal struct PLAINTEXTKEYBLOBHEADER
	{
		// Token: 0x040002E7 RID: 743
		internal byte bType;

		// Token: 0x040002E8 RID: 744
		internal byte bVersion;

		// Token: 0x040002E9 RID: 745
		internal short reserved;

		// Token: 0x040002EA RID: 746
		internal int aiKeyAlg;

		// Token: 0x040002EB RID: 747
		internal int keyLength;

		// Token: 0x040002EC RID: 748
		internal static readonly int SizeOf = Marshal.SizeOf(typeof(PLAINTEXTKEYBLOBHEADER));
	}
}
