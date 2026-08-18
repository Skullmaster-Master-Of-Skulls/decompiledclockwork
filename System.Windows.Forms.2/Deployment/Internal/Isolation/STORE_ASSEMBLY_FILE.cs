using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000024 RID: 36
	internal struct STORE_ASSEMBLY_FILE
	{
		// Token: 0x0400011B RID: 283
		public uint Size;

		// Token: 0x0400011C RID: 284
		public uint Flags;

		// Token: 0x0400011D RID: 285
		[MarshalAs(UnmanagedType.LPWStr)]
		public string FileName;

		// Token: 0x0400011E RID: 286
		public uint FileStatusFlags;
	}
}
