using System;
using System.Collections.Generic;
using System.Drawing;

namespace Spire.Xls.Core.Spreadsheet
{
	// Token: 0x02000635 RID: 1589
	public class GradientArrayWrapper : XlsObject, IGradient
	{
		// Token: 0x06006146 RID: 24902 RVA: 0x003D8800 File Offset: 0x003D7800
		public GradientArrayWrapper(IXLSRange range) : base((range as XlsRange).Application, range)
		{
			this.ᜀ.AddRange(range.Cells);
		}

		// Token: 0x17000FFB RID: 4091
		// (get) Token: 0x06006147 RID: 24903 RVA: 0x003D883C File Offset: 0x003D783C
		public OColor BackColorObject
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
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000FFC RID: 4092
		// (get) Token: 0x06006148 RID: 24904 RVA: 0x003D887C File Offset: 0x003D787C
		// (set) Token: 0x06006149 RID: 24905 RVA: 0x003D89E8 File Offset: 0x003D79E8
		public Color BackColor
		{
			get
			{
				switch (0)
				{
				default:
				{
					int num;
					for (;;)
					{
						num = 0;
						bool flag = true;
						int num2 = 0;
						int count = this.ᜀ.Count;
						int num3 = 0;
						for (;;)
						{
							switch (num3)
							{
							case 0:
								goto IL_E6;
							case 1:
								goto IL_E6;
							case 2:
							{
								if (num2 >= count)
								{
									num3 = 6;
									continue;
								}
								IXLSRange ixlsrange = this.ᜀ[num2];
								num3 = 4;
								continue;
							}
							case 3:
							{
								IXLSRange ixlsrange;
								num = ixlsrange.Style.Interior.Gradient.BackColor.ToArgb();
								flag = false;
								num3 = 5;
								continue;
							}
							case 4:
							{
								IL_D5:
								if (flag)
								{
									num3 = 3;
									continue;
								}
								if (true)
								{
								}
								IXLSRange ixlsrange;
								Color backColor = ixlsrange.Style.Interior.Gradient.BackColor;
								num3 = 8;
								continue;
							}
							case 5:
								goto IL_5B;
							case 6:
								goto IL_11E;
							case 7:
								goto IL_B6;
							case 8:
							{
								Color backColor;
								if (backColor.ToArgb() != num)
								{
									num3 = 7;
									continue;
								}
								goto IL_5B;
							}
							}
							break;
							IL_5B:
							num2++;
							num3 = 1;
							continue;
							IL_E6:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_D5;
							default:
								if (false)
								{
								}
								num3 = 2;
								break;
							}
						}
					}
					IL_B6:
					return spr\u1D39.ᜂ;
					IL_11E:
					return spr\u1D39.ᜀ(num);
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
						{
							if (num >= count)
							{
								num2 = 3;
								continue;
							}
							IXLSRange ixlsrange = this.ᜀ[num];
							ixlsrange.Style.Interior.Gradient.BackColor = value;
							num++;
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								return;
							}
							if (true)
							{
							}
							if (false)
							{
							}
							num2 = 2;
							continue;
						}
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

		// Token: 0x17000FFD RID: 4093
		// (get) Token: 0x0600614A RID: 24906 RVA: 0x003D8AA0 File Offset: 0x003D7AA0
		// (set) Token: 0x0600614B RID: 24907 RVA: 0x003D8BEC File Offset: 0x003D7BEC
		public ExcelColors BackKnownColor
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
						int num2 = 0;
						for (;;)
						{
							switch (num2)
							{
							case 0:
								goto IL_E1;
							case 1:
								goto IL_E1;
							case 2:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									return excelColors;
								default:
								{
									if (false)
									{
									}
									IXLSRange ixlsrange;
									if (ixlsrange.Style.Interior.Gradient.BackKnownColor != excelColors)
									{
										num2 = 3;
										continue;
									}
									goto IL_5B;
								}
								}
								break;
							case 3:
								return ExcelColors.Black;
							case 4:
								if (flag)
								{
									num2 = 5;
									continue;
								}
								num2 = 2;
								continue;
							case 5:
							{
								if (true)
								{
								}
								IXLSRange ixlsrange;
								excelColors = ixlsrange.Style.Interior.Gradient.BackKnownColor;
								flag = false;
								num2 = 7;
								continue;
							}
							case 6:
								return excelColors;
							case 7:
								goto IL_5B;
							case 8:
							{
								if (num >= count)
								{
									num2 = 6;
									continue;
								}
								IXLSRange ixlsrange = this.ᜀ[num];
								num2 = 4;
								continue;
							}
							}
							break;
							IL_5B:
							num++;
							num2 = 1;
							continue;
							IL_E1:
							num2 = 8;
						}
					}
					return ExcelColors.Black;
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
						{
							if (num >= count)
							{
								num2 = 3;
								continue;
							}
							IXLSRange ixlsrange = this.ᜀ[num];
							ixlsrange.Style.Interior.Gradient.BackKnownColor = value;
							num++;
							if (true)
							{
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								return;
							default:
								if (false)
								{
								}
								num2 = 1;
								continue;
							}
							break;
						}
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

