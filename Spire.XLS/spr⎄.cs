using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x02000510 RID: 1296
[CLSCompliant(false)]
[spr\u2593(TBIFFRecord.CodeName)]
internal class spr\u2384 : spr\u251F
{
	// Token: 0x06004EBC RID: 20156 RVA: 0x002FC57C File Offset: 0x002FB57C
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
		return this.ᜀ;
	}

	// Token: 0x06004EBD RID: 20157 RVA: 0x002FC5C0 File Offset: 0x002FB5C0
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
		this.ᜀ = A_0;
	}

	// Token: 0x06004EBE RID: 20158 RVA: 0x002FC604 File Offset: 0x002FB604
	public spr\u2384()
	{
	}

	// Token: 0x06004EBF RID: 20159 RVA: 0x002FC618 File Offset: 0x002FB618
	public spr\u2384(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x06004EC0 RID: 20160 RVA: 0x002FC630 File Offset: 0x002FB630
	public spr\u2384(int A_0) : base(A_0)
	{
	}

	// Token: 0x06004EC1 RID: 20161 RVA: 0x002FC644 File Offset: 0x002FB644
	public override void ᜂ()
	{
		int a_ = 13;
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
			{
				base.ᜰ();
				int a_2 = (int)base.ᜌ(0);
				int num2;
				this.ᜀ = base.ᜀ(2, a_2, out num2);
				num = 2;
				continue;
			}
			case 1:
				goto IL_C0;
			case 2:
			{
				if (true)
				{
				}
				int num2;
				if (3 + num2 != base.Length)
				{
					num = 1;
					continue;
				}
				return;
			}
			}
			if (base.Length <= 0)
			{
				return;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_C0;
			default:
				if (false)
				{
				}
				num = 0;
				break;
			}
		}
		IL_C0:
		throw new sprῩ(RecordTableEnumerator.b("ᑂ㝄⡆❈ⱊ浌㱎═⅒㱔㥖㹘筚㉜ⵞ䅠ݢѤ፦ࡨ䭪Ŭ੮ὰᑲŴὶ坸", a_));
	}

	// Token: 0x06004EC2 RID: 20162 RVA: 0x002FC714 File Offset: 0x002FB714
	public override void ᜀ(ExcelVersion A_0)
	{
		for (;;)
		{
			IL_14:
			this.m_iLength = this.GetStoreSize(ExcelVersion.Version97to2003);
			for (;;)
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (this.m_iLength > 0)
						{
							num = 1;
							continue;
						}
						return;
					case 1:
						this.ᜀ = new byte[this.m_iLength];
						base.ᜃ(0, this.ᜀ);
						num = 2;
						continue;
					case 2:
						goto IL_63;
					}
					goto IL_14;
				}
				IL_63:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_79;
				}
			}
		}
		IL_79:
		if (true)
		{
		}
		if (false)
		{
		}
	}

	// Token: 0x06004EC3 RID: 20163 RVA: 0x002FC7B4 File Offset: 0x002FB7B4
	public override int ᜁ(ExcelVersion A_0)
	{
		int num = 2;
		for (;;)
		{
			int num2;
			int num3;
			switch (num)
			{
			case 0:
				return 0;
			case 1:
				num2 = this.ᜀ.Length;
				goto IL_47;
			case 3:
				num = 5;
				continue;
			case 4:
				if (num3 == 0)
				{
					num = 0;
					continue;
				}
				goto IL_A0;
			case 5:
				num2 = 0;
				goto IL_47;
			}
			IL_28:
			if (this.ᜀ == null)
			{
				num = 3;
				continue;
			}
			num = 1;
			continue;
			IL_47:
			num3 = num2;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_28;
			default:
				if (false)
				{
				}
				if (true)
				{
				}
				num = 4;
				break;
			}
		}
		return 0;
		IL_A0:
		return 3 + this.ᜀ.Length * 2;
	}

	// Token: 0x04002391 RID: 9105
	private new string ᜀ;
}
