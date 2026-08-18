using System;

namespace iTextSharp.text.pdf.qrcode
{
	// Token: 0x02000210 RID: 528
	public sealed class Mode
	{
		// Token: 0x0600142D RID: 5165 RVA: 0x0007371C File Offset: 0x0007271C
		private Mode(int[] characterCountBitsForVersions, int bits, string name)
		{
			this.characterCountBitsForVersions = characterCountBitsForVersions;
			this.bits = bits;
			this.name = name;
		}

		// Token: 0x0600142E RID: 5166 RVA: 0x0007373C File Offset: 0x0007273C
		public static Mode ForBits(int bits)
		{
			switch (bits)
			{
			case 0:
				return Mode.TERMINATOR;
			case 1:
				return Mode.NUMERIC;
			case 2:
				return Mode.ALPHANUMERIC;
			case 3:
				return Mode.STRUCTURED_APPEND;
			case 4:
				return Mode.BYTE;
			case 5:
				return Mode.FNC1_FIRST_POSITION;
			case 7:
				return Mode.ECI;
			case 8:
				return Mode.KANJI;
			case 9:
				return Mode.FNC1_SECOND_POSITION;
			}
			throw new ArgumentException();
		}

		// Token: 0x0600142F RID: 5167 RVA: 0x000737B8 File Offset: 0x000727B8
		public int GetCharacterCountBits(Version version)
		{
			if (this.characterCountBitsForVersions == null)
			{
				throw new ArgumentException("Character count doesn't apply to this mode");
			}
			int versionNumber = version.GetVersionNumber();
			int num;
			if (versionNumber <= 9)
			{
				num = 0;
			}
			else if (versionNumber <= 26)
			{
				num = 1;
			}
			else
			{
				num = 2;
			}
			return this.characterCountBitsForVersions[num];
		}

		// Token: 0x06001430 RID: 5168 RVA: 0x000737FB File Offset: 0x000727FB
		public int GetBits()
		{
			return this.bits;
		}

		// Token: 0x06001431 RID: 5169 RVA: 0x00073803 File Offset: 0x00072803
		public string GetName()
		{
			return this.name;
		}

		// Token: 0x06001432 RID: 5170 RVA: 0x0007380B File Offset: 0x0007280B
		public override string ToString()
		{
			return this.name;
		}

		// Token: 0x06001433 RID: 5171 RVA: 0x00073854 File Offset: 0x00072854
		// Note: this type is marked as 'beforefieldinit'.
		static Mode()
		{
			int[] array = new int[3];
			Mode.TERMINATOR = new Mode(array, 0, "TERMINATOR");
			Mode.NUMERIC = new Mode(new int[]
			{
				10,
				12,
				14
			}, 1, "NUMERIC");
			Mode.ALPHANUMERIC = new Mode(new int[]
			{
				9,
				11,
				13
			}, 2, "ALPHANUMERIC");
			int[] array2 = new int[3];
			Mode.STRUCTURED_APPEND = new Mode(array2, 3, "STRUCTURED_APPEND");
			Mode.BYTE = new Mode(new int[]
			{
				8,
				16,
				16
			}, 4, "BYTE");
			Mode.ECI = new Mode(null, 7, "ECI");
			Mode.KANJI = new Mode(new int[]
			{
				8,
				10,
				12
			}, 8, "KANJI");
			Mode.FNC1_FIRST_POSITION = new Mode(null, 5, "FNC1_FIRST_POSITION");
			Mode.FNC1_SECOND_POSITION = new Mode(null, 9, "FNC1_SECOND_POSITION");
		}

		// Token: 0x04000DE4 RID: 3556
		public static readonly Mode TERMINATOR;

		// Token: 0x04000DE5 RID: 3557
		public static readonly Mode NUMERIC;

		// Token: 0x04000DE6 RID: 3558
		public static readonly Mode ALPHANUMERIC;

		// Token: 0x04000DE7 RID: 3559
		public static readonly Mode STRUCTURED_APPEND;

		// Token: 0x04000DE8 RID: 3560
		public static readonly Mode BYTE;

		// Token: 0x04000DE9 RID: 3561
		public static readonly Mode ECI;

		// Token: 0x04000DEA RID: 3562
		public static readonly Mode KANJI;

		// Token: 0x04000DEB RID: 3563
		public static readonly Mode FNC1_FIRST_POSITION;

		// Token: 0x04000DEC RID: 3564
		public static readonly Mode FNC1_SECOND_POSITION;

		// Token: 0x04000DED RID: 3565
		private int[] characterCountBitsForVersions;

		// Token: 0x04000DEE RID: 3566
		private int bits;

		// Token: 0x04000DEF RID: 3567
		private string name;
	}
}
