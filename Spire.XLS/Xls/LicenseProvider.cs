using System;

namespace Spire.Xls
{
	// Token: 0x02000049 RID: 73
	public class LicenseProvider
	{
		// Token: 0x06000501 RID: 1281 RVA: 0x00029740 File Offset: 0x00028740
		public static void Register(string userName, string code)
		{
			try
			{
				int num = 4;
				for (;;)
				{
					string ᜁ;
					string ᜂ;
					switch (num)
					{
					case 0:
						ᜁ = userName;
						goto IL_9C;
					case 1:
						if (code != null)
						{
							num = 8;
							continue;
						}
						num = 6;
						continue;
					case 2:
						goto IL_8E;
					case 3:
						ᜁ = "";
						goto IL_9C;
					case 5:
						goto IL_CC;
					case 6:
						ᜂ = "";
						goto IL_BC;
					case 7:
						ᜂ = code;
						goto IL_BC;
					case 8:
						num = 7;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_8E:
						num = 0;
						continue;
					default:
						if (false)
						{
						}
						if (userName != null)
						{
							if (true)
							{
							}
							num = 2;
							continue;
						}
						num = 3;
						continue;
					}
					IL_9C:
					spr\u2067.ᜁ = ᜁ;
					num = 1;
					continue;
					IL_BC:
					spr\u2067.ᜂ = ᜂ;
					num = 5;
				}
				IL_CC:;
			}
			catch
			{
			}
		}
	}
}
