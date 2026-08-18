using System;
using System.Data;

namespace TechnoPro.Common.Public.Entities.UnivDataAccess
{
	// Token: 0x02000157 RID: 343
	public class QueryResult
	{
		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000833 RID: 2099 RVA: 0x000118E6 File Offset: 0x0000FAE6
		// (set) Token: 0x06000834 RID: 2100 RVA: 0x000118EE File Offset: 0x0000FAEE
		public DataTable DataTable { get; set; }

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000835 RID: 2101 RVA: 0x000118F7 File Offset: 0x0000FAF7
		// (set) Token: 0x06000836 RID: 2102 RVA: 0x000118FF File Offset: 0x0000FAFF
		public int Id { get; set; }

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000837 RID: 2103 RVA: 0x00011908 File Offset: 0x0000FB08
		// (set) Token: 0x06000838 RID: 2104 RVA: 0x00011910 File Offset: 0x0000FB10
		public string ErrorMessage { get; set; }
	}
}
