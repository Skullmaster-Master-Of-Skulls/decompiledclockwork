using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000893 RID: 2195
	[ComVisible(true)]
	public class PKCS1MaskGenerationMethod : MaskGenerationMethod
	{
		// Token: 0x06004FE3 RID: 20451 RVA: 0x00115EE7 File Offset: 0x00114EE7
		public PKCS1MaskGenerationMethod()
		{
			this.HashNameValue = "SHA1";
		}

		// Token: 0x17000DF5 RID: 3573
		// (get) Token: 0x06004FE4 RID: 20452 RVA: 0x00115EFA File Offset: 0x00114EFA
		// (set) Token: 0x06004FE5 RID: 20453 RVA: 0x00115F02 File Offset: 0x00114F02
		public string HashName
		{
			get
			{
				return this.HashNameValue;
			}
			set
			{
				this.HashNameValue = value;
				if (this.HashNameValue == null)
				{
					this.HashNameValue = "SHA1";
				}
			}
		}

		// Token: 0x06004FE6 RID: 20454 RVA: 0x00115F20 File Offset: 0x00114F20
		public override byte[] GenerateMask(byte[] rgbSeed, int cbReturn)
		{
			HashAlgorithm hashAlgorithm = (HashAlgorithm)CryptoConfig.CreateFromName(this.HashNameValue);
			byte[] inputBuffer = new byte[4];
			byte[] array = new byte[cbReturn];
			uint num = 0U;
			for (int i = 0; i < array.Length; i += hashAlgorithm.Hash.Length)
			{
				Utils.ConvertIntToByteArray(num++, ref inputBuffer);
				hashAlgorithm.TransformBlock(rgbSeed, 0, rgbSeed.Length, rgbSeed, 0);
				hashAlgorithm.TransformFinalBlock(inputBuffer, 0, 4);
				byte[] hash = hashAlgorithm.Hash;
				hashAlgorithm.Initialize();
				if (array.Length - i > hash.Length)
				{
					Buffer.BlockCopy(hash, 0, array, i, hash.Length);
				}
				else
				{
					Buffer.BlockCopy(hash, 0, array, i, array.Length - i);
				}
			}
			return array;
		}

		// Token: 0x04002924 RID: 10532
		private string HashNameValue;
	}
}
