using System;

namespace TechnoPro.Common.Web.Security.Credentials
{
	// Token: 0x02000011 RID: 17
	public class UserNameCredentials : IClientIdCredentials, IUserCredentials
	{
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00003768 File Offset: 0x00001968
		// (set) Token: 0x06000081 RID: 129 RVA: 0x00003770 File Offset: 0x00001970
		public string UserName { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00003779 File Offset: 0x00001979
		// (set) Token: 0x06000083 RID: 131 RVA: 0x00003781 File Offset: 0x00001981
		public string Password { get; set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000084 RID: 132 RVA: 0x0000378A File Offset: 0x0000198A
		// (set) Token: 0x06000085 RID: 133 RVA: 0x00003792 File Offset: 0x00001992
		public string ClientId { get; set; }
	}
}
