using System;

namespace System.ServiceModel.Administration
{
	// Token: 0x02000453 RID: 1107
	internal class WbemInvalidParameterException : WbemException
	{
		// Token: 0x06002AF9 RID: 11001 RVA: 0x000A88A6 File Offset: 0x000A6AA6
		internal WbemInvalidParameterException(string name) : base(-2147217400, name)
		{
		}

		// Token: 0x06002AFA RID: 11002 RVA: 0x000A88B4 File Offset: 0x000A6AB4
		internal WbemInvalidParameterException() : base(WbemNative.WbemStatus.WBEM_E_INVALID_PARAMETER)
		{
		}
	}
}
