using System;
using System.IO;
using System.Text;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x02000399 RID: 921
[spr\u2593(TBIFFRecord.Footer)]
[spr\u2593(TBIFFRecord.Header)]
[CLSCompliant(false)]
internal class sprᢔ : BiffRecordRaw
{
	// Token: 0x06003829 RID: 14377 RVA: 0x001F670C File Offset: 0x001F570C
	public string ᜁ()
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

	// Token: 0x0600382A RID: 14378 RVA: 0x001F6750 File Offset: 0x001F5750
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
		this.ᜀ = A_0;
	}

	// Token: 0x0600382B RID: 14379 RVA: 0x001F6794 File Offset: 0x001F5794
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
		return 0;
	}

	// Token: 0x0600382C RID: 14380 RVA: 0x001F67D0 File Offset: 0x001F57D0
	public sprᢔ()
	{
	}

	// Token: 0x0600382D RID: 14381 RVA: 0x001F67F0 File Offset: 0x001F57F0
	public sprᢔ(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x0600382E RID: 14382 RVA: 0x001F6810 File Offset: 0x001F5810
	public sprᢔ(int A_0) : base(A_0)
	{
	}

	// Token: 0x0600382F RID: 14383 RVA: 0x001F6830 File Offset: 0x001F5830
	public virtual void ᜀ(DataProvider A_0, int A_1, int A_2, ExcelVersion A_3)
	{
		int a_ = 9;
		for (;;)
		{
			IL_09:
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_09;
					}
					if (false)
					{
					}
					break;
				case 1:
				{
					int num2;
					if (num2 != this.m_iLength)
					{
						if (true)
						{
						}
						num = 3;
						continue;
					}
					return;
				}
				case 2:
				{
					int num2;
					this.ᜀ = A_0.ReadString16Bit(A_1, out num2);
					num = 1;
					continue;
				}
				case 3:
					goto IL_A6;
				}
				if (this.m_iLength <= 0)
				{
					return;
				}
				num = 2;
			}
		}
		IL_A6:
		throw new sprῩ(RecordTableEnumerator.b("栾㍀ⱂ⭄⁆楈㡊㥌㵎㡐㵒㉔睖㙘⥚絜㭞`ᝢѤ䝦ը๪ͬ࡮հ᭲孴", a_));
	}

	// Token: 0x06003830 RID: 14384 RVA: 0x001F68E8 File Offset: 0x001F58E8
	public virtual void ᜀ(DataProvider A_0, int A_1, ExcelVersion A_2)
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			for (;;)
			{
				IL_28:
				switch (num)
				{
				case 0:
					if (this.m_iLength > 0)
					{
						num = 1;
						continue;
					}
					return;
				case 1:
					if (true)
					{
					}
					A_0.WriteString16BitUpdateOffset(ref A_1, this.ᜀ);
					num = 2;
					continue;
				case 2:
					return;
				}
				goto IL_3A;
			}
			return;
		}
		if (false)
		{
		}
		IL_3A:
		this.m_iLength = this.GetStoreSize(A_2);
		num = 0;
		goto IL_28;
	}

	// Token: 0x06003831 RID: 14385 RVA: 0x001F6978 File Offset: 0x001F5978
	public virtual int ᜀ(ExcelVersion A_0)
	{
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			goto IL_4E;
		case 1:
			goto IL_20;
		default:
			goto IL_20;
		}
		int num;
		for (;;)
		{
			IL_30:
			if (true)
			{
			}
			switch (num)
			{
			case 0:
				if (this.ᜀ.Length != 0)
				{
					num = 1;
					continue;
				}
				return 0;
			case 1:
				goto IL_90;
			case 2:
				num = 0;
				continue;
			}
			goto IL_4E;
		}
		IL_90:
		return 3 + Encoding.Unicode.GetByteCount(this.ᜀ);
		IL_20:
		if (false)
		{
		}
		num = 3;
		goto IL_30;
		IL_4E:
		if (this.ᜀ != null)
		{
			num = 2;
			goto IL_30;
		}
		return 0;
	}

	// Token: 0x040018CA RID: 6346
	private new string ᜀ = string.Empty;
}
