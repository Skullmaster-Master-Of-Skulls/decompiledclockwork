using System;

namespace Microsoft.Owin.Security
{
	// Token: 0x02000004 RID: 4
	public abstract class AuthenticationOptions
	{
		// Token: 0x06000003 RID: 3 RVA: 0x00002149 File Offset: 0x00000349
		protected AuthenticationOptions(string authenticationType)
		{
			this.Description = new AuthenticationDescription();
			this.AuthenticationType = authenticationType;
			this.AuthenticationMode = AuthenticationMode.Active;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000004 RID: 4 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06000005 RID: 5 RVA: 0x00002172 File Offset: 0x00000372
		public string AuthenticationType
		{
			get
			{
				return this._authenticationType;
			}
			set
			{
				this._authenticationType = value;
				this.Description.AuthenticationType = value;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000006 RID: 6 RVA: 0x00002187 File Offset: 0x00000387
		// (set) Token: 0x06000007 RID: 7 RVA: 0x0000218F File Offset: 0x0000038F
		public AuthenticationMode AuthenticationMode { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002198 File Offset: 0x00000398
		// (set) Token: 0x06000009 RID: 9 RVA: 0x000021A0 File Offset: 0x000003A0
		public AuthenticationDescription Description { get; set; }

		// Token: 0x04000004 RID: 4
		private string _authenticationType;
	}
}
