using System;

namespace System.Net.Mime
{
	// Token: 0x020006E0 RID: 1760
	internal class WriteStateInfoBase
	{
		// Token: 0x0600364A RID: 13898 RVA: 0x000E7CE4 File Offset: 0x000E6CE4
		internal WriteStateInfoBase()
		{
			this.buffer = new byte[1024];
			this._header = new byte[0];
			this._footer = new byte[0];
			this._maxLineLength = 70;
			this._currentLineLength = 0;
			this._currentBufferUsed = 0;
		}

		// Token: 0x0600364B RID: 13899 RVA: 0x000E7D35 File Offset: 0x000E6D35
		internal WriteStateInfoBase(int bufferSize, byte[] header, byte[] footer, int maxLineLength) : this(bufferSize, header, footer, maxLineLength, 0)
		{
		}

		// Token: 0x0600364C RID: 13900 RVA: 0x000E7D43 File Offset: 0x000E6D43
		internal WriteStateInfoBase(int bufferSize, byte[] header, byte[] footer, int maxLineLength, int mimeHeaderLength)
		{
			this.buffer = new byte[bufferSize];
			this._header = header;
			this._footer = footer;
			this._maxLineLength = maxLineLength;
			this._currentLineLength = mimeHeaderLength;
			this._currentBufferUsed = 0;
		}

		// Token: 0x17000C90 RID: 3216
		// (get) Token: 0x0600364D RID: 13901 RVA: 0x000E7D7C File Offset: 0x000E6D7C
		internal int FooterLength
		{
			get
			{
				return this._footer.Length;
			}
		}

		// Token: 0x17000C91 RID: 3217
		// (get) Token: 0x0600364E RID: 13902 RVA: 0x000E7D86 File Offset: 0x000E6D86
		internal byte[] Footer
		{
			get
			{
				return this._footer;
			}
		}

		// Token: 0x17000C92 RID: 3218
		// (get) Token: 0x0600364F RID: 13903 RVA: 0x000E7D8E File Offset: 0x000E6D8E
		internal byte[] Header
		{
			get
			{
				return this._header;
			}
		}

		// Token: 0x17000C93 RID: 3219
		// (get) Token: 0x06003650 RID: 13904 RVA: 0x000E7D96 File Offset: 0x000E6D96
		internal byte[] Buffer
		{
			get
			{
				return this.buffer;
			}
		}

		// Token: 0x17000C94 RID: 3220
		// (get) Token: 0x06003651 RID: 13905 RVA: 0x000E7D9E File Offset: 0x000E6D9E
		internal int Length
		{
			get
			{
				return this._currentBufferUsed;
			}
		}

		// Token: 0x17000C95 RID: 3221
		// (get) Token: 0x06003652 RID: 13906 RVA: 0x000E7DA6 File Offset: 0x000E6DA6
		internal int CurrentLineLength
		{
			get
			{
				return this._currentLineLength;
			}
		}

		// Token: 0x06003653 RID: 13907 RVA: 0x000E7DB0 File Offset: 0x000E6DB0
		private void EnsureSpaceInBuffer(int moreBytes)
		{
			int num = this.Buffer.Length;
			while (this._currentBufferUsed + moreBytes >= num)
			{
				num *= 2;
			}
			if (num > this.Buffer.Length)
			{
				byte[] array = new byte[num];
				this.buffer.CopyTo(array, 0);
				this.buffer = array;
			}
		}

		// Token: 0x06003654 RID: 13908 RVA: 0x000E7E00 File Offset: 0x000E6E00
		internal void Append(byte aByte)
		{
			this.EnsureSpaceInBuffer(1);
			this.Buffer[this._currentBufferUsed++] = aByte;
			this._currentLineLength++;
		}

		// Token: 0x06003655 RID: 13909 RVA: 0x000E7E3B File Offset: 0x000E6E3B
		internal void Append(params byte[] bytes)
		{
			this.EnsureSpaceInBuffer(bytes.Length);
			bytes.CopyTo(this.buffer, this.Length);
			this._currentLineLength += bytes.Length;
			this._currentBufferUsed += bytes.Length;
		}

		// Token: 0x06003656 RID: 13910 RVA: 0x000E7E78 File Offset: 0x000E6E78
		internal void AppendCRLF(bool includeSpace)
		{
			this.AppendFooter();
			this.Append(new byte[]
			{
				13,
				10
			});
			this._currentLineLength = 0;
			if (includeSpace)
			{
				this.Append(32);
			}
			this.AppendHeader();
		}

		// Token: 0x06003657 RID: 13911 RVA: 0x000E7EBB File Offset: 0x000E6EBB
		internal void AppendHeader()
		{
			if (this.Header != null && this.Header.Length != 0)
			{
				this.Append(this.Header);
			}
		}

		// Token: 0x06003658 RID: 13912 RVA: 0x000E7EDB File Offset: 0x000E6EDB
		internal void AppendFooter()
		{
			if (this.Footer != null && this.Footer.Length != 0)
			{
				this.Append(this.Footer);
			}
		}

		// Token: 0x17000C96 RID: 3222
		// (get) Token: 0x06003659 RID: 13913 RVA: 0x000E7EFB File Offset: 0x000E6EFB
		internal int MaxLineLength
		{
			get
			{
				return this._maxLineLength;
			}
		}

		// Token: 0x0600365A RID: 13914 RVA: 0x000E7F03 File Offset: 0x000E6F03
		internal void Reset()
		{
			this._currentBufferUsed = 0;
			this._currentLineLength = 0;
		}

		// Token: 0x0600365B RID: 13915 RVA: 0x000E7F13 File Offset: 0x000E6F13
		internal void BufferFlushed()
		{
			this._currentBufferUsed = 0;
		}

		// Token: 0x04003180 RID: 12672
		protected const int defaultBufferSize = 1024;

		// Token: 0x04003181 RID: 12673
		private const int defaultMaxLineLength = 70;

		// Token: 0x04003182 RID: 12674
		protected byte[] _header;

		// Token: 0x04003183 RID: 12675
		protected byte[] _footer;

		// Token: 0x04003184 RID: 12676
		protected int _maxLineLength;

		// Token: 0x04003185 RID: 12677
		protected byte[] buffer;

		// Token: 0x04003186 RID: 12678
		protected int _currentLineLength;

		// Token: 0x04003187 RID: 12679
		protected int _currentBufferUsed;
	}
}
