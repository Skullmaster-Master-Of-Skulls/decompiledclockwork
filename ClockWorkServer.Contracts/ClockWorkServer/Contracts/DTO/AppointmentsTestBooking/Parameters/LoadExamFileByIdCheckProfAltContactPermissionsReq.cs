using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Parameters
{
	// Token: 0x02000A53 RID: 2643
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadExamFileByIdCheckProfAltContactPermissionsReq : BaseMessageReq
	{
		// Token: 0x17001430 RID: 5168
		// (get) Token: 0x06003784 RID: 14212 RVA: 0x0001AFFC File Offset: 0x000191FC
		// (set) Token: 0x06003785 RID: 14213 RVA: 0x0001B004 File Offset: 0x00019204
		[DataMember]
		public int InstructorId { get; set; }

		// Token: 0x17001431 RID: 5169
		// (get) Token: 0x06003786 RID: 14214 RVA: 0x0001B00D File Offset: 0x0001920D
		// (set) Token: 0x06003787 RID: 14215 RVA: 0x0001B015 File Offset: 0x00019215
		[DataMember]
		public int AltContactId { get; set; }

		// Token: 0x17001432 RID: 5170
		// (get) Token: 0x06003788 RID: 14216 RVA: 0x0001B01E File Offset: 0x0001921E
		// (set) Token: 0x06003789 RID: 14217 RVA: 0x0001B026 File Offset: 0x00019226
		[DataMember]
		public int ExamId { get; set; }

		// Token: 0x17001433 RID: 5171
		// (get) Token: 0x0600378A RID: 14218 RVA: 0x0001B02F File Offset: 0x0001922F
		// (set) Token: 0x0600378B RID: 14219 RVA: 0x0001B037 File Offset: 0x00019237
		[DataMember]
		public int ExamFileId { get; set; }
	}
}
