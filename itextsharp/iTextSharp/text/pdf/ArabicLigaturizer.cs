using System;
using System.Text;

namespace iTextSharp.text.pdf
{
	// Token: 0x02000288 RID: 648
	public class ArabicLigaturizer
	{
		// Token: 0x0600186B RID: 6251 RVA: 0x0008D88C File Offset: 0x0008C88C
		private static bool IsVowel(char s)
		{
			return (s >= 'ً' && s <= 'ٕ') || s == 'ٰ';
		}

		// Token: 0x0600186C RID: 6252 RVA: 0x0008D8A8 File Offset: 0x0008C8A8
		private static char Charshape(char s, int which)
		{
			if (s >= 'ء' && s <= 'ۓ')
			{
				int i = 0;
				int num = ArabicLigaturizer.chartable.Length - 1;
				while (i <= num)
				{
					int num2 = (i + num) / 2;
					if (s == ArabicLigaturizer.chartable[num2][0])
					{
						return ArabicLigaturizer.chartable[num2][which + 1];
					}
					if (s < ArabicLigaturizer.chartable[num2][0])
					{
						num = num2 - 1;
					}
					else
					{
						i = num2 + 1;
					}
				}
			}
			else if (s >= 'ﻵ' && s <= 'ﻻ')
			{
				return (char)((int)s + which);
			}
			return s;
		}

		// Token: 0x0600186D RID: 6253 RVA: 0x0008D924 File Offset: 0x0008C924
		private static int Shapecount(char s)
		{
			if (s >= 'ء' && s <= 'ۓ' && !ArabicLigaturizer.IsVowel(s))
			{
				int i = 0;
				int num = ArabicLigaturizer.chartable.Length - 1;
				while (i <= num)
				{
					int num2 = (i + num) / 2;
					if (s == ArabicLigaturizer.chartable[num2][0])
					{
						return ArabicLigaturizer.chartable[num2].Length - 1;
					}
					if (s < ArabicLigaturizer.chartable[num2][0])
					{
						num = num2 - 1;
					}
					else
					{
						i = num2 + 1;
					}
				}
			}
			else if (s == '‍')
			{
				return 4;
			}
			return 1;
		}

		// Token: 0x0600186E RID: 6254 RVA: 0x0008D99C File Offset: 0x0008C99C
		private static int Ligature(char newchar, ArabicLigaturizer.Charstruct oldchar)
		{
			int num = 0;
			if (oldchar.basechar == '\0')
			{
				return 0;
			}
			if (ArabicLigaturizer.IsVowel(newchar))
			{
				num = 1;
				if (oldchar.vowel != '\0' && newchar != 'ّ')
				{
					num = 2;
				}
				switch (newchar)
				{
				case 'ّ':
					if (oldchar.mark1 == '\0')
					{
						oldchar.mark1 = 'ّ';
						goto IL_16C;
					}
					return 0;
				case 'ٓ':
				{
					char basechar = oldchar.basechar;
					if (basechar == 'ا')
					{
						oldchar.basechar = 'آ';
						num = 2;
						goto IL_16C;
					}
					goto IL_16C;
				}
				case 'ٔ':
				{
					char basechar2 = oldchar.basechar;
					if (basechar2 <= 'ي')
					{
						if (basechar2 == 'ا')
						{
							oldchar.basechar = 'أ';
							num = 2;
							goto IL_16C;
						}
						switch (basechar2)
						{
						case 'و':
							oldchar.basechar = 'ؤ';
							num = 2;
							goto IL_16C;
						case 'ى':
						case 'ي':
							break;
						default:
							goto IL_138;
						}
					}
					else if (basechar2 != 'ی')
					{
						if (basechar2 != 'ﻻ')
						{
							goto IL_138;
						}
						oldchar.basechar = 'ﻷ';
						num = 2;
						goto IL_16C;
					}
					oldchar.basechar = 'ئ';
					num = 2;
					goto IL_16C;
					IL_138:
					oldchar.mark1 = 'ٔ';
					goto IL_16C;
				}
				case 'ٕ':
				{
					char basechar3 = oldchar.basechar;
					if (basechar3 == 'ا')
					{
						oldchar.basechar = 'إ';
						num = 2;
						goto IL_16C;
					}
					if (basechar3 != 'ﻻ')
					{
						oldchar.mark1 = 'ٕ';
						goto IL_16C;
					}
					oldchar.basechar = 'ﻹ';
					num = 2;
					goto IL_16C;
				}
				}
				oldchar.vowel = newchar;
				IL_16C:
				if (num == 1)
				{
					oldchar.lignum++;
				}
				return num;
			}
			if (oldchar.vowel != '\0')
			{
				return 0;
			}
			char basechar4 = oldchar.basechar;
			if (basechar4 != '\0')
			{
				if (basechar4 == 'ل')
				{
					switch (newchar)
					{
					case 'آ':
						oldchar.basechar = 'ﻵ';
						oldchar.numshapes = 2;
						num = 3;
						break;
					case 'أ':
						oldchar.basechar = 'ﻷ';
						oldchar.numshapes = 2;
						num = 3;
						break;
					case 'إ':
						oldchar.basechar = 'ﻹ';
						oldchar.numshapes = 2;
						num = 3;
						break;
					case 'ا':
						oldchar.basechar = 'ﻻ';
						oldchar.numshapes = 2;
						num = 3;
						break;
					}
				}
			}
			else
			{
				oldchar.basechar = newchar;
				oldchar.numshapes = ArabicLigaturizer.Shapecount(newchar);
				num = 1;
			}
			return num;
		}

