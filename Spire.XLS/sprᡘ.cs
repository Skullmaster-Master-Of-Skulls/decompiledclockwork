using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x020002F1 RID: 753
[spr\u2593(TBIFFRecord.ChartDropBar)]
[CLSCompliant(false)]
internal class sprᡘ : BiffRecordRaw
{
	// Token: 0x06002EA1 RID: 11937 RVA: 0x001A19AC File Offset: 0x001A09AC
	public ushort ᜀ()
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

	// Token: 0x06002EA2 RID: 11938 RVA: 0x001A19F0 File Offset: 0x001A09F0
	public void ᜀ(ushort A_0)
	{
		int a_ = 10;
		int num = 6;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_6E;
			case 1:
				if (A_0 > 500)
				{
					num = 5;
					continue;
				}
				if (true)
				{
				}
				num = 2;
				continue;
			case 2:
				if (A_0 != this.ᜁ)
				{
					num = 0;
					continue;
				}
				return;
			case 3:
				num = 1;
				continue;
			case 4:
				return;
			case 5:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_6E;
				default:
					goto IL_C6;
				}
				break;
			}
			if (A_0 >= 0)
			{
				num = 3;
				continue;
			}
			break;
			IL_6E:
			this.ᜁ = A_0;
			num = 4;
		}
		IL_70:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ⴿᵁㅃ㕅ཇ⭉㱋", a_), RecordTableEnumerator.b("ᘿ⍁⡃㍅ⵇ橉⽋⽍㹏㱑㭓≕硗㡙㥛繝౟ݡᝣᕥ䡧ṩѫ཭ṯ剱䑳噵᝷ࡹ屻᥽겋揄望뚕ꦗꪙ겛낝", a_));
		IL_C6:
		if (false)
		{
		}
		goto IL_70;
	}

	// Token: 0x06002EA3 RID: 11939 RVA: 0x001A1AE0 File Offset: 0x001A0AE0
	public sprᡘ()
	{
	}

	// Token: 0x06002EA4 RID: 11940 RVA: 0x001A1AF4 File Offset: 0x001A0AF4
	public sprᡘ(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x06002EA5 RID: 11941 RVA: 0x001A1B0C File Offset: 0x001A0B0C
	public sprᡘ(int A_0) : base(A_0)
	{
	}

	// Token: 0x06002EA6 RID: 11942 RVA: 0x001A1B20 File Offset: 0x001A0B20
	public virtual void ᜀ(DataProvider A_0, int A_1, int A_2, ExcelVersion A_3)
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
		this.ᜁ = A_0.ReadUInt16(A_1);
	}

	// Token: 0x06002EA7 RID: 11943 RVA: 0x001A1B68 File Offset: 0x001A0B68
	public virtual void ᜀ(DataProvider A_0, int A_1, ExcelVersion A_2)
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
		A_0.WriteUInt16(A_1, this.ᜁ);
		this.m_iLength = 2;
	}

	// Token: 0x06002EA8 RID: 11944 RVA: 0x001A1BB8 File Offset: 0x001A0BB8
	public virtual int ᜀ(ExcelVersion A_0)
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
		return 2;
	}

	// Token: 0x040014FA RID: 5370
	private new const int ᜀ = 2;

	// Token: 0x040014FB RID: 5371
	[spr\u2429(0, 2)]
	private ushort ᜁ;
}
