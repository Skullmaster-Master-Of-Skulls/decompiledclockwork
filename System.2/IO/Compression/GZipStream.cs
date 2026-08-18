using System;
using System.Security.Permissions;

namespace System.IO.Compression
{
	// Token: 0x0200042E RID: 1070
	[__DynamicallyInvokable]
	public class GZipStream : Stream
	{
		// Token: 0x06002822 RID: 10274 RVA: 0x000B8781 File Offset: 0x000B6981
		[__DynamicallyInvokable]
		public GZipStream(Stream stream, CompressionMode mode) : this(stream, mode, false)
		{
		}

		// Token: 0x06002823 RID: 10275 RVA: 0x000B878C File Offset: 0x000B698C
		[__DynamicallyInvokable]
		public GZipStream(Stream stream, CompressionMode mode, bool leaveOpen)
		{
			if (mode == CompressionMode.Decompress)
			{
				this.deflateStream = new DeflateStream(stream, leaveOpen, new GZipDecoder());
				return;
			}
			this.deflateStream = new DeflateStream(stream, mode, leaveOpen);
			this.deflateStream.SetFileFormatWriter(new GZipFormatter());
		}

		// Token: 0x06002824 RID: 10276 RVA: 0x000B87C8 File Offset: 0x000B69C8
		[__DynamicallyInvokable]
		public GZipStream(Stream stream, CompressionLevel compressionLevel) : this(stream, compressionLevel, false)
		{
		}

		// Token: 0x06002825 RID: 10277 RVA: 0x000B87D3 File Offset: 0x000B69D3
		[__DynamicallyInvokable]
		public GZipStream(Stream stream, CompressionLevel compressionLevel, bool leaveOpen)
		{
			this.deflateStream = new DeflateStream(stream, compressionLevel, leaveOpen);
			this.deflateStream.SetFileFormatWriter(new GZipFormatter());
		}

		// Token: 0x170009E2 RID: 2530
		// (get) Token: 0x06002826 RID: 10278 RVA: 0x000B87F9 File Offset: 0x000B69F9
		[__DynamicallyInvokable]
		public override bool CanRead
		{
			[__DynamicallyInvokable]
			get
			{
				return this.deflateStream != null && this.deflateStream.CanRead;
			}
		}

		// Token: 0x170009E3 RID: 2531
		// (get) Token: 0x06002827 RID: 10279 RVA: 0x000B8810 File Offset: 0x000B6A10
		[__DynamicallyInvokable]
		public override bool CanWrite
		{
			[__DynamicallyInvokable]
			get
			{
				return this.deflateStream != null && this.deflateStream.CanWrite;
			}
		}

		// Token: 0x170009E4 RID: 2532
		// (get) Token: 0x06002828 RID: 10280 RVA: 0x000B8827 File Offset: 0x000B6A27
		[__DynamicallyInvokable]
		public override bool CanSeek
		{
			[__DynamicallyInvokable]
			get
			{
				return this.deflateStream != null && this.deflateStream.CanSeek;
			}
		}

		// Token: 0x170009E5 RID: 2533
		// (get) Token: 0x06002829 RID: 10281 RVA: 0x000B883E File Offset: 0x000B6A3E
		[__DynamicallyInvokable]
		public override long Length
		{
			[__DynamicallyInvokable]
			get
			{
				throw new NotSupportedException(SR.GetString("NotSupported"));
			}
		}

		// Token: 0x170009E6 RID: 2534
		// (get) Token: 0x0600282A RID: 10282 RVA: 0x000B884F File Offset: 0x000B6A4F
		// (set) Token: 0x0600282B RID: 10283 RVA: 0x000B8860 File Offset: 0x000B6A60
		[__DynamicallyInvokable]
		public override long Position
		{
			[__DynamicallyInvokable]
			get
			{
				throw new NotSupportedException(SR.GetString("NotSupported"));
			}
			[__DynamicallyInvokable]
			set
			{
				throw new NotSupportedException(SR.GetString("NotSupported"));
			}
		}

