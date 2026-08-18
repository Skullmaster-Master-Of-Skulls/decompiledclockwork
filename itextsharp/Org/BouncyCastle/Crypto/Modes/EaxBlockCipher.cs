using System;
using Org.BouncyCastle.Crypto.Macs;
using Org.BouncyCastle.Crypto.Parameters;

namespace Org.BouncyCastle.Crypto.Modes
{
	// Token: 0x02000506 RID: 1286
	public class EaxBlockCipher : IAeadBlockCipher
	{
		// Token: 0x06002BDC RID: 11228 RVA: 0x0010921C File Offset: 0x0010821C
		public EaxBlockCipher(IBlockCipher cipher)
		{
			this.blockSize = cipher.GetBlockSize();
			this.mac = new CMac(cipher);
			this.macBlock = new byte[this.blockSize];
			this.bufBlock = new byte[this.blockSize * 2];
			this.associatedTextMac = new byte[this.mac.GetMacSize()];
			this.nonceMac = new byte[this.mac.GetMacSize()];
			this.cipher = new SicBlockCipher(cipher);
		}

		// Token: 0x1700078E RID: 1934
		// (get) Token: 0x06002BDD RID: 11229 RVA: 0x001092A3 File Offset: 0x001082A3
		public virtual string AlgorithmName
		{
			get
			{
				return this.cipher.GetUnderlyingCipher().AlgorithmName + "/EAX";
			}
		}

		// Token: 0x06002BDE RID: 11230 RVA: 0x001092BF File Offset: 0x001082BF
		public virtual int GetBlockSize()
		{
			return this.cipher.GetBlockSize();
		}

		// Token: 0x06002BDF RID: 11231 RVA: 0x001092CC File Offset: 0x001082CC
		public virtual void Init(bool forEncryption, ICipherParameters parameters)
		{
			this.forEncryption = forEncryption;
			byte[] array;
			byte[] array2;
			ICipherParameters parameters2;
			if (parameters is AeadParameters)
			{
				AeadParameters aeadParameters = (AeadParameters)parameters;
				array = aeadParameters.GetNonce();
				array2 = aeadParameters.GetAssociatedText();
				this.macSize = aeadParameters.MacSize / 8;
				parameters2 = aeadParameters.Key;
			}
			else
			{
				if (!(parameters is ParametersWithIV))
				{
					throw new ArgumentException("invalid parameters passed to EAX");
				}
				ParametersWithIV parametersWithIV = (ParametersWithIV)parameters;
				array = parametersWithIV.GetIV();
				array2 = new byte[0];
				this.macSize = this.mac.GetMacSize() / 2;
				parameters2 = parametersWithIV.Parameters;
			}
			byte[] array3 = new byte[this.blockSize];
			this.mac.Init(parameters2);
			array3[this.blockSize - 1] = 1;
			this.mac.BlockUpdate(array3, 0, this.blockSize);
			this.mac.BlockUpdate(array2, 0, array2.Length);
			this.mac.DoFinal(this.associatedTextMac, 0);
			array3[this.blockSize - 1] = 0;
			this.mac.BlockUpdate(array3, 0, this.blockSize);
			this.mac.BlockUpdate(array, 0, array.Length);
			this.mac.DoFinal(this.nonceMac, 0);
			array3[this.blockSize - 1] = 2;
			this.mac.BlockUpdate(array3, 0, this.blockSize);
			this.cipher.Init(true, new ParametersWithIV(parameters2, this.nonceMac));
		}

		// Token: 0x06002BE0 RID: 11232 RVA: 0x00109434 File Offset: 0x00108434
		private void calculateMac()
		{
			byte[] array = new byte[this.blockSize];
			this.mac.DoFinal(array, 0);
			for (int i = 0; i < this.macBlock.Length; i++)
			{
				this.macBlock[i] = (this.nonceMac[i] ^ this.associatedTextMac[i] ^ array[i]);
			}
		}

		// Token: 0x06002BE1 RID: 11233 RVA: 0x0010948C File Offset: 0x0010848C
		public virtual void Reset()
		{
			this.Reset(true);
		}

		// Token: 0x06002BE2 RID: 11234 RVA: 0x00109498 File Offset: 0x00108498
		private void Reset(bool clearMac)
		{
			this.cipher.Reset();
			this.mac.Reset();
			this.bufOff = 0;
			Array.Clear(this.bufBlock, 0, this.bufBlock.Length);
			if (clearMac)
			{
				Array.Clear(this.macBlock, 0, this.macBlock.Length);
			}
			byte[] array = new byte[this.blockSize];
			array[this.blockSize - 1] = 2;
			this.mac.BlockUpdate(array, 0, this.blockSize);
		}

		// Token: 0x06002BE3 RID: 11235 RVA: 0x00109517 File Offset: 0x00108517
		public virtual int ProcessByte(byte input, byte[] outBytes, int outOff)
		{
			return this.process(input, outBytes, outOff);
		}

