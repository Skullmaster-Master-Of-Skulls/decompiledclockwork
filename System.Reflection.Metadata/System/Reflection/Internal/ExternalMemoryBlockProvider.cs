using System;
using System.IO;

namespace System.Reflection.Internal
{
	// Token: 0x02000156 RID: 342
	internal sealed class ExternalMemoryBlockProvider : MemoryBlockProvider
	{
		// Token: 0x06000AB5 RID: 2741 RVA: 0x0001E99B File Offset: 0x0001CB9B
		public unsafe ExternalMemoryBlockProvider(byte* memory, int size)
		{
			this._memory = memory;
			this._size = size;
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000AB6 RID: 2742 RVA: 0x0001E9B1 File Offset: 0x0001CBB1
		public override int Size
		{
			get
			{
				return this._size;
			}
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x0001E9B9 File Offset: 0x0001CBB9
		protected override AbstractMemoryBlock GetMemoryBlockImpl(int start, int size)
		{
			return new ExternalMemoryBlock(this, this._memory + start, size);
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x0001E9CA File Offset: 0x0001CBCA
		public override Stream GetStream(out StreamConstraints constraints)
		{
			constraints = new StreamConstraints(null, 0L, this._size);
			return new ReadOnlyUnmanagedMemoryStream(this._memory, this._size);
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x0001E9F1 File Offset: 0x0001CBF1
		protected override void Dispose(bool disposing)
		{
			this._memory = null;
			this._size = 0;
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000ABA RID: 2746 RVA: 0x0001EA02 File Offset: 0x0001CC02
		public unsafe byte* Pointer
		{
			get
			{
				return this._memory;
			}
		}

		// Token: 0x040008F5 RID: 2293
		private unsafe byte* _memory;

		// Token: 0x040008F6 RID: 2294
		private int _size;
	}
}
