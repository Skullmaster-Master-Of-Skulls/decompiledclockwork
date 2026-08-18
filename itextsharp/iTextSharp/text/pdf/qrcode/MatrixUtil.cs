using System;

namespace iTextSharp.text.pdf.qrcode
{
	// Token: 0x02000637 RID: 1591
	public sealed class MatrixUtil
	{
		// Token: 0x060035D4 RID: 13780 RVA: 0x0014DF5A File Offset: 0x0014CF5A
		private MatrixUtil()
		{
		}

		// Token: 0x060035D5 RID: 13781 RVA: 0x0014DF62 File Offset: 0x0014CF62
		public static void ClearMatrix(ByteMatrix matrix)
		{
			matrix.Clear(-1);
		}

		// Token: 0x060035D6 RID: 13782 RVA: 0x0014DF6B File Offset: 0x0014CF6B
		public static void BuildMatrix(BitVector dataBits, ErrorCorrectionLevel ecLevel, int version, int maskPattern, ByteMatrix matrix)
		{
			MatrixUtil.ClearMatrix(matrix);
			MatrixUtil.EmbedBasicPatterns(version, matrix);
			MatrixUtil.EmbedTypeInfo(ecLevel, maskPattern, matrix);
			MatrixUtil.MaybeEmbedVersionInfo(version, matrix);
			MatrixUtil.EmbedDataBits(dataBits, maskPattern, matrix);
		}

		// Token: 0x060035D7 RID: 13783 RVA: 0x0014DF96 File Offset: 0x0014CF96
		public static void EmbedBasicPatterns(int version, ByteMatrix matrix)
		{
			MatrixUtil.EmbedPositionDetectionPatternsAndSeparators(matrix);
			MatrixUtil.EmbedDarkDotAtLeftBottomCorner(matrix);
			MatrixUtil.MaybeEmbedPositionAdjustmentPatterns(version, matrix);
			MatrixUtil.EmbedTimingPatterns(matrix);
		}

		// Token: 0x060035D8 RID: 13784 RVA: 0x0014DFB4 File Offset: 0x0014CFB4
		public static void EmbedTypeInfo(ErrorCorrectionLevel ecLevel, int maskPattern, ByteMatrix matrix)
		{
			BitVector bitVector = new BitVector();
			MatrixUtil.MakeTypeInfoBits(ecLevel, maskPattern, bitVector);
			for (int i = 0; i < bitVector.Size(); i++)
			{
				int value = bitVector.At(bitVector.Size() - 1 - i);
				int x = MatrixUtil.TYPE_INFO_COORDINATES[i][0];
				int y = MatrixUtil.TYPE_INFO_COORDINATES[i][1];
				matrix.Set(x, y, value);
				if (i < 8)
				{
					int x2 = matrix.GetWidth() - i - 1;
					int y2 = 8;
					matrix.Set(x2, y2, value);
				}
				else
				{
					int x3 = 8;
					int y3 = matrix.GetHeight() - 7 + (i - 8);
					matrix.Set(x3, y3, value);
				}
			}
		}

		// Token: 0x060035D9 RID: 13785 RVA: 0x0014E04C File Offset: 0x0014D04C
		public static void MaybeEmbedVersionInfo(int version, ByteMatrix matrix)
		{
			if (version < 7)
			{
				return;
			}
			BitVector bitVector = new BitVector();
			MatrixUtil.MakeVersionInfoBits(version, bitVector);
			int num = 17;
			for (int i = 0; i < 6; i++)
			{
				for (int j = 0; j < 3; j++)
				{
					int value = bitVector.At(num);
					num--;
					matrix.Set(i, matrix.GetHeight() - 11 + j, value);
					matrix.Set(matrix.GetHeight() - 11 + j, i, value);
				}
			}
		}

