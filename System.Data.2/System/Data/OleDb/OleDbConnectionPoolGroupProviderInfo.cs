using System;
using System.Data.ProviderBase;

namespace System.Data.OleDb
{
	// Token: 0x02000246 RID: 582
	internal sealed class OleDbConnectionPoolGroupProviderInfo : DbConnectionPoolGroupProviderInfo
	{
		// Token: 0x060024CD RID: 9421 RVA: 0x000FB18C File Offset: 0x000FA58C
		internal OleDbConnectionPoolGroupProviderInfo()
		{
		}

		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x060024CE RID: 9422 RVA: 0x000FB1A0 File Offset: 0x000FA5A0
		internal bool HasQuoteFix
		{
			get
			{
				return this._hasQuoteFix;
			}
		}

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x060024CF RID: 9423 RVA: 0x000FB1B4 File Offset: 0x000FA5B4
		internal string QuotePrefix
		{
			get
			{
				return this._quotePrefix;
			}
		}

		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x060024D0 RID: 9424 RVA: 0x000FB1C8 File Offset: 0x000FA5C8
		internal string QuoteSuffix
		{
			get
			{
				return this._quoteSuffix;
			}
		}

		// Token: 0x060024D1 RID: 9425 RVA: 0x000FB1DC File Offset: 0x000FA5DC
		internal void SetQuoteFix(string prefix, string suffix)
		{
			this._quotePrefix = prefix;
			this._quoteSuffix = suffix;
			this._hasQuoteFix = true;
		}

		// Token: 0x040015AD RID: 5549
		private bool _hasQuoteFix;

		// Token: 0x040015AE RID: 5550
		private string _quotePrefix;

		// Token: 0x040015AF RID: 5551
		private string _quoteSuffix;
	}
}
