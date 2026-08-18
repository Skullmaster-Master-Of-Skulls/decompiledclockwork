using System;
using System.Collections.Generic;
using System.Drawing;

namespace Spire.Xls.Core.Spreadsheet
{
	// Token: 0x0200063E RID: 1598
	public class InteriorArrayWrapper : XlsObject, IInterior
	{
		// Token: 0x0600617E RID: 24958 RVA: 0x003DB7C8 File Offset: 0x003DA7C8
		public InteriorArrayWrapper(IXLSRange range) : base((range as XlsRange).Application, range)
		{
			this.ᜀ.AddRange(range.Cells);
		}

		// Token: 0x17001005 RID: 4101
		// (get) Token: 0x0600617F RID: 24959 RVA: 0x003DB804 File Offset: 0x003DA804
		// (set) Token: 0x06006180 RID: 24960 RVA: 0x003DB944 File Offset: 0x003DA944
		public ExcelColors PatternKnownColor
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
						int num2 = 4;
						for (;;)
						{
							switch (num2)
							{
							case 0:
							{
								if (num >= count)
								{
									goto IL_DF;
								}
								IXLSRange ixlsrange = this.ᜀ[num];
								num2 = 7;
								continue;
							}
							case 1:
								goto IL_58;
							case 2:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_DF;
								default:
									goto IL_128;
								}
								break;
							case 3:
							{
								IXLSRange ixlsrange;
								excelColors = ixlsrange.Style.Interior.PatternKnownColor;
								flag = false;
								num2 = 1;
								continue;
							}
							case 4:
								goto IL_C5;
							case 5:
								if (true)
								{
								}
								goto IL_C5;
							case 6:
							{
								IXLSRange ixlsrange;
								if (ixlsrange.Style.Interior.PatternKnownColor != excelColors)
								{
									num2 = 2;
									continue;
								}
								goto IL_58;
							}
							case 7:
								if (flag)
								{
									num2 = 3;
									continue;
								}
								num2 = 6;
								continue;
							case 8:
								return excelColors;
							}
							break;
							IL_58:
							num++;
							num2 = 5;
							continue;
							IL_C5:
							num2 = 0;
							continue;
							IL_DF:
							num2 = 8;
						}
					}
					return excelColors;
					IL_128:
					if (false)
					{
					}
					return ExcelColors.Black;
				}
				}
			}
			set
			{
				for (;;)
				{
					IL_18:
					int num = 0;
					int count = this.ᜀ.Count;
					for (;;)
					{
						IL_26:
						int num2 = 3;
						for (;;)
						{
							switch (num2)
							{
							case 0:
							{
								if (num >= count)
								{
									num2 = 1;
									continue;
								}
								IXLSRange ixlsrange = this.ᜀ[num];
								ixlsrange.Style.Interior.PatternKnownColor = value;
								num++;
								num2 = 2;
								continue;
							}
							case 1:
								return;
							case 2:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_26;
								default:
									if (false)
									{
									}
									goto IL_38;
								}
								break;
							case 3:
								if (true)
								{
								}
								goto IL_38;
							}
							goto IL_18;
							IL_38:
							num2 = 0;
						}
					}
				}
			}
		}

		// Token: 0x17001006 RID: 4102
		// (get) Token: 0x06006181 RID: 24961 RVA: 0x003DB9F4 File Offset: 0x003DA9F4
		// (set) Token: 0x06006182 RID: 24962 RVA: 0x003DBB44 File Offset: 0x003DAB44
		public Color PatternColor
		{
			get
			{
				switch (0)
				{
				default:
				{
					Color color;
					for (;;)
					{
						color = spr\u1D39.ᜂ;
						bool flag = true;
						int num = 0;
						int count = this.ᜀ.Count;
						int num2 = 4;
						for (;;)
						{
							switch (num2)
							{
							case 0:
								if (true)
								{
								}
								goto IL_5C;
							case 1:
							{
								IXLSRange ixlsrange;
								if (ixlsrange.Style.Interior.PatternColor != color)
								{
									num2 = 3;
									continue;
								}
								goto IL_5C;
							}
							case 2:
								return color;
							case 3:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_D9;
								default:
									goto IL_134;
								}
								break;
							case 4:
								goto IL_C9;
							case 5:
								goto IL_C9;
							case 6:
							{
								if (num >= count)
								{
									goto IL_D9;
								}
								IXLSRange ixlsrange = this.ᜀ[num];
								num2 = 7;
								continue;
							}
							case 7:
								if (flag)
								{
									num2 = 8;
									continue;
								}
								num2 = 1;
								continue;
							case 8:
							{
								IXLSRange ixlsrange;
								color = ixlsrange.Style.Interior.PatternColor;
								flag = false;
								num2 = 0;
								continue;
							}
							}
							break;
							IL_5C:
							num++;
							num2 = 5;
							continue;
							IL_C9:
							num2 = 6;
							continue;
							IL_D9:
							num2 = 2;
						}
					}
					return color;
					IL_134:
					if (false)
					{
					}
					return spr\u1D39.ᜂ;
				}
				}
			}
			set
			{
				for (;;)
				{
					IL_18:
					int num = 0;
					int count = this.ᜀ.Count;
					int num2;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_54:
						goto IL_56;
					default:
						if (false)
						{
						}
						num2 = 3;
						break;
					}
					for (;;)
					{
						IL_02:
						switch (num2)
						{
						case 0:
						{
							if (num >= count)
							{
								num2 = 2;
								continue;
							}
							IXLSRange ixlsrange = this.ᜀ[num];
							ixlsrange.Style.Interior.PatternColor = value;
							num++;
							if (true)
							{
							}
							num2 = 1;
							continue;
						}
						case 1:
							goto IL_A1;
						case 2:
							return;
						case 3:
							goto IL_54;
						}
						goto IL_18;
					}
					IL_A1:
					IL_56:
					num2 = 0;
					goto IL_02;
				}
			}
		}

		// Token: 0x17001007 RID: 4103
		// (get) Token: 0x06006183 RID: 24963 RVA: 0x003DBBF4 File Offset: 0x003DABF4
		// (set) Token: 0x06006184 RID: 24964 RVA: 0x003DBD34 File Offset: 0x003DAD34
		public ExcelColors KnownColor
		{
			get
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
					switch (0)
					{
					default:
						for (;;)
						{
							ExcelColors excelColors = ExcelColors.Black;
							bool flag = true;
							int num = 0;
							int count = this.ᜀ.Count;
							int num2 = 6;
							for (;;)
							{
								switch (num2)
								{
								case 0:
									goto IL_74;
								case 1:
									goto IL_A9;
								case 2:
									if (flag)
									{
										num2 = 7;
										continue;
									}
									num2 = 8;
									continue;
								case 3:
									return excelColors;
								case 4:
								{
									if (num >= count)
									{
										num2 = 3;
										continue;
									}
									IXLSRange ixlsrange = this.ᜀ[num];
									num2 = 2;
									continue;
								}
								case 5:
									goto IL_EB;
								case 6:
									goto IL_EB;
								case 7:
								{
									if (true)
									{
									}
									IXLSRange ixlsrange;
									excelColors = ixlsrange.Style.Interior.KnownColor;
									flag = false;
									num2 = 0;
									continue;
								}
								case 8:
								{
									IXLSRange ixlsrange;
									if (ixlsrange.Style.Interior.KnownColor != excelColors)
									{
										num2 = 1;
										continue;
									}
									goto IL_74;
								}
								}
								break;
								IL_74:
								num++;
								num2 = 5;
								continue;
								IL_EB:
								num2 = 4;
							}
						}
						IL_A9:
						break;
					}
					break;
				}
				return ExcelColors.Black;
			}
			set
			{
				for (;;)
				{
					IL_18:
					int num = 0;
					int count = this.ᜀ.Count;
					int num2;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_5C:
						goto IL_5E;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						num2 = 2;
						break;
					}
					for (;;)
					{
						IL_02:
						switch (num2)
						{
						case 0:
							return;
						case 1:
							goto IL_A1;
						case 2:
							goto IL_5C;
						case 3:
						{
							if (num >= count)
							{
								num2 = 0;
								continue;
							}
							IXLSRange ixlsrange = this.ᜀ[num];
							ixlsrange.Style.Interior.KnownColor = value;
							num++;
							num2 = 1;
							continue;
						}
						}
						goto IL_18;
					}
					IL_A1:
					IL_5E:
					num2 = 3;
					goto IL_02;
				}
			}
		}

		// Token: 0x17001008 RID: 4104
		// (get) Token: 0x06006185 RID: 24965 RVA: 0x003DBDE4 File Offset: 0x003DADE4
		// (set) Token: 0x06006186 RID: 24966 RVA: 0x003DBF30 File Offset: 0x003DAF30
		public Color Color
		{
			get
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
					switch (0)
					{
					default:
						for (;;)
						{
							Color color = spr\u1D39.ᜂ;
							bool flag = true;
							int num = 0;
							int count = this.ᜀ.Count;
							int num2 = 0;
							for (;;)
							{
								switch (num2)
								{
								case 0:
									if (true)
									{
									}
									goto IL_F4;
								case 1:
									goto IL_F4;
								case 2:
								{
									if (num >= count)
									{
										num2 = 3;
										continue;
									}
									IXLSRange ixlsrange = this.ᜀ[num];
									num2 = 5;
									continue;
								}
								case 3:
									return color;
								case 4:
									goto IL_80;
								case 5:
									if (flag)
									{
										num2 = 6;
										continue;
									}
									num2 = 8;
									continue;
								case 6:
								{
									IXLSRange ixlsrange;
									color = ixlsrange.Style.Interior.Color;
									flag = false;
									num2 = 4;
									continue;
								}
								case 7:
									goto IL_BD;
								case 8:
								{
									IXLSRange ixlsrange;
									if (ixlsrange.Style.Interior.Color != color)
									{
										num2 = 7;
										continue;
									}
									goto IL_80;
								}
								}
								break;
								IL_80:
								num++;
								num2 = 1;
								continue;
								IL_F4:
								num2 = 2;
							}
						}
						IL_BD:
						break;
					}
					break;
				}
				return spr\u1D39.ᜂ;
			}
			set
			{
				for (;;)
				{
					IL_18:
					int num = 0;
					int count = this.ᜀ.Count;
					int num2;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_54:
						goto IL_56;
					default:
						if (false)
						{
						}
						num2 = 0;
						break;
					}
					for (;;)
					{
						IL_02:
						switch (num2)
						{
						case 0:
							goto IL_54;
						case 1:
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
							ixlsrange.Style.Interior.Color = value;
							num++;
							num2 = 2;
							continue;
						}
						case 2:
							goto IL_A1;
						case 3:
							return;
						}
						goto IL_18;
					}
					IL_A1:
					IL_56:
					num2 = 1;
					goto IL_02;
				}
			}
		}

		// Token: 0x17001009 RID: 4105
		// (get) Token: 0x06006187 RID: 24967 RVA: 0x003DBFE0 File Offset: 0x003DAFE0
		public ExcelGradient Gradient
		{
			get
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
					switch (0)
					{
					default:
					{
						IGradient gradient;
						for (;;)
						{
							gradient = null;
							bool flag = true;
							int num = 0;
							int count = this.ᜀ.Count;
							if (true)
							{
							}
							int num2 = 3;
							for (;;)
							{
								switch (num2)
								{
								case 0:
									if (flag)
									{
										num2 = 4;
										continue;
									}
									num2 = 5;
									continue;
								case 1:
									goto IL_EB;
								case 2:
									goto IL_B4;
								case 3:
									goto IL_EB;
								case 4:
								{
									IXLSRange ixlsrange;
									gradient = ixlsrange.Style.Interior.Gradient;
									flag = false;
									num2 = 7;
									continue;
								}
								case 5:
								{
									IXLSRange ixlsrange;
									if (ixlsrange.Style.Interior.Gradient != gradient)
									{
										num2 = 2;
										continue;
									}
									goto IL_7C;
								}
								case 6:
									goto IL_107;
								case 7:
									goto IL_7C;
								case 8:
								{
									if (num >= count)
									{
										num2 = 6;
										continue;
									}
									IXLSRange ixlsrange = this.ᜀ[num];
									num2 = 0;
									continue;
								}
								}
								break;
								IL_7C:
								num++;
								num2 = 1;
								continue;
								IL_EB:
								num2 = 8;
							}
						}
						IL_B4:
						break;
						IL_107:
						return new ExcelGradient(gradient);
					}
					}
					break;
				}
				return new ExcelGradient(new GradientArrayWrapper((IXLSRange)base.Parent));
			}
		}

		// Token: 0x1700100A RID: 4106
		// (get) Token: 0x06006188 RID: 24968 RVA: 0x003DC138 File Offset: 0x003DB138
		// (set) Token: 0x06006189 RID: 24969 RVA: 0x003DC278 File Offset: 0x003DB278
		public ExcelPatternType FillPattern
		{
			get
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
					switch (0)
					{
					default:
					{
						ExcelPatternType excelPatternType;
						for (;;)
						{
							excelPatternType = ExcelPatternType.None;
							bool flag = true;
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
										num2 = 8;
										continue;
									}
									IXLSRange ixlsrange = this.ᜀ[num];
									num2 = 1;
									continue;
								}
								case 1:
									if (flag)
									{
										num2 = 6;
										continue;
									}
									num2 = 2;
									continue;
								case 2:
								{
									IXLSRange ixlsrange;
									if (ixlsrange.Style.Interior.FillPattern != excelPatternType)
									{
										num2 = 5;
										continue;
									}
									goto IL_74;
								}
								case 3:
									goto IL_E3;
								case 4:
									goto IL_E3;
								case 5:
									goto IL_A9;
								case 6:
								{
									IXLSRange ixlsrange;
									excelPatternType = ixlsrange.Style.Interior.FillPattern;
									flag = false;
									num2 = 7;
									continue;
								}
								case 7:
									goto IL_74;
								case 8:
									goto IL_FF;
								}
								break;
								IL_74:
								num++;
								num2 = 4;
								continue;
								IL_E3:
								num2 = 0;
							}
						}
						IL_A9:
						break;
						IL_FF:
						if (true)
						{
						}
						return excelPatternType;
					}
					}
					break;
				}
				return ExcelPatternType.None;
			}
			set
			{
				for (;;)
				{
					IL_20:
					int num = 0;
					int count = this.ᜀ.Count;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_5C:
						goto IL_5E;
					case 1:
						goto IL_4E;
					default:
						goto IL_4E;
					}
					int num2;
					for (;;)
					{
						IL_02:
						if (true)
						{
						}
						switch (num2)
						{
						case 0:
						{
							if (num >= count)
							{
								num2 = 1;
								continue;
							}
							IXLSRange ixlsrange = this.ᜀ[num];
							ixlsrange.Style.Interior.FillPattern = value;
							num++;
							num2 = 2;
							continue;
						}
						case 1:
							return;
						case 2:
							goto IL_A1;
						case 3:
							goto IL_5C;
						}
						goto IL_20;
					}
					IL_A1:
					goto IL_5E;
					IL_4E:
					if (false)
					{
					}
					num2 = 3;
					goto IL_02;
					IL_5E:
					num2 = 0;
					goto IL_02;
				}
			}
		}

		// Token: 0x0600618A RID: 24970 RVA: 0x003DC328 File Offset: 0x003DB328
		public void BeginUpdate()
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

		// Token: 0x0600618B RID: 24971 RVA: 0x003DC364 File Offset: 0x003DB364
		public void EndUpdate()
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

		// Token: 0x04002EA9 RID: 11945
		private long \u2460\u00A2\u0093\u009B;

		// Token: 0x04002EAA RID: 11946
		private string \u2609\u00A9\u0095\u00AD;

		// Token: 0x04002EAB RID: 11947
		private List<IXLSRange> ᜀ = new List<IXLSRange>();
	}
}
