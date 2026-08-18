using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x020004FD RID: 1277
[spr\u2593(TBIFFRecord.WindowZoom)]
[CLSCompliant(false)]
internal class spr\u1CF7 : BiffRecordRaw
{
	// Token: 0x06004DF6 RID: 19958 RVA: 0x002F888C File Offset: 0x002F788C
	public ushort ᜁ()
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

	// Token: 0x06004DF7 RID: 19959 RVA: 0x002F88D0 File Offset: 0x002F78D0
	public void ᜀ(ushort A_0)
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
		this.ᜁ = A_0;
	}

	// Token: 0x06004DF8 RID: 19960 RVA: 0x002F8914 File Offset: 0x002F7914
	public ushort ᜂ()
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
		return this.ᜂ;
	}

	// Token: 0x06004DF9 RID: 19961 RVA: 0x002F8958 File Offset: 0x002F7958
	public void ᜁ(ushort A_0)
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
		this.ᜂ = A_0;
	}

	// Token: 0x06004DFA RID: 19962 RVA: 0x002F899C File Offset: 0x002F799C
	public int ᜀ()
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
		return (int)((double)this.ᜁ * 100.0 / (double)this.ᜂ);
	}

	// Token: 0x06004DFB RID: 19963 RVA: 0x002F89F4 File Offset: 0x002F79F4
	public void ᜀ(int A_0)
	{
		int a_ = 11;
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				goto IL_A3;
			case 2:
				num = 3;
				continue;
			case 3:
				if (A_0 <= 400)
				{
					goto IL_A5;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_A5;
				default:
					if (false)
					{
					}
					num = 1;
					continue;
				}
				break;
			}
			if (A_0 < 10)
			{
				break;
			}
			if (true)
			{
			}
			num = 2;
		}
		IL_40:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ᭀⱂ⩄⩆", a_), RecordTableEnumerator.b("ᭀⱂ⩄⩆楈♊㡌㱎═獒㝔㉖祘㉚㍜罞፠ɢ୤f౨䭪୬ᵮṰṲ啴䙶䥸孺ᱼᅾꎂ놄랆릈ꖊ", a_));
		IL_A3:
		goto IL_40;
		IL_A5:
		this.ᜁ = (ushort)A_0;
		this.ᜂ = 100;
	}

	// Token: 0x06004DFC RID: 19964 RVA: 0x002F8AB8 File Offset: 0x002F7AB8
	public spr\u1CF7()
	{
	}

	// Token: 0x06004DFD RID: 19965 RVA: 0x002F8ADC File Offset: 0x002F7ADC
	public spr\u1CF7(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x06004DFE RID: 19966 RVA: 0x002F8B04 File Offset: 0x002F7B04
	public spr\u1CF7(int A_0) : base(A_0)
	{
	}

	// Token: 0x06004DFF RID: 19967 RVA: 0x002F8B28 File Offset: 0x002F7B28
	public virtual void ᜀ(DataProvider A_0, int A_1, int A_2, ExcelVersion A_3)
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
		this.ᜁ = A_0.ReadUInt16(A_1);
		this.ᜂ = A_0.ReadUInt16(A_1 + 2);
	}

	// Token: 0x06004E00 RID: 19968 RVA: 0x002F8B80 File Offset: 0x002F7B80
	public virtual void ᜀ(DataProvider A_0, int A_1, ExcelVersion A_2)
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
		this.m_iLength = 4;
		A_0.WriteUInt16(A_1, this.ᜁ);
		A_0.WriteUInt16(A_1 + 2, this.ᜂ);
	}

	// Token: 0x06004E01 RID: 19969 RVA: 0x002F8BE0 File Offset: 0x002F7BE0
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
		return 4;
	}

	// Token: 0x04002346 RID: 9030
	private new const int ᜀ = 4;

	// Token: 0x04002347 RID: 9031
	[spr\u2429(0, 2)]
	private ushort ᜁ = 100;

	// Token: 0x04002348 RID: 9032
	[spr\u2429(2, 2)]
	private ushort ᜂ = 100;
}
