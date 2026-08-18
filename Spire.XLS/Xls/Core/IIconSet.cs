using System;
using System.Collections.Generic;

namespace Spire.Xls.Core
{
	// Token: 0x02000383 RID: 899
	public interface IIconSet
	{
		// Token: 0x17000CE1 RID: 3297
		// (get) Token: 0x060036A2 RID: 13986
		IList<IConditionValue> IconCriteria { get; }

		// Token: 0x17000CE2 RID: 3298
		// (get) Token: 0x060036A3 RID: 13987
		// (set) Token: 0x060036A4 RID: 13988
		IconSetType IconSet { get; set; }

		// Token: 0x17000CE3 RID: 3299
		// (get) Token: 0x060036A5 RID: 13989
		// (set) Token: 0x060036A6 RID: 13990
		bool PercentileValues { get; set; }

		// Token: 0x17000CE4 RID: 3300
		// (get) Token: 0x060036A7 RID: 13991
		// (set) Token: 0x060036A8 RID: 13992
		bool IsReverseOrder { get; set; }

		// Token: 0x17000CE5 RID: 3301
		// (get) Token: 0x060036A9 RID: 13993
		// (set) Token: 0x060036AA RID: 13994
		bool ShowIconOnly { get; set; }
	}
}
