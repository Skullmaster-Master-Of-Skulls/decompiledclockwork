using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.FullTest
{
	// Token: 0x02000A89 RID: 2697
	[DataContract(Namespace = "http://tpro.ca")]
	public class InstructorAcknowledgedStudentDTO
	{
		// Token: 0x1700148A RID: 5258
		// (get) Token: 0x0600386E RID: 14446 RVA: 0x0001B5F6 File Offset: 0x000197F6
		// (set) Token: 0x0600386F RID: 14447 RVA: 0x0001B5FE File Offset: 0x000197FE
		[DataMember]
		public int SelectedIndex { get; set; }

		// Token: 0x1700148B RID: 5259
		// (get) Token: 0x06003870 RID: 14448 RVA: 0x0001B607 File Offset: 0x00019807
		// (set) Token: 0x06003871 RID: 14449 RVA: 0x0001B60F File Offset: 0x0001980F
		[DataMember]
		public string SelectedText { get; set; }

		// Token: 0x1700148C RID: 5260
		// (get) Token: 0x06003872 RID: 14450 RVA: 0x0001B618 File Offset: 0x00019818
		// (set) Token: 0x06003873 RID: 14451 RVA: 0x0001B620 File Offset: 0x00019820
		[DataMember]
		public DateTime? DateAcknowledged { get; set; }
	}
}
