using System;
using System.ComponentModel;
using Spire.License;

// Token: 0x020001EE RID: 494
internal class spr\u2543
{
	// Token: 0x060015B1 RID: 5553 RVA: 0x00160028 File Offset: 0x0015F028
	internal static LicenseType ᜀ(License A_0)
	{
		int num = 4;
		for (;;)
		{
			switch (num)
			{
			case 0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				default:
					if (false)
					{
					}
					num = 6;
					continue;
				}
				break;
			case 1:
				return LicenseType.Demo;
			case 2:
				goto IL_DC;
			case 3:
				if (((LicenseInfo)A_0).Type != LicenseType.Runtime)
				{
					num = 1;
					continue;
				}
				return LicenseType.Runtime;
			case 5:
				if (A_0.GetType() == typeof(LicenseInfo))
				{
					num = 0;
					continue;
				}
				return LicenseType.Demo;
			case 6:
				if (((LicenseInfo)A_0).IsUpdateRightExpired)
				{
					num = 2;
					continue;
				}
				if (true)
				{
				}
				num = 3;
				continue;
			case 7:
				num = 5;
				continue;
			}
			if (A_0 == null)
			{
				return LicenseType.Demo;
			}
			num = 7;
		}
		return LicenseType.Demo;
		IL_DC:
		return LicenseType.Demo;
	}

	// Token: 0x060015B3 RID: 5555 RVA: 0x00160128 File Offset: 0x0015F128
	// Note: this type is marked as 'beforefieldinit'.
	static spr\u2543()
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
		spr\u2543.ᜀ = string.Empty;
		spr\u2543.ᜁ = string.Empty;
	}

	// Token: 0x040019DD RID: 6621
	internal static string ᜀ;

	// Token: 0x040019DE RID: 6622
	internal static string ᜁ;
}
