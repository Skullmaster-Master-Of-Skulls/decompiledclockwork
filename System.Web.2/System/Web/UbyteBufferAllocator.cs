using System;

namespace System.Web
{
	// Token: 0x0200003F RID: 63
	internal class UbyteBufferAllocator : BufferAllocator
	{
		// Token: 0x0600050C RID: 1292 RVA: 0x000065E8 File Offset: 0x000047E8
		internal UbyteBufferAllocator(int bufferSize, int maxFree) : base(maxFree)
		{
			this._bufferSize = bufferSize;
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x000065F8 File Offset: 0x000047F8
		protected override object AllocBuffer()
		{
			return new byte[this._bufferSize];
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x0600050E RID: 1294 RVA: 0x00006605 File Offset: 0x00004805
		public override int BufferSize
		{
			get
			{
				return this._bufferSize;
			}
		}

		// Token: 0x04000121 RID: 289
		private int _bufferSize;
	}
}
