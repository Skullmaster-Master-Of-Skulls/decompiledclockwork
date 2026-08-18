using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Tasks
{
	// Token: 0x020001E3 RID: 483
	[DataContract(Namespace = "http://tpro.ca")]
	public class TaskDTO
	{
		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000AE8 RID: 2792 RVA: 0x00005011 File Offset: 0x00003211
		// (set) Token: 0x06000AE9 RID: 2793 RVA: 0x00005019 File Offset: 0x00003219
		[DataMember]
		public int TaskId { get; set; }

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000AEA RID: 2794 RVA: 0x00005022 File Offset: 0x00003222
		// (set) Token: 0x06000AEB RID: 2795 RVA: 0x0000502A File Offset: 0x0000322A
		[DataMember]
		public PersonBaseDTO Owner { get; set; }

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000AEC RID: 2796 RVA: 0x00005033 File Offset: 0x00003233
		// (set) Token: 0x06000AED RID: 2797 RVA: 0x0000503B File Offset: 0x0000323B
		[DataMember]
		public string Title { get; set; }

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000AEE RID: 2798 RVA: 0x00005044 File Offset: 0x00003244
		// (set) Token: 0x06000AEF RID: 2799 RVA: 0x0000504C File Offset: 0x0000324C
		[DataMember]
		public string Description { get; set; }

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000AF0 RID: 2800 RVA: 0x00005055 File Offset: 0x00003255
		// (set) Token: 0x06000AF1 RID: 2801 RVA: 0x0000505D File Offset: 0x0000325D
		[DataMember]
		public DateTime? DueDate { get; set; }

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000AF2 RID: 2802 RVA: 0x00005066 File Offset: 0x00003266
		// (set) Token: 0x06000AF3 RID: 2803 RVA: 0x0000506E File Offset: 0x0000326E
		[DataMember]
		public bool IsCompleted { get; set; }

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000AF4 RID: 2804 RVA: 0x00005077 File Offset: 0x00003277
		// (set) Token: 0x06000AF5 RID: 2805 RVA: 0x0000507F File Offset: 0x0000327F
		[DataMember]
		public int IconId { get; set; }

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000AF6 RID: 2806 RVA: 0x00005088 File Offset: 0x00003288
		// (set) Token: 0x06000AF7 RID: 2807 RVA: 0x00005090 File Offset: 0x00003290
		[DataMember]
		public int OrderNum { get; set; }

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000AF8 RID: 2808 RVA: 0x00005099 File Offset: 0x00003299
		// (set) Token: 0x06000AF9 RID: 2809 RVA: 0x000050A1 File Offset: 0x000032A1
		[DataMember]
		public DateTime? Reminder { get; set; }

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000AFA RID: 2810 RVA: 0x000050AA File Offset: 0x000032AA
		// (set) Token: 0x06000AFB RID: 2811 RVA: 0x000050B2 File Offset: 0x000032B2
		[DataMember]
		public TaskGroupDTO TaskGroup { get; set; }

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000AFC RID: 2812 RVA: 0x000050BB File Offset: 0x000032BB
		// (set) Token: 0x06000AFD RID: 2813 RVA: 0x000050C3 File Offset: 0x000032C3
		[DataMember]
		public int Progress { get; set; }

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000AFE RID: 2814 RVA: 0x000050CC File Offset: 0x000032CC
		// (set) Token: 0x06000AFF RID: 2815 RVA: 0x000050D4 File Offset: 0x000032D4
		[DataMember]
		public int Priority { get; set; }

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000B00 RID: 2816 RVA: 0x000050DD File Offset: 0x000032DD
		// (set) Token: 0x06000B01 RID: 2817 RVA: 0x000050E5 File Offset: 0x000032E5
		[DataMember]
		public int? OverrideColourArgb { get; set; }

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000B02 RID: 2818 RVA: 0x000050EE File Offset: 0x000032EE
		// (set) Token: 0x06000B03 RID: 2819 RVA: 0x000050F6 File Offset: 0x000032F6
		[DataMember]
		public DateTime DateEntered { get; set; }

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000B04 RID: 2820 RVA: 0x000050FF File Offset: 0x000032FF
		// (set) Token: 0x06000B05 RID: 2821 RVA: 0x00005107 File Offset: 0x00003307
		[DataMember]
		public PersonBaseDTO WhoEntered { get; set; }

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000B06 RID: 2822 RVA: 0x00005110 File Offset: 0x00003310
		// (set) Token: 0x06000B07 RID: 2823 RVA: 0x00005118 File Offset: 0x00003318
		[DataMember]
		public DateTime? DateLastModified { get; set; }

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000B08 RID: 2824 RVA: 0x00005121 File Offset: 0x00003321
		// (set) Token: 0x06000B09 RID: 2825 RVA: 0x00005129 File Offset: 0x00003329
		[DataMember]
		public PersonBaseDTO WhoLastModified { get; set; }

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000B0A RID: 2826 RVA: 0x00005132 File Offset: 0x00003332
		// (set) Token: 0x06000B0B RID: 2827 RVA: 0x0000513A File Offset: 0x0000333A
		[DataMember]
		public int? PrimaryTaskId { get; set; }

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000B0C RID: 2828 RVA: 0x00005143 File Offset: 0x00003343
		// (set) Token: 0x06000B0D RID: 2829 RVA: 0x0000514B File Offset: 0x0000334B
		[DataMember]
		public List<TaskNoteDTO> Notes { get; set; }

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000B0E RID: 2830 RVA: 0x00005154 File Offset: 0x00003354
		// (set) Token: 0x06000B0F RID: 2831 RVA: 0x0000515C File Offset: 0x0000335C
		[DataMember]
		public List<TaskClientDTO> Clients { get; set; }

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000B10 RID: 2832 RVA: 0x00005165 File Offset: 0x00003365
		// (set) Token: 0x06000B11 RID: 2833 RVA: 0x0000516D File Offset: 0x0000336D
		[DataMember]
		public bool IsPrivate { get; set; }
	}
}
