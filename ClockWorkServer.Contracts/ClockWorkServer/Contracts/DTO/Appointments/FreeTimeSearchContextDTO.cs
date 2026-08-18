using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.Appointments;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments
{
	// Token: 0x02000937 RID: 2359
	[DataContract(Namespace = "http://tpro.ca")]
	public class FreeTimeSearchContextDTO
	{
		// Token: 0x1700112A RID: 4394
		// (get) Token: 0x0600305B RID: 12379 RVA: 0x000179D0 File Offset: 0x00015BD0
		// (set) Token: 0x0600305C RID: 12380 RVA: 0x000179D8 File Offset: 0x00015BD8
		[DataMember]
		public IList<int> PersonIds { get; set; }

		// Token: 0x1700112B RID: 4395
		// (get) Token: 0x0600305D RID: 12381 RVA: 0x000179E1 File Offset: 0x00015BE1
		// (set) Token: 0x0600305E RID: 12382 RVA: 0x000179E9 File Offset: 0x00015BE9
		[DataMember]
		public eFreeTimeSearchMethod SearchMethod { get; set; }

		// Token: 0x1700112C RID: 4396
		// (get) Token: 0x0600305F RID: 12383 RVA: 0x000179F2 File Offset: 0x00015BF2
		// (set) Token: 0x06003060 RID: 12384 RVA: 0x000179FA File Offset: 0x00015BFA
		[DataMember]
		public DateTime SearchStartDateTime { get; set; }

		// Token: 0x1700112D RID: 4397
		// (get) Token: 0x06003061 RID: 12385 RVA: 0x00017A03 File Offset: 0x00015C03
		// (set) Token: 0x06003062 RID: 12386 RVA: 0x00017A0B File Offset: 0x00015C0B
		[DataMember]
		public TimeSpan SearchEnd { get; set; }

		// Token: 0x1700112E RID: 4398
		// (get) Token: 0x06003063 RID: 12387 RVA: 0x00017A14 File Offset: 0x00015C14
		// (set) Token: 0x06003064 RID: 12388 RVA: 0x00017A1C File Offset: 0x00015C1C
		[DataMember]
		public TimeSpan SearchAppointmentDuration { get; set; }

		// Token: 0x1700112F RID: 4399
		// (get) Token: 0x06003065 RID: 12389 RVA: 0x00017A25 File Offset: 0x00015C25
		// (set) Token: 0x06003066 RID: 12390 RVA: 0x00017A2D File Offset: 0x00015C2D
		[DataMember]
		public IList<FreeTimeSearchRecurringRuleDTO> RecurringRules { get; set; }
	}
}
