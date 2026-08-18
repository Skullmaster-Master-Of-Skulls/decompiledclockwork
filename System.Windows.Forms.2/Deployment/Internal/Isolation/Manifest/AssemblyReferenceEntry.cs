using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020000AB RID: 171
	[StructLayout(LayoutKind.Sequential)]
	internal class AssemblyReferenceEntry
	{
		// Token: 0x040002D4 RID: 724
		public IReferenceIdentity ReferenceIdentity;

		// Token: 0x040002D5 RID: 725
		public uint Flags;

		// Token: 0x040002D6 RID: 726
		public AssemblyReferenceDependentAssemblyEntry DependentAssembly;
	}
}
