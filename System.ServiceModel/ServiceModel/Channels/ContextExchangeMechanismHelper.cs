using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x020007B0 RID: 1968
	internal static class ContextExchangeMechanismHelper
	{
		// Token: 0x06004A7A RID: 19066 RVA: 0x00111B87 File Offset: 0x0010FD87
		public static bool IsDefined(ContextExchangeMechanism value)
		{
			return value == ContextExchangeMechanism.ContextSoapHeader || value == ContextExchangeMechanism.HttpCookie;
		}
	}
}
