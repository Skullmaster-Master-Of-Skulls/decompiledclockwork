using System;
using iTextSharp.text.error_messages;

namespace iTextSharp.text.pdf
{
	// Token: 0x0200016C RID: 364
	public sealed class BidiOrder
	{
		// Token: 0x06000DB7 RID: 3511 RVA: 0x0004ABC7 File Offset: 0x00049BC7
		public BidiOrder(sbyte[] types)
		{
			BidiOrder.ValidateTypes(types);
			this.initialTypes = (sbyte[])types.Clone();
			this.RunAlgorithm();
		}

		// Token: 0x06000DB8 RID: 3512 RVA: 0x0004ABF3 File Offset: 0x00049BF3
		public BidiOrder(sbyte[] types, sbyte paragraphEmbeddingLevel)
		{
			BidiOrder.ValidateTypes(types);
			BidiOrder.ValidateParagraphEmbeddingLevel(paragraphEmbeddingLevel);
			this.initialTypes = (sbyte[])types.Clone();
			this.paragraphEmbeddingLevel = paragraphEmbeddingLevel;
			this.RunAlgorithm();
		}

		// Token: 0x06000DB9 RID: 3513 RVA: 0x0004AC2C File Offset: 0x00049C2C
		public BidiOrder(char[] text, int offset, int length, sbyte paragraphEmbeddingLevel)
		{
			this.initialTypes = new sbyte[length];
			for (int i = 0; i < length; i++)
			{
				this.initialTypes[i] = BidiOrder.rtypes[(int)text[offset + i]];
			}
			BidiOrder.ValidateParagraphEmbeddingLevel(paragraphEmbeddingLevel);
			this.paragraphEmbeddingLevel = paragraphEmbeddingLevel;
			this.RunAlgorithm();
		}

		// Token: 0x06000DBA RID: 3514 RVA: 0x0004AC86 File Offset: 0x00049C86
		public static sbyte GetDirection(char c)
		{
			return BidiOrder.rtypes[(int)c];
		}

		// Token: 0x06000DBB RID: 3515 RVA: 0x0004AC90 File Offset: 0x00049C90
		private void RunAlgorithm()
		{
			this.textLength = this.initialTypes.Length;
			this.resultTypes = (sbyte[])this.initialTypes.Clone();
			if (this.paragraphEmbeddingLevel == -1)
			{
				this.DetermineParagraphEmbeddingLevel();
			}
			this.resultLevels = new sbyte[this.textLength];
			this.SetLevels(0, this.textLength, this.paragraphEmbeddingLevel);
			this.DetermineExplicitEmbeddingLevels();
			this.textLength = this.RemoveExplicitCodes();
			sbyte val = this.paragraphEmbeddingLevel;
			int num;
			for (int i = 0; i < this.textLength; i = num)
			{
				sbyte b = this.resultLevels[i];
				sbyte sor = BidiOrder.TypeForLevel((int)Math.Max(val, b));
				num = i + 1;
				while (num < this.textLength && this.resultLevels[num] == b)
				{
					num++;
				}
				sbyte val2 = (num < this.textLength) ? this.resultLevels[num] : this.paragraphEmbeddingLevel;
				sbyte eor = BidiOrder.TypeForLevel((int)Math.Max(val2, b));
				this.ResolveWeakTypes(i, num, b, sor, eor);
				this.ResolveNeutralTypes(i, num, b, sor, eor);
				this.ResolveImplicitLevels(i, num, b, sor, eor);
				val = b;
			}
			this.textLength = this.ReinsertExplicitCodes(this.textLength);
		}

		// Token: 0x06000DBC RID: 3516 RVA: 0x0004ADC4 File Offset: 0x00049DC4
		private void DetermineParagraphEmbeddingLevel()
		{
			sbyte b = -1;
			for (int i = 0; i < this.textLength; i++)
			{
				sbyte b2 = this.resultTypes[i];
				if (b2 == 0 || b2 == 4 || b2 == 3)
				{
					b = b2;
					break;
				}
			}
			if (b == -1)
			{
				this.paragraphEmbeddingLevel = 0;
				return;
			}
			if (b == 0)
			{
				this.paragraphEmbeddingLevel = 0;
				return;
			}
			this.paragraphEmbeddingLevel = 1;
		}

		// Token: 0x06000DBD RID: 3517 RVA: 0x0004AE1C File Offset: 0x00049E1C
		private void DetermineExplicitEmbeddingLevels()
		{
			this.embeddings = BidiOrder.ProcessEmbeddings(this.resultTypes, this.paragraphEmbeddingLevel);
			for (int i = 0; i < this.textLength; i++)
			{
				sbyte b = this.embeddings[i];
				if (((int)b & 128) != 0)
				{
					b &= sbyte.MaxValue;
					this.resultTypes[i] = BidiOrder.TypeForLevel((int)b);
				}
				this.resultLevels[i] = b;
			}
		}

		// Token: 0x06000DBE RID: 3518 RVA: 0x0004AE80 File Offset: 0x00049E80
		private int RemoveExplicitCodes()
		{
			int num = 0;
			for (int i = 0; i < this.textLength; i++)
			{
				sbyte b = this.initialTypes[i];
				if (b != 1 && b != 5 && b != 2 && b != 6 && b != 7 && b != 14)
				{
					this.embeddings[num] = this.embeddings[i];
					this.resultTypes[num] = this.resultTypes[i];
					this.resultLevels[num] = this.resultLevels[i];
					num++;
				}
			}
			return num;
		}

		// Token: 0x06000DBF RID: 3519 RVA: 0x0004AEF8 File Offset: 0x00049EF8
		private int ReinsertExplicitCodes(int textLength)
		{
			int num = this.initialTypes.Length;
			while (--num >= 0)
			{
				sbyte b = this.initialTypes[num];
				if (b == 1 || b == 5 || b == 2 || b == 6 || b == 7 || b == 14)
				{
					this.embeddings[num] = 0;
					this.resultTypes[num] = b;
					this.resultLevels[num] = -1;
				}
				else
				{
					textLength--;
					this.embeddings[num] = this.embeddings[textLength];
					this.resultTypes[num] = this.resultTypes[textLength];
					this.resultLevels[num] = this.resultLevels[textLength];
				}
			}
			if (this.resultLevels[0] == -1)
			{
				this.resultLevels[0] = this.paragraphEmbeddingLevel;
			}
			for (int i = 1; i < this.initialTypes.Length; i++)
			{
				if (this.resultLevels[i] == -1)
				{
					this.resultLevels[i] = this.resultLevels[i - 1];
				}
			}
			return this.initialTypes.Length;
		}

