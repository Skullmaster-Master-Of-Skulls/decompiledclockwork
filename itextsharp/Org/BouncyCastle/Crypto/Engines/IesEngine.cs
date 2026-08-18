using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x02000433 RID: 1075
	public class IesEngine
	{
		// Token: 0x06002493 RID: 9363 RVA: 0x000DE9D1 File Offset: 0x000DD9D1
		public IesEngine(IBasicAgreement agree, IDerivationFunction kdf, IMac mac)
		{
			this.agree = agree;
			this.kdf = kdf;
			this.mac = mac;
			this.macBuf = new byte[mac.GetMacSize()];
		}

		// Token: 0x06002494 RID: 9364 RVA: 0x000DE9FF File Offset: 0x000DD9FF
		public IesEngine(IBasicAgreement agree, IDerivationFunction kdf, IMac mac, BufferedBlockCipher cipher)
		{
			this.agree = agree;
			this.kdf = kdf;
			this.mac = mac;
			this.macBuf = new byte[mac.GetMacSize()];
			this.cipher = cipher;
		}

		// Token: 0x06002495 RID: 9365 RVA: 0x000DEA35 File Offset: 0x000DDA35
		public void Init(bool forEncryption, ICipherParameters privParameters, ICipherParameters pubParameters, ICipherParameters iesParameters)
		{
			this.forEncryption = forEncryption;
			this.privParam = privParameters;
			this.pubParam = pubParameters;
			this.param = (IesParameters)iesParameters;
		}

		// Token: 0x06002496 RID: 9366 RVA: 0x000DEA5C File Offset: 0x000DDA5C
		private byte[] DecryptBlock(byte[] in_enc, int inOff, int inLen, byte[] z)
		{
			KdfParameters kdfParameters = new KdfParameters(z, this.param.GetDerivationV());
			int macKeySize = this.param.MacKeySize;
			this.kdf.Init(kdfParameters);
			inLen -= this.mac.GetMacSize();
			byte[] array2;
			KeyParameter parameters;
			if (this.cipher == null)
			{
				byte[] array = this.GenerateKdfBytes(kdfParameters, inLen + macKeySize / 8);
				array2 = new byte[inLen];
				for (int num = 0; num != inLen; num++)
				{
					array2[num] = (in_enc[inOff + num] ^ array[num]);
				}
				parameters = new KeyParameter(array, inLen, macKeySize / 8);
			}
			else
			{
				int cipherKeySize = ((IesWithCipherParameters)this.param).CipherKeySize;
				byte[] key = this.GenerateKdfBytes(kdfParameters, cipherKeySize / 8 + macKeySize / 8);
				this.cipher.Init(false, new KeyParameter(key, 0, cipherKeySize / 8));
				array2 = this.cipher.DoFinal(in_enc, inOff, inLen);
				parameters = new KeyParameter(key, cipherKeySize / 8, macKeySize / 8);
			}
			byte[] encodingV = this.param.GetEncodingV();
			this.mac.Init(parameters);
			this.mac.BlockUpdate(in_enc, inOff, inLen);
			this.mac.BlockUpdate(encodingV, 0, encodingV.Length);
			this.mac.DoFinal(this.macBuf, 0);
			inOff += inLen;
			for (int i = 0; i < this.macBuf.Length; i++)
			{
				if (this.macBuf[i] != in_enc[inOff + i])
				{
					throw new InvalidCipherTextException("IMac codes failed to equal.");
				}
			}
			return array2;
		}

		// Token: 0x06002497 RID: 9367 RVA: 0x000DEBD4 File Offset: 0x000DDBD4
		private byte[] EncryptBlock(byte[] input, int inOff, int inLen, byte[] z)
		{
			KdfParameters kParam = new KdfParameters(z, this.param.GetDerivationV());
			int macKeySize = this.param.MacKeySize;
			byte[] array2;
			int num;
			KeyParameter parameters;
			if (this.cipher == null)
			{
				byte[] array = this.GenerateKdfBytes(kParam, inLen + macKeySize / 8);
				array2 = new byte[inLen + this.mac.GetMacSize()];
				num = inLen;
				for (int num2 = 0; num2 != inLen; num2++)
				{
					array2[num2] = (input[inOff + num2] ^ array[num2]);
				}
				parameters = new KeyParameter(array, inLen, macKeySize / 8);
			}
			else
			{
				int cipherKeySize = ((IesWithCipherParameters)this.param).CipherKeySize;
				byte[] key = this.GenerateKdfBytes(kParam, cipherKeySize / 8 + macKeySize / 8);
				this.cipher.Init(true, new KeyParameter(key, 0, cipherKeySize / 8));
				num = this.cipher.GetOutputSize(inLen);
				byte[] array3 = new byte[num];
				int num3 = this.cipher.ProcessBytes(input, inOff, inLen, array3, 0);
				num3 += this.cipher.DoFinal(array3, num3);
				array2 = new byte[num3 + this.mac.GetMacSize()];
				num = num3;
				Array.Copy(array3, 0, array2, 0, num3);
				parameters = new KeyParameter(key, cipherKeySize / 8, macKeySize / 8);
			}
			byte[] encodingV = this.param.GetEncodingV();
			this.mac.Init(parameters);
			this.mac.BlockUpdate(array2, 0, num);
			this.mac.BlockUpdate(encodingV, 0, encodingV.Length);
			this.mac.DoFinal(array2, num);
			return array2;
		}

		// Token: 0x06002498 RID: 9368 RVA: 0x000DED5C File Offset: 0x000DDD5C
		private byte[] GenerateKdfBytes(KdfParameters kParam, int length)
		{
			byte[] array = new byte[length];
			this.kdf.Init(kParam);
			this.kdf.GenerateBytes(array, 0, array.Length);
			return array;
		}

		// Token: 0x06002499 RID: 9369 RVA: 0x000DED90 File Offset: 0x000DDD90
		public byte[] ProcessBlock(byte[] input, int inOff, int inLen)
		{
			this.agree.Init(this.privParam);
			BigInteger bigInteger = this.agree.CalculateAgreement(this.pubParam);
			byte[] z = bigInteger.ToByteArray();
			if (!this.forEncryption)
			{
				return this.DecryptBlock(input, inOff, inLen, z);
			}
			return this.EncryptBlock(input, inOff, inLen, z);
		}

		// Token: 0x0400198B RID: 6539
		private readonly IBasicAgreement agree;

		// Token: 0x0400198C RID: 6540
		private readonly IDerivationFunction kdf;

		// Token: 0x0400198D RID: 6541
		private readonly IMac mac;

		// Token: 0x0400198E RID: 6542
		private readonly BufferedBlockCipher cipher;

		// Token: 0x0400198F RID: 6543
		private readonly byte[] macBuf;

		// Token: 0x04001990 RID: 6544
		private bool forEncryption;

		// Token: 0x04001991 RID: 6545
		private ICipherParameters privParam;

		// Token: 0x04001992 RID: 6546
		private ICipherParameters pubParam;

		// Token: 0x04001993 RID: 6547
		private IesParameters param;
	}
}
