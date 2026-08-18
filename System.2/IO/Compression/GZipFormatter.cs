using System;

namespace System.IO.Compression
{
	// Token: 0x02000430 RID: 1072
	internal class GZipFormatter : IFileFormatWriter
	{
		// Token: 0x06002837 RID: 10295 RVA: 0x000B8A0B File Offset: 0x000B6C0B
		internal GZipFormatter() : this(3)
		{
		}

		// Token: 0x06002838 RID: 10296 RVA: 0x000B8A14 File Offset: 0x000B6C14
		internal GZipFormatter(int compressionLevel)
		{
			if (compressionLevel == 10)
			{
				this.headerBytes[8] = 2;
			}
		}

		// Token: 0x06002839 RID: 10297 RVA: 0x000B8A42 File Offset: 0x000B6C42
		public byte[] GetHeader()
		{
			return this.headerBytes;
		}

		// Token: 0x0600283A RID: 10298 RVA: 0x000B8A4C File Offset: 0x000B6C4C
		public void UpdateWithBytesRead(byte[] buffer, int offset, int bytesToCopy)
		{
			this._crc32 = Crc32Helper.UpdateCrc32(this._crc32, buffer, offset, bytesToCopy);
			long num = this._inputStreamSizeModulo + (long)((ulong)bytesToCopy);
			if (num >= 4294967296L)
			{
				num %= 4294967296L;
			}
			this._inputStreamSizeModulo = num;
		}

		// Token: 0x0600283B RID: 10299 RVA: 0x000B8A98 File Offset: 0x000B6C98
		public byte[] GetFooter()
		{
			byte[] array = new byte[8];
			this.WriteUInt32(array, this._crc32, 0);
			this.WriteUInt32(array, (uint)this._inputStreamSizeModulo, 4);
			return array;
		}

		// Token: 0x0600283C RID: 10300 RVA: 0x000B8ACA File Offset: 0x000B6CCA
		internal void WriteUInt32(byte[] b, uint value, int startIndex)
		{
			b[startIndex] = (byte)value;
			b[startIndex + 1] = (byte)(value >> 8);
			b[startIndex + 2] = (byte)(value >> 16);
			b[startIndex + 3] = (byte)(value >> 24);
		}

		// Token: 0x040021EA RID: 8682
		private byte[] headerBytes = new byte[]
		{
			31,
			139,
			8,
			0,
			0,
			0,
			0,
			0,
			4,
			0
		};

		// Token: 0x040021EB RID: 8683
		private uint _crc32;

		// Token: 0x040021EC RID: 8684
		private long _inputStreamSizeModulo;
	}
}