		// Token: 0x060035DA RID: 13786 RVA: 0x0014E0BC File Offset: 0x0014D0BC
		public static void EmbedDataBits(BitVector dataBits, int maskPattern, ByteMatrix matrix)
		{
			int num = 0;
			int num2 = -1;
			int i = matrix.GetWidth() - 1;
			int num3 = matrix.GetHeight() - 1;
			while (i > 0)
			{
				if (i == 6)
				{
					i--;
				}
				while (num3 >= 0 && num3 < matrix.GetHeight())
				{
					for (int j = 0; j < 2; j++)
					{
						int x = i - j;
						if (MatrixUtil.IsEmpty((int)matrix.Get(x, num3)))
						{
							int num4;
							if (num < dataBits.Size())
							{
								num4 = dataBits.At(num);
								num++;
							}
							else
							{
								num4 = 0;
							}
							if (maskPattern != -1 && MaskUtil.GetDataMaskBit(maskPattern, x, num3))
							{
								num4 ^= 1;
							}
							matrix.Set(x, num3, num4);
						}
					}
					num3 += num2;
				}
				num2 = -num2;
				num3 += num2;
				i -= 2;
			}
			if (num != dataBits.Size())
			{
				throw new WriterException(string.Concat(new object[]
				{
					"Not all bits consumed: ",
					num,
					'/',
					dataBits.Size()
				}));
			}
		}

		// Token: 0x060035DB RID: 13787 RVA: 0x0014E1C0 File Offset: 0x0014D1C0
		public static int FindMSBSet(int value)
		{
			uint num = (uint)value;
			int num2 = 0;
			while (num != 0U)
			{
				num >>= 1;
				num2++;
			}
			return num2;
		}

		// Token: 0x060035DC RID: 13788 RVA: 0x0014E1E0 File Offset: 0x0014D1E0
		public static int CalculateBCHCode(int value, int poly)
		{
			int num = MatrixUtil.FindMSBSet(poly);
			value <<= num - 1;
			while (MatrixUtil.FindMSBSet(value) >= num)
			{
				value ^= poly << MatrixUtil.FindMSBSet(value) - num;
			}
			return value;
		}

		// Token: 0x060035DD RID: 13789 RVA: 0x0014E21C File Offset: 0x0014D21C
		public static void MakeTypeInfoBits(ErrorCorrectionLevel ecLevel, int maskPattern, BitVector bits)
		{
			if (!QRCode.IsValidMaskPattern(maskPattern))
			{
				throw new WriterException("Invalid mask pattern");
			}
			int value = ecLevel.GetBits() << 3 | maskPattern;
			bits.AppendBits(value, 5);
			int value2 = MatrixUtil.CalculateBCHCode(value, 1335);
			bits.AppendBits(value2, 10);
			BitVector bitVector = new BitVector();
			bitVector.AppendBits(21522, 15);
			bits.Xor(bitVector);
			if (bits.Size() != 15)
			{
				throw new WriterException("should not happen but we got: " + bits.Size());
			}
		}

		// Token: 0x060035DE RID: 13790 RVA: 0x0014E2A4 File Offset: 0x0014D2A4
		public static void MakeVersionInfoBits(int version, BitVector bits)
		{
			bits.AppendBits(version, 6);
			int value = MatrixUtil.CalculateBCHCode(version, 7973);
			bits.AppendBits(value, 12);
			if (bits.Size() != 18)
			{
				throw new WriterException("should not happen but we got: " + bits.Size());
			}
		}

		// Token: 0x060035DF RID: 13791 RVA: 0x0014E2F3 File Offset: 0x0014D2F3
		private static bool IsEmpty(int value)
		{
			return value == -1;
		}

		// Token: 0x060035E0 RID: 13792 RVA: 0x0014E2F9 File Offset: 0x0014D2F9
		private static bool IsValidValue(int value)
		{
			return value == -1 || value == 0 || value == 1;
		}

