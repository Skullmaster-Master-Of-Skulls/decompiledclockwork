using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x020009F1 RID: 2545
	[DataContract(Namespace = "http://tpro.ca")]
	public class SaveClassTestDefinitionReq : BaseMessageReq
	{
		// Token: 0x1700131B RID: 4891
		// (get) Token: 0x060034FE RID: 13566 RVA: 0x00019C9E File Offset: 0x00017E9E
		// (set) Token: 0x060034FF RID: 13567 RVA: 0x00019CA6 File Offset: 0x00017EA6
		[DataMember]
		public ClassTestDTO OldClassTest { get; set; }

		// Token: 0x1700131C RID: 4892
		// (get) Token: 0x06003500 RID: 13568 RVA: 0x00019CAF File Offset: 0x00017EAF
		// (set) Token: 0x06003501 RID: 13569 RVA: 0x00019CB7 File Offset: 0x00017EB7
		[DataMember]
		public ClassTestDTO NewClassTest { get; set; }
	}
}
