using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000B9F RID: 2975
	[DataContract(Namespace = "http://tpro.ca")]
	public class ProofOfPurchaseInfoDTO
	{
		// Token: 0x1700171C RID: 5916
		// (get) Token: 0x06003EC0 RID: 16064 RVA: 0x0001EC88 File Offset: 0x0001CE88
		// (set) Token: 0x06003EC1 RID: 16065 RVA: 0x0001EC90 File Offset: 0x0001CE90
		[DataMember]
		public int ProofOfPurchaseId { get; set; }

		// Token: 0x1700171D RID: 5917
		// (get) Token: 0x06003EC2 RID: 16066 RVA: 0x0001EC99 File Offset: 0x0001CE99
		// (set) Token: 0x06003EC3 RID: 16067 RVA: 0x0001ECA1 File Offset: 0x0001CEA1
		[DataMember]
		public byte[] ProofOfPurchaseReceipt { get; set; }

		// Token: 0x1700171E RID: 5918
		// (get) Token: 0x06003EC4 RID: 16068 RVA: 0x0001ECAA File Offset: 0x0001CEAA
		// (set) Token: 0x06003EC5 RID: 16069 RVA: 0x0001ECB2 File Offset: 0x0001CEB2
		[DataMember]
		public string Notes { get; set; }

		// Token: 0x1700171F RID: 5919
		// (get) Token: 0x06003EC6 RID: 16070 RVA: 0x0001ECBB File Offset: 0x0001CEBB
		// (set) Token: 0x06003EC7 RID: 16071 RVA: 0x0001ECC3 File Offset: 0x0001CEC3
		[DataMember]
		public PersonBaseDTO WhoAcceptedProofOfPurchase { get; set; }

		// Token: 0x17001720 RID: 5920
		// (get) Token: 0x06003EC8 RID: 16072 RVA: 0x0001ECCC File Offset: 0x0001CECC
		// (set) Token: 0x06003EC9 RID: 16073 RVA: 0x0001ECD4 File Offset: 0x0001CED4
		[DataMember]
		public DateTime? WhenWasAccepted { get; set; }

		// Token: 0x17001721 RID: 5921
		// (get) Token: 0x06003ECA RID: 16074 RVA: 0x0001ECDD File Offset: 0x0001CEDD
		// (set) Token: 0x06003ECB RID: 16075 RVA: 0x0001ECE5 File Offset: 0x0001CEE5
		[DataMember]
		public Guid MediaContentUniqueId { get; set; }

		// Token: 0x17001722 RID: 5922
		// (get) Token: 0x06003ECC RID: 16076 RVA: 0x0001ECEE File Offset: 0x0001CEEE
		// (set) Token: 0x06003ECD RID: 16077 RVA: 0x0001ECF6 File Offset: 0x0001CEF6
		[DataMember]
		public int StudentPersonId { get; set; }

		// Token: 0x17001723 RID: 5923
		// (get) Token: 0x06003ECE RID: 16078 RVA: 0x0001ECFF File Offset: 0x0001CEFF
		// (set) Token: 0x06003ECF RID: 16079 RVA: 0x0001ED07 File Offset: 0x0001CF07
		[DataMember]
		public string Filename { get; set; }

		// Token: 0x17001724 RID: 5924
		// (get) Token: 0x06003ED0 RID: 16080 RVA: 0x0001ED10 File Offset: 0x0001CF10
		// (set) Token: 0x06003ED1 RID: 16081 RVA: 0x0001ED18 File Offset: 0x0001CF18
		[DataMember]
		public string Extension { get; set; }
	}
}
