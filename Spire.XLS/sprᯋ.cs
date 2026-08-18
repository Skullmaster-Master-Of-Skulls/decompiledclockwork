using System;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Parser.Biff_Records.ObjRecords;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x020002B1 RID: 689
internal class sprᯋ : spr\u25AD
{
	// Token: 0x060029C7 RID: 10695 RVA: 0x001787E8 File Offset: 0x001777E8
	public new bool ᜀ()
	{
		while (this.ᜀ != 1)
		{
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
				return false;
			}
		}
		return true;
	}

	// Token: 0x060029C8 RID: 10696 RVA: 0x00178834 File Offset: 0x00177834
	public new void ᜀ(bool A_0)
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
		this.ᜀ = (A_0 ? 1 : 0);
	}

	// Token: 0x060029C9 RID: 10697 RVA: 0x00178884 File Offset: 0x00177884
	public byte ᜂ()
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

	// Token: 0x060029CA RID: 10698 RVA: 0x001788C8 File Offset: 0x001778C8
	public new void ᜀ(byte A_0)
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
		this.ᜁ = A_0;
	}

	// Token: 0x060029CB RID: 10699 RVA: 0x0017890C File Offset: 0x0017790C
	public sprᯋ() : base(TObjSubRecordType.ftRboData)
	{
	}

	// Token: 0x060029CC RID: 10700 RVA: 0x00178924 File Offset: 0x00177924
	public sprᯋ(ushort A_0, byte[] A_1) : base(TObjSubRecordType.ftRboData, A_0, A_1)
	{
	}

	// Token: 0x060029CD RID: 10701 RVA: 0x0017893C File Offset: 0x0017793C
	protected override void ᜀ(byte[] A_0)
	{
		int a_ = 13;
		while (A_0 == null)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				if (true)
				{
				}
				throw new ArgumentNullException(RecordTableEnumerator.b("⅂いⅆ⽈⹊㽌", a_));
			}
		}
		this.ᜁ = A_0[0];
		this.ᜀ = A_0[2];
	}

	// Token: 0x060029CE RID: 10702 RVA: 0x001789AC File Offset: 0x001779AC
	public override void ᜀ(DataProvider A_0, int A_1)
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
		A_0.WriteInt16(A_1, (short)base.ᜏ());
		A_1 += 2;
		short value = (short)(this.ᜀ(ExcelVersion.Version97to2003) - 4);
		A_0.WriteInt16(A_1, value);
		A_1 += 2;
		A_0.WriteByte(A_1, this.ᜁ);
		A_1 += 2;
		A_0.WriteByte(A_1, this.ᜀ);
		A_1++;
	}

	// Token: 0x060029CF RID: 10703 RVA: 0x00178A38 File Offset: 0x00177A38
	public override int ᜀ(ExcelVersion A_0)
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
		return 8;
	}

	// Token: 0x040013E7 RID: 5095
	private new byte ᜀ;

	// Token: 0x040013E8 RID: 5096
	private new byte ᜁ;
}