		// Token: 0x17000FFE RID: 4094
		// (get) Token: 0x0600614C RID: 24908 RVA: 0x003D8CA4 File Offset: 0x003D7CA4
		public OColor ForeColorObject
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
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000FFF RID: 4095
		// (get) Token: 0x0600614D RID: 24909 RVA: 0x003D8CE4 File Offset: 0x003D7CE4
		// (set) Token: 0x0600614E RID: 24910 RVA: 0x003D8E50 File Offset: 0x003D7E50
		public Color ForeColor
		{
			get
			{
				switch (0)
				{
				default:
				{
					int num;
					for (;;)
					{
						num = 0;
						bool flag = true;
						int num2 = 0;
						int count = this.ᜀ.Count;
						int num3 = 4;
						for (;;)
						{
							switch (num3)
							{
							case 0:
								if (true)
								{
								}
								goto IL_E6;
							case 1:
								goto IL_B6;
							case 2:
							{
								Color foreColor;
								if (foreColor.ToArgb() != num)
								{
									num3 = 1;
									continue;
								}
								goto IL_5B;
							}
							case 3:
								goto IL_5B;
							case 4:
								goto IL_E6;
							case 5:
							{
								IL_D5:
								if (flag)
								{
									num3 = 8;
									continue;
								}
								IXLSRange ixlsrange;
								Color foreColor = ixlsrange.Style.Interior.Gradient.ForeColor;
								num3 = 2;
								continue;
							}
							case 6:
								goto IL_11E;
							case 7:
							{
								if (num2 >= count)
								{
									num3 = 6;
									continue;
								}
								IXLSRange ixlsrange = this.ᜀ[num2];
								num3 = 5;
								continue;
							}
							case 8:
							{
								IXLSRange ixlsrange;
								num = ixlsrange.Style.Interior.Gradient.ForeColor.ToArgb();
								flag = false;
								num3 = 3;
								continue;
							}
							}
							break;
							IL_5B:
							num2++;
							num3 = 0;
							continue;
							IL_E6:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_D5;
							default:
								if (false)
								{
								}
								num3 = 7;
								break;
							}
						}
					}
					IL_B6:
					return spr\u1D39.ᜂ;
					IL_11E:
					return spr\u1D39.ᜀ(num);
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
						{
							if (num >= count)
							{
								num2 = 3;
								continue;
							}
							if (true)
							{
							}
							IXLSRange ixlsrange = this.ᜀ[num];
							ixlsrange.Style.Interior.Gradient.ForeColor = value;
							num++;
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								return;
							default:
								if (false)
								{
								}
								num2 = 2;
								continue;
							}
							break;
						}
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

		// Token: 0x17001000 RID: 4096
		// (get) Token: 0x0600614F RID: 24911 RVA: 0x003D8F08 File Offset: 0x003D7F08
		// (set) Token: 0x06006150 RID: 24912 RVA: 0x003D9054 File Offset: 0x003D8054
		public ExcelColors ForeKnownColor
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
						int num2 = 6;
						for (;;)
						{
							switch (num2)
							{
							case 0:
							{
								IXLSRange ixlsrange;
								excelColors = ixlsrange.Style.Interior.Gradient.ForeKnownColor;
								flag = false;
								num2 = 8;
								continue;
							}
							case 1:
								goto IL_E9;
							case 2:
								return ExcelColors.Black;
							case 3:
								return excelColors;
							case 4:
								if (flag)
								{
									num2 = 0;
									continue;
								}
								num2 = 7;
								continue;
							case 5:
							{
								if (num >= count)
								{
									num2 = 3;
									continue;
								}
								IXLSRange ixlsrange = this.ᜀ[num];
								num2 = 4;
								continue;
							}
							case 6:
								goto IL_E9;
							case 7:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									return excelColors;
								default:
								{
									if (false)
									{
									}
									IXLSRange ixlsrange;
									if (ixlsrange.Style.Interior.Gradient.ForeKnownColor != excelColors)
									{
										num2 = 2;
										continue;
									}
									goto IL_5B;
								}
								}
								break;
							case 8:
								goto IL_5B;
							}
							break;
							IL_5B:
							num++;
							if (true)
							{
							}
							num2 = 1;
							continue;
							IL_E9:
							num2 = 5;
						}
					}
					return ExcelColors.Black;
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
							goto IL_30;
						case 3:
						{
							if (num >= count)
							{
								num2 = 1;
								continue;
							}
							IXLSRange ixlsrange = this.ᜀ[num];
							ixlsrange.Style.Interior.Gradient.ForeKnownColor = value;
							num++;
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								return;
							}
							if (true)
							{
							}
							if (false)
							{
							}
							num2 = 2;
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

