using System;
using System.IO;

namespace System.Net
{
	// Token: 0x02000599 RID: 1433
	internal class FixedSizeReader
	{
		// Token: 0x06002C2C RID: 11308 RVA: 0x000BE029 File Offset: 0x000BD029
		public FixedSizeReader(Stream transport)
		{
			this._Transport = transport;
		}

		// Token: 0x06002C2D RID: 11309 RVA: 0x000BE038 File Offset: 0x000BD038
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

		// Token: 0x06002C2E RID: 11310 RVA: 0x000BE07C File Offset: 0x000BD07C
		public void AsyncReadPacket(AsyncProtocolRequest request)
		{
			this._Request = request;
			this._TotalRead = 0;
			this.StartReading();
		}

		// Token: 0x06002C2F RID: 11311 RVA: 0x000BE094 File Offset: 0x000BD094
		private void StartReading()
		{
			for (;;)
			{
				IAsyncResult asyncResult = this._Transport.BeginRead(this._Request.Buffer, this._Request.Offset + this._TotalRead, this._Request.Count - this._TotalRead, FixedSizeReader._ReadCallback, this);
				if (!asyncResult.CompletedSynchronously)
				{
					break;
				}
				int bytes = this._Transport.EndRead(asyncResult);
				if (this.CheckCompletionBeforeNextRead(bytes))
				{
					return;
				}
			}
		}

		// Token: 0x06002C30 RID: 11312 RVA: 0x000BE104 File Offset: 0x000BD104
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

		// Token: 0x06002C31 RID: 11313 RVA: 0x000BE174 File Offset: 0x000BD174
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

		// Token: 0x04002A09 RID: 10761
		private static readonly AsyncCallback _ReadCallback = new AsyncCallback(FixedSizeReader.ReadCallback);

		// Token: 0x04002A0A RID: 10762
		private readonly Stream _Transport;

		// Token: 0x04002A0B RID: 10763
		private AsyncProtocolRequest _Request;

		// Token: 0x04002A0C RID: 10764
		private int _TotalRead;
	}
}
