using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.Intake;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Intake
{
	// Token: 0x020005CF RID: 1487
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateRealStudentAccountFromIntakeResultDTO
	{
		// Token: 0x17000A1B RID: 2587
		// (get) Token: 0x06001E80 RID: 7808 RVA: 0x0000DE2B File Offset: 0x0000C02B
		// (set) Token: 0x06001E81 RID: 7809 RVA: 0x0000DE33 File Offset: 0x0000C033
		[DataMember]
		public int PersonId { get; set; }

		// Token: 0x17000A1C RID: 2588
		// (get) Token: 0x06001E82 RID: 7810 RVA: 0x0000DE3C File Offset: 0x0000C03C
		// (set) Token: 0x06001E83 RID: 7811 RVA: 0x0000DE44 File Offset: 0x0000C044
		[DataMember]
		public eCreateRealStudentAccountFromIntakeStatus Status { get; set; }
	}
}