		// Token: 0x17001001 RID: 4097
		// (get) Token: 0x06006151 RID: 24913 RVA: 0x003D910C File Offset: 0x003D810C
		// (set) Token: 0x06006152 RID: 24914 RVA: 0x003D9258 File Offset: 0x003D8258
		public GradientStyleType GradientStyle
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						GradientStyleType gradientStyleType = GradientStyleType.Horizontal;
						bool flag = true;
						int num = 0;
						int count = this.ᜀ.Count;
						int num2 = 5;
						for (;;)
						{
							if (true)
							{
							}
							switch (num2)
							{
							case 0:
								return GradientStyleType.Horizontal;
							case 1:
								return gradientStyleType;
							case 2:
								goto IL_E9;
							case 3:
							{
								IXLSRange ixlsrange;
								gradientStyleType = ixlsrange.Style.Interior.Gradient.GradientStyle;
								flag = false;
								num2 = 4;
								continue;
							}
							case 4:
								goto IL_63;
							case 5:
								goto IL_E9;
							case 6:
								if (flag)
								{
									num2 = 3;
									continue;
								}
								num2 = 8;
								continue;
							case 7:
							{
								if (num >= count)
								{
									num2 = 1;
									continue;
								}
								IXLSRange ixlsrange = this.ᜀ[num];
								num2 = 6;
								continue;
							}
							case 8:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									return gradientStyleType;
								default:
								{
									if (false)
									{
									}
									IXLSRange ixlsrange;
									if (ixlsrange.Style.Interior.Gradient.GradientStyle != gradientStyleType)
									{
										num2 = 0;
										continue;
									}
									goto IL_63;
								}
								}
								break;
							}
							break;
							IL_63:
							num++;
							num2 = 2;
							continue;
							IL_E9:
							num2 = 7;
						}
					}
					return GradientStyleType.Horizontal;
				}
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
							ixlsrange.Style.Interior.Gradient.GradientStyle = value;
							num++;
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								return;
							default:
								if (false)
								{
								}
								num2 = 0;
								continue;
							}
							break;
						}
						case 2:
							return;
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
			}
		}

		// Token: 0x17001002 RID: 4098
		// (get) Token: 0x06006153 RID: 24915 RVA: 0x003D9310 File Offset: 0x003D8310
		// (set) Token: 0x06006154 RID: 24916 RVA: 0x003D945C File Offset: 0x003D845C
		public GradientVariantsType GradientVariant
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						GradientVariantsType gradientVariantsType = GradientVariantsType.ShadingVariants1;
						bool flag = true;
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
								gradientVariantsType = ixlsrange.Style.Interior.Gradient.GradientVariant;
								flag = false;
								num2 = 5;
								continue;
							}
							case 1:
								return gradientVariantsType;
							case 2:
								goto IL_E9;
							case 3:
								if (flag)
								{
									num2 = 0;
									continue;
								}
								num2 = 6;
								continue;
							case 4:
								goto IL_E9;
							case 5:
								goto IL_5B;
							case 6:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									return gradientVariantsType;
								default:
								{
									if (false)
									{
									}
									IXLSRange ixlsrange;
									if (ixlsrange.Style.Interior.Gradient.GradientVariant != gradientVariantsType)
									{
										num2 = 7;
										continue;
									}
									goto IL_5B;
								}
								}
								break;
							case 7:
								return GradientVariantsType.ShadingVariants1;
							case 8:
							{
								if (num >= count)
								{
									num2 = 1;
									continue;
								}
								IXLSRange ixlsrange = this.ᜀ[num];
								num2 = 3;
								continue;
							}
							}
							break;
							IL_5B:
							num++;
							if (true)
							{
							}
							num2 = 4;
							continue;
							IL_E9:
							num2 = 8;
						}
					}
					return GradientVariantsType.ShadingVariants1;
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
						{
							if (num >= count)
							{
								num2 = 2;
								continue;
							}
							IXLSRange ixlsrange = this.ᜀ[num];
							ixlsrange.Style.Interior.Gradient.GradientVariant = value;
							num++;
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								return;
							default:
								if (false)
								{
								}
								num2 = 3;
								continue;
							}
							break;
						}
						case 1:
							if (true)
							{
							}
							goto IL_38;
						case 2:
							return;
						case 3:
							goto IL_38;
						}
						break;
						IL_38:
						num2 = 0;
					}
				}
			}
		}

		// Token: 0x06006155 RID: 24917 RVA: 0x003D9514 File Offset: 0x003D8514
		public int CompareTo(IGradient gradient)
		{
			for (;;)
			{
				for (;;)
				{
					int num = 0;
					int count = this.ᜀ.Count;
					int num2 = 4;
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
									num2 = 5;
									continue;
								}
								if (true)
								{
								}
								IXLSRange ixlsrange = this.ᜀ[num];
								num2 = 1;
								continue;
							}
							}
							break;
						case 1:
						{
							IXLSRange ixlsrange;
							if (ixlsrange.Style.Interior.Gradient.CompareTo(gradient) != 0)
							{
								num2 = 3;
								continue;
							}
							num++;
							num2 = 2;
							continue;
						}
						case 2:
							goto IL_94;
						case 3:
							return 1;
						case 4:
							goto IL_94;
						case 5:
							return 0;
						}
						break;
						IL_94:
						num2 = 0;
					}
				}
			}
			return 1;
		}

		// Token: 0x06006156 RID: 24918 RVA: 0x003D95F0 File Offset: 0x003D85F0
		public void TwoColorGradient()
		{
			for (;;)
			{
				for (;;)
				{
					int num = 0;
					int count = this.ᜀ.Count;
					int num2 = 1;
					for (;;)
					{
						if (true)
						{
						}
						switch (num2)
						{
						case 0:
							return;
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
							ixlsrange.Style.Interior.Gradient.TwoColorGradient();
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

		// Token: 0x06006157 RID: 24919 RVA: 0x003D96A4 File Offset: 0x003D86A4
		public void TwoColorGradient(GradientStyleType style, GradientVariantsType variant)
		{
			for (;;)
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
							return;
						case 1:
						{
							if (num >= count)
							{
								num2 = 0;
								continue;
							}
							IXLSRange ixlsrange = this.ᜀ[num];
							ixlsrange.Style.Interior.Gradient.TwoColorGradient(style, variant);
							num++;
							num2 = 2;
							continue;
						}
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
								goto IL_30;
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
			}
		}

		// Token: 0x06006158 RID: 24920 RVA: 0x003D9758 File Offset: 0x003D8758
		public void BeginUpdate()
		{
			for (;;)
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
							ixlsrange.Style.Interior.Gradient.Wrapped.BeginUpdate();
							num++;
							num2 = 3;
							continue;
						}
						case 1:
							goto IL_30;
						case 2:
							return;
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
								goto IL_30;
							}
							break;
						}
						break;
						IL_30:
						if (true)
						{
						}
						num2 = 0;
					}
				}
			}
		}

		// Token: 0x06006159 RID: 24921 RVA: 0x003D9810 File Offset: 0x003D8810
		public void EndUpdate()
		{
			for (;;)
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
								break;
							default:
								if (false)
								{
								}
								goto IL_30;
							}
							break;
						case 1:
						{
							if (num >= count)
							{
								num2 = 2;
								continue;
							}
							IXLSRange ixlsrange = this.ᜀ[num];
							ixlsrange.Style.Interior.Gradient.Wrapped.EndUpdate();
							num++;
							num2 = 0;
							continue;
						}
						case 2:
							goto IL_44;
						case 3:
							goto IL_30;
						}
						break;
						IL_30:
						num2 = 1;
					}
				}
			}
			IL_44:
			if (true)
			{
			}
		}

		// Token: 0x04002E81 RID: 11905
		private byte \u25D9\u00A5\u0089\u00A1;

		// Token: 0x04002E82 RID: 11906
		private List<IXLSRange> ᜀ = new List<IXLSRange>();
	}
}
