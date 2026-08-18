using System;

namespace iTextSharp.text.pdf.qrcode
{
	// Token: 0x02000454 RID: 1108
	public sealed class MaskUtil
	{
		// Token: 0x06002560 RID: 9568 RVA: 0x000E25E7 File Offset: 0x000E15E7
		private MaskUtil()
		{
		}

		// Token: 0x06002561 RID: 9569 RVA: 0x000E25EF File Offset: 0x000E15EF
		public static int ApplyMaskPenaltyRule1(ByteMatrix matrix)
		{
			return MaskUtil.ApplyMaskPenaltyRule1Internal(matrix, true) + MaskUtil.ApplyMaskPenaltyRule1Internal(matrix, false);
		}

		// Token: 0x06002562 RID: 9570 RVA: 0x000E2600 File Offset: 0x000E1600
		public static int ApplyMaskPenaltyRule2(ByteMatrix matrix)
		{
			int num = 0;
			sbyte[][] array = matrix.GetArray();
			int width = matrix.GetWidth();
			int height = matrix.GetHeight();
			for (int i = 0; i < height - 1; i++)
			{
				for (int j = 0; j < width - 1; j++)
				{
					int num2 = (int)array[i][j];
					if (num2 == (int)array[i][j + 1] && num2 == (int)array[i + 1][j] && num2 == (int)array[i + 1][j + 1])
					{
						num += 3;
					}
				}
			}
			return num;
		}

		// Token: 0x06002563 RID: 9571 RVA: 0x000E2680 File Offset: 0x000E1680
		public static int ApplyMaskPenaltyRule3(ByteMatrix matrix)
		{
			int num = 0;
			sbyte[][] array = matrix.GetArray();
			int width = matrix.GetWidth();
			int height = matrix.GetHeight();
			for (int i = 0; i < height; i++)
			{
				for (int j = 0; j < width; j++)
				{
					if (j + 6 < width && array[i][j] == 1 && array[i][j + 1] == 0 && array[i][j + 2] == 1 && array[i][j + 3] == 1 && array[i][j + 4] == 1 && array[i][j + 5] == 0 && array[i][j + 6] == 1 && ((j + 10 < width && array[i][j + 7] == 0 && array[i][j + 8] == 0 && array[i][j + 9] == 0 && array[i][j + 10] == 0) || (j - 4 >= 0 && array[i][j - 1] == 0 && array[i][j - 2] == 0 && array[i][j - 3] == 0 && array[i][j - 4] == 0)))
					{
						num += 40;
					}
					if (i + 6 < height && array[i][j] == 1 && array[i + 1][j] == 0 && array[i + 2][j] == 1 && array[i + 3][j] == 1 && array[i + 4][j] == 1 && array[i + 5][j] == 0 && array[i + 6][j] == 1 && ((i + 10 < height && array[i + 7][j] == 0 && array[i + 8][j] == 0 && array[i + 9][j] == 0 && array[i + 10][j] == 0) || (i - 4 >= 0 && array[i - 1][j] == 0 && array[i - 2][j] == 0 && array[i - 3][j] == 0 && array[i - 4][j] == 0)))
					{
						num += 40;
					}
				}
			}
			return num;
		}

		// Token: 0x06002564 RID: 9572 RVA: 0x000E2880 File Offset: 0x000E1880
		public static int ApplyMaskPenaltyRule4(ByteMatrix matrix)
		{
			int num = 0;
			sbyte[][] array = matrix.GetArray();
			int width = matrix.GetWidth();
			int height = matrix.GetHeight();
			for (int i = 0; i < height; i++)
			{
				for (int j = 0; j < width; j++)
				{
					if (array[i][j] == 1)
					{
						num++;
					}
				}
			}
			int num2 = matrix.GetHeight() * matrix.GetWidth();
			double num3 = (double)num / (double)num2;
			return Math.Abs((int)(num3 * 100.0 - 50.0)) / 5 * 10;
		}

		// Token: 0x06002565 RID: 9573 RVA: 0x000E290C File Offset: 0x000E190C
		public static bool GetDataMaskBit(int maskPattern, int x, int y)
		{
			if (!QRCode.IsValidMaskPattern(maskPattern))
			{
				throw new ArgumentException("Invalid mask pattern");
			}
			int num;
			switch (maskPattern)
			{
			case 0:
				num = (y + x & 1);
				break;
			case 1:
				num = (y & 1);
				break;
			case 2:
				num = x % 3;
				break;
			case 3:
				num = (y + x) % 3;
				break;
			case 4:
				num = ((y >> 1) + x / 3 & 1);
				break;
			case 5:
			{
				int num2 = y * x;
				num = (num2 & 1) + num2 % 3;
				break;
			}
			case 6:
			{
				int num2 = y * x;
				num = ((num2 & 1) + num2 % 3 & 1);
				break;
			}
			case 7:
			{
				int num2 = y * x;
				num = (num2 % 3 + (y + x & 1) & 1);
				break;
			}
			default:
				throw new ArgumentException("Invalid mask pattern: " + maskPattern);
			}
			return num == 0;
		}

		// Token: 0x06002566 RID: 9574 RVA: 0x000E29C8 File Offset: 0x000E19C8
		private static int ApplyMaskPenaltyRule1Internal(ByteMatrix matrix, bool isHorizontal)
		{
			int num = 0;
			int num2 = 0;
			int num3 = -1;
			int num4 = isHorizontal ? matrix.GetHeight() : matrix.GetWidth();
			int num5 = isHorizontal ? matrix.GetWidth() : matrix.GetHeight();
			sbyte[][] array = matrix.GetArray();
			for (int i = 0; i < num4; i++)
			{
				for (int j = 0; j < num5; j++)
				{
					int num6 = (int)(isHorizontal ? array[i][j] : array[j][i]);
					if (num6 == num3)
					{
						num2++;
						if (num2 == 5)
						{
							num += 3;
						}
						else if (num2 > 5)
						{
							num++;
						}
					}
					else
					{
						num2 = 1;
						num3 = num6;
					}
				}
				num2 = 0;
			}
			return num;
		}
	}
}
