using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.DynamicForms.DynamicFieldConversion
{
	// Token: 0x02000380 RID: 896
	public class DynamicDataConversionSet
	{
		// Token: 0x17000B88 RID: 2952
		// (get) Token: 0x06001BC3 RID: 7107 RVA: 0x0001F7C4 File Offset: 0x0001D9C4
		// (set) Token: 0x06001BC4 RID: 7108 RVA: 0x0001F7CC File Offset: 0x0001D9CC
		public eDynamicFieldAvailableConversion Conversion { get; set; }

		// Token: 0x17000B89 RID: 2953
		// (get) Token: 0x06001BC5 RID: 7109 RVA: 0x0001F7D5 File Offset: 0x0001D9D5
		// (set) Token: 0x06001BC6 RID: 7110 RVA: 0x0001F7DD File Offset: 0x0001D9DD
		public int ControlId { get; set; }

		// Token: 0x17000B8A RID: 2954
		// (get) Token: 0x06001BC7 RID: 7111 RVA: 0x0001F7E6 File Offset: 0x0001D9E6
		// (set) Token: 0x06001BC8 RID: 7112 RVA: 0x0001F7EE File Offset: 0x0001D9EE
		public IList<DynamicDataConversionItem> ConversionItems { get; set; }
	}
}
