using System;
using Microsoft.Owin.Security.Provider;

namespace Microsoft.Owin.Security.Infrastructure
{
	// Token: 0x02000016 RID: 22
	public class AuthenticationTokenReceiveContext : BaseContext
	{
		// Token: 0x06000039 RID: 57 RVA: 0x00002A77 File Offset: 0x00000C77
		public AuthenticationTokenReceiveContext(IOwinContext context, ISecureDataFormat<AuthenticationTicket> secureDataFormat, string token) : base(context)
		{
			if (secureDataFormat == null)
			{
				throw new ArgumentNullException("secureDataFormat");
			}
			if (token == null)
			{
				throw new ArgumentNullException("token");
			}
			this._secureDataFormat = secureDataFormat;
			this.Token = token;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00002AAA File Offset: 0x00000CAA
		// (set) Token: 0x0600003B RID: 59 RVA: 0x00002AB2 File Offset: 0x00000CB2
		public string Token { get; protected set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002ABB File Offset: 0x00000CBB
		// (set) Token: 0x0600003D RID: 61 RVA: 0x00002AC3 File Offset: 0x00000CC3
		public AuthenticationTicket Ticket { get; protected set; }

		// Token: 0x0600003E RID: 62 RVA: 0x00002ACC File Offset: 0x00000CCC
		public void DeserializeTicket(string protectedData)
		{
			this.Ticket = this._secureDataFormat.Unprotect(protectedData);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002AE0 File Offset: 0x00000CE0
		public void SetTicket(AuthenticationTicket ticket)
		{
			if (ticket == null)
			{
				throw new ArgumentNullException("ticket");
			}
			this.Ticket = ticket;
		}

		// Token: 0x04000019 RID: 25
		private readonly ISecureDataFormat<AuthenticationTicket> _secureDataFormat;
	}
}