		// Token: 0x060035E1 RID: 13793 RVA: 0x0014E308 File Offset: 0x0014D308
		private static void EmbedTimingPatterns(ByteMatrix matrix)
		{
			for (int i = 8; i < matrix.GetWidth() - 8; i++)
			{
				int value = (i + 1) % 2;
				if (!MatrixUtil.IsValidValue((int)matrix.Get(i, 6)))
				{
					throw new WriterException();
				}
				if (MatrixUtil.IsEmpty((int)matrix.Get(i, 6)))
				{
					matrix.Set(i, 6, value);
				}
				if (!MatrixUtil.IsValidValue((int)matrix.Get(6, i)))
				{
					throw new WriterException();
				}
				if (MatrixUtil.IsEmpty((int)matrix.Get(6, i)))
				{
					matrix.Set(6, i, value);
				}
			}
		}

		// Token: 0x060035E2 RID: 13794 RVA: 0x0014E388 File Offset: 0x0014D388
		private static void EmbedDarkDotAtLeftBottomCorner(ByteMatrix matrix)
		{
			if (matrix.Get(8, matrix.GetHeight() - 8) == 0)
			{
				throw new WriterException();
			}
			matrix.Set(8, matrix.GetHeight() - 8, 1);
		}

		// Token: 0x060035E3 RID: 13795 RVA: 0x0014E3B4 File Offset: 0x0014D3B4
		private static void EmbedHorizontalSeparationPattern(int xStart, int yStart, ByteMatrix matrix)
		{
			if (MatrixUtil.HORIZONTAL_SEPARATION_PATTERN[0].Length != 8 || MatrixUtil.HORIZONTAL_SEPARATION_PATTERN.GetLength(0) != 1)
			{
				throw new WriterException("Bad horizontal separation pattern");
			}
			for (int i = 0; i < 8; i++)
			{
				if (!MatrixUtil.IsEmpty((int)matrix.Get(xStart + i, yStart)))
				{
					throw new WriterException();
				}
				matrix.Set(xStart + i, yStart, MatrixUtil.HORIZONTAL_SEPARATION_PATTERN[0][i]);
			}
		}

		// Token: 0x060035E4 RID: 13796 RVA: 0x0014E41C File Offset: 0x0014D41C
		private static void EmbedVerticalSeparationPattern(int xStart, int yStart, ByteMatrix matrix)
		{
			if (MatrixUtil.VERTICAL_SEPARATION_PATTERN[0].Length != 1 || MatrixUtil.VERTICAL_SEPARATION_PATTERN.GetLength(0) != 7)
			{
				throw new WriterException("Bad vertical separation pattern");
			}
			for (int i = 0; i < 7; i++)
			{
				if (!MatrixUtil.IsEmpty((int)matrix.Get(xStart, yStart + i)))
				{
					throw new WriterException();
				}
				matrix.Set(xStart, yStart + i, MatrixUtil.VERTICAL_SEPARATION_PATTERN[i][0]);
			}
		}

		// Token: 0x060035E5 RID: 13797 RVA: 0x0014E484 File Offset: 0x0014D484
		private static void EmbedPositionAdjustmentPattern(int xStart, int yStart, ByteMatrix matrix)
		{
			if (MatrixUtil.POSITION_ADJUSTMENT_PATTERN[0].Length != 5 || MatrixUtil.POSITION_ADJUSTMENT_PATTERN.GetLength(0) != 5)
			{
				throw new WriterException("Bad position adjustment");
			}
			for (int i = 0; i < 5; i++)
			{
				for (int j = 0; j < 5; j++)
				{
					if (!MatrixUtil.IsEmpty((int)matrix.Get(xStart + j, yStart + i)))
					{
						throw new WriterException();
					}
					matrix.Set(xStart + j, yStart + i, MatrixUtil.POSITION_ADJUSTMENT_PATTERN[i][j]);
				}
			}
		}

		// Token: 0x060035E6 RID: 13798 RVA: 0x0014E4FC File Offset: 0x0014D4FC
		private static void EmbedPositionDetectionPattern(int xStart, int yStart, ByteMatrix matrix)
		{
			if (MatrixUtil.POSITION_DETECTION_PATTERN[0].Length != 7 || MatrixUtil.POSITION_DETECTION_PATTERN.GetLength(0) != 7)
			{
				throw new WriterException("Bad position detection pattern");
			}
			for (int i = 0; i < 7; i++)
			{
				for (int j = 0; j < 7; j++)
				{
					if (!MatrixUtil.IsEmpty((int)matrix.Get(xStart + j, yStart + i)))
					{
						throw new WriterException();
					}
					matrix.Set(xStart + j, yStart + i, MatrixUtil.POSITION_DETECTION_PATTERN[i][j]);
				}
			}
		}

