using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020004D3 RID: 1235
internal class spr\u19AF : IComparer, IComparer<spr\u2429>
{
	// Token: 0x06004BE2 RID: 19426 RVA: 0x002E7C04 File Offset: 0x002E6C04
	public int ᜀ(object A_0, object A_1)
	{
		switch (0)
		{
		default:
		{
			spr\u2429 spr_u;
			spr\u2429 spr_u2;
			for (;;)
			{
				spr_u = (spr\u2429)A_0;
				spr_u2 = (spr\u2429)A_1;
				int num = spr_u.ᜄ().CompareTo(spr_u2.ᜄ());
				int num2 = 3;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						return -1;
					case 1:
						num2 = 13;
						continue;
					case 2:
						if (spr_u2.ᜂ())
						{
							num2 = 16;
							continue;
						}
						return num;
					case 3:
						if (num == 0)
						{
							num2 = 10;
							continue;
						}
						return num;
					case 4:
						if (spr_u2.ᜂ())
						{
							num2 = 0;
							continue;
						}
						goto IL_143;
					case 5:
						if (spr_u.ᜂ())
						{
							num2 = 14;
							continue;
						}
						return num;
					case 6:
						goto IL_1CB;
					case 7:
						if (spr_u.ᜂ())
						{
							num2 = 1;
							continue;
						}
						goto IL_A4;
					case 8:
						if (spr_u2.ᜂ())
						{
							num2 = 15;
							continue;
						}
						return num;
					case 9:
						if (!spr_u.ᜂ())
						{
							num2 = 11;
							continue;
						}
						goto IL_19C;
					case 10:
						num2 = 9;
						continue;
					case 11:
						if (true)
						{
						}
						num2 = 8;
						continue;
					case 12:
						if (!spr_u.ᜂ())
						{
							num2 = 6;
							continue;
						}
						goto IL_143;
					case 13:
						if (!spr_u2.ᜂ())
						{
							num2 = 17;
							continue;
						}
						goto IL_A4;
					case 14:
						num2 = 2;
						continue;
					case 15:
						goto IL_19C;
					case 16:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_1CB;
						default:
							goto IL_17E;
						}
						break;
					case 17:
						return 1;
					}
					break;
					IL_A4:
					num2 = 12;
					continue;
					IL_143:
					num2 = 5;
					continue;
					IL_19C:
					num2 = 7;
					continue;
					IL_1CB:
					num2 = 4;
				}
			}
			IL_17E:
			if (false)
			{
			}
			return spr_u.ᜁ().CompareTo(spr_u2.ᜁ());
		}
		}
	}

	// Token: 0x06004BE3 RID: 19427 RVA: 0x002E7E2C File Offset: 0x002E6E2C
	public int ᜀ(spr\u2429 A_0, spr\u2429 A_1)
	{
		for (;;)
		{
			int num = A_0.ᜄ().CompareTo(A_1.ᜄ());
			int num2 = 1;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					num2 = 6;
					continue;
				case 1:
					if (num == 0)
					{
						num2 = 0;
						continue;
					}
					return num;
				case 2:
					return -1;
				case 3:
					num2 = 8;
					continue;
				case 4:
					if (A_0.ᜂ())
					{
						num2 = 17;
						continue;
					}
					return num;
				case 5:
					num2 = 10;
					continue;
				case 6:
					if (!A_0.ᜂ())
					{
						num2 = 13;
						continue;
					}
					goto IL_17F;
				case 7:
					if (A_0.ᜂ())
					{
						num2 = 3;
						continue;
					}
					goto IL_82;
				case 8:
					if (!A_1.ᜂ())
					{
						goto IL_1DB;
					}
					goto IL_82;
				case 9:
					goto IL_11D;
				case 10:
					if (A_1.ᜂ())
					{
						num2 = 2;
						continue;
					}
					goto IL_142;
				case 11:
					if (A_1.ᜂ())
					{
						if (true)
						{
						}
						num2 = 14;
						continue;
					}
					return num;
				case 12:
					if (!A_0.ᜂ())
					{
						num2 = 5;
						continue;
					}
					goto IL_142;
				case 13:
					num2 = 11;
					continue;
				case 14:
					goto IL_17F;
				case 15:
					if (!A_1.ᜂ())
					{
						return num;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1DB;
					default:
						if (false)
						{
						}
						num2 = 9;
						continue;
					}
					break;
				case 16:
					return 1;
				case 17:
					num2 = 15;
					continue;
				}
				break;
				IL_82:
				num2 = 12;
				continue;
				IL_142:
				num2 = 4;
				continue;
				IL_17F:
				num2 = 7;
				continue;
				IL_1DB:
				num2 = 16;
			}
		}
		IL_11D:
		return A_0.ᜁ().CompareTo(A_1.ᜁ());
	}
}
