using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Surveys
{
	// Token: 0x02000201 RID: 513
	[DataContract(Namespace = "http://tpro.ca")]
	public class SurveyDTO
	{
		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000BA6 RID: 2982 RVA: 0x00005572 File Offset: 0x00003772
		// (set) Token: 0x06000BA7 RID: 2983 RVA: 0x0000557A File Offset: 0x0000377A
		[DataMember]
		public int SurveyId { get; set; }

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000BA8 RID: 2984 RVA: 0x00005583 File Offset: 0x00003783
		// (set) Token: 0x06000BA9 RID: 2985 RVA: 0x0000558B File Offset: 0x0000378B
		[DataMember]
		public string Title { get; set; }

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000BAA RID: 2986 RVA: 0x00005594 File Offset: 0x00003794
		// (set) Token: 0x06000BAB RID: 2987 RVA: 0x0000559C File Offset: 0x0000379C
		[DataMember]
		public string Description { get; set; }

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000BAC RID: 2988 RVA: 0x000055A5 File Offset: 0x000037A5
		// (set) Token: 0x06000BAD RID: 2989 RVA: 0x000055AD File Offset: 0x000037AD
		[DataMember]
		public string ShortCode { get; set; }

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000BAE RID: 2990 RVA: 0x000055B6 File Offset: 0x000037B6
		// (set) Token: 0x06000BAF RID: 2991 RVA: 0x000055BE File Offset: 0x000037BE
		[DataMember]
		public DynamicFormDTO Form { get; set; }

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000BB0 RID: 2992 RVA: 0x000055C7 File Offset: 0x000037C7
		// (set) Token: 0x06000BB1 RID: 2993 RVA: 0x000055CF File Offset: 0x000037CF
		[DataMember]
		public bool UseWizard { get; set; }

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000BB2 RID: 2994 RVA: 0x000055D8 File Offset: 0x000037D8
		// (set) Token: 0x06000BB3 RID: 2995 RVA: 0x000055E0 File Offset: 0x000037E0
		[DataMember]
		public bool RequiresLogin { get; set; }

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000BB4 RID: 2996 RVA: 0x000055E9 File Offset: 0x000037E9
		// (set) Token: 0x06000BB5 RID: 2997 RVA: 0x000055F1 File Offset: 0x000037F1
		[DataMember]
		public bool CanOnlyBeFilledInOnce { get; set; }

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000BB6 RID: 2998 RVA: 0x000055FA File Offset: 0x000037FA
		// (set) Token: 0x06000BB7 RID: 2999 RVA: 0x00005602 File Offset: 0x00003802
		[DataMember]
		public int Captcha { get; set; }

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000BB8 RID: 3000 RVA: 0x0000560B File Offset: 0x0000380B
		// (set) Token: 0x06000BB9 RID: 3001 RVA: 0x00005613 File Offset: 0x00003813
		[DataMember]
		public int StudentEmailConfirmationTemplateId { get; set; }

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000BBA RID: 3002 RVA: 0x0000561C File Offset: 0x0000381C
		// (set) Token: 0x06000BBB RID: 3003 RVA: 0x00005624 File Offset: 0x00003824
		[DataMember]
		public int StaffEmailConfirmationTemplateId { get; set; }

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000BBC RID: 3004 RVA: 0x0000562D File Offset: 0x0000382D
		// (set) Token: 0x06000BBD RID: 3005 RVA: 0x00005635 File Offset: 0x00003835
		[DataMember]
		public string SubmitMessage { get; set; }

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000BBE RID: 3006 RVA: 0x0000563E File Offset: 0x0000383E
		// (set) Token: 0x06000BBF RID: 3007 RVA: 0x00005646 File Offset: 0x00003846
		[DataMember]
		public string SubmitButtonText { get; set; }

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000BC0 RID: 3008 RVA: 0x0000564F File Offset: 0x0000384F
		// (set) Token: 0x06000BC1 RID: 3009 RVA: 0x00005657 File Offset: 0x00003857
		[DataMember]
		public DateTime? StartDate { get; set; }

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000BC2 RID: 3010 RVA: 0x00005660 File Offset: 0x00003860
		// (set) Token: 0x06000BC3 RID: 3011 RVA: 0x00005668 File Offset: 0x00003868
		[DataMember]
		public DateTime? EndDate { get; set; }

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000BC4 RID: 3012 RVA: 0x00005671 File Offset: 0x00003871
		// (set) Token: 0x06000BC5 RID: 3013 RVA: 0x00005679 File Offset: 0x00003879
		[DataMember]
		public GroupDTO RestrictedToGroup { get; set; }

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000BC6 RID: 3014 RVA: 0x00005682 File Offset: 0x00003882
		// (set) Token: 0x06000BC7 RID: 3015 RVA: 0x0000568A File Offset: 0x0000388A
		[DataMember]
		public bool IsDeleted { get; set; }

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000BC8 RID: 3016 RVA: 0x00005693 File Offset: 0x00003893
		// (set) Token: 0x06000BC9 RID: 3017 RVA: 0x0000569B File Offset: 0x0000389B
		[DataMember]
		public bool IsDisabled { get; set; }

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000BCA RID: 3018 RVA: 0x000056A4 File Offset: 0x000038A4
		// (set) Token: 0x06000BCB RID: 3019 RVA: 0x000056AC File Offset: 0x000038AC
		[DataMember]
		public BasicPersonDTO WhoCreated { get; set; }

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000BCC RID: 3020 RVA: 0x000056B5 File Offset: 0x000038B5
		// (set) Token: 0x06000BCD RID: 3021 RVA: 0x000056BD File Offset: 0x000038BD
		[DataMember]
		public BasicPersonDTO WhoLastModified { get; set; }

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000BCE RID: 3022 RVA: 0x000056C6 File Offset: 0x000038C6
		// (set) Token: 0x06000BCF RID: 3023 RVA: 0x000056CE File Offset: 0x000038CE
		[DataMember]
		public DateTime DateCreated { get; set; }

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000BD0 RID: 3024 RVA: 0x000056D7 File Offset: 0x000038D7
		// (set) Token: 0x06000BD1 RID: 3025 RVA: 0x000056DF File Offset: 0x000038DF
		[DataMember]
		public DateTime? DateLastModified { get; set; }
	}
}
