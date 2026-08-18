using System;
using System.Drawing;
using System.Reflection;
using Spire.Xls;
using Spire.Xls.Core;
using Spire.Xls.Core.Interfaces;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x020004CF RID: 1231
[DefaultMember("Item")]
internal class sprᴖ : XlsObject, IStyle, IExtendIndex
{
	// Token: 0x06004B7C RID: 19324 RVA: 0x002E3E30 File Offset: 0x002E2E30
	internal sprᴖ(spr\u1DF5 A_0, object A_1) : base(A_0, A_1)
	{
		this.ᜀ();
	}

	// Token: 0x06004B7D RID: 19325 RVA: 0x002E3E4C File Offset: 0x002E2E4C
	private void ᜀ()
	{
		int a_ = 8;
		for (;;)
		{
			this.ᜀ = (base.FindParent(typeof(spr\u1CCF)) as spr\u1CCF);
			if (this.ᜀ != null)
			{
				return;
			}
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_4C;
			}
		}
		IL_4C:
		if (false)
		{
		}
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("丽ℿぁ⅃⡅㱇", a_), RecordTableEnumerator.b("渽ℿぁ⅃⡅㱇橉⍋ⱍ㩏㝑㝓≕硗㥙㵛そ๟ൡၣ䙥੧ཀྵ䱫࡭Ὧݱᩳት噷", a_));
	}

	// Token: 0x06004B7E RID: 19326 RVA: 0x002E3ED8 File Offset: 0x002E2ED8
	public IStyle ᜀ(int A_0)
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
		return this.ᜀ.ᜂ(A_0).Style;
	}

	// Token: 0x06004B7F RID: 19327 RVA: 0x002E3F24 File Offset: 0x002E2F24
	public int ᜤ()
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
		return this.ᜀ.ᜯ();
	}

	// Token: 0x06004B80 RID: 19328 RVA: 0x002E3F6C File Offset: 0x002E2F6C
	internal XlsWorkbook \u1713()
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
		return this.ᜀ.ᜢ();
	}

	// Token: 0x06004B81 RID: 19329 RVA: 0x002E3FB4 File Offset: 0x002E2FB4
	public IBorders ᜐ()
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				for (;;)
				{
					this.ᜂ = new spr\u2366(base.ReservedHandle, this);
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_56;
					}
				}
				IL_56:
				if (true)
				{
				}
				if (false)
				{
				}
				num = 2;
				continue;
			case 2:
				goto IL_76;
			}
			if (this.ᜂ != null)
			{
				break;
			}
			num = 0;
		}
		IL_76:
		return this.ᜂ;
	}

	// Token: 0x06004B82 RID: 19330 RVA: 0x002E4040 File Offset: 0x002E3040
	public bool ᜠ()
	{
		for (;;)
		{
			for (;;)
			{
				int num = this.ᜤ();
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
							return false;
						case 1:
							return false;
						case 2:
						{
							if (num == 0)
							{
								if (true)
								{
								}
								num2 = 0;
								continue;
							}
							bool builtIn = this.ᜀ(0).BuiltIn;
							int num3 = 1;
							num2 = 5;
							continue;
						}
						case 3:
						{
							bool builtIn;
							return builtIn;
						}
						case 4:
							goto IL_A9;
						case 5:
							goto IL_A9;
						case 6:
						{
							bool builtIn;
							int num3;
							if (builtIn != this.ᜀ(num3).BuiltIn)
							{
								num2 = 1;
								continue;
							}
							num3++;
							num2 = 4;
							continue;
						}
						case 7:
						{
							int num3;
							if (num3 >= num)
							{
								num2 = 3;
								continue;
							}
							num2 = 6;
							continue;
						}
						}
						break;
						IL_A9:
						num2 = 7;
					}
					break;
				}
				}
			}
		}
		return false;
	}

	// Token: 0x06004B83 RID: 19331 RVA: 0x002E4134 File Offset: 0x002E3134
	public ExcelPatternType \u170D()
	{
		for (;;)
		{
			for (;;)
			{
				int num = this.ᜤ();
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
						{
							ExcelPatternType fillPattern;
							int num3;
							if (fillPattern != this.ᜀ(num3).FillPattern)
							{
								num2 = 1;
								continue;
							}
							num3++;
							num2 = 6;
							continue;
						}
						case 1:
							return ExcelPatternType.None;
						case 2:
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
						case 3:
						{
							if (true)
							{
							}
							if (num == 0)
							{
								num2 = 4;
								continue;
							}
							ExcelPatternType fillPattern = this.ᜀ(0).FillPattern;
							int num3 = 1;
							num2 = 7;
							continue;
						}
						case 4:
							return ExcelPatternType.None;
						case 5:
						{
							ExcelPatternType fillPattern;
							return fillPattern;
						}
						case 6:
							goto IL_A9;
						case 7:
							goto IL_A9;
						}
						break;
						IL_A9:
						num2 = 2;
					}
					break;
				}
				}
			}
		}
		return ExcelPatternType.None;
	}

	// Token: 0x06004B84 RID: 19332 RVA: 0x002E4228 File Offset: 0x002E3228
	public void ᜀ(ExcelPatternType A_0)
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_80:
			num = 1;
			break;
		default:
			if (false)
			{
			}
			goto IL_34;
		}
		int num2;
		int num3;
		for (;;)
		{
			IL_1E:
			switch (num)
			{
			case 0:
				return;
			case 1:
				goto IL_47;
			case 2:
				goto IL_47;
			case 3:
				if (true)
				{
				}
				if (num2 >= num3)
				{
					num = 0;
					continue;
				}
				goto IL_6F;
			}
			goto IL_34;
			IL_47:
			num = 3;
		}
		return;
		IL_6F:
		this.ᜀ(num2).FillPattern = A_0;
		num2++;
		goto IL_80;
		IL_34:
		num2 = 0;
		num3 = this.ᜤ();
		num = 2;
		goto IL_1E;
	}

	// Token: 0x06004B85 RID: 19333 RVA: 0x002E42C0 File Offset: 0x002E32C0
	public ExcelColors ᜨ()
	{
		for (;;)
		{
			for (;;)
			{
				int num = this.ᜤ();
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
					if (true)
					{
					}
					int num2 = 0;
					for (;;)
					{
						switch (num2)
						{
						case 0:
						{
							if (num == 0)
							{
								num2 = 7;
								continue;
							}
							ExcelColors knownColor = this.ᜀ(0).KnownColor;
							int num3 = 1;
							num2 = 4;
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
							num2 = 3;
							continue;
						}
						case 2:
						{
							ExcelColors knownColor;
							return knownColor;
						}
						case 3:
						{
							ExcelColors knownColor;
							int num3;
							if (knownColor != this.ᜀ(num3).KnownColor)
							{
								num2 = 6;
								continue;
							}
							num3++;
							num2 = 5;
							continue;
						}
						case 4:
							goto IL_A9;
						case 5:
							goto IL_A9;
						case 6:
							return ExcelColors.Black;
						case 7:
							return ExcelColors.Black;
						}
						break;
						IL_A9:
						num2 = 1;
					}
					break;
				}
				}
			}
		}
		return ExcelColors.Black;
	}

	// Token: 0x06004B86 RID: 19334 RVA: 0x002E43B4 File Offset: 0x002E33B4
	public void ᜀ(ExcelColors A_0)
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_80:
			num = 0;
			break;
		default:
			if (false)
			{
			}
			goto IL_34;
		}
		int num2;
		int num3;
		for (;;)
		{
			IL_1E:
			switch (num)
			{
			case 0:
				goto IL_47;
			case 1:
				if (num2 >= num3)
				{
					num = 3;
					continue;
				}
				goto IL_67;
			case 2:
				goto IL_47;
			case 3:
				return;
			}
			goto IL_34;
			IL_47:
			num = 1;
		}
		return;
		IL_67:
		if (true)
		{
		}
		this.ᜀ(num2).KnownColor = A_0;
		num2++;
		goto IL_80;
		IL_34:
		num2 = 0;
		num3 = this.ᜤ();
		num = 2;
		goto IL_1E;
	}

	// Token: 0x06004B87 RID: 19335 RVA: 0x002E444C File Offset: 0x002E344C
	public Color ᜩ()
	{
		for (;;)
		{
			for (;;)
			{
				int num = this.ᜤ();
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
					int num2 = 4;
					for (;;)
					{
						if (true)
						{
						}
						switch (num2)
						{
						case 0:
							goto IL_AA;
						case 1:
							goto IL_70;
						case 2:
							goto IL_B2;
						case 3:
						{
							int num3;
							if (num3 >= num)
							{
								num2 = 7;
								continue;
							}
							num2 = 6;
							continue;
						}
						case 4:
						{
							if (num == 0)
							{
								num2 = 1;
								continue;
							}
							Color color = this.ᜀ(0).Color;
							int num3 = 1;
							num2 = 5;
							continue;
						}
						case 5:
							goto IL_B2;
						case 6:
						{
							int num3;
							Color color;
							if (color != this.ᜀ(num3).Color)
							{
								num2 = 0;
								continue;
							}
							num3++;
							num2 = 2;
							continue;
						}
						case 7:
						{
							Color color;
							return color;
						}
						}
						break;
						IL_B2:
						num2 = 3;
					}
					break;
				}
				}
			}
		}
		IL_70:
		return spr\u1D39.ᜂ;
		IL_AA:
		return spr\u1D39.ᜂ;
	}

	// Token: 0x06004B88 RID: 19336 RVA: 0x002E454C File Offset: 0x002E354C
	public void ᜀ(Color A_0)
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_80:
			num = 1;
			break;
		default:
			if (false)
			{
			}
			goto IL_34;
		}
		int num2;
		int num3;
		for (;;)
		{
			IL_1E:
			switch (num)
			{
			case 0:
				return;
			case 1:
				goto IL_4F;
			case 2:
				if (num2 >= num3)
				{
					num = 0;
					continue;
				}
				goto IL_6F;
			case 3:
				goto IL_4F;
			}
			goto IL_34;
			IL_4F:
			num = 2;
		}
		return;
		IL_6F:
		this.ᜀ(num2).Color = A_0;
		num2++;
		goto IL_80;
		IL_34:
		if (true)
		{
		}
		num2 = 0;
		num3 = this.ᜤ();
		num = 3;
		goto IL_1E;
	}

	// Token: 0x06004B89 RID: 19337 RVA: 0x002E45E4 File Offset: 0x002E35E4
	public ExcelColors ᜑ()
	{
		for (;;)
		{
			for (;;)
			{
				int num = this.ᜤ();
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
					if (true)
					{
					}
					int num2 = 0;
					for (;;)
					{
						switch (num2)
						{
						case 0:
						{
							if (num == 0)
							{
								num2 = 7;
								continue;
							}
							ExcelColors patternKnownColor = this.ᜀ(0).PatternKnownColor;
							int num3 = 1;
							num2 = 5;
							continue;
						}
						case 1:
							goto IL_A9;
						case 2:
						{
							ExcelColors patternKnownColor;
							return patternKnownColor;
						}
						case 3:
						{
							ExcelColors patternKnownColor;
							int num3;
							if (patternKnownColor != this.ᜀ(num3).PatternKnownColor)
							{
								num2 = 4;
								continue;
							}
							num3++;
							num2 = 1;
							continue;
						}
						case 4:
							return ExcelColors.Black;
						case 5:
							goto IL_A9;
						case 6:
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
						case 7:
							return ExcelColors.Black;
						}
						break;
						IL_A9:
						num2 = 6;
					}
					break;
				}
				}
			}
		}
		return ExcelColors.Black;
	}

	// Token: 0x06004B8A RID: 19338 RVA: 0x002E46D8 File Offset: 0x002E36D8
	public void ᜃ(ExcelColors A_0)
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_80:
			num = 2;
			break;
		default:
			if (false)
			{
			}
			goto IL_34;
		}
		int num2;
		int num3;
		for (;;)
		{
			IL_1E:
			switch (num)
			{
			case 0:
				if (num2 >= num3)
				{
					num = 1;
					continue;
				}
				goto IL_6F;
			case 1:
				return;
			case 2:
				goto IL_4F;
			case 3:
				goto IL_4F;
			}
			goto IL_34;
			IL_4F:
			num = 0;
		}
		return;
		IL_6F:
		this.ᜀ(num2).PatternKnownColor = A_0;
		num2++;
		goto IL_80;
		IL_34:
		if (true)
		{
		}
		num2 = 0;
		num3 = this.ᜤ();
		num = 3;
		goto IL_1E;
	}

	// Token: 0x06004B8B RID: 19339 RVA: 0x002E4770 File Offset: 0x002E3770
	public Color \u171E()
	{
		for (;;)
		{
			for (;;)
			{
				int num = this.ᜤ();
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
						{
							if (num == 0)
							{
								num2 = 2;
								continue;
							}
							Color patternColor = this.ᜀ(0).PatternColor;
							int num3 = 1;
							num2 = 4;
							continue;
						}
						case 1:
							goto IL_AA;
						case 2:
							goto IL_68;
						case 3:
						{
							int num3;
							if (num3 >= num)
							{
								num2 = 5;
								continue;
							}
							num2 = 6;
							continue;
						}
						case 4:
							goto IL_B2;
						case 5:
						{
							Color patternColor;
							return patternColor;
						}
						case 6:
						{
							Color patternColor;
							int num3;
							if (patternColor != this.ᜀ(num3).PatternColor)
							{
								num2 = 1;
								continue;
							}
							num3++;
							num2 = 7;
							continue;
						}
						case 7:
							if (true)
							{
							}
							goto IL_B2;
						}
						break;
						IL_B2:
						num2 = 3;
					}
					break;
				}
				}
			}
		}
		IL_68:
		return spr\u1D39.ᜂ;
		IL_AA:
		return spr\u1D39.ᜂ;
	}

	// Token: 0x06004B8C RID: 19340 RVA: 0x002E4870 File Offset: 0x002E3870
	public void ᜁ(Color A_0)
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_80:
			num = 1;
			break;
		default:
			if (true)
			{
			}
			if (false)
			{
			}
			goto IL_3C;
		}
		int num2;
		int num3;
		for (;;)
		{
			IL_26:
			switch (num)
			{
			case 0:
				goto IL_4F;
			case 1:
				goto IL_4F;
			case 2:
				return;
			case 3:
				if (num2 >= num3)
				{
					num = 2;
					continue;
				}
				goto IL_6F;
			}
			goto IL_3C;
			IL_4F:
			num = 3;
		}
		return;
		IL_6F:
		this.ᜀ(num2).PatternColor = A_0;
		num2++;
		goto IL_80;
		IL_3C:
		num2 = 0;
		num3 = this.ᜤ();
		num = 0;
		goto IL_26;
	}

	// Token: 0x06004B8D RID: 19341 RVA: 0x002E4908 File Offset: 0x002E3908
	public IFont ᜥ()
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_76;
			case 1:
				for (;;)
				{
					this.ᜁ = new sprᠦ(base.ReservedHandle, this);
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_56;
					}
				}
				IL_56:
				if (false)
				{
				}
				if (true)
				{
				}
				num = 0;
				continue;
			}
			if (this.ᜁ != null)
			{
				break;
			}
			num = 1;
		}
		IL_76:
		return this.ᜁ;
	}

	// Token: 0x06004B8E RID: 19342 RVA: 0x002E4994 File Offset: 0x002E3994
	public IInterior \u1716()
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

	// Token: 0x06004B8F RID: 19343 RVA: 0x002E49D4 File Offset: 0x002E39D4
	public bool ᜌ()
	{
		bool formulaHidden;
		for (;;)
		{
			for (;;)
			{
				int num = this.ᜤ();
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
						{
							if (num == 0)
							{
								num2 = 3;
								continue;
							}
							formulaHidden = this.ᜀ(0).FormulaHidden;
							int num3 = 1;
							num2 = 6;
							continue;
						}
						case 1:
						{
							int num3;
							if (formulaHidden != this.ᜀ(num3).FormulaHidden)
							{
								num2 = 4;
								continue;
							}
							num3++;
							num2 = 5;
							continue;
						}
						case 2:
						{
							int num3;
							if (num3 >= num)
							{
								num2 = 7;
								continue;
							}
							num2 = 1;
							continue;
						}
						case 3:
							return false;
						case 4:
							return false;
						case 5:
							goto IL_9E;
						case 6:
							goto IL_9E;
						case 7:
							goto IL_B8;
						}
						break;
						IL_9E:
						num2 = 2;
					}
					break;
				}
				}
			}
		}
		return false;
		IL_B8:
		if (true)
		{
		}
		return formulaHidden;
	}

	// Token: 0x06004B90 RID: 19344 RVA: 0x002E4AC4 File Offset: 0x002E3AC4
	public void ᜃ(bool A_0)
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_80:
			num = 1;
			break;
		default:
			if (true)
			{
			}
			if (false)
			{
			}
			goto IL_3C;
		}
		int num2;
		int num3;
		for (;;)
		{
			IL_26:
			switch (num)
			{
			case 0:
				return;
			case 1:
				goto IL_4F;
			case 2:
				goto IL_4F;
			case 3:
				if (num2 >= num3)
				{
					num = 0;
					continue;
				}
				goto IL_6F;
			}
			goto IL_3C;
			IL_4F:
			num = 3;
		}
		return;
		IL_6F:
		this.ᜀ(num2).FormulaHidden = A_0;
		num2++;
		goto IL_80;
		IL_3C:
		num2 = 0;
		num3 = this.ᜤ();
		num = 2;
		goto IL_26;
	}

	// Token: 0x06004B91 RID: 19345 RVA: 0x002E4B5C File Offset: 0x002E3B5C
	public HorizontalAlignType ᜣ()
	{
		HorizontalAlignType horizontalAlignment;
		for (;;)
		{
			IL_28:
			int num = this.ᜤ();
			for (;;)
			{
				int num2 = 3;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						return HorizontalAlignType.General;
					case 1:
						goto IL_82;
					case 2:
					{
						int num3;
						if (horizontalAlignment != this.ᜀ(num3).HorizontalAlignment)
						{
							num2 = 0;
							continue;
						}
						num3++;
						num2 = 1;
						continue;
					}
					case 3:
					{
						if (num == 0)
						{
							num2 = 7;
							continue;
						}
						horizontalAlignment = this.ᜀ(0).HorizontalAlignment;
						int num3 = 1;
						num2 = 6;
						continue;
					}
					case 4:
						goto IL_A4;
					case 5:
					{
						if (true)
						{
						}
						int num3;
						if (num3 >= num)
						{
							num2 = 4;
							continue;
						}
						num2 = 2;
						continue;
					}
					case 6:
						goto IL_82;
					case 7:
						return HorizontalAlignType.General;
					}
					goto IL_28;
					IL_82:
					num2 = 5;
				}
				IL_A4:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_BA;
				}
			}
		}
		return HorizontalAlignType.General;
		IL_BA:
		if (false)
		{
		}
		return horizontalAlignment;
	}

	// Token: 0x06004B92 RID: 19346 RVA: 0x002E4C4C File Offset: 0x002E3C4C
	public void ᜀ(HorizontalAlignType A_0)
	{
		for (;;)
		{
			IL_34:
			int num = 0;
			int num2 = this.ᜤ();
			int num3 = 0;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return;
				default:
					if (false)
					{
					}
					switch (num3)
					{
					case 0:
						goto IL_47;
					case 1:
						if (num >= num2)
						{
							num3 = 3;
							continue;
						}
						this.ᜀ(num).HorizontalAlignment = A_0;
						num++;
						if (true)
						{
						}
						num3 = 2;
						continue;
					case 2:
						goto IL_47;
					case 3:
						return;
					}
					goto IL_34;
					IL_47:
					num3 = 1;
					break;
				}
			}
		}
	}

	// Token: 0x06004B93 RID: 19347 RVA: 0x002E4CE8 File Offset: 0x002E3CE8
	public bool ᜏ()
	{
		bool includeAlignment;
		for (;;)
		{
			IL_30:
			int num = this.ᜤ();
			for (;;)
			{
				int num2 = 2;
				for (;;)
				{
					if (true)
					{
					}
					switch (num2)
					{
					case 0:
						goto IL_8D;
					case 1:
					{
						int num3;
						if (includeAlignment != this.ᜀ(num3).IncludeAlignment)
						{
							num2 = 6;
							continue;
						}
						num3++;
						num2 = 7;
						continue;
					}
					case 2:
					{
						if (num == 0)
						{
							num2 = 3;
							continue;
						}
						includeAlignment = this.ᜀ(0).IncludeAlignment;
						int num3 = 1;
						num2 = 0;
						continue;
					}
					case 3:
						return false;
					case 4:
					{
						int num3;
						if (num3 >= num)
						{
							num2 = 5;
							continue;
						}
						num2 = 1;
						continue;
					}
					case 5:
						goto IL_A7;
					case 6:
						return false;
					case 7:
						goto IL_8D;
					}
					goto IL_30;
					IL_8D:
					num2 = 4;
				}
				IL_A7:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_BD;
				}
			}
		}
		return false;
		IL_BD:
		if (false)
		{
		}
		return includeAlignment;
	}

	// Token: 0x06004B94 RID: 19348 RVA: 0x002E4DDC File Offset: 0x002E3DDC
	public void ᜇ(bool A_0)
	{
		for (;;)
		{
			IL_34:
			int num = 0;
			int num2 = this.ᜤ();
			int num3 = 0;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return;
				default:
					if (false)
					{
					}
					switch (num3)
					{
					case 0:
						goto IL_47;
					case 1:
						return;
					case 2:
						if (num >= num2)
						{
							num3 = 1;
							continue;
						}
						if (true)
						{
						}
						this.ᜀ(num).IncludeAlignment = A_0;
						num++;
						num3 = 3;
						continue;
					case 3:
						goto IL_47;
					}
					goto IL_34;
					IL_47:
					num3 = 2;
					break;
				}
			}
		}
	}

	// Token: 0x06004B95 RID: 19349 RVA: 0x002E4E78 File Offset: 0x002E3E78
	public bool \u171D()
	{
		bool includeBorder;
		for (;;)
		{
			IL_28:
			int num = this.ᜤ();
			for (;;)
			{
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
						includeBorder = this.ᜀ(0).IncludeBorder;
						int num3 = 1;
						num2 = 7;
						continue;
					}
					case 1:
					{
						int num3;
						if (num3 >= num)
						{
							num2 = 3;
							continue;
						}
						num2 = 6;
						continue;
					}
					case 2:
						goto IL_7F;
					case 3:
						goto IL_99;
					case 4:
						return false;
					case 5:
						return false;
					case 6:
					{
						int num3;
						if (includeBorder != this.ᜀ(num3).IncludeBorder)
						{
							num2 = 5;
							continue;
						}
						num3++;
						num2 = 2;
						continue;
					}
					case 7:
						goto IL_7F;
					}
					goto IL_28;
					IL_7F:
					num2 = 1;
				}
				IL_99:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_AF;
				}
			}
		}
		return false;
		IL_AF:
		if (false)
		{
		}
		if (true)
		{
		}
		return includeBorder;
	}

	// Token: 0x06004B96 RID: 19350 RVA: 0x002E4F64 File Offset: 0x002E3F64
	public void ᜄ(bool A_0)
	{
		for (;;)
		{
			IL_34:
			int num = 0;
			int num2 = this.ᜤ();
			int num3 = 2;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return;
				default:
					if (false)
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
						this.ᜀ(num).IncludeBorder = A_0;
						num++;
						if (true)
						{
						}
						num3 = 3;
						continue;
					case 2:
						goto IL_47;
					case 3:
						goto IL_47;
					}
					goto IL_34;
					IL_47:
					num3 = 1;
					break;
				}
			}
		}
	}

	// Token: 0x06004B97 RID: 19351 RVA: 0x002E5000 File Offset: 0x002E4000
	public bool ᜊ()
	{
		bool includeFont;
		for (;;)
		{
			IL_28:
			int num = this.ᜤ();
			for (;;)
			{
				int num2 = 6;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						return false;
					case 1:
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
					case 2:
						goto IL_8D;
					case 3:
						goto IL_8D;
					case 4:
					{
						int num3;
						if (includeFont != this.ᜀ(num3).IncludeFont)
						{
							num2 = 0;
							continue;
						}
						num3++;
						num2 = 3;
						continue;
					}
					case 5:
						goto IL_45;
					case 6:
					{
						if (num == 0)
						{
							num2 = 5;
							continue;
						}
						includeFont = this.ᜀ(0).IncludeFont;
						int num3 = 1;
						num2 = 2;
						continue;
					}
					case 7:
						goto IL_A7;
					}
					goto IL_28;
					IL_8D:
					num2 = 1;
				}
				IL_A7:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_BD;
				}
			}
		}
		IL_45:
		if (true)
		{
		}
		return false;
		IL_BD:
		if (false)
		{
		}
		return includeFont;
	}

	// Token: 0x06004B98 RID: 19352 RVA: 0x002E50F4 File Offset: 0x002E40F4
	public void ᜁ(bool A_0)
	{
		for (;;)
		{
			IL_34:
			int num = 0;
			int num2 = this.ᜤ();
			int num3 = 0;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return;
				default:
					if (false)
					{
					}
					switch (num3)
					{
					case 0:
						goto IL_47;
					case 1:
						if (num >= num2)
						{
							num3 = 3;
							continue;
						}
						this.ᜀ(num).IncludeFont = A_0;
						num++;
						num3 = 2;
						continue;
					case 2:
						goto IL_47;
					case 3:
						goto IL_5B;
					}
					goto IL_34;
					IL_47:
					num3 = 1;
					break;
				}
			}
		}
		IL_5B:
		if (true)
		{
		}
	}

	// Token: 0x06004B99 RID: 19353 RVA: 0x002E5190 File Offset: 0x002E4190
	public bool \u1712()
	{
		bool includeNumberFormat;
		for (;;)
		{
			IL_28:
			int num = this.ᜤ();
			for (;;)
			{
				int num2 = 5;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_7F;
					case 1:
						return false;
					case 2:
						return false;
					case 3:
						goto IL_7F;
					case 4:
					{
						int num3;
						if (num3 >= num)
						{
							num2 = 7;
							continue;
						}
						num2 = 6;
						continue;
					}
					case 5:
					{
						if (num == 0)
						{
							num2 = 1;
							continue;
						}
						if (true)
						{
						}
						includeNumberFormat = this.ᜀ(0).IncludeNumberFormat;
						int num3 = 1;
						num2 = 3;
						continue;
					}
					case 6:
					{
						int num3;
						if (includeNumberFormat != this.ᜀ(num3).IncludeNumberFormat)
						{
							num2 = 2;
							continue;
						}
						num3++;
						num2 = 0;
						continue;
					}
					case 7:
						goto IL_99;
					}
					goto IL_28;
					IL_7F:
					num2 = 4;
				}
				IL_99:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_AF;
				}
			}
		}
		return false;
		IL_AF:
		if (false)
		{
		}
		return includeNumberFormat;
	}

	// Token: 0x06004B9A RID: 19354 RVA: 0x002E527C File Offset: 0x002E427C
	public void ᜂ(bool A_0)
	{
		for (;;)
		{
			IL_3C:
			int num = 0;
			int num2 = this.ᜤ();
			int num3 = 3;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					switch (num3)
					{
					case 0:
						if (num >= num2)
						{
							num3 = 1;
							continue;
						}
						this.ᜀ(num).IncludeNumberFormat = A_0;
						num++;
						num3 = 2;
						continue;
					case 1:
						return;
					case 2:
						goto IL_4F;
					case 3:
						goto IL_4F;
					}
					goto IL_3C;
					IL_4F:
					num3 = 0;
					break;
				}
			}
		}
	}

	// Token: 0x06004B9B RID: 19355 RVA: 0x002E5318 File Offset: 0x002E4318
	public bool \u1719()
	{
		bool includePatterns;
		for (;;)
		{
			IL_30:
			int num = this.ᜤ();
			for (;;)
			{
				int num2 = 1;
				for (;;)
				{
					if (true)
					{
					}
					switch (num2)
					{
					case 0:
					{
						int num3;
						if (includePatterns != this.ᜀ(num3).IncludePatterns)
						{
							num2 = 4;
							continue;
						}
						num3++;
						num2 = 3;
						continue;
					}
					case 1:
					{
						if (num == 0)
						{
							num2 = 6;
							continue;
						}
						includePatterns = this.ᜀ(0).IncludePatterns;
						int num3 = 1;
						num2 = 5;
						continue;
					}
					case 2:
						goto IL_A7;
					case 3:
						goto IL_8D;
					case 4:
						return false;
					case 5:
						goto IL_8D;
					case 6:
						return false;
					case 7:
					{
						int num3;
						if (num3 >= num)
						{
							num2 = 2;
							continue;
						}
						num2 = 0;
						continue;
					}
					}
					goto IL_30;
					IL_8D:
					num2 = 7;
				}
				IL_A7:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_BD;
				}
			}
		}
		return false;
		IL_BD:
		if (false)
		{
		}
		return includePatterns;
	}

	// Token: 0x06004B9C RID: 19356 RVA: 0x002E540C File Offset: 0x002E440C
	public void ᜉ(bool A_0)
	{
		for (;;)
		{
			IL_34:
			int num = 0;
			int num2 = this.ᜤ();
			int num3 = 3;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return;
				default:
					if (false)
					{
					}
					switch (num3)
					{
					case 0:
						if (num >= num2)
						{
							if (true)
							{
							}
							num3 = 2;
							continue;
						}
						this.ᜀ(num).IncludePatterns = A_0;
						num++;
						num3 = 1;
						continue;
					case 1:
						goto IL_47;
					case 2:
						return;
					case 3:
						goto IL_47;
					}
					goto IL_34;
					IL_47:
					num3 = 0;
					break;
				}
			}
		}
	}

	// Token: 0x06004B9D RID: 19357 RVA: 0x002E54A8 File Offset: 0x002E44A8
	public bool ᜂ()
	{
		bool includeProtection;
		for (;;)
		{
			IL_28:
			int num = this.ᜤ();
			for (;;)
			{
				int num2 = 6;
				for (;;)
				{
					switch (num2)
					{
					case 0:
					{
						int num3;
						if (num3 >= num)
						{
							num2 = 3;
							continue;
						}
						num2 = 4;
						continue;
					}
					case 1:
						goto IL_8D;
					case 2:
						goto IL_45;
					case 3:
						goto IL_A7;
					case 4:
					{
						int num3;
						if (includeProtection != this.ᜀ(num3).IncludeProtection)
						{
							num2 = 7;
							continue;
						}
						num3++;
						num2 = 1;
						continue;
					}
					case 5:
						goto IL_8D;
					case 6:
					{
						if (num == 0)
						{
							num2 = 2;
							continue;
						}
						includeProtection = this.ᜀ(0).IncludeProtection;
						int num3 = 1;
						num2 = 5;
						continue;
					}
					case 7:
						return false;
					}
					goto IL_28;
					IL_8D:
					num2 = 0;
				}
				IL_A7:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_BD;
				}
			}
		}
		IL_45:
		if (true)
		{
		}
		return false;
		IL_BD:
		if (false)
		{
		}
		return includeProtection;
	}

	// Token: 0x06004B9E RID: 19358 RVA: 0x002E559C File Offset: 0x002E459C
	public void ᜅ(bool A_0)
	{
		for (;;)
		{
			IL_34:
			int num = 0;
			int num2 = this.ᜤ();
			int num3 = 2;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return;
				default:
					if (false)
					{
					}
					switch (num3)
					{
					case 0:
						if (num >= num2)
						{
							num3 = 1;
							continue;
						}
						this.ᜀ(num).IncludeProtection = A_0;
						num++;
						num3 = 3;
						continue;
					case 1:
						return;
					case 2:
						if (true)
						{
						}
						goto IL_4F;
					case 3:
						goto IL_4F;
					}
					goto IL_34;
					IL_4F:
					num3 = 0;
					break;
				}
			}
		}
	}

	// Token: 0x06004B9F RID: 19359 RVA: 0x002E5638 File Offset: 0x002E4638
	public int \u1714()
	{
		int indentLevel;
		for (;;)
		{
			IL_28:
			int num = this.ᜤ();
			for (;;)
			{
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
						indentLevel = this.ᜀ(0).IndentLevel;
						int num3 = 1;
						num2 = 3;
						continue;
					}
					case 1:
						goto IL_A8;
					case 2:
						goto IL_8E;
					case 3:
						goto IL_8E;
					case 4:
						goto IL_45;
					case 5:
					{
						int num3;
						if (indentLevel != this.ᜀ(num3).IndentLevel)
						{
							num2 = 6;
							continue;
						}
						num3++;
						num2 = 2;
						continue;
					}
					case 6:
						return int.MinValue;
					case 7:
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
					}
					goto IL_28;
					IL_8E:
					num2 = 7;
				}
				IL_A8:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_BE;
				}
			}
		}
		IL_45:
		if (true)
		{
		}
		return int.MinValue;
		IL_BE:
		if (false)
		{
		}
		return indentLevel;
	}

	// Token: 0x06004BA0 RID: 19360 RVA: 0x002E5730 File Offset: 0x002E4730
	public void ᜂ(int A_0)
	{
		for (;;)
		{
			IL_3C:
			int num = 0;
			int num2 = this.ᜤ();
			int num3 = 3;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					switch (num3)
					{
					case 0:
						goto IL_4F;
					case 1:
						return;
					case 2:
						if (num >= num2)
						{
							num3 = 1;
							continue;
						}
						this.ᜀ(num).IndentLevel = A_0;
						num++;
						num3 = 0;
						continue;
					case 3:
						goto IL_4F;
					}
					goto IL_3C;
					IL_4F:
					num3 = 2;
					break;
				}
			}
		}
	}

	// Token: 0x06004BA1 RID: 19361 RVA: 0x002E57CC File Offset: 0x002E47CC
	public bool \u171B()
	{
		bool locked;
		for (;;)
		{
			IL_28:
			int num = this.ᜤ();
			for (;;)
			{
				int num2 = 2;
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
						num2 = 5;
						continue;
					}
					case 1:
						return false;
					case 2:
					{
						if (num == 0)
						{
							num2 = 1;
							continue;
						}
						locked = this.ᜀ(0).Locked;
						int num3 = 1;
						num2 = 3;
						continue;
					}
					case 3:
						goto IL_8D;
					case 4:
						goto IL_8D;
					case 5:
					{
						int num3;
						if (locked != this.ᜀ(num3).Locked)
						{
							if (true)
							{
							}
							num2 = 6;
							continue;
						}
						num3++;
						num2 = 4;
						continue;
					}
					case 6:
						return false;
					case 7:
						goto IL_A7;
					}
					goto IL_28;
					IL_8D:
					num2 = 0;
				}
				IL_A7:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_BD;
				}
			}
		}
		return false;
		IL_BD:
		if (false)
		{
		}
		return locked;
	}

	// Token: 0x06004BA2 RID: 19362 RVA: 0x002E58C0 File Offset: 0x002E48C0
	public void ᜀ(bool A_0)
	{
		if (true)
		{
		}
		for (;;)
		{
			IL_3C:
			int num = 0;
			int num2 = this.ᜤ();
			int num3 = 0;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return;
				default:
					if (false)
					{
					}
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
						this.ᜀ(num).Locked = A_0;
						num++;
						num3 = 3;
						continue;
					case 2:
						return;
					case 3:
						goto IL_4F;
					}
					goto IL_3C;
					IL_4F:
					num3 = 1;
					break;
				}
			}
		}
	}

	// Token: 0x06004BA3 RID: 19363 RVA: 0x002E5958 File Offset: 0x002E4958
	public string ᜅ()
	{
		string name;
		for (;;)
		{
			IL_28:
			int num = this.ᜤ();
			for (;;)
			{
				int num2 = 4;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_92;
					case 1:
						goto IL_92;
					case 2:
						goto IL_45;
					case 3:
						goto IL_AC;
					case 4:
					{
						if (num == 0)
						{
							num2 = 2;
							continue;
						}
						name = this.ᜀ(0).Name;
						int num3 = 1;
						num2 = 1;
						continue;
					}
					case 5:
						goto IL_8E;
					case 6:
					{
						int num3;
						if (num3 >= num)
						{
							num2 = 3;
							continue;
						}
						num2 = 7;
						continue;
					}
					case 7:
					{
						int num3;
						if (name != this.ᜀ(num3).Name)
						{
							num2 = 5;
							continue;
						}
						num3++;
						if (true)
						{
						}
						num2 = 0;
						continue;
					}
					}
					goto IL_28;
					IL_92:
					num2 = 6;
				}
				IL_AC:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_C2;
				}
			}
		}
		IL_45:
		return null;
		IL_8E:
		return null;
		IL_C2:
		if (false)
		{
		}
		return name;
	}

	// Token: 0x06004BA4 RID: 19364 RVA: 0x002E5A50 File Offset: 0x002E4A50
	public string ᜈ()
	{
		string numberFormat;
		for (;;)
		{
			IL_28:
			int num = this.ᜤ();
			for (;;)
			{
				int num2 = 2;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_86;
					case 1:
						if (true)
						{
						}
						goto IL_8A;
					case 2:
					{
						if (num == 0)
						{
							num2 = 6;
							continue;
						}
						numberFormat = this.ᜀ(0).NumberFormat;
						int num3 = 1;
						num2 = 1;
						continue;
					}
					case 3:
					{
						int num3;
						if (num3 >= num)
						{
							num2 = 4;
							continue;
						}
						num2 = 7;
						continue;
					}
					case 4:
						goto IL_A4;
					case 5:
						goto IL_8A;
					case 6:
						goto IL_45;
					case 7:
					{
						int num3;
						if (numberFormat != this.ᜀ(num3).NumberFormat)
						{
							num2 = 0;
							continue;
						}
						num3++;
						num2 = 5;
						continue;
					}
					}
					goto IL_28;
					IL_8A:
					num2 = 3;
				}
				IL_A4:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_BA;
				}
			}
		}
		IL_45:
		return null;
		IL_86:
		return null;
		IL_BA:
		if (false)
		{
		}
		return numberFormat;
	}

	// Token: 0x06004BA5 RID: 19365 RVA: 0x002E5B48 File Offset: 0x002E4B48
	public void ᜁ(string A_0)
	{
		for (;;)
		{
			IL_46:
			int num = 0;
			int num2 = this.ᜤ();
			int num3 = 1;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					switch (num3)
					{
					case 0:
						return;
					case 1:
						goto IL_59;
					case 2:
						goto IL_59;
					case 3:
						if (num >= num2)
						{
							num3 = 0;
							continue;
						}
						this.ᜀ(num).NumberFormat = A_0;
						num++;
						num3 = 2;
						continue;
					}
					goto IL_46;
					IL_59:
					num3 = 3;
					break;
				}
			}
		}
	}

	// Token: 0x06004BA6 RID: 19366 RVA: 0x002E5BE0 File Offset: 0x002E4BE0
	public string ᜋ()
	{
		for (;;)
		{
			int num = this.ᜤ();
			int num2 = 7;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_42;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_91;
					default:
						if (false)
						{
						}
						goto IL_82;
					}
					break;
				case 2:
				{
					int num3;
					if (num3 >= num)
					{
						goto IL_91;
					}
					num2 = 3;
					continue;
				}
				case 3:
				{
					int num3;
					string numberFormatLocal;
					if (numberFormatLocal != this.ᜀ(num3).NumberFormatLocal)
					{
						num2 = 6;
						continue;
					}
					num3++;
					num2 = 4;
					continue;
				}
				case 4:
					goto IL_82;
				case 5:
				{
					string numberFormatLocal;
					return numberFormatLocal;
				}
				case 6:
					goto IL_76;
				case 7:
				{
					if (num == 0)
					{
						num2 = 0;
						continue;
					}
					string numberFormatLocal = this.ᜀ(0).NumberFormatLocal;
					int num3 = 1;
					num2 = 1;
					continue;
				}
				}
				break;
				IL_82:
				num2 = 2;
				continue;
				IL_91:
				num2 = 5;
			}
		}
		IL_42:
		if (true)
		{
		}
		return null;
		IL_76:
		return null;
	}

	// Token: 0x06004BA7 RID: 19367 RVA: 0x002E5CD0 File Offset: 0x002E4CD0
	public void ᜀ(string A_0)
	{
		for (;;)
		{
			int num = 0;
			int num2 = this.ᜤ();
			int num3 = 1;
			for (;;)
			{
				switch (num3)
				{
				case 0:
					if (num < num2)
					{
						this.ᜀ(num).NumberFormatLocal = A_0;
						num++;
						num3 = 3;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_88;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						num3 = 2;
						continue;
					}
					break;
				case 1:
					goto IL_35;
				case 2:
					return;
				case 3:
					goto IL_88;
				}
				break;
				IL_35:
				num3 = 0;
				continue;
				IL_88:
				goto IL_35;
			}
		}
	}

	// Token: 0x06004BA8 RID: 19368 RVA: 0x002E5D68 File Offset: 0x002E4D68
	public int ᜃ()
	{
		for (;;)
		{
			int num = this.ᜤ();
			int num2 = 4;
			for (;;)
			{
				switch (num2)
				{
				case 0:
				{
					int numberFormatIndex;
					return numberFormatIndex;
				}
				case 1:
					return int.MinValue;
				case 2:
				{
					int num3;
					if (num3 >= num)
					{
						goto IL_90;
					}
					num2 = 7;
					continue;
				}
				case 3:
					return int.MinValue;
				case 4:
				{
					if (num == 0)
					{
						num2 = 1;
						continue;
					}
					int numberFormatIndex = this.ᜀ(0).NumberFormatIndex;
					int num3 = 1;
					num2 = 6;
					continue;
				}
				case 5:
					goto IL_79;
				case 6:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_90;
					default:
						if (false)
						{
						}
						goto IL_79;
					}
					break;
				case 7:
				{
					int numberFormatIndex;
					int num3;
					if (numberFormatIndex != this.ᜀ(num3).NumberFormatIndex)
					{
						num2 = 3;
						continue;
					}
					num3++;
					num2 = 5;
					continue;
				}
				}
				break;
				IL_79:
				if (true)
				{
				}
				num2 = 2;
				continue;
				IL_90:
				num2 = 0;
			}
		}
		return int.MinValue;
	}

	// Token: 0x06004BA9 RID: 19369 RVA: 0x002E5E5C File Offset: 0x002E4E5C
	public void ᜁ(int A_0)
	{
		for (;;)
		{
			int num = 0;
			int num2 = this.ᜤ();
			int num3 = 0;
			for (;;)
			{
				switch (num3)
				{
				case 0:
					goto IL_35;
				case 1:
					if (num < num2)
					{
						this.ᜀ(num).NumberFormatIndex = A_0;
						num++;
						num3 = 3;
						continue;
					}
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_88;
					default:
						if (false)
						{
						}
						num3 = 2;
						continue;
					}
					break;
				case 2:
					return;
				case 3:
					goto IL_88;
				}
				break;
				IL_35:
				num3 = 1;
				continue;
				IL_88:
				goto IL_35;
			}
		}
	}

	// Token: 0x06004BAA RID: 19370 RVA: 0x002E5EF4 File Offset: 0x002E4EF4
	public INumberFormat ᜁ()
	{
		int num = this.ᜃ();
		if (num >= 0)
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
				return this.\u1713().InnerFormats.ᜁ(this.ᜃ());
			}
		}
		if (true)
		{
		}
		return null;
	}

	// Token: 0x06004BAB RID: 19371 RVA: 0x002E5F54 File Offset: 0x002E4F54
	public int ᜎ()
	{
		for (;;)
		{
			int num = this.ᜤ();
			int num2 = 5;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					return int.MinValue;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_90;
					default:
						if (false)
						{
						}
						goto IL_81;
					}
					break;
				case 2:
				{
					int rotation;
					int num3;
					if (rotation != this.ᜀ(num3).Rotation)
					{
						num2 = 6;
						continue;
					}
					num3++;
					num2 = 3;
					continue;
				}
				case 3:
					goto IL_81;
				case 4:
				{
					int rotation;
					return rotation;
				}
				case 5:
				{
					if (num == 0)
					{
						num2 = 0;
						continue;
					}
					int rotation = this.ᜀ(0).Rotation;
					int num3 = 1;
					num2 = 1;
					continue;
				}
				case 6:
					return int.MinValue;
				case 7:
				{
					int num3;
					if (num3 >= num)
					{
						goto IL_90;
					}
					if (true)
					{
					}
					num2 = 2;
					continue;
				}
				}
				break;
				IL_81:
				num2 = 7;
				continue;
				IL_90:
				num2 = 4;
			}
		}
		return int.MinValue;
	}

	// Token: 0x06004BAC RID: 19372 RVA: 0x002E6048 File Offset: 0x002E5048
	public void ᜃ(int A_0)
	{
		for (;;)
		{
			if (true)
			{
			}
			int num = 0;
			int num2 = this.ᜤ();
			int num3 = 1;
			for (;;)
			{
				switch (num3)
				{
				case 0:
					return;
				case 1:
					goto IL_3D;
				case 2:
					goto IL_88;
				case 3:
					if (num < num2)
					{
						this.ᜀ(num).Rotation = A_0;
						num++;
						num3 = 2;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_88;
					default:
						if (false)
						{
						}
						num3 = 0;
						continue;
					}
					break;
				}
				break;
				IL_3D:
				num3 = 3;
				continue;
				IL_88:
				goto IL_3D;
			}
		}
	}

	// Token: 0x06004BAD RID: 19373 RVA: 0x002E60E0 File Offset: 0x002E50E0
	public bool ᜉ()
	{
		for (;;)
		{
			int num = this.ᜤ();
			int num2 = 5;
			for (;;)
			{
				switch (num2)
				{
				case 0:
				{
					bool shrinkToFit;
					int num3;
					if (shrinkToFit != this.ᜀ(num3).ShrinkToFit)
					{
						num2 = 6;
						continue;
					}
					num3++;
					num2 = 1;
					continue;
				}
				case 1:
					goto IL_7D;
				case 2:
				{
					bool shrinkToFit;
					return shrinkToFit;
				}
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_8C;
					default:
						if (false)
						{
						}
						goto IL_7D;
					}
					break;
				case 4:
					return false;
				case 5:
				{
					if (num == 0)
					{
						num2 = 4;
						continue;
					}
					bool shrinkToFit = this.ᜀ(0).ShrinkToFit;
					int num3 = 1;
					num2 = 3;
					continue;
				}
				case 6:
					return false;
				case 7:
				{
					int num3;
					if (num3 >= num)
					{
						goto IL_8C;
					}
					if (true)
					{
					}
					num2 = 0;
					continue;
				}
				}
				break;
				IL_7D:
				num2 = 7;
				continue;
				IL_8C:
				num2 = 2;
			}
		}
		return false;
	}

	// Token: 0x06004BAE RID: 19374 RVA: 0x002E61CC File Offset: 0x002E51CC
	public void ᜈ(bool A_0)
	{
		for (;;)
		{
			int num = 0;
			int num2 = this.ᜤ();
			int num3 = 3;
			for (;;)
			{
				switch (num3)
				{
				case 0:
					if (num < num2)
					{
						if (true)
						{
						}
						this.ᜀ(num).ShrinkToFit = A_0;
						num++;
						num3 = 1;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_88;
					default:
						if (false)
						{
						}
						num3 = 2;
						continue;
					}
					break;
				case 1:
					goto IL_88;
				case 2:
					return;
				case 3:
					goto IL_35;
				}
				break;
				IL_35:
				num3 = 0;
				continue;
				IL_88:
				goto IL_35;
			}
		}
	}

	// Token: 0x06004BAF RID: 19375 RVA: 0x002E6264 File Offset: 0x002E5264
	public VerticalAlignType ᜦ()
	{
		for (;;)
		{
			int num = this.ᜤ();
			int num2 = 7;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					return VerticalAlignType.Top;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_81;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						goto IL_75;
					}
					break;
				case 2:
				{
					int num3;
					if (num3 >= num)
					{
						goto IL_81;
					}
					num2 = 6;
					continue;
				}
				case 3:
				{
					VerticalAlignType verticalAlignment;
					return verticalAlignment;
				}
				case 4:
					return VerticalAlignType.Top;
				case 5:
					goto IL_75;
				case 6:
				{
					int num3;
					VerticalAlignType verticalAlignment;
					if (verticalAlignment != this.ᜀ(num3).VerticalAlignment)
					{
						num2 = 4;
						continue;
					}
					num3++;
					num2 = 5;
					continue;
				}
				case 7:
				{
					if (num == 0)
					{
						num2 = 0;
						continue;
					}
					VerticalAlignType verticalAlignment = this.ᜀ(0).VerticalAlignment;
					int num3 = 1;
					num2 = 1;
					continue;
				}
				}
				break;
				IL_75:
				num2 = 2;
				continue;
				IL_81:
				num2 = 3;
			}
		}
		return VerticalAlignType.Top;
	}

	// Token: 0x06004BB0 RID: 19376 RVA: 0x002E634C File Offset: 0x002E534C
	public void ᜀ(VerticalAlignType A_0)
	{
		for (;;)
		{
			int num = 0;
			int num2 = this.ᜤ();
			int num3 = 2;
			for (;;)
			{
				switch (num3)
				{
				case 0:
					goto IL_88;
				case 1:
					if (num < num2)
					{
						this.ᜀ(num).VerticalAlignment = A_0;
						num++;
						num3 = 0;
						continue;
					}
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_88;
					default:
						if (false)
						{
						}
						num3 = 3;
						continue;
					}
					break;
				case 2:
					goto IL_35;
				case 3:
					return;
				}
				break;
				IL_35:
				num3 = 1;
				continue;
				IL_88:
				goto IL_35;
			}
		}
	}

	// Token: 0x06004BB1 RID: 19377 RVA: 0x002E63E4 File Offset: 0x002E53E4
	public bool ᜧ()
	{
		for (;;)
		{
			int num = this.ᜤ();
			int num2 = 7;
			for (;;)
			{
				switch (num2)
				{
				case 0:
				{
					int num3;
					if (num3 >= num)
					{
						goto IL_8C;
					}
					num2 = 1;
					continue;
				}
				case 1:
				{
					int num3;
					bool wrapText;
					if (wrapText != this.ᜀ(num3).WrapText)
					{
						num2 = 4;
						continue;
					}
					num3++;
					num2 = 6;
					continue;
				}
				case 2:
					return false;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_8C;
					default:
						if (false)
						{
						}
						goto IL_7D;
					}
					break;
				case 4:
					return false;
				case 5:
				{
					bool wrapText;
					return wrapText;
				}
				case 6:
					goto IL_7D;
				case 7:
				{
					if (true)
					{
					}
					if (num == 0)
					{
						num2 = 2;
						continue;
					}
					bool wrapText = this.ᜀ(0).WrapText;
					int num3 = 1;
					num2 = 3;
					continue;
				}
				}
				break;
				IL_7D:
				num2 = 0;
				continue;
				IL_8C:
				num2 = 5;
			}
		}
		return false;
	}

	// Token: 0x06004BB2 RID: 19378 RVA: 0x002E64D0 File Offset: 0x002E54D0
	public void ᜋ(bool A_0)
	{
		for (;;)
		{
			int num = 0;
			int num2 = this.ᜤ();
			int num3 = 2;
			for (;;)
			{
				switch (num3)
				{
				case 0:
					goto IL_80;
				case 1:
					if (num < num2)
					{
						this.ᜀ(num).WrapText = A_0;
						num++;
						num3 = 0;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_80;
					default:
						if (false)
						{
						}
						num3 = 3;
						continue;
					}
					break;
				case 2:
					goto IL_35;
				case 3:
					goto IL_65;
				}
				break;
				IL_35:
				num3 = 1;
				continue;
				IL_80:
				goto IL_35;
			}
		}
		IL_65:
		if (true)
		{
		}
	}

	// Token: 0x06004BB3 RID: 19379 RVA: 0x002E6568 File Offset: 0x002E5568
	public bool ᜪ()
	{
		for (;;)
		{
			int num = this.ᜤ();
			int num2 = 7;
			for (;;)
			{
				switch (num2)
				{
				case 0:
				{
					bool isInitialized;
					int num3;
					if (isInitialized != this.ᜀ(num3).IsInitialized)
					{
						num2 = 1;
						continue;
					}
					num3++;
					num2 = 4;
					continue;
				}
				case 1:
					return false;
				case 2:
				{
					bool isInitialized;
					return isInitialized;
				}
				case 3:
				{
					if (true)
					{
					}
					int num3;
					if (num3 >= num)
					{
						goto IL_89;
					}
					num2 = 0;
					continue;
				}
				case 4:
					goto IL_75;
				case 5:
					return false;
				case 6:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_89;
					default:
						if (false)
						{
						}
						goto IL_75;
					}
					break;
				case 7:
				{
					if (num == 0)
					{
						num2 = 5;
						continue;
					}
					bool isInitialized = this.ᜀ(0).IsInitialized;
					int num3 = 1;
					num2 = 6;
					continue;
				}
				}
				break;
				IL_75:
				num2 = 3;
				continue;
				IL_89:
				num2 = 2;
			}
		}
		return false;
	}

	// Token: 0x06004BB4 RID: 19380 RVA: 0x002E6650 File Offset: 0x002E5650
	public ReadingOrderType \u171C()
	{
		for (;;)
		{
			int num = this.ᜤ();
			int num2 = 2;
			for (;;)
			{
				switch (num2)
				{
				case 0:
				{
					ReadingOrderType readingOrder;
					return readingOrder;
				}
				case 1:
					return ReadingOrderType.Context;
				case 2:
				{
					if (num == 0)
					{
						num2 = 1;
						continue;
					}
					ReadingOrderType readingOrder = this.ᜀ(0).ReadingOrder;
					int num3 = 1;
					num2 = 4;
					continue;
				}
				case 3:
				{
					int num3;
					if (num3 >= num)
					{
						goto IL_8C;
					}
					num2 = 5;
					continue;
				}
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_8C;
					default:
						if (false)
						{
						}
						goto IL_7D;
					}
					break;
				case 5:
				{
					if (true)
					{
					}
					ReadingOrderType readingOrder;
					int num3;
					if (readingOrder != this.ᜀ(num3).ReadingOrder)
					{
						num2 = 6;
						continue;
					}
					num3++;
					num2 = 7;
					continue;
				}
				case 6:
					return ReadingOrderType.Context;
				case 7:
					goto IL_7D;
				}
				break;
				IL_7D:
				num2 = 3;
				continue;
				IL_8C:
				num2 = 0;
			}
		}
		return ReadingOrderType.Context;
	}

	// Token: 0x06004BB5 RID: 19381 RVA: 0x002E673C File Offset: 0x002E573C
	public void ᜀ(ReadingOrderType A_0)
	{
		for (;;)
		{
			int num = 0;
			int num2 = this.ᜤ();
			if (true)
			{
			}
			int num3 = 2;
			for (;;)
			{
				switch (num3)
				{
				case 0:
					if (num < num2)
					{
						this.ᜀ(num).ReadingOrder = A_0;
						num++;
						num3 = 1;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_88;
					default:
						if (false)
						{
						}
						num3 = 3;
						continue;
					}
					break;
				case 1:
					goto IL_88;
				case 2:
					goto IL_3D;
				case 3:
					return;
				}
				break;
				IL_3D:
				num3 = 0;
				continue;
				IL_88:
				goto IL_3D;
			}
		}
	}

	// Token: 0x06004BB6 RID: 19382 RVA: 0x002E67D4 File Offset: 0x002E57D4
	public bool ᜆ()
	{
		for (;;)
		{
			int num = this.ᜤ();
			int num2 = 3;
			for (;;)
			{
				if (true)
				{
				}
				switch (num2)
				{
				case 0:
					return false;
				case 1:
					goto IL_7D;
				case 2:
				{
					bool isFirstSymbolApostrophe;
					return isFirstSymbolApostrophe;
				}
				case 3:
				{
					if (num == 0)
					{
						num2 = 7;
						continue;
					}
					bool isFirstSymbolApostrophe = this.ᜀ(0).IsFirstSymbolApostrophe;
					int num3 = 1;
					num2 = 6;
					continue;
				}
				case 4:
				{
					bool isFirstSymbolApostrophe;
					int num3;
					if (isFirstSymbolApostrophe != this.ᜀ(num3).IsFirstSymbolApostrophe)
					{
						num2 = 0;
						continue;
					}
					num3++;
					num2 = 1;
					continue;
				}
				case 5:
				{
					int num3;
					if (num3 >= num)
					{
						goto IL_8C;
					}
					num2 = 4;
					continue;
				}
				case 6:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_8C;
					default:
						if (false)
						{
						}
						goto IL_7D;
					}
					break;
				case 7:
					return false;
				}
				break;
				IL_7D:
				num2 = 5;
				continue;
				IL_8C:
				num2 = 2;
			}
		}
		return false;
	}

	// Token: 0x06004BB7 RID: 19383 RVA: 0x002E68C0 File Offset: 0x002E58C0
	public void ᜊ(bool A_0)
	{
		for (;;)
		{
			int num = 0;
			int num2 = this.ᜤ();
			int num3 = 3;
			for (;;)
			{
				switch (num3)
				{
				case 0:
					goto IL_88;
				case 1:
					if (num < num2)
					{
						this.ᜀ(num).IsFirstSymbolApostrophe = A_0;
						num++;
						if (true)
						{
						}
						num3 = 0;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_88;
					default:
						if (false)
						{
						}
						num3 = 2;
						continue;
					}
					break;
				case 2:
					return;
				case 3:
					goto IL_35;
				}
				break;
				IL_35:
				num3 = 1;
				continue;
				IL_88:
				goto IL_35;
			}
		}
	}

	// Token: 0x06004BB8 RID: 19384 RVA: 0x002E6958 File Offset: 0x002E5958
	public bool ᜢ()
	{
		for (;;)
		{
			int num = this.ᜤ();
			int num2 = 6;
			for (;;)
			{
				switch (num2)
				{
				case 0:
				{
					bool justifyLast;
					int num3;
					if (justifyLast != this.ᜀ(num3).JustifyLast)
					{
						num2 = 2;
						continue;
					}
					num3++;
					num2 = 1;
					continue;
				}
				case 1:
					goto IL_75;
				case 2:
					return false;
				case 3:
				{
					bool justifyLast;
					return justifyLast;
				}
				case 4:
				{
					int num3;
					if (num3 >= num)
					{
						goto IL_81;
					}
					num2 = 0;
					continue;
				}
				case 5:
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_81;
					default:
						if (false)
						{
						}
						goto IL_75;
					}
					break;
				case 6:
				{
					if (num == 0)
					{
						num2 = 7;
						continue;
					}
					bool justifyLast = this.ᜀ(0).JustifyLast;
					int num3 = 1;
					num2 = 5;
					continue;
				}
				case 7:
					return false;
				}
				break;
				IL_75:
				num2 = 4;
				continue;
				IL_81:
				num2 = 3;
			}
		}
		return false;
	}

	// Token: 0x06004BB9 RID: 19385 RVA: 0x002E6A40 File Offset: 0x002E5A40
	public void ᜆ(bool A_0)
	{
		if (true)
		{
		}
		for (;;)
		{
			int num = 0;
			int num2 = this.ᜤ();
			int num3 = 2;
			for (;;)
			{
				switch (num3)
				{
				case 0:
					if (num >= num2)
					{
						num3 = 1;
						continue;
					}
					this.ᜀ(num).JustifyLast = A_0;
					num++;
					goto IL_76;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_76;
					default:
						goto IL_5D;
					}
					break;
				case 2:
					goto IL_33;
				case 3:
					goto IL_33;
				}
				break;
				IL_33:
				num3 = 0;
				continue;
				IL_76:
				num3 = 3;
			}
		}
		IL_5D:
		if (false)
		{
		}
	}

	// Token: 0x06004BBA RID: 19386 RVA: 0x002E6AD8 File Offset: 0x002E5AD8
	public ExcelColors \u1715()
	{
		for (;;)
		{
			int num = this.ᜤ();
			int num2 = 6;
			for (;;)
			{
				int num3;
				ExcelColors patternKnownColor;
				switch (num2)
				{
				case 0:
					goto IL_87;
				case 1:
					goto IL_42;
				case 2:
					return ExcelColors.Black;
				case 3:
					if (num3 >= num)
					{
						num2 = 7;
						continue;
					}
					num2 = 4;
					continue;
				case 4:
					if (patternKnownColor != this.ᜀ(num3).PatternKnownColor)
					{
						num2 = 2;
						continue;
					}
					num3++;
					num2 = 0;
					continue;
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_A3;
					default:
						if (false)
						{
						}
						goto IL_87;
					}
					break;
				case 6:
					if (num == 0)
					{
						num2 = 1;
						continue;
					}
					goto IL_A3;
				case 7:
					return patternKnownColor;
				}
				break;
				IL_87:
				num2 = 3;
				continue;
				IL_A3:
				patternKnownColor = this.ᜀ(0).PatternKnownColor;
				num3 = 1;
				num2 = 5;
			}
		}
		IL_42:
		if (true)
		{
		}
		return ExcelColors.Black;
	}

	// Token: 0x06004BBB RID: 19387 RVA: 0x002E6BC4 File Offset: 0x002E5BC4
	public void ᜁ(ExcelColors A_0)
	{
		for (;;)
		{
			int num = 0;
			int num2 = this.ᜤ();
			int num3 = 3;
			for (;;)
			{
				switch (num3)
				{
				case 0:
					goto IL_2B;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_76;
					default:
						goto IL_55;
					}
					break;
				case 2:
					if (num >= num2)
					{
						num3 = 1;
						continue;
					}
					if (true)
					{
					}
					this.ᜀ(num).PatternKnownColor = A_0;
					num++;
					goto IL_76;
				case 3:
					goto IL_2B;
				}
				break;
				IL_2B:
				num3 = 2;
				continue;
				IL_76:
				num3 = 0;
			}
		}
		IL_55:
		if (false)
		{
		}
	}

	// Token: 0x06004BBC RID: 19388 RVA: 0x002E6C5C File Offset: 0x002E5C5C
	public Color \u1718()
	{
		for (;;)
		{
			if (true)
			{
			}
			int num = this.ᜤ();
			int num2 = 5;
			for (;;)
			{
				Color patternColor;
				int num3;
				switch (num2)
				{
				case 0:
					return patternColor;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_AF;
					default:
						if (false)
						{
						}
						goto IL_93;
					}
					break;
				case 2:
					goto IL_93;
				case 3:
					if (num3 >= num)
					{
						num2 = 0;
						continue;
					}
					num2 = 7;
					continue;
				case 4:
					goto IL_4A;
				case 5:
					if (num == 0)
					{
						num2 = 4;
						continue;
					}
					goto IL_AF;
				case 6:
					goto IL_8B;
				case 7:
					if (patternColor != this.ᜀ(num3).PatternColor)
					{
						num2 = 6;
						continue;
					}
					num3++;
					num2 = 2;
					continue;
				}
				break;
				IL_93:
				num2 = 3;
				continue;
				IL_AF:
				patternColor = this.ᜀ(0).PatternColor;
				num3 = 1;
				num2 = 1;
			}
		}
		IL_4A:
		return spr\u1D39.ᜂ;
		IL_8B:
		return spr\u1D39.ᜂ;
	}

	// Token: 0x06004BBD RID: 19389 RVA: 0x002E6D58 File Offset: 0x002E5D58
	public void ᜂ(Color A_0)
	{
		for (;;)
		{
			int num = 0;
			int num2 = this.ᜤ();
			int num3 = 1;
			for (;;)
			{
				switch (num3)
				{
				case 0:
					goto IL_2B;
				case 1:
					goto IL_2B;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_76;
					default:
						goto IL_55;
					}
					break;
				case 3:
					if (num >= num2)
					{
						num3 = 2;
						continue;
					}
					this.ᜀ(num).PatternColor = A_0;
					num++;
					goto IL_76;
				}
				break;
				IL_2B:
				num3 = 3;
				continue;
				IL_76:
				num3 = 0;
			}
		}
		IL_55:
		if (false)
		{
		}
		if (true)
		{
		}
	}

	// Token: 0x06004BBE RID: 19390 RVA: 0x002E6DF0 File Offset: 0x002E5DF0
	public ExcelColors ᜄ()
	{
		for (;;)
		{
			int num = this.ᜤ();
			int num2 = 6;
			for (;;)
			{
				ExcelColors knownColor;
				int num3;
				switch (num2)
				{
				case 0:
					return ExcelColors.Black;
				case 1:
					goto IL_8A;
				case 2:
					return ExcelColors.Black;
				case 3:
					return knownColor;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_A6;
					default:
						if (false)
						{
						}
						goto IL_8A;
					}
					break;
				case 5:
					if (knownColor != this.ᜀ(num3).KnownColor)
					{
						num2 = 0;
						continue;
					}
					num3++;
					num2 = 1;
					continue;
				case 6:
					if (num == 0)
					{
						num2 = 2;
						continue;
					}
					goto IL_A6;
				case 7:
					if (num3 >= num)
					{
						num2 = 3;
						continue;
					}
					if (true)
					{
					}
					num2 = 5;
					continue;
				}
				break;
				IL_8A:
				num2 = 7;
				continue;
				IL_A6:
				knownColor = this.ᜀ(0).KnownColor;
				num3 = 1;
				num2 = 4;
			}
		}
		return ExcelColors.Black;
	}

	// Token: 0x06004BBF RID: 19391 RVA: 0x002E6EE0 File Offset: 0x002E5EE0
	public void ᜂ(ExcelColors A_0)
	{
		if (true)
		{
		}
		for (;;)
		{
			int num = 0;
			int num2 = this.ᜤ();
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
						goto IL_76;
					default:
						goto IL_5D;
					}
					break;
				case 1:
					goto IL_33;
				case 2:
					if (num >= num2)
					{
						num3 = 0;
						continue;
					}
					this.ᜀ(num).KnownColor = A_0;
					num++;
					goto IL_76;
				case 3:
					goto IL_33;
				}
				break;
				IL_33:
				num3 = 2;
				continue;
				IL_76:
				num3 = 3;
			}
		}
		IL_5D:
		if (false)
		{
		}
	}

	// Token: 0x06004BC0 RID: 19392 RVA: 0x002E6F78 File Offset: 0x002E5F78
	public Color \u171F()
	{
		for (;;)
		{
			int num = this.ᜤ();
			int num2 = 3;
			for (;;)
			{
				Color color;
				int num3;
				switch (num2)
				{
				case 0:
					goto IL_80;
				case 1:
					return color;
				case 2:
					goto IL_42;
				case 3:
					if (num == 0)
					{
						num2 = 2;
						continue;
					}
					goto IL_A4;
				case 4:
					if (color != this.ᜀ(num3).Color)
					{
						num2 = 0;
						continue;
					}
					num3++;
					num2 = 6;
					continue;
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_A4;
					default:
						if (false)
						{
						}
						goto IL_88;
					}
					break;
				case 6:
					goto IL_88;
				case 7:
					if (num3 >= num)
					{
						num2 = 1;
						continue;
					}
					num2 = 4;
					continue;
				}
				break;
				IL_88:
				num2 = 7;
				continue;
				IL_A4:
				color = this.ᜀ(0).Color;
				num3 = 1;
				if (true)
				{
				}
				num2 = 5;
			}
		}
		IL_42:
		return spr\u1D39.ᜂ;
		IL_80:
		return spr\u1D39.ᜂ;
	}

	// Token: 0x06004BC1 RID: 19393 RVA: 0x002E7070 File Offset: 0x002E6070
	public void ᜃ(Color A_0)
	{
		for (;;)
		{
			int num = 0;
			int num2 = this.ᜤ();
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
						goto IL_76;
					default:
						goto IL_55;
					}
					break;
				case 1:
					goto IL_2B;
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
					this.ᜀ(num).Color = A_0;
					num++;
					goto IL_76;
				}
				break;
				IL_2B:
				num3 = 3;
				continue;
				IL_76:
				num3 = 2;
			}
		}
		IL_55:
		if (false)
		{
		}
	}

	// Token: 0x06004BC2 RID: 19394 RVA: 0x002E7108 File Offset: 0x002E6108
	public bool \u171A()
	{
		for (;;)
		{
			int num = this.ᜤ();
			int num2 = 2;
			for (;;)
			{
				bool isModified;
				int num3;
				switch (num2)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_A6;
					default:
						if (false)
						{
						}
						goto IL_8A;
					}
					break;
				case 1:
					return false;
				case 2:
					if (num == 0)
					{
						num2 = 6;
						continue;
					}
					goto IL_A6;
				case 3:
					return isModified;
				case 4:
					if (num3 >= num)
					{
						num2 = 3;
						continue;
					}
					num2 = 5;
					continue;
				case 5:
					if (isModified != this.ᜀ(num3).IsModified)
					{
						num2 = 1;
						continue;
					}
					num3++;
					num2 = 7;
					continue;
				case 6:
					goto IL_42;
				case 7:
					goto IL_8A;
				}
				break;
				IL_8A:
				num2 = 4;
				continue;
				IL_A6:
				isModified = this.ᜀ(0).IsModified;
				num3 = 1;
				num2 = 0;
			}
		}
		IL_42:
		if (true)
		{
		}
		return false;
	}

	// Token: 0x06004BC3 RID: 19395 RVA: 0x002E71F8 File Offset: 0x002E61F8
	public virtual void \u1717()
	{
		for (;;)
		{
			int num = 0;
			int num2 = this.ᜤ();
			int num3 = 0;
			for (;;)
			{
				switch (num3)
				{
				case 0:
					goto IL_2B;
				case 1:
					goto IL_2B;
				case 2:
					if (num >= num2)
					{
						num3 = 3;
						continue;
					}
					this.ᜀ(num).BeginUpdate();
					num++;
					goto IL_75;
				case 3:
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_75;
					default:
						goto IL_5D;
					}
					break;
				}
				break;
				IL_2B:
				num3 = 2;
				continue;
				IL_75:
				num3 = 1;
			}
		}
		IL_5D:
		if (false)
		{
		}
	}

	// Token: 0x06004BC4 RID: 19396 RVA: 0x002E7290 File Offset: 0x002E6290
	public virtual void ᜡ()
	{
		for (;;)
		{
			int num = 0;
			int num2 = this.ᜤ();
			int num3 = 1;
			for (;;)
			{
				switch (num3)
				{
				case 0:
					if (num >= num2)
					{
						num3 = 3;
						continue;
					}
					if (true)
					{
					}
					this.ᜀ(num).EndUpdate();
					num++;
					goto IL_75;
				case 1:
					goto IL_2B;
				case 2:
					goto IL_2B;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_75;
					default:
						goto IL_55;
					}
					break;
				}
				break;
				IL_2B:
				num3 = 0;
				continue;
				IL_75:
				num3 = 2;
			}
		}
		IL_55:
		if (false)
		{
		}
	}

	// Token: 0x06004BC5 RID: 19397 RVA: 0x002E7328 File Offset: 0x002E6328
	public int ᜇ()
	{
		if (true)
		{
		}
		for (;;)
		{
			int num = this.ᜤ();
			int num2 = 1;
			for (;;)
			{
				int num3;
				int extendedFormatIndex;
				switch (num2)
				{
				case 0:
					if (num3 >= num)
					{
						num2 = 2;
						continue;
					}
					num2 = 4;
					continue;
				case 1:
					if (num == 0)
					{
						num2 = 7;
						continue;
					}
					goto IL_AC;
				case 2:
					return extendedFormatIndex;
				case 3:
					goto IL_90;
				case 4:
					if (extendedFormatIndex != ((IExtendIndex)this.ᜀ(num3)).ExtendedFormatIndex)
					{
						num2 = 5;
						continue;
					}
					num3++;
					num2 = 3;
					continue;
				case 5:
					return int.MinValue;
				case 6:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_AC;
					default:
						if (false)
						{
						}
						goto IL_90;
					}
					break;
				case 7:
					return int.MinValue;
				}
				break;
				IL_90:
				num2 = 0;
				continue;
				IL_AC:
				extendedFormatIndex = ((IExtendIndex)this.ᜀ(0)).ExtendedFormatIndex;
				num3 = 1;
				num2 = 6;
			}
		}
		return int.MinValue;
	}

	// Token: 0x0400227F RID: 8831
	private spr\u1CCF ᜀ;

	// Token: 0x04002280 RID: 8832
	private sprᠦ ᜁ;

	// Token: 0x04002281 RID: 8833
	private spr\u2366 ᜂ;
}
