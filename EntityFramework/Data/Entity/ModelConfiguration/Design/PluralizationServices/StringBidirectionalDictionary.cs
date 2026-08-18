using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Pluralization;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.ModelConfiguration.Design.PluralizationServices
{
	// Token: 0x02000808 RID: 2056
	internal class StringBidirectionalDictionary : BidirectionalDictionary<string, string>
	{
		// Token: 0x06005C9D RID: 23709 RVA: 0x0019001A File Offset: 0x0018E21A
		internal StringBidirectionalDictionary()
		{
		}

		// Token: 0x06005C9E RID: 23710 RVA: 0x00190022 File Offset: 0x0018E222
		internal StringBidirectionalDictionary(Dictionary<string, string> firstToSecondDictionary) : base(firstToSecondDictionary)
		{
		}

		// Token: 0x06005C9F RID: 23711 RVA: 0x0019002B File Offset: 0x0018E22B
		[SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase")]
		internal override bool ExistsInFirst(string value)
		{
			return base.ExistsInFirst(value.ToLowerInvariant());
		}

		// Token: 0x06005CA0 RID: 23712 RVA: 0x00190039 File Offset: 0x0018E239
		[SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase")]
		internal override bool ExistsInSecond(string value)
		{
			return base.ExistsInSecond(value.ToLowerInvariant());
		}

		// Token: 0x06005CA1 RID: 23713 RVA: 0x00190047 File Offset: 0x0018E247
		[SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase")]
		internal override string GetFirstValue(string value)
		{
			return base.GetFirstValue(value.ToLowerInvariant());
		}

		// Token: 0x06005CA2 RID: 23714 RVA: 0x00190055 File Offset: 0x0018E255
		[SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase")]
		internal override string GetSecondValue(string value)
		{
			return base.GetSecondValue(value.ToLowerInvariant());
		}
	}
}