		// Token: 0x0600186F RID: 6255 RVA: 0x0008DBE8 File Offset: 0x0008CBE8
		private static void Copycstostring(StringBuilder str, ArabicLigaturizer.Charstruct s, int level)
		{
			if (s.basechar == '\0')
			{
				return;
			}
			str.Append(s.basechar);
			s.lignum--;
			if (s.mark1 != '\0')
			{
				if ((level & 1) == 0)
				{
					str.Append(s.mark1);
					s.lignum--;
				}
				else
				{
					s.lignum--;
				}
			}
			if (s.vowel != '\0')
			{
				if ((level & 1) == 0)
				{
					str.Append(s.vowel);
					s.lignum--;
					return;
				}
				s.lignum--;
			}
		}

		// Token: 0x06001870 RID: 6256 RVA: 0x0008DC88 File Offset: 0x0008CC88
		internal static void Doublelig(StringBuilder str, int level)
		{
			int length;
			int num = length = str.Length;
			int num2 = 0;
			int i = 1;
			while (i < length)
			{
				char c = '\0';
				if ((level & 4) != 0)
				{
					switch (str[num2])
					{
					case 'َ':
						if (str[i] == 'ّ')
						{
							c = 'ﱠ';
						}
						break;
					case 'ُ':
						if (str[i] == 'ّ')
						{
							c = 'ﱡ';
						}
						break;
					case 'ِ':
						if (str[i] == 'ّ')
						{
							c = 'ﱢ';
						}
						break;
					case 'ّ':
						switch (str[i])
						{
						case 'ٌ':
							c = 'ﱞ';
							break;
						case 'ٍ':
							c = 'ﱟ';
							break;
						case 'َ':
							c = 'ﱠ';
							break;
						case 'ُ':
							c = 'ﱡ';
							break;
						case 'ِ':
							c = 'ﱢ';
							break;
						}
						break;
					}
				}
				if ((level & 8) != 0)
				{
					char c2 = str[num2];
					if (c2 <= 'ﻓ')
					{
						if (c2 != 'ﺑ')
						{
							if (c2 != 'ﺗ')
							{
								if (c2 == 'ﻓ')
								{
									char c3 = str[i];
									if (c3 == 'ﻲ')
									{
										c = 'ﰲ';
									}
								}
							}
							else
							{
								char c4 = str[i];
								if (c4 != 'ﺠ')
								{
									if (c4 != 'ﺤ')
									{
										if (c4 == 'ﺨ')
										{
											c = 'ﲣ';
										}
									}
									else
									{
										c = 'ﲢ';
									}
								}
								else
								{
									c = 'ﲡ';
								}
							}
						}
						else
						{
							char c5 = str[i];
							if (c5 != 'ﺠ')
							{
								if (c5 != 'ﺤ')
								{
									if (c5 == 'ﺨ')
									{
										c = 'ﲞ';
									}
								}
								else
								{
									c = 'ﲝ';
								}
							}
							else
							{
								c = 'ﲜ';
							}
						}
					}
					else if (c2 != 'ﻟ')
					{
						if (c2 != 'ﻣ')
						{
							switch (c2)
							{
							case 'ﻧ':
							{
								char c6 = str[i];
								if (c6 != 'ﺠ')
								{
									if (c6 != 'ﺤ')
									{
										if (c6 == 'ﺨ')
										{
											c = 'ﳔ';
										}
									}
									else
									{
										c = 'ﳓ';
									}
								}
								else
								{
									c = 'ﳒ';
								}
								break;
							}
							case 'ﻨ':
								switch (str[i])
								{
								case 'ﺮ':
									c = 'ﲊ';
									break;
								case 'ﺰ':
									c = 'ﲋ';
									break;
								}
								break;
							}
						}
						else
						{
							char c7 = str[i];
							if (c7 <= 'ﺤ')
							{
								if (c7 != 'ﺠ')
								{
									if (c7 == 'ﺤ')
									{
										c = 'ﳏ';
									}
								}
								else
								{
									c = 'ﳎ';
								}
							}
							else if (c7 != 'ﺨ')
							{
								if (c7 == 'ﻤ')
								{
									c = 'ﳑ';
								}
							}
							else
							{
								c = 'ﳐ';
							}
						}
					}
					else
					{
						char c8 = str[i];
						switch (c8)
						{
						case 'ﺞ':
							c = 'ﰿ';
							break;
						case 'ﺟ':
						case 'ﺡ':
						case 'ﺣ':
						case 'ﺥ':
						case 'ﺧ':
							break;
						case 'ﺠ':
							c = 'ﳉ';
							break;
						case 'ﺢ':
							c = 'ﱀ';
							break;
						case 'ﺤ':
							c = 'ﳊ';
							break;
						case 'ﺦ':
							c = 'ﱁ';
							break;
						case 'ﺨ':
							c = 'ﳋ';
							break;
						default:
							switch (c8)
							{
							case 'ﻢ':
								c = 'ﱂ';
								break;
							case 'ﻤ':
								c = 'ﳌ';
								break;
							}
							break;
						}
					}
				}
				if (c != '\0')
				{
					str[num2] = c;
					num--;
					i++;
				}
				else
				{
					num2++;
					str[num2] = str[i];
					i++;
				}
			}
			str.Length = num;
		}

