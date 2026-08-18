using System;
using Spire.DataExport.XLS.Formula;

// Token: 0x02000063 RID: 99
internal class spr\u25EB : sprạ
{
	// Token: 0x06000333 RID: 819 RVA: 0x0001EAE8 File Offset: 0x0001DAE8
	public ushort ᜂ()
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

	// Token: 0x06000334 RID: 820 RVA: 0x0001EB2C File Offset: 0x0001DB2C
	public spr\u25EB() : base(FormulaTokenCode.Int, 3, FormulaTokenType.Operand)
	{
	}

	// Token: 0x06000335 RID: 821 RVA: 0x0001EB44 File Offset: 0x0001DB44
	public override void ᜀ(object[] A_0)
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
		this.ᜀ = (ushort)A_0[0];
	}

	// Token: 0x06000336 RID: 822 RVA: 0x0001EB90 File Offset: 0x0001DB90
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
		this.ᜀ = BitConverter.ToUInt16(A_0, A_1);
	}

	// Token: 0x06000337 RID: 823 RVA: 0x0001EBD8 File Offset: 0x0001DBD8
	public override byte[] ᜁ()
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
		byte[] array = base.ᜁ();
		BitConverter.GetBytes(this.ᜂ()).CopyTo(array, 1);
		return array;
	}

	// Token: 0x06000338 RID: 824 RVA: 0x0001EC30 File Offset: 0x0001DC30
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
		return this.ᜀ.ToString();
	}

	// Token: 0x04000257 RID: 599
	private new ushort ᜀ;
}