		// Token: 0x06000DC0 RID: 3520 RVA: 0x0004AFE0 File Offset: 0x00049FE0
		private static sbyte[] ProcessEmbeddings(sbyte[] resultTypes, sbyte paragraphEmbeddingLevel)
		{
			int num = 62;
			int num2 = resultTypes.Length;
			sbyte[] array = new sbyte[num2];
			sbyte[] array2 = new sbyte[num];
			int num3 = 0;
			int num4 = 0;
			int num5 = 0;
			sbyte b = paragraphEmbeddingLevel;
			sbyte b2 = paragraphEmbeddingLevel;
			for (int i = 0; i < num2; i++)
			{
				array[i] = b2;
				sbyte b3 = resultTypes[i];
				sbyte b4 = b3;
				switch (b4)
				{
				case 1:
				case 2:
				case 5:
				case 6:
					if (num5 == 0)
					{
						sbyte b5;
						if (b3 == 5 || b3 == 6)
						{
							b5 = (b + 1 | 1);
						}
						else
						{
							b5 = (b + 2 & -2);
						}
						if ((int)b5 < num)
						{
							array2[num3] = b2;
							num3++;
							b = b5;
							if (b3 == 2 || b3 == 6)
							{
								b2 = (sbyte)((byte)b5 | 128);
							}
							else
							{
								b2 = b5;
							}
							array[i] = b2;
							break;
						}
						if (b == 60)
						{
							num4++;
							break;
						}
					}
					num5++;
					break;
				case 3:
				case 4:
					break;
				case 7:
					if (num5 > 0)
					{
						num5--;
					}
					else if (num4 > 0 && b != 61)
					{
						num4--;
					}
					else if (num3 > 0)
					{
						num3--;
						b2 = array2[num3];
						b = (b2 & sbyte.MaxValue);
					}
					break;
				default:
					if (b4 == 15)
					{
						num3 = 0;
						num5 = 0;
						num4 = 0;
						b = paragraphEmbeddingLevel;
						b2 = paragraphEmbeddingLevel;
						array[i] = paragraphEmbeddingLevel;
					}
					break;
				}
			}
			return array;
		}

		// Token: 0x06000DC1 RID: 3521 RVA: 0x0004B12C File Offset: 0x0004A12C
		private void ResolveWeakTypes(int start, int limit, sbyte level, sbyte sor, sbyte eor)
		{
			sbyte b = sor;
			for (int i = start; i < limit; i++)
			{
				sbyte b2 = this.resultTypes[i];
				if (b2 == 13)
				{
					this.resultTypes[i] = b;
				}
				else
				{
					b = b2;
				}
			}
			for (int j = start; j < limit; j++)
			{
				if (this.resultTypes[j] == 8)
				{
					int k = j - 1;
					while (k >= start)
					{
						sbyte b3 = this.resultTypes[k];
						if (b3 == 0 || b3 == 3 || b3 == 4)
						{
							if (b3 == 4)
							{
								this.resultTypes[j] = 11;
								break;
							}
							break;
						}
						else
						{
							k--;
						}
					}
				}
			}
			for (int l = start; l < limit; l++)
			{
				if (this.resultTypes[l] == 4)
				{
					this.resultTypes[l] = 3;
				}
			}
			for (int m = start + 1; m < limit - 1; m++)
			{
				if (this.resultTypes[m] == 9 || this.resultTypes[m] == 12)
				{
					sbyte b4 = this.resultTypes[m - 1];
					sbyte b5 = this.resultTypes[m + 1];
					if (b4 == 8 && b5 == 8)
					{
						this.resultTypes[m] = 8;
					}
					else if (this.resultTypes[m] == 12 && b4 == 11 && b5 == 11)
					{
						this.resultTypes[m] = 11;
					}
				}
			}
			for (int n = start; n < limit; n++)
			{
				if (this.resultTypes[n] == 10)
				{
					int num = n;
					int num2 = this.FindRunLimit(num, limit, new sbyte[]
					{
						10
					});
					sbyte b6 = (num == start) ? sor : this.resultTypes[num - 1];
					if (b6 != 8)
					{
						b6 = ((num2 == limit) ? eor : this.resultTypes[num2]);
					}
					if (b6 == 8)
					{
						this.SetTypes(num, num2, 8);
					}
					n = num2;
				}
			}
			for (int num3 = start; num3 < limit; num3++)
			{
				sbyte b7 = this.resultTypes[num3];
				if (b7 == 9 || b7 == 10 || b7 == 12)
				{
					this.resultTypes[num3] = 18;
				}
			}
			for (int num4 = start; num4 < limit; num4++)
			{
				if (this.resultTypes[num4] == 8)
				{
					sbyte b8 = sor;
					for (int num5 = num4 - 1; num5 >= start; num5--)
					{
						sbyte b9 = this.resultTypes[num5];
						if (b9 == 0 || b9 == 3)
						{
							b8 = b9;
							break;
						}
					}
					if (b8 == 0)
					{
						this.resultTypes[num4] = 0;
					}
				}
			}
		}

		// Token: 0x06000DC2 RID: 3522 RVA: 0x0004B374 File Offset: 0x0004A374
		private void ResolveNeutralTypes(int start, int limit, sbyte level, sbyte sor, sbyte eor)
		{
			for (int i = start; i < limit; i++)
			{
				sbyte b = this.resultTypes[i];
				if (b == 17 || b == 18 || b == 15 || b == 16)
				{
					int num = i;
					int num2 = this.FindRunLimit(num, limit, new sbyte[]
					{
						15,
						16,
						17,
						18
					});
					sbyte b2;
					if (num == start)
					{
						b2 = sor;
					}
					else
					{
						b2 = this.resultTypes[num - 1];
						if (b2 != 0 && b2 != 3)
						{
							if (b2 == 11)
							{
								b2 = 3;
							}
							else if (b2 == 8)
							{
								b2 = 3;
							}
						}
					}
					sbyte b3;
					if (num2 == limit)
					{
						b3 = eor;
					}
					else
					{
						b3 = this.resultTypes[num2];
						if (b3 != 0 && b3 != 3)
						{
							if (b3 == 11)
							{
								b3 = 3;
							}
							else if (b3 == 8)
							{
								b3 = 3;
							}
						}
					}
					sbyte newType;
					if (b2 == b3)
					{
						newType = b2;
					}
					else
					{
						newType = BidiOrder.TypeForLevel((int)level);
					}
					this.SetTypes(num, num2, newType);
					i = num2;
				}
			}
		}

