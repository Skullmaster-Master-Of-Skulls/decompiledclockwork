using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider
{
	// Token: 0x02000270 RID: 624
	[DataContract(Namespace = "http://tpro.ca")]
	public class SPApplicationCourseDTO
	{
		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06000E7E RID: 3710 RVA: 0x00006D0A File Offset: 0x00004F0A
		// (set) Token: 0x06000E7F RID: 3711 RVA: 0x00006D12 File Offset: 0x00004F12
		[DataMember]
		public int SPApplicationCourseId { get; set; }

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06000E80 RID: 3712 RVA: 0x00006D1B File Offset: 0x00004F1B
		// (set) Token: 0x06000E81 RID: 3713 RVA: 0x00006D23 File Offset: 0x00004F23
		[DataMember]
		public SPApplicationDTO Application { get; set; }

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06000E82 RID: 3714 RVA: 0x00006D2C File Offset: 0x00004F2C
		// (set) Token: 0x06000E83 RID: 3715 RVA: 0x00006D34 File Offset: 0x00004F34
		[DataMember]
		public SPProviderCourseRegistrationDTO ProviderCourseRegistration { get; set; }

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06000E84 RID: 3716 RVA: 0x00006D3D File Offset: 0x00004F3D
		// (set) Token: 0x06000E85 RID: 3717 RVA: 0x00006D45 File Offset: 0x00004F45
		[DataMember]
		public string LookupSubject { get; set; }

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06000E86 RID: 3718 RVA: 0x00006D4E File Offset: 0x00004F4E
		// (set) Token: 0x06000E87 RID: 3719 RVA: 0x00006D56 File Offset: 0x00004F56
		[DataMember]
		public string LookupCourseCode { get; set; }

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06000E88 RID: 3720 RVA: 0x00006D5F File Offset: 0x00004F5F
		// (set) Token: 0x06000E89 RID: 3721 RVA: 0x00006D67 File Offset: 0x00004F67
		[DataMember]
		public string LookupCourseSection { get; set; }

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06000E8A RID: 3722 RVA: 0x00006D70 File Offset: 0x00004F70
		// (set) Token: 0x06000E8B RID: 3723 RVA: 0x00006D78 File Offset: 0x00004F78
		[DataMember]
		public string LookupTimeOfDay { get; set; }
	}
}
