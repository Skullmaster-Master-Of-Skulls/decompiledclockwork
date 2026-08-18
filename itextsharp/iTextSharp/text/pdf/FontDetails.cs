using System;
using System.Collections.Generic;

namespace iTextSharp.text.pdf
{
	// Token: 0x02000287 RID: 647
	public class FontDetails
	{
		// Token: 0x06001863 RID: 6243 RVA: 0x0008D444 File Offset: 0x0008C444
		internal FontDetails(PdfName fontName, PdfIndirectReference indirectReference, BaseFont baseFont)
		{
			this.fontName = fontName;
			this.indirectReference = indirectReference;
			this.baseFont = baseFont;
			this.fontType = baseFont.FontType;
			switch (this.fontType)
			{
			case 0:
			case 1:
				this.shortTag = new byte[256];
				return;
			case 2:
				this.cjkTag = new IntHashtable();
				this.cjkFont = (CJKFont)baseFont;
				return;
			case 3:
				this.longTag = new Dictionary<int, int[]>();
				this.ttu = (TrueTypeFontUnicode)baseFont;
				this.symbolic = baseFont.IsFontSpecific();
				return;
			default:
				return;
			}
		}

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x06001864 RID: 6244 RVA: 0x0008D4E9 File Offset: 0x0008C4E9
		internal PdfIndirectReference IndirectReference
		{
			get
			{
				return this.indirectReference;
			}
		}

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x06001865 RID: 6245 RVA: 0x0008D4F1 File Offset: 0x0008C4F1
		internal PdfName FontName
		{
			get
			{
				return this.fontName;
			}
		}

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06001866 RID: 6246 RVA: 0x0008D4F9 File Offset: 0x0008C4F9
		internal BaseFont BaseFont
		{
			get
			{
				return this.baseFont;
			}
		}

		// Token: 0x06001867 RID: 6247 RVA: 0x0008D504 File Offset: 0x0008C504
		internal byte[] ConvertToBytes(string text)
		{
			byte[] array = null;
			switch (this.fontType)
			{
			case 0:
			case 1:
			{
				array = this.baseFont.ConvertToBytes(text);
				int num = array.Length;
				for (int i = 0; i < num; i++)
				{
					this.shortTag[(int)(array[i] & byte.MaxValue)] = 1;
				}
				break;
			}
			case 2:
			{
				int length = text.Length;
				for (int j = 0; j < length; j++)
				{
					this.cjkTag[this.cjkFont.GetCidCode((int)text[j])] = 0;
				}
				array = this.baseFont.ConvertToBytes(text);
				break;
			}
			case 3:
			{
				int num2 = text.Length;
				char[] array2 = new char[num2];
				int length2 = 0;
				if (this.symbolic)
				{
					array = PdfEncodings.ConvertToBytes(text, "symboltt");
					num2 = array.Length;
					for (int k = 0; k < num2; k++)
					{
						int[] metricsTT = this.ttu.GetMetricsTT((int)(array[k] & byte.MaxValue));
						if (metricsTT != null)
						{
							this.longTag[metricsTT[0]] = new int[]
							{
								metricsTT[0],
								metricsTT[1],
								(int)this.ttu.GetUnicodeDifferences((int)(array[k] & byte.MaxValue))
							};
							array2[length2++] = (char)metricsTT[0];
						}
					}
				}
				else
				{
					for (int l = 0; l < num2; l++)
					{
						int num3;
						if (Utilities.IsSurrogatePair(text, l))
						{
							num3 = Utilities.ConvertToUtf32(text, l);
							l++;
						}
						else
						{
							num3 = (int)text[l];
						}
						int[] metricsTT = this.ttu.GetMetricsTT(num3);
						if (metricsTT != null)
						{
							int num4 = metricsTT[0];
							int key = num4;
							if (!this.longTag.ContainsKey(key))
							{
								this.longTag[key] = new int[]
								{
									num4,
									metricsTT[1],
									num3
								};
							}
							array2[length2++] = (char)num4;
						}
					}
				}
				string text2 = new string(array2, 0, length2);
				array = PdfEncodings.ConvertToBytes(text2, "UNICODEBIGUNMARKED");
				break;
			}
			case 4:
				array = this.baseFont.ConvertToBytes(text);
				break;
			case 5:
				return this.baseFont.ConvertToBytes(text);
			}
			return array;
		}

		// Token: 0x06001868 RID: 6248 RVA: 0x0008D74C File Offset: 0x0008C74C
		internal void WriteFont(PdfWriter writer)
		{
			switch (this.fontType)
			{
			case 0:
			case 1:
			{
				int num = 0;
				while (num < 256 && this.shortTag[num] == 0)
				{
					num++;
				}
				int num2 = 255;
				while (num2 >= num && this.shortTag[num2] == 0)
				{
					num2--;
				}
				if (num > 255)
				{
					num = 255;
					num2 = 255;
				}
				this.baseFont.WriteFont(writer, this.indirectReference, new object[]
				{
					num,
					num2,
					this.shortTag,
					this.subset
				});
				return;
			}
			case 2:
				this.baseFont.WriteFont(writer, this.indirectReference, new object[]
				{
					this.cjkTag
				});
				return;
			case 3:
				this.baseFont.WriteFont(writer, this.indirectReference, new object[]
				{
					this.longTag,
					this.subset
				});
				break;
			case 4:
				break;
			case 5:
				this.baseFont.WriteFont(writer, this.indirectReference, null);
				return;
			default:
				return;
			}
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x0600186A RID: 6250 RVA: 0x0008D884 File Offset: 0x0008C884
		// (set) Token: 0x06001869 RID: 6249 RVA: 0x0008D87B File Offset: 0x0008C87B
		public bool Subset
		{
			get
			{
				return this.subset;
			}
			set
			{
				this.subset = value;
			}
		}

		// Token: 0x04001064 RID: 4196
		private PdfIndirectReference indirectReference;

		// Token: 0x04001065 RID: 4197
		private PdfName fontName;

		// Token: 0x04001066 RID: 4198
		private BaseFont baseFont;

		// Token: 0x04001067 RID: 4199
		private TrueTypeFontUnicode ttu;

		// Token: 0x04001068 RID: 4200
		private CJKFont cjkFont;

		// Token: 0x04001069 RID: 4201
		private byte[] shortTag;

		// Token: 0x0400106A RID: 4202
		private Dictionary<int, int[]> longTag;

		// Token: 0x0400106B RID: 4203
		private IntHashtable cjkTag;

		// Token: 0x0400106C RID: 4204
		private int fontType;

		// Token: 0x0400106D RID: 4205
		private bool symbolic;

		// Token: 0x0400106E RID: 4206
		protected bool subset = true;
	}
}
