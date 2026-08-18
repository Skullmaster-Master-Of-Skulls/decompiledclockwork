using System;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Parser.Biff_Records.ObjRecords;

// Token: 0x02000567 RID: 1383
[CLSCompliant(false)]
internal class spr\u2474 : spr\u25AD
{
	// Token: 0x0600533D RID: 21309 RVA: 0x0033EE80 File Offset: 0x0033DE80
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
		return this.ᜁ;
	}

	// Token: 0x0600533E RID: 21310 RVA: 0x0033EEC4 File Offset: 0x0033DEC4
	[CLSCompliant(false)]
	public spr\u2474(TObjSubRecordType A_0, ushort A_1, byte[] A_2) : base(A_0, A_1, A_2)
	{
	}

	// Token: 0x0600533F RID: 21311 RVA: 0x0033EEDC File Offset: 0x0033DEDC
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
		this.ᜁ = (byte[])A_0.Clone();
	}

	// Token: 0x06005340 RID: 21312 RVA: 0x0033EF28 File Offset: 0x0033DF28
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
		A_0.WriteInt16(A_1, 22);
		A_1 += 2;
		A_0.WriteBytes(A_1, this.ᜁ, 0, this.ᜁ.Length);
	}

	// Token: 0x06005341 RID: 21313 RVA: 0x0033EF9C File Offset: 0x0033DF9C
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
		spr\u2474 spr_u = (spr\u2474)base.ᜁ();
		spr_u.ᜁ = spr\u1CD3.ᜀ(this.ᜁ);
		return spr_u;
	}

	// Token: 0x06005342 RID: 21314 RVA: 0x0033EFF8 File Offset: 0x0033DFF8
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
		return 26;
	}

	// Token: 0x040026FD RID: 9981
	private new const int ᜀ = 26;

	// Token: 0x040026FE RID: 9982
	private new byte[] ᜁ;
}
