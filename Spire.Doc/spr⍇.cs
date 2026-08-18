using System;
using System.Collections.Generic;
using System.ComponentModel;
using Spire.CompoundFile.Doc;
using Spire.License;

// Token: 0x020003D8 RID: 984
internal class spr\u2347
{
	// Token: 0x06003769 RID: 14185 RVA: 0x0033C5D4 File Offset: 0x0033B5D4
	static spr\u2347()
	{
		int a_ = 1;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		spr\u2347.ᜀ = new List<string>
		{
			ClipboardData.b("㑦ᥨɪὬ੮彰㝲ᩴᑶ⽸ቺ᡼ࡾꮄ킆麗", a_),
			ClipboardData.b("㑦ᥨɪὬ੮彰㝲ᩴᑶ⽸ቺ᡼ࡾꮄ솆力ﲎ", a_)
		};
	}

	// Token: 0x0600376A RID: 14186 RVA: 0x0033C64C File Offset: 0x0033B64C
	internal static bool ᜀ(InternalLicense A_0)
	{
		switch (0)
		{
		default:
		{
			int num = 4;
			for (;;)
			{
				int num2;
				string item;
				switch (num)
				{
				case 0:
					goto IL_B3;
				case 1:
					if (A_0.AssemblyList != null)
					{
						num = 13;
						continue;
					}
					return false;
				case 2:
					num = 1;
					continue;
				case 3:
					return false;
				case 5:
				{
					if (A_0.AssemblyList.Length == 0)
					{
						num = 7;
						continue;
					}
					string[] assemblyList = A_0.AssemblyList;
					num2 = 0;
					num = 10;
					continue;
				}
				case 6:
					num = 14;
					continue;
				case 7:
					goto IL_16E;
				case 8:
				{
					bool result;
					return result;
				}
				case 9:
				{
					string[] assemblyList;
					if (num2 >= assemblyList.Length)
					{
						num = 3;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_B3;
					default:
						if (false)
						{
						}
						if (true)
						{
						}
						item = assemblyList[num2];
						num = 0;
						continue;
					}
					break;
				}
				case 10:
					goto IL_F0;
				case 11:
				{
					bool result = true;
					num = 8;
					continue;
				}
				case 12:
					goto IL_F0;
				case 13:
					num = 5;
					continue;
				case 14:
					if ((A_0.LicenseType & LicenseType.Runtime) == LicenseType.Runtime)
					{
						num = 2;
						continue;
					}
					return false;
				}
				if (A_0 != null)
				{
					num = 6;
					continue;
				}
				return false;
				IL_B3:
				if (spr\u2347.ᜀ.Contains(item))
				{
					num = 11;
					continue;
				}
				num2++;
				num = 12;
				continue;
				IL_F0:
				num = 9;
			}
			return false;
			IL_16E:
			return false;
		}
		}
	}

	// Token: 0x0600376B RID: 14187 RVA: 0x0033C7F0 File Offset: 0x0033B7F0
	internal static LicenseType ᜀ(License A_0)
	{
		for (;;)
		{
			IL_00:
			int num = 7;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return LicenseType.Demo;
				case 1:
					goto IL_DF;
				case 2:
					num = 5;
					continue;
				case 3:
					if (((LicenseInfo)A_0).IsUpdateRightExpired)
					{
						num = 1;
						continue;
					}
					num = 4;
					continue;
				case 4:
					if (((LicenseInfo)A_0).Type != LicenseType.Runtime)
					{
						num = 0;
						continue;
					}
					return LicenseType.Runtime;
				case 5:
					if (A_0.GetType() != typeof(LicenseInfo))
					{
						return LicenseType.None;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_00;
					default:
						if (false)
						{
						}
						num = 6;
						continue;
					}
					break;
				case 6:
					if (true)
					{
					}
					num = 3;
					continue;
				}
				if (A_0 == null)
				{
					return LicenseType.None;
				}
				num = 2;
			}
		}
		return LicenseType.Demo;
		IL_DF:
		return LicenseType.None;
	}

	// Token: 0x040029F4 RID: 10740
	[ThreadStatic]
	private static List<string> ᜀ;
}
