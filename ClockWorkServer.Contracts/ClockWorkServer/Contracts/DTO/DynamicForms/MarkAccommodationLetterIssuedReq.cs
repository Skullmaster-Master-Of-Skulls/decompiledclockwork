using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x02000626 RID: 1574
	[DataContract(Namespace = "http://tpro.ca")]
	public class MarkAccommodationLetterIssuedReq : BaseMessageReq
	{
		// Token: 0x17000AB2 RID: 2738
		// (get) Token: 0x06002004 RID: 8196 RVA: 0x0000E890 File Offset: 0x0000CA90
		// (set) Token: 0x06002005 RID: 8197 RVA: 0x0000E898 File Offset: 0x0000CA98
		[DataMember]
		public int PersonId { get; set; }

		// Token: 0x17000AB3 RID: 2739
		// (get) Token: 0x06002006 RID: 8198 RVA: 0x0000E8A1 File Offset: 0x0000CAA1
		// (set) Token: 0x06002007 RID: 8199 RVA: 0x0000E8A9 File Offset: 0x0000CAA9
		[DataMember]
		public IList<int> LuCourseIds { get; set; }
	}
}
