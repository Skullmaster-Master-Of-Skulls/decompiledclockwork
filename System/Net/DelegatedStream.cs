using System;
using System.IO;
using System.Net.Sockets;

namespace System.Net
{
	// Token: 0x0200067B RID: 1659
	internal class DelegatedStream : Stream
	{
		// Token: 0x06003350 RID: 13136 RVA: 0x000D8AD1 File Offset: 0x000D7AD1
		protected DelegatedStream()
		{
		}

		// Token: 0x06003351 RID: 13137 RVA: 0x000D8AD9 File Offset: 0x000D7AD9
		protected DelegatedStream(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			this.stream = stream;
			this.netStream = (stream as NetworkStream);
		}

		// Token: 0x17000C0F RID: 3087
		// (get) Token: 0x06003352 RID: 13138 RVA: 0x000D8B02 File Offset: 0x000D7B02
		protected Stream BaseStream
		{
			get
			{
				return this.stream;
			}
		}

		// Token: 0x17000C10 RID: 3088
		// (get) Token: 0x06003353 RID: 13139 RVA: 0x000D8B0A File Offset: 0x000D7B0A
		public override bool CanRead
		{
			get
			{
				return this.stream.CanRead;
			}
		}

		// Token: 0x17000C11 RID: 3089
		// (get) Token: 0x06003354 RID: 13140 RVA: 0x000D8B17 File Offset: 0x000D7B17
		public override bool CanSeek
		{
			get
			{
				return this.stream.CanSeek;
			}
		}

		// Token: 0x17000C12 RID: 3090
		// (get) Token: 0x06003355 RID: 13141 RVA: 0x000D8B24 File Offset: 0x000D7B24
		public override bool CanWrite
		{
			get
			{
				return this.stream.CanWrite;
			}
		}

		// Token: 0x17000C13 RID: 3091
		// (get) Token: 0x06003356 RID: 13142 RVA: 0x000D8B31 File Offset: 0x000D7B31
		public override long Length
		{
			get
			{
				if (!this.CanSeek)
				{
					throw new NotSupportedException(SR.GetString("SeekNotSupported"));
				}
				return this.stream.Length;
			}
		}

		// Token: 0x17000C14 RID: 3092
		// (get) Token: 0x06003357 RID: 13143 RVA: 0x000D8B56 File Offset: 0x000D7B56
		// (set) Token: 0x06003358 RID: 13144 RVA: 0x000D8B7B File Offset: 0x000D7B7B
		public override long Position
		{
			get
			{
				if (!this.CanSeek)
				{
					throw new NotSupportedException(SR.GetString("SeekNotSupported"));
				}
				return this.stream.Position;
			}
			set
			{
				if (!this.CanSeek)
				{
					throw new NotSupportedException(SR.GetString("SeekNotSupported"));
				}
				this.stream.Position = value;
			}
		}

		// Token: 0x06003359 RID: 13145 RVA: 0x000D8BA4 File Offset: 0x000D7BA4
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			if (!this.CanRead)
			{
				throw new NotSupportedException(SR.GetString("ReadNotSupported"));
			}
			IAsyncResult result;
			if (this.netStream != null)
			{
				result = this.netStream.UnsafeBeginRead(buffer, offset, count, callback, state);
			}
			else
			{
				result = this.stream.BeginRead(buffer, offset, count, callback, state);
			}
			return result;
		}

		// Token: 0x0600335A RID: 13146 RVA: 0x000D8BFC File Offset: 0x000D7BFC
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			if (!this.CanWrite)
			{
				throw new NotSupportedException(SR.GetString("WriteNotSupported"));
			}
			IAsyncResult result;
			if (this.netStream != null)
			{
				result = this.netStream.UnsafeBeginWrite(buffer, offset, count, callback, state);
			}
			else
			{
				result = this.stream.BeginWrite(buffer, offset, count, callback, state);
			}
			return result;
		}

		// Token: 0x0600335B RID: 13147 RVA: 0x000D8C54 File Offset: 0x000D7C54
		public override void Close()
		{
			this.stream.Close();
		}

		// Token: 0x0600335C RID: 13148 RVA: 0x000D8C64 File Offset: 0x000D7C64
		public override int EndRead(IAsyncResult asyncResult)
		{
			if (!this.CanRead)
			{
				throw new NotSupportedException(SR.GetString("ReadNotSupported"));
			}
			return this.stream.EndRead(asyncResult);
		}

		// Token: 0x0600335D RID: 13149 RVA: 0x000D8C97 File Offset: 0x000D7C97
		public override void EndWrite(IAsyncResult asyncResult)
		{
			if (!this.CanWrite)
			{
				throw new NotSupportedException(SR.GetString("WriteNotSupported"));
			}
			this.stream.EndWrite(asyncResult);
		}

		// Token: 0x0600335E RID: 13150 RVA: 0x000D8CBD File Offset: 0x000D7CBD
		public override void Flush()
		{
			this.stream.Flush();
		}

		// Token: 0x0600335F RID: 13151 RVA: 0x000D8CCC File Offset: 0x000D7CCC
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (!this.CanRead)
			{
				throw new NotSupportedException(SR.GetString("ReadNotSupported"));
			}
			return this.stream.Read(buffer, offset, count);
		}

		// Token: 0x06003360 RID: 13152 RVA: 0x000D8D04 File Offset: 0x000D7D04
		public override long Seek(long offset, SeekOrigin origin)
		{
			if (!this.CanSeek)
			{
				throw new NotSupportedException(SR.GetString("SeekNotSupported"));
			}
			return this.stream.Seek(offset, origin);
		}

		// Token: 0x06003361 RID: 13153 RVA: 0x000D8D38 File Offset: 0x000D7D38
		public override void SetLength(long value)
		{
			if (!this.CanSeek)
			{
				throw new NotSupportedException(SR.GetString("SeekNotSupported"));
			}
			this.stream.SetLength(value);
		}

		// Token: 0x06003362 RID: 13154 RVA: 0x000D8D5E File Offset: 0x000D7D5E
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (!this.CanWrite)
			{
				throw new NotSupportedException(SR.GetString("WriteNotSupported"));
			}
			this.stream.Write(buffer, offset, count);
		}

		// Token: 0x04002F91 RID: 12177
		private Stream stream;

		// Token: 0x04002F92 RID: 12178
		private NetworkStream netStream;
	}
}
