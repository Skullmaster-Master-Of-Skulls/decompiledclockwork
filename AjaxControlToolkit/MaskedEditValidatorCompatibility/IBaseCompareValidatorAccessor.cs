using System;

namespace AjaxControlToolkit.MaskedEditValidatorCompatibility
{
	// Token: 0x02000135 RID: 309
	internal interface IBaseCompareValidatorAccessor : IBaseValidatorAccessor, IWebControlAccessor
	{
		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x060007AD RID: 1965
		int CutoffYear { get; }

		// Token: 0x060007AE RID: 1966
		string GetDateElementOrder();
	}
}
