using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x020009B7 RID: 2487
	[DataContract(Namespace = "http://tpro.ca")]
	public class UnbookedTestExamStudentDTO
	{
		// Token: 0x17001272 RID: 4722
		// (get) Token: 0x0600336E RID: 13166 RVA: 0x00019034 File Offset: 0x00017234
		// (set) Token: 0x0600336F RID: 13167 RVA: 0x0001903C File Offset: 0x0001723C
		[DataMember]
		public PersonBaseDTO Student { get; set; }

		// Token: 0x17001273 RID: 4723
		// (get) Token: 0x06003370 RID: 13168 RVA: 0x00019045 File Offset: 0x00017245
		// (set) Token: 0x06003371 RID: 13169 RVA: 0x0001904D File Offset: 0x0001724D
		[DataMember]
		public ClassTestBaseDTO ClassTest { get; set; }

		// Token: 0x17001274 RID: 4724
		// (get) Token: 0x06003372 RID: 13170 RVA: 0x00019056 File Offset: 0x00017256
		// (set) Token: 0x06003373 RID: 13171 RVA: 0x0001905E File Offset: 0x0001725E
		[DataMember]
		public string StudentEmail { get; set; }

		// Token: 0x17001275 RID: 4725
		// (get) Token: 0x06003374 RID: 13172 RVA: 0x00019067 File Offset: 0x00017267
		// (set) Token: 0x06003375 RID: 13173 RVA: 0x0001906F File Offset: 0x0001726F
		[DataMember]
		public DateTime? DateLetterIssued { get; set; }
	}
}