		// Token: 0x06000DC3 RID: 3523 RVA: 0x0004B454 File Offset: 0x0004A454
		private void ResolveImplicitLevels(int start, int limit, sbyte level, sbyte sor, sbyte eor)
		{
			if ((level & 1) == 0)
			{
				for (int i = start; i < limit; i++)
				{
					sbyte b = this.resultTypes[i];
					if (b != 0)
					{
						if (b == 3)
						{
							sbyte[] array = this.resultLevels;
							int num = i;
							array[num] += 1;
						}
						else
						{
							sbyte[] array2 = this.resultLevels;
							int num2 = i;
							array2[num2] += 2;
						}
					}
				}
				return;
			}
			for (int j = start; j < limit; j++)
			{
				sbyte b2 = this.resultTypes[j];
				if (b2 != 3)
				{
					sbyte[] array3 = this.resultLevels;
					int num3 = j;
					array3[num3] += 1;
				}
			}
		}

		// Token: 0x06000DC4 RID: 3524 RVA: 0x0004B4EC File Offset: 0x0004A4EC
		public byte[] GetLevels()
		{
			return this.GetLevels(new int[]
			{
				this.textLength
			});
		}

		// Token: 0x06000DC5 RID: 3525 RVA: 0x0004B510 File Offset: 0x0004A510
		public byte[] GetLevels(int[] linebreaks)
		{
			BidiOrder.ValidateLineBreaks(linebreaks, this.textLength);
			byte[] array = new byte[this.resultLevels.Length];
			for (int i = 0; i < this.resultLevels.Length; i++)
			{
				array[i] = (byte)this.resultLevels[i];
			}
			for (int j = 0; j < array.Length; j++)
			{
				sbyte b = this.initialTypes[j];
				if (b == 15 || b == 16)
				{
					array[j] = (byte)this.paragraphEmbeddingLevel;
					int num = j - 1;
					while (num >= 0 && BidiOrder.IsWhitespace(this.initialTypes[num]))
					{
						array[num] = (byte)this.paragraphEmbeddingLevel;
						num--;
					}
				}
			}
			int num2 = 0;
			foreach (int num3 in linebreaks)
			{
				int num4 = num3 - 1;
				while (num4 >= num2 && BidiOrder.IsWhitespace(this.initialTypes[num4]))
				{
					array[num4] = (byte)this.paragraphEmbeddingLevel;
					num4--;
				}
				num2 = num3;
			}
			return array;
		}

		// Token: 0x06000DC6 RID: 3526 RVA: 0x0004B600 File Offset: 0x0004A600
		private static int[] ComputeMultilineReordering(sbyte[] levels, int[] linebreaks)
		{
			int[] array = new int[levels.Length];
			int num = 0;
			foreach (int num2 in linebreaks)
			{
				sbyte[] array2 = new sbyte[num2 - num];
				Array.Copy(levels, num, array2, 0, array2.Length);
				int[] array3 = BidiOrder.ComputeReordering(array2);
				for (int j = 0; j < array3.Length; j++)
				{
					array[num + j] = array3[j] + num;
				}
				num = num2;
			}
			return array;
		}

		// Token: 0x06000DC7 RID: 3527 RVA: 0x0004B670 File Offset: 0x0004A670
		private static int[] ComputeReordering(sbyte[] levels)
		{
			int num = levels.Length;
			int[] array = new int[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = i;
			}
			sbyte b = 0;
			sbyte b2 = 63;
			for (int j = 0; j < num; j++)
			{
				sbyte b3 = levels[j];
				if (b3 > b)
				{
					b = b3;
				}
				if ((b3 & 1) != 0 && b3 < b2)
				{
					b2 = b3;
				}
			}
			for (int k = (int)b; k >= (int)b2; k--)
			{
				for (int l = 0; l < num; l++)
				{
					if ((int)levels[l] >= k)
					{
						int num2 = l;
						int num3 = l + 1;
						while (num3 < num && (int)levels[num3] >= k)
						{
							num3++;
						}
						int m = num2;
						int num4 = num3 - 1;
						while (m < num4)
						{
							int num5 = array[m];
							array[m] = array[num4];
							array[num4] = num5;
							m++;
							num4--;
						}
						l = num3;
					}
				}
			}
			return array;
		}

		// Token: 0x06000DC8 RID: 3528 RVA: 0x0004B74B File Offset: 0x0004A74B
		public sbyte GetBaseLevel()
		{
			return this.paragraphEmbeddingLevel;
		}

		// Token: 0x06000DC9 RID: 3529 RVA: 0x0004B754 File Offset: 0x0004A754
		private static bool IsWhitespace(sbyte biditype)
		{
			switch (biditype)
			{
			case 1:
			case 2:
			case 5:
			case 6:
			case 7:
				break;
			case 3:
			case 4:
				return false;
			default:
				if (biditype != 14 && biditype != 17)
				{
					return false;
				}
				break;
			}
			return true;
		}

		// Token: 0x06000DCA RID: 3530 RVA: 0x0004B794 File Offset: 0x0004A794
		private static sbyte TypeForLevel(int level)
		{
			if ((level & 1) != 0)
			{
				return 3;
			}
			return 0;
		}

		// Token: 0x06000DCB RID: 3531 RVA: 0x0004B7A0 File Offset: 0x0004A7A0
		private int FindRunLimit(int index, int limit, sbyte[] validSet)
		{
			index--;
			IL_26:
			while (++index < limit)
			{
				sbyte b = this.resultTypes[index];
				for (int i = 0; i < validSet.Length; i++)
				{
					if (b == validSet[i])
					{
						goto IL_26;
					}
				}
				return index;
			}
			return limit;
		}

		// Token: 0x06000DCC RID: 3532 RVA: 0x0004B7E0 File Offset: 0x0004A7E0
		private int FindRunStart(int index, sbyte[] validSet)
		{
			IL_23:
			while (--index >= 0)
			{
				sbyte b = this.resultTypes[index];
				for (int i = 0; i < validSet.Length; i++)
				{
					if (b == validSet[i])
					{
						goto IL_23;
					}
				}
				return index + 1;
			}
			return 0;
		}

		// Token: 0x06000DCD RID: 3533 RVA: 0x0004B81C File Offset: 0x0004A81C
		private void SetTypes(int start, int limit, sbyte newType)
		{
			for (int i = start; i < limit; i++)
			{
				this.resultTypes[i] = newType;
			}
		}

		// Token: 0x06000DCE RID: 3534 RVA: 0x0004B840 File Offset: 0x0004A840
		private void SetLevels(int start, int limit, sbyte newLevel)
		{
			for (int i = start; i < limit; i++)
			{
				this.resultLevels[i] = newLevel;
			}
		}

