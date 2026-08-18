using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x020009AD RID: 2477
	[DataContract(Namespace = "http://tpro.ca")]
	public class ClassTestForEditDTO
	{
		// Token: 0x170011EF RID: 4591
		// (get) Token: 0x0600325E RID: 12894 RVA: 0x0001875D File Offset: 0x0001695D
		// (set) Token: 0x0600325F RID: 12895 RVA: 0x00018765 File Offset: 0x00016965
		[DataMember]
		public ClassTestDTO ClassTest { get; set; }
	}
}
