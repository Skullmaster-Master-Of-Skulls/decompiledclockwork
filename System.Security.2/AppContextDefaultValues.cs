using System;

namespace System
{
	// Token: 0x02000006 RID: 6
	internal static class AppContextDefaultValues
	{
		// Token: 0x06000006 RID: 6 RVA: 0x000020C0 File Offset: 0x000002C0
		public static void PopulateDefaultValues()
		{
			string platformIdentifier;
			string profile;
			int version;
			AppContextDefaultValues.ParseTargetFrameworkName(out platformIdentifier, out profile, out version);
			AppContextDefaultValues.PopulateDefaultValuesPartial(platformIdentifier, profile, version);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020E0 File Offset: 0x000002E0
		private static void ParseTargetFrameworkName(out string identifier, out string profile, out int version)
		{
			string targetFrameworkName = AppDomain.CurrentDomain.SetupInformation.TargetFrameworkName;
			if (!AppContextDefaultValues.TryParseFrameworkName(targetFrameworkName, out identifier, out version, out profile))
			{
				identifier = ".NETFramework";
				version = 40000;
				profile = string.Empty;
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002120 File Offset: 0x00000320
		private static bool TryParseFrameworkName(string frameworkName, out string identifier, out int version, out string profile)
		{
			string empty;
			profile = (empty = string.Empty);
			identifier = empty;
			version = 0;
			if (frameworkName == null || frameworkName.Length == 0)
			{
				return false;
			}
			string[] array = frameworkName.Split(new char[]
			{
				','
			});
			version = 0;
			if (array.Length < 2 || array.Length > 3)
			{
				return false;
			}
			identifier = array[0].Trim();
			if (identifier.Length == 0)
			{
				return false;
			}
			bool result = false;
			profile = null;
			for (int i = 1; i < array.Length; i++)
			{
				string[] array2 = array[i].Split(new char[]
				{
					'='
				});
				if (array2.Length != 2)
				{
					return false;
				}
				string text = array2[0].Trim();
				string text2 = array2[1].Trim();
				if (text.Equals("Version", StringComparison.OrdinalIgnoreCase))
				{
					result = true;
					if (text2.Length > 0 && (text2[0] == 'v' || text2[0] == 'V'))
					{
						text2 = text2.Substring(1);
					}
					Version version2 = new Version(text2);
					version = version2.Major * 10000;
					if (version2.Minor > 0)
					{
						version += version2.Minor * 100;
					}
					if (version2.Build > 0)
					{
						version += version2.Build;
					}
				}
				else
				{
					if (!text.Equals("Profile", StringComparison.OrdinalIgnoreCase))
					{
						return false;
					}
					if (!string.IsNullOrEmpty(text2))
					{
						profile = text2;
					}
				}
			}
			return result;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000227C File Offset: 0x0000047C
		private static void PopulateDefaultValuesPartial(string platformIdentifier, string profile, int version)
		{
			if (platformIdentifier == ".NETCore" || platformIdentifier == ".NETFramework")
			{
				if (version <= 40700)
				{
					LocalAppContext.DefineSwitchDefault(LocalAppContextSwitches.SwitchXmlUseInsecureHashAlgorithms, true);
					LocalAppContext.DefineSwitchDefault(LocalAppContextSwitches.SwitchCmsUseInsecureHashAlgorithms, true);
				}
				if (version <= 40702)
				{
					LocalAppContext.DefineSwitchDefault(LocalAppContextSwitches.SwitchEnvelopedCmsUseLegacyDefaultAlgorithm, true);
				}
			}
		}
	}
}