		// Token: 0x060035E7 RID: 13799 RVA: 0x0014E574 File Offset: 0x0014D574
		private static void EmbedPositionDetectionPatternsAndSeparators(ByteMatrix matrix)
		{
			int num = MatrixUtil.POSITION_DETECTION_PATTERN[0].Length;
			MatrixUtil.EmbedPositionDetectionPattern(0, 0, matrix);
			MatrixUtil.EmbedPositionDetectionPattern(matrix.GetWidth() - num, 0, matrix);
			MatrixUtil.EmbedPositionDetectionPattern(0, matrix.GetWidth() - num, matrix);
			int num2 = MatrixUtil.HORIZONTAL_SEPARATION_PATTERN[0].Length;
			MatrixUtil.EmbedHorizontalSeparationPattern(0, num2 - 1, matrix);
			MatrixUtil.EmbedHorizontalSeparationPattern(matrix.GetWidth() - num2, num2 - 1, matrix);
			MatrixUtil.EmbedHorizontalSeparationPattern(0, matrix.GetWidth() - num2, matrix);
			int num3 = MatrixUtil.VERTICAL_SEPARATION_PATTERN.Length;
			MatrixUtil.EmbedVerticalSeparationPattern(num3, 0, matrix);
			MatrixUtil.EmbedVerticalSeparationPattern(matrix.GetHeight() - num3 - 1, 0, matrix);
			MatrixUtil.EmbedVerticalSeparationPattern(num3, matrix.GetHeight() - num3, matrix);
		}

		// Token: 0x060035E8 RID: 13800 RVA: 0x0014E618 File Offset: 0x0014D618
		private static void MaybeEmbedPositionAdjustmentPatterns(int version, ByteMatrix matrix)
		{
			if (version < 2)
			{
				return;
			}
			int num = version - 1;
			int[] array = MatrixUtil.POSITION_ADJUSTMENT_PATTERN_COORDINATE_TABLE[num];
			int num2 = MatrixUtil.POSITION_ADJUSTMENT_PATTERN_COORDINATE_TABLE[num].Length;
			for (int i = 0; i < num2; i++)
			{
				for (int j = 0; j < num2; j++)
				{
					int num3 = array[i];
					int num4 = array[j];
					if (num4 != -1 && num3 != -1 && MatrixUtil.IsEmpty((int)matrix.Get(num4, num3)))
					{
						MatrixUtil.EmbedPositionAdjustmentPattern(num4 - 2, num3 - 2, matrix);
					}
				}
			}
		}

