using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x020002EC RID: 748
[CLSCompliant(false)]
[spr\u2593(TBIFFRecord.DBCell)]
internal class spr\u2466 : spr\u191F
{
	// Token: 0x06002E6B RID: 11883 RVA: 0x001A0C48 File Offset: 0x0019FC48
	public int ᜁ()
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
		return this.ᜂ;
	}

	// Token: 0x06002E6C RID: 11884 RVA: 0x001A0C8C File Offset: 0x0019FC8C
	public new void ᜀ(int A_0)
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
		this.ᜂ = A_0;
	}

	// Token: 0x06002E6D RID: 11885 RVA: 0x001A0CD0 File Offset: 0x0019FCD0
	public ushort[] ᜂ()
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
		return this.ᜃ;
	}

	// Token: 0x06002E6E RID: 11886 RVA: 0x001A0D14 File Offset: 0x0019FD14
	public new void ᜀ(ushort[] A_0)
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
		this.ᜃ = A_0;
	}

	// Token: 0x06002E6F RID: 11887 RVA: 0x001A0D58 File Offset: 0x0019FD58
	public virtual int ᜃ()
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
		return 4;
	}

	// Token: 0x06002E70 RID: 11888 RVA: 0x001A0D94 File Offset: 0x0019FD94
	public spr\u2466()
	{
		ushort[] array = new ushort[1];
		this.ᜃ = array;
		base..ctor();
	}

	// Token: 0x06002E71 RID: 11889 RVA: 0x001A0DB8 File Offset: 0x0019FDB8
	public spr\u2466(Stream A_0, out int A_1)
	{
		ushort[] array = new ushort[1];
		this.ᜃ = array;
		base..ctor(A_0, out A_1);
	}

	// Token: 0x06002E72 RID: 11890 RVA: 0x001A0DDC File Offset: 0x0019FDDC
	public spr\u2466(int A_0)
	{
		ushort[] array = new ushort[1];
		this.ᜃ = array;
		base..ctor(A_0);
	}

	// Token: 0x06002E73 RID: 11891 RVA: 0x001A0E00 File Offset: 0x0019FE00
	public virtual void ᜀ(DataProvider A_0, int A_1, int A_2, ExcelVersion A_3)
	{
		for (;;)
		{
			this.ᜂ = A_0.ReadInt32(A_1);
			A_1 += 4;
			this.ᜀ();
			int num = (this.m_iLength - 4) / 2;
			this.ᜃ = new ushort[num];
			int num2 = 0;
			int num3 = 1;
			for (;;)
			{
				switch (num3)
				{
				case 0:
					goto IL_5B;
				case 1:
					if (true)
					{
					}
					goto IL_5B;
				case 2:
					return;
				case 3:
					if (num2 < num)
					{
						this.ᜃ[num2] = A_0.ReadUInt16(A_1);
						num2++;
						A_1 += 2;
						num3 = 0;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					default:
						if (false)
						{
						}
						num3 = 2;
						continue;
					}
					break;
				}
				break;
				IL_5B:
				num3 = 3;
			}
		}
	}

	// Token: 0x06002E74 RID: 11892 RVA: 0x001A0ECC File Offset: 0x0019FECC
	public virtual void ᜀ(DataProvider A_0, int A_1, ExcelVersion A_2)
	{
		for (;;)
		{
			int num = this.ᜃ.Length;
			this.m_iLength = this.GetStoreSize(A_2);
			A_0.WriteInt32(A_1, this.ᜂ);
			A_1 += 4;
			int num2 = 0;
			int num3 = 2;
			for (;;)
			{
				switch (num3)
				{
				case 0:
					return;
				case 1:
					goto IL_54;
				case 2:
					if (true)
					{
					}
					goto IL_54;
				case 3:
					if (num2 < num)
					{
						A_0.WriteUInt16(A_1, this.ᜃ[num2]);
						A_1 += 2;
						num2++;
						num3 = 1;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					default:
						if (false)
						{
						}
						num3 = 0;
						continue;
					}
					break;
				}
				break;
				IL_54:
				num3 = 3;
			}
		}
	}

	// Token: 0x06002E75 RID: 11893 RVA: 0x001A0F94 File Offset: 0x0019FF94
	private new void ᜀ()
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
			if ((base.Length - 2) % 2 == 0)
			{
				return;
			}
			if (true)
			{
			}
			break;
		}
		throw new sprῩ(RecordTableEnumerator.b("Cхେ⽉⁋≍ɏ㝑㝓㥕⩗㹙", a_));
	}

	// Token: 0x06002E76 RID: 11894 RVA: 0x001A0FFC File Offset: 0x0019FFFC
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
		return 4 + this.ᜃ.Length * 2;
	}

	// Token: 0x040014EA RID: 5354
	private new const int ᜀ = 4;

	// Token: 0x040014EB RID: 5355
	private const int ᜁ = 2;

	// Token: 0x040014EC RID: 5356
	[spr\u2429(0, 4, false)]
	private int ᜂ;

	// Token: 0x040014ED RID: 5357
	private new ushort[] ᜃ;
}
