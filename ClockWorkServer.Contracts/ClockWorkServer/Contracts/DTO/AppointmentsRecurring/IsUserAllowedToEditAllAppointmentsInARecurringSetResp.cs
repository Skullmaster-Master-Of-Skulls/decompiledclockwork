using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsRecurring
{
	// Token: 0x02000AB8 RID: 2744
	[DataContract(Namespace = "http://tpro.ca")]
	public class IsUserAllowedToEditAllAppointmentsInARecurringSetResp
	{
		// Token: 0x17001553 RID: 5459
		// (get) Token: 0x06003A31 RID: 14897 RVA: 0x0001C3D1 File Offset: 0x0001A5D1
		// (set) Token: 0x06003A32 RID: 14898 RVA: 0x0001C3D9 File Offset: 0x0001A5D9
		[DataMember]
		public bool AllowedToEditEntireGroup { get; set; }
	}
}
