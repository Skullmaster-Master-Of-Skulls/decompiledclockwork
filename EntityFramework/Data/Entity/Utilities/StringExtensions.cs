using System;
using System.Data.Entity.Resources;
using System.Globalization;
using System.Text.RegularExpressions;

namespace System.Data.Entity.Utilities
{
	// Token: 0x02000008 RID: 8
	internal static class StringExtensions
	{
		// Token: 0x06000060 RID: 96 RVA: 0x00003760 File Offset: 0x00001960
		public static bool EqualsIgnoreCase(this string s1, string s2)
		{
			return string.Equals(s1, s2, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x0000376A File Offset: 0x0000196A
		internal static bool EqualsOrdinal(this string s1, string s2)
		{
			return string.Equals(s1, s2, StringComparison.Ordinal);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003774 File Offset: 0x00001974
		public static string MigrationName(this string migrationId)
		{
			return migrationId.Substring(16);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x0000377E File Offset: 0x0000197E
		public static string RestrictTo(this string s, int size)
		{
			if (string.IsNullOrEmpty(s) || s.Length <= size)
			{
				return s;
			}
			return s.Substring(0, size);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x0000379B File Offset: 0x0000199B
		public static void EachLine(this string s, Action<string> action)
		{
			s.Split(StringExtensions._lineEndings, StringSplitOptions.None).Each(action);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000037AF File Offset: 0x000019AF
		public static bool IsValidMigrationId(this string migrationId)
		{
			return StringExtensions._migrationIdPattern.IsMatch(migrationId) || migrationId == "0";
		}

		// Token: 0x06000066 RID: 102 RVA: 0x000037CB File Offset: 0x000019CB
		public static bool IsAutomaticMigration(this string migrationId)
		{
			return migrationId.EndsWith(Strings.AutomaticMigration, StringComparison.Ordinal);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000037DC File Offset: 0x000019DC
		public static string ToAutomaticMigrationId(this string migrationId)
		{
			long num = Convert.ToInt64(migrationId.Substring(0, 15), CultureInfo.InvariantCulture) - 1L;
			return string.Concat(new object[]
			{
				num,
				migrationId.Substring(15),
				"_",
				Strings.AutomaticMigration
			});
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003831 File Offset: 0x00001A31
		public static bool IsValidUndottedName(this string name)
		{
			return !string.IsNullOrEmpty(name) && StringExtensions._undottedNameValidator.IsMatch(name);
		}

		// Token: 0x0400000D RID: 13
		private const string StartCharacterExp = "[\\p{L}\\p{Nl}_]";

		// Token: 0x0400000E RID: 14
		private const string OtherCharacterExp = "[\\p{L}\\p{Nl}\\p{Nd}\\p{Mn}\\p{Mc}\\p{Pc}\\p{Cf}]";

		// Token: 0x0400000F RID: 15
		private const string NameExp = "[\\p{L}\\p{Nl}_][\\p{L}\\p{Nl}\\p{Nd}\\p{Mn}\\p{Mc}\\p{Pc}\\p{Cf}]{0,}";

		// Token: 0x04000010 RID: 16
		private static readonly Regex _undottedNameValidator = new Regex("^[\\p{L}\\p{Nl}_][\\p{L}\\p{Nl}\\p{Nd}\\p{Mn}\\p{Mc}\\p{Pc}\\p{Cf}]{0,}$", RegexOptions.Compiled | RegexOptions.Singleline);

		// Token: 0x04000011 RID: 17
		private static readonly Regex _migrationIdPattern = new Regex("\\d{15}_.+");

		// Token: 0x04000012 RID: 18
		private static readonly string[] _lineEndings = new string[]
		{
			"\r\n",
			"\n"
		};
	}
}
