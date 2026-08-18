using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x020002A8 RID: 680
[CLSCompliant(false)]
[spr\u2593(TBIFFRecord.TabId)]
internal class spr᧒ : BiffRecordRaw
{
	// Token: 0x06002917 RID: 10519 RVA: 0x00174F40 File Offset: 0x00173F40
	public ushort[] ᜁ()
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

	// Token: 0x06002918 RID: 10520 RVA: 0x00174F84 File Offset: 0x00173F84
	public void ᜀ(ushort[] A_0)
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

	// Token: 0x06002919 RID: 10521 RVA: 0x00174FC8 File Offset: 0x00173FC8
	public spr᧒()
	{
	}

	// Token: 0x0600291A RID: 10522 RVA: 0x00174FF0 File Offset: 0x00173FF0
	public spr᧒(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x0600291B RID: 10523 RVA: 0x00175018 File Offset: 0x00174018
	public spr᧒(int A_0) : base(A_0)
	{
	}

	// Token: 0x0600291C RID: 10524 RVA: 0x00175040 File Offset: 0x00174040
	public virtual void ᜀ(DataProvider A_0, int A_1, int A_2, ExcelVersion A_3)
	{
		for (;;)
		{
			for (;;)
			{
				this.ᜀ();
				this.ᜀ = new ushort[base.Length / 2];
				int num = 0;
				int num2 = this.m_iLength + A_1;
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
					int num3 = 2;
					for (;;)
					{
						switch (num3)
						{
						case 0:
							goto IL_62;
						case 1:
							if (A_1 >= num2)
							{
								num3 = 3;
								continue;
							}
							if (true)
							{
							}
							this.ᜀ[num] = A_0.ReadUInt16(A_1);
							num++;
							A_1 += 2;
							num3 = 0;
							continue;
						case 2:
							goto IL_62;
						case 3:
							return;
						}
						break;
						IL_62:
						num3 = 1;
					}
					break;
				}
				}
			}
		}
	}

	// Token: 0x0600291D RID: 10525 RVA: 0x001750FC File Offset: 0x001740FC
	public virtual void ᜀ(DataProvider A_0, int A_1, ExcelVersion A_2)
	{
		for (;;)
		{
			for (;;)
			{
				this.m_iLength = this.GetStoreSize(ExcelVersion.Version97to2003);
				int num = 0;
				int num2 = this.ᜀ.Length;
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
					int num3 = 2;
					for (;;)
					{
						switch (num3)
						{
						case 0:
							return;
						case 1:
							goto IL_56;
						case 2:
							goto IL_56;
						case 3:
							if (num >= num2)
							{
								num3 = 0;
								continue;
							}
							A_0.WriteUInt16(A_1, this.ᜀ[num]);
							A_1 += 2;
							num++;
							if (true)
							{
							}
							num3 = 1;
							continue;
						}
						break;
						IL_56:
						num3 = 3;
					}
					break;
				}
				}
			}
		}
	}

	// Token: 0x0600291E RID: 10526 RVA: 0x001751AC File Offset: 0x001741AC
	private void ᜀ()
	{
		int a_ = 2;
		if (this.m_iLength % 2 != 0)
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
				break;
			}
			throw new sprῩ(RecordTableEnumerator.b("男弹主夽┿Ł⅃⩅⑇㥉ṋ⭍㍏㵑♓㉕", a_));
		}
	}

	// Token: 0x0600291F RID: 10527 RVA: 0x00175210 File Offset: 0x00174210
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
		return this.ᜀ.Length * 2;
	}

	// Token: 0x0400139B RID: 5019
	private new ushort[] ᜀ = new ushort[]
	{
		1
	};
}
