using System;
using System.Text.RegularExpressions;

namespace AutoMapper
{
	// Token: 0x02000034 RID: 52
	public class PascalCaseNamingConvention : INamingConvention
	{
		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x00004C5B File Offset: 0x00002E5B
		public Regex SplittingExpression { get; } = new Regex("(\\p{Lu}+(?=$|\\p{Lu}[\\p{Ll}0-9])|\\p{Lu}?[\\p{Ll}0-9]+)");

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x00004C63 File Offset: 0x00002E63
		public string SeparatorCharacter
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00004C6C File Offset: 0x00002E6C
		public string ReplaceValue(Match match)
		{
			return match.Value[0].ToString().ToUpper() + match.Value.Substring(1);
		}
	}
}
