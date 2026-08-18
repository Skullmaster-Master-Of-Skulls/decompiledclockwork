using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C05 RID: 3077
	[DataContract(Namespace = "http://tpro.ca")]
	public class ChangeMediaJobVolunteerListActiveStatusReq : BaseMessageReq
	{
		// Token: 0x170017E2 RID: 6114
		// (get) Token: 0x060040BB RID: 16571 RVA: 0x0001FBEC File Offset: 0x0001DDEC
		// (set) Token: 0x060040BC RID: 16572 RVA: 0x0001FBF4 File Offset: 0x0001DDF4
		[DataMember]
		public IList<int> JobVolunteerIdList { get; set; }

		// Token: 0x170017E3 RID: 6115
		// (get) Token: 0x060040BD RID: 16573 RVA: 0x0001FBFD File Offset: 0x0001DDFD
		// (set) Token: 0x060040BE RID: 16574 RVA: 0x0001FC05 File Offset: 0x0001DE05
		[DataMember]
		public bool IsActive { get; set; }
	}
}
