using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Reflection.Internal
{
	// Token: 0x0200008C RID: 140
	internal sealed class ReadOnlyUnmanagedMemoryStream : Stream
	{
		// Token: 0x0600037F RID: 895 RVA: 0x00008CA1 File Offset: 0x00006EA1
		[SecurityCritical]
		public unsafe ReadOnlyUnmanagedMemoryStream(byte* data, int length)
		{
			this._data = data;
			this._length = length;
		}

		// Token: 0x06000380 RID: 896 RVA: 0x00008CB8 File Offset: 0x00006EB8
		[SecuritySafeCritical]
		public unsafe override int ReadByte()
		{
			if (this._position >= this._length)
			{
				return -1;
			}
			int data = this._data;
			int position = this._position;
			this._position = position + 1;
			return (int)(*(data + position));
		}

		// Token: 0x06000381 RID: 897 RVA: 0x00008CF0 File Offset: 0x00006EF0
		[SecuritySafeCritical]
		public unsafe override int Read(byte[] buffer, int offset, int count)
		{
			int num = Math.Min(count, this._length - this._position);
			Marshal.Copy((IntPtr)((void*)(this._data + this._position)), buffer, offset, num);
			this._position += num;
			return num;
		}

		// Token: 0x06000382 RID: 898 RVA: 0x00008D3A File Offset: 0x00006F3A
		public override void Flush()
		{
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000383 RID: 899 RVA: 0x00008D3C File Offset: 0x00006F3C
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000384 RID: 900 RVA: 0x00008D3F File Offset: 0x00006F3F
		public override bool CanSeek
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000385 RID: 901 RVA: 0x00008D42 File Offset: 0x00006F42
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000386 RID: 902 RVA: 0x00008D45 File Offset: 0x00006F45
		public override long Length
		{
			get
			{
				return (long)this._length;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000387 RID: 903 RVA: 0x00008D4E File Offset: 0x00006F4E
		// (set) Token: 0x06000388 RID: 904 RVA: 0x00008D57 File Offset: 0x00006F57
		public override long Position
		{
			get
			{
				return (long)this._position;
			}
			set
			{
				this.Seek(value, SeekOrigin.Begin);
			}
		}

		// Token: 0x06000389 RID: 905 RVA: 0x00008D64 File Offset: 0x00006F64
		public override long Seek(long offset, SeekOrigin origin)
		{
			long num;
			checked
			{
				try
				{
					switch (origin)
					{
					case SeekOrigin.Begin:
						num = offset;
						break;
					case SeekOrigin.Current:
						num = offset + unchecked((long)this._position);
						break;
					case SeekOrigin.End:
						num = offset + unchecked((long)this._length);
						break;
					default:
						throw new ArgumentOutOfRangeException("origin");
					}
				}
				catch (OverflowException)
				{
					throw new ArgumentOutOfRangeException("offset");
				}
				if (num < 0L || num > 2147483647L)
				{
					throw new ArgumentOutOfRangeException("offset");
				}
			}
			this._position = (int)num;
			return num;
		}

		// Token: 0x0600038A RID: 906 RVA: 0x00008DEC File Offset: 0x00006FEC
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600038B RID: 907 RVA: 0x00008DF3 File Offset: 0x00006FF3
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0400049E RID: 1182
		[SecurityCritical]
		private unsafe readonly byte* _data;

		// Token: 0x0400049F RID: 1183
		private readonly int _length;

		// Token: 0x040004A0 RID: 1184
		private int _position;
	}
}
