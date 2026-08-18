using System;
using System.Collections;
using System.Data.Common;
using System.IO;

namespace System.Data.SqlClient
{
	// Token: 0x0200030F RID: 783
	internal sealed class SqlCachedStream : Stream
	{
		// Token: 0x060028E0 RID: 10464 RVA: 0x002B2738 File Offset: 0x002B1B38
		internal SqlCachedStream(SqlCachedBuffer sqlBuf)
		{
			this._cachedBytes = sqlBuf.CachedBytes;
		}

		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x060028E1 RID: 10465 RVA: 0x002B2758 File Offset: 0x002B1B58
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x060028E2 RID: 10466 RVA: 0x002B2768 File Offset: 0x002B1B68
		public override bool CanSeek
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x060028E3 RID: 10467 RVA: 0x002B2778 File Offset: 0x002B1B78
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x060028E4 RID: 10468 RVA: 0x002B2788 File Offset: 0x002B1B88
		public override long Length
		{
			get
			{
				return this.TotalLength;
			}
		}

		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x060028E5 RID: 10469 RVA: 0x002B27A8 File Offset: 0x002B1BA8
		// (set) Token: 0x060028E6 RID: 10470 RVA: 0x002B27F8 File Offset: 0x002B1BF8
		public override long Position
		{
			get
			{
				long num = 0L;
				if (this._currentArrayIndex > 0)
				{
					for (int i = 0; i < this._currentArrayIndex; i++)
					{
						byte[] array = (byte[])this._cachedBytes[i];
						num += (long)array.Length;
					}
				}
				return num + (long)this._currentPosition;
			}
			set
			{
				if (this._cachedBytes == null)
				{
					throw ADP.StreamClosed("set_Position");
				}
				this.SetInternalPosition(value, "set_Position");
			}
		}

		// Token: 0x060028E7 RID: 10471 RVA: 0x002B2828 File Offset: 0x002B1C28
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing && this._cachedBytes != null)
				{
					this._cachedBytes.Clear();
				}
				this._cachedBytes = null;
				this._currentPosition = 0;
				this._currentArrayIndex = 0;
				this._totalLength = 0L;
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x060028E8 RID: 10472 RVA: 0x002B2898 File Offset: 0x002B1C98
		public override void Flush()
		{
			throw ADP.NotSupported();
		}

		// Token: 0x060028E9 RID: 10473 RVA: 0x002B28B8 File Offset: 0x002B1CB8
		public override int Read(byte[] buffer, int offset, int count)
		{
			int num = 0;
			if (this._cachedBytes == null)
			{
				throw ADP.StreamClosed("Read");
			}
			if (buffer == null)
			{
				throw ADP.ArgumentNull("buffer");
			}
			if (offset < 0 || count < 0)
			{
				throw ADP.ArgumentOutOfRange(string.Empty, (offset < 0) ? "offset" : "count");
			}
			if (buffer.Length - offset < count)
			{
				throw ADP.ArgumentOutOfRange("count");
			}
			if (this._cachedBytes.Count > this._currentArrayIndex)
			{
				byte[] array = (byte[])this._cachedBytes[this._currentArrayIndex];
				while (count > 0)
				{
					if (array.Length <= this._currentPosition)
					{
						this._currentArrayIndex++;
						if (this._cachedBytes.Count <= this._currentArrayIndex)
						{
							break;
						}
						array = (byte[])this._cachedBytes[this._currentArrayIndex];
						this._currentPosition = 0;
					}
					int num2 = array.Length - this._currentPosition;
					if (num2 > count)
					{
						num2 = count;
					}
					Array.Copy(array, this._currentPosition, buffer, offset, num2);
					this._currentPosition += num2;
					count -= num2;
					offset += num2;
					num += num2;
				}
				return num;
			}
			return 0;
		}

		// Token: 0x060028EA RID: 10474 RVA: 0x002B29E8 File Offset: 0x002B1DE8
		public override long Seek(long offset, SeekOrigin origin)
		{
			long num = 0L;
			if (this._cachedBytes == null)
			{
				throw ADP.StreamClosed("Read");
			}
			switch (origin)
			{
			case SeekOrigin.Begin:
				this.SetInternalPosition(offset, "offset");
				break;
			case SeekOrigin.Current:
				num = offset + this.Position;
				this.SetInternalPosition(num, "offset");
				break;
			case SeekOrigin.End:
				num = this.TotalLength + offset;
				this.SetInternalPosition(num, "offset");
				break;
			default:
				throw ADP.InvalidSeekOrigin("offset");
			}
			return num;
		}

		// Token: 0x060028EB RID: 10475 RVA: 0x002B2A78 File Offset: 0x002B1E78
		public override void SetLength(long value)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x060028EC RID: 10476 RVA: 0x002B2A98 File Offset: 0x002B1E98
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x060028ED RID: 10477 RVA: 0x002B2AB8 File Offset: 0x002B1EB8
		private void SetInternalPosition(long lPos, string argumentName)
		{
			long num = lPos;
			if (num < 0L)
			{
				throw new ArgumentOutOfRangeException(argumentName);
			}
			for (int i = 0; i < this._cachedBytes.Count; i++)
			{
				byte[] array = (byte[])this._cachedBytes[i];
				if (num <= (long)array.Length)
				{
					this._currentArrayIndex = i;
					this._currentPosition = (int)num;
					return;
				}
				num -= (long)array.Length;
			}
			if (num > 0L)
			{
				throw new ArgumentOutOfRangeException(argumentName);
			}
		}

		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x060028EE RID: 10478 RVA: 0x002B2B38 File Offset: 0x002B1F38
		private long TotalLength
		{
			get
			{
				if (this._totalLength == 0L && this._cachedBytes != null)
				{
					long num = 0L;
					for (int i = 0; i < this._cachedBytes.Count; i++)
					{
						byte[] array = (byte[])this._cachedBytes[i];
						num += (long)array.Length;
					}
					this._totalLength = num;
				}
				return this._totalLength;
			}
		}

		// Token: 0x040019A1 RID: 6561
		private int _currentPosition;

		// Token: 0x040019A2 RID: 6562
		private int _currentArrayIndex;

		// Token: 0x040019A3 RID: 6563
		private ArrayList _cachedBytes;

		// Token: 0x040019A4 RID: 6564
		private long _totalLength;
	}
}
