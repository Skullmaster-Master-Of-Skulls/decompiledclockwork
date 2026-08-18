using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace System.Net.Http.Internal
{
	// Token: 0x02000019 RID: 25
	internal abstract class DelegatingStream : Stream
	{
		// Token: 0x060000B6 RID: 182 RVA: 0x00004690 File Offset: 0x00002890
		protected DelegatingStream(Stream innerStream)
		{
			if (innerStream == null)
			{
				throw Error.ArgumentNull("innerStream");
			}
			this._innerStream = innerStream;
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x000046AD File Offset: 0x000028AD
		protected Stream InnerStream
		{
			get
			{
				return this._innerStream;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x000046B5 File Offset: 0x000028B5
		public override bool CanRead
		{
			get
			{
				return this._innerStream.CanRead;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x000046C2 File Offset: 0x000028C2
		public override bool CanSeek
		{
			get
			{
				return this._innerStream.CanSeek;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000BA RID: 186 RVA: 0x000046CF File Offset: 0x000028CF
		public override bool CanWrite
		{
			get
			{
				return this._innerStream.CanWrite;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000BB RID: 187 RVA: 0x000046DC File Offset: 0x000028DC
		public override long Length
		{
			get
			{
				return this._innerStream.Length;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000BC RID: 188 RVA: 0x000046E9 File Offset: 0x000028E9
		// (set) Token: 0x060000BD RID: 189 RVA: 0x000046F6 File Offset: 0x000028F6
		public override long Position
		{
			get
			{
				return this._innerStream.Position;
			}
			set
			{
				this._innerStream.Position = value;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00004704 File Offset: 0x00002904
		// (set) Token: 0x060000BF RID: 191 RVA: 0x00004711 File Offset: 0x00002911
		public override int ReadTimeout
		{
			get
			{
				return this._innerStream.ReadTimeout;
			}
			set
			{
				this._innerStream.ReadTimeout = value;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x0000471F File Offset: 0x0000291F
		public override bool CanTimeout
		{
			get
			{
				return this._innerStream.CanTimeout;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x0000472C File Offset: 0x0000292C
		// (set) Token: 0x060000C2 RID: 194 RVA: 0x00004739 File Offset: 0x00002939
		public override int WriteTimeout
		{
			get
			{
				return this._innerStream.WriteTimeout;
			}
			set
			{
				this._innerStream.WriteTimeout = value;
			}
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00004747 File Offset: 0x00002947
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this._innerStream.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x0000475E File Offset: 0x0000295E
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this._innerStream.Seek(offset, origin);
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x0000476D File Offset: 0x0000296D
		public override int Read(byte[] buffer, int offset, int count)
		{
			return this._innerStream.Read(buffer, offset, count);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x0000477D File Offset: 0x0000297D
		public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			return this._innerStream.ReadAsync(buffer, offset, count, cancellationToken);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x0000478F File Offset: 0x0000298F
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return this._innerStream.BeginRead(buffer, offset, count, callback, state);
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x000047A3 File Offset: 0x000029A3
		public override int EndRead(IAsyncResult asyncResult)
		{
			return this._innerStream.EndRead(asyncResult);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x000047B1 File Offset: 0x000029B1
		public override int ReadByte()
		{
			return this._innerStream.ReadByte();
		}

		// Token: 0x060000CA RID: 202 RVA: 0x000047BE File Offset: 0x000029BE
		public override void Flush()
		{
			this._innerStream.Flush();
		}

		// Token: 0x060000CB RID: 203 RVA: 0x000047CB File Offset: 0x000029CB
		public override Task CopyToAsync(Stream destination, int bufferSize, CancellationToken cancellationToken)
		{
			return this._innerStream.CopyToAsync(destination, bufferSize, cancellationToken);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x000047DB File Offset: 0x000029DB
		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			return this._innerStream.FlushAsync(cancellationToken);
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000047E9 File Offset: 0x000029E9
		public override void SetLength(long value)
		{
			this._innerStream.SetLength(value);
		}

		// Token: 0x060000CE RID: 206 RVA: 0x000047F7 File Offset: 0x000029F7
		public override void Write(byte[] buffer, int offset, int count)
		{
			this._innerStream.Write(buffer, offset, count);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00004807 File Offset: 0x00002A07
		public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			return this._innerStream.WriteAsync(buffer, offset, count, cancellationToken);
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00004819 File Offset: 0x00002A19
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return this._innerStream.BeginWrite(buffer, offset, count, callback, state);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x0000482D File Offset: 0x00002A2D
		public override void EndWrite(IAsyncResult asyncResult)
		{
			this._innerStream.EndWrite(asyncResult);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x0000483B File Offset: 0x00002A3B
		public override void WriteByte(byte value)
		{
			this._innerStream.WriteByte(value);
		}

		// Token: 0x04000038 RID: 56
		private Stream _innerStream;
	}
}
