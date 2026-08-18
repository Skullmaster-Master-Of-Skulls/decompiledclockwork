using System;

namespace Spire.Doc
{
	// Token: 0x020000DB RID: 219
	public class LicenseProvider
	{
		// Token: 0x06000293 RID: 659 RVA: 0x0001B7D0 File Offset: 0x0001A7D0
		public static void Register(string userName, string code)
		{
			try
			{
				int num = 3;
				for (;;)
				{
					string ᜀ;
					string ᜁ;
					switch (num)
					{
					case 0:
						if (code != null)
						{
							num = 5;
							continue;
						}
						num = 6;
						continue;
					case 1:
						ᜀ = "";
						goto IL_99;
					case 2:
						num = 7;
						continue;
					case 4:
						goto IL_C9;
					case 5:
						num = 8;
						continue;
					case 6:
						goto IL_5C;
					case 7:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_5C;
						default:
							if (false)
							{
							}
							ᜀ = userName;
							goto IL_99;
						}
						break;
					case 8:
						if (true)
						{
						}
						ᜁ = code;
						goto IL_B9;
					}
					if (userName != null)
					{
						num = 2;
						continue;
					}
					num = 1;
					continue;
					IL_99:
					spr\u2543.ᜀ = ᜀ;
					num = 0;
					continue;
					IL_B9:
					spr\u2543.ᜁ = ᜁ;
					num = 4;
					continue;
					IL_5C:
					ᜁ = "";
					goto IL_B9;
				}
				IL_C9:;
			}
			catch
			{
			}
		}
	}
}
