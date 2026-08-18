using System;
using System.Collections;
using System.Web.Util;

namespace System.Web
{
	// Token: 0x0200003E RID: 62
	internal abstract class BufferAllocator : IBufferAllocator
	{
		// Token: 0x06000505 RID: 1285 RVA: 0x0000646D File Offset: 0x0000466D
		static BufferAllocator()
		{
			if (BufferAllocator.s_ProcsFudgeFactor < 1)
			{
				BufferAllocator.s_ProcsFudgeFactor = 1;
			}
			if (BufferAllocator.s_ProcsFudgeFactor > 4)
			{
				BufferAllocator.s_ProcsFudgeFactor = 4;
			}
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x00006495 File Offset: 0x00004695
		internal BufferAllocator(int maxFree)
		{
			this._buffers = new Stack();
			this._numFree = 0;
			this._maxFree = maxFree * BufferAllocator.s_ProcsFudgeFactor;
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x000064BC File Offset: 0x000046BC
		public void ReleaseAllBuffers()
		{
			if (this._numFree > 0)
			{
				lock (this)
				{
					this._buffers.Clear();
					this._numFree = 0;
				}
			}
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x0000650C File Offset: 0x0000470C
		public object GetBuffer()
		{
			object obj = null;
			if (this._numFree > 0)
			{
				lock (this)
				{
					if (this._numFree > 0)
					{
						obj = this._buffers.Pop();
						this._numFree--;
					}
				}
			}
			if (obj == null)
			{
				obj = this.AllocBuffer();
			}
			return obj;
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x0000657C File Offset: 0x0000477C
		public void ReuseBuffer(object buffer)
		{
			if (this._numFree < this._maxFree)
			{
				lock (this)
				{
					if (this._numFree < this._maxFree)
					{
						this._buffers.Push(buffer);
						this._numFree++;
					}
				}
			}
		}

		// Token: 0x0600050A RID: 1290
		protected abstract object AllocBuffer();

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x0600050B RID: 1291
		public abstract int BufferSize { get; }

		// Token: 0x0400011D RID: 285
		private int _maxFree;

		// Token: 0x0400011E RID: 286
		private int _numFree;

		// Token: 0x0400011F RID: 287
		private Stack _buffers;

		// Token: 0x04000120 RID: 288
		private static int s_ProcsFudgeFactor = SystemInfo.GetNumProcessCPUs();
	}
}
