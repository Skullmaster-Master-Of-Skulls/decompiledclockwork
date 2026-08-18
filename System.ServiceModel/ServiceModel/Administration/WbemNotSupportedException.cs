using System;

namespace System.ServiceModel.Administration
{
	// Token: 0x02000454 RID: 1108
	internal class WbemNotSupportedException : WbemException
	{
		// Token: 0x06002AFB RID: 11003 RVA: 0x000A88C1 File Offset: 0x000A6AC1
		internal WbemNotSupportedException() : base(WbemNative.WbemStatus.WBEM_E_NOT_SUPPORTED)
		{
		}
	}
}
