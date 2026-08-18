using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider
{
	// Token: 0x0200026D RID: 621
	[DataContract(Namespace = "http://tpro.ca")]
	public class SPApplicationDTO
	{
		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06000E4F RID: 3663 RVA: 0x00006B94 File Offset: 0x00004D94
		// (set) Token: 0x06000E50 RID: 3664 RVA: 0x00006B9C File Offset: 0x00004D9C
		[DataMember]
		public int SPApplicationId { get; set; }

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06000E51 RID: 3665 RVA: 0x00006BA5 File Offset: 0x00004DA5
		// (set) Token: 0x06000E52 RID: 3666 RVA: 0x00006BAD File Offset: 0x00004DAD
		[DataMember]
		public SPProviderDTO Provider { get; set; }

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06000E53 RID: 3667 RVA: 0x00006BB6 File Offset: 0x00004DB6
		// (set) Token: 0x06000E54 RID: 3668 RVA: 0x00006BBE File Offset: 0x00004DBE
		[DataMember]
		public SPProviderTypeDTO ProviderType { get; set; }

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06000E55 RID: 3669 RVA: 0x00006BC7 File Offset: 0x00004DC7
		// (set) Token: 0x06000E56 RID: 3670 RVA: 0x00006BCF File Offset: 0x00004DCF
		[DataMember]
		public SPApplicationAvailabilityTypeDTO ApplicationAvailabilityType { get; set; }

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06000E57 RID: 3671 RVA: 0x00006BD8 File Offset: 0x00004DD8
		// (set) Token: 0x06000E58 RID: 3672 RVA: 0x00006BE0 File Offset: 0x00004DE0
		[DataMember]
		public string Note1 { get; set; }

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06000E59 RID: 3673 RVA: 0x00006BE9 File Offset: 0x00004DE9
		// (set) Token: 0x06000E5A RID: 3674 RVA: 0x00006BF1 File Offset: 0x00004DF1
		[DataMember]
		public string Note2 { get; set; }

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06000E5B RID: 3675 RVA: 0x00006BFA File Offset: 0x00004DFA
		// (set) Token: 0x06000E5C RID: 3676 RVA: 0x00006C02 File Offset: 0x00004E02
		[DataMember]
		public DateTime DateEntered { get; set; }

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06000E5D RID: 3677 RVA: 0x00006C0B File Offset: 0x00004E0B
		// (set) Token: 0x06000E5E RID: 3678 RVA: 0x00006C13 File Offset: 0x00004E13
		[DataMember]
		public PersonBaseDTO WhoEntered { get; set; }

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06000E5F RID: 3679 RVA: 0x00006C1C File Offset: 0x00004E1C
		// (set) Token: 0x06000E60 RID: 3680 RVA: 0x00006C24 File Offset: 0x00004E24
		[DataMember]
		public bool IsActive { get; set; }

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06000E61 RID: 3681 RVA: 0x00006C2D File Offset: 0x00004E2D
		// (set) Token: 0x06000E62 RID: 3682 RVA: 0x00006C35 File Offset: 0x00004E35
		[DataMember]
		public float RateOfPay { get; set; }

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06000E63 RID: 3683 RVA: 0x00006C3E File Offset: 0x00004E3E
		// (set) Token: 0x06000E64 RID: 3684 RVA: 0x00006C46 File Offset: 0x00004E46
		[DataMember]
		public SPRateOfPayTypeDTO RateOfPayType { get; set; }
	}
}
