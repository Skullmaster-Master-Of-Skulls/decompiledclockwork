using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.DynamicForms
{
	// Token: 0x0200036F RID: 879
	public class DynamicFormWithFields
	{
		// Token: 0x17000B4D RID: 2893
		// (get) Token: 0x06001B39 RID: 6969 RVA: 0x0001F2E2 File Offset: 0x0001D4E2
		// (set) Token: 0x06001B3A RID: 6970 RVA: 0x0001F2EA File Offset: 0x0001D4EA
		public DynamicFormWithExtendedInfo Form { get; set; }

		// Token: 0x17000B4E RID: 2894
		// (get) Token: 0x06001B3B RID: 6971 RVA: 0x0001F2F3 File Offset: 0x0001D4F3
		// (set) Token: 0x06001B3C RID: 6972 RVA: 0x0001F2FB File Offset: 0x0001D4FB
		public IList<DynamicFieldOnForm> Fields { get; set; }
	}
}
