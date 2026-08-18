using System;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Authentication.ContractParameters;
using TechnoPro.Common.Core.Ldap;
using TechnoPro.Common.Core.Mappers.Authentication;
using TechnoPro.Common.ICore.Authentication;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x02000057 RID: 87
	public class LdapServiceManager : ILdap, IService
	{
		// Token: 0x06000353 RID: 851 RVA: 0x0000FBC4 File Offset: 0x0000DDC4
		public int CheckConnectivity()
		{
			return 1;
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0000FBD8 File Offset: 0x0000DDD8
		public LdapLoginResp LdapLogin(LdapLoginReq Request)
		{
			ILdapManager ldapManager = new LdapManager(Request.GetOperationContext());
			return new LdapLoginResp
			{
				LoginResult = ldapManager.LdapLogin(Request.ConnectionInfo.ToDomainObject(), Request.UserName, Request.PassWord).ToDTO()
			};
		}
	}
}