		// Token: 0x06001871 RID: 6257 RVA: 0x0008E080 File Offset: 0x0008D080
		private static bool Connects_to_left(ArabicLigaturizer.Charstruct a)
		{
			return a.numshapes > 2;
		}

		// Token: 0x06001872 RID: 6258 RVA: 0x0008E08C File Offset: 0x0008D08C
		internal static void Shape(char[] text, StringBuilder str, int level)
		{
			int i = 0;
			ArabicLigaturizer.Charstruct charstruct = new ArabicLigaturizer.Charstruct();
			ArabicLigaturizer.Charstruct charstruct2 = new ArabicLigaturizer.Charstruct();
			int num2;
			while (i < text.Length)
			{
				char c = text[i++];
				if (ArabicLigaturizer.Ligature(c, charstruct2) == 0)
				{
					int num = ArabicLigaturizer.Shapecount(c);
					if (num == 1)
					{
						num2 = 0;
					}
					else
					{
						num2 = 2;
					}
					if (ArabicLigaturizer.Connects_to_left(charstruct))
					{
						num2++;
					}
					num2 %= charstruct2.numshapes;
					charstruct2.basechar = ArabicLigaturizer.Charshape(charstruct2.basechar, num2);
					ArabicLigaturizer.Copycstostring(str, charstruct, level);
					charstruct = charstruct2;
					charstruct2 = new ArabicLigaturizer.Charstruct();
					charstruct2.basechar = c;
					charstruct2.numshapes = num;
					charstruct2.lignum++;
				}
			}
			if (ArabicLigaturizer.Connects_to_left(charstruct))
			{
				num2 = 1;
			}
			else
			{
				num2 = 0;
			}
			num2 %= charstruct2.numshapes;
			charstruct2.basechar = ArabicLigaturizer.Charshape(charstruct2.basechar, num2);
			ArabicLigaturizer.Copycstostring(str, charstruct, level);
			ArabicLigaturizer.Copycstostring(str, charstruct2, level);
		}

