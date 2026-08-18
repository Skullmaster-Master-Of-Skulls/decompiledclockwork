using System;
using System.Drawing;
using System.Reflection;
using Spire.Xls;
using Spire.Xls.Core;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x020002E3 RID: 739
[DefaultMember("Item")]
internal class spr\u2366 : CollectionExtended<object>, IBorders
{
	// Token: 0x06002E12 RID: 11794 RVA: 0x0019E790 File Offset: 0x0019D790
	internal spr\u2366(spr\u1DF5 A_0, object A_1) : base(A_0, A_1)
	{
		this.ᜀ();
		base.InnerList.Add(new spr\u2208(A_0, this, BordersLineType.DiagonalDown));
		base.InnerList.Add(new spr\u2208(A_0, this, BordersLineType.DiagonalUp));
		base.InnerList.Add(new spr\u2208(A_0, this, BordersLineType.EdgeBottom));
		base.InnerList.Add(new spr\u2208(A_0, this, BordersLineType.EdgeLeft));
		base.InnerList.Add(new spr\u2208(A_0, this, BordersLineType.EdgeRight));
		base.InnerList.Add(new spr\u2208(A_0, this, BordersLineType.EdgeTop));
	}

	// Token: 0x06002E13 RID: 11795 RVA: 0x0019E820 File Offset: 0x0019D820
	private new void ᜀ()
	{
		int a_ = 18;
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
			this.ᜀ = (base.FindParent(typeof(sprᴖ)) as sprᴖ);
			if (this.ᜀ != null)
			{
				return;
			}
			break;
		}
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("㡇⭉㹋⭍㹏♑", a_), RecordTableEnumerator.b("ᡇ⭉㹋⭍㹏♑瑓㥕㩗す㥛㵝ᑟ䉡ݣݥ٧ѩͫᩭ偯ၱᅳ噵ṷᕹॻၽ겁", a_));
	}

	// Token: 0x06002E14 RID: 11796 RVA: 0x0019E8AC File Offset: 0x0019D8AC
	public new IBorders ᜀ(int A_0)
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
		return this.ᜀ.ᜀ(A_0).Borders;
	}

	// Token: 0x06002E15 RID: 11797 RVA: 0x0019E8F8 File Offset: 0x0019D8F8
	public int ᜄ()
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
		return this.ᜀ.ᜤ();
	}

	// Token: 0x06002E16 RID: 11798 RVA: 0x0019E940 File Offset: 0x0019D940
	public new ExcelColors ᜂ()
	{
		for (;;)
		{
			int num = this.ᜄ();
			int num2 = 5;
			for (;;)
			{
				switch (num2)
				{
				case 0:
				{
					int num3;
					if (num3 >= num)
					{
						num2 = 2;
						continue;
					}
					num2 = 3;
					continue;
				}
				case 1:
					return ExcelColors.Black;
				case 2:
				{
					ExcelColors knownColor;
					return knownColor;
				}
				case 3:
				{
					int num3;
					ExcelColors knownColor;
					if (knownColor != this.ᜀ(num3).KnownColor)
					{
						num2 = 1;
						continue;
					}
					num3++;
					num2 = 7;
					continue;
				}
				case 4:
					return ExcelColors.Black;
				case 5:
				{
					if (num == 0)
					{
						num2 = 4;
						continue;
					}
					if (true)
					{
					}
					ExcelColors knownColor = this.ᜀ(0).KnownColor;
					int num3 = 1;
					num2 = 6;
					continue;
				}
				case 6:
					goto IL_9E;
				case 7:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						goto IL_9E;
					}
					break;
				}
				break;
				IL_9E:
				num2 = 0;
			}
		}
		return ExcelColors.Black;
	}

	// Token: 0x06002E17 RID: 11799 RVA: 0x0019EA30 File Offset: 0x0019DA30
	public new void ᜀ(ExcelColors A_0)
	{
		for (;;)
		{
			int num = 0;
			int num2 = this.ᜄ();
			int num3 = 1;
			for (;;)
			{
				switch (num3)
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
						if (false)
						{
						}
						goto IL_47;
					}
					break;
				case 2:
					goto IL_47;
				case 3:
					if (num >= num2)
					{
						if (true)
						{
						}
						num3 = 0;
						continue;
					}
					this.ᜀ(num).KnownColor = A_0;
					num++;
					num3 = 2;
					continue;
				}
				break;
				IL_47:
				num3 = 3;
			}
		}
	}

	// Token: 0x06002E18 RID: 11800 RVA: 0x0019EAC8 File Offset: 0x0019DAC8
	public Color ᜆ()
	{
		for (;;)
		{
			int num = this.ᜄ();
			int num2 = 4;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_B5;
				case 1:
				{
					int num3;
					if (num3 >= num)
					{
						num2 = 6;
						continue;
					}
					num2 = 7;
					continue;
				}
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						goto IL_B5;
					}
					break;
				case 3:
					goto IL_4F;
				case 4:
				{
					if (num == 0)
					{
						num2 = 3;
						continue;
					}
					Color color = this.ᜀ(0).Color;
					int num3 = 1;
					num2 = 0;
					continue;
				}
				case 5:
					goto IL_AD;
				case 6:
				{
					Color color;
					return color;
				}
				case 7:
				{
					int num3;
					Color color;
					if (color != this.ᜀ(num3).Color)
					{
						num2 = 5;
						continue;
					}
					num3++;
					if (true)
					{
					}
					num2 = 2;
					continue;
				}
				}
				break;
				IL_B5:
				num2 = 1;
			}
		}
		IL_4F:
		return spr\u1D39.ᜂ;
		IL_AD:
		return spr\u1D39.ᜂ;
	}

	// Token: 0x06002E19 RID: 11801 RVA: 0x0019EBCC File Offset: 0x0019DBCC
	public new void ᜀ(Color A_0)
	{
		for (;;)
		{
			int num = 0;
			int num2 = this.ᜄ();
			int num3 = 3;
			for (;;)
			{
				switch (num3)
				{
				case 0:
					goto IL_4F;
				case 1:
					if (num >= num2)
					{
						num3 = 2;
						continue;
					}
					this.ᜀ(num).Color = A_0;
					num++;
					num3 = 0;
					continue;
				case 2:
					return;
				case 3:
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
						goto IL_4F;
					}
					break;
				}
				break;
				IL_4F:
				num3 = 1;
			}
		}
	}

	// Token: 0x06002E1A RID: 11802 RVA: 0x0019EC64 File Offset: 0x0019DC64
	int IBorders.ᜃ()
	{
		for (;;)
		{
			int num = this.ᜄ();
			int num2 = 0;
			for (;;)
			{
				switch (num2)
				{
				case 0:
				{
					if (num == 0)
					{
						num2 = 4;
						continue;
					}
					int count = this.ᜀ(0).Count;
					int num3 = 1;
					num2 = 6;
					continue;
				}
				case 1:
					goto IL_A0;
				case 2:
				{
					int count;
					int num3;
					if (count != this.ᜀ(num3).Count)
					{
						num2 = 1;
						continue;
					}
					num3++;
					num2 = 5;
					continue;
				}
				case 3:
				{
					int count;
					return count;
				}
				case 4:
					return int.MinValue;
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						goto IL_B0;
					}
					break;
				case 6:
					goto IL_B0;
				case 7:
				{
					int num3;
					if (num3 >= num)
					{
						num2 = 3;
						continue;
					}
					num2 = 2;
					continue;
				}
				}
				break;
				IL_B0:
				num2 = 7;
			}
		}
		return int.MinValue;
		IL_A0:
		if (true)
		{
		}
		return int.MinValue;
	}

	// Token: 0x06002E1B RID: 11803 RVA: 0x0019ED60 File Offset: 0x0019DD60
	public new IBorder ᜀ(BordersLineType A_0)
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
		return null;
	}

	// Token: 0x06002E1C RID: 11804 RVA: 0x0019ED9C File Offset: 0x0019DD9C
	public new LineStyleType ᜁ()
	{
		LineStyleType lineStyle;
		for (;;)
		{
			int num = this.ᜄ();
			int num2 = 2;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_A4;
				case 1:
					goto IL_BE;
				case 2:
				{
					if (num == 0)
					{
						num2 = 5;
						continue;
					}
					lineStyle = this.ᜀ(0).LineStyle;
					int num3 = 1;
					num2 = 0;
					continue;
				}
				case 3:
				{
					int num3;
					if (num3 >= num)
					{
						num2 = 1;
						continue;
					}
					num2 = 6;
					continue;
				}
				case 4:
					return LineStyleType.None;
				case 5:
					return LineStyleType.None;
				case 6:
				{
					int num3;
					if (lineStyle != this.ᜀ(num3).LineStyle)
					{
						num2 = 4;
						continue;
					}
					num3++;
					num2 = 7;
					continue;
				}
				case 7:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						goto IL_A4;
					}
					break;
				}
				break;
				IL_A4:
				num2 = 3;
			}
		}
		return LineStyleType.None;
		IL_BE:
		if (true)
		{
		}
		return lineStyle;
	}

	// Token: 0x06002E1D RID: 11805 RVA: 0x0019EE90 File Offset: 0x0019DE90
	public new void ᜀ(LineStyleType A_0)
	{
		for (;;)
		{
			int num = 0;
			int num2 = this.ᜄ();
			int num3 = 0;
			for (;;)
			{
				if (true)
				{
				}
				switch (num3)
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
						goto IL_4F;
					}
					break;
				case 1:
					return;
				case 2:
					goto IL_4F;
				case 3:
					if (num >= num2)
					{
						num3 = 1;
						continue;
					}
					this.ᜀ(num).LineStyle = A_0;
					num++;
					num3 = 2;
					continue;
				}
				break;
				IL_4F:
				num3 = 3;
			}
		}
	}

	// Token: 0x06002E1E RID: 11806 RVA: 0x0019EF28 File Offset: 0x0019DF28
	public LineStyleType ᜅ()
	{
		for (;;)
		{
			int num = this.ᜄ();
			int num2 = 0;
			for (;;)
			{
				switch (num2)
				{
				case 0:
				{
					if (num == 0)
					{
						num2 = 3;
						continue;
					}
					LineStyleType value = this.ᜀ(0).Value;
					int num3 = 1;
					num2 = 5;
					continue;
				}
				case 1:
				{
					int num3;
					if (num3 >= num)
					{
						num2 = 2;
						continue;
					}
					num2 = 4;
					continue;
				}
				case 2:
				{
					LineStyleType value;
					return value;
				}
				case 3:
					return LineStyleType.None;
				case 4:
				{
					LineStyleType value;
					int num3;
					if (value != this.ᜀ(num3).Value)
					{
						num2 = 6;
						continue;
					}
					num3++;
					if (true)
					{
					}
					num2 = 7;
					continue;
				}
				case 5:
					goto IL_AC;
				case 6:
					return LineStyleType.None;
				case 7:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						goto IL_AC;
					}
					break;
				}
				break;
				IL_AC:
				num2 = 1;
			}
		}
		return LineStyleType.None;
	}

	// Token: 0x06002E1F RID: 11807 RVA: 0x0019F01C File Offset: 0x0019E01C
	public new void ᜁ(LineStyleType A_0)
	{
		for (;;)
		{
			int num = 0;
			int num2 = this.ᜄ();
			if (true)
			{
			}
			int num3 = 2;
			for (;;)
			{
				switch (num3)
				{
				case 0:
					return;
				case 1:
					if (num >= num2)
					{
						num3 = 0;
						continue;
					}
					this.ᜀ(num).Value = A_0;
					num++;
					num3 = 3;
					continue;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						goto IL_4F;
					}
					break;
				case 3:
					goto IL_4F;
				}
				break;
				IL_4F:
				num3 = 1;
			}
		}
	}

	// Token: 0x040014D3 RID: 5331
	private new sprᴖ ᜀ;
}
