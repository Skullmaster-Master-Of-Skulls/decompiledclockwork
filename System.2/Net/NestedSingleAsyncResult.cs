using System;

namespace System.Net
{
	// Token: 0x020001CA RID: 458
	internal class NestedSingleAsyncResult : LazyAsyncResult
	{
		// Token: 0x06001230 RID: 4656 RVA: 0x00060F56 File Offset: 0x0005F156
		internal NestedSingleAsyncResult(object asyncObject, object asyncState, AsyncCallback asyncCallback, object result) : base(asyncObject, asyncState, asyncCallback, result)
		{
		}

		// Token: 0x06001231 RID: 4657 RVA: 0x00060F63 File Offset: 0x0005F163
		internal NestedSingleAsyncResult(object asyncObject, object asyncState, AsyncCallback asyncCallback, byte[] buffer, int offset, int size) : base(asyncObject, asyncState, asyncCallback)
		{
			this.Buffer = buffer;
			this.Offset = offset;
			this.Size = size;
		}

		// Token: 0x0400148B RID: 5259
		internal byte[] Buffer;

		// Token: 0x0400148C RID: 5260
		internal int Offset;

		// Token: 0x0400148D RID: 5261
		internal int Size;
	}
}
