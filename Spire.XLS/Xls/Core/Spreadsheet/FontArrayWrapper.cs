using System;
using System.Collections.Generic;
using System.Drawing;

namespace Spire.Xls.Core.Spreadsheet
{
	// Token: 0x0200061A RID: 1562
	public class FontArrayWrapper : XlsObject, IFont
	{
		// Token: 0x06005EB0 RID: 24240 RVA: 0x003B2504 File Offset: 0x003B1504
		public FontArrayWrapper(IXLSRange range) : base(((XlsRange)range).Application, range)
		{
			this.ᜀ.AddRange(range.Cells);
		}

		// Token: 0x17000F71 RID: 3953
		// (get) Token: 0x06005EB1 RID: 24241 RVA: 0x003B2540 File Offset: 0x003B1540
		// (set) Token: 0x06005EB2 RID: 24242 RVA: 0x003B2680 File Offset: 0x003B1680
		public bool IsItalic
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
							bool flag = false;
							bool flag2 = true;
							int num = 0;
							int count = this.ᜀ.Count;
							int num2 = 6;
							for (;;)
							{
								switch (num2)
								{
								case 0:
									goto IL_86;
								case 1:
									if (flag2)
									{
										num2 = 2;
										continue;
									}
									num2 = 3;
									continue;
								case 2:
								{
									IXLSRange ixlsrange;
									flag = ixlsrange.Style.Font.IsItalic;
									flag2 = false;
									num2 = 0;
									continue;
								}
								case 3:
								{
									IXLSRange ixlsrange;
									if (ixlsrange.Style.Font.IsItalic != flag)
									{
										num2 = 7;
										continue;
									}
									goto IL_86;
								}
								case 4:
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
								case 5:
									goto IL_EB;
								case 6:
									if (true)
									{
									}
									goto IL_EB;
								case 7:
									goto IL_BE;
								case 8:
									return flag;
								}
								break;
								IL_86:
								num++;
								num2 = 5;
								continue;
								IL_EB:
								num2 = 4;
							}
						}
						IL_BE:
						break;
					}
					break;
				}
				return false;
			}
			set
			{
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
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_38;
							default:
								goto IL_9A;
							}
							break;
						case 1:
							goto IL_38;
						case 2:
						{
							if (num >= count)
							{
								num2 = 0;
								continue;
							}
							IXLSRange ixlsrange = this.ᜀ[num];
							ixlsrange.Style.Font.IsItalic = value;
							num++;
							num2 = 1;
							continue;
						}
						case 3:
							if (true)
							{
							}
							goto IL_38;
						}
						break;
						IL_38:
						num2 = 2;
					}
				}
				IL_9A:
				if (false)
				{
				}
			}
		}

		// Token: 0x17000F72 RID: 3954
		// (get) Token: 0x06005EB3 RID: 24243 RVA: 0x003B2730 File Offset: 0x003B1730
		// (set) Token: 0x06005EB4 RID: 24244 RVA: 0x003B286C File Offset: 0x003B186C
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
									if (flag)
									{
										num2 = 7;
										continue;
									}
									num2 = 2;
									continue;
								case 1:
									goto IL_B3;
								case 2:
								{
									IXLSRange ixlsrange;
									if (ixlsrange.Style.Font.KnownColor != excelColors)
									{
										num2 = 1;
										continue;
									}
									goto IL_7E;
								}
								case 3:
									goto IL_7E;
								case 4:
									goto IL_E0;
								case 5:
									goto IL_E0;
								case 6:
								{
									if (num >= count)
									{
										num2 = 8;
										continue;
									}
									IXLSRange ixlsrange = this.ᜀ[num];
									num2 = 0;
									continue;
								}
								case 7:
								{
									IXLSRange ixlsrange;
									excelColors = ixlsrange.Style.Font.KnownColor;
									flag = false;
									num2 = 3;
									continue;
								}
								case 8:
									goto IL_FC;
								}
								break;
								IL_7E:
								num++;
								num2 = 5;
								continue;
								IL_E0:
								num2 = 6;
							}
						}
						IL_B3:
						break;
						IL_FC:
						if (true)
						{
						}
						return excelColors;
					}
					}
					break;
				}
				return ExcelColors.Black;
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
						{
							if (num >= count)
							{
								num2 = 1;
								continue;
							}
							IXLSRange ixlsrange = this.ᜀ[num];
							ixlsrange.Style.Font.KnownColor = value;
							num++;
							num2 = 3;
							continue;
						}
						case 1:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_30;
							default:
								goto IL_92;
							}
							break;
						case 2:
							goto IL_30;
						case 3:
							goto IL_30;
						}
						break;
						IL_30:
						num2 = 0;
					}
				}
				IL_92:
				if (true)
				{
				}
				if (false)
				{
				}
			}
		}

		// Token: 0x17000F73 RID: 3955
		// (get) Token: 0x06005EB5 RID: 24245 RVA: 0x003B291C File Offset: 0x003B191C
		// (set) Token: 0x06005EB6 RID: 24246 RVA: 0x003B297C File Offset: 0x003B197C
		public Color Color
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
				ExcelColors knownColor = this.KnownColor;
				IXLSRange ixlsrange = this.ᜀ[0];
				return ixlsrange.Worksheet.Workbook.GetPaletteColor(knownColor);
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
						{
							if (num >= count)
							{
								num2 = 2;
								continue;
							}
							IXLSRange ixlsrange = this.ᜀ[num];
							ixlsrange.Style.Font.Color = value;
							num++;
							num2 = 3;
							continue;
						}
						case 1:
							if (true)
							{
							}
							goto IL_38;
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_38;
							default:
								goto IL_9A;
							}
							break;
						case 3:
							goto IL_38;
						}
						break;
						IL_38:
						num2 = 0;
					}
				}
				IL_9A:
				if (false)
				{
				}
			}
		}

		// Token: 0x17000F74 RID: 3956
		// (get) Token: 0x06005EB7 RID: 24247 RVA: 0x003B2A2C File Offset: 0x003B1A2C
		// (set) Token: 0x06005EB8 RID: 24248 RVA: 0x003B2B6C File Offset: 0x003B1B6C
		public bool IsBold
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
									if (true)
									{
									}
									goto IL_EB;
								case 1:
									goto IL_BE;
								case 2:
									return flag;
								case 3:
								{
									IXLSRange ixlsrange;
									flag = ixlsrange.Style.Font.IsBold;
									flag2 = false;
									num2 = 5;
									continue;
								}
								case 4:
								{
									IXLSRange ixlsrange;
									if (ixlsrange.Style.Font.IsBold != flag)
									{
										num2 = 1;
										continue;
									}
									goto IL_86;
								}
								case 5:
									goto IL_86;
								case 6:
									goto IL_EB;
								case 7:
								{
									if (num >= count)
									{
										num2 = 2;
										continue;
									}
									IXLSRange ixlsrange = this.ᜀ[num];
									num2 = 8;
									continue;
								}
								case 8:
									if (flag2)
									{
										num2 = 3;
										continue;
									}
									num2 = 4;
									continue;
								}
								break;
								IL_86:
								num++;
								num2 = 6;
								continue;
								IL_EB:
								num2 = 7;
							}
						}
						IL_BE:
						break;
					}
					break;
				}
				return false;
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
							ixlsrange.Style.Font.IsBold = value;
							num++;
							num2 = 3;
							continue;
						}
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_30;
							default:
								goto IL_9A;
							}
							break;
						case 3:
							goto IL_30;
						}
						break;
						IL_30:
						num2 = 1;
					}
				}
				IL_9A:
				if (false)
				{
				}
			}
		}

		// Token: 0x17000F75 RID: 3957
		// (get) Token: 0x06005EB9 RID: 24249 RVA: 0x003B2C1C File Offset: 0x003B1C1C
		// (set) Token: 0x06005EBA RID: 24250 RVA: 0x003B2CBC File Offset: 0x003B1CBC
		public bool MacOSOutlineFont
		{
			get
			{
				bool result;
				for (;;)
				{
					result = false;
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
							goto IL_3A;
						case 1:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_3A;
							default:
								goto IL_8B;
							}
							break;
						case 2:
							goto IL_3A;
						case 3:
						{
							if (num >= count)
							{
								num2 = 1;
								continue;
							}
							IXLSRange ixlsrange = this.ᜀ[num];
							num++;
							num2 = 2;
							continue;
						}
						}
						break;
						IL_3A:
						num2 = 3;
					}
				}
				IL_8B:
				if (false)
				{
				}
				return result;
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
							if (num >= count)
							{
								num2 = 3;
								continue;
							}
							num++;
							num2 = 0;
							continue;
						case 3:
							if (true)
							{
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_30;
							default:
								goto IL_7C;
							}
							break;
						}
						break;
						IL_30:
						num2 = 2;
					}
				}
				IL_7C:
				if (false)
				{
				}
			}
		}

		// Token: 0x17000F76 RID: 3958
		// (get) Token: 0x06005EBB RID: 24251 RVA: 0x003B2D4C File Offset: 0x003B1D4C
		// (set) Token: 0x06005EBC RID: 24252 RVA: 0x003B2DEC File Offset: 0x003B1DEC
		public bool MacOSShadow
		{
			get
			{
				bool result;
				for (;;)
				{
					result = false;
					int num = 0;
					int count = this.ᜀ.Count;
					int num2 = 3;
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
								goto IL_3A;
							default:
								goto IL_8B;
							}
							break;
						case 1:
						{
							if (num >= count)
							{
								num2 = 0;
								continue;
							}
							IXLSRange ixlsrange = this.ᜀ[num];
							num++;
							num2 = 2;
							continue;
						}
						case 2:
							goto IL_3A;
						case 3:
							goto IL_3A;
						}
						break;
						IL_3A:
						num2 = 1;
					}
				}
				IL_8B:
				if (false)
				{
				}
				return result;
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
			}
		}

		// Token: 0x17000F77 RID: 3959
		// (get) Token: 0x06005EBD RID: 24253 RVA: 0x003B2E28 File Offset: 0x003B1E28
		// (set) Token: 0x06005EBE RID: 24254 RVA: 0x003B2F78 File Offset: 0x003B1F78
		public double Size
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
							double num = 0.0;
							bool flag = true;
							int num2 = 0;
							int count = this.ᜀ.Count;
							if (true)
							{
							}
							int num3 = 8;
							for (;;)
							{
								switch (num3)
								{
								case 0:
									goto IL_F3;
								case 1:
								{
									IXLSRange ixlsrange;
									if (ixlsrange.Style.Font.Size != num)
									{
										num3 = 6;
										continue;
									}
									goto IL_8E;
								}
								case 2:
								{
									if (num2 >= count)
									{
										num3 = 3;
										continue;
									}
									IXLSRange ixlsrange = this.ᜀ[num2];
									num3 = 4;
									continue;
								}
								case 3:
									return num;
								case 4:
									if (flag)
									{
										num3 = 7;
										continue;
									}
									num3 = 1;
									continue;
								case 5:
									goto IL_8E;
								case 6:
									goto IL_C6;
								case 7:
								{
									IXLSRange ixlsrange;
									num = ixlsrange.Style.Font.Size;
									flag = false;
									num3 = 5;
									continue;
								}
								case 8:
									goto IL_F3;
								}
								break;
								IL_8E:
								num2++;
								num3 = 0;
								continue;
								IL_F3:
								num3 = 2;
							}
						}
						IL_C6:
						break;
					}
					break;
				}
				return 0.0;
			}
			set
			{
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
							goto IL_38;
						case 1:
						{
							if (num >= count)
							{
								num2 = 2;
								continue;
							}
							IXLSRange ixlsrange = this.ᜀ[num];
							ixlsrange.Style.Font.Size = value;
							num++;
							num2 = 0;
							continue;
						}
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_38;
							default:
								goto IL_9A;
							}
							break;
						case 3:
							if (true)
							{
							}
							goto IL_38;
						}
						break;
						IL_38:
						num2 = 1;
					}
				}
				IL_9A:
				if (false)
				{
				}
			}
		}

		// Token: 0x17000F78 RID: 3960
		// (get) Token: 0x06005EBF RID: 24255 RVA: 0x003B3028 File Offset: 0x003B2028
		// (set) Token: 0x06005EC0 RID: 24256 RVA: 0x003B3168 File Offset: 0x003B2168
		public bool IsStrikethrough
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
							bool flag = false;
							bool flag2 = true;
							int num = 0;
							int count = this.ᜀ.Count;
							int num2 = 6;
							for (;;)
							{
								switch (num2)
								{
								case 0:
									goto IL_7E;
								case 1:
									goto IL_BE;
								case 2:
									if (flag2)
									{
										num2 = 8;
										continue;
									}
									num2 = 3;
									continue;
								case 3:
								{
									if (true)
									{
									}
									IXLSRange ixlsrange;
									if (ixlsrange.Style.Font.IsStrikethrough != flag)
									{
										num2 = 1;
										continue;
									}
									goto IL_7E;
								}
								case 4:
									return flag;
								case 5:
									goto IL_EB;
								case 6:
									goto IL_EB;
								case 7:
								{
									if (num >= count)
									{
										num2 = 4;
										continue;
									}
									IXLSRange ixlsrange = this.ᜀ[num];
									num2 = 2;
									continue;
								}
								case 8:
								{
									IXLSRange ixlsrange;
									flag = ixlsrange.Style.Font.IsStrikethrough;
									flag2 = false;
									num2 = 0;
									continue;
								}
								}
								break;
								IL_7E:
								num++;
								num2 = 5;
								continue;
								IL_EB:
								num2 = 7;
							}
						}
						IL_BE:
						break;
					}
					break;
				}
				return false;
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
								goto IL_38;
							default:
								goto IL_9A;
							}
							break;
						case 1:
							goto IL_38;
						case 2:
							goto IL_38;
						case 3:
						{
							if (num >= count)
							{
								num2 = 0;
								continue;
							}
							IXLSRange ixlsrange = this.ᜀ[num];
							ixlsrange.Style.Font.IsStrikethrough = value;
							num++;
							num2 = 1;
							continue;
						}
						}
						break;
						IL_38:
						num2 = 3;
					}
				}
				IL_9A:
				if (false)
				{
				}
			}
		}

		// Token: 0x17000F79 RID: 3961
		// (get) Token: 0x06005EC1 RID: 24257 RVA: 0x003B3218 File Offset: 0x003B2218
		// (set) Token: 0x06005EC2 RID: 24258 RVA: 0x003B3354 File Offset: 0x003B2354
		public bool IsSubscript
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
						bool flag;
						for (;;)
						{
							flag = false;
							bool flag2 = true;
							int num = 0;
							int count = this.ᜀ.Count;
							int num2 = 6;
							for (;;)
							{
								switch (num2)
								{
								case 0:
									goto IL_E0;
								case 1:
								{
									IXLSRange ixlsrange;
									if (ixlsrange.Style.Font.IsSubscript != flag)
									{
										num2 = 2;
										continue;
									}
									goto IL_7E;
								}
								case 2:
									goto IL_B3;
								case 3:
									if (flag2)
									{
										num2 = 8;
										continue;
									}
									num2 = 1;
									continue;
								case 4:
									goto IL_7E;
								case 5:
								{
									if (num >= count)
									{
										num2 = 7;
										continue;
									}
									IXLSRange ixlsrange = this.ᜀ[num];
									num2 = 3;
									continue;
								}
								case 6:
									goto IL_E0;
								case 7:
									goto IL_FC;
								case 8:
								{
									IXLSRange ixlsrange;
									flag = ixlsrange.Style.Font.IsSubscript;
									flag2 = false;
									num2 = 4;
									continue;
								}
								}
								break;
								IL_7E:
								num++;
								num2 = 0;
								continue;
								IL_E0:
								num2 = 5;
							}
						}
						IL_B3:
						break;
						IL_FC:
						if (true)
						{
						}
						return flag;
					}
					}
					break;
				}
				return false;
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
								goto IL_30;
							default:
								goto IL_9A;
							}
							break;
						case 1:
							goto IL_30;
						case 2:
							goto IL_30;
						case 3:
						{
							if (num >= count)
							{
								num2 = 0;
								continue;
							}
							IXLSRange ixlsrange = this.ᜀ[num];
							ixlsrange.Style.Font.IsSubscript = value;
							num++;
							num2 = 2;
							continue;
						}
						}
						break;
						IL_30:
						if (true)
						{
						}
						num2 = 3;
					}
				}
				IL_9A:
				if (false)
				{
				}
			}
		}

		// Token: 0x17000F7A RID: 3962
		// (get) Token: 0x06005EC3 RID: 24259 RVA: 0x003B3404 File Offset: 0x003B2404
		// (set) Token: 0x06005EC4 RID: 24260 RVA: 0x003B3544 File Offset: 0x003B2544
		public bool IsSuperscript
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
						int num2 = 3;
						for (;;)
						{
							IXLSRange ixlsrange;
							switch (num2)
							{
							case 0:
								flag = ixlsrange.Style.Font.IsSuperscript;
								flag2 = false;
								num2 = 1;
								continue;
							case 1:
								goto IL_58;
							case 2:
								goto IL_70;
							case 3:
								goto IL_BD;
							case 4:
								if (flag2)
								{
									num2 = 0;
									continue;
								}
								num2 = 2;
								continue;
							case 5:
								goto IL_F5;
							case 6:
								return false;
							case 7:
								goto IL_BD;
							case 8:
								if (num < count)
								{
									ixlsrange = this.ᜀ[num];
									num2 = 4;
									continue;
								}
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_70;
								default:
									if (false)
									{
									}
									num2 = 5;
									continue;
								}
								break;
							}
							break;
							IL_58:
							num++;
							num2 = 7;
							continue;
							IL_70:
							if (ixlsrange.Style.Font.IsSuperscript != flag)
							{
								num2 = 6;
								continue;
							}
							goto IL_58;
							IL_BD:
							num2 = 8;
						}
					}
					return false;
					IL_F5:
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
							goto IL_38;
						case 3:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								return;
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
								ixlsrange.Style.Font.IsSuperscript = value;
								num++;
								num2 = 2;
								continue;
							}
							}
							break;
						}
						break;
						IL_38:
						num2 = 3;
					}
				}
			}
		}

		// Token: 0x17000F7B RID: 3963
		// (get) Token: 0x06005EC5 RID: 24261 RVA: 0x003B35F4 File Offset: 0x003B25F4
		// (set) Token: 0x06005EC6 RID: 24262 RVA: 0x003B3734 File Offset: 0x003B2734
		public FontUnderlineType Underline
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						FontUnderlineType fontUnderlineType = FontUnderlineType.None;
						bool flag = true;
						int num = 0;
						int count = this.ᜀ.Count;
						int num2 = 7;
						for (;;)
						{
							IXLSRange ixlsrange;
							switch (num2)
							{
							case 0:
								fontUnderlineType = ixlsrange.Style.Font.Underline;
								flag = false;
								num2 = 3;
								continue;
							case 1:
								return FontUnderlineType.None;
							case 2:
								if (flag)
								{
									num2 = 0;
									continue;
								}
								num2 = 4;
								continue;
							case 3:
								goto IL_58;
							case 4:
								goto IL_70;
							case 5:
								if (num < count)
								{
									ixlsrange = this.ᜀ[num];
									num2 = 2;
									continue;
								}
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_70;
								default:
									if (false)
									{
									}
									num2 = 8;
									continue;
								}
								break;
							case 6:
								goto IL_BD;
							case 7:
								goto IL_BD;
							case 8:
								return fontUnderlineType;
							}
							break;
							IL_58:
							num++;
							num2 = 6;
							continue;
							IL_70:
							if (ixlsrange.Style.Font.Underline != fontUnderlineType)
							{
								num2 = 1;
								continue;
							}
							goto IL_58;
							IL_BD:
							if (true)
							{
							}
							num2 = 5;
						}
					}
					return FontUnderlineType.None;
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
							return;
						case 1:
							goto IL_30;
						case 2:
							goto IL_30;
						case 3:
						{
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								return;
							}
							if (false)
							{
							}
							if (num >= count)
							{
								num2 = 0;
								continue;
							}
							IXLSRange ixlsrange = this.ᜀ[num];
							ixlsrange.Style.Font.Underline = value;
							num++;
							if (true)
							{
							}
							num2 = 1;
							continue;
						}
						}
						break;
						IL_30:
						num2 = 3;
					}
				}
			}
		}

		// Token: 0x17000F7C RID: 3964
		// (get) Token: 0x06005EC7 RID: 24263 RVA: 0x003B37E4 File Offset: 0x003B27E4
		// (set) Token: 0x06005EC8 RID: 24264 RVA: 0x003B392C File Offset: 0x003B292C
		public string FontName
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
						int num2 = 6;
						for (;;)
						{
							if (true)
							{
							}
							IXLSRange ixlsrange;
							switch (num2)
							{
							case 0:
								goto IL_CD;
							case 1:
								if (flag)
								{
									num2 = 3;
									continue;
								}
								num2 = 5;
								continue;
							case 2:
								if (num < count)
								{
									ixlsrange = this.ᜀ[num];
									num2 = 1;
									continue;
								}
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_78;
								default:
									if (false)
									{
									}
									num2 = 8;
									continue;
								}
								break;
							case 3:
								text = ixlsrange.Style.Font.FontName;
								flag = false;
								num2 = 7;
								continue;
							case 4:
								goto IL_9D;
							case 5:
								goto IL_78;
							case 6:
								goto IL_CD;
							case 7:
								goto IL_60;
							case 8:
								return text;
							}
							break;
							IL_60:
							num++;
							num2 = 0;
							continue;
							IL_78:
							if (ixlsrange.Style.Font.FontName != text)
							{
								num2 = 4;
								continue;
							}
							goto IL_60;
							IL_CD:
							num2 = 2;
						}
					}
					IL_9D:
					return null;
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
							return;
						case 1:
							goto IL_30;
						case 2:
							goto IL_30;
						case 3:
						{
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								return;
							}
							if (false)
							{
							}
							if (num >= count)
							{
								num2 = 0;
								continue;
							}
							if (true)
							{
							}
							IXLSRange ixlsrange = this.ᜀ[num];
							ixlsrange.Style.Font.FontName = value;
							num++;
							num2 = 1;
							continue;
						}
						}
						break;
						IL_30:
						num2 = 3;
					}
				}
			}
		}

		// Token: 0x17000F7D RID: 3965
		// (get) Token: 0x06005EC9 RID: 24265 RVA: 0x003B39DC File Offset: 0x003B29DC
		// (set) Token: 0x06005ECA RID: 24266 RVA: 0x003B3B34 File Offset: 0x003B2B34
		public FontVertialAlignmentType VerticalAlignment
		{
			get
			{
				switch (0)
				{
				default:
				{
					FontVertialAlignmentType fontVertialAlignmentType;
					for (;;)
					{
						fontVertialAlignmentType = FontVertialAlignmentType.Baseline;
						bool flag = true;
						int num = 0;
						int count = this.ᜀ.Count;
						int num2 = 8;
						for (;;)
						{
							XlsRange xlsRange;
							switch (num2)
							{
							case 0:
								if (xlsRange.Style.Font.VerticalAlignment != fontVertialAlignmentType)
								{
									num2 = 2;
									continue;
								}
								goto IL_5C;
							case 1:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_10D;
								default:
									if (false)
									{
									}
									if (num >= count)
									{
										num2 = 7;
										continue;
									}
									xlsRange = (this.ᜀ[num] as XlsRange);
									num2 = 6;
									continue;
								}
								break;
							case 2:
								fontVertialAlignmentType = FontVertialAlignmentType.Baseline;
								num2 = 5;
								continue;
							case 3:
								goto IL_D3;
							case 4:
								goto IL_10D;
							case 5:
								goto IL_140;
							case 6:
								if (flag)
								{
									num2 = 4;
									continue;
								}
								num2 = 0;
								continue;
							case 7:
								goto IL_10B;
							case 8:
								goto IL_D3;
							case 9:
								goto IL_5C;
							}
							break;
							IL_5C:
							num++;
							num2 = 3;
							continue;
							IL_D3:
							num2 = 1;
							continue;
							IL_10D:
							fontVertialAlignmentType = xlsRange.Style.Font.VerticalAlignment;
							flag = false;
							num2 = 9;
						}
					}
					IL_10B:
					return fontVertialAlignmentType;
					IL_140:
					if (true)
					{
					}
					return fontVertialAlignmentType;
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
							return;
						case 1:
							goto IL_30;
						case 2:
						{
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								return;
							}
							if (false)
							{
							}
							if (num >= count)
							{
								num2 = 0;
								continue;
							}
							IXLSRange ixlsrange = this.ᜀ[num];
							ixlsrange.Style.Font.VerticalAlignment = value;
							num++;
							if (true)
							{
							}
							num2 = 3;
							continue;
						}
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

		// Token: 0x06005ECB RID: 24267 RVA: 0x003B3BE4 File Offset: 0x003B2BE4
		public Font GenerateNativeFont()
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
			IXLSRange ixlsrange = this.ᜀ[0];
			IStyle style = ixlsrange.Style;
			IFont font = style.Font;
			return font.GenerateNativeFont();
		}

		// Token: 0x17000F7E RID: 3966
		// (get) Token: 0x06005ECC RID: 24268 RVA: 0x003B3C40 File Offset: 0x003B2C40
		public bool IsAutoColor
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

		// Token: 0x06005ECD RID: 24269 RVA: 0x003B3C7C File Offset: 0x003B2C7C
		public void BeginUpdate()
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
		}

		// Token: 0x06005ECE RID: 24270 RVA: 0x003B3CB8 File Offset: 0x003B2CB8
		public void EndUpdate()
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
		}

		// Token: 0x04002D7E RID: 11646
		private bool \u2609\u008C\u0094\u008E;

		// Token: 0x04002D7F RID: 11647
		private long \u2609\u009E\u00A2\u0094;

		// Token: 0x04002D80 RID: 11648
		private bool \u2609\u007F\u00A9\u00A4;

		// Token: 0x04002D81 RID: 11649
		private bool \u2609\u00AB\u007F\u00A8;

		// Token: 0x04002D82 RID: 11650
		private bool \u25D8\u0097\u007F\u0091;

		// Token: 0x04002D83 RID: 11651
		private List<IXLSRange> ᜀ = new List<IXLSRange>();
	}
}
