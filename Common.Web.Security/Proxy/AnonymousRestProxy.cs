using System;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Web.Security.Credentials;

namespace TechnoPro.Common.Web.Security.Proxy
{
	// Token: 0x0200000B RID: 11
	public abstract class AnonymousRestProxy<T> : RestProxy<T>, IWebService where T : IWebService
	{
		// Token: 0x06000073 RID: 115 RVA: 0x00003659 File Offset: 0x00001859
		protected AnonymousRestProxy(string serviceAddress, string clientId) : base(serviceAddress)
		{
			this.ClientCredentials.ClientId = clientId;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x0000366E File Offset: 0x0000186E
		protected AnonymousRestProxy(string serviceAddress, string serviceAddressSuffix, string clientId) : base(serviceAddress, serviceAddressSuffix)
		{
			this.ClientCredentials.ClientId = clientId;
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00003684 File Offset: 0x00001884
		// (set) Token: 0x06000076 RID: 118 RVA: 0x000036B9 File Offset: 0x000018B9
		public new IClientIdCredentials ClientCredentials
		{
			get
			{
				IClientIdCredentials result;
				if ((result = (this._userSecCredentials as IClientIdCredentials)) == null)
				{
					result = (IClientIdCredentials)(this._userSecCredentials = ObjectFactory.Resolve<IUserCredentials>(this.DefaultAuthenticationMethod));
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
