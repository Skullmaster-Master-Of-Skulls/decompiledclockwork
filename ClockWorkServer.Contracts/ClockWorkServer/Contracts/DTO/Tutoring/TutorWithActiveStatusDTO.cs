using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.Tutoring;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Tutoring
{
	// Token: 0x020001AE RID: 430
	[DataContract(Namespace = "http://tpro.ca")]
	public class TutorWithActiveStatusDTO : TutorDTO
	{
		// Token: 0x170001DD RID: 477
		// (get) Token: 0x060009D6 RID: 2518 RVA: 0x000046A2 File Offset: 0x000028A2
		// (set) Token: 0x060009D7 RID: 2519 RVA: 0x000046AA File Offset: 0x000028AA
		[DataMember]
		public eTutorStatus Status { get; set; }
	}
}
