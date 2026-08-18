using System;

namespace OracleInternal.SelfTuning
{
	// Token: 0x020000D7 RID: 215
	internal interface IOracleTunable
	{
		// Token: 0x17000212 RID: 530
		// (get) Token: 0x060008A3 RID: 2211
		string ID { get; }

		// Token: 0x060008A4 RID: 2212
		void OnUpdateRecommendations(RecommendationType recommendationType, int value);

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x060008A5 RID: 2213
		int MaxAllowedValue { get; }
	}
}
