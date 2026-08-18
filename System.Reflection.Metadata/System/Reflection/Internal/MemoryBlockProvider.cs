using System;
using System.IO;

namespace System.Reflection.Internal
{
	// Token: 0x02000157 RID: 343
	internal abstract class MemoryBlockProvider : IDisposable
	{
		// Token: 0x06000ABB RID: 2747 RVA: 0x0001EA0A File Offset: 0x0001CC0A
		public AbstractMemoryBlock GetMemoryBlock()
		{
			return this.GetMemoryBlockImpl(0, this.Size);
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x0001EA19 File Offset: 0x0001CC19
		public AbstractMemoryBlock GetMemoryBlock(int start, int size)
		{
			if ((ulong)start + (ulong)size > (ulong)((long)this.Size))
			{
				Throw.ImageTooSmallOrContainsInvalidOffsetOrCount();
			}
			return this.GetMemoryBlockImpl(start, size);
		}

		// Token: 0x06000ABD RID: 2749
		protected abstract AbstractMemoryBlock GetMemoryBlockImpl(int start, int size);

		// Token: 0x06000ABE RID: 2750
		public abstract Stream GetStream(out StreamConstraints constraints);

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000ABF RID: 2751
		public abstract int Size { get; }

		// Token: 0x06000AC0 RID: 2752
		protected abstract void Dispose(bool disposing);

		// Token: 0x06000AC1 RID: 2753 RVA: 0x0001EA36 File Offset: 0x0001CC36
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
}
