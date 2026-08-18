using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.Authentication.Authentication
{
	// Token: 0x0200049C RID: 1180
	public class AuthenticationResultParameters
	{
		// Token: 0x17000EB3 RID: 3763
		// (get) Token: 0x06002395 RID: 9109 RVA: 0x0002700D File Offset: 0x0002520D
		// (set) Token: 0x06002396 RID: 9110 RVA: 0x00027015 File Offset: 0x00025215
		public ExternalUserInfo ExternalUserInfo { get; set; }

		// Token: 0x17000EB4 RID: 3764
		// (get) Token: 0x06002397 RID: 9111 RVA: 0x0002701E File Offset: 0x0002521E
		// (set) Token: 0x06002398 RID: 9112 RVA: 0x00027026 File Offset: 0x00025226
		public bool IsSuccess { get; set; }

		// Token: 0x17000EB5 RID: 3765
		// (get) Token: 0x06002399 RID: 9113 RVA: 0x0002702F File Offset: 0x0002522F
		// (set) Token: 0x0600239A RID: 9114 RVA: 0x00027037 File Offset: 0x00025237
		public IDictionary<string, string> Args { get; set; }

		// Token: 0x17000EB6 RID: 3766
		// (get) Token: 0x0600239B RID: 9115 RVA: 0x00027040 File Offset: 0x00025240
		// (set) Token: 0x0600239C RID: 9116 RVA: 0x00027048 File Offset: 0x00025248
		public string LoggingMessage { get; set; }
	}
}
