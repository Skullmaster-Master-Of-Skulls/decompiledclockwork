using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000BA0 RID: 2976
	[DataContract(Namespace = "http://tpro.ca")]
	[KnownType(typeof(MediaContentRequestedInfoDTO))]
	public class MediaContentRequestedInfoExtendedDTO : MediaContentRequestedInfoDTO
	{
		// Token: 0x17001725 RID: 5925
		// (get) Token: 0x06003ED3 RID: 16083 RVA: 0x0001ED21 File Offset: 0x0001CF21
		// (set) Token: 0x06003ED4 RID: 16084 RVA: 0x0001ED29 File Offset: 0x0001CF29
		[DataMember]
		public bool HardCopy { get; set; }

		// Token: 0x17001726 RID: 5926
		// (get) Token: 0x06003ED5 RID: 16085 RVA: 0x0001ED32 File Offset: 0x0001CF32
		// (set) Token: 0x06003ED6 RID: 16086 RVA: 0x0001ED3A File Offset: 0x0001CF3A
		[DataMember]
		public int FileSize { get; set; }
	}
}
