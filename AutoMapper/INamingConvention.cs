using System;
using System.Text.RegularExpressions;

namespace AutoMapper
{
	// Token: 0x02000020 RID: 32
	public interface INamingConvention
	{
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000E8 RID: 232
		Regex SplittingExpression { get; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000E9 RID: 233
		string SeparatorCharacter { get; }

		// Token: 0x060000EA RID: 234
		string ReplaceValue(Match match);
	}
}
