using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.VisualC;

namespace System.EnterpriseServices.Thunk
{
	// Token: 0x02000012 RID: 18
	[MiscellaneousBits(64)]
	[DebugInfoInPDB]
	[NativeCppClass]
	[StructLayout(LayoutKind.Sequential, Size = 32768)]
	internal struct SimpleHashtable<unsigned\u0020__int64,int>
	{
		// Token: 0x040000E3 RID: 227
		private long <alignment\u0020member>;

		// Token: 0x02000013 RID: 19
		[NativeCppClass]
		[DebugInfoInPDB]
		[MiscellaneousBits(64)]
		[StructLayout(LayoutKind.Sequential, Size = 32)]
		internal struct Bucket
		{
			// Token: 0x040000E4 RID: 228
			private long <alignment\u0020member>;
		}
	}
}
