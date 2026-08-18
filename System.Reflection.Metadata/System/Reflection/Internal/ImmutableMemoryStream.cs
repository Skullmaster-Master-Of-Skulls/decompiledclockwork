using System;
using System.Collections.Immutable;
using System.IO;

namespace System.Reflection.Internal
{
	// Token: 0x02000162 RID: 354
	internal sealed class ImmutableMemoryStream : Stream
	{
		// Token: 0x06000AF3 RID: 2803 RVA: 0x0001F48F File Offset: 0x0001D68F
		internal ImmutableMemoryStream(ImmutableArray<byte> array)
		{
			this._array = array;
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x0001F49E File Offset: 0x0001D69E
		public ImmutableArray<byte> GetBuffer()
		{
			return this._array;
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000AF5 RID: 2805 RVA: 0x0001F4A6 File Offset: 0x0001D6A6
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000AF6 RID: 2806 RVA: 0x0001F4A6 File Offset: 0x0001D6A6
		public override bool CanSeek
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000AF7 RID: 2807 RVA: 0x0000206D File Offset: 0x0000026D
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000AF8 RID: 2808 RVA: 0x0001F4AC File Offset: 0x0001D6AC
		public override long Length
		{
			get
			{
				return (long)this._array.Length;
			}
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000AF9 RID: 2809 RVA: 0x0001F4C8 File Offset: 0x0001D6C8
		// (set) Token: 0x06000AFA RID: 2810 RVA: 0x0001F4D4 File Offset: 0x0001D6D4
		public override long Position
		{
			get
			{
				return (long)this._position;
			}
			set
			{
				if (value < 0L || value >= (long)this._array.Length)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._position = (int)value;
			}
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x000031EB File Offset: 0x000013EB
		public override void Flush()
		{
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x0001F50C File Offset: 0x0001D70C
		public override int Read(byte[] buffer, int offset, int count)
		{
			int num = Math.Min(count, this._array.Length - this._position);
			this._array.CopyTo(this._position, buffer, offset, num);
			this._position += num;
			return num;
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x0001F55C File Offset: 0x0001D75C
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
						num = offset + unchecked((long)this._array.Length);
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
			if (num < 0L || num >= (long)this._array.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			this._position = (int)num;
			return num;
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x0001F5F8 File Offset: 0x0001D7F8
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x0001F5F8 File Offset: 0x0001D7F8
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x04000914 RID: 2324
		private readonly ImmutableArray<byte> _array;

		// Token: 0x04000915 RID: 2325
		private int _position;
	}
}