		// Token: 0x06000DCF RID: 3535 RVA: 0x0004B864 File Offset: 0x0004A864
		private static void ValidateTypes(sbyte[] types)
		{
			if (types == null)
			{
				throw new ArgumentException(MessageLocalization.GetComposedMessage("types.is.null"));
			}
			for (int i = 0; i < types.Length; i++)
			{
				if (types[i] < 0 || types[i] > 18)
				{
					throw new ArgumentException(MessageLocalization.GetComposedMessage("illegal.type.value.at.1.2", i, types[i]));
				}
			}
			for (int j = 0; j < types.Length - 1; j++)
			{
				if (types[j] == 15)
				{
					throw new ArgumentException(MessageLocalization.GetComposedMessage("b.type.before.end.of.paragraph.at.index.1", j));
				}
			}
		}

		// Token: 0x06000DD0 RID: 3536 RVA: 0x0004B8EA File Offset: 0x0004A8EA
		private static void ValidateParagraphEmbeddingLevel(sbyte paragraphEmbeddingLevel)
		{
			if (paragraphEmbeddingLevel != -1 && paragraphEmbeddingLevel != 0 && paragraphEmbeddingLevel != 1)
			{
				throw new ArgumentException(MessageLocalization.GetComposedMessage("illegal.paragraph.embedding.level.1", paragraphEmbeddingLevel));
			}
		}

		// Token: 0x06000DD1 RID: 3537 RVA: 0x0004B910 File Offset: 0x0004A910
		private static void ValidateLineBreaks(int[] linebreaks, int textLength)
		{
			int num = 0;
			for (int i = 0; i < linebreaks.Length; i++)
			{
				int num2 = linebreaks[i];
				if (num2 <= num)
				{
					throw new ArgumentException(MessageLocalization.GetComposedMessage("bad.linebreak.1.at.index.2", num2, i));
				}
				num = num2;
			}
			if (num != textLength)
			{
				throw new ArgumentException(MessageLocalization.GetComposedMessage("last.linebreak.must.be.at.1", textLength));
			}
		}

		// Token: 0x06000DD2 RID: 3538 RVA: 0x0004C6EC File Offset: 0x0004B6EC
		static BidiOrder()
		{
			for (int i = 0; i < BidiOrder.baseTypes.Length; i++)
			{
				int j = (int)BidiOrder.baseTypes[i];
				int num = (int)BidiOrder.baseTypes[++i];
				sbyte b = (sbyte)BidiOrder.baseTypes[++i];
				while (j <= num)
				{
					BidiOrder.rtypes[j++] = b;
				}
			}
		}

		// Token: 0x04000A0C RID: 2572
		public const sbyte L = 0;

		// Token: 0x04000A0D RID: 2573
		public const sbyte LRE = 1;

		// Token: 0x04000A0E RID: 2574
		public const sbyte LRO = 2;

		// Token: 0x04000A0F RID: 2575
		public const sbyte R = 3;

		// Token: 0x04000A10 RID: 2576
		public const sbyte AL = 4;

		// Token: 0x04000A11 RID: 2577
		public const sbyte RLE = 5;

		// Token: 0x04000A12 RID: 2578
		public const sbyte RLO = 6;

		// Token: 0x04000A13 RID: 2579
		public const sbyte PDF = 7;

		// Token: 0x04000A14 RID: 2580
		public const sbyte EN = 8;

		// Token: 0x04000A15 RID: 2581
		public const sbyte ES = 9;

		// Token: 0x04000A16 RID: 2582
		public const sbyte ET = 10;

		// Token: 0x04000A17 RID: 2583
		public const sbyte AN = 11;

		// Token: 0x04000A18 RID: 2584
		public const sbyte CS = 12;

		// Token: 0x04000A19 RID: 2585
		public const sbyte NSM = 13;

		// Token: 0x04000A1A RID: 2586
		public const sbyte BN = 14;

		// Token: 0x04000A1B RID: 2587
		public const sbyte B = 15;

		// Token: 0x04000A1C RID: 2588
		public const sbyte S = 16;

		// Token: 0x04000A1D RID: 2589
		public const sbyte WS = 17;

		// Token: 0x04000A1E RID: 2590
		public const sbyte ON = 18;

		// Token: 0x04000A1F RID: 2591
		public const sbyte TYPE_MIN = 0;

		// Token: 0x04000A20 RID: 2592
		public const sbyte TYPE_MAX = 18;

		// Token: 0x04000A21 RID: 2593
		private sbyte[] initialTypes;

		// Token: 0x04000A22 RID: 2594
		private sbyte[] embeddings;

		// Token: 0x04000A23 RID: 2595
		private sbyte paragraphEmbeddingLevel = -1;

		// Token: 0x04000A24 RID: 2596
		private int textLength;

		// Token: 0x04000A25 RID: 2597
		private sbyte[] resultTypes;

		// Token: 0x04000A26 RID: 2598
		private sbyte[] resultLevels;

		// Token: 0x04000A27 RID: 2599
		private static sbyte[] rtypes = new sbyte[65536];

