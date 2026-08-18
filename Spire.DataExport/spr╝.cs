using System;
using Spire.DataExport.CollectionEditors;
using Spire.DataExport.XLS.Formula;

// Token: 0x02000066 RID: 102
internal class spr\u255D : sprạ
{
	// Token: 0x06000350 RID: 848 RVA: 0x0001F854 File Offset: 0x0001E854
	public spr\u255D() : base(FormulaTokenCode.Bool, 2, FormulaTokenType.Operand)
	{
	}

	// Token: 0x06000351 RID: 849 RVA: 0x0001F86C File Offset: 0x0001E86C
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

	// Token: 0x06000352 RID: 850 RVA: 0x0001F8B0 File Offset: 0x0001E8B0
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
		this.ᜂ = (bool)A_0[0];
	}

	// Token: 0x06000353 RID: 851 RVA: 0x0001F8FC File Offset: 0x0001E8FC
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
		this.ᜂ = BitConverter.ToBoolean(A_0, A_1);
	}

	// Token: 0x06000354 RID: 852 RVA: 0x0001F944 File Offset: 0x0001E944
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
		BitConverter.GetBytes(this.ᜂ).CopyTo(array, 1);
		return array;
	}

	// Token: 0x06000355 RID: 853 RVA: 0x0001F99C File Offset: 0x0001E99C
	public override string ᜀ()
	{
		while (!this.ᜂ)
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
				return spr\u255D.ᜁ;
			}
		}
		return spr\u255D.ᜀ;
	}

	// Token: 0x06000356 RID: 854 RVA: 0x0001F9EC File Offset: 0x0001E9EC
	// Note: this type is marked as 'beforefieldinit'.
	static spr\u255D()
	{
		int a_ = 17;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		spr\u255D.ᜀ = HyperlinksCollectionEditor.b("礬紮搰瘲", a_);
		spr\u255D.ᜁ = HyperlinksCollectionEditor.b("欬渮細怲瀴", a_);
	}

	// Token: 0x0400025F RID: 607
	public new static string ᜀ;

	// Token: 0x04000260 RID: 608
	public new static string ᜁ;

	// Token: 0x04000261 RID: 609
	private bool ᜂ;
}
