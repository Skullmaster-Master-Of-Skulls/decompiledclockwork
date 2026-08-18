using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.OnlineForms
{
	// Token: 0x020003EC RID: 1004
	[DataContract(Namespace = "http://tpro.ca")]
	public class OnlineFormDTO
	{
		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x060015EB RID: 5611 RVA: 0x0000A36E File Offset: 0x0000856E
		// (set) Token: 0x060015EC RID: 5612 RVA: 0x0000A376 File Offset: 0x00008576
		[DataMember]
		public int OnlineFormId { get; set; }

		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x060015ED RID: 5613 RVA: 0x0000A37F File Offset: 0x0000857F
		// (set) Token: 0x060015EE RID: 5614 RVA: 0x0000A387 File Offset: 0x00008587
		[DataMember]
		public string Title { get; set; }

		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x060015EF RID: 5615 RVA: 0x0000A390 File Offset: 0x00008590
		// (set) Token: 0x060015F0 RID: 5616 RVA: 0x0000A398 File Offset: 0x00008598
		[DataMember]
		public string Description { get; set; }

		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x060015F1 RID: 5617 RVA: 0x0000A3A1 File Offset: 0x000085A1
		// (set) Token: 0x060015F2 RID: 5618 RVA: 0x0000A3A9 File Offset: 0x000085A9
		[DataMember]
		public string ShortCode { get; set; }

		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x060015F3 RID: 5619 RVA: 0x0000A3B2 File Offset: 0x000085B2
		// (set) Token: 0x060015F4 RID: 5620 RVA: 0x0000A3BA File Offset: 0x000085BA
		[DataMember]
		public DynamicFormDTO Form { get; set; }

		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x060015F5 RID: 5621 RVA: 0x0000A3C3 File Offset: 0x000085C3
		// (set) Token: 0x060015F6 RID: 5622 RVA: 0x0000A3CB File Offset: 0x000085CB
		[DataMember]
		public bool UseWizard { get; set; }

		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x060015F7 RID: 5623 RVA: 0x0000A3D4 File Offset: 0x000085D4
		// (set) Token: 0x060015F8 RID: 5624 RVA: 0x0000A3DC File Offset: 0x000085DC
		[DataMember]
		public bool RequiresLogin { get; set; }

		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x060015F9 RID: 5625 RVA: 0x0000A3E5 File Offset: 0x000085E5
		// (set) Token: 0x060015FA RID: 5626 RVA: 0x0000A3ED File Offset: 0x000085ED
		[DataMember]
		public bool CanOnlyBeFilledInOnce { get; set; }

		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x060015FB RID: 5627 RVA: 0x0000A3F6 File Offset: 0x000085F6
		// (set) Token: 0x060015FC RID: 5628 RVA: 0x0000A3FE File Offset: 0x000085FE
		[DataMember]
		public int Captcha { get; set; }

		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x060015FD RID: 5629 RVA: 0x0000A407 File Offset: 0x00008607
		// (set) Token: 0x060015FE RID: 5630 RVA: 0x0000A40F File Offset: 0x0000860F
		[DataMember]
		public int StudentEmailConfirmationTemplateId { get; set; }

		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x060015FF RID: 5631 RVA: 0x0000A418 File Offset: 0x00008618
		// (set) Token: 0x06001600 RID: 5632 RVA: 0x0000A420 File Offset: 0x00008620
		[DataMember]
		public int StaffEmailConfirmationTemplateId { get; set; }

		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x06001601 RID: 5633 RVA: 0x0000A429 File Offset: 0x00008629
		// (set) Token: 0x06001602 RID: 5634 RVA: 0x0000A431 File Offset: 0x00008631
		[DataMember]
		public string SubmitMessage { get; set; }

		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x06001603 RID: 5635 RVA: 0x0000A43A File Offset: 0x0000863A
		// (set) Token: 0x06001604 RID: 5636 RVA: 0x0000A442 File Offset: 0x00008642
		[DataMember]
		public string SubmitButtonText { get; set; }

		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x06001605 RID: 5637 RVA: 0x0000A44B File Offset: 0x0000864B
		// (set) Token: 0x06001606 RID: 5638 RVA: 0x0000A453 File Offset: 0x00008653
		[DataMember]
		public DateTime? StartDate { get; set; }

		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x06001607 RID: 5639 RVA: 0x0000A45C File Offset: 0x0000865C
		// (set) Token: 0x06001608 RID: 5640 RVA: 0x0000A464 File Offset: 0x00008664
		[DataMember]
		public DateTime? EndDate { get; set; }

		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x06001609 RID: 5641 RVA: 0x0000A46D File Offset: 0x0000866D
		// (set) Token: 0x0600160A RID: 5642 RVA: 0x0000A475 File Offset: 0x00008675
		[DataMember]
		public GroupDTO RestrictedToGroup { get; set; }

		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x0600160B RID: 5643 RVA: 0x0000A47E File Offset: 0x0000867E
		// (set) Token: 0x0600160C RID: 5644 RVA: 0x0000A486 File Offset: 0x00008686
		[DataMember]
		public bool IsDeleted { get; set; }

		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x0600160D RID: 5645 RVA: 0x0000A48F File Offset: 0x0000868F
		// (set) Token: 0x0600160E RID: 5646 RVA: 0x0000A497 File Offset: 0x00008697
		[DataMember]
		public bool IsDisabled { get; set; }

		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x0600160F RID: 5647 RVA: 0x0000A4A0 File Offset: 0x000086A0
		// (set) Token: 0x06001610 RID: 5648 RVA: 0x0000A4A8 File Offset: 0x000086A8
		[DataMember]
		public BasicPersonDTO WhoCreated { get; set; }

		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x06001611 RID: 5649 RVA: 0x0000A4B1 File Offset: 0x000086B1
		// (set) Token: 0x06001612 RID: 5650 RVA: 0x0000A4B9 File Offset: 0x000086B9
		[DataMember]
		public BasicPersonDTO WhoLastModified { get; set; }

		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x06001613 RID: 5651 RVA: 0x0000A4C2 File Offset: 0x000086C2
		// (set) Token: 0x06001614 RID: 5652 RVA: 0x0000A4CA File Offset: 0x000086CA
		[DataMember]
		public DateTime DateCreated { get; set; }

		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x06001615 RID: 5653 RVA: 0x0000A4D3 File Offset: 0x000086D3
		// (set) Token: 0x06001616 RID: 5654 RVA: 0x0000A4DB File Offset: 0x000086DB
		[DataMember]
		public DateTime? DateLastModified { get; set; }
	}
}
