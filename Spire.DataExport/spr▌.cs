using System;
using System.Text;
using Spire.DataExport.XLS.Formula;

// Token: 0x0200010B RID: 267
internal class spr\u258C : sprạ
{
	// Token: 0x060005E0 RID: 1504 RVA: 0x000386A8 File Offset: 0x000376A8
	public spr\u258C() : base(FormulaTokenCode.Str, 9, FormulaTokenType.Operand)
	{
	}

	// Token: 0x060005E1 RID: 1505 RVA: 0x000386C0 File Offset: 0x000376C0
	public string ᜃ()
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

	// Token: 0x060005E2 RID: 1506 RVA: 0x00038704 File Offset: 0x00037704
	public bool ᜂ()
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

	// Token: 0x060005E3 RID: 1507 RVA: 0x00038748 File Offset: 0x00037748
	public override int ᜄ()
	{
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			goto IL_48;
		}
		if (false)
		{
		}
		int length = this.ᜁ.Length;
		if (!this.ᜂ)
		{
			if (true)
			{
			}
			return length + 3;
		}
		IL_48:
		return length * 2 + 3;
	}

	// Token: 0x060005E4 RID: 1508 RVA: 0x000387A4 File Offset: 0x000377A4
	public override void ᜀ(object[] A_0)
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
		this.ᜁ = (string)A_0[0];
	}

	// Token: 0x060005E5 RID: 1509 RVA: 0x000387F0 File Offset: 0x000377F0
	public override void ᜀ(byte[] A_0, int A_1)
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
		this.ᜂ = (A_0[A_1 + 1] == 1);
		byte a_ = A_0[A_1];
		this.ᜁ = spr\u22CE.ᜀ(this.ᜂ, A_0, A_1 + 2, (int)a_);
	}

	// Token: 0x060005E6 RID: 1510 RVA: 0x00038854 File Offset: 0x00037854
	public override byte[] ᜁ()
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
		byte[] array = new byte[this.ᜁ.Length * 2 + 3];
		array[0] = base.\u170D();
		array[1] = (byte)this.ᜁ.Length;
		array[2] = 1;
		Encoding.Unicode.GetBytes(this.ᜁ).CopyTo(array, 3);
		return array;
	}

	// Token: 0x060005E7 RID: 1511 RVA: 0x000388D8 File Offset: 0x000378D8
	public override string ᜀ()
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

	// Token: 0x04000593 RID: 1427
	public new const char ᜀ = '"';

	// Token: 0x04000594 RID: 1428
	private new string ᜁ;

	// Token: 0x04000595 RID: 1429
	private bool ᜂ;
}
