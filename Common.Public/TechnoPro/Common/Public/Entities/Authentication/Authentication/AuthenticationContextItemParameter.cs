using System;

namespace TechnoPro.Common.Public.Entities.Authentication.Authentication
{
	// Token: 0x0200049F RID: 1183
	public class AuthenticationContextItemParameter
	{
		// Token: 0x060023AA RID: 9130 RVA: 0x0000D55A File Offset: 0x0000B75A
		public AuthenticationContextItemParameter()
		{
		}

		// Token: 0x060023AB RID: 9131 RVA: 0x00027186 File Offset: 0x00025386
		public AuthenticationContextItemParameter(eAuthenticationContextItemParameter p, bool isRequired = false)
		{
			this.Parameter = p;
			this.IsRequired = isRequired;
		}

		// Token: 0x17000EBB RID: 3771
		// (get) Token: 0x060023AC RID: 9132 RVA: 0x000271A0 File Offset: 0x000253A0
		// (set) Token: 0x060023AD RID: 9133 RVA: 0x000271A8 File Offset: 0x000253A8
		public eAuthenticationContextItemParameter Parameter { get; set; }

		// Token: 0x17000EBC RID: 3772
		// (get) Token: 0x060023AE RID: 9134 RVA: 0x000271B1 File Offset: 0x000253B1
		// (set) Token: 0x060023AF RID: 9135 RVA: 0x000271B9 File Offset: 0x000253B9
		public bool IsRequired { get; set; }
	}
}
