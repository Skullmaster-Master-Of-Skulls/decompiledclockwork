using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;

namespace System.Data.SqlClient
{
	// Token: 0x020001FC RID: 508
	internal sealed class SqlCachedStream : Stream
	{
		// Token: 0x06001F7D RID: 8061 RVA: 0x000D9758 File Offset: 0x000D8B58
		internal SqlCachedStream(SqlCachedBuffer sqlBuf)
		{
			this._cachedBytes = sqlBuf.CachedBytes;
		}

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x06001F7E RID: 8062 RVA: 0x000D9778 File Offset: 0x000D8B78
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x06001F7F RID: 8063 RVA: 0x000D9788 File Offset: 0x000D8B88
		public override bool CanSeek
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x06001F80 RID: 8064 RVA: 0x000D9798 File Offset: 0x000D8B98
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x06001F81 RID: 8065 RVA: 0x000D97A8 File Offset: 0x000D8BA8
		public override long Length
		{
			get
			{
				return this.TotalLength;
			}
		}

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x06001F82 RID: 8066 RVA: 0x000D97BC File Offset: 0x000D8BBC
		// (set) Token: 0x06001F83 RID: 8067 RVA: 0x000D9804 File Offset: 0x000D8C04
		public override long Position
		{
			get
			{
				long num = 0L;
				if (this._currentArrayIndex > 0)
				{
					for (int i = 0; i < this._currentArrayIndex; i++)
					{
						num += (long)this._cachedBytes[i].Length;
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

		// Token: 0x06001F84 RID: 8068 RVA: 0x000D9830 File Offset: 0x000D8C30
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

		// Token: 0x06001F85 RID: 8069 RVA: 0x000D9898 File Offset: 0x000D8C98
		public override void Flush()
		{
			throw ADP.NotSupported();
		}

		// Token: 0x06001F86 RID: 8070 RVA: 0x000D98AC File Offset: 0x000D8CAC
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
			if (this._cachedBytes.Count <= this._currentArrayIndex)
			{
				return 0;
			}
			while (count > 0)
			{
				if (this._cachedBytes[this._currentArrayIndex].Length <= this._currentPosition)
				{
					this._currentArrayIndex++;
					if (this._cachedBytes.Count <= this._currentArrayIndex)
					{
						break;
					}
					this._currentPosition = 0;
				}
				int num2 = this._cachedBytes[this._currentArrayIndex].Length - this._currentPosition;
				if (num2 > count)
				{
					num2 = count;
				}
				Array.Copy(this._cachedBytes[this._currentArrayIndex], this._currentPosition, buffer, offset, num2);
				this._currentPosition += num2;
				count -= num2;
				offset += num2;
				num += num2;
			}
			return num;
		}

		// Token: 0x06001F87 RID: 8071 RVA: 0x000D99D4 File Offset: 0x000D8DD4
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

		// Token: 0x06001F88 RID: 8072 RVA: 0x000D9A54 File Offset: 0x000D8E54
		public override void SetLength(long value)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x06001F89 RID: 8073 RVA: 0x000D9A68 File Offset: 0x000D8E68
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x06001F8A RID: 8074 RVA: 0x000D9A7C File Offset: 0x000D8E7C
		private void SetInternalPosition(long lPos, string argumentName)
		{
			long num = lPos;
			if (num < 0L)
			{
				throw new ArgumentOutOfRangeException(argumentName);
			}
			for (int i = 0; i < this._cachedBytes.Count; i++)
			{
				if (num <= (long)this._cachedBytes[i].Length)
				{
					this._currentArrayIndex = i;
					this._currentPosition = (int)num;
					return;
				}
				num -= (long)this._cachedBytes[i].Length;
			}
			if (num > 0L)
			{
				throw new ArgumentOutOfRangeException(argumentName);
			}
		}

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x06001F8B RID: 8075 RVA: 0x000D9AF0 File Offset: 0x000D8EF0
		private long TotalLength
		{
			get
			{
				if (this._totalLength == 0L && this._cachedBytes != null)
				{
					long num = 0L;
					for (int i = 0; i < this._cachedBytes.Count; i++)
					{
						num += (long)this._cachedBytes[i].Length;
					}
					this._totalLength = num;
				}
				return this._totalLength;
			}
		}

		// Token: 0x040011D8 RID: 4568
		private int _currentPosition;

		// Token: 0x040011D9 RID: 4569
		private int _currentArrayIndex;

		// Token: 0x040011DA RID: 4570
		private List<byte[]> _cachedBytes;

		// Token: 0x040011DB RID: 4571
		private long _totalLength;
	}
}
