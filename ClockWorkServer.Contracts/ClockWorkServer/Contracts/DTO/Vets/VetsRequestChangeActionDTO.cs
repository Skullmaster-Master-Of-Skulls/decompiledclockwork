using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.People;
using TechnoPro.Common.Public.Entities.Vets;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Vets
{
	// Token: 0x0200011B RID: 283
	[DataContract(Namespace = "http://tpro.ca")]
	public class VetsRequestChangeActionDTO
	{
		// Token: 0x170000CD RID: 205
		// (get) Token: 0x0600071E RID: 1822 RVA: 0x000031B1 File Offset: 0x000013B1
		// (set) Token: 0x0600071F RID: 1823 RVA: 0x000031B9 File Offset: 0x000013B9
		[DataMember]
		public eVetsRequestChangeActionType ActionType { get; set; }

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000720 RID: 1824 RVA: 0x000031C2 File Offset: 0x000013C2
		// (set) Token: 0x06000721 RID: 1825 RVA: 0x000031CA File Offset: 0x000013CA
		[DataMember]
		public DateTime DateOfChange { get; set; }

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000722 RID: 1826 RVA: 0x000031D3 File Offset: 0x000013D3
		// (set) Token: 0x06000723 RID: 1827 RVA: 0x000031DB File Offset: 0x000013DB
		[DataMember]
		public PersonBase WhoChanged { get; set; }
	}
}
