using System;

namespace iTextSharp.text.pdf.qrcode
{
	// Token: 0x020002C9 RID: 713
	public sealed class FormatInformation
	{
		// Token: 0x06001AAB RID: 6827 RVA: 0x0009CF46 File Offset: 0x0009BF46
		private FormatInformation(int formatInfo)
		{
			this.errorCorrectionLevel = ErrorCorrectionLevel.ForBits(formatInfo >> 3 & 3);
			this.dataMask = (byte)(formatInfo & 7);
		}

		// Token: 0x06001AAC RID: 6828 RVA: 0x0009CF68 File Offset: 0x0009BF68
		public static int NumBitsDiffering(int a, int b)
		{
			a ^= b;
			return FormatInformation.BITS_SET_IN_HALF_BYTE[a & 15] + FormatInformation.BITS_SET_IN_HALF_BYTE[a >> 4 & 15] + FormatInformation.BITS_SET_IN_HALF_BYTE[a >> 8 & 15] + FormatInformation.BITS_SET_IN_HALF_BYTE[a >> 12 & 15] + FormatInformation.BITS_SET_IN_HALF_BYTE[a >> 16 & 15] + FormatInformation.BITS_SET_IN_HALF_BYTE[a >> 20 & 15] + FormatInformation.BITS_SET_IN_HALF_BYTE[a >> 24 & 15] + FormatInformation.BITS_SET_IN_HALF_BYTE[a >> 28 & 15];
		}

		// Token: 0x06001AAD RID: 6829 RVA: 0x0009CFE4 File Offset: 0x0009BFE4
		public static FormatInformation DecodeFormatInformation(int maskedFormatInfo1, int maskedFormatInfo2)
		{
			FormatInformation formatInformation = FormatInformation.DoDecodeFormatInformation(maskedFormatInfo1, maskedFormatInfo2);
			if (formatInformation != null)
			{
				return formatInformation;
			}
			return FormatInformation.DoDecodeFormatInformation(maskedFormatInfo1 ^ 21522, maskedFormatInfo2 ^ 21522);
		}

		// Token: 0x06001AAE RID: 6830 RVA: 0x0009D014 File Offset: 0x0009C014
		private static FormatInformation DoDecodeFormatInformation(int maskedFormatInfo1, int maskedFormatInfo2)
		{
			int num = int.MaxValue;
			int formatInfo = 0;
			for (int i = 0; i < FormatInformation.FORMAT_INFO_DECODE_LOOKUP.GetLength(0); i++)
			{
				int[] array = FormatInformation.FORMAT_INFO_DECODE_LOOKUP[i];
				int num2 = array[0];
				if (num2 == maskedFormatInfo1 || num2 == maskedFormatInfo2)
				{
					return new FormatInformation(array[1]);
				}
				int num3 = FormatInformation.NumBitsDiffering(maskedFormatInfo1, num2);
				if (num3 < num)
				{
					formatInfo = array[1];
					num = num3;
				}
				if (maskedFormatInfo1 != maskedFormatInfo2)
				{
					num3 = FormatInformation.NumBitsDiffering(maskedFormatInfo2, num2);
					if (num3 < num)
					{
						formatInfo = array[1];
						num = num3;
					}
				}
			}
			if (num <= 3)
			{
				return new FormatInformation(formatInfo);
			}
			return null;
		}

		// Token: 0x06001AAF RID: 6831 RVA: 0x0009D09B File Offset: 0x0009C09B
		public ErrorCorrectionLevel GetErrorCorrectionLevel()
		{
			return this.errorCorrectionLevel;
		}

		// Token: 0x06001AB0 RID: 6832 RVA: 0x0009D0A3 File Offset: 0x0009C0A3
		public byte GetDataMask()
		{
			return this.dataMask;
		}

		// Token: 0x06001AB1 RID: 6833 RVA: 0x0009D0AB File Offset: 0x0009C0AB
		public int HashCode()
		{
			return this.errorCorrectionLevel.Ordinal() << 3 | (int)this.dataMask;
		}