		// Token: 0x060035E9 RID: 13801 RVA: 0x0014EC74 File Offset: 0x0014DC74
		// Note: this type is marked as 'beforefieldinit'.
		static MatrixUtil()
		{
			int[][] array = new int[1][];
			int[][] array2 = array;
			int num = 0;
			int[] array3 = new int[8];
			array2[num] = array3;
			MatrixUtil.HORIZONTAL_SEPARATION_PATTERN = array;
			int[][] array4 = new int[7][];
			int[][] array5 = array4;
			int num2 = 0;
			int[] array6 = new int[1];
			array5[num2] = array6;
			int[][] array7 = array4;
			int num3 = 1;
			int[] array8 = new int[1];
			array7[num3] = array8;
			int[][] array9 = array4;
			int num4 = 2;
			int[] array10 = new int[1];
			array9[num4] = array10;
			int[][] array11 = array4;
			int num5 = 3;
			int[] array12 = new int[1];
			array11[num5] = array12;
			int[][] array13 = array4;
			int num6 = 4;
			int[] array14 = new int[1];
			array13[num6] = array14;
			int[][] array15 = array4;
			int num7 = 5;
			int[] array16 = new int[1];
			array15[num7] = array16;
			int[][] array17 = array4;
			int num8 = 6;
			int[] array18 = new int[1];
			array17[num8] = array18;
			MatrixUtil.VERTICAL_SEPARATION_PATTERN = array4;
			MatrixUtil.POSITION_ADJUSTMENT_PATTERN = new int[][]
			{
				new int[]
				{
					1,
					1,
					1,
					1,
					1
				},
				new int[]
				{
					1,
					0,
					0,
					0,
					1
				},
				new int[]
				{
					1,
					0,
					1,
					0,
					1
				},
				new int[]
				{
					1,
					0,
					0,
					0,
					1
				},
				new int[]
				{
					1,
					1,
					1,
					1,
					1
				}
			};
			MatrixUtil.POSITION_ADJUSTMENT_PATTERN_COORDINATE_TABLE = new int[][]
			{
				new int[]
				{
					-1,
					-1,
					-1,
					-1,
					-1,
					-1,
					-1
				},
				new int[]
				{
					6,
					18,
					-1,
					-1,
					-1,
					-1,
					-1
				},
				new int[]
				{
					6,
					22,
					-1,
					-1,
					-1,
					-1,
					-1
				},
				new int[]
				{
					6,
					26,
					-1,
					-1,
					-1,
					-1,
					-1
				},
				new int[]
				{
					6,
					30,
					-1,
					-1,
					-1,
					-1,
					-1
				},
				new int[]
				{
					6,
					34,
					-1,
					-1,
					-1,
					-1,
					-1
				},
				new int[]
				{
					6,
					22,
					38,
					-1,
					-1,
					-1,
					-1
				},
				new int[]
				{
					6,
					24,
					42,
					-1,
					-1,
					-1,
					-1
				},
				new int[]
				{
					6,
					26,
					46,
					-1,
					-1,
					-1,
					-1
				},
				new int[]
				{
					6,
					28,
					50,
					-1,
					-1,
					-1,
					-1
				},
				new int[]
				{
					6,
					30,
					54,
					-1,
					-1,
					-1,
					-1
				},
				new int[]
				{
					6,
					32,
					58,
					-1,
					-1,
					-1,
					-1
				},
				new int[]
				{
					6,
					34,
					62,
					-1,
					-1,
					-1,
					-1
				},
				new int[]
				{
					6,
					26,
					46,
					66,
					-1,
					-1,
					-1
				},
				new int[]
				{
					6,
					26,
					48,
					70,
					-1,
					-1,
					-1
				},
				new int[]
				{
					6,
					26,
					50,
					74,
					-1,
					-1,
					-1
				},
				new int[]
				{
					6,
					30,
					54,
					78,
					-1,
					-1,
					-1
				},
				new int[]
				{
					6,
					30,
					56,
					82,
					-1,
					-1,
					-1
				},
				new int[]
				{
					6,
					30,
					58,
					86,
					-1,
					-1,
					-1
				},
				new int[]
				{
					6,
					34,
					62,
					90,
					-1,
					-1,
					-1
				},
				new int[]
				{
					6,
					28,
					50,
					72,
					94,
					-1,
					-1
				},
				new int[]
				{
					6,
					26,
					50,
					74,
					98,
					-1,
					-1
				},
				new int[]
				{
					6,
					30,
					54,
					78,
					102,
					-1,
					-1
				},
				new int[]
				{
					6,
					28,
					54,
					80,
					106,
					-1,
					-1
				},
				new int[]
				{
					6,
					32,
					58,
					84,
					110,
					-1,
					-1
				},
				new int[]
				{
					6,
					30,
					58,
					86,
					114,
					-1,
					-1
				},
				new int[]
				{
					6,
					34,
					62,
					90,
					118,
					-1,
					-1
				},
				new int[]
				{
					6,
					26,
					50,
					74,
					98,
					122,
					-1
				},
				new int[]
				{
					6,
					30,
					54,
					78,
					102,
					126,
					-1
				},
				new int[]
				{
					6,
					26,
					52,
					78,
					104,
					130,
					-1
				},
				new int[]
				{
					6,
					30,
					56,
					82,
					108,
					134,
					-1
				},
				new int[]
				{
					6,
					34,
					60,
					86,
					112,
					138,
					-1
				},
				new int[]
				{
					6,
					30,
					58,
					86,
					114,
					142,
					-1
				},
				new int[]
				{
					6,
					34,
					62,
					90,
					118,
					146,
					-1
				},
				new int[]
				{
					6,
					30,
					54,
					78,
					102,
					126,
					150
				},
				new int[]
				{
					6,
					24,
					50,
					76,
					102,
					128,
					154
				},
				new int[]
				{
					6,
					28,
					54,
					80,
					106,
					132,
					158
				},
				new int[]
				{
					6,
					32,
					58,
					84,
					110,
					136,
					162
				},
				new int[]
				{
					6,
					26,
					54,
					82,
					110,
					138,
					166
				},
				new int[]
				{
					6,
					30,
					58,
					86,
					114,
					142,
					170
				}
			};
			int[][] array19 = new int[15][];
			int[][] array20 = array19;
			int num9 = 0;
			int[] array21 = new int[2];
			array21[0] = 8;
			array20[num9] = array21;
			array19[1] = new int[]
			{
				8,
				1
			};
			array19[2] = new int[]
			{
				8,
				2
			};
			array19[3] = new int[]
			{
				8,
				3
			};
			array19[4] = new int[]
			{
				8,
				4
			};
			array19[5] = new int[]
			{
				8,
				5
			};
			array19[6] = new int[]
			{
				8,
				7
			};
			array19[7] = new int[]
			{
				8,
				8
			};
			array19[8] = new int[]
			{
				7,
				8
			};
			array19[9] = new int[]
			{
				5,
				8
			};
			array19[10] = new int[]
			{
				4,
				8
			};
			array19[11] = new int[]
			{
				3,
				8
			};
			array19[12] = new int[]
			{
				2,
				8
			};
			array19[13] = new int[]
			{
				1,
				8
			};
			array19[14] = new int[]
			{
				0,
				8
			};
			MatrixUtil.TYPE_INFO_COORDINATES = array19;
		}

