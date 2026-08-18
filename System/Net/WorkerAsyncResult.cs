using System;

namespace System.Net
{
	// Token: 0x0200054D RID: 1357
	internal class WorkerAsyncResult : LazyAsyncResult
	{
		// Token: 0x06002937 RID: 10551 RVA: 0x000AC695 File Offset: 0x000AB695
		public WorkerAsyncResult(object asyncObject, object asyncState, AsyncCallback savedAsyncCallback, byte[] buffer, int offset, int end) : base(asyncObject, asyncState, savedAsyncCallback)
		{
			this.Buffer = buffer;
			this.Offset = offset;
			this.End = end;
		}

		// Token: 0x04002848 RID: 10312
		public byte[] Buffer;

		// Token: 0x04002849 RID: 10313
		public int Offset;

		// Token: 0x0400284A RID: 10314
		public int End;

		// Token: 0x0400284B RID: 10315
		public bool IsWrite;

		// Token: 0x0400284C RID: 10316
		public WorkerAsyncResult ParentResult;

		// Token: 0x0400284D RID: 10317
		public bool HeaderDone;

		// Token: 0x0400284E RID: 10318
		public bool HandshakeDone;
	}
}