		// Token: 0x06001873 RID: 6259 RVA: 0x0008E184 File Offset: 0x0008D184
		internal static int Arabic_shape(char[] src, int srcoffset, int srclength, char[] dest, int destoffset, int destlength, int level)
		{
			char[] array = new char[srclength];
			for (int i = srclength + srcoffset - 1; i >= srcoffset; i--)
			{
				array[i - srcoffset] = src[i];
			}
			StringBuilder stringBuilder = new StringBuilder(srclength);
			ArabicLigaturizer.Shape(array, stringBuilder, level);
			if ((level & 12) != 0)
			{
				ArabicLigaturizer.Doublelig(stringBuilder, level);
			}
			Array.Copy(stringBuilder.ToString().ToCharArray(), 0, dest, destoffset, stringBuilder.Length);
			return stringBuilder.Length;
		}

		// Token: 0x06001874 RID: 6260 RVA: 0x0008E1F0 File Offset: 0x0008D1F0
		internal static void ProcessNumbers(char[] text, int offset, int length, int options)
		{
			int num = offset + length;
			if ((options & 224) != 0)
			{
				char c = '0';
				int num2 = options & 256;
				if (num2 != 0)
				{
					if (num2 == 256)
					{
						c = '۰';
					}
				}
				else
				{
					c = '٠';
				}
				int num3 = options & 224;
				if (num3 <= 64)
				{
					if (num3 == 32)
					{
						int num4 = (int)(c - '0');
						for (int i = offset; i < num; i++)
						{
							char c2 = text[i];
							if (c2 <= '9' && c2 >= '0')
							{
								int num5 = i;
								text[num5] += (char)num4;
							}
						}
						return;
					}
					if (num3 != 64)
					{
						return;
					}
					char c3 = c + '\t';
					int num6 = (int)('0' - c);
					for (int j = offset; j < num; j++)
					{
						char c4 = text[j];
						if (c4 <= c3 && c4 >= c)
						{
							int num7 = j;
							text[num7] += (char)num6;
						}
					}
					return;
				}
				else
				{
					if (num3 == 96)
					{
						ArabicLigaturizer.ShapeToArabicDigitsWithContext(text, 0, length, c, false);
						return;
					}
					if (num3 != 128)
					{
						return;
					}
					ArabicLigaturizer.ShapeToArabicDigitsWithContext(text, 0, length, c, true);
				}
			}
		}

		// Token: 0x06001875 RID: 6261 RVA: 0x0008E300 File Offset: 0x0008D300
		internal static void ShapeToArabicDigitsWithContext(char[] dest, int start, int length, char digitBase, bool lastStrongWasAL)
		{
			digitBase -= '0';
			int num = start + length;
			for (int i = start; i < num; i++)
			{
				char c = dest[i];
				sbyte direction = BidiOrder.GetDirection(c);
				switch (direction)
				{
				case 0:
				case 3:
					lastStrongWasAL = false;
					break;
				case 1:
				case 2:
					break;
				case 4:
					lastStrongWasAL = true;
					break;
				default:
					if (direction == 8)
					{
						if (lastStrongWasAL && c <= '9')
						{
							dest[i] = c + digitBase;
						}
					}
					break;
				}
			}
		}

		// Token: 0x0400106F RID: 4207
		private const char ALEF = 'ا';

		// Token: 0x04001070 RID: 4208
		private const char ALEFHAMZA = 'أ';

		// Token: 0x04001071 RID: 4209
		private const char ALEFHAMZABELOW = 'إ';

		// Token: 0x04001072 RID: 4210
		private const char ALEFMADDA = 'آ';

		// Token: 0x04001073 RID: 4211
		private const char LAM = 'ل';

		// Token: 0x04001074 RID: 4212
		private const char HAMZA = 'ء';

		// Token: 0x04001075 RID: 4213
		private const char TATWEEL = 'ـ';

		// Token: 0x04001076 RID: 4214
		private const char ZWJ = '‍';

