using System;
using System.Collections.Generic;
using System.IO;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x02000367 RID: 871
[spr\u2593(TBIFFRecord.HeaderFooterImage)]
[CLSCompliant(false)]
internal class spr\u1976 : spr\u23E6, spr\u21D9
{
	// Token: 0x06003546 RID: 13638 RVA: 0x001E7028 File Offset: 0x001E6028
	public spr\u1976()
	{
	}

	// Token: 0x06003547 RID: 13639 RVA: 0x001E703C File Offset: 0x001E603C
	public spr\u1976(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x06003548 RID: 13640 RVA: 0x001E7054 File Offset: 0x001E6054
	public spr\u1976(int A_0) : base(A_0)
	{
	}

	// Token: 0x06003549 RID: 13641 RVA: 0x001E7068 File Offset: 0x001E6068
	protected override int ᜀ()
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
		return spr\u1976.ᜃ;
	}

	// Token: 0x0600354A RID: 13642 RVA: 0x001E70A8 File Offset: 0x001E60A8
	protected override Stream ᜀ(out int A_0)
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
		int count = this.ᜂ.Count;
		A_0 = 1;
		MemoryStream memoryStream = new MemoryStream();
		memoryStream.Write(spr\u1976.ᜀ, 0, spr\u1976.ᜀ.Length);
		return memoryStream;
	}

	// Token: 0x0600354B RID: 13643 RVA: 0x001E710C File Offset: 0x001E610C
	protected override int ᜀ(List<byte[]> A_0, BiffRecordRaw A_1)
	{
		int a_ = 18;
		switch (0)
		{
		default:
		{
			int num = 0;
			byte[] array;
			int num2;
			for (;;)
			{
				switch (num)
				{
				case 1:
					goto IL_F7;
				case 2:
					if (A_1 == null)
					{
						num = 6;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					default:
					{
						if (false)
						{
						}
						array = A_1.Data;
						num2 = array.Length;
						int num3 = num2 - spr\u1976.ᜃ;
						num = 5;
						continue;
					}
					}
					break;
				case 3:
					goto IL_54;
				case 4:
				{
					int num3;
					byte[] array2 = new byte[num3];
					num2 = num3;
					Buffer.BlockCopy(array, spr\u1976.ᜃ, array2, 0, num3);
					array = array2;
					num = 1;
					continue;
				}
				case 5:
				{
					int num3;
					if (num3 > 0)
					{
						num = 4;
						continue;
					}
					goto IL_124;
				}
				case 6:
					goto IL_11F;
				}
				if (A_0 == null)
				{
					num = 3;
				}
				else
				{
					if (true)
					{
					}
					num = 2;
				}
			}
			IL_54:
			throw new ArgumentNullException(RecordTableEnumerator.b("⥇㡉㹋ᱍ㕏ㅑ㭓⑕㱗⥙", a_));
			IL_F7:
			goto IL_124;
			IL_11F:
			throw new ArgumentNullException(RecordTableEnumerator.b("㩇⽉⽋⅍≏㙑", a_));
			IL_124:
			A_0.Add(array);
			return num2;
		}
		}
	}

	// Token: 0x0600354C RID: 13644 RVA: 0x001E7248 File Offset: 0x001E6248
	protected override spr\u1A58 ᜆ()
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
		spr\u1A58 spr_u1A = new sprᣁ(this);
		spr_u1A.ᜁ(new EventHandler(this.ᜀ));
		return spr_u1A;
	}

	// Token: 0x0600354D RID: 13645 RVA: 0x001E72A0 File Offset: 0x001E62A0
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

	// Token: 0x0600354E RID: 13646 RVA: 0x001E72E4 File Offset: 0x001E62E4
	// Note: this type is marked as 'beforefieldinit'.
	static spr\u1976()
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
		spr\u1976.ᜀ = new byte[]
		{
			102,
			8,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			2,
			0
		};
		spr\u1976.ᜁ = new byte[]
		{
			102,
			8,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			1,
			0
		};
		spr\u1976.ᜂ = new byte[]
		{
			102,
			8,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			6,
			0
		};
		spr\u1976.ᜃ = spr\u1976.ᜀ.Length;
	}

	// Token: 0x0400173A RID: 5946
	internal new static readonly byte[] ᜀ;

	// Token: 0x0400173B RID: 5947
	internal new static readonly byte[] ᜁ;

	// Token: 0x0400173C RID: 5948
	internal new static readonly byte[] ᜂ;

	// Token: 0x0400173D RID: 5949
	internal new static readonly int ᜃ;
}
