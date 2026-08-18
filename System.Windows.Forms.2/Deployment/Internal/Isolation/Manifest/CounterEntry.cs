using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020000E4 RID: 228
	[StructLayout(LayoutKind.Sequential)]
	internal class CounterEntry
	{
		// Token: 0x040003A1 RID: 929
		public Guid CounterSetGuid;

		// Token: 0x040003A2 RID: 930
		public uint CounterId;

		// Token: 0x040003A3 RID: 931
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Name;

		// Token: 0x040003A4 RID: 932
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Description;

		// Token: 0x040003A5 RID: 933
		public uint CounterType;

		// Token: 0x040003A6 RID: 934
		public ulong Attributes;

		// Token: 0x040003A7 RID: 935
		public uint BaseId;

		// Token: 0x040003A8 RID: 936
		public uint DefaultScale;
	}
}
