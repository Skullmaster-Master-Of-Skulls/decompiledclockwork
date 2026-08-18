using System;

namespace System.ServiceModel
{
	// Token: 0x0200013B RID: 315
	internal static class MessageCredentialTypeHelper
	{
		// Token: 0x060008AB RID: 2219 RVA: 0x00022D20 File Offset: 0x00020F20
		internal static bool IsDefined(MessageCredentialType value)
		{
			return value == MessageCredentialType.None || value == MessageCredentialType.UserName || value == MessageCredentialType.Windows || value == MessageCredentialType.Certificate || value == MessageCredentialType.IssuedToken;
		}
	}
}
