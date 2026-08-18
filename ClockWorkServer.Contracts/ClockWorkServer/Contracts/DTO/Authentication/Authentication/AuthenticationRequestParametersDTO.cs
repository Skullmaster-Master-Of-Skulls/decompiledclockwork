using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Authentication.Authentication
{
	// Token: 0x020008EF RID: 2287
	[DataContract(Namespace = "http://tpro.ca")]
	public class AuthenticationRequestParametersDTO
	{
		// Token: 0x06002E86 RID: 11910 RVA: 0x0001605C File Offset: 0x0001425C
		public AuthenticationRequestParametersDTO()
		{
			this.AuthenticationArgs = new Dictionary<string, string>();
		}

		// Token: 0x06002E87 RID: 11911 RVA: 0x00016072 File Offset: 0x00014272
		public AuthenticationRequestParametersDTO(string un, string pwd, IDictionary<string, string> args)
		{
			this.AuthenticationArgs = (args ?? new Dictionary<string, string>());
			this.UserName = un;
			this.Password = pwd;
		}

		// Token: 0x17001078 RID: 4216
		// (get) Token: 0x06002E88 RID: 11912 RVA: 0x0001609D File Offset: 0x0001429D
		// (set) Token: 0x06002E89 RID: 11913 RVA: 0x000160A5 File Offset: 0x000142A5
		[DataMember]
		public AuthenticationContextItemDTO ContextItem { get; set; }

		// Token: 0x17001079 RID: 4217
		// (get) Token: 0x06002E8A RID: 11914 RVA: 0x000160AE File Offset: 0x000142AE
		// (set) Token: 0x06002E8B RID: 11915 RVA: 0x000160B6 File Offset: 0x000142B6
		[DataMember]
		public string UserName { get; set; }

		// Token: 0x1700107A RID: 4218
		// (get) Token: 0x06002E8C RID: 11916 RVA: 0x000160BF File Offset: 0x000142BF
		// (set) Token: 0x06002E8D RID: 11917 RVA: 0x000160C7 File Offset: 0x000142C7
		[DataMember]
		public string Password { get; set; }

		// Token: 0x1700107B RID: 4219
		// (get) Token: 0x06002E8E RID: 11918 RVA: 0x000160D0 File Offset: 0x000142D0
		// (set) Token: 0x06002E8F RID: 11919 RVA: 0x000160D8 File Offset: 0x000142D8
		[DataMember]
		public IDictionary<string, string> AuthenticationArgs { get; set; }
	}
}
