using System;

namespace System.ServiceModel
{
	// Token: 0x02000154 RID: 340
	internal static class TcpClientCredentialTypeHelper
	{
		// Token: 0x060009D4 RID: 2516 RVA: 0x000261CB File Offset: 0x000243CB
		internal static bool IsDefined(TcpClientCredentialType value)
		{
			return value == TcpClientCredentialType.None || value == TcpClientCredentialType.Windows || value == TcpClientCredentialType.Certificate;
		}
	}
}
