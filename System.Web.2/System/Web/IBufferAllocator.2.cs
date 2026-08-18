using System;

namespace System.Web
{
	// Token: 0x0200003C RID: 60
	internal interface IBufferAllocator<T> : IBufferAllocator
	{
		// Token: 0x060004FE RID: 1278
		T[] GetBuffer();

		// Token: 0x060004FF RID: 1279
		T[] GetBuffer(int minSize);

		// Token: 0x06000500 RID: 1280
		void ReuseBuffer(T[] buffer);
	}
}
