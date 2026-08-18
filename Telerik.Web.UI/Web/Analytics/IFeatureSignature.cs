using System;

namespace Telerik.Web.Analytics
{
	// Token: 0x0200047A RID: 1146
	public interface IFeatureSignature
	{
		// Token: 0x17000D47 RID: 3399
		// (get) Token: 0x060028E9 RID: 10473
		// (set) Token: 0x060028EA RID: 10474
		string FeatureName { get; set; }

		// Token: 0x17000D48 RID: 3400
		// (get) Token: 0x060028EB RID: 10475
		// (set) Token: 0x060028EC RID: 10476
		string FeatureGroup { get; set; }

		// Token: 0x17000D49 RID: 3401
		// (get) Token: 0x060028ED RID: 10477
		// (set) Token: 0x060028EE RID: 10478
		string FeatureValue { get; set; }

		// Token: 0x17000D4A RID: 3402
		// (get) Token: 0x060028EF RID: 10479
		// (set) Token: 0x060028F0 RID: 10480
		FeatureClass FeatureClass { get; set; }

		// Token: 0x17000D4B RID: 3403
		// (get) Token: 0x060028F1 RID: 10481
		// (set) Token: 0x060028F2 RID: 10482
		FeaturePriority FeaturePriority { get; set; }

		// Token: 0x17000D4C RID: 3404
		// (get) Token: 0x060028F3 RID: 10483
		// (set) Token: 0x060028F4 RID: 10484
		FeatureType FeatureType { get; set; }

		// Token: 0x17000D4D RID: 3405
		// (get) Token: 0x060028F5 RID: 10485
		// (set) Token: 0x060028F6 RID: 10486
		Type ControlType { get; set; }
	}
}
