using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x02000326 RID: 806
[spr\u2593(TBIFFRecord.PageItemIndexes)]
[CLSCompliant(false)]
internal class sprᡈ : spr\u251F
{
	// Token: 0x060031C2 RID: 12738 RVA: 0x001CC3D0 File Offset: 0x001CB3D0
	public sprᡈ()
	{
	}

	// Token: 0x060031C3 RID: 12739 RVA: 0x001CC3E4 File Offset: 0x001CB3E4
	public sprᡈ(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x060031C4 RID: 12740 RVA: 0x001CC3FC File Offset: 0x001CB3FC
	public sprᡈ(int A_0) : base(A_0)
	{
	}

	// Token: 0x060031C5 RID: 12741 RVA: 0x001CC410 File Offset: 0x001CB410
	public new ushort[] ᜀ()
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

	// Token: 0x060031C6 RID: 12742 RVA: 0x001CC454 File Offset: 0x001CB454
	public new void ᜀ(ushort[] A_0)
	{
		int a_ = 11;
		if (A_0 == null)
		{
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				throw new ArgumentNullException(RecordTableEnumerator.b("㝀≂⥄㉆ⱈ", a_));
			}
		}
		this.ᜀ = A_0;
	}

	// Token: 0x060031C7 RID: 12743 RVA: 0x001CC4B8 File Offset: 0x001CB4B8
	public override void ᜂ()
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
		int num = this.m_iLength / 2;
		this.ᜀ = new ushort[num];
		Buffer.BlockCopy(this.ᜀ, 0, this.ᜀ, 0, this.m_iLength);
	}

	// Token: 0x060031C8 RID: 12744 RVA: 0x001CC524 File Offset: 0x001CB524
	public override void ᜀ(ExcelVersion A_0)
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
		int num = this.ᜀ.Length * 2;
		this.ᜀ = new byte[num];
		Buffer.BlockCopy(this.ᜀ, 0, this.ᜀ, 0, num);
	}

	// Token: 0x040015DC RID: 5596
	private new ushort[] ᜀ;
}
