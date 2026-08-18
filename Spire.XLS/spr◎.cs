using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x0200047B RID: 1147
[spr\u2593(TBIFFRecord.ExternalSourceInfo)]
[CLSCompliant(false)]
internal class spr\u25CE : BiffRecordRaw
{
	// Token: 0x06004629 RID: 17961 RVA: 0x002AA9A8 File Offset: 0x002A99A8
	public spr\u25CE()
	{
	}

	// Token: 0x0600462A RID: 17962 RVA: 0x002AA9BC File Offset: 0x002A99BC
	public spr\u25CE(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x0600462B RID: 17963 RVA: 0x002AA9D4 File Offset: 0x002A99D4
	public spr\u25CE(int A_0) : base(A_0)
	{
	}

	// Token: 0x0600462C RID: 17964 RVA: 0x002AA9E8 File Offset: 0x002A99E8
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
		return this.ᜂ;
	}

	// Token: 0x0600462D RID: 17965 RVA: 0x002AAA2C File Offset: 0x002A9A2C
	public ushort ᜌ()
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
		return BiffRecordRaw.ᜀ(this.ᜂ, 7);
	}

	// Token: 0x0600462E RID: 17966 RVA: 0x002AAA74 File Offset: 0x002A9A74
	public void ᜄ(ushort A_0)
	{
		int a_ = 4;
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_91;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_2D;
				default:
					if (false)
					{
					}
					num = 3;
					continue;
				}
				break;
			case 3:
				if (A_0 > 4)
				{
					if (true)
					{
					}
					num = 0;
					continue;
				}
				goto IL_9D;
			}
			goto IL_29;
			IL_2D:
			num = 2;
			continue;
			IL_29:
			if (A_0 >= 1)
			{
				goto IL_2D;
			}
			break;
		}
		IL_37:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("䰹崻刽㔿❁", a_), RecordTableEnumerator.b("氹崻刽㔿❁摃╅⥇⑉≋⅍⑏牑㙓㍕硗㙙㥛ⵝ፟䉡啣䙥१ѩ࡫乭ᝯqᅳ᝵౷ό๻幽ꢇ뺉", a_));
		IL_91:
		goto IL_37;
		IL_9D:
		BiffRecordRaw.ᜀ(ref this.ᜂ, 7, A_0);
	}

	// Token: 0x0600462F RID: 17967 RVA: 0x002AAB2C File Offset: 0x002A9B2C
	public bool ᜈ()
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

	// Token: 0x06004630 RID: 17968 RVA: 0x002AAB70 File Offset: 0x002A9B70
	public void ᜁ(bool A_0)
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

	// Token: 0x06004631 RID: 17969 RVA: 0x002AABB4 File Offset: 0x002A9BB4
	public bool ᜉ()
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

	// Token: 0x06004632 RID: 17970 RVA: 0x002AABF8 File Offset: 0x002A9BF8
	public void ᜅ(bool A_0)
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

	// Token: 0x06004633 RID: 17971 RVA: 0x002AAC3C File Offset: 0x002A9C3C
	public bool ᜋ()
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

	// Token: 0x06004634 RID: 17972 RVA: 0x002AAC80 File Offset: 0x002A9C80
	public void ᜂ(bool A_0)
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
		this.ᜅ = A_0;
	}

	// Token: 0x06004635 RID: 17973 RVA: 0x002AACC4 File Offset: 0x002A9CC4
	public bool ᜀ()
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
		return this.ᜆ;
	}

	// Token: 0x06004636 RID: 17974 RVA: 0x002AAD08 File Offset: 0x002A9D08
	public new void ᜃ(bool A_0)
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
		this.ᜆ = A_0;
	}

	// Token: 0x06004637 RID: 17975 RVA: 0x002AAD4C File Offset: 0x002A9D4C
	public new bool ᜃ()
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
		return this.ᜇ;
	}

	// Token: 0x06004638 RID: 17976 RVA: 0x002AAD90 File Offset: 0x002A9D90
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
		this.ᜇ = A_0;
	}

	// Token: 0x06004639 RID: 17977 RVA: 0x002AADD4 File Offset: 0x002A9DD4
	public bool ᜅ()
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
		return this.ᜈ;
	}

	// Token: 0x0600463A RID: 17978 RVA: 0x002AAE18 File Offset: 0x002A9E18
	public void ᜄ(bool A_0)
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
		this.ᜈ = A_0;
	}

	// Token: 0x0600463B RID: 17979 RVA: 0x002AAE5C File Offset: 0x002A9E5C
	public ushort ᜂ()
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
		return this.ᜉ;
	}

	// Token: 0x0600463C RID: 17980 RVA: 0x002AAEA0 File Offset: 0x002A9EA0
	public void ᜂ(ushort A_0)
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
		this.ᜉ = A_0;
	}

	// Token: 0x0600463D RID: 17981 RVA: 0x002AAEE4 File Offset: 0x002A9EE4
	public ushort ᜄ()
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
		return this.ᜊ;
	}

	// Token: 0x0600463E RID: 17982 RVA: 0x002AAF28 File Offset: 0x002A9F28
	public void ᜁ(ushort A_0)
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
		this.ᜊ = A_0;
	}

	// Token: 0x0600463F RID: 17983 RVA: 0x002AAF6C File Offset: 0x002A9F6C
	public ushort ᜊ()
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
		return this.ᜋ;
	}

	// Token: 0x06004640 RID: 17984 RVA: 0x002AAFB0 File Offset: 0x002A9FB0
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
		this.ᜋ = A_0;
	}

	// Token: 0x06004641 RID: 17985 RVA: 0x002AAFF4 File Offset: 0x002A9FF4
	public ushort ᜇ()
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
		return this.ᜌ;
	}

	// Token: 0x06004642 RID: 17986 RVA: 0x002AB038 File Offset: 0x002AA038
	public void ᜅ(ushort A_0)
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
		this.ᜌ = A_0;
	}

	// Token: 0x06004643 RID: 17987 RVA: 0x002AB07C File Offset: 0x002AA07C
	public ushort ᜁ()
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
		return this.\u170D;
	}

	// Token: 0x06004644 RID: 17988 RVA: 0x002AB0C0 File Offset: 0x002AA0C0
	public new void ᜃ(ushort A_0)
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
		this.\u170D = A_0;
	}

	// Token: 0x06004645 RID: 17989 RVA: 0x002AB104 File Offset: 0x002AA104
	public virtual void ᜀ(DataProvider A_0, int A_1, int A_2, ExcelVersion A_3)
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
		this.ᜂ = A_0.ReadUInt16(A_1);
		this.ᜃ = A_0.ReadBit(A_1, 3);
		this.ᜄ = A_0.ReadBit(A_1, 4);
		this.ᜅ = A_0.ReadBit(A_1, 5);
		this.ᜆ = A_0.ReadBit(A_1, 6);
		this.ᜇ = A_0.ReadBit(A_1, 7);
		this.ᜈ = A_0.ReadBit(A_1 + 1, 0);
		this.ᜉ = A_0.ReadUInt16(A_1 + 2);
		this.ᜊ = A_0.ReadUInt16(A_1 + 4);
		this.ᜋ = A_0.ReadUInt16(A_1 + 6);
		this.ᜌ = A_0.ReadUInt16(A_1 + 8);
		this.\u170D = A_0.ReadUInt16(A_1 + 10);
	}

	// Token: 0x06004646 RID: 17990 RVA: 0x002AB1F0 File Offset: 0x002AA1F0
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
		A_0.WriteUInt16(A_1, this.ᜂ);
		A_0.WriteBit(A_1, this.ᜃ, 3);
		A_0.WriteBit(A_1, this.ᜄ, 4);
		A_0.WriteBit(A_1, this.ᜅ, 5);
		A_0.WriteBit(A_1, this.ᜆ, 6);
		A_0.WriteBit(A_1, this.ᜇ, 7);
		A_0.WriteBit(A_1 + 1, this.ᜈ, 0);
		A_0.WriteUInt16(A_1 + 2, this.ᜉ);
		A_0.WriteUInt16(A_1 + 4, this.ᜊ);
		A_0.WriteUInt16(A_1 + 6, this.ᜋ);
		A_0.WriteUInt16(A_1 + 8, this.ᜌ);
		A_0.WriteUInt16(A_1 + 10, this.\u170D);
		this.m_iLength = 12;
	}

	// Token: 0x06004647 RID: 17991 RVA: 0x002AB2E4 File Offset: 0x002AA2E4
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
		return 12;
	}

	// Token: 0x04002009 RID: 8201
	private new const ushort ᜀ = 7;

	// Token: 0x0400200A RID: 8202
	private const int ᜁ = 12;

	// Token: 0x0400200B RID: 8203
	[spr\u2429(0, 2)]
	private ushort ᜂ;

	// Token: 0x0400200C RID: 8204
	[spr\u2429(0, 3, TFieldType.Bit)]
	private new bool ᜃ;

	// Token: 0x0400200D RID: 8205
	[spr\u2429(0, 4, TFieldType.Bit)]
	private bool ᜄ;

	// Token: 0x0400200E RID: 8206
	[spr\u2429(0, 5, TFieldType.Bit)]
	private bool ᜅ;

	// Token: 0x0400200F RID: 8207
	[spr\u2429(0, 6, TFieldType.Bit)]
	private bool ᜆ;

	// Token: 0x04002010 RID: 8208
	[spr\u2429(0, 7, TFieldType.Bit)]
	private bool ᜇ;

	// Token: 0x04002011 RID: 8209
	[spr\u2429(1, 0, TFieldType.Bit)]
	private bool ᜈ;

	// Token: 0x04002012 RID: 8210
	[spr\u2429(2, 2)]
	private ushort ᜉ;

	// Token: 0x04002013 RID: 8211
	[spr\u2429(4, 2)]
	private ushort ᜊ;

	// Token: 0x04002014 RID: 8212
	[spr\u2429(6, 2)]
	private ushort ᜋ;

	// Token: 0x04002015 RID: 8213
	[spr\u2429(8, 2)]
	private ushort ᜌ;

	// Token: 0x04002016 RID: 8214
	[spr\u2429(10, 2)]
	private ushort \u170D;
}
