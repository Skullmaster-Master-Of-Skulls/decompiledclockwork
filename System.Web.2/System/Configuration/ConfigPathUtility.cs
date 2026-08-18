using System;

namespace System.Configuration
{
	// Token: 0x020008A7 RID: 2215
	internal static class ConfigPathUtility
	{
		// Token: 0x0600678D RID: 26509 RVA: 0x00170308 File Offset: 0x0016E508
		internal static bool IsValid(string configPath)
		{
			if (string.IsNullOrEmpty(configPath))
			{
				return false;
			}
			int num = -1;
			for (int i = 0; i <= configPath.Length; i++)
			{
				char c;
				if (i < configPath.Length)
				{
					c = configPath[i];
				}
				else
				{
					c = '/';
				}
				if (c == '\\')
				{
					return false;
				}
				if (c == '/')
				{
					if (i == num + 1)
					{
						return false;
					}
					if (i == num + 2 && configPath[num + 1] == '.')
					{
						return false;
					}
					if (i == num + 3 && configPath[num + 1] == '.' && configPath[num + 2] == '.')
					{
						return false;
					}
					num = i;
				}
			}
			return true;
		}

		// Token: 0x0600678E RID: 26510 RVA: 0x00170398 File Offset: 0x0016E598
		internal static string GetParent(string configPath)
		{
			if (string.IsNullOrEmpty(configPath))
			{
				return null;
			}
			int num = configPath.LastIndexOf('/');
			string result;
			if (num == -1)
			{
				result = null;
			}
			else
			{
				result = configPath.Substring(0, num);
			}
			return result;
		}

		// Token: 0x040035B3 RID: 13747
		private const char SeparatorChar = '/';
	}
}
