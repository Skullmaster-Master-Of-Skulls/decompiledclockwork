using System;

namespace System.ServiceModel.Administration
{
	// Token: 0x02000455 RID: 1109
	internal class WbemInvalidMethodException : WbemException
	{
		// Token: 0x06002AFC RID: 11004 RVA: 0x000A88CE File Offset: 0x000A6ACE
		internal WbemInvalidMethodException() : base(WbemNative.WbemStatus.WBEM_E_INVALID_METHOD)
		{
		}
	}
}
