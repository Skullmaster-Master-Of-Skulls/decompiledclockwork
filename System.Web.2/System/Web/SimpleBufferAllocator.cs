using System;
using System.Collections.Generic;

namespace System.Web
{
	// Token: 0x02000043 RID: 67
	internal class SimpleBufferAllocator<T> : IBufferAllocator<T>, IBufferAllocator
	{
		// Token: 0x06000518 RID: 1304 RVA: 0x0000667C File Offset: 0x0000487C
		public SimpleBufferAllocator(int bufferSize)
		{
			if (bufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("bufferSize");
			}
			this._buffers = new Stack<T[]>();
			this._bufferSize = bufferSize;
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x000066A5 File Offset: 0x000048A5
		public T[] GetBuffer()
		{
			return this.GetBufferImpl();
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x000066B0 File Offset: 0x000048B0
		public T[] GetBuffer(int minSize)
		{
			if (minSize < 0)
			{
				throw new ArgumentOutOfRangeException("minSize");
			}
			T[] result;
			if (minSize <= this.BufferSize)
			{
				result = this.GetBufferImpl();
			}
			else
			{
				result = SimpleBufferAllocator<T>.AllocBuffer(minSize);
			}
			return result;
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x000066A5 File Offset: 0x000048A5
		object IBufferAllocator.GetBuffer()
		{
			return this.GetBufferImpl();
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x000066E8 File Offset: 0x000048E8
		public void ReuseBuffer(T[] buffer)
		{
			this.ReuseBufferImpl(buffer);
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x000066F1 File Offset: 0x000048F1
		void IBufferAllocator.ReuseBuffer(object buffer)
		{
			this.ReuseBufferImpl((T[])buffer);
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x000066FF File Offset: 0x000048FF
		public void ReleaseAllBuffers()
		{
			this._buffers.Clear();
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x0600051F RID: 1311 RVA: 0x0000670C File Offset: 0x0000490C
		public int BufferSize
		{
			get
			{
				return this._bufferSize;
			}
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x00006714 File Offset: 0x00004914
		private T[] GetBufferImpl()
		{
			T[] result;
			if (this._buffers.Count > 0)
			{
				result = this._buffers.Pop();
			}
			else
			{
				result = SimpleBufferAllocator<T>.AllocBuffer(this.BufferSize);
			}
			return result;
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x0000674C File Offset: 0x0000494C
		private void ReuseBufferImpl(T[] buffer)
		{
			if (buffer != null && buffer.Length == this.BufferSize)
			{
				this._buffers.Push(buffer);
			}
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x00006768 File Offset: 0x00004968
		private static T[] AllocBuffer(int size)
		{
			return new T[size];
		}

		// Token: 0x04000125 RID: 293
		private Stack<T[]> _buffers;

		// Token: 0x04000126 RID: 294
		private readonly int _bufferSize;
	}
}
