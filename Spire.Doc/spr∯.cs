using System;
using System.Collections.Generic;
using System.IO;
using Spire.CompoundFile.Doc;

// Token: 0x02000388 RID: 904
internal class spr\u222F
{
	// Token: 0x06003270 RID: 12912 RVA: 0x002E6F28 File Offset: 0x002E5F28
	public List<spr\u2486> ᜁ()
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

	// Token: 0x06003271 RID: 12913 RVA: 0x002E6F6C File Offset: 0x002E5F6C
	public spr\u222F()
	{
		this.ᜀ = new List<spr\u2486>();
		base..ctor();
	}

	// Token: 0x06003272 RID: 12914 RVA: 0x002E6F8C File Offset: 0x002E5F8C
	public spr\u222F(byte[] A_0)
	{
		int a_ = 11;
		this.ᜀ = new List<spr\u2486>();
		base..ctor();
		if (A_0 == null)
		{
			throw new ArgumentNullException(ClipboardData.b("ᕰቲŴᙶ", a_));
		}
		int i = 0;
		int num = A_0.Length;
		this.ᜀ = new List<spr\u2486>();
		int num2 = 0;
		while (i < num)
		{
			this.ᜀ.Add(new spr\u2486(A_0, i, num2));
			i += 128;
			num2++;
		}
	}

	// Token: 0x06003273 RID: 12915 RVA: 0x002E7008 File Offset: 0x002E6008
	public int ᜀ()
	{
		switch (0)
		{
		default:
		{
			int result;
			for (;;)
			{
				IL_33:
				result = -1;
				int num = 0;
				int count = this.ᜀ.Count;
				for (;;)
				{
					int num2 = 0;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_AB;
						case 1:
							goto IL_C7;
						case 2:
							result = num;
							num2 = 4;
							continue;
						case 3:
						{
							spr\u2486 spr_u;
							if (spr_u.ᜄ() == spr\u2486.EntryType.Invalid)
							{
								num2 = 2;
								continue;
							}
							num++;
							if (true)
							{
							}
							num2 = 6;
							continue;
						}
						case 4:
							goto IL_7A;
						case 5:
						{
							if (num >= count)
							{
								num2 = 1;
								continue;
							}
							spr\u2486 spr_u = this.ᜀ[num];
							num2 = 3;
							continue;
						}
						case 6:
							goto IL_AB;
						}
						goto IL_33;
						IL_AB:
						num2 = 5;
					}
					IL_C7:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_DD;
					}
				}
			}
			IL_7A:
			return result;
			IL_DD:
			if (false)
			{
			}
			return result;
		}
		}
	}

	// Token: 0x06003274 RID: 12916 RVA: 0x002E70FC File Offset: 0x002E60FC
	internal void ᜀ(spr\u2486 A_0)
	{
		int num;
		for (;;)
		{
			num = this.ᜀ();
			if (num >= 0)
			{
				break;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_38;
			}
		}
		this.ᜀ[num] = A_0;
		A_0.ᜃ(num);
		return;
		IL_38:
		if (true)
		{
		}
		if (false)
		{
		}
		this.ᜀ.Add(A_0);
	}

	// Token: 0x06003275 RID: 12917 RVA: 0x002E7168 File Offset: 0x002E6168
	public void ᜀ(Stream A_0)
	{
		int a_ = 1;
		for (;;)
		{
			IL_09:
			int num = 3;
			for (;;)
			{
				int num2;
				int count;
				switch (num)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_09;
					default:
						if (false)
						{
						}
						goto IL_B2;
					}
					break;
				case 1:
					goto IL_B2;
				case 2:
					goto IL_3C;
				case 4:
				{
					if (true)
					{
					}
					if (num2 >= count)
					{
						num = 5;
						continue;
					}
					spr\u2486 spr_u = this.ᜀ[num2];
					spr_u.ᜀ(A_0);
					num2++;
					num = 1;
					continue;
				}
				case 5:
					return;
				}
				if (A_0 == null)
				{
					num = 2;
					continue;
				}
				num2 = 0;
				count = this.ᜀ.Count;
				num = 0;
				continue;
				IL_B2:
				num = 4;
			}
		}
		IL_3C:
		throw new ArgumentNullException(ClipboardData.b("ᑦᵨᥪ࡬๮ᱰ", a_));
	}

	// Token: 0x040027E6 RID: 10214
	private List<spr\u2486> ᜀ;
}
