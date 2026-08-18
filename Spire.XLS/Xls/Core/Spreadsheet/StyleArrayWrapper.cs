using System;
using System.Collections.Generic;
using System.Drawing;
using Spire.Xls.Core.Interfaces;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Core.Spreadsheet
{
	// Token: 0x02000629 RID: 1577
	public class StyleArrayWrapper : XlsObject, IStyle, IExtendIndex
	{
		// Token: 0x0600603E RID: 24638 RVA: 0x003CD388 File Offset: 0x003CC388
		public StyleArrayWrapper(IXLSRange range) : base(((XlsRange)range).Application, range)
		{
			this.ᜀ.AddRange(range.Cells);
			IWorksheet worksheet = range.Worksheet;
			this.ᜁ = (worksheet.Workbook as XlsWorkbook);
		}

		// Token: 0x17000FBC RID: 4028
		// (get) Token: 0x0600603F RID: 24639 RVA: 0x003CD3DC File Offset: 0x003CC3DC
		// (set) Token: 0x06006040 RID: 24640 RVA: 0x003CD434 File Offset: 0x003CC434
		public bool JustifyLast
		{
			get
			{
				int a_ = 11;
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				throw new NotImplementedException(RecordTableEnumerator.b("ཀⱂㅄ杆⁈♊㵌⍎㑐㹒ご㥖ⵘ㹚㥜罞ᅠᅢ੤ᝦ౨ᥪᥬ᙮彰", a_));
			}
			set
			{
				int a_ = 8;
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				throw new NotImplementedException(RecordTableEnumerator.b("瀽⼿㙁摃⽅╇㩉⁋⭍㵏㝑㩓≕㵗㹙籛⹝቟ൡᑣͥᩧṩᕫ䁭", a_));
			}
		}

		// Token: 0x17000FBD RID: 4029
		// (get) Token: 0x06006041 RID: 24641 RVA: 0x003CD48C File Offset: 0x003CC48C
		// (set) Token: 0x06006042 RID: 24642 RVA: 0x003CD4E4 File Offset: 0x003CC4E4
		public string NumberFormatLocal
		{
			get
			{
				int a_ = 11;
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				throw new NotImplementedException(RecordTableEnumerator.b("ཀⱂㅄ杆⁈♊㵌⍎㑐㹒ご㥖ⵘ㹚㥜罞ᅠᅢ੤ᝦ౨ᥪᥬ᙮彰", a_));
			}
			set
			{
				int a_ = 0;
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				throw new NotImplementedException(RecordTableEnumerator.b("砵圷丹᰻圽ⴿ㉁⡃⍅╇⽉≋㩍㕏㙑瑓♕⩗㕙ⱛ㭝቟ᙡᵣ䡥", a_));
			}
		}

		// Token: 0x17000FBE RID: 4030
		// (get) Token: 0x06006043 RID: 24643 RVA: 0x003CD53C File Offset: 0x003CC53C
		public int ExtendedFormatIndex
		{
			get
			{
				switch (0)
				{
				default:
				{
					int extendedFormatIndex;
					for (;;)
					{
						IL_37:
						int count = this.ᜀ.Count;
						int num;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							IL_C9:
							goto IL_7A;
						default:
							if (false)
							{
							}
							num = 2;
							break;
						}
						for (;;)
						{
							IL_10:
							switch (num)
							{
							case 0:
							{
								int num2;
								if (num2 >= count)
								{
									num = 5;
									continue;
								}
								IExtendIndex extendIndex = (IExtendIndex)((XlsRange)this.ᜀ[num2]).Style;
								num = 6;
								continue;
							}
							case 1:
								goto IL_C9;
							case 2:
								if (count > 0)
								{
									num = 7;
									continue;
								}
								return int.MinValue;
							case 3:
								return int.MinValue;
							case 4:
								goto IL_131;
							case 5:
								goto IL_90;
							case 6:
							{
								IExtendIndex extendIndex;
								if (extendedFormatIndex != extendIndex.ExtendedFormatIndex)
								{
									num = 3;
									continue;
								}
								int num2;
								num2++;
								num = 4;
								continue;
							}
							case 7:
							{
								IExtendIndex extendIndex = (IExtendIndex)((XlsRange)this.ᜀ[0]).Style;
								extendedFormatIndex = extendIndex.ExtendedFormatIndex;
								int num2 = 1;
								num = 1;
								continue;
							}
							}
							goto IL_37;
						}
						IL_131:
						IL_7A:
						num = 0;
						goto IL_10;
					}
					IL_90:
					if (true)
					{
					}
					return extendedFormatIndex;
				}
				}
			}
		}

		// Token: 0x17000FBF RID: 4031
		// (get) Token: 0x06006044 RID: 24644 RVA: 0x003CD684 File Offset: 0x003CC684
		public IBorders Borders
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
				return new BordersCollectionArrayWrapper((IXLSRange)base.Parent);
			}
		}

		// Token: 0x17000FC0 RID: 4032
		// (get) Token: 0x06006045 RID: 24645 RVA: 0x003CD6D0 File Offset: 0x003CC6D0
		public bool BuiltIn
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						bool flag = false;
						bool flag2 = true;
						int num = 0;
						int count = this.ᜀ.Count;
						int num2 = 2;
						for (;;)
						{
							switch (num2)
							{
							case 0:
							{
								IXLSRange ixlsrange;
								flag = ixlsrange.Style.BuiltIn;
								flag2 = false;
								goto IL_119;
							}
							case 1:
								if (flag2)
								{
									num2 = 0;
									continue;
								}
								num2 = 7;
								continue;
							case 2:
								goto IL_E2;
							case 3:
								if (true)
								{
								}
								goto IL_E2;
							case 4:
								goto IL_5B;
							case 5:
								return false;
							case 6:
								if (num >= count)
								{
									num2 = 8;
									continue;
								}
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_119;
								default:
								{
									if (false)
									{
									}
									IXLSRange ixlsrange = this.ᜀ[num];
									num2 = 1;
									continue;
								}
								}
								break;
							case 7:
							{
								IXLSRange ixlsrange;
								if (ixlsrange.Style.BuiltIn != flag)
								{
									num2 = 5;
									continue;
								}
								goto IL_5B;
							}
							case 8:
								return flag;
							}
							break;
							IL_5B:
							num++;
							num2 = 3;
							continue;
							IL_E2:
							num2 = 6;
							continue;
							IL_119:
							num2 = 4;
						}
					}
					return false;
				}
			}
		}

		// Token: 0x17000FC1 RID: 4033
		// (get) Token: 0x06006046 RID: 24646 RVA: 0x003CD80C File Offset: 0x003CC80C
		// (set) Token: 0x06006047 RID: 24647 RVA: 0x003CD944 File Offset: 0x003CC944
		public ExcelPatternType FillPattern
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						ExcelPatternType excelPatternType = ExcelPatternType.None;
						bool flag = true;
						int num = 0;
						int count = this.ᜀ.Count;
						int num2 = 7;
						for (;;)
						{
							switch (num2)
							{
							case 0:
							{
								IXLSRange ixlsrange;
								if (ixlsrange.Style.FillPattern != excelPatternType)
								{
									num2 = 8;
									continue;
								}
								goto IL_5B;
							}
							case 1:
								return excelPatternType;
							case 2:
								if (num >= count)
								{
									num2 = 1;
									continue;
								}
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_116;
								default:
								{
									if (false)
									{
									}
									IXLSRange ixlsrange = this.ᜀ[num];
									num2 = 5;
									continue;
								}
								}
								break;
							case 3:
								goto IL_5B;
							case 4:
								goto IL_DF;
							case 5:
								if (true)
								{
								}
								if (flag)
								{
									num2 = 6;
									continue;
								}
								num2 = 0;
								continue;
							case 6:
							{
								IXLSRange ixlsrange;
								excelPatternType = ixlsrange.Style.FillPattern;
								flag = false;
								goto IL_116;
							}
							case 7:
								goto IL_DF;
							case 8:
								return ExcelPatternType.None;
							}
							break;
							IL_5B:
							num++;
							num2 = 4;
							continue;
							IL_DF:
							num2 = 2;
							continue;
							IL_116:
							num2 = 3;
						}
					}
					return ExcelPatternType.None;
				}
			}
			set
			{
				for (;;)
				{
					for (;;)
					{
						int num = 0;
						int count = this.ᜀ.Count;
						if (true)
						{
						}
						int num2 = 0;
						for (;;)
						{
							switch (num2)
							{
							case 0:
								goto IL_38;
							case 1:
								return;
							case 2:
							{
								if (num >= count)
								{
									num2 = 1;
									continue;
								}
								IXLSRange ixlsrange = this.ᜀ[num];
								ixlsrange.Style.FillPattern = value;
								num++;
								num2 = 3;
								continue;
							}
							case 3:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									break;
								default:
									if (false)
									{
									}
									goto IL_38;
								}
								break;
							}
							break;
							IL_38:
							num2 = 2;
						}
					}
				}
			}
		}

		// Token: 0x17000FC2 RID: 4034
		// (get) Token: 0x06006048 RID: 24648 RVA: 0x003CD9EC File Offset: 0x003CC9EC
		// (set) Token: 0x06006049 RID: 24649 RVA: 0x003CDB20 File Offset: 0x003CCB20
		public ExcelColors FillBackground
		{
			get
			{
				switch (0)
				{
				default:
				{
					ExcelColors excelColors;
					for (;;)
					{
						excelColors = ExcelColors.Black;
						bool flag = true;
						int num = 0;
						int count = this.ᜀ.Count;
						int num2 = 7;
						for (;;)
						{
							switch (num2)
							{
							case 0:
							{
								IXLSRange ixlsrange;
								if (ixlsrange.Style.KnownColor != excelColors)
								{
									num2 = 8;
									continue;
								}
								goto IL_58;
							}
							case 1:
								goto IL_58;
							case 2:
							{
								IXLSRange ixlsrange;
								excelColors = ixlsrange.Style.KnownColor;
								flag = false;
								goto IL_113;
							}
							case 3:
								if (num >= count)
								{
									num2 = 4;
									continue;
								}
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_113;
								default:
								{
									if (false)
									{
									}
									IXLSRange ixlsrange = this.ᜀ[num];
									num2 = 6;
									continue;
								}
								}
								break;
							case 4:
								goto IL_FA;
							case 5:
								goto IL_D4;
							case 6:
								if (flag)
								{
									num2 = 2;
									continue;
								}
								num2 = 0;
								continue;
							case 7:
								goto IL_D4;
							case 8:
								return ExcelColors.Black;
							}
							break;
							IL_58:
							num++;
							num2 = 5;
							continue;
							IL_D4:
							num2 = 3;
							continue;
							IL_113:
							num2 = 1;
						}
					}
					return ExcelColors.Black;
					IL_FA:
					if (true)
					{
					}
					return excelColors;
				}
				}
			}
			set
			{
				for (;;)
				{
					for (;;)
					{
						int num = 0;
						int count = this.ᜀ.Count;
						int num2 = 2;
						for (;;)
						{
							if (true)
							{
							}
							switch (num2)
							{
							case 0:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									break;
								default:
									if (false)
									{
									}
									goto IL_38;
								}
								break;
							case 1:
								return;
							case 2:
								goto IL_38;
							case 3:
							{
								if (num >= count)
								{
									num2 = 1;
									continue;
								}
								IXLSRange ixlsrange = this.ᜀ[num];
								ixlsrange.Style.KnownColor = value;
								num++;
								num2 = 0;
								continue;
							}
							}
							break;
							IL_38:
							num2 = 3;
						}
					}
				}
			}
		}

		// Token: 0x17000FC3 RID: 4035
		// (get) Token: 0x0600604A RID: 24650 RVA: 0x003CDBC8 File Offset: 0x003CCBC8
		// (set) Token: 0x0600604B RID: 24651 RVA: 0x003CDC14 File Offset: 0x003CCC14
		public Color FillBackgroundRGB
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
				return this.ᜁ.GetPaletteColor(this.FillBackground);
			}
			set
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
				this.FillBackground = this.ᜁ.GetNearestColor(value);
			}
		}

		// Token: 0x17000FC4 RID: 4036
		// (get) Token: 0x0600604C RID: 24652 RVA: 0x003CDC64 File Offset: 0x003CCC64
		// (set) Token: 0x0600604D RID: 24653 RVA: 0x003CDDA0 File Offset: 0x003CCDA0
		public ExcelColors FillForeground
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						ExcelColors excelColors = ExcelColors.Black;
						bool flag = true;
						int num = 0;
						int count = this.ᜀ.Count;
						int num2 = 2;
						for (;;)
						{
							switch (num2)
							{
							case 0:
								if (num >= count)
								{
									num2 = 7;
									continue;
								}
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_119;
								default:
								{
									if (false)
									{
									}
									IXLSRange ixlsrange = this.ᜀ[num];
									num2 = 3;
									continue;
								}
								}
								break;
							case 1:
							{
								IXLSRange ixlsrange;
								excelColors = ixlsrange.Style.PatternKnownColor;
								flag = false;
								goto IL_119;
							}
							case 2:
								goto IL_E2;
							case 3:
								if (flag)
								{
									num2 = 1;
									continue;
								}
								if (true)
								{
								}
								num2 = 6;
								continue;
							case 4:
								goto IL_5B;
							case 5:
								return ExcelColors.Black;
							case 6:
							{
								IXLSRange ixlsrange;
								if (ixlsrange.Style.PatternKnownColor != excelColors)
								{
									num2 = 5;
									continue;
								}
								goto IL_5B;
							}
							case 7:
								return excelColors;
							case 8:
								goto IL_E2;
							}
							break;
							IL_5B:
							num++;
							num2 = 8;
							continue;
							IL_E2:
							num2 = 0;
							continue;
							IL_119:
							num2 = 4;
						}
					}
					return ExcelColors.Black;
				}
			}
			set
			{
				for (;;)
				{
					for (;;)
					{
						int num = 0;
						int count = this.ᜀ.Count;
						int num2 = 0;
						for (;;)
						{
							switch (num2)
							{
							case 0:
								goto IL_30;
							case 1:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									break;
								default:
									if (false)
									{
									}
									goto IL_30;
								}
								break;
							case 2:
							{
								if (true)
								{
								}
								if (num >= count)
								{
									num2 = 3;
									continue;
								}
								IXLSRange ixlsrange = this.ᜀ[num];
								ixlsrange.Style.PatternKnownColor = value;
								num++;
								num2 = 1;
								continue;
							}
							case 3:
								return;
							}
							break;
							IL_30:
							num2 = 2;
						}
					}
				}
			}
		}

		// Token: 0x17000FC5 RID: 4037
		// (get) Token: 0x0600604E RID: 24654 RVA: 0x003CDE48 File Offset: 0x003CCE48
		// (set) Token: 0x0600604F RID: 24655 RVA: 0x003CDE94 File Offset: 0x003CCE94
		public Color FillForegroundRGB
		{
			get
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
				return this.ᜁ.GetPaletteColor(this.FillForeground);
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
				this.FillForeground = this.ᜁ.GetNearestColor(value);
			}
		}

		// Token: 0x17000FC6 RID: 4038
		// (get) Token: 0x06006050 RID: 24656 RVA: 0x003CDEE4 File Offset: 0x003CCEE4
		public IFont Font
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						IFont font = null;
						bool flag = true;
						int num = 0;
						int count = this.ᜀ.Count;
						int num2 = 0;
						for (;;)
						{
							switch (num2)
							{
							case 0:
								goto IL_E2;
							case 1:
							{
								if (true)
								{
								}
								IXLSRange ixlsrange;
								if (ixlsrange.Style.Font != font)
								{
									num2 = 3;
									continue;
								}
								goto IL_5B;
							}
							case 2:
								if (flag)
								{
									num2 = 8;
									continue;
								}
								num2 = 1;
								continue;
							case 3:
								goto IL_96;
							case 4:
								goto IL_5B;
							case 5:
								goto IL_E2;
							case 6:
								return font;
							case 7:
								if (num >= count)
								{
									num2 = 6;
									continue;
								}
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_119;
								default:
								{
									if (false)
									{
									}
									IXLSRange ixlsrange = this.ᜀ[num];
									num2 = 2;
									continue;
								}
								}
								break;
							case 8:
							{
								IXLSRange ixlsrange;
								font = ixlsrange.Style.Font;
								flag = false;
								goto IL_119;
							}
							}
							break;
							IL_5B:
							num++;
							num2 = 5;
							continue;
							IL_E2:
							num2 = 7;
							continue;
							IL_119:
							num2 = 4;
						}
					}
					IL_96:
					return new FontArrayWrapper((IXLSRange)base.Parent);
				}
			}
		}

		// Token: 0x17000FC7 RID: 4039
		// (get) Token: 0x06006051 RID: 24657 RVA: 0x003CE030 File Offset: 0x003CD030
		// (set) Token: 0x06006052 RID: 24658 RVA: 0x003CE164 File Offset: 0x003CD164
		public bool FormulaHidden
		{
			get
			{
				switch (0)
				{
				default:
				{
					bool flag;
					for (;;)
					{
						flag = false;
						bool flag2 = true;
						int num = 0;
						int count = this.ᜀ.Count;
						int num2 = 5;
						for (;;)
						{
							switch (num2)
							{
							case 0:
								return false;
							case 1:
							{
								IXLSRange ixlsrange;
								if (ixlsrange.Style.FormulaHidden != flag)
								{
									num2 = 0;
									continue;
								}
								goto IL_58;
							}
							case 2:
								goto IL_FA;
							case 3:
								goto IL_D4;
							case 4:
								goto IL_58;
							case 5:
								goto IL_D4;
							case 6:
								if (flag2)
								{
									num2 = 8;
									continue;
								}
								num2 = 1;
								continue;
							case 7:
								if (num >= count)
								{
									num2 = 2;
									continue;
								}
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_10B;
								default:
								{
									if (false)
									{
									}
									IXLSRange ixlsrange = this.ᜀ[num];
									num2 = 6;
									continue;
								}
								}
								break;
							case 8:
							{
								IXLSRange ixlsrange;
								flag = ixlsrange.Style.FormulaHidden;
								flag2 = false;
								goto IL_10B;
							}
							}
							break;
							IL_58:
							num++;
							num2 = 3;
							continue;
							IL_D4:
							num2 = 7;
							continue;
							IL_10B:
							num2 = 4;
						}
					}
					return false;
					IL_FA:
					if (true)
					{
					}
					return flag;
				}
				}
			}
			set
			{
				for (;;)
				{
					for (;;)
					{
						if (true)
						{
						}
						int num = 0;
						int count = this.ᜀ.Count;
						int num2 = 3;
						for (;;)
						{
							switch (num2)
							{
							case 0:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									break;
								default:
									if (false)
									{
									}
									goto IL_38;
								}
								break;
							case 1:
								return;
							case 2:
							{
								if (num >= count)
								{
									num2 = 1;
									continue;
								}
								IXLSRange ixlsrange = this.ᜀ[num];
								ixlsrange.Style.FormulaHidden = value;
								num++;
								num2 = 0;
								continue;
							}
							case 3:
								goto IL_38;
							}
							break;
							IL_38:
							num2 = 2;
						}
					}
				}
			}
		}

		// Token: 0x17000FC8 RID: 4040
		// (get) Token: 0x06006053 RID: 24659 RVA: 0x003CE20C File Offset: 0x003CD20C
		// (set) Token: 0x06006054 RID: 24660 RVA: 0x003CE344 File Offset: 0x003CD344
		public HorizontalAlignType HorizontalAlignment
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						HorizontalAlignType horizontalAlignType = HorizontalAlignType.General;
						bool flag = true;
						int num = 0;
						int count = this.ᜀ.Count;
						int num2 = 2;
						for (;;)
						{
							switch (num2)
							{
							case 0:
								if (num >= count)
								{
									num2 = 8;
									continue;
								}
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_116;
								default:
								{
									if (false)
									{
									}
									IXLSRange ixlsrange = this.ᜀ[num];
									num2 = 3;
									continue;
								}
								}
								break;
							case 1:
								return HorizontalAlignType.General;
							case 2:
								goto IL_DF;
							case 3:
								if (true)
								{
								}
								if (flag)
								{
									num2 = 4;
									continue;
								}
								num2 = 7;
								continue;
							case 4:
							{
								IXLSRange ixlsrange;
								horizontalAlignType = ixlsrange.Style.HorizontalAlignment;
								flag = false;
								goto IL_116;
							}
							case 5:
								goto IL_DF;
							case 6:
								goto IL_5B;
							case 7:
							{
								IXLSRange ixlsrange;
								if (ixlsrange.Style.HorizontalAlignment != horizontalAlignType)
								{
									num2 = 1;
									continue;
								}
								goto IL_5B;
							}
							case 8:
								return horizontalAlignType;
							}
							break;
							IL_5B:
							num++;
							num2 = 5;
							continue;
							IL_DF:
							num2 = 0;
							continue;
							IL_116:
							num2 = 6;
						}
					}
					return HorizontalAlignType.General;
				}
			}
			set
			{
				for (;;)
				{
					int num = 0;
					int count = this.ᜀ.Count;
					int num2 = 1;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_30;
						case 1:
							goto IL_30;
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
							{
								if (false)
								{
								}
								if (num >= count)
								{
									num2 = 3;
									continue;
								}
								IXLSRange ixlsrange = this.ᜀ[num];
								ixlsrange.Style.HorizontalAlignment = value;
								num++;
								if (true)
								{
								}
								num2 = 0;
								continue;
							}
							}
							break;
						case 3:
							return;
						}
						break;
						IL_30:
						num2 = 2;
					}
				}
			}
		}

		// Token: 0x17000FC9 RID: 4041
		// (get) Token: 0x06006055 RID: 24661 RVA: 0x003CE3F0 File Offset: 0x003CD3F0
		// (set) Token: 0x06006056 RID: 24662 RVA: 0x003CE52C File Offset: 0x003CD52C
		public bool IncludeAlignment
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						bool flag = false;
						bool flag2 = true;
						int num = 0;
						int count = this.ᜀ.Count;
						int num2 = 6;
						for (;;)
						{
							if (true)
							{
							}
							switch (num2)
							{
							case 0:
								return flag;
							case 1:
							{
								if (num >= count)
								{
									num2 = 0;
									continue;
								}
								IXLSRange ixlsrange = this.ᜀ[num];
								num2 = 3;
								continue;
							}
							case 2:
								goto IL_86;
							case 3:
								if (flag2)
								{
									num2 = 4;
									continue;
								}
								num2 = 5;
								continue;
							case 4:
							{
								IXLSRange ixlsrange;
								flag = ixlsrange.Style.IncludeAlignment;
								flag2 = false;
								num2 = 2;
								continue;
							}
							case 5:
							{
								IXLSRange ixlsrange;
								if (ixlsrange.Style.IncludeAlignment != flag)
								{
									num2 = 7;
									continue;
								}
								goto IL_86;
							}
							case 6:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									return false;
								}
								if (false)
								{
								}
								goto IL_EC;
							case 7:
								goto IL_BF;
							case 8:
								goto IL_EC;
							}
							break;
							IL_86:
							num++;
							num2 = 8;
							continue;
							IL_EC:
							num2 = 1;
						}
					}
					IL_BF:
					return false;
				}
			}
			set
			{
				for (;;)
				{
					int num = 0;
					int count = this.ᜀ.Count;
					int num2 = 0;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_30;
						case 1:
							return;
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
							{
								if (false)
								{
								}
								if (num >= count)
								{
									num2 = 1;
									continue;
								}
								if (true)
								{
								}
								IXLSRange ixlsrange = this.ᜀ[num];
								ixlsrange.Style.IncludeAlignment = value;
								num++;
								num2 = 3;
								continue;
							}
							}
							break;
						case 3:
							goto IL_30;
						}
						break;
						IL_30:
						num2 = 2;
					}
				}
			}
		}

		// Token: 0x17000FCA RID: 4042
		// (get) Token: 0x06006057 RID: 24663 RVA: 0x003CE5D8 File Offset: 0x003CD5D8
		// (set) Token: 0x06006058 RID: 24664 RVA: 0x003CE710 File Offset: 0x003CD710
		public bool IncludeBorder
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						bool flag = false;
						bool flag2 = true;
						int num = 0;
						int count = this.ᜀ.Count;
						int num2 = 7;
						for (;;)
						{
							switch (num2)
							{
							case 0:
								if (flag2)
								{
									num2 = 5;
									continue;
								}
								num2 = 4;
								continue;
							case 1:
								return flag;
							case 2:
								goto IL_E1;
							case 3:
								goto IL_7E;
							case 4:
							{
								IXLSRange ixlsrange;
								if (ixlsrange.Style.IncludeBorder != flag)
								{
									num2 = 8;
									continue;
								}
								goto IL_7E;
							}
							case 5:
							{
								IXLSRange ixlsrange;
								flag = ixlsrange.Style.IncludeBorder;
								flag2 = false;
								num2 = 3;
								continue;
							}
							case 6:
							{
								if (num >= count)
								{
									num2 = 1;
									continue;
								}
								IXLSRange ixlsrange = this.ᜀ[num];
								num2 = 0;
								continue;
							}
							case 7:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_11F;
								default:
									if (false)
									{
									}
									goto IL_E1;
								}
								break;
							case 8:
								goto IL_B4;
							}
							break;
							IL_7E:
							num++;
							num2 = 2;
							continue;
							IL_E1:
							num2 = 6;
						}
					}
					IL_B4:
					IL_11F:
					if (true)
					{
					}
					return false;
				}
			}
			set
			{
				for (;;)
				{
					int num = 0;
					int count = this.ᜀ.Count;
					int num2 = 0;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_30;
						case 1:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
							{
								if (false)
								{
								}
								if (num >= count)
								{
									num2 = 3;
									continue;
								}
								IXLSRange ixlsrange = this.ᜀ[num];
								ixlsrange.Style.IncludeBorder = value;
								num++;
								if (true)
								{
								}
								num2 = 2;
								continue;
							}
							}
							break;
						case 2:
							goto IL_30;
						case 3:
							return;
						}
						break;
						IL_30:
						num2 = 1;
					}
				}
			}
		}

		// Token: 0x17000FCB RID: 4043
		// (get) Token: 0x06006059 RID: 24665 RVA: 0x003CE7BC File Offset: 0x003CD7BC
		// (set) Token: 0x0600605A RID: 24666 RVA: 0x003CE8F8 File Offset: 0x003CD8F8
		public bool IncludeFont
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						bool flag = false;
						bool flag2 = true;
						int num = 0;
						int count = this.ᜀ.Count;
						int num2 = 0;
						for (;;)
						{
							switch (num2)
							{
							case 0:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									return false;
								default:
									if (true)
									{
									}
									if (false)
									{
									}
									goto IL_EC;
								}
								break;
							case 1:
								goto IL_BF;
							case 2:
								return flag;
							case 3:
								if (flag2)
								{
									num2 = 4;
									continue;
								}
								num2 = 5;
								continue;
							case 4:
							{
								IXLSRange ixlsrange;
								flag = ixlsrange.Style.IncludeFont;
								flag2 = false;
								num2 = 6;
								continue;
							}
							case 5:
							{
								IXLSRange ixlsrange;
								if (ixlsrange.Style.IncludeFont != flag)
								{
									num2 = 1;
									continue;
								}
								goto IL_86;
							}
							case 6:
								goto IL_86;
							case 7:
								goto IL_EC;
							case 8:
							{
								if (num >= count)
								{
									num2 = 2;
									continue;
								}
								IXLSRange ixlsrange = this.ᜀ[num];
								num2 = 3;
								continue;
							}
							}
							break;
							IL_86:
							num++;
							num2 = 7;
							continue;
							IL_EC:
							num2 = 8;
						}
					}
					IL_BF:
					return false;
				}
			}
			set
			{
				for (;;)
				{
					int num = 0;
					int count = this.ᜀ.Count;
					int num2 = 2;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_60;
						case 1:
							goto IL_30;
						case 2:
							goto IL_30;
						case 3:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
							{
								if (false)
								{
								}
								if (num >= count)
								{
									num2 = 0;
									continue;
								}
								IXLSRange ixlsrange = this.ᜀ[num];
								ixlsrange.Style.IncludeFont = value;
								num++;
								num2 = 1;
								continue;
							}
							}
							break;
						}
						break;
						IL_30:
						num2 = 3;
					}
				}
				IL_60:
				if (true)
				{
				}
			}
		}

		// Token: 0x17000FCC RID: 4044
		// (get) Token: 0x0600605B RID: 24667 RVA: 0x003CE9A4 File Offset: 0x003CD9A4
		// (set) Token: 0x0600605C RID: 24668 RVA: 0x003CEADC File Offset: 0x003CDADC
		public bool IncludeNumberFormat
		{
			get
			{
				switch (0)
				{
				default:
				{
					bool flag;
					for (;;)
					{
						flag = false;
						bool flag2 = true;
						int num = 0;
						int count = this.ᜀ.Count;
						int num2 = 5;
						for (;;)
						{
							switch (num2)
							{
							case 0:
								goto IL_FD;
							case 1:
							{
								IXLSRange ixlsrange;
								if (ixlsrange.Style.IncludeNumberFormat != flag)
								{
									num2 = 7;
									continue;
								}
								goto IL_7E;
							}
							case 2:
								goto IL_7E;
							case 3:
								if (flag2)
								{
									num2 = 8;
									continue;
								}
								num2 = 1;
								continue;
							case 4:
								goto IL_E1;
							case 5:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									return false;
								default:
									if (false)
									{
									}
									goto IL_E1;
								}
								break;
							case 6:
							{
								if (num >= count)
								{
									num2 = 0;
									continue;
								}
								IXLSRange ixlsrange = this.ᜀ[num];
								num2 = 3;
								continue;
							}
							case 7:
								goto IL_B4;
							case 8:
							{
								IXLSRange ixlsrange;
								flag = ixlsrange.Style.IncludeNumberFormat;
								flag2 = false;
								num2 = 2;
								continue;
							}
							}
							break;
							IL_7E:
							num++;
							num2 = 4;
							continue;
							IL_E1:
							num2 = 6;
						}
					}
					IL_B4:
					return false;
					IL_FD:
					if (true)
					{
					}
					return flag;
				}
				}
			}
			set
			{
				for (;;)
				{
					if (true)
					{
					}
					int num = 0;
					int count = this.ᜀ.Count;
					int num2 = 1;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							return;
						case 1:
							goto IL_38;
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
							{
								if (false)
								{
								}
								if (num >= count)
								{
									num2 = 0;
									continue;
								}
								IXLSRange ixlsrange = this.ᜀ[num];
								ixlsrange.Style.IncludeNumberFormat = value;
								num++;
								num2 = 3;
								continue;
							}
							}
							break;
						case 3:
							goto IL_38;
						}
						break;
						IL_38:
						num2 = 2;
					}
				}
			}
		}

		// Token: 0x17000FCD RID: 4045
		// (get) Token: 0x0600605D RID: 24669 RVA: 0x003CEB88 File Offset: 0x003CDB88
		// (set) Token: 0x0600605E RID: 24670 RVA: 0x003CECC0 File Offset: 0x003CDCC0
		public bool IncludePatterns
		{
			get
			{
				switch (0)
				{
				default:
					if (true)
					{
					}
					for (;;)
					{
						bool flag = false;
						bool flag2 = true;
						int num = 0;
						int count = this.ᜀ.Count;
						int num2 = 7;
						for (;;)
						{
							switch (num2)
							{
							case 0:
								return flag;
							case 1:
								if (flag2)
								{
									num2 = 4;
									continue;
								}
								num2 = 2;
								continue;
							case 2:
							{
								IXLSRange ixlsrange;
								if (ixlsrange.Style.IncludePatterns != flag)
								{
									num2 = 5;
									continue;
								}
								goto IL_86;
							}
							case 3:
								goto IL_E9;
							case 4:
							{
								IXLSRange ixlsrange;
								flag = ixlsrange.Style.IncludePatterns;
								flag2 = false;
								num2 = 8;
								continue;
							}
							case 5:
								goto IL_BC;
							case 6:
							{
								if (num >= count)
								{
									num2 = 0;
									continue;
								}
								IXLSRange ixlsrange = this.ᜀ[num];
								num2 = 1;
								continue;
							}
							case 7:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									return false;
								}
								if (false)
								{
								}
								goto IL_E9;
							case 8:
								goto IL_86;
							}
							break;
							IL_86:
							num++;
							num2 = 3;
							continue;
							IL_E9:
							num2 = 6;
						}
					}
					IL_BC:
					return false;
				}
			}
			set
			{
				for (;;)
				{
					int num = 0;
					int count = this.ᜀ.Count;
					int num2 = 1;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
							{
								if (false)
								{
								}
								if (num >= count)
								{
									if (true)
									{
									}
									num2 = 2;
									continue;
								}
								IXLSRange ixlsrange = this.ᜀ[num];
								ixlsrange.Style.IncludePatterns = value;
								num++;
								num2 = 3;
								continue;
							}
							}
							break;
						case 1:
							goto IL_30;
						case 2:
							return;
						case 3:
							goto IL_30;
						}
						break;
						IL_30:
						num2 = 0;
					}
				}
			}
		}

		// Token: 0x17000FCE RID: 4046
		// (get) Token: 0x0600605F RID: 24671 RVA: 0x003CED6C File Offset: 0x003CDD6C
		// (set) Token: 0x06006060 RID: 24672 RVA: 0x003CEEA8 File Offset: 0x003CDEA8
		public bool IncludeProtection
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						bool flag = false;
						bool flag2 = true;
						int num = 0;
						int count = this.ᜀ.Count;
						int num2 = 3;
						for (;;)
						{
							switch (num2)
							{
							case 0:
							{
								IXLSRange ixlsrange;
								if (ixlsrange.Style.IncludeProtection != flag)
								{
									num2 = 7;
									continue;
								}
								goto IL_86;
							}
							case 1:
							{
								if (num >= count)
								{
									num2 = 2;
									continue;
								}
								IXLSRange ixlsrange = this.ᜀ[num];
								num2 = 6;
								continue;
							}
							case 2:
								return flag;
							case 3:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									return false;
								default:
									if (true)
									{
									}
									if (false)
									{
									}
									goto IL_EC;
								}
								break;
							case 4:
								goto IL_EC;
							case 5:
								goto IL_86;
							case 6:
								if (flag2)
								{
									num2 = 8;
									continue;
								}
								num2 = 0;
								continue;
							case 7:
								goto IL_BF;
							case 8:
							{
								IXLSRange ixlsrange;
								flag = ixlsrange.Style.IncludeProtection;
								flag2 = false;
								num2 = 5;
								continue;
							}
							}
							break;
							IL_86:
							num++;
							num2 = 4;
							continue;
							IL_EC:
							num2 = 1;
						}
					}
					IL_BF:
					return false;
				}
			}
			set
			{
				for (;;)
				{
					int num = 0;
					int count = this.ᜀ.Count;
					int num2 = 2;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
							{
								if (true)
								{
								}
								if (false)
								{
								}
								if (num >= count)
								{
									num2 = 3;
									continue;
								}
								IXLSRange ixlsrange = this.ᜀ[num];
								ixlsrange.Style.IncludeProtection = value;
								num++;
								num2 = 1;
								continue;
							}
							}
							break;
						case 1:
							goto IL_30;
						case 2:
							goto IL_30;
						case 3:
							return;
						}
						break;
						IL_30:
						num2 = 0;
					}
				}
			}
		}

		// Token: 0x17000FCF RID: 4047
		// (get) Token: 0x06006061 RID: 24673 RVA: 0x003CEF54 File Offset: 0x003CDF54
		// (set) Token: 0x06006062 RID: 24674 RVA: 0x003CF08C File Offset: 0x003CE08C
		public int IndentLevel
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						int num = 0;
						bool flag = true;
						int num2 = 0;
						int count = this.ᜀ.Count;
						int num3 = 8;
						for (;;)
						{
							switch (num3)
							{
							case 0:
								goto IL_E9;
							case 1:
								if (flag)
								{
									num3 = 4;
									continue;
								}
								num3 = 2;
								continue;
							case 2:
							{
								IXLSRange ixlsrange;
								if (ixlsrange.Style.IndentLevel != num)
								{
									num3 = 6;
									continue;
								}
								goto IL_7E;
							}
							case 3:
							{
								if (num2 >= count)
								{
									num3 = 5;
									continue;
								}
								if (true)
								{
								}
								IXLSRange ixlsrange = this.ᜀ[num2];
								num3 = 1;
								continue;
							}
							case 4:
							{
								IXLSRange ixlsrange;
								num = ixlsrange.Style.IndentLevel;
								flag = false;
								num3 = 7;
								continue;
							}
							case 5:
								return num;
							case 6:
								goto IL_B4;
							case 7:
								goto IL_7E;
							case 8:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									return 0;
								default:
									if (false)
									{
									}
									goto IL_E9;
								}
								break;
							}
							break;
							IL_7E:
							num2++;
							num3 = 0;
							continue;
							IL_E9:
							num3 = 3;
						}
					}
					IL_B4:
					return 0;
				}
			}
			set
			{
				for (;;)
				{
					if (true)
					{
					}
					int num = 0;
					int count = this.ᜀ.Count;
					int num2 = 2;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							return;
						case 1:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
							{
								if (false)
								{
								}
								if (num >= count)
								{
									num2 = 0;
									continue;
								}
								IXLSRange ixlsrange = this.ᜀ[num];
								ixlsrange.Style.IndentLevel = value;
								num++;
								num2 = 3;
								continue;
							}
							}
							break;
						case 2:
							goto IL_38;
						case 3:
							goto IL_38;
						}
						break;
						IL_38:
						num2 = 1;
					}
				}
			}
		}

		// Token: 0x17000FD0 RID: 4048
		// (get) Token: 0x06006063 RID: 24675 RVA: 0x003CF138 File Offset: 0x003CE138
		public bool IsInitialized
		{
			get
			{
				switch (0)
				{
				default:
				{
					bool hasStyle;
					for (;;)
					{
						hasStyle = ((XlsRange)this.ᜀ[0]).HasStyle;
						int num = 0;
						int count = this.ᜀ.Count;
						int num2 = 5;
						for (;;)
						{
							switch (num2)
							{
							case 0:
							{
								XlsRange xlsRange;
								if (xlsRange.HasStyle != hasStyle)
								{
									num2 = 2;
									continue;
								}
								num++;
								num2 = 1;
								continue;
							}
							case 1:
								goto IL_D3;
							case 2:
								goto IL_D1;
							case 3:
							{
								if (num >= count)
								{
									num2 = 4;
									continue;
								}
								XlsRange xlsRange = this.ᜀ[num] as XlsRange;
								num2 = 0;
								continue;
							}
							case 4:
								goto IL_EF;
							case 5:
								goto IL_D3;
							}
							break;
							IL_D3:
							num2 = 3;
						}
					}
					IL_D1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return hasStyle;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						return false;
					}
					IL_EF:
					return hasStyle;
				}
				}
			}
		}

		// Token: 0x17000FD1 RID: 4049
		// (get) Token: 0x06006064 RID: 24676 RVA: 0x003CF238 File Offset: 0x003CE238
		// (set) Token: 0x06006065 RID: 24677 RVA: 0x003CF36C File Offset: 0x003CE36C
		public bool Locked
		{
			get
			{
				if (true)
				{
				}
				switch (0)
				{
				default:
					for (;;)
					{
						bool flag = false;
						bool flag2 = true;
						int num = 0;
						int count = this.ᜀ.Count;
						int num2 = 7;
						for (;;)
						{
							switch (num2)
							{
							case 0:
								return flag;
							case 1:
							{
								IXLSRange ixlsrange;
								if (ixlsrange.Style.Locked != flag)
								{
									num2 = 3;
									continue;
								}
								goto IL_60;
							}
							case 2:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									return flag;
								default:
								{
									if (false)
									{
									}
									IXLSRange ixlsrange;
									flag = ixlsrange.Style.Locked;
									flag2 = false;
									num2 = 4;
									continue;
								}
								}
								break;
							case 3:
								return false;
							case 4:
								goto IL_60;
							case 5:
							{
								if (num >= count)
								{
									num2 = 0;
									continue;
								}
								IXLSRange ixlsrange = this.ᜀ[num];
								num2 = 8;
								continue;
							}
							case 6:
								goto IL_C0;
							case 7:
								goto IL_C0;
							case 8:
								if (flag2)
								{
									num2 = 2;
									continue;
								}
								num2 = 1;
								continue;
							}
							break;
							IL_60:
							num++;
							num2 = 6;
							continue;
							IL_C0:
							num2 = 5;
						}
					}
					return false;
				}
			}
			set
			{
				for (;;)
				{
					int num = 0;
					int count = this.ᜀ.Count;
					int num2 = 1;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_38;
						case 1:
							if (true)
							{
							}
							goto IL_38;
						case 2:
							if (num >= count)
							{
								num2 = 3;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								continue;
							default:
							{
								if (false)
								{
								}
								IXLSRange ixlsrange = this.ᜀ[num];
								ixlsrange.Style.Locked = value;
								num++;
								num2 = 0;
								continue;
							}
							}
							break;
						case 3:
							return;
						}
						break;
						IL_38:
						num2 = 2;
					}
				}
			}
		}

		// Token: 0x17000FD2 RID: 4050
		// (get) Token: 0x06006066 RID: 24678 RVA: 0x003CF418 File Offset: 0x003CE418
		public string Name
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						string text = null;
						bool flag = true;
						int num = 0;
						int count = this.ᜀ.Count;
						int num2 = 3;
						for (;;)
						{
							switch (num2)
							{
							case 0:
								goto IL_BD;
							case 1:
								return text;
							case 2:
								goto IL_8D;
							case 3:
								goto IL_BD;
							case 4:
								if (flag)
								{
									num2 = 7;
									continue;
								}
								num2 = 6;
								continue;
							case 5:
								goto IL_58;
							case 6:
							{
								IXLSRange ixlsrange;
								if (ixlsrange.Style.Name != text)
								{
									num2 = 2;
									continue;
								}
								goto IL_58;
							}
							case 7:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									return text;
								default:
								{
									if (false)
									{
									}
									IXLSRange ixlsrange;
									text = ixlsrange.Style.Name;
									flag = false;
									if (true)
									{
									}
									num2 = 5;
									continue;
								}
								}
								break;
							case 8:
							{
								if (num >= count)
								{
									num2 = 1;
									continue;
								}
								IXLSRange ixlsrange = this.ᜀ[num];
								num2 = 4;
								continue;
							}
							}
							break;
							IL_58:
							num++;
							num2 = 0;
							continue;
							IL_BD:
							num2 = 8;
						}
					}
					IL_8D:
					return null;
				}
			}
		}

		// Token: 0x17000FD3 RID: 4051
		// (get) Token: 0x06006067 RID: 24679 RVA: 0x003CF554 File Offset: 0x003CE554
		// (set) Token: 0x06006068 RID: 24680 RVA: 0x003CF690 File Offset: 0x003CE690
		public string NumberFormat
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						if (true)
						{
						}
						string text = null;
						bool flag = true;
						int num = 0;
						int count = this.ᜀ.Count;
						int num2 = 1;
						for (;;)
						{
							switch (num2)
							{
							case 0:
								goto IL_60;
							case 1:
								goto IL_C8;
							case 2:
							{
								if (num >= count)
								{
									num2 = 4;
									continue;
								}
								IXLSRange ixlsrange = this.ᜀ[num];
								num2 = 8;
								continue;
							}
							case 3:
								goto IL_C8;
							case 4:
								return text;
							case 5:
								goto IL_98;
							case 6:
							{
								IXLSRange ixlsrange;
								if (ixlsrange.Style.NumberFormat != text)
								{
									num2 = 5;
									continue;
								}
								goto IL_60;
							}
							case 7:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									return text;
								default:
								{
									if (false)
									{
									}
									IXLSRange ixlsrange;
									text = ixlsrange.Style.NumberFormat;
									flag = false;
									num2 = 0;
									continue;
								}
								}
								break;
							case 8:
								if (flag)
								{
									num2 = 7;
									continue;
								}
								num2 = 6;
								continue;
							}
							break;
							IL_60:
							num++;
							num2 = 3;
							continue;
							IL_C8:
							num2 = 2;
						}
					}
					IL_98:
					return null;
				}
			}
			set
			{
				for (;;)
				{
					int num = 0;
					int count = this.ᜀ.Count;
					int num2 = 1;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_30;
						case 1:
							goto IL_30;
						case 2:
							goto IL_4E;
						case 3:
							if (num >= count)
							{
								num2 = 2;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								continue;
							default:
							{
								if (false)
								{
								}
								IXLSRange ixlsrange = this.ᜀ[num];
								ixlsrange.Style.NumberFormat = value;
								num++;
								num2 = 0;
								continue;
							}
							}
							break;
						}
						break;
						IL_30:
						num2 = 3;
					}
				}
				IL_4E:
				if (true)
				{
				}
			}
		}

		// Token: 0x17000FD4 RID: 4052
		// (get) Token: 0x06006069 RID: 24681 RVA: 0x003CF73C File Offset: 0x003CE73C
		// (set) Token: 0x0600606A RID: 24682 RVA: 0x003CF878 File Offset: 0x003CE878
		public int NumberFormatIndex
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						int num = int.MinValue;
						bool flag = true;
						int num2 = 0;
						int count = this.ᜀ.Count;
						int num3 = 7;
						for (;;)
						{
							switch (num3)
							{
							case 0:
								goto IL_5C;
							case 1:
							{
								if (num2 >= count)
								{
									num3 = 8;
									continue;
								}
								IXLSRange ixlsrange = this.ᜀ[num2];
								num3 = 4;
								continue;
							}
							case 2:
							{
								IXLSRange ixlsrange;
								if (ixlsrange.Style.NumberFormatIndex != num)
								{
									num3 = 6;
									continue;
								}
								goto IL_5C;
							}
							case 3:
								goto IL_C4;
							case 4:
								if (flag)
								{
									num3 = 5;
									continue;
								}
								num3 = 2;
								continue;
							case 5:
								if (true)
								{
								}
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									return num;
								default:
								{
									if (false)
									{
									}
									IXLSRange ixlsrange;
									num = ixlsrange.Style.NumberFormatIndex;
									flag = false;
									num3 = 0;
									continue;
								}
								}
								break;
							case 6:
								return int.MinValue;
							case 7:
								goto IL_C4;
							case 8:
								return num;
							}
							break;
							IL_5C:
							num2++;
							num3 = 3;
							continue;
							IL_C4:
							num3 = 1;
						}
					}
					return int.MinValue;
				}
			}
			set
			{
				for (;;)
				{
					int num = 0;
					int count = this.ᜀ.Count;
					int num2 = 0;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_30;
						case 1:
							if (num >= count)
							{
								num2 = 2;
								continue;
							}
							if (true)
							{
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								continue;
							default:
							{
								if (false)
								{
								}
								IXLSRange ixlsrange = this.ᜀ[num];
								ixlsrange.Style.NumberFormatIndex = value;
								num++;
								num2 = 3;
								continue;
							}
							}
							break;
						case 2:
							return;
						case 3:
							goto IL_30;
						}
						break;
						IL_30:
						num2 = 1;
					}
				}
			}
		}

		// Token: 0x17000FD5 RID: 4053
		// (get) Token: 0x0600606B RID: 24683 RVA: 0x003CF924 File Offset: 0x003CE924
		public INumberFormat NumberFormatSettings
		{
			get
			{
				int numberFormatIndex = this.NumberFormatIndex;
				if (numberFormatIndex > 0)
				{
					if (true)
					{
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
						return this.ᜀ[0].Style.NumberFormatSettings;
					}
				}
				return null;
			}
		}

		// Token: 0x17000FD6 RID: 4054
		// (get) Token: 0x0600606C RID: 24684 RVA: 0x003CF984 File Offset: 0x003CE984
		// (set) Token: 0x0600606D RID: 24685 RVA: 0x003CFAB8 File Offset: 0x003CEAB8
		public int Rotation
		{
			get
			{
				switch (0)
				{
				default:
				{
					int num = 3;
					for (;;)
					{
						int num2;
						int count;
						int rotation;
						switch (num)
						{
						case 0:
							return int.MinValue;
						case 1:
							return 0;
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_E0;
							default:
							{
								if (false)
								{
								}
								if (num2 >= count)
								{
									num = 5;
									continue;
								}
								if (true)
								{
								}
								IXLSRange ixlsrange = this.ᜀ[num2];
								num = 4;
								continue;
							}
							}
							break;
						case 4:
						{
							IXLSRange ixlsrange;
							if (ixlsrange.Style.Rotation != rotation)
							{
								num = 0;
								continue;
							}
							num2++;
							num = 6;
							continue;
						}
						case 5:
							return rotation;
						case 6:
							goto IL_A6;
						case 7:
							goto IL_A6;
						}
						if (this.ᜀ.Count == 0)
						{
							num = 1;
							continue;
						}
						goto IL_E0;
						IL_A6:
						num = 2;
						continue;
						IL_E0:
						rotation = this.ᜀ[0].Style.Rotation;
						num2 = 1;
						count = this.ᜀ.Count;
						num = 7;
					}
					return 0;
				}
				}
			}
			set
			{
				for (;;)
				{
					int num = 0;
					int count = this.ᜀ.Count;
					int num2 = 1;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_30;
						case 1:
							goto IL_30;
						case 2:
							return;
						case 3:
							if (num >= count)
							{
								num2 = 2;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								continue;
							default:
							{
								if (false)
								{
								}
								IXLSRange ixlsrange = this.ᜀ[num];
								ixlsrange.Style.Rotation = value;
								num++;
								num2 = 0;
								continue;
							}
							}
							break;
						}
						break;
						IL_30:
						if (true)
						{
						}
						num2 = 3;
					}
				}
			}
		}

		// Token: 0x17000FD7 RID: 4055
		// (get) Token: 0x0600606E RID: 24686 RVA: 0x003CFB64 File Offset: 0x003CEB64
		// (set) Token: 0x0600606F RID: 24687 RVA: 0x003CFC98 File Offset: 0x003CEC98
		public bool ShrinkToFit
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						bool flag = false;
						bool flag2 = true;
						int num = 0;
						int count = this.ᜀ.Count;
						if (true)
						{
						}
						int num2 = 1;
						for (;;)
						{
							switch (num2)
							{
							case 0:
								if (flag2)
								{
									num2 = 6;
									continue;
								}
								num2 = 5;
								continue;
							case 1:
								goto IL_C0;
							case 2:
								goto IL_C0;
							case 3:
								goto IL_60;
							case 4:
								return flag;
							case 5:
							{
								IXLSRange ixlsrange;
								if (ixlsrange.Style.ShrinkToFit != flag)
								{
									num2 = 7;
									continue;
								}
								goto IL_60;
							}
							case 6:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									return flag;
								default:
								{
									if (false)
									{
									}
									IXLSRange ixlsrange;
									flag = ixlsrange.Style.ShrinkToFit;
									flag2 = false;
									num2 = 3;
									continue;
								}
								}
								break;
							case 7:
								return false;
							case 8:
							{
								if (num >= count)
								{
									num2 = 4;
									continue;
								}
								IXLSRange ixlsrange = this.ᜀ[num];
								num2 = 0;
								continue;
							}
							}
							break;
							IL_60:
							num++;
							num2 = 2;
							continue;
							IL_C0:
							num2 = 8;
						}
					}
					return false;
				}
			}
			set
			{
				for (;;)
				{
					int num = 0;
					int count = this.ᜀ.Count;
					int num2 = 0;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_30;
						case 1:
							if (num >= count)
							{
								num2 = 3;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								continue;
							default:
							{
								if (false)
								{
								}
								IXLSRange ixlsrange = this.ᜀ[num];
								ixlsrange.Style.ShrinkToFit = value;
								num++;
								num2 = 2;
								continue;
							}
							}
							break;
						case 2:
							goto IL_30;
						case 3:
							return;
						}
						break;
						IL_30:
						if (true)
						{
						}
						num2 = 1;
					}
				}
			}
		}

		// Token: 0x17000FD8 RID: 4056
		// (get) Token: 0x06006070 RID: 24688 RVA: 0x003CFD44 File Offset: 0x003CED44
		// (set) Token: 0x06006071 RID: 24689 RVA: 0x003CFE78 File Offset: 0x003CEE78
		public VerticalAlignType VerticalAlignment
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						VerticalAlignType verticalAlignType = VerticalAlignType.Bottom;
						bool flag = true;
						int num = 0;
						int count = this.ᜀ.Count;
						int num2 = 7;
						for (;;)
						{
							switch (num2)
							{
							case 0:
								if (flag)
								{
									num2 = 1;
									continue;
								}
								num2 = 4;
								continue;
							case 1:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									return verticalAlignType;
								default:
								{
									if (true)
									{
									}
									if (false)
									{
									}
									IXLSRange ixlsrange;
									verticalAlignType = ixlsrange.Style.VerticalAlignment;
									flag = false;
									num2 = 2;
									continue;
								}
								}
								break;
							case 2:
								goto IL_58;
							case 3:
								return verticalAlignType;
							case 4:
							{
								IXLSRange ixlsrange;
								if (ixlsrange.Style.VerticalAlignment != verticalAlignType)
								{
									num2 = 6;
									continue;
								}
								goto IL_58;
							}
							case 5:
								goto IL_B8;
							case 6:
								return VerticalAlignType.Bottom;
							case 7:
								goto IL_B8;
							case 8:
							{
								if (num >= count)
								{
									num2 = 3;
									continue;
								}
								IXLSRange ixlsrange = this.ᜀ[num];
								num2 = 0;
								continue;
							}
							}
							break;
							IL_58:
							num++;
							num2 = 5;
							continue;
							IL_B8:
							num2 = 8;
						}
					}
					return VerticalAlignType.Bottom;
				}
			}
			set
			{
				for (;;)
				{
					int num = 0;
					int count = this.ᜀ.Count;
					int num2 = 1;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							if (num >= count)
							{
								num2 = 2;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								continue;
							default:
							{
								if (false)
								{
								}
								IXLSRange ixlsrange = this.ᜀ[num];
								ixlsrange.Style.VerticalAlignment = value;
								num++;
								if (true)
								{
								}
								num2 = 3;
								continue;
							}
							}
							break;
						case 1:
							goto IL_30;
						case 2:
							return;
						case 3:
							goto IL_30;
						}
						break;
						IL_30:
						num2 = 0;
					}
				}
			}
		}

		// Token: 0x17000FD9 RID: 4057
		// (get) Token: 0x06006072 RID: 24690 RVA: 0x003CFF24 File Offset: 0x003CEF24
		// (set) Token: 0x06006073 RID: 24691 RVA: 0x003D0058 File Offset: 0x003CF058
		public bool WrapText
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						bool flag = false;
						bool flag2 = true;
						int num = 0;
						int count = this.ᜀ.Count;
						int num2 = 2;
						for (;;)
						{
							switch (num2)
							{
							case 0:
								return flag;
							case 1:
								if (flag2)
								{
									num2 = 6;
									continue;
								}
								num2 = 4;
								continue;
							case 2:
								goto IL_C0;
							case 3:
								return false;
							case 4:
							{
								IXLSRange ixlsrange;
								if (ixlsrange.Style.WrapText != flag)
								{
									num2 = 3;
									continue;
								}
								goto IL_58;
							}
							case 5:
							{
								if (num >= count)
								{
									num2 = 0;
									continue;
								}
								IXLSRange ixlsrange = this.ᜀ[num];
								if (true)
								{
								}
								num2 = 1;
								continue;
							}
							case 6:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									return flag;
								default:
								{
									if (false)
									{
									}
									IXLSRange ixlsrange;
									flag = ixlsrange.Style.WrapText;
									flag2 = false;
									num2 = 8;
									continue;
								}
								}
								break;
							case 7:
								goto IL_C0;
							case 8:
								goto IL_58;
							}
							break;
							IL_58:
							num++;
							num2 = 7;
							continue;
							IL_C0:
							num2 = 5;
						}
					}
					return false;
				}
			}
			set
			{
				for (;;)
				{
					if (true)
					{
					}
					int num = 0;
					int count = this.ᜀ.Count;
					int num2 = 2;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_38;
						case 1:
							if (num >= count)
							{
								num2 = 3;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								continue;
							default:
							{
								if (false)
								{
								}
								IXLSRange ixlsrange = this.ᜀ[num];
								ixlsrange.Style.WrapText = value;
								num++;
								num2 = 0;
								continue;
							}
							}
							break;
						case 2:
							goto IL_38;
						case 3:
							return;
						}
						break;
						IL_38:
						num2 = 1;
					}
				}
			}
		}

		// Token: 0x17000FDA RID: 4058
		// (get) Token: 0x06006074 RID: 24692 RVA: 0x003D0104 File Offset: 0x003CF104
		// (set) Token: 0x06006075 RID: 24693 RVA: 0x003D026C File Offset: 0x003CF26C
		public ReadingOrderType ReadingOrder
		{
			get
			{
				int a_ = 18;
				switch (0)
				{
				default:
				{
					int num = 9;
					ReadingOrderType readingOrder;
					for (;;)
					{
						CellRange[] array;
						switch (num)
						{
						case 0:
							goto IL_6C;
						case 1:
							return ReadingOrderType.Context;
						case 2:
							return ReadingOrderType.Context;
						case 3:
							goto IL_112;
						case 4:
							goto IL_DA;
						case 5:
						{
							int num2;
							int num3;
							if (num2 < num3)
							{
								IXLSRange ixlsrange = array[num2];
								num = 8;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_150;
							default:
								if (false)
								{
								}
								num = 3;
								continue;
							}
							break;
						}
						case 6:
							goto IL_DA;
						case 7:
						{
							if (readingOrder == ReadingOrderType.Context)
							{
								num = 1;
								continue;
							}
							int num2 = 1;
							int num3 = array.Length;
							num = 4;
							continue;
						}
						case 8:
						{
							IXLSRange ixlsrange;
							if (readingOrder != ixlsrange.Style.ReadingOrder)
							{
								num = 2;
								continue;
							}
							int num2;
							num2++;
							num = 6;
							continue;
						}
						}
						if (this.ᜀ == null)
						{
							num = 0;
							continue;
						}
						array = (CellRange[])this.ᜀ.ToArray();
						readingOrder = ((IXLSRange)array[0]).Style.ReadingOrder;
						num = 7;
						continue;
						IL_DA:
						num = 5;
					}
					IL_6C:
					throw new ApplicationException(RecordTableEnumerator.b("᱇≉⥋㱍㕏牑㵓╕硗㑙㍛⩝䁟ၡţ॥୧ᡩ࡫乭᥯ᱱ味ᕵ᝷ᙹၻ᭽ꒉ", a_));
					IL_112:
					IL_150:
					if (true)
					{
					}
					return readingOrder;
				}
				}
			}
			set
			{
				int a_ = 18;
				int num = 4;
				for (;;)
				{
					int num2;
					int count;
					switch (num)
					{
					case 0:
						goto IL_C7;
					case 1:
					{
						if (num2 >= count)
						{
							num = 3;
							continue;
						}
						IXLSRange ixlsrange = this.ᜀ[num2];
						ixlsrange.Style.ReadingOrder = value;
						num2++;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_A9;
						default:
							if (false)
							{
							}
							num = 5;
							continue;
						}
						break;
					}
					case 2:
						goto IL_41;
					case 3:
						return;
					case 5:
						goto IL_C7;
					}
					if (this.ᜀ == null)
					{
						num = 2;
						continue;
					}
					if (true)
					{
					}
					num2 = 0;
					count = this.ᜀ.Count;
					num = 0;
					continue;
					IL_C7:
					num = 1;
				}
				IL_41:
				IL_A9:
				throw new ApplicationException(RecordTableEnumerator.b("᱇≉⥋㱍㕏牑㵓╕硗㑙㍛⩝䁟ၡţ॥୧ᡩ࡫乭᥯ᱱ味ᕵ᝷ᙹၻ᭽ꒉ", a_));
			}
		}

		// Token: 0x17000FDB RID: 4059
		// (get) Token: 0x06006076 RID: 24694 RVA: 0x003D0360 File Offset: 0x003CF360
		// (set) Token: 0x06006077 RID: 24695 RVA: 0x003D0434 File Offset: 0x003CF434
		public bool IsFirstSymbolApostrophe
		{
			get
			{
				bool flag;
				for (;;)
				{
					flag = true;
					int num = 0;
					int count = this.ᜀ.Count;
					int num2 = 0;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_7D;
						case 1:
							return flag;
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								if (true)
								{
								}
								if (false)
								{
								}
								if (flag)
								{
									num2 = 1;
									continue;
								}
								flag = ((XlsRange)this.ᜀ[num]).Style.IsFirstSymbolApostrophe;
								num++;
								num2 = 4;
								continue;
							}
							break;
						case 3:
							num2 = 2;
							continue;
						case 4:
							goto IL_7D;
						case 5:
							if (num < count)
							{
								num2 = 3;
								continue;
							}
							return flag;
						}
						break;
						IL_7D:
						num2 = 5;
					}
				}
				return flag;
			}
			set
			{
				for (;;)
				{
					int num = 0;
					int count = this.ᜀ.Count;
					if (true)
					{
					}
					int num2 = 0;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_38;
						case 1:
							return;
						case 2:
							if (num >= count)
							{
								num2 = 1;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								continue;
							default:
								if (false)
								{
								}
								((XlsRange)this.ᜀ[num]).Style.IsFirstSymbolApostrophe = value;
								num++;
								num2 = 3;
								continue;
							}
							break;
						case 3:
							goto IL_38;
						}
						break;
						IL_38:
						num2 = 2;
					}
				}
			}
		}

		// Token: 0x17000FDC RID: 4060
		// (get) Token: 0x06006078 RID: 24696 RVA: 0x003D04E4 File Offset: 0x003CF4E4
		// (set) Token: 0x06006079 RID: 24697 RVA: 0x003D0618 File Offset: 0x003CF618
		public ExcelColors PatternKnownColor
		{
			get
			{
				switch (0)
				{
				default:
				{
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_11C:
						goto IL_86;
					case 1:
						goto IL_36;
					default:
						goto IL_36;
					}
					int num;
					bool flag;
					ExcelColors excelColors;
					int num2;
					int count;
					for (;;)
					{
						IL_3E:
						switch (num)
						{
						case 0:
							if (flag)
							{
								num = 7;
								continue;
							}
							num = 8;
							continue;
						case 1:
							return ExcelColors.Black;
						case 2:
							goto IL_11C;
						case 3:
							goto IL_E3;
						case 4:
							return excelColors;
						case 5:
						{
							if (num2 >= count)
							{
								num = 4;
								continue;
							}
							IXLSRange ixlsrange = this.ᜀ[num2];
							num = 0;
							continue;
						}
						case 6:
							goto IL_E3;
						case 7:
						{
							IXLSRange ixlsrange;
							excelColors = ixlsrange.Style.PatternKnownColor;
							flag = false;
							num = 2;
							continue;
						}
						case 8:
						{
							IXLSRange ixlsrange;
							if (ixlsrange.Style.PatternKnownColor != excelColors)
							{
								num = 1;
								continue;
							}
							goto IL_86;
						}
						}
						goto IL_69;
						IL_E3:
						num = 5;
					}
					return ExcelColors.Black;
					IL_36:
					if (false)
					{
					}
					IL_69:
					excelColors = ExcelColors.Black;
					flag = true;
					num2 = 0;
					count = this.ᜀ.Count;
					num = 3;
					goto IL_3E;
					IL_86:
					num2++;
					num = 6;
					goto IL_3E;
				}
				}
			}
			set
			{
				for (;;)
				{
					int num = 0;
					int count = this.ᜀ.Count;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
					{
						if (false)
						{
						}
						int num2 = 2;
						for (;;)
						{
							switch (num2)
							{
							case 0:
								goto IL_4C;
							case 1:
								return;
							case 2:
								goto IL_4C;
							case 3:
							{
								if (num >= count)
								{
									num2 = 1;
									continue;
								}
								IXLSRange ixlsrange = this.ᜀ[num];
								ixlsrange.Style.PatternKnownColor = value;
								num++;
								if (true)
								{
								}
								num2 = 0;
								continue;
							}
							}
							break;
							IL_4C:
							num2 = 3;
						}
						break;
					}
					}
				}
			}
		}

		// Token: 0x17000FDD RID: 4061
		// (get) Token: 0x0600607A RID: 24698 RVA: 0x003D06C4 File Offset: 0x003CF6C4
		// (set) Token: 0x0600607B RID: 24699 RVA: 0x003D0710 File Offset: 0x003CF710
		public Color PatternColor
		{
			get
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
				return this.ᜁ.GetPaletteColor(this.PatternKnownColor);
			}
			set
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
				this.PatternKnownColor = this.ᜁ.GetNearestColor(value);
			}
		}

		// Token: 0x17000FDE RID: 4062
		// (get) Token: 0x0600607C RID: 24700 RVA: 0x003D0760 File Offset: 0x003CF760
		// (set) Token: 0x0600607D RID: 24701 RVA: 0x003D0894 File Offset: 0x003CF894
		public ExcelColors KnownColor
		{
			get
			{
				switch (0)
				{
				default:
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_11C:
						goto IL_7E;
					default:
						if (false)
						{
						}
						goto IL_61;
					}
					int num;
					ExcelColors excelColors;
					bool flag;
					int num2;
					int count;
					for (;;)
					{
						IL_36:
						switch (num)
						{
						case 0:
							goto IL_E3;
						case 1:
							goto IL_E3;
						case 2:
						{
							IXLSRange ixlsrange;
							if (ixlsrange.Style.KnownColor != excelColors)
							{
								num = 6;
								continue;
							}
							goto IL_7E;
						}
						case 3:
						{
							IXLSRange ixlsrange;
							excelColors = ixlsrange.Style.KnownColor;
							flag = false;
							num = 8;
							continue;
						}
						case 4:
							return excelColors;
						case 5:
							if (flag)
							{
								num = 3;
								continue;
							}
							num = 2;
							continue;
						case 6:
							return ExcelColors.Black;
						case 7:
						{
							if (num2 >= count)
							{
								num = 4;
								continue;
							}
							if (true)
							{
							}
							IXLSRange ixlsrange = this.ᜀ[num2];
							num = 5;
							continue;
						}
						case 8:
							goto IL_11C;
						}
						goto IL_61;
						IL_E3:
						num = 7;
					}
					return ExcelColors.Black;
					IL_61:
					excelColors = ExcelColors.Black;
					flag = true;
					num2 = 0;
					count = this.ᜀ.Count;
					num = 0;
					goto IL_36;
					IL_7E:
					num2++;
					num = 1;
					goto IL_36;
				}
				}
			}
			set
			{
				for (;;)
				{
					int num = 0;
					int count = this.ᜀ.Count;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
					{
						if (false)
						{
						}
						int num2 = 3;
						for (;;)
						{
							switch (num2)
							{
							case 0:
								goto IL_4C;
							case 1:
								return;
							case 2:
							{
								if (num >= count)
								{
									num2 = 1;
									continue;
								}
								if (true)
								{
								}
								IXLSRange ixlsrange = this.ᜀ[num];
								ixlsrange.Style.KnownColor = value;
								num++;
								num2 = 0;
								continue;
							}
							case 3:
								goto IL_4C;
							}
							break;
							IL_4C:
							num2 = 2;
						}
						break;
					}
					}
				}
			}
		}

		// Token: 0x17000FDF RID: 4063
		// (get) Token: 0x0600607E RID: 24702 RVA: 0x003D0940 File Offset: 0x003CF940
		// (set) Token: 0x0600607F RID: 24703 RVA: 0x003D098C File Offset: 0x003CF98C
		public Color Color
		{
			get
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
				return this.ᜁ.GetPaletteColor(this.KnownColor);
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
				this.KnownColor = this.ᜁ.GetNearestColor(value);
			}
		}

		// Token: 0x17000FE0 RID: 4064
		// (get) Token: 0x06006080 RID: 24704 RVA: 0x003D09DC File Offset: 0x003CF9DC
		public IInterior Interior
		{
			get
			{
				switch (0)
				{
				default:
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_11C:
						goto IL_7E;
					default:
						if (false)
						{
						}
						goto IL_61;
					}
					int num;
					int num2;
					int count;
					IInterior interior;
					bool flag;
					for (;;)
					{
						IL_36:
						switch (num)
						{
						case 0:
						{
							if (num2 >= count)
							{
								num = 1;
								continue;
							}
							IXLSRange ixlsrange = this.ᜀ[num2];
							num = 5;
							continue;
						}
						case 1:
							return interior;
						case 2:
							goto IL_11C;
						case 3:
						{
							IXLSRange ixlsrange;
							interior = ixlsrange.Style.Interior;
							flag = false;
							num = 2;
							continue;
						}
						case 4:
						{
							IXLSRange ixlsrange;
							if (ixlsrange.Style.Interior != interior)
							{
								num = 6;
								continue;
							}
							goto IL_7E;
						}
						case 5:
							if (flag)
							{
								num = 3;
								continue;
							}
							if (true)
							{
							}
							num = 4;
							continue;
						case 6:
							goto IL_B6;
						case 7:
							goto IL_E3;
						case 8:
							goto IL_E3;
						}
						goto IL_61;
						IL_E3:
						num = 0;
					}
					IL_B6:
					return new InteriorArrayWrapper((IXLSRange)base.Parent);
					IL_61:
					interior = null;
					flag = true;
					num2 = 0;
					count = this.ᜀ.Count;
					num = 7;
					goto IL_36;
					IL_7E:
					num2++;
					num = 8;
					goto IL_36;
				}
				}
			}
		}

		// Token: 0x17000FE1 RID: 4065
		// (get) Token: 0x06006081 RID: 24705 RVA: 0x003D0B1C File Offset: 0x003CFB1C
		public bool IsModified
		{
			get
			{
				bool flag;
				for (;;)
				{
					flag = true;
					int num = 0;
					int count = this.ᜀ.Count;
					int num2 = 4;
					for (;;)
					{
						if (true)
						{
						}
						switch (num2)
						{
						case 0:
							goto IL_61;
						case 1:
							num2 = 3;
							continue;
						case 2:
							if (num >= count)
							{
								return flag;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_4A;
							default:
								if (false)
								{
								}
								num2 = 1;
								continue;
							}
							break;
						case 3:
							if (flag)
							{
								num2 = 5;
								continue;
							}
							flag = this.ᜀ[num].Style.IsModified;
							num++;
							num2 = 0;
							continue;
						case 4:
							goto IL_4A;
						case 5:
							return flag;
						}
						break;
						IL_61:
						num2 = 2;
						continue;
						IL_4A:
						goto IL_61;
					}
				}
				return flag;
			}
		}

		// Token: 0x06006082 RID: 24706 RVA: 0x003D0BE8 File Offset: 0x003CFBE8
		public virtual void BeginUpdate()
		{
			for (;;)
			{
				int num = 0;
				int count = this.ᜀ.Count;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
				{
					if (false)
					{
					}
					int num2 = 1;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							return;
						case 1:
							goto IL_4C;
						case 2:
						{
							if (num >= count)
							{
								num2 = 0;
								continue;
							}
							if (true)
							{
							}
							IXLSRange ixlsrange = this.ᜀ[num];
							ixlsrange.Style.BeginUpdate();
							num++;
							num2 = 3;
							continue;
						}
						case 3:
							goto IL_4C;
						}
						break;
						IL_4C:
						num2 = 2;
					}
					break;
				}
				}
			}
		}

		// Token: 0x06006083 RID: 24707 RVA: 0x003D0C94 File Offset: 0x003CFC94
		public virtual void EndUpdate()
		{
			for (;;)
			{
				int num = 0;
				int count = this.ᜀ.Count;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
				{
					if (false)
					{
					}
					int num2 = 0;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_4C;
						case 1:
						{
							if (num >= count)
							{
								num2 = 2;
								continue;
							}
							if (true)
							{
							}
							IXLSRange ixlsrange = this.ᜀ[num];
							ixlsrange.Style.EndUpdate();
							num++;
							num2 = 3;
							continue;
						}
						case 2:
							return;
						case 3:
							goto IL_4C;
						}
						break;
						IL_4C:
						num2 = 1;
					}
					break;
				}
				}
			}
		}

		// Token: 0x04002E41 RID: 11841
		private List<IXLSRange> ᜀ = new List<IXLSRange>();

		// Token: 0x04002E42 RID: 11842
		private long[] \u2593\u00A4\u0093\u0098;

		// Token: 0x04002E43 RID: 11843
		private string \u25D8\u00A5\u00AF\u0086;

		// Token: 0x04002E44 RID: 11844
		private XlsWorkbook ᜁ;
	}
}
