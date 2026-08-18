using System;

namespace System.ServiceModel
{
	// Token: 0x0200012F RID: 303
	internal static class BasicHttpMessageCredentialTypeHelper
	{
		// Token: 0x06000851 RID: 2129 RVA: 0x00021F2B File Offset: 0x0002012B
		internal static bool IsDefined(BasicHttpMessageCredentialType value)
		{
			return value == BasicHttpMessageCredentialType.UserName || value == BasicHttpMessageCredentialType.Certificate;
		}
	}
}
