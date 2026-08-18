using System;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Paddings
{
	// Token: 0x02000087 RID: 135
	public class Pkcs7Padding : IBlockCipherPadding
	{
		// Token: 0x0600042E RID: 1070 RVA: 0x00016447 File Offset: 0x00015447
		public void Init(SecureRandom random)
		{
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600042F RID: 1071 RVA: 0x00016449 File Offset: 0x00015449
		public string PaddingName
		{
			get
			{
				return "PKCS7";
			}
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x00016450 File Offset: 0x00015450
		public int AddPadding(byte[] input, int inOff)
		{
			byte b = (byte)(input.Length - inOff);
			while (inOff < input.Length)
			{
				input[inOff] = b;
				inOff++;
			}
			return (int)b;
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x00016478 File Offset: 0x00015478
		public int PadCount(byte[] input)
		{
			int num = (int)input[input.Length - 1];
			if (num < 1 || num > input.Length)
			{
				throw new InvalidCipherTextException("pad block corrupted");
			}
			for (int i = 1; i <= num; i++)
			{
				if ((int)input[input.Length - i] != num)
				{
					throw new InvalidCipherTextException("pad block corrupted");
				}
			}
			return num;
		}
	}
}
