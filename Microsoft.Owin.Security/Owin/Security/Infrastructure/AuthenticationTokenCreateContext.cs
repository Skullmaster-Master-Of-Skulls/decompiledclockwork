using System;
using Microsoft.Owin.Security.Provider;

namespace Microsoft.Owin.Security.Infrastructure
{
	// Token: 0x02000019 RID: 25
	public class AuthenticationTokenCreateContext : BaseContext
	{
		// Token: 0x06000051 RID: 81 RVA: 0x00002E82 File Offset: 0x00001082
		public AuthenticationTokenCreateContext(IOwinContext context, ISecureDataFormat<AuthenticationTicket> secureDataFormat, AuthenticationTicket ticket) : base(context)
		{
			if (secureDataFormat == null)
			{
				throw new ArgumentNullException("secureDataFormat");
			}
			if (ticket == null)
			{
				throw new ArgumentNullException("ticket");
			}
			this._secureDataFormat = secureDataFormat;
			this.Ticket = ticket;
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00002EB5 File Offset: 0x000010B5
		// (set) Token: 0x06000053 RID: 83 RVA: 0x00002EBD File Offset: 0x000010BD
		public string Token { get; protected set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00002EC6 File Offset: 0x000010C6
		// (set) Token: 0x06000055 RID: 85 RVA: 0x00002ECE File Offset: 0x000010CE
		public AuthenticationTicket Ticket { get; protected set; }

		// Token: 0x06000056 RID: 86 RVA: 0x00002ED7 File Offset: 0x000010D7
		public string SerializeTicket()
		{
			return this._secureDataFormat.Protect(this.Ticket);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002EEA File Offset: 0x000010EA
		public void SetToken(string tokenValue)
		{
			if (tokenValue == null)
			{
				throw new ArgumentNullException("tokenValue");
			}
			this.Token = tokenValue;
		}

		// Token: 0x04000020 RID: 32
		private readonly ISecureDataFormat<AuthenticationTicket> _secureDataFormat;
	}
}
