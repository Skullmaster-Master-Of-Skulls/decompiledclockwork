using System;
using System.Runtime.InteropServices;

namespace System.Web.Caching
{
	// Token: 0x020008A2 RID: 2210
	[StructLayout(LayoutKind.Explicit)]
	internal struct UsageEntry
	{
		// Token: 0x0400358F RID: 13711
		[FieldOffset(0)]
		internal UsageEntryLink _ref1;

		// Token: 0x04003590 RID: 13712
		[FieldOffset(4)]
		internal int _cFree;

		// Token: 0x04003591 RID: 13713
		[FieldOffset(8)]
		internal UsageEntryLink _ref2;

		// Token: 0x04003592 RID: 13714
		[FieldOffset(16)]
		internal DateTime _utcDate;

		// Token: 0x04003593 RID: 13715
		[FieldOffset(24)]
		internal CacheEntry _cacheEntry;
	}
}
