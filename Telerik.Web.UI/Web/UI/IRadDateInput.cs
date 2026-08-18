using System;
using System.Globalization;

namespace Telerik.Web.UI
{
	// Token: 0x0200055B RID: 1371
	public interface IRadDateInput
	{
		// Token: 0x17000FF0 RID: 4080
		// (get) Token: 0x0600315E RID: 12638
		// (set) Token: 0x0600315F RID: 12639
		CultureInfo Culture { get; set; }

		// Token: 0x17000FF1 RID: 4081
		// (get) Token: 0x06003160 RID: 12640
		// (set) Token: 0x06003161 RID: 12641
		int ShortYearCenturyEnd { get; set; }

		// Token: 0x17000FF2 RID: 4082
		// (get) Token: 0x06003162 RID: 12642
		// (set) Token: 0x06003163 RID: 12643
		string DateFormat { get; set; }
	}
}
