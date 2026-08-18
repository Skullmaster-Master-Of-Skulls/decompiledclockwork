using System;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Web.Security.Credentials;

namespace TechnoPro.Common.Web.Security.Proxy
{
	// Token: 0x02000009 RID: 9
	public abstract class BearerTokenRestProxy<T> : RestProxy<T>, IWebService where T : IWebService
	{
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000066 RID: 102 RVA: 0x000034C8 File Offset: 0x000016C8
		// (set) Token: 0x06000067 RID: 103 RVA: 0x000034D0 File Offset: 0x000016D0
		protected override string DefaultAuthenticationMethod { get; set; } = "Bearer";

		// Token: 0x06000068 RID: 104 RVA: 0x000034D9 File Offset: 0x000016D9
		protected BearerTokenRestProxy(string serviceAddress, string token = null) : base(serviceAddress, "Bearer")
		{
			this.ClientCredentials.Token = token;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000034FE File Offset: 0x000016FE
		protected BearerTokenRestProxy(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, "Bearer")
		{
			this.ClientCredentials.Token = token;
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00003524 File Offset: 0x00001724
		// (set) Token: 0x0600006B RID: 107 RVA: 0x00003559 File Offset: 0x00001759
		public new TokenUserCredentials ClientCredentials
		{
			get
			{
				TokenUserCredentials result;
				if ((result = (this._userSecCredentials as TokenUserCredentials)) == null)
				{
					result = (TokenUserCredentials)(this._userSecCredentials = ObjectFactory.Resolve<IUserCredentials>(this.DefaultAuthenticationMethod));
				}
				return result;
			}
			set
			{
				this._userSecCredentials = value;
			}
		}
	}
}
