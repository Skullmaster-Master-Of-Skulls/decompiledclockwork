using System;
using System.Data.ProviderBase;

namespace System.Data.OleDb
{
	// Token: 0x02000217 RID: 535
	internal sealed class OleDbConnectionPoolGroupProviderInfo : DbConnectionPoolGroupProviderInfo
	{
		// Token: 0x06001EA2 RID: 7842 RVA: 0x00275218 File Offset: 0x00274618
		internal OleDbConnectionPoolGroupProviderInfo()
		{
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x06001EA3 RID: 7843 RVA: 0x00275238 File Offset: 0x00274638
		internal bool HasQuoteFix
		{
			get
			{
				return this._hasQuoteFix;
			}
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06001EA4 RID: 7844 RVA: 0x00275258 File Offset: 0x00274658
		internal string QuotePrefix
		{
			get
			{
				return this._quotePrefix;
			}
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x06001EA5 RID: 7845 RVA: 0x00275278 File Offset: 0x00274678
		internal string QuoteSuffix
		{
			get
			{
				return this._quoteSuffix;
			}
		}

		// Token: 0x06001EA6 RID: 7846 RVA: 0x00275298 File Offset: 0x00274698
		internal void SetQuoteFix(string prefix, string suffix)
		{
			this._quotePrefix = prefix;
			this._quoteSuffix = suffix;
			this._hasQuoteFix = true;
		}

		// Token: 0x0400127F RID: 4735
		private bool _hasQuoteFix;

		// Token: 0x04001280 RID: 4736
		private string _quotePrefix;

		// Token: 0x04001281 RID: 4737
		private string _quoteSuffix;
	}
}
