using System;

namespace Renci.SshNet.Common
{
	// Token: 0x020000E8 RID: 232
	public class AuthenticationPasswordChangeEventArgs : AuthenticationEventArgs
	{
		// Token: 0x17000288 RID: 648
		// (get) Token: 0x060009BF RID: 2495 RVA: 0x000205D2 File Offset: 0x0001E7D2
		// (set) Token: 0x060009C0 RID: 2496 RVA: 0x000205DA File Offset: 0x0001E7DA
		public byte[] NewPassword { get; set; }

		// Token: 0x060009C1 RID: 2497 RVA: 0x000205E3 File Offset: 0x0001E7E3
		public AuthenticationPasswordChangeEventArgs(string username) : base(username)
		{
		}
	}
}
