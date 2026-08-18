using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x0200079A RID: 1946
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAlternateContactsBySearchStringResp
	{
		// Token: 0x17000DE9 RID: 3561
		// (get) Token: 0x060027FB RID: 10235 RVA: 0x00012D1E File Offset: 0x00010F1E
		// (set) Token: 0x060027FC RID: 10236 RVA: 0x00012D26 File Offset: 0x00010F26
		[DataMember]
		public IList<AlternateContactDTO> AltContacts { get; set; }
	}
}
