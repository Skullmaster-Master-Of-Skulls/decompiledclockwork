using System;
using System.Data;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Reports.RunReportResults
{
	// Token: 0x02000356 RID: 854
	[DataContract(Namespace = "http://tpro.ca")]
	public class RunFunctionDataDTO
	{
		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x06001385 RID: 4997 RVA: 0x000091F1 File Offset: 0x000073F1
		// (set) Token: 0x06001386 RID: 4998 RVA: 0x000091F9 File Offset: 0x000073F9
		[DataMember]
		public string Name { get; set; }

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x06001387 RID: 4999 RVA: 0x00009202 File Offset: 0x00007402
		// (set) Token: 0x06001388 RID: 5000 RVA: 0x0000920A File Offset: 0x0000740A
		[DataMember]
		public DataTable Table { get; set; }

		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x06001389 RID: 5001 RVA: 0x00009213 File Offset: 0x00007413
		// (set) Token: 0x0600138A RID: 5002 RVA: 0x0000921B File Offset: 0x0000741B
		[DataMember]
		public string TableSort { get; set; }

		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x0600138B RID: 5003 RVA: 0x00009224 File Offset: 0x00007424
		// (set) Token: 0x0600138C RID: 5004 RVA: 0x0000922C File Offset: 0x0000742C
		[DataMember]
		public bool AddToAdditionalData { get; set; }

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x0600138D RID: 5005 RVA: 0x00009235 File Offset: 0x00007435
		// (set) Token: 0x0600138E RID: 5006 RVA: 0x0000923D File Offset: 0x0000743D
		[DataMember]
		public bool IsPrimary { get; set; }
	}
}
