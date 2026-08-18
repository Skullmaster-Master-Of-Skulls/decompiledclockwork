using System;

namespace System.Web
{
	// Token: 0x02000040 RID: 64
	internal class CharBufferAllocator : BufferAllocator
	{
		// Token: 0x0600050F RID: 1295 RVA: 0x0000660D File Offset: 0x0000480D
		internal CharBufferAllocator(int bufferSize, int maxFree) : base(maxFree)
		{
			this._bufferSize = bufferSize;
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x0000661D File Offset: 0x0000481D
		protected override object AllocBuffer()
		{
			return new char[this._bufferSize];
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000511 RID: 1297 RVA: 0x0000662A File Offset: 0x0000482A
		public override int BufferSize
		{
			get
			{
				return this._bufferSize;
			}
		}

		// Token: 0x04000122 RID: 290
		private int _bufferSize;
	}
}
