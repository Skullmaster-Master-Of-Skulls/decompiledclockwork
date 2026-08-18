using System;
using System.IO;
using System.Text;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x0200056C RID: 1388
[CLSCompliant(false)]
[spr\u2593(TBIFFRecord.Format)]
internal class spr\u240D : BiffRecordRaw
{
	// Token: 0x06005372 RID: 21362 RVA: 0x0033FFFC File Offset: 0x0033EFFC
	public int ᜀ()
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
		return (int)this.ᜀ;
	}

	// Token: 0x06005373 RID: 21363 RVA: 0x00340040 File Offset: 0x0033F040
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
		this.ᜀ = (ushort)A_0;
	}

	// Token: 0x06005374 RID: 21364 RVA: 0x00340084 File Offset: 0x0033F084
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
		return this.ᜂ;
	}

	// Token: 0x06005375 RID: 21365 RVA: 0x003400C8 File Offset: 0x0033F0C8
	public void ᜀ(string A_0)
	{
		for (;;)
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					this.ᜂ = A_0;
					this.ᜁ = (ushort)this.ᜂ.Length;
					num = 1;
					continue;
				case 1:
					goto IL_5F;
				}
				if (!(this.ᜂ != A_0))
				{
					return;
				}
				num = 0;
			}
			IL_5F:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_75;
			}
		}
		IL_75:
		if (true)
		{
		}
		if (false)
		{
		}
	}

	// Token: 0x06005376 RID: 21366 RVA: 0x0034015C File Offset: 0x0033F15C
	public virtual int ᜂ()
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
		return 5;
	}

	// Token: 0x06005377 RID: 21367 RVA: 0x00340198 File Offset: 0x0033F198
	public spr\u240D()
	{
	}

	// Token: 0x06005378 RID: 21368 RVA: 0x003401B8 File Offset: 0x0033F1B8
	public spr\u240D(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x06005379 RID: 21369 RVA: 0x003401D8 File Offset: 0x0033F1D8
	public spr\u240D(int A_0) : base(A_0)
	{
	}

	// Token: 0x0600537A RID: 21370 RVA: 0x003401F8 File Offset: 0x0033F1F8
	public virtual void ᜀ(DataProvider A_0, int A_1, int A_2, ExcelVersion A_3)
	{
		int a_ = 9;
		for (;;)
		{
			this.ᜀ = A_0.ReadUInt16(A_1);
			this.ᜁ = A_0.ReadUInt16(A_1 + 2);
			int num;
			this.ᜀ(A_0.ReadString(A_1 + 4, (int)this.ᜁ, out num, false));
			if (this.m_iLength == 5 + num)
			{
				return;
			}
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_68;
			}
		}
		IL_68:
		if (false)
		{
		}
		throw new sprῩ(RecordTableEnumerator.b("刾Ṁ⩂ॄ≆❈ⱊ㥌❎煐㉒㭔㍖祘࡚⥜ⵞࡠൢɤ䝦ը๪ͬ࡮հ᭲啴፶ᙸ孺፼ၾꎂﶈꮊﮒ떔辠", a_));
	}

	// Token: 0x0600537B RID: 21371 RVA: 0x00340294 File Offset: 0x0033F294
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
		this.m_iLength = this.GetStoreSize(A_2);
		A_0.WriteUInt16(A_1, this.ᜀ);
		A_0.WriteUInt16(A_1 + 2, this.ᜁ);
		A_0.WriteByte(A_1 + 4, 1);
		A_0.WriteBytes(A_1 + 5, Encoding.Unicode.GetBytes(this.ᜂ), 0, (int)(this.ᜁ * 2));
	}

	// Token: 0x0600537C RID: 21372 RVA: 0x00340324 File Offset: 0x0033F324
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
		return (int)(5 + this.ᜁ * 2);
	}

	// Token: 0x0400270C RID: 9996
	[spr\u2429(0, 2)]
	private new ushort ᜀ;

	// Token: 0x0400270D RID: 9997
	[spr\u2429(2, 2)]
	private ushort ᜁ;

	// Token: 0x0400270E RID: 9998
	private string ᜂ = string.Empty;
}
