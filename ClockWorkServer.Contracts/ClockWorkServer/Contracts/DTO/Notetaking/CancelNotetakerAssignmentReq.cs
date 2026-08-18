using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Notetaking
{
	// Token: 0x02000452 RID: 1106
	[DataContract(Namespace = "http://tpro.ca")]
	public class CancelNotetakerAssignmentReq : BaseReportMessageReq
	{
		// Token: 0x17000773 RID: 1907
		// (get) Token: 0x060017AC RID: 6060 RVA: 0x0000AF05 File Offset: 0x00009105
		// (set) Token: 0x060017AD RID: 6061 RVA: 0x0000AF0D File Offset: 0x0000910D
		[DataMember]
		public int StudentPersonId { get; set; }

		// Token: 0x17000774 RID: 1908
		// (get) Token: 0x060017AE RID: 6062 RVA: 0x0000AF16 File Offset: 0x00009116
		// (set) Token: 0x060017AF RID: 6063 RVA: 0x0000AF1E File Offset: 0x0000911E
		[DataMember]
		public int StudentLuCourseId { get; set; }

		// Token: 0x17000775 RID: 1909
		// (get) Token: 0x060017B0 RID: 6064 RVA: 0x0000AF27 File Offset: 0x00009127
		// (set) Token: 0x060017B1 RID: 6065 RVA: 0x0000AF2F File Offset: 0x0000912F
		[DataMember]
		public string Why { get; set; }
	}
}