		// Token: 0x06002BE4 RID: 11236 RVA: 0x00109524 File Offset: 0x00108524
		public virtual int ProcessBytes(byte[] inBytes, int inOff, int len, byte[] outBytes, int outOff)
		{
			int num = 0;
			for (int num2 = 0; num2 != len; num2++)
			{
				num += this.process(inBytes[inOff + num2], outBytes, outOff + num);
			}
			return num;
		}

		// Token: 0x06002BE5 RID: 11237 RVA: 0x00109554 File Offset: 0x00108554
		public virtual int DoFinal(byte[] outBytes, int outOff)
		{
			int num = this.bufOff;
			byte[] array = new byte[this.bufBlock.Length];
			this.bufOff = 0;
			if (this.forEncryption)
			{
				this.cipher.ProcessBlock(this.bufBlock, 0, array, 0);
				this.cipher.ProcessBlock(this.bufBlock, this.blockSize, array, this.blockSize);
				Array.Copy(array, 0, outBytes, outOff, num);
				this.mac.BlockUpdate(array, 0, num);
				this.calculateMac();
				Array.Copy(this.macBlock, 0, outBytes, outOff + num, this.macSize);
				this.Reset(false);
				return num + this.macSize;
			}
			if (num > this.macSize)
			{
				this.mac.BlockUpdate(this.bufBlock, 0, num - this.macSize);
				this.cipher.ProcessBlock(this.bufBlock, 0, array, 0);
				this.cipher.ProcessBlock(this.bufBlock, this.blockSize, array, this.blockSize);
				Array.Copy(array, 0, outBytes, outOff, num - this.macSize);
			}
			this.calculateMac();
			if (!this.verifyMac(this.bufBlock, num - this.macSize))
			{
				throw new InvalidCipherTextException("mac check in EAX failed");
			}
			this.Reset(false);
			return num - this.macSize;
		}

		// Token: 0x06002BE6 RID: 11238 RVA: 0x0010969C File Offset: 0x0010869C
		public virtual byte[] GetMac()
		{
			byte[] array = new byte[this.macSize];
			Array.Copy(this.macBlock, 0, array, 0, this.macSize);
			return array;
		}

		// Token: 0x06002BE7 RID: 11239 RVA: 0x001096CA File Offset: 0x001086CA
		public virtual int GetUpdateOutputSize(int len)
		{
			return (len + this.bufOff) / this.blockSize * this.blockSize;
		}

		// Token: 0x06002BE8 RID: 11240 RVA: 0x001096E2 File Offset: 0x001086E2
		public virtual int GetOutputSize(int len)
		{
			if (this.forEncryption)
			{
				return len + this.bufOff + this.macSize;
			}
			return len + this.bufOff - this.macSize;
		}

		// Token: 0x06002BE9 RID: 11241 RVA: 0x0010970C File Offset: 0x0010870C
		private int process(byte b, byte[] outBytes, int outOff)
		{
			this.bufBlock[this.bufOff++] = b;
			if (this.bufOff == this.bufBlock.Length)
			{
				int result;
				if (this.forEncryption)
				{
					result = this.cipher.ProcessBlock(this.bufBlock, 0, outBytes, outOff);
					this.mac.BlockUpdate(outBytes, outOff, this.blockSize);
				}
				else
				{
					this.mac.BlockUpdate(this.bufBlock, 0, this.blockSize);
					result = this.cipher.ProcessBlock(this.bufBlock, 0, outBytes, outOff);
				}
				this.bufOff = this.blockSize;
				Array.Copy(this.bufBlock, this.blockSize, this.bufBlock, 0, this.blockSize);
				return result;
			}
			return 0;
		}

		// Token: 0x06002BEA RID: 11242 RVA: 0x001097D4 File Offset: 0x001087D4
		private bool verifyMac(byte[] mac, int off)
		{
			for (int i = 0; i < this.macSize; i++)
			{
				if (this.macBlock[i] != mac[off + i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04001E45 RID: 7749
		private SicBlockCipher cipher;

		// Token: 0x04001E46 RID: 7750
		private bool forEncryption;

		// Token: 0x04001E47 RID: 7751
		private int blockSize;

		// Token: 0x04001E48 RID: 7752
		private IMac mac;

		// Token: 0x04001E49 RID: 7753
		private byte[] nonceMac;

		// Token: 0x04001E4A RID: 7754
		private byte[] associatedTextMac;

		// Token: 0x04001E4B RID: 7755
		private byte[] macBlock;

		// Token: 0x04001E4C RID: 7756
		private int macSize;

		// Token: 0x04001E4D RID: 7757
		private byte[] bufBlock;

		// Token: 0x04001E4E RID: 7758
		private int bufOff;

		// Token: 0x02000507 RID: 1287
		private enum Tag : byte
		{
			// Token: 0x04001E50 RID: 7760
			N,
			// Token: 0x04001E51 RID: 7761
			H,
			// Token: 0x04001E52 RID: 7762
			C
		}
	}
}
