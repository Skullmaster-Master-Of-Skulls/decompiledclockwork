using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x0200000F RID: 15
	internal class TlsBlockCipherCipherSuite : TlsCipherSuite
	{
		// Token: 0x06000068 RID: 104 RVA: 0x00005073 File Offset: 0x00004073
		internal TlsBlockCipherCipherSuite(IBlockCipher encrypt, IBlockCipher decrypt, IDigest writeDigest, IDigest readDigest, int cipherKeySize, short keyExchange)
		{
			this.encryptCipher = encrypt;
			this.decryptCipher = decrypt;
			this.writeDigest = writeDigest;
			this.readDigest = readDigest;
			this.cipherKeySize = cipherKeySize;
			this.keyExchange = keyExchange;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000050A8 File Offset: 0x000040A8
		internal override void Init(TlsProtocolHandler handler, byte[] ms, byte[] cr, byte[] sr)
		{
			this.handler = handler;
			int num = 2 * this.cipherKeySize + this.writeDigest.GetDigestSize() + this.readDigest.GetDigestSize() + this.encryptCipher.GetBlockSize() + this.decryptCipher.GetBlockSize();
			byte[] buf = new byte[num];
			byte[] array = new byte[cr.Length + sr.Length];
			Array.Copy(cr, 0, array, sr.Length, cr.Length);
			Array.Copy(sr, 0, array, 0, sr.Length);
			TlsUtilities.PRF(ms, "key expansion", array, buf);
			int num2 = 0;
			this.writeMac = TlsBlockCipherCipherSuite.CreateTlsMac(this.writeDigest, buf, ref num2);
			this.readMac = TlsBlockCipherCipherSuite.CreateTlsMac(this.readDigest, buf, ref num2);
			KeyParameter key = TlsBlockCipherCipherSuite.CreateKeyParameter(buf, ref num2, this.cipherKeySize);
			KeyParameter key2 = TlsBlockCipherCipherSuite.CreateKeyParameter(buf, ref num2, this.cipherKeySize);
			ParametersWithIV parameters = TlsBlockCipherCipherSuite.CreateParametersWithIV(key, buf, ref num2, this.encryptCipher.GetBlockSize());
			ParametersWithIV parameters2 = TlsBlockCipherCipherSuite.CreateParametersWithIV(key2, buf, ref num2, this.decryptCipher.GetBlockSize());
			if (num2 != num)
			{
				handler.FailWithError(2, 80);
			}
			this.encryptCipher.Init(true, parameters);
			this.decryptCipher.Init(false, parameters2);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000051D8 File Offset: 0x000041D8
		private static TlsMac CreateTlsMac(IDigest digest, byte[] buf, ref int off)
		{
			int digestSize = digest.GetDigestSize();
			TlsMac result = new TlsMac(digest, buf, off, digestSize);
			off += digestSize;
			return result;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00005200 File Offset: 0x00004200
		private static KeyParameter CreateKeyParameter(byte[] buf, ref int off, int len)
		{
			KeyParameter result = new KeyParameter(buf, off, len);
			off += len;
			return result;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00005220 File Offset: 0x00004220
		private static ParametersWithIV CreateParametersWithIV(KeyParameter key, byte[] buf, ref int off, int len)
		{
			ParametersWithIV result = new ParametersWithIV(key, buf, off, len);
			off += len;
			return result;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00005240 File Offset: 0x00004240
		internal override byte[] EncodePlaintext(short type, byte[] plaintext, int offset, int len)
		{
			int blockSize = this.encryptCipher.GetBlockSize();
			int num = blockSize - (len + this.writeMac.Size + 1) % blockSize;
			int max = (255 - num) / blockSize;
			int num2 = this.chooseExtraPadBlocks(this.handler.Random, max);
			int num3 = num + num2 * blockSize;
			int num4 = len + this.writeMac.Size + num3 + 1;
			byte[] array = new byte[num4];
			Array.Copy(plaintext, offset, array, 0, len);
			byte[] array2 = this.writeMac.CalculateMac(type, plaintext, offset, len);
			Array.Copy(array2, 0, array, len, array2.Length);
			int num5 = len + array2.Length;
			for (int i = 0; i <= num3; i++)
			{
				array[i + num5] = (byte)num3;
			}
			for (int j = 0; j < num4; j += blockSize)
			{
				this.encryptCipher.ProcessBlock(array, j, array, j);
			}
			return array;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x0000532C File Offset: 0x0000432C
		private int chooseExtraPadBlocks(SecureRandom r, int max)
		{
			int x = r.NextInt();
			int val = this.lowestBitSet(x);
			return Math.Min(val, max);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00005350 File Offset: 0x00004350
		private int lowestBitSet(int x)
		{
			if (x == 0)
			{
				return 32;
			}
			int num = 0;
			while ((x & 1) == 0)
			{
				num++;
				x >>= 1;
			}
			return num;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00005378 File Offset: 0x00004378
		internal override byte[] DecodeCiphertext(short type, byte[] ciphertext, int offset, int len)
		{
			int num = this.readMac.Size + 1;
			int blockSize = this.decryptCipher.GetBlockSize();
			bool flag = false;
			if (len < num)
			{
				this.handler.FailWithError(2, 50);
			}
			if (len % blockSize != 0)
			{
				this.handler.FailWithError(2, 21);
			}
			for (int i = 0; i < len; i += blockSize)
			{
				this.decryptCipher.ProcessBlock(ciphertext, i + offset, ciphertext, i + offset);
			}
			int num2 = offset + len - 1;
			byte b = ciphertext[num2];
			int num3 = (int)b;
			int num4 = len - num;
			if (num3 > num4)
			{
				flag = true;
				num3 = 0;
			}
			else
			{
				int num5 = 0;
				for (int j = num2 - num3; j < num2; j++)
				{
					num5 |= (int)(ciphertext[j] ^ b);
				}
				if (num5 != 0)
				{
					flag = true;
					num3 = 0;
				}
			}
			int num6 = len - num - num3;
			byte[] array = this.readMac.CalculateMac(type, ciphertext, offset, num6);
			byte[] array2 = new byte[array.Length];
			Array.Copy(ciphertext, offset + num6, array2, 0, array.Length);
			if (!Arrays.ConstantTimeAreEqual(array, array2))
			{
				flag = true;
			}
			if (flag)
			{
				this.handler.FailWithError(2, 20);
			}
			byte[] array3 = new byte[num6];
			Array.Copy(ciphertext, offset, array3, 0, num6);
			return array3;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000071 RID: 113 RVA: 0x000054A7 File Offset: 0x000044A7
		internal override short KeyExchangeAlgorithm
		{
			get
			{
				return this.keyExchange;
			}
		}

		// Token: 0x0400002F RID: 47
		private TlsProtocolHandler handler;

		// Token: 0x04000030 RID: 48
		private IBlockCipher encryptCipher;

		// Token: 0x04000031 RID: 49
		private IBlockCipher decryptCipher;

		// Token: 0x04000032 RID: 50
		private IDigest writeDigest;

		// Token: 0x04000033 RID: 51
		private IDigest readDigest;

		// Token: 0x04000034 RID: 52
		private TlsMac writeMac;

		// Token: 0x04000035 RID: 53
		private TlsMac readMac;

		// Token: 0x04000036 RID: 54
		private int cipherKeySize;

		// Token: 0x04000037 RID: 55
		private short keyExchange;
	}
}
