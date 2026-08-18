using System;

namespace System.Net
{
	// Token: 0x02000220 RID: 544
	internal class BufferAsyncResult : LazyAsyncResult
	{
		// Token: 0x06001413 RID: 5139 RVA: 0x0006A997 File Offset: 0x00068B97
		public BufferAsyncResult(object asyncObject, BufferOffsetSize[] buffers, object asyncState, AsyncCallback asyncCallback) : base(asyncObject, asyncState, asyncCallback)
		{
			this.Buffers = buffers;
			this.IsWrite = true;
		}

		// Token: 0x06001414 RID: 5140 RVA: 0x0006A9B1 File Offset: 0x00068BB1
		public BufferAsyncResult(object asyncObject, byte[] buffer, int offset, int count, object asyncState, AsyncCallback asyncCallback) : this(asyncObject, buffer, offset, count, false, asyncState, asyncCallback)
		{
		}

		// Token: 0x06001415 RID: 5141 RVA: 0x0006A9C3 File Offset: 0x00068BC3
		public BufferAsyncResult(object asyncObject, byte[] buffer, int offset, int count, bool isWrite, object asyncState, AsyncCallback asyncCallback) : base(asyncObject, asyncState, asyncCallback)
		{
			this.Buffer = buffer;
			this.Offset = offset;
			this.Count = count;
			this.IsWrite = isWrite;
		}

		// Token: 0x0400160F RID: 5647
		public byte[] Buffer;

		// Token: 0x04001610 RID: 5648
		public BufferOffsetSize[] Buffers;

		// Token: 0x04001611 RID: 5649
		public int Offset;

		// Token: 0x04001612 RID: 5650
		public int Count;

		// Token: 0x04001613 RID: 5651
		public bool IsWrite;
	}
}
