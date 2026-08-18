using System;
using System.Globalization;

namespace Telerik.Web.UI
{
	// Token: 0x020012C2 RID: 4802
	public interface IRadNumericTextBox
	{
		// Token: 0x17004116 RID: 16662
		// (get) Token: 0x0600C945 RID: 51525
		// (set) Token: 0x0600C946 RID: 51526
		CultureInfo Culture { get; set; }

		// Token: 0x17004117 RID: 16663
		// (get) Token: 0x0600C947 RID: 51527
		// (set) Token: 0x0600C948 RID: 51528
		NumericType Type { get; set; }
	}
}
