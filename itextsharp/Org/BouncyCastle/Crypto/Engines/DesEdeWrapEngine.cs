using System;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x020002FB RID: 763
	public class DesEdeWrapEngine : IWrapper
	{
		// Token: 0x06001C0A RID: 7178 RVA: 0x000A8198 File Offset: 0x000A7198
		public void Init(bool forWrapping, ICipherParameters parameters)
		{
			this.forWrapping = forWrapping;
			this.engine = new CbcBlockCipher(new DesEdeEngine());
			SecureRandom secureRandom;
			if (parameters is ParametersWithRandom)
			{
				ParametersWithRandom parametersWithRandom = (ParametersWithRandom)parameters;
				parameters = parametersWithRandom.Parameters;
				secureRandom = parametersWithRandom.Random;
			}
			else
			{
				secureRandom = new SecureRandom();
			}
			if (parameters is KeyParameter)
			{
				this.param = (KeyParameter)parameters;
				if (this.forWrapping)
				{
					this.iv = new byte[8];
					secureRandom.NextBytes(this.iv);
					this.paramPlusIV = new ParametersWithIV(this.param, this.iv);
					return;
				}
			}
			else if (parameters is ParametersWithIV)
			{
				if (!forWrapping)
				{
					throw new ArgumentException("You should not supply an IV for unwrapping");
				}
				this.paramPlusIV = (ParametersWithIV)parameters;
				this.iv = this.paramPlusIV.GetIV();
				this.param = (KeyParameter)this.paramPlusIV.Parameters;
				if (this.iv.Length != 8)
				{
					throw new ArgumentException("IV is not 8 octets", "parameters");
				}
			}
		}

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x06001C0B RID: 7179 RVA: 0x000A8295 File Offset: 0x000A7295
		public string AlgorithmName
		{
			get
			{
				return "DESede";
			}
		}

		// Token: 0x06001C0C RID: 7180 RVA: 0x000A829C File Offset: 0x000A729C
		public byte[] Wrap(byte[] input, int inOff, int length)
		{
			if (!this.forWrapping)
			{
				throw new InvalidOperationException("Not initialized for wrapping");
			}
			byte[] array = new byte[length];
			Array.Copy(input, inOff, array, 0, length);
			byte[] array2 = this.CalculateCmsKeyChecksum(array);
			byte[] array3 = new byte[array.Length + array2.Length];
			Array.Copy(array, 0, array3, 0, array.Length);
			Array.Copy(array2, 0, array3, array.Length, array2.Length);
			int blockSize = this.engine.GetBlockSize();
			if (array3.Length % blockSize != 0)
			{
				throw new InvalidOperationException("Not multiple of block length");
			}
			this.engine.Init(true, this.paramPlusIV);
			byte[] array4 = new byte[array3.Length];
			for (int num = 0; num != array3.Length; num += blockSize)
			{
				this.engine.ProcessBlock(array3, num, array4, num);
			}
			byte[] array5 = new byte[this.iv.Length + array4.Length];
			Array.Copy(this.iv, 0, array5, 0, this.iv.Length);
			Array.Copy(array4, 0, array5, this.iv.Length, array4.Length);
			byte[] array6 = DesEdeWrapEngine.reverse(array5);
			ParametersWithIV parameters = new ParametersWithIV(this.param, DesEdeWrapEngine.IV2);
			this.engine.Init(true, parameters);
			for (int num2 = 0; num2 != array6.Length; num2 += blockSize)
			{
				this.engine.ProcessBlock(array6, num2, array6, num2);
			}
			return array6;
		}

		// Token: 0x06001C0D RID: 7181 RVA: 0x000A83F0 File Offset: 0x000A73F0
		public byte[] Unwrap(byte[] input, int inOff, int length)
		{
			if (this.forWrapping)
			{
				throw new InvalidOperationException("Not set for unwrapping");
			}
			if (input == null)
			{
				throw new InvalidCipherTextException("Null pointer as ciphertext");
			}
			int blockSize = this.engine.GetBlockSize();
			if (length % blockSize != 0)
			{
				throw new InvalidCipherTextException("Ciphertext not multiple of " + blockSize);
			}
			ParametersWithIV parameters = new ParametersWithIV(this.param, DesEdeWrapEngine.IV2);
			this.engine.Init(false, parameters);
			byte[] array = new byte[length];
			for (int num = 0; num != array.Length; num += blockSize)
			{
				this.engine.ProcessBlock(input, inOff + num, array, num);
			}
			byte[] array2 = DesEdeWrapEngine.reverse(array);
			this.iv = new byte[8];
			byte[] array3 = new byte[array2.Length - 8];
			Array.Copy(array2, 0, this.iv, 0, 8);
			Array.Copy(array2, 8, array3, 0, array2.Length - 8);
			this.paramPlusIV = new ParametersWithIV(this.param, this.iv);
			this.engine.Init(false, this.paramPlusIV);
			byte[] array4 = new byte[array3.Length];
			for (int num2 = 0; num2 != array4.Length; num2 += blockSize)
			{
				this.engine.ProcessBlock(array3, num2, array4, num2);
			}
			byte[] array5 = new byte[array4.Length - 8];
			byte[] array6 = new byte[8];
			Array.Copy(array4, 0, array5, 0, array4.Length - 8);
			Array.Copy(array4, array4.Length - 8, array6, 0, 8);
			if (!this.CheckCmsKeyChecksum(array5, array6))
			{
				throw new InvalidCipherTextException("Checksum inside ciphertext is corrupted");
			}
			return array5;
		}

		// Token: 0x06001C0E RID: 7182 RVA: 0x000A8578 File Offset: 0x000A7578
		private byte[] CalculateCmsKeyChecksum(byte[] key)
		{
			this.sha1.BlockUpdate(key, 0, key.Length);
			this.sha1.DoFinal(this.digest, 0);
			byte[] array = new byte[8];
			Array.Copy(this.digest, 0, array, 0, 8);
			return array;
		}

		// Token: 0x06001C0F RID: 7183 RVA: 0x000A85BF File Offset: 0x000A75BF
		private bool CheckCmsKeyChecksum(byte[] key, byte[] checksum)
		{
			return Arrays.ConstantTimeAreEqual(this.CalculateCmsKeyChecksum(key), checksum);
		}

		// Token: 0x06001C10 RID: 7184 RVA: 0x000A85D0 File Offset: 0x000A75D0
		private static byte[] reverse(byte[] bs)
		{
			byte[] array = new byte[bs.Length];
			for (int i = 0; i < bs.Length; i++)
			{
				array[i] = bs[bs.Length - (i + 1)];
			}
			return array;
		}

		// Token: 0x04001348 RID: 4936
		private CbcBlockCipher engine;

		// Token: 0x04001349 RID: 4937
		private KeyParameter param;

		// Token: 0x0400134A RID: 4938
		private ParametersWithIV paramPlusIV;

		// Token: 0x0400134B RID: 4939
		private byte[] iv;

		// Token: 0x0400134C RID: 4940
		private bool forWrapping;

		// Token: 0x0400134D RID: 4941
		private static readonly byte[] IV2 = new byte[]
		{
			74,
			221,
			162,
			44,
			121,
			232,
			33,
			5
		};

		// Token: 0x0400134E RID: 4942
		private readonly IDigest sha1 = new Sha1Digest();

		// Token: 0x0400134F RID: 4943
		private readonly byte[] digest = new byte[20];
	}
}
