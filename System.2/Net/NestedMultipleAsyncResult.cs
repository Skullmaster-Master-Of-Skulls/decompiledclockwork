using System;

namespace System.Net
{
	// Token: 0x020001C9 RID: 457
	internal class NestedMultipleAsyncResult : LazyAsyncResult
	{
		// Token: 0x0600122F RID: 4655 RVA: 0x00060F04 File Offset: 0x0005F104
		internal NestedMultipleAsyncResult(object asyncObject, object asyncState, AsyncCallback asyncCallback, BufferOffsetSize[] buffers) : base(asyncObject, asyncState, asyncCallback)
		{
			this.Buffers = buffers;
			this.Size = 0;
			for (int i = 0; i < this.Buffers.Length; i++)
			{
				this.Size += this.Buffers[i].Size;
			}
		}

		// Token: 0x04001489 RID: 5257
		internal BufferOffsetSize[] Buffers;

		// Token: 0x0400148A RID: 5258
		internal int Size;
	}
}
