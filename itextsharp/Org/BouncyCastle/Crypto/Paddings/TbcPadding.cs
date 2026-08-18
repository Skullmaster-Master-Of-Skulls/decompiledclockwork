using System;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Paddings
{
	// Token: 0x020005A9 RID: 1449
	public class TbcPadding : IBlockCipherPadding
	{
		// Token: 0x1700088B RID: 2187
		// (get) Token: 0x06003201 RID: 12801 RVA: 0x001377D5 File Offset: 0x001367D5
		public string PaddingName
		{
			get
			{
				return "TBC";
			}
		}

		// Token: 0x06003202 RID: 12802 RVA: 0x001377DC File Offset: 0x001367DC
		public virtual void Init(SecureRandom random)
		{
		}

		// Token: 0x06003203 RID: 12803 RVA: 0x001377E0 File Offset: 0x001367E0
		public virtual int AddPadding(byte[] input, int inOff)
		{
			int result = input.Length - inOff;
			byte b;
			if (inOff > 0)
			{
				b = (((input[inOff - 1] & 1) == 0) ? byte.MaxValue : 0);
			}
			else
			{
				b = (((input[input.Length - 1] & 1) == 0) ? byte.MaxValue : 0);
			}
			while (inOff < input.Length)
			{
				input[inOff] = b;
				inOff++;
			}
			return result;
		}

		// Token: 0x06003204 RID: 12804 RVA: 0x00137834 File Offset: 0x00136834
		public virtual int PadCount(byte[] input)
		{
			byte b = input[input.Length - 1];
			int num = input.Length - 1;
			while (num > 0 && input[num - 1] == b)
			{
				num--;
			}
			return input.Length - num;
		}
	}
}
