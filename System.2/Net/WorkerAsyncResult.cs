using System;

namespace System.Net
{
	// Token: 0x0200021A RID: 538
	internal class WorkerAsyncResult : LazyAsyncResult
	{
		// Token: 0x060013D9 RID: 5081 RVA: 0x00069289 File Offset: 0x00067489
		public WorkerAsyncResult(object asyncObject, object asyncState, AsyncCallback savedAsyncCallback, byte[] buffer, int offset, int end) : base(asyncObject, asyncState, savedAsyncCallback)
		{
			this.Buffer = buffer;
			this.Offset = offset;
			this.End = end;
		}

		// Token: 0x040015DD RID: 5597
		public byte[] Buffer;

		// Token: 0x040015DE RID: 5598
		public int Offset;

		// Token: 0x040015DF RID: 5599
		public int End;

		// Token: 0x040015E0 RID: 5600
		public bool IsWrite;

		// Token: 0x040015E1 RID: 5601
		public WorkerAsyncResult ParentResult;

		// Token: 0x040015E2 RID: 5602
		public bool HeaderDone;

		// Token: 0x040015E3 RID: 5603
		public bool HandshakeDone;
	}
}
