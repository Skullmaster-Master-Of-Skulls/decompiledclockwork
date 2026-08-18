using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Surveys
{
	// Token: 0x02000202 RID: 514
	[DataContract(Namespace = "http://tpro.ca")]
	public class SurveyForDisplayDTO
	{
		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000BD3 RID: 3027 RVA: 0x000056E8 File Offset: 0x000038E8
		// (set) Token: 0x06000BD4 RID: 3028 RVA: 0x000056F0 File Offset: 0x000038F0
		[DataMember]
		public int SurveyId { get; set; }

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000BD5 RID: 3029 RVA: 0x000056F9 File Offset: 0x000038F9
		// (set) Token: 0x06000BD6 RID: 3030 RVA: 0x00005701 File Offset: 0x00003901
		[DataMember]
		public string Title { get; set; }

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000BD7 RID: 3031 RVA: 0x0000570A File Offset: 0x0000390A
		// (set) Token: 0x06000BD8 RID: 3032 RVA: 0x00005712 File Offset: 0x00003912
		[DataMember]
		public string Description { get; set; }

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000BD9 RID: 3033 RVA: 0x0000571B File Offset: 0x0000391B
		// (set) Token: 0x06000BDA RID: 3034 RVA: 0x00005723 File Offset: 0x00003923
		[DataMember]
		public string ShortCode { get; set; }

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000BDB RID: 3035 RVA: 0x0000572C File Offset: 0x0000392C
		// (set) Token: 0x06000BDC RID: 3036 RVA: 0x00005734 File Offset: 0x00003934
		[DataMember]
		public int ScreenNum { get; set; }
	}
}
