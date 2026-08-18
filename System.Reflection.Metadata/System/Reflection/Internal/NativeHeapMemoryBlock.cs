using System;
using System.Collections.Immutable;
using System.Runtime.InteropServices;

namespace System.Reflection.Internal
{
	// Token: 0x02000159 RID: 345
	internal sealed class NativeHeapMemoryBlock : AbstractMemoryBlock
	{
		// Token: 0x06000AC9 RID: 2761 RVA: 0x0001EB07 File Offset: 0x0001CD07
		internal unsafe NativeHeapMemoryBlock(int size)
		{
			this._pointer = (byte*)((void*)Marshal.AllocHGlobal(size));
			this._size = size;
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x0001EB28 File Offset: 0x0001CD28
		~NativeHeapMemoryBlock()
		{
			this.Dispose(false);
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x0001EB58 File Offset: 0x0001CD58
		protected unsafe override void Dispose(bool disposing)
		{
			Marshal.FreeHGlobal((IntPtr)((void*)this._pointer));
			this._pointer = null;
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000ACC RID: 2764 RVA: 0x0001EB72 File Offset: 0x0001CD72
		public unsafe override byte* Pointer
		{
			get
			{
				return this._pointer;
			}
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000ACD RID: 2765 RVA: 0x0001EB7A File Offset: 0x0001CD7A
		public override int Size
		{
			get
			{
				return this._size;
			}
		}

		// Token: 0x06000ACE RID: 2766 RVA: 0x0001EB82 File Offset: 0x0001CD82
		public override ImmutableArray<byte> GetContent(int offset)
		{
			ImmutableArray<byte> result = AbstractMemoryBlock.CreateImmutableArray(this._pointer + offset, this._size - offset);
			GC.KeepAlive(this);
			return result;
		}

		// Token: 0x040008FB RID: 2299
		private unsafe byte* _pointer;

		// Token: 0x040008FC RID: 2300
		private readonly int _size;
	}
}
