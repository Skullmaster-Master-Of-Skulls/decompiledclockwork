using System;

namespace Telerik.Web.Analytics
{
	// Token: 0x02000479 RID: 1145
	internal class FeatureResolver : IFeatureResolver
	{
		// Token: 0x060028E7 RID: 10471 RVA: 0x000844BF File Offset: 0x000826BF
		public string ResolveToString(IFeatureSignature signature)
		{
			return string.Format("{0}/{1}.{2}", signature.FeatureGroup, signature.FeatureName, signature.FeatureValue);
		}
	}
}
