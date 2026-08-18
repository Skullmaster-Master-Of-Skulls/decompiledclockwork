using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProviderOriginal
{
	// Token: 0x020002DD RID: 733
	[DataContract(Namespace = "http://tpro.ca")]
	public class ServiceProviderDTO : ServiceProviderBaseDTO
	{
		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x06001095 RID: 4245 RVA: 0x00007B38 File Offset: 0x00005D38
		// (set) Token: 0x06001096 RID: 4246 RVA: 0x00007B40 File Offset: 0x00005D40
		[DataMember]
		public string AdditionalServices { get; set; }

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x06001097 RID: 4247 RVA: 0x00007B49 File Offset: 0x00005D49
		// (set) Token: 0x06001098 RID: 4248 RVA: 0x00007B51 File Offset: 0x00005D51
		[DataMember]
		public string Specialization { get; set; }

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x06001099 RID: 4249 RVA: 0x00007B5A File Offset: 0x00005D5A
		// (set) Token: 0x0600109A RID: 4250 RVA: 0x00007B62 File Offset: 0x00005D62
		[DataMember]
		public string Notes1 { get; set; }

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x0600109B RID: 4251 RVA: 0x00007B6B File Offset: 0x00005D6B
		// (set) Token: 0x0600109C RID: 4252 RVA: 0x00007B73 File Offset: 0x00005D73
		[DataMember]
		public string Notes2 { get; set; }

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x0600109D RID: 4253 RVA: 0x00007B7C File Offset: 0x00005D7C
		// (set) Token: 0x0600109E RID: 4254 RVA: 0x00007B84 File Offset: 0x00005D84
		[DataMember]
		public string Phone1 { get; set; }

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x0600109F RID: 4255 RVA: 0x00007B8D File Offset: 0x00005D8D
		// (set) Token: 0x060010A0 RID: 4256 RVA: 0x00007B95 File Offset: 0x00005D95
		[DataMember]
		public string Phone2 { get; set; }

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x060010A1 RID: 4257 RVA: 0x00007B9E File Offset: 0x00005D9E
		// (set) Token: 0x060010A2 RID: 4258 RVA: 0x00007BA6 File Offset: 0x00005DA6
		[DataMember]
		public string PhoneNote { get; set; }

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x060010A3 RID: 4259 RVA: 0x00007BAF File Offset: 0x00005DAF
		// (set) Token: 0x060010A4 RID: 4260 RVA: 0x00007BB7 File Offset: 0x00005DB7
		[DataMember]
		public string Address { get; set; }

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x060010A5 RID: 4261 RVA: 0x00007BC0 File Offset: 0x00005DC0
		// (set) Token: 0x060010A6 RID: 4262 RVA: 0x00007BC8 File Offset: 0x00005DC8
		[DataMember]
		public DateTime DateEntered { get; set; }

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x060010A7 RID: 4263 RVA: 0x00007BD1 File Offset: 0x00005DD1
		// (set) Token: 0x060010A8 RID: 4264 RVA: 0x00007BD9 File Offset: 0x00005DD9
		[DataMember]
		public int WhoEnteredPersonId { get; set; }

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x060010A9 RID: 4265 RVA: 0x00007BE2 File Offset: 0x00005DE2
		// (set) Token: 0x060010AA RID: 4266 RVA: 0x00007BEA File Offset: 0x00005DEA
		[DataMember]
		public bool IsActive { get; set; }

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x060010AB RID: 4267 RVA: 0x00007BF3 File Offset: 0x00005DF3
		// (set) Token: 0x060010AC RID: 4268 RVA: 0x00007BFB File Offset: 0x00005DFB
		[DataMember]
		public string IsActiveNote { get; set; }

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x060010AD RID: 4269 RVA: 0x00007C04 File Offset: 0x00005E04
		// (set) Token: 0x060010AE RID: 4270 RVA: 0x00007C0C File Offset: 0x00005E0C
		[DataMember]
		public string Address2 { get; set; }

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x060010AF RID: 4271 RVA: 0x00007C15 File Offset: 0x00005E15
		// (set) Token: 0x060010B0 RID: 4272 RVA: 0x00007C1D File Offset: 0x00005E1D
		[DataMember]
		public string Email2 { get; set; }

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x060010B1 RID: 4273 RVA: 0x00007C26 File Offset: 0x00005E26
		// (set) Token: 0x060010B2 RID: 4274 RVA: 0x00007C2E File Offset: 0x00005E2E
		[DataMember]
		public bool AddressActive { get; set; }

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x060010B3 RID: 4275 RVA: 0x00007C37 File Offset: 0x00005E37
		// (set) Token: 0x060010B4 RID: 4276 RVA: 0x00007C3F File Offset: 0x00005E3F
		[DataMember]
		public bool Address2Active { get; set; }
	}
}
