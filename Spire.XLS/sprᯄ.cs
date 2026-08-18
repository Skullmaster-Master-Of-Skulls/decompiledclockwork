using System;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Parser.Biff_Records.ObjRecords;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x0200050A RID: 1290
[CLSCompliant(false)]
internal class sprᯄ : spr\u25AD
{
	// Token: 0x06004E7E RID: 20094 RVA: 0x002FB0AC File Offset: 0x002FA0AC
	public new CheckState ᜀ()
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
		return (CheckState)this.ᜀ;
	}

	// Token: 0x06004E7F RID: 20095 RVA: 0x002FB0F0 File Offset: 0x002FA0F0
	public new void ᜀ(CheckState A_0)
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
		this.ᜀ = (byte)A_0;
	}

	// Token: 0x06004E80 RID: 20096 RVA: 0x002FB134 File Offset: 0x002FA134
	public sprᯄ() : base(TObjSubRecordType.ftCbls)
	{
	}

	// Token: 0x06004E81 RID: 20097 RVA: 0x002FB14C File Offset: 0x002FA14C
	public sprᯄ(TObjSubRecordType A_0, ushort A_1, byte[] A_2) : base(A_0, A_1, A_2)
	{
	}

	// Token: 0x06004E82 RID: 20098 RVA: 0x002FB164 File Offset: 0x002FA164
	protected override void ᜀ(byte[] A_0)
	{
		int a_ = 14;
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
				this.ᜀ = A_0[0];
				return;
			}
			break;
		}
		if (true)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("♃㍅⹇ⱉ⥋㱍", a_));
	}

	// Token: 0x06004E83 RID: 20099 RVA: 0x002FB1CC File Offset: 0x002FA1CC
	public override void ᜀ(DataProvider A_0, int A_1)
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
		A_0.WriteInt16(A_1, (short)base.ᜏ());
		A_1 += 2;
		short value = (short)(this.ᜀ(ExcelVersion.Version97to2003) - 4);
		A_0.WriteInt16(A_1, value);
		A_1 += 2;
		A_0.WriteByte(A_1, this.ᜀ);
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

	// Token: 0x06004E84 RID: 20100 RVA: 0x002FB288 File Offset: 0x002FA288
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
		return 16;
	}

	// Token: 0x0400237E RID: 9086
	private new byte ᜀ;
}
