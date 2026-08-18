using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.AlternativeFormat;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000B9D RID: 2973
	[DataContract(Namespace = "http://tpro.ca")]
	public class MediaContentPerFormatStatusInfoDTO
	{
		// Token: 0x17001705 RID: 5893
		// (get) Token: 0x06003E8D RID: 16013 RVA: 0x0001E9E9 File Offset: 0x0001CBE9
		// (set) Token: 0x06003E8E RID: 16014 RVA: 0x0001E9F1 File Offset: 0x0001CBF1
		[DataMember]
		public int MediaContentPerFormatId { get; set; }

		// Token: 0x17001706 RID: 5894
		// (get) Token: 0x06003E8F RID: 16015 RVA: 0x0001E9FA File Offset: 0x0001CBFA
		// (set) Token: 0x06003E90 RID: 16016 RVA: 0x0001EA02 File Offset: 0x0001CC02
		[DataMember]
		public MediaContentFormat MediaContentFormat { get; set; }

		// Token: 0x17001707 RID: 5895
		// (get) Token: 0x06003E91 RID: 16017 RVA: 0x0001EA0B File Offset: 0x0001CC0B
		// (set) Token: 0x06003E92 RID: 16018 RVA: 0x0001EA13 File Offset: 0x0001CC13
		[DataMember]
		public eMediaContentPerFormatStatus Status { get; set; }

		// Token: 0x17001708 RID: 5896
		// (get) Token: 0x06003E93 RID: 16019 RVA: 0x0001EA1C File Offset: 0x0001CC1C
		// (set) Token: 0x06003E94 RID: 16020 RVA: 0x0001EA24 File Offset: 0x0001CC24
		[DataMember]
		public IList<int> CompletedJobIds { get; set; }

		// Token: 0x17001709 RID: 5897
		// (get) Token: 0x06003E95 RID: 16021 RVA: 0x0001EA2D File Offset: 0x0001CC2D
		// (set) Token: 0x06003E96 RID: 16022 RVA: 0x0001EA35 File Offset: 0x0001CC35
		[DataMember]
		public IList<int> InProgressJobIds { get; set; }
	}
}
