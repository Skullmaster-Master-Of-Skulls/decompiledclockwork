using System;
using System.IO;

namespace System.Net
{
	// Token: 0x0200021F RID: 543
	internal class FixedSizeReader
	{
		// Token: 0x0600140C RID: 5132 RVA: 0x0006A7CD File Offset: 0x000689CD
		public FixedSizeReader(Stream transport)
		{
			this._Transport = transport;
		}

		// Token: 0x0600140D RID: 5133 RVA: 0x0006A7DC File Offset: 0x000689DC
		public int ReadPacket(byte[] buffer, int offset, int count)
		{
			int num = count;
			for (;;)
			{
				int num2 = this._Transport.Read(buffer, offset, num);
				if (num2 == 0)
				{
					break;
				}
				num -= num2;
				offset += num2;
				if (num == 0)
				{
					return count;
				}
			}
			if (num != count)
			{
				throw new IOException(SR.GetString("net_io_eof"));
			}
			return 0;
		}

		// Token: 0x0600140E RID: 5134 RVA: 0x0006A820 File Offset: 0x00068A20
		public void AsyncReadPacket(AsyncProtocolRequest request)
		{
			this._Request = request;
			this._TotalRead = 0;
			this.StartReading();
		}

		// Token: 0x0600140F RID: 5135 RVA: 0x0006A838 File Offset: 0x00068A38
		private void StartReading()
		{
			int bytes;
			do
			{
				IAsyncResult asyncResult = this._Transport.BeginRead(this._Request.Buffer, this._Request.Offset + this._TotalRead, this._Request.Count - this._TotalRead, FixedSizeReader._ReadCallback, this);
				if (!asyncResult.CompletedSynchronously)
				{
					break;
				}
				bytes = this._Transport.EndRead(asyncResult);
			}
			while (!this.CheckCompletionBeforeNextRead(bytes));
		}

		// Token: 0x06001410 RID: 5136 RVA: 0x0006A8A4 File Offset: 0x00068AA4
		private bool CheckCompletionBeforeNextRead(int bytes)
		{
			if (bytes == 0)
			{
				if (this._TotalRead == 0)
				{
					this._Request.CompleteRequest(0);
					return true;
				}
				throw new IOException(SR.GetString("net_io_eof"));
			}
			else
			{
				if ((this._TotalRead += bytes) == this._Request.Count)
				{
					this._Request.CompleteRequest(this._Request.Count);
					return true;
				}
				return false;
			}
		}

		// Token: 0x06001411 RID: 5137 RVA: 0x0006A914 File Offset: 0x00068B14
		private static void ReadCallback(IAsyncResult transportResult)
		{
			if (transportResult.CompletedSynchronously)
			{
				return;
			}
			FixedSizeReader fixedSizeReader = (FixedSizeReader)transportResult.AsyncState;
			AsyncProtocolRequest request = fixedSizeReader._Request;
			try
			{
				int bytes = fixedSizeReader._Transport.EndRead(transportResult);
				if (!fixedSizeReader.CheckCompletionBeforeNextRead(bytes))
				{
					fixedSizeReader.StartReading();
				}
			}
			catch (Exception e)
			{
				if (request.IsUserCompleted)
				{
					throw;
				}
				request.CompleteWithError(e);
			}
		}

		// Token: 0x0400160B RID: 5643
		private static readonly AsyncCallback _ReadCallback = new AsyncCallback(FixedSizeReader.ReadCallback);

		// Token: 0x0400160C RID: 5644
		private readonly Stream _Transport;

		// Token: 0x0400160D RID: 5645
		private AsyncProtocolRequest _Request;

		// Token: 0x0400160E RID: 5646
		private int _TotalRead;
	}
}
