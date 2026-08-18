using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.AlternativeFormat;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000B9C RID: 2972
	[DataContract(Namespace = "http://tpro.ca")]
	public class MediaContentPerFormatInfoDTO
	{
		// Token: 0x17001702 RID: 5890
		// (get) Token: 0x06003E86 RID: 16006 RVA: 0x0001E9B6 File Offset: 0x0001CBB6
		// (set) Token: 0x06003E87 RID: 16007 RVA: 0x0001E9BE File Offset: 0x0001CBBE
		[DataMember]
		public int MediaContentPerFormatId { get; set; }

		// Token: 0x17001703 RID: 5891
		// (get) Token: 0x06003E88 RID: 16008 RVA: 0x0001E9C7 File Offset: 0x0001CBC7
		// (set) Token: 0x06003E89 RID: 16009 RVA: 0x0001E9CF File Offset: 0x0001CBCF
		[DataMember]
		public MediaContentFormat MediaContentFormat { get; set; }

		// Token: 0x17001704 RID: 5892
		// (get) Token: 0x06003E8A RID: 16010 RVA: 0x0001E9D8 File Offset: 0x0001CBD8
		// (set) Token: 0x06003E8B RID: 16011 RVA: 0x0001E9E0 File Offset: 0x0001CBE0
		[DataMember]
		public Guid MediaContentId { get; set; }
	}
}
