using System;
using System.Drawing;
using System.Globalization;
using System.Text;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Core.Parser.Biff_Records.Formula
{
	// Token: 0x020002B4 RID: 692
	public abstract class Ptg : ICloneable
	{
		// Token: 0x060029DE RID: 10718 RVA: 0x00178D8C File Offset: 0x00177D8C
		protected Ptg()
		{
		}

		// Token: 0x060029DF RID: 10719 RVA: 0x00178DA0 File Offset: 0x00177DA0
		protected Ptg(DataProvider provider, int offset, ExcelVersion version)
		{
			this.ᜁ = (FormulaToken)provider.ReadByte(offset);
			offset++;
			this.InfillPTG(provider, ref offset, version);
		}

		// Token: 0x17000CA5 RID: 3237
		// (get) Token: 0x060029E0 RID: 10720 RVA: 0x00178DD0 File Offset: 0x00177DD0
		public virtual bool IsOperation
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return false;
			}
		}

		// Token: 0x17000CA6 RID: 3238
		// (get) Token: 0x060029E1 RID: 10721 RVA: 0x00178E0C File Offset: 0x00177E0C
		// (set) Token: 0x060029E2 RID: 10722 RVA: 0x00178E50 File Offset: 0x00177E50
		public virtual FormulaToken TokenCode
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜁ;
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				this.ᜁ = value;
			}
		}

		// Token: 0x060029E3 RID: 10723 RVA: 0x00178E94 File Offset: 0x00177E94
		public static string GetString16Bit(byte[] data, int offset)
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			int num;
			return Ptg.GetString16Bit(data, offset, out num);
		}

		// Token: 0x060029E4 RID: 10724 RVA: 0x00178ED8 File Offset: 0x00177ED8
		public static string GetString16Bit(byte[] data, int offset, out int iFullLength)
		{
			int a_ = 3;
			int num = 2;
			ushort num2;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					bool flag;
					iFullLength = (int)(flag ? (3 + num2 * 2) : (3 + num2));
					num = 1;
					continue;
				}
				case 1:
					if (iFullLength >= data.Length)
					{
						num = 6;
						continue;
					}
					num = 5;
					continue;
				case 3:
					goto IL_B9;
				case 4:
					goto IL_DF;
				case 5:
				{
					bool flag;
					if (!flag)
					{
						num = 3;
						continue;
					}
					goto IL_12C;
				}
				case 6:
					goto IL_116;
				case 7:
					if (true)
					{
					}
					num = 4;
					continue;
				}
				if (offset + 3 >= data.Length)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_8F;
					default:
						if (false)
						{
						}
						num = 7;
						break;
					}
				}
				else
				{
					num2 = BitConverter.ToUInt16(data, offset);
					offset += 2;
					bool flag = BitConverter.ToBoolean(data, offset);
					offset++;
					num = 0;
				}
			}
			IL_8F:
			return BiffRecordRaw.LatinEncoding.GetString(data, offset, (int)num2);
			IL_B9:
			goto IL_8F;
			IL_DF:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("縸帺䤼氾㕀ㅂⱄ⥆⹈穊筌ൎ㡐❒潔睖㵘㩚⥜㹞䅠ɢᝤᕦࡨቪ䵬᭮Ṱᱲ啴Ѷᑸ᩺ᅼ፾꾀", a_));
			IL_116:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("縸帺䤼氾㕀ㅂⱄ⥆⹈穊筌ൎ㡐❒潔睖㵘㩚⥜㹞䅠ɢᝤᕦࡨቪ䵬᭮Ṱᱲ啴Ѷᑸ᩺ᅼ፾꾀", a_));
			IL_12C:
			return Encoding.Unicode.GetString(data, offset, (int)(num2 * 2));
		}

		// Token: 0x060029E5 RID: 10725 RVA: 0x00179020 File Offset: 0x00178020
		public virtual void InfillPTG(DataProvider provider, ref int offset, ExcelVersion version)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
		}

		// Token: 0x060029E6 RID: 10726
		public abstract int GetSize(ExcelVersion version);

		// Token: 0x060029E7 RID: 10727 RVA: 0x0017905C File Offset: 0x0017805C
		public virtual byte[] ToByteArray(ExcelVersion version)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			byte[] array = new byte[this.GetSize(version)];
			array[0] = (byte)this.TokenCode;
			return array;
		}

		// Token: 0x060029E8 RID: 10728 RVA: 0x001790B0 File Offset: 0x001780B0
		public override string ToString()
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			return this.ToString(null, 0, 0, false);
		}

		// Token: 0x060029E9 RID: 10729 RVA: 0x001790F8 File Offset: 0x001780F8
		public virtual string ToString(FormulaUtil formulaUtil)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			return this.ToString(formulaUtil, 0, 0, false);
		}

		// Token: 0x060029EA RID: 10730 RVA: 0x00179140 File Offset: 0x00178140
		public virtual string ToString(FormulaUtil formulaUtil, int iRow, int iColumn, bool bR1C1)
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			return this.ToString(formulaUtil, iRow, iColumn, bR1C1, null, false);
		}

		// Token: 0x060029EB RID: 10731 RVA: 0x00179188 File Offset: 0x00178188
		public virtual string ToString(int row, int col, bool bR1C1)
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			return this.ToString(null, row, col, bR1C1);
		}

		// Token: 0x060029EC RID: 10732 RVA: 0x001791D0 File Offset: 0x001781D0
		public virtual string ToString(FormulaUtil formulaUtil, int row, int col, bool bR1C1, NumberFormatInfo numberFormat)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			return this.ToString(formulaUtil, row, col, bR1C1, numberFormat, false);
		}

		// Token: 0x060029ED RID: 10733 RVA: 0x0017921C File Offset: 0x0017821C
		public virtual string ToString(FormulaUtil formulaUtil, int row, int col, bool bR1C1, NumberFormatInfo numberFormat, bool isForSerialization)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			return base.ToString();
		}

		// Token: 0x060029EE RID: 10734 RVA: 0x00179260 File Offset: 0x00178260
		public virtual Ptg Offset(int iRowOffset, int iColumnOffset, XlsWorkbook book)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			return (Ptg)this.Clone();
		}

		// Token: 0x060029EF RID: 10735 RVA: 0x001792A8 File Offset: 0x001782A8
		public virtual Ptg Offset(int iCurSheetIndex, int iTokenRow, int iTokenColumn, int iSourceSheetIndex, Rectangle rectSource, int iDestSheetIndex, Rectangle rectDest, out bool bChanged, XlsWorkbook book)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			bChanged = false;
			return (Ptg)this.Clone();
		}

		// Token: 0x060029F0 RID: 10736 RVA: 0x001792F4 File Offset: 0x001782F4
		public static bool RectangleContains(Rectangle rect, int iRow, int iColumn)
		{
			int num = 7;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 6;
					continue;
				case 1:
					num = 4;
					continue;
				case 2:
					if (rect.Top <= iRow)
					{
						num = 1;
						continue;
					}
					return false;
				case 3:
					num = 2;
					continue;
				case 4:
					if (rect.Bottom >= iRow)
					{
						num = 5;
						continue;
					}
					return false;
				case 5:
					return true;
				case 6:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						if (rect.Right >= iColumn)
						{
							num = 3;
							continue;
						}
						return false;
					}
					break;
				case 7:
					if (true)
					{
					}
					break;
				}
				if (rect.Left > iColumn)
				{
					return false;
				}
				num = 0;
			}
			return true;
		}

		// Token: 0x060029F1 RID: 10737 RVA: 0x001793D4 File Offset: 0x001783D4
		public virtual Ptg ConvertSharedToken(IWorkbook parent, int iRow, int iColumn)
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			return (Ptg)this.Clone();
		}

		// Token: 0x060029F2 RID: 10738 RVA: 0x0017941C File Offset: 0x0017841C
		public virtual Ptg ConvertPtgToNPtg(IWorkbook parent, int iRow, int iColumn)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			return this;
		}

		// Token: 0x060029F3 RID: 10739 RVA: 0x00179458 File Offset: 0x00178458
		public int CompareTo(Ptg token)
		{
			int num = 7;
			int num2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (num2 == 0)
					{
						num = 2;
						continue;
					}
					goto IL_59;
				case 1:
					num2 = this.CompareContent(token);
					num = 3;
					continue;
				case 2:
					goto IL_D0;
				case 3:
					goto IL_A7;
				case 4:
					if (num2 == 0)
					{
						num = 1;
						continue;
					}
					goto IL_D2;
				case 5:
					return 1;
				case 6:
					goto IL_59;
				}
				if (token != null)
				{
					num2 = this.TokenCode - token.TokenCode;
					num = 0;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_D0;
				default:
					if (false)
					{
					}
					num = 5;
					continue;
				}
				IL_59:
				num = 4;
				continue;
				IL_D0:
				num2 = this.GetSize(ExcelVersion.Version2007) - token.GetSize(ExcelVersion.Version2007);
				num = 6;
			}
			return 1;
			IL_A7:
			IL_D2:
			if (true)
			{
			}
			return num2;
		}

		// Token: 0x060029F4 RID: 10740 RVA: 0x00179540 File Offset: 0x00178540
		protected int CompareContent(Ptg token)
		{
			int a_ = 0;
			int num = 3;
			for (;;)
			{
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					goto IL_8F;
				case 1:
					goto IL_46;
				case 2:
				{
					byte[] array;
					byte[] array2;
					if (!BiffRecordRaw.CompareArrays(array, array2))
					{
						num = 0;
						continue;
					}
					return 0;
				}
				}
				if (token == null)
				{
					num = 1;
				}
				else
				{
					byte[] array = this.ToByteArray(ExcelVersion.Version2007);
					byte[] array2 = token.ToByteArray(ExcelVersion.Version2007);
					num = 2;
				}
			}
			IL_46:
			throw new ArgumentNullException(RecordTableEnumerator.b("䈵圷儹夻倽", a_));
			IL_8F:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return 0;
			default:
				if (false)
				{
				}
				return 1;
			}
		}

		// Token: 0x060029F5 RID: 10741 RVA: 0x001795F4 File Offset: 0x001785F4
		public static bool CompareArrays(Ptg[] arrTokens1, Ptg[] arrTokens2)
		{
			switch (0)
			{
			default:
			{
				int num;
				Ptg ptg;
				Ptg token;
				int num2;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_8D:
					ptg = arrTokens1[num];
					token = arrTokens2[num];
					num2 = 2;
					break;
				default:
					if (false)
					{
					}
					num2 = 1;
					break;
				}
				for (;;)
				{
					switch (num2)
					{
					case 0:
						num2 = 15;
						continue;
					case 2:
						if (ptg.CompareTo(token) != 0)
						{
							num2 = 12;
							continue;
						}
						num++;
						num2 = 5;
						continue;
					case 3:
					{
						int num3;
						if (num3 != arrTokens2.Length)
						{
							num2 = 4;
							continue;
						}
						num = 0;
						num2 = 8;
						continue;
					}
					case 4:
						return false;
					case 5:
						goto IL_105;
					case 6:
						return true;
					case 7:
						goto IL_194;
					case 8:
						goto IL_105;
					case 9:
					{
						if (arrTokens2 == null)
						{
							num2 = 7;
							continue;
						}
						int num3 = arrTokens1.Length;
						num2 = 3;
						continue;
					}
					case 10:
						num2 = 9;
						continue;
					case 11:
						if (arrTokens1 != null)
						{
							num2 = 10;
							continue;
						}
						return false;
					case 12:
						return false;
					case 13:
						return true;
					case 14:
					{
						int num3;
						if (num >= num3)
						{
							if (true)
							{
							}
							num2 = 6;
							continue;
						}
						goto IL_8D;
					}
					case 15:
						if (arrTokens2 == null)
						{
							num2 = 13;
							continue;
						}
						goto IL_B7;
					}
					if (arrTokens1 == null)
					{
						num2 = 0;
						continue;
					}
					IL_B7:
					num2 = 11;
					continue;
					IL_105:
					num2 = 14;
				}
				return false;
				IL_194:
				return false;
			}
			}
		}

		// Token: 0x060029F6 RID: 10742 RVA: 0x00179798 File Offset: 0x00178798
		public virtual string ToString(FormulaUtil formulaUtil, int row, int col, bool bR1C1, NumberFormatInfo numberInfo, bool isForSerialization, IWorksheet sheet)
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			return this.ToString(formulaUtil, row, col, bR1C1, numberInfo, isForSerialization);
		}

		// Token: 0x060029F7 RID: 10743 RVA: 0x001797E4 File Offset: 0x001787E4
		public object Clone()
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			return base.MemberwiseClone();
		}

		// Token: 0x060029F8 RID: 10744 RVA: 0x00179828 File Offset: 0x00178828
		public static FormulaToken IndexToCode(FormulaToken baseToken, int index)
		{
			int a_ = 10;
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (true)
					{
					}
					if (index > 3)
					{
						num = 1;
						continue;
					}
					goto IL_A0;
				case 1:
					goto IL_9E;
				case 3:
					num = 0;
					continue;
				}
				IL_29:
				if (index < 1)
				{
					break;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					num = 3;
					continue;
				}
				goto IL_29;
			}
			IL_5D:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⤿ⱁ⁃⍅ぇ", a_), RecordTableEnumerator.b("ᘿ⍁⡃㍅ⵇ橉⽋⽍㹏㱑㭓≕硗㡙㥛繝౟ݡᝣᕥ䡧ṩѫ཭ṯ剱䕳噵᝷ࡹ屻᥽겋揄望뚕ꮗ뒙", a_));
			IL_9E:
			goto IL_5D;
			IL_A0:
			return baseToken + (index - 1) * 32;
		}

		// Token: 0x040013EB RID: 5099
		private string \u2460\u008E\u0088\u00A7;

		// Token: 0x040013EC RID: 5100
		private const int ᜀ = 32;

		// Token: 0x040013ED RID: 5101
		private int[] \u25D8\u00AD\u008C\u00A6;

		// Token: 0x040013EE RID: 5102
		private FormulaToken ᜁ;
	}
}
