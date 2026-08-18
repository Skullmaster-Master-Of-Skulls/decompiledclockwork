using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Inventory;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsPointOfContact
{
	// Token: 0x0200091E RID: 2334
	[DataContract(Namespace = "http://tpro.ca")]
	public class SaveLoanActionAsPointOfContactReq : BaseMessageReq
	{
		// Token: 0x170010C1 RID: 4289
		// (get) Token: 0x06002F4D RID: 12109 RVA: 0x00016835 File Offset: 0x00014A35
		// (set) Token: 0x06002F4E RID: 12110 RVA: 0x0001683D File Offset: 0x00014A3D
		[DataMember]
		public InventoryProductSnapshotDTO ProductSnapshot { get; set; }
	}
}
