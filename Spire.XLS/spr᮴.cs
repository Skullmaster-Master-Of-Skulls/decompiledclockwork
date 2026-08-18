using System;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Parser.Biff_Records.ObjRecords;

// Token: 0x0200044A RID: 1098
internal class spr᮴ : spr\u25AD
{
	// Token: 0x06004221 RID: 16929 RVA: 0x00251244 File Offset: 0x00250244
	public new bool ᜀ()
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

	// Token: 0x06004222 RID: 16930 RVA: 0x00251288 File Offset: 0x00250288
	[CLSCompliant(false)]
	public spr᮴(TObjSubRecordType A_0, ushort A_1, byte[] A_2) : base(A_0, A_1, A_2)
	{
	}

	// Token: 0x06004223 RID: 16931 RVA: 0x002512A0 File Offset: 0x002502A0
	protected override void ᜀ(byte[] A_0)
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_7C;
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
					base.ᜂ(2);
					this.ᜂ = new byte[(int)base.ᜎ()];
					num = 0;
					continue;
				}
				break;
			}
			if (true)
			{
			}
			if (base.ᜎ() != 0)
			{
				break;
			}
			num = 2;
		}
		IL_7C:
		this.ᜂ = (byte[])A_0.Clone();
		this.ᜁ = BiffRecordRaw.GetBit(A_0, 0, 5);
	}

	// Token: 0x06004224 RID: 16932 RVA: 0x0025134C File Offset: 0x0025034C
	public override void ᜀ(DataProvider A_0, int A_1)
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
		A_0.WriteInt16(A_1, (short)base.ᜏ());
		A_1 += 2;
		A_0.WriteInt16(A_1, 2);
		A_1 += 2;
		A_0.WriteBytes(A_1, this.ᜂ, 0, this.ᜂ.Length);
	}

	// Token: 0x06004225 RID: 16933 RVA: 0x002513C0 File Offset: 0x002503C0
	public override object ᜁ()
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
		spr᮴ spr᮴ = (spr᮴)base.ᜁ();
		spr᮴.ᜂ = spr\u1CD3.ᜀ(this.ᜂ);
		return spr᮴;
	}

	// Token: 0x06004226 RID: 16934 RVA: 0x0025141C File Offset: 0x0025041C
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
		return 6;
	}

	// Token: 0x04001D44 RID: 7492
	private new const int ᜀ = 6;

	// Token: 0x04001D45 RID: 7493
	private new bool ᜁ;

	// Token: 0x04001D46 RID: 7494
	private byte[] ᜂ;
}