		// Token: 0x04000A28 RID: 2600
		private static char[] baseTypes = new char[]
		{
			'\0',
			'\b',
			'\u000e',
			'\t',
			'\t',
			'\u0010',
			'\n',
			'\n',
			'\u000f',
			'\v',
			'\v',
			'\u0010',
			'\f',
			'\f',
			'\u0011',
			'\r',
			'\r',
			'\u000f',
			'\u000e',
			'\u001b',
			'\u000e',
			'\u001c',
			'\u001e',
			'\u000f',
			'\u001f',
			'\u001f',
			'\u0010',
			' ',
			' ',
			'\u0011',
			'!',
			'"',
			'\u0012',
			'#',
			'%',
			'\n',
			'&',
			'*',
			'\u0012',
			'+',
			'+',
			'\n',
			',',
			',',
			'\f',
			'-',
			'-',
			'\n',
			'.',
			'.',
			'\f',
			'/',
			'/',
			'\t',
			'0',
			'9',
			'\b',
			':',
			':',
			'\f',
			';',
			'@',
			'\u0012',
			'A',
			'Z',
			'\0',
			'[',
			'`',
			'\u0012',
			'a',
			'z',
			'\0',
			'{',
			'~',
			'\u0012',
			'\u007f',
			'\u0084',
			'\u000e',
			'\u0085',
			'\u0085',
			'\u000f',
			'\u0086',
			'\u009f',
			'\u000e',
			'\u00a0',
			'\u00a0',
			'\f',
			'¡',
			'¡',
			'\u0012',
			'¢',
			'¥',
			'\n',
			'¦',
			'©',
			'\u0012',
			'ª',
			'ª',
			'\0',
			'«',
			'¯',
			'\u0012',
			'°',
			'±',
			'\n',
			'²',
			'³',
			'\b',
			'´',
			'´',
			'\u0012',
			'µ',
			'µ',
			'\0',
			'¶',
			'¸',
			'\u0012',
			'¹',
			'¹',
			'\b',
			'º',
			'º',
			'\0',
			'»',
			'¿',
			'\u0012',
			'À',
			'Ö',
			'\0',
			'×',
			'×',
			'\u0012',
			'Ø',
			'ö',
			'\0',
			'÷',
			'÷',
			'\u0012',
			'ø',
			'ʸ',
			'\0',
			'ʹ',
			'ʺ',
			'\u0012',
			'ʻ',
			'ˁ',
			'\0',
			'˂',
			'ˏ',
			'\u0012',
			'ː',
			'ˑ',
			'\0',
			'˒',
			'˟',
			'\u0012',
			'ˠ',
			'ˤ',
			'\0',
			'˥',
			'˭',
			'\u0012',
			'ˮ',
			'ˮ',
			'\0',
			'˯',
			'˿',
			'\u0012',
			'̀',
			'͗',
			'\r',
			'͘',
			'͜',
			'\0',
			'͝',
			'ͯ',
			'\r',
			'Ͱ',
			'ͳ',
			'\0',
			'ʹ',
			'͵',
			'\u0012',
			'Ͷ',
			'ͽ',
			'\0',
			';',
			';',
			'\u0012',
			'Ϳ',
			'΃',
			'\0',
			'΄',
			'΅',
			'\u0012',
			'Ά',
			'Ά',
			'\0',
			'·',
			'·',
			'\u0012',
			'Έ',
			'ϵ',
			'\0',
			'϶',
			'϶',
			'\u0012',
			'Ϸ',
			'҂',
			'\0',
			'҃',
			'҆',
			'\r',
			'҇',
			'҇',
			'\0',
			'҈',
			'҉',
			'\r',
			'Ҋ',
			'։',
			'\0',
			'֊',
			'֊',
			'\u0012',
			'֋',
			'֐',
			'\0',
			'֑',
			'֡',
			'\r',
			'֢',
			'֢',
			'\0',
			'֣',
			'ֹ',
			'\r',
			'ֺ',
			'ֺ',
			'\0',
			'ֻ',
			'ֽ',
			'\r',
			'־',
			'־',
			'\u0003',
			'ֿ',
			'ֿ',
			'\r',
			'׀',
			'׀',
			'\u0003',
			'ׁ',
			'ׂ',
			'\r',
			'׃',
			'׃',
			'\u0003',
			'ׄ',
			'ׄ',
			'\r',
			'ׅ',
			'׏',
			'\0',
			'א',
			'ת',
			'\u0003',
			'׫',
			'ׯ',
			'\0',
			'װ',
			'״',
			'\u0003',
			'׵',
			'׿',
			'\0',
			'؀',
			'؃',
			'\u0004',
			'؄',
			'؋',
			'\0',
			'،',
			'،',
			'\f',
			'؍',
			'؍',
			'\u0004',
			'؎',
			'؏',
			'\u0012',
			'ؐ',
			'ؕ',
			'\r',
			'ؖ',
			'ؚ',
			'\0',
			'؛',
			'؛',
			'\u0004',
			'؜',
			'؞',
			'\0',
			'؟',
			'؟',
			'\u0004',
			'ؠ',
			'ؠ',
			'\0',
			'ء',
			'غ',
			'\u0004',
			'ػ',
			'ؿ',
			'\0',
			'ـ',
			'ي',
			'\u0004',
			'ً',
			'٘',
			'\r',
			'ٙ',
			'ٟ',
			'\0',
			'٠',
			'٩',
			'\v',
			'٪',
			'٪',
			'\n',
			'٫',
			'٬',
			'\v',
			'٭',
			'ٯ',
			'\u0004',
			'ٰ',
			'ٰ',
			'\r',
			'ٱ',
			'ە',
			'\u0004',
			'ۖ',
			'ۜ',
			'\r',
			'۝',
			'۝',
			'\u0004',
			'۞',
			'ۤ',
			'\r',
			'ۥ',
			'ۦ',
			'\u0004',
			'ۧ',
			'ۨ',
			'\r',
			'۩',
			'۩',
			'\u0012',
			'۪',
			'ۭ',
			'\r',
			'ۮ',
			'ۯ',
			'\u0004',
			'۰',
			'۹',
			'\b',
			'ۺ',
			'܍',
			'\u0004',
			'܎',
			'܎',
			'\0',
			'܏',
			'܏',
			'\u000e',
			'ܐ',
			'ܐ',
			'\u0004',
			'ܑ',
			'ܑ',
			'\r',
			'ܒ',
			'ܯ',
			'\u0004',
			'ܰ',
			'݊',
			'\r',
			'݋',
			'݌',
			'\0',
			'ݍ',
			'ݏ',
			'\u0004',
			'ݐ',
			'ݿ',
			'\0',
			'ހ',
			'ޥ',
			'\u0004',
			'ަ',
			'ް',
			'\r',
			'ޱ',
			'ޱ',
			'\u0004',
			'޲',
			'ऀ',
			'\0',
			'ँ',
			'ं',
			'\r',
			'ः',
			'ऻ',
			'\0',
			'़',
			'़',
			'\r',
			'ऽ',
			'ी',
			'\0',
			'ु',
			'ै',
			'\r',
			'ॉ',
			'ौ',
			'\0',
			'्',
			'्',
			'\r',
			'ॎ',
			'ॐ',
			'\0',
			'॑',
			'॔',
			'\r',
			'ॕ',
			'ॡ',
			'\0',
			'ॢ',
			'ॣ',
			'\r',
			'।',
			'ঀ',
			'\0',
			'ঁ',
			'ঁ',
			'\r',
			'ং',
			'঻',
			'\0',
			'়',
			'়',
			'\r',
			'ঽ',
			'ী',
			'\0',
			'ু',
			'ৄ',
			'\r',
			'৅',
			'ৌ',
			'\0',
			'্',
			'্',
			'\r',
			'ৎ',
			'ৡ',
			'\0',
			'ৢ',
			'ৣ',
			'\r',
			'৤',
			'ৱ',
			'\0',
			'৲',
			'৳',
			'\n',
			'৴',
			'਀',
			'\0',
			'ਁ',
			'ਂ',
			'\r',
			'ਃ',
			'਻',
			'\0',
			'਼',
			'਼',
			'\r',
			'਽',
			'ੀ',
			'\0',
			'ੁ',
			'ੂ',
			'\r',
			'੃',
			'੆',
			'\0',
			'ੇ',
			'ੈ',
			'\r',
			'੉',
			'੊',
			'\0',
			'ੋ',
			'੍',
			'\r',
			'੎',
			'੯',
			'\0',
			'ੰ',
			'ੱ',
			'\r',
			'ੲ',
			'઀',
			'\0',
			'ઁ',
			'ં',
			'\r',
			'ઃ',
			'઻',
			'\0',
			'઼',
			'઼',
			'\r',
			'ઽ',
			'ી',
			'\0',
			'ુ',
			'ૅ',
			'\r',
			'૆',
			'૆',
			'\0',
			'ે',
			'ૈ',
			'\r',
			'ૉ',
			'ૌ',
			'\0',
			'્',
			'્',
			'\r',
			'૎',
			'ૡ',
			'\0',
			'ૢ',
			'ૣ',
			'\r',
			'૤',
			'૰',
			'\0',
			'૱',
			'૱',
			'\n',
			'૲',
			'଀',
			'\0',
			'ଁ',
			'ଁ',
			'\r',
			'ଂ',
			'଻',
			'\0',
			'଼',
			'଼',
			'\r',
			'ଽ',
			'ା',
			'\0',
			'ି',
			'ି',
			'\r',
			'ୀ',
			'ୀ',
			'\0',
			'ୁ',
			'ୃ',
			'\r',
			'ୄ',
			'ୌ',
			'\0',
			'୍',
			'୍',
			'\r',
			'୎',
			'୕',
			'\0',
			'ୖ',
			'ୖ',
			'\r',
			'ୗ',
			'஁',
			'\0',
			'ஂ',
			'ஂ',
			'\r',
			'ஃ',
			'ி',
			'\0',
			'ீ',
			'ீ',
			'\r',
			'ு',
			'ௌ',
			'\0',
			'்',
			'்',
			'\r',
			'௎',
			'௲',
			'\0',
			'௳',
			'௸',
			'\u0012',
			'௹',
			'௹',
			'\n',
			'௺',
			'௺',
			'\u0012',
			'௻',
			'ఽ',
			'\0',
			'ా',
			'ీ',
			'\r',
			'ు',
			'౅',
			'\0',
			'ె',
			'ై',
			'\r',
			'౉',
			'౉',
			'\0',
			'ొ',
			'్',
			'\r',
			'౎',
			'౔',
			'\0',
			'ౕ',
			'ౖ',
			'\r',
			'౗',
			'಻',
			'\0',
			'಼',
			'಼',
			'\r',
			'ಽ',
			'ೋ',
			'\0',
			'ೌ',
			'್',
			'\r',
			'೎',
			'ീ',
			'\0',
			'ു',
			'ൃ',
			'\r',
			'ൄ',
			'ൌ',
			'\0',
			'്',
			'്',
			'\r',
			'ൎ',
			'෉',
			'\0',
			'්',
			'්',
			'\r',
			'෋',
			'ෑ',
			'\0',
			'ි',
			'ු',
			'\r',
			'෕',
			'෕',
			'\0',
			'ූ',
			'ූ',
			'\r',
			'෗',
			'ะ',
			'\0',
			'ั',
			'ั',
			'\r',
			'า',
			'ำ',
			'\0',
			'ิ',
			'ฺ',
			'\r',
			'฻',
			'฾',
			'\0',
			'฿',
			'฿',
			'\n',
			'เ',
			'ๆ',
			'\0',
			'็',
			'๎',
			'\r',
			'๏',
			'ະ',
			'\0',
			'ັ',
			'ັ',
			'\r',
			'າ',
			'ຳ',
			'\0',
			'ິ',
			'ູ',
			'\r',
			'຺',
			'຺',
			'\0',
			'ົ',
			'ຼ',
			'\r',
			'ຽ',
			'໇',
			'\0',
			'່',
			'ໍ',
			'\r',
			'໎',
			'༗',
			'\0',
			'༘',
			'༙',
			'\r',
			'༚',
			'༴',
			'\0',
			'༵',
			'༵',
			'\r',
			'༶',
			'༶',
			'\0',
			'༷',
			'༷',
			'\r',
			'༸',
			'༸',
			'\0',
			'༹',
			'༹',
			'\r',
			'༺',
			'༽',
			'\u0012',
			'༾',
			'཰',
			'\0',
			'ཱ',
			'ཾ',
			'\r',
			'ཿ',
			'ཿ',
			'\0',
			'ྀ',
			'྄',
			'\r',
			'྅',
			'྅',
			'\0',
			'྆',
			'྇',
			'\r',
			'ྈ',
			'ྏ',
			'\0',
			'ྐ',
			'ྗ',
			'\r',
			'྘',
			'྘',
			'\0',
			'ྙ',
			'ྼ',
			'\r',
			'྽',
			'࿅',
			'\0',
			'࿆',
			'࿆',
			'\r',
			'࿇',
			'ာ',
			'\0',
			'ိ',
			'ူ',
			'\r',
			'ေ',
			'ေ',
			'\0',
			'ဲ',
			'ဲ',
			'\r',
			'ဳ',
			'ဵ',
			'\0',
			'ံ',
			'့',
			'\r',
			'း',
			'း',
			'\0',
			'္',
			'္',
			'\r',
			'်',
			'ၗ',
			'\0',
			'ၘ',
			'ၙ',
			'\r',
			'ၚ',
			'ᙿ',
			'\0',
			'\u1680',
			'\u1680',
			'\u0011',
			'ᚁ',
			'ᚚ',
			'\0',
			'᚛',
			'᚜',
			'\u0012',
			'᚝',
			'ᜑ',
			'\0',
			'ᜒ',
			'᜔',
			'\r',
			'᜕',
			'ᜱ',
			'\0',
			'ᜲ',
			'᜴',
			'\r',
			'᜵',
			'ᝑ',
			'\0',
			'ᝒ',
			'ᝓ',
			'\r',
			'᝔',
			'᝱',
			'\0',
			'ᝲ',
			'ᝳ',
			'\r',
			'᝴',
			'ា',
			'\0',
			'ិ',
			'ួ',
			'\r',
			'ើ',
			'ៅ',
			'\0',
			'ំ',
			'ំ',
			'\r',
			'ះ',
			'ៈ',
			'\0',
			'៉',
			'៓',
			'\r',
			'។',
			'៚',
			'\0',
			'៛',
			'៛',
			'\n',
			'ៜ',
			'ៜ',
			'\0',
			'៝',
			'៝',
			'\r',
			'៞',
			'៯',
			'\0',
			'៰',
			'៹',
			'\u0012',
			'៺',
			'៿',
			'\0',
			'᠀',
			'᠊',
			'\u0012',
			'᠋',
			'᠍',
			'\r',
			'᠎',
			'᠎',
			'\u0011',
			'᠏',
			'ᢨ',
			'\0',
			'ᢩ',
			'ᢩ',
			'\r',
			'ᢪ',
			'᤟',
			'\0',
			'ᤠ',
			'ᤢ',
			'\r',
			'ᤣ',
			'ᤦ',
			'\0',
			'ᤧ',
			'ᤫ',
			'\r',
			'᤬',
			'ᤱ',
			'\0',
			'ᤲ',
			'ᤲ',
			'\r',
			'ᤳ',
			'ᤸ',
			'\0',
			'᤹',
			'᤻',
			'\r',
			'᤼',
			'᤿',
			'\0',
			'᥀',
			'᥀',
			'\u0012',
			'᥁',
			'᥃',
			'\0',
			'᥄',
			'᥅',
			'\u0012',
			'᥆',
			'᧟',
			'\0',
			'᧠',
			'᧿',
			'\u0012',
			'ᨀ',
			'ᾼ',
			'\0',
			'᾽',
			'᾽',
			'\u0012',
			'ι',
			'ι',
			'\0',
			'᾿',
			'῁',
			'\u0012',
			'ῂ',
			'ῌ',
			'\0',
			'῍',
			'῏',
			'\u0012',
			'ῐ',
			'῜',
			'\0',
			'῝',
			'῟',
			'\u0012',
			'ῠ',
			'Ῥ',
			'\0',
			'῭',
			'`',
			'\u0012',
			'῰',
			'ῼ',
			'\0',
			'´',
			'῾',
			'\u0012',
			'῿',
			'῿',
			'\0',
			'\u2000',
			'\u200a',
			'\u0011',
			'​',
			'‍',
			'\u000e',
			'‎',
			'‎',
			'\0',
			'‏',
			'‏',
			'\u0003',
			'‐',
			'‧',
			'\u0012',
			'\u2028',
			'\u2028',
			'\u0011',
			'\u2029',
			'\u2029',
			'\u000f',
			'‪',
			'‪',
			'\u0001',
			'‫',
			'‫',
			'\u0005',
			'‬',
			'‬',
			'\a',
			'‭',
			'‭',
			'\u0002',
			'‮',
			'‮',
			'\u0006',
			'\u202f',
			'\u202f',
			'\u0011',
			'‰',
			'‴',
			'\n',
			'‵',
			'⁔',
			'\u0012',
			'⁕',
			'⁖',
			'\0',
			'⁗',
			'⁗',
			'\u0012',
			'⁘',
			'⁞',
			'\0',
			'\u205f',
			'\u205f',
			'\u0011',
			'⁠',
			'⁣',
			'\u000e',
			'⁤',
			'⁩',
			'\0',
			'⁪',
			'⁯',
			'\u000e',
			'⁰',
			'⁰',
			'\b',
			'ⁱ',
			'⁳',
			'\0',
			'⁴',
			'⁹',
			'\b',
			'⁺',
			'⁻',
			'\n',
			'⁼',
			'⁾',
			'\u0012',
			'ⁿ',
			'ⁿ',
			'\0',
			'₀',
			'₉',
			'\b',
			'₊',
			'₋',
			'\n',
			'₌',
			'₎',
			'\u0012',
			'₏',
			'₟',
			'\0',
			'₠',
			'₱',
			'\n',
			'₲',
			'⃏',
			'\0',
			'⃐',
			'⃪',
			'\r',
			'⃫',
			'⃿',
			'\0',
			'℀',
			'℁',
			'\u0012',
			'ℂ',
			'ℂ',
			'\0',
			'℃',
			'℆',
			'\u0012',
			'ℇ',
			'ℇ',
			'\0',
			'℈',
			'℉',
			'\u0012',
			'ℊ',
			'ℓ',
			'\0',
			'℔',
			'℔',
			'\u0012',
			'ℕ',
			'ℕ',
			'\0',
			'№',
			'℘',
			'\u0012',
			'ℙ',
			'ℝ',
			'\0',
			'℞',
			'℣',
			'\u0012',
			'ℤ',
			'ℤ',
			'\0',
			'℥',
			'℥',
			'\u0012',
			'Ω',
			'Ω',
			'\0',
			'℧',
			'℧',
			'\u0012',
			'ℨ',
			'ℨ',
			'\0',
			'℩',
			'℩',
			'\u0012',
			'K',
			'ℭ',
			'\0',
			'℮',
			'℮',
			'\n',
			'ℯ',
			'ℱ',
			'\0',
			'Ⅎ',
			'Ⅎ',
			'\u0012',
			'ℳ',
			'ℹ',
			'\0',
			'℺',
			'℻',
			'\u0012',
			'ℼ',
			'ℿ',
			'\0',
			'⅀',
			'⅄',
			'\u0012',
			'ⅅ',
			'ⅉ',
			'\0',
			'⅊',
			'⅋',
			'\u0012',
			'⅌',
			'⅒',
			'\0',
			'⅓',
			'⅟',
			'\u0012',
			'Ⅰ',
			'↏',
			'\0',
			'←',
			'∑',
			'\u0012',
			'−',
			'∓',
			'\n',
			'∔',
			'⌵',
			'\u0012',
			'⌶',
			'⍺',
			'\0',
			'⍻',
			'⎔',
			'\u0012',
			'⎕',
			'⎕',
			'\0',
			'⎖',
			'⏐',
			'\u0012',
			'⏑',
			'⏿',
			'\0',
			'␀',
			'␦',
			'\u0012',
			'␧',
			'␿',
			'\0',
			'⑀',
			'⑊',
			'\u0012',
			'⑋',
			'⑟',
			'\0',
			'①',
			'⒛',
			'\b',
			'⒜',
			'ⓩ',
			'\0',
			'⓪',
			'⓪',
			'\b',
			'⓫',
			'☗',
			'\u0012',
			'☘',
			'☘',
			'\0',
			'☙',
			'♽',
			'\u0012',
			'♾',
			'♿',
			'\0',
			'⚀',
			'⚑',
			'\u0012',
			'⚒',
			'⚟',
			'\0',
			'⚠',
			'⚡',
			'\u0012',
			'⚢',
			'✀',
			'\0',
			'✁',
			'✄',
			'\u0012',
			'✅',
			'✅',
			'\0',
			'✆',
			'✉',
			'\u0012',
			'✊',
			'✋',
			'\0',
			'✌',
			'✧',
			'\u0012',
			'✨',
			'✨',
			'\0',
			'✩',
			'❋',
			'\u0012',
			'❌',
			'❌',
			'\0',
			'❍',
			'❍',
			'\u0012',
			'❎',
			'❎',
			'\0',
			'❏',
			'❒',
			'\u0012',
			'❓',
			'❕',
			'\0',
			'❖',
			'❖',
			'\u0012',
			'❗',
			'❗',
			'\0',
			'❘',
			'❞',
			'\u0012',
			'❟',
			'❠',
			'\0',
			'❡',
			'➔',
			'\u0012',
			'➕',
			'➗',
			'\0',
			'➘',
			'➯',
			'\u0012',
			'➰',
			'➰',
			'\0',
			'➱',
			'➾',
			'\u0012',
			'➿',
			'⟏',
			'\0',
			'⟐',
			'⟫',
			'\u0012',
			'⟬',
			'⟯',
			'\0',
			'⟰',
			'⬍',
			'\u0012',
			'⬎',
			'⹿',
			'\0',
			'⺀',
			'⺙',
			'\u0012',
			'⺚',
			'⺚',
			'\0',
			'⺛',
			'⻳',
			'\u0012',
			'⻴',
			'⻿',
			'\0',
			'⼀',
			'⿕',
			'\u0012',
			'⿖',
			'⿯',
			'\0',
			'⿰',
			'⿻',
			'\u0012',
			'⿼',
			'⿿',
			'\0',
			'\u3000',
			'\u3000',
			'\u0011',
			'、',
			'〄',
			'\u0012',
			'々',
			'〇',
			'\0',
			'〈',
			'〠',
			'\u0012',
			'〡',
			'〩',
			'\0',
			'〪',
			'〯',
			'\r',
			'〰',
			'〰',
			'\u0012',
			'〱',
			'〵',
			'\0',
			'〶',
			'〷',
			'\u0012',
			'〸',
			'〼',
			'\0',
			'〽',
			'〿',
			'\u0012',
			'぀',
			'゘',
			'\0',
			'゙',
			'゚',
			'\r',
			'゛',
			'゜',
			'\u0012',
			'ゝ',
			'ゟ',
			'\0',
			'゠',
			'゠',
			'\u0012',
			'ァ',
			'ヺ',
			'\0',
			'・',
			'・',
			'\u0012',
			'ー',
			'㈜',
			'\0',
			'㈝',
			'㈞',
			'\u0012',
			'㈟',
			'㉏',
			'\0',
			'㉐',
			'㉟',
			'\u0012',
			'㉠',
			'㉻',
			'\0',
			'㉼',
			'㉽',
			'\u0012',
			'㉾',
			'㊰',
			'\0',
			'㊱',
			'㊿',
			'\u0012',
			'㋀',
			'㋋',
			'\0',
			'㋌',
			'㋏',
			'\u0012',
			'㋐',
			'㍶',
			'\0',
			'㍷',
			'㍺',
			'\u0012',
			'㍻',
			'㏝',
			'\0',
			'㏞',
			'㏟',
			'\u0012',
			'㏠',
			'㏾',
			'\0',
			'㏿',
			'㏿',
			'\u0012',
			'㐀',
			'䶿',
			'\0',
			'䷀',
			'䷿',
			'\u0012',
			'一',
			'꒏',
			'\0',
			'꒐',
			'꓆',
			'\u0012',
			'꓇',
			'﬜',
			'\0',
			'יִ',
			'יִ',
			'\u0003',
			'ﬞ',
			'ﬞ',
			'\r',
			'ײַ',
			'ﬨ',
			'\u0003',
			'﬩',
			'﬩',
			'\n',
			'שׁ',
			'זּ',
			'\u0003',
			'﬷',
			'﬷',
			'\0',
			'טּ',
			'לּ',
			'\u0003',
			'﬽',
			'﬽',
			'\0',
			'מּ',
			'מּ',
			'\u0003',
			'﬿',
			'﬿',
			'\0',
			'נּ',
			'סּ',
			'\u0003',
			'﭂',
			'﭂',
			'\0',
			'ףּ',
			'פּ',
			'\u0003',
			'﭅',
			'﭅',
			'\0',
			'צּ',
			'ﭏ',
			'\u0003',
			'ﭐ',
			'ﮱ',
			'\u0004',
			'﮲',
			'﯒',
			'\0',
			'ﯓ',
			'ﴽ',
			'\u0004',
			'﴾',
			'﴿',
			'\u0012',
			'﵀',
			'﵏',
			'\0',
			'ﵐ',
			'ﶏ',
			'\u0004',
			'﶐',
			'﶑',
			'\0',
			'ﶒ',
			'ﷇ',
			'\u0004',
			'﷈',
			'﷯',
			'\0',
			'ﷰ',
			'﷼',
			'\u0004',
			'﷽',
			'﷽',
			'\u0012',
			'﷾',
			'﷿',
			'\0',
			'︀',
			'️',
			'\r',
			'︐',
			'︟',
			'\0',
			'︠',
			'︣',
			'\r',
			'︤',
			'︯',
			'\0',
			'︰',
			'﹏',
			'\u0012',
			'﹐',
			'﹐',
			'\f',
			'﹑',
			'﹑',
			'\u0012',
			'﹒',
			'﹒',
			'\f',
			'﹓',
			'﹓',
			'\0',
			'﹔',
			'﹔',
			'\u0012',
			'﹕',
			'﹕',
			'\f',
			'﹖',
			'﹞',
			'\u0012',
			'﹟',
			'﹟',
			'\n',
			'﹠',
			'﹡',
			'\u0012',
			'﹢',
			'﹣',
			'\n',
			'﹤',
			'﹦',
			'\u0012',
			'﹧',
			'﹧',
			'\0',
			'﹨',
			'﹨',
			'\u0012',
			'﹩',
			'﹪',
			'\n',
			'﹫',
			'﹫',
			'\u0012',
			'﹬',
			'﹯',
			'\0',
			'ﹰ',
			'ﹴ',
			'\u0004',
			'﹵',
			'﹵',
			'\0',
			'ﹶ',
			'ﻼ',
			'\u0004',
			'﻽',
			'﻾',
			'\0',
			'﻿',
			'﻿',
			'\u000e',
			'＀',
			'＀',
			'\0',
			'！',
			'＂',
			'\u0012',
			'＃',
			'％',
			'\n',
			'＆',
			'＊',
			'\u0012',
			'＋',
			'＋',
			'\n',
			'，',
			'，',
			'\f',
			'－',
			'－',
			'\n',
			'．',
			'．',
			'\f',
			'／',
			'／',
			'\t',
			'０',
			'９',
			'\b',
			'：',
			'：',
			'\f',
			'；',
			'＠',
			'\u0012',
			'Ａ',
			'Ｚ',
			'\0',
			'［',
			'｀',
			'\u0012',
			'ａ',
			'ｚ',
			'\0',
			'｛',
			'･',
			'\u0012',
			'ｦ',
			'￟',
			'\0',
			'￠',
			'￡',
			'\n',
			'￢',
			'￤',
			'\u0012',
			'￥',
			'￦',
			'\n',
			'￧',
			'￧',
			'\0',
			'￨',
			'￮',
			'\u0012',
			'￯',
			'￸',
			'\0',
			'￹',
			'￻',
			'\u000e',
			'￼',
			'�',
			'\u0012',
			'￾',
			char.MaxValue,
			'\0'
		};
	}
}
