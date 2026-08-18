using System;
using System.Text.RegularExpressions;

namespace AutoMapper
{
	// Token: 0x0200002D RID: 45
	public class LowerUnderscoreNamingConvention : INamingConvention
	{
		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000126 RID: 294 RVA: 0x00002FE4 File Offset: 0x000011E4
		public Regex SplittingExpression { get; } = new Regex("[\\p{Ll}\\p{Lu}0-9]+(?=_?)");

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000127 RID: 295 RVA: 0x00002FEC File Offset: 0x000011EC
		public string SeparatorCharacter
		{
			get
			{
				return "_";
			}
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00002FF3 File Offset: 0x000011F3
		public string ReplaceValue(Match match)
		{
			return match.Value.ToLower();
		}
	}
}
