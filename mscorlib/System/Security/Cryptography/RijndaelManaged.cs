using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x020008A4 RID: 2212
	[ComVisible(true)]
	public sealed class RijndaelManaged : Rijndael
	{
		// Token: 0x06005072 RID: 20594 RVA: 0x00119E87 File Offset: 0x00118E87
		public RijndaelManaged()
		{
			if (Utils.FipsAlgorithmPolicy == 1)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Cryptography_NonCompliantFIPSAlgorithm"));
			}
		}

		// Token: 0x06005073 RID: 20595 RVA: 0x00119EA7 File Offset: 0x00118EA7
		public override ICryptoTransform CreateEncryptor(byte[] rgbKey, byte[] rgbIV)
		{
			return this.NewEncryptor(rgbKey, this.ModeValue, rgbIV, this.FeedbackSizeValue, RijndaelManagedTransformMode.Encrypt);
		}

		// Token: 0x06005074 RID: 20596 RVA: 0x00119EBE File Offset: 0x00118EBE
		public override ICryptoTransform CreateDecryptor(byte[] rgbKey, byte[] rgbIV)
		{
			return this.NewEncryptor(rgbKey, this.ModeValue, rgbIV, this.FeedbackSizeValue, RijndaelManagedTransformMode.Decrypt);
		}

		// Token: 0x06005075 RID: 20597 RVA: 0x00119ED5 File Offset: 0x00118ED5
		public override void GenerateKey()
		{
			this.KeyValue = new byte[this.KeySizeValue / 8];
			Utils.StaticRandomNumberGenerator.GetBytes(this.KeyValue);
		}

		// Token: 0x06005076 RID: 20598 RVA: 0x00119EFA File Offset: 0x00118EFA
		public override void GenerateIV()
		{
			this.IVValue = new byte[this.BlockSizeValue / 8];
			Utils.StaticRandomNumberGenerator.GetBytes(this.IVValue);
		}

		// Token: 0x06005077 RID: 20599 RVA: 0x00119F20 File Offset: 0x00118F20
		private ICryptoTransform NewEncryptor(byte[] rgbKey, CipherMode mode, byte[] rgbIV, int feedbackSize, RijndaelManagedTransformMode encryptMode)
		{
			if (rgbKey == null)
			{
				rgbKey = new byte[this.KeySizeValue / 8];
				Utils.StaticRandomNumberGenerator.GetBytes(rgbKey);
			}
			if (mode != CipherMode.ECB && rgbIV == null)
			{
				rgbIV = new byte[this.BlockSizeValue / 8];
				Utils.StaticRandomNumberGenerator.GetBytes(rgbIV);
			}
			return new RijndaelManagedTransform(rgbKey, mode, rgbIV, this.BlockSizeValue, feedbackSize, this.PaddingValue, encryptMode);
		}
	}
}
