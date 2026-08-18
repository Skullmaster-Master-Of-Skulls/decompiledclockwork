using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Http
{
	// Token: 0x02000019 RID: 25
	[__DynamicallyInvokable]
	public class StreamContent : HttpContent
	{
		// Token: 0x0600015B RID: 347 RVA: 0x000063D1 File Offset: 0x000045D1
		[__DynamicallyInvokable]
		public StreamContent(Stream content) : this(content, 4096)
		{
		}

		// Token: 0x0600015C RID: 348 RVA: 0x000063E0 File Offset: 0x000045E0
		[__DynamicallyInvokable]
		public StreamContent(Stream content, int bufferSize)
		{
			if (content == null)
			{
				throw new ArgumentNullException("content");
			}
			if (bufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("bufferSize");
			}
			this.content = content;
			this.bufferSize = bufferSize;
			if (content.CanSeek)
			{
				this.start = content.Position;
			}
			if (Logging.On)
			{
				Logging.Associate(Logging.Http, this, content);
			}
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00006448 File Offset: 0x00004648
		[__DynamicallyInvokable]
		protected override Task SerializeToStreamAsync(Stream stream, TransportContext context)
		{
			this.PrepareContent();
			StreamToStreamCopy streamToStreamCopy = new StreamToStreamCopy(this.content, stream, this.bufferSize, !this.content.CanSeek);
			return streamToStreamCopy.StartAsync();
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00006482 File Offset: 0x00004682
		[__DynamicallyInvokable]
		protected internal override bool TryComputeLength(out long length)
		{
			if (this.content.CanSeek)
			{
				length = this.content.Length - this.start;
				return true;
			}
			length = 0L;
			return false;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x000064AC File Offset: 0x000046AC
		[__DynamicallyInvokable]
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.content.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000160 RID: 352 RVA: 0x000064C3 File Offset: 0x000046C3
		[__DynamicallyInvokable]
		protected override Task<Stream> CreateContentReadStreamAsync()
		{
			return Task.FromResult<Stream>(new StreamContent.ReadOnlyStream(this.content));
		}

		// Token: 0x06000161 RID: 353 RVA: 0x000064D5 File Offset: 0x000046D5
		private void PrepareContent()
		{
			if (this.contentConsumed)
			{
				if (!this.content.CanSeek)
				{
					throw new InvalidOperationException(SR.net_http_content_stream_already_read);
				}
				this.content.Position = this.start;
			}
			this.contentConsumed = true;
		}

		// Token: 0x040000BE RID: 190
		private const int defaultBufferSize = 4096;

		// Token: 0x040000BF RID: 191
		private Stream content;

		// Token: 0x040000C0 RID: 192
		private int bufferSize;

		// Token: 0x040000C1 RID: 193
		private bool contentConsumed;

		// Token: 0x040000C2 RID: 194
		private long start;

		// Token: 0x02000061 RID: 97
		private class ReadOnlyStream : DelegatingStream
		{
			// Token: 0x1700010B RID: 267
			// (get) Token: 0x0600044B RID: 1099 RVA: 0x00010080 File Offset: 0x0000E280
			public override bool CanWrite
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700010C RID: 268
			// (get) Token: 0x0600044C RID: 1100 RVA: 0x00010083 File Offset: 0x0000E283
			// (set) Token: 0x0600044D RID: 1101 RVA: 0x0001008F File Offset: 0x0000E28F
			public override int WriteTimeout
			{
				get
				{
					throw new NotSupportedException(SR.net_http_content_readonly_stream);
				}
				set
				{
					throw new NotSupportedException(SR.net_http_content_readonly_stream);
				}
			}

			// Token: 0x0600044E RID: 1102 RVA: 0x0001009B File Offset: 0x0000E29B
			public ReadOnlyStream(Stream innerStream) : base(innerStream)
			{
			}

			// Token: 0x0600044F RID: 1103 RVA: 0x000100A4 File Offset: 0x0000E2A4
			public override void Flush()
			{
				throw new NotSupportedException(SR.net_http_content_readonly_stream);
			}

			// Token: 0x06000450 RID: 1104 RVA: 0x000100B0 File Offset: 0x0000E2B0
			public override Task FlushAsync(CancellationToken cancellationToken)
			{
				throw new NotSupportedException(SR.net_http_content_readonly_stream);
			}

			// Token: 0x06000451 RID: 1105 RVA: 0x000100BC File Offset: 0x0000E2BC
			public override void SetLength(long value)
			{
				throw new NotSupportedException(SR.net_http_content_readonly_stream);
			}

			// Token: 0x06000452 RID: 1106 RVA: 0x000100C8 File Offset: 0x0000E2C8
			public override void Write(byte[] buffer, int offset, int count)
			{
				throw new NotSupportedException(SR.net_http_content_readonly_stream);
			}

			// Token: 0x06000453 RID: 1107 RVA: 0x000100D4 File Offset: 0x0000E2D4
			public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
			{
				throw new NotSupportedException(SR.net_http_content_readonly_stream);
			}

			// Token: 0x06000454 RID: 1108 RVA: 0x000100E0 File Offset: 0x0000E2E0
			public override void EndWrite(IAsyncResult asyncResult)
			{
				throw new NotSupportedException(SR.net_http_content_readonly_stream);
			}

			// Token: 0x06000455 RID: 1109 RVA: 0x000100EC File Offset: 0x0000E2EC
			public override void WriteByte(byte value)
			{
				throw new NotSupportedException(SR.net_http_content_readonly_stream);
			}

			// Token: 0x06000456 RID: 1110 RVA: 0x000100F8 File Offset: 0x0000E2F8
			public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
			{
				throw new NotSupportedException(SR.net_http_content_readonly_stream);
			}
		}
	}
}
