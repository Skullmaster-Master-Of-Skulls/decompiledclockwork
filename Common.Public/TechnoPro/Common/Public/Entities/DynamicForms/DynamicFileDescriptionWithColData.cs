using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.DynamicForms
{
	// Token: 0x0200034B RID: 843
	public class DynamicFileDescriptionWithColData : DynamicFileDescription
	{
		// Token: 0x17000ADE RID: 2782
		// (get) Token: 0x06001A2E RID: 6702 RVA: 0x0001E5AA File Offset: 0x0001C7AA
		// (set) Token: 0x06001A2F RID: 6703 RVA: 0x0001E5B2 File Offset: 0x0001C7B2
		public IList<string> ColumnData { get; set; }
	}
}
