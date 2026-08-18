using System;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Parser.Biff_Records.ObjRecords;

// Token: 0x02000508 RID: 1288
[CLSCompliant(false)]
internal class spr\u2285 : spr\u25AD
{
	// Token: 0x06004E6D RID: 20077 RVA: 0x002FAAC4 File Offset: 0x002F9AC4
	public new string ᜀ()
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
		return this.ᜅ;
	}

	// Token: 0x06004E6E RID: 20078 RVA: 0x002FAB08 File Offset: 0x002F9B08
	public new void ᜀ(string A_0)
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
		this.ᜅ = A_0;
	}

	// Token: 0x06004E6F RID: 20079 RVA: 0x002FAB4C File Offset: 0x002F9B4C
	public spr\u2285() : base(TObjSubRecordType.ftPictFmla)
	{
		int num = spr\u2285.ᜁ.Length;
		this.ᜃ = new byte[num];
		Buffer.BlockCopy(spr\u2285.ᜁ, 0, this.ᜃ, 0, num);
		num = spr\u2285.ᜂ.Length;
		this.ᜄ = new byte[num];
		Buffer.BlockCopy(spr\u2285.ᜂ, 0, this.ᜄ, 0, num);
	}

	// Token: 0x06004E70 RID: 20080 RVA: 0x002FABB0 File Offset: 0x002F9BB0
	public spr\u2285(TObjSubRecordType A_0, ushort A_1, byte[] A_2) : base(A_0, A_1, A_2)
	{
	}

	// Token: 0x06004E71 RID: 20081 RVA: 0x002FABC8 File Offset: 0x002F9BC8
	protected override void ᜀ(byte[] A_0)
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
		this.ᜃ = new byte[14];
		Buffer.BlockCopy(A_0, 0, this.ᜃ, 0, 14);
		int num = 14;
		this.ᜅ = BiffRecordRaw.GetString16BitUpdateOffset(A_0, ref num);
		int num2 = A_0.Length - num;
		this.ᜄ = new byte[num2];
		Buffer.BlockCopy(A_0, num, this.ᜄ, 0, num2);
	}

	// Token: 0x06004E72 RID: 20082 RVA: 0x002FAC54 File Offset: 0x002F9C54
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
		int num = this.ᜀ(ExcelVersion.Version97to2003) - 4;
		A_0.WriteInt16(A_1, (short)num);
		A_1 += 2;
		num = this.ᜃ.Length;
		A_0.WriteBytes(A_1, this.ᜃ, 0, num);
		A_1 += num;
		A_1 += A_0.WriteString16Bit(A_1, this.ᜅ, false);
		num = this.ᜄ.Length;
		A_0.WriteBytes(A_1, this.ᜄ, 0, num);
		A_1 += num;
	}

	// Token: 0x06004E73 RID: 20083 RVA: 0x002FAD08 File Offset: 0x002F9D08
	public override object ᜁ()
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
		spr\u2285 spr_u = (spr\u2285)base.ᜁ();
		spr_u.ᜃ = spr\u1CD3.ᜀ(this.ᜃ);
		spr_u.ᜄ = spr\u1CD3.ᜀ(this.ᜄ);
		return spr_u;
	}

	// Token: 0x06004E74 RID: 20084 RVA: 0x002FAD74 File Offset: 0x002F9D74
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
		return 4 + this.ᜄ.Length + this.ᜃ.Length + 3 + this.ᜅ.Length;
	}

	// Token: 0x06004E75 RID: 20085 RVA: 0x002FADD0 File Offset: 0x002F9DD0
	// Note: this type is marked as 'beforefieldinit'.
	static spr\u2285()
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
		spr\u2285.ᜁ = new byte[]
		{
			30,
			0,
			5,
			0,
			12,
			151,
			65,
			7,
			2,
			8,
			8,
			232,
			7,
			3
		};
		byte[] array = new byte[16];
		array[4] = 68;
		spr\u2285.ᜂ = array;
	}

	// Token: 0x04002377 RID: 9079
	private new const int ᜀ = 14;

	// Token: 0x04002378 RID: 9080
	private new static readonly byte[] ᜁ;

	// Token: 0x04002379 RID: 9081
	private static readonly byte[] ᜂ;

	// Token: 0x0400237A RID: 9082
	private byte[] ᜃ;

	// Token: 0x0400237B RID: 9083
	private byte[] ᜄ;

	// Token: 0x0400237C RID: 9084
	private string ᜅ;
}
