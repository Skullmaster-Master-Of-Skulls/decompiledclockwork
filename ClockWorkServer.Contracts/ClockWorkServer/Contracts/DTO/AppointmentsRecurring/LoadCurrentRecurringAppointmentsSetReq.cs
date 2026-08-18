using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsRecurring
{
	// Token: 0x02000AB0 RID: 2736
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadCurrentRecurringAppointmentsSetReq : BaseMessageReq
	{
		// Token: 0x17001547 RID: 5447
		// (get) Token: 0x06003A11 RID: 14865 RVA: 0x0001C305 File Offset: 0x0001A505
		// (set) Token: 0x06003A12 RID: 14866 RVA: 0x0001C30D File Offset: 0x0001A50D
		[DataMember]
		public int MasterGroupCode { get; set; }
	}
}
