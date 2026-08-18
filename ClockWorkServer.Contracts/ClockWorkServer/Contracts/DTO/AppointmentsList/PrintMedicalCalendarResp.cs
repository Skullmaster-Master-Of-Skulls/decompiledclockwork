using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsList
{
	// Token: 0x02000ADF RID: 2783
	[DataContract(Namespace = "http://tpro.ca")]
	public class PrintMedicalCalendarResp
	{
		// Token: 0x17001591 RID: 5521
		// (get) Token: 0x06003AD7 RID: 15063 RVA: 0x0001CA8A File Offset: 0x0001AC8A
		// (set) Token: 0x06003AD8 RID: 15064 RVA: 0x0001CA92 File Offset: 0x0001AC92
		[DataMember]
		public BinaryFileDTO File { get; set; }
	}
}
