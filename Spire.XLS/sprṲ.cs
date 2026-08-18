using System;
using System.IO;
using System.Text;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x0200052D RID: 1325
[spr\u2593(TBIFFRecord.QuickTip)]
[CLSCompliant(false)]
internal class sprṲ : spr\u251F
{
	// Token: 0x060050FC RID: 20732 RVA: 0x0032C674 File Offset: 0x0032B674
	public new TAddr ᜀ()
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

	// Token: 0x060050FD RID: 20733 RVA: 0x0032C6B8 File Offset: 0x0032B6B8
	public new void ᜀ(TAddr A_0)
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
		this.ᜂ = A_0;
	}

	// Token: 0x060050FE RID: 20734 RVA: 0x0032C6FC File Offset: 0x0032B6FC
	public string ᜁ()
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
		return this.ᜃ;
	}

	// Token: 0x060050FF RID: 20735 RVA: 0x0032C740 File Offset: 0x0032B740
	public new void ᜀ(string A_0)
	{
		int a_ = 10;
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		this.ᜃ = ((A_0[A_0.Length - 1] != '\0') ? (A_0 + RecordTableEnumerator.b("䀿", a_)) : A_0);
	}

	// Token: 0x06005100 RID: 20736 RVA: 0x0032C7B8 File Offset: 0x0032B7B8
	public virtual int ᜃ()
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
		return 10;
	}

	// Token: 0x06005101 RID: 20737 RVA: 0x0032C7F8 File Offset: 0x0032B7F8
	public sprṲ()
	{
	}

	// Token: 0x06005102 RID: 20738 RVA: 0x0032C824 File Offset: 0x0032B824
	public sprṲ(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x06005103 RID: 20739 RVA: 0x0032C850 File Offset: 0x0032B850
	public sprṲ(int A_0) : base(A_0)
	{
	}

	// Token: 0x06005104 RID: 20740 RVA: 0x0032C87C File Offset: 0x0032B87C
	public override void ᜂ()
	{
		int a_ = 14;
		int num2;
		for (;;)
		{
			this.ᜁ = base.ᜌ(0);
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (this.m_iLength % 2 != 0)
					{
						num = 2;
						continue;
					}
					goto IL_86;
				case 1:
					goto IL_7A;
				case 2:
					goto IL_13A;
				case 3:
					if (this.ᜁ == 2048)
					{
						num = 0;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_86;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						num = 1;
						continue;
					}
					break;
				case 4:
					goto IL_E7;
				case 5:
					if (num2 != this.ᜃ.Length - 1)
					{
						num = 4;
						continue;
					}
					goto IL_13C;
				}
				break;
				IL_86:
				this.ᜂ = base.\u1716(2);
				this.ᜃ = Encoding.Unicode.GetString(this.ᜀ, 10, this.m_iLength - 10);
				num2 = this.ᜃ.IndexOf('\0');
				num = 5;
			}
		}
		IL_7A:
		throw new sprῩ(RecordTableEnumerator.b("ᕃ㍅ⅇ⥉❋ᩍ㥏≑瑓さㅗ⡙⽛⩝䁟ᕡୣᑥ౧䩩ū᭭ͯٱ味ᑵᵷ婹䱻ٽ끿몁뒃뚅ꚇ", a_));
		IL_E7:
		throw new sprῩ(RecordTableEnumerator.b("ṃ⍅㩇╉態㩍㕏⁑㥓㽕㙗㭙⡛㭝џ䉡ᝣብᩧͩɫ७偯ᙱ᭳፵୷婹ቻᅽꊁﲇꪉ뒓ﶛ躟", a_));
		IL_13A:
		throw new sprῩ();
		IL_13C:
		this.ᜃ = this.ᜃ.Remove(num2, 1);
	}

	// Token: 0x06005105 RID: 20741 RVA: 0x0032C9D8 File Offset: 0x0032B9D8
	public override void ᜀ(ExcelVersion A_0)
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
		this.ᜀ = new byte[this.GetStoreSize(ExcelVersion.Version97to2003)];
		base.ᜀ(0, this.ᜁ);
		this.m_iLength = 2;
		base.ᜀ(this.m_iLength, this.ᜂ);
		this.m_iLength += 8;
		byte[] bytes = Encoding.Unicode.GetBytes(this.ᜃ);
		int num = bytes.Length;
		base.ᜁ(this.m_iLength, bytes);
		this.m_iLength += num;
	}

	// Token: 0x06005106 RID: 20742 RVA: 0x0032CA8C File Offset: 0x0032BA8C
	public override int ᜁ(ExcelVersion A_0)
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
		return 10 + Encoding.Unicode.GetByteCount(this.ᜃ);
	}

	// Token: 0x04002433 RID: 9267
	private new const int ᜀ = 10;

	// Token: 0x04002434 RID: 9268
	[spr\u2429(0, 2)]
	private new ushort ᜁ = 2048;

	// Token: 0x04002435 RID: 9269
	private new TAddr ᜂ;

	// Token: 0x04002436 RID: 9270
	private new string ᜃ = string.Empty;
}
