using System;
using System.Collections.Generic;
using System.Globalization;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Parser.Biff_Records.Formula;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x0200039A RID: 922
[spr\u2400(FormulaToken.tUnaryMinus, "-")]
[spr\u2400(FormulaToken.tUnaryPlus, "+")]
[spr\u2400(FormulaToken.tPercent, "%", true)]
internal class spr\u23FA : sprឯ
{
	// Token: 0x06003832 RID: 14386 RVA: 0x001F6A18 File Offset: 0x001F5A18
	static spr\u23FA()
	{
		int a_ = 6;
		for (;;)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					spr\u23FA.ᜁ = new Dictionary<string, spr\u2400>(3);
					spr\u23FA.ᜀ = new spr\u2400[]
					{
						new spr\u2400(FormulaToken.tUnaryMinus, RecordTableEnumerator.b("ᄻ", a_)),
						new spr\u2400(FormulaToken.tUnaryPlus, RecordTableEnumerator.b("᜻", a_)),
						new spr\u2400(FormulaToken.tPercent, RecordTableEnumerator.b("᤻", a_), true)
					};
					int num = 0;
					int num2 = spr\u23FA.ᜀ.Length;
					if (true)
					{
					}
					int num3 = 1;
					for (;;)
					{
						switch (num3)
						{
						case 0:
							goto IL_CD;
						case 1:
							goto IL_B1;
						case 2:
						{
							if (num >= num2)
							{
								num3 = 0;
								continue;
							}
							spr\u2400 spr_u = spr\u23FA.ᜀ[num];
							spr\u23FA.ᜁ.Add(spr_u.ᜂ(), spr_u);
							num++;
							num3 = 3;
							continue;
						}
						case 3:
							goto IL_B1;
						}
						break;
						IL_B1:
						num3 = 2;
					}
				}
				IL_CD:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_110;
				}
				break;
			}
		}
		IL_110:
		if (false)
		{
		}
	}

	// Token: 0x06003833 RID: 14387 RVA: 0x001F6B48 File Offset: 0x001F5B48
	public static FormulaToken ᜀ(string A_0)
	{
		int a_ = 15;
		for (;;)
		{
			IL_09:
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_46;
				case 1:
					if (A_0.Length != 0)
					{
						goto IL_A6;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_09;
					default:
						if (false)
						{
						}
						num = 3;
						continue;
					}
					break;
				case 3:
					goto IL_90;
				}
				if (A_0 == null)
				{
					if (true)
					{
					}
					num = 0;
				}
				else
				{
					num = 1;
				}
			}
		}
		IL_46:
		throw new ArgumentNullException(RecordTableEnumerator.b("⩄㝆ⱈ㥊ⱌ㭎㡐㱒㭔іじ㱚㍜", a_));
		IL_90:
		throw new ArgumentException(RecordTableEnumerator.b("⩄㝆ⱈ㥊ⱌ㭎㡐㱒㭔іじ㱚㍜罞䱠䍢ᙤ፦᭨ɪͬ࡮兰ၲᑴ᥶᝸ᑺॼ彾ꖄﮊ歷", a_));
		IL_A6:
		spr\u2400 spr_u = spr\u23FA.ᜁ[A_0];
		return spr_u.ᜀ();
	}

	// Token: 0x06003834 RID: 14388 RVA: 0x001F6C10 File Offset: 0x001F5C10
	public spr\u23FA()
	{
	}

	// Token: 0x06003835 RID: 14389 RVA: 0x001F6C24 File Offset: 0x001F5C24
	public spr\u23FA(string A_0)
	{
		int a_ = 19;
		base..ctor();
		spr\u2400 spr_u;
		if (!spr\u23FA.ᜁ.TryGetValue(A_0, out spr_u))
		{
			spr\u2400[] array = this.ᜀ();
			if (array == null)
			{
				throw new ArgumentNullException(RecordTableEnumerator.b("᱈╊♌ⅎ㹐⑒㭔睖㙘⭚㡜ⵞ`ᝢ౤ࡦݨ", a_));
			}
			int num = array.Length;
			int i = 0;
			while (i < num)
			{
				spr_u = array[i];
				if (!(spr_u.ᜂ() == A_0))
				{
					i++;
				}
				else
				{
					IL_60:
					if (i == num)
					{
						throw new ArgumentNullException(RecordTableEnumerator.b("᱈╊♌ⅎ㹐⑒㭔睖㙘⭚㡜ⵞ`ᝢ౤ࡦݨ䕪", a_));
					}
					goto IL_82;
				}
			}
			goto IL_60;
		}
		IL_82:
		base.ᜁ(A_0);
		this.TokenCode = spr_u.ᜀ();
		base.ᜈ(spr_u.ᜁ());
	}

	// Token: 0x06003836 RID: 14390 RVA: 0x001F6CD4 File Offset: 0x001F5CD4
	public spr\u23FA(DataProvider A_0, int A_1, ExcelVersion A_2) : base(A_0, A_1, A_2)
	{
	}

	// Token: 0x06003837 RID: 14391 RVA: 0x001F6CEC File Offset: 0x001F5CEC
	public override TOperation ᜂ()
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
		return TOperation.TYPE_UNARY;
	}

	// Token: 0x06003838 RID: 14392 RVA: 0x001F6D28 File Offset: 0x001F5D28
	protected override spr\u2400[] ᜀ()
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
		return spr\u23FA.ᜀ;
	}

	// Token: 0x06003839 RID: 14393 RVA: 0x001F6D68 File Offset: 0x001F5D68
	public override int ᜀ(ExcelVersion A_0)
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
		return 1;
	}

	// Token: 0x0600383A RID: 14394 RVA: 0x001F6DA4 File Offset: 0x001F5DA4
	public override void ᜀ(FormulaUtil A_0, Stack<object> A_1, bool A_2)
	{
		int a_ = 1;
		int num = 3;
		string text;
		string text2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_B4;
			case 1:
				goto IL_46;
			case 2:
				if (base.\u1714())
				{
					num = 0;
					continue;
				}
				goto IL_CA;
			}
			if (A_1 == null)
			{
				if (true)
				{
				}
				num = 1;
			}
			else
			{
				FormulaUtil.PushOperandToStack(A_1, this.ToString());
				text = (string)A_1.Pop();
				text2 = (string)A_1.Pop();
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_CA;
				default:
					if (false)
					{
					}
					num = 2;
					break;
				}
			}
		}
		IL_46:
		throw new ArgumentNullException(RecordTableEnumerator.b("堶䤸帺似帾⽀❂㙄", a_));
		IL_B4:
		A_1.Push(text2 + text);
		return;
		IL_CA:
		A_1.Push(text + text2);
	}

	// Token: 0x0600383B RID: 14395 RVA: 0x001F6E88 File Offset: 0x001F5E88
	public override string ᜀ(FormulaUtil A_0, int A_1, int A_2, bool A_3, NumberFormatInfo A_4, bool A_5)
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
		return base.\u1712();
	}

	// Token: 0x0600383C RID: 14396 RVA: 0x001F6ECC File Offset: 0x001F5ECC
	public override string[] ᜀ(string A_0, ref int A_1, FormulaUtil A_2)
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
		string[] array = new string[]
		{
			A_2.GetRightUnaryOperand(A_0, A_1)
		};
		A_1 += array[0].Length + this.ToString().Length;
		return array;
	}

	// Token: 0x040018CB RID: 6347
	private new static readonly spr\u2400[] ᜀ;

	// Token: 0x040018CC RID: 6348
	private static readonly Dictionary<string, spr\u2400> ᜁ;
}
