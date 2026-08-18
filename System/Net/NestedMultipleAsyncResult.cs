using System;

namespace System.Net
{
	// Token: 0x020004F2 RID: 1266
	internal class NestedMultipleAsyncResult : LazyAsyncResult
	{
		// Token: 0x060027A7 RID: 10151 RVA: 0x000A3250 File Offset: 0x000A2250
		internal NestedMultipleAsyncResult(object asyncObject, object asyncState, AsyncCallback asyncCallback, BufferOffsetSize[] buffers) : base(asyncObject, asyncState, asyncCallback)
		{
			this.Buffers = buffers;
			this.Size = 0;
			for (int i = 0; i < this.Buffers.Length; i++)
			{
				this.Size += this.Buffers[i].Size;
			}
		}

		// Token: 0x040026CA RID: 9930
		internal BufferOffsetSize[] Buffers;

		// Token: 0x040026CB RID: 9931
		internal int Size;
	}
}
