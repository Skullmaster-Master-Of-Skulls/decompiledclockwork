using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x020004D7 RID: 1239
[CLSCompliant(false)]
[spr\u2593(TBIFFRecord.WriteAccess)]
internal class spr\u1802 : BiffRecordRaw
{
	// Token: 0x06004C03 RID: 19459 RVA: 0x002E9608 File Offset: 0x002E8608
	public string ᜀ()
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

	// Token: 0x06004C04 RID: 19460 RVA: 0x002E964C File Offset: 0x002E864C
	public void ᜀ(string A_0)
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

	// Token: 0x06004C05 RID: 19461 RVA: 0x002E9690 File Offset: 0x002E8690
	public virtual int ᜃ()
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
		return 112;
	}

	// Token: 0x06004C06 RID: 19462 RVA: 0x002E96D0 File Offset: 0x002E86D0
	public virtual int ᜁ()
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
		return 112;
	}

	// Token: 0x06004C07 RID: 19463 RVA: 0x002E9710 File Offset: 0x002E8710
	public virtual bool ᜂ()
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
		return true;
	}

	// Token: 0x06004C08 RID: 19464 RVA: 0x002E974C File Offset: 0x002E874C
	public spr\u1802()
	{
		int a_ = 2;
		this.ᜄ = RecordTableEnumerator.b("洷䤹夻䰽", a_);
		base..ctor();
	}

	// Token: 0x06004C09 RID: 19465 RVA: 0x002E977C File Offset: 0x002E877C
	public spr\u1802(Stream A_0, out int A_1)
	{
		int a_ = 8;
		this.ᜄ = RecordTableEnumerator.b("欽㌿❁㙃", a_);
		base..ctor(A_0, out A_1);
	}

	// Token: 0x06004C0A RID: 19466 RVA: 0x002E97B0 File Offset: 0x002E87B0
	public spr\u1802(int A_0)
	{
		int a_ = 18;
		this.ᜄ = RecordTableEnumerator.b("ᵇ㥉⥋㱍", a_);
		base..ctor(A_0);
	}

	// Token: 0x06004C0B RID: 19467 RVA: 0x002E97E4 File Offset: 0x002E87E4
	public virtual void ᜀ(DataProvider A_0, int A_1, int A_2, ExcelVersion A_3)
	{
		for (;;)
		{
			uint num = (uint)A_0.ReadUInt16(A_1);
			if (true)
			{
			}
			int num2 = 1;
			for (;;)
			{
				switch (num2)
				{
				case 0:
				{
					int num3;
					this.ᜄ = A_0.ReadString16Bit(A_1, out num3);
					num2 = 2;
					continue;
				}
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					default:
						if (false)
						{
						}
						if ((ulong)num < (ulong)((long)A_2))
						{
							num2 = 0;
							continue;
						}
						return;
					}
					break;
				case 2:
					return;
				}
				break;
			}
		}
	}

	// Token: 0x06004C0C RID: 19468 RVA: 0x002E986C File Offset: 0x002E886C
	public virtual void ᜀ(DataProvider A_0, int A_1, ExcelVersion A_2)
	{
		int a_ = 19;
		for (;;)
		{
			this.m_iLength = A_1;
			int num = 6;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					int num2;
					int num3;
					if (num2 >= num3)
					{
						num = 2;
						continue;
					}
					if (true)
					{
					}
					A_0.WriteByte(A_1, 32);
					num2++;
					A_1++;
					num = 5;
					continue;
				}
				case 1:
					goto IL_DC;
				case 2:
					goto IL_F9;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_57;
					default:
					{
						if (false)
						{
						}
						int num2 = 0;
						int num3 = this.m_iLength - A_1;
						num = 1;
						continue;
					}
					}
					break;
				case 4:
					goto IL_57;
				case 5:
					goto IL_DC;
				case 6:
					if (this.ᜄ == null)
					{
						num = 4;
						continue;
					}
					goto IL_FB;
				case 7:
					if (A_1 - this.m_iLength < 112)
					{
						num = 3;
						continue;
					}
					goto IL_149;
				case 8:
					goto IL_FB;
				}
				break;
				IL_57:
				this.ᜄ = RecordTableEnumerator.b("᱈㡊⡌㵎", a_);
				num = 8;
				continue;
				IL_DC:
				num = 0;
				continue;
				IL_FB:
				A_0.WriteUInt16(A_1, (ushort)this.ᜄ.Length);
				A_1 += 2;
				A_0.WriteStringNoLenUpdateOffset(ref A_1, this.ᜄ, false);
				num = 7;
			}
		}
		IL_F9:
		IL_149:
		this.m_iLength = 112;
	}

	// Token: 0x04002294 RID: 8852
	private new const string ᜀ = "User";

	// Token: 0x04002295 RID: 8853
	private const int ᜁ = 112;

	// Token: 0x04002296 RID: 8854
	private const int ᜂ = 112;

	// Token: 0x04002297 RID: 8855
	private new const byte ᜃ = 32;

	// Token: 0x04002298 RID: 8856
	private string ᜄ;
}
