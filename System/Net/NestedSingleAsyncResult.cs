using System;

namespace System.Net
{
	// Token: 0x020004F3 RID: 1267
	internal class NestedSingleAsyncResult : LazyAsyncResult
	{
		// Token: 0x060027A8 RID: 10152 RVA: 0x000A32A2 File Offset: 0x000A22A2
		internal NestedSingleAsyncResult(object asyncObject, object asyncState, AsyncCallback asyncCallback, object result) : base(asyncObject, asyncState, asyncCallback, result)
		{
		}

		// Token: 0x060027A9 RID: 10153 RVA: 0x000A32AF File Offset: 0x000A22AF
		internal NestedSingleAsyncResult(object asyncObject, object asyncState, AsyncCallback asyncCallback, byte[] buffer, int offset, int size) : base(asyncObject, asyncState, asyncCallback)
		{
			this.Buffer = buffer;
			this.Offset = offset;
			this.Size = size;
		}

		// Token: 0x040026CC RID: 9932
		internal byte[] Buffer;

		// Token: 0x040026CD RID: 9933
		internal int Offset;

		// Token: 0x040026CE RID: 9934
		internal int Size;
	}
}
