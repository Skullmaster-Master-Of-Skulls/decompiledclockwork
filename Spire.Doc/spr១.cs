using System;
using System.Collections.Generic;
using System.Reflection;

// Token: 0x02000288 RID: 648
[CLSCompliant(false)]
[DefaultMember("Item")]
internal class spr១ : List<object>
{
	// Token: 0x0600226E RID: 8814 RVA: 0x002376E8 File Offset: 0x002366E8
	internal spr១(sprḍ A_0)
	{
		this.ᜁ = A_0;
	}

	// Token: 0x0600226F RID: 8815 RVA: 0x00237704 File Offset: 0x00236704
	internal spr\u2227 ᜁ()
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
		this.ᜁ.ᜀ(20491, (ushort)base.Count);
		spr\u1CC1 spr_u1CC = new spr\u1CC1(61955);
		spr\u1CC1 spr_u1CC2 = new spr\u1CC1(61956);
		spr_u1CC.ᜁ(new byte[3]);
		spr_u1CC2.ᜁ(new byte[3]);
		spr_u1CC.ᜅ()[0] = (byte)base.Count;
		spr_u1CC2.ᜅ()[0] = (byte)base.Count;
		this.ᜁ.ᜆ(spr_u1CC);
		this.ᜁ.ᜆ(spr_u1CC2);
		spr\u2227 spr_u = new spr\u2227(spr_u1CC, spr_u1CC2);
		base.Add(spr_u);
		return spr_u;
	}

	// Token: 0x06002270 RID: 8816 RVA: 0x002377CC File Offset: 0x002367CC
	internal spr\u2227 ᜂ()
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
		this.ᜁ.ᜀ(20491, (ushort)base.Count);
		spr\u2227 spr_u = new spr\u2227();
		base.Add(spr_u);
		return spr_u;
	}

	// Token: 0x06002271 RID: 8817 RVA: 0x0023782C File Offset: 0x0023682C
	internal spr\u2227 ᜀ(int A_0)
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
		return (spr\u2227)base[A_0];
	}

	// Token: 0x06002272 RID: 8818 RVA: 0x00237874 File Offset: 0x00236874
	internal bool ᜀ()
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
		return this.ᜁ.ᜀ(12293, 1) == 1;
	}

	// Token: 0x06002273 RID: 8819 RVA: 0x002378C4 File Offset: 0x002368C4
	internal void ᜀ(bool A_0)
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
		this.ᜁ.ᜁ(12293, A_0 ? 1 : 0);
	}

	// Token: 0x06002274 RID: 8820 RVA: 0x0023791C File Offset: 0x0023691C
	internal void ᜃ()
	{
		switch (0)
		{
		default:
			for (;;)
			{
				int num = (int)(this.ᜁ.ᜁ(20491, 0) + 1);
				int num2 = 0;
				int num3 = 0;
				for (;;)
				{
					int num6;
					int num10;
					switch (num3)
					{
					case 0:
						goto IL_46C;
					case 1:
					{
						int num4;
						if (num4.ToString() == 61955.ToString())
						{
							num3 = 24;
							continue;
						}
						int num5 = this.ᜁ.ᜁ(num6).ᜈ();
						num3 = 25;
						continue;
					}
					case 2:
					{
						byte[] value = this.ᜁ.ᜁ(num6).ᜅ();
						List<ushort> list;
						list.Add(BitConverter.ToUInt16(value, 1));
						num3 = 13;
						continue;
					}
					case 3:
						num3 = 15;
						continue;
					case 4:
					{
						if (num2 >= num)
						{
							num3 = 23;
							continue;
						}
						spr\u2227 item = new spr\u2227();
						base.Add(item);
						num2++;
						num3 = 6;
						continue;
					}
					case 5:
					{
						int num7;
						if (num7 >= num)
						{
							num3 = 9;
							continue;
						}
						int num8;
						this.ᜀ(num7).ᜁ((ushort)num8);
						ushort num9;
						this.ᜀ(num7).ᜀ(num9);
						num7++;
						num3 = 12;
						continue;
					}
					case 6:
						goto IL_46C;
					case 7:
						if (num10 >= base.Count)
						{
							num3 = 30;
							continue;
						}
						num3 = 21;
						continue;
					case 8:
						if (num10 + 1 < base.Count)
						{
							num3 = 10;
							continue;
						}
						goto IL_1D3;
					case 9:
						return;
					case 10:
					{
						List<ushort> list;
						this.ᜀ(num10).ᜀ(list[num10]);
						num3 = 17;
						continue;
					}
					case 11:
						goto IL_256;
					case 12:
						goto IL_256;
					case 13:
						goto IL_490;
					case 14:
						num10 = 0;
						num3 = 22;
						continue;
					case 15:
						if (base.Count >= 1)
						{
							num3 = 14;
							continue;
						}
						return;
					case 16:
						goto IL_490;
					case 17:
						goto IL_1D3;
					case 18:
						goto IL_40C;
					case 19:
					{
						List<ushort> list2;
						this.ᜀ(num10).ᜁ(list2[num10]);
						goto IL_166;
					}
					case 20:
					{
						if (num6 >= this.ᜁ.ᜈ())
						{
							num3 = 3;
							continue;
						}
						int num4 = this.ᜁ.ᜁ(num6).ᜈ();
						num3 = 1;
						continue;
					}
					case 21:
					{
						List<ushort> list2;
						if (list2.Count > num10)
						{
							num3 = 19;
							continue;
						}
						goto IL_1D3;
					}
					case 22:
						if (true)
						{
						}
						goto IL_2A2;
					case 23:
						num3 = 27;
						continue;
					case 24:
					{
						byte[] value2 = this.ᜁ.ᜁ(num6).ᜅ();
						List<ushort> list2;
						list2.Add(BitConverter.ToUInt16(value2, 1));
						num3 = 16;
						continue;
					}
					case 25:
					{
						int num5;
						if (num5.ToString() == 61956.ToString())
						{
							num3 = 2;
							continue;
						}
						goto IL_490;
					}
					case 26:
					{
						ushort num11 = this.ᜁ.ᜁ(45087, 0);
						ushort num12 = this.ᜁ.ᜁ(45089, 0);
						ushort num13 = this.ᜁ.ᜁ(45090, 0);
						int num14 = (int)(num11 - num12 - num13);
						ushort num9 = this.ᜁ.ᜁ(36876, 720);
						int num8 = (num14 - (num - 1) * (int)num9) / num;
						int num7 = 0;
						num3 = 11;
						continue;
					}
					case 27:
					{
						if (this.ᜀ())
						{
							num3 = 26;
							continue;
						}
						List<ushort> list2 = new List<ushort>();
						List<ushort> list = new List<ushort>();
						num6 = 0;
						num3 = 29;
						continue;
					}
					case 28:
						goto IL_2A2;
					case 29:
						goto IL_40C;
					case 30:
						return;
					}
					break;
					IL_166:
					num3 = 8;
					continue;
					IL_1D3:
					num10++;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_166;
					default:
						if (false)
						{
						}
						num3 = 28;
						continue;
					}
					IL_256:
					num3 = 5;
					continue;
					IL_2A2:
					num3 = 7;
					continue;
					IL_40C:
					num3 = 20;
					continue;
					IL_46C:
					num3 = 4;
					continue;
					IL_490:
					num6++;
					num3 = 18;
				}
			}
			return;
		}
	}

	// Token: 0x0400210D RID: 8461
	private const int ᜀ = 720;

	// Token: 0x0400210E RID: 8462
	private sprḍ ᜁ;
}
