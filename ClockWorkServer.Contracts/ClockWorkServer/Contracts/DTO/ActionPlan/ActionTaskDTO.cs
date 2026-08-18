using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ActionPlan
{
	// Token: 0x02000C94 RID: 3220
	[DataContract(Namespace = "http://tpro.ca")]
	public class ActionTaskDTO
	{
		// Token: 0x1700189A RID: 6298
		// (get) Token: 0x0600431F RID: 17183 RVA: 0x000244BC File Offset: 0x000226BC
		// (set) Token: 0x06004320 RID: 17184 RVA: 0x000244C4 File Offset: 0x000226C4
		[DataMember]
		public int TaskId { get; set; }

		// Token: 0x1700189B RID: 6299
		// (get) Token: 0x06004321 RID: 17185 RVA: 0x000244CD File Offset: 0x000226CD
		// (set) Token: 0x06004322 RID: 17186 RVA: 0x000244D5 File Offset: 0x000226D5
		[DataMember]
		public eWhoResponsibleDTO WhoResponsible { get; set; }

		// Token: 0x1700189C RID: 6300
		// (get) Token: 0x06004323 RID: 17187 RVA: 0x000244DE File Offset: 0x000226DE
		// (set) Token: 0x06004324 RID: 17188 RVA: 0x000244E6 File Offset: 0x000226E6
		[DataMember]
		public PersonBaseDTO Student { get; set; }

		// Token: 0x1700189D RID: 6301
		// (get) Token: 0x06004325 RID: 17189 RVA: 0x000244EF File Offset: 0x000226EF
		// (set) Token: 0x06004326 RID: 17190 RVA: 0x000244F7 File Offset: 0x000226F7
		[DataMember]
		public DateTime DateAdded { get; set; }

		// Token: 0x1700189E RID: 6302
		// (get) Token: 0x06004327 RID: 17191 RVA: 0x00024500 File Offset: 0x00022700
		// (set) Token: 0x06004328 RID: 17192 RVA: 0x00024508 File Offset: 0x00022708
		[DataMember]
		public DateTime LastDateModified { get; set; }

		// Token: 0x1700189F RID: 6303
		// (get) Token: 0x06004329 RID: 17193 RVA: 0x00024511 File Offset: 0x00022711
		// (set) Token: 0x0600432A RID: 17194 RVA: 0x00024519 File Offset: 0x00022719
		[DataMember]
		public PersonBaseDTO WhoAdded { get; set; }

		// Token: 0x170018A0 RID: 6304
		// (get) Token: 0x0600432B RID: 17195 RVA: 0x00024522 File Offset: 0x00022722
		// (set) Token: 0x0600432C RID: 17196 RVA: 0x0002452A File Offset: 0x0002272A
		[DataMember]
		public PersonBaseDTO WhoLastModified { get; set; }

		// Token: 0x170018A1 RID: 6305
		// (get) Token: 0x0600432D RID: 17197 RVA: 0x00024533 File Offset: 0x00022733
		// (set) Token: 0x0600432E RID: 17198 RVA: 0x0002453B File Offset: 0x0002273B
		[DataMember]
		public string Description { get; set; }

		// Token: 0x170018A2 RID: 6306
		// (get) Token: 0x0600432F RID: 17199 RVA: 0x00024544 File Offset: 0x00022744
		// (set) Token: 0x06004330 RID: 17200 RVA: 0x0002454C File Offset: 0x0002274C
		[DataMember]
		public string StaffNotes { get; set; }

		// Token: 0x170018A3 RID: 6307
		// (get) Token: 0x06004331 RID: 17201 RVA: 0x00024555 File Offset: 0x00022755
		// (set) Token: 0x06004332 RID: 17202 RVA: 0x0002455D File Offset: 0x0002275D
		[DataMember]
		public string StudentNotes { get; set; }

		// Token: 0x170018A4 RID: 6308
		// (get) Token: 0x06004333 RID: 17203 RVA: 0x00024566 File Offset: 0x00022766
		// (set) Token: 0x06004334 RID: 17204 RVA: 0x0002456E File Offset: 0x0002276E
		[DataMember]
		public int OrderNum { get; set; }

		// Token: 0x170018A5 RID: 6309
		// (get) Token: 0x06004335 RID: 17205 RVA: 0x00024577 File Offset: 0x00022777
		// (set) Token: 0x06004336 RID: 17206 RVA: 0x0002457F File Offset: 0x0002277F
		[DataMember]
		public ActionTaskCompletionStatusDTO CompletionStatus { get; set; }
	}
}
