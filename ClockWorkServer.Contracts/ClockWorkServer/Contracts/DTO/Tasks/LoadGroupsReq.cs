using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Tasks
{
	// Token: 0x02000200 RID: 512
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadGroupsReq : BaseMessageReq
	{
		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000BA1 RID: 2977 RVA: 0x00005550 File Offset: 0x00003750
		// (set) Token: 0x06000BA2 RID: 2978 RVA: 0x00005558 File Offset: 0x00003758
		[DataMember]
		public bool IncludeShared { get; set; }

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000BA3 RID: 2979 RVA: 0x00005561 File Offset: 0x00003761
		// (set) Token: 0x06000BA4 RID: 2980 RVA: 0x00005569 File Offset: 0x00003769
		[DataMember]
		public bool IncludePrivate { get; set; }
	}
}