		// Token: 0x0600282C RID: 10284 RVA: 0x000B8871 File Offset: 0x000B6A71
		[__DynamicallyInvokable]
		public override void Flush()
		{
			if (this.deflateStream == null)
			{
				throw new ObjectDisposedException(null, SR.GetString("ObjectDisposed_StreamClosed"));
			}
			this.deflateStream.Flush();
		}

		// Token: 0x0600282D RID: 10285 RVA: 0x000B8897 File Offset: 0x000B6A97
		[__DynamicallyInvokable]
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException(SR.GetString("NotSupported"));
		}

		// Token: 0x0600282E RID: 10286 RVA: 0x000B88A8 File Offset: 0x000B6AA8
		[__DynamicallyInvokable]
		public override void SetLength(long value)
		{
			throw new NotSupportedException(SR.GetString("NotSupported"));
		}

		// Token: 0x0600282F RID: 10287 RVA: 0x000B88B9 File Offset: 0x000B6AB9
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override IAsyncResult BeginRead(byte[] array, int offset, int count, AsyncCallback asyncCallback, object asyncState)
		{
			if (this.deflateStream == null)
			{
				throw new InvalidOperationException(SR.GetString("ObjectDisposed_StreamClosed"));
			}
			return this.deflateStream.BeginRead(array, offset, count, asyncCallback, asyncState);
		}

		// Token: 0x06002830 RID: 10288 RVA: 0x000B88E5 File Offset: 0x000B6AE5
		[__DynamicallyInvokable]
		public override int EndRead(IAsyncResult asyncResult)
		{
			if (this.deflateStream == null)
			{
				throw new InvalidOperationException(SR.GetString("ObjectDisposed_StreamClosed"));
			}
			return this.deflateStream.EndRead(asyncResult);
		}

		// Token: 0x06002831 RID: 10289 RVA: 0x000B890B File Offset: 0x000B6B0B
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override IAsyncResult BeginWrite(byte[] array, int offset, int count, AsyncCallback asyncCallback, object asyncState)
		{
			if (this.deflateStream == null)
			{
				throw new InvalidOperationException(SR.GetString("ObjectDisposed_StreamClosed"));
			}
			return this.deflateStream.BeginWrite(array, offset, count, asyncCallback, asyncState);
		}

		// Token: 0x06002832 RID: 10290 RVA: 0x000B8937 File Offset: 0x000B6B37
		[__DynamicallyInvokable]
		public override void EndWrite(IAsyncResult asyncResult)
		{
			if (this.deflateStream == null)
			{
				throw new InvalidOperationException(SR.GetString("ObjectDisposed_StreamClosed"));
			}
			this.deflateStream.EndWrite(asyncResult);
		}

		// Token: 0x06002833 RID: 10291 RVA: 0x000B895D File Offset: 0x000B6B5D
		[__DynamicallyInvokable]
		public override int Read(byte[] array, int offset, int count)
		{
			if (this.deflateStream == null)
			{
				throw new ObjectDisposedException(null, SR.GetString("ObjectDisposed_StreamClosed"));
			}
			return this.deflateStream.Read(array, offset, count);
		}

		// Token: 0x06002834 RID: 10292 RVA: 0x000B8986 File Offset: 0x000B6B86
		[__DynamicallyInvokable]
		public override void Write(byte[] array, int offset, int count)
		{
			if (this.deflateStream == null)
			{
				throw new ObjectDisposedException(null, SR.GetString("ObjectDisposed_StreamClosed"));
			}
			this.deflateStream.Write(array, offset, count);
		}

		// Token: 0x06002835 RID: 10293 RVA: 0x000B89B0 File Offset: 0x000B6BB0
		[__DynamicallyInvokable]
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing && this.deflateStream != null)
				{
					this.deflateStream.Close();
				}
				this.deflateStream = null;
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x170009E7 RID: 2535
		// (get) Token: 0x06002836 RID: 10294 RVA: 0x000B89F4 File Offset: 0x000B6BF4
		[__DynamicallyInvokable]
		public Stream BaseStream
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.deflateStream != null)
				{
					return this.deflateStream.BaseStream;
				}
				return null;
			}
		}

		// Token: 0x040021E0 RID: 8672
		private DeflateStream deflateStream;
	}
}
