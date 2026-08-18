using System;
using System.IO;
using System.Text;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x02000262 RID: 610
[CLSCompliant(false)]
[spr\u2593(TBIFFRecord.PivotString)]
internal class spr\u260F : BiffRecordRaw, spr\u1929
{
	// Token: 0x06002495 RID: 9365 RVA: 0x00154DC4 File Offset: 0x00153DC4
	public spr\u260F()
	{
	}

	// Token: 0x06002496 RID: 9366 RVA: 0x00154DD8 File Offset: 0x00153DD8
	public spr\u260F(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x06002497 RID: 9367 RVA: 0x00154DF0 File Offset: 0x00153DF0
	public spr\u260F(int A_0) : base(A_0)
	{
	}

	// Token: 0x06002498 RID: 9368 RVA: 0x00154E04 File Offset: 0x00153E04
	public string ᜀ()
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

	// Token: 0x06002499 RID: 9369 RVA: 0x00154E48 File Offset: 0x00153E48
	public void ᜀ(string A_0)
	{
		int a_ = 2;
		if (A_0 == null)
		{
			for (;;)
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
					goto IL_2C;
				}
			}
			IL_2C:
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("丷嬹倻䬽┿", a_));
		}
		this.ᜀ = A_0;
		this.ᜁ = spr\u251F.ᜀ(A_0);
	}

	// Token: 0x0600249A RID: 9370 RVA: 0x00154EB8 File Offset: 0x00153EB8
	public virtual bool ᜂ()
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
		return true;
	}

	// Token: 0x0600249B RID: 9371 RVA: 0x00154EF4 File Offset: 0x00153EF4
	public virtual void ᜀ(DataProvider A_0, int A_1, int A_2, ExcelVersion A_3)
	{
		for (;;)
		{
			int num;
			this.ᜀ = A_0.ReadString16Bit(A_1, out num);
			int num2 = 2;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					this.ᜁ = true;
					if (true)
					{
					}
					num2 = 1;
					continue;
				case 1:
					return;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					default:
						if (false)
						{
						}
						if (this.ᜀ.Length * 2 + 3 < num)
						{
							num2 = 0;
							continue;
						}
						return;
					}
					break;
				}
				break;
			}
		}
	}

	// Token: 0x0600249C RID: 9372 RVA: 0x00154F8C File Offset: 0x00153F8C
	public virtual void ᜀ(DataProvider A_0, int A_1, ExcelVersion A_2)
	{
		if (this.ᜁ)
		{
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_20;
				}
			}
			IL_20:
			if (true)
			{
			}
			if (false)
			{
			}
			int num = A_1;
			A_0.WriteString16BitUpdateOffset(ref A_1, this.ᜀ, false);
			this.m_iLength = A_1 - num;
			return;
		}
		this.m_iLength = A_0.WriteString16Bit(A_1, this.ᜀ);
	}

	// Token: 0x0600249D RID: 9373 RVA: 0x00155000 File Offset: 0x00154000
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
		return 3 + (this.ᜁ ? this.ᜀ.Length : Encoding.Unicode.GetByteCount(this.ᜀ));
	}

	// Token: 0x0600249E RID: 9374 RVA: 0x00155068 File Offset: 0x00154068
	object spr\u1929.ᜁ()
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
		return this.ᜀ();
	}

	// Token: 0x0600249F RID: 9375 RVA: 0x001550AC File Offset: 0x001540AC
	void spr\u1929.ᜀ(object A_0)
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
		this.ᜀ((string)A_0);
	}

	// Token: 0x04001282 RID: 4738
	[spr\u2429(0, TFieldType.String16Bit)]
	private new string ᜀ;

	// Token: 0x04001283 RID: 4739
	private bool ᜁ;
}
