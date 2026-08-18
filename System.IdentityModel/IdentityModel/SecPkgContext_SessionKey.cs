using System;
using System.Runtime.InteropServices;

namespace System.IdentityModel
{
	// Token: 0x020000A2 RID: 162
	internal struct SecPkgContext_SessionKey
	{
		// Token: 0x0400049D RID: 1181
		internal uint SessionKeyLength;

		// Token: 0x0400049E RID: 1182
		internal IntPtr Sessionkey;

		// Token: 0x0400049F RID: 1183
		internal static readonly int Size = Marshal.SizeOf(typeof(SecPkgContext_SessionKey));

		// Token: 0x040004A0 RID: 1184
		internal static readonly int SessionkeyOffset = (int)Marshal.OffsetOf(typeof(SecPkgContext_SessionKey), "Sessionkey");
	}
}
