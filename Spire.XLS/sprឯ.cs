using System;
using System.Collections.Generic;
using System.Globalization;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Parser.Biff_Records.Formula;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x020002B5 RID: 693
internal abstract class sprឯ : Ptg
{
	// Token: 0x060029F9 RID: 10745 RVA: 0x001798E0 File Offset: 0x001788E0
	public sprឯ()
	{
	}

	// Token: 0x060029FA RID: 10746 RVA: 0x00179900 File Offset: 0x00178900
	protected sprឯ(DataProvider A_0, int A_1, ExcelVersion A_2) : base(A_0, A_1, A_2)
	{
	}

	// Token: 0x060029FB RID: 10747 RVA: 0x00179924 File Offset: 0x00178924
	public virtual bool \u1713()
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
		return true;
	}

	// Token: 0x060029FC RID: 10748
	public abstract TOperation ᜂ();

	// Token: 0x060029FD RID: 10749 RVA: 0x00179960 File Offset: 0x00178960
	public virtual int ᜃ()
	{
		for (;;)
		{
			IL_14:
			TOperation toperation = this.ᜂ();
			for (;;)
			{
				IL_1B:
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return 0;
					case 1:
						if (true)
						{
						}
						switch (toperation)
						{
						case TOperation.TYPE_UNARY:
							return 2;
						case TOperation.TYPE_BINARY:
							return 1;
						default:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_1B;
							default:
								if (false)
								{
								}
								num = 2;
								continue;
							}
							break;
						}
						break;
					case 2:
						num = 0;
						continue;
					}
					goto IL_14;
				}
			}
		}
		return 2;
	}

	// Token: 0x060029FE RID: 10750 RVA: 0x001799E8 File Offset: 0x001789E8
	public string \u1712()
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
		return this.ᜁ;
	}

	// Token: 0x060029FF RID: 10751 RVA: 0x00179A2C File Offset: 0x00178A2C
	public void ᜁ(string A_0)
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
		this.ᜁ = A_0;
	}

	// Token: 0x06002A00 RID: 10752 RVA: 0x00179A70 File Offset: 0x00178A70
	public bool \u1714()
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
		return this.ᜂ;
	}

	// Token: 0x06002A01 RID: 10753 RVA: 0x00179AB4 File Offset: 0x00178AB4
	public void ᜈ(bool A_0)
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
		this.ᜂ = A_0;
	}

	// Token: 0x06002A02 RID: 10754
	protected abstract spr\u2400[] ᜀ();

	// Token: 0x06002A03 RID: 10755 RVA: 0x00179AF8 File Offset: 0x00178AF8
	public virtual void ᜀ(Stack<object> A_0)
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
		this.ᜀ(null, A_0, false);
	}

	// Token: 0x06002A04 RID: 10756
	public abstract void ᜀ(FormulaUtil A_0, Stack<object> A_1, bool A_2);

	// Token: 0x06002A05 RID: 10757 RVA: 0x00179B3C File Offset: 0x00178B3C
	public virtual string ᜀ(FormulaUtil A_0, int A_1, int A_2, bool A_3, NumberFormatInfo A_4, bool A_5)
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
		return this.ᜁ;
	}

	// Token: 0x06002A06 RID: 10758
	public abstract string[] ᜀ(string A_0, ref int A_1, FormulaUtil A_2);

	// Token: 0x06002A07 RID: 10759 RVA: 0x00179B80 File Offset: 0x00178B80
	public virtual ParseFormulaOptions ᜀ(ParseFormulaOptions A_0)
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
		return A_0 | ParseFormulaOptions.ParseComplexOperand;
	}

	// Token: 0x06002A08 RID: 10760 RVA: 0x00179BC0 File Offset: 0x00178BC0
	protected string ᜀ(FormulaUtil A_0)
	{
		int a_ = 5;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			goto IL_34;
		}
		if (false)
		{
		}
		if (A_0 == null)
		{
			return RecordTableEnumerator.b("᜺", a_);
		}
		IL_34:
		if (true)
		{
		}
		return A_0.OperandsSeparator;
	}

	// Token: 0x06002A09 RID: 10761 RVA: 0x00179C20 File Offset: 0x00178C20
	public virtual void ᜀ(DataProvider A_0, ref int A_1, ExcelVersion A_2)
	{
		switch (0)
		{
		default:
		{
			spr\u2400 spr_u;
			for (;;)
			{
				IL_3F:
				spr\u2400[] array = this.ᜀ();
				for (;;)
				{
					IL_46:
					int num = 4;
					for (;;)
					{
						if (true)
						{
						}
						switch (num)
						{
						case 0:
							goto IL_B2;
						case 1:
							goto IL_B5;
						case 2:
							if (spr_u.ᜀ() != this.TokenCode)
							{
								int num2;
								num2++;
								num = 1;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_46;
							default:
								if (false)
								{
								}
								num = 0;
								continue;
							}
							break;
						case 3:
							return;
						case 4:
						{
							if (array == null)
							{
								num = 3;
								continue;
							}
							int num2 = 0;
							int num3 = array.Length;
							num = 5;
							continue;
						}
						case 5:
							goto IL_B5;
						case 6:
							return;
						case 7:
						{
							int num2;
							int num3;
							if (num2 >= num3)
							{
								num = 6;
								continue;
							}
							spr_u = array[num2];
							num = 2;
							continue;
						}
						}
						goto IL_3F;
						IL_B5:
						num = 7;
					}
				}
			}
			return;
			IL_B2:
			this.ᜁ = spr_u.ᜂ();
			this.ᜂ = spr_u.ᜁ();
			return;
		}
		}
	}

	// Token: 0x040013EF RID: 5103
	private const string ᜀ = ",";

	// Token: 0x040013F0 RID: 5104
	private string ᜁ = string.Empty;

	// Token: 0x040013F1 RID: 5105
	private bool ᜂ;
}
