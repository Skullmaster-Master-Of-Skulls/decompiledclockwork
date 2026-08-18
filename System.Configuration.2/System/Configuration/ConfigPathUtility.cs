using System;

namespace System.Configuration
{
	// Token: 0x0200000E RID: 14
	internal static class ConfigPathUtility
	{
		// Token: 0x06000020 RID: 32 RVA: 0x00002480 File Offset: 0x00000680
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

		// Token: 0x06000021 RID: 33 RVA: 0x0000250E File Offset: 0x0000070E
		internal static string Combine(string parentConfigPath, string childConfigPath)
		{
			if (string.IsNullOrEmpty(parentConfigPath))
			{
				return childConfigPath;
			}
			if (string.IsNullOrEmpty(childConfigPath))
			{
				return parentConfigPath;
			}
			return parentConfigPath + "/" + childConfigPath;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002530 File Offset: 0x00000730
		internal static string[] GetParts(string configPath)
		{
			return configPath.Split(new char[]
			{
				'/'
			});
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002550 File Offset: 0x00000750
		internal static string GetName(string configPath)
		{
			if (string.IsNullOrEmpty(configPath))
			{
				return configPath;
			}
			int num = configPath.LastIndexOf('/');
			if (num == -1)
			{
				return configPath;
			}
			return configPath.Substring(num + 1);
		}

		// Token: 0x040000AF RID: 175
		private const char SeparatorChar = '/';
	}
}
