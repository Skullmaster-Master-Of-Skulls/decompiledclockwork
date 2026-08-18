using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x020009B9 RID: 2489
	[DataContract(Namespace = "http://tpro.ca")]
	public class AppointmentTestExamInfoDTO
	{
		// Token: 0x06003380 RID: 13184 RVA: 0x000190BC File Offset: 0x000172BC
		public AppointmentTestExamInfoDTO()
		{
			this.ClassTestDefinition = new ClassTestDTO();
			this.StudentClassTestDefinition = new StudentClassTestDTO();
		}

		// Token: 0x1700127A RID: 4730
		// (get) Token: 0x06003381 RID: 13185 RVA: 0x000190DE File Offset: 0x000172DE
		// (set) Token: 0x06003382 RID: 13186 RVA: 0x000190E6 File Offset: 0x000172E6
		[DataMember]
		public ClassTestDTO ClassTestDefinition { get; set; }

		// Token: 0x1700127B RID: 4731
		// (get) Token: 0x06003383 RID: 13187 RVA: 0x000190EF File Offset: 0x000172EF
		// (set) Token: 0x06003384 RID: 13188 RVA: 0x000190F7 File Offset: 0x000172F7
		[DataMember]
		public StudentClassTestDTO StudentClassTestDefinition { get; set; }

		// Token: 0x1700127C RID: 4732
		// (get) Token: 0x06003385 RID: 13189 RVA: 0x00019100 File Offset: 0x00017300
		// (set) Token: 0x06003386 RID: 13190 RVA: 0x00019108 File Offset: 0x00017308
		[DataMember]
		public int BreakTimeMinutes { get; set; }

		// Token: 0x1700127D RID: 4733
		// (get) Token: 0x06003387 RID: 13191 RVA: 0x00019111 File Offset: 0x00017311
		// (set) Token: 0x06003388 RID: 13192 RVA: 0x00019119 File Offset: 0x00017319
		[DataMember]
		public SittingDTO Sitting { get; set; }
	}
}
