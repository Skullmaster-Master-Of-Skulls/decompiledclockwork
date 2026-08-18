using System;
using System.Net.Security;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000837 RID: 2103
	internal static class AuthenticationLevelHelper
	{
		// Token: 0x06004E8C RID: 20108 RVA: 0x0011E526 File Offset: 0x0011C726
		internal static string ToString(AuthenticationLevel authenticationLevel)
		{
			if (authenticationLevel == AuthenticationLevel.MutualAuthRequested)
			{
				return "mutualAuthRequested";
			}
			if (authenticationLevel == AuthenticationLevel.MutualAuthRequired)
			{
				return "mutualAuthRequired";
			}
			if (authenticationLevel == AuthenticationLevel.None)
			{
				return "none";
			}
			return authenticationLevel.ToString();
		}
	}
}
