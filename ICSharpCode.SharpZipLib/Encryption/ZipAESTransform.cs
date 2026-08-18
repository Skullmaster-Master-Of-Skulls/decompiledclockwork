using System;
using System.Security.Cryptography;

namespace ICSharpCode.SharpZipLib.Encryption
{
	// Token: 0x02000004 RID: 4
	internal class ZipAESTransform : ICryptoTransform, IDisposable
	{
		// Token: 0x06000006 RID: 6 RVA: 0x000023A8 File Offset: 0x000013A8
		public ZipAESTransform(string key, byte[] saltBytes, int blockSize, bool writeMode)
		{
			if (blockSize != 16 && blockSize != 32)
			{
				throw new Exception("Invalid blocksize " + blockSize + ". Must be 16 or 32.");
			}
			if (saltBytes.Length != blockSize / 2)
			{
				throw new Exception(string.Concat(new object[]
				{
					"Invalid salt len. Must be ",
					blockSize / 2,
					" for blocksize ",
					blockSize
				}));
			}
			this._blockSize = blockSize;
			this._encryptBuffer = new byte[this._blockSize];
			this._encrPos = 16;
			Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(key, saltBytes, 1000);
			RijndaelManaged rijndaelManaged = new RijndaelManaged();
			rijndaelManaged.Mode = CipherMode.ECB;
			this._counterNonce = new byte[this._blockSize];
			byte[] bytes = rfc2898DeriveBytes.GetBytes(this._blockSize);
			byte[] bytes2 = rfc2898DeriveBytes.GetBytes(this._blockSize);
			this._encryptor = rijndaelManaged.CreateEncryptor(bytes, bytes2);
			this._pwdVerifier = rfc2898DeriveBytes.GetBytes(2);
			this._hmacsha1 = new HMACSHA1(bytes2);
			this._writeMode = writeMode;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000024BC File Offset: 0x000014BC
		public int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
		{
			if (!this._writeMode)
			{
				this._hmacsha1.TransformBlock(inputBuffer, inputOffset, inputCount, inputBuffer, inputOffset);
			}
			for (int i = 0; i < inputCount; i++)
			{
				if (this._encrPos == 16)
				{
					int num = 0;
					for (;;)
					{
						byte[] counterNonce = this._counterNonce;
						int num2 = num;
						if ((counterNonce[num2] += 1) != 0)
						{
							break;
						}
						num++;
					}
					this._encryptor.TransformBlock(this._counterNonce, 0, this._blockSize, this._encryptBuffer, 0);
					this._encrPos = 0;
				}
				outputBuffer[i + outputOffset] = (inputBuffer[i + inputOffset] ^ this._encryptBuffer[this._encrPos++]);
			}
			if (this._writeMode)
			{
				this._hmacsha1.TransformBlock(outputBuffer, outputOffset, inputCount, outputBuffer, outputOffset);
			}
			return inputCount;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002590 File Offset: 0x00001590
		public byte[] PwdVerifier
		{
			get
			{
				return this._pwdVerifier;
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002598 File Offset: 0x00001598
		public byte[] GetAuthCode()
		{
			if (!this._finalised)
			{
				byte[] inputBuffer = new byte[0];
				this._hmacsha1.TransformFinalBlock(inputBuffer, 0, 0);
				this._finalised = true;
			}
			return this._hmacsha1.Hash;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000025D5 File Offset: 0x000015D5
		public byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
		{
			throw new NotImplementedException("ZipAESTransform.TransformFinalBlock");
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000025E1 File Offset: 0x000015E1
		public int InputBlockSize
		{
			get
			{
				return this._blockSize;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000025E9 File Offset: 0x000015E9
		public int OutputBlockSize
		{
			get
			{
				return this._blockSize;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000025F1 File Offset: 0x000015F1
		public bool CanTransformMultipleBlocks
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000025F4 File Offset: 0x000015F4
		public bool CanReuseTransform
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000025F7 File Offset: 0x000015F7
		public void Dispose()
		{
			this._encryptor.Dispose();
		}

		// Token: 0x04000009 RID: 9
		private const int PWD_VER_LENGTH = 2;

		// Token: 0x0400000A RID: 10
		private const int KEY_ROUNDS = 1000;

		// Token: 0x0400000B RID: 11
		private const int ENCRYPT_BLOCK = 16;

		// Token: 0x0400000C RID: 12
		private int _blockSize;

		// Token: 0x0400000D RID: 13
		private ICryptoTransform _encryptor;

		// Token: 0x0400000E RID: 14
		private readonly byte[] _counterNonce;

		// Token: 0x0400000F RID: 15
		private byte[] _encryptBuffer;

		// Token: 0x04000010 RID: 16
		private int _encrPos;

		// Token: 0x04000011 RID: 17
		private byte[] _pwdVerifier;

		// Token: 0x04000012 RID: 18
		private HMACSHA1 _hmacsha1;

		// Token: 0x04000013 RID: 19
		private bool _finalised;

		// Token: 0x04000014 RID: 20
		private bool _writeMode;
	}
}
