using System;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Paddings;
using Org.BouncyCastle.Crypto.Parameters;

namespace Org.BouncyCastle.Crypto.Macs
{
	// Token: 0x020001F0 RID: 496
	public class ISO9797Alg3Mac : IMac
	{
		// Token: 0x0600134E RID: 4942 RVA: 0x0006E9EB File Offset: 0x0006D9EB
		public ISO9797Alg3Mac(IBlockCipher cipher) : this(cipher, cipher.GetBlockSize() * 8, null)
		{
		}

		// Token: 0x0600134F RID: 4943 RVA: 0x0006E9FD File Offset: 0x0006D9FD
		public ISO9797Alg3Mac(IBlockCipher cipher, IBlockCipherPadding padding) : this(cipher, cipher.GetBlockSize() * 8, padding)
		{
		}

		// Token: 0x06001350 RID: 4944 RVA: 0x0006EA0F File Offset: 0x0006DA0F
		public ISO9797Alg3Mac(IBlockCipher cipher, int macSizeInBits) : this(cipher, macSizeInBits, null)
		{
		}

		// Token: 0x06001351 RID: 4945 RVA: 0x0006EA1C File Offset: 0x0006DA1C
		public ISO9797Alg3Mac(IBlockCipher cipher, int macSizeInBits, IBlockCipherPadding padding)
		{
			if (macSizeInBits % 8 != 0)
			{
				throw new ArgumentException("MAC size must be multiple of 8");
			}
			if (!(cipher is DesEngine))
			{
				throw new ArgumentException("cipher must be instance of DesEngine");
			}
			this.cipher = new CbcBlockCipher(cipher);
			this.padding = padding;
			this.macSize = macSizeInBits / 8;
			this.mac = new byte[cipher.GetBlockSize()];
			this.buf = new byte[cipher.GetBlockSize()];
			this.bufOff = 0;
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06001352 RID: 4946 RVA: 0x0006EA97 File Offset: 0x0006DA97
		public string AlgorithmName
		{
			get
			{
				return "ISO9797Alg3";
			}
		}

		// Token: 0x06001353 RID: 4947 RVA: 0x0006EAA0 File Offset: 0x0006DAA0
		public void Init(ICipherParameters parameters)
		{
			this.Reset();
			if (!(parameters is KeyParameter))
			{
				throw new ArgumentException("parameters must be an instance of KeyParameter");
			}
			KeyParameter keyParameter = (KeyParameter)parameters;
			byte[] key = keyParameter.GetKey();
			KeyParameter parameters2;
			if (key.Length == 16)
			{
				parameters2 = new KeyParameter(key, 0, 8);
				this.lastKey2 = new KeyParameter(key, 8, 8);
				this.lastKey3 = parameters2;
			}
			else
			{
				if (key.Length != 24)
				{
					throw new ArgumentException("Key must be either 112 or 168 bit long");
				}
				parameters2 = new KeyParameter(key, 0, 8);
				this.lastKey2 = new KeyParameter(key, 8, 8);
				this.lastKey3 = new KeyParameter(key, 16, 8);
			}
			this.cipher.Init(true, parameters2);
		}

		// Token: 0x06001354 RID: 4948 RVA: 0x0006EB42 File Offset: 0x0006DB42
		public int GetMacSize()
		{
			return this.macSize;
		}

		// Token: 0x06001355 RID: 4949 RVA: 0x0006EB4C File Offset: 0x0006DB4C
		public void Update(byte input)
		{
			if (this.bufOff == this.buf.Length)
			{
				this.cipher.ProcessBlock(this.buf, 0, this.mac, 0);
				this.bufOff = 0;
			}
			this.buf[this.bufOff++] = input;
		}

		// Token: 0x06001356 RID: 4950 RVA: 0x0006EBA4 File Offset: 0x0006DBA4
		public void BlockUpdate(byte[] input, int inOff, int len)
		{
			if (len < 0)
			{
				throw new ArgumentException("Can't have a negative input length!");
			}
			int blockSize = this.cipher.GetBlockSize();
			int num = 0;
			int num2 = blockSize - this.bufOff;
			if (len > num2)
			{
				Array.Copy(input, inOff, this.buf, this.bufOff, num2);
				num += this.cipher.ProcessBlock(this.buf, 0, this.mac, 0);
				this.bufOff = 0;
				len -= num2;
				inOff += num2;
				while (len > blockSize)
				{
					num += this.cipher.ProcessBlock(input, inOff, this.mac, 0);
					len -= blockSize;
					inOff += blockSize;
				}
			}
			Array.Copy(input, inOff, this.buf, this.bufOff, len);
			this.bufOff += len;
		}

		// Token: 0x06001357 RID: 4951 RVA: 0x0006EC68 File Offset: 0x0006DC68
		public int DoFinal(byte[] output, int outOff)
		{
			int blockSize = this.cipher.GetBlockSize();
			if (this.padding == null)
			{
				while (this.bufOff < blockSize)
				{
					this.buf[this.bufOff++] = 0;
				}
			}
			else
			{
				if (this.bufOff == blockSize)
				{
					this.cipher.ProcessBlock(this.buf, 0, this.mac, 0);
					this.bufOff = 0;
				}
				this.padding.AddPadding(this.buf, this.bufOff);
			}
			this.cipher.ProcessBlock(this.buf, 0, this.mac, 0);
			DesEngine desEngine = new DesEngine();
			desEngine.Init(false, this.lastKey2);
			desEngine.ProcessBlock(this.mac, 0, this.mac, 0);
			desEngine.Init(true, this.lastKey3);
			desEngine.ProcessBlock(this.mac, 0, this.mac, 0);
			Array.Copy(this.mac, 0, output, outOff, this.macSize);
			this.Reset();
			return this.macSize;
		}

		// Token: 0x06001358 RID: 4952 RVA: 0x0006ED75 File Offset: 0x0006DD75
		public void Reset()
		{
			Array.Clear(this.buf, 0, this.buf.Length);
			this.bufOff = 0;
			this.cipher.Reset();
		}

		// Token: 0x04000D81 RID: 3457
		private byte[] mac;

		// Token: 0x04000D82 RID: 3458
		private byte[] buf;

		// Token: 0x04000D83 RID: 3459
		private int bufOff;

		// Token: 0x04000D84 RID: 3460
		private IBlockCipher cipher;

		// Token: 0x04000D85 RID: 3461
		private IBlockCipherPadding padding;

		// Token: 0x04000D86 RID: 3462
		private int macSize;

		// Token: 0x04000D87 RID: 3463
		private KeyParameter lastKey2;

		// Token: 0x04000D88 RID: 3464
		private KeyParameter lastKey3;
	}
}
