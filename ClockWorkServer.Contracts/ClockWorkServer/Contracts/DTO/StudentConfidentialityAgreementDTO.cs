using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.ClockWorkServer.Contracts.DTO
{
	// Token: 0x020000F7 RID: 247
	[DataContract(Namespace = "http://tpro.ca")]
	public class StudentConfidentialityAgreementDTO
	{
		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000640 RID: 1600 RVA: 0x0000299D File Offset: 0x00000B9D
		// (set) Token: 0x06000641 RID: 1601 RVA: 0x000029A5 File Offset: 0x00000BA5
		[DataMember]
		public int StudentConfidentialityAgreementId { get; set; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000642 RID: 1602 RVA: 0x000029AE File Offset: 0x00000BAE
		// (set) Token: 0x06000643 RID: 1603 RVA: 0x000029B6 File Offset: 0x00000BB6
		[DataMember]
		public DateTime SignedOn { get; set; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000644 RID: 1604 RVA: 0x000029BF File Offset: 0x00000BBF
		// (set) Token: 0x06000645 RID: 1605 RVA: 0x000029C7 File Offset: 0x00000BC7
		[DataMember]
		public PersonBaseDTO Student { get; set; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000646 RID: 1606 RVA: 0x000029D0 File Offset: 0x00000BD0
		// (set) Token: 0x06000647 RID: 1607 RVA: 0x000029D8 File Offset: 0x00000BD8
		[DataMember]
		public eClockWorkModules ModuleName { get; set; }
	}
}
