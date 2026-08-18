using System;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Parser.Biff_Records.ObjRecords;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x02000531 RID: 1329
[CLSCompliant(false)]
internal class sprἨ : spr\u25AD
{
	// Token: 0x06005123 RID: 20771 RVA: 0x0032D3E0 File Offset: 0x0032C3E0
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

	// Token: 0x06005124 RID: 20772 RVA: 0x0032D424 File Offset: 0x0032C424
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

	// Token: 0x06005125 RID: 20773 RVA: 0x0032D468 File Offset: 0x0032C468
	public bool ᜂ()
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

	// Token: 0x06005126 RID: 20774 RVA: 0x0032D4AC File Offset: 0x0032C4AC
	public new void ᜀ(bool A_0)
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

	// Token: 0x06005127 RID: 20775 RVA: 0x0032D4F0 File Offset: 0x0032C4F0
	public sprἨ() : base(TObjSubRecordType.ftCblsData)
	{
	}

	// Token: 0x06005128 RID: 20776 RVA: 0x0032D508 File Offset: 0x0032C508
	public sprἨ(TObjSubRecordType A_0, ushort A_1, byte[] A_2) : base(A_0, A_1, A_2)
	{
	}

	// Token: 0x06005129 RID: 20777 RVA: 0x0032D520 File Offset: 0x0032C520
	protected override void ᜀ(byte[] A_0)
	{
		int a_ = 12;
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_85;
			case 2:
				goto IL_30;
			}
			if (A_0 == null)
			{
				num = 2;
			}
			else
			{
				this.ᜀ = A_0[0];
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_85;
				default:
					if (false)
					{
					}
					if (true)
					{
					}
					num = 0;
					break;
				}
			}
		}
		IL_30:
		throw new ArgumentNullException(RecordTableEnumerator.b("⁁ㅃ⁅⹇⽉㹋", a_));
		IL_85:
		this.ᜁ = (A_0[6] != 3);
	}

	// Token: 0x0600512A RID: 20778 RVA: 0x0032D5C8 File Offset: 0x0032C5C8
	public override void ᜀ(DataProvider A_0, int A_1)
	{
		for (;;)
		{
			A_0.WriteInt16(A_1, (short)base.ᜏ());
			A_1 += 2;
			short value = (short)(this.ᜀ(ExcelVersion.Version97to2003) - 4);
			A_0.WriteInt16(A_1, value);
			A_1 += 2;
			A_0.WriteByte(A_1, this.ᜀ);
			A_1++;
			A_0.WriteInt32(A_1, 0);
			A_1 += 4;
			A_0.WriteByte(A_1, 0);
			A_1++;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_9F;
				case 1:
					if (true)
					{
					}
					if (!this.ᜁ)
					{
						num = 2;
						continue;
					}
					num = 0;
					continue;
				case 2:
					goto IL_92;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_92;
					default:
						goto IL_C3;
					}
					break;
				}
				break;
				IL_92:
				num = 3;
			}
		}
		IL_9F:
		byte b = 2;
		goto IL_D6;
		IL_C3:
		if (false)
		{
		}
		b = 3;
		IL_D6:
		byte value2 = b;
		A_0.WriteByte(A_1, value2);
		A_1++;
		A_0.WriteByte(A_1, 0);
		A_1++;
	}

	// Token: 0x0600512B RID: 20779 RVA: 0x0032D6C8 File Offset: 0x0032C6C8
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
		return 12;
	}

	// Token: 0x04002442 RID: 9282
	private new byte ᜀ;

	// Token: 0x04002443 RID: 9283
	private new bool ᜁ;
}
