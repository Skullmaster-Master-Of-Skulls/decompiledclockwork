using System;
using System.Globalization;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x02000CE5 RID: 3301
	internal class PivotResultItem
	{
		// Token: 0x1700276F RID: 10095
		// (get) Token: 0x06007B45 RID: 31557 RVA: 0x001C4E25 File Offset: 0x001C3025
		// (set) Token: 0x06007B46 RID: 31558 RVA: 0x001C4E2D File Offset: 0x001C302D
		public object Key { get; set; }

		// Token: 0x17002770 RID: 10096
		// (get) Token: 0x06007B47 RID: 31559 RVA: 0x001C4E36 File Offset: 0x001C3036
		// (set) Token: 0x06007B48 RID: 31560 RVA: 0x001C4E3E File Offset: 0x001C303E
		public object Aggregates { get; set; }

		// Token: 0x06007B49 RID: 31561 RVA: 0x001C4E48 File Offset: 0x001C3048
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "Key: {0}; Aggregates: {1}", new object[]
			{
				this.Key,
				this.Aggregates
			});
		}
	}
}
