using System;
using System.Collections.Generic;

namespace OracleInternal.I18N
{
	// Token: 0x020000FB RID: 251
	[Serializable]
	internal abstract class TLBConv : Conv
	{
		// Token: 0x06000A90 RID: 2704 RVA: 0x00075C88 File Offset: 0x00073E88
		public TLBConv()
		{
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x00075C90 File Offset: 0x00073E90
		protected TLBConv(int oracleId) : base(oracleId)
		{
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x00075C9C File Offset: 0x00073E9C
		public static TLBConv GetGLBInstance(int oraId)
		{
			string text = string.Format("{0:X}", oraId);
			if (TLBConv.m_converterStore.ContainsKey(text))
			{
				return TLBConv.m_converterStore[text];
			}
			string str = "lx2" + "0000".Substring(0, 4 - text.Length) + text;
			TLBConv tlbconv = (TLBConv)TLBConvBoot.ReadObj(str + ".glb");
			if (tlbconv == null)
			{
				return null;
			}
			tlbconv.BuildUnicodeToOracleMapping();
			TLBConv.m_converterStore.Add(text, tlbconv);
			return tlbconv;
		}

		// Token: 0x06000A93 RID: 2707 RVA: 0x00075D24 File Offset: 0x00073F24
		protected void StoreMappingRange(int ucsCodePt, Dictionary<int, char[]> htable, Dictionary<int, char[]> htable2)
		{
			int num = ucsCodePt >> 24 & 255;
			int num2 = ucsCodePt >> 16 & 255;
			int num3 = ucsCodePt >> 8 & 255;
			int num4 = ucsCodePt & 255;
			int key = num;
			int key2 = ucsCodePt >> 16 & 65535;
			int key3 = ucsCodePt >> 8 & 16777215;
			int num5 = 26;
			int num6;
			if (ucsCodePt >= 0)
			{
				num6 = ucsCodePt >> num5;
			}
			else
			{
				num6 = (ucsCodePt >> num5) + (2 << ~num5);
			}
			char[] array2;
			if (num6 == 54)
			{
				if (!htable.ContainsKey(key))
				{
					char[] array = new char[2];
					array[0] = 'ÿ';
					array2 = array;
				}
				else
				{
					array2 = htable[key];
				}
				if (array2[0] == 'ÿ' && array2[1] == '\0')
				{
					array2[0] = (char)num2;
					array2[1] = (char)num2;
				}
				else
				{
					if (num2 < (int)(array2[0] & '￿'))
					{
						array2[0] = (char)num2;
					}
					if (num2 > (int)(array2[0] & '￿'))
					{
						array2[1] = (char)num2;
					}
				}
				if (!htable.ContainsKey(key))
				{
					htable.Add(key, array2);
				}
				if (!htable.ContainsKey(key2))
				{
					char[] array3 = new char[2];
					array3[0] = 'ÿ';
					array2 = array3;
				}
				else
				{
					array2 = htable[key2];
				}
				if (array2[0] == 'ÿ' && array2[1] == '\0')
				{
					array2[0] = (char)num3;
					array2[1] = (char)num3;
				}
				else
				{
					if (num3 < (int)(array2[0] & '￿'))
					{
						array2[0] = (char)num3;
					}
					if (num3 > (int)(array2[0] & '￿'))
					{
						array2[1] = (char)num3;
					}
				}
				if (!htable.ContainsKey(key2))
				{
					htable.Add(key2, array2);
				}
			}
			if (!htable2.ContainsKey(key3))
			{
				char[] array4 = new char[2];
				array4[0] = 'ÿ';
				array2 = array4;
			}
			else
			{
				array2 = htable2[key3];
			}
			if (array2[0] == 'ÿ' && array2[1] == '\0')
			{
				array2[0] = (char)num4;
				array2[1] = (char)num4;
			}
			else
			{
				if (num4 < (int)(array2[0] & '￿'))
				{
					array2[0] = (char)num4;
				}
				if (num4 > (int)(array2[0] & '￿'))
				{
					array2[1] = (char)num4;
				}
			}
			if (!htable2.ContainsKey(key3))
			{
				htable2.Add(key3, array2);
			}
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x00075F30 File Offset: 0x00074130
		public int GetGroupId()
		{
			return this.m_groupId;
		}

		// Token: 0x06000A95 RID: 2709
		public abstract bool IsOraCharacterReplacement(char ch, char lowsur);

		// Token: 0x06000A96 RID: 2710
		public abstract void BuildUnicodeToOracleMapping();

		// Token: 0x06000A97 RID: 2711
		public abstract void ExtractCodepoints(IList<int[]> vtable);

		// Token: 0x06000A98 RID: 2712
		public abstract void ExtractExtraMappings(IList<int[]> vtable);

		// Token: 0x06000A99 RID: 2713
		public abstract bool HasExtraMappings();

		// Token: 0x06000A9A RID: 2714
		public abstract char GetOraChar1ByteRep();

		// Token: 0x06000A9B RID: 2715
		public abstract char GetOraChar2ByteRep();

		// Token: 0x06000A9C RID: 2716
		public abstract int GetUCS2CharRep();

		// Token: 0x06000A9D RID: 2717 RVA: 0x00075F38 File Offset: 0x00074138
		public char[] GetLeadingCodes()
		{
			return null;
		}

		// Token: 0x04000CA8 RID: 3240
		public const int CHARCONV1BYTEID = 0;

		// Token: 0x04000CA9 RID: 3241
		public const int CHARCONV12BYTEID = 1;

		// Token: 0x04000CAA RID: 3242
		public const int CHARCONVJAEUCID = 2;

		// Token: 0x04000CAB RID: 3243
		public const int CHARCONVLCFIXEDID = 3;

		// Token: 0x04000CAC RID: 3244
		public const int CHARCONVSJISID = 4;

		// Token: 0x04000CAD RID: 3245
		public const int CHARCONVZHTEUCID = 5;

		// Token: 0x04000CAE RID: 3246
		public const int CHARCONV2BYTEFIXEDID = 6;

		// Token: 0x04000CAF RID: 3247
		public const int CHARCONVSHIFTID = 7;

		// Token: 0x04000CB0 RID: 3248
		public const int CHARCONVLCID = 8;

		// Token: 0x04000CB1 RID: 3249
		public const int CHARCONVGB18030ID = 9;

		// Token: 0x04000CB2 RID: 3250
		public const int CHARCONVAL16UTF16ID = 10;

		// Token: 0x04000CB3 RID: 3251
		public const int CHARCONVMSOLISO2022JPFWID = 11;

		// Token: 0x04000CB4 RID: 3252
		public const int CHARCONVMSOLISO2022JPHWID = 12;

		// Token: 0x04000CB5 RID: 3253
		public const int CHARCONVGBKID = 13;

		// Token: 0x04000CB6 RID: 3254
		private const string CONVERTERNAMEPREFIX = "lx2";

		// Token: 0x04000CB7 RID: 3255
		private const string CONVERTERIDPREFIX = "0000";

		// Token: 0x04000CB8 RID: 3256
		public const byte UNDEFINED_DISPLAY_WIDTH = 255;

		// Token: 0x04000CB9 RID: 3257
		public const int BELOW_CJK = 12287;

		// Token: 0x04000CBA RID: 3258
		protected const int HIBYTEMASK = 65280;

		// Token: 0x04000CBB RID: 3259
		protected const int LOWBYTEMASK = 255;

		// Token: 0x04000CBC RID: 3260
		protected const int STORE_INCREMENT = 10;

		// Token: 0x04000CBD RID: 3261
		protected const char CHAR_INVALID_ORA_CHAR = '￿';

		// Token: 0x04000CBE RID: 3262
		protected const int FIRSTBSHIFT = 24;

		// Token: 0x04000CBF RID: 3263
		protected const int SECONDBSHIFT = 16;

		// Token: 0x04000CC0 RID: 3264
		protected const int THIRDBSHIFT = 8;

		// Token: 0x04000CC1 RID: 3265
		protected const int UB2MASK = 65535;

		// Token: 0x04000CC2 RID: 3266
		protected const int UB4MASK = 65535;

		// Token: 0x04000CC3 RID: 3267
		protected const string BEGIN_UNISTR = "UNISTR('";

		// Token: 0x04000CC4 RID: 3268
		protected const string END_UNISTR = "')";

		// Token: 0x04000CC5 RID: 3269
		private static Dictionary<string, TLBConv> m_converterStore = new Dictionary<string, TLBConv>();

		// Token: 0x04000CC6 RID: 3270
		protected int m_groupId;

		// Token: 0x04000CC7 RID: 3271
		public int[][] extraUnicodeToOracleMapping;
	}
}
