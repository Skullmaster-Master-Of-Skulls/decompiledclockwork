using System;

namespace System.Windows.Forms
{
	// Token: 0x0200028F RID: 655
	public interface IFeatureSupport
	{
		// Token: 0x060029A9 RID: 10665
		bool IsPresent(object feature);

		// Token: 0x060029AA RID: 10666
		bool IsPresent(object feature, Version minimumVersion);

		// Token: 0x060029AB RID: 10667
		Version GetVersionPresent(object feature);
	}
}
