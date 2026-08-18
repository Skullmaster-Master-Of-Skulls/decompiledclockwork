using System;
using System.Collections.Generic;
using System.Drawing;

namespace Spire.Xls.Core.Spreadsheet.Collections
{
	// Token: 0x020001F9 RID: 505
	public class BordersCollectionArrayWrapper : CollectionExtended<object>, IBorders
	{
		// Token: 0x06001C89 RID: 7305 RVA: 0x000F6A80 File Offset: 0x000F5A80
		public BordersCollectionArrayWrapper(IXLSRange range) : base(((XlsRange)range).Application, range)
		{
			this.ᜀ.AddRange(range.Cells);
		}

		// Token: 0x17000A9E RID: 2718
		// (get) Token: 0x06001C8A RID: 7306 RVA: 0x000F6ABC File Offset: 0x000F5ABC
		// (set) Token: 0x06001C8B RID: 7307 RVA: 0x000F6BA0 File Offset: 0x000F5BA0
		public ExcelColors KnownColor
		{
			get
			{
				for (;;)
				{
					ExcelColors knownColor = this.ᜀ[0].Borders.KnownColor;
					int num = 1;
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
								goto IL_B0;
							default:
								goto IL_6F;
							}
							break;
						case 1:
							return knownColor;
						case 2:
							if (this.ᜀ[num].Borders.KnownColor != knownColor)
							{
								num2 = 0;
								continue;
							}
							if (true)
							{
							}
							num++;
							num2 = 4;
							continue;
						case 3:
							goto IL_A5;
						case 4:
							goto IL_A5;
						case 5:
							goto IL_B0;
						}
						break;
						IL_B0:
						if (num >= this.ᜀ.Count)
						{
							num2 = 1;
							continue;
						}
						num2 = 2;
						continue;
						IL_A5:
						num2 = 5;
					}
				}
				IL_6F:
				if (false)
				{
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
							return;
						case 1:
							goto IL_38;
						case 2:
							if (true)
							{
							}
							goto IL_38;
						case 3:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								return;
							default:
								if (false)
								{
								}
								if (num >= count)
								{
									num2 = 0;
									continue;
								}
								this.ᜀ[num].Borders.KnownColor = value;
								num++;
								num2 = 1;
								continue;
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

		// Token: 0x17000A9F RID: 2719
		// (get) Token: 0x06001C8C RID: 7308 RVA: 0x000F6C4C File Offset: 0x000F5C4C
		// (set) Token: 0x06001C8D RID: 7309 RVA: 0x000F6D70 File Offset: 0x000F5D70
		public Color Color
		{
			get
			{
				switch (0)
				{
				default:
				{
					Color result;
					for (;;)
					{
						result = this.ᜀ[0].Borders.Color;
						int num = result.ToArgb();
						int num2 = 1;
						int num3 = 6;
						for (;;)
						{
							switch (num3)
							{
							case 0:
							{
								if (num2 >= this.ᜀ.Count)
								{
									num3 = 4;
									continue;
								}
								Color color = this.ᜀ[num2].Borders.Color;
								num3 = 2;
								continue;
							}
							case 1:
								goto IL_C7;
							case 2:
							{
								Color color;
								if (color.ToArgb() != num)
								{
									num3 = 5;
									continue;
								}
								num2++;
								num3 = 1;
								continue;
							}
							case 3:
								goto IL_87;
							case 4:
								if (true)
								{
								}
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_67;
								default:
									goto IL_10B;
								}
								break;
							case 5:
								result = spr\u1D39.ᜂ;
								num3 = 3;
								continue;
							case 6:
								goto IL_67;
							}
							break;
							IL_C7:
							num3 = 0;
							continue;
							IL_67:
							goto IL_C7;
						}
					}
					IL_87:
					return result;
					IL_10B:
					if (false)
					{
					}
					return result;
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
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								return;
							default:
								if (false)
								{
								}
								if (num >= count)
								{
									num2 = 3;
									continue;
								}
								this.ᜀ[num].Borders.Color = value;
								num++;
								if (true)
								{
								}
								num2 = 0;
								continue;
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

		// Token: 0x17000AA0 RID: 2720
		public IBorder this[BordersLineType Index]
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
				return new XlsBorderArrayWrapper((IXLSRange)base.Parent, Index);
			}
		}

		// Token: 0x17000AA1 RID: 2721
		// (get) Token: 0x06001C8F RID: 7311 RVA: 0x000F6E68 File Offset: 0x000F5E68
		// (set) Token: 0x06001C90 RID: 7312 RVA: 0x000F6F4C File Offset: 0x000F5F4C
		public LineStyleType LineStyle
		{
			get
			{
				for (;;)
				{
					LineStyleType lineStyle = this.ᜀ[0].Borders.LineStyle;
					int num = 1;
					int num2 = 3;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							return lineStyle;
						case 1:
							goto IL_B0;
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_B0;
							default:
								goto IL_6F;
							}
							break;
						case 3:
							goto IL_A5;
						case 4:
							goto IL_A5;
						case 5:
							if (lineStyle != this.ᜀ[num].Borders.LineStyle)
							{
								num2 = 2;
								continue;
							}
							num++;
							if (true)
							{
							}
							num2 = 4;
							continue;
						}
						break;
						IL_B0:
						if (num >= this.ᜀ.Count)
						{
							num2 = 0;
							continue;
						}
						num2 = 5;
						continue;
						IL_A5:
						num2 = 1;
					}
				}
				IL_6F:
				if (false)
				{
				}
				return LineStyleType.None;
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
							if (true)
							{
							}
							goto IL_38;
						case 1:
							return;
						case 2:
							goto IL_38;
						case 3:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								return;
							default:
								if (false)
								{
								}
								if (num >= count)
								{
									num2 = 1;
									continue;
								}
								this.ᜀ[num].Borders.LineStyle = value;
								num++;
								num2 = 2;
								continue;
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

		// Token: 0x17000AA2 RID: 2722
		// (get) Token: 0x06001C91 RID: 7313 RVA: 0x000F6FF8 File Offset: 0x000F5FF8
		// (set) Token: 0x06001C92 RID: 7314 RVA: 0x000F703C File Offset: 0x000F603C
		public LineStyleType Value
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
				return this.LineStyle;
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
				this.LineStyle = value;
			}
		}

		// Token: 0x06001C93 RID: 7315 RVA: 0x000F7080 File Offset: 0x000F6080
		private new void ᜀ()
		{
			int a_ = 19;
			this.ᜁ = (base.FindParent(typeof(XlsWorkbook)) as XlsWorkbook);
			if (this.ᜁ != null)
			{
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
					return;
				}
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("᥈⩊㽌⩎㽐❒畔㡖㭘ㅚ㡜㱞ᕠ䍢٤٦ݨժɬ᭮兰ᅲၴ坶ὸᑺࡼᅾ궂", a_));
		}

		// Token: 0x04001084 RID: 4228
		private int \u2593ª\u0090\u008D;

		// Token: 0x04001085 RID: 4229
		private string \u2460\u009C\u0082\u0089;

		// Token: 0x04001086 RID: 4230
		private new List<IXLSRange> ᜀ = new List<IXLSRange>();

		// Token: 0x04001087 RID: 4231
		private new XlsWorkbook ᜁ;
	}
}
