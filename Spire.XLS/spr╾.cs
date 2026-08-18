using System;
using System.Collections.Generic;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x020003BD RID: 957
internal class spr\u257E : ICloneable
{
	// Token: 0x06003A6B RID: 14955 RVA: 0x0020E30C File Offset: 0x0020D30C
	private spr\u2237 ᜀ()
	{
		while (this.ᜀ != null)
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
				return this.ᜀ[0] as spr\u2237;
			}
		}
		if (true)
		{
		}
		return null;
	}

	// Token: 0x06003A6C RID: 14956 RVA: 0x0020E364 File Offset: 0x0020D364
	public int ᜂ()
	{
		spr\u2237 spr_u;
		for (;;)
		{
			spr_u = this.ᜀ();
			if (spr_u == null)
			{
				break;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_36;
			}
		}
		if (true)
		{
		}
		return -1;
		IL_36:
		if (false)
		{
		}
		return (int)spr_u.ᜀ();
	}

	// Token: 0x06003A6D RID: 14957 RVA: 0x0020E3B4 File Offset: 0x0020D3B4
	public void ᜀ(int A_0)
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
		this.ᜀ().ᜀ((ushort)A_0);
	}

	// Token: 0x06003A6E RID: 14958 RVA: 0x0020E3FC File Offset: 0x0020D3FC
	public spr\u257E()
	{
	}

	// Token: 0x06003A6F RID: 14959 RVA: 0x0020E410 File Offset: 0x0020D410
	public spr\u257E(IList<BiffRecordRaw> A_0, int A_1)
	{
		this.ᜀ(A_0, A_1);
	}

	// Token: 0x06003A70 RID: 14960 RVA: 0x0020E42C File Offset: 0x0020D42C
	public int ᜀ(IList<BiffRecordRaw> A_0, int A_1)
	{
		for (;;)
		{
			this.ᜀ = new List<BiffRecordRaw>();
			BiffRecordRaw biffRecordRaw = A_0[A_1];
			int num = 5;
			for (;;)
			{
				int count;
				switch (num)
				{
				case 0:
					biffRecordRaw = A_0[A_1];
					if (true)
					{
					}
					num = 3;
					continue;
				case 1:
					goto IL_8E;
				case 2:
					goto IL_70;
				case 3:
					if (biffRecordRaw.TypeCode == TBIFFRecord.StreamId)
					{
						num = 6;
						continue;
					}
					goto IL_72;
				case 4:
					goto IL_72;
				case 5:
					if (biffRecordRaw.TypeCode == TBIFFRecord.StreamId)
					{
						count = A_0.Count;
						num = 4;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_8E;
					default:
						if (false)
						{
						}
						num = 2;
						continue;
					}
					break;
				case 6:
					return A_1;
				}
				break;
				IL_72:
				this.ᜀ.Add(biffRecordRaw);
				A_1++;
				num = 1;
				continue;
				IL_8E:
				if (A_1 >= count)
				{
					return A_1;
				}
				num = 0;
			}
		}
		IL_70:
		throw new ArgumentOutOfRangeException();
	}

	// Token: 0x06003A71 RID: 14961 RVA: 0x0020E534 File Offset: 0x0020D534
	public void ᜀ(RecordArrayList A_0)
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (this.ᜀ.Count <= 0)
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
					goto IL_4C;
				default:
					if (false)
					{
					}
					num = 4;
					continue;
				}
				break;
			case 2:
				goto IL_4C;
			case 3:
				return;
			case 4:
				A_0.AddList(this.ᜀ);
				num = 3;
				continue;
			}
			if (this.ᜀ != null)
			{
				num = 2;
				continue;
			}
			break;
			IL_4C:
			num = 0;
		}
	}

	// Token: 0x06003A72 RID: 14962 RVA: 0x0020E5E0 File Offset: 0x0020D5E0
	public object ᜁ()
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
		spr\u257E spr_u257E = (spr\u257E)base.MemberwiseClone();
		spr_u257E.ᜀ = spr\u1CD3.ᜀ<BiffRecordRaw>(this.ᜀ);
		return spr_u257E;
	}

	// Token: 0x04001985 RID: 6533
	private List<BiffRecordRaw> ᜀ;
}
