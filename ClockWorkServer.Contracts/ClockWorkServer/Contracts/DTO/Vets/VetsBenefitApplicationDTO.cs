using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Academic;
using TechnoPro.ClockWorkServer.Contracts.DTO.General;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Public.Entities.Vets;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Vets
{
	// Token: 0x020000FF RID: 255
	[DataContract(Namespace = "http://tpro.ca")]
	[KnownType(typeof(VetsBenefitApplicationRegistrationDTO))]
	[KnownType(typeof(VetsBenefitApplicationChapterDTO))]
	[KnownType(typeof(VetsBenefitApplicationBenAppDTO))]
	[KnownType(typeof(VetsBenefitApplicationAgreementDTO))]
	[KnownType(typeof(VetsBenefitApplicationStatusDTO))]
	public class VetsBenefitApplicationDTO
	{
		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600067C RID: 1660 RVA: 0x00002D35 File Offset: 0x00000F35
		// (set) Token: 0x0600067D RID: 1661 RVA: 0x00002D3D File Offset: 0x00000F3D
		[DataMember]
		public Guid BenefitApplicationId { get; set; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600067E RID: 1662 RVA: 0x00002D46 File Offset: 0x00000F46
		// (set) Token: 0x0600067F RID: 1663 RVA: 0x00002D4E File Offset: 0x00000F4E
		[DataMember]
		public PersonBaseDTO Student { get; set; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000680 RID: 1664 RVA: 0x00002D57 File Offset: 0x00000F57
		// (set) Token: 0x06000681 RID: 1665 RVA: 0x00002D5F File Offset: 0x00000F5F
		[DataMember]
		public SemesterDTO Semester { get; set; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000682 RID: 1666 RVA: 0x00002D68 File Offset: 0x00000F68
		// (set) Token: 0x06000683 RID: 1667 RVA: 0x00002D70 File Offset: 0x00000F70
		[DataMember]
		public int PerSemesterId { get; set; }

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000684 RID: 1668 RVA: 0x00002D79 File Offset: 0x00000F79
		// (set) Token: 0x06000685 RID: 1669 RVA: 0x00002D81 File Offset: 0x00000F81
		[DataMember]
		public VetsChapterDTO Chapter { get; set; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000686 RID: 1670 RVA: 0x00002D8A File Offset: 0x00000F8A
		// (set) Token: 0x06000687 RID: 1671 RVA: 0x00002D92 File Offset: 0x00000F92
		[DataMember]
		public bool StudentAgreed { get; set; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000688 RID: 1672 RVA: 0x00002D9B File Offset: 0x00000F9B
		// (set) Token: 0x06000689 RID: 1673 RVA: 0x00002DA3 File Offset: 0x00000FA3
		[DataMember]
		public bool BenAppCompleted { get; set; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600068A RID: 1674 RVA: 0x00002DAC File Offset: 0x00000FAC
		// (set) Token: 0x0600068B RID: 1675 RVA: 0x00002DB4 File Offset: 0x00000FB4
		[DataMember]
		public bool RegistrationCompleted { get; set; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600068C RID: 1676 RVA: 0x00002DBD File Offset: 0x00000FBD
		// (set) Token: 0x0600068D RID: 1677 RVA: 0x00002DC5 File Offset: 0x00000FC5
		[DataMember]
		public eVetsBenefitApplicationStep? PreferredStep { get; set; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600068E RID: 1678 RVA: 0x00002DCE File Offset: 0x00000FCE
		// (set) Token: 0x0600068F RID: 1679 RVA: 0x00002DD6 File Offset: 0x00000FD6
		[DataMember]
		public eVetsRequestStatus FinalStatus { get; set; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000690 RID: 1680 RVA: 0x00002DDF File Offset: 0x00000FDF
		// (set) Token: 0x06000691 RID: 1681 RVA: 0x00002DE7 File Offset: 0x00000FE7
		[DataMember]
		public eVetsBenefitApplicationStep MinPageAllow { get; set; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000692 RID: 1682 RVA: 0x00002DF0 File Offset: 0x00000FF0
		// (set) Token: 0x06000693 RID: 1683 RVA: 0x00002DF8 File Offset: 0x00000FF8
		[DataMember]
		public eVetsBenefitApplicationStep MaxPageAllow { get; set; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000694 RID: 1684 RVA: 0x00002E01 File Offset: 0x00001001
		// (set) Token: 0x06000695 RID: 1685 RVA: 0x00002E09 File Offset: 0x00001009
		[DataMember]
		public int ScreenerPersonId { get; set; }

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000696 RID: 1686 RVA: 0x00002E12 File Offset: 0x00001012
		// (set) Token: 0x06000697 RID: 1687 RVA: 0x00002E1A File Offset: 0x0000101A
		[DataMember]
		public int CertifierPersonId { get; set; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000698 RID: 1688 RVA: 0x00002E23 File Offset: 0x00001023
		// (set) Token: 0x06000699 RID: 1689 RVA: 0x00002E2B File Offset: 0x0000102B
		[DataMember]
		public Guid CurrentProgressStepId { get; set; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600069A RID: 1690 RVA: 0x00002E34 File Offset: 0x00001034
		// (set) Token: 0x0600069B RID: 1691 RVA: 0x00002E3C File Offset: 0x0000103C
		[DataMember]
		public ModificationHistoryItemBaseDTO ModificationHistoryItem { get; set; }
	}
}
