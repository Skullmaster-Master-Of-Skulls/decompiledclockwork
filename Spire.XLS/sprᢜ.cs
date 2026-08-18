using System;
using System.Drawing;
using System.Globalization;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Parser.Biff_Records.Formula;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x02000536 RID: 1334
[spr\u2400(FormulaToken.tMemArea3)]
[spr\u2400(FormulaToken.tMemArea2)]
[spr\u2400(FormulaToken.tMemArea1)]
internal class sprᢜ : Ptg, sprḝ
{
	// Token: 0x0600515A RID: 20826 RVA: 0x0032E278 File Offset: 0x0032D278
	public sprᢜ()
	{
	}

	// Token: 0x0600515B RID: 20827 RVA: 0x0032E28C File Offset: 0x0032D28C
	public sprᢜ(DataProvider A_0, int A_1, ExcelVersion A_2) : base(A_0, A_1, A_2)
	{
	}

	// Token: 0x0600515C RID: 20828 RVA: 0x0032E2A4 File Offset: 0x0032D2A4
	public sprᢜ(string A_0)
	{
	}

	// Token: 0x0600515D RID: 20829 RVA: 0x0032E2B8 File Offset: 0x0032D2B8
	public ushort ᜀ()
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

	// Token: 0x0600515E RID: 20830 RVA: 0x0032E2FC File Offset: 0x0032D2FC
	public Ptg[] ᜂ()
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
		return this.ᜄ;
	}

	// Token: 0x0600515F RID: 20831 RVA: 0x0032E340 File Offset: 0x0032D340
	public Rectangle[] ᜃ()
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
		return this.ᜅ;
	}

	// Token: 0x06005160 RID: 20832 RVA: 0x0032E384 File Offset: 0x0032D384
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
		return (int)(this.ᜃ + 7);
	}

	// Token: 0x06005161 RID: 20833 RVA: 0x0032E3C8 File Offset: 0x0032D3C8
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
		return base.ToByteArray(A_0);
	}

	// Token: 0x06005162 RID: 20834 RVA: 0x0032E40C File Offset: 0x0032D40C
	public virtual string ᜀ(FormulaUtil A_0, int A_1, int A_2, bool A_3, NumberFormatInfo A_4, bool A_5)
	{
		int a_ = 15;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		return RecordTableEnumerator.b("ࡄ≆⑈੊㽌⩎ぐ", a_);
	}

	// Token: 0x06005163 RID: 20835 RVA: 0x0032E460 File Offset: 0x0032D460
	public int ᜀ(DataProvider A_0, int A_1)
	{
		switch (0)
		{
		default:
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_F5:
				goto IL_7B;
			default:
				if (false)
				{
				}
				goto IL_55;
			}
			int num;
			int num2;
			ushort num3;
			for (;;)
			{
				IL_36:
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					goto IL_F5;
				case 1:
					return A_1;
				case 2:
				{
					if (num2 >= (int)num3)
					{
						num = 1;
						continue;
					}
					ushort top = A_0.ReadUInt16(A_1);
					A_1 += 2;
					ushort bottom = A_0.ReadUInt16(A_1);
					A_1 += 2;
					ushort left = A_0.ReadUInt16(A_1);
					A_1 += 2;
					ushort right = A_0.ReadUInt16(A_1);
					A_1 += 2;
					this.ᜅ[num2] = Rectangle.FromLTRB((int)left, (int)top, (int)right, (int)bottom);
					num2++;
					num = 0;
					continue;
				}
				case 3:
					goto IL_79;
				}
				goto IL_55;
			}
			IL_79:
			goto IL_7B;
			IL_55:
			num3 = A_0.ReadUInt16(A_1);
			A_1 += 2;
			this.ᜅ = new Rectangle[(int)num3];
			num2 = 0;
			num = 3;
			goto IL_36;
			IL_7B:
			num = 2;
			goto IL_36;
		}
		}
	}

	// Token: 0x06005164 RID: 20836 RVA: 0x0032E568 File Offset: 0x0032D568
	public int ᜄ()
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
					goto IL_72;
				default:
					if (false)
					{
					}
					break;
				}
				break;
			case 1:
				goto IL_7A;
			case 2:
				goto IL_68;
			case 3:
				if (true)
				{
				}
				goto IL_72;
			}
			if (this.ᜅ == null)
			{
				num = 3;
				continue;
			}
			num = 2;
			continue;
			IL_72:
			num = 1;
		}
		IL_68:
		int num2 = this.ᜅ.Length;
		goto IL_7D;
		IL_7A:
		num2 = 0;
		IL_7D:
		int num3 = num2;
		return 2 + num3 * 8;
	}

	// Token: 0x06005165 RID: 20837 RVA: 0x0032E5F8 File Offset: 0x0032D5F8
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
		this.ᜂ = A_0.ReadInt32(A_1);
		A_1 += 4;
		this.ᜃ = A_0.ReadUInt16(A_1);
		A_1 += 2;
		int num;
		this.ᜄ = FormulaUtil.ᜀ(A_0, A_1, (int)this.ᜃ, out num, A_2);
		A_1 += (int)this.ᜃ;
	}

	// Token: 0x04002452 RID: 9298
	private const int ᜀ = 8;

	// Token: 0x04002453 RID: 9299
	private const int ᜁ = 7;

	// Token: 0x04002454 RID: 9300
	private int ᜂ;

	// Token: 0x04002455 RID: 9301
	private ushort ᜃ;

	// Token: 0x04002456 RID: 9302
	private Ptg[] ᜄ;

	// Token: 0x04002457 RID: 9303
	private Rectangle[] ᜅ;
}
