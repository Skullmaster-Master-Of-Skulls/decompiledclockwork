using System;
using System.Text.RegularExpressions;

namespace System.Web.Util
{
	// Token: 0x020001C9 RID: 457
	internal class RegexUtil
	{
		// Token: 0x06001755 RID: 5973 RVA: 0x00049490 File Offset: 0x00047690
		public static bool IsMatch(string stringToMatch, string pattern, RegexOptions regOption, int? timeoutInMillsec)
		{
			int regexTimeout = RegexUtil.GetRegexTimeout(timeoutInMillsec);
			if (regexTimeout > 0 || timeoutInMillsec != null)
			{
				return Regex.IsMatch(stringToMatch, pattern, regOption, TimeSpan.FromMilliseconds((double)regexTimeout));
			}
			return Regex.IsMatch(stringToMatch, pattern, regOption);
		}

		// Token: 0x06001756 RID: 5974 RVA: 0x000494CC File Offset: 0x000476CC
		public static Match Match(string stringToMatch, string pattern, RegexOptions regOption, int? timeoutInMillsec)
		{
			int regexTimeout = RegexUtil.GetRegexTimeout(timeoutInMillsec);
			if (regexTimeout > 0 || timeoutInMillsec != null)
			{
				return Regex.Match(stringToMatch, pattern, regOption, TimeSpan.FromMilliseconds((double)regexTimeout));
			}
			return Regex.Match(stringToMatch, pattern, regOption);
		}

		// Token: 0x06001757 RID: 5975 RVA: 0x00049508 File Offset: 0x00047708
		public static Regex CreateRegex(string pattern, RegexOptions option, int? timeoutInMillsec)
		{
			int regexTimeout = RegexUtil.GetRegexTimeout(timeoutInMillsec);
			if (regexTimeout > 0 || timeoutInMillsec != null)
			{
				return new Regex(pattern, option, TimeSpan.FromMilliseconds((double)regexTimeout));
			}
			return new Regex(pattern, option);
		}

		// Token: 0x06001758 RID: 5976 RVA: 0x00049540 File Offset: 0x00047740
		internal static Regex CreateRegex(string pattern, RegexOptions option)
		{
			return RegexUtil.CreateRegex(pattern, option, null);
		}

		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x06001759 RID: 5977 RVA: 0x00049560 File Offset: 0x00047760
		private static bool IsRegexTimeoutSetInAppDomain
		{
			get
			{
				if (RegexUtil._isRegexTimeoutSetInAppDomain == null)
				{
					bool value = false;
					try
					{
						value = (AppDomain.CurrentDomain.GetData("REGEX_DEFAULT_MATCH_TIMEOUT") != null);
					}
					catch
					{
					}
					RegexUtil._isRegexTimeoutSetInAppDomain = new bool?(value);
				}
				return RegexUtil._isRegexTimeoutSetInAppDomain.Value;
			}
		}

		// Token: 0x0600175A RID: 5978 RVA: 0x000495B8 File Offset: 0x000477B8
		private static int GetRegexTimeout(int? timeoutInMillsec)
		{
			int result = -1;
			if (timeoutInMillsec != null)
			{
				result = timeoutInMillsec.Value;
			}
			else if (!RegexUtil.IsRegexTimeoutSetInAppDomain && BinaryCompatibility.Current.TargetsAtLeastFramework461)
			{
				result = 2000;
			}
			return result;
		}

		// Token: 0x04001702 RID: 5890
		private static bool? _isRegexTimeoutSetInAppDomain;
	}
}
