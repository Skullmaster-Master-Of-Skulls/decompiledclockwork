using System;
using System.Globalization;
using System.Text;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Parser.Biff_Records.Formula;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x020004A8 RID: 1192
[spr\u2400(FormulaToken.tStringConstant)]
internal class spr\u24A7 : Ptg
{
	// Token: 0x060049AC RID: 18860 RVA: 0x002CB020 File Offset: 0x002CA020
	public spr\u24A7()
	{
	}

	// Token: 0x060049AD RID: 18861 RVA: 0x002CB048 File Offset: 0x002CA048
	public spr\u24A7(string A_0)
	{
		this.TokenCode = FormulaToken.tStringConstant;
		this.ᜀ(A_0);
	}

	// Token: 0x060049AE RID: 18862 RVA: 0x002CB07C File Offset: 0x002CA07C
	public spr\u24A7(DataProvider A_0, int A_1, ExcelVersion A_2) : base(A_0, A_1, A_2)
	{
	}

	// Token: 0x060049AF RID: 18863 RVA: 0x002CB0A4 File Offset: 0x002CA0A4
	public string ᜀ()
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
		return this.ᜀ;
	}

	// Token: 0x060049B0 RID: 18864 RVA: 0x002CB0E8 File Offset: 0x002CA0E8
	public void ᜀ(string A_0)
	{
		int a_ = 17;
		if (A_0.Length > 255)
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
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ㅆ⡈❊㡌⩎", a_), RecordTableEnumerator.b("ᑆ㵈㥊⑌ⅎ㙐獒㱔⑖祘⽚㉜ぞ䅠ར੤०๨䕪", a_));
			}
		}
		this.ᜀ = A_0;
		this.ᜁ = 1;
	}

	// Token: 0x060049B1 RID: 18865 RVA: 0x002CB16C File Offset: 0x002CA16C
	public virtual int ᜁ(ExcelVersion A_0)
	{
		if (this.ᜁ != 1)
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
				return this.ᜀ.Length + 3;
			}
		}
		return this.ᜀ.Length * 2 + 3;
	}

	// Token: 0x060049B2 RID: 18866 RVA: 0x002CB1D0 File Offset: 0x002CA1D0
	public virtual string ᜀ(FormulaUtil A_0, int A_1, int A_2, bool A_3, NumberFormatInfo A_4, bool A_5)
	{
		int a_ = 1;
		string text;
		for (;;)
		{
			text = this.ᜀ;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_95;
			default:
			{
				if (false)
				{
				}
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_95;
					case 1:
						if (text != null)
						{
							num = 2;
							continue;
						}
						goto IL_97;
					case 2:
						text = text.Replace(RecordTableEnumerator.b("ᔶ", a_), RecordTableEnumerator.b("ᔶᬸ", a_));
						if (true)
						{
						}
						num = 0;
						continue;
					}
					break;
				}
				break;
			}
			}
		}
		IL_95:
		IL_97:
		return RecordTableEnumerator.b("ᔶ", a_) + text + RecordTableEnumerator.b("ᔶ", a_);
	}

	// Token: 0x060049B3 RID: 18867 RVA: 0x002CB298 File Offset: 0x002CA298
	public virtual byte[] ᜀ(ExcelVersion A_0)
	{
		int num = 3;
		byte[] bytes;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_70;
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_8E;
				default:
					if (false)
					{
					}
					bytes = Encoding.Unicode.GetBytes(this.ᜀ);
					num = 2;
					continue;
				}
				break;
			case 2:
				goto IL_8E;
			}
			if (this.ᜁ == 1)
			{
				num = 1;
			}
			else
			{
				bytes = BiffRecordRaw.LatinEncoding.GetBytes(this.ᜀ);
				if (true)
				{
				}
				num = 0;
			}
		}
		IL_70:
		IL_8E:
		byte[] array = new byte[bytes.Length + 3];
		array[0] = (byte)this.TokenCode;
		array[1] = (byte)this.ᜀ.Length;
		array[2] = this.ᜁ;
		bytes.CopyTo(array, 3);
		return array;
	}

	// Token: 0x060049B4 RID: 18868 RVA: 0x002CB378 File Offset: 0x002CA378
	public virtual void ᜀ(DataProvider A_0, ref int A_1, ExcelVersion A_2)
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
		this.ᜁ = A_0.ReadByte(A_1 + 1);
		int num;
		this.ᜀ = A_0.ReadString8Bit(A_1, out num);
		A_1 += num;
	}

	// Token: 0x04002172 RID: 8562
	public string ᜀ = string.Empty;

	// Token: 0x04002173 RID: 8563
	public byte ᜁ = 1;
}
