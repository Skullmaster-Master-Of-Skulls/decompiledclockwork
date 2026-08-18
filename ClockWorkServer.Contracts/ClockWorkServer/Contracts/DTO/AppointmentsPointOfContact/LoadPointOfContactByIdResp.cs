using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsPointOfContact
{
	// Token: 0x02000922 RID: 2338
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadPointOfContactByIdResp
	{
		// Token: 0x170010C5 RID: 4293
		// (get) Token: 0x06002F59 RID: 12121 RVA: 0x00016879 File Offset: 0x00014A79
		// (set) Token: 0x06002F5A RID: 12122 RVA: 0x00016881 File Offset: 0x00014A81
		[DataMember]
		public PointOfContactDTO PointOfContact { get; set; }
	}
}
