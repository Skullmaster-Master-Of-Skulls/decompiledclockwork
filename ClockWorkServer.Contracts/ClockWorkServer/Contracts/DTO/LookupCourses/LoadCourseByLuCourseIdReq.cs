using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x020007AC RID: 1964
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadCourseByLuCourseIdReq : BaseMessageReq
	{
		// Token: 0x17000E12 RID: 3602
		// (get) Token: 0x06002865 RID: 10341 RVA: 0x000132D3 File Offset: 0x000114D3
		// (set) Token: 0x06002866 RID: 10342 RVA: 0x000132DB File Offset: 0x000114DB
		[DataMember]
		public int LuCourseId { get; set; }
	}
}
