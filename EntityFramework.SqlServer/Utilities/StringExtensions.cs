using System;
using System.Data.Entity.SqlServer.Resources;
using System.Globalization;
using System.Text.RegularExpressions;

namespace System.Data.Entity.SqlServer.Utilities
{
	// Token: 0x0200000C RID: 12
	internal static class StringExtensions
	{
		// Token: 0x0600007C RID: 124 RVA: 0x00003E98 File Offset: 0x00002098
		public static bool EqualsIgnoreCase(this string s1, string s2)
		{
			return string.Equals(s1, s2, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003EA2 File Offset: 0x000020A2
		internal static bool EqualsOrdinal(this string s1, string s2)
		{
			return string.Equals(s1, s2, StringComparison.Ordinal);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003EAC File Offset: 0x000020AC
		public static string MigrationName(this string migrationId)
		{
			return migrationId.Substring(16);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003EB6 File Offset: 0x000020B6
		public static string RestrictTo(this string s, int size)
		{
			if (string.IsNullOrEmpty(s) || s.Length <= size)
			{
				return s;
			}
			return s.Substring(0, size);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003ED3 File Offset: 0x000020D3
		public static void EachLine(this string s, Action<string> action)
		{
			s.Split(StringExtensions._lineEndings, StringSplitOptions.None).Each(action);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003EE7 File Offset: 0x000020E7
		public static bool IsValidMigrationId(this string migrationId)
		{
			return StringExtensions._migrationIdPattern.IsMatch(migrationId) || migrationId == "0";
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003F03 File Offset: 0x00002103
		public static bool IsAutomaticMigration(this string migrationId)
		{
			return migrationId.EndsWith(Strings.AutomaticMigration, StringComparison.Ordinal);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003F14 File Offset: 0x00002114
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

		// Token: 0x06000084 RID: 132 RVA: 0x00003F69 File Offset: 0x00002169
		public static bool IsValidUndottedName(this string name)
		{
			return !string.IsNullOrEmpty(name) && StringExtensions._undottedNameValidator.IsMatch(name);
		}

		// Token: 0x04000010 RID: 16
		private const string StartCharacterExp = "[\\p{L}\\p{Nl}_]";

		// Token: 0x04000011 RID: 17
		private const string OtherCharacterExp = "[\\p{L}\\p{Nl}\\p{Nd}\\p{Mn}\\p{Mc}\\p{Pc}\\p{Cf}]";

		// Token: 0x04000012 RID: 18
		private const string NameExp = "[\\p{L}\\p{Nl}_][\\p{L}\\p{Nl}\\p{Nd}\\p{Mn}\\p{Mc}\\p{Pc}\\p{Cf}]{0,}";

		// Token: 0x04000013 RID: 19
		private static readonly Regex _undottedNameValidator = new Regex("^[\\p{L}\\p{Nl}_][\\p{L}\\p{Nl}\\p{Nd}\\p{Mn}\\p{Mc}\\p{Pc}\\p{Cf}]{0,}$", RegexOptions.Compiled | RegexOptions.Singleline);

		// Token: 0x04000014 RID: 20
		private static readonly Regex _migrationIdPattern = new Regex("\\d{15}_.+");

		// Token: 0x04000015 RID: 21
		private static readonly string[] _lineEndings = new string[]
		{
			"\r\n",
			"\n"
		};
	}
}
