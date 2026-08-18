using System;

namespace System
{
	// Token: 0x0200005C RID: 92
	internal static class AppContextDefaultValues
	{
		// Token: 0x0600034F RID: 847 RVA: 0x0000D26C File Offset: 0x0000B46C
		public static void PopulateDefaultValues()
		{
			string platformIdentifier;
			string profile;
			int version;
			AppContextDefaultValues.ParseTargetFrameworkName(out platformIdentifier, out profile, out version);
			AppContextDefaultValues.PopulateDefaultValuesPartial(platformIdentifier, profile, version);
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0000D28C File Offset: 0x0000B48C
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

		// Token: 0x06000351 RID: 849 RVA: 0x0000D2CC File Offset: 0x0000B4CC
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

		// Token: 0x06000352 RID: 850 RVA: 0x0000D428 File Offset: 0x0000B628
		private static void PopulateDefaultValuesPartial(string platformIdentifier, string profile, int version)
		{
			if (!(platformIdentifier == ".NETCore") && !(platformIdentifier == ".NETFramework"))
			{
				if (!(platformIdentifier == "WindowsPhone") && !(platformIdentifier == "WindowsPhoneApp"))
				{
					return;
				}
				if (version <= 80100)
				{
					LocalAppContext.DefineSwitchDefault("Switch.System.Xml.DontThrowOnInvalidSurrogatePairs", true);
					LocalAppContext.DefineSwitchDefault("Switch.System.Xml.IgnoreEmptyKeySequences", true);
					LocalAppContext.DefineSwitchDefault("Switch.System.Xml.IgnoreKindInUtcTimeSerialization", true);
				}
			}
			else
			{
				if (version <= 40502)
				{
					LocalAppContext.DefineSwitchDefault("Switch.System.Xml.DontThrowOnInvalidSurrogatePairs", true);
					LocalAppContext.DefineSwitchDefault("Switch.System.Xml.IgnoreEmptyKeySequences", true);
				}
				if (version <= 40601)
				{
					LocalAppContext.DefineSwitchDefault("Switch.System.Xml.IgnoreKindInUtcTimeSerialization", true);
					return;
				}
			}
		}
	}
}
