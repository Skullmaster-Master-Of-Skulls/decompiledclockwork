using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x020004DB RID: 1243
[CLSCompliant(false)]
[spr\u2593(TBIFFRecord.PivotIndexList)]
internal class sprᜡ : spr\u251F
{
	// Token: 0x06004C4F RID: 19535 RVA: 0x002EAEE8 File Offset: 0x002E9EE8
	public sprᜡ()
	{
	}

	// Token: 0x06004C50 RID: 19536 RVA: 0x002EAEFC File Offset: 0x002E9EFC
	public sprᜡ(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x06004C51 RID: 19537 RVA: 0x002EAF14 File Offset: 0x002E9F14
	public sprᜡ(int A_0) : base(A_0)
	{
	}

	// Token: 0x06004C52 RID: 19538 RVA: 0x002EAF28 File Offset: 0x002E9F28
	public new byte[] ᜀ()
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

	// Token: 0x06004C53 RID: 19539 RVA: 0x002EAF6C File Offset: 0x002E9F6C
	public new void ᜀ(byte[] A_0)
	{
		int a_ = 9;
		while (A_0 != null)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				continue;
			}
			if (false)
			{
			}
			this.ᜀ = A_0;
			return;
		}
		if (true)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("䤾⁀⽂い≆", a_));
	}

	// Token: 0x06004C54 RID: 19540 RVA: 0x002EAFD0 File Offset: 0x002E9FD0
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
		this.ᜀ = new byte[this.m_iLength];
		Array.Copy(this.ᜀ, 0, this.ᜀ, 0, this.m_iLength);
	}

	// Token: 0x06004C55 RID: 19541 RVA: 0x002EB038 File Offset: 0x002EA038
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
		bool autoGrowData = this.AutoGrowData;
		this.AutoGrowData = true;
		this.m_iLength = this.ᜀ.Length;
		base.ᜀ(0, this.ᜀ, 0, this.m_iLength);
		this.AutoGrowData = autoGrowData;
	}

	// Token: 0x040022B9 RID: 8889
	private new byte[] ᜀ;
}
