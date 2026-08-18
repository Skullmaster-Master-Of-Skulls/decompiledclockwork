using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x02000428 RID: 1064
[spr\u2593(TBIFFRecord.LabelRanges)]
[CLSCompliant(false)]
internal class sprỌ : spr\u251F
{
	// Token: 0x0600407B RID: 16507 RVA: 0x002433D4 File Offset: 0x002423D4
	public ushort ᜅ()
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

	// Token: 0x0600407C RID: 16508 RVA: 0x00243418 File Offset: 0x00242418
	public new TAddr[] ᜃ()
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
		return this.ᜁ;
	}

	// Token: 0x0600407D RID: 16509 RVA: 0x0024345C File Offset: 0x0024245C
	public void ᜁ(TAddr[] A_0)
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
		this.ᜁ = A_0;
		this.ᜀ = ((A_0 != null) ? ((ushort)A_0.Length) : 0);
	}

	// Token: 0x0600407E RID: 16510 RVA: 0x002434B4 File Offset: 0x002424B4
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
		return this.ᜂ;
	}

	// Token: 0x0600407F RID: 16511 RVA: 0x002434F8 File Offset: 0x002424F8
	public new TAddr[] ᜀ()
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

	// Token: 0x06004080 RID: 16512 RVA: 0x0024353C File Offset: 0x0024253C
	public new void ᜀ(TAddr[] A_0)
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
		this.ᜂ = ((A_0 != null) ? ((ushort)A_0.Length) : 0);
	}

	// Token: 0x06004081 RID: 16513 RVA: 0x00243594 File Offset: 0x00242594
	public virtual int ᜄ()
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
		return 4;
	}

	// Token: 0x06004082 RID: 16514 RVA: 0x002435D0 File Offset: 0x002425D0
	public sprỌ()
	{
	}

	// Token: 0x06004083 RID: 16515 RVA: 0x002435E4 File Offset: 0x002425E4
	public sprỌ(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x06004084 RID: 16516 RVA: 0x002435FC File Offset: 0x002425FC
	public sprỌ(int A_0) : base(A_0)
	{
	}

	// Token: 0x06004085 RID: 16517 RVA: 0x00243610 File Offset: 0x00242610
	public override void ᜂ()
	{
		for (;;)
		{
			base.ᜰ();
			this.ᜁ = new TAddr[(int)this.ᜀ];
			int num = 2;
			int num2 = 0;
			int num3 = 3;
			for (;;)
			{
				switch (num3)
				{
				case 0:
					goto IL_9E;
				case 1:
					if (num != this.m_iLength)
					{
						num3 = 5;
						continue;
					}
					return;
				case 2:
					num3 = 1;
					continue;
				case 3:
					goto IL_A0;
				case 4:
				{
					this.ᜂ = base.ᜌ(num);
					this.ᜃ = new TAddr[(int)this.ᜂ];
					num += 2;
					int num4 = 0;
					num3 = 9;
					continue;
				}
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_9E;
					default:
						goto IL_13E;
					}
					break;
				case 6:
					goto IL_A0;
				case 7:
				{
					int num4;
					if (num4 >= (int)this.ᜂ)
					{
						num3 = 2;
						continue;
					}
					this.ᜃ[num4] = base.\u1716(num);
					num4++;
					num += 8;
					num3 = 0;
					continue;
				}
				case 8:
					if (num2 >= (int)this.ᜀ)
					{
						num3 = 4;
						continue;
					}
					this.ᜁ[num2] = base.\u1716(num);
					num2++;
					num += 8;
					if (true)
					{
					}
					num3 = 6;
					continue;
				case 9:
					goto IL_55;
				}
				break;
				IL_55:
				num3 = 7;
				continue;
				IL_9E:
				goto IL_55;
				IL_A0:
				num3 = 8;
			}
		}
		IL_13E:
		if (false)
		{
		}
		throw new sprῩ();
	}

	// Token: 0x06004086 RID: 16518 RVA: 0x0024379C File Offset: 0x0024279C
	public override void ᜀ(ExcelVersion A_0)
	{
		for (;;)
		{
			IL_28:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_E5:
				goto IL_10B;
			case 1:
				goto IL_48;
			default:
				goto IL_48;
			}
			int num;
			int num2;
			int num3;
			for (;;)
			{
				IL_02:
				switch (num)
				{
				case 0:
					if (num2 >= (int)this.ᜀ)
					{
						num = 6;
						continue;
					}
					base.ᜀ(num3, this.ᜁ[num2]);
					num2++;
					num3 += 8;
					num = 7;
					continue;
				case 1:
					goto IL_E7;
				case 2:
					goto IL_68;
				case 3:
				{
					int num4;
					if (num4 >= (int)this.ᜂ)
					{
						num = 4;
						continue;
					}
					base.ᜀ(num3, this.ᜃ[num4]);
					num4++;
					num3 += 8;
					num = 5;
					continue;
				}
				case 4:
					return;
				case 5:
					goto IL_E7;
				case 6:
				{
					base.ᜀ(num3, this.ᜂ);
					num3 += 2;
					int num4 = 0;
					num = 1;
					continue;
				}
				case 7:
					goto IL_E5;
				}
				goto IL_28;
				IL_E7:
				num = 3;
			}
			IL_68:
			goto IL_10B;
			IL_48:
			if (false)
			{
			}
			base.ᜰ();
			num3 = 2;
			num2 = 0;
			if (true)
			{
			}
			num = 2;
			goto IL_02;
			IL_10B:
			num = 0;
			goto IL_02;
		}
	}

	// Token: 0x04001CD8 RID: 7384
	[spr\u2429(0, 2)]
	private new ushort ᜀ;

	// Token: 0x04001CD9 RID: 7385
	private new TAddr[] ᜁ;

	// Token: 0x04001CDA RID: 7386
	private new ushort ᜂ;

	// Token: 0x04001CDB RID: 7387
	private new TAddr[] ᜃ;
}
