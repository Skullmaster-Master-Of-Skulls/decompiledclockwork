using System;
using Spire.DataExport.XLS.Formula;

// Token: 0x02000093 RID: 147
internal class spr\u2341 : sprạ
{
	// Token: 0x0600047D RID: 1149 RVA: 0x0002BD90 File Offset: 0x0002AD90
	public spr\u2487 ᜃ()
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

	// Token: 0x0600047E RID: 1150 RVA: 0x0002BDD4 File Offset: 0x0002ADD4
	public byte ᜂ()
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

	// Token: 0x0600047F RID: 1151 RVA: 0x0002BE18 File Offset: 0x0002AE18
	public spr\u2341(FormulaTokenCode A_0) : base(A_0, 4, FormulaTokenType.Function)
	{
	}

	// Token: 0x06000480 RID: 1152 RVA: 0x0002BE30 File Offset: 0x0002AE30
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
		this.ᜀ = spr\u2006.ᜀ().ᜀ(A_0[0] as string);
		this.ᜁ = (byte)A_0[1];
	}

	// Token: 0x06000481 RID: 1153 RVA: 0x0002BE94 File Offset: 0x0002AE94
	public override void ᜀ(byte[] A_0, int A_1)
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
		this.ᜁ = A_0[A_1];
		ushort a_ = BitConverter.ToUInt16(A_0, A_1 + 1);
		this.ᜀ = spr\u2006.ᜀ().ᜀ(a_);
	}

	// Token: 0x06000482 RID: 1154 RVA: 0x0002BEF4 File Offset: 0x0002AEF4
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
		byte[] array = base.ᜁ();
		array[1] = this.ᜁ;
		BitConverter.GetBytes(this.ᜀ.ᜁ()).CopyTo(array, 2);
		return array;
	}

	// Token: 0x06000483 RID: 1155 RVA: 0x0002BF58 File Offset: 0x0002AF58
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
		return this.ᜀ.ᜄ().ToUpper();
	}

	// Token: 0x040002C2 RID: 706
	private new spr\u2487 ᜀ;

	// Token: 0x040002C3 RID: 707
	private new byte ᜁ;
}
