using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000022 RID: 34
	internal struct STORE_ASSEMBLY
	{
		// Token: 0x04000114 RID: 276
		public uint Status;

		// Token: 0x04000115 RID: 277
		public IDefinitionIdentity DefinitionIdentity;

		// Token: 0x04000116 RID: 278
		[MarshalAs(UnmanagedType.LPWStr)]
		public string ManifestPath;

		// Token: 0x04000117 RID: 279
		public ulong AssemblySize;

		// Token: 0x04000118 RID: 280
		public ulong ChangeId;
	}
}
