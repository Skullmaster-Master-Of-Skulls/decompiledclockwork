using System;
using System.IO;
using System.Runtime.InteropServices;

namespace System.Reflection.Internal
{
	// Token: 0x02000168 RID: 360
	internal sealed class ReadOnlyUnmanagedMemoryStream : Stream
	{
		// Token: 0x06000B40 RID: 2880 RVA: 0x0002074F File Offset: 0x0001E94F
		public unsafe ReadOnlyUnmanagedMemoryStream(byte* data, int length)
		{
			this._data = data;
			this._length = length;
		}

		// Token: 0x06000B41 RID: 2881 RVA: 0x00020768 File Offset: 0x0001E968
		public unsafe override int ReadByte()
		{
			if (this._position == this._length)
			{
				return -1;
			}
			int data = this._data;
			int position = this._position;
			this._position = position + 1;
			return (int)(*(data + position));
		}

		// Token: 0x06000B42 RID: 2882 RVA: 0x000207A0 File Offset: 0x0001E9A0
		public unsafe override int Read(byte[] buffer, int offset, int count)
		{
			int num = Math.Min(count, this._length - this._position);
			Marshal.Copy((IntPtr)((void*)(this._data + this._position)), buffer, offset, num);
			this._position += num;
			return num;
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x000031EB File Offset: 0x000013EB
		public override void Flush()
		{
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000B44 RID: 2884 RVA: 0x0001F4A6 File Offset: 0x0001D6A6
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000B45 RID: 2885 RVA: 0x0001F4A6 File Offset: 0x0001D6A6
		public override bool CanSeek
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000B46 RID: 2886 RVA: 0x0000206D File Offset: 0x0000026D
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06000B47 RID: 2887 RVA: 0x000207EA File Offset: 0x0001E9EA
		public override long Length
		{
			get
			{
				return (long)this._length;
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000B48 RID: 2888 RVA: 0x000207F3 File Offset: 0x0001E9F3
		// (set) Token: 0x06000B49 RID: 2889 RVA: 0x000207FC File Offset: 0x0001E9FC
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

		// Token: 0x06000B4A RID: 2890 RVA: 0x00020808 File Offset: 0x0001EA08
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
			}
			if (num < 0L || num >= (long)this._length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			this._position = (int)num;
			return num;
		}

		// Token: 0x06000B4B RID: 2891 RVA: 0x0001F5F8 File Offset: 0x0001D7F8
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000B4C RID: 2892 RVA: 0x0001F5F8 File Offset: 0x0001D7F8
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0400092E RID: 2350
		private unsafe readonly byte* _data;

		// Token: 0x0400092F RID: 2351
		private readonly int _length;

		// Token: 0x04000930 RID: 2352
		private int _position;
	}
}