		// Token: 0x04001077 RID: 4215
		private const char HAMZAABOVE = 'ٔ';

		// Token: 0x04001078 RID: 4216
		private const char HAMZABELOW = 'ٕ';

		// Token: 0x04001079 RID: 4217
		private const char WAWHAMZA = 'ؤ';

		// Token: 0x0400107A RID: 4218
		private const char YEHHAMZA = 'ئ';

		// Token: 0x0400107B RID: 4219
		private const char WAW = 'و';

		// Token: 0x0400107C RID: 4220
		private const char ALEFMAKSURA = 'ى';

		// Token: 0x0400107D RID: 4221
		private const char YEH = 'ي';

		// Token: 0x0400107E RID: 4222
		private const char FARSIYEH = 'ی';

		// Token: 0x0400107F RID: 4223
		private const char SHADDA = 'ّ';

		// Token: 0x04001080 RID: 4224
		private const char KASRA = 'ِ';

		// Token: 0x04001081 RID: 4225
		private const char FATHA = 'َ';

		// Token: 0x04001082 RID: 4226
		private const char DAMMA = 'ُ';

		// Token: 0x04001083 RID: 4227
		private const char MADDA = 'ٓ';

		// Token: 0x04001084 RID: 4228
		private const char LAM_ALEF = 'ﻻ';

		// Token: 0x04001085 RID: 4229
		private const char LAM_ALEFHAMZA = 'ﻷ';

		// Token: 0x04001086 RID: 4230
		private const char LAM_ALEFHAMZABELOW = 'ﻹ';

		// Token: 0x04001087 RID: 4231
		private const char LAM_ALEFMADDA = 'ﻵ';

		// Token: 0x04001088 RID: 4232
		public const int ar_nothing = 0;

		// Token: 0x04001089 RID: 4233
		public const int ar_novowel = 1;

		// Token: 0x0400108A RID: 4234
		public const int ar_composedtashkeel = 4;

		// Token: 0x0400108B RID: 4235
		public const int ar_lig = 8;

		// Token: 0x0400108C RID: 4236
		public const int DIGITS_EN2AN = 32;

		// Token: 0x0400108D RID: 4237
		public const int DIGITS_AN2EN = 64;

		// Token: 0x0400108E RID: 4238
		public const int DIGITS_EN2AN_INIT_LR = 96;

		// Token: 0x0400108F RID: 4239
		public const int DIGITS_EN2AN_INIT_AL = 128;

		// Token: 0x04001090 RID: 4240
		private const int DIGITS_RESERVED = 160;

		// Token: 0x04001091 RID: 4241
		public const int DIGITS_MASK = 224;

		// Token: 0x04001092 RID: 4242
		public const int DIGIT_TYPE_AN = 0;

		// Token: 0x04001093 RID: 4243
		public const int DIGIT_TYPE_AN_EXTENDED = 256;

		// Token: 0x04001094 RID: 4244
		public const int DIGIT_TYPE_MASK = 256;

