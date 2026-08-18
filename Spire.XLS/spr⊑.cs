using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Spire.Xls.Core.Parser.Biff_Records.Formula;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x0200043F RID: 1087
internal class spr\u2291
{
	// Token: 0x0600414F RID: 16719 RVA: 0x002494D4 File Offset: 0x002484D4
	public spr\u2291(XlsWorkbook A_0)
	{
		int a_ = 1;
		this.ᜉ = new StringBuilder();
		this.ᜋ = ',';
		this.ᜌ = NumberFormatInfo.CurrentInfo;
		this.\u170D = -1;
		base..ctor();
		if (A_0 == null)
		{
			throw new ArgumentNullException(RecordTableEnumerator.b("唶嘸吺嘼", a_));
		}
		this.ᜊ = A_0;
	}

	// Token: 0x06004150 RID: 16720 RVA: 0x00249538 File Offset: 0x00248538
	public void ᜁ(string A_0)
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
		this.ᜄ = A_0;
		this.ᜂ = A_0.Length;
		this.ᜃ = 0;
		this.ᜊ();
	}

	// Token: 0x06004151 RID: 16721 RVA: 0x00249594 File Offset: 0x00248594
	private void ᜊ()
	{
		if (this.ᜃ < this.ᜂ)
		{
			for (;;)
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
					goto IL_2E;
				}
			}
			IL_2E:
			if (false)
			{
			}
			this.ᜁ = this.ᜄ[this.ᜃ];
			this.ᜃ++;
			return;
		}
		this.ᜁ = '\u0001';
	}

	// Token: 0x06004152 RID: 16722 RVA: 0x0024960C File Offset: 0x0024860C
	private void ᜀ(char A_0)
	{
		for (;;)
		{
			int startIndex = Math.Min(this.ᜄ.Length - 1, this.ᜃ);
			int num = this.ᜄ.LastIndexOf(A_0, startIndex);
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
						int num3 = this.ᜃ - num;
						this.ᜃ = num + 1;
						this.ᜉ.Remove(this.ᜉ.Length - num3 + 1, num3 - 1);
						this.ᜁ = A_0;
						if (true)
						{
						}
						num2 = 2;
						continue;
					}
					}
					break;
				case 1:
					if (num >= 0)
					{
						num2 = 0;
						continue;
					}
					return;
				case 2:
					return;
				}
				break;
			}
		}
	}

	// Token: 0x06004153 RID: 16723 RVA: 0x002496DC File Offset: 0x002486DC
	public void ᜋ()
	{
		int a_ = 9;
		switch (0)
		{
		default:
			for (;;)
			{
				this.ᜇ = this.ᜆ;
				this.ᜈ = this.ᜅ;
				this.ᜉ.Length = 0;
				int num = 10;
				for (;;)
				{
					char c;
					char c2;
					char c3;
					char c4;
					switch (num)
					{
					case 0:
						goto IL_2E0;
					case 1:
						goto IL_2E0;
					case 2:
					{
						int num2;
						if (num2 / this.ᜀ(this.ᜄ, '!', '\'') <= 2)
						{
							num = 45;
							continue;
						}
						goto IL_234;
					}
					case 3:
						if (c != '^')
						{
							num = 15;
							continue;
						}
						this.ᜆ = FormulaToken.tPower;
						this.ᜊ();
						num = 42;
						continue;
					case 4:
						goto IL_2E0;
					case 5:
						c2 = this.ᜄ[this.ᜄ.IndexOf(this.ᜁ) + 1];
						num = 43;
						continue;
					case 6:
						goto IL_2E0;
					case 7:
						goto IL_7E0;
					case 8:
						this.ᜄ();
						num = 16;
						continue;
					case 9:
						if (c != '\u0001')
						{
							num = 25;
							continue;
						}
						this.ᜆ = FormulaToken.EndOfFormula;
						num = 67;
						continue;
					case 10:
						if (this.ᜆ != FormulaToken.DDELink)
						{
							num = 52;
							continue;
						}
						goto IL_5C8;
					case 11:
						return;
					case 12:
						switch (c)
						{
						case '"':
							this.\u170D = -1;
							this.ᜀ(true);
							this.ᜆ = FormulaToken.tStringConstant;
							num = 1;
							continue;
						case '#':
							this.ᜂ();
							num = 14;
							continue;
						case '$':
						case ',':
						case '.':
						case '0':
						case '1':
						case '2':
						case '3':
						case '4':
						case '5':
						case '6':
						case '7':
						case '8':
						case '9':
						case ';':
							goto IL_823;
						case '%':
							this.ᜊ();
							this.ᜆ = FormulaToken.tPercent;
							num = 0;
							continue;
						case '&':
							this.ᜊ();
							this.ᜆ = FormulaToken.tConcat;
							num = 47;
							continue;
						case '\'':
							this.\u170D = -1;
							num = 50;
							continue;
						case '(':
							this.ᜊ();
							this.ᜆ = FormulaToken.tParentheses;
							num = 40;
							continue;
						case ')':
							this.ᜊ();
							this.ᜆ = FormulaToken.CloseParenthesis;
							num = 56;
							continue;
						case '*':
							this.ᜊ();
							this.ᜆ = FormulaToken.tMul;
							num = 66;
							continue;
						case '+':
							this.ᜊ();
							this.ᜆ = FormulaToken.tAdd;
							num = 62;
							continue;
						case '-':
							this.ᜊ();
							this.ᜆ = FormulaToken.tSub;
							num = 69;
							continue;
						case '/':
							this.ᜊ();
							this.ᜆ = FormulaToken.tDiv;
							num = 53;
							continue;
						case ':':
							this.ᜊ();
							this.ᜆ = FormulaToken.tCellRange;
							num = 36;
							continue;
						case '<':
							this.ᜇ();
							num = 4;
							continue;
						case '=':
							this.ᜊ();
							this.ᜆ = FormulaToken.tEqual;
							num = 28;
							continue;
						case '>':
							this.ᜈ();
							num = 51;
							continue;
						default:
							num = 61;
							continue;
						}
						break;
					case 13:
						goto IL_6AE;
					case 14:
						goto IL_2E0;
					case 15:
						num = 54;
						continue;
					case 16:
						goto IL_2E0;
					case 17:
						c3 = this.ᜄ[this.ᜄ.IndexOf(this.ᜁ) - 1];
						num = 22;
						continue;
					case 18:
						goto IL_2E0;
					case 19:
					{
						int num2 = this.ᜁ(this.ᜄ, '\'');
						num = 27;
						continue;
					}
					case 20:
						goto IL_2E0;
					case 21:
						this.ᜉ();
						num = 6;
						continue;
					case 22:
						goto IL_781;
					case 23:
						if (this.ᜁ != '.')
						{
							num = 64;
							continue;
						}
						goto IL_51F;
					case 24:
						if (this.ᜄ.IndexOf(this.ᜁ) > 0)
						{
							num = 17;
							continue;
						}
						goto IL_781;
					case 25:
						num = 12;
						continue;
					case 26:
						goto IL_823;
					case 27:
					{
						int num2;
						if (num2 % 2 == 0)
						{
							num = 73;
							continue;
						}
						goto IL_7E0;
					}
					case 28:
						goto IL_2E0;
					case 29:
						this.ᜊ();
						this.ᜆ = FormulaToken.Comma;
						num = 18;
						continue;
					case 30:
						goto IL_2E0;
					case 31:
						if (this.ᜁ == ' ')
						{
							num = 8;
							continue;
						}
						goto IL_2E0;
					case 32:
						if (char.IsDigit(c2))
						{
							num = 68;
							continue;
						}
						goto IL_293;
					case 33:
						goto IL_823;
					case 34:
						goto IL_234;
					case 35:
						if (this.ᜁ == c4)
						{
							num = 55;
							continue;
						}
						goto IL_293;
					case 36:
						goto IL_2E0;
					case 37:
						if (this.ᜄ.IndexOf(this.ᜁ) < this.ᜄ.Length - 1)
						{
							num = 5;
							continue;
						}
						goto IL_6AE;
					case 38:
						goto IL_2E0;
					case 39:
						if (this.ᜇ == FormulaToken.DDELink)
						{
							num = 44;
							continue;
						}
						this.ᜆ = FormulaToken.Identifier;
						this.ᜅ();
						num = 49;
						continue;
					case 40:
						goto IL_2E0;
					case 41:
						if (c <= '>')
						{
							num = 59;
							continue;
						}
						num = 3;
						continue;
					case 42:
						goto IL_2E0;
					case 43:
						goto IL_6AE;
					case 44:
						this.ᜊ();
						num = 30;
						continue;
					case 45:
						num = 72;
						continue;
					case 46:
						this.ᜅ();
						num = 38;
						continue;
					case 47:
						goto IL_2E0;
					case 48:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_422;
						default:
							if (false)
							{
							}
							if (this.ᜁ > ' ')
							{
								num = 46;
								continue;
							}
							num = 31;
							continue;
						}
						break;
					case 49:
						goto IL_2E0;
					case 50:
						if (this.ᜄ.Contains(RecordTableEnumerator.b("ᠾ", a_)))
						{
							num = 19;
							continue;
						}
						goto IL_234;
					case 51:
						goto IL_2E0;
					case 52:
						this.ᜆ = FormulaToken.None;
						num = 58;
						continue;
					case 53:
						goto IL_2E0;
					case 54:
						if (c != '{')
						{
							num = 57;
							continue;
						}
						this.ᜁ();
						this.ᜆ = FormulaToken.tArray1;
						num = 74;
						continue;
					case 55:
						num = 71;
						continue;
					case 56:
						goto IL_2E0;
					case 57:
						num = 26;
						continue;
					case 58:
						goto IL_5C8;
					case 59:
						num = 9;
						continue;
					case 60:
						if (this.ᜆ != FormulaToken.None)
						{
							num = 11;
							continue;
						}
						this.ᜊ();
						num = 13;
						continue;
					case 61:
						goto IL_422;
					case 62:
						goto IL_2E0;
					case 63:
						if (char.IsDigit(this.ᜁ))
						{
							num = 21;
							continue;
						}
						num = 23;
						continue;
					case 64:
						num = 35;
						continue;
					case 65:
						num = 32;
						continue;
					case 66:
						goto IL_2E0;
					case 67:
						goto IL_2E0;
					case 68:
						goto IL_51F;
					case 69:
						goto IL_2E0;
					case 70:
						if (true)
						{
						}
						if (this.ᜁ == this.ᜋ)
						{
							num = 29;
							continue;
						}
						num = 48;
						continue;
					case 71:
						if (char.IsDigit(c3))
						{
							num = 65;
							continue;
						}
						goto IL_293;
					case 72:
						if ((this.ᜀ(this.ᜄ, '(') + this.ᜀ(this.ᜄ, ')')) % 2 != 0)
						{
							num = 7;
							continue;
						}
						goto IL_234;
					case 73:
						num = 2;
						continue;
					case 74:
						goto IL_2E0;
					}
					break;
					IL_234:
					this.ᜀ(true);
					num = 39;
					continue;
					IL_293:
					c = this.ᜁ;
					num = 41;
					continue;
					IL_2E0:
					num = 60;
					continue;
					IL_422:
					num = 33;
					continue;
					IL_51F:
					this.ᜉ();
					num = 20;
					continue;
					IL_5C8:
					c4 = this.ᜌ.NumberDecimalSeparator[0];
					c3 = ' ';
					c2 = ' ';
					num = 24;
					continue;
					IL_6AE:
					this.ᜅ = this.ᜃ;
					num = 63;
					continue;
					IL_781:
					num = 37;
					continue;
					IL_7E0:
					this.\u170D = this.ᜄ.LastIndexOf('\'');
					num = 34;
					continue;
					IL_823:
					num = 70;
				}
			}
			return;
		}
	}

	// Token: 0x06004154 RID: 16724 RVA: 0x0024A090 File Offset: 0x00249090
	private int ᜁ(string A_0, char A_1)
	{
		switch (0)
		{
		default:
		{
			int num;
			for (;;)
			{
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
					num = 0;
					char[] array = A_0.ToCharArray();
					char[] array2 = array;
					int num2 = 0;
					int num3 = 1;
					for (;;)
					{
						switch (num3)
						{
						case 0:
							goto IL_BD;
						case 1:
							goto IL_BD;
						case 2:
							return num;
						case 3:
						{
							if (true)
							{
							}
							char c;
							if (c == A_1)
							{
								num3 = 5;
								continue;
							}
							goto IL_72;
						}
						case 4:
						{
							if (num2 >= array2.Length)
							{
								num3 = 2;
								continue;
							}
							char c = array2[num2];
							num3 = 3;
							continue;
						}
						case 5:
							num++;
							num3 = 6;
							continue;
						case 6:
							goto IL_72;
						}
						break;
						IL_72:
						num2++;
						num3 = 0;
						continue;
						IL_BD:
						num3 = 4;
					}
					break;
				}
				}
			}
			return num;
		}
		}
	}

	// Token: 0x06004155 RID: 16725 RVA: 0x0024A17C File Offset: 0x0024917C
	private int ᜀ(string A_0, char A_1)
	{
		switch (0)
		{
		default:
		{
			int num;
			for (;;)
			{
				bool flag = false;
				num = 0;
				char[] array = A_0.ToCharArray();
				char[] array2 = array;
				int num2 = 0;
				int num3 = 10;
				for (;;)
				{
					switch (num3)
					{
					case 0:
						num++;
						num3 = 4;
						continue;
					case 1:
					{
						char c;
						if (c != A_1)
						{
							goto IL_76;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_76;
						default:
							if (false)
							{
							}
							num3 = 11;
							continue;
						}
						break;
					}
					case 2:
						goto IL_12C;
					case 3:
					{
						char c;
						if (c == '\'')
						{
							if (true)
							{
							}
							num3 = 8;
							continue;
						}
						goto IL_12C;
					}
					case 4:
						goto IL_76;
					case 5:
						return num;
					case 6:
					{
						if (num2 >= array2.Length)
						{
							num3 = 5;
							continue;
						}
						char c = array2[num2];
						num3 = 1;
						continue;
					}
					case 7:
						goto IL_BD;
					case 8:
						flag = !flag;
						num3 = 2;
						continue;
					case 9:
						if (!flag)
						{
							num3 = 0;
							continue;
						}
						goto IL_76;
					case 10:
						goto IL_BD;
					case 11:
						num3 = 9;
						continue;
					}
					break;
					IL_76:
					num3 = 3;
					continue;
					IL_BD:
					num3 = 6;
					continue;
					IL_12C:
					num2++;
					num3 = 7;
				}
			}
			return num;
		}
		}
	}

	// Token: 0x06004156 RID: 16726 RVA: 0x0024A2E4 File Offset: 0x002492E4
	private int ᜀ(string A_0, char A_1, char A_2)
	{
		switch (0)
		{
		default:
		{
			int num;
			for (;;)
			{
				IL_3B:
				num = 0;
				int num2 = 0;
				char[] array = A_0.ToCharArray();
				char[] array2 = array;
				int num3 = 0;
				for (;;)
				{
					IL_4C:
					int num4 = 1;
					for (;;)
					{
						switch (num4)
						{
						case 0:
							return num;
						case 1:
							goto IL_D3;
						case 2:
							if (array[num2 - 1] == A_2)
							{
								num4 = 4;
								continue;
							}
							goto IL_61;
						case 3:
							goto IL_61;
						case 4:
							num++;
							num4 = 3;
							continue;
						case 5:
							num4 = 2;
							continue;
						case 6:
						{
							char c;
							if (c == A_1)
							{
								num4 = 5;
								continue;
							}
							goto IL_61;
						}
						case 7:
							goto IL_D3;
						case 8:
						{
							if (num3 >= array2.Length)
							{
								num4 = 0;
								continue;
							}
							char c = array2[num3];
							num4 = 6;
							continue;
						}
						}
						goto IL_3B;
						IL_61:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_4C;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							num2++;
							num3++;
							num4 = 7;
							continue;
						}
						IL_D3:
						num4 = 8;
					}
				}
			}
			return num;
		}
		}
	}

	// Token: 0x06004157 RID: 16727 RVA: 0x0024A40C File Offset: 0x0024940C
	public void ᜐ()
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
		this.ᜈ = this.ᜅ;
		this.ᜇ = this.ᜆ;
	}

	// Token: 0x06004158 RID: 16728 RVA: 0x0024A460 File Offset: 0x00249460
	public void ᜌ()
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
		this.ᜅ = this.ᜈ;
		this.ᜆ = this.ᜇ;
		this.ᜁ = this.ᜄ[this.ᜅ];
		this.ᜉ.Length = 0;
	}

	// Token: 0x06004159 RID: 16729 RVA: 0x0024A4D8 File Offset: 0x002494D8
	private void ᜉ()
	{
		for (;;)
		{
			this.ᜆ = FormulaToken.tInteger;
			this.ᜆ();
			int num = 8;
			for (;;)
			{
				switch (num)
				{
				case 0:
					this.ᜆ = FormulaToken.tNumber;
					this.ᜉ.Append(this.ᜁ);
					this.ᜊ();
					goto IL_1DE;
				case 1:
					goto IL_A4;
				case 2:
					num = 12;
					continue;
				case 3:
					this.ᜆ = FormulaToken.Identifier;
					this.ᜉ.Append(this.ᜁ);
					this.ᜊ();
					this.ᜆ();
					num = 9;
					continue;
				case 4:
					goto IL_7F;
				case 5:
					if (this.ᜁ != '-')
					{
						num = 2;
						continue;
					}
					goto IL_7F;
				case 6:
					if (this.ᜁ == ':')
					{
						num = 3;
						continue;
					}
					return;
				case 7:
					goto IL_A2;
				case 8:
					if (this.ᜁ == this.ᜌ.NumberDecimalSeparator[0])
					{
						num = 10;
						continue;
					}
					goto IL_A4;
				case 9:
					return;
				case 10:
					this.ᜆ = FormulaToken.tNumber;
					this.ᜉ.Append(this.ᜁ);
					this.ᜊ();
					this.ᜆ();
					num = 1;
					continue;
				case 11:
					if (char.ToUpper(this.ᜁ) == 'E')
					{
						num = 0;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1DE;
					default:
						if (false)
						{
						}
						num = 6;
						continue;
					}
					break;
				case 12:
					if (this.ᜁ == '+')
					{
						if (true)
						{
						}
						num = 4;
						continue;
					}
					goto IL_78;
				}
				break;
				IL_7F:
				this.ᜉ.Append(this.ᜁ);
				this.ᜊ();
				num = 7;
				continue;
				IL_A4:
				num = 11;
				continue;
				IL_1DE:
				num = 5;
			}
		}
		IL_78:
		this.ᜆ();
		return;
		IL_A2:
		goto IL_78;
	}

	// Token: 0x0600415A RID: 16730 RVA: 0x0024A6E8 File Offset: 0x002496E8
	private void ᜈ()
	{
		for (;;)
		{
			if (true)
			{
			}
			this.ᜊ();
			if (this.ᜁ == '=')
			{
				break;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_49;
			}
		}
		this.ᜊ();
		this.ᜆ = FormulaToken.tGreaterEqual;
		return;
		IL_49:
		if (false)
		{
		}
		this.ᜆ = FormulaToken.tGreater;
	}

	// Token: 0x0600415B RID: 16731 RVA: 0x0024A74C File Offset: 0x0024974C
	private void ᜇ()
	{
		for (;;)
		{
			this.ᜊ();
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_5E;
			default:
			{
				if (true)
				{
				}
				if (false)
				{
				}
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (this.ᜁ == '=')
						{
							num = 1;
							continue;
						}
						num = 2;
						continue;
					case 1:
						goto IL_5C;
					case 2:
						if (this.ᜁ == '>')
						{
							num = 3;
							continue;
						}
						goto IL_9B;
					case 3:
						goto IL_8A;
					}
					break;
				}
				break;
			}
			}
		}
		IL_5C:
		this.ᜊ();
		this.ᜆ = FormulaToken.tLessEqual;
		return;
		IL_5E:
		this.ᜊ();
		this.ᜆ = FormulaToken.tNotEqual;
		return;
		IL_8A:
		goto IL_5E;
		IL_9B:
		this.ᜆ = FormulaToken.tLessThan;
	}

	// Token: 0x0600415C RID: 16732 RVA: 0x0024A808 File Offset: 0x00249808
	private void ᜆ()
	{
		int num = 2;
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
				switch (num)
				{
				case 0:
					if (!char.IsDigit(this.ᜁ))
					{
						num = 1;
						continue;
					}
					this.ᜉ.Append(this.ᜁ);
					this.ᜊ();
					num = 3;
					continue;
				case 1:
					return;
				}
				IL_46:
				num = 0;
				break;
				goto IL_46;
			}
		}
	}

	// Token: 0x0600415D RID: 16733 RVA: 0x0024A8A8 File Offset: 0x002498A8
	private void ᜀ(char A_0, char A_1)
	{
		for (;;)
		{
			Stack<char> stack = new Stack<char>();
			stack.Push(A_0);
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return;
				case 1:
					stack.Pop();
					num = 5;
					continue;
				case 2:
					stack.Push(A_0);
					num = 6;
					continue;
				case 3:
					if (this.ᜁ == A_0)
					{
						num = 2;
						continue;
					}
					goto IL_64;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_64;
					default:
						if (false)
						{
						}
						goto IL_8A;
					}
					break;
				case 5:
					if (stack.Count == 0)
					{
						num = 7;
						continue;
					}
					goto IL_C3;
				case 6:
					goto IL_C3;
				case 7:
					return;
				case 8:
					if (this.ᜁ == A_1)
					{
						num = 1;
						continue;
					}
					goto IL_C3;
				case 9:
					if (this.ᜁ == '\u0001')
					{
						num = 0;
						continue;
					}
					goto IL_8A;
				}
				break;
				IL_64:
				if (true)
				{
				}
				num = 8;
				continue;
				IL_8A:
				this.ᜊ();
				this.ᜉ.Append(this.ᜁ);
				num = 3;
				continue;
				IL_C3:
				num = 9;
			}
		}
	}

	// Token: 0x0600415E RID: 16734 RVA: 0x0024A9E4 File Offset: 0x002499E4
	private void ᜅ()
	{
		int a_ = 6;
		switch (0)
		{
		default:
			for (;;)
			{
				bool flag = false;
				bool flag2 = false;
				int num = 0;
				int num2 = 18;
				for (;;)
				{
					char c;
					string strA;
					switch (num2)
					{
					case 0:
						if (this.ᜁ == ':')
						{
							num2 = 23;
							continue;
						}
						goto IL_505;
					case 1:
						if (this.ᜁ != '#')
						{
							num2 = 60;
							continue;
						}
						goto IL_4DE;
					case 2:
						num2 = 1;
						continue;
					case 3:
						goto IL_242;
					case 4:
						if (this.ᜁ == '(')
						{
							num2 = 28;
							continue;
						}
						num2 = 31;
						continue;
					case 5:
						goto IL_176;
					case 6:
						num2 = 37;
						continue;
					case 7:
						if (c != '\u0001')
						{
							num2 = 13;
							continue;
						}
						goto IL_2E6;
					case 8:
						goto IL_451;
					case 9:
						if (num > 1)
						{
							num2 = 38;
							continue;
						}
						goto IL_505;
					case 10:
						goto IL_242;
					case 11:
						if (this.ᜁ == '\'')
						{
							num2 = 42;
							continue;
						}
						goto IL_242;
					case 12:
						if (this.ᜁ != '!')
						{
							num2 = 6;
							continue;
						}
						goto IL_4DE;
					case 13:
						goto IL_3B9;
					case 14:
						if (this.ᜁ != '.')
						{
							num2 = 54;
							continue;
						}
						goto IL_4DE;
					case 15:
						goto IL_543;
					case 16:
						goto IL_2E6;
					case 17:
						if (this.ᜁ == '\u0001')
						{
							num2 = 16;
							continue;
						}
						goto IL_3B9;
					case 18:
						goto IL_543;
					case 19:
						if (this.ᜆ == FormulaToken.DDELink)
						{
							num2 = 20;
							continue;
						}
						num2 = 9;
						continue;
					case 20:
						this.ᜊ();
						num2 = 29;
						continue;
					case 21:
						c = ']';
						this.ᜀ(this.ᜁ, c);
						c = '\u0001';
						num2 = 10;
						continue;
					case 22:
						num2 = 4;
						continue;
					case 23:
						flag2 = true;
						goto IL_207;
					case 24:
						goto IL_5F1;
					case 25:
						num2 = 14;
						continue;
					case 26:
						if (this.ᜁ == '|')
						{
							num2 = 8;
							continue;
						}
						num2 = 47;
						continue;
					case 27:
						if (this.ᜆ != FormulaToken.DDELink)
						{
							num2 = 22;
							continue;
						}
						return;
					case 28:
						goto IL_6AA;
					case 29:
						goto IL_5F1;
					case 30:
						goto IL_1AE;
					case 31:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_207;
						default:
							if (false)
							{
							}
							if (string.Compare(strA, RecordTableEnumerator.b("䠻䰽㔿❁", a_), StringComparison.CurrentCultureIgnoreCase) == 0)
							{
								num2 = 58;
								continue;
							}
							num2 = 50;
							continue;
						}
						break;
					case 32:
						num2 = 36;
						continue;
					case 33:
						num2 = 12;
						continue;
					case 34:
						this.ᜆ = (flag ? FormulaToken.Identifier3D : FormulaToken.Identifier);
						num2 = 63;
						continue;
					case 35:
						if (this.ᜁ != '\'')
						{
							num2 = 2;
							continue;
						}
						goto IL_4DE;
					case 36:
						if (this.ᜁ != '[')
						{
							num2 = 40;
							continue;
						}
						goto IL_4DE;
					case 37:
						if (this.ᜁ != ':')
						{
							num2 = 25;
							continue;
						}
						goto IL_4DE;
					case 38:
						num2 = 56;
						continue;
					case 39:
						if (this.ᜁ != '_')
						{
							num2 = 33;
							continue;
						}
						goto IL_4DE;
					case 40:
						num2 = 35;
						continue;
					case 41:
						num2 = 39;
						continue;
					case 42:
						c = this.ᜁ;
						num2 = 3;
						continue;
					case 43:
						num2 = 17;
						continue;
					case 44:
						if (!char.IsLetterOrDigit(this.ᜁ))
						{
							num2 = 45;
							continue;
						}
						goto IL_4DE;
					case 45:
						num2 = 61;
						continue;
					case 46:
						flag = true;
						num++;
						num2 = 19;
						continue;
					case 47:
						if (this.ᜉ.ToString() == RecordTableEnumerator.b("猻䠽┿ぁ㉃⽅ⵇ㵉测浍ɏᝑቓ睕", a_))
						{
							num2 = 5;
							continue;
						}
						num2 = 62;
						continue;
					case 48:
						goto IL_5F1;
					case 49:
						goto IL_200;
					case 50:
						if (string.Compare(strA, RecordTableEnumerator.b("娻弽ⰿㅁ⅃", a_), StringComparison.CurrentCultureIgnoreCase) == 0)
						{
							num2 = 30;
							continue;
						}
						if (true)
						{
						}
						num2 = 26;
						continue;
					case 51:
						if (this.ᜁ == '!')
						{
							num2 = 46;
							continue;
						}
						num2 = 0;
						continue;
					case 52:
						if (this.ᜁ == '[')
						{
							num2 = 21;
							continue;
						}
						num2 = 11;
						continue;
					case 53:
						if (this.ᜁ != c)
						{
							num2 = 43;
							continue;
						}
						goto IL_2E6;
					case 54:
						num2 = 55;
						continue;
					case 55:
						if (this.ᜁ != '$')
						{
							num2 = 32;
							continue;
						}
						goto IL_4DE;
					case 56:
						if (flag2)
						{
							num2 = 64;
							continue;
						}
						goto IL_505;
					case 57:
						if (this.ᜁ != ']')
						{
							num2 = 24;
							continue;
						}
						goto IL_4DE;
					case 58:
						goto IL_5CB;
					case 59:
						goto IL_505;
					case 60:
						num2 = 57;
						continue;
					case 61:
						if (this.ᜁ < '\u0080')
						{
							num2 = 41;
							continue;
						}
						goto IL_4DE;
					case 62:
						if (this.ᜉ.ToString().EndsWith(RecordTableEnumerator.b("ᴻᴽሿ݁Ƀ杅", a_)))
						{
							num2 = 49;
							continue;
						}
						num2 = 34;
						continue;
					case 63:
						return;
					case 64:
						this.ᜀ(':');
						num2 = 48;
						continue;
					}
					break;
					IL_207:
					num2 = 59;
					continue;
					IL_242:
					num2 = 7;
					continue;
					IL_2E6:
					this.ᜊ();
					num2 = 15;
					continue;
					IL_3B9:
					this.ᜊ();
					this.ᜉ.Append(this.ᜁ);
					num2 = 53;
					continue;
					IL_4DE:
					num2 = 51;
					continue;
					IL_505:
					this.ᜉ.Append(this.ᜁ);
					c = '\u0001';
					num2 = 52;
					continue;
					IL_543:
					num2 = 44;
					continue;
					IL_5F1:
					strA = this.ᜉ.ToString();
					num2 = 27;
				}
			}
			IL_176:
			this.ᜆ = FormulaToken.Identifier3D;
			return;
			IL_1AE:
			this.ᜆ = FormulaToken.ValueFalse;
			return;
			IL_200:
			this.ᜆ = FormulaToken.tError;
			return;
			IL_451:
			this.ᜊ();
			this.ᜆ = FormulaToken.DDELink;
			return;
			IL_5CB:
			this.ᜆ = FormulaToken.ValueTrue;
			return;
			IL_6AA:
			this.ᜆ = FormulaToken.tFunction1;
			return;
		}
	}

	// Token: 0x0600415F RID: 16735 RVA: 0x0024B16C File Offset: 0x0024A16C
	private void ᜄ()
	{
		if (true)
		{
		}
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (this.ᜁ != ' ')
				{
					num = 1;
					continue;
				}
				this.ᜉ.Append(this.ᜁ);
				this.ᜊ();
				num = 3;
				continue;
			case 1:
				goto IL_44;
			case 3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_44;
				default:
					if (false)
					{
					}
					break;
				}
				break;
			}
			IL_2A:
			num = 0;
			continue;
			goto IL_2A;
		}
		IL_44:
		this.ᜆ = FormulaToken.Space;
	}

	// Token: 0x06004160 RID: 16736 RVA: 0x0024B214 File Offset: 0x0024A214
	private void ᜀ(bool A_0)
	{
		int a_ = 0;
		for (;;)
		{
			char c = '\0';
			int num = 12;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (true)
					{
					}
					num = 17;
					continue;
				case 1:
					if (this.ᜃ())
					{
						num = 6;
						continue;
					}
					goto IL_80;
				case 2:
					c = this.ᜁ;
					this.ᜊ();
					num = 10;
					continue;
				case 3:
					this.ᜉ.Append(this.ᜁ);
					this.ᜊ();
					num = 15;
					continue;
				case 4:
					num = 1;
					continue;
				case 5:
					num = 9;
					continue;
				case 6:
					this.ᜊ();
					num = 13;
					continue;
				case 7:
					goto IL_19C;
				case 8:
					goto IL_D8;
				case 9:
					if (A_0)
					{
						num = 16;
						continue;
					}
					return;
				case 10:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_D8;
					default:
						if (false)
						{
						}
						goto IL_13F;
					}
					break;
				case 11:
					goto IL_13F;
				case 12:
					if (A_0)
					{
						num = 2;
						continue;
					}
					goto IL_13F;
				case 13:
					if (this.ᜁ == c)
					{
						num = 3;
						continue;
					}
					return;
				case 14:
					if (this.ᜁ == '\u0001')
					{
						num = 5;
						continue;
					}
					num = 8;
					continue;
				case 15:
					goto IL_13F;
				case 16:
					this.ᜀ(RecordTableEnumerator.b("缵嘷夹医匽〿⹁⅃㉅ⵇ橉㽋㩍≏㭑㩓ㅕ瑗穙ㅛ㝝፟ᅡൣࡥཧ䩩", a_) + c + RecordTableEnumerator.b("വᠷ椹䠻䰽⤿ⱁ⍃晅㭇㹉ⵋ㱍⑏㝑こ", a_), null);
					num = 7;
					continue;
				case 17:
					if (this.ᜁ == c)
					{
						num = 4;
						continue;
					}
					goto IL_80;
				}
				break;
				IL_80:
				this.ᜉ.Append(this.ᜁ);
				this.ᜊ();
				num = 11;
				continue;
				IL_D8:
				if (A_0)
				{
					num = 0;
					continue;
				}
				goto IL_80;
				IL_13F:
				num = 14;
			}
		}
		IL_19C:;
	}

	// Token: 0x06004161 RID: 16737 RVA: 0x0024B43C File Offset: 0x0024A43C
	private bool ᜃ()
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
			if (this.\u170D == -1)
			{
				return true;
			}
			break;
		}
		return this.ᜃ == this.\u170D + 1;
	}

	// Token: 0x06004162 RID: 16738 RVA: 0x0024B494 File Offset: 0x0024A494
	private void ᜂ()
	{
		switch (0)
		{
		default:
		{
			ICollection<string> keys = FormulaUtil.ErrorNameToCode.Keys;
			string text = null;
			IEnumerator<string> enumerator = keys.GetEnumerator();
			try
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						goto IL_C8;
					case 2:
					{
						if (!enumerator.MoveNext())
						{
							num = 1;
							continue;
						}
						string text2 = enumerator.Current;
						int length = text2.Length;
						num = 3;
						continue;
					}
					case 3:
					{
						string text2;
						int length;
						if (string.Compare(text2, 0, this.ᜄ, this.ᜃ - 1, length, StringComparison.CurrentCultureIgnoreCase) == 0)
						{
							num = 6;
							continue;
						}
						break;
					}
					case 4:
						goto IL_D4;
					case 5:
						goto IL_C8;
					case 6:
					{
						string text2;
						text = text2;
						num = 5;
						continue;
					}
					}
					IL_9B:
					num = 2;
					continue;
					goto IL_9B;
					IL_C8:
					num = 4;
				}
				IL_D4:;
			}
			finally
			{
				int num = 0;
				for (;;)
				{
					switch (num)
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
							break;
						}
						break;
					case 1:
						enumerator.Dispose();
						num = 2;
						continue;
					case 2:
						goto IL_12F;
					}
					if (enumerator == null)
					{
						break;
					}
					num = 1;
				}
				IL_12F:;
			}
			if (true)
			{
			}
			this.ᜉ.Length = 0;
			this.ᜉ.Append(text);
			this.ᜃ += text.Length - 1;
			this.ᜆ = FormulaToken.tError;
			this.ᜊ();
			return;
		}
		}
	}

	// Token: 0x06004163 RID: 16739 RVA: 0x0024B628 File Offset: 0x0024A628
	private void ᜁ()
	{
		int a_ = 1;
		for (;;)
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_11D:
				num = 6;
				break;
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᜉ.Length = 0;
				num = 2;
				break;
			}
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 10;
					continue;
				case 1:
					goto IL_12A;
				case 2:
					goto IL_9C;
				case 3:
					goto IL_F1;
				case 4:
					this.ᜀ(RecordTableEnumerator.b("琶嘸为儼嬾⽀摂ㅄ杆⽈≊⍌⭎煐㙒㭔㍖祘㑚㭜罞`ᅢᝤ٦ၨ", a_), null);
					num = 3;
					continue;
				case 5:
					if (this.ᜁ == '\u0001')
					{
						num = 4;
						continue;
					}
					goto IL_180;
				case 6:
					this.ᜀ();
					num = 9;
					continue;
				case 7:
					if (this.ᜁ != '}')
					{
						num = 0;
						continue;
					}
					goto IL_12A;
				case 8:
					goto IL_9C;
				case 9:
					goto IL_9C;
				case 10:
					if (this.ᜁ == '\u0001')
					{
						num = 1;
						continue;
					}
					this.ᜉ.Append(this.ᜁ);
					num = 11;
					continue;
				case 11:
					if (this.ᜁ == '"')
					{
						goto IL_11D;
					}
					this.ᜊ();
					num = 8;
					continue;
				}
				break;
				IL_9C:
				num = 7;
				continue;
				IL_12A:
				this.ᜉ.Append(this.ᜁ);
				num = 5;
			}
		}
		IL_F1:
		IL_180:
		this.ᜊ();
	}

	// Token: 0x06004164 RID: 16740 RVA: 0x0024B7BC File Offset: 0x0024A7BC
	private void ᜀ()
	{
		int a_ = 0;
		for (;;)
		{
			IL_41:
			char c;
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_11C:
				if (this.ᜁ != c)
				{
					goto IL_95;
				}
				num = 5;
				break;
			default:
				if (false)
				{
				}
				c = this.ᜁ;
				this.ᜊ();
				num = 10;
				break;
			}
			for (;;)
			{
				IL_0B:
				switch (num)
				{
				case 0:
					if (this.ᜁ == '\u0001')
					{
						num = 6;
						continue;
					}
					return;
				case 1:
					goto IL_179;
				case 2:
					if (this.ᜁ == '\u0001')
					{
						num = 11;
						continue;
					}
					this.ᜉ.Append(this.ᜁ);
					num = 3;
					continue;
				case 3:
					goto IL_11C;
				case 4:
					goto IL_74;
				case 5:
					this.ᜊ();
					num = 8;
					continue;
				case 6:
					this.ᜀ(RecordTableEnumerator.b("电夷吹ᬻ䨽怿⑁ⵃ⡅ⱇ橉⥋⁍㑏牑㭓さ硗⹙㑛㭝䁟ᅡၣᑥŧѩ୫", a_), null);
					num = 7;
					continue;
				case 7:
					goto IL_F2;
				case 8:
					if (this.ᜁ == c)
					{
						num = 9;
						continue;
					}
					goto IL_138;
				case 9:
					this.ᜉ.Append(this.ᜁ);
					num = 1;
					continue;
				case 10:
					goto IL_74;
				case 11:
					goto IL_138;
				}
				goto IL_41;
				IL_74:
				num = 2;
				continue;
				IL_138:
				num = 0;
			}
			IL_179:
			IL_95:
			this.ᜊ();
			num = 4;
			goto IL_0B;
		}
		IL_F2:
		if (true)
		{
		}
	}

	// Token: 0x06004165 RID: 16741 RVA: 0x0024B954 File Offset: 0x0024A954
	public void ᜀ(string A_0, Exception A_1)
	{
		int a_ = 3;
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_103;
			case 1:
				if (true)
				{
				}
				A_0 = A_0 + RecordTableEnumerator.b("᜸ᬺ", a_) + A_1.Message;
				num = 0;
				continue;
			case 2:
				goto IL_74;
			case 4:
				if (A_1 == null)
				{
					goto IL_105;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_9A;
				default:
					if (false)
					{
					}
					num = 5;
					continue;
				}
				break;
			case 5:
				A_0 = A_0 + RecordTableEnumerator.b("᜸ᬺ", a_) + A_1.Message;
				num = 2;
				continue;
			}
			if (A_1 is spr\u2313)
			{
				num = 1;
				continue;
			}
			A_0 = A_0 + RecordTableEnumerator.b("ᤸᬺ尼䬾慀㍂⩄㑆⁈㽊⑌⁎㽐獒", a_) + this.ᜅ;
			IL_9A:
			num = 4;
		}
		IL_74:
		IL_103:
		IL_105:
		throw new spr\u2313(A_0, this.ᜄ, this.ᜃ, A_1);
	}

	// Token: 0x06004166 RID: 16742 RVA: 0x0024BA7C File Offset: 0x0024AA7C
	public void ᜀ(string A_0)
	{
		int a_ = 8;
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (true)
				{
				}
				break;
			case 1:
				goto IL_42;
			case 2:
				if (A_0.Length == 0)
				{
					num = 1;
					continue;
				}
				goto IL_93;
			case 3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_38;
				default:
					if (false)
					{
					}
					num = 2;
					continue;
				}
				break;
			case 4:
				goto IL_5B;
			}
			goto IL_35;
			IL_38:
			num = 3;
			continue;
			IL_35:
			if (A_0 != null)
			{
				goto IL_38;
			}
			IL_42:
			A_0 = string.Empty;
			num = 4;
		}
		IL_5B:
		IL_93:
		string a_2 = string.Format(RecordTableEnumerator.b("䔽瀿㽁ᅃ⡅ⵇ㉉㱋⭍㍏♑ㅓ㉕硗⹙㍛㕝՟ౡ䑣ብᅧᩩ५呭偯ॱ䕳୵呷婹ཻ੽ꢇﲉ꺓뚕ꢙ", a_), A_0, this.ᜆ, this.ᜉ);
		this.ᜀ(a_2, null);
	}

	// Token: 0x06004167 RID: 16743 RVA: 0x0024BB4C File Offset: 0x0024AB4C
	public string ᜎ()
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
		return this.ᜉ.ToString();
	}

	// Token: 0x06004168 RID: 16744 RVA: 0x0024BB94 File Offset: 0x0024AB94
	public char ᜏ()
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
		return this.ᜋ;
	}

	// Token: 0x06004169 RID: 16745 RVA: 0x0024BBD8 File Offset: 0x0024ABD8
	public void ᜁ(char A_0)
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
		this.ᜋ = A_0;
	}

	// Token: 0x0600416A RID: 16746 RVA: 0x0024BC1C File Offset: 0x0024AC1C
	public NumberFormatInfo \u170D()
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
		return this.ᜌ;
	}

	// Token: 0x0600416B RID: 16747 RVA: 0x0024BC60 File Offset: 0x0024AC60
	public void ᜀ(NumberFormatInfo A_0)
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
		this.ᜌ = A_0;
	}

	// Token: 0x04001D0B RID: 7435
	private const char ᜀ = '\u0001';

	// Token: 0x04001D0C RID: 7436
	private char ᜁ;

	// Token: 0x04001D0D RID: 7437
	private int ᜂ;

	// Token: 0x04001D0E RID: 7438
	private int ᜃ;

	// Token: 0x04001D0F RID: 7439
	private string ᜄ;

	// Token: 0x04001D10 RID: 7440
	private int ᜅ;

	// Token: 0x04001D11 RID: 7441
	public FormulaToken ᜆ;

	// Token: 0x04001D12 RID: 7442
	public FormulaToken ᜇ;

	// Token: 0x04001D13 RID: 7443
	private int ᜈ;

	// Token: 0x04001D14 RID: 7444
	private StringBuilder ᜉ;

	// Token: 0x04001D15 RID: 7445
	private XlsWorkbook ᜊ;

	// Token: 0x04001D16 RID: 7446
	private char ᜋ;

	// Token: 0x04001D17 RID: 7447
	private NumberFormatInfo ᜌ;

	// Token: 0x04001D18 RID: 7448
	private int \u170D;
}
