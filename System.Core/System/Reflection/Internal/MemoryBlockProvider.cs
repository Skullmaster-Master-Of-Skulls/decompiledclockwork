using System;
using System.IO;

namespace System.Reflection.Internal
{
	// Token: 0x0200007E RID: 126
	internal abstract class MemoryBlockProvider : IDisposable
	{
		// Token: 0x06000320 RID: 800 RVA: 0x00007E2A File Offset: 0x0000602A
		public AbstractMemoryBlock GetMemoryBlock()
		{
			return this.GetMemoryBlockImpl(0, this.Size);
		}

		// Token: 0x06000321 RID: 801 RVA: 0x00007E39 File Offset: 0x00006039
		public AbstractMemoryBlock GetMemoryBlock(int start, int size)
		{
			if ((ulong)start + (ulong)size > (ulong)((long)this.Size))
			{
				Throw.ImageTooSmallOrContainsInvalidOffsetOrCount();
			}
			return this.GetMemoryBlockImpl(start, size);
		}

		// Token: 0x06000322 RID: 802
		protected abstract AbstractMemoryBlock GetMemoryBlockImpl(int start, int size);

		// Token: 0x06000323 RID: 803
		public abstract Stream GetStream(out StreamConstraints constraints);

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000324 RID: 804
		public abstract int Size { get; }

		// Token: 0x06000325 RID: 805
		protected abstract void Dispose(bool disposing);

		// Token: 0x06000326 RID: 806 RVA: 0x00007E56 File Offset: 0x00006056
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
}
