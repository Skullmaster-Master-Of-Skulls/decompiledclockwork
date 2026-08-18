using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x0200042B RID: 1067
[CLSCompliant(false)]
[spr\u2593(TBIFFRecord.DCONBIN)]
internal class sprᯁ : spr\u251F
{
	// Token: 0x0600409C RID: 16540 RVA: 0x0024405C File Offset: 0x0024305C
	public sprᯁ()
	{
	}

	// Token: 0x0600409D RID: 16541 RVA: 0x00244070 File Offset: 0x00243070
	public sprᯁ(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x0600409E RID: 16542 RVA: 0x00244088 File Offset: 0x00243088
	public sprᯁ(int A_0) : base(A_0)
	{
	}

	// Token: 0x0600409F RID: 16543 RVA: 0x0024409C File Offset: 0x0024309C
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

	// Token: 0x060040A0 RID: 16544 RVA: 0x002440E0 File Offset: 0x002430E0
	public new void ᜀ(string A_0)
	{
		int a_ = 13;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			break;
		default:
			if (true)
			{
			}
			if (false)
			{
			}
			if (A_0 != null)
			{
				this.ᜀ = A_0;
				return;
			}
			break;
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("㕂⑄⭆㱈⹊", a_));
	}

	// Token: 0x060040A1 RID: 16545 RVA: 0x00244144 File Offset: 0x00243144
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
		return this.ᜁ;
	}

	// Token: 0x060040A2 RID: 16546 RVA: 0x00244188 File Offset: 0x00243188
	public void ᜁ(string A_0)
	{
		int a_ = 5;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			break;
		default:
			if (false)
			{
			}
			if (A_0 != null)
			{
				this.ᜁ = A_0;
				return;
			}
			break;
		}
		if (true)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("䴺尼匾㑀♂", a_));
	}

	// Token: 0x060040A3 RID: 16547 RVA: 0x002441EC File Offset: 0x002431EC
	public override void ᜂ()
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
		int num = 0;
		this.ᜀ = base.ᜋ(ref num);
		this.ᜁ = base.ᜋ(ref num);
	}

	// Token: 0x060040A4 RID: 16548 RVA: 0x00244248 File Offset: 0x00243248
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
		this.AutoGrowData = true;
		bool a_ = spr\u251F.ᜀ(this.ᜀ);
		int num = base.ᜀ(0, this.ᜀ, true, a_);
		a_ = spr\u251F.ᜀ(this.ᜁ);
		this.m_iLength = num + base.ᜀ(num, this.ᜁ, false, a_);
	}

	// Token: 0x04001CDE RID: 7390
	[spr\u2429(0, TFieldType.String16Bit)]
	private new string ᜀ;

	// Token: 0x04001CDF RID: 7391
	private new string ᜁ;
}
