using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x02000627 RID: 1575
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAllAppointmentIdsWithNotesReq : BaseMessageReq
	{
		// Token: 0x17000AB4 RID: 2740
		// (get) Token: 0x06002009 RID: 8201 RVA: 0x0000E8B2 File Offset: 0x0000CAB2
		// (set) Token: 0x0600200A RID: 8202 RVA: 0x0000E8BA File Offset: 0x0000CABA
		[DataMember]
		public int StudentPersonId { get; set; }

		// Token: 0x17000AB5 RID: 2741
		// (get) Token: 0x0600200B RID: 8203 RVA: 0x0000E8C3 File Offset: 0x0000CAC3
		// (set) Token: 0x0600200C RID: 8204 RVA: 0x0000E8CB File Offset: 0x0000CACB
		[DataMember]
		public DateTime? StartDate { get; set; }

		// Token: 0x17000AB6 RID: 2742
		// (get) Token: 0x0600200D RID: 8205 RVA: 0x0000E8D4 File Offset: 0x0000CAD4
		// (set) Token: 0x0600200E RID: 8206 RVA: 0x0000E8DC File Offset: 0x0000CADC
		[DataMember]
		public DateTime? EndDate { get; set; }

		// Token: 0x17000AB7 RID: 2743
		// (get) Token: 0x0600200F RID: 8207 RVA: 0x0000E8E5 File Offset: 0x0000CAE5
		// (set) Token: 0x06002010 RID: 8208 RVA: 0x0000E8ED File Offset: 0x0000CAED
		[DataMember]
		public IList<int> ScreenNums { get; set; }
	}
}
