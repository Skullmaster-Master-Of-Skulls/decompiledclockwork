using System;
using System.Collections.Generic;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x02000364 RID: 868
[spr\u2593(TBIFFRecord.MSODrawingGroup)]
[CLSCompliant(false)]
internal class spr\u23E6 : spr\u2453, ICloneable
{
	// Token: 0x06003524 RID: 13604 RVA: 0x001E63C8 File Offset: 0x001E53C8
	public spr\u23E6()
	{
	}

	// Token: 0x06003525 RID: 13605 RVA: 0x001E63E8 File Offset: 0x001E53E8
	public spr\u23E6(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x06003526 RID: 13606 RVA: 0x001E6408 File Offset: 0x001E5408
	public spr\u23E6(int A_0) : base(A_0)
	{
	}

	// Token: 0x06003527 RID: 13607 RVA: 0x001E6428 File Offset: 0x001E5428
	public spr\u1D3B[] ᜁ()
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
		return this.ᜂ.ToArray();
	}

	// Token: 0x06003528 RID: 13608 RVA: 0x001E6470 File Offset: 0x001E5470
	public new List<spr\u1D3B> ᜃ()
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

	// Token: 0x06003529 RID: 13609 RVA: 0x001E64B4 File Offset: 0x001E54B4
	public virtual bool ᜅ()
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
		return true;
	}

	// Token: 0x0600352A RID: 13610 RVA: 0x001E64F0 File Offset: 0x001E54F0
	protected new virtual int ᜀ()
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

	// Token: 0x0600352B RID: 13611 RVA: 0x001E652C File Offset: 0x001E552C
	public override void ᜂ()
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_99:
			num = 0;
			break;
		default:
			if (false)
			{
			}
			num = 1;
			break;
		}
		for (;;)
		{
			switch (num)
			{
			case 0:
				return;
			case 2:
				goto IL_52;
			}
			if (this.ᜀ == null)
			{
				return;
			}
			num = 2;
		}
		IL_52:
		if (true)
		{
		}
		base.ᜀ(base.TypeCode);
		base.ᜂ();
		this.ᜁ = new byte[this.ᜀ.Length];
		this.ᜀ.CopyTo(this.ᜁ, 0);
		this.ᜄ();
		goto IL_99;
	}

	// Token: 0x0600352C RID: 13612 RVA: 0x001E65DC File Offset: 0x001E55DC
	protected virtual void ᜄ()
	{
		for (;;)
		{
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
				{
					if (true)
					{
					}
					if (false)
					{
					}
					MemoryStream memoryStream = new MemoryStream(this.ᜀ);
					memoryStream.Position = (long)this.ᜀ();
					int num = this.ᜀ.Length;
					int num2 = 3;
					for (;;)
					{
						switch (num2)
						{
						case 0:
						{
							if (memoryStream.Position >= (long)num)
							{
								num2 = 1;
								continue;
							}
							spr\u1D3B item = spr\u231F.ᜀ(null, memoryStream);
							this.ᜂ.Add(item);
							num2 = 2;
							continue;
						}
						case 1:
							return;
						case 2:
							goto IL_72;
						case 3:
							goto IL_72;
						}
						break;
						IL_72:
						num2 = 0;
					}
					break;
				}
				}
			}
		}
	}

	// Token: 0x0600352D RID: 13613 RVA: 0x001E669C File Offset: 0x001E569C
	public override void ᜀ(ExcelVersion A_0)
	{
		switch (0)
		{
		default:
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_116:
				num = 3;
				break;
			default:
				if (false)
				{
				}
				num = 1;
				break;
			}
			int num2;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					this.ᜂ.Clear();
					int a_;
					Stream stream = this.ᜀ(out a_);
					this.ᜀ(stream, a_);
					num2 = (int)stream.Length;
					this.ᜁ = ((MemoryStream)stream).GetBuffer();
					num = 2;
					continue;
				}
				case 2:
					this.m_iLength = ((num2 > this.MaximumRecordSize) ? this.MaximumRecordSize : num2);
					this.AutoGrowData = true;
					base.ᜀ(0, this.ᜁ, 0, this.m_iLength);
					base.ᜀ(A_0);
					num = 5;
					continue;
				case 3:
				{
					int iLength = this.m_iLength;
					int a_2 = num2 - iLength;
					base.ᜎ().ᜀ(this.ᜁ, iLength, a_2);
					this.m_iLength = base.ᜎ().ᜆ();
					num = 4;
					continue;
				}
				case 4:
					return;
				case 5:
					goto IL_10D;
				}
				if (true)
				{
				}
				if (this.ᜂ.Count <= 0)
				{
					return;
				}
				num = 0;
			}
			IL_10D:
			if (num2 > this.MaximumRecordSize)
			{
				goto IL_116;
			}
			return;
		}
		}
	}

	// Token: 0x0600352E RID: 13614 RVA: 0x001E6810 File Offset: 0x001E5810
	protected new virtual Stream ᜀ(out int A_0)
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
		A_0 = 0;
		return new MemoryStream();
	}

	// Token: 0x0600352F RID: 13615 RVA: 0x001E6854 File Offset: 0x001E5854
	protected new void ᜀ(Stream A_0, int A_1)
	{
		for (;;)
		{
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
				{
					if (true)
					{
					}
					if (false)
					{
					}
					int count = this.ᜂ.Count;
					int num = 0;
					int num2 = 1;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_5E;
						case 1:
							goto IL_5E;
						case 2:
						{
							if (num >= count)
							{
								num2 = 3;
								continue;
							}
							spr\u1D3B spr_u1D3B = this.ᜂ[num];
							spr_u1D3B.ᜆ(A_0);
							num++;
							num2 = 0;
							continue;
						}
						case 3:
							return;
						}
						break;
						IL_5E:
						num2 = 2;
					}
					break;
				}
				}
			}
		}
	}

	// Token: 0x06003530 RID: 13616 RVA: 0x001E68FC File Offset: 0x001E58FC
	public override int ᜁ(ExcelVersion A_0)
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_62:
			if (true)
			{
			}
			num = 2;
			break;
		default:
			if (false)
			{
			}
			num = 1;
			break;
		}
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_52;
			case 2:
				goto IL_72;
			}
			if (!base.NeedInfill)
			{
				goto IL_74;
			}
			num = 0;
		}
		IL_52:
		this.ᜀ(A_0);
		base.NeedInfill = false;
		goto IL_62;
		IL_72:
		IL_74:
		return this.m_iLength;
	}

	// Token: 0x06003531 RID: 13617 RVA: 0x001E6984 File Offset: 0x001E5984
	public new void ᜀ(spr\u1D3B A_0)
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
		this.ᜂ.Add(A_0);
	}

	// Token: 0x06003532 RID: 13618 RVA: 0x001E69CC File Offset: 0x001E59CC
	protected override spr\u1A58 ᜆ()
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
		spr\u1A58 spr_u1A = base.ᜆ();
		spr_u1A.ᜀ(TBIFFRecord.MSODrawingGroup);
		return spr_u1A;
	}

	// Token: 0x06003533 RID: 13619 RVA: 0x001E6A1C File Offset: 0x001E5A1C
	public new object ᜇ()
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
		spr\u23E6 spr_u23E = (spr\u23E6)base.ᜇ();
		this.ᜂ = new List<spr\u1D3B>(spr_u23E.ᜂ);
		return spr_u23E;
	}

	// Token: 0x0400172F RID: 5935
	private new const int ᜀ = 0;

	// Token: 0x04001730 RID: 5936
	protected new byte[] ᜁ;

	// Token: 0x04001731 RID: 5937
	protected new List<spr\u1D3B> ᜂ = new List<spr\u1D3B>();
}
