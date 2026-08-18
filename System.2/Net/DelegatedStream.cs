using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net
{
	// Token: 0x02000227 RID: 551
	internal class DelegatedStream : Stream
	{
		// Token: 0x0600144A RID: 5194 RVA: 0x0006B910 File Offset: 0x00069B10
		protected DelegatedStream()
		{
		}

		// Token: 0x0600144B RID: 5195 RVA: 0x0006B918 File Offset: 0x00069B18
		protected DelegatedStream(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			this.stream = stream;
			this.netStream = (stream as NetworkStream);
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x0600144C RID: 5196 RVA: 0x0006B941 File Offset: 0x00069B41
		protected Stream BaseStream
		{
			get
			{
				return this.stream;
			}
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x0600144D RID: 5197 RVA: 0x0006B949 File Offset: 0x00069B49
		public override bool CanRead
		{
			get
			{
				return this.stream.CanRead;
			}
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x0600144E RID: 5198 RVA: 0x0006B956 File Offset: 0x00069B56
		public override bool CanSeek
		{
			get
			{
				return this.stream.CanSeek;
			}
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x0600144F RID: 5199 RVA: 0x0006B963 File Offset: 0x00069B63
		public override bool CanWrite
		{
			get
			{
				return this.stream.CanWrite;
			}
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06001450 RID: 5200 RVA: 0x0006B970 File Offset: 0x00069B70
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

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06001451 RID: 5201 RVA: 0x0006B995 File Offset: 0x00069B95
		// (set) Token: 0x06001452 RID: 5202 RVA: 0x0006B9BA File Offset: 0x00069BBA
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

		// Token: 0x06001453 RID: 5203 RVA: 0x0006B9E0 File Offset: 0x00069BE0
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

		// Token: 0x06001454 RID: 5204 RVA: 0x0006BA38 File Offset: 0x00069C38
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

		// Token: 0x06001455 RID: 5205 RVA: 0x0006BA90 File Offset: 0x00069C90
		public override void Close()
		{
			this.stream.Close();
		}

		// Token: 0x06001456 RID: 5206 RVA: 0x0006BAA0 File Offset: 0x00069CA0
		public override int EndRead(IAsyncResult asyncResult)
		{
			if (!this.CanRead)
			{
				throw new NotSupportedException(SR.GetString("ReadNotSupported"));
			}
			return this.stream.EndRead(asyncResult);
		}

		// Token: 0x06001457 RID: 5207 RVA: 0x0006BAD3 File Offset: 0x00069CD3
		public override void EndWrite(IAsyncResult asyncResult)
		{
			if (!this.CanWrite)
			{
				throw new NotSupportedException(SR.GetString("WriteNotSupported"));
			}
			this.stream.EndWrite(asyncResult);
		}

		// Token: 0x06001458 RID: 5208 RVA: 0x0006BAF9 File Offset: 0x00069CF9
		public override void Flush()
		{
			this.stream.Flush();
		}

		// Token: 0x06001459 RID: 5209 RVA: 0x0006BB06 File Offset: 0x00069D06
		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			return this.stream.FlushAsync(cancellationToken);
		}

		// Token: 0x0600145A RID: 5210 RVA: 0x0006BB14 File Offset: 0x00069D14
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (!this.CanRead)
			{
				throw new NotSupportedException(SR.GetString("ReadNotSupported"));
			}
			return this.stream.Read(buffer, offset, count);
		}

		// Token: 0x0600145B RID: 5211 RVA: 0x0006BB49 File Offset: 0x00069D49
		public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			if (!this.CanRead)
			{
				throw new NotSupportedException(SR.GetString("ReadNotSupported"));
			}
			return this.stream.ReadAsync(buffer, offset, count, cancellationToken);
		}

		// Token: 0x0600145C RID: 5212 RVA: 0x0006BB74 File Offset: 0x00069D74
		public override long Seek(long offset, SeekOrigin origin)
		{
			if (!this.CanSeek)
			{
				throw new NotSupportedException(SR.GetString("SeekNotSupported"));
			}
			return this.stream.Seek(offset, origin);
		}

		// Token: 0x0600145D RID: 5213 RVA: 0x0006BBA8 File Offset: 0x00069DA8
		public override void SetLength(long value)
		{
			if (!this.CanSeek)
			{
				throw new NotSupportedException(SR.GetString("SeekNotSupported"));
			}
			this.stream.SetLength(value);
		}

		// Token: 0x0600145E RID: 5214 RVA: 0x0006BBCE File Offset: 0x00069DCE
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (!this.CanWrite)
			{
				throw new NotSupportedException(SR.GetString("WriteNotSupported"));
			}
			this.stream.Write(buffer, offset, count);
		}

		// Token: 0x0600145F RID: 5215 RVA: 0x0006BBF6 File Offset: 0x00069DF6
		public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			if (!this.CanWrite)
			{
				throw new NotSupportedException(SR.GetString("WriteNotSupported"));
			}
			return this.stream.WriteAsync(buffer, offset, count, cancellationToken);
		}

		// Token: 0x0400162D RID: 5677
		private Stream stream;

		// Token: 0x0400162E RID: 5678
		private NetworkStream netStream;
	}
}
