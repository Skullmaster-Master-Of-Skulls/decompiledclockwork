using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x0200047E RID: 1150
[CLSCompliant(false)]
[spr\u2593(TBIFFRecord.ObjectProtect)]
internal class spr\u17CF : BiffRecordRaw
{
	// Token: 0x06004663 RID: 18019 RVA: 0x002AB9A0 File Offset: 0x002AA9A0
	public bool ᜁ()
	{
		while (this.ᜁ != 0)
		{
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				return true;
			}
		}
		return false;
	}

	// Token: 0x06004664 RID: 18020 RVA: 0x002AB9E8 File Offset: 0x002AA9E8
	public void ᜀ(bool A_0)
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
		this.ᜁ = (A_0 ? 1 : 0);
	}

	// Token: 0x06004665 RID: 18021 RVA: 0x002ABA34 File Offset: 0x002AAA34
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
		return 2;
	}

	// Token: 0x06004666 RID: 18022 RVA: 0x002ABA70 File Offset: 0x002AAA70
	public virtual int ᜀ()
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
		return 2;
	}

	// Token: 0x06004667 RID: 18023 RVA: 0x002ABAAC File Offset: 0x002AAAAC
	public spr\u17CF()
	{
	}

	// Token: 0x06004668 RID: 18024 RVA: 0x002ABAC0 File Offset: 0x002AAAC0
	public spr\u17CF(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x06004669 RID: 18025 RVA: 0x002ABAD8 File Offset: 0x002AAAD8
	public spr\u17CF(int A_0) : base(A_0)
	{
	}

	// Token: 0x0600466A RID: 18026 RVA: 0x002ABAEC File Offset: 0x002AAAEC
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
		this.ᜁ = A_0.ReadUInt16(A_1);
	}

	// Token: 0x0600466B RID: 18027 RVA: 0x002ABB34 File Offset: 0x002AAB34
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
		A_0.WriteUInt16(A_1, this.ᜁ);
		this.m_iLength = 2;
	}

	// Token: 0x0600466C RID: 18028 RVA: 0x002ABB84 File Offset: 0x002AAB84
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
		return 2;
	}

	// Token: 0x0400201F RID: 8223
	private new const int ᜀ = 2;

	// Token: 0x04002020 RID: 8224
	[spr\u2429(0, 2)]
	private ushort ᜁ;
}
