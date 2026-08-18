using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Authentication.Authentication
{
	// Token: 0x020008EC RID: 2284
	[DataContract(Namespace = "http://tpro.ca")]
	public class AuthenticationArgsDTO
	{
		// Token: 0x06002E75 RID: 11893 RVA: 0x00015FC3 File Offset: 0x000141C3
		public AuthenticationArgsDTO()
		{
			this.SecureArgs = new Dictionary<string, string>();
			this.InsecureArgs = new Dictionary<string, string>();
		}

		// Token: 0x17001071 RID: 4209
		// (get) Token: 0x06002E76 RID: 11894 RVA: 0x00015FE5 File Offset: 0x000141E5
		// (set) Token: 0x06002E77 RID: 11895 RVA: 0x00015FED File Offset: 0x000141ED
		[DataMember]
		public IDictionary<string, string> SecureArgs { get; set; }

		// Token: 0x17001072 RID: 4210
		// (get) Token: 0x06002E78 RID: 11896 RVA: 0x00015FF6 File Offset: 0x000141F6
		// (set) Token: 0x06002E79 RID: 11897 RVA: 0x00015FFE File Offset: 0x000141FE
		[DataMember]
		public IDictionary<string, string> InsecureArgs { get; set; }
	}
}
