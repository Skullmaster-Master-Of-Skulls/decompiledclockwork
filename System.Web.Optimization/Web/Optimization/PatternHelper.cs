using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web.Optimization.Resources;

namespace System.Web.Optimization
{
	// Token: 0x02000029 RID: 41
	internal static class PatternHelper
	{
		// Token: 0x06000141 RID: 321 RVA: 0x000050F8 File Offset: 0x000032F8
		internal static PatternType GetPatternType(string input)
		{
			if (input.Contains("{version}"))
			{
				return PatternType.Version;
			}
			if (!input.Contains("*"))
			{
				return PatternType.Exact;
			}
			if (input.Length == 1)
			{
				return PatternType.All;
			}
			if (input.StartsWith("*", StringComparison.OrdinalIgnoreCase))
			{
				return PatternType.Suffix;
			}
			return PatternType.Prefix;
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00005134 File Offset: 0x00003334
		internal static Regex BuildRegex(string input)
		{
			input = input.Replace("{version}", "<version>");
			input = Regex.Escape(input);
			input = input.Replace("<version>", "(\\d+(\\s*\\.\\s*\\d+){1,3})(-[a-z][0-9a-z-]*)?");
			return new Regex("^" + input + "$", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled);
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00005184 File Offset: 0x00003384
		internal static Regex BuildWildcardRegex(string input)
		{
			input = input.Replace("*", "<star>");
			input = Regex.Escape(input);
			input = input.Replace("<star>", ".*");
			return new Regex("^" + input + "$", RegexOptions.IgnoreCase);
		}

		// Token: 0x06000144 RID: 324 RVA: 0x000051D4 File Offset: 0x000033D4
		internal static Exception ValidatePattern(PatternType type, string pattern, string argumentName)
		{
			switch (type)
			{
			case PatternType.Suffix:
			{
				string text = pattern.Substring(1);
				if (text.Contains("*"))
				{
					return new ArgumentException(string.Format(CultureInfo.CurrentCulture, OptimizationResources.InvalidPattern, new object[]
					{
						pattern
					}), argumentName);
				}
				break;
			}
			case PatternType.Prefix:
			{
				string text2 = pattern.Substring(0, pattern.Length - 1);
				if (text2.Contains("*"))
				{
					return new ArgumentException(string.Format(CultureInfo.CurrentCulture, OptimizationResources.InvalidPattern, new object[]
					{
						pattern
					}), argumentName);
				}
				break;
			}
			case PatternType.Version:
				if (pattern.Contains("*"))
				{
					return new ArgumentException(string.Format(CultureInfo.CurrentCulture, OptimizationResources.InvalidPattern, new object[]
					{
						pattern
					}), argumentName);
				}
				break;
			}
			return null;
		}

		// Token: 0x04000070 RID: 112
		internal const string VersionToken = "{version}";

		// Token: 0x04000071 RID: 113
		private const RegexOptions _flags = RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled;

		// Token: 0x04000072 RID: 114
		internal const string VersionRegEx = "(\\d+(\\s*\\.\\s*\\d+){1,3})(-[a-z][0-9a-z-]*)?";
	}
}
