using System;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;

namespace System.ServiceModel.Security
{
	// Token: 0x02000279 RID: 633
	// (Invoke) Token: 0x06001212 RID: 4626
	internal delegate SecurityToken GetInfoCardTokenCallback(bool requiresInfoCard, CardSpacePolicyElement[] chain, SecurityTokenSerializer tokenSerializer);
}
