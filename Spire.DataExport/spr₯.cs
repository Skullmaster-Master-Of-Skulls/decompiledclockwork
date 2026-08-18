using System;
using Spire.DataExport.CollectionEditors;
using Spire.DataExport.XLS.Formula;

// Token: 0x02000062 RID: 98
internal class spr\u20AF : sprạ
{
	// Token: 0x0600032E RID: 814 RVA: 0x0001E994 File Offset: 0x0001D994
	public spr\u20AF(FormulaTokenCode A_0) : base(A_0, 5, FormulaTokenType.Operand)
	{
	}

	// Token: 0x0600032F RID: 815 RVA: 0x0001E9AC File Offset: 0x0001D9AC
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
		object obj = A_0[0];
		object obj2 = A_0[1];
	}

	// Token: 0x06000330 RID: 816 RVA: 0x0001E9F0 File Offset: 0x0001D9F0
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
		this.ᜀ = BitConverter.ToUInt16(A_0, A_1 + 1);
	}

	// Token: 0x06000331 RID: 817 RVA: 0x0001EA3C File Offset: 0x0001DA3C
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
		BitConverter.GetBytes(this.ᜀ).CopyTo(array, 1);
		return array;
	}

	// Token: 0x06000332 RID: 818 RVA: 0x0001EA94 File Offset: 0x0001DA94
	public override string ᜀ()
	{
		int a_ = 7;
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		return HyperlinksCollectionEditor.b("洢䐤䨦䰨", a_);
	}

	// Token: 0x04000256 RID: 598
	private new ushort ᜀ;
}
