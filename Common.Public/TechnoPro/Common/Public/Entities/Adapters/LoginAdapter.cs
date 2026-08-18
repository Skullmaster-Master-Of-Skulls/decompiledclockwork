using System;
using TechnoPro.Common.Public.Entities.Login;

namespace TechnoPro.Common.Public.Entities.Adapters
{
	// Token: 0x020005DA RID: 1498
	public static class LoginAdapter
	{
		// Token: 0x06003040 RID: 12352 RVA: 0x0003E17C File Offset: 0x0003C37C
		public static eLoginMethod ELoginMethodFromString(this string val)
		{
			eLoginMethod result;
			if (!(val == "windowslogin"))
			{
				if (!(val == "ldap"))
				{
					if (!(val == "domain"))
					{
						if (!(val == "shiboleth"))
						{
							result = eLoginMethod.ClockWorkLogin;
						}
						else
						{
							result = eLoginMethod.Shiboleth;
						}
					}
					else
					{
						result = eLoginMethod.ActiveDirectory;
					}
				}
				else
				{
					result = eLoginMethod.Ldap;
				}
			}
			else
			{
				result = eLoginMethod.WindowsLogin;
			}
			return result;
		}

		// Token: 0x06003041 RID: 12353 RVA: 0x0003E1DC File Offset: 0x0003C3DC
		public static string ELoginMethodToString(this eLoginMethod loginMethod)
		{
			switch (loginMethod)
			{
			case eLoginMethod.WindowsLogin:
				return "windowslogin";
			case eLoginMethod.Ldap:
				return "ldap";
			case (eLoginMethod)3:
				break;
			case eLoginMethod.ActiveDirectory:
				return "domain";
			default:
				if (loginMethod == eLoginMethod.Shiboleth)
				{
					return "shiboleth";
				}
				break;
			}
			return "";
		}
	}
}
