using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProviderOriginal
{
	// Token: 0x020002DB RID: 731
	[DataContract(Namespace = "http://tpro.ca")]
	public class ServiceProviderBaseDTO
	{
		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x06001071 RID: 4209 RVA: 0x00007A17 File Offset: 0x00005C17
		// (set) Token: 0x06001072 RID: 4210 RVA: 0x00007A1F File Offset: 0x00005C1F
		[DataMember]
		public int ServiceProviderId { get; set; }

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x06001073 RID: 4211 RVA: 0x00007A28 File Offset: 0x00005C28
		// (set) Token: 0x06001074 RID: 4212 RVA: 0x00007A30 File Offset: 0x00005C30
		[DataMember]
		public string FirstName { get; set; }

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x06001075 RID: 4213 RVA: 0x00007A39 File Offset: 0x00005C39
		// (set) Token: 0x06001076 RID: 4214 RVA: 0x00007A41 File Offset: 0x00005C41
		[DataMember]
		public string LastName { get; set; }

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x06001077 RID: 4215 RVA: 0x00007A4A File Offset: 0x00005C4A
		// (set) Token: 0x06001078 RID: 4216 RVA: 0x00007A52 File Offset: 0x00005C52
		[DataMember]
		public string MiddleName { get; set; }

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x06001079 RID: 4217 RVA: 0x00007A5B File Offset: 0x00005C5B
		// (set) Token: 0x0600107A RID: 4218 RVA: 0x00007A63 File Offset: 0x00005C63
		[DataMember]
		public string NickName { get; set; }

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x0600107B RID: 4219 RVA: 0x00007A6C File Offset: 0x00005C6C
		// (set) Token: 0x0600107C RID: 4220 RVA: 0x00007A74 File Offset: 0x00005C74
		[DataMember]
		public string StudentNumber { get; set; }

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x0600107D RID: 4221 RVA: 0x00007A7D File Offset: 0x00005C7D
		// (set) Token: 0x0600107E RID: 4222 RVA: 0x00007A85 File Offset: 0x00005C85
		[DataMember]
		public string Username { get; set; }

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x0600107F RID: 4223 RVA: 0x00007A8E File Offset: 0x00005C8E
		// (set) Token: 0x06001080 RID: 4224 RVA: 0x00007A96 File Offset: 0x00005C96
		[DataMember]
		public bool RegistrationIsComplete { get; set; }

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x06001081 RID: 4225 RVA: 0x00007A9F File Offset: 0x00005C9F
		// (set) Token: 0x06001082 RID: 4226 RVA: 0x00007AA7 File Offset: 0x00005CA7
		[DataMember]
		public string Email { get; set; }
	}
}
