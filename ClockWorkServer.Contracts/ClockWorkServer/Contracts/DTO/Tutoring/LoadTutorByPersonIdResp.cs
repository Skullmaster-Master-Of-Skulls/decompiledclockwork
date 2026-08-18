using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Tutoring
{
	// Token: 0x0200019D RID: 413
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadTutorByPersonIdResp
	{
		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x0600099B RID: 2459 RVA: 0x0000453D File Offset: 0x0000273D
		// (set) Token: 0x0600099C RID: 2460 RVA: 0x00004545 File Offset: 0x00002745
		[DataMember]
		public TutorDTO Tutor { get; set; }
	}
}
