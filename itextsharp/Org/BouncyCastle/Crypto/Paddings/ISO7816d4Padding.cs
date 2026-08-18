using System;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Paddings
{
	// Token: 0x02000124 RID: 292
	public class ISO7816d4Padding : IBlockCipherPadding
	{
		// Token: 0x06000ABE RID: 2750 RVA: 0x00038345 File Offset: 0x00037345
		public void Init(SecureRandom random)
		{
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000ABF RID: 2751 RVA: 0x00038347 File Offset: 0x00037347
		public string PaddingName
		{
			get
			{
				return "ISO7816-4";
			}
		}

		// Token: 0x06000AC0 RID: 2752 RVA: 0x00038350 File Offset: 0x00037350
		public int AddPadding(byte[] input, int inOff)
		{
			int result = input.Length - inOff;
			input[inOff] = 128;
			for (inOff++; inOff < input.Length; inOff++)
			{
				input[inOff] = 0;
			}
			return result;
		}

		// Token: 0x06000AC1 RID: 2753 RVA: 0x00038384 File Offset: 0x00037384
		public int PadCount(byte[] input)
		{
			int num = input.Length - 1;
			while (num > 0 && input[num] == 0)
			{
				num--;
			}
			if (input[num] != 128)
			{
				throw new InvalidCipherTextException("pad block corrupted");
			}
			return input.Length - num;
		}
	}
}
