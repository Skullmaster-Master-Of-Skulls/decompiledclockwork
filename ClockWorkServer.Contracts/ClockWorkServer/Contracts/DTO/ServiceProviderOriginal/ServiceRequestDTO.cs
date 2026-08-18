using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProviderOriginal
{
	// Token: 0x020002E1 RID: 737
	[DataContract(Namespace = "http://tpro.ca")]
	public class ServiceRequestDTO : ServiceRequestBaseDTO
	{
		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x060010D7 RID: 4311 RVA: 0x00007D50 File Offset: 0x00005F50
		// (set) Token: 0x060010D8 RID: 4312 RVA: 0x00007D58 File Offset: 0x00005F58
		[DataMember]
		public string DateTimeRequestTitle { get; set; }

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x060010D9 RID: 4313 RVA: 0x00007D61 File Offset: 0x00005F61
		// (set) Token: 0x060010DA RID: 4314 RVA: 0x00007D69 File Offset: 0x00005F69
		[DataMember]
		public DateTime? StartDateTimeRequest { get; set; }

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x060010DB RID: 4315 RVA: 0x00007D72 File Offset: 0x00005F72
		// (set) Token: 0x060010DC RID: 4316 RVA: 0x00007D7A File Offset: 0x00005F7A
		[DataMember]
		public DateTime? EndDateTimeRequest { get; set; }

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x060010DD RID: 4317 RVA: 0x00007D83 File Offset: 0x00005F83
		// (set) Token: 0x060010DE RID: 4318 RVA: 0x00007D8B File Offset: 0x00005F8B
		[DataMember]
		public ServiceProviderTypeDTO ProviderType { get; set; }

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x060010DF RID: 4319 RVA: 0x00007D94 File Offset: 0x00005F94
		// (set) Token: 0x060010E0 RID: 4320 RVA: 0x00007D9C File Offset: 0x00005F9C
		[DataMember]
		public DateTime? DateEntered { get; set; }

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x060010E1 RID: 4321 RVA: 0x00007DA5 File Offset: 0x00005FA5
		// (set) Token: 0x060010E2 RID: 4322 RVA: 0x00007DAD File Offset: 0x00005FAD
		[DataMember]
		public DateTime? StartDate { get; set; }

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x060010E3 RID: 4323 RVA: 0x00007DB6 File Offset: 0x00005FB6
		// (set) Token: 0x060010E4 RID: 4324 RVA: 0x00007DBE File Offset: 0x00005FBE
		[DataMember]
		public DateTime? EndDate { get; set; }

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x060010E5 RID: 4325 RVA: 0x00007DC7 File Offset: 0x00005FC7
		// (set) Token: 0x060010E6 RID: 4326 RVA: 0x00007DCF File Offset: 0x00005FCF
		[DataMember]
		public PersonBaseDTO WhoEntered { get; set; }

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x060010E7 RID: 4327 RVA: 0x00007DD8 File Offset: 0x00005FD8
		// (set) Token: 0x060010E8 RID: 4328 RVA: 0x00007DE0 File Offset: 0x00005FE0
		[DataMember]
		public ServiceProviderRequestDetailBaseDTO RequestDetailBase { get; set; }

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x060010E9 RID: 4329 RVA: 0x00007DE9 File Offset: 0x00005FE9
		// (set) Token: 0x060010EA RID: 4330 RVA: 0x00007DF1 File Offset: 0x00005FF1
		[DataMember]
		public string Notes { get; set; }

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x060010EB RID: 4331 RVA: 0x00007DFA File Offset: 0x00005FFA
		// (set) Token: 0x060010EC RID: 4332 RVA: 0x00007E02 File Offset: 0x00006002
		[DataMember]
		public bool StudentRequested { get; set; }

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x060010ED RID: 4333 RVA: 0x00007E0B File Offset: 0x0000600B
		// (set) Token: 0x060010EE RID: 4334 RVA: 0x00007E13 File Offset: 0x00006013
		[DataMember]
		public string StudentRequestedCancelNote { get; set; }

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x060010EF RID: 4335 RVA: 0x00007E1C File Offset: 0x0000601C
		// (set) Token: 0x060010F0 RID: 4336 RVA: 0x00007E24 File Offset: 0x00006024
		[DataMember]
		public DateTime? DateAssigned { get; set; }

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x060010F1 RID: 4337 RVA: 0x00007E2D File Offset: 0x0000602D
		// (set) Token: 0x060010F2 RID: 4338 RVA: 0x00007E35 File Offset: 0x00006035
		[DataMember]
		public string SpecialInstructions { get; set; }

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x060010F3 RID: 4339 RVA: 0x00007E3E File Offset: 0x0000603E
		// (set) Token: 0x060010F4 RID: 4340 RVA: 0x00007E46 File Offset: 0x00006046
		[DataMember]
		public IList<ServiceRequestPartBaseDTO> SubRequestParts { get; set; }

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x060010F5 RID: 4341 RVA: 0x00007E4F File Offset: 0x0000604F
		// (set) Token: 0x060010F6 RID: 4342 RVA: 0x00007E57 File Offset: 0x00006057
		[DataMember]
		public string PartsDescription { get; set; }

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x060010F7 RID: 4343 RVA: 0x00007E60 File Offset: 0x00006060
		// (set) Token: 0x060010F8 RID: 4344 RVA: 0x00007E68 File Offset: 0x00006068
		[DataMember]
		public bool IsActive { get; set; }

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x060010F9 RID: 4345 RVA: 0x00007E71 File Offset: 0x00006071
		// (set) Token: 0x060010FA RID: 4346 RVA: 0x00007E79 File Offset: 0x00006079
		[DataMember]
		public DateTime? DateInserted { get; set; }
	}
}
