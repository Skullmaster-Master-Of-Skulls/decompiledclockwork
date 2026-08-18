using System;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Parser.Biff_Records.ObjRecords;

// Token: 0x020002B0 RID: 688
[CLSCompliant(false)]
internal class spr\u2437 : spr\u25AD
{
	// Token: 0x060029C1 RID: 10689 RVA: 0x00178614 File Offset: 0x00177614
	[CLSCompliant(false)]
	public spr\u2437(TObjSubRecordType A_0, ushort A_1, byte[] A_2) : base(A_0, A_1, A_2)
	{
	}

	// Token: 0x060029C2 RID: 10690 RVA: 0x0017862C File Offset: 0x0017762C
	public new byte[] ᜀ()
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

	// Token: 0x060029C3 RID: 10691 RVA: 0x00178670 File Offset: 0x00177670
	protected override void ᜀ(byte[] A_0)
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
		this.ᜀ = new byte[(int)base.ᜎ()];
		Array.Copy(A_0, 0, this.ᜀ, 0, (int)base.ᜎ());
	}

	// Token: 0x060029C4 RID: 10692 RVA: 0x001786D0 File Offset: 0x001776D0
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
		A_0.WriteInt16(A_1, (short)base.ᜎ());
		A_1 += 2;
		A_0.WriteBytes(A_1, this.ᜀ, 0, this.ᜀ.Length);
	}

	// Token: 0x060029C5 RID: 10693 RVA: 0x00178748 File Offset: 0x00177748
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
		spr\u2437 spr_u = (spr\u2437)base.ᜁ();
		spr_u.ᜀ = spr\u1CD3.ᜀ(this.ᜀ);
		return spr_u;
	}

	// Token: 0x060029C6 RID: 10694 RVA: 0x001787A4 File Offset: 0x001777A4
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
		return (int)(base.ᜎ() + 4);
	}

	// Token: 0x040013E6 RID: 5094
	private new byte[] ᜀ;
}
