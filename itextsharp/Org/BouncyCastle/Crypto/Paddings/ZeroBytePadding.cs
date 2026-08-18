using System;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Paddings
{
	// Token: 0x0200018D RID: 397
	public class ZeroBytePadding : IBlockCipherPadding
	{
		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000F63 RID: 3939 RVA: 0x00058C9D File Offset: 0x00057C9D
		public string PaddingName
		{
			get
			{
				return "ZeroBytePadding";
			}
		}

		// Token: 0x06000F64 RID: 3940 RVA: 0x00058CA4 File Offset: 0x00057CA4
		public void Init(SecureRandom random)
		{
		}

		// Token: 0x06000F65 RID: 3941 RVA: 0x00058CA8 File Offset: 0x00057CA8
		public int AddPadding(byte[] input, int inOff)
		{
			int result = input.Length - inOff;
			while (inOff < input.Length)
			{
				input[inOff] = 0;
				inOff++;
			}
			return result;
		}

		// Token: 0x06000F66 RID: 3942 RVA: 0x00058CD0 File Offset: 0x00057CD0
		public int PadCount(byte[] input)
		{
			int num = input.Length;
			while (num > 0 && input[num - 1] == 0)
			{
				num--;
			}
			return input.Length - num;
		}
	}
}
