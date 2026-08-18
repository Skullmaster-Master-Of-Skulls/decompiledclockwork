using System;
using System.Runtime.InteropServices;

namespace System.IO
{
	// Token: 0x020005D1 RID: 1489
	internal sealed class UnmanagedMemoryStreamWrapper : MemoryStream
	{
		// Token: 0x0600379E RID: 14238 RVA: 0x000BB85B File Offset: 0x000BA85B
		internal UnmanagedMemoryStreamWrapper(UnmanagedMemoryStream stream)
		{
			this._unmanagedStream = stream;
		}

		// Token: 0x1700095C RID: 2396
		// (get) Token: 0x0600379F RID: 14239 RVA: 0x000BB86A File Offset: 0x000BA86A
		public override bool CanRead
		{
			get
			{
				return this._unmanagedStream.CanRead;
			}
		}

		// Token: 0x1700095D RID: 2397
		// (get) Token: 0x060037A0 RID: 14240 RVA: 0x000BB877 File Offset: 0x000BA877
		public override bool CanSeek
		{
			get
			{
				return this._unmanagedStream.CanSeek;
			}
		}

		// Token: 0x1700095E RID: 2398
		// (get) Token: 0x060037A1 RID: 14241 RVA: 0x000BB884 File Offset: 0x000BA884
		public override bool CanWrite
		{
			get
			{
				return this._unmanagedStream.CanWrite;
			}
		}

		// Token: 0x060037A2 RID: 14242 RVA: 0x000BB894 File Offset: 0x000BA894
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					this._unmanagedStream.Close();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x060037A3 RID: 14243 RVA: 0x000BB8CC File Offset: 0x000BA8CC
		public override void Flush()
		{
			this._unmanagedStream.Flush();
		}

		// Token: 0x060037A4 RID: 14244 RVA: 0x000BB8D9 File Offset: 0x000BA8D9
		public override byte[] GetBuffer()
		{
			throw new UnauthorizedAccessException(Environment.GetResourceString("UnauthorizedAccess_MemStreamBuffer"));
		}

		// Token: 0x1700095F RID: 2399
		// (get) Token: 0x060037A5 RID: 14245 RVA: 0x000BB8EA File Offset: 0x000BA8EA
		// (set) Token: 0x060037A6 RID: 14246 RVA: 0x000BB8F8 File Offset: 0x000BA8F8
		public override int Capacity
		{
			get
			{
				return (int)this._unmanagedStream.Capacity;
			}
			set
			{
				throw new IOException(Environment.GetResourceString("IO.IO_FixedCapacity"));
			}
		}

		// Token: 0x17000960 RID: 2400
		// (get) Token: 0x060037A7 RID: 14247 RVA: 0x000BB909 File Offset: 0x000BA909
		public override long Length
		{
			get
			{
				return this._unmanagedStream.Length;
			}
		}

		// Token: 0x17000961 RID: 2401
		// (get) Token: 0x060037A8 RID: 14248 RVA: 0x000BB916 File Offset: 0x000BA916
		// (set) Token: 0x060037A9 RID: 14249 RVA: 0x000BB923 File Offset: 0x000BA923
		public override long Position
		{
			get
			{
				return this._unmanagedStream.Position;
			}
			set
			{
				this._unmanagedStream.Position = value;
			}
		}

		// Token: 0x060037AA RID: 14250 RVA: 0x000BB931 File Offset: 0x000BA931
		public override int Read([In] [Out] byte[] buffer, int offset, int count)
		{
			return this._unmanagedStream.Read(buffer, offset, count);
		}

		// Token: 0x060037AB RID: 14251 RVA: 0x000BB941 File Offset: 0x000BA941
		public override int ReadByte()
		{
			return this._unmanagedStream.ReadByte();
		}

		// Token: 0x060037AC RID: 14252 RVA: 0x000BB94E File Offset: 0x000BA94E
		public override long Seek(long offset, SeekOrigin loc)
		{
			return this._unmanagedStream.Seek(offset, loc);
		}

		// Token: 0x060037AD RID: 14253 RVA: 0x000BB960 File Offset: 0x000BA960
		public override byte[] ToArray()
		{
			if (!this._unmanagedStream._isOpen)
			{
				__Error.StreamIsClosed();
			}
			if (!this._unmanagedStream.CanRead)
			{
				__Error.ReadNotSupported();
			}
			byte[] array = new byte[this._unmanagedStream.Length];
			Buffer.memcpy(this._unmanagedStream.Pointer, 0, array, 0, (int)this._unmanagedStream.Length);
			return array;
		}

		// Token: 0x060037AE RID: 14254 RVA: 0x000BB9C3 File Offset: 0x000BA9C3
		public override void Write(byte[] buffer, int offset, int count)
		{
			this._unmanagedStream.Write(buffer, offset, count);
		}

		// Token: 0x060037AF RID: 14255 RVA: 0x000BB9D3 File Offset: 0x000BA9D3
		public override void WriteByte(byte value)
		{
			this._unmanagedStream.WriteByte(value);
		}

		// Token: 0x060037B0 RID: 14256 RVA: 0x000BB9E4 File Offset: 0x000BA9E4
		public override void WriteTo(Stream stream)
		{
			if (!this._unmanagedStream._isOpen)
			{
				__Error.StreamIsClosed();
			}
			if (!this._unmanagedStream.CanRead)
			{
				__Error.ReadNotSupported();
			}
			if (stream == null)
			{
				throw new ArgumentNullException("stream", Environment.GetResourceString("ArgumentNull_Stream"));
			}
			byte[] array = this.ToArray();
			stream.Write(array, 0, array.Length);
		}

		// Token: 0x04001CDF RID: 7391
		private UnmanagedMemoryStream _unmanagedStream;
	}
}
