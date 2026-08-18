using System;
using System.Collections.Immutable;

namespace System.Reflection.Internal
{
	// Token: 0x02000153 RID: 339
	internal sealed class ByteArrayMemoryBlock : AbstractMemoryBlock
	{
		// Token: 0x06000AA4 RID: 2724 RVA: 0x0001E7D1 File Offset: 0x0001C9D1
		internal ByteArrayMemoryBlock(ByteArrayMemoryProvider provider, int start, int size)
		{
			this._provider = provider;
			this._size = size;
			this._start = start;
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x0001E7EE File Offset: 0x0001C9EE
		protected override void Dispose(bool disposing)
		{
			this._provider = null;
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000AA6 RID: 2726 RVA: 0x0001E7F7 File Offset: 0x0001C9F7
		public unsafe override byte* Pointer
		{
			get
			{
				return this._provider.Pointer + this._start;
			}
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000AA7 RID: 2727 RVA: 0x0001E80B File Offset: 0x0001CA0B
		public override int Size
		{
			get
			{
				return this._size;
			}
		}

		// Token: 0x06000AA8 RID: 2728 RVA: 0x0001E813 File Offset: 0x0001CA13
		public override ImmutableArray<byte> GetContent(int offset)
		{
			return ImmutableArray.Create<byte>(this._provider.array, this._start + offset, this._size - offset);
		}

		// Token: 0x040008ED RID: 2285
		private ByteArrayMemoryProvider _provider;

		// Token: 0x040008EE RID: 2286
		private readonly int _start;

		// Token: 0x040008EF RID: 2287
		private readonly int _size;
	}
}
