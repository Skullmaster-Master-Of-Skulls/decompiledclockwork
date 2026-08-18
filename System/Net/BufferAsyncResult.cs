using System;

namespace System.Net
{
	// Token: 0x0200059A RID: 1434
	internal class BufferAsyncResult : LazyAsyncResult
	{
		// Token: 0x06002C33 RID: 11315 RVA: 0x000BE1F7 File Offset: 0x000BD1F7
		public BufferAsyncResult(object asyncObject, BufferOffsetSize[] buffers, object asyncState, AsyncCallback asyncCallback) : base(asyncObject, asyncState, asyncCallback)
		{
			this.Buffers = buffers;
			this.IsWrite = true;
		}

		// Token: 0x06002C34 RID: 11316 RVA: 0x000BE211 File Offset: 0x000BD211
		public BufferAsyncResult(object asyncObject, byte[] buffer, int offset, int count, object asyncState, AsyncCallback asyncCallback) : this(asyncObject, buffer, offset, count, false, asyncState, asyncCallback)
		{
		}

		// Token: 0x06002C35 RID: 11317 RVA: 0x000BE223 File Offset: 0x000BD223
		public BufferAsyncResult(object asyncObject, byte[] buffer, int offset, int count, bool isWrite, object asyncState, AsyncCallback asyncCallback) : base(asyncObject, asyncState, asyncCallback)
		{
			this.Buffer = buffer;
			this.Offset = offset;
			this.Count = count;
			this.IsWrite = isWrite;
		}

		// Token: 0x04002A0D RID: 10765
		public byte[] Buffer;

		// Token: 0x04002A0E RID: 10766
		public BufferOffsetSize[] Buffers;

		// Token: 0x04002A0F RID: 10767
		public int Offset;

		// Token: 0x04002A10 RID: 10768
		public int Count;

		// Token: 0x04002A11 RID: 10769
		public bool IsWrite;
	}
}
