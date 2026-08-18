using System;
using Spire.Xls;
using Spire.Xls.Core;
using Spire.Xls.Core.Spreadsheet;

// Token: 0x020003F2 RID: 1010
internal class spr\u2310
{
	// Token: 0x06003CB6 RID: 15542 RVA: 0x0021ED34 File Offset: 0x0021DD34
	public spr\u192F ᜀ(IXLSRange A_0, spr\u192F A_1)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				spr\u192F spr_u192F = null;
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return A_1;
					case 1:
						if ((A_0 as XlsRange).HasConditionFormats)
						{
							num = 9;
							continue;
						}
						goto IL_62;
					case 2:
					{
						if (spr_u192F != null)
						{
							num = 6;
							continue;
						}
						IConditionalFormats conditionalFormats;
						int num2;
						IConditionalFormat a_ = conditionalFormats[num2];
						spr_u192F = this.ᜂ(a_, A_0, A_1);
						num2++;
						num = 3;
						continue;
					}
					case 3:
						goto IL_84;
					case 4:
						if (spr_u192F == null)
						{
							goto IL_79;
						}
						return spr_u192F;
					case 5:
						num = 2;
						continue;
					case 6:
						goto IL_62;
					case 7:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_79;
						default:
						{
							if (false)
							{
							}
							int num2;
							int count;
							if (num2 < count)
							{
								num = 5;
								continue;
							}
							goto IL_62;
						}
						}
						break;
					case 8:
						goto IL_84;
					case 9:
					{
						IConditionalFormats conditionalFormats = (A_0 as XlsRange).ConditionalFormats;
						int num2 = 0;
						int count = conditionalFormats.Count;
						num = 8;
						continue;
					}
					}
					break;
					IL_62:
					if (true)
					{
					}
					num = 4;
					continue;
					IL_79:
					num = 0;
					continue;
					IL_84:
					num = 7;
				}
			}
			return A_1;
		}
	}

	// Token: 0x06003CB7 RID: 15543 RVA: 0x0021EE78 File Offset: 0x0021DE78
	private spr\u192F ᜂ(IConditionalFormat A_0, IXLSRange A_1, spr\u192F A_2)
	{
		spr\u192F result;
		for (;;)
		{
			IL_20:
			int num;
			ConditionalFormatType formatType;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_8B:
				num = 5;
				break;
			default:
				if (false)
				{
				}
				formatType = A_0.FormatType;
				if (true)
				{
				}
				num = 2;
				break;
			}
			for (;;)
			{
				switch (num)
				{
				case 0:
					result = null;
					num = 1;
					continue;
				case 1:
					return result;
				case 2:
					switch (formatType)
					{
					case ConditionalFormatType.CellValue:
						goto IL_81;
					case ConditionalFormatType.Formula:
						result = this.ᜀ(A_0, A_1, A_2);
						num = 3;
						continue;
					default:
						num = 4;
						continue;
					}
					break;
				case 3:
					return result;
				case 4:
					num = 0;
					continue;
				case 5:
					return result;
				}
				goto IL_20;
			}
			IL_81:
			result = this.ᜁ(A_0, A_1, A_2);
			goto IL_8B;
		}
		return result;
	}

	// Token: 0x06003CB8 RID: 15544 RVA: 0x0021EF44 File Offset: 0x0021DF44
	private spr\u192F ᜁ(IConditionalFormat A_0, IXLSRange A_1, spr\u192F A_2)
	{
		switch (0)
		{
		default:
		{
			spr\u192F result;
			for (;;)
			{
				sprᲖ sprᲖ = A_0 as sprᲖ;
				IWorksheet worksheet = A_1.Worksheet;
				object a_ = this.ᜀ.ᜀ(sprᲖ.ᜌ(), worksheet);
				object a_2 = this.ᜀ.ᜀ(sprᲖ.\u170D(), worksheet);
				bool flag = false;
				ComparisonOperatorType @operator = A_0.Operator;
				int num = 10;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_13C;
					case 1:
						goto IL_13C;
					case 2:
						goto IL_13C;
					case 3:
						num = 6;
						continue;
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_186;
						default:
							if (false)
							{
							}
							goto IL_13C;
						}
						break;
					case 5:
						goto IL_13C;
					case 6:
						goto IL_13C;
					case 7:
						goto IL_13C;
					case 8:
						goto IL_13C;
					case 9:
						goto IL_186;
					case 10:
						switch (@operator)
						{
						case ComparisonOperatorType.None:
							goto IL_13C;
						case ComparisonOperatorType.Between:
							flag = this.ᜁ(A_1, a_, a_2);
							num = 4;
							continue;
						case ComparisonOperatorType.NotBetween:
							flag = this.ᜀ(A_1, a_, a_2);
							num = 11;
							continue;
						case ComparisonOperatorType.Equal:
							flag = this.ᜆ(A_1, a_);
							if (true)
							{
							}
							num = 8;
							continue;
						case ComparisonOperatorType.NotEqual:
							flag = this.ᜁ(A_1, a_);
							num = 7;
							continue;
						case ComparisonOperatorType.Greater:
							flag = this.ᜅ(A_1, a_);
							num = 0;
							continue;
						case ComparisonOperatorType.Less:
							flag = this.ᜃ(A_1, a_);
							num = 5;
							continue;
						case ComparisonOperatorType.GreaterOrEqual:
							flag = this.ᜄ(A_1, a_);
							num = 2;
							continue;
						case ComparisonOperatorType.LessOrEqual:
							flag = this.ᜂ(A_1, a_);
							num = 1;
							continue;
						default:
							num = 3;
							continue;
						}
						break;
					case 11:
						goto IL_13C;
					case 12:
						if (flag)
						{
							num = 9;
							continue;
						}
						return result;
					case 13:
						return result;
					}
					break;
					IL_13C:
					result = null;
					num = 12;
					continue;
					IL_186:
					result = this.ᜇ(A_0, A_2);
					num = 13;
				}
			}
			return result;
		}
		}
	}

	// Token: 0x06003CB9 RID: 15545 RVA: 0x0021F16C File Offset: 0x0021E16C
	private bool ᜁ(IXLSRange A_0, object A_1, object A_2)
	{
		int num = 0;
		for (;;)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return false;
			}
			if (false)
			{
			}
			switch (num)
			{
			case 1:
				goto IL_97;
			case 2:
				goto IL_75;
			case 3:
				num = 4;
				continue;
			case 4:
				if (A_2 == null)
				{
					if (true)
					{
					}
					num = 1;
					continue;
				}
				num = 5;
				continue;
			case 5:
				if (this.ᜄ(A_0, A_1))
				{
					num = 2;
					continue;
				}
				return false;
			}
			if (A_1 == null)
			{
				return false;
			}
			num = 3;
		}
		IL_75:
		return this.ᜂ(A_0, A_2);
		IL_97:
		return false;
	}

	// Token: 0x06003CBA RID: 15546 RVA: 0x0021F21C File Offset: 0x0021E21C
	private bool ᜆ(IXLSRange A_0, object A_1)
	{
		while (A_1 == null)
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
				if (true)
				{
				}
				return false;
			}
		}
		return this.ᜀ(A_0, A_1) == 0;
	}

	// Token: 0x06003CBB RID: 15547 RVA: 0x0021F26C File Offset: 0x0021E26C
	private bool ᜅ(IXLSRange A_0, object A_1)
	{
		while (A_1 == null)
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
				return false;
			}
		}
		return this.ᜀ(A_0, A_1) > 0;
	}

	// Token: 0x06003CBC RID: 15548 RVA: 0x0021F2BC File Offset: 0x0021E2BC
	private bool ᜄ(IXLSRange A_0, object A_1)
	{
		while (A_1 == null)
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
				return false;
			}
		}
		return this.ᜀ(A_0, A_1) >= 0;
	}

	// Token: 0x06003CBD RID: 15549 RVA: 0x0021F30C File Offset: 0x0021E30C
	private bool ᜃ(IXLSRange A_0, object A_1)
	{
		int num = 3;
		int num2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (num2 == -2147483648)
				{
					return false;
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
					if (false)
					{
					}
					num = 2;
					continue;
				}
				break;
			case 1:
				return false;
			case 2:
				goto IL_81;
			}
			IL_20:
			if (A_1 == null)
			{
				num = 1;
				continue;
			}
			num2 = this.ᜀ(A_0, A_1);
			num = 0;
			continue;
			goto IL_20;
		}
		return false;
		IL_81:
		return num2 < 0;
	}

	// Token: 0x06003CBE RID: 15550 RVA: 0x0021F3A0 File Offset: 0x0021E3A0
	private bool ᜂ(IXLSRange A_0, object A_1)
	{
		int num = 1;
		int num2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (num2 == -2147483648)
				{
					return false;
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
					num = 2;
					continue;
				}
				break;
			case 2:
				goto IL_84;
			case 3:
				return false;
			}
			IL_20:
			if (true)
			{
			}
			if (A_1 == null)
			{
				num = 3;
				continue;
			}
			num2 = this.ᜀ(A_0, A_1);
			num = 0;
			continue;
			goto IL_20;
		}
		return false;
		IL_84:
		return num2 <= 0;
	}

	// Token: 0x06003CBF RID: 15551 RVA: 0x0021F438 File Offset: 0x0021E438
	private bool ᜀ(IXLSRange A_0, object A_1, object A_2)
	{
		int num = 5;
		for (;;)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return false;
			}
			if (false)
			{
			}
			switch (num)
			{
			case 0:
				num = 3;
				continue;
			case 1:
				goto IL_97;
			case 2:
				goto IL_75;
			case 3:
				if (A_2 == null)
				{
					if (true)
					{
					}
					num = 1;
					continue;
				}
				num = 4;
				continue;
			case 4:
				if (!this.ᜃ(A_0, A_1))
				{
					num = 2;
					continue;
				}
				return true;
			}
			if (A_1 == null)
			{
				return false;
			}
			num = 0;
		}
		IL_75:
		return this.ᜅ(A_0, A_2);
		IL_97:
		return false;
	}

	// Token: 0x06003CC0 RID: 15552 RVA: 0x0021F4E8 File Offset: 0x0021E4E8
	private bool ᜁ(IXLSRange A_0, object A_1)
	{
		int num = 0;
		int num2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_08;
			case 1:
				return false;
			case 2:
				if (num2 == -2147483648)
				{
					return false;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_08;
				default:
					if (false)
					{
					}
					num = 3;
					continue;
				}
				break;
			case 3:
				goto IL_84;
			}
			IL_28:
			if (A_1 == null)
			{
				num = 1;
				continue;
			}
			num2 = this.ᜀ(A_0, A_1);
			num = 2;
			continue;
			IL_08:
			if (true)
			{
			}
			goto IL_28;
		}
		return false;
		IL_84:
		return num2 != 0;
	}

	// Token: 0x06003CC1 RID: 15553 RVA: 0x0021F580 File Offset: 0x0021E580
	private int ᜀ(IXLSRange A_0, object A_1)
	{
		switch (0)
		{
		default:
		{
			int num = 10;
			int result;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_0.FormulaStringValue != null)
					{
						num = 16;
						continue;
					}
					num = 12;
					continue;
				case 1:
				{
					double numberValue = A_0.NumberValue;
					result = this.ᜀ(numberValue, A_1);
					num = 4;
					continue;
				}
				case 2:
					return result;
				case 3:
					goto IL_10E;
				case 4:
					goto IL_EF;
				case 5:
					goto IL_A6;
				case 6:
					return result;
				case 7:
					result = this.ᜀ(A_0.FormulaBoolValue, A_1);
					num = 5;
					continue;
				case 8:
					if (A_0.HasBoolean)
					{
						num = 19;
						continue;
					}
					num = 15;
					continue;
				case 9:
					num = 14;
					continue;
				case 11:
					return int.MinValue;
				case 12:
					if (!double.IsNaN(A_0.FormulaNumberValue))
					{
						num = 18;
						continue;
					}
					return result;
				case 13:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_26F;
					default:
						if (false)
						{
						}
						if (A_0.HasNumber)
						{
							num = 1;
							continue;
						}
						num = 21;
						continue;
					}
					break;
				case 14:
					if (A_0.HasFormulaBoolValue)
					{
						num = 7;
						continue;
					}
					num = 0;
					continue;
				case 15:
					if (A_0.HasFormula)
					{
						num = 9;
						continue;
					}
					return result;
				case 16:
					result = this.ᜀ(A_0.FormulaStringValue, A_1);
					num = 3;
					continue;
				case 17:
				{
					string text = A_0.Text;
					this.ᜀ(text, A_1);
					num = 20;
					continue;
				}
				case 18:
					result = this.ᜀ(A_0.FormulaNumberValue, A_1);
					num = 6;
					continue;
				case 19:
				{
					bool booleanValue = A_0.BooleanValue;
					result = this.ᜀ(booleanValue, A_1);
					num = 2;
					continue;
				}
				case 20:
					return result;
				case 21:
					if (A_0.HasString)
					{
						num = 17;
						continue;
					}
					goto IL_26F;
				}
				if (A_1 == null)
				{
					num = 11;
					continue;
				}
				result = int.MinValue;
				num = 13;
				continue;
				IL_26F:
				num = 8;
			}
			return int.MinValue;
			IL_A6:
			IL_EF:
			return result;
			IL_10E:
			if (true)
			{
			}
			return result;
		}
		}
	}

	// Token: 0x06003CC2 RID: 15554 RVA: 0x0021F828 File Offset: 0x0021E828
	private int ᜀ(double A_0, object A_1)
	{
		while (!(A_1 is double))
		{
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				continue;
			}
			if (false)
			{
			}
			return int.MinValue;
		}
		return A_0.CompareTo((double)A_1);
	}

	// Token: 0x06003CC3 RID: 15555 RVA: 0x0021F880 File Offset: 0x0021E880
	private int ᜀ(string A_0, object A_1)
	{
		string text;
		for (;;)
		{
			text = (A_1 as string);
			if (text != null)
			{
				goto IL_40;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_20;
			}
		}
		IL_20:
		if (true)
		{
		}
		if (false)
		{
		}
		return int.MinValue;
		IL_40:
		return this.ᜁ.Compare(A_0, text);
	}

	// Token: 0x06003CC4 RID: 15556 RVA: 0x0021F8DC File Offset: 0x0021E8DC
	private int ᜀ(bool A_0, object A_1)
	{
		while (!(A_1 is bool))
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
				return int.MinValue;
			}
		}
		return A_0.CompareTo((bool)A_1);
	}

	// Token: 0x06003CC5 RID: 15557 RVA: 0x0021F934 File Offset: 0x0021E934
	private spr\u192F ᜀ(IConditionalFormat A_0, IXLSRange A_1, spr\u192F A_2)
	{
		if (true)
		{
		}
		spr\u192F result;
		for (;;)
		{
			result = null;
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					result = this.ᜇ(A_0, A_2);
					num = 1;
					continue;
				case 1:
					return result;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					default:
						if (false)
						{
						}
						if (A_1.Formula == A_0.FirstFormula)
						{
							num = 0;
							continue;
						}
						return result;
					}
					break;
				}
				break;
			}
		}
		return result;
	}

	// Token: 0x06003CC6 RID: 15558 RVA: 0x0021F9C0 File Offset: 0x0021E9C0
	private spr\u192F ᜇ(IConditionalFormat A_0, spr\u192F A_1)
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
		spr\u192F spr_u192F = new spr\u22FD(A_1);
		this.ᜅ(A_0, spr_u192F);
		this.ᜆ(A_0, spr_u192F);
		this.ᜀ(A_0, spr_u192F);
		return spr_u192F;
	}

	// Token: 0x06003CC7 RID: 15559 RVA: 0x0021FA1C File Offset: 0x0021EA1C
	private void ᜆ(IConditionalFormat A_0, spr\u192F A_1)
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
		A_1.ᜀ().BeginUpdate();
		this.ᜁ(A_0, A_1);
		this.ᜃ(A_0, A_1);
		A_1.ᜀ().EndUpdate();
	}

	// Token: 0x06003CC8 RID: 15560 RVA: 0x0021FA80 File Offset: 0x0021EA80
	private void ᜅ(IConditionalFormat A_0, spr\u192F A_1)
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
		this.ᜂ(A_0, A_1);
		this.ᜀ(A_0 as XlsConditionalFormat, A_1);
		this.ᜄ(A_0, A_1);
	}

	// Token: 0x06003CC9 RID: 15561 RVA: 0x0021FAD8 File Offset: 0x0021EAD8
	private void ᜀ(XlsConditionalFormat A_0, spr\u192F A_1)
	{
		int num = 0;
		for (;;)
		{
			if (true)
			{
			}
			switch (num)
			{
			case 1:
				return;
			case 2:
				goto IL_50;
			}
			if (!A_0.IsPatternColorPresent)
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
				num = 2;
				continue;
			}
			IL_50:
			A_1.ᜂ(A_0.Color);
			num = 1;
		}
	}

	// Token: 0x06003CCA RID: 15562 RVA: 0x0021FB58 File Offset: 0x0021EB58
	private void ᜄ(IConditionalFormat A_0, spr\u192F A_1)
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				return;
			case 1:
				goto IL_50;
			}
			if (!A_0.IsBackgroundColorPresent)
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
				if (true)
				{
				}
				num = 1;
				continue;
			}
			IL_50:
			A_1.ᜃ(A_0.BackColor);
			num = 0;
		}
	}

	// Token: 0x06003CCB RID: 15563 RVA: 0x0021FBD8 File Offset: 0x0021EBD8
	private void ᜃ(IConditionalFormat A_0, spr\u192F A_1)
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_48;
			case 2:
				return;
			}
			if (!A_0.IsFontColorPresent)
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
				num = 0;
				continue;
			}
			IL_48:
			A_1.ᜀ().Color = A_0.FontColor;
			if (true)
			{
			}
			num = 2;
		}
	}

	// Token: 0x06003CCC RID: 15564 RVA: 0x0021FC5C File Offset: 0x0021EC5C
	private void ᜂ(IConditionalFormat A_0, spr\u192F A_1)
	{
		if (true)
		{
		}
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_50;
			case 2:
				return;
			}
			if (!A_0.IsPatternFormatPresent)
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
				num = 0;
				continue;
			}
			IL_50:
			A_1.ᜀ(A_0.FillPattern);
			num = 2;
		}
	}

	// Token: 0x06003CCD RID: 15565 RVA: 0x0021FCDC File Offset: 0x0021ECDC
	private void ᜁ(IConditionalFormat A_0, spr\u192F A_1)
	{
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				goto IL_4B;
			case 2:
				return;
			}
			if (!A_0.IsFontFormatPresent)
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
				num = 1;
				continue;
			}
			IL_4B:
			if (true)
			{
			}
			IFont font = A_1.ᜀ();
			font.IsBold = A_0.IsBold;
			font.IsItalic = A_0.IsItalic;
			font.IsStrikethrough = A_0.IsStrikeThrough;
			font.IsSubscript = A_0.IsSubScript;
			font.IsSuperscript = A_0.IsSuperScript;
			font.Underline = A_0.Underline;
			num = 2;
		}
	}

	// Token: 0x06003CCE RID: 15566 RVA: 0x0021FDA4 File Offset: 0x0021EDA4
	private void ᜀ(IConditionalFormat A_0, spr\u192F A_1)
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_A0;
				default:
					if (false)
					{
					}
					goto IL_19D;
				}
				break;
			case 1:
				A_1.\u173F().ᜀ(A_0.TopBorderColor);
				A_1.ᜄ(A_0.TopBorderStyle);
				num = 7;
				continue;
			case 3:
				goto IL_8A;
			case 4:
				num = 12;
				continue;
			case 5:
				A_1.ᝅ().ᜀ(A_0.LeftBorderColor);
				A_1.ᜀ(A_0.LeftBorderStyle);
				num = 0;
				continue;
			case 6:
				if (true)
				{
				}
				A_1.\u1756().ᜀ(A_0.RightBorderColor);
				A_1.ᜂ(A_0.RightBorderStyle);
				num = 3;
				continue;
			case 7:
				goto IL_146;
			case 8:
				A_1.ᜡ().ᜀ(A_0.BottomBorderColor);
				A_1.ᜅ(A_0.BottomBorderStyle);
				num = 9;
				continue;
			case 9:
				return;
			case 10:
				if (A_0.IsRightBorderModified)
				{
					num = 6;
					continue;
				}
				goto IL_8A;
			case 11:
				if (A_0.IsTopBorderModified)
				{
					goto IL_A0;
				}
				goto IL_146;
			case 12:
				if (A_0.IsLeftBorderModified)
				{
					num = 5;
					continue;
				}
				goto IL_19D;
			case 13:
				if (A_0.IsBottomBorderModified)
				{
					num = 8;
					continue;
				}
				return;
			}
			if (A_0.IsBorderFormatPresent)
			{
				num = 4;
				continue;
			}
			break;
			IL_8A:
			num = 11;
			continue;
			IL_A0:
			num = 1;
			continue;
			IL_146:
			num = 13;
			continue;
			IL_19D:
			num = 10;
		}
	}

	// Token: 0x04001A39 RID: 6713
	private sprᠮ ᜀ = new sprᠮ();

	// Token: 0x04001A3A RID: 6714
	private Spire.Xls.Core.Spreadsheet.StringComparer ᜁ = new Spire.Xls.Core.Spreadsheet.StringComparer();
}
