using System;

namespace TechnoPro.Common.Web.Security.Credentials
{
	// Token: 0x02000010 RID: 16
	public interface IClientIdCredentials : IUserCredentials
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600007E RID: 126
		// (set) Token: 0x0600007F RID: 127
		string ClientId { get; set; }
	}
}
