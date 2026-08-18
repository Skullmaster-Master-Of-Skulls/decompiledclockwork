using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsList
{
	// Token: 0x02000AE4 RID: 2788
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAppointmentsReq : BaseMessageReq
	{
		// Token: 0x1700159C RID: 5532
		// (get) Token: 0x06003AF2 RID: 15090 RVA: 0x0001CB45 File Offset: 0x0001AD45
		// (set) Token: 0x06003AF3 RID: 15091 RVA: 0x0001CB4D File Offset: 0x0001AD4D
		[DataMember]
		public DateTime StartDate { get; set; }

		// Token: 0x1700159D RID: 5533
		// (get) Token: 0x06003AF4 RID: 15092 RVA: 0x0001CB56 File Offset: 0x0001AD56
		// (set) Token: 0x06003AF5 RID: 15093 RVA: 0x0001CB5E File Offset: 0x0001AD5E
		[DataMember]
		public int NumDays { get; set; }

		// Token: 0x1700159E RID: 5534
		// (get) Token: 0x06003AF6 RID: 15094 RVA: 0x0001CB67 File Offset: 0x0001AD67
		// (set) Token: 0x06003AF7 RID: 15095 RVA: 0x0001CB6F File Offset: 0x0001AD6F
		[DataMember]
		public IList<int> PersonIds { get; set; }

		// Token: 0x1700159F RID: 5535
		// (get) Token: 0x06003AF8 RID: 15096 RVA: 0x0001CB78 File Offset: 0x0001AD78
		// (set) Token: 0x06003AF9 RID: 15097 RVA: 0x0001CB80 File Offset: 0x0001AD80
		[DataMember]
		public bool LoadIsStudentsFirstAppointment { get; set; }
	}
}
