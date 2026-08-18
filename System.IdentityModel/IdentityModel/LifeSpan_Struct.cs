using System;
using System.Runtime.InteropServices;

namespace System.IdentityModel
{
	// Token: 0x020000A0 RID: 160
	internal struct LifeSpan_Struct
	{
		// Token: 0x04000496 RID: 1174
		internal long start;

		// Token: 0x04000497 RID: 1175
		internal long end;

		// Token: 0x04000498 RID: 1176
		internal static readonly int Size = Marshal.SizeOf(typeof(LifeSpan_Struct));
	}
}
