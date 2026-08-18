using System;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Paddings;

namespace Org.BouncyCastle.Crypto.Macs
{
	// Token: 0x02000018 RID: 24
	public class CMac : IMac
	{
		// Token: 0x060000A1 RID: 161 RVA: 0x00005A82 File Offset: 0x00004A82
		public CMac(IBlockCipher cipher) : this(cipher, cipher.GetBlockSize() * 8)
		{
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00005A94 File Offset: 0x00004A94
		public CMac(IBlockCipher cipher, int macSizeInBits)
		{
			if (macSizeInBits % 8 != 0)
			{
				throw new ArgumentException("MAC size must be multiple of 8");
			}
			if (macSizeInBits > cipher.GetBlockSize() * 8)
			{
				throw new ArgumentException("MAC size must be less or equal to " + cipher.GetBlockSize() * 8);
			}
			if (cipher.GetBlockSize() != 8 && cipher.GetBlockSize() != 16)
			{
				throw new ArgumentException("Block size must be either 64 or 128 bits");
			}
			this.cipher = new CbcBlockCipher(cipher);
			this.macSize = macSizeInBits / 8;
			this.mac = new byte[cipher.GetBlockSize()];
			this.buf = new byte[cipher.GetBlockSize()];
			this.ZEROES = new byte[cipher.GetBlockSize()];
			this.bufOff = 0;
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00005B4C File Offset: 0x00004B4C
		public string AlgorithmName
		{
			get
			{
				return this.cipher.AlgorithmName;
			}
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00005B5C File Offset: 0x00004B5C
		private byte[] doubleLu(byte[] inBytes)
		{
			int num = (inBytes[0] & byte.MaxValue) >> 7;
			byte[] array = new byte[inBytes.Length];
			for (int i = 0; i < inBytes.Length - 1; i++)
			{
				array[i] = (byte)(((int)inBytes[i] << 1) + ((inBytes[i + 1] & byte.MaxValue) >> 7));
			}
			array[inBytes.Length - 1] = (byte)(inBytes[inBytes.Length - 1] << 1);
			if (num == 1)
			{
				byte[] array2 = array;
				int num2 = inBytes.Length - 1;
				array2[num2] ^= ((inBytes.Length == 16) ? 135 : 27);
			}
			return array;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00005BE4 File Offset: 0x00004BE4
		public void Init(ICipherParameters parameters)
		{
			this.Reset();
			this.cipher.Init(true, parameters);
			this.L = new byte[this.ZEROES.Length];
			this.cipher.ProcessBlock(this.ZEROES, 0, this.L, 0);
			this.Lu = this.doubleLu(this.L);
			this.Lu2 = this.doubleLu(this.Lu);
			this.cipher.Init(true, parameters);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00005C62 File Offset: 0x00004C62
		public int GetMacSize()
		{
			return this.macSize;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00005C6C File Offset: 0x00004C6C
		public void Update(byte input)
		{
			if (this.bufOff == this.buf.Length)
			{
				this.cipher.ProcessBlock(this.buf, 0, this.mac, 0);
				this.bufOff = 0;
			}
			this.buf[this.bufOff++] = input;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00005CC4 File Offset: 0x00004CC4
		public void BlockUpdate(byte[] inBytes, int inOff, int len)
		{
			if (len < 0)
			{
				throw new ArgumentException("Can't have a negative input length!");
			}
			int blockSize = this.cipher.GetBlockSize();
			int num = blockSize - this.bufOff;
			if (len > num)
			{
				Array.Copy(inBytes, inOff, this.buf, this.bufOff, num);
				this.cipher.ProcessBlock(this.buf, 0, this.mac, 0);
				this.bufOff = 0;
				len -= num;
				inOff += num;
				while (len > blockSize)
				{
					this.cipher.ProcessBlock(inBytes, inOff, this.mac, 0);
					len -= blockSize;
					inOff += blockSize;
				}
			}
			Array.Copy(inBytes, inOff, this.buf, this.bufOff, len);
			this.bufOff += len;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00005D80 File Offset: 0x00004D80
		public int DoFinal(byte[] outBytes, int outOff)
		{
			int blockSize = this.cipher.GetBlockSize();
			byte[] array;
			if (this.bufOff == blockSize)
			{
				array = this.Lu;
			}
			else
			{
				new ISO7816d4Padding().AddPadding(this.buf, this.bufOff);
				array = this.Lu2;
			}
			for (int i = 0; i < this.mac.Length; i++)
			{
				byte[] array2 = this.buf;
				int num = i;
				array2[num] ^= array[i];
			}
			this.cipher.ProcessBlock(this.buf, 0, this.mac, 0);
			Array.Copy(this.mac, 0, outBytes, outOff, this.macSize);
			this.Reset();
			return this.macSize;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00005E32 File Offset: 0x00004E32
		public void Reset()
		{
			Array.Clear(this.buf, 0, this.buf.Length);
			this.bufOff = 0;
			this.cipher.Reset();
		}

		// Token: 0x0400004D RID: 77
		private const byte CONSTANT_128 = 135;

		// Token: 0x0400004E RID: 78
		private const byte CONSTANT_64 = 27;

		// Token: 0x0400004F RID: 79
		private byte[] ZEROES;

		// Token: 0x04000050 RID: 80
		private byte[] mac;

		// Token: 0x04000051 RID: 81
		private byte[] buf;

		// Token: 0x04000052 RID: 82
		private int bufOff;

		// Token: 0x04000053 RID: 83
		private IBlockCipher cipher;

		// Token: 0x04000054 RID: 84
		private int macSize;

		// Token: 0x04000055 RID: 85
		private byte[] L;

		// Token: 0x04000056 RID: 86
		private byte[] Lu;

		// Token: 0x04000057 RID: 87
		private byte[] Lu2;
	}
}
