using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x02000365 RID: 869
[spr\u2593(TBIFFRecord.HorizontalPageBreaks)]
[CLSCompliant(false)]
internal class spr\u2539 : spr\u251F
{
	// Token: 0x06003534 RID: 13620 RVA: 0x001E6A78 File Offset: 0x001E5A78
	public new spr\u2539.ᜀ[] ᜀ()
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

	// Token: 0x06003535 RID: 13621 RVA: 0x001E6ABC File Offset: 0x001E5ABC
	public new void ᜀ(spr\u2539.ᜀ[] A_0)
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
		this.ᜃ = ((A_0 != null) ? ((ushort)A_0.Length) : 0);
	}

	// Token: 0x06003536 RID: 13622 RVA: 0x001E6B14 File Offset: 0x001E5B14
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
		return 2;
	}

	// Token: 0x06003537 RID: 13623 RVA: 0x001E6B50 File Offset: 0x001E5B50
	public spr\u2539()
	{
	}

	// Token: 0x06003538 RID: 13624 RVA: 0x001E6B64 File Offset: 0x001E5B64
	public spr\u2539(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x06003539 RID: 13625 RVA: 0x001E6B7C File Offset: 0x001E5B7C
	public spr\u2539(int A_0) : base(A_0)
	{
	}

	// Token: 0x0600353A RID: 13626 RVA: 0x001E6B90 File Offset: 0x001E5B90
	public override void ᜂ()
	{
		int num2;
		int num3;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
		{
			IL_78:
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_A3;
				case 1:
					num = 4;
					continue;
				case 2:
					goto IL_A1;
				case 3:
				{
					if (num2 >= (int)this.ᜃ)
					{
						num = 1;
						continue;
					}
					ushort a_ = base.ᜌ(num3);
					ushort a_2 = base.ᜌ(num3 + 2);
					ushort a_3 = base.ᜌ(num3 + 4);
					this.ᜄ[num2] = new spr\u2539.ᜀ(a_, a_2, a_3);
					num2++;
					num3 += 6;
					if (true)
					{
					}
					num = 5;
					continue;
				}
				case 4:
					if (num3 != this.m_iLength)
					{
						num = 2;
						continue;
					}
					return;
				case 5:
					goto IL_A3;
				}
				goto IL_55;
				IL_A3:
				num = 3;
			}
			IL_A1:
			throw new sprῩ();
		}
		default:
			if (false)
			{
			}
			switch (0)
			{
			}
			break;
		}
		IL_55:
		this.ᜃ = base.ᜌ(0);
		this.ᜄ = new spr\u2539.ᜀ[(int)this.ᜃ];
		num3 = 2;
		num2 = 0;
		goto IL_78;
	}

	// Token: 0x0600353B RID: 13627 RVA: 0x001E6CB4 File Offset: 0x001E5CB4
	public override void ᜀ(ExcelVersion A_0)
	{
		for (;;)
		{
			this.ᜀ = new byte[this.GetStoreSize(ExcelVersion.Version97to2003)];
			base.ᜀ(0, this.ᜃ);
			this.m_iLength = 2;
			int num = 0;
			int num2 = 0;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_54;
				case 1:
					return;
				case 2:
					goto IL_54;
				case 3:
					if (num >= (int)this.ᜃ)
					{
						if (true)
						{
						}
						num2 = 1;
						continue;
					}
					base.ᜀ(this.m_iLength, this.ᜄ[num].ᜃ());
					base.ᜀ(this.m_iLength + 2, this.ᜄ[num].ᜀ());
					base.ᜀ(this.m_iLength + 4, this.ᜄ[num].ᜁ());
					num++;
					this.m_iLength += 6;
					num2 = 2;
					continue;
				}
				break;
				IL_54:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return;
				default:
					if (false)
					{
					}
					num2 = 3;
					break;
				}
			}
		}
	}

	// Token: 0x0600353C RID: 13628 RVA: 0x001E6DC8 File Offset: 0x001E5DC8
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
		return (int)(2 + 6 * this.ᜃ);
	}

	// Token: 0x04001732 RID: 5938
	private new const int ᜀ = 2;

	// Token: 0x04001733 RID: 5939
	internal new const int ᜁ = 6;

	// Token: 0x04001734 RID: 5940
	internal new const int ᜂ = 2;

	// Token: 0x04001735 RID: 5941
	[spr\u2429(0, 2)]
	private new ushort ᜃ;

	// Token: 0x04001736 RID: 5942
	private new spr\u2539.ᜀ[] ᜄ;

	// Token: 0x02000366 RID: 870
	internal new class ᜀ : ICloneable
	{
		// Token: 0x0600353D RID: 13629 RVA: 0x001E6E10 File Offset: 0x001E5E10
		public ᜀ()
		{
		}

		// Token: 0x0600353E RID: 13630 RVA: 0x001E6E24 File Offset: 0x001E5E24
		public ᜀ(ushort A_0, ushort A_1, ushort A_2)
		{
			this.ᜀ = A_0;
			this.ᜁ = A_1;
			this.ᜂ = A_2;
		}

		// Token: 0x0600353F RID: 13631 RVA: 0x001E6E4C File Offset: 0x001E5E4C
		public ushort ᜃ()
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

		// Token: 0x06003540 RID: 13632 RVA: 0x001E6E90 File Offset: 0x001E5E90
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
			this.ᜀ = A_0;
		}

		// Token: 0x06003541 RID: 13633 RVA: 0x001E6ED4 File Offset: 0x001E5ED4
		public ushort ᜀ()
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
			return this.ᜁ;
		}

		// Token: 0x06003542 RID: 13634 RVA: 0x001E6F18 File Offset: 0x001E5F18
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
			this.ᜁ = A_0;
		}

		// Token: 0x06003543 RID: 13635 RVA: 0x001E6F5C File Offset: 0x001E5F5C
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
			return this.ᜂ;
		}

		// Token: 0x06003544 RID: 13636 RVA: 0x001E6FA0 File Offset: 0x001E5FA0
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
			this.ᜂ = A_0;
		}

		// Token: 0x06003545 RID: 13637 RVA: 0x001E6FE4 File Offset: 0x001E5FE4
		public object ᜂ()
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
			return base.MemberwiseClone();
		}

		// Token: 0x04001737 RID: 5943
		private ushort ᜀ;

		// Token: 0x04001738 RID: 5944
		private ushort ᜁ;

		// Token: 0x04001739 RID: 5945
		private ushort ᜂ;
	}
}
