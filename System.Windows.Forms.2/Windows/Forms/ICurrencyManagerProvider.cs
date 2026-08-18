using System;

namespace System.Windows.Forms
{
	// Token: 0x0200028C RID: 652
	[SRDescription("ICurrencyManagerProviderDescr")]
	public interface ICurrencyManagerProvider
	{
		// Token: 0x170009C1 RID: 2497
		// (get) Token: 0x06002999 RID: 10649
		CurrencyManager CurrencyManager { get; }

		// Token: 0x0600299A RID: 10650
		CurrencyManager GetRelatedCurrencyManager(string dataMember);
	}
}
