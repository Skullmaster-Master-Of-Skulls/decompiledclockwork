using System;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Parser.Biff_Records.ObjRecords;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x02000530 RID: 1328
internal class sprᾑ : spr\u25AD
{
	// Token: 0x0600511E RID: 20766 RVA: 0x0032D25C File Offset: 0x0032C25C
	public sprᾑ() : base(TObjSubRecordType.ftRbo)
	{
	}

	// Token: 0x0600511F RID: 20767 RVA: 0x0032D274 File Offset: 0x0032C274
	public sprᾑ(ushort A_0, byte[] A_1) : base(TObjSubRecordType.ftRbo, A_0, A_1)
	{
	}

	// Token: 0x06005120 RID: 20768 RVA: 0x0032D28C File Offset: 0x0032C28C
	protected override void ᜀ(byte[] A_0)
	{
		int a_ = 2;
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
			if (A_0 == null)
			{
				throw new ArgumentNullException(RecordTableEnumerator.b("娷伹娻堽┿ぁ", a_));
			}
			break;
		}
	}

	// Token: 0x06005121 RID: 20769 RVA: 0x0032D2EC File Offset: 0x0032C2EC
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
		A_0.WriteByte(A_1, 0);
		A_1++;
		A_0.WriteInt32(A_1, 0);
		A_1 += 4;
		A_0.WriteInt32(A_1, 0);
		A_1 += 4;
		A_0.WriteByte(A_1, 0);
		A_1++;
		A_0.WriteByte(A_1, 3);
		A_1++;
		A_0.WriteByte(A_1, 0);
		A_1++;
	}

	// Token: 0x06005122 RID: 20770 RVA: 0x0032D3A0 File Offset: 0x0032C3A0
	public override int ᜀ(ExcelVersion A_0)
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
		return 10;
	}
}
