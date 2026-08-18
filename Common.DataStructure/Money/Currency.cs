using System;

namespace TechnoPro.Common.DataStructure.Money
{
	// Token: 0x02000017 RID: 23
	internal class Currency
	{
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000090 RID: 144 RVA: 0x000033AB File Offset: 0x000015AB
		// (set) Token: 0x06000091 RID: 145 RVA: 0x000033B3 File Offset: 0x000015B3
		public CurrencyCodeKind CurrencyCode { get; private set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000092 RID: 146 RVA: 0x000033BC File Offset: 0x000015BC
		// (set) Token: 0x06000093 RID: 147 RVA: 0x000033C4 File Offset: 0x000015C4
		public string EnglishName { get; private set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000094 RID: 148 RVA: 0x000033CD File Offset: 0x000015CD
		// (set) Token: 0x06000095 RID: 149 RVA: 0x000033D5 File Offset: 0x000015D5
		public string Symbol { get; private set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000096 RID: 150 RVA: 0x000033DE File Offset: 0x000015DE
		// (set) Token: 0x06000097 RID: 151 RVA: 0x000033E6 File Offset: 0x000015E6
		public int SignificantDecimalDigits { get; private set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000098 RID: 152 RVA: 0x000033EF File Offset: 0x000015EF
		// (set) Token: 0x06000099 RID: 153 RVA: 0x000033F7 File Offset: 0x000015F7
		public CurrencyRoundingKind RoundingType { get; private set; }

		// Token: 0x0600009A RID: 154 RVA: 0x00003400 File Offset: 0x00001600
		public Currency(CurrencyCodeKind currencyCode, string englishName, string sign, int significantDecimalDigits)
		{
			this.CurrencyCode = currencyCode;
			this.EnglishName = englishName;
			this.Symbol = sign;
			this.SignificantDecimalDigits = significantDecimalDigits;
			this.RoundingType = CurrencyRoundingKind.AwayFromZero;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x0000342C File Offset: 0x0000162C
		public Currency(CurrencyCodeKind currencyCode, string englishName, string sign, int significantDecimalDigits, CurrencyRoundingKind roundingType)
		{
			this.CurrencyCode = currencyCode;
			this.EnglishName = englishName;
			this.Symbol = sign;
			this.SignificantDecimalDigits = significantDecimalDigits;
			this.RoundingType = roundingType;
		}
	}
}
