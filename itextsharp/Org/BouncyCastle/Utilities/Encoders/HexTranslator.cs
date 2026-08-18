using System;

namespace Org.BouncyCastle.Utilities.Encoders
{
	// Token: 0x020003C8 RID: 968
	public class HexTranslator : ITranslator
	{
		// Token: 0x060021B2 RID: 8626 RVA: 0x000CCC72 File Offset: 0x000CBC72
		public int GetEncodedBlockSize()
		{
			return 2;
		}

		// Token: 0x060021B3 RID: 8627 RVA: 0x000CCC78 File Offset: 0x000CBC78
		public int Encode(byte[] input, int inOff, int length, byte[] outBytes, int outOff)
		{
			int i = 0;
			int num = 0;
			while (i < length)
			{
				outBytes[outOff + num] = HexTranslator.hexTable[input[inOff] >> 4 & 15];
				outBytes[outOff + num + 1] = HexTranslator.hexTable[(int)(input[inOff] & 15)];
				inOff++;
				i++;
				num += 2;
			}
			return length * 2;
		}

		// Token: 0x060021B4 RID: 8628 RVA: 0x000CCCC9 File Offset: 0x000CBCC9
		public int GetDecodedBlockSize()
		{
			return 1;
		}

		// Token: 0x060021B5 RID: 8629 RVA: 0x000CCCCC File Offset: 0x000CBCCC
		public int Decode(byte[] input, int inOff, int length, byte[] outBytes, int outOff)
		{
			int num = length / 2;
			for (int i = 0; i < num; i++)
			{
				byte b = input[inOff + i * 2];
				byte b2 = input[inOff + i * 2 + 1];
				if (b < 97)
				{
					outBytes[outOff] = (byte)(b - 48 << 4);
				}
				else
				{
					outBytes[outOff] = (byte)(b - 97 + 10 << 4);
				}
				if (b2 < 97)
				{
					int num2 = outOff;
					outBytes[num2] += b2 - 48;
				}
				else
				{
					int num3 = outOff;
					outBytes[num3] += b2 - 97 + 10;
				}
				outOff++;
			}
			return num;
		}

		// Token: 0x04001743 RID: 5955
		private static readonly byte[] hexTable = new byte[]
		{
			48,
			49,
			50,
			51,
			52,
			53,
			54,
			55,
			56,
			57,
			97,
			98,
			99,
			100,
			101,
			102
		};
	}
}
