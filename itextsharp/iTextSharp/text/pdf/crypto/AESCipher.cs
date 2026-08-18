using System;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Paddings;
using Org.BouncyCastle.Crypto.Parameters;

namespace iTextSharp.text.pdf.crypto
{
	// Token: 0x020001CF RID: 463
	public class AESCipher
	{
		// Token: 0x06001212 RID: 4626 RVA: 0x00067CC8 File Offset: 0x00066CC8
		public AESCipher(bool forEncryption, byte[] key, byte[] iv)
		{
			IBlockCipher cipher = new AesFastEngine();
			IBlockCipher cipher2 = new CbcBlockCipher(cipher);
			this.bp = new PaddedBufferedBlockCipher(cipher2);
			KeyParameter parameters = new KeyParameter(key);
			ParametersWithIV parameters2 = new ParametersWithIV(parameters, iv);
			this.bp.Init(forEncryption, parameters2);
		}

		// Token: 0x06001213 RID: 4627 RVA: 0x00067D10 File Offset: 0x00066D10
		public byte[] Update(byte[] inp, int inpOff, int inpLen)
		{
			int updateOutputSize = this.bp.GetUpdateOutputSize(inpLen);
			byte[] array = null;
			if (updateOutputSize > 0)
			{
				array = new byte[updateOutputSize];
			}
			this.bp.ProcessBytes(inp, inpOff, inpLen, array, 0);
			return array;
		}

		// Token: 0x06001214 RID: 4628 RVA: 0x00067D50 File Offset: 0x00066D50
		public byte[] DoFinal()
		{
			int outputSize = this.bp.GetOutputSize(0);
			byte[] array = new byte[outputSize];
			int num = 0;
			try
			{
				num = this.bp.DoFinal(array, 0);
			}
			catch
			{
				return array;
			}
			if (num != array.Length)
			{
				byte[] array2 = new byte[num];
				Array.Copy(array, 0, array2, 0, num);
				return array2;
			}
			return array;
		}

		// Token: 0x04000CB4 RID: 3252
		private PaddedBufferedBlockCipher bp;
	}
}
