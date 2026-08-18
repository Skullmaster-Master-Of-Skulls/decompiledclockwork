using System;

namespace System.Web
{
	// Token: 0x0200003D RID: 61
	internal interface IAllocatorProvider
	{
		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000501 RID: 1281
		IBufferAllocator<char> CharBufferAllocator { get; }

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000502 RID: 1282
		IBufferAllocator<int> IntBufferAllocator { get; }

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000503 RID: 1283
		IBufferAllocator<IntPtr> IntPtrBufferAllocator { get; }

		// Token: 0x06000504 RID: 1284
		void TrimMemory();
	}
}
