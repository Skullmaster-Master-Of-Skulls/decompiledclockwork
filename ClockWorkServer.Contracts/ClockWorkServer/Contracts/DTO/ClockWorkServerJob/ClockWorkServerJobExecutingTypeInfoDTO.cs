using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkServerJob
{
	// Token: 0x0200084B RID: 2123
	[DataContract(Namespace = "http://tpro.ca")]
	public class ClockWorkServerJobExecutingTypeInfoDTO
	{
		// Token: 0x17000F3B RID: 3899
		// (get) Token: 0x06002B63 RID: 11107 RVA: 0x000149AE File Offset: 0x00012BAE
		// (set) Token: 0x06002B64 RID: 11108 RVA: 0x000149B6 File Offset: 0x00012BB6
		[DataMember]
		public string Title { get; set; }

		// Token: 0x17000F3C RID: 3900
		// (get) Token: 0x06002B65 RID: 11109 RVA: 0x000149BF File Offset: 0x00012BBF
		// (set) Token: 0x06002B66 RID: 11110 RVA: 0x000149C7 File Offset: 0x00012BC7
		[DataMember]
		public string ParametersDescription { get; set; }

		// Token: 0x17000F3D RID: 3901
		// (get) Token: 0x06002B67 RID: 11111 RVA: 0x000149D0 File Offset: 0x00012BD0
		// (set) Token: 0x06002B68 RID: 11112 RVA: 0x000149D8 File Offset: 0x00012BD8
		[DataMember]
		public string ExecutingType { get; set; }

		// Token: 0x17000F3E RID: 3902
		// (get) Token: 0x06002B69 RID: 11113 RVA: 0x000149E1 File Offset: 0x00012BE1
		// (set) Token: 0x06002B6A RID: 11114 RVA: 0x000149E9 File Offset: 0x00012BE9
		[DataMember]
		public string ControlParametersType { get; set; }
	}
}
