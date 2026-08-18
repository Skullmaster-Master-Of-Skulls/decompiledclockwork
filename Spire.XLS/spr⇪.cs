using System;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Parser.Biff_Records.ObjRecords;

// Token: 0x020004DF RID: 1247
[CLSCompliant(false)]
internal class spr\u21EA : spr\u25AD
{
	// Token: 0x06004C9D RID: 19613 RVA: 0x002ECF8C File Offset: 0x002EBF8C
	public new short ᜀ()
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

	// Token: 0x06004C9E RID: 19614 RVA: 0x002ECFD0 File Offset: 0x002EBFD0
	[CLSCompliant(false)]
	public spr\u21EA(TObjSubRecordType A_0, ushort A_1, byte[] A_2) : base(A_0, A_1, A_2)
	{
	}

	// Token: 0x06004C9F RID: 19615 RVA: 0x002ECFE8 File Offset: 0x002EBFE8
	protected override void ᜀ(byte[] A_0)
	{
		for (;;)
		{
			IL_00:
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_78;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_00;
					default:
						if (false)
						{
						}
						base.ᜂ(2);
						A_0 = new byte[(int)base.ᜎ()];
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
					goto IL_7A;
				}
				num = 2;
			}
		}
		IL_78:
		IL_7A:
		this.ᜂ = (byte[])A_0.Clone();
		this.ᜁ = BiffRecordRaw.GetInt16(A_0, 0);
	}

	// Token: 0x06004CA0 RID: 19616 RVA: 0x002ED090 File Offset: 0x002EC090
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

	// Token: 0x06004CA1 RID: 19617 RVA: 0x002ED104 File Offset: 0x002EC104
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
		spr\u21EA spr_u21EA = (spr\u21EA)base.ᜁ();
		spr_u21EA.ᜂ = spr\u1CD3.ᜀ(this.ᜂ);
		return spr_u21EA;
	}

	// Token: 0x06004CA2 RID: 19618 RVA: 0x002ED160 File Offset: 0x002EC160
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

	// Token: 0x040022E4 RID: 8932
	internal new const int ᜀ = 6;

	// Token: 0x040022E5 RID: 8933
	private new short ᜁ;

	// Token: 0x040022E6 RID: 8934
	private byte[] ᜂ;
}
