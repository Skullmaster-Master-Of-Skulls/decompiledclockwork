using System;
using System.Collections.Generic;
using System.ComponentModel;
using Spire.License;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x02000293 RID: 659
internal abstract class spr\u2067
{
	// Token: 0x060026F7 RID: 9975 RVA: 0x00161EAC File Offset: 0x00160EAC
	static spr\u2067()
	{
		int a_ = 2;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		spr\u2067.ᜀ = true;
		spr\u2067.ᜁ = string.Empty;
		spr\u2067.ᜂ = string.Empty;
		spr\u2067.ᜃ = null;
		spr\u2067.ᜃ = new List<string>
		{
			RecordTableEnumerator.b("欷䨹唻䰽┿汁ᝃ㙅㩇⽉ⵋ⩍⍏㩑ㅓ㍕ⱗ", a_)
		};
	}

	// Token: 0x060026F8 RID: 9976 RVA: 0x00161F30 File Offset: 0x00160F30
	internal static bool ᜀ(InternalLicense A_0)
	{
		switch (0)
		{
		default:
		{
			int num = 12;
			for (;;)
			{
				bool result;
				switch (num)
				{
				case 0:
					if (A_0.AssemblyList == null)
					{
						return false;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_148;
					default:
						if (false)
						{
						}
						num = 4;
						continue;
					}
					break;
				case 1:
					if ((A_0.LicenseType & LicenseType.Runtime) == LicenseType.Runtime)
					{
						num = 9;
						continue;
					}
					return false;
				case 2:
				{
					if (A_0.AssemblyList.Length == 0)
					{
						num = 6;
						continue;
					}
					string[] assemblyList = A_0.AssemblyList;
					int num2 = 0;
					num = 5;
					continue;
				}
				case 3:
				{
					string item;
					if (spr\u2067.ᜃ.Contains(item))
					{
						if (true)
						{
						}
						num = 13;
						continue;
					}
					int num2;
					num2++;
					num = 10;
					continue;
				}
				case 4:
					num = 2;
					continue;
				case 5:
					goto IL_F3;
				case 6:
					goto IL_17E;
				case 7:
				{
					string[] assemblyList;
					int num2;
					if (num2 >= assemblyList.Length)
					{
						num = 11;
						continue;
					}
					string item = assemblyList[num2];
					num = 3;
					continue;
				}
				case 8:
					num = 1;
					continue;
				case 9:
					num = 0;
					continue;
				case 10:
					goto IL_F3;
				case 11:
					return false;
				case 13:
					goto IL_148;
				case 14:
					return result;
				}
				if (A_0 != null)
				{
					num = 8;
					continue;
				}
				return false;
				IL_F3:
				num = 7;
				continue;
				IL_148:
				result = true;
				num = 14;
			}
			return false;
			IL_17E:
			return false;
		}
		}
	}

	// Token: 0x060026F9 RID: 9977 RVA: 0x001620D8 File Offset: 0x001610D8
	internal static LicenseType ᜀ(License A_0)
	{
		int num = 3;
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
					if (((LicenseInfo)A_0).Type != LicenseType.Runtime)
					{
						num = 5;
						continue;
					}
					return LicenseType.Runtime;
				}
				break;
			case 1:
				num = 7;
				continue;
			case 2:
				goto IL_DF;
			case 4:
				if (true)
				{
				}
				num = 6;
				continue;
			case 5:
				return LicenseType.Demo;
			case 6:
				if (((LicenseInfo)A_0).IsUpdateRightExpired)
				{
					num = 2;
					continue;
				}
				num = 0;
				continue;
			case 7:
				if (A_0.GetType() == typeof(LicenseInfo))
				{
					num = 4;
					continue;
				}
				return LicenseType.Demo;
			}
			if (A_0 == null)
			{
				return LicenseType.Demo;
			}
			num = 1;
		}
		return LicenseType.Demo;
		IL_DF:
		return LicenseType.Demo;
	}

	// Token: 0x0400132E RID: 4910
	internal static bool ᜀ;

	// Token: 0x0400132F RID: 4911
	internal static string ᜁ;

	// Token: 0x04001330 RID: 4912
	internal static string ᜂ;

	// Token: 0x04001331 RID: 4913
	private static List<string> ᜃ;
}
