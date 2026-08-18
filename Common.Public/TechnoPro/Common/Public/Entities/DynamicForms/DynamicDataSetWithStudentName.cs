using System;
using System.Collections.Generic;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.DynamicForms
{
	// Token: 0x02000348 RID: 840
	public class DynamicDataSetWithStudentName
	{
		// Token: 0x17000AD0 RID: 2768
		// (get) Token: 0x06001A0F RID: 6671 RVA: 0x0001E4AB File Offset: 0x0001C6AB
		// (set) Token: 0x06001A10 RID: 6672 RVA: 0x0001E4B3 File Offset: 0x0001C6B3
		public PersonBase Student { get; set; }

		// Token: 0x17000AD1 RID: 2769
		// (get) Token: 0x06001A11 RID: 6673 RVA: 0x0001E4BC File Offset: 0x0001C6BC
		// (set) Token: 0x06001A12 RID: 6674 RVA: 0x0001E4C4 File Offset: 0x0001C6C4
		public List<DynamicData> Data { get; set; }
	}
}
