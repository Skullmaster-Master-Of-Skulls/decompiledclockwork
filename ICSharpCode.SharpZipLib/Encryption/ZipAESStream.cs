using System;
using System.IO;
using System.Security.Cryptography;

namespace ICSharpCode.SharpZipLib.Encryption
{
	// Token: 0x02000003 RID: 3
	internal class ZipAESStream : CryptoStream
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000021C5 File Offset: 0x000011C5
		public ZipAESStream(Stream stream, ZipAESTransform transform, CryptoStreamMode mode) : base(stream, transform, mode)
		{
			this._stream = stream;
			this._transform = transform;
			this._slideBuffer = new byte[1024];
			this._blockAndAuth = 26;
			if (mode != CryptoStreamMode.Read)
			{
				throw new Exception("ZipAESStream only for read");
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002204 File Offset: 0x00001204
		public override int Read(byte[] outBuffer, int offset, int count)
		{
			int i = 0;
			while (i < count)
			{
				int num = this._slideBufFreePos - this._slideBufStartPos;
				int num2 = this._blockAndAuth - num;
				if (this._slideBuffer.Length - this._slideBufFreePos < num2)
				{
					int num3 = 0;
					int j = this._slideBufStartPos;
					while (j < this._slideBufFreePos)
					{
						this._slideBuffer[num3] = this._slideBuffer[j];
						j++;
						num3++;
					}
					this._slideBufFreePos -= this._slideBufStartPos;
					this._slideBufStartPos = 0;
				}
				int num4 = this._stream.Read(this._slideBuffer, this._slideBufFreePos, num2);
				this._slideBufFreePos += num4;
				num = this._slideBufFreePos - this._slideBufStartPos;
				if (num < this._blockAndAuth)
				{
					if (num > 10)
					{
						int num5 = num - 10;
						this._transform.TransformBlock(this._slideBuffer, this._slideBufStartPos, num5, outBuffer, offset);
						i += num5;
						this._slideBufStartPos += num5;
					}
					else if (num < 10)
					{
						throw new Exception("Internal error missed auth code");
					}
					byte[] authCode = this._transform.GetAuthCode();
					for (int k = 0; k < 10; k++)
					{
						if (authCode[k] != this._slideBuffer[this._slideBufStartPos + k])
						{
							throw new Exception("AES Authentication Code does not match. This is a super-CRC check on the data in the file after compression and encryption. \r\nThe file may be damaged.");
						}
					}
					break;
				}
				this._transform.TransformBlock(this._slideBuffer, this._slideBufStartPos, 16, outBuffer, offset);
				i += 16;
				offset += 16;
				this._slideBufStartPos += 16;
			}
			return i;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x0000239E File Offset: 0x0000139E
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000001 RID: 1
		private const int AUTH_CODE_LENGTH = 10;

		// Token: 0x04000002 RID: 2
		private const int CRYPTO_BLOCK_SIZE = 16;

		// Token: 0x04000003 RID: 3
		private Stream _stream;

		// Token: 0x04000004 RID: 4
		private ZipAESTransform _transform;

		// Token: 0x04000005 RID: 5
		private byte[] _slideBuffer;

		// Token: 0x04000006 RID: 6
		private int _slideBufStartPos;

		// Token: 0x04000007 RID: 7
		private int _slideBufFreePos;

		// Token: 0x04000008 RID: 8
		private int _blockAndAuth;
	}
}
