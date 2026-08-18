using System;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Paddings
{
	// Token: 0x02000246 RID: 582
	public class X923Padding : IBlockCipherPadding
	{
		// Token: 0x0600166A RID: 5738 RVA: 0x00082604 File Offset: 0x00081604
		public void Init(SecureRandom random)
		{
			this.random = random;
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x0600166B RID: 5739 RVA: 0x0008260D File Offset: 0x0008160D
		public string PaddingName
		{
			get
			{
				return "X9.23";
			}
		}

		// Token: 0x0600166C RID: 5740 RVA: 0x00082614 File Offset: 0x00081614
		public int AddPadding(byte[] input, int inOff)
		{
			byte b = (byte)(input.Length - inOff);
			while (inOff < input.Length - 1)
			{
				if (this.random == null)
				{
					input[inOff] = 0;
				}
				else
				{
					input[inOff] = (byte)this.random.NextInt();
				}
				inOff++;
			}
			input[inOff] = b;
			return (int)b;
		}

		// Token: 0x0600166D RID: 5741 RVA: 0x0008265C File Offset: 0x0008165C
		public int PadCount(byte[] input)
		{
			int num = (int)(input[input.Length - 1] & byte.MaxValue);
			if (num > input.Length)
			{
				throw new InvalidCipherTextException("pad block corrupted");
			}
			return num;
		}

		// Token: 0x04000F58 RID: 3928
		private SecureRandom random;
	}
}
