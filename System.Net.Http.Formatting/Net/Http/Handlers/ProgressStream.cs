using System;
using System.IO;
using System.Net.Http.Internal;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Http.Handlers
{
	// Token: 0x02000029 RID: 41
	internal class ProgressStream : DelegatingStream
	{
		// Token: 0x0600013A RID: 314 RVA: 0x00005CB8 File Offset: 0x00003EB8
		public ProgressStream(Stream innerStream, ProgressMessageHandler handler, HttpRequestMessage request, HttpResponseMessage response) : base(innerStream)
		{
			if (request.Content != null)
			{
				this._totalBytesToSend = request.Content.Headers.ContentLength;
			}
			if (response != null && response.Content != null)
			{
				this._totalBytesToReceive = response.Content.Headers.ContentLength;
			}
			this._handler = handler;
			this._request = request;
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00005D1C File Offset: 0x00003F1C
		public override int Read(byte[] buffer, int offset, int count)
		{
			int num = base.InnerStream.Read(buffer, offset, count);
			this.ReportBytesReceived(num, null);
			return num;
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00005D44 File Offset: 0x00003F44
		public override int ReadByte()
		{
			int num = base.InnerStream.ReadByte();
			this.ReportBytesReceived((num == -1) ? 0 : 1, null);
			return num;
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00005E84 File Offset: 0x00004084
		public override async Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			int readCount = await base.InnerStream.ReadAsync(buffer, offset, count, cancellationToken);
			this.ReportBytesReceived(readCount, null);
			return readCount;
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00005EEB File Offset: 0x000040EB
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return base.InnerStream.BeginRead(buffer, offset, count, callback, state);
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00005F00 File Offset: 0x00004100
		public override int EndRead(IAsyncResult asyncResult)
		{
			int num = base.InnerStream.EndRead(asyncResult);
			this.ReportBytesReceived(num, asyncResult.AsyncState);
			return num;
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00005F28 File Offset: 0x00004128
		public override void Write(byte[] buffer, int offset, int count)
		{
			base.InnerStream.Write(buffer, offset, count);
			this.ReportBytesSent(count, null);
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00005F40 File Offset: 0x00004140
		public override void WriteByte(byte value)
		{
			base.InnerStream.WriteByte(value);
			this.ReportBytesSent(1, null);
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00006054 File Offset: 0x00004254
		public override async Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			await base.InnerStream.WriteAsync(buffer, offset, count, cancellationToken);
			this.ReportBytesSent(count, null);
		}

		// Token: 0x06000143 RID: 323 RVA: 0x000060BB File Offset: 0x000042BB
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return new ProgressWriteAsyncResult(base.InnerStream, this, buffer, offset, count, callback, state);
		}

		// Token: 0x06000144 RID: 324 RVA: 0x000060D0 File Offset: 0x000042D0
		public override void EndWrite(IAsyncResult asyncResult)
		{
			ProgressWriteAsyncResult.End(asyncResult);
		}

		// Token: 0x06000145 RID: 325 RVA: 0x000060D8 File Offset: 0x000042D8
		internal void ReportBytesSent(int bytesSent, object userState)
		{
			if (bytesSent > 0)
			{
				this._bytesSent += (long)bytesSent;
				int progressPercentage = 0;
				if (this._totalBytesToSend != null && this._totalBytesToSend != 0L)
				{
					progressPercentage = (int)(100L * this._bytesSent / this._totalBytesToSend).Value;
				}
				this._handler.OnHttpRequestProgress(this._request, new HttpProgressEventArgs(progressPercentage, userState, this._bytesSent, this._totalBytesToSend));
			}
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00006190 File Offset: 0x00004390
		private void ReportBytesReceived(int bytesReceived, object userState)
		{
			if (bytesReceived > 0)
			{
				this._bytesReceived += (long)bytesReceived;
				int progressPercentage = 0;
				if (this._totalBytesToReceive != null && this._totalBytesToReceive != 0L)
				{
					progressPercentage = (int)(100L * this._bytesReceived / this._totalBytesToReceive).Value;
				}
				this._handler.OnHttpResponseProgress(this._request, new HttpProgressEventArgs(progressPercentage, userState, this._bytesReceived, this._totalBytesToReceive));
			}
		}

		// Token: 0x04000054 RID: 84
		private readonly ProgressMessageHandler _handler;

		// Token: 0x04000055 RID: 85
		private readonly HttpRequestMessage _request;

		// Token: 0x04000056 RID: 86
		private long _bytesReceived;

		// Token: 0x04000057 RID: 87
		private long? _totalBytesToReceive;

		// Token: 0x04000058 RID: 88
		private long _bytesSent;

		// Token: 0x04000059 RID: 89
		private long? _totalBytesToSend;
	}
}
