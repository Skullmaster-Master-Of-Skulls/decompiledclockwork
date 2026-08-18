using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x0200053A RID: 1338
[spr\u2593(TBIFFRecord.Dimensions)]
[CLSCompliant(false)]
internal class spr\u203C : BiffRecordRaw
{
	// Token: 0x0600517F RID: 20863 RVA: 0x0032EB84 File Offset: 0x0032DB84
	public ushort ᜅ()
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
		return this.ᜅ;
	}

	// Token: 0x06005180 RID: 20864 RVA: 0x0032EBC8 File Offset: 0x0032DBC8
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
		return this.ᜁ;
	}

	// Token: 0x06005181 RID: 20865 RVA: 0x0032EC0C File Offset: 0x0032DC0C
	public void ᜁ(int A_0)
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

	// Token: 0x06005182 RID: 20866 RVA: 0x0032EC50 File Offset: 0x0032DC50
	public new int ᜃ()
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

	// Token: 0x06005183 RID: 20867 RVA: 0x0032EC94 File Offset: 0x0032DC94
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
		this.ᜂ = A_0;
	}

	// Token: 0x06005184 RID: 20868 RVA: 0x0032ECD8 File Offset: 0x0032DCD8
	public ushort ᜆ()
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

	// Token: 0x06005185 RID: 20869 RVA: 0x0032ED1C File Offset: 0x0032DD1C
	public void ᜁ(ushort A_0)
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
		this.ᜃ = A_0;
	}

	// Token: 0x06005186 RID: 20870 RVA: 0x0032ED60 File Offset: 0x0032DD60
	public ushort ᜁ()
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
		return this.ᜄ;
	}

	// Token: 0x06005187 RID: 20871 RVA: 0x0032EDA4 File Offset: 0x0032DDA4
	public void ᜀ(ushort A_0)
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
		this.ᜄ = A_0;
	}

	// Token: 0x06005188 RID: 20872 RVA: 0x0032EDE8 File Offset: 0x0032DDE8
	public virtual int ᜂ()
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
		return 14;
	}

	// Token: 0x06005189 RID: 20873 RVA: 0x0032EE28 File Offset: 0x0032DE28
	public virtual int ᜄ()
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		return 14;
	}

	// Token: 0x0600518A RID: 20874 RVA: 0x0032EE68 File Offset: 0x0032DE68
	public spr\u203C()
	{
	}

	// Token: 0x0600518B RID: 20875 RVA: 0x0032EE7C File Offset: 0x0032DE7C
	public spr\u203C(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x0600518C RID: 20876 RVA: 0x0032EE94 File Offset: 0x0032DE94
	public spr\u203C(int A_0) : base(A_0)
	{
	}

	// Token: 0x0600518D RID: 20877 RVA: 0x0032EEA8 File Offset: 0x0032DEA8
	public virtual void ᜀ(DataProvider A_0, int A_1, int A_2, ExcelVersion A_3)
	{
		for (;;)
		{
			IL_1E:
			this.ᜁ = A_0.ReadInt32(A_1);
			A_1 += 4;
			this.ᜂ = A_0.ReadInt32(A_1);
			A_1 += 4;
			this.ᜃ = A_0.ReadUInt16(A_1);
			A_1 += 2;
			this.ᜄ = A_0.ReadUInt16(A_1);
			A_1 += 2;
			this.ᜅ = A_0.ReadUInt16(A_1);
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_B0:
				num = 1;
				break;
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				num = 2;
				break;
			}
			for (;;)
			{
				switch (num)
				{
				case 0:
					return;
				case 1:
					this.ᜄ = this.ᜃ + 1;
					num = 0;
					continue;
				case 2:
					goto IL_A2;
				}
				goto IL_1E;
			}
			IL_A2:
			if (this.ᜄ <= this.ᜃ)
			{
				goto IL_B0;
			}
			break;
		}
	}

	// Token: 0x0600518E RID: 20878 RVA: 0x0032EF90 File Offset: 0x0032DF90
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
		this.m_iLength = 14;
		A_0.WriteInt32(A_1, this.ᜁ);
		A_1 += 4;
		A_0.WriteInt32(A_1, this.ᜂ);
		A_1 += 4;
		A_0.WriteUInt16(A_1, this.ᜃ);
		A_1 += 2;
		A_0.WriteUInt16(A_1, this.ᜄ);
		A_1 += 2;
		A_0.WriteUInt16(A_1, this.ᜅ);
	}

	// Token: 0x0400245B RID: 9307
	private new const int ᜀ = 14;

	// Token: 0x0400245C RID: 9308
	[spr\u2429(0, 4, true)]
	private int ᜁ;

	// Token: 0x0400245D RID: 9309
	[spr\u2429(4, 4, true)]
	private int ᜂ;

	// Token: 0x0400245E RID: 9310
	[spr\u2429(8, 2)]
	private new ushort ᜃ;

	// Token: 0x0400245F RID: 9311
	[spr\u2429(10, 2)]
	private ushort ᜄ;

	// Token: 0x04002460 RID: 9312
	[spr\u2429(12, 2)]
	private ushort ᜅ;
}
