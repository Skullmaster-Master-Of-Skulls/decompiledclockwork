using System;
using System.Runtime.InteropServices;

namespace System.Web.Caching
{
	// Token: 0x0200089B RID: 2203
	[StructLayout(LayoutKind.Explicit)]
	internal struct ExpiresEntry
	{
		// Token: 0x04003561 RID: 13665
		[FieldOffset(0)]
		internal DateTime _utcExpires;

		// Token: 0x04003562 RID: 13666
		[FieldOffset(0)]
		internal ExpiresEntryRef _next;

		// Token: 0x04003563 RID: 13667
		[FieldOffset(4)]
		internal int _cFree;

		// Token: 0x04003564 RID: 13668
		[FieldOffset(8)]
		internal CacheEntry _cacheEntry;
	}
}
