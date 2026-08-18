using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Public.Entities.Cases;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Cases
{
	// Token: 0x02000899 RID: 2201
	[DataContract(Namespace = "http://tpro.ca")]
	public class CaseClientDTO
	{
		// Token: 0x17000FB3 RID: 4019
		// (get) Token: 0x06002C9F RID: 11423 RVA: 0x00015218 File Offset: 0x00013418
		// (set) Token: 0x06002CA0 RID: 11424 RVA: 0x00015220 File Offset: 0x00013420
		[DataMember]
		public PersonBaseDTO Client { get; set; }

		// Token: 0x17000FB4 RID: 4020
		// (get) Token: 0x06002CA1 RID: 11425 RVA: 0x00015229 File Offset: 0x00013429
		// (set) Token: 0x06002CA2 RID: 11426 RVA: 0x00015231 File Offset: 0x00013431
		[DataMember]
		public eCaseClientType ClientType { get; set; }
	}
}
