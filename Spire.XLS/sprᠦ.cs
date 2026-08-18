using System;
using System.Drawing;
using System.Reflection;
using Spire.Xls;
using Spire.Xls.Core;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x020004F6 RID: 1270
[DefaultMember("Item")]
internal class sprᠦ : XlsObject, IFont
{
	// Token: 0x06004D7C RID: 19836 RVA: 0x002F5140 File Offset: 0x002F4140
	internal sprᠦ(spr\u1DF5 A_0, object A_1) : base(A_0, A_1)
	{
		this.ᜀ();
	}

	// Token: 0x06004D7D RID: 19837 RVA: 0x002F515C File Offset: 0x002F415C
	private void ᜀ()
	{
		int a_ = 2;
		this.ᜀ = (base.FindParent(typeof(sprᴖ)) as sprᴖ);
		if (this.ᜀ == null)
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
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("䠷嬹主嬽⸿㙁", a_), RecordTableEnumerator.b("样嬹主嬽⸿㙁摃⥅⩇⁉⥋ⵍ⑏牑㝓㝕㙗㑙㍛⩝䁟aţ䙥๧թᥫmᑯ山", a_));
			}
		}
	}

	// Token: 0x06004D7E RID: 19838 RVA: 0x002F51E8 File Offset: 0x002F41E8
	public IFont ᜀ(int A_0)
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
		return this.ᜀ.ᜀ(A_0).Font;
	}

	// Token: 0x06004D7F RID: 19839 RVA: 0x002F5234 File Offset: 0x002F4234
	public int ᜇ()
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

	// Token: 0x06004D80 RID: 19840 RVA: 0x002F527C File Offset: 0x002F427C
	public bool ᜆ()
	{
		for (;;)
		{
			int num = this.ᜇ();
			int num2 = 4;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_9B;
				case 1:
				{
					bool isBold;
					int num3;
					if (isBold != this.ᜀ(num3).IsBold)
					{
						num2 = 7;
						continue;
					}
					num3++;
					goto IL_52;
				}
				case 2:
					return false;
				case 3:
					goto IL_9B;
				case 4:
				{
					if (num == 0)
					{
						num2 = 2;
						continue;
					}
					if (true)
					{
					}
					bool isBold = this.ᜀ(0).IsBold;
					int num3 = 1;
					num2 = 3;
					continue;
				}
				case 5:
				{
					int num3;
					if (num3 >= num)
					{
						num2 = 6;
						continue;
					}
					num2 = 1;
					continue;
				}
				case 6:
				{
					bool isBold;
					return isBold;
				}
				case 7:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_52;
					default:
						goto IL_91;
					}
					break;
				}
				break;
				IL_52:
				num2 = 0;
				continue;
				IL_9B:
				num2 = 5;
			}
		}
		return false;
		IL_91:
		if (false)
		{
		}
		return false;
	}

	// Token: 0x06004D81 RID: 19841 RVA: 0x002F5368 File Offset: 0x002F4368
	public void ᜃ(bool A_0)
	{
		for (;;)
		{
			int num = 0;
			int num2 = this.ᜇ();
			int num3 = 1;
			for (;;)
			{
				switch (num3)
				{
				case 0:
					return;
				case 1:
					goto IL_2B;
				case 2:
					if (num >= num2)
					{
						num3 = 0;
						continue;
					}
					this.ᜀ(num).IsBold = A_0;
					num++;
					if (true)
					{
					}
					num3 = 3;
					continue;
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
						goto IL_2B;
					}
					break;
				}
				break;
				IL_2B:
				num3 = 2;
			}
		}
	}

	// Token: 0x06004D82 RID: 19842 RVA: 0x002F5400 File Offset: 0x002F4400
	public ExcelColors ᜌ()
	{
		for (;;)
		{
			int num = this.ᜇ();
			int num2 = 2;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_A9;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_55;
					default:
						goto IL_9F;
					}
					break;
				case 2:
				{
					if (num == 0)
					{
						num2 = 3;
						continue;
					}
					ExcelColors knownColor = this.ᜀ(0).KnownColor;
					int num3 = 1;
					num2 = 0;
					continue;
				}
				case 3:
					return ExcelColors.Black;
				case 4:
				{
					ExcelColors knownColor;
					int num3;
					if (knownColor != this.ᜀ(num3).KnownColor)
					{
						num2 = 1;
						continue;
					}
					num3++;
					goto IL_55;
				}
				case 5:
				{
					ExcelColors knownColor;
					return knownColor;
				}
				case 6:
					goto IL_A9;
				case 7:
				{
					int num3;
					if (num3 >= num)
					{
						num2 = 5;
						continue;
					}
					num2 = 4;
					continue;
				}
				}
				break;
				IL_55:
				if (true)
				{
				}
				num2 = 6;
				continue;
				IL_A9:
				num2 = 7;
			}
		}
		return ExcelColors.Black;
		IL_9F:
		if (false)
		{
		}
		return ExcelColors.Black;
	}

	// Token: 0x06004D83 RID: 19843 RVA: 0x002F54F4 File Offset: 0x002F44F4
	public void ᜀ(ExcelColors A_0)
	{
		for (;;)
		{
			int num = 0;
			int num2 = this.ᜇ();
			int num3 = 3;
			for (;;)
			{
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
						goto IL_3D;
					}
					break;
				case 1:
					if (num >= num2)
					{
						num3 = 2;
						continue;
					}
					this.ᜀ(num).KnownColor = A_0;
					num++;
					num3 = 0;
					continue;
				case 2:
					return;
				case 3:
					if (true)
					{
					}
					goto IL_3D;
				}
				break;
				IL_3D:
				num3 = 1;
			}
		}
	}

	// Token: 0x06004D84 RID: 19844 RVA: 0x002F558C File Offset: 0x002F458C
	public Color \u1712()
	{
		for (;;)
		{
			int num = this.ᜇ();
			int num2 = 2;
			for (;;)
			{
				switch (num2)
				{
				case 0:
				{
					Color color;
					return color;
				}
				case 1:
					goto IL_4F;
				case 2:
				{
					if (num == 0)
					{
						num2 = 1;
						continue;
					}
					Color color = this.ᜀ(0).Color;
					int num3 = 1;
					num2 = 7;
					continue;
				}
				case 3:
				{
					int num3;
					if (num3 >= num)
					{
						num2 = 0;
						continue;
					}
					num2 = 4;
					continue;
				}
				case 4:
				{
					Color color;
					int num3;
					if (color != this.ᜀ(num3).Color)
					{
						num2 = 5;
						continue;
					}
					num3++;
					goto IL_55;
				}
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_55;
					default:
						goto IL_9C;
					}
					break;
				case 6:
					goto IL_B2;
				case 7:
					goto IL_B2;
				}
				break;
				IL_55:
				num2 = 6;
				continue;
				IL_B2:
				num2 = 3;
			}
		}
		IL_4F:
		return spr\u1D39.ᜂ;
		IL_9C:
		if (false)
		{
		}
		if (true)
		{
		}
		return spr\u1D39.ᜂ;
	}

	// Token: 0x06004D85 RID: 19845 RVA: 0x002F568C File Offset: 0x002F468C
	public void ᜀ(Color A_0)
	{
		for (;;)
		{
			int num = 0;
			int num2 = this.ᜇ();
			if (true)
			{
			}
			int num3 = 0;
			for (;;)
			{
				switch (num3)
				{
				case 0:
					goto IL_3D;
				case 1:
					if (num >= num2)
					{
						num3 = 3;
						continue;
					}
					this.ᜀ(num).Color = A_0;
					num++;
					num3 = 2;
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
						goto IL_3D;
					}
					break;
				case 3:
					return;
				}
				break;
				IL_3D:
				num3 = 1;
			}
		}
	}

	// Token: 0x06004D86 RID: 19846 RVA: 0x002F5724 File Offset: 0x002F4724
	public bool ᜅ()
	{
		bool isItalic;
		for (;;)
		{
			int num = this.ᜇ();
			int num2 = 3;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_B8;
				case 1:
					goto IL_9E;
				case 2:
				{
					int num3;
					if (isItalic != this.ᜀ(num3).IsItalic)
					{
						num2 = 4;
						continue;
					}
					num3++;
					goto IL_55;
				}
				case 3:
				{
					if (num == 0)
					{
						num2 = 7;
						continue;
					}
					isItalic = this.ᜀ(0).IsItalic;
					int num3 = 1;
					num2 = 1;
					continue;
				}
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_55;
					default:
						goto IL_94;
					}
					break;
				case 5:
					goto IL_9E;
				case 6:
				{
					int num3;
					if (num3 >= num)
					{
						num2 = 0;
						continue;
					}
					num2 = 2;
					continue;
				}
				case 7:
					return false;
				}
				break;
				IL_55:
				num2 = 5;
				continue;
				IL_9E:
				num2 = 6;
			}
		}
		return false;
		IL_94:
		if (false)
		{
		}
		return false;
		IL_B8:
		if (true)
		{
		}
		return isItalic;
	}

	// Token: 0x06004D87 RID: 19847 RVA: 0x002F5814 File Offset: 0x002F4814
	public void ᜂ(bool A_0)
	{
		for (;;)
		{
			int num = 0;
			int num2 = this.ᜇ();
			int num3 = 2;
			for (;;)
			{
				if (true)
				{
				}
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
					this.ᜀ(num).IsItalic = A_0;
					num++;
					num3 = 3;
					continue;
				case 2:
					goto IL_3D;
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
						goto IL_3D;
					}
					break;
				}
				break;
				IL_3D:
				num3 = 1;
			}
		}
	}

	// Token: 0x06004D88 RID: 19848 RVA: 0x002F58AC File Offset: 0x002F48AC
	public bool ᜊ()
	{
		for (;;)
		{
			int num = this.ᜇ();
			int num2 = 7;
			for (;;)
			{
				switch (num2)
				{
				case 0:
				{
					bool macOSOutlineFont;
					return macOSOutlineFont;
				}
				case 1:
				{
					int num3;
					if (num3 >= num)
					{
						num2 = 0;
						continue;
					}
					num2 = 4;
					continue;
				}
				case 2:
					goto IL_AE;
				case 3:
					return false;
				case 4:
				{
					bool macOSOutlineFont;
					int num3;
					if (macOSOutlineFont != ((XlsFont)this.ᜀ(num3)).MacOSOutlineFont)
					{
						num2 = 6;
						continue;
					}
					num3++;
					goto IL_55;
				}
				case 5:
					goto IL_AE;
				case 6:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_55;
					default:
						goto IL_A4;
					}
					break;
				case 7:
				{
					if (num == 0)
					{
						num2 = 3;
						continue;
					}
					bool macOSOutlineFont = ((XlsFont)this.ᜀ(0)).MacOSOutlineFont;
					int num3 = 1;
					num2 = 5;
					continue;
				}
				}
				break;
				IL_55:
				if (true)
				{
				}
				num2 = 2;
				continue;
				IL_AE:
				num2 = 1;
			}
		}
		return false;
		IL_A4:
		if (false)
		{
		}
		return false;
	}

	// Token: 0x06004D89 RID: 19849 RVA: 0x002F59A8 File Offset: 0x002F49A8
	public void ᜀ(bool A_0)
	{
		for (;;)
		{
			int num = 0;
			int num2 = this.ᜇ();
			if (true)
			{
			}
			int num3 = 0;
			for (;;)
			{
				switch (num3)
				{
				case 0:
					goto IL_3D;
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
						goto IL_3D;
					}
					break;
				case 2:
					if (num >= num2)
					{
						num3 = 3;
						continue;
					}
					((XlsFont)this.ᜀ(num)).MacOSOutlineFont = A_0;
					num++;
					num3 = 1;
					continue;
				case 3:
					return;
				}
				break;
				IL_3D:
				num3 = 2;
			}
		}
	}

	// Token: 0x06004D8A RID: 19850 RVA: 0x002F5A44 File Offset: 0x002F4A44
	public bool ᜏ()
	{
		for (;;)
		{
			int num = this.ᜇ();
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
						goto IL_55;
					default:
						goto IL_A4;
					}
					break;
				case 1:
					goto IL_AE;
				case 2:
					goto IL_AE;
				case 3:
					return false;
				case 4:
				{
					if (num == 0)
					{
						num2 = 3;
						continue;
					}
					bool macOSShadow = ((XlsFont)this.ᜀ(0)).MacOSShadow;
					int num3 = 1;
					num2 = 1;
					continue;
				}
				case 5:
				{
					bool macOSShadow;
					int num3;
					if (macOSShadow != ((XlsFont)this.ᜀ(num3)).MacOSShadow)
					{
						num2 = 0;
						continue;
					}
					num3++;
					goto IL_55;
				}
				case 6:
				{
					int num3;
					if (num3 >= num)
					{
						num2 = 7;
						continue;
					}
					num2 = 5;
					continue;
				}
				case 7:
				{
					bool macOSShadow;
					return macOSShadow;
				}
				}
				break;
				IL_55:
				if (true)
				{
				}
				num2 = 2;
				continue;
				IL_AE:
				num2 = 6;
			}
		}
		return false;
		IL_A4:
		if (false)
		{
		}
		return false;
	}

	// Token: 0x06004D8B RID: 19851 RVA: 0x002F5B40 File Offset: 0x002F4B40
	public void ᜄ(bool A_0)
	{
		for (;;)
		{
			int num = 0;
			int num2 = this.ᜇ();
			int num3 = 0;
			for (;;)
			{
				switch (num3)
				{
				case 0:
					goto IL_2B;
				case 1:
					if (true)
					{
					}
					if (num >= num2)
					{
						num3 = 3;
						continue;
					}
					((XlsFont)this.ᜀ(num)).MacOSShadow = A_0;
					num++;
					num3 = 2;
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
						goto IL_2B;
					}
					break;
				case 3:
					return;
				}
				break;
				IL_2B:
				num3 = 1;
			}
		}
	}

	// Token: 0x06004D8C RID: 19852 RVA: 0x002F5BDC File Offset: 0x002F4BDC
	public double ᜐ()
	{
		for (;;)
		{
			int num = this.ᜇ();
			int num2 = 1;
			for (;;)
			{
				if (true)
				{
				}
				switch (num2)
				{
				case 0:
					goto IL_B1;
				case 1:
				{
					if (num == 0)
					{
						num2 = 3;
						continue;
					}
					double size = this.ᜀ(0).Size;
					int num3 = 1;
					num2 = 7;
					continue;
				}
				case 2:
				{
					double size;
					return size;
				}
				case 3:
					goto IL_57;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_5D;
					default:
						goto IL_9F;
					}
					break;
				case 5:
				{
					double size;
					int num3;
					if (size != this.ᜀ(num3).Size)
					{
						num2 = 4;
						continue;
					}
					num3++;
					goto IL_5D;
				}
				case 6:
				{
					int num3;
					if (num3 >= num)
					{
						num2 = 2;
						continue;
					}
					num2 = 5;
					continue;
				}
				case 7:
					goto IL_B1;
				}
				break;
				IL_5D:
				num2 = 0;
				continue;
				IL_B1:
				num2 = 6;
			}
		}
		IL_57:
		return -2147483648.0;
		IL_9F:
		if (false)
		{
		}
		return double.MinValue;
	}

	// Token: 0x06004D8D RID: 19853 RVA: 0x002F5CE0 File Offset: 0x002F4CE0
	public void ᜀ(double A_0)
	{
		for (;;)
		{
			int num = 0;
			int num2 = this.ᜇ();
			int num3 = 1;
			for (;;)
			{
				switch (num3)
				{
				case 0:
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
						goto IL_2B;
					}
					break;
				case 1:
					goto IL_2B;
				case 2:
					if (num >= num2)
					{
						num3 = 3;
						continue;
					}
					this.ᜀ(num).Size = A_0;
					num++;
					num3 = 0;
					continue;
				case 3:
					return;
				}
				break;
				IL_2B:
				num3 = 2;
			}
		}
	}

	// Token: 0x06004D8E RID: 19854 RVA: 0x002F5D78 File Offset: 0x002F4D78
	public bool ᜋ()
	{
		for (;;)
		{
			int num = this.ᜇ();
			int num2 = 3;
			for (;;)
			{
				switch (num2)
				{
				case 0:
				{
					int num3;
					if (num3 >= num)
					{
						num2 = 7;
						continue;
					}
					num2 = 4;
					continue;
				}
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_55;
					default:
						goto IL_9F;
					}
					break;
				case 2:
					return false;
				case 3:
				{
					if (num == 0)
					{
						num2 = 2;
						continue;
					}
					bool isStrikethrough = this.ᜀ(0).IsStrikethrough;
					int num3 = 1;
					num2 = 6;
					continue;
				}
				case 4:
				{
					int num3;
					bool isStrikethrough;
					if (isStrikethrough != this.ᜀ(num3).IsStrikethrough)
					{
						num2 = 1;
						continue;
					}
					num3++;
					goto IL_55;
				}
				case 5:
					goto IL_A9;
				case 6:
					goto IL_A9;
				case 7:
				{
					bool isStrikethrough;
					return isStrikethrough;
				}
				}
				break;
				IL_55:
				if (true)
				{
				}
				num2 = 5;
				continue;
				IL_A9:
				num2 = 0;
			}
		}
		return false;
		IL_9F:
		if (false)
		{
		}
		return false;
	}

	// Token: 0x06004D8F RID: 19855 RVA: 0x002F5E6C File Offset: 0x002F4E6C
	public void ᜁ(bool A_0)
	{
		for (;;)
		{
			int num = 0;
			int num2 = this.ᜇ();
			int num3 = 2;
			for (;;)
			{
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
						goto IL_2B;
					}
					break;
				case 1:
					if (num >= num2)
					{
						num3 = 3;
						continue;
					}
					this.ᜀ(num).IsStrikethrough = A_0;
					num++;
					num3 = 0;
					continue;
				case 2:
					goto IL_2B;
				case 3:
					goto IL_49;
				}
				break;
				IL_2B:
				num3 = 1;
			}
		}
		IL_49:
		if (true)
		{
		}
	}

	// Token: 0x06004D90 RID: 19856 RVA: 0x002F5F04 File Offset: 0x002F4F04
	public bool ᜂ()
	{
		for (;;)
		{
			int num = this.ᜇ();
			if (true)
			{
			}
			int num2 = 3;
			for (;;)
			{
				switch (num2)
				{
				case 0:
				{
					bool isSubscript;
					int num3;
					if (isSubscript != this.ᜀ(num3).IsSubscript)
					{
						num2 = 7;
						continue;
					}
					num3++;
					goto IL_5A;
				}
				case 1:
					goto IL_A6;
				case 2:
					return false;
				case 3:
				{
					if (num == 0)
					{
						num2 = 2;
						continue;
					}
					bool isSubscript = this.ᜀ(0).IsSubscript;
					int num3 = 1;
					num2 = 1;
					continue;
				}
				case 4:
				{
					int num3;
					if (num3 >= num)
					{
						num2 = 5;
						continue;
					}
					num2 = 0;
					continue;
				}
				case 5:
				{
					bool isSubscript;
					return isSubscript;
				}
				case 6:
					goto IL_A6;
				case 7:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_5A;
					default:
						goto IL_9C;
					}
					break;
				}
				break;
				IL_5A:
				num2 = 6;
				continue;
				IL_A6:
				num2 = 4;
			}
		}
		return false;
		IL_9C:
		if (false)
		{
		}
		return false;
	}

	// Token: 0x06004D91 RID: 19857 RVA: 0x002F5FF4 File Offset: 0x002F4FF4
	public void ᜅ(bool A_0)
	{
		for (;;)
		{
			int num = 0;
			int num2 = this.ᜇ();
			int num3 = 2;
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
						goto IL_2B;
					}
					break;
				case 2:
					goto IL_2B;
				case 3:
					if (num >= num2)
					{
						num3 = 0;
						continue;
					}
					if (true)
					{
					}
					this.ᜀ(num).IsSubscript = A_0;
					num++;
					num3 = 1;
					continue;
				}
				break;
				IL_2B:
				num3 = 3;
			}
		}
	}

	// Token: 0x06004D92 RID: 19858 RVA: 0x002F608C File Offset: 0x002F508C
	public bool ᜃ()
	{
		for (;;)
		{
			int num = this.ᜇ();
			int num2 = 7;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_48;
					default:
						goto IL_87;
					}
					break;
				case 1:
				{
					int num3;
					if (num3 >= num)
					{
						num2 = 6;
						continue;
					}
					num2 = 4;
					continue;
				}
				case 2:
					goto IL_91;
				case 3:
					goto IL_91;
				case 4:
				{
					int num3;
					bool isSuperscript;
					if (isSuperscript != this.ᜀ(num3).IsSuperscript)
					{
						num2 = 0;
						continue;
					}
					num3++;
					goto IL_48;
				}
				case 5:
					return false;
				case 6:
				{
					bool isSuperscript;
					return isSuperscript;
				}
				case 7:
				{
					if (num == 0)
					{
						num2 = 5;
						continue;
					}
					if (true)
					{
					}
					bool isSuperscript = this.ᜀ(0).IsSuperscript;
					int num3 = 1;
					num2 = 2;
					continue;
				}
				}
				break;
				IL_48:
				num2 = 3;
				continue;
				IL_91:
				num2 = 1;
			}
		}
		return false;
		IL_87:
		if (false)
		{
		}
		return false;
	}

	// Token: 0x06004D93 RID: 19859 RVA: 0x002F6178 File Offset: 0x002F5178
	public void ᜆ(bool A_0)
	{
		for (;;)
		{
			int num = 0;
			int num2 = this.ᜇ();
			int num3 = 2;
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
						goto IL_8B;
					}
					if (false)
					{
					}
					if (num >= num2)
					{
						num3 = 1;
						continue;
					}
					this.ᜀ(num).IsSuperscript = A_0;
					num++;
					num3 = 3;
					continue;
				case 1:
					return;
				case 2:
					goto IL_33;
				case 3:
					goto IL_8B;
				}
				break;
				IL_33:
				num3 = 0;
				continue;
				IL_8B:
				goto IL_33;
			}
		}
	}

	// Token: 0x06004D94 RID: 19860 RVA: 0x002F6214 File Offset: 0x002F5214
	public FontUnderlineType ᜉ()
	{
		for (;;)
		{
			int num = this.ᜇ();
			int num2 = 4;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_97;
				case 1:
					goto IL_97;
				case 2:
				{
					int num3;
					if (num3 >= num)
					{
						if (true)
						{
						}
						num2 = 3;
						continue;
					}
					num2 = 6;
					continue;
				}
				case 3:
				{
					FontUnderlineType underline;
					return underline;
				}
				case 4:
				{
					if (num == 0)
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
							num2 = 7;
							continue;
						}
					}
					FontUnderlineType underline = this.ᜀ(0).Underline;
					int num3 = 1;
					num2 = 1;
					continue;
				}
				case 5:
					return FontUnderlineType.None;
				case 6:
				{
					int num3;
					FontUnderlineType underline;
					if (underline != this.ᜀ(num3).Underline)
					{
						num2 = 5;
						continue;
					}
					num3++;
					num2 = 0;
					continue;
				}
				case 7:
					return FontUnderlineType.None;
				}
				break;
				IL_97:
				num2 = 2;
			}
		}
		return FontUnderlineType.None;
	}

	// Token: 0x06004D95 RID: 19861 RVA: 0x002F6308 File Offset: 0x002F5308
	public void ᜀ(FontUnderlineType A_0)
	{
		for (;;)
		{
			int num = 0;
			int num2 = this.ᜇ();
			int num3 = 0;
			for (;;)
			{
				switch (num3)
				{
				case 0:
					goto IL_2B;
				case 1:
					goto IL_65;
				case 2:
					goto IL_80;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_80;
					default:
						if (false)
						{
						}
						if (num >= num2)
						{
							num3 = 1;
							continue;
						}
						this.ᜀ(num).Underline = A_0;
						num++;
						num3 = 2;
						continue;
					}
					break;
				}
				break;
				IL_2B:
				num3 = 3;
				continue;
				IL_80:
				goto IL_2B;
			}
		}
		IL_65:
		if (true)
		{
		}
	}

	// Token: 0x06004D96 RID: 19862 RVA: 0x002F63A0 File Offset: 0x002F53A0
	public string ᜈ()
	{
		for (;;)
		{
			int num = this.ᜇ();
			int num2 = 2;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_95;
				case 1:
				{
					string fontName;
					return fontName;
				}
				case 2:
				{
					if (num == 0)
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
							num2 = 7;
							continue;
						}
					}
					string fontName = this.ᜀ(0).FontName;
					int num3 = 1;
					if (true)
					{
					}
					num2 = 3;
					continue;
				}
				case 3:
					goto IL_99;
				case 4:
					goto IL_99;
				case 5:
				{
					string fontName;
					int num3;
					if (fontName != this.ᜀ(num3).FontName)
					{
						num2 = 0;
						continue;
					}
					num3++;
					num2 = 4;
					continue;
				}
				case 6:
				{
					int num3;
					if (num3 >= num)
					{
						num2 = 1;
						continue;
					}
					num2 = 5;
					continue;
				}
				case 7:
					goto IL_5E;
				}
				break;
				IL_99:
				num2 = 6;
			}
		}
		IL_5E:
		return null;
		IL_95:
		return null;
	}

	// Token: 0x06004D97 RID: 19863 RVA: 0x002F6494 File Offset: 0x002F5494
	public void ᜀ(string A_0)
	{
		for (;;)
		{
			int num = 0;
			int num2 = this.ᜇ();
			if (true)
			{
			}
			int num3 = 3;
			for (;;)
			{
				switch (num3)
				{
				case 0:
					goto IL_8B;
				case 1:
					return;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_8B;
					}
					if (false)
					{
					}
					if (num >= num2)
					{
						num3 = 1;
						continue;
					}
					this.ᜀ(num).FontName = A_0;
					num++;
					num3 = 0;
					continue;
				case 3:
					goto IL_33;
				}
				break;
				IL_33:
				num3 = 2;
				continue;
				IL_8B:
				goto IL_33;
			}
		}
	}

	// Token: 0x06004D98 RID: 19864 RVA: 0x002F6530 File Offset: 0x002F5530
	public FontVertialAlignmentType ᜁ()
	{
		for (;;)
		{
			int num = this.ᜇ();
			int num2 = 1;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_97;
				case 1:
				{
					if (num == 0)
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
							num2 = 2;
							continue;
						}
					}
					FontVertialAlignmentType verticalAlignment = this.ᜀ(0).VerticalAlignment;
					int num3 = 1;
					num2 = 0;
					continue;
				}
				case 2:
					return FontVertialAlignmentType.Baseline;
				case 3:
				{
					FontVertialAlignmentType verticalAlignment;
					return verticalAlignment;
				}
				case 4:
					return FontVertialAlignmentType.Baseline;
				case 5:
				{
					FontVertialAlignmentType verticalAlignment;
					int num3;
					if (verticalAlignment != this.ᜀ(num3).VerticalAlignment)
					{
						num2 = 4;
						continue;
					}
					num3++;
					num2 = 7;
					continue;
				}
				case 6:
				{
					if (true)
					{
					}
					int num3;
					if (num3 >= num)
					{
						num2 = 3;
						continue;
					}
					num2 = 5;
					continue;
				}
				case 7:
					goto IL_97;
				}
				break;
				IL_97:
				num2 = 6;
			}
		}
		return FontVertialAlignmentType.Baseline;
	}

	// Token: 0x06004D99 RID: 19865 RVA: 0x002F6624 File Offset: 0x002F5624
	public void ᜀ(FontVertialAlignmentType A_0)
	{
		for (;;)
		{
			int num = 0;
			int num2 = this.ᜇ();
			int num3 = 1;
			for (;;)
			{
				switch (num3)
				{
				case 0:
					return;
				case 1:
					goto IL_2B;
				case 2:
					goto IL_8B;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_8B;
					default:
						if (false)
						{
						}
						if (num >= num2)
						{
							num3 = 0;
							continue;
						}
						this.ᜀ(num).VerticalAlignment = A_0;
						num++;
						if (true)
						{
						}
						num3 = 2;
						continue;
					}
					break;
				}
				break;
				IL_2B:
				num3 = 3;
				continue;
				IL_8B:
				goto IL_2B;
			}
		}
	}

	// Token: 0x06004D9A RID: 19866 RVA: 0x002F66C0 File Offset: 0x002F56C0
	public bool ᜎ()
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
		return false;
	}

	// Token: 0x06004D9B RID: 19867 RVA: 0x002F66FC File Offset: 0x002F56FC
	public Font ᜄ()
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
		throw new NotSupportedException();
	}

	// Token: 0x06004D9C RID: 19868 RVA: 0x002F673C File Offset: 0x002F573C
	public void ᜑ()
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

	// Token: 0x06004D9D RID: 19869 RVA: 0x002F6778 File Offset: 0x002F5778
	public void \u170D()
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

	// Token: 0x0400232A RID: 9002
	private sprᴖ ᜀ;
}
