using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.DynamicForms
{
	// Token: 0x0200035E RID: 862
	[Serializable]
	public class DynamicDataSet
	{
		// Token: 0x17000B23 RID: 2851
		// (get) Token: 0x06001AD3 RID: 6867 RVA: 0x0001ECCE File Offset: 0x0001CECE
		// (set) Token: 0x06001AD4 RID: 6868 RVA: 0x0001ECD6 File Offset: 0x0001CED6
		public DynamicDataContext Context { get; set; }

		// Token: 0x17000B24 RID: 2852
		// (get) Token: 0x06001AD5 RID: 6869 RVA: 0x0001ECDF File Offset: 0x0001CEDF
		// (set) Token: 0x06001AD6 RID: 6870 RVA: 0x0001ECE7 File Offset: 0x0001CEE7
		public List<DynamicData> Data { get; set; }
	}
}