		// Token: 0x04001095 RID: 4245
		private static char[][] chartable = new char[][]
		{
			new char[]
			{
				'ء',
				'ﺀ'
			},
			new char[]
			{
				'آ',
				'ﺁ',
				'ﺂ'
			},
			new char[]
			{
				'أ',
				'ﺃ',
				'ﺄ'
			},
			new char[]
			{
				'ؤ',
				'ﺅ',
				'ﺆ'
			},
			new char[]
			{
				'إ',
				'ﺇ',
				'ﺈ'
			},
			new char[]
			{
				'ئ',
				'ﺉ',
				'ﺊ',
				'ﺋ',
				'ﺌ'
			},
			new char[]
			{
				'ا',
				'ﺍ',
				'ﺎ'
			},
			new char[]
			{
				'ب',
				'ﺏ',
				'ﺐ',
				'ﺑ',
				'ﺒ'
			},
			new char[]
			{
				'ة',
				'ﺓ',
				'ﺔ'
			},
			new char[]
			{
				'ت',
				'ﺕ',
				'ﺖ',
				'ﺗ',
				'ﺘ'
			},
			new char[]
			{
				'ث',
				'ﺙ',
				'ﺚ',
				'ﺛ',
				'ﺜ'
			},
			new char[]
			{
				'ج',
				'ﺝ',
				'ﺞ',
				'ﺟ',
				'ﺠ'
			},
			new char[]
			{
				'ح',
				'ﺡ',
				'ﺢ',
				'ﺣ',
				'ﺤ'
			},
			new char[]
			{
				'خ',
				'ﺥ',
				'ﺦ',
				'ﺧ',
				'ﺨ'
			},
			new char[]
			{
				'د',
				'ﺩ',
				'ﺪ'
			},
			new char[]
			{
				'ذ',
				'ﺫ',
				'ﺬ'
			},
			new char[]
			{
				'ر',
				'ﺭ',
				'ﺮ'
			},
			new char[]
			{
				'ز',
				'ﺯ',
				'ﺰ'
			},
			new char[]
			{
				'س',
				'ﺱ',
				'ﺲ',
				'ﺳ',
				'ﺴ'
			},
			new char[]
			{
				'ش',
				'ﺵ',
				'ﺶ',
				'ﺷ',
				'ﺸ'
			},
			new char[]
			{
				'ص',
				'ﺹ',
				'ﺺ',
				'ﺻ',
				'ﺼ'
			},
			new char[]
			{
				'ض',
				'ﺽ',
				'ﺾ',
				'ﺿ',
				'ﻀ'
			},
			new char[]
			{
				'ط',
				'ﻁ',
				'ﻂ',
				'ﻃ',
				'ﻄ'
			},
			new char[]
			{
				'ظ',
				'ﻅ',
				'ﻆ',
				'ﻇ',
				'ﻈ'
			},
			new char[]
			{
				'ع',
				'ﻉ',
				'ﻊ',
				'ﻋ',
				'ﻌ'
			},
			new char[]
			{
				'غ',
				'ﻍ',
				'ﻎ',
				'ﻏ',
				'ﻐ'
			},
			new char[]
			{
				'ـ',
				'ـ',
				'ـ',
				'ـ',
				'ـ'
			},
			new char[]
			{
				'ف',
				'ﻑ',
				'ﻒ',
				'ﻓ',
				'ﻔ'
			},
			new char[]
			{
				'ق',
				'ﻕ',
				'ﻖ',
				'ﻗ',
				'ﻘ'
			},
			new char[]
			{
				'ك',
				'ﻙ',
				'ﻚ',
				'ﻛ',
				'ﻜ'
			},
			new char[]
			{
				'ل',
				'ﻝ',
				'ﻞ',
				'ﻟ',
				'ﻠ'
			},
			new char[]
			{
				'م',
				'ﻡ',
				'ﻢ',
				'ﻣ',
				'ﻤ'
			},
			new char[]
			{
				'ن',
				'ﻥ',
				'ﻦ',
				'ﻧ',
				'ﻨ'
			},
			new char[]
			{
				'ه',
				'ﻩ',
				'ﻪ',
				'ﻫ',
				'ﻬ'
			},
			new char[]
			{
				'و',
				'ﻭ',
				'ﻮ'
			},
			new char[]
			{
				'ى',
				'ﻯ',
				'ﻰ',
				'ﯨ',
				'ﯩ'
			},
			new char[]
			{
				'ي',
				'ﻱ',
				'ﻲ',
				'ﻳ',
				'ﻴ'
			},
			new char[]
			{
				'ٱ',
				'ﭐ',
				'ﭑ'
			},
			new char[]
			{
				'ٹ',
				'ﭦ',
				'ﭧ',
				'ﭨ',
				'ﭩ'
			},
			new char[]
			{
				'ٺ',
				'ﭞ',
				'ﭟ',
				'ﭠ',
				'ﭡ'
			},
			new char[]
			{
				'ٻ',
				'ﭒ',
				'ﭓ',
				'ﭔ',
				'ﭕ'
			},
			new char[]
			{
				'پ',
				'ﭖ',
				'ﭗ',
				'ﭘ',
				'ﭙ'
			},
			new char[]
			{
				'ٿ',
				'ﭢ',
				'ﭣ',
				'ﭤ',
				'ﭥ'
			},
			new char[]
			{
				'ڀ',
				'ﭚ',
				'ﭛ',
				'ﭜ',
				'ﭝ'
			},
			new char[]
			{
				'ڃ',
				'ﭶ',
				'ﭷ',
				'ﭸ',
				'ﭹ'
			},
			new char[]
			{
				'ڄ',
				'ﭲ',
				'ﭳ',
				'ﭴ',
				'ﭵ'
			},
			new char[]
			{
				'چ',
				'ﭺ',
				'ﭻ',
				'ﭼ',
				'ﭽ'
			},
			new char[]
			{
				'ڇ',
				'ﭾ',
				'ﭿ',
				'ﮀ',
				'ﮁ'
			},
			new char[]
			{
				'ڈ',
				'ﮈ',
				'ﮉ'
			},
			new char[]
			{
				'ڌ',
				'ﮄ',
				'ﮅ'
			},
			new char[]
			{
				'ڍ',
				'ﮂ',
				'ﮃ'
			},
			new char[]
			{
				'ڎ',
				'ﮆ',
				'ﮇ'
			},
			new char[]
			{
				'ڑ',
				'ﮌ',
				'ﮍ'
			},
			new char[]
			{
				'ژ',
				'ﮊ',
				'ﮋ'
			},
			new char[]
			{
				'ڤ',
				'ﭪ',
				'ﭫ',
				'ﭬ',
				'ﭭ'
			},
			new char[]
			{
				'ڦ',
				'ﭮ',
				'ﭯ',
				'ﭰ',
				'ﭱ'
			},
			new char[]
			{
				'ک',
				'ﮎ',
				'ﮏ',
				'ﮐ',
				'ﮑ'
			},
			new char[]
			{
				'ڭ',
				'ﯓ',
				'ﯔ',
				'ﯕ',
				'ﯖ'
			},
			new char[]
			{
				'گ',
				'ﮒ',
				'ﮓ',
				'ﮔ',
				'ﮕ'
			},
			new char[]
			{
				'ڱ',
				'ﮚ',
				'ﮛ',
				'ﮜ',
				'ﮝ'
			},
			new char[]
			{
				'ڳ',
				'ﮖ',
				'ﮗ',
				'ﮘ',
				'ﮙ'
			},
			new char[]
			{
				'ں',
				'ﮞ',
				'ﮟ'
			},
			new char[]
			{
				'ڻ',
				'ﮠ',
				'ﮡ',
				'ﮢ',
				'ﮣ'
			},
			new char[]
			{
				'ھ',
				'ﮪ',
				'ﮫ',
				'ﮬ',
				'ﮭ'
			},
			new char[]
			{
				'ۀ',
				'ﮤ',
				'ﮥ'
			},
			new char[]
			{
				'ہ',
				'ﮦ',
				'ﮧ',
				'ﮨ',
				'ﮩ'
			},
			new char[]
			{
				'ۅ',
				'ﯠ',
				'ﯡ'
			},
			new char[]
			{
				'ۆ',
				'ﯙ',
				'ﯚ'
			},
			new char[]
			{
				'ۇ',
				'ﯗ',
				'ﯘ'
			},
			new char[]
			{
				'ۈ',
				'ﯛ',
				'ﯜ'
			},
			new char[]
			{
				'ۉ',
				'ﯢ',
				'ﯣ'
			},
			new char[]
			{
				'ۋ',
				'ﯞ',
				'ﯟ'
			},
			new char[]
			{
				'ی',
				'ﯼ',
				'ﯽ',
				'ﯾ',
				'ﯿ'
			},
			new char[]
			{
				'ې',
				'ﯤ',
				'ﯥ',
				'ﯦ',
				'ﯧ'
			},
			new char[]
			{
				'ے',
				'ﮮ',
				'ﮯ'
			},
			new char[]
			{
				'ۓ',
				'ﮰ',
				'ﮱ'
			}
		};

		// Token: 0x02000289 RID: 649
		private class Charstruct
		{
			// Token: 0x04001096 RID: 4246
			internal char basechar;

			// Token: 0x04001097 RID: 4247
			internal char mark1;

			// Token: 0x04001098 RID: 4248
			internal char vowel;

			// Token: 0x04001099 RID: 4249
			internal int lignum;

			// Token: 0x0400109A RID: 4250
			internal int numshapes = 1;
		}
	}
}
