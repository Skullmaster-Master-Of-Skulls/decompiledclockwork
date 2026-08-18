using System;
using System.Collections.ObjectModel;
using System.IdentityModel.Policy;
using System.ServiceModel.Channels;

namespace System.ServiceModel
{
	// Token: 0x020000C3 RID: 195
	public class ServiceAuthenticationManager
	{
		// Token: 0x0600037F RID: 895 RVA: 0x000148D1 File Offset: 0x00012AD1
		public virtual ReadOnlyCollection<IAuthorizationPolicy> Authenticate(ReadOnlyCollection<IAuthorizationPolicy> authPolicy, Uri listenUri, ref Message message)
		{
			return authPolicy;
		}
	}
}