		// Token: 0x0400243A RID: 9274
		private const int VERSION_INFO_POLY = 7973;

		// Token: 0x0400243B RID: 9275
		private const int TYPE_INFO_POLY = 1335;

		// Token: 0x0400243C RID: 9276
		private const int TYPE_INFO_MASK_PATTERN = 21522;

		// Token: 0x0400243D RID: 9277
		private static readonly int[][] POSITION_DETECTION_PATTERN = new int[][]
		{
			new int[]
			{
				1,
				1,
				1,
				1,
				1,
				1,
				1
			},
			new int[]
			{
				1,
				0,
				0,
				0,
				0,
				0,
				1
			},
			new int[]
			{
				1,
				0,
				1,
				1,
				1,
				0,
				1
			},
			new int[]
			{
				1,
				0,
				1,
				1,
				1,
				0,
				1
			},
			new int[]
			{
				1,
				0,
				1,
				1,
				1,
				0,
				1
			},
			new int[]
			{
				1,
				0,
				0,
				0,
				0,
				0,
				1
			},
			new int[]
			{
				1,
				1,
				1,
				1,
				1,
				1,
				1
			}
		};

		// Token: 0x0400243E RID: 9278
		private static readonly int[][] HORIZONTAL_SEPARATION_PATTERN;

		// Token: 0x0400243F RID: 9279
		private static readonly int[][] VERTICAL_SEPARATION_PATTERN;

		// Token: 0x04002440 RID: 9280
		private static readonly int[][] POSITION_ADJUSTMENT_PATTERN;

		// Token: 0x04002441 RID: 9281
		private static readonly int[][] POSITION_ADJUSTMENT_PATTERN_COORDINATE_TABLE;

		// Token: 0x04002442 RID: 9282
		private static readonly int[][] TYPE_INFO_COORDINATES;
	}
}
