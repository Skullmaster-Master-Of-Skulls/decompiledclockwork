using System;
using System.IO;

namespace SevenZip.Compression.LZ
{
	// Token: 0x02000012 RID: 18
	public class OutWindow
	{
		// Token: 0x0600006C RID: 108 RVA: 0x00005B7B File Offset: 0x00003D7B
		public void Create(uint windowSize)
		{
			if (this._windowSize != windowSize)
			{
				this._buffer = new byte[windowSize];
			}
			this._windowSize = windowSize;
			this._pos = 0U;
			this._streamPos = 0U;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00005BA8 File Offset: 0x00003DA8
		public void Init(Stream stream, bool solid)
		{
			this.ReleaseStream();
			this._stream = stream;
			if (!solid)
			{
				this._streamPos = 0U;
				this._pos = 0U;
			}
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00005BC8 File Offset: 0x00003DC8
		public void Init(Stream stream)
		{
			this.Init(stream, false);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00005BD2 File Offset: 0x00003DD2
		public void ReleaseStream()
		{
			this.Flush();
			this._stream = null;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00005BE4 File Offset: 0x00003DE4
		public void Flush()
		{
			uint num = this._pos - this._streamPos;
			if (num == 0U)
			{
				return;
			}
			this._stream.Write(this._buffer, (int)this._streamPos, (int)num);
			if (this._pos >= this._windowSize)
			{
				this._pos = 0U;
			}
			this._streamPos = this._pos;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00005C3C File Offset: 0x00003E3C
		public void CopyBlock(uint distance, uint len)
		{
			uint num = this._pos - distance - 1U;
			if (num >= this._windowSize)
			{
				num += this._windowSize;
			}
			while (len > 0U)
			{
				if (num >= this._windowSize)
				{
					num = 0U;
				}
				this._buffer[(int)((UIntPtr)(this._pos++))] = this._buffer[(int)((UIntPtr)(num++))];
				if (this._pos >= this._windowSize)
				{
					this.Flush();
				}
				len -= 1U;
			}
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00005CB8 File Offset: 0x00003EB8
		public void PutByte(byte b)
		{
			this._buffer[(int)((UIntPtr)(this._pos++))] = b;
			if (this._pos >= this._windowSize)
			{
				this.Flush();
			}
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00005CF4 File Offset: 0x00003EF4
		public byte GetByte(uint distance)
		{
			uint num = this._pos - distance - 1U;
			if (num >= this._windowSize)
			{
				num += this._windowSize;
			}
			return this._buffer[(int)((UIntPtr)num)];
		}

		// Token: 0x04000078 RID: 120
		private byte[] _buffer;

		// Token: 0x04000079 RID: 121
		private uint _pos;

		// Token: 0x0400007A RID: 122
		private uint _windowSize;

		// Token: 0x0400007B RID: 123
		private uint _streamPos;

		// Token: 0x0400007C RID: 124
		private Stream _stream;
	}
}
