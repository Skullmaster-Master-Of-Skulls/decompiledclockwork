using System;
using System.Collections.ObjectModel;

namespace System.IdentityModel.Tokens
{
	// Token: 0x0200010E RID: 270
	public class AuthenticationContext
	{
		// Token: 0x0600076A RID: 1898 RVA: 0x0001F657 File Offset: 0x0001D857
		public AuthenticationContext()
		{
			this._authorities = new Collection<string>();
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x0600076B RID: 1899 RVA: 0x0001F66A File Offset: 0x0001D86A
		public Collection<string> Authorities
		{
			get
			{
				return this._authorities;
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x0600076C RID: 1900 RVA: 0x0001F672 File Offset: 0x0001D872
		// (set) Token: 0x0600076D RID: 1901 RVA: 0x0001F67A File Offset: 0x0001D87A
		public string ContextClass
		{
			get
			{
				return this._contextClass;
			}
			set
			{
				this._contextClass = value;
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x0600076E RID: 1902 RVA: 0x0001F683 File Offset: 0x0001D883
		// (set) Token: 0x0600076F RID: 1903 RVA: 0x0001F68B File Offset: 0x0001D88B
		public string ContextDeclaration
		{
			get
			{
				return this._contextDeclaration;
			}
			set
			{
				this._contextDeclaration = value;
			}
		}

		// Token: 0x04000AAC RID: 2732
		private Collection<string> _authorities;

		// Token: 0x04000AAD RID: 2733
		private string _contextClass;

		// Token: 0x04000AAE RID: 2734
		private string _contextDeclaration;
	}
}
