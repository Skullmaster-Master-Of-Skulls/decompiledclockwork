using System;
using System.Collections.Immutable;

namespace System.Reflection.Internal
{
	// Token: 0x02000155 RID: 341
	internal sealed class ExternalMemoryBlock : AbstractMemoryBlock
	{
		// Token: 0x06000AB0 RID: 2736 RVA: 0x0001E93B File Offset: 0x0001CB3B
		public unsafe ExternalMemoryBlock(object memoryOwner, byte* buffer, int size)
		{
			this._memoryOwner = memoryOwner;
			this._buffer = buffer;
			this._size = size;
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x0001E958 File Offset: 0x0001CB58
		protected override void Dispose(bool disposing)
		{
			this._buffer = null;
			this._size = 0;
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000AB2 RID: 2738 RVA: 0x0001E969 File Offset: 0x0001CB69
		public unsafe override byte* Pointer
		{
			get
			{
				return this._buffer;
			}
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000AB3 RID: 2739 RVA: 0x0001E971 File Offset: 0x0001CB71
		public override int Size
		{
			get
			{
				return this._size;
			}
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x0001E979 File Offset: 0x0001CB79
		public override ImmutableArray<byte> GetContent(int offset)
		{
			ImmutableArray<byte> result = AbstractMemoryBlock.CreateImmutableArray(this._buffer + offset, this._size - offset);
			GC.KeepAlive(this._memoryOwner);
			return result;
		}

		// Token: 0x040008F2 RID: 2290
		private readonly object _memoryOwner;

		// Token: 0x040008F3 RID: 2291
		private unsafe byte* _buffer;

		// Token: 0x040008F4 RID: 2292
		private int _size;
	}
}
