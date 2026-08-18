using System;
using System.Collections.Immutable;
using System.Runtime.InteropServices;

namespace System.Reflection.Internal
{
	// Token: 0x02000158 RID: 344
	internal sealed class MemoryMappedFileBlock : AbstractMemoryBlock
	{
		// Token: 0x06000AC3 RID: 2755 RVA: 0x0001EA45 File Offset: 0x0001CC45
		internal unsafe MemoryMappedFileBlock(IDisposable accessor, SafeBuffer safeBuffer, byte* pointer, int size)
		{
			this._accessor = accessor;
			this._safeBuffer = safeBuffer;
			this._pointer = pointer;
			this._size = size;
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x0001EA6C File Offset: 0x0001CC6C
		~MemoryMappedFileBlock()
		{
			this.Dispose(false);
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x0001EA9C File Offset: 0x0001CC9C
		protected override void Dispose(bool disposing)
		{
			if (this._safeBuffer != null)
			{
				this._safeBuffer.ReleasePointer();
				this._safeBuffer = null;
			}
			if (this._accessor != null)
			{
				this._accessor.Dispose();
				this._accessor = null;
			}
			this._pointer = null;
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000AC6 RID: 2758 RVA: 0x0001EADA File Offset: 0x0001CCDA
		public unsafe override byte* Pointer
		{
			get
			{
				return this._pointer;
			}
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000AC7 RID: 2759 RVA: 0x0001EAE2 File Offset: 0x0001CCE2
		public override int Size
		{
			get
			{
				return this._size;
			}
		}

		// Token: 0x06000AC8 RID: 2760 RVA: 0x0001EAEA File Offset: 0x0001CCEA
		public override ImmutableArray<byte> GetContent(int offset)
		{
			ImmutableArray<byte> result = AbstractMemoryBlock.CreateImmutableArray(this.Pointer + offset, this.Size - offset);
			GC.KeepAlive(this);
			return result;
		}

		// Token: 0x040008F7 RID: 2295
		private readonly int _size;

		// Token: 0x040008F8 RID: 2296
		private IDisposable _accessor;

		// Token: 0x040008F9 RID: 2297
		private unsafe byte* _pointer;

		// Token: 0x040008FA RID: 2298
		private SafeBuffer _safeBuffer;
	}
}