		// Token: 0x06001AB2 RID: 6834 RVA: 0x0009D0C1 File Offset: 0x0009C0C1
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06001AB3 RID: 6835 RVA: 0x0009D0CC File Offset: 0x0009C0CC
		public override bool Equals(object o)
		{
			if (!(o is FormatInformation))
			{
				return false;
			}
			FormatInformation formatInformation = (FormatInformation)o;
			return this.errorCorrectionLevel == formatInformation.errorCorrectionLevel && this.dataMask == formatInformation.dataMask;
		}

		// Token: 0x06001AB4 RID: 6836 RVA: 0x0009D148 File Offset: 0x0009C148
		// Note: this type is marked as 'beforefieldinit'.
		static FormatInformation()
		{
			int[][] array = new int[32][];
			int[][] array2 = array;
			int num = 0;
			int[] array3 = new int[2];
			array3[0] = 21522;
			array2[num] = array3;
			array[1] = new int[]
			{
				20773,
				1
			};
			array[2] = new int[]
			{
				24188,
				2
			};
			array[3] = new int[]
			{
				23371,
				3
			};
			array[4] = new int[]
			{
				17913,
				4
			};
			array[5] = new int[]
			{
				16590,
				5
			};
			array[6] = new int[]
			{
				20375,
				6
			};
			array[7] = new int[]
			{
				19104,
				7
			};
			array[8] = new int[]
			{
				30660,
				8
			};
			array[9] = new int[]
			{
				29427,
				9
			};
			array[10] = new int[]
			{
				32170,
				10
			};
			array[11] = new int[]
			{
				30877,
				11
			};
			array[12] = new int[]
			{
				26159,
				12
			};
			array[13] = new int[]
			{
				25368,
				13
			};
			array[14] = new int[]
			{
				27713,
				14
			};
			array[15] = new int[]
			{
				26998,
				15
			};
			array[16] = new int[]
			{
				5769,
				16
			};
			array[17] = new int[]
			{
				5054,
				17
			};
			array[18] = new int[]
			{
				7399,
				18
			};
			array[19] = new int[]
			{
				6608,
				19
			};
			array[20] = new int[]
			{
				1890,
				20
			};
			array[21] = new int[]
			{
				597,
				21
			};
			array[22] = new int[]
			{
				3340,
				22
			};
			array[23] = new int[]
			{
				2107,
				23
			};
			array[24] = new int[]
			{
				13663,
				24
			};
			array[25] = new int[]
			{
				12392,
				25
			};
			array[26] = new int[]
			{
				16177,
				26
			};
			array[27] = new int[]
			{
				14854,
				27
			};
			array[28] = new int[]
			{
				9396,
				28
			};
			array[29] = new int[]
			{
				8579,
				29
			};
			array[30] = new int[]
			{
				11994,
				30
			};
			array[31] = new int[]
			{
				11245,
				31
			};
			FormatInformation.FORMAT_INFO_DECODE_LOOKUP = array;
			FormatInformation.BITS_SET_IN_HALF_BYTE = new int[]
			{
				0,
				1,
				1,
				2,
				1,
				2,
				2,
				3,
				1,
				2,
				2,
				3,
				2,
				3,
				3,
				4
			};
		}

		// Token: 0x040011C3 RID: 4547
		private const int FORMAT_INFO_MASK_QR = 21522;

		// Token: 0x040011C4 RID: 4548
		private static readonly int[][] FORMAT_INFO_DECODE_LOOKUP;

		// Token: 0x040011C5 RID: 4549
		private static readonly int[] BITS_SET_IN_HALF_BYTE;

		// Token: 0x040011C6 RID: 4550
		private ErrorCorrectionLevel errorCorrectionLevel;

		// Token: 0x040011C7 RID: 4551
		private byte dataMask;
	}
}
