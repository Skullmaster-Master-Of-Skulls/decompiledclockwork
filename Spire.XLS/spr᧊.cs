using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x0200056E RID: 1390
[CLSCompliant(false)]
[spr\u2593(TBIFFRecord.ExtSSTInfoSub)]
internal class spr\u19CA : BiffRecordRaw
{
	// Token: 0x06005387 RID: 21383 RVA: 0x00340598 File Offset: 0x0033F598
	public int ᜂ()
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

	// Token: 0x06005388 RID: 21384 RVA: 0x003405DC File Offset: 0x0033F5DC
	public void ᜀ(int A_0)
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
		this.ᜁ = A_0;
	}

	// Token: 0x06005389 RID: 21385 RVA: 0x00340620 File Offset: 0x0033F620
	public new ushort ᜃ()
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
		return this.ᜂ;
	}

	// Token: 0x0600538A RID: 21386 RVA: 0x00340664 File Offset: 0x0033F664
	public void ᜀ(ushort A_0)
	{
		int a_ = 10;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			break;
		default:
			if (false)
			{
			}
			if (A_0 > 8228)
			{
				if (true)
				{
				}
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ȿ㝁❃ⵅⵇ㹉Ὃᵍяᵑ㉓さ⭗㽙⡛", a_), RecordTableEnumerator.b("ȿ㝁❃ⵅⵇ㹉汋ᵍ͏ّ瑓ᥕ㹗㱙⽛㭝ᑟ䉡ݣݥ٧ѩͫᩭ偯ၱᅳ噵ᑷ᭹๻᥽ꒃ꺍\udd8f펑첓뚕ﾙﾛ튟욡蒣향솧킩즫肭邯ﶱ\udab3隵\uddb7\udbb9\udfbb횽臁ꯃꣅ볇ꏉꋋ믍뗏蛓돕믗뗙껛뫝샟跡苣胥鯧迩飫컭鷯蟱蟳苵\ud8f7飹駻\udefd珿瘁攃琅簇漉栋⸍瘏怑笓笕㠗怙礛氝伟డ", a_));
			}
			break;
		}
		this.ᜂ = A_0;
	}

	// Token: 0x0600538B RID: 21387 RVA: 0x003406DC File Offset: 0x0033F6DC
	public ushort ᜀ()
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
		return this.ᜃ;
	}

	// Token: 0x0600538C RID: 21388 RVA: 0x00340720 File Offset: 0x0033F720
	public virtual int ᜄ()
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

	// Token: 0x0600538D RID: 21389 RVA: 0x0034075C File Offset: 0x0033F75C
	public virtual int ᜁ()
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

	// Token: 0x0600538E RID: 21390 RVA: 0x00340798 File Offset: 0x0033F798
	public spr\u19CA()
	{
	}

	// Token: 0x0600538F RID: 21391 RVA: 0x003407AC File Offset: 0x0033F7AC
	public spr\u19CA(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x06005390 RID: 21392 RVA: 0x003407C4 File Offset: 0x0033F7C4
	public spr\u19CA(int A_0) : base(A_0)
	{
	}

	// Token: 0x06005391 RID: 21393 RVA: 0x003407D8 File Offset: 0x0033F7D8
	public virtual void ᜀ(DataProvider A_0, int A_1, int A_2, ExcelVersion A_3)
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
		this.ᜁ = A_0.ReadInt32(A_1);
		this.ᜂ = A_0.ReadUInt16(A_1 + 4);
		this.ᜃ = A_0.ReadUInt16(A_1 + 6);
	}

	// Token: 0x06005392 RID: 21394 RVA: 0x00340840 File Offset: 0x0033F840
	public virtual void ᜀ(DataProvider A_0, int A_1, ExcelVersion A_2)
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
		A_0.WriteInt32(A_1, this.ᜁ);
		A_0.WriteUInt16(A_1 + 4, this.ᜂ);
		A_0.WriteUInt16(A_1 + 6, this.ᜃ);
		this.m_iLength = 8;
	}

	// Token: 0x06005393 RID: 21395 RVA: 0x003408B0 File Offset: 0x0033F8B0
	public virtual int ᜀ(ExcelVersion A_0)
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
		return 8;
	}

	// Token: 0x04002711 RID: 10001
	private new const int ᜀ = 8;

	// Token: 0x04002712 RID: 10002
	[spr\u2429(0, 4, true)]
	private int ᜁ;

	// Token: 0x04002713 RID: 10003
	[spr\u2429(4, 2)]
	private ushort ᜂ;

	// Token: 0x04002714 RID: 10004
	[spr\u2429(6, 2)]
	private new ushort ᜃ;
}
