using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x02000475 RID: 1141
[spr\u2593(TBIFFRecord.RString)]
[CLSCompliant(false)]
internal class spr\u19F6 : spr\u251F, spr\u23A5, spr\u22BB
{
	// Token: 0x060045ED RID: 17901 RVA: 0x002A9718 File Offset: 0x002A8718
	public new int ᜃ()
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

	// Token: 0x060045EE RID: 17902 RVA: 0x002A975C File Offset: 0x002A875C
	public void ᜁ(int A_0)
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

	// Token: 0x060045EF RID: 17903 RVA: 0x002A97A0 File Offset: 0x002A87A0
	public int ᜁ()
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

	// Token: 0x060045F0 RID: 17904 RVA: 0x002A97E4 File Offset: 0x002A87E4
	public new void ᜀ(int A_0)
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

	// Token: 0x060045F1 RID: 17905 RVA: 0x002A9828 File Offset: 0x002A8828
	public ushort ᜇ()
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

	// Token: 0x060045F2 RID: 17906 RVA: 0x002A986C File Offset: 0x002A886C
	public new void ᜀ(ushort A_0)
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

	// Token: 0x060045F3 RID: 17907 RVA: 0x002A98B0 File Offset: 0x002A88B0
	public string ᜄ()
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
		return this.ᜃ;
	}

	// Token: 0x060045F4 RID: 17908 RVA: 0x002A98F4 File Offset: 0x002A88F4
	public new void ᜀ(string A_0)
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

	// Token: 0x060045F5 RID: 17909 RVA: 0x002A9938 File Offset: 0x002A8938
	public spr\u19F6.ᜀ[] ᜅ()
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

	// Token: 0x060045F6 RID: 17910 RVA: 0x002A997C File Offset: 0x002A897C
	public new void ᜀ(spr\u19F6.ᜀ[] A_0)
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
		this.ᜅ = A_0;
		this.ᜄ = ((A_0 != null) ? ((ushort)A_0.Length) : 0);
	}

	// Token: 0x060045F7 RID: 17911 RVA: 0x002A99D4 File Offset: 0x002A89D4
	public virtual int ᜆ()
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
		return 8;
	}

	// Token: 0x060045F8 RID: 17912 RVA: 0x002A9A10 File Offset: 0x002A8A10
	public spr\u19F6()
	{
	}

	// Token: 0x060045F9 RID: 17913 RVA: 0x002A9A24 File Offset: 0x002A8A24
	public spr\u19F6(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x060045FA RID: 17914 RVA: 0x002A9A3C File Offset: 0x002A8A3C
	public spr\u19F6(int A_0) : base(A_0)
	{
	}

	// Token: 0x060045FB RID: 17915 RVA: 0x002A9A50 File Offset: 0x002A8A50
	public override void ᜂ()
	{
		for (;;)
		{
			this.ᜀ = (int)base.ᜌ(0);
			this.ᜁ = (int)base.ᜌ(2);
			this.ᜂ = base.ᜌ(4);
			int num = 6;
			this.ᜃ = base.ᜋ(ref num);
			this.ᜄ = base.ᜌ(num);
			num += 2;
			this.ᜅ = new spr\u19F6.ᜀ[(int)this.ᜄ];
			int num2 = 0;
			int num3 = 3;
			for (;;)
			{
				switch (num3)
				{
				case 0:
					return;
				case 1:
					if (num2 >= (int)this.ᜄ)
					{
						num3 = 0;
						continue;
					}
					this.ᜅ[num2].ᜀ = base.ᜌ(num);
					this.ᜅ[num2].ᜁ = base.ᜌ(num + 2);
					num2++;
					num += 4;
					num3 = 2;
					continue;
				case 2:
					IL_111:
					goto IL_85;
				case 3:
					if (true)
					{
					}
					goto IL_85;
				}
				break;
				IL_85:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_111;
				default:
					if (false)
					{
					}
					num3 = 1;
					break;
				}
			}
		}
	}

	// Token: 0x060045FC RID: 17916 RVA: 0x002A9B74 File Offset: 0x002A8B74
	public override void ᜀ(ExcelVersion A_0)
	{
		for (;;)
		{
			this.AutoGrowData = true;
			base.ᜀ(0, (ushort)this.ᜀ);
			base.ᜀ(2, (ushort)this.ᜁ);
			base.ᜀ(4, this.ᜂ);
			this.m_iLength = 6;
			base.ᜀ(ref this.m_iLength, this.ᜃ);
			base.ᜀ(this.m_iLength, this.ᜄ);
			this.m_iLength += 2;
			int num = 0;
			if (true)
			{
			}
			int num2 = 2;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					if (num >= (int)this.ᜄ)
					{
						num2 = 1;
						continue;
					}
					base.ᜀ(this.m_iLength, this.ᜅ[num].ᜀ);
					base.ᜀ(this.m_iLength + 2, this.ᜅ[num].ᜁ);
					num++;
					this.m_iLength += 4;
					num2 = 3;
					continue;
				case 1:
					return;
				case 2:
					goto IL_98;
				case 3:
					IL_138:
					goto IL_98;
				}
				break;
				IL_98:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_138;
				default:
					if (false)
					{
					}
					num2 = 0;
					break;
				}
			}
		}
	}

	// Token: 0x060045FD RID: 17917 RVA: 0x002A9CC0 File Offset: 0x002A8CC0
	string spr\u22BB.ᜀ()
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
		return this.ᜄ();
	}

	// Token: 0x04001FE3 RID: 8163
	[spr\u2429(0, 2)]
	private new int ᜀ;

	// Token: 0x04001FE4 RID: 8164
	[spr\u2429(2, 2)]
	private new int ᜁ;

	// Token: 0x04001FE5 RID: 8165
	[spr\u2429(4, 2)]
	private new ushort ᜂ;

	// Token: 0x04001FE6 RID: 8166
	private new string ᜃ;

	// Token: 0x04001FE7 RID: 8167
	private new ushort ᜄ;

	// Token: 0x04001FE8 RID: 8168
	private new spr\u19F6.ᜀ[] ᜅ;

	// Token: 0x02000476 RID: 1142
	[CLSCompliant(false)]
	internal new struct ᜀ
	{
		// Token: 0x04001FE9 RID: 8169
		public ushort ᜀ;

		// Token: 0x04001FEA RID: 8170
		public ushort ᜁ;
	}
}
