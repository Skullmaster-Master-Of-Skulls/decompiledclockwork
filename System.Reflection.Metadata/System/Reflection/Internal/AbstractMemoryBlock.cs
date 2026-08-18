using System;
using System.Collections.Immutable;
using System.Runtime.InteropServices;

namespace System.Reflection.Internal
{
	// Token: 0x02000152 RID: 338
	internal abstract class AbstractMemoryBlock : IDisposable
	{
		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000A9C RID: 2716
		public unsafe abstract byte* Pointer { get; }

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000A9D RID: 2717
		public abstract int Size { get; }

		// Token: 0x06000A9E RID: 2718
		public abstract ImmutableArray<byte> GetContent(int offset);

		// Token: 0x06000A9F RID: 2719 RVA: 0x0001E78F File Offset: 0x0001C98F
		public ImmutableArray<byte> GetContent()
		{
			return this.GetContent(0);
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x0001E798 File Offset: 0x0001C998
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000AA1 RID: 2721
		protected abstract void Dispose(bool disposing);

		// Token: 0x06000AA2 RID: 2722 RVA: 0x0001E7A8 File Offset: 0x0001C9A8
		protected unsafe static ImmutableArray<byte> CreateImmutableArray(byte* ptr, int length)
		{
			byte[] destination = new byte[length];
			Marshal.Copy((IntPtr)((void*)ptr), destination, 0, length);
			return ImmutableByteArrayInterop.DangerousCreateFromUnderlyingArray(ref destination);
		}
	}
}
