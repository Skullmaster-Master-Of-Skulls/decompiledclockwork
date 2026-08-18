using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x020005A5 RID: 1445
[spr\u2593(TBIFFRecord.ChartWrapper)]
[CLSCompliant(false)]
internal class spr\u23F0 : BiffRecordRaw, ICloneable
{
	// Token: 0x060057AD RID: 22445 RVA: 0x0037BA2C File Offset: 0x0037AA2C
	public spr\u23F0()
	{
	}

	// Token: 0x060057AE RID: 22446 RVA: 0x0037BA40 File Offset: 0x0037AA40
	public spr\u23F0(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x060057AF RID: 22447 RVA: 0x0037BA58 File Offset: 0x0037AA58
	public spr\u23F0(int A_0) : base(A_0)
	{
	}

	// Token: 0x060057B0 RID: 22448 RVA: 0x0037BA6C File Offset: 0x0037AA6C
	public BiffRecordRaw ᜀ()
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

	// Token: 0x060057B1 RID: 22449 RVA: 0x0037BAB0 File Offset: 0x0037AAB0
	public void ᜀ(BiffRecordRaw A_0)
	{
		int a_ = 16;
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
					continue;
				}
				break;
			}
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("ぅ⥇♉㥋⭍", a_));
		}
		this.ᜁ = A_0;
	}

	// Token: 0x060057B2 RID: 22450 RVA: 0x0037BB14 File Offset: 0x0037AB14
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
		this.ᜁ = spr\u175E.ᜀ(A_0, A_1 + 4, A_3);
	}

	// Token: 0x060057B3 RID: 22451 RVA: 0x0037BB60 File Offset: 0x0037AB60
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
		int storeSize = this.ᜁ.GetStoreSize(A_2);
		this.m_iLength = 4 + storeSize + 4;
		A_0.WriteUInt16(A_1 + 4, (ushort)this.ᜁ.TypeCode);
		A_0.WriteUInt16(A_1 + 4 + 2, (ushort)storeSize);
		this.ᜁ.InfillInternalData(A_0, A_1 + 4 + 4, A_2);
		A_0.WriteUInt16(A_1, (ushort)base.TypeCode);
		A_0.WriteUInt16(A_1 + 2, 0);
	}

	// Token: 0x060057B4 RID: 22452 RVA: 0x0037BC00 File Offset: 0x0037AC00
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
		return 8 + this.ᜁ.GetStoreSize(A_0);
	}

	// Token: 0x060057B5 RID: 22453 RVA: 0x0037BC4C File Offset: 0x0037AC4C
	public object ᜁ()
	{
		object obj;
		for (;;)
		{
			obj = base.Clone();
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
			{
				if (false)
				{
				}
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						spr\u23F0 spr_u23F = (spr\u23F0)obj;
						spr_u23F.ᜁ = (BiffRecordRaw)this.ᜁ.Clone();
						num = 2;
						continue;
					}
					case 1:
						if (this.ᜁ != null)
						{
							num = 0;
							continue;
						}
						return obj;
					case 2:
						return obj;
					}
					break;
				}
				break;
			}
			}
		}
		return obj;
	}

	// Token: 0x040029AE RID: 10670
	private new const int ᜀ = 4;

	// Token: 0x040029AF RID: 10671
	private BiffRecordRaw ᜁ;
}
