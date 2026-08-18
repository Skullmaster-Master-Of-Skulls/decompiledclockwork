using System;
using System.Collections.Generic;
using System.Globalization;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Parser.Biff_Records.Formula;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x020004E4 RID: 1252
[spr\u1CD7("#NAME?", 29)]
[spr\u2400(FormulaToken.tError)]
[spr\u1CD7("#NULL!", 0)]
[spr\u1CD7("#NUM!", 36)]
[spr\u1CD7("#N/A", 42)]
[spr\u1CD7("#DIV/0!", 7)]
[spr\u1CD7("#VALUE!", 15)]
internal class sprἪ : Ptg
{
	// Token: 0x06004CC9 RID: 19657 RVA: 0x002EE5CC File Offset: 0x002ED5CC
	static sprἪ()
	{
		int a_ = 5;
		switch (0)
		{
		default:
			for (;;)
			{
				sprἪ.ᜁ = new Dictionary<string, int>(6);
				sprἪ.ᜂ = new Dictionary<int, string>(6);
				spr\u1CD7[] array = new spr\u1CD7[]
				{
					new spr\u1CD7(RecordTableEnumerator.b("ᠺ猼樾ീག摄", a_), 0),
					new spr\u1CD7(RecordTableEnumerator.b("ᠺ礼瘾ᝀ求畄晆", a_), 7),
					new spr\u1CD7(RecordTableEnumerator.b("ᠺ欼績ീᙂD晆", a_), 15),
					new spr\u1CD7(RecordTableEnumerator.b("ᠺ猼績ీق穄", a_), 29),
					new spr\u1CD7(RecordTableEnumerator.b("ᠺ猼樾ీ扂", a_), 36),
					new spr\u1CD7(RecordTableEnumerator.b("ᠺ猼ှ@", a_), 42)
				};
				int num = 0;
				int num2 = array.Length;
				if (true)
				{
				}
				int num3 = 3;
				for (;;)
				{
					switch (num3)
					{
					case 0:
					{
						if (num >= num2)
						{
							num3 = 1;
							continue;
						}
						spr\u1CD7 spr_u1CD = array[num];
						sprἪ.ᜁ.Add(spr_u1CD.ᜁ(), spr_u1CD.ᜀ());
						sprἪ.ᜂ.Add(spr_u1CD.ᜀ(), spr_u1CD.ᜁ());
						num++;
						num3 = 2;
						continue;
					}
					case 1:
						return;
					case 2:
						goto IL_11D;
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
							goto IL_11D;
						}
						break;
					}
					break;
					IL_11D:
					num3 = 0;
				}
			}
			return;
		}
	}

	// Token: 0x06004CCA RID: 19658 RVA: 0x002EE760 File Offset: 0x002ED760
	public sprἪ()
	{
	}

	// Token: 0x06004CCB RID: 19659 RVA: 0x002EE774 File Offset: 0x002ED774
	public sprἪ(DataProvider A_0, int A_1, ExcelVersion A_2) : base(A_0, A_1, A_2)
	{
	}

	// Token: 0x06004CCC RID: 19660 RVA: 0x002EE78C File Offset: 0x002ED78C
	public sprἪ(int A_0)
	{
		this.TokenCode = FormulaToken.tError;
		this.ᜃ = (byte)A_0;
	}

	// Token: 0x06004CCD RID: 19661 RVA: 0x002EE7B0 File Offset: 0x002ED7B0
	public sprἪ(string A_0) : this(sprἪ.ᜁ[A_0])
	{
	}

	// Token: 0x06004CCE RID: 19662 RVA: 0x002EE7D0 File Offset: 0x002ED7D0
	public byte ᜀ()
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
		return this.ᜃ;
	}

	// Token: 0x06004CCF RID: 19663 RVA: 0x002EE814 File Offset: 0x002ED814
	public void ᜀ(byte A_0)
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
		this.ᜃ = A_0;
	}

	// Token: 0x06004CD0 RID: 19664 RVA: 0x002EE858 File Offset: 0x002ED858
	public virtual int ᜁ(ExcelVersion A_0)
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
		return 2;
	}

	// Token: 0x06004CD1 RID: 19665 RVA: 0x002EE894 File Offset: 0x002ED894
	public virtual string ᜀ(FormulaUtil A_0, int A_1, int A_2, bool A_3, NumberFormatInfo A_4, bool A_5)
	{
		int a_ = 8;
		string result;
		if (!sprἪ.ᜂ.TryGetValue((int)this.ᜃ, out result))
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
				break;
			}
			return RecordTableEnumerator.b("ᴽ฿流Ճ", a_);
		}
		return result;
	}

	// Token: 0x06004CD2 RID: 19666 RVA: 0x002EE900 File Offset: 0x002ED900
	public virtual byte[] ᜀ(ExcelVersion A_0)
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
		byte[] array = base.ToByteArray(A_0);
		array[1] = this.ᜃ;
		return array;
	}

	// Token: 0x06004CD3 RID: 19667 RVA: 0x002EE950 File Offset: 0x002ED950
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
		this.ᜃ = A_0.ReadByte(A_1++);
	}

	// Token: 0x040022F8 RID: 8952
	public const string ᜀ = "#N/A";

	// Token: 0x040022F9 RID: 8953
	public static readonly Dictionary<string, int> ᜁ;

	// Token: 0x040022FA RID: 8954
	public static readonly Dictionary<int, string> ᜂ;

	// Token: 0x040022FB RID: 8955
	private byte ᜃ;
}
