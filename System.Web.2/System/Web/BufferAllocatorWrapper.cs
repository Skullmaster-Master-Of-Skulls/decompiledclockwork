using System;

namespace System.Web
{
	// Token: 0x02000044 RID: 68
	internal class BufferAllocatorWrapper<T> : IBufferAllocator<T>, IBufferAllocator
	{
		// Token: 0x06000523 RID: 1315 RVA: 0x00006770 File Offset: 0x00004970
		public BufferAllocatorWrapper(IBufferAllocator allocator)
		{
			this._allocator = allocator;
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x0000677F File Offset: 0x0000497F
		public T[] GetBuffer()
		{
			return (T[])this._allocator.GetBuffer();
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x00006794 File Offset: 0x00004994
		public T[] GetBuffer(int minSize)
		{
			if (minSize < 0)
			{
				throw new ArgumentOutOfRangeException("minSize");
			}
			T[] result;
			if (minSize <= this.BufferSize)
			{
				result = (T[])this._allocator.GetBuffer();
			}
			else
			{
				result = new T[minSize];
			}
			return result;
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x000067D6 File Offset: 0x000049D6
		public void ReuseBuffer(T[] buffer)
		{
			if (buffer != null && buffer.Length == this.BufferSize)
			{
				this._allocator.ReuseBuffer(buffer);
			}
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x000067F2 File Offset: 0x000049F2
		object IBufferAllocator.GetBuffer()
		{
			return this._allocator.GetBuffer();
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x000067FF File Offset: 0x000049FF
		void IBufferAllocator.ReuseBuffer(object buffer)
		{
			this.ReuseBuffer((T[])buffer);
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x0000680D File Offset: 0x00004A0D
		public void ReleaseAllBuffers()
		{
			this._allocator.ReleaseAllBuffers();
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x0600052A RID: 1322 RVA: 0x0000681A File Offset: 0x00004A1A
		public int BufferSize
		{
			get
			{
				return this._allocator.BufferSize;
			}
		}

		// Token: 0x04000127 RID: 295
		private IBufferAllocator _allocator;
	}
}
