using System;

namespace Telerik.Web.Analytics
{
	// Token: 0x0200047C RID: 1148
	public interface IFeatureContract
	{
		// Token: 0x17000D4E RID: 3406
		// (get) Token: 0x060028FB RID: 10491
		IFeatureSignature Signature { get; }

		// Token: 0x060028FC RID: 10492
		IFeatureContract OfGroup(string group);

		// Token: 0x060028FD RID: 10493
		IFeatureContract OfInstance(IFeatureGroup control);

		// Token: 0x060028FE RID: 10494
		IFeatureContract OfControlType(Type type);

		// Token: 0x060028FF RID: 10495
		IFeatureContract OfType(FeatureType type);

		// Token: 0x06002900 RID: 10496
		IFeatureContract OfClass(FeatureClass featureClass);

		// Token: 0x06002901 RID: 10497
		IFeatureContract OfPriority(FeaturePriority level);

		// Token: 0x06002902 RID: 10498
		IFeatureContract OfValue(Func<string> result);

		// Token: 0x06002903 RID: 10499
		IFeatureContract OfName(Func<string> result);
	}
}
