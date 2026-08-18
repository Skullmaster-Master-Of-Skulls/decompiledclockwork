using System;
using System.IO;
using System.Net.Http.Internal;

namespace System.Net.Http.Handlers
{
	// Token: 0x0200002B RID: 43
	internal class ProgressWriteAsyncResult : AsyncResult
	{
		// Token: 0x06000150 RID: 336 RVA: 0x000063C4 File Offset: 0x000045C4
		public ProgressWriteAsyncResult(Stream innerStream, ProgressStream progressStream, byte[] buffer, int offset, int count, AsyncCallback callback, object state) : base(callback, state)
		{
			this._innerStream = innerStream;
			this._progressStream = progressStream;
			this._count = count;
			try
			{
				IAsyncResult asyncResult = innerStream.BeginWrite(buffer, offset, count, ProgressWriteAsyncResult._writeCompletedCallback, this);
				if (asyncResult.CompletedSynchronously)
				{
					this.WriteCompleted(asyncResult);
				}
			}
			catch (Exception exception)
			{
				base.Complete(true, exception);
			}
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00006430 File Offset: 0x00004630
		private static void WriteCompletedCallback(IAsyncResult result)
		{
			if (result.CompletedSynchronously)
			{
				return;
			}
			ProgressWriteAsyncResult progressWriteAsyncResult = (ProgressWriteAsyncResult)result.AsyncState;
			try
			{
				progressWriteAsyncResult.WriteCompleted(result);
			}
			catch (Exception exception)
			{
				progressWriteAsyncResult.Complete(false, exception);
			}
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00006478 File Offset: 0x00004678
		private void WriteCompleted(IAsyncResult result)
		{
			this._innerStream.EndWrite(result);
			this._progressStream.ReportBytesSent(this._count, base.AsyncState);
			base.Complete(result.CompletedSynchronously);
		}

		// Token: 0x06000153 RID: 339 RVA: 0x000064A9 File Offset: 0x000046A9
		public static void End(IAsyncResult result)
		{
			AsyncResult.End<ProgressWriteAsyncResult>(result);
		}

		// Token: 0x04000060 RID: 96
		private static readonly AsyncCallback _writeCompletedCallback = new AsyncCallback(ProgressWriteAsyncResult.WriteCompletedCallback);

		// Token: 0x04000061 RID: 97
		private readonly Stream _innerStream;

		// Token: 0x04000062 RID: 98
		private readonly ProgressStream _progressStream;

		// Token: 0x04000063 RID: 99
		private readonly int _count;
	}
}
