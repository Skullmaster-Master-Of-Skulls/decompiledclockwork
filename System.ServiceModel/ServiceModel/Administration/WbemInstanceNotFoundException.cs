using System;

namespace System.ServiceModel.Administration
{
	// Token: 0x02000452 RID: 1106
	internal class WbemInstanceNotFoundException : WbemException
	{
		// Token: 0x06002AF8 RID: 11000 RVA: 0x000A8899 File Offset: 0x000A6A99
		internal WbemInstanceNotFoundException() : base(WbemNative.WbemStatus.WBEM_E_NOT_FOUND)
		{
		}
	}
}
