using System;

namespace Telerik.Web.Analytics
{
	// Token: 0x0200047E RID: 1150
	public interface IFeatureConstraint
	{
		// Token: 0x17000D57 RID: 3415
		// (get) Token: 0x06002922 RID: 10530
		// (set) Token: 0x06002923 RID: 10531
		Func<FeatureSignature, bool> Constraint { get; set; }
	}
}
