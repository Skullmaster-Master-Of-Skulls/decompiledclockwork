using System;

namespace TechnoPro.Common.Public.Entities.Authentication.Authentication
{
	// Token: 0x0200049B RID: 1179
	public class AuthenticationRequestParameters
	{
		// Token: 0x06002389 RID: 9097 RVA: 0x00026F77 File Offset: 0x00025177
		public AuthenticationRequestParameters()
		{
			this.AuthenticationArgs = new AuthenticationArgs();
		}

		// Token: 0x0600238A RID: 9098 RVA: 0x00026F8D File Offset: 0x0002518D
		public AuthenticationRequestParameters(string un, string pwd, AuthenticationArgs args, bool verboseLoggingEnabled)
		{
			this.AuthenticationArgs = (args ?? new AuthenticationArgs());
			this.UserName = un;
			this.Password = pwd;
		}

		// Token: 0x17000EAE RID: 3758
		// (get) Token: 0x0600238B RID: 9099 RVA: 0x00026FB8 File Offset: 0x000251B8
		// (set) Token: 0x0600238C RID: 9100 RVA: 0x00026FC0 File Offset: 0x000251C0
		public AuthenticationContextItem ContextItem { get; set; }

		// Token: 0x17000EAF RID: 3759
		// (get) Token: 0x0600238D RID: 9101 RVA: 0x00026FC9 File Offset: 0x000251C9
		// (set) Token: 0x0600238E RID: 9102 RVA: 0x00026FD1 File Offset: 0x000251D1
		public string UserName { get; set; }

		// Token: 0x17000EB0 RID: 3760
		// (get) Token: 0x0600238F RID: 9103 RVA: 0x00026FDA File Offset: 0x000251DA
		// (set) Token: 0x06002390 RID: 9104 RVA: 0x00026FE2 File Offset: 0x000251E2
		public string Password { get; set; }

		// Token: 0x17000EB1 RID: 3761
		// (get) Token: 0x06002391 RID: 9105 RVA: 0x00026FEB File Offset: 0x000251EB
		// (set) Token: 0x06002392 RID: 9106 RVA: 0x00026FF3 File Offset: 0x000251F3
		public AuthenticationArgs AuthenticationArgs { get; set; }

		// Token: 0x17000EB2 RID: 3762
		// (get) Token: 0x06002393 RID: 9107 RVA: 0x00026FFC File Offset: 0x000251FC
		// (set) Token: 0x06002394 RID: 9108 RVA: 0x00027004 File Offset: 0x00025204
		public bool VerboseLoggingEnabled { get; set; }
	}
}
