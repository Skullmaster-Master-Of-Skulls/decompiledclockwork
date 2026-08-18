using System;
using System.Collections.Immutable;
using System.IO;

namespace System.Reflection.Internal
{
	// Token: 0x02000088 RID: 136
	internal sealed class ImmutableMemoryStream : Stream
	{
		// Token: 0x06000363 RID: 867 RVA: 0x000088B9 File Offset: 0x00006AB9
		internal ImmutableMemoryStream(ImmutableArray<byte> array)
		{
			this._array = array;
		}

		// Token: 0x06000364 RID: 868 RVA: 0x000088C8 File Offset: 0x00006AC8
		public ImmutableArray<byte> GetBuffer()
		{
			return this._array;
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000365 RID: 869 RVA: 0x000088D0 File Offset: 0x00006AD0
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000366 RID: 870 RVA: 0x000088D3 File Offset: 0x00006AD3
		public override bool CanSeek
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000367 RID: 871 RVA: 0x000088D6 File Offset: 0x00006AD6
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000368 RID: 872 RVA: 0x000088DC File Offset: 0x00006ADC
		public override long Length
		{
			get
			{
				return (long)this._array.Length;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000369 RID: 873 RVA: 0x000088F8 File Offset: 0x00006AF8
		// (set) Token: 0x0600036A RID: 874 RVA: 0x00008904 File Offset: 0x00006B04
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

		// Token: 0x0600036B RID: 875 RVA: 0x0000893B File Offset: 0x00006B3B
		public override void Flush()
		{
		}

		// Token: 0x0600036C RID: 876 RVA: 0x00008940 File Offset: 0x00006B40
		public override int Read(byte[] buffer, int offset, int count)
		{
			int num = Math.Min(count, this._array.Length - this._position);
			this._array.CopyTo(this._position, buffer, offset, num);
			this._position += num;
			return num;
		}

		// Token: 0x0600036D RID: 877 RVA: 0x00008990 File Offset: 0x00006B90
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

		// Token: 0x0600036E RID: 878 RVA: 0x00008A2C File Offset: 0x00006C2C
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600036F RID: 879 RVA: 0x00008A33 File Offset: 0x00006C33
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x04000495 RID: 1173
		private readonly ImmutableArray<byte> _array;

		// Token: 0x04000496 RID: 1174
		private int _position;
	}
}
