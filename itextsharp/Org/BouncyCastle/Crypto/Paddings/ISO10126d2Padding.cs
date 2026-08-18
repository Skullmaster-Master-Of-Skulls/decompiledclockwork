using System;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Paddings
{
	// Token: 0x0200054C RID: 1356
	public class ISO10126d2Padding : IBlockCipherPadding
	{
		// Token: 0x06002EA0 RID: 11936 RVA: 0x0011FCB4 File Offset: 0x0011ECB4
		public void Init(SecureRandom random)
		{
			this.random = ((random != null) ? random : new SecureRandom());
		}

		// Token: 0x17000803 RID: 2051
		// (get) Token: 0x06002EA1 RID: 11937 RVA: 0x0011FCC7 File Offset: 0x0011ECC7
		public string PaddingName
		{
			get
			{
				return "ISO10126-2";
			}
		}

		// Token: 0x06002EA2 RID: 11938 RVA: 0x0011FCD0 File Offset: 0x0011ECD0
		public int AddPadding(byte[] input, int inOff)
		{
			byte b = (byte)(input.Length - inOff);
			while (inOff < input.Length - 1)
			{
				input[inOff] = (byte)this.random.NextInt();
				inOff++;
			}
			input[inOff] = b;
			return (int)b;
		}

		// Token: 0x06002EA3 RID: 11939 RVA: 0x0011FD08 File Offset: 0x0011ED08
		public int PadCount(byte[] input)
		{
			int num = (int)(input[input.Length - 1] & byte.MaxValue);
			if (num > input.Length)
			{
				throw new InvalidCipherTextException("pad block corrupted");
			}
			return num;
		}

		// Token: 0x04002014 RID: 8212
		private SecureRandom random;
	}
}
