using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x020009B5 RID: 2485
	internal interface IMergeEnabledMessageProperty
	{
		// Token: 0x0600617E RID: 24958
		bool TryMergeWithProperty(object propertyToMerge);
	}
}
