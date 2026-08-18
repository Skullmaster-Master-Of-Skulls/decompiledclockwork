using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Threading;
using Spire.Xls;
using Spire.Xls.Core;
using Spire.Xls.Core.Parser.Biff_Records.Formula;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x0200024A RID: 586
internal class spr\u236F
{
	// Token: 0x06002357 RID: 9047 RVA: 0x00147544 File Offset: 0x00146544
	public spr\u236F(XlsWorkbook A_0)
	{
		int a_ = 4;
		this.ᜆ = new List<Ptg>();
		this.ᜇ = new Stack<sprᯡ>();
		base..ctor();
		if (A_0 == null)
		{
			throw new ArgumentNullException(RecordTableEnumerator.b("堹医儽⬿", a_));
		}
		this.ᜈ = A_0;
		this.ᜅ = new spr\u2291(A_0);
	}

	// Token: 0x06002358 RID: 9048 RVA: 0x001475A4 File Offset: 0x001465A4
	public void ᜀ(char A_0, char A_1)
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
		this.ᜅ.ᜁ(A_0);
	}

	// Token: 0x06002359 RID: 9049 RVA: 0x001475EC File Offset: 0x001465EC
	internal void ᜁ(string A_0, Dictionary<Type, sprᨳ> A_1, int A_2, ParseFormulaOptions A_3, ParseParameters A_4)
	{
		int a_ = 14;
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_4C;
			case 2:
			{
				if (A_0.Length == 0)
				{
					num = 4;
					continue;
				}
				CultureInfo cultureInfo = null;
				num = 8;
				continue;
			}
			case 3:
				goto IL_10B;
			case 4:
				goto IL_DE;
			case 5:
				goto IL_186;
			case 6:
				goto IL_10B;
			case 7:
				if (this.ᜇ.Count <= 0)
				{
					return;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				default:
					if (false)
					{
					}
					num = 5;
					continue;
				}
				break;
			case 8:
			{
				CultureInfo cultureInfo;
				if ((cultureInfo = this.ᜈ.AppImplementation.\u171F()) != null)
				{
					num = 9;
					continue;
				}
				this.ᜅ.ᜀ(Thread.CurrentThread.CurrentCulture.NumberFormat);
				num = 6;
				continue;
			}
			case 9:
			{
				CultureInfo cultureInfo;
				this.ᜅ.ᜀ(cultureInfo.NumberFormat);
				if (true)
				{
				}
				num = 3;
				continue;
			}
			}
			if (A_0 == null)
			{
				num = 0;
				continue;
			}
			A_0 = A_0.TrimEnd(new char[]
			{
				' '
			});
			num = 2;
			continue;
			IL_10B:
			this.ᜆ.Clear();
			this.ᜇ.Clear();
			this.ᜅ.ᜁ(A_0);
			this.ᜅ.ᜋ();
			this.ᜀ(Priority.None, A_1, A_2, A_3, A_4);
			num = 7;
		}
		IL_4C:
		throw new ArgumentNullException(RecordTableEnumerator.b("≃⥅㩇❉㥋≍ㅏ", a_));
		IL_DE:
		throw new ArgumentException(RecordTableEnumerator.b("≃⥅㩇❉㥋≍ㅏ牑祓癕⭗⹙⹛㝝๟ա䑣ե१ѩɫŭѯ剱ᙳ፵塷όᅻ๽ﮁꪃ", a_));
		IL_186:
		throw new NotSupportedException(RecordTableEnumerator.b("ᅃ⡅ⵇ㉉㱋⭍㍏♑ㅓ㉕硗⥙ⱛ㽝͟ݡᝣ䡥", a_));
	}

	// Token: 0x0600235A RID: 9050 RVA: 0x001477CC File Offset: 0x001467CC
	private Ptg ᜀ(Priority A_0, Dictionary<Type, sprᨳ> A_1, int A_2, ParseFormulaOptions A_3, ParseParameters A_4)
	{
		int a_ = 15;
		Ptg ptg;
		for (;;)
		{
			IL_09:
			switch (0)
			{
			default:
				for (;;)
				{
					ptg = this.ᜀ(A_0, A_1, A_2, ref A_3, A_4);
					this.ᜆ.Add(ptg);
					sprᯡ sprᯡ = null;
					int num = 18;
					for (;;)
					{
						FormulaToken formulaToken;
						FormulaToken formulaToken2;
						switch (num)
						{
						case 0:
							goto IL_6A3;
						case 1:
							if (true)
							{
							}
							if (formulaToken != FormulaToken.Space)
							{
								num = 3;
								continue;
							}
							sprᯡ = this.ᜂ(A_1, A_2, A_3, A_4);
							num = 11;
							continue;
						case 2:
							num = 32;
							continue;
						case 3:
							num = 8;
							continue;
						case 4:
							goto IL_6A3;
						case 5:
							this.ᜇ.Push(sprᯡ);
							sprᯡ = null;
							num = 7;
							continue;
						case 6:
							if ((A_3 & (ParseFormulaOptions.ParseOperand | ParseFormulaOptions.ParseComplexOperand)) == ParseFormulaOptions.None)
							{
								num = 34;
								continue;
							}
							return ptg;
						case 7:
							return ptg;
						case 8:
							goto IL_214;
						case 9:
							if (A_0 >= Priority.CellRange)
							{
								num = 39;
								continue;
							}
							this.ᜀ(ref A_3);
							this.ᜅ.ᜋ();
							this.ᜀ(Priority.CellRange, A_1, A_2, A_3, A_4);
							ptg = this.ᜀ(FormulaToken.tCellRange, ref sprᯡ);
							num = 44;
							continue;
						case 10:
							return ptg;
						case 11:
							goto IL_4AD;
						case 12:
							if (sprᯡ != null)
							{
								num = 5;
								continue;
							}
							return ptg;
						case 13:
							return ptg;
						case 14:
							this.ᜅ.ᜀ(RecordTableEnumerator.b("ୄ⡆楈๊㕌㽎⍐㙒♔⑖じ㑚㍜罞ݠౢၤ०൨", a_));
							num = 16;
							continue;
						case 15:
							return ptg;
						case 16:
							goto IL_4AD;
						case 17:
							this.ᜇ.Push(sprᯡ);
							sprᯡ = null;
							num = 10;
							continue;
						case 18:
							if (ptg == null)
							{
								num = 14;
								continue;
							}
							goto IL_4AD;
						case 19:
							goto IL_6A3;
						case 20:
							switch (formulaToken)
							{
							case FormulaToken.tAdd:
								num = 21;
								continue;
							case FormulaToken.tSub:
								num = 23;
								continue;
							case FormulaToken.tMul:
							case FormulaToken.tDiv:
								num = 26;
								continue;
							case FormulaToken.tPower:
								num = 27;
								continue;
							case FormulaToken.tConcat:
								num = 35;
								continue;
							case FormulaToken.tLessThan:
							case FormulaToken.tLessEqual:
							case FormulaToken.tEqual:
							case FormulaToken.tGreaterEqual:
							case FormulaToken.tGreater:
							case FormulaToken.tNotEqual:
								num = 29;
								continue;
							case FormulaToken.tCellRangeIntersection:
							case FormulaToken.tCellRangeList:
							case FormulaToken.tUnaryPlus:
							case FormulaToken.tUnaryMinus:
								goto IL_214;
							case FormulaToken.tCellRange:
								num = 9;
								continue;
							case FormulaToken.tPercent:
								this.ᜅ.ᜋ();
								ptg = new spr\u23FA(RecordTableEnumerator.b("恄", a_));
								this.ᜆ.Add(ptg);
								num = 37;
								continue;
							default:
								num = 2;
								continue;
							}
							break;
						case 21:
							if (A_0 >= Priority.PlusMinus)
							{
								num = 31;
								continue;
							}
							this.ᜅ.ᜋ();
							this.ᜀ(Priority.PlusMinus, A_1, A_2, A_3, A_4);
							ptg = this.ᜀ(formulaToken2, ref sprᯡ);
							num = 4;
							continue;
						case 22:
							goto IL_4AD;
						case 23:
							if (A_0 >= Priority.PlusMinus)
							{
								num = 42;
								continue;
							}
							this.ᜅ.ᜋ();
							this.ᜀ(Priority.PlusMinus, A_1, A_2, A_3, A_4);
							ptg = this.ᜀ(formulaToken2, ref sprᯡ);
							num = 0;
							continue;
						case 24:
							if (sprᯡ != null)
							{
								num = 45;
								continue;
							}
							return ptg;
						case 25:
							goto IL_6A3;
						case 26:
							if (A_0 >= Priority.MulDiv)
							{
								num = 40;
								continue;
							}
							this.ᜀ(ref A_3);
							this.ᜅ.ᜋ();
							this.ᜀ(Priority.MulDiv, A_1, A_2, A_3, A_4);
							ptg = this.ᜀ(formulaToken2, ref sprᯡ);
							num = 30;
							continue;
						case 27:
							if (A_0 >= Priority.Power)
							{
								num = 46;
								continue;
							}
							this.ᜅ.ᜋ();
							this.ᜀ(Priority.Power, A_1, A_2, A_3, A_4);
							ptg = this.ᜀ(FormulaToken.tPower, ref sprᯡ);
							num = 36;
							continue;
						case 28:
							goto IL_6A3;
						case 29:
							if (A_0 >= Priority.Equality)
							{
								num = 15;
								continue;
							}
							formulaToken2 = this.ᜅ.ᜆ;
							this.ᜅ.ᜋ();
							this.ᜀ(Priority.Equality, A_1, A_2, A_3, A_4);
							ptg = this.ᜀ(formulaToken2, ref sprᯡ);
							num = 19;
							continue;
						case 30:
							goto IL_6A3;
						case 31:
							num = 41;
							continue;
						case 32:
							switch (formulaToken)
							{
							case FormulaToken.EndOfFormula:
								return ptg;
							case FormulaToken.CloseParenthesis:
								num = 24;
								continue;
							case FormulaToken.Comma:
								num = 6;
								continue;
							default:
								num = 38;
								continue;
							}
							break;
						case 33:
							goto IL_363;
						case 34:
							this.ᜅ.ᜋ();
							this.ᜀ(Priority.None, A_1, A_2, A_3, A_4);
							ptg = new spr\u1A9E(this.ᜅ.ᜏ().ToString());
							this.ᜆ.Add(ptg);
							num = 25;
							continue;
						case 35:
							if (A_0 >= Priority.Concat)
							{
								num = 13;
								continue;
							}
							this.ᜅ.ᜋ();
							this.ᜀ(Priority.Concat, A_1, A_2, A_3, A_4);
							ptg = this.ᜀ(FormulaToken.tConcat, ref sprᯡ);
							num = 43;
							continue;
						case 36:
							goto IL_6A3;
						case 37:
							goto IL_6A3;
						case 38:
							num = 1;
							continue;
						case 39:
							return ptg;
						case 40:
							return ptg;
						case 41:
							if (sprᯡ != null)
							{
								num = 17;
								continue;
							}
							return ptg;
						case 42:
							num = 12;
							continue;
						case 43:
							goto IL_6A3;
						case 44:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_09;
							default:
								if (false)
								{
								}
								goto IL_6A3;
							}
							break;
						case 45:
							this.ᜇ.Push(sprᯡ);
							sprᯡ = null;
							num = 33;
							continue;
						case 46:
							return ptg;
						}
						break;
						IL_214:
						this.ᜅ.ᜀ(RecordTableEnumerator.b("၄⥆ⱈ㍊㵌⩎㉐❒ご㍖祘⽚㉜㑞Ѡൢ䭤", a_));
						num = 28;
						continue;
						IL_4AD:
						formulaToken2 = this.ᜅ.ᜆ;
						formulaToken = formulaToken2;
						num = 20;
						continue;
						IL_6A3:
						sprᯡ = null;
						num = 22;
					}
				}
				break;
			}
		}
		return ptg;
		IL_363:
		return ptg;
	}

	// Token: 0x0600235B RID: 9051 RVA: 0x00147E90 File Offset: 0x00146E90
	private Ptg ᜀ(FormulaToken A_0, ref sprᯡ A_1)
	{
		for (;;)
		{
			IL_00:
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					this.ᜆ.Add(this.ᜇ.Pop());
					num = 4;
					continue;
				case 1:
					if (true)
					{
					}
					this.ᜆ.Add(A_1);
					A_1 = null;
					num = 2;
					continue;
				case 2:
					goto IL_C2;
				case 4:
					goto IL_70;
				case 5:
					if (this.ᜇ.Count > 0)
					{
						num = 0;
						continue;
					}
					goto IL_C4;
				}
				if (A_1 != null)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_00;
					default:
						if (false)
						{
						}
						num = 1;
						break;
					}
				}
				else
				{
					num = 5;
				}
			}
		}
		IL_70:
		IL_C2:
		IL_C4:
		Ptg ptg = FormulaUtil.ᜀ(A_0);
		this.ᜆ.Add(ptg);
		return ptg;
	}

	// Token: 0x0600235C RID: 9052 RVA: 0x00147F78 File Offset: 0x00146F78
	private sprᯡ ᜂ(Dictionary<Type, sprᨳ> A_0, int A_1, ParseFormulaOptions A_2, ParseParameters A_3)
	{
		switch (0)
		{
		default:
		{
			sprᯡ sprᯡ;
			for (;;)
			{
				for (;;)
				{
					string text = this.ᜅ.ᜎ();
					int length = text.Length;
					int num = 0;
					Ptg ptg = null;
					bool flag = true;
					FormulaToken formulaToken = this.ᜅ.ᜇ;
					int num2 = 1;
					for (;;)
					{
						FormulaToken formulaToken2;
						switch (num2)
						{
						case 0:
							goto IL_A5;
						case 1:
							if (formulaToken != FormulaToken.CloseParenthesis)
							{
								num2 = 8;
								continue;
							}
							goto IL_EC;
						case 2:
							goto IL_A5;
						case 3:
							if (ptg != null)
							{
								num2 = 13;
								continue;
							}
							return sprᯡ;
						case 4:
							if (flag)
							{
								num2 = 5;
								continue;
							}
							goto IL_A5;
						case 5:
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
								this.ᜅ.ᜋ();
								num2 = 0;
								continue;
							}
							break;
						case 6:
							num2 = 11;
							continue;
						case 7:
							if (formulaToken == FormulaToken.Identifier)
							{
								num2 = 12;
								continue;
							}
							goto IL_C9;
						case 8:
							num2 = 7;
							continue;
						case 9:
							return sprᯡ;
						case 10:
							if (num > 0)
							{
								num2 = 14;
								continue;
							}
							return sprᯡ;
						case 11:
							if (formulaToken2 == FormulaToken.tParentheses)
							{
								num2 = 15;
								continue;
							}
							goto IL_C9;
						case 12:
							goto IL_EC;
						case 13:
							this.ᜆ.Insert(this.ᜆ.Count - 1, sprᯡ);
							sprᯡ = null;
							num2 = 9;
							continue;
						case 14:
							sprᯡ = this.ᜀ(num);
							num2 = 3;
							continue;
						case 15:
							goto IL_1ED;
						case 16:
							if (formulaToken2 != FormulaToken.Identifier)
							{
								num2 = 6;
								continue;
							}
							goto IL_1ED;
						}
						break;
						IL_A5:
						sprᯡ = null;
						num2 = 10;
						continue;
						IL_C9:
						num = length;
						num2 = 4;
						continue;
						IL_EC:
						this.ᜅ.ᜋ();
						flag = false;
						formulaToken2 = this.ᜅ.ᜆ;
						num2 = 16;
						continue;
						IL_1ED:
						num = length - 1;
						this.ᜀ(Priority.None, A_0, A_1, A_2, A_3);
						ptg = FormulaUtil.ᜀ(FormulaToken.tCellRangeIntersection);
						this.ᜆ.Add(ptg);
						num2 = 2;
					}
				}
			}
			return sprᯡ;
		}
		}
	}

	// Token: 0x0600235D RID: 9053 RVA: 0x001481D8 File Offset: 0x001471D8
	private sprᯡ ᜀ(int A_0)
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
		sprᯡ sprᯡ = (sprᯡ)FormulaUtil.ᜀ(FormulaToken.tAttr, 64, 256);
		sprᯡ.ᜀ(A_0);
		return sprᯡ;
	}

	// Token: 0x0600235E RID: 9054 RVA: 0x00148230 File Offset: 0x00147230
	private Ptg ᜀ(Priority A_0, Dictionary<Type, sprᨳ> A_1, int A_2, ref ParseFormulaOptions A_3, ParseParameters A_4)
	{
		int a_ = 9;
		switch (0)
		{
		default:
		{
			Ptg result;
			for (;;)
			{
				result = null;
				sprᯡ sprᯡ = null;
				CultureInfo cultureInfo = this.ᜈ.AppImplementation.\u171F();
				int num = 6;
				for (;;)
				{
					bool flag;
					FormulaToken formulaToken;
					switch (num)
					{
					case 0:
						num = 80;
						continue;
					case 1:
						goto IL_BA1;
					case 2:
						this.ᜅ.ᜆ = FormulaToken.tError;
						num = 3;
						continue;
					case 3:
						goto IL_34A;
					case 4:
						goto IL_B5B;
					case 5:
						if (!flag)
						{
							num = 62;
							continue;
						}
						goto IL_545;
					case 6:
						goto IL_545;
					case 7:
						num = 24;
						continue;
					case 8:
						num = 9;
						continue;
					case 9:
						if (this.ᜇ.Count > 0)
						{
							num = 22;
							continue;
						}
						goto IL_66D;
					case 10:
						goto IL_B5B;
					case 11:
						if (this.ᜇ.Count > 0)
						{
							num = 19;
							continue;
						}
						goto IL_805;
					case 12:
						goto IL_BA1;
					case 13:
						if (sprᯡ == null)
						{
							num = 0;
							continue;
						}
						goto IL_CC1;
					case 14:
						if (this.ᜇ.Count > 0)
						{
							num = 59;
							continue;
						}
						goto IL_932;
					case 15:
						this.ᜆ.Add(this.ᜇ.Pop());
						num = 67;
						continue;
					case 16:
						switch (formulaToken)
						{
						case FormulaToken.tAdd:
							this.ᜅ.ᜋ();
							num = 61;
							continue;
						case FormulaToken.tSub:
							this.ᜅ.ᜋ();
							num = 28;
							continue;
						default:
							num = 79;
							continue;
						}
						break;
					case 17:
						goto IL_B5B;
					case 18:
						num = 26;
						continue;
					case 19:
						this.ᜆ.Add(this.ᜇ.Pop());
						num = 40;
						continue;
					case 20:
						goto IL_8DF;
					case 21:
						this.ᜆ.RemoveAt(this.ᜆ.Count - 1);
						num = 20;
						continue;
					case 22:
						this.ᜆ.Add(this.ᜇ.Pop());
						num = 49;
						continue;
					case 23:
						goto IL_B5B;
					case 24:
						if (this.ᜇ.Count > 0)
						{
							num = 15;
							continue;
						}
						goto IL_8C6;
					case 25:
						this.ᜆ.Add(this.ᜇ.Pop());
						num = 29;
						continue;
					case 26:
						goto IL_B5B;
					case 27:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_811;
						default:
							if (false)
							{
							}
							if (this.ᜅ.ᜎ() == RecordTableEnumerator.b("瀾㝀♂㝄ㅆ⁈⹊㩌湎牐Œၔᅖ硘", a_))
							{
								num = 2;
								continue;
							}
							goto IL_34A;
						}
						break;
					case 28:
						if (sprᯡ == null)
						{
							num = 47;
							continue;
						}
						goto IL_BC3;
					case 29:
						goto IL_3E0;
					case 30:
						goto IL_49E;
					case 31:
						if (sprᯡ != null)
						{
							num = 21;
							continue;
						}
						goto IL_8DF;
					case 32:
						this.ᜆ.Add(this.ᜇ.Pop());
						num = 39;
						continue;
					case 33:
						if (sprᯡ != null)
						{
							num = 74;
							continue;
						}
						num = 91;
						continue;
					case 34:
						this.ᜆ.Add(this.ᜇ.Pop());
						num = 50;
						continue;
					case 35:
					{
						ushort a_2;
						if (ushort.TryParse(this.ᜅ.ᜎ(), NumberStyles.Float, (cultureInfo == null) ? this.ᜅ.\u170D() : cultureInfo.NumberFormat, out a_2))
						{
							num = 75;
							continue;
						}
						goto IL_A82;
					}
					case 36:
						goto IL_B5B;
					case 37:
						A_3 -= 8;
						A_3 |= ParseFormulaOptions.ParseComplexOperand;
						num = 89;
						continue;
					case 38:
						goto IL_B5B;
					case 39:
						goto IL_BC3;
					case 40:
						goto IL_805;
					case 41:
						goto IL_B5B;
					case 42:
						if (this.ᜇ.Count > 0)
						{
							num = 25;
							continue;
						}
						goto IL_3E0;
					case 43:
						sprᯡ = this.ᜇ.Pop();
						sprᯡ.ᜁ(true);
						this.ᜆ.Add(sprᯡ);
						num = 1;
						continue;
					case 44:
						this.ᜅ.ᜀ(RecordTableEnumerator.b("稾⽀❂敄㝆⡈㥊⡌ⅎ═㭒ご⑖じ⡚絜ㅞ๠ᝢ䕤Ŧ٨Ṫͬ୮", a_));
						num = 48;
						continue;
					case 45:
						goto IL_B5B;
					case 46:
						if (sprᯡ == null)
						{
							num = 78;
							continue;
						}
						goto IL_49E;
					case 47:
						num = 83;
						continue;
					case 48:
						goto IL_2F9;
					case 49:
						goto IL_66D;
					case 50:
						goto IL_A07;
					case 51:
						num = 42;
						continue;
					case 52:
						num = 63;
						continue;
					case 53:
						num = 11;
						continue;
					case 54:
						goto IL_B5B;
					case 55:
						goto IL_B5B;
					case 56:
						goto IL_B5B;
					case 57:
						if (this.ᜅ.ᜆ != FormulaToken.CloseParenthesis)
						{
							num = 44;
							continue;
						}
						goto IL_2F9;
					case 58:
						if (true)
						{
						}
						goto IL_B5B;
					case 59:
						this.ᜆ.Add(this.ᜇ.Pop());
						num = 87;
						continue;
					case 60:
						num = 14;
						continue;
					case 61:
						if (sprᯡ == null)
						{
							num = 53;
							continue;
						}
						goto IL_805;
					case 62:
						return result;
					case 63:
						switch (formulaToken)
						{
						case FormulaToken.Comma:
							num = 65;
							continue;
						case FormulaToken.OpenBracket:
						case FormulaToken.CloseBracket:
							goto IL_B5B;
						case FormulaToken.ValueTrue:
							result = new sprᥒ(true);
							this.ᜅ.ᜋ();
							num = 17;
							continue;
						case FormulaToken.ValueFalse:
							num = 46;
							continue;
						case FormulaToken.Space:
							sprᯡ = (sprᯡ)FormulaUtil.ᜀ(FormulaToken.tAttr, 64, 256);
							sprᯡ.ᜀ(this.ᜅ.ᜎ().Length);
							this.ᜆ.Add(sprᯡ);
							this.ᜅ.ᜋ();
							flag = true;
							num = 10;
							continue;
						case FormulaToken.Identifier:
						case FormulaToken.Identifier3D:
							goto IL_AF2;
						case FormulaToken.DDELink:
							result = this.ᜁ(A_1, A_2, A_3, A_4);
							num = 45;
							continue;
						default:
							num = 18;
							continue;
						}
						break;
					case 64:
						if ((A_3 & ParseFormulaOptions.ParseOperand) != ParseFormulaOptions.None)
						{
							num = 37;
							continue;
						}
						goto IL_37E;
					case 65:
						if (sprᯡ == null)
						{
							num = 7;
							continue;
						}
						goto IL_8C6;
					case 66:
						switch (formulaToken)
						{
						case FormulaToken.tParentheses:
							num = 31;
							continue;
						case FormulaToken.tMissingArgument:
						case FormulaToken.tExtended:
						case FormulaToken.tAttr:
						case FormulaToken.tSheet:
						case FormulaToken.tEndSheet:
						case FormulaToken.tBoolean:
							goto IL_B5B;
						case FormulaToken.tStringConstant:
							num = 92;
							continue;
						case FormulaToken.tError:
							num = 90;
							continue;
						case FormulaToken.tInteger:
							num = 35;
							continue;
						case FormulaToken.tNumber:
							goto IL_A82;
						case FormulaToken.tArray1:
							num = 85;
							continue;
						case FormulaToken.tFunction1:
							result = this.ᜀ(A_1, A_2, A_3, A_4);
							num = 23;
							continue;
						default:
							num = 52;
							continue;
						}
						break;
					case 67:
						goto IL_8C6;
					case 68:
						goto IL_B5B;
					case 69:
						goto IL_CC1;
					case 70:
						A_3 -= 8;
						A_3 |= ParseFormulaOptions.ParseComplexOperand;
						num = 77;
						continue;
					case 71:
						this.ᜆ.Add(this.ᜇ.Pop());
						num = 30;
						continue;
					case 72:
						goto IL_B5B;
					case 73:
						if (this.ᜇ.Count > 0)
						{
							num = 71;
							continue;
						}
						goto IL_49E;
					case 74:
						this.ᜆ.Add(sprᯡ);
						num = 12;
						continue;
					case 75:
					{
						ushort a_2;
						result = new sprℿ(a_2);
						num = 86;
						continue;
					}
					case 76:
						num = 82;
						continue;
					case 77:
						goto IL_6B4;
					case 78:
						num = 73;
						continue;
					case 79:
						num = 66;
						continue;
					case 80:
						if (this.ᜇ.Count > 0)
						{
							num = 88;
							continue;
						}
						goto IL_CC1;
					case 81:
						goto IL_B5B;
					case 82:
						if (this.ᜇ.Count > 0)
						{
							num = 34;
							continue;
						}
						goto IL_A07;
					case 83:
						if (this.ᜇ.Count > 0)
						{
							num = 32;
							continue;
						}
						goto IL_BC3;
					case 84:
						goto IL_811;
					case 85:
						if (sprᯡ == null)
						{
							num = 8;
							continue;
						}
						goto IL_66D;
					case 86:
						if (sprᯡ == null)
						{
							num = 51;
							continue;
						}
						goto IL_3E0;
					case 87:
						goto IL_932;
					case 88:
						this.ᜆ.Add(this.ᜇ.Pop());
						num = 69;
						continue;
					case 89:
						goto IL_37E;
					case 90:
						if (sprᯡ == null)
						{
							num = 60;
							continue;
						}
						goto IL_932;
					case 91:
						if (this.ᜇ.Count > 0)
						{
							num = 43;
							continue;
						}
						goto IL_BA1;
					case 92:
						if (sprᯡ == null)
						{
							num = 76;
							continue;
						}
						goto IL_A07;
					}
					break;
					IL_2F9:
					num = 33;
					continue;
					IL_34A:
					this.ᜅ.ᜋ();
					this.ᜀ(ref A_3);
					string a_3;
					result = this.ᜀ(a_3, A_1, A_2, A_3, A_4);
					num = 54;
					continue;
					IL_37E:
					result = this.ᜀ(Priority.UnaryMinus, A_1, A_2, A_3, A_4);
					result = new spr\u23FA(RecordTableEnumerator.b("ሾ", a_));
					num = 68;
					continue;
					IL_3E0:
					this.ᜅ.ᜋ();
					this.ᜀ(ref A_3);
					num = 56;
					continue;
					IL_49E:
					result = new sprᥒ(false);
					this.ᜅ.ᜋ();
					num = 58;
					continue;
					IL_545:
					flag = false;
					formulaToken = this.ᜅ.ᜆ;
					num = 16;
					continue;
					IL_66D:
					int a_4 = FormulaUtil.ᜀ(typeof(spr\u2372), 1, A_1, A_2, A_3);
					FormulaToken a_5 = spr\u2372.ᜀ(a_4);
					result = this.ᜀ(a_5, A_4);
					this.ᜅ.ᜋ();
					num = 55;
					continue;
					IL_6B4:
					result = this.ᜀ(Priority.UnaryMinus, A_1, A_2, A_3, A_4);
					result = new spr\u23FA(RecordTableEnumerator.b("ᐾ", a_));
					num = 4;
					continue;
					IL_811:
					if ((A_3 & ParseFormulaOptions.ParseOperand) != ParseFormulaOptions.None)
					{
						num = 70;
						continue;
					}
					goto IL_6B4;
					IL_805:
					num = 84;
					continue;
					IL_8C6:
					result = FormulaUtil.ᜁ(FormulaToken.tMissingArgument);
					num = 38;
					continue;
					IL_8DF:
					this.ᜅ.ᜋ();
					ParseFormulaOptions a_6 = A_3 & ~ParseFormulaOptions.ParseOperand;
					result = this.ᜀ(Priority.None, A_1, A_2, a_6, A_4);
					num = 57;
					continue;
					IL_932:
					result = this.ᜀ(A_1, A_2, A_3, A_4.Worksheet);
					this.ᜅ.ᜋ();
					num = 36;
					continue;
					IL_A07:
					result = new spr\u24A7(this.ᜅ.ᜎ());
					this.ᜅ.ᜋ();
					num = 81;
					continue;
					IL_AF2:
					a_3 = this.ᜅ.ᜎ();
					num = 27;
					continue;
					try
					{
						IL_A82:
						double a_7 = double.Parse(this.ᜅ.ᜎ(), NumberStyles.Float, (cultureInfo == null) ? this.ᜅ.\u170D() : cultureInfo.NumberFormat);
						result = new spr\u180B(a_7);
						goto IL_B7E;
					}
					catch (Exception a_8)
					{
						this.ᜅ.ᜀ(string.Format(RecordTableEnumerator.b("瘾⽀㕂⑄⭆⁈⽊浌ⅎ⑐㹒㝔㉖⭘筚♜潞ᱠ", a_), this.ᜅ.ᜎ()), a_8);
						goto IL_B7E;
					}
					goto IL_AF2;
					IL_B7E:
					num = 13;
					continue;
					IL_B5B:
					num = 5;
					continue;
					IL_BA1:
					result = new sprὪ();
					this.ᜅ.ᜋ();
					num = 72;
					continue;
					IL_BC3:
					num = 64;
					continue;
					IL_CC1:
					this.ᜅ.ᜋ();
					this.ᜀ(ref A_3);
					num = 41;
				}
			}
			return result;
		}
		}
	}

	// Token: 0x0600235F RID: 9055 RVA: 0x00148F5C File Offset: 0x00147F5C
	private void ᜀ(ref ParseFormulaOptions A_0)
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_A0:
			num = 6;
			break;
		default:
			if (false)
			{
			}
			num = 7;
			break;
		}
		FormulaToken formulaToken;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (formulaToken != FormulaToken.tCellRange)
				{
					num = 12;
					continue;
				}
				return;
			case 1:
				num = 0;
				continue;
			case 2:
				num = 4;
				continue;
			case 3:
				formulaToken = this.ᜅ.ᜆ;
				num = 8;
				continue;
			case 4:
				goto IL_95;
			case 5:
				if (formulaToken != FormulaToken.Space)
				{
					num = 1;
					continue;
				}
				return;
			case 6:
				num = 5;
				continue;
			case 8:
				if (formulaToken != FormulaToken.Comma)
				{
					if (true)
					{
					}
					num = 9;
					continue;
				}
				return;
			case 9:
				num = 10;
				continue;
			case 10:
				if (formulaToken != FormulaToken.EndOfFormula)
				{
					num = 2;
					continue;
				}
				return;
			case 11:
				return;
			case 12:
				A_0 -= 8;
				A_0 |= ParseFormulaOptions.ParseComplexOperand;
				num = 11;
				continue;
			}
			if ((A_0 & ParseFormulaOptions.ParseOperand) == ParseFormulaOptions.None)
			{
				return;
			}
			num = 3;
		}
		IL_95:
		if (formulaToken != FormulaToken.CloseParenthesis)
		{
			goto IL_A0;
		}
	}

	// Token: 0x06002360 RID: 9056 RVA: 0x001490B4 File Offset: 0x001480B4
	private Ptg ᜁ(Dictionary<Type, sprᨳ> A_0, int A_1, ParseFormulaOptions A_2, ParseParameters A_3)
	{
		int a_ = 10;
		if (A_3 == null)
		{
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_51;
			}
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("ℿぁ⍃㍅╇⽉≋㩍⍏", a_));
		}
		IL_51:
		string a_2 = this.ᜅ.ᜎ();
		this.ᜅ.ᜋ();
		string a_3 = this.ᜅ.ᜎ();
		this.ᜅ.ᜋ();
		string a_4 = this.ᜅ.ᜎ();
		this.ᜅ.ᜆ = FormulaToken.None;
		this.ᜅ.ᜋ();
		return this.ᜀ(a_2, a_3, a_4, A_0, A_1, A_2, A_3);
	}

	// Token: 0x06002361 RID: 9057 RVA: 0x00149174 File Offset: 0x00148174
	private Ptg ᜀ(string A_0, string A_1, string A_2, Dictionary<Type, sprᨳ> A_3, int A_4, ParseFormulaOptions A_5, ParseParameters A_6)
	{
		int a_ = 17;
		switch (0)
		{
		default:
		{
			int num;
			int num3;
			sprᭆ sprᭆ;
			for (;;)
			{
				string text = A_0 + RecordTableEnumerator.b("㭆", a_) + A_1;
				XlsWorkbook xlsWorkbook = (XlsWorkbook)A_6.Workbook;
				XlsExternBookCollection externWorkbooks = xlsWorkbook.ExternWorkbooks;
				XlsExternWorkbook xlsExternWorkbook = externWorkbooks[text];
				num = -1;
				int num2 = 6;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_136;
					case 1:
						goto IL_92;
					case 2:
						num = externWorkbooks.AddDDEFile(text);
						xlsExternWorkbook = externWorkbooks[num];
						goto IL_107;
					case 3:
						if (num3 < 0)
						{
							num2 = 5;
							continue;
						}
						goto IL_138;
					case 4:
						goto IL_92;
					case 5:
						num3 = sprᭆ.ᜃ(A_2);
						num2 = 0;
						continue;
					case 6:
						if (xlsExternWorkbook == null)
						{
							num2 = 2;
							continue;
						}
						num = xlsExternWorkbook.Index;
						num2 = 4;
						continue;
					}
					break;
					IL_92:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_107:
						if (true)
						{
						}
						num2 = 1;
						break;
					default:
						if (false)
						{
						}
						sprᭆ = xlsExternWorkbook.ExternNames;
						num3 = sprᭆ.ᜂ(A_2);
						num2 = 3;
						break;
					}
				}
			}
			IL_136:
			IL_138:
			sprἉ sprἉ = sprᭆ.ᜀ(num3);
			sprἉ.ᜄ().ᜁ(32738);
			int a_2 = FormulaUtil.ᜀ(typeof(spr\u2372), 1, A_3, A_4, A_5);
			FormulaToken a_3 = spr\u1B76.ᜀ(a_2);
			spr\u1B76 spr_u1B = (spr\u1B76)FormulaUtil.ᜁ(a_3);
			spr_u1B.ᜀ((ushort)(num3 + 1));
			spr_u1B.ᜁ((ushort)num);
			return spr_u1B;
		}
		}
	}

	// Token: 0x06002362 RID: 9058 RVA: 0x0014931C File Offset: 0x0014831C
	private Ptg ᜀ(string A_0, Dictionary<Type, sprᨳ> A_1, int A_2, ParseFormulaOptions A_3, ParseParameters A_4)
	{
		int a_ = 12;
		for (;;)
		{
			IL_09:
			switch (0)
			{
			default:
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_77;
					case 1:
					{
						Ptg result;
						int num2;
						if (!this.ᜀ(A_0, A_1, A_2, A_3, A_4, out result, num2))
						{
							num = 6;
							continue;
						}
						return result;
					}
					case 2:
						if (true)
						{
						}
						break;
					case 3:
					{
						Ptg result;
						return result;
					}
					case 4:
						goto IL_10E;
					case 5:
						goto IL_CA;
					case 6:
					{
						int num2;
						Ptg result = this.ᜀ(A_0, A_4, A_1, A_2, A_3, num2);
						num = 3;
						continue;
					}
					case 7:
						goto IL_113;
					case 8:
					{
						int num3 = A_0.LastIndexOf('!');
						num = 11;
						continue;
					}
					case 9:
						if (this.ᜅ.ᜇ == FormulaToken.Identifier3D)
						{
							num = 8;
							continue;
						}
						goto IL_113;
					case 10:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_09;
						default:
						{
							if (false)
							{
							}
							if (A_0.Length == 0)
							{
								num = 4;
								continue;
							}
							Ptg result = null;
							int num2 = -1;
							num = 9;
							continue;
						}
						}
						break;
					case 11:
					{
						int num3;
						if (num3 <= 0)
						{
							num = 5;
							continue;
						}
						string a_2 = A_0.Substring(0, num3);
						A_0 = A_0.Substring(num3 + 1);
						int num2 = this.ᜀ(a_2, A_4);
						num = 7;
						continue;
					}
					}
					if (A_0 == null)
					{
						num = 0;
						continue;
					}
					num = 10;
					continue;
					IL_113:
					num = 1;
				}
				break;
			}
			}
		}
		IL_77:
		throw new ArgumentNullException(RecordTableEnumerator.b("⭁⁃⍅♇㹉╋⡍㥏㝑♓", a_));
		IL_CA:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⭁⁃⍅♇㹉╋⡍㥏㝑♓", a_));
		IL_10E:
		throw new ArgumentException(RecordTableEnumerator.b("⭁⁃⍅♇㹉╋⡍㥏㝑♓癕畗穙⽛⩝቟ୡ੣ť䡧३൫mṯᵱs噵᩷ό屻᭽ﾅꚇ", a_));
	}

	// Token: 0x06002363 RID: 9059 RVA: 0x001494FC File Offset: 0x001484FC
	private int ᜀ(string A_0, ParseParameters A_1)
	{
		switch (0)
		{
		default:
		{
			int result;
			for (;;)
			{
				int length = A_0.Length;
				int num = 17;
				for (;;)
				{
					int num3;
					int num2;
					XlsExternWorkbook xlsExternWorkbook;
					string text;
					int num4;
					XlsWorkbook xlsWorkbook;
					int num5;
					string text2;
					int num6;
					string text3;
					switch (num)
					{
					case 0:
						goto IL_179;
					case 1:
						goto IL_10B;
					case 2:
						num2 = num3 + 1;
						goto IL_22A;
					case 3:
						num4 = xlsExternWorkbook.ᜂ(text);
						goto IL_264;
					case 4:
						goto IL_372;
					case 5:
						num = 13;
						continue;
					case 6:
						if (true)
						{
						}
						num5 = xlsWorkbook.ExternWorkbooks.InsertSelfSupbook() + 1;
						num = 9;
						continue;
					case 7:
						num = 23;
						continue;
					case 8:
						if (text2 == null)
						{
							num = 20;
							continue;
						}
						num = 25;
						continue;
					case 9:
						goto IL_DC;
					case 10:
						num = 28;
						continue;
					case 11:
						if (num3 <= 0)
						{
							num = 5;
							continue;
						}
						num = 2;
						continue;
					case 12:
						goto IL_32B;
					case 13:
						num2 = 0;
						goto IL_22A;
					case 14:
						num4 = 65534;
						goto IL_264;
					case 15:
						if (num3 > 0)
						{
							num = 35;
							continue;
						}
						goto IL_372;
					case 16:
						return result;
					case 17:
						if (A_0[0] == '\'')
						{
							num = 7;
							continue;
						}
						goto IL_179;
					case 18:
						if (text != null)
						{
							num = 27;
							continue;
						}
						goto IL_251;
					case 19:
						text3 = A_0.Substring(0, num6);
						num = 32;
						continue;
					case 20:
						result = xlsWorkbook.AddSheetReference(text);
						num = 24;
						continue;
					case 21:
						if (num6 > 0)
						{
							num = 19;
							continue;
						}
						goto IL_309;
					case 22:
						goto IL_32B;
					case 23:
						if (A_0[length - 1] == '\'')
						{
							num = 31;
							continue;
						}
						goto IL_179;
					case 24:
						return result;
					case 25:
						if (xlsWorkbook.Loading)
						{
							num = 29;
							continue;
						}
						goto IL_40D;
					case 26:
						if (text.Length <= 0)
						{
							num = 30;
							continue;
						}
						num = 3;
						continue;
					case 27:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_10B;
						default:
							if (false)
							{
							}
							num = 26;
							continue;
						}
						break;
					case 28:
						if (num5 == 0)
						{
							num = 6;
							continue;
						}
						goto IL_DC;
					case 29:
						num = 1;
						continue;
					case 30:
						goto IL_251;
					case 31:
						A_0 = A_0.Substring(1, length - 2);
						num = 0;
						continue;
					case 32:
						goto IL_309;
					case 33:
						if (int.TryParse(text2, out num5))
						{
							num = 10;
							continue;
						}
						goto IL_40D;
					case 34:
						num = 33;
						continue;
					case 35:
						text2 = A_0.Substring(num6 + 1, num3 - num6 - 1);
						num = 4;
						continue;
					}
					break;
					IL_DC:
					xlsExternWorkbook = xlsWorkbook.ExternWorkbooks[num5 - 1];
					num = 22;
					continue;
					IL_10B:
					if (text3 == null)
					{
						num = 34;
						continue;
					}
					goto IL_40D;
					IL_179:
					text = null;
					text2 = null;
					text3 = null;
					num6 = A_0.IndexOf('[');
					num3 = A_0.IndexOf(']');
					num = 11;
					continue;
					IL_22A:
					int startIndex = num2;
					num = 21;
					continue;
					IL_251:
					num = 14;
					continue;
					IL_264:
					int num7 = num4;
					result = this.ᜈ.AddSheetReference(xlsExternWorkbook.Index, num7, num7);
					num = 16;
					continue;
					IL_309:
					num = 15;
					continue;
					IL_32B:
					num = 18;
					continue;
					IL_372:
					result = -1;
					text = A_0.Substring(startIndex);
					xlsWorkbook = (XlsWorkbook)A_1.Workbook;
					num5 = -1;
					num = 8;
					continue;
					IL_40D:
					xlsExternWorkbook = xlsWorkbook.ExternWorkbooks.ᜀ(text2, text3);
					num = 12;
				}
			}
			return result;
		}
		}
	}

	// Token: 0x06002364 RID: 9060 RVA: 0x0014993C File Offset: 0x0014893C
	private Ptg ᜀ(Dictionary<Type, sprᨳ> A_0, int A_1, ParseFormulaOptions A_2, ParseParameters A_3)
	{
		int a_ = 6;
		switch (0)
		{
		default:
		{
			int num = 23;
			sprᯡ sprᯡ2;
			Ptg result;
			for (;;)
			{
				string text;
				switch (num)
				{
				case 0:
					if (text == RecordTableEnumerator.b("画砽", a_))
					{
						num = 15;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_2B9;
					default:
						if (false)
						{
						}
						num = 16;
						continue;
					}
					break;
				case 1:
					if (text.StartsWith(RecordTableEnumerator.b("挻䘽ⰿ⑁⩃桅", a_)))
					{
						num = 6;
						continue;
					}
					goto IL_10A;
				case 2:
				{
					int index = this.ᜆ.Count - 1;
					sprᯡ sprᯡ = (sprᯡ)this.ᜆ[index];
					num = 7;
					continue;
				}
				case 3:
					goto IL_140;
				case 4:
					num = 17;
					continue;
				case 5:
					A_2 -= 16;
					A_2 |= ParseFormulaOptions.ParseOperand;
					num = 22;
					continue;
				case 6:
					text.Replace(RecordTableEnumerator.b("挻䘽ⰿ⑁⩃桅", a_), string.Empty);
					num = 9;
					continue;
				case 7:
				{
					sprᯡ sprᯡ;
					if (!sprᯡ.ᜇ())
					{
						num = 26;
						continue;
					}
					goto IL_2CA;
				}
				case 8:
					goto IL_140;
				case 9:
					goto IL_10A;
				case 10:
					goto IL_2CA;
				case 11:
					goto IL_140;
				case 12:
					if (sprᯡ2 != null)
					{
						num = 25;
						continue;
					}
					num = 30;
					continue;
				case 13:
					goto IL_140;
				case 14:
					goto IL_295;
				case 15:
					result = this.ᜀ(A_0, A_1, A_2, A_3, sprᯡ2);
					sprᯡ2 = null;
					num = 11;
					continue;
				case 16:
				{
					ExcelFunction a_2;
					if (FormulaUtil.FunctionAliasToId.TryGetValue(text, out a_2))
					{
						num = 19;
						continue;
					}
					num = 20;
					continue;
				}
				case 17:
					if (!this.ᜀ(ref result, A_0, A_1, A_2, A_3))
					{
						num = 28;
						continue;
					}
					goto IL_140;
				case 18:
					goto IL_2C5;
				case 19:
					num = 21;
					continue;
				case 20:
				{
					int num2;
					int num3;
					if (!FormulaUtil.ᜀ(this.ᜅ.ᜎ(), A_3.Workbook, out num2, out num3))
					{
						num = 4;
						continue;
					}
					goto IL_39A;
				}
				case 21:
				{
					ExcelFunction a_2;
					if (this.ᜀ(a_2, this.ᜈ.Version))
					{
						num = 29;
						continue;
					}
					result = this.ᜀ(A_0, A_1, A_2, A_3, true);
					num = 3;
					continue;
				}
				case 22:
					goto IL_333;
				case 24:
					if (this.ᜅ.ᜇ == FormulaToken.Space)
					{
						num = 2;
						continue;
					}
					goto IL_2CA;
				case 25:
					if (true)
					{
					}
					this.ᜆ.Add(sprᯡ2);
					num = 14;
					continue;
				case 26:
				{
					sprᯡ sprᯡ;
					sprᯡ2 = sprᯡ;
					int index;
					this.ᜆ.RemoveAt(index);
					num = 10;
					continue;
				}
				case 27:
					sprᯡ2 = this.ᜇ.Pop();
					sprᯡ2.ᜁ(4);
					this.ᜆ.Add(sprᯡ2);
					goto IL_2B9;
				case 28:
					goto IL_39A;
				case 29:
				{
					ExcelFunction a_2;
					result = this.ᜀ(a_2, A_0, A_1, A_2, A_3);
					num = 8;
					continue;
				}
				case 30:
					if (this.ᜇ.Count > 0)
					{
						num = 27;
						continue;
					}
					goto IL_42B;
				}
				if ((A_2 & ParseFormulaOptions.ParseComplexOperand) != ParseFormulaOptions.None)
				{
					num = 5;
					continue;
				}
				goto IL_333;
				IL_10A:
				result = null;
				sprᯡ2 = null;
				num = 24;
				continue;
				IL_140:
				num = 12;
				continue;
				IL_2B9:
				num = 18;
				continue;
				IL_2CA:
				num = 0;
				continue;
				IL_333:
				text = this.ᜅ.ᜎ().ToUpper();
				num = 1;
				continue;
				IL_39A:
				result = this.ᜀ(A_0, A_1, A_2, A_3, false);
				num = 13;
			}
			IL_295:
			IL_2C5:
			IL_42B:
			sprᯡ2 = null;
			return result;
		}
		}
	}

	// Token: 0x06002365 RID: 9061 RVA: 0x00149D78 File Offset: 0x00148D78
	private bool ᜀ(ExcelFunction A_0, ExcelVersion A_1)
	{
		bool result;
		for (;;)
		{
			result = true;
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return result;
				case 1:
					goto IL_6F;
				case 2:
					result = false;
					num = 6;
					continue;
				case 3:
					num = 5;
					continue;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_6F;
					default:
						if (false)
						{
						}
						if (A_1 < ExcelVersion.Version2010)
						{
							if (true)
							{
							}
							num = 3;
							continue;
						}
						return result;
					}
					break;
				case 5:
					if (FormulaUtil.ᜁ(A_0))
					{
						num = 2;
						continue;
					}
					num = 9;
					continue;
				case 6:
					return result;
				case 7:
					if (FormulaUtil.ᜀ(A_0))
					{
						num = 8;
						continue;
					}
					return result;
				case 8:
					result = false;
					num = 0;
					continue;
				case 9:
					if (A_1 < ExcelVersion.Version2007)
					{
						num = 1;
						continue;
					}
					return result;
				}
				break;
				IL_6F:
				num = 7;
			}
		}
		return result;
	}

	// Token: 0x06002366 RID: 9062 RVA: 0x00149E78 File Offset: 0x00148E78
	private bool ᜀ(ref Ptg A_0, Dictionary<Type, sprᨳ> A_1, int A_2, ParseFormulaOptions A_3, ParseParameters A_4)
	{
		string text;
		for (;;)
		{
			for (;;)
			{
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
					text = this.ᜅ.ᜎ();
					text = text.ToUpper();
					if (true)
					{
					}
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (!Enum.IsDefined(typeof(XLSXFunction), text))
							{
								num = 2;
								continue;
							}
							goto IL_A2;
						case 1:
							if (A_4.Workbook.Version != ExcelVersion.Version97to2003)
							{
								num = 3;
								continue;
							}
							return false;
						case 2:
							goto IL_96;
						case 3:
							num = 0;
							continue;
						}
						break;
					}
					break;
				}
				}
			}
		}
		return false;
		IL_96:
		return false;
		IL_A2:
		XLSXFunction a_ = (XLSXFunction)Enum.Parse(typeof(XLSXFunction), text, false);
		A_0 = this.ᜀ((ExcelFunction)a_, A_1, A_2, A_3, A_4);
		return true;
	}

	// Token: 0x06002367 RID: 9063 RVA: 0x00149F50 File Offset: 0x00148F50
	private bool ᜀ(string A_0, Dictionary<Type, sprᨳ> A_1, int A_2, ParseFormulaOptions A_3, ParseParameters A_4, out Ptg A_5, int A_6)
	{
		int a_ = 16;
		switch (0)
		{
		default:
		{
			int num = 26;
			bool flag3;
			for (;;)
			{
				bool isR1C;
				string a_2;
				string text;
				string a_3;
				string a_4;
				FormulaToken a_5;
				int cellRow;
				int cellColumn;
				XlsWorkbook a_6;
				int a_7;
				switch (num)
				{
				case 0:
					if (this.ᜈ.FormulaUtil.ᜀ(A_0, isR1C, out a_2, out text, out a_3, out a_4))
					{
						num = 6;
						continue;
					}
					num = 20;
					continue;
				case 1:
					if (A_1.ContainsKey(typeof(sprᦈ)))
					{
						num = 24;
						continue;
					}
					goto IL_4D7;
				case 2:
					if (text != null)
					{
						num = 4;
						continue;
					}
					goto IL_446;
				case 3:
					goto IL_22A;
				case 4:
				{
					bool flag = text.Contains('$'.ToString());
					num = 12;
					continue;
				}
				case 5:
					if (A_1.ContainsKey(typeof(spr\u2596)))
					{
						num = 16;
						continue;
					}
					goto IL_3C2;
				case 6:
				{
					bool flag2 = false;
					num = 11;
					continue;
				}
				case 7:
					num = 1;
					continue;
				case 8:
					goto IL_2B6;
				case 9:
					num = 35;
					continue;
				case 10:
				{
					bool flag;
					if (!flag)
					{
						num = 9;
						continue;
					}
					goto IL_4D7;
				}
				case 11:
					if (text != null)
					{
						num = 34;
						continue;
					}
					goto IL_2B6;
				case 12:
					goto IL_446;
				case 13:
					goto IL_37C;
				case 14:
				{
					bool flag = false;
					num = 2;
					continue;
				}
				case 15:
					goto IL_4A4;
				case 16:
					a_5 = spr\u2596.ᜀ(FormulaUtil.ᜀ(typeof(spr\u2596), 0, A_1, A_2, A_3));
					A_5 = FormulaUtil.ᜀ(a_5, cellRow, cellColumn, a_2, text, a_3, a_4, isR1C, a_6);
					num = 3;
					continue;
				case 17:
					num = 5;
					continue;
				case 18:
					num = 19;
					continue;
				case 19:
					if (A_6 != -1)
					{
						num = 30;
						continue;
					}
					return flag3;
				case 20:
					if (FormulaUtil.IsCell(A_0, isR1C, out a_2, out text))
					{
						num = 14;
						continue;
					}
					flag3 = false;
					num = 23;
					continue;
				case 21:
					if (flag3)
					{
						num = 18;
						continue;
					}
					return flag3;
				case 22:
					goto IL_22A;
				case 23:
					goto IL_22A;
				case 24:
					a_7 = FormulaUtil.ᜀ(typeof(sprᦈ), 0, A_1, A_2, A_3) + 1;
					a_5 = sprᦈ.ᜀ(a_7);
					num = 27;
					continue;
				case 25:
					goto IL_22A;
				case 27:
					goto IL_37C;
				case 28:
				{
					bool flag2;
					if (!flag2)
					{
						num = 29;
						continue;
					}
					goto IL_3C2;
				}
				case 29:
					num = 32;
					continue;
				case 30:
					A_5 = (A_5 as spr\u1CD5).ᜀ(A_6);
					num = 33;
					continue;
				case 31:
					goto IL_DB;
				case 32:
					if (A_1 != null)
					{
						num = 17;
						continue;
					}
					goto IL_3C2;
				case 33:
					goto IL_26B;
				case 34:
				{
					bool flag2 = text.Contains('$'.ToString());
					num = 8;
					continue;
				}
				case 35:
					if (A_1 != null)
					{
						num = 7;
						continue;
					}
					goto IL_4D7;
				case 36:
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
						if (A_0 == null)
						{
							num = 15;
							continue;
						}
						flag3 = true;
						A_5 = null;
						IWorksheet worksheet = A_4.Worksheet;
						a_6 = (XlsWorkbook)A_4.Workbook;
						isR1C = A_4.IsR1C1;
						cellRow = A_4.CellRow;
						cellColumn = A_4.CellColumn;
						Dictionary<string, string> worksheetNames = A_4.WorksheetNames;
						ParseFormulaOptions parseFormulaOptions = A_3 & ParseFormulaOptions.DataValidation;
						num = 0;
						continue;
					}
					}
					break;
				}
				if (true)
				{
				}
				if (A_4 == null)
				{
					num = 31;
					continue;
				}
				num = 36;
				continue;
				IL_22A:
				num = 21;
				continue;
				IL_2B6:
				num = 28;
				continue;
				IL_37C:
				A_5 = FormulaUtil.ᜀ(a_5, cellRow, cellColumn, a_2, text, isR1C);
				num = 25;
				continue;
				IL_3C2:
				a_5 = sprᲔ.ᜀ(FormulaUtil.ᜀ(typeof(sprᲔ), 0, A_1, A_2, A_3));
				A_5 = FormulaUtil.ᜀ(a_5, cellRow, cellColumn, a_2, text, a_3, a_4, isR1C, a_6);
				num = 22;
				continue;
				IL_446:
				num = 10;
				continue;
				IL_4D7:
				a_7 = FormulaUtil.ᜀ(typeof(sprᦊ), 0, A_1, A_2, A_3);
				a_5 = sprᦊ.ᜀ(a_7);
				num = 13;
			}
			IL_DB:
			throw new ArgumentNullException(RecordTableEnumerator.b("❅㩇ⵉ㥋⍍㕏㱑⁓╕", a_));
			IL_26B:
			return flag3;
			IL_4A4:
			throw new ArgumentNullException(RecordTableEnumerator.b("㕅㱇㡉ੋ⅍≏㽑⅓㩕㥗", a_));
		}
		}
	}

	// Token: 0x06002368 RID: 9064 RVA: 0x0014A464 File Offset: 0x00149464
	private Ptg ᜀ(string A_0, ParseParameters A_1, Dictionary<Type, sprᨳ> A_2, int A_3, ParseFormulaOptions A_4, int A_5)
	{
		for (;;)
		{
			for (;;)
			{
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
					XlsWorkbook xlsWorkbook = (XlsWorkbook)A_1.Workbook;
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_8C;
						case 1:
							num = 3;
							continue;
						case 2:
							if (A_5 >= 0)
							{
								num = 1;
								continue;
							}
							goto IL_98;
						case 3:
							if (true)
							{
							}
							if (!xlsWorkbook.IsLocalReference(A_5))
							{
								num = 0;
								continue;
							}
							goto IL_98;
						}
						break;
					}
					break;
				}
				}
			}
		}
		IL_8C:
		return this.ᜁ(A_5, A_0, A_1, A_2, A_3, A_4);
		IL_98:
		return this.ᜀ(A_5, A_0, A_1, A_2, A_3, A_4);
	}

	// Token: 0x06002369 RID: 9065 RVA: 0x0014A518 File Offset: 0x00149518
	private Ptg ᜁ(int A_0, string A_1, ParseParameters A_2, Dictionary<Type, sprᨳ> A_3, int A_4, ParseFormulaOptions A_5)
	{
		int a_ = 7;
		switch (0)
		{
		default:
		{
			int num = 7;
			int num2;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					sprᭆ sprᭆ;
					num2 = sprᭆ.ᜃ(A_1);
					num = 4;
					continue;
				}
				case 1:
					goto IL_97;
				case 2:
					if (A_1 == null)
					{
						num = 3;
						continue;
					}
					num = 6;
					continue;
				case 3:
					goto IL_B1;
				case 4:
					goto IL_CB;
				case 5:
					if (num2 < 0)
					{
						num = 0;
						continue;
					}
					goto IL_167;
				case 6:
				{
					if (A_0 < 0)
					{
						if (true)
						{
						}
						num = 1;
						continue;
					}
					XlsWorkbook xlsWorkbook = (XlsWorkbook)A_2.Workbook;
					int bookIndex = xlsWorkbook.GetBookIndex(A_0);
					XlsExternWorkbook xlsExternWorkbook = xlsWorkbook.ExternWorkbooks[bookIndex];
					sprᭆ sprᭆ = xlsExternWorkbook.ExternNames;
					num2 = sprᭆ.ᜂ(A_1);
					num = 5;
					continue;
				}
				case 8:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_50;
					default:
						goto IL_E6;
					}
					break;
				}
				goto IL_4D;
				IL_50:
				num = 8;
				continue;
				IL_4D:
				if (A_2 == null)
				{
					goto IL_50;
				}
				num = 2;
			}
			IL_97:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("吼派⑀╂ౄ⥆ⵈ⹊㕌", a_));
			IL_B1:
			throw new ArgumentNullException(RecordTableEnumerator.b("丼䬾㍀ᝂ⩄ⱆⱈ╊", a_));
			IL_CB:
			goto IL_167;
			IL_E6:
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("尼䴾♀㙂⡄≆❈㽊㹌", a_));
			IL_167:
			return this.ᜀ(A_0, num2, A_2, A_3, A_4, A_5);
		}
		}
	}

	// Token: 0x0600236A RID: 9066 RVA: 0x0014A69C File Offset: 0x0014969C
	private Ptg ᜀ(int A_0, string A_1, ParseParameters A_2, Dictionary<Type, sprᨳ> A_3, int A_4, ParseFormulaOptions A_5)
	{
		int a_ = 8;
		switch (0)
		{
		default:
		{
			int num = 0;
			INamedRange namedRange;
			for (;;)
			{
				XlsWorkbook xlsWorkbook;
				INameRanges nameRanges;
				INameRanges names;
				INamedRange namedRange2;
				switch (num)
				{
				case 1:
					if (xlsWorkbook.ThrowOnUnknownNames)
					{
						num = 6;
						continue;
					}
					namedRange = nameRanges.Add(A_1);
					num = 11;
					continue;
				case 2:
					names = xlsWorkbook.Names;
					goto IL_2FE;
				case 3:
				{
					IWorksheet sheetByReference;
					if (sheetByReference == null)
					{
						num = 12;
						continue;
					}
					num = 19;
					continue;
				}
				case 4:
					if (namedRange == null)
					{
						num = 26;
						continue;
					}
					goto IL_2E6;
				case 5:
					goto IL_12E;
				case 6:
					goto IL_24B;
				case 7:
					goto IL_AB;
				case 8:
				{
					XlsWorksheet xlsWorksheet;
					if (xlsWorksheet == null)
					{
						num = 5;
						continue;
					}
					num = 24;
					continue;
				}
				case 9:
					goto IL_10B;
				case 10:
					goto IL_2E6;
				case 11:
					goto IL_2E4;
				case 12:
					num = 2;
					continue;
				case 13:
					if (namedRange == null)
					{
						num = 14;
						continue;
					}
					goto IL_10B;
				case 14:
					namedRange = nameRanges.GetByName(A_1);
					num = 21;
					continue;
				case 15:
					num = 16;
					continue;
				case 16:
				{
					if (A_1.Length == 0)
					{
						num = 23;
						continue;
					}
					xlsWorkbook = (XlsWorkbook)A_2.Workbook;
					XlsWorksheet xlsWorksheet = A_2.Worksheet as XlsWorksheet;
					nameRanges = null;
					num = 20;
					continue;
				}
				case 17:
					num = 8;
					continue;
				case 18:
					if (A_1 != null)
					{
						num = 15;
						continue;
					}
					goto IL_F7;
				case 19:
				{
					IWorksheet sheetByReference;
					names = (sheetByReference as XlsWorksheet).Names;
					goto IL_2FE;
				}
				case 20:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_12E;
					default:
					{
						if (false)
						{
						}
						if (A_0 == -1)
						{
							num = 17;
							continue;
						}
						IWorksheet sheetByReference = xlsWorkbook.GetSheetByReference(A_0, false);
						num = 3;
						continue;
					}
					}
					break;
				case 21:
					if (true)
					{
					}
					goto IL_10B;
				case 22:
					namedRange2 = null;
					goto IL_24D;
				case 23:
					goto IL_2AB;
				case 24:
				{
					XlsWorksheet xlsWorksheet;
					namedRange2 = xlsWorksheet.Names[A_1];
					goto IL_24D;
				}
				case 25:
					num = 1;
					continue;
				case 26:
					namedRange = xlsWorkbook.Names[A_1];
					num = 10;
					continue;
				case 27:
					if (namedRange == null)
					{
						num = 25;
						continue;
					}
					goto IL_348;
				}
				if (A_2 == null)
				{
					num = 7;
					continue;
				}
				num = 18;
				continue;
				IL_10B:
				num = 27;
				continue;
				IL_12E:
				num = 22;
				continue;
				IL_24D:
				namedRange = namedRange2;
				num = 4;
				continue;
				IL_2E6:
				nameRanges = xlsWorkbook.Names;
				num = 9;
				continue;
				IL_2FE:
				nameRanges = names;
				namedRange = nameRanges[A_1];
				num = 13;
			}
			IL_AB:
			throw new ArgumentNullException(RecordTableEnumerator.b("弽㈿╁ㅃ⭅ⵇ⑉㡋㵍", a_));
			IL_F7:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("䴽㐿ぁ၃⥅⍇⽉≋", a_));
			IL_24B:
			throw new spr\u2313(A_1 + RecordTableEnumerator.b("ḽ⤿ㅁ摃⡅❇㹉汋㡍ㅏ㹑㵓㉕硗㑙㵛㍝՟١䑣ᑥ१ѩ୫୭", a_));
			IL_2AB:
			goto IL_F7;
			IL_2E4:
			IL_348:
			int index = namedRange.Index;
			return this.ᜀ(A_0, index, A_2, A_3, A_4, A_5);
		}
		}
	}

	// Token: 0x0600236B RID: 9067 RVA: 0x0014AA0C File Offset: 0x00149A0C
	private Ptg ᜀ(int A_0, int A_1, ParseParameters A_2, Dictionary<Type, sprᨳ> A_3, int A_4, ParseFormulaOptions A_5)
	{
		Ptg result;
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
		{
			IL_61:
			FormulaToken a_ = spr\u1B76.ᜀ(FormulaUtil.ᜀ(typeof(spr\u1B76), 0, A_3, A_4, A_5));
			result = FormulaUtil.ᜀ(a_, new object[]
			{
				A_0,
				A_1
			});
			num = 2;
			break;
		}
		default:
			if (true)
			{
			}
			if (false)
			{
			}
			switch (0)
			{
			default:
				num = 3;
				break;
			}
			break;
		}
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_61;
			case 1:
				return result;
			case 2:
				return result;
			}
			if (A_0 >= 0)
			{
				num = 0;
			}
			else
			{
				FormulaToken a_2 = spr\u25A0.ᜀ(FormulaUtil.ᜀ(typeof(spr\u25A0), 0, A_3, A_4, A_5));
				result = FormulaUtil.ᜀ(a_2, new object[]
				{
					A_1
				});
				num = 1;
			}
		}
		return result;
	}

	// Token: 0x0600236C RID: 9068 RVA: 0x0014AB14 File Offset: 0x00149B14
	private Ptg ᜀ(string A_0, string A_1, ParseParameters A_2, Dictionary<Type, sprᨳ> A_3, int A_4, ParseFormulaOptions A_5)
	{
		switch (0)
		{
		default:
		{
			Ptg result;
			for (;;)
			{
				IWorkbook workbook = A_2.Workbook;
				IWorksheet worksheet = A_2.Worksheet;
				int num = 7;
				for (;;)
				{
					INamedRange namedRange;
					INamedRange namedRange2;
					switch (num)
					{
					case 0:
						if (worksheet == null)
						{
							num = 11;
							continue;
						}
						num = 12;
						continue;
					case 1:
					{
						int index = namedRange.Index;
						FormulaToken a_ = spr\u25A0.ᜀ(FormulaUtil.ᜀ(typeof(spr\u25A0), 0, A_3, A_4, A_5));
						result = FormulaUtil.ᜀ(a_, new object[]
						{
							index
						});
						num = 4;
						continue;
					}
					case 2:
						num = 14;
						continue;
					case 3:
						namedRange = workbook.Names[A_1];
						num = 5;
						continue;
					case 4:
						return result;
					case 5:
						goto IL_E0;
					case 6:
						goto IL_1CF;
					case 7:
						if (A_0 != null)
						{
							num = 2;
							continue;
						}
						goto IL_1CF;
					case 8:
						if (true)
						{
						}
						if (namedRange != null)
						{
							goto IL_E0;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return result;
						default:
							if (false)
							{
							}
							num = 3;
							continue;
						}
						break;
					case 9:
						if (namedRange != null)
						{
							num = 1;
							continue;
						}
						return result;
					case 10:
						worksheet = workbook.Worksheets[A_0];
						num = 6;
						continue;
					case 11:
						num = 13;
						continue;
					case 12:
						namedRange2 = ((XlsWorksheet)worksheet).Names[A_1];
						goto IL_128;
					case 13:
						namedRange2 = null;
						goto IL_128;
					case 14:
						if (A_0.Length > 0)
						{
							num = 10;
							continue;
						}
						goto IL_1CF;
					}
					break;
					IL_E0:
					result = null;
					num = 9;
					continue;
					IL_128:
					namedRange = namedRange2;
					num = 8;
					continue;
					IL_1CF:
					num = 0;
				}
			}
			return result;
		}
		}
	}

	// Token: 0x0600236D RID: 9069 RVA: 0x0014AD14 File Offset: 0x00149D14
	private bool ᜀ(IWorkbook A_0, string A_1)
	{
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return false;
				default:
					if (false)
					{
					}
					if (A_0.Worksheets[A_1] != null)
					{
						num = 5;
						continue;
					}
					return true;
				}
				break;
			case 1:
				if (A_1.Length == 0)
				{
					if (true)
					{
					}
					num = 2;
					continue;
				}
				num = 0;
				continue;
			case 2:
				goto IL_A0;
			case 4:
				num = 1;
				continue;
			case 5:
				goto IL_6F;
			}
			if (A_1 == null)
			{
				return false;
			}
			num = 4;
		}
		IL_6F:
		return false;
		IL_A0:
		return false;
	}

	// Token: 0x0600236E RID: 9070 RVA: 0x0014ADC8 File Offset: 0x00149DC8
	private Ptg ᜀ(ExcelFunction A_0, Dictionary<Type, sprᨳ> A_1, int A_2, ParseFormulaOptions A_3, ParseParameters A_4)
	{
		int a_ = 6;
		switch (0)
		{
		default:
		{
			if (true)
			{
			}
			spr\u1B43 spr_u1B;
			for (;;)
			{
				spr_u1B = null;
				List<int> list = this.ᜀ(A_3, A_1, A_4, A_0);
				int num = 6;
				for (;;)
				{
					int num2;
					switch (num)
					{
					case 0:
						num = 1;
						continue;
					case 1:
						goto IL_11A;
					case 2:
						goto IL_95;
					case 3:
						return spr_u1B;
					case 4:
						return spr_u1B;
					case 5:
						this.ᜅ.ᜀ(RecordTableEnumerator.b("欻䰽⼿ⱁ⍃晅⥇㡉⭋㭍㵏㝑㩓≕⭗穙㉛⭝ൟaţᑥ䡧౩ͫᱭ偯ᑱųᡵ᭷๹ᕻᅽ뢁ꒃ", a_) + A_0, null);
						num = 2;
						continue;
					case 6:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_11A;
						default:
						{
							if (false)
							{
							}
							if (FormulaUtil.FunctionIdToParamCount.TryGetValue(A_0, out num2))
							{
								num = 0;
								continue;
							}
							int a_2 = FormulaUtil.ᜀ(typeof(spr\u231A), 0, A_1, A_2, A_3);
							FormulaToken a_3 = spr\u231A.ᜀ(a_2);
							spr_u1B = (spr\u1B43)FormulaUtil.ᜀ(a_3, A_0);
							spr_u1B.ᜀ((byte)(list.Count - 1));
							num = 3;
							continue;
						}
						}
						break;
					}
					break;
					IL_95:
					int a_4 = FormulaUtil.ᜀ(typeof(spr\u1B43), 0, A_1, A_2, A_3);
					FormulaToken a_5 = spr\u1B43.ᜀ(a_4);
					spr_u1B = (spr\u1B43)FormulaUtil.ᜀ(a_5, A_0);
					num = 4;
					continue;
					IL_11A:
					if (num2 == list.Count - 1)
					{
						goto IL_95;
					}
					num = 5;
				}
			}
			return spr_u1B;
		}
		}
	}

	// Token: 0x0600236F RID: 9071 RVA: 0x0014AF58 File Offset: 0x00149F58
	private Ptg ᜀ(Dictionary<Type, sprᨳ> A_0, int A_1, ParseFormulaOptions A_2, ParseParameters A_3, sprᯡ A_4)
	{
		int a_ = 11;
		switch (0)
		{
		default:
		{
			List<int> list;
			for (;;)
			{
				list = this.ᜀ(A_2, A_0, A_3, ExcelFunction.IF);
				int num = list.Count - 1;
				int num2 = 24;
				for (;;)
				{
					int num3;
					int num4;
					sprᯡ sprᯡ;
					int num5;
					int num6;
					int num7;
					Ptg item;
					bool flag;
					switch (num2)
					{
					case 0:
						num3 += A_4.GetSize(A_3.Version);
						num2 = 25;
						continue;
					case 1:
						num2 = 16;
						continue;
					case 2:
						num4 = 0;
						goto IL_19E;
					case 3:
						sprᯡ = this.ᜇ.Pop();
						sprᯡ.ᜁ(4);
						num3 += sprᯡ.GetSize(A_3.Version);
						num2 = 14;
						continue;
					case 4:
						goto IL_173;
					case 5:
						num2 = 2;
						continue;
					case 6:
						if (A_4 != null)
						{
							num2 = 15;
							continue;
						}
						goto IL_290;
					case 7:
						if (A_4 != null)
						{
							num2 = 0;
							continue;
						}
						goto IL_30C;
					case 8:
						goto IL_290;
					case 9:
						if (num != 3)
						{
							num2 = 5;
							continue;
						}
						num2 = 20;
						continue;
					case 10:
						if (sprᯡ != null)
						{
							num2 = 17;
							continue;
						}
						goto IL_C9;
					case 11:
						goto IL_C9;
					case 12:
						if (num < 2)
						{
							num2 = 4;
							continue;
						}
						goto IL_110;
					case 13:
						if (num == 3)
						{
							num2 = 21;
							continue;
						}
						goto IL_418;
					case 14:
						goto IL_372;
					case 15:
						this.ᜆ.Add(A_4);
						num2 = 8;
						continue;
					case 16:
						num5 = 0;
						goto IL_234;
					case 17:
						this.ᜆ.Add(sprᯡ);
						num2 = 11;
						continue;
					case 18:
						num5 = 8;
						goto IL_234;
					case 19:
						goto IL_2F5;
					case 20:
						num4 = this.ᜀ(num6, list[3], A_3) + 4;
						goto IL_19E;
					case 21:
						item = FormulaUtil.ᜀ(FormulaToken.tAttr, new object[]
						{
							num7,
							3
						});
						this.ᜆ.Add(item);
						num2 = 19;
						continue;
					case 22:
						if (!flag)
						{
							num2 = 1;
							continue;
						}
						num2 = 18;
						continue;
					case 23:
						if (this.ᜇ.Count > 0)
						{
							num2 = 3;
							continue;
						}
						goto IL_372;
					case 24:
						if (num <= 3)
						{
							num2 = 26;
							continue;
						}
						goto IL_173;
					case 25:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_1A3;
						default:
							if (false)
							{
							}
							goto IL_30C;
						}
						break;
					case 26:
						num2 = 12;
						continue;
					case 27:
						goto IL_110;
					}
					break;
					IL_C9:
					num2 = 13;
					continue;
					IL_110:
					int num8 = list[1];
					num6 = list[2];
					int num9 = this.ᜀ(num8, list[2], A_3) + 4;
					num2 = 9;
					continue;
					IL_173:
					this.ᜅ.ᜀ(RecordTableEnumerator.b("@ㅂ≄㉆⑈⹊⍌㭎煐げ㩔≖㝘⽚絜㥞๠ᅢ䕤⹦⽨䭪୬ᩮὰၲŴṶᙸᕺ嵼ቾꞆ권붎놐ﲒ랖ꪘ떚", a_), null);
					num2 = 27;
					continue;
					IL_1A3:
					num2 = 23;
					continue;
					IL_19E:
					num3 = num4;
					sprᯡ = null;
					goto IL_1A3;
					IL_234:
					num7 = num5;
					item = FormulaUtil.ᜀ(FormulaToken.tAttr, new object[]
					{
						num7,
						num3 + 3
					});
					this.ᜆ.Insert(num6, item);
					num2 = 6;
					continue;
					IL_290:
					num2 = 10;
					continue;
					IL_30C:
					item = FormulaUtil.ᜀ(FormulaToken.tAttr, new object[]
					{
						2,
						num9
					});
					this.ᜆ.Insert(num8, item);
					num6++;
					flag = ((A_2 & ParseFormulaOptions.InArray) == ParseFormulaOptions.None);
					num2 = 22;
					continue;
					IL_372:
					if (true)
					{
					}
					num2 = 7;
				}
			}
			IL_2F5:
			IL_418:
			int a_2 = FormulaUtil.ᜀ(typeof(spr\u231A), 1, A_0, A_1, A_2);
			FormulaToken a_3 = spr\u231A.ᜀ(a_2);
			spr\u1B43 spr_u1B = (spr\u1B43)FormulaUtil.ᜀ(a_3, ExcelFunction.IF);
			spr_u1B.ᜀ((byte)(list.Count - 1));
			return spr_u1B;
		}
		}
	}

	// Token: 0x06002370 RID: 9072 RVA: 0x0014B3BC File Offset: 0x0014A3BC
	private Ptg ᜀ(Dictionary<Type, sprᨳ> A_0, int A_1, ParseFormulaOptions A_2, ParseParameters A_3, bool A_4)
	{
		int a_ = 15;
		int num2;
		int index;
		for (;;)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					IWorksheet worksheet = A_3.Worksheet;
					int num = 6;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (num2 == -1)
							{
								num = 2;
								continue;
							}
							num = 1;
							continue;
						case 1:
							goto IL_11C;
						case 2:
							if (true)
							{
							}
							num = 8;
							continue;
						case 3:
							goto IL_19E;
						case 4:
							if (A_4)
							{
								num = 9;
								continue;
							}
							this.ᜅ.ᜀ(this.ᜅ.ᜎ() + RecordTableEnumerator.b("敄⹆㩈╊橌㭎煐げ⁔⑖ⵘ㑚ぜ罞ݠᙢ୤ѦᵨɪɬŮ彰", a_), null);
							num = 3;
							continue;
						case 5:
							num = 4;
							continue;
						case 6:
							if (!FormulaUtil.ᜀ(this.ᜅ.ᜎ(), A_3.Workbook, out num2, out index))
							{
								num = 5;
								continue;
							}
							goto IL_19E;
						case 7:
							goto IL_19E;
						case 8:
							goto IL_97;
						case 9:
						{
							INamedRange namedRange = this.ᜈ.Names.Add(this.ᜅ.ᜎ());
							(namedRange as XlsName).IsFunction = true;
							index = namedRange.Index;
							num2 = -1;
							num = 7;
							continue;
						}
						}
						break;
						IL_19E:
						num = 0;
					}
				}
				IL_11C:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_132;
				}
				break;
			}
		}
		IL_97:
		Ptg ptg = FormulaUtil.ᜀ(FormulaToken.tName1, new object[]
		{
			index
		});
		goto IL_1CA;
		IL_132:
		if (false)
		{
		}
		ptg = FormulaUtil.ᜀ(FormulaToken.tNameX1, new object[]
		{
			num2,
			index
		});
		IL_1CA:
		Ptg item = ptg;
		this.ᜆ.Add(item);
		List<int> list = this.ᜀ(A_2, A_0, A_3, ExcelFunction.CustomFunction);
		int a_2 = FormulaUtil.ᜀ(typeof(spr\u231A), 1, A_0, A_1, A_2);
		FormulaToken a_3 = spr\u231A.ᜀ(a_2);
		spr\u231A spr_u231A = (spr\u231A)FormulaUtil.ᜀ(a_3, ExcelFunction.CustomFunction);
		spr_u231A.ᜀ((byte)list.Count);
		return spr_u231A;
	}

	// Token: 0x06002371 RID: 9073 RVA: 0x0014B5F4 File Offset: 0x0014A5F4
	private int ᜀ(int A_0, int A_1, ParseParameters A_2)
	{
		int a_ = 6;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
			{
				if (A_0 >= A_1)
				{
					num = 3;
					continue;
				}
				int num2 = 0;
				int num3 = A_0;
				num = 6;
				continue;
			}
			case 1:
			{
				int num2;
				return num2;
			}
			case 3:
				goto IL_BB;
			case 4:
				goto IL_C7;
			case 5:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_BB;
				default:
					if (false)
					{
					}
					num = 0;
					continue;
				}
				break;
			case 6:
				goto IL_C7;
			case 7:
			{
				int num3;
				if (num3 >= A_1)
				{
					if (true)
					{
					}
					num = 1;
					continue;
				}
				Ptg ptg = this.ᜆ[num3];
				int num2;
				num2 += ptg.GetSize(A_2.Version);
				num3++;
				num = 4;
				continue;
			}
			}
			if (A_0 >= 0)
			{
				num = 5;
				continue;
			}
			break;
			IL_C7:
			num = 7;
		}
		IL_BB:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("唻洽㐿⍁㙃㉅᱇╉❋⭍㹏", a_));
	}

	// Token: 0x06002372 RID: 9074 RVA: 0x0014B704 File Offset: 0x0014A704
	private List<int> ᜀ(ParseFormulaOptions A_0, Dictionary<Type, sprᨳ> A_1, ParseParameters A_2, ExcelFunction A_3)
	{
		int a_ = 0;
		switch (0)
		{
		default:
		{
			List<int> list;
			for (;;)
			{
				Dictionary<Type, sprᨳ> a_2 = FormulaUtil.\u1715[A_3];
				ParseFormulaOptions parseFormulaOptions = A_0;
				int num = 6;
				for (;;)
				{
					int num2;
					switch (num)
					{
					case 0:
						goto IL_2FE;
					case 1:
						goto IL_147;
					case 2:
						goto IL_1C8;
					case 3:
						if (true)
						{
						}
						goto IL_215;
					case 4:
						goto IL_2C9;
					case 5:
						goto IL_1C8;
					case 6:
						if ((A_0 & ParseFormulaOptions.DataValidation) != ParseFormulaOptions.None)
						{
							num = 8;
							continue;
						}
						goto IL_147;
					case 7:
						if (this.ᜇ.Count > 0)
						{
							num = 16;
							continue;
						}
						goto IL_169;
					case 8:
						a_2 = A_1;
						num = 1;
						continue;
					case 9:
						if ((A_0 & ParseFormulaOptions.RootLevel) != ParseFormulaOptions.None)
						{
							num = 20;
							continue;
						}
						goto IL_215;
					case 10:
						if (this.ᜅ.ᜆ == FormulaToken.CloseParenthesis)
						{
							num = 13;
							continue;
						}
						this.ᜀ(Priority.None, a_2, num2, parseFormulaOptions, A_2);
						num = 7;
						continue;
					case 11:
						if (this.ᜅ.ᜆ != FormulaToken.tParentheses)
						{
							num = 0;
							continue;
						}
						this.ᜅ.ᜋ();
						num2 = 0;
						list = new List<int>();
						list.Add(this.ᜆ.Count);
						num = 2;
						continue;
					case 12:
						goto IL_245;
					case 13:
						goto IL_1F2;
					case 14:
						this.ᜅ.ᜋ();
						num = 12;
						continue;
					case 15:
						goto IL_169;
					case 16:
					{
						sprᯡ sprᯡ = this.ᜇ.Pop();
						sprᯡ.ᜁ(true);
						this.ᜆ.Add(sprᯡ);
						num = 15;
						continue;
					}
					case 17:
						if (this.ᜅ.ᜆ != FormulaToken.Comma)
						{
							goto IL_245;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_17A;
						default:
							if (false)
							{
							}
							num = 14;
							continue;
						}
						break;
					case 18:
						this.ᜆ.Add(FormulaUtil.ᜀ(FormulaToken.tAttr, 1, 0));
						num = 4;
						continue;
					case 19:
						if (FormulaUtil.IndexOf(FormulaUtil.\u171B, A_3) != -1)
						{
							num = 18;
							continue;
						}
						goto IL_2C9;
					case 20:
						parseFormulaOptions--;
						num = 3;
						continue;
					}
					break;
					IL_147:
					num = 9;
					continue;
					IL_17A:
					num = 17;
					continue;
					IL_169:
					list.Add(this.ᜆ.Count);
					goto IL_17A;
					IL_1C8:
					num = 10;
					continue;
					IL_215:
					parseFormulaOptions |= ParseFormulaOptions.ParseOperand;
					num = 19;
					continue;
					IL_245:
					num2++;
					num = 5;
					continue;
					IL_2C9:
					this.ᜅ.ᜋ();
					num = 11;
				}
			}
			IL_1F2:
			this.ᜅ.ᜋ();
			return list;
			IL_2FE:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("电夷吹ᬻ䨽怿❁㱃㉅㩇⭉⽋㩍灏㑑⅓㡕㭗⹙㕛ㅝ๟䉡գᑥཧὩū୭ṯٱݳ塵", a_));
		}
		}
	}

	// Token: 0x06002373 RID: 9075 RVA: 0x0014BA20 File Offset: 0x0014AA20
	private Ptg ᜀ(FormulaToken A_0, ParseParameters A_1)
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
		string text = this.ᜅ.ᜎ();
		return FormulaUtil.ᜀ(A_0, new object[]
		{
			text,
			A_1.FormulaUtility
		});
	}

	// Token: 0x06002374 RID: 9076 RVA: 0x0014BA84 File Offset: 0x0014AA84
	private Ptg ᜀ(Dictionary<Type, sprᨳ> A_0, int A_1, ParseFormulaOptions A_2, IWorksheet A_3)
	{
		int a_ = 11;
		switch (0)
		{
		default:
		{
			Ptg ptg;
			for (;;)
			{
				IL_58:
				int a_2;
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_23C:
					a_2 = FormulaUtil.ᜀ(typeof(spr\u23C7), 1, A_0, A_1, A_2);
					num = 6;
					break;
				case 1:
					goto IL_78;
				default:
					goto IL_78;
				}
				int num2;
				string text;
				for (;;)
				{
					IL_19:
					sprẄ sprẄ;
					FormulaToken formulaToken;
					FormulaToken a_3;
					switch (num)
					{
					case 0:
						sprẄ.ᜂ(ushort.MaxValue);
						num = 11;
						continue;
					case 1:
						goto IL_14A;
					case 2:
						if (num2 != -1)
						{
							num = 12;
							continue;
						}
						num = 3;
						continue;
					case 3:
					{
						if (text.EndsWith(RecordTableEnumerator.b("所ᅂDņ案", a_)))
						{
							num = 1;
							continue;
						}
						string text2 = this.ᜅ.ᜎ();
						ConstructorInfo constructorInfo = FormulaUtil.ErrorNameToConstructor[text2];
						ptg = (Ptg)constructorInfo.Invoke(new object[]
						{
							text2
						});
						num = 4;
						continue;
					}
					case 4:
						goto IL_14F;
					case 5:
						formulaToken = spr\u23C7.ᜀ(a_2);
						goto IL_1EC;
					case 6:
						if (A_3 == null)
						{
							num = 7;
							continue;
						}
						num = 5;
						continue;
					case 7:
						if (true)
						{
						}
						num = 8;
						continue;
					case 8:
						formulaToken = spr\u1B37.ᜀ(a_2);
						goto IL_1EC;
					case 9:
						if (sprẄ != null)
						{
							num = 0;
							continue;
						}
						return ptg;
					case 10:
						return ptg;
					case 11:
						return ptg;
					case 12:
					{
						text = text.Substring(0, num2);
						text = text.Trim(new char[]
						{
							'\''
						});
						int num3 = this.ᜈ.AddSheetReference(text);
						a_2 = FormulaUtil.ᜀ(typeof(spr\u1B37), 1, A_0, A_1, A_2);
						a_3 = spr\u1B37.ᜀ(a_2);
						ptg = FormulaUtil.ᜁ(a_3);
						((spr\u1B37)ptg).ᜂ((ushort)num3);
						num = 10;
						continue;
					}
					case 13:
						goto IL_14F;
					}
					goto IL_58;
					IL_14F:
					sprẄ = (ptg as sprẄ);
					num = 9;
					continue;
					IL_1EC:
					a_3 = formulaToken;
					ptg = FormulaUtil.ᜁ(a_3);
					num = 13;
				}
				IL_14A:
				goto IL_23C;
				IL_78:
				if (false)
				{
				}
				ptg = null;
				text = this.ᜅ.ᜎ();
				num2 = text.LastIndexOf('!', text.Length - 2);
				num = 2;
				goto IL_19;
			}
			return ptg;
		}
		}
	}

	// Token: 0x06002375 RID: 9077 RVA: 0x0014BD04 File Offset: 0x0014AD04
	public List<Ptg> ᜀ()
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
		return this.ᜆ;
	}

	// Token: 0x06002376 RID: 9078 RVA: 0x0014BD48 File Offset: 0x0014AD48
	public NumberFormatInfo ᜁ()
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
		return this.ᜅ.\u170D();
	}

	// Token: 0x06002377 RID: 9079 RVA: 0x0014BD90 File Offset: 0x0014AD90
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
		this.ᜅ.ᜀ(A_0);
	}

	// Token: 0x04001223 RID: 4643
	private const string ᜀ = "IF";

	// Token: 0x04001224 RID: 4644
	private const int ᜁ = 64;

	// Token: 0x04001225 RID: 4645
	private const int ᜂ = 256;

	// Token: 0x04001226 RID: 4646
	private const int ᜃ = 32738;

	// Token: 0x04001227 RID: 4647
	private const char ᜄ = '$';

	// Token: 0x04001228 RID: 4648
	private spr\u2291 ᜅ;

	// Token: 0x04001229 RID: 4649
	private List<Ptg> ᜆ;

	// Token: 0x0400122A RID: 4650
	private Stack<sprᯡ> ᜇ;

	// Token: 0x0400122B RID: 4651
	private XlsWorkbook ᜈ;
}
