using System;
using System.Collections.Generic;
using System.Drawing;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Core.Spreadsheet
{
	// Token: 0x02000634 RID: 1588
	public class XlsBorderArrayWrapper : XlsObject, IBorder
	{
		// Token: 0x0600613C RID: 24892 RVA: 0x003D7FAC File Offset: 0x003D6FAC
		public XlsBorderArrayWrapper(IXLSRange range, BordersLineType index)
		{
			int a_ = 8;
			this.ᜀ = new List<IXLSRange>();
			base..ctor(((XlsRange)range).Application, range);
			this.ᜁ = index;
			this.ᜀ.AddRange(range.Cells);
			this.ᜂ = (base.FindParent(typeof(XlsWorkbook)) as XlsWorkbook);
			if (this.ᜂ == null)
			{
				throw new ArgumentNullException(RecordTableEnumerator.b("渽ℿぁ⅃⡅㱇橉⍋ⱍ㩏㝑㝓≕硗㥙㵛そ๟ൡၣ䙥੧ཀྵ䱫࡭Ὧݱᩳት噷", a_));
			}
		}

		// Token: 0x17000FF6 RID: 4086
		// (get) Token: 0x0600613D RID: 24893 RVA: 0x003D8030 File Offset: 0x003D7030
		// (set) Token: 0x0600613E RID: 24894 RVA: 0x003D812C File Offset: 0x003D712C
		public ExcelColors KnownColor
		{
			get
			{
				for (;;)
				{
					IL_00:
					for (;;)
					{
						ExcelColors knownColor = this.ᜀ[0].Borders[this.ᜁ].KnownColor;
						int num = 1;
						int num2 = 3;
						for (;;)
						{
							switch (num2)
							{
							case 0:
								return knownColor;
							case 1:
								if (num >= this.ᜀ.Count)
								{
									num2 = 0;
									continue;
								}
								num2 = 5;
								continue;
							case 2:
								goto IL_C5;
							case 3:
								goto IL_C5;
							case 4:
								return ExcelColors.Black;
							case 5:
								if (knownColor != this.ᜀ[num].Borders[this.ᜁ].KnownColor)
								{
									switch ((1 == 1) ? 1 : 0)
									{
									case 0:
									case 2:
										goto IL_00;
									}
									if (false)
									{
									}
									num2 = 4;
									continue;
								}
								if (true)
								{
								}
								num++;
								num2 = 2;
								continue;
							}
							break;
							IL_C5:
							num2 = 1;
						}
					}
				}
				return ExcelColors.Black;
			}
			set
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					break;
				}
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
							goto IL_5E;
						case 1:
							goto IL_5E;
						case 2:
						{
							if (num >= count)
							{
								num2 = 3;
								continue;
							}
							IXLSRange ixlsrange = this.ᜀ[num];
							ixlsrange.Borders[this.ᜁ].KnownColor = value;
							num++;
							num2 = 0;
							continue;
						}
						case 3:
							return;
						}
						break;
						IL_5E:
						num2 = 2;
					}
				}
			}
		}

		// Token: 0x17000FF7 RID: 4087
		// (get) Token: 0x0600613F RID: 24895 RVA: 0x003D81E0 File Offset: 0x003D71E0
		public OColor OColor
		{
			get
			{
				for (;;)
				{
					for (;;)
					{
						OColor ocolor = this.ᜀ[0].Borders[this.ᜁ].OColor;
						int num = 1;
						int num2 = 1;
						for (;;)
						{
							switch (num2)
							{
							case 0:
								return ocolor;
							case 1:
								goto IL_C2;
							case 2:
								if (!(ocolor != this.ᜀ[num].Borders[this.ᜁ].OColor))
								{
									num++;
									num2 = 5;
									continue;
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
									num2 = 4;
									continue;
								}
								break;
							case 3:
								if (true)
								{
								}
								if (num >= this.ᜀ.Count)
								{
									num2 = 0;
									continue;
								}
								num2 = 2;
								continue;
							case 4:
								goto IL_C0;
							case 5:
								goto IL_C2;
							}
							break;
							IL_C2:
							num2 = 3;
						}
					}
				}
				IL_C0:
				return null;
			}
		}

		// Token: 0x17000FF8 RID: 4088
		// (get) Token: 0x06006140 RID: 24896 RVA: 0x003D82E4 File Offset: 0x003D72E4
		// (set) Token: 0x06006141 RID: 24897 RVA: 0x003D83EC File Offset: 0x003D73EC
		public Color Color
		{
			get
			{
				for (;;)
				{
					for (;;)
					{
						Color color = this.ᜀ[0].Borders[this.ᜁ].Color;
						int num = 1;
						int num2 = 3;
						for (;;)
						{
							switch (num2)
							{
							case 0:
								if (!(color != this.ᜀ[num].Borders[this.ᜁ].Color))
								{
									num++;
									num2 = 1;
									continue;
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
									num2 = 2;
									continue;
								}
								break;
							case 1:
								goto IL_C6;
							case 2:
								goto IL_C4;
							case 3:
								goto IL_C6;
							case 4:
								if (true)
								{
								}
								if (num >= this.ᜀ.Count)
								{
									num2 = 5;
									continue;
								}
								num2 = 0;
								continue;
							case 5:
								return color;
							}
							break;
							IL_C6:
							num2 = 4;
						}
					}
				}
				IL_C4:
				return spr\u1D39.ᜂ;
			}
			set
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					break;
				}
				for (;;)
				{
					int num = 0;
					int count = this.ᜀ.Count;
					int num2 = 3;
					for (;;)
					{
						switch (num2)
						{
						case 0:
						{
							if (num >= count)
							{
								if (true)
								{
								}
								num2 = 1;
								continue;
							}
							IXLSRange ixlsrange = this.ᜀ[num];
							ixlsrange.Borders[this.ᜁ].Color = value;
							num++;
							num2 = 2;
							continue;
						}
						case 1:
							return;
						case 2:
							goto IL_56;
						case 3:
							goto IL_56;
						}
						break;
						IL_56:
						num2 = 0;
					}
				}
			}
		}

		// Token: 0x17000FF9 RID: 4089
		// (get) Token: 0x06006142 RID: 24898 RVA: 0x003D84A0 File Offset: 0x003D74A0
		// (set) Token: 0x06006143 RID: 24899 RVA: 0x003D859C File Offset: 0x003D759C
		public LineStyleType LineStyle
		{
			get
			{
				for (;;)
				{
					IL_00:
					for (;;)
					{
						LineStyleType lineStyle = this.ᜀ[0].Borders[this.ᜁ].LineStyle;
						int num = 1;
						int num2 = 0;
						for (;;)
						{
							switch (num2)
							{
							case 0:
								goto IL_C5;
							case 1:
								return LineStyleType.None;
							case 2:
								return lineStyle;
							case 3:
								if (lineStyle != this.ᜀ[num].Borders[this.ᜁ].LineStyle)
								{
									switch ((1 == 1) ? 1 : 0)
									{
									case 0:
									case 2:
										goto IL_00;
									}
									if (false)
									{
									}
									num2 = 1;
									continue;
								}
								num++;
								if (true)
								{
								}
								num2 = 5;
								continue;
							case 4:
								if (num >= this.ᜀ.Count)
								{
									num2 = 2;
									continue;
								}
								num2 = 3;
								continue;
							case 5:
								goto IL_C5;
							}
							break;
							IL_C5:
							num2 = 4;
						}
					}
				}
				return LineStyleType.None;
			}
			set
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					break;
				}
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
							goto IL_5E;
						case 1:
							goto IL_5E;
						case 2:
							return;
						case 3:
						{
							if (num >= count)
							{
								num2 = 2;
								continue;
							}
							IXLSRange ixlsrange = this.ᜀ[num];
							ixlsrange.Borders[this.ᜁ].LineStyle = value;
							num++;
							num2 = 0;
							continue;
						}
						}
						break;
						IL_5E:
						num2 = 3;
					}
				}
			}
		}

		// Token: 0x17000FFA RID: 4090
		// (get) Token: 0x06006144 RID: 24900 RVA: 0x003D8650 File Offset: 0x003D7650
		// (set) Token: 0x06006145 RID: 24901 RVA: 0x003D874C File Offset: 0x003D774C
		public bool ShowDiagonalLine
		{
			get
			{
				for (;;)
				{
					for (;;)
					{
						bool showDiagonalLine = this.ᜀ[0].Borders[this.ᜁ].ShowDiagonalLine;
						int num = 1;
						int num2 = 0;
						for (;;)
						{
							switch (num2)
							{
							case 0:
								goto IL_C5;
							case 1:
								return false;
							case 2:
								if (num >= this.ᜀ.Count)
								{
									num2 = 4;
									continue;
								}
								num2 = 5;
								continue;
							case 3:
								goto IL_C5;
							case 4:
								return showDiagonalLine;
							case 5:
								if (showDiagonalLine == this.ᜀ[num].Borders[this.ᜁ].ShowDiagonalLine)
								{
									num++;
									num2 = 3;
									continue;
								}
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
									num2 = 1;
									continue;
								}
								break;
							}
							break;
							IL_C5:
							num2 = 2;
						}
					}
				}
				return false;
			}
			set
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					break;
				}
				for (;;)
				{
					int num = 0;
					int count = this.ᜀ.Count;
					int num2 = 0;
					for (;;)
					{
						if (true)
						{
						}
						switch (num2)
						{
						case 0:
							goto IL_5E;
						case 1:
							goto IL_5E;
						case 2:
						{
							if (num >= count)
							{
								num2 = 3;
								continue;
							}
							IXLSRange ixlsrange = this.ᜀ[num];
							ixlsrange.Borders[this.ᜁ].ShowDiagonalLine = value;
							num++;
							num2 = 1;
							continue;
						}
						case 3:
							return;
						}
						break;
						IL_5E:
						num2 = 2;
					}
				}
			}
		}

		// Token: 0x04002E7A RID: 11898
		private List<IXLSRange> ᜀ;

		// Token: 0x04002E7B RID: 11899
		private int[] \u2460\u00B0\u0086\u009C;

		// Token: 0x04002E7C RID: 11900
		private BordersLineType ᜁ;

		// Token: 0x04002E7D RID: 11901
		private long \u2609\u0086\u008A\u009E;

		// Token: 0x04002E7E RID: 11902
		private int[] \u2609\u008A\u00AC\u00A6;

		// Token: 0x04002E7F RID: 11903
		private float \u2593\u0083\u0080\u0091;

		// Token: 0x04002E80 RID: 11904
		private XlsWorkbook ᜂ;
	}
}
