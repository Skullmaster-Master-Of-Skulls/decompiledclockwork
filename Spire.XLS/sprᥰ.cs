using System;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Parser.Biff_Records.Formula;
using Spire.Xls.Core.Parser.Biff_Records.ObjRecords;
using Spire.Xls.Core.Spreadsheet;

// Token: 0x02000509 RID: 1289
internal class sprᥰ : spr\u25AD
{
	// Token: 0x06004E76 RID: 20086 RVA: 0x002FAE38 File Offset: 0x002F9E38
	public new Ptg[] ᜀ()
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

	// Token: 0x06004E77 RID: 20087 RVA: 0x002FAE7C File Offset: 0x002F9E7C
	public new void ᜀ(Ptg[] A_0)
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

	// Token: 0x06004E78 RID: 20088 RVA: 0x002FAEC0 File Offset: 0x002F9EC0
	public sprᥰ() : base(TObjSubRecordType.ftMacro)
	{
	}

	// Token: 0x06004E79 RID: 20089 RVA: 0x002FAED4 File Offset: 0x002F9ED4
	[CLSCompliant(false)]
	public sprᥰ(ushort A_0, byte[] A_1) : base(TObjSubRecordType.ftMacro, A_0, A_1)
	{
	}

	// Token: 0x06004E7A RID: 20090 RVA: 0x002FAEEC File Offset: 0x002F9EEC
	protected override void ᜀ(byte[] A_0)
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
		int num = 0;
		int a_ = (int)BitConverter.ToUInt16(A_0, num);
		num += 2;
		num += 4;
		spr\u24E5 a_2 = new spr\u24E5(A_0);
		int num2;
		this.ᜀ = FormulaUtil.ᜀ(a_2, num, a_, out num2, ExcelVersion.Version97to2003);
	}

	// Token: 0x06004E7B RID: 20091 RVA: 0x002FAF54 File Offset: 0x002F9F54
	protected override void ᜁ(DataProvider A_0, int A_1)
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
		byte[] array = FormulaUtil.ᜀ(this.ᜀ, ExcelVersion.Version97to2003);
		int num = array.Length;
		A_0.WriteUInt16(A_1, (ushort)num);
		A_1 += 2;
		A_0.WriteInt32(A_1, 0);
		A_1 += 4;
		A_0.WriteBytes(A_1, array);
	}

	// Token: 0x06004E7C RID: 20092 RVA: 0x002FAFC4 File Offset: 0x002F9FC4
	public override int ᜀ(ExcelVersion A_0)
	{
		int num;
		for (;;)
		{
			for (;;)
			{
				num = sprᡣ.ᜀ(this.ᜀ, A_0, true) + 4 + 2 + 4;
				int num2 = 0;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						if (num % 2 != 0)
						{
							num2 = 1;
							continue;
						}
						return num;
					case 1:
						num++;
						num2 = 2;
						continue;
					case 2:
						goto IL_4B;
					}
					break;
				}
			}
			IL_4B:
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_69;
			}
		}
		IL_69:
		if (false)
		{
		}
		return num;
	}

	// Token: 0x06004E7D RID: 20093 RVA: 0x002FB050 File Offset: 0x002FA050
	public override object ᜁ()
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
		sprᥰ sprᥰ = (sprᥰ)base.ᜁ();
		sprᥰ.ᜀ = spr\u1CD3.ᜀ(this.ᜀ);
		return sprᥰ;
	}

	// Token: 0x0400237D RID: 9085
	private new Ptg[] ᜀ;
}
