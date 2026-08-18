using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x0200026B RID: 619
[CLSCompliant(false)]
[spr\u2593(TBIFFRecord.MSODrawing)]
internal class spr\u2293 : spr\u251F, ICloneable, spr\u21D9
{
	// Token: 0x06002567 RID: 9575 RVA: 0x0015B578 File Offset: 0x0015A578
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
		return base.Length;
	}

	// Token: 0x06002568 RID: 9576 RVA: 0x0015B5BC File Offset: 0x0015A5BC
	public void ᜁ(int A_0)
	{
		int a_ = 8;
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				if (A_0 > this.ᜀ.Length)
				{
					num = 3;
					continue;
				}
				goto IL_96;
			case 2:
				goto IL_5D;
			case 3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_5D;
				default:
					goto IL_8E;
				}
				break;
			}
			if (A_0 < 0)
			{
				if (true)
				{
				}
				num = 2;
				continue;
			}
			goto IL_96;
			IL_5D:
			num = 1;
		}
		IL_8E:
		if (false)
		{
		}
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("氽┿⅁⭃㑅ⱇى⥋⁍㝏♑㱓", a_));
		IL_96:
		this.m_iLength = A_0;
	}

	// Token: 0x06002569 RID: 9577 RVA: 0x0015B668 File Offset: 0x0015A668
	public spr\u2293()
	{
	}

	// Token: 0x0600256A RID: 9578 RVA: 0x0015B67C File Offset: 0x0015A67C
	public spr\u2293(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x0600256B RID: 9579 RVA: 0x0015B694 File Offset: 0x0015A694
	public spr\u2293(int A_0) : base(A_0)
	{
	}

	// Token: 0x0600256C RID: 9580 RVA: 0x0015B6A8 File Offset: 0x0015A6A8
	public override void ᜂ()
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
	}

	// Token: 0x0600256D RID: 9581 RVA: 0x0015B6E4 File Offset: 0x0015A6E4
	public override void ᜀ(ExcelVersion A_0)
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
		this.m_iLength = this.ᜀ.Length;
	}

	// Token: 0x0600256E RID: 9582 RVA: 0x0015B730 File Offset: 0x0015A730
	public virtual bool ᜀ()
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

	// Token: 0x0600256F RID: 9583 RVA: 0x0015B76C File Offset: 0x0015A76C
	public new void ᜀ(int A_0, byte[] A_1)
	{
		int a_ = 15;
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_3D;
				default:
					if (false)
					{
					}
					if (A_1.Length < A_0)
					{
						num = 2;
						continue;
					}
					goto IL_91;
				}
				break;
			case 2:
				goto IL_8F;
			case 3:
				goto IL_3D;
			}
			if (A_0 >= 0)
			{
				if (true)
				{
				}
				num = 3;
				continue;
			}
			break;
			IL_3D:
			num = 0;
		}
		IL_3F:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⥄≆❈ⱊ㥌❎", a_));
		IL_8F:
		goto IL_3F;
		IL_91:
		this.ᜀ = new byte[A_0];
		Array.Copy(A_1, 0, this.ᜀ, 0, A_0);
	}

	// Token: 0x06002570 RID: 9584 RVA: 0x0015B828 File Offset: 0x0015A828
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
		this.m_iLength = A_0;
	}

	// Token: 0x06002571 RID: 9585 RVA: 0x0015B86C File Offset: 0x0015A86C
	public override int ᜁ(ExcelVersion A_0)
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
		return this.ᜀ.Length;
	}
}
