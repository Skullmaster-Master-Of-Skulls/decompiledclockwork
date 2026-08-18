using System;

namespace Renci.SshNet.Common
{
	// Token: 0x020000E7 RID: 231
	public abstract class AuthenticationEventArgs : EventArgs
	{
		// Token: 0x17000287 RID: 647
		// (get) Token: 0x060009BC RID: 2492 RVA: 0x000205B2 File Offset: 0x0001E7B2
		// (set) Token: 0x060009BD RID: 2493 RVA: 0x000205BA File Offset: 0x0001E7BA
		public string Username { get; private set; }

		// Token: 0x060009BE RID: 2494 RVA: 0x000205C3 File Offset: 0x0001E7C3
		protected AuthenticationEventArgs(string username)
		{
			this.Username = username;
		}
	}
}
